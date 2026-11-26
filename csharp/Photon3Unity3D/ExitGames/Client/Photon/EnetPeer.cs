using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExitGames.Client.Photon;

internal class EnetPeer : PeerBase
{
	private const int CRC_LENGTH = 4;

	private static readonly int HMAC_SIZE = 32;

	private static readonly int BLOCK_SIZE = 16;

	private static readonly int IV_SIZE = 16;

	private const int EncryptedDataGramHeaderSize = 7;

	private const int EncryptedHeaderSize = 5;

	private List<NCommand> sentReliableCommands = new List<NCommand>();

	private StreamBuffer outgoingAcknowledgementsPool;

	internal const int UnsequencedWindowSize = 128;

	internal readonly int[] unsequencedWindow = new int[4];

	internal int outgoingUnsequencedGroupNumber;

	internal int incomingUnsequencedGroupNumber;

	private byte udpCommandCount;

	private byte[] udpBuffer;

	private int udpBufferIndex;

	private int udpBufferLength;

	private byte[] bufferForEncryption;

	internal int commandBufferSize = 100;

	internal int challenge;

	internal int reliableCommandsRepeated;

	internal int reliableCommandsSent;

	internal int serverSentTime;

	internal static readonly byte[] udpHeader0xF3 = new byte[2] { 243, 2 };

	internal static readonly byte[] messageHeader = udpHeader0xF3;

	protected bool datagramEncryptedConnection;

	private EnetChannel[] channelArray = new EnetChannel[0];

	private const byte ControlChannelNumber = byte.MaxValue;

	protected internal const short PeerIdForConnect = -1;

	protected internal const short PeerIdForConnectTrace = -2;

	private Queue<int> commandsToRemove = new Queue<int>();

	private int fragmentLength = 0;

	private int fragmentLengthMtuValue = 0;

	private Queue<NCommand> commandsToResend = new Queue<NCommand>();

	private Queue<NCommand> CommandQueue = new Queue<NCommand>();

	internal override int QueuedIncomingCommandsCount
	{
		get
		{
			int num = 0;
			lock (channelArray)
			{
				for (int i = 0; i < channelArray.Length; i++)
				{
					EnetChannel enetChannel = channelArray[i];
					num += enetChannel.incomingReliableCommandsList.Count;
					num += enetChannel.incomingUnreliableCommandsList.Count;
				}
			}
			return num;
		}
	}

	internal override int QueuedOutgoingCommandsCount
	{
		get
		{
			int num = 0;
			lock (channelArray)
			{
				for (int i = 0; i < channelArray.Length; i++)
				{
					EnetChannel enetChannel = channelArray[i];
					num += enetChannel.outgoingReliableCommandsList.Count;
					num += enetChannel.outgoingUnreliableCommandsList.Count;
				}
			}
			return num;
		}
	}

	internal EnetPeer()
	{
		TrafficPackageHeaderSize = 12;
	}

	internal override void InitPeerBase()
	{
		base.InitPeerBase();
		if (photonPeer.PayloadEncryptionSecret != null && usedTransportProtocol == ConnectionProtocol.Udp)
		{
			InitEncryption(photonPeer.PayloadEncryptionSecret);
		}
		if (photonPeer.encryptor != null && photonPeer.decryptor != null)
		{
			isEncryptionAvailable = true;
		}
		peerID = (short)(photonPeer.EnableServerTracing ? (-2) : (-1));
		challenge = SupportClass.ThreadSafeRandom.Next();
		if (udpBuffer == null || udpBuffer.Length != mtu)
		{
			udpBuffer = new byte[mtu];
		}
		reliableCommandsSent = 0;
		reliableCommandsRepeated = 0;
		lock (channelArray)
		{
			EnetChannel[] array = channelArray;
			if (array.Length != ChannelCount + 1)
			{
				array = new EnetChannel[ChannelCount + 1];
			}
			for (byte b = 0; b < ChannelCount; b++)
			{
				array[b] = new EnetChannel(b, commandBufferSize);
			}
			array[ChannelCount] = new EnetChannel(byte.MaxValue, commandBufferSize);
			channelArray = array;
		}
		lock (sentReliableCommands)
		{
			sentReliableCommands = new List<NCommand>(commandBufferSize);
		}
		outgoingAcknowledgementsPool = new StreamBuffer();
		CommandLogInit();
	}

	internal void ApplyRandomizedSequenceNumbers()
	{
		if (!photonPeer.randomizeSequenceNumbers)
		{
			return;
		}
		lock (channelArray)
		{
			EnetChannel[] array = channelArray;
			foreach (EnetChannel enetChannel in array)
			{
				int num = photonPeer.sequenceNumberSource[enetChannel.ChannelNumber % photonPeer.sequenceNumberSource.Length];
				string debugReturn = $"Channel {enetChannel.ChannelNumber} seqNr in: {enetChannel.incomingReliableSequenceNumber} out: {enetChannel.outgoingReliableSequenceNumber}. randomize value: {num}";
				EnqueueDebugReturn(DebugLevel.INFO, debugReturn);
				enetChannel.incomingReliableSequenceNumber = num;
				if (photonPeer.randomizeSequenceNumbersWillSetOutgoingValue)
				{
					enetChannel.outgoingReliableSequenceNumber = num;
				}
				else
				{
					enetChannel.outgoingReliableSequenceNumber += num;
				}
				enetChannel.outgoingReliableUnsequencedNumber = num;
			}
		}
	}

	internal override bool Connect(string ipport, string appID, object custom = null)
	{
		if (peerConnectionState != ConnectionStateValue.Disconnected)
		{
			Listener.DebugReturn(DebugLevel.WARNING, "Connect() can't be called if peer is not Disconnected. Not connecting. peerConnectionState: " + peerConnectionState);
			return false;
		}
		if ((int)debugOut >= 5)
		{
			Listener.DebugReturn(DebugLevel.ALL, "Connect()");
		}
		ServerAddress = ipport;
		InitPeerBase();
		if (SocketImplementation != null)
		{
			PhotonSocket = (IPhotonSocket)Activator.CreateInstance(SocketImplementation, this);
		}
		else
		{
			PhotonSocket = new SocketUdp(this);
		}
		if (PhotonSocket == null)
		{
			Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed, because SocketImplementation or socket was null. Set PhotonPeer.SocketImplementation before Connect().");
			return false;
		}
		if (PhotonSocket.Connect())
		{
			if (TrafficStatsEnabled)
			{
				TrafficStatsOutgoing.ControlCommandBytes += 44;
				TrafficStatsOutgoing.ControlCommandCount++;
			}
			peerConnectionState = ConnectionStateValue.Connecting;
			return true;
		}
		return false;
	}

	public override void OnConnect()
	{
		QueueOutgoingReliableCommand(new NCommand(this, 2, null, byte.MaxValue));
	}

	internal override void Disconnect()
	{
		if (peerConnectionState == ConnectionStateValue.Disconnected || peerConnectionState == ConnectionStateValue.Disconnecting)
		{
			return;
		}
		if (sentReliableCommands != null)
		{
			lock (sentReliableCommands)
			{
				sentReliableCommands.Clear();
			}
		}
		lock (channelArray)
		{
			EnetChannel[] array = channelArray;
			foreach (EnetChannel enetChannel in array)
			{
				enetChannel.clearAll();
			}
		}
		bool isSimulationEnabled = NetworkSimulationSettings.IsSimulationEnabled;
		NetworkSimulationSettings.IsSimulationEnabled = false;
		NCommand nCommand = new NCommand(this, 4, null, byte.MaxValue);
		QueueOutgoingReliableCommand(nCommand);
		SendOutgoingCommands();
		if (TrafficStatsEnabled)
		{
			TrafficStatsOutgoing.CountControlCommand(nCommand.Size);
		}
		NetworkSimulationSettings.IsSimulationEnabled = isSimulationEnabled;
		PhotonSocket.Disconnect();
		peerConnectionState = ConnectionStateValue.Disconnected;
		EnqueueStatusCallback(StatusCode.Disconnect);
		datagramEncryptedConnection = false;
	}

	internal override void StopConnection()
	{
		if (PhotonSocket != null)
		{
			PhotonSocket.Disconnect();
		}
		peerConnectionState = ConnectionStateValue.Disconnected;
		if (Listener != null)
		{
			Listener.OnStatusChanged(StatusCode.Disconnect);
		}
	}

	internal override void FetchServerTimestamp()
	{
		if (peerConnectionState != ConnectionStateValue.Connected || !ApplicationIsInitialized)
		{
			if ((int)debugOut >= 3)
			{
				EnqueueDebugReturn(DebugLevel.INFO, "FetchServerTimestamp() was skipped, as the client is not connected. Current ConnectionState: " + peerConnectionState);
			}
		}
		else
		{
			CreateAndEnqueueCommand(12, null, byte.MaxValue);
		}
	}

	internal override bool DispatchIncomingCommands()
	{
		int count = CommandQueue.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				lock (CommandQueue)
				{
					NCommand command = CommandQueue.Dequeue();
					ExecuteCommand(command);
				}
			}
		}
		while (true)
		{
			MyAction myAction;
			lock (ActionQueue)
			{
				if (ActionQueue.Count <= 0)
				{
					break;
				}
				myAction = ActionQueue.Dequeue();
				goto IL_00a8;
			}
			IL_00a8:
			myAction();
		}
		NCommand value = null;
		lock (channelArray)
		{
			for (int j = 0; j < channelArray.Length; j++)
			{
				EnetChannel enetChannel = channelArray[j];
				if (enetChannel.incomingUnsequencedCommandsList.Count > 0)
				{
					value = enetChannel.incomingUnsequencedCommandsList.Dequeue();
					break;
				}
				if (enetChannel.incomingUnreliableCommandsList.Count > 0)
				{
					int num = int.MaxValue;
					foreach (int key in enetChannel.incomingUnreliableCommandsList.Keys)
					{
						NCommand nCommand = enetChannel.incomingUnreliableCommandsList[key];
						if (key < enetChannel.incomingUnreliableSequenceNumber || nCommand.reliableSequenceNumber < enetChannel.incomingReliableSequenceNumber)
						{
							commandsToRemove.Enqueue(key);
							continue;
						}
						int limitOfUnreliableCommands = photonPeer.LimitOfUnreliableCommands;
						if (limitOfUnreliableCommands > 0 && enetChannel.incomingUnreliableCommandsList.Count > limitOfUnreliableCommands)
						{
							commandsToRemove.Enqueue(key);
						}
						else if (key < num && nCommand.reliableSequenceNumber <= enetChannel.incomingReliableSequenceNumber)
						{
							num = key;
						}
					}
					while (commandsToRemove.Count > 0)
					{
						enetChannel.incomingUnreliableCommandsList.Remove(commandsToRemove.Dequeue());
					}
					if (num < int.MaxValue)
					{
						value = enetChannel.incomingUnreliableCommandsList[num];
					}
					if (value != null)
					{
						enetChannel.incomingUnreliableCommandsList.Remove(value.unreliableSequenceNumber);
						enetChannel.incomingUnreliableSequenceNumber = value.unreliableSequenceNumber;
						break;
					}
				}
				if (value != null || enetChannel.incomingReliableCommandsList.Count <= 0)
				{
					continue;
				}
				enetChannel.incomingReliableCommandsList.TryGetValue(enetChannel.incomingReliableSequenceNumber + 1, out value);
				if (value != null)
				{
					if (value.commandType != 8)
					{
						enetChannel.incomingReliableSequenceNumber = value.reliableSequenceNumber;
						enetChannel.incomingReliableCommandsList.Remove(value.reliableSequenceNumber);
					}
					else if (value.fragmentsRemaining > 0)
					{
						value = null;
					}
					else
					{
						enetChannel.incomingReliableSequenceNumber = value.reliableSequenceNumber + value.fragmentCount - 1;
					}
					break;
				}
			}
		}
		if (value != null && value.Payload != null)
		{
			ByteCountCurrentDispatch = value.Size;
			CommandInCurrentDispatch = value;
			if (DeserializeMessageAndCallback(value.Payload))
			{
				value.FreePayload();
				CommandInCurrentDispatch = null;
				return true;
			}
			CommandInCurrentDispatch = null;
		}
		return false;
	}

	private int GetFragmentLength()
	{
		if (fragmentLength == 0 || mtu != fragmentLengthMtuValue)
		{
			int num = mtu;
			if (datagramEncryptedConnection)
			{
				num = num - 7 - HMAC_SIZE - IV_SIZE;
				num = num / BLOCK_SIZE * BLOCK_SIZE;
				return num - 5 - 36;
			}
			fragmentLength = num - 12 - 36;
			fragmentLengthMtuValue = mtu;
		}
		return fragmentLength;
	}

	private int CalculateBufferLen()
	{
		int num = mtu;
		if (datagramEncryptedConnection)
		{
			num = num - 7 - HMAC_SIZE - IV_SIZE;
			num = num / BLOCK_SIZE * BLOCK_SIZE;
			return num - 1;
		}
		return num;
	}

	private int CalculateInitialOffset()
	{
		if (datagramEncryptedConnection)
		{
			return 5;
		}
		int num = 12;
		if (photonPeer.CrcEnabled)
		{
			num += 4;
		}
		return num;
	}

	internal override bool SendAcksOnly()
	{
		if (peerConnectionState == ConnectionStateValue.Disconnected)
		{
			return false;
		}
		if (PhotonSocket == null || !PhotonSocket.Connected)
		{
			return false;
		}
		lock (udpBuffer)
		{
			int num = 0;
			udpBufferIndex = CalculateInitialOffset();
			udpBufferLength = CalculateBufferLen();
			udpCommandCount = 0;
			timeInt = SupportClass.GetTickCount() - timeBase;
			lock (outgoingAcknowledgementsPool)
			{
				num = SerializeAckToBuffer();
				timeLastSendAck = timeInt;
			}
			if (timeInt > timeoutInt && sentReliableCommands.Count > 0)
			{
				lock (sentReliableCommands)
				{
					foreach (NCommand sentReliableCommand in sentReliableCommands)
					{
						if (sentReliableCommand != null && sentReliableCommand.roundTripTimeout != 0 && timeInt - sentReliableCommand.commandSentTime > sentReliableCommand.roundTripTimeout)
						{
							sentReliableCommand.commandSentCount = 1;
							sentReliableCommand.roundTripTimeout = 0;
							sentReliableCommand.timeoutTime = int.MaxValue;
							sentReliableCommand.commandSentTime = timeInt;
						}
					}
				}
			}
			if (udpCommandCount <= 0)
			{
				return false;
			}
			if (TrafficStatsEnabled)
			{
				TrafficStatsOutgoing.TotalPacketCount++;
				TrafficStatsOutgoing.TotalCommandsInPackets += udpCommandCount;
			}
			SendData(udpBuffer, udpBufferIndex);
			return num > 0;
		}
	}

	internal override bool SendOutgoingCommands()
	{
		if (peerConnectionState == ConnectionStateValue.Disconnected)
		{
			return false;
		}
		if (!PhotonSocket.Connected)
		{
			return false;
		}
		lock (udpBuffer)
		{
			int num = 0;
			udpBufferIndex = CalculateInitialOffset();
			udpBufferLength = CalculateBufferLen();
			udpCommandCount = 0;
			timeInt = SupportClass.GetTickCount() - timeBase;
			timeLastSendOutgoing = timeInt;
			lock (outgoingAcknowledgementsPool)
			{
				if (outgoingAcknowledgementsPool.Length > 0)
				{
					num = SerializeAckToBuffer();
					timeLastSendAck = timeInt;
				}
			}
			if (!IsSendingOnlyAcks && timeInt > timeoutInt && sentReliableCommands.Count > 0)
			{
				lock (sentReliableCommands)
				{
					commandsToResend.Clear();
					for (int i = 0; i < sentReliableCommands.Count; i++)
					{
						NCommand nCommand = sentReliableCommands[i];
						if (nCommand == null || timeInt - nCommand.commandSentTime <= nCommand.roundTripTimeout)
						{
							continue;
						}
						if (nCommand.commandSentCount > photonPeer.SentCountAllowance || timeInt > nCommand.timeoutTime)
						{
							if ((int)debugOut >= 2)
							{
								Listener.DebugReturn(DebugLevel.WARNING, string.Concat("Timeout-disconnect! Command: ", nCommand, " now: ", timeInt, " challenge: ", Convert.ToString(challenge, 16)));
							}
							if (CommandLog != null)
							{
								CommandLog.Enqueue(new CmdLogSentReliable(nCommand, timeInt, roundTripTime, roundTripTimeVariance, triggeredTimeout: true));
								CommandLogResize();
							}
							peerConnectionState = ConnectionStateValue.Zombie;
							Listener.OnStatusChanged(StatusCode.TimeoutDisconnect);
							Disconnect();
							return false;
						}
						commandsToResend.Enqueue(nCommand);
					}
					while (commandsToResend.Count > 0)
					{
						NCommand nCommand2 = commandsToResend.Dequeue();
						QueueOutgoingReliableCommand(nCommand2);
						sentReliableCommands.Remove(nCommand2);
						reliableCommandsRepeated++;
						if ((int)debugOut >= 3)
						{
							Listener.DebugReturn(DebugLevel.INFO, string.Format("Resending: {0}. times out after: {1} sent: {3} now: {2} rtt/var: {4}/{5} last recv: {6}", nCommand2, nCommand2.roundTripTimeout, timeInt, nCommand2.commandSentTime, roundTripTime, roundTripTimeVariance, SupportClass.GetTickCount() - timestampOfLastReceive));
						}
					}
				}
			}
			if (!IsSendingOnlyAcks && peerConnectionState == ConnectionStateValue.Connected && timePingInterval > 0 && sentReliableCommands.Count == 0 && timeInt - timeLastAckReceive > timePingInterval && !AreReliableCommandsInTransit() && udpBufferIndex + 12 < udpBufferLength)
			{
				NCommand nCommand3 = new NCommand(this, 5, null, byte.MaxValue);
				QueueOutgoingReliableCommand(nCommand3);
				if (TrafficStatsEnabled)
				{
					TrafficStatsOutgoing.CountControlCommand(nCommand3.Size);
				}
			}
			if (!IsSendingOnlyAcks)
			{
				lock (channelArray)
				{
					for (int j = 0; j < channelArray.Length; j++)
					{
						EnetChannel enetChannel = channelArray[j];
						num += SerializeToBuffer(enetChannel.outgoingReliableCommandsList);
						num += SerializeToBuffer(enetChannel.outgoingUnreliableCommandsList);
					}
				}
			}
			if (udpCommandCount <= 0)
			{
				return false;
			}
			if (TrafficStatsEnabled)
			{
				TrafficStatsOutgoing.TotalPacketCount++;
				TrafficStatsOutgoing.TotalCommandsInPackets += udpCommandCount;
			}
			SendData(udpBuffer, udpBufferIndex);
			return num > 0;
		}
	}

	private bool AreReliableCommandsInTransit()
	{
		lock (channelArray)
		{
			for (int i = 0; i < channelArray.Length; i++)
			{
				EnetChannel enetChannel = channelArray[i];
				if (enetChannel.outgoingReliableCommandsList.Count > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	internal override bool EnqueueOperation(Dictionary<byte, object> parameters, byte opCode, SendOptions sendParams, EgMessageType messageType = EgMessageType.Operation)
	{
		if (peerConnectionState != ConnectionStateValue.Connected)
		{
			if ((int)debugOut >= 1)
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Cannot send op: " + opCode + " Not connected. PeerState: " + peerConnectionState);
			}
			Listener.OnStatusChanged(StatusCode.SendError);
			return false;
		}
		if (sendParams.Channel >= ChannelCount)
		{
			if ((int)debugOut >= 1)
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Cannot send op: Selected channel (" + sendParams.Channel + ")>= channelCount (" + ChannelCount + ").");
			}
			Listener.OnStatusChanged(StatusCode.SendError);
			return false;
		}
		byte commandType = 7;
		if (sendParams.DeliveryMode == DeliveryMode.UnreliableUnsequenced)
		{
			commandType = 11;
		}
		else if (sendParams.DeliveryMode == DeliveryMode.ReliableUnsequenced)
		{
			commandType = 14;
		}
		else if (sendParams.DeliveryMode == DeliveryMode.Reliable)
		{
			commandType = 6;
		}
		StreamBuffer payload = SerializeOperationToMessage(opCode, parameters, messageType, sendParams.Encrypt);
		return CreateAndEnqueueCommand(commandType, payload, sendParams.Channel);
	}

	internal override bool EnqueueMessage(object message, SendOptions sendOptions)
	{
		if (peerConnectionState != ConnectionStateValue.Connected)
		{
			if ((int)debugOut >= 1)
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Cannot send message! Not connected. PeerState: " + peerConnectionState);
			}
			Listener.OnStatusChanged(StatusCode.SendError);
			return false;
		}
		byte channel = sendOptions.Channel;
		if (channel >= ChannelCount)
		{
			if ((int)debugOut >= 1)
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Cannot send op: Selected channel (" + channel + ")>= channelCount (" + ChannelCount + ").");
			}
			Listener.OnStatusChanged(StatusCode.SendError);
			return false;
		}
		StreamBuffer payload = SerializeMessageToMessage(message, sendOptions.Encrypt, messageHeader, writeLength: false);
		byte commandType = 7;
		if (sendOptions.DeliveryMode == DeliveryMode.UnreliableUnsequenced)
		{
			commandType = 11;
		}
		else if (sendOptions.DeliveryMode == DeliveryMode.ReliableUnsequenced)
		{
			commandType = 14;
		}
		else if (sendOptions.DeliveryMode == DeliveryMode.Reliable)
		{
			commandType = 6;
		}
		return CreateAndEnqueueCommand(commandType, payload, channel);
	}

	private EnetChannel GetChannel(byte channelNumber)
	{
		return (channelNumber == byte.MaxValue) ? channelArray[channelArray.Length - 1] : channelArray[channelNumber];
	}

	internal bool CreateAndEnqueueCommand(byte commandType, StreamBuffer payload, byte channelNumber)
	{
		EnetChannel channel = GetChannel(channelNumber);
		ByteCountLastOperation = 0;
		int num = GetFragmentLength();
		if (payload == null || payload.Length <= num)
		{
			NCommand nCommand = new NCommand(this, commandType, payload, channel.ChannelNumber);
			if (nCommand.IsFlaggedReliable)
			{
				QueueOutgoingReliableCommand(nCommand);
				ByteCountLastOperation = nCommand.Size;
				if (TrafficStatsEnabled)
				{
					TrafficStatsOutgoing.CountReliableOpCommand(nCommand.Size);
					TrafficStatsGameLevel.CountOperation(nCommand.Size);
				}
			}
			else
			{
				QueueOutgoingUnreliableCommand(nCommand);
				ByteCountLastOperation = nCommand.Size;
				if (TrafficStatsEnabled)
				{
					TrafficStatsOutgoing.CountUnreliableOpCommand(nCommand.Size);
					TrafficStatsGameLevel.CountOperation(nCommand.Size);
				}
			}
		}
		else
		{
			bool flag = commandType == 14 || commandType == 11;
			int fragmentCount = (payload.Length + num - 1) / num;
			int startSequenceNumber = (flag ? channel.outgoingReliableUnsequencedNumber : channel.outgoingReliableSequenceNumber) + 1;
			byte[] buffer = payload.GetBuffer();
			int num2 = 0;
			for (int i = 0; i < payload.Length; i += num)
			{
				if (payload.Length - i < num)
				{
					num = payload.Length - i;
				}
				StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
				streamBuffer.Write(buffer, i, num);
				NCommand nCommand2 = new NCommand(this, (byte)(flag ? 15 : 8), streamBuffer, channel.ChannelNumber);
				nCommand2.fragmentNumber = num2;
				nCommand2.startSequenceNumber = startSequenceNumber;
				nCommand2.fragmentCount = fragmentCount;
				nCommand2.totalLength = payload.Length;
				nCommand2.fragmentOffset = i;
				QueueOutgoingReliableCommand(nCommand2);
				ByteCountLastOperation += nCommand2.Size;
				if (TrafficStatsEnabled)
				{
					TrafficStatsOutgoing.CountFragmentOpCommand(nCommand2.Size);
					TrafficStatsGameLevel.CountOperation(nCommand2.Size);
				}
				num2++;
			}
		}
		return true;
	}

	internal override StreamBuffer SerializeOperationToMessage(byte opCode, Dictionary<byte, object> parameters, EgMessageType messageType, bool encrypt)
	{
		encrypt = encrypt && !datagramEncryptedConnection;
		StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
		streamBuffer.SetLength(0L);
		if (!encrypt)
		{
			streamBuffer.Write(messageHeader, 0, messageHeader.Length);
		}
		serializationProtocol.SerializeOperationRequest(streamBuffer, opCode, parameters, setType: false);
		if (encrypt)
		{
			byte[] array = CryptoProvider.Encrypt(streamBuffer.GetBuffer(), 0, streamBuffer.Length);
			streamBuffer.SetLength(0L);
			streamBuffer.Write(messageHeader, 0, messageHeader.Length);
			streamBuffer.Write(array, 0, array.Length);
		}
		byte[] buffer = streamBuffer.GetBuffer();
		if (messageType != EgMessageType.Operation)
		{
			buffer[messageHeader.Length - 1] = (byte)messageType;
		}
		if (encrypt)
		{
			buffer[messageHeader.Length - 1] = (byte)(buffer[messageHeader.Length - 1] | 0x80);
		}
		return streamBuffer;
	}

	internal int SerializeAckToBuffer()
	{
		outgoingAcknowledgementsPool.Seek(0L, SeekOrigin.Begin);
		while (outgoingAcknowledgementsPool.Position + 20 <= outgoingAcknowledgementsPool.Length)
		{
			if (udpBufferIndex + 20 > udpBufferLength)
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, "UDP package is full. Commands in Package: " + udpCommandCount + ". bytes left in queue: " + outgoingAcknowledgementsPool.Position);
				}
				break;
			}
			byte[] bufferAndAdvance = outgoingAcknowledgementsPool.GetBufferAndAdvance(20, out var offset);
			Buffer.BlockCopy(bufferAndAdvance, offset, udpBuffer, udpBufferIndex, 20);
			udpBufferIndex += 20;
			udpCommandCount++;
		}
		outgoingAcknowledgementsPool.Compact();
		outgoingAcknowledgementsPool.Position = outgoingAcknowledgementsPool.Length;
		return outgoingAcknowledgementsPool.Length / 20;
	}

	internal int SerializeToBuffer(Queue<NCommand> commandList)
	{
		while (commandList.Count > 0)
		{
			NCommand nCommand = commandList.Peek();
			if (nCommand == null)
			{
				commandList.Dequeue();
				continue;
			}
			if (udpBufferIndex + nCommand.Size > udpBufferLength)
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, "UDP package is full. Commands in Package: " + udpCommandCount + ". Commands left in queue: " + commandList.Count);
				}
				break;
			}
			nCommand.SerializeHeader(udpBuffer, ref udpBufferIndex);
			if (nCommand.SizeOfPayload > 0)
			{
				Buffer.BlockCopy(nCommand.Serialize(), 0, udpBuffer, udpBufferIndex, nCommand.SizeOfPayload);
				udpBufferIndex += nCommand.SizeOfPayload;
			}
			udpCommandCount++;
			if (nCommand.IsFlaggedReliable)
			{
				QueueSentCommand(nCommand);
				if (CommandLog != null)
				{
					CommandLog.Enqueue(new CmdLogSentReliable(nCommand, timeInt, roundTripTime, roundTripTimeVariance));
					CommandLogResize();
				}
			}
			else
			{
				nCommand.FreePayload();
			}
			commandList.Dequeue();
		}
		return commandList.Count;
	}

	internal void SendData(byte[] data, int length)
	{
		try
		{
			if (datagramEncryptedConnection)
			{
				SendDataEncrypted(data, length);
				return;
			}
			int targetOffset = 0;
			Protocol.Serialize(peerID, data, ref targetOffset);
			data[2] = (byte)(photonPeer.CrcEnabled ? 204 : 0);
			data[3] = udpCommandCount;
			targetOffset = 4;
			Protocol.Serialize(timeInt, data, ref targetOffset);
			Protocol.Serialize(challenge, data, ref targetOffset);
			if (photonPeer.CrcEnabled)
			{
				Protocol.Serialize(0, data, ref targetOffset);
				uint value = SupportClass.CalculateCrc(data, length);
				targetOffset -= 4;
				Protocol.Serialize((int)value, data, ref targetOffset);
			}
			bytesOut += length;
			SendToSocket(data, length);
		}
		catch (Exception ex)
		{
			if ((int)debugOut >= 1)
			{
				Listener.DebugReturn(DebugLevel.ERROR, ex.ToString());
			}
			SupportClass.WriteStackTrace(ex);
		}
	}

	private void SendToSocket(byte[] data, int length)
	{
		if (NetworkSimulationSettings.IsSimulationEnabled)
		{
			byte[] array = new byte[length];
			Buffer.BlockCopy(data, 0, array, 0, length);
			SendNetworkSimulated(array);
		}
		else
		{
			PhotonSocket.Send(data, length);
		}
	}

	private void SendDataEncrypted(byte[] data, int length)
	{
		if (bufferForEncryption == null || bufferForEncryption.Length != mtu)
		{
			bufferForEncryption = new byte[mtu];
		}
		byte[] array = bufferForEncryption;
		int targetOffset = 0;
		Protocol.Serialize(peerID, array, ref targetOffset);
		array[2] = 1;
		targetOffset++;
		Protocol.Serialize(challenge, array, ref targetOffset);
		data[0] = udpCommandCount;
		int targetOffset2 = 1;
		Protocol.Serialize(timeInt, data, ref targetOffset2);
		photonPeer.encryptor.Encrypt(data, length, array, ref targetOffset);
		Buffer.BlockCopy(photonPeer.encryptor.FinishHMAC(array, 0, targetOffset), 0, array, targetOffset, HMAC_SIZE);
		SendToSocket(array, targetOffset + HMAC_SIZE);
	}

	internal void QueueSentCommand(NCommand command)
	{
		command.commandSentTime = timeInt;
		command.commandSentCount++;
		if (command.roundTripTimeout == 0)
		{
			command.roundTripTimeout = roundTripTime + 4 * roundTripTimeVariance;
			command.timeoutTime = timeInt + DisconnectTimeout;
		}
		else if (command.commandSentCount > photonPeer.QuickResendAttempts + 1)
		{
			command.roundTripTimeout *= 2;
		}
		lock (sentReliableCommands)
		{
			if (sentReliableCommands.Count == 0)
			{
				int num = command.commandSentTime + command.roundTripTimeout;
				if (num < timeoutInt)
				{
					timeoutInt = num;
				}
			}
			reliableCommandsSent++;
			sentReliableCommands.Add(command);
		}
	}

	internal void QueueOutgoingReliableCommand(NCommand command)
	{
		EnetChannel channel = GetChannel(command.commandChannelID);
		lock (channel)
		{
			if (command.reliableSequenceNumber == 0)
			{
				if (command.IsFlaggedUnsequenced)
				{
					command.reliableSequenceNumber = ++channel.outgoingReliableUnsequencedNumber;
				}
				else
				{
					command.reliableSequenceNumber = ++channel.outgoingReliableSequenceNumber;
				}
			}
			channel.outgoingReliableCommandsList.Enqueue(command);
		}
	}

	internal void QueueOutgoingUnreliableCommand(NCommand command)
	{
		EnetChannel channel = GetChannel(command.commandChannelID);
		lock (channel)
		{
			if (command.IsFlaggedUnsequenced)
			{
				command.reliableSequenceNumber = 0;
				command.unsequencedGroupNumber = ++outgoingUnsequencedGroupNumber;
			}
			else
			{
				command.reliableSequenceNumber = channel.outgoingReliableSequenceNumber;
				command.unreliableSequenceNumber = ++channel.outgoingUnreliableSequenceNumber;
			}
			channel.outgoingUnreliableCommandsList.Enqueue(command);
		}
	}

	internal void QueueOutgoingAcknowledgement(NCommand readCommand, int sendTime)
	{
		lock (outgoingAcknowledgementsPool)
		{
			byte[] bufferAndAdvance = outgoingAcknowledgementsPool.GetBufferAndAdvance(20, out var offset);
			NCommand.CreateAck(bufferAndAdvance, offset, readCommand, sendTime);
		}
	}

	internal override void ReceiveIncomingCommands(byte[] inBuff, int dataLength)
	{
		timestampOfLastReceive = SupportClass.GetTickCount();
		try
		{
			int offset = 0;
			Protocol.Deserialize(out short _, inBuff, ref offset);
			byte b = inBuff[offset++];
			int value2;
			byte b2;
			if (b == 1)
			{
				if (photonPeer.decryptor == null)
				{
					EnqueueDebugReturn(DebugLevel.ERROR, "Got encrypted packet, but encryption is not set up. Packet ignored");
					return;
				}
				datagramEncryptedConnection = true;
				if (!photonPeer.decryptor.CheckHMAC(inBuff, dataLength))
				{
					packetLossByCrc++;
					if (peerConnectionState != ConnectionStateValue.Disconnected && (int)debugOut >= 3)
					{
						EnqueueDebugReturn(DebugLevel.INFO, "Ignored package due to wrong HMAC.");
					}
					return;
				}
				Protocol.Deserialize(out value2, inBuff, ref offset);
				inBuff = photonPeer.decryptor.DecryptBufferWithIV(inBuff, offset, dataLength - offset - HMAC_SIZE, out dataLength);
				dataLength = inBuff.Length;
				offset = 0;
				b2 = inBuff[offset++];
				Protocol.Deserialize(out serverSentTime, inBuff, ref offset);
				bytesIn += 12 + IV_SIZE + HMAC_SIZE + dataLength + (BLOCK_SIZE - dataLength % BLOCK_SIZE);
			}
			else
			{
				if (datagramEncryptedConnection)
				{
					EnqueueDebugReturn(DebugLevel.WARNING, "Got not encrypted packet, but expected only encrypted. Packet ignored");
					return;
				}
				b2 = inBuff[offset++];
				Protocol.Deserialize(out serverSentTime, inBuff, ref offset);
				Protocol.Deserialize(out value2, inBuff, ref offset);
				if (b == 204)
				{
					Protocol.Deserialize(out int value3, inBuff, ref offset);
					bytesIn += 4L;
					offset -= 4;
					Protocol.Serialize(0, inBuff, ref offset);
					uint num = SupportClass.CalculateCrc(inBuff, dataLength);
					if (value3 != (int)num)
					{
						packetLossByCrc++;
						if (peerConnectionState != ConnectionStateValue.Disconnected && (int)debugOut >= 3)
						{
							EnqueueDebugReturn(DebugLevel.INFO, $"Ignored package due to wrong CRC. Incoming:  {(uint)value3:X} Local: {num:X}");
						}
						return;
					}
				}
				bytesIn += 12L;
			}
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.TotalPacketCount++;
				TrafficStatsIncoming.TotalCommandsInPackets += b2;
			}
			if (b2 > commandBufferSize || b2 <= 0)
			{
				EnqueueDebugReturn(DebugLevel.ERROR, "too many/few incoming commands in package: " + b2 + " > " + commandBufferSize);
			}
			if (value2 != challenge)
			{
				packetLossByChallenge++;
				if (peerConnectionState != ConnectionStateValue.Disconnected && (int)debugOut >= 5)
				{
					EnqueueDebugReturn(DebugLevel.ALL, "Info: Ignoring received package due to wrong challenge. Challenge in-package!=local:" + value2 + "!=" + challenge + " Commands in it: " + b2);
				}
				return;
			}
			timeInt = SupportClass.GetTickCount() - timeBase;
			for (int i = 0; i < b2; i++)
			{
				NCommand nCommand = new NCommand(this, inBuff, ref offset);
				if (nCommand.commandType != 1 && nCommand.commandType != 16)
				{
					lock (CommandQueue)
					{
						CommandQueue.Enqueue(nCommand);
					}
				}
				else
				{
					ExecuteCommand(nCommand);
				}
				if (nCommand.IsFlaggedReliable)
				{
					if (InReliableLog != null)
					{
						InReliableLog.Enqueue(new CmdLogReceivedReliable(nCommand, timeInt, roundTripTime, roundTripTimeVariance, timeInt - timeLastSendOutgoing, timeInt - timeLastSendAck));
						CommandLogResize();
					}
					QueueOutgoingAcknowledgement(nCommand, serverSentTime);
					if (TrafficStatsEnabled)
					{
						TrafficStatsIncoming.TimestampOfLastReliableCommand = SupportClass.GetTickCount();
						TrafficStatsOutgoing.CountControlCommand(20);
					}
				}
			}
		}
		catch (Exception ex)
		{
			if ((int)debugOut >= 1)
			{
				EnqueueDebugReturn(DebugLevel.ERROR, $"Exception while reading commands from incoming data: {ex}");
			}
			SupportClass.WriteStackTrace(ex);
		}
	}

	internal bool ExecuteCommand(NCommand command)
	{
		bool result = true;
		switch (command.commandType)
		{
		case 2:
		case 5:
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountControlCommand(command.Size);
			}
			break;
		case 4:
		{
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountControlCommand(command.Size);
			}
			StatusCode statusCode = StatusCode.DisconnectByServer;
			if (command.reservedByte == 1)
			{
				statusCode = StatusCode.DisconnectByServerLogic;
			}
			else if (command.reservedByte == 3)
			{
				statusCode = StatusCode.DisconnectByServerUserLimit;
			}
			if ((int)debugOut >= 3)
			{
				Listener.DebugReturn(DebugLevel.INFO, "Server " + ServerAddress + " sent disconnect. PeerId: " + (ushort)peerID + " RTT/Variance:" + roundTripTime + "/" + roundTripTimeVariance + " reason byte: " + command.reservedByte);
			}
			ConnectionStateValue connectionStateValue = peerConnectionState;
			peerConnectionState = ConnectionStateValue.Disconnecting;
			Listener.OnStatusChanged(statusCode);
			peerConnectionState = connectionStateValue;
			Disconnect();
			break;
		}
		case 1:
		case 16:
		{
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.TimestampOfLastAck = SupportClass.GetTickCount();
				TrafficStatsIncoming.CountControlCommand(command.Size);
			}
			timeLastAckReceive = timeInt;
			lastRoundTripTime = timeInt - command.ackReceivedSentTime;
			NCommand nCommand = RemoveSentReliableCommand(command.ackReceivedReliableSequenceNumber, command.commandChannelID, command.commandType == 16);
			if (CommandLog != null)
			{
				CommandLog.Enqueue(new CmdLogReceivedAck(command, timeInt, roundTripTime, roundTripTimeVariance));
				CommandLogResize();
			}
			if (nCommand == null)
			{
				break;
			}
			nCommand.FreePayload();
			if (nCommand.commandType == 12)
			{
				if (lastRoundTripTime <= roundTripTime)
				{
					serverTimeOffset = serverSentTime + (lastRoundTripTime >> 1) - SupportClass.GetTickCount();
					serverTimeOffsetIsAvailable = true;
				}
				else
				{
					FetchServerTimestamp();
				}
				break;
			}
			UpdateRoundTripTimeAndVariance(lastRoundTripTime);
			if (nCommand.commandType == 4 && peerConnectionState == ConnectionStateValue.Disconnecting)
			{
				if ((int)debugOut >= 3)
				{
					EnqueueDebugReturn(DebugLevel.INFO, "Received disconnect ACK by server");
				}
				EnqueueActionForDispatch(() =>
				{
					PhotonSocket.Disconnect();
				});
			}
			else if (nCommand.commandType == 2)
			{
				roundTripTime = lastRoundTripTime;
			}
			break;
		}
		case 6:
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountReliableOpCommand(command.Size);
			}
			if (peerConnectionState == ConnectionStateValue.Connected)
			{
				result = QueueIncomingCommand(command);
			}
			break;
		case 14:
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountReliableOpCommand(command.Size);
			}
			if (peerConnectionState == ConnectionStateValue.Connected)
			{
				result = QueueIncomingCommand(command);
			}
			break;
		case 7:
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountUnreliableOpCommand(command.Size);
			}
			if (peerConnectionState == ConnectionStateValue.Connected)
			{
				result = QueueIncomingCommand(command);
			}
			break;
		case 11:
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountUnreliableOpCommand(command.Size);
			}
			if (peerConnectionState == ConnectionStateValue.Connected)
			{
				result = QueueIncomingCommand(command);
			}
			break;
		case 8:
		case 15:
			if (peerConnectionState != ConnectionStateValue.Connected)
			{
				break;
			}
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountFragmentOpCommand(command.Size);
			}
			if (command.fragmentNumber > command.fragmentCount || command.fragmentOffset >= command.totalLength || command.fragmentOffset + command.Payload.Length > command.totalLength)
			{
				if ((int)debugOut >= 1)
				{
					Listener.DebugReturn(DebugLevel.ERROR, "Received fragment has bad size: " + command);
				}
			}
			else
			{
				if (!QueueIncomingCommand(command))
				{
					break;
				}
				bool flag = command.commandType == 8;
				EnetChannel channel = GetChannel(command.commandChannelID);
				NCommand fragment = null;
				if (command.reliableSequenceNumber == command.startSequenceNumber)
				{
					fragment = command;
					fragment.fragmentsRemaining--;
					NCommand fragment2 = null;
					int num = command.startSequenceNumber + 1;
					while (fragment.fragmentsRemaining > 0 && num < fragment.startSequenceNumber + fragment.fragmentCount)
					{
						if (channel.TryGetFragment(num++, flag, out fragment2))
						{
							fragment.fragmentsRemaining--;
						}
					}
				}
				else if (channel.TryGetFragment(command.startSequenceNumber, flag, out fragment))
				{
					fragment.fragmentsRemaining--;
				}
				if (fragment == null || fragment.fragmentsRemaining > 0)
				{
					break;
				}
				byte[] array = new byte[fragment.totalLength];
				for (int num2 = fragment.startSequenceNumber; num2 < fragment.startSequenceNumber + fragment.fragmentCount; num2++)
				{
					if (channel.TryGetFragment(num2, flag, out var fragment3))
					{
						Buffer.BlockCopy(fragment3.Payload.GetBuffer(), 0, array, fragment3.fragmentOffset, fragment3.Payload.Length);
						fragment3.FreePayload();
						channel.RemoveFragment(fragment3.reliableSequenceNumber, flag);
						continue;
					}
					throw new Exception("startCommand.fragmentsRemaining was 0 but not all fragments were found to be combined!");
				}
				fragment.Payload = new StreamBuffer(array);
				fragment.Size = 12 * fragment.fragmentCount + fragment.totalLength;
				if (!flag)
				{
					channel.incomingUnsequencedCommandsList.Enqueue(fragment);
				}
				else
				{
					channel.incomingReliableCommandsList.Add(fragment.startSequenceNumber, fragment);
				}
			}
			break;
		case 3:
			if (TrafficStatsEnabled)
			{
				TrafficStatsIncoming.CountControlCommand(command.Size);
			}
			if (peerConnectionState == ConnectionStateValue.Connecting)
			{
				byte[] buf = PrepareConnectData(ServerAddress, AppId, CustomInitData);
				CreateAndEnqueueCommand(6, new StreamBuffer(buf), 0);
				if (photonPeer.randomizeSequenceNumbers)
				{
					ApplyRandomizedSequenceNumbers();
				}
				peerConnectionState = ConnectionStateValue.Connected;
			}
			break;
		}
		return result;
	}

	internal bool QueueIncomingCommand(NCommand command)
	{
		EnetChannel channel = GetChannel(command.commandChannelID);
		if (channel == null)
		{
			if ((int)debugOut >= 1)
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Received command for non-existing channel: " + command.commandChannelID);
			}
			return false;
		}
		if ((int)debugOut >= 5)
		{
			Listener.DebugReturn(DebugLevel.ALL, string.Concat("queueIncomingCommand() ", command, " channel seq# r/u: ", channel.incomingReliableSequenceNumber, "/", channel.incomingUnreliableSequenceNumber));
		}
		if (command.IsFlaggedReliable)
		{
			if (command.IsFlaggedUnsequenced)
			{
				return channel.QueueIncomingReliableUnsequenced(command);
			}
			if (command.reliableSequenceNumber <= channel.incomingReliableSequenceNumber)
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, string.Concat("incoming command ", command, " is old (not saving it). Dispatched incomingReliableSequenceNumber: ", channel.incomingReliableSequenceNumber));
				}
				return false;
			}
			if (channel.ContainsReliableSequenceNumber(command.reliableSequenceNumber))
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, string.Concat("Info: command was received before! Old/New: ", channel.FetchReliableSequenceNumber(command.reliableSequenceNumber), "/", command, " inReliableSeq#: ", channel.incomingReliableSequenceNumber));
				}
				return false;
			}
			channel.incomingReliableCommandsList.Add(command.reliableSequenceNumber, command);
			return true;
		}
		if (command.commandFlags == 0)
		{
			if (command.reliableSequenceNumber < channel.incomingReliableSequenceNumber)
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, "incoming reliable-seq# < Dispatched-rel-seq#. not saved.");
				}
				return true;
			}
			if (command.unreliableSequenceNumber <= channel.incomingUnreliableSequenceNumber)
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, "incoming unreliable-seq# < Dispatched-unrel-seq#. not saved.");
				}
				return true;
			}
			if (channel.ContainsUnreliableSequenceNumber(command.unreliableSequenceNumber))
			{
				if ((int)debugOut >= 3)
				{
					Listener.DebugReturn(DebugLevel.INFO, string.Concat("command was received before! Old/New: ", channel.incomingUnreliableCommandsList[command.unreliableSequenceNumber], "/", command));
				}
				return false;
			}
			channel.incomingUnreliableCommandsList.Add(command.unreliableSequenceNumber, command);
			return true;
		}
		if (command.commandFlags == 2)
		{
			int unsequencedGroupNumber = command.unsequencedGroupNumber;
			int num = command.unsequencedGroupNumber % 128;
			if (unsequencedGroupNumber >= incomingUnsequencedGroupNumber + 128)
			{
				incomingUnsequencedGroupNumber = unsequencedGroupNumber - num;
				for (int i = 0; i < unsequencedWindow.Length; i++)
				{
					unsequencedWindow[i] = 0;
				}
			}
			else if (unsequencedGroupNumber < incomingUnsequencedGroupNumber || (unsequencedWindow[num / 32] & (1 << num % 32)) != 0)
			{
				return false;
			}
			unsequencedWindow[num / 32] |= 1 << num % 32;
			channel.incomingUnsequencedCommandsList.Enqueue(command);
			return true;
		}
		return false;
	}

	internal NCommand RemoveSentReliableCommand(int ackReceivedReliableSequenceNumber, int ackReceivedChannel, bool isUnsequenced)
	{
		NCommand nCommand = null;
		lock (sentReliableCommands)
		{
			foreach (NCommand sentReliableCommand in sentReliableCommands)
			{
				if (sentReliableCommand != null && sentReliableCommand.reliableSequenceNumber == ackReceivedReliableSequenceNumber && sentReliableCommand.IsFlaggedUnsequenced == isUnsequenced && sentReliableCommand.commandChannelID == ackReceivedChannel)
				{
					nCommand = sentReliableCommand;
					break;
				}
			}
			if (nCommand != null)
			{
				sentReliableCommands.Remove(nCommand);
				if (sentReliableCommands.Count > 0)
				{
					timeoutInt = timeInt + 25;
				}
			}
			else if ((int)debugOut >= 5 && peerConnectionState != ConnectionStateValue.Connected && peerConnectionState != ConnectionStateValue.Disconnecting)
			{
				EnqueueDebugReturn(DebugLevel.ALL, $"No sent command for ACK (Ch: {ackReceivedReliableSequenceNumber} Sq#: {ackReceivedChannel}). PeerState: {peerConnectionState}.");
			}
		}
		return nCommand;
	}

	internal string CommandListToString(NCommand[] list)
	{
		if ((int)debugOut < 5)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < list.Length; i++)
		{
			stringBuilder.Append(i + "=");
			stringBuilder.Append(list[i]);
			stringBuilder.Append(" # ");
		}
		return stringBuilder.ToString();
	}
}
