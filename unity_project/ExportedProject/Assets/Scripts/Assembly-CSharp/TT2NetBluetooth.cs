using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Photon.Pun;
using Shatalmic;
using UnityEngine;

public class TT2NetBluetooth : MonoBehaviour
{
	private class WriteQueueItem
	{
		public Networking.NetworkDevice networkDevice;

		public byte[] data;

		public WriteQueueItem(Networking.NetworkDevice networkDevice, byte[] data)
		{
			this.networkDevice = networkDevice;
			this.data = data;
		}
	}

	private class DiscoveredServerPlayer
	{
		public FriendData friendData;

		public float timestamp;
	}

	private class DiscoveredClientPlayer
	{
		public FriendData friendData;

		public Networking.NetworkDevice networkDevice;
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Initialized__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action StartedAdvertising__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Networking.NetworkDevice> DeviceConnected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Networking.NetworkDevice> DeviceDisconnected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<byte[]> ReceivedDataFromServer__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action DiscoveredPlayersUpdated__BackingField;

	public string networkName;

	public bool endlessBroadcastEnabled;

	public float broadcastInterval = 3f;

	public float playerTimeout = 5f;

	private Networking server;

	private Networking client;

	private TT2NetLocalBroadcastData broadcastData;

	private byte[] broadcastDataBytes;

	private float broadcastTimer;

	private List<Networking.NetworkDevice> connectedDevices = new List<Networking.NetworkDevice>();

	private bool isWritingToDevice;

	private List<WriteQueueItem> writeQueue = new List<WriteQueueItem>();

	private Dictionary<string, DiscoveredServerPlayer> discoveredServerPlayers = new Dictionary<string, DiscoveredServerPlayer>();

	private Dictionary<string, DiscoveredClientPlayer> discoveredClientPlayers = new Dictionary<string, DiscoveredClientPlayer>();

	public static TT2NetBluetooth Instance { get; private set; }

	public bool IsInitialized { get; private set; }

	public bool IsAdvertising { get; private set; }

	public List<FriendData> PlayerList { get; private set; }

	public event Action Initialized
	{
		add
		{
			Action action = Initialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Initialized__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Initialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Initialized__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action StartedAdvertising
	{
		add
		{
			Action action = StartedAdvertising__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StartedAdvertising__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = StartedAdvertising__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StartedAdvertising__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Networking.NetworkDevice> DeviceConnected
	{
		add
		{
			Action<Networking.NetworkDevice> action = DeviceConnected__BackingField;
			Action<Networking.NetworkDevice> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeviceConnected__BackingField, (Action<Networking.NetworkDevice>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Networking.NetworkDevice> action = DeviceConnected__BackingField;
			Action<Networking.NetworkDevice> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeviceConnected__BackingField, (Action<Networking.NetworkDevice>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Networking.NetworkDevice> DeviceDisconnected
	{
		add
		{
			Action<Networking.NetworkDevice> action = DeviceDisconnected__BackingField;
			Action<Networking.NetworkDevice> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeviceDisconnected__BackingField, (Action<Networking.NetworkDevice>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Networking.NetworkDevice> action = DeviceDisconnected__BackingField;
			Action<Networking.NetworkDevice> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeviceDisconnected__BackingField, (Action<Networking.NetworkDevice>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<byte[]> ReceivedDataFromServer
	{
		add
		{
			Action<byte[]> action = ReceivedDataFromServer__BackingField;
			Action<byte[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ReceivedDataFromServer__BackingField, (Action<byte[]>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<byte[]> action = ReceivedDataFromServer__BackingField;
			Action<byte[]> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ReceivedDataFromServer__BackingField, (Action<byte[]>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action DiscoveredPlayersUpdated
	{
		add
		{
			Action action = DiscoveredPlayersUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DiscoveredPlayersUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DiscoveredPlayersUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DiscoveredPlayersUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		server = base.gameObject.AddComponent<Networking>();
		client = base.gameObject.AddComponent<Networking>();
		broadcastData = new TT2NetLocalBroadcastData();
		broadcastData.friendData = Player.instance.pvpData.MyFriendData();
		broadcastData.appVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
		broadcastDataBytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(broadcastData));
		PlayerList = new List<FriendData>();
		Instance = this;
	}

	private void Start()
	{
		server.Initialize(HandleOnErrorServer, HandleOnStatusMessageServer, HandleOnInitializeServer);
	}

	private void Update()
	{
		if (endlessBroadcastEnabled && connectedDevices.Count > 0)
		{
			broadcastTimer -= Time.unscaledDeltaTime;
			if (broadcastTimer <= 0f)
			{
				broadcastTimer += broadcastInterval;
				for (int i = 0; i < connectedDevices.Count; i++)
				{
					SendToClient(broadcastDataBytes, connectedDevices[i]);
				}
			}
		}
		if (discoveredServerPlayers.Count <= 0)
		{
			return;
		}
		bool flag = false;
		List<string> list = new List<string>(discoveredServerPlayers.Keys);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (Time.unscaledTime - discoveredServerPlayers[list[num]].timestamp >= playerTimeout)
			{
				if (PlayerList.Contains(discoveredServerPlayers[list[num]].friendData))
				{
					PlayerList.Remove(discoveredServerPlayers[list[num]].friendData);
				}
				discoveredServerPlayers.Remove(list[num]);
				flag = true;
			}
		}
		if (flag)
		{
			OnDiscoveredPlayersUpdated();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause && !IsInitialized)
		{
			Log("Attempting to initialize the SERVER instance.");
			server.Initialize(HandleOnErrorServer, HandleOnStatusMessageServer, HandleOnInitializeServer);
		}
	}

	private void OnApplicationFocus(bool focus)
	{
		if (focus && !IsInitialized)
		{
			Log("Attempting to initialize the SERVER instance.");
			server.Initialize(HandleOnErrorServer, HandleOnStatusMessageServer, HandleOnInitializeServer);
		}
	}

	private void OnDestroy()
	{
		Instance = null;
		server.StopServer(null);
		client.StopClient(null);
		if (IsInitialized)
		{
			server.DeInitialize(null);
			IsInitialized = false;
		}
	}

	public void StartServer()
	{
		if (IsInitialized)
		{
			Log(string.Format("Start Server  |  Network Name: {0}", networkName));
			server.StartServer(networkName, HandleOnDeviceReady, HandleOnDeviceDisconnected, HandleOnDeviceData);
		}
		else
		{
			LogError(string.Format("Attempting to START server, but the server Networking instance is not initialized."));
		}
	}

	public void StopServer(Action onFinishedStoppingServer = null)
	{
		if (IsInitialized)
		{
			server.StopServer(onFinishedStoppingServer);
		}
		else
		{
			LogError(string.Format("Attempting to STOP server, but the server Networking instance is not initialized."));
		}
	}

	public void StartClient()
	{
		if (IsInitialized)
		{
			Log(string.Format("Start Client  |  Network Name: {0}    Client Name: {1}", networkName, SystemInfo.deviceName));
			client.StartClient(networkName, SystemInfo.deviceName, HandleOnStartedAdvertising, HandleOnCharacteristicWritten);
		}
		else
		{
			LogError(string.Format("Attempting to START client, but the client Networking instance is not initialized."));
		}
	}

	public void StopClient(Action onStoppedAdvertising = null)
	{
		if (IsInitialized)
		{
			client.StopClient(onStoppedAdvertising);
			IsAdvertising = false;
		}
		else
		{
			LogError(string.Format("Attempting to STOP client, but the client Networking instance is not initialized."));
		}
	}

	public void SendToClient(byte[] data, Networking.NetworkDevice clientDevice)
	{
		if (!isWritingToDevice)
		{
			if (connectedDevices.Contains(clientDevice))
			{
				Log("Begin writing to device " + clientDevice.Name);
				isWritingToDevice = true;
				server.WriteDevice(clientDevice, data, HandleOnWritten);
			}
			else
			{
				Log("The specified device is not currently in the list of connected devices.");
			}
			return;
		}
		WriteQueueItem writeQueueItem = writeQueue.Find((WriteQueueItem x) => x.networkDevice == clientDevice);
		if (writeQueueItem != null)
		{
			writeQueueItem.data = data;
			return;
		}
		Log("Already writing to a connected device, so queueing this write request until after.");
		writeQueue.Add(new WriteQueueItem(clientDevice, data));
	}

	public void SendFromClient(byte[] data)
	{
		Log("Send From Client: " + data);
		client.SendFromClient(data);
	}

	public FriendData GetFriendData(string playerId)
	{
		return PlayerList.Find((FriendData x) => x.playerID == playerId);
	}

	private void HandleOnErrorServer(string error)
	{
		LogError("Server Error: " + error);
		if (!error.Contains("Bluetooth LE Not Enabled"))
		{
		}
	}

	private void HandleOnStatusMessageServer(string status)
	{
		Log("Server Status: " + status);
	}

	private void HandleOnInitializeServer()
	{
		Log("Server Initialized");
		IsInitialized = true;
		if (Initialized__BackingField != null)
		{
			Initialized__BackingField();
		}
	}

	private void HandleOnDeviceReady(Networking.NetworkDevice networkDevice)
	{
		Log("Device Connected: " + networkDevice.Name);
		if (!connectedDevices.Contains(networkDevice))
		{
			connectedDevices.Add(networkDevice);
			Log(string.Format("Added {0} to the list of connected devices", networkDevice.Name));
		}
		SendToClient(broadcastDataBytes, networkDevice);
		if (DeviceConnected__BackingField != null)
		{
			DeviceConnected__BackingField(networkDevice);
		}
	}

	private void HandleOnDeviceDisconnected(Networking.NetworkDevice networkDevice)
	{
		Log("Device Disconnected: " + networkDevice.Name);
		if (connectedDevices.Contains(networkDevice))
		{
			connectedDevices.Remove(networkDevice);
			Log(string.Format("Removed {0} from the list of connected devices", networkDevice.Name));
			List<string> list = new List<string>(discoveredClientPlayers.Keys);
			for (int num = list.Count - 1; num >= 0; num--)
			{
				if (discoveredClientPlayers[list[num]].networkDevice == networkDevice)
				{
					Log(string.Format("Removed {0} from the discovered players.", discoveredClientPlayers[list[num]].friendData.name));
					if (PlayerList.Contains(discoveredClientPlayers[list[num]].friendData))
					{
						PlayerList.Remove(discoveredClientPlayers[list[num]].friendData);
					}
					discoveredClientPlayers.Remove(list[num]);
					OnDiscoveredPlayersUpdated();
					break;
				}
			}
		}
		if (DeviceDisconnected__BackingField != null)
		{
			DeviceDisconnected__BackingField(networkDevice);
		}
	}

	private void HandleOnDeviceData(Networking.NetworkDevice networkDevice, string characteristic, byte[] data)
	{
		Log(string.Format("Received Data from {0}  |  Characteristic: {1}    Data: {2}", networkDevice.Name, characteristic, Encoding.UTF8.GetString(data)));
		TT2NetLocalBroadcastData tT2NetLocalBroadcastData = null;
		try
		{
			tT2NetLocalBroadcastData = JsonUtility.FromJson<TT2NetLocalBroadcastData>(Encoding.UTF8.GetString(data));
		}
		catch (Exception)
		{
			LogError("Failed to parse data from device " + networkDevice.Name);
			return;
		}
		Log("Received broadcast data from CLIENT device: " + tT2NetLocalBroadcastData);
		if (PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion != tT2NetLocalBroadcastData.appVersion)
		{
			return;
		}
		DiscoveredClientPlayer value = null;
		if (!discoveredClientPlayers.TryGetValue(tT2NetLocalBroadcastData.friendData.playerID, out value))
		{
			value = new DiscoveredClientPlayer();
			value.networkDevice = networkDevice;
			value.friendData = tT2NetLocalBroadcastData.friendData;
			discoveredClientPlayers.Add(tT2NetLocalBroadcastData.friendData.playerID, value);
			if (!PlayerList.Contains(tT2NetLocalBroadcastData.friendData))
			{
				PlayerList.Add(tT2NetLocalBroadcastData.friendData);
			}
			OnDiscoveredPlayersUpdated();
		}
	}

	private void HandleOnWritten()
	{
		isWritingToDevice = false;
		if (writeQueue.Count > 0)
		{
			WriteQueueItem writeQueueItem = writeQueue.Shift();
			SendToClient(writeQueueItem.data, writeQueueItem.networkDevice);
		}
	}

	private void HandleOnStartedAdvertising()
	{
		Log(SystemInfo.deviceName + " Started Advertising");
		IsAdvertising = true;
		if (StartedAdvertising__BackingField != null)
		{
			StartedAdvertising__BackingField();
		}
	}

	private void HandleOnCharacteristicWritten(string clientName, string characteristic, byte[] data)
	{
		Log(string.Format("{0} wrote data {1}", clientName, Encoding.UTF8.GetString(data)));
		TT2NetLocalBroadcastData tT2NetLocalBroadcastData = null;
		try
		{
			tT2NetLocalBroadcastData = JsonUtility.FromJson<TT2NetLocalBroadcastData>(Encoding.UTF8.GetString(data));
		}
		catch (Exception)
		{
			LogError("Failed to parse data from device " + clientName);
			return;
		}
		Log("Received broadcast data from SERVER device: " + tT2NetLocalBroadcastData);
		SendFromClient(broadcastDataBytes);
		if (PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion != tT2NetLocalBroadcastData.appVersion)
		{
			return;
		}
		DiscoveredServerPlayer value = null;
		if (!discoveredServerPlayers.TryGetValue(tT2NetLocalBroadcastData.friendData.playerID, out value))
		{
			value = new DiscoveredServerPlayer();
			value.friendData = tT2NetLocalBroadcastData.friendData;
			discoveredServerPlayers.Add(tT2NetLocalBroadcastData.friendData.playerID, value);
			if (!PlayerList.Contains(tT2NetLocalBroadcastData.friendData))
			{
				PlayerList.Add(tT2NetLocalBroadcastData.friendData);
			}
			OnDiscoveredPlayersUpdated();
		}
		value.timestamp = Time.unscaledTime;
	}

	protected void OnDiscoveredPlayersUpdated()
	{
		if (DiscoveredPlayersUpdated__BackingField != null)
		{
			DiscoveredPlayersUpdated__BackingField();
		}
	}

	protected void Log(string message)
	{
		UnityEngine.Debug.LogFormat("[TT2NetBluetooth] - {0}", message);
	}

	protected void LogError(string message)
	{
		UnityEngine.Debug.LogErrorFormat("[TT2NetBluetooth] - {0}", message);
	}
}
