using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
	public byte maxPlayersPerRoom = 2;

	public GameObject controlPanel;

	public GameObject progressLabel;

	private string _gameVersion = "1";

	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	private void Start()
	{
		progressLabel.SetActive(value: false);
		controlPanel.SetActive(value: true);
	}

	public void Connect()
	{
		progressLabel.SetActive(value: true);
		controlPanel.SetActive(value: false);
		if (PhotonNetwork.IsConnected)
		{
			PhotonNetwork.JoinRandomRoom();
			return;
		}
		PhotonNetwork.GameVersion = _gameVersion;
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		Debug.Log("Launcher: OnDisconnectedFromPhoton() was called by PUN");
		progressLabel.SetActive(value: false);
		controlPanel.SetActive(value: true);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("Launcher: OnPhotonRandomJoinFailed() was called by PUN");
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			MaxPlayers = maxPlayersPerRoom
		});
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Launcher: OnJoinedRoom() was called by PUN. Now this client is in a room");
	}
}
