using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shatalmic;

public class Networking : MonoBehaviour
{
	public class Characteristic
	{
		public string ServiceUUID;

		public string CharacteristicUUID;

		public bool Found;
	}

	public class NetworkDevice
	{
		public string Name;

		public string Address;

		public bool Connected;

		public bool DoDisconnect;
	}

	private enum States
	{
		None,
		StartScan,
		Connect,
		Subscribe,
		Disconnect,
		Disconnecting
	}

	private float _timeout;

	private States _state;

	private NetworkDevice _deviceToConnect;

	private NetworkDevice _deviceToDisconnect;

	public static List<Characteristic> Characteristics = new List<Characteristic>
	{
		new Characteristic
		{
			ServiceUUID = "37200001-7638-4216-B629-96AD40F79AA1",
			CharacteristicUUID = "37200002-7638-4216-B629-96AD40F79AA1",
			Found = false
		}
	};

	public Characteristic SampleCharacteristic = Characteristics[0];

	public List<NetworkDevice> NetworkDeviceList;

	public Action<string> OnError = (string error) =>
	{
		BluetoothLEHardwareInterface.Log("Error: " + error);
	};

	public Action<string> OnStatusMessage = (string message) =>
	{
		BluetoothLEHardwareInterface.Log(message);
	};

	public Action<NetworkDevice> OnDeviceReady;

	public Action<NetworkDevice> OnDeviceDisconnected;

	public Action<NetworkDevice, string, byte[]> OnDeviceData;

	private bool _serverStarted;

	private string _networkName;

	public Action OnFinishedStoppingServer;

	public bool AllCharacteristicsFound => !Characteristics.Where((Characteristic c) => !c.Found).Any();

	public string StatusMessage
	{
		set
		{
			if (OnStatusMessage != null)
			{
				OnStatusMessage(value);
			}
		}
	}

	public Characteristic GetCharacteristic(string serviceUUID, string characteristicsUUID)
	{
		return Characteristics.Where((Characteristic c) => IsEqual(serviceUUID, c.ServiceUUID) && IsEqual(characteristicsUUID, c.CharacteristicUUID)).FirstOrDefault();
	}

	private void SetState(States newState, float timeout)
	{
		_state = newState;
		_timeout = timeout;
	}

	private void Reset()
	{
		_timeout = 0f;
		_state = States.None;
		_networkName = null;
		_deviceToConnect = null;
		_deviceToDisconnect = null;
		NetworkDeviceList = new List<NetworkDevice>();
	}

	public void Initialize(Action<string> onError, Action<string> onStatusMessage, Action onInitialize = null)
	{
		BluetoothLEHardwareInterface.Log("BLNE: Bluetooth Low Energy Networking Initialize");
		if (onError != null)
		{
			OnError = onError;
		}
		if (onStatusMessage != null)
		{
			OnStatusMessage = onStatusMessage;
		}
		BluetoothLEHardwareInterface.Initialize(asCentral: true, asPeripheral: true, onInitialize, (string error1) =>
		{
			if (error1.Contains("Peripheral mode is Not Available"))
			{
				if (OnError != null)
				{
					OnError("Client mode not available");
				}
				BluetoothLEHardwareInterface.Initialize(asCentral: true, asPeripheral: false, onInitialize, (string obj) =>
				{
					if (OnError != null)
					{
						OnError(obj);
					}
				});
			}
			else if (OnError != null)
			{
				OnError(error1);
			}
		});
	}

	public void DeInitialize(Action action)
	{
		BluetoothLEHardwareInterface.Log("BLNE: Bluetooth Low Energy Networking DeInitialize");
		BluetoothLEHardwareInterface.DeInitialize(action);
	}

	public void StartServer(string networkName, Action<NetworkDevice> onDeviceReady, Action<NetworkDevice> onDeviceDisconnected, Action<NetworkDevice, string, byte[]> onDeviceData)
	{
		if (!_serverStarted)
		{
			Reset();
			_serverStarted = true;
			_networkName = networkName;
			OnDeviceReady = onDeviceReady;
			OnDeviceDisconnected = onDeviceDisconnected;
			OnDeviceData = onDeviceData;
			SetState(States.StartScan, 0.1f);
		}
	}

	public void StopServer(Action onFinishedStoppingServer)
	{
		if (_serverStarted)
		{
			BluetoothLEHardwareInterface.StopScan();
			if (NetworkDeviceList != null && NetworkDeviceList.Count > 0)
			{
				OnFinishedStoppingServer = onFinishedStoppingServer;
				foreach (NetworkDevice networkDevice in NetworkDeviceList)
				{
					networkDevice.DoDisconnect = true;
				}
				SetState(States.Disconnect, 0.1f);
			}
			else if (onFinishedStoppingServer != null)
			{
				onFinishedStoppingServer();
				_serverStarted = false;
			}
		}
		else if (onFinishedStoppingServer != null)
		{
			onFinishedStoppingServer();
			_serverStarted = false;
		}
	}

	public void WriteDevice(NetworkDevice device, byte[] bytes, Action onWritten)
	{
		BluetoothLEHardwareInterface.WriteCharacteristic(device.Address, SampleCharacteristic.ServiceUUID, SampleCharacteristic.CharacteristicUUID, bytes, bytes.Length, withResponse: true, (string Characteristic) =>
		{
			if (onWritten != null)
			{
				onWritten();
			}
		});
	}

	public void StartClient(string networkName, string clientName, Action onStartedAdvertising, Action<string, string, byte[]> onCharacteristicWritten)
	{
		Reset();
		BluetoothLEHardwareInterface.PeripheralName(networkName + ":" + clientName);
		BluetoothLEHardwareInterface.RemoveServices();
		BluetoothLEHardwareInterface.RemoveCharacteristics();
		BluetoothLEHardwareInterface.CBCharacteristicProperties properties = (BluetoothLEHardwareInterface.CBCharacteristicProperties)26;
		BluetoothLEHardwareInterface.CBAttributePermissions permissions = (BluetoothLEHardwareInterface.CBAttributePermissions)17;
		BluetoothLEHardwareInterface.CreateCharacteristic(SampleCharacteristic.CharacteristicUUID, properties, permissions, null, 5, (string characteristic, byte[] bytes) =>
		{
			if (onCharacteristicWritten != null)
			{
				onCharacteristicWritten(clientName, characteristic, bytes);
			}
		});
		BluetoothLEHardwareInterface.CreateService(SampleCharacteristic.ServiceUUID, primary: true, (string characteristic) =>
		{
			StatusMessage = "Created service";
		});
		BluetoothLEHardwareInterface.StartAdvertising(() =>
		{
			StatusMessage = "8";
			if (onStartedAdvertising != null)
			{
				onStartedAdvertising();
			}
		});
	}

	public void SendFromClient(byte[] bytes)
	{
		StatusMessage = "SendFromClient";
		BluetoothLEHardwareInterface.UpdateCharacteristicValue(SampleCharacteristic.CharacteristicUUID, bytes, bytes.Length);
	}

	public void StopClient(Action onStoppedAdvertising)
	{
		BluetoothLEHardwareInterface.StopAdvertising(() =>
		{
			if (onStoppedAdvertising != null)
			{
				onStoppedAdvertising();
			}
		});
	}

	private void Start()
	{
		Reset();
	}

	private void Update()
	{
		if (!(_timeout > 0f))
		{
			return;
		}
		_timeout -= Time.deltaTime;
		if (!(_timeout <= 0f))
		{
			return;
		}
		_timeout = 0f;
		switch (_state)
		{
		case States.None:
			if (_deviceToConnect == null)
			{
				StatusMessage = "Can connected";
				_deviceToConnect = (from d in NetworkDeviceList
					where !d.Connected
					select (d)).FirstOrDefault();
				if (_deviceToConnect != null)
				{
					StatusMessage = $"Need connect: {_deviceToConnect.Name}";
					SetState(States.Connect, 0.1f);
				}
			}
			break;
		case States.StartScan:
			_state = States.None;
			BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (string address, string deviceName) =>
			{
				if (deviceName.StartsWith(_networkName))
				{
					StatusMessage = "Found " + address;
					if (NetworkDeviceList == null)
					{
						NetworkDeviceList = new List<NetworkDevice>();
					}
					NetworkDeviceList.Add(new NetworkDevice
					{
						Name = deviceName,
						Address = address,
						Connected = false
					});
					if (_deviceToConnect == null)
					{
						SetState(States.None, 0.01f);
					}
				}
			}, null, rssiOnly: true);
			break;
		case States.Connect:
			if (_deviceToConnect == null)
			{
				break;
			}
			StatusMessage = $"Connecting to {_deviceToConnect.Name}...";
			BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceToConnect.Address, null, null, (string address, string serviceUUID, string characteristicUUID) =>
			{
				Characteristic characteristic = GetCharacteristic(serviceUUID, characteristicUUID);
				if (characteristic != null)
				{
					characteristic.Found = true;
					if (AllCharacteristicsFound)
					{
						_deviceToConnect.Connected = true;
						SetState(States.Subscribe, 3f);
					}
				}
			}, (string disconnectAddress) =>
			{
				NetworkDevice networkDevice = (from d in NetworkDeviceList
					where d.Address.Equals(disconnectAddress)
					select (d)).FirstOrDefault();
				if (networkDevice != null)
				{
					StatusMessage = "Disconnected from " + networkDevice.Name;
					if (OnDeviceDisconnected != null && OnDeviceDisconnected != null)
					{
						OnDeviceDisconnected(networkDevice);
					}
					NetworkDeviceList.Remove(networkDevice);
					StatusMessage = $"1 device count: {NetworkDeviceList.Count}";
					if (networkDevice == _deviceToDisconnect)
					{
						_deviceToDisconnect = null;
						SetState(States.Disconnect, 0.1f);
					}
				}
			});
			break;
		case States.Subscribe:
			_state = States.None;
			StatusMessage = "Subscribe to device";
			BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceToConnect.Address, SampleCharacteristic.ServiceUUID, SampleCharacteristic.CharacteristicUUID, (string deviceAddressNotify, string characteristicNotify) =>
			{
				NetworkDevice networkDevice = (from d in NetworkDeviceList
					where d.Address.Equals(deviceAddressNotify)
					select (d)).FirstOrDefault();
				if (networkDevice != null && OnDeviceData != null)
				{
					OnDeviceReady(networkDevice);
					_deviceToConnect = null;
					StatusMessage = $"Device completely connected to {networkDevice.Name}";
					SetState(States.None, 0.1f);
				}
			}, (string deviceAddressData, string characteristricData, byte[] bytes) =>
			{
				NetworkDevice networkDevice = (from d in NetworkDeviceList
					where d.Address.Equals(deviceAddressData)
					select (d)).FirstOrDefault();
				if (networkDevice != null && OnDeviceData != null)
				{
					OnDeviceData(networkDevice, characteristricData, bytes);
				}
			});
			break;
		case States.Disconnect:
			_deviceToDisconnect = (from d in NetworkDeviceList
				where d.DoDisconnect
				select (d)).FirstOrDefault();
			if (_deviceToDisconnect != null)
			{
				SetState(States.Disconnecting, 5f);
				if (_deviceToDisconnect.Connected)
				{
					BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceToDisconnect.Address, (string address) =>
					{
					});
					break;
				}
				NetworkDeviceList.Remove(_deviceToDisconnect);
				StatusMessage = $"2 device count: {NetworkDeviceList.Count}";
				_deviceToDisconnect = null;
				_state = States.None;
			}
			else
			{
				_state = States.None;
				if (OnFinishedStoppingServer != null)
				{
					OnFinishedStoppingServer();
					OnFinishedStoppingServer = null;
				}
				_serverStarted = false;
			}
			break;
		case States.Disconnecting:
			if (_deviceToDisconnect != null && NetworkDeviceList != null && NetworkDeviceList.Contains(_deviceToDisconnect))
			{
				StatusMessage = $"3 device count: {NetworkDeviceList.Count}";
				NetworkDeviceList.Remove(_deviceToDisconnect);
				_deviceToDisconnect = null;
			}
			SetState(States.Disconnect, 0.1f);
			break;
		}
	}

	private bool IsEqual(string uuid1, string uuid2)
	{
		return uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0;
	}
}
