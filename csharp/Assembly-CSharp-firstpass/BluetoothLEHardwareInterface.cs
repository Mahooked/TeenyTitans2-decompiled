using System;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothLEHardwareInterface
{
	public enum CBCharacteristicProperties
	{
		CBCharacteristicPropertyBroadcast = 1,
		CBCharacteristicPropertyRead = 2,
		CBCharacteristicPropertyWriteWithoutResponse = 4,
		CBCharacteristicPropertyWrite = 8,
		CBCharacteristicPropertyNotify = 0x10,
		CBCharacteristicPropertyIndicate = 0x20,
		CBCharacteristicPropertyAuthenticatedSignedWrites = 0x40,
		CBCharacteristicPropertyExtendedProperties = 0x80,
		CBCharacteristicPropertyNotifyEncryptionRequired = 0x100,
		CBCharacteristicPropertyIndicateEncryptionRequired = 0x200
	}

	public enum CBAttributePermissions
	{
		CBAttributePermissionsReadable = 1,
		CBAttributePermissionsWriteable = 16,
		CBAttributePermissionsReadEncryptionRequired = 2,
		CBAttributePermissionsWriteEncryptionRequired = 32
	}

	private static AndroidJavaObject _android;

	private static BluetoothDeviceScript bluetoothDeviceScript;

	public static void Log(string message)
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidBluetoothLog", message);
		}
	}

	public static BluetoothDeviceScript Initialize(bool asCentral, bool asPeripheral, Action action, Action<string> errorAction)
	{
		bluetoothDeviceScript = null;
		GameObject gameObject = GameObject.Find("BluetoothLEReceiver");
		if (gameObject == null)
		{
			gameObject = new GameObject("BluetoothLEReceiver");
			bluetoothDeviceScript = gameObject.AddComponent<BluetoothDeviceScript>();
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.InitializedAction = action;
				bluetoothDeviceScript.ErrorAction = errorAction;
			}
		}
		else
		{
			bluetoothDeviceScript = gameObject.GetComponent<BluetoothDeviceScript>();
		}
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		if (Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.SendMessage("OnBluetoothMessage", "Initialized");
			}
		}
		else
		{
			if (_android == null)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE");
				_android = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
			}
			if (_android != null)
			{
				_android.Call("androidBluetoothInitialize", asCentral, asPeripheral);
			}
		}
		return bluetoothDeviceScript;
	}

	public static void DeInitialize(Action action)
	{
		if (bluetoothDeviceScript != null)
		{
			bluetoothDeviceScript.DeinitializedAction = action;
		}
		if (Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.SendMessage("OnBluetoothMessage", "DeInitialized");
			}
		}
		else if (_android != null)
		{
			_android.Call("androidBluetoothDeInitialize");
		}
	}

	public static void FinishDeInitialize()
	{
		GameObject gameObject = GameObject.Find("BluetoothLEReceiver");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public static void BluetoothEnable(bool enable)
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidBluetoothEnable", enable);
		}
	}

	public static void PauseMessages(bool isPaused)
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidBluetoothPause", isPaused);
		}
	}

	public static void ScanForPeripheralsWithServices(string[] serviceUUIDs, Action<string, string> action, Action<string, string, int, byte[]> actionAdvertisingInfo = null, bool rssiOnly = false, bool clearPeripheralList = true, int recordType = 255)
	{
		if (Application.isEditor)
		{
			return;
		}
		if (bluetoothDeviceScript != null)
		{
			Log($"action: {action}");
			bluetoothDeviceScript.DiscoveredPeripheralAction = action;
			bluetoothDeviceScript.DiscoveredPeripheralWithAdvertisingInfoAction = actionAdvertisingInfo;
			if (bluetoothDeviceScript.DiscoveredDeviceList != null)
			{
				bluetoothDeviceScript.DiscoveredDeviceList.Clear();
			}
		}
		string text = null;
		if (serviceUUIDs != null && serviceUUIDs.Length > 0)
		{
			text = string.Empty;
			foreach (string text2 in serviceUUIDs)
			{
				text = text + text2 + "|";
			}
			text = text.Substring(0, text.Length - 1);
		}
		if (_android != null)
		{
			if (text == null)
			{
				text = string.Empty;
			}
			_android.Call("androidBluetoothScanForPeripheralsWithServices", text, rssiOnly, recordType);
		}
	}

	public static void RetrieveListOfPeripheralsWithServices(string[] serviceUUIDs, Action<string, string> action)
	{
		if (Application.isEditor)
		{
			return;
		}
		if (bluetoothDeviceScript != null)
		{
			bluetoothDeviceScript.RetrievedConnectedPeripheralAction = action;
			if (bluetoothDeviceScript.DiscoveredDeviceList != null)
			{
				bluetoothDeviceScript.DiscoveredDeviceList.Clear();
			}
		}
		string text = ((serviceUUIDs.Length <= 0) ? null : string.Empty);
		foreach (string text2 in serviceUUIDs)
		{
			text = text + text2 + "|";
		}
		text = text.Substring(0, text.Length - 1);
		if (_android != null)
		{
			_android.Call("androidBluetoothRetrieveListOfPeripheralsWithServices", text);
		}
	}

	public static void StopScan()
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidBluetoothStopScan");
		}
	}

	public static void DisconnectAll()
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidBluetoothDisconnectAll");
		}
	}

	public static void ConnectToPeripheral(string name, Action<string> connectAction, Action<string, string> serviceAction, Action<string, string, string> characteristicAction, Action<string> disconnectAction = null)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.ConnectedPeripheralAction = connectAction;
				bluetoothDeviceScript.DiscoveredServiceAction = serviceAction;
				bluetoothDeviceScript.DiscoveredCharacteristicAction = characteristicAction;
				bluetoothDeviceScript.ConnectedDisconnectPeripheralAction = disconnectAction;
			}
			if (_android != null)
			{
				_android.Call("androidBluetoothConnectToPeripheral", name);
			}
		}
	}

	public static void DisconnectPeripheral(string name, Action<string> action)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.DisconnectedPeripheralAction = action;
			}
			if (_android != null)
			{
				_android.Call("androidBluetoothDisconnectPeripheral", name);
			}
		}
	}

	public static void ReadCharacteristic(string name, string service, string characteristic, Action<string, byte[]> action)
	{
		if (Application.isEditor)
		{
			return;
		}
		if (bluetoothDeviceScript != null)
		{
			if (!bluetoothDeviceScript.DidUpdateCharacteristicValueAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateCharacteristicValueAction[name] = new Dictionary<string, Action<string, byte[]>>();
			}
			bluetoothDeviceScript.DidUpdateCharacteristicValueAction[name][FullUUID(characteristic).ToLower()] = action;
		}
		if (_android != null)
		{
			_android.Call("androidReadCharacteristic", name, service, characteristic);
		}
	}

	public static void WriteCharacteristic(string name, string service, string characteristic, byte[] data, int length, bool withResponse, Action<string> action)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.DidWriteCharacteristicAction = action;
			}
			if (_android != null)
			{
				_android.Call("androidWriteCharacteristic", name, service, characteristic, data, length, withResponse);
			}
		}
	}

	public static void SubscribeCharacteristic(string name, string service, string characteristic, Action<string> notificationAction, Action<string, byte[]> action)
	{
		if (Application.isEditor)
		{
			return;
		}
		if (bluetoothDeviceScript != null)
		{
			name = name.ToUpper();
			service = service.ToUpper();
			characteristic = characteristic.ToUpper();
			if (!bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicAction[name] = new Dictionary<string, Action<string>>();
			}
			bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicAction[name][FullUUID(characteristic).ToLower()] = notificationAction;
			if (!bluetoothDeviceScript.DidUpdateCharacteristicValueAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateCharacteristicValueAction[name] = new Dictionary<string, Action<string, byte[]>>();
			}
			bluetoothDeviceScript.DidUpdateCharacteristicValueAction[name][FullUUID(characteristic).ToLower()] = action;
		}
		if (_android != null)
		{
			_android.Call("androidSubscribeCharacteristic", name, service, characteristic);
		}
	}

	public static void SubscribeCharacteristicWithDeviceAddress(string name, string service, string characteristic, Action<string, string> notificationAction, Action<string, string, byte[]> action)
	{
		if (Application.isEditor)
		{
			return;
		}
		if (bluetoothDeviceScript != null)
		{
			name = name.ToUpper();
			service = service.ToUpper();
			characteristic = characteristic.ToUpper();
			if (!bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction[name] = new Dictionary<string, Action<string, string>>();
			}
			bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction[name][FullUUID(characteristic).ToLower()] = notificationAction;
			if (!bluetoothDeviceScript.DidUpdateCharacteristicValueWithDeviceAddressAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateCharacteristicValueWithDeviceAddressAction[name] = new Dictionary<string, Action<string, string, byte[]>>();
			}
			bluetoothDeviceScript.DidUpdateCharacteristicValueWithDeviceAddressAction[name][FullUUID(characteristic).ToLower()] = action;
		}
		if (_android != null)
		{
			_android.Call("androidSubscribeCharacteristic", name, service, characteristic);
		}
	}

	public static void UnSubscribeCharacteristic(string name, string service, string characteristic, Action<string> action)
	{
		if (Application.isEditor)
		{
			return;
		}
		if (bluetoothDeviceScript != null)
		{
			name = name.ToUpper();
			service = service.ToUpper();
			characteristic = characteristic.ToUpper();
			if (!bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction[name] = new Dictionary<string, Action<string, string>>();
			}
			bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction[name][FullUUID(characteristic).ToLower()] = null;
			if (!bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicAction.ContainsKey(name))
			{
				bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicAction[name] = new Dictionary<string, Action<string>>();
			}
			bluetoothDeviceScript.DidUpdateNotificationStateForCharacteristicAction[name][FullUUID(characteristic).ToLower()] = null;
		}
		if (_android != null)
		{
			_android.Call("androidUnsubscribeCharacteristic", name, service, characteristic);
		}
	}

	public static void PeripheralName(string newName)
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidPeripheralName", newName);
		}
	}

	public static void CreateService(string uuid, bool primary, Action<string> action)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.ServiceAddedAction = action;
			}
			if (_android != null)
			{
				_android.Call("androidCreateService", uuid, primary);
			}
		}
	}

	public static void RemoveService(string uuid)
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidRemoveService", uuid);
		}
	}

	public static void RemoveServices()
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidRemoveServices");
		}
	}

	public static void CreateCharacteristic(string uuid, CBCharacteristicProperties properties, CBAttributePermissions permissions, byte[] data, int length, Action<string, byte[]> action)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.PeripheralReceivedWriteDataAction = action;
			}
			if (_android != null)
			{
				_android.Call("androidCreateCharacteristic", uuid, (int)properties, (int)permissions, data, length);
			}
		}
	}

	public static void RemoveCharacteristic(string uuid)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.PeripheralReceivedWriteDataAction = null;
			}
			if (_android != null)
			{
				_android.Call("androidRemoveCharacteristic", uuid);
			}
		}
	}

	public static void RemoveCharacteristics()
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidRemoveCharacteristics");
		}
	}

	public static void StartAdvertising(Action action)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.StartedAdvertisingAction = action;
			}
			if (_android != null)
			{
				_android.Call("androidStartAdvertising");
			}
		}
	}

	public static void StopAdvertising(Action action)
	{
		if (!Application.isEditor)
		{
			if (bluetoothDeviceScript != null)
			{
				bluetoothDeviceScript.StoppedAdvertisingAction = action;
			}
			if (_android != null)
			{
				_android.Call("androidStopAdvertising");
			}
		}
	}

	public static void UpdateCharacteristicValue(string uuid, byte[] data, int length)
	{
		if (!Application.isEditor && _android != null)
		{
			_android.Call("androidUpdateCharacteristicValue", uuid, data, length);
		}
	}

	public static string FullUUID(string uuid)
	{
		if (uuid.Length == 4)
		{
			return "0000" + uuid + "-0000-1000-8000-00805f9b34fb";
		}
		return uuid;
	}
}
