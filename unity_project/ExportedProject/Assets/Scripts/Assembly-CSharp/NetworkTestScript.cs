using System;
using System.Collections.Generic;
using Shatalmic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkTestScript : MonoBehaviour
{
	private Networking networking;

	private List<Networking.NetworkDevice> connectedDeviceList;

	private bool isServer = true;

	private bool writeDeviceBytes;

	private byte[] bytesToWrite;

	private int deviceToWriteIndex;

	private bool sendingCubeRotation;

	private Quaternion newCubeRotation = Quaternion.identity;

	private Networking.NetworkDevice deviceToSkip;

	public GameObject Cube;

	public Text TextStatus;

	public Text NetworkName;

	public Text ClientName;

	public GameObject ButtonStartServer;

	public GameObject ButtonStartClient;

	public GameObject ButtonStopNetwork;

	private void Update()
	{
		if (writeDeviceBytes)
		{
			writeDeviceBytes = false;
			if (bytesToWrite != null && connectedDeviceList != null && deviceToWriteIndex >= 0 && deviceToWriteIndex < connectedDeviceList.Count)
			{
				Action action = () =>
				{
					deviceToWriteIndex++;
					writeDeviceBytes = deviceToWriteIndex < connectedDeviceList.Count;
					if (!writeDeviceBytes)
					{
						bytesToWrite = null;
						sendingCubeRotation = false;
					}
				};
				Networking.NetworkDevice networkDevice = connectedDeviceList[deviceToWriteIndex];
				if (networkDevice != deviceToSkip)
				{
					networking.WriteDevice(networkDevice, bytesToWrite, action);
				}
				else
				{
					action();
				}
			}
		}
		if (newCubeRotation != Quaternion.identity)
		{
			if (Cube != null)
			{
				Cube.transform.rotation = newCubeRotation;
			}
			newCubeRotation = Quaternion.identity;
		}
	}

	protected void SendCubeRotation(Quaternion rotation, Networking.NetworkDevice device = null)
	{
		if (sendingCubeRotation)
		{
			return;
		}
		sendingCubeRotation = true;
		byte[] array = new byte[16];
		Array.Copy(BitConverter.GetBytes(rotation.x), 0, array, 0, 4);
		Array.Copy(BitConverter.GetBytes(rotation.y), 0, array, 4, 4);
		Array.Copy(BitConverter.GetBytes(rotation.z), 0, array, 8, 4);
		Array.Copy(BitConverter.GetBytes(rotation.w), 0, array, 12, 4);
		if (isServer)
		{
			if (device == null)
			{
				if (connectedDeviceList != null)
				{
					if (connectedDeviceList.Count == 1)
					{
						if (deviceToSkip == null)
						{
							networking.WriteDevice(connectedDeviceList[0], array, () =>
							{
								sendingCubeRotation = false;
							});
						}
						else
						{
							deviceToSkip = null;
							sendingCubeRotation = false;
						}
					}
					else
					{
						deviceToWriteIndex = 0;
						writeDeviceBytes = true;
						bytesToWrite = array;
					}
				}
				else if (deviceToSkip != null)
				{
					deviceToSkip = null;
				}
			}
			else
			{
				networking.WriteDevice(device, array, () =>
				{
					sendingCubeRotation = false;
				});
			}
		}
		else
		{
			networking.SendFromClient(array);
			sendingCubeRotation = false;
		}
	}

	protected void ParseCubePosition(byte[] bytes)
	{
		newCubeRotation = new Quaternion(BitConverter.ToSingle(bytes, 0), BitConverter.ToSingle(bytes, 4), BitConverter.ToSingle(bytes, 8), BitConverter.ToSingle(bytes, 12));
		if (isServer)
		{
			SendCubeRotation(newCubeRotation);
		}
	}

	public void OnButton(Button button)
	{
		switch (button.name)
		{
		case "ButtonStartServer":
			if (string.IsNullOrEmpty(NetworkName.text))
			{
				TextStatus.text = "Enter a network name";
				break;
			}
			TextStatus.text = string.Empty;
			deviceToSkip = null;
			deviceToWriteIndex = 0;
			sendingCubeRotation = false;
			isServer = true;
			ButtonStartServer.SetActive(false);
			ButtonStartClient.SetActive(false);
			ButtonStopNetwork.SetActive(true);
			if (Cube != null)
			{
				Cube.SetActive(false);
			}
			networking.StartServer(NetworkName.text, (Networking.NetworkDevice connectedDevice) =>
			{
				if (connectedDeviceList == null)
				{
					connectedDeviceList = new List<Networking.NetworkDevice>();
				}
				if (!connectedDeviceList.Contains(connectedDevice))
				{
					connectedDeviceList.Add(connectedDevice);
					if (Cube != null)
					{
						Cube.SetActive(true);
						SendCubeRotation(Cube.transform.rotation, connectedDevice);
					}
				}
			}, (Networking.NetworkDevice disconnectedDevice) =>
			{
				if (connectedDeviceList != null && connectedDeviceList.Contains(disconnectedDevice))
				{
					connectedDeviceList.Remove(disconnectedDevice);
				}
			}, (Networking.NetworkDevice dataDevice, string characteristic, byte[] bytes) =>
			{
				deviceToSkip = dataDevice;
				ParseCubePosition(bytes);
			});
			break;
		case "ButtonStartClient":
			if (string.IsNullOrEmpty(NetworkName.text) || string.IsNullOrEmpty(ClientName.text))
			{
				TextStatus.text = "Enter both a network and client name";
				break;
			}
			TextStatus.text = string.Empty;
			isServer = false;
			ButtonStartServer.SetActive(false);
			ButtonStartClient.SetActive(false);
			ButtonStopNetwork.SetActive(true);
			if (Cube != null)
			{
				Cube.SetActive(false);
			}
			networking.StartClient(NetworkName.text, ClientName.text, () =>
			{
				networking.StatusMessage = "Started advertising";
			}, (string clientName, string characteristic, byte[] bytes) =>
			{
				if (Cube != null)
				{
					Cube.SetActive(true);
				}
				ParseCubePosition(bytes);
			});
			break;
		case "ButtonStopNetwork":
			if (isServer)
			{
				if (connectedDeviceList != null)
				{
					connectedDeviceList = null;
				}
				networking.StopServer(() =>
				{
					ButtonStartServer.SetActive(true);
					ButtonStartClient.SetActive(true);
					ButtonStopNetwork.SetActive(false);
					if (Cube != null)
					{
						Cube.SetActive(false);
					}
				});
				break;
			}
			networking.StopClient(() =>
			{
				ButtonStartServer.SetActive(true);
				ButtonStartClient.SetActive(true);
				ButtonStopNetwork.SetActive(false);
				if (Cube != null)
				{
					Cube.SetActive(false);
				}
			});
			break;
		}
	}

	private void Start()
	{
		ButtonStartServer.SetActive(true);
		ButtonStartClient.SetActive(true);
		ButtonStopNetwork.SetActive(false);
		if (Cube != null)
		{
			Cube.SetActive(false);
		}
		if (networking == null)
		{
			networking = GetComponent<Networking>();
			networking.Initialize((string error) =>
			{
				BluetoothLEHardwareInterface.Log("Error: " + error);
			}, (string message) =>
			{
				if (TextStatus != null)
				{
					TextStatus.text = message;
				}
				BluetoothLEHardwareInterface.Log("Message: " + message);
			});
		}
		if (!(Cube != null))
		{
			return;
		}
		MouseDragRotate component = Cube.GetComponent<MouseDragRotate>();
		if (component != null)
		{
			component.OnMouseEvent = (Quaternion rotation) =>
			{
				deviceToSkip = null;
				SendCubeRotation(rotation);
			};
		}
	}
}
