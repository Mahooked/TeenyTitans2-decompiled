using System;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothDeviceScript : MonoBehaviour
{
	public List<string> DiscoveredDeviceList;

	public Action InitializedAction;

	public Action DeinitializedAction;

	public Action<string> ErrorAction;

	public Action<string> ServiceAddedAction;

	public Action StartedAdvertisingAction;

	public Action StoppedAdvertisingAction;

	public Action<string, string> DiscoveredPeripheralAction;

	public Action<string, string, int, byte[]> DiscoveredPeripheralWithAdvertisingInfoAction;

	public Action<string, string> RetrievedConnectedPeripheralAction;

	public Action<string, byte[]> PeripheralReceivedWriteDataAction;

	public Action<string> ConnectedPeripheralAction;

	public Action<string> ConnectedDisconnectPeripheralAction;

	public Action<string> DisconnectedPeripheralAction;

	public Action<string, string> DiscoveredServiceAction;

	public Action<string, string, string> DiscoveredCharacteristicAction;

	public Action<string> DidWriteCharacteristicAction;

	public Dictionary<string, Dictionary<string, Action<string>>> DidUpdateNotificationStateForCharacteristicAction;

	public Dictionary<string, Dictionary<string, Action<string, string>>> DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction;

	public Dictionary<string, Dictionary<string, Action<string, byte[]>>> DidUpdateCharacteristicValueAction;

	public Dictionary<string, Dictionary<string, Action<string, string, byte[]>>> DidUpdateCharacteristicValueWithDeviceAddressAction;

	private const string deviceInitializedString = "Initialized";

	private const string deviceDeInitializedString = "DeInitialized";

	private const string deviceErrorString = "Error";

	private const string deviceServiceAdded = "ServiceAdded";

	private const string deviceStartedAdvertising = "StartedAdvertising";

	private const string deviceStoppedAdvertising = "StoppedAdvertising";

	private const string deviceDiscoveredPeripheral = "DiscoveredPeripheral";

	private const string deviceRetrievedConnectedPeripheral = "RetrievedConnectedPeripheral";

	private const string devicePeripheralReceivedWriteData = "PeripheralReceivedWriteData";

	private const string deviceConnectedPeripheral = "ConnectedPeripheral";

	private const string deviceDisconnectedPeripheral = "DisconnectedPeripheral";

	private const string deviceDiscoveredService = "DiscoveredService";

	private const string deviceDiscoveredCharacteristic = "DiscoveredCharacteristic";

	private const string deviceDidWriteCharacteristic = "DidWriteCharacteristic";

	private const string deviceDidUpdateNotificationStateForCharacteristic = "DidUpdateNotificationStateForCharacteristic";

	private const string deviceDidUpdateValueForCharacteristic = "DidUpdateValueForCharacteristic";

	private void Start()
	{
		DiscoveredDeviceList = new List<string>();
		DidUpdateNotificationStateForCharacteristicAction = new Dictionary<string, Dictionary<string, Action<string>>>();
		DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction = new Dictionary<string, Dictionary<string, Action<string, string>>>();
		DidUpdateCharacteristicValueAction = new Dictionary<string, Dictionary<string, Action<string, byte[]>>>();
		DidUpdateCharacteristicValueWithDeviceAddressAction = new Dictionary<string, Dictionary<string, Action<string, string, byte[]>>>();
	}

	private void Update()
	{
	}

	public void OnBluetoothMessage(string message)
	{
		if (message == null)
		{
			return;
		}
		char[] separator = new char[1] { '~' };
		string[] array = message.Split(separator);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += $"| {array[i]}";
		}
		BluetoothLEHardwareInterface.Log(text);
		if (message.Length >= "Initialized".Length && message.Substring(0, "Initialized".Length) == "Initialized")
		{
			if (InitializedAction != null)
			{
				InitializedAction();
			}
		}
		else if (message.Length >= "DeInitialized".Length && message.Substring(0, "DeInitialized".Length) == "DeInitialized")
		{
			BluetoothLEHardwareInterface.FinishDeInitialize();
			if (DeinitializedAction != null)
			{
				DeinitializedAction();
			}
		}
		else if (message.Length >= "Error".Length && message.Substring(0, "Error".Length) == "Error")
		{
			string obj = string.Empty;
			if (array.Length >= 2)
			{
				obj = array[1];
			}
			if (ErrorAction != null)
			{
				ErrorAction(obj);
			}
		}
		else if (message.Length >= "ServiceAdded".Length && message.Substring(0, "ServiceAdded".Length) == "ServiceAdded")
		{
			if (array.Length >= 2 && ServiceAddedAction != null)
			{
				ServiceAddedAction(array[1]);
			}
		}
		else if (message.Length >= "StartedAdvertising".Length && message.Substring(0, "StartedAdvertising".Length) == "StartedAdvertising")
		{
			BluetoothLEHardwareInterface.Log("Started Advertising");
			if (StartedAdvertisingAction != null)
			{
				StartedAdvertisingAction();
			}
		}
		else if (message.Length >= "StoppedAdvertising".Length && message.Substring(0, "StoppedAdvertising".Length) == "StoppedAdvertising")
		{
			BluetoothLEHardwareInterface.Log("Stopped Advertising");
			if (StoppedAdvertisingAction != null)
			{
				StoppedAdvertisingAction();
			}
		}
		else if (message.Length >= "DiscoveredPeripheral".Length && message.Substring(0, "DiscoveredPeripheral".Length) == "DiscoveredPeripheral")
		{
			if (array.Length < 3)
			{
				return;
			}
			if (!DiscoveredDeviceList.Contains(array[1]))
			{
				DiscoveredDeviceList.Add(array[1]);
				if (DiscoveredPeripheralAction != null)
				{
					DiscoveredPeripheralAction(array[1], array[2]);
				}
			}
			if (array.Length >= 5 && DiscoveredPeripheralWithAdvertisingInfoAction != null)
			{
				int result = 0;
				if (!int.TryParse(array[3], out result))
				{
					result = 0;
				}
				byte[] arg = Convert.FromBase64String(array[4]);
				DiscoveredPeripheralWithAdvertisingInfoAction(array[1], array[2], result, arg);
			}
		}
		else if (message.Length >= "RetrievedConnectedPeripheral".Length && message.Substring(0, "RetrievedConnectedPeripheral".Length) == "RetrievedConnectedPeripheral")
		{
			if (array.Length >= 3)
			{
				DiscoveredDeviceList.Add(array[1]);
				if (RetrievedConnectedPeripheralAction != null)
				{
					RetrievedConnectedPeripheralAction(array[1], array[2]);
				}
			}
		}
		else if (message.Length >= "PeripheralReceivedWriteData".Length && message.Substring(0, "PeripheralReceivedWriteData".Length) == "PeripheralReceivedWriteData")
		{
			if (array.Length >= 3)
			{
				OnPeripheralData(array[1], array[2]);
			}
		}
		else if (message.Length >= "ConnectedPeripheral".Length && message.Substring(0, "ConnectedPeripheral".Length) == "ConnectedPeripheral")
		{
			if (array.Length >= 2 && ConnectedPeripheralAction != null)
			{
				ConnectedPeripheralAction(array[1]);
			}
		}
		else if (message.Length >= "DisconnectedPeripheral".Length && message.Substring(0, "DisconnectedPeripheral".Length) == "DisconnectedPeripheral")
		{
			if (array.Length >= 2)
			{
				if (ConnectedDisconnectPeripheralAction != null)
				{
					ConnectedDisconnectPeripheralAction(array[1]);
				}
				if (DisconnectedPeripheralAction != null)
				{
					DisconnectedPeripheralAction(array[1]);
				}
			}
		}
		else if (message.Length >= "DiscoveredService".Length && message.Substring(0, "DiscoveredService".Length) == "DiscoveredService")
		{
			if (array.Length >= 3 && DiscoveredServiceAction != null)
			{
				DiscoveredServiceAction(array[1], array[2]);
			}
		}
		else if (message.Length >= "DiscoveredCharacteristic".Length && message.Substring(0, "DiscoveredCharacteristic".Length) == "DiscoveredCharacteristic")
		{
			if (array.Length >= 4 && DiscoveredCharacteristicAction != null)
			{
				DiscoveredCharacteristicAction(array[1], array[2], array[3]);
			}
		}
		else if (message.Length >= "DidWriteCharacteristic".Length && message.Substring(0, "DidWriteCharacteristic".Length) == "DidWriteCharacteristic")
		{
			if (array.Length >= 2 && DidWriteCharacteristicAction != null)
			{
				DidWriteCharacteristicAction(array[1]);
			}
		}
		else if (message.Length >= "DidUpdateNotificationStateForCharacteristic".Length && message.Substring(0, "DidUpdateNotificationStateForCharacteristic".Length) == "DidUpdateNotificationStateForCharacteristic")
		{
			if (array.Length < 3)
			{
				return;
			}
			if (DidUpdateNotificationStateForCharacteristicAction != null && DidUpdateNotificationStateForCharacteristicAction.ContainsKey(array[1]))
			{
				Dictionary<string, Action<string>> dictionary = DidUpdateNotificationStateForCharacteristicAction[array[1]];
				if (dictionary != null && dictionary.ContainsKey(array[2]))
				{
					dictionary[array[2]]?.Invoke(array[2]);
				}
			}
			if (DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction != null && DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction.ContainsKey(array[1]))
			{
				Dictionary<string, Action<string, string>> dictionary2 = DidUpdateNotificationStateForCharacteristicWithDeviceAddressAction[array[1]];
				if (dictionary2 != null && dictionary2.ContainsKey(array[2]))
				{
					dictionary2[array[2]]?.Invoke(array[1], array[2]);
				}
			}
		}
		else if (message.Length >= "DidUpdateValueForCharacteristic".Length && message.Substring(0, "DidUpdateValueForCharacteristic".Length) == "DidUpdateValueForCharacteristic" && array.Length >= 4)
		{
			OnBluetoothData(array[1], array[2], array[3]);
		}
	}

	public void OnBluetoothData(string base64Data)
	{
		OnBluetoothData(string.Empty, string.Empty, base64Data);
	}

	public void OnBluetoothData(string deviceAddress, string characteristic, string base64Data)
	{
		if (base64Data == null)
		{
			return;
		}
		byte[] array = Convert.FromBase64String(base64Data);
		if (array.Length <= 0)
		{
			return;
		}
		deviceAddress = deviceAddress.ToUpper();
		characteristic = characteristic.ToUpper();
		BluetoothLEHardwareInterface.Log("Device: " + deviceAddress + " Characteristic Received: " + characteristic);
		string text = string.Empty;
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			text += $"{b:X2}";
		}
		BluetoothLEHardwareInterface.Log(text);
		if (DidUpdateCharacteristicValueAction != null && DidUpdateCharacteristicValueAction.ContainsKey(deviceAddress))
		{
			Dictionary<string, Action<string, byte[]>> dictionary = DidUpdateCharacteristicValueAction[deviceAddress];
			characteristic = characteristic.ToLower();
			if (dictionary != null && dictionary.ContainsKey(characteristic))
			{
				dictionary[characteristic]?.Invoke(characteristic, array);
			}
		}
		if (DidUpdateCharacteristicValueWithDeviceAddressAction != null && DidUpdateCharacteristicValueWithDeviceAddressAction.ContainsKey(deviceAddress))
		{
			Dictionary<string, Action<string, string, byte[]>> dictionary2 = DidUpdateCharacteristicValueWithDeviceAddressAction[deviceAddress];
			characteristic = characteristic.ToLower();
			if (dictionary2 != null && dictionary2.ContainsKey(characteristic))
			{
				dictionary2[characteristic]?.Invoke(deviceAddress, characteristic, array);
			}
		}
	}

	public void OnPeripheralData(string characteristic, string base64Data)
	{
		if (base64Data == null)
		{
			return;
		}
		byte[] array = Convert.FromBase64String(base64Data);
		if (array.Length > 0)
		{
			BluetoothLEHardwareInterface.Log("Peripheral Received: " + characteristic);
			string text = string.Empty;
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				text += $"{b:X2}";
			}
			BluetoothLEHardwareInterface.Log(text);
			if (PeripheralReceivedWriteDataAction != null)
			{
				PeripheralReceivedWriteDataAction(characteristic, array);
			}
		}
	}
}
