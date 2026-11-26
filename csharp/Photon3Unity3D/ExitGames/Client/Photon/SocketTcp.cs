using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace ExitGames.Client.Photon;

internal class SocketTcp : IPhotonSocket, IDisposable
{
	private Socket sock;

	private readonly object syncer = new object();

	public SocketTcp(PeerBase npeer)
		: base(npeer)
	{
		if (ReportDebugOfLevel(DebugLevel.ALL))
		{
			Listener.DebugReturn(DebugLevel.ALL, "SocketTcp: TCP, DotNet, Unity.");
		}
		PollReceive = false;
	}

	public void Dispose()
	{
		State = PhotonSocketState.Disconnecting;
		if (sock != null)
		{
			try
			{
				if (sock.Connected)
				{
					sock.Close();
				}
			}
			catch (Exception ex)
			{
				EnqueueDebugReturn(DebugLevel.INFO, "Exception in Dispose(): " + ex);
			}
		}
		sock = null;
		State = PhotonSocketState.Disconnected;
	}

	public override bool Connect()
	{
		if (!base.Connect())
		{
			return false;
		}
		State = PhotonSocketState.Connecting;
		Thread thread = new Thread(DnsAndConnect);
		thread.Name = "photon dns thread";
		thread.IsBackground = true;
		thread.Start();
		return true;
	}

	public override bool Disconnect()
	{
		if (ReportDebugOfLevel(DebugLevel.INFO))
		{
			EnqueueDebugReturn(DebugLevel.INFO, "SocketTcp.Disconnect()");
		}
		State = PhotonSocketState.Disconnecting;
		lock (syncer)
		{
			if (sock != null)
			{
				try
				{
					sock.Close();
				}
				catch (Exception ex)
				{
					EnqueueDebugReturn(DebugLevel.INFO, "Exception in Disconnect(): " + ex);
				}
				sock = null;
			}
		}
		State = PhotonSocketState.Disconnected;
		return true;
	}

	public override PhotonSocketError Send(byte[] data, int length)
	{
		if (sock == null || !sock.Connected)
		{
			return PhotonSocketError.Skipped;
		}
		try
		{
			sock.Send(data, 0, length, SocketFlags.None);
		}
		catch (Exception ex)
		{
			if (State != PhotonSocketState.Disconnecting && State != PhotonSocketState.Disconnected)
			{
				if (ReportDebugOfLevel(DebugLevel.ERROR))
				{
					string text = "";
					if (sock != null)
					{
						text = string.Format(" Local: {0} Remote: {1} ({2}, {3})", sock.LocalEndPoint, sock.RemoteEndPoint, sock.Connected ? "connected" : "not connected", sock.IsBound ? "bound" : "not bound");
					}
					EnqueueDebugReturn(DebugLevel.ERROR, string.Format("Cannot send to: {0} ({4}). Uptime: {1} ms. {2} {3}", ServerAddress, SupportClass.GetTickCount() - peerBase.timeBase, AddressResolvedAsIpv6 ? " IPv6" : string.Empty, text, ex));
				}
				HandleException(StatusCode.Exception);
			}
			return PhotonSocketError.Exception;
		}
		return PhotonSocketError.Success;
	}

	public override PhotonSocketError Receive(out byte[] data)
	{
		data = null;
		return PhotonSocketError.NoData;
	}

	public void DnsAndConnect()
	{
		try
		{
			IPAddress ipAddress = IPhotonSocket.GetIpAddress(ServerAddress);
			if (ipAddress == null)
			{
				throw new ArgumentException("Invalid IPAddress. Address: " + ServerAddress);
			}
			sock = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			sock.NoDelay = true;
			sock.ReceiveTimeout = peerBase.DisconnectTimeout;
			sock.SendTimeout = peerBase.DisconnectTimeout;
			sock.Connect(ipAddress, ServerPort);
			AddressResolvedAsIpv6 = IsIpv6SimpleCheck(ipAddress);
			State = PhotonSocketState.Connected;
			peerBase.OnConnect();
		}
		catch (SecurityException ex)
		{
			if (ReportDebugOfLevel(DebugLevel.ERROR))
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Connect() to '" + ServerAddress + "' failed: " + ex.ToString());
			}
			HandleException(StatusCode.SecurityExceptionOnConnect);
			return;
		}
		catch (Exception ex2)
		{
			if (ReportDebugOfLevel(DebugLevel.ERROR))
			{
				Listener.DebugReturn(DebugLevel.ERROR, "Connect() to '" + ServerAddress + "' failed: " + ex2.ToString());
			}
			HandleException(StatusCode.ExceptionOnConnect);
			return;
		}
		Thread thread = new Thread(ReceiveLoop);
		thread.Name = "photon receive thread";
		thread.IsBackground = true;
		thread.Start();
	}

	public void ReceiveLoop()
	{
		StreamBuffer streamBuffer = new StreamBuffer(MTU);
		byte[] array = new byte[9];
		while (State == PhotonSocketState.Connected)
		{
			streamBuffer.SetLength(0L);
			try
			{
				int num = 0;
				int num2 = 0;
				while (num < 9)
				{
					try
					{
						num2 = sock.Receive(array, num, 9 - num, SocketFlags.None);
					}
					catch (SocketException ex)
					{
						if (State != PhotonSocketState.Disconnecting && State > PhotonSocketState.Disconnected && ex.SocketErrorCode == SocketError.WouldBlock)
						{
							if (ReportDebugOfLevel(DebugLevel.ALL))
							{
								EnqueueDebugReturn(DebugLevel.ALL, "ReceiveLoop() got a WouldBlock exception. This is non-fatal. Going to continue.");
							}
							continue;
						}
						throw;
					}
					num += num2;
					if (num2 == 0)
					{
						throw new SocketException(10054);
					}
				}
				if (array[0] == 240)
				{
					HandleReceivedDatagram(array, array.Length, willBeReused: true);
					continue;
				}
				int num3 = (array[1] << 24) | (array[2] << 16) | (array[3] << 8) | array[4];
				if (peerBase.TrafficStatsEnabled)
				{
					if (array[5] == 0)
					{
						peerBase.TrafficStatsIncoming.CountReliableOpCommand(num3);
					}
					else
					{
						peerBase.TrafficStatsIncoming.CountUnreliableOpCommand(num3);
					}
				}
				if (ReportDebugOfLevel(DebugLevel.ALL))
				{
					EnqueueDebugReturn(DebugLevel.ALL, "TCP < " + num3);
				}
				streamBuffer.SetCapacityMinimum(num3 - 7);
				streamBuffer.Write(array, 7, num - 7);
				num = 0;
				num3 -= 9;
				while (num < num3)
				{
					try
					{
						num2 = sock.Receive(streamBuffer.GetBuffer(), streamBuffer.Position, num3 - num, SocketFlags.None);
					}
					catch (SocketException ex2)
					{
						if (State != PhotonSocketState.Disconnecting && State > PhotonSocketState.Disconnected && ex2.SocketErrorCode == SocketError.WouldBlock)
						{
							if (ReportDebugOfLevel(DebugLevel.ALL))
							{
								EnqueueDebugReturn(DebugLevel.ALL, "ReceiveLoop() got a WouldBlock exception. This is non-fatal. Going to continue.");
							}
							continue;
						}
						throw;
					}
					streamBuffer.Position += num2;
					num += num2;
					if (num2 == 0)
					{
						throw new SocketException(10054);
					}
				}
				HandleReceivedDatagram(streamBuffer.ToArray(), streamBuffer.Length, willBeReused: false);
				if (ReportDebugOfLevel(DebugLevel.ALL))
				{
					EnqueueDebugReturn(DebugLevel.ALL, "TCP < " + streamBuffer.Length + ((streamBuffer.Length == num3 + 2) ? " OK" : " BAD"));
				}
			}
			catch (SocketException ex3)
			{
				if (State != PhotonSocketState.Disconnecting && State != PhotonSocketState.Disconnected)
				{
					if (ReportDebugOfLevel(DebugLevel.ERROR))
					{
						EnqueueDebugReturn(DebugLevel.ERROR, "Receiving failed. SocketException: " + ex3.SocketErrorCode);
					}
					if (ex3.SocketErrorCode == SocketError.ConnectionReset || ex3.SocketErrorCode == SocketError.ConnectionAborted)
					{
						HandleException(StatusCode.DisconnectByServer);
					}
					else
					{
						HandleException(StatusCode.ExceptionOnReceive);
					}
				}
			}
			catch (Exception ex4)
			{
				if (State != PhotonSocketState.Disconnecting && State != PhotonSocketState.Disconnected)
				{
					if (ReportDebugOfLevel(DebugLevel.ERROR))
					{
						EnqueueDebugReturn(DebugLevel.ERROR, string.Concat("Receive issue. State: ", State, ". Server: '", ServerAddress, "' Exception: ", ex4));
					}
					HandleException(StatusCode.ExceptionOnReceive);
				}
			}
		}
		Disconnect();
	}
}
