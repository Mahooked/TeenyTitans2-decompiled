using System;
using System.Collections.Generic;
using System.Text;
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

	public event Action Initialized;

	public event Action StartedAdvertising;

	public event Action<Networking.NetworkDevice> DeviceConnected;

	public event Action<Networking.NetworkDevice> DeviceDisconnected;

	public event Action<byte[]> ReceivedDataFromServer;

	public event Action DiscoveredPlayersUpdated;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		server = gameObject.AddComponent<Networking>();
		client = gameObject.AddComponent<Networking>();
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
			Log($"Start Server  |  Network Name: {networkName}");
			server.StartServer(networkName, HandleOnDeviceReady, HandleOnDeviceDisconnected, HandleOnDeviceData);
		}
		else
		{
			LogError($"Attempting to START server, but the server Networking instance is not initialized.");
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
			LogError($"Attempting to STOP server, but the server Networking instance is not initialized.");
		}
	}

	public void StartClient()
	{
		if (IsInitialized)
		{
			Log($"Start Client  |  Network Name: {networkName}    Client Name: {SystemInfo.deviceName}");
			client.StartClient(networkName, SystemInfo.deviceName, HandleOnStartedAdvertising, HandleOnCharacteristicWritten);
		}
		else
		{
			LogError($"Attempting to START client, but the client Networking instance is not initialized.");
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
			LogError($"Attempting to STOP client, but the client Networking instance is not initialized.");
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
		if (Initialized != null)
		{
			Initialized();
		}
	}

	private void HandleOnDeviceReady(Networking.NetworkDevice networkDevice)
	{
		Log("Device Connected: " + networkDevice.Name);
		if (!connectedDevices.Contains(networkDevice))
		{
			connectedDevices.Add(networkDevice);
			Log($"Added {networkDevice.Name} to the list of connected devices");
		}
		SendToClient(broadcastDataBytes, networkDevice);
		if (DeviceConnected != null)
		{
			DeviceConnected(networkDevice);
		}
	}

	private void HandleOnDeviceDisconnected(Networking.NetworkDevice networkDevice)
	{
		Log("Device Disconnected: " + networkDevice.Name);
		if (connectedDevices.Contains(networkDevice))
		{
			connectedDevices.Remove(networkDevice);
			Log($"Removed {networkDevice.Name} from the list of connected devices");
			List<string> list = new List<string>(discoveredClientPlayers.Keys);
			for (int num = list.Count - 1; num >= 0; num--)
			{
				if (discoveredClientPlayers[list[num]].networkDevice == networkDevice)
				{
					Log($"Removed {discoveredClientPlayers[list[num]].friendData.name} from the discovered players.");
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
		if (DeviceDisconnected != null)
		{
			DeviceDisconnected(networkDevice);
		}
	}

	private void HandleOnDeviceData(Networking.NetworkDevice networkDevice, string characteristic, byte[] data)
	{
		Log($"Received Data from {networkDevice.Name}  |  Characteristic: {characteristic}    Data: {Encoding.UTF8.GetString(data)}");
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
		if (StartedAdvertising != null)
		{
			StartedAdvertising();
		}
	}

	private void HandleOnCharacteristicWritten(string clientName, string characteristic, byte[] data)
	{
		Log($"{clientName} wrote data {Encoding.UTF8.GetString(data)}");
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
		if (DiscoveredPlayersUpdated != null)
		{
			DiscoveredPlayersUpdated();
		}
	}

	protected void Log(string message)
	{
		Debug.LogFormat("[TT2NetBluetooth] - {0}", message);
	}

	protected void LogError(string message)
	{
		Debug.LogErrorFormat("[TT2NetBluetooth] - {0}", message);
	}
}
