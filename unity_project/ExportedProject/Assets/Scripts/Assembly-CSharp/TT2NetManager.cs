using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class TT2NetManager : MonoBehaviourPunCallbacks
{
	[Serializable]
	public class NetworkBattleRequest
	{
		public int initiatorPlayerID;

		public int otherPlayerID;

		public bool hasPendingBattleRequest;
	}

	private static TT2NetManager _instance;

	private static bool didApplicationQuit;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ConnectedToNetworkEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action DisconnectedFromNetworkEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action JoinRoomFailed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnJoinedRoomEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ExitGames.Client.Photon.Hashtable> CustomRoomPropertiesUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TT2NetPlayer> PlayerPropertiesUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Photon.Realtime.Player> OnPlayerConnectedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Photon.Realtime.Player> OnPlayerDisconnectedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TT2NetPlayer> OnMatchFound__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TT2NetPlayer, int> OnPrepareForBattleEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<TT2NetPlayer> OnBattleAcceptedEvent__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SearchForMatchCanceled__BackingField;

	private RoomInfo[] roomsList;

	private TT2NetMatchmakingSettings.SearchGroup searchGroup;

	private int searchStepIndex;

	private IEnumerator waitToRetryCoroutine;

	private IEnumerator waitForOtherPlayerCoroutine;

	private bool isSearchCanceled;

	public List<NetworkBattleRequest> networkBattleRequests = new List<NetworkBattleRequest>();

	public List<TT2NetPlayer> networkPlayers = new List<TT2NetPlayer>();

	private GUIStyle labelStyle;

	private static List<string> log = new List<string>();

	private static int msgCount = 0;

	private static int maxLines = 4;

	public static TT2NetManager Instance
	{
		get
		{
			if (_instance == null && !didApplicationQuit)
			{
				_instance = new GameObject("TT2NetManager").AddComponent<TT2NetManager>();
				PhotonStatsGui photonStatsGui = _instance.gameObject.AddComponent<PhotonStatsGui>();
				photonStatsGui.statsWindowOn = false;
			}
			return _instance;
		}
	}

	public bool IsSearchingForMatch { get; private set; }

	public TT2NetPlayer masterClientPlayer
	{
		get
		{
			return networkPlayers.Find((TT2NetPlayer x) => x.IsMasterClient);
		}
	}

	public TT2NetPlayer myNetworkPlayer
	{
		get
		{
			return networkPlayers.Find((TT2NetPlayer x) => x != null && x.IsMine);
		}
	}

	public TT2NetPlayer otherNetworkPlayer
	{
		get
		{
			return networkPlayers.Find((TT2NetPlayer x) => x != null && !x.IsMine);
		}
	}

	public bool IsConnectedToMaster
	{
		get
		{
			return PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterserver;
		}
	}

	public bool IsConnected
	{
		get
		{
			return PhotonNetwork.IsConnected;
		}
	}

	public bool IsConnectedAndReady
	{
		get
		{
			return PhotonNetwork.IsConnectedAndReady;
		}
	}

	public bool InRoom
	{
		get
		{
			return PhotonNetwork.InRoom;
		}
	}

	public string CurrentRegion
	{
		get
		{
			return PhotonNetwork.CloudRegion;
		}
	}

	public int StartTime { get; private set; }

	public int PrevServerTimestamp { get; private set; }

	public int TimeSinceStart
	{
		get
		{
			return PhotonNetwork.ServerTimestamp - StartTime;
		}
	}

	public float TimeSinceStartSeconds
	{
		get
		{
			return (float)TimeSinceStart / 1000f;
		}
	}

	public int DeltaTime
	{
		get
		{
			return PhotonNetwork.ServerTimestamp - PrevServerTimestamp;
		}
	}

	public float DeltaTimeSeconds
	{
		get
		{
			return (float)DeltaTime / 1000f;
		}
	}

	public int LastSeed { get; set; }

	public bool IsPlayerPlayingAction
	{
		get
		{
			if (myNetworkPlayer == null || otherNetworkPlayer == null)
			{
				return false;
			}
			if (myNetworkPlayer.IsPlayingAction || otherNetworkPlayer.IsPlayingAction)
			{
				return true;
			}
			return false;
		}
	}

	public event Action ConnectedToNetworkEvent
	{
		add
		{
			Action action = ConnectedToNetworkEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ConnectedToNetworkEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ConnectedToNetworkEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ConnectedToNetworkEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action DisconnectedFromNetworkEvent
	{
		add
		{
			Action action = DisconnectedFromNetworkEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DisconnectedFromNetworkEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DisconnectedFromNetworkEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DisconnectedFromNetworkEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action JoinRoomFailed
	{
		add
		{
			Action action = JoinRoomFailed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref JoinRoomFailed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = JoinRoomFailed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref JoinRoomFailed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnJoinedRoomEvent
	{
		add
		{
			Action action = OnJoinedRoomEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnJoinedRoomEvent__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnJoinedRoomEvent__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnJoinedRoomEvent__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<ExitGames.Client.Photon.Hashtable> CustomRoomPropertiesUpdated
	{
		add
		{
			Action<ExitGames.Client.Photon.Hashtable> action = CustomRoomPropertiesUpdated__BackingField;
			Action<ExitGames.Client.Photon.Hashtable> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CustomRoomPropertiesUpdated__BackingField, (Action<ExitGames.Client.Photon.Hashtable>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ExitGames.Client.Photon.Hashtable> action = CustomRoomPropertiesUpdated__BackingField;
			Action<ExitGames.Client.Photon.Hashtable> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CustomRoomPropertiesUpdated__BackingField, (Action<ExitGames.Client.Photon.Hashtable>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TT2NetPlayer> PlayerPropertiesUpdated
	{
		add
		{
			Action<TT2NetPlayer> action = PlayerPropertiesUpdated__BackingField;
			Action<TT2NetPlayer> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerPropertiesUpdated__BackingField, (Action<TT2NetPlayer>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TT2NetPlayer> action = PlayerPropertiesUpdated__BackingField;
			Action<TT2NetPlayer> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerPropertiesUpdated__BackingField, (Action<TT2NetPlayer>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Photon.Realtime.Player> OnPlayerConnectedEvent
	{
		add
		{
			Action<Photon.Realtime.Player> action = OnPlayerConnectedEvent__BackingField;
			Action<Photon.Realtime.Player> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerConnectedEvent__BackingField, (Action<Photon.Realtime.Player>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Photon.Realtime.Player> action = OnPlayerConnectedEvent__BackingField;
			Action<Photon.Realtime.Player> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerConnectedEvent__BackingField, (Action<Photon.Realtime.Player>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Photon.Realtime.Player> OnPlayerDisconnectedEvent
	{
		add
		{
			Action<Photon.Realtime.Player> action = OnPlayerDisconnectedEvent__BackingField;
			Action<Photon.Realtime.Player> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerDisconnectedEvent__BackingField, (Action<Photon.Realtime.Player>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Photon.Realtime.Player> action = OnPlayerDisconnectedEvent__BackingField;
			Action<Photon.Realtime.Player> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerDisconnectedEvent__BackingField, (Action<Photon.Realtime.Player>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TT2NetPlayer> OnMatchFound
	{
		add
		{
			Action<TT2NetPlayer> action = OnMatchFound__BackingField;
			Action<TT2NetPlayer> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnMatchFound__BackingField, (Action<TT2NetPlayer>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TT2NetPlayer> action = OnMatchFound__BackingField;
			Action<TT2NetPlayer> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnMatchFound__BackingField, (Action<TT2NetPlayer>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TT2NetPlayer, int> OnPrepareForBattleEvent
	{
		add
		{
			Action<TT2NetPlayer, int> action = OnPrepareForBattleEvent__BackingField;
			Action<TT2NetPlayer, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPrepareForBattleEvent__BackingField, (Action<TT2NetPlayer, int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TT2NetPlayer, int> action = OnPrepareForBattleEvent__BackingField;
			Action<TT2NetPlayer, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPrepareForBattleEvent__BackingField, (Action<TT2NetPlayer, int>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<TT2NetPlayer> OnBattleAcceptedEvent
	{
		add
		{
			Action<TT2NetPlayer> action = OnBattleAcceptedEvent__BackingField;
			Action<TT2NetPlayer> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnBattleAcceptedEvent__BackingField, (Action<TT2NetPlayer>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<TT2NetPlayer> action = OnBattleAcceptedEvent__BackingField;
			Action<TT2NetPlayer> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnBattleAcceptedEvent__BackingField, (Action<TT2NetPlayer>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action SearchForMatchCanceled
	{
		add
		{
			Action action = SearchForMatchCanceled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SearchForMatchCanceled__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SearchForMatchCanceled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SearchForMatchCanceled__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		PhotonNetwork.AutomaticallySyncScene = false;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}

	private void OnApplicationQuit()
	{
		didApplicationQuit = true;
	}

	private void LateUpdate()
	{
		PrevServerTimestamp = PhotonNetwork.ServerTimestamp;
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		Log("[TT2NetManager::OnPhotonPlayerConnected] - " + newPlayer.NickName + " joined.");
		if (OnPlayerConnectedEvent__BackingField != null)
		{
			OnPlayerConnectedEvent__BackingField(newPlayer);
		}
		if (PhotonNetwork.CurrentRoom.PlayerCount != GameManager.instance.gameSettings.networkMaxPlayersPerRoom || !PhotonNetwork.IsMasterClient || !(otherNetworkPlayer != null))
		{
			return;
		}
		Log("[TT2NetManager::OnPhotonPlayerConnected] - Match Found!");
		if (IsSearchingForMatch)
		{
			IsSearchingForMatch = false;
			if (waitForOtherPlayerCoroutine != null)
			{
				StopCoroutine(waitForOtherPlayerCoroutine);
				waitForOtherPlayerCoroutine = null;
			}
		}
		if (OnMatchFound__BackingField != null)
		{
			OnMatchFound__BackingField(otherNetworkPlayer);
		}
	}

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
		Log("OnPhotonPlayerDisconnected(): " + otherPlayer.NickName);
		if (PhotonNetwork.IsMasterClient)
		{
			Log("OnPhotonPlayerDisconnected(): isMasterClient " + PhotonNetwork.IsMasterClient);
		}
		if (OnPlayerDisconnectedEvent__BackingField != null)
		{
			OnPlayerDisconnectedEvent__BackingField(otherPlayer);
		}
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		Log("OnReceivedRoomListUpdate() Total Players: " + PhotonNetwork.PlayerList.Length);
		roomsList = roomList.ToArray();
	}

	public override void OnLeftRoom()
	{
		Log("You left the room");
	}

	public override void OnConnectedToMaster()
	{
		Log("[TT2NetManager::OnConnectedToMaster] - Network Region: " + PhotonNetwork.NetworkingClient.CloudRegion);
		if (ConnectedToNetworkEvent__BackingField != null)
		{
			ConnectedToNetworkEvent__BackingField();
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Log("[TT2NetManager::OnPhotonRandomJoinFailed] - <Color=Red> Failed </Color>: Next -> Try next step");
		if (IsSearchingForMatch && !isSearchCanceled)
		{
			waitToRetryCoroutine = WaitToRetry();
			StartCoroutine(waitToRetryCoroutine);
		}
		if (JoinRoomFailed__BackingField != null)
		{
			JoinRoomFailed__BackingField();
		}
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		Log("OnDisconnectedFromPhoton()");
		if (DisconnectedFromNetworkEvent__BackingField != null)
		{
			DisconnectedFromNetworkEvent__BackingField();
		}
	}

	public override void OnJoinedRoom()
	{
		Log("[TT2NetManager::OnJoinedRoom] - OnJoinedRoom() called");
		Log("[TT2NetManager::OnJoinedRoom] - " + PhotonNetwork.NickName + " joined the room \"" + PhotonNetwork.CurrentRoom.Name + "\" with total players: " + PhotonNetwork.CurrentRoom.PlayerCount);
		if (IsSearchingForMatch && isSearchCanceled)
		{
			Log("[TT2NetManager::OnJoinedRoom] - Searching was canceled while joining room");
			return;
		}
		GameObject gameObject = PhotonNetwork.Instantiate("Prefabs/TT2Net/TT2NetPlayerPrefab", Vector3.zero, Quaternion.identity, 0);
		gameObject.name = PhotonNetwork.NickName;
		Log("[TT2NetManager::OnJoinedRoom] - Added player " + gameObject.name + " to the scene");
		if (OnJoinedRoomEvent__BackingField != null)
		{
			OnJoinedRoomEvent__BackingField();
		}
		if (PhotonNetwork.CurrentRoom.PlayerCount != GameManager.instance.gameSettings.networkMaxPlayersPerRoom)
		{
			return;
		}
		if (PhotonNetwork.IsMasterClient)
		{
			Log("[TT2NetManager::OnJoinedRoom] - Match Found!");
			if (OnMatchFound__BackingField != null)
			{
				OnMatchFound__BackingField(otherNetworkPlayer);
			}
		}
		else if (IsSearchingForMatch)
		{
			IsSearchingForMatch = false;
			if (waitForOtherPlayerCoroutine != null)
			{
				StopCoroutine(waitForOtherPlayerCoroutine);
				waitForOtherPlayerCoroutine = null;
			}
		}
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.ContainsKey("StartTime"))
		{
			StartTime = (int)propertiesThatChanged["StartTime"];
			LastSeed = StartTime;
		}
		if (CustomRoomPropertiesUpdated__BackingField != null)
		{
			CustomRoomPropertiesUpdated__BackingField(PhotonNetwork.CurrentRoom.CustomProperties);
		}
	}

	public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player target, ExitGames.Client.Photon.Hashtable changedProps)
	{
		TT2NetPlayer obj = null;
		for (int i = 0; i < networkPlayers.Count; i++)
		{
			if (networkPlayers[i].photonView.Owner == target)
			{
				obj = networkPlayers[i];
				break;
			}
		}
		if (PlayerPropertiesUpdated__BackingField != null)
		{
			PlayerPropertiesUpdated__BackingField(obj);
		}
	}

	public void Connect()
	{
		if (PhotonNetwork.IsConnected)
		{
			Log("[TT2NetManager::Connect] - PhotonNetwork is already connected");
		}
		else
		{
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	public void Connect(string userId, string region = "")
	{
		if (PhotonNetwork.IsConnected)
		{
			Log("[TT2NetManager::Connect] - PhotonNetwork is already connected");
			return;
		}
		PhotonNetwork.AuthValues = new AuthenticationValues(userId);
		if (string.IsNullOrEmpty(region))
		{
			PhotonNetwork.ConnectUsingSettings();
		}
		else
		{
			PhotonNetwork.ConnectToRegion(region);
		}
	}

	public void Disconnect()
	{
		if (PhotonNetwork.IsConnected)
		{
			PhotonNetwork.Disconnect();
		}
		else
		{
			Log("[TT2NetManager::Connect] - PhotonNetwork is already disconnected");
		}
	}

	public void StartSearchingForMatch()
	{
		searchGroup = GameManager.instance.matchmakingSettings.GetSearchGroup(PhotonNetwork.CountOfRooms);
		if (searchGroup != null && searchGroup.searchSteps.Length > 0)
		{
			IsSearchingForMatch = true;
			isSearchCanceled = false;
			searchStepIndex = 0;
			SearchForMatch(searchStepIndex);
		}
		else
		{
			Log("[TT2NetManager::StartSearchingForMatch] - No Search Group was found, or the Search Group has no steps.");
		}
	}

	public void CancelSearchingForMatch()
	{
		if (IsSearchingForMatch && !isSearchCanceled)
		{
			StartCoroutine(CancelSearchSequence());
		}
	}

	private IEnumerator CancelSearchSequence()
	{
		isSearchCanceled = true;
		yield return new WaitWhile(() => PhotonNetwork.NetworkClientState == ClientState.Joining);
		if (waitToRetryCoroutine != null)
		{
			StopCoroutine(waitToRetryCoroutine);
			waitToRetryCoroutine = null;
		}
		if (waitForOtherPlayerCoroutine != null)
		{
			StopCoroutine(waitForOtherPlayerCoroutine);
			waitForOtherPlayerCoroutine = null;
		}
		PhotonNetwork.LeaveRoom();
		IsSearchingForMatch = false;
		if (SearchForMatchCanceled__BackingField != null)
		{
			SearchForMatchCanceled__BackingField();
		}
	}

	private void SearchForMatch(int stepIndex)
	{
		string text = string.Empty;
		TT2NetMatchmakingSettings.SearchStep searchStep = searchGroup.searchSteps[stepIndex];
		TT2NetMatchmakingSettings matchmakingSettings = GameManager.instance.matchmakingSettings;
		int highestFigLevelInParty = Player.instance.GetHighestFigLevelInParty();
		int skillRating = Player.instance.pvpData.skillRating;
		switch (searchStep.filterType)
		{
		case TT2NetMatchmakingSettings.FilterType.HighestLevelFig:
			text = string.Format("C0 >= {0} AND C0 <= {1}", highestFigLevelInParty - searchStep.intValue1, highestFigLevelInParty + searchStep.intValue1);
			Log("[TT2NetManager::SearchForMatch] - Search via Highest Level Fig: " + text);
			break;
		case TT2NetMatchmakingSettings.FilterType.SkillRating:
			text = string.Format("C1 >= {0} AND C1 <= {1} AND C0 >= {2} AND C0 <= {3}", skillRating - searchStep.intValue1, skillRating + searchStep.intValue1, highestFigLevelInParty - matchmakingSettings.maxLevelDisparity, highestFigLevelInParty + matchmakingSettings.maxLevelDisparity);
			Log("[TT2NetManager::SearchForMatch] - Search via Skill Rating: " + text);
			break;
		}
		TypedLobby typedLobby = new TypedLobby("myLobby", LobbyType.SqlLobby);
		PhotonNetwork.JoinRandomRoom(null, (byte)GameManager.instance.gameSettings.networkMaxPlayersPerRoom, MatchmakingMode.FillRoom, typedLobby, text);
	}

	private IEnumerator WaitToRetry()
	{
		searchStepIndex++;
		if (searchStepIndex == searchGroup.searchSteps.Length)
		{
			waitToRetryCoroutine = null;
			CreateRoomForMatchmaking();
		}
		else
		{
			yield return new WaitForSeconds(GameManager.instance.matchmakingSettings.searchStepDelay);
			waitToRetryCoroutine = null;
			SearchForMatch(searchStepIndex);
		}
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public void CreateRoomForMatchmaking()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = (byte)GameManager.instance.gameSettings.networkMaxPlayersPerRoom;
		roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
		roomOptions.CustomRoomProperties["BattleArenaUid"] = GameManager.instance.battleArenaDatabase.GetRandomArena().uid;
		roomOptions.CustomRoomProperties["C0"] = Player.instance.GetHighestFigLevelInParty();
		roomOptions.CustomRoomProperties["C1"] = Player.instance.pvpData.skillRating;
		roomOptions.CustomRoomPropertiesForLobby = new string[2] { "C0", "C1" };
		TypedLobby typedLobby = new TypedLobby("myLobby", LobbyType.SqlLobby);
		PhotonNetwork.CreateRoom(null, roomOptions, typedLobby);
		Log(string.Format("Created a Room for Matchmaking | BattleArenaUid: {0}    C0: {1}    C1: {2}", roomOptions.CustomRoomProperties["BattleArenaUid"], roomOptions.CustomRoomProperties["C0"], roomOptions.CustomRoomProperties["C1"]));
		waitForOtherPlayerCoroutine = WaitForOtherPlayer();
		StartCoroutine(waitForOtherPlayerCoroutine);
	}

	private IEnumerator WaitForOtherPlayer()
	{
		for (float timeoutTimer = GameManager.instance.matchmakingSettings.timeout; timeoutTimer > 0f; timeoutTimer -= Time.deltaTime)
		{
			yield return null;
		}
		PhotonNetwork.LeaveRoom();
		yield return new WaitUntil(new Func<bool>(() => PhotonNetwork.IsConnectedAndReady));
		waitForOtherPlayerCoroutine = null;
		searchStepIndex = -1;
		int highestLevel = Player.instance.GetHighestFigLevelInParty();
		PhotonNetwork.JoinRandomRoom(sqlLobbyFilter: string.Format("C0 >= {0} AND C0 <= {1}", highestLevel - GameManager.instance.matchmakingSettings.maxLevelDisparity, highestLevel + GameManager.instance.matchmakingSettings.maxLevelDisparity), typedLobby: new TypedLobby("myLobby", LobbyType.SqlLobby), expectedCustomRoomProperties: null, expectedMaxPlayers: (byte)GameManager.instance.gameSettings.networkMaxPlayersPerRoom, matchingType: MatchmakingMode.FillRoom);
	}

	public void CreateRoomForFriendBattle(string playerId, string roomName)
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.EmptyRoomTtl = 0;
		roomOptions.PlayerTtl = 0;
		roomOptions.IsVisible = false;
		roomOptions.MaxPlayers = (byte)GameManager.instance.gameSettings.networkMaxPlayersPerRoom;
		roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
		roomOptions.CustomRoomProperties["BattleArenaUid"] = GameManager.instance.battleArenaDatabase.GetRandomArena().uid;
		PhotonNetwork.CreateRoom(roomName, roomOptions, null, new string[1] { playerId });
	}

	public void JoinRoom(string roomName)
	{
		Log(string.Format("[TT2NetManager::JoinRoom] - Attempt to join room \"{0}\"", roomName));
		PhotonNetwork.JoinRoom(roomName);
	}

	public void AddNetworkPlayer(TT2NetPlayer networkPlayer)
	{
		if (!(networkPlayer != null) || networkPlayers.Contains(networkPlayer))
		{
			return;
		}
		networkPlayers.Add(networkPlayer);
		if (!PhotonNetwork.IsMasterClient || !PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount != GameManager.instance.gameSettings.networkMaxPlayersPerRoom)
		{
			return;
		}
		if (IsSearchingForMatch)
		{
			IsSearchingForMatch = false;
			if (waitForOtherPlayerCoroutine != null)
			{
				StopCoroutine(waitForOtherPlayerCoroutine);
				waitForOtherPlayerCoroutine = null;
			}
		}
		if (OnMatchFound__BackingField != null)
		{
			OnMatchFound__BackingField(otherNetworkPlayer);
		}
	}

	public void RemoveNetworkPlayer(TT2NetPlayer networkPlayer)
	{
		if (networkPlayers.Contains(networkPlayer))
		{
			networkPlayers.Remove(networkPlayer);
		}
	}

	public TT2NetPlayer GetNetworkPlayerWithID(int playerID)
	{
		return networkPlayers.Find((TT2NetPlayer x) => x.ID == playerID);
	}

	public void PrepareForBattle(int initiatorId, int arenaUid)
	{
		if (OnPrepareForBattleEvent__BackingField != null)
		{
			OnPrepareForBattleEvent__BackingField(GetNetworkPlayerWithID(initiatorId), arenaUid);
		}
	}

	public NetworkBattleRequest GetBattleRequest(int initiatorPlayerID, int otherPlayerID)
	{
		return networkBattleRequests.Find((NetworkBattleRequest x) => x.initiatorPlayerID == initiatorPlayerID && x.otherPlayerID == otherPlayerID);
	}

	public void AddPendingBattleRequest(int initiatorPlayerID, int otherPlayerID)
	{
		Log("Add Battle Request From Initiator ID:" + initiatorPlayerID + " Other Player ID:" + otherPlayerID);
		int count = networkBattleRequests.Count;
		bool flag = false;
		Log("Total Request Count : " + count);
		int num = -1;
		for (int i = 0; i < count; i++)
		{
			NetworkBattleRequest networkBattleRequest = networkBattleRequests[i];
			Log("Network Battle Request Initiator ID:" + initiatorPlayerID + " networkBattleRequest.initiatorPlayerID:" + networkBattleRequest.initiatorPlayerID + " Other Player ID:" + otherPlayerID + " networkBattleRequest.otherPlayerID: " + networkBattleRequest.otherPlayerID);
			if (networkBattleRequest.initiatorPlayerID == initiatorPlayerID && networkBattleRequest.otherPlayerID == otherPlayerID)
			{
				num = i;
				Log("Found Network Battle Request Initiator ID:" + initiatorPlayerID + " Other Player ID:" + otherPlayerID);
			}
		}
		if (num > -1)
		{
			networkBattleRequests.RemoveAt(num);
		}
		if (!flag)
		{
			NetworkBattleRequest networkBattleRequest2 = new NetworkBattleRequest();
			networkBattleRequest2.hasPendingBattleRequest = true;
			networkBattleRequest2.initiatorPlayerID = initiatorPlayerID;
			networkBattleRequest2.otherPlayerID = otherPlayerID;
			networkBattleRequests.Add(networkBattleRequest2);
			Log("Created Network Battle Request Initiator ID:" + initiatorPlayerID + " Other Player ID:" + otherPlayerID);
		}
	}

	public void CancelPendingBattleRequest(int initiatorPlayerID, int otherPlayerID)
	{
		Log("Cancel Pending Battle Request Initiator ID:" + initiatorPlayerID + " Other Player ID:" + otherPlayerID);
		int count = networkBattleRequests.Count;
		for (int i = 0; i < count; i++)
		{
			NetworkBattleRequest networkBattleRequest = networkBattleRequests[i];
			if (networkBattleRequest.hasPendingBattleRequest && networkBattleRequest.initiatorPlayerID == initiatorPlayerID && networkBattleRequest.otherPlayerID == otherPlayerID)
			{
				networkBattleRequest.hasPendingBattleRequest = false;
			}
		}
	}

	public void AcceptBattleRequest(int initiatorPlayerID, int otherPlayerID)
	{
		TT2NetPlayer networkPlayerWithID = GetNetworkPlayerWithID(otherPlayerID);
		if (networkPlayerWithID != null)
		{
			Log("Accepted Battle Request From Other Player ID:" + otherPlayerID);
			if (OnBattleAcceptedEvent__BackingField != null)
			{
				OnBattleAcceptedEvent__BackingField(networkPlayerWithID);
			}
			CancelPendingBattleRequest(initiatorPlayerID, otherPlayerID);
		}
		else
		{
			Log("Failure Accepting Battle Request, other player missing... Cancelling");
			CancelPendingBattleRequest(initiatorPlayerID, otherPlayerID);
		}
	}

	public static void SendPrepareForBattle(int arenaUid)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendPrepareForBattle(Instance.myNetworkPlayer.ID, arenaUid);
		}
	}

	public static TT2NetPlayerAction SendBattleDecision(int abilityUID, int characterUID, bool isCast, float meterProgress, float finalDamageMultiplier, int seed)
	{
		UnityEngine.Debug.Log("SendBattleDecision " + abilityUID);
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendBattleDecision(abilityUID, characterUID, isCast, meterProgress, finalDamageMultiplier, seed);
		}
		return null;
	}

	public static void SendBattleDecisionConfirm(int abilityUid, int characterUid, bool isCast, float meterProgress, float finalDamageMultiplier, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendBattleDecisionConfirm(abilityUid, characterUid, isCast, meterProgress, finalDamageMultiplier, seed, isConfirmed);
		}
	}

	public static void SendUpdateBattleReady(bool isReady)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendUpdateBattleReady(isReady);
		}
	}

	public static void SendUpdateBattleLoaded(bool isLoaded)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendUpdateBattleLoaded(isLoaded);
		}
	}

	public static TT2NetPlayerAction SendCharacterSwap(int characterUID)
	{
		UnityEngine.Debug.Log("SendCharacterSwap " + characterUID);
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendCharacterSwap(characterUID);
		}
		return null;
	}

	public static void SendCharacterSwapConfirm(int characterUid, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendCharacterSwapConfirm(characterUid, isConfirmed);
		}
	}

	public static void SendTofuSpawned()
	{
		UnityEngine.Debug.Log("SendTofuSpawned");
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendTofuSpawned();
		}
	}

	public static void SendTofuCollected()
	{
		UnityEngine.Debug.Log("SendTofuCollected");
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendTofuCollected();
		}
	}

	public static TT2NetPlayerAction SendTofuUsed(int tofuType, int seed)
	{
		UnityEngine.Debug.Log("SendTofuUsed" + tofuType);
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendTofuUsed(tofuType, seed);
		}
		return null;
	}

	public static void SendTofuUsedConfirm(int tofuType, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendTofuUsedConfirm(tofuType, seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendStatusEffectTick(int effectUid, int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendStatusEffectTick(effectUid, seed);
		}
		return null;
	}

	public static void SendStatusEffectTickConfirm(int effectUid, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendStatusEffectTickConfirm(effectUid, seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendStatusEffectExpire(int effectUid, int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendStatusEffectExpire(effectUid, seed);
		}
		return null;
	}

	public static void SendStatusEffectExpireConfirm(int effectUid, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendStatusEffectExpireConfirm(effectUid, seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendChannelingTick(int characterUid, int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendChannelingTick(characterUid, seed);
		}
		return null;
	}

	public static void SendChannelingTickConfirm(int characterUid, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendChannelingTickConfirm(characterUid, seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendChannelingNextStep(int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendChannelingNextStep(seed);
		}
		return null;
	}

	public static void SendChannelingNextStepConfirm(int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendChannelingNextStepConfirm(seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendChannelingExpire(int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendChannelingExpire(seed);
		}
		return null;
	}

	public static void SendChannelingExpireConfirm(int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendChannelingExpireConfirm(seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendAccessoryUsed(float curChargeHealth, int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendAccessoryUsed(curChargeHealth, seed);
		}
		return null;
	}

	public static void SendAccessoryUsedConfirm(float curChargeHealth, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendAccessoryUsedConfirm(curChargeHealth, seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendAccessoryTick(int effectIndex, int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendAccessoryTick(effectIndex, seed);
		}
		return null;
	}

	public static void SendAccessoryTickConfirm(int effectIndex, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendAccessoryTickConfirm(effectIndex, seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendAccessoryExpire(int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendAccessoryExpire(seed);
		}
		return null;
	}

	public static void SendAccessoryExpireConfirm(int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendAccessoryExpireConfirm(seed, isConfirmed);
		}
	}

	public static TT2NetPlayerAction SendModChipTriggerTick(int characterUid, int triggerType, int seed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			return Instance.myNetworkPlayer.SendModChipTriggerTick(characterUid, triggerType, seed);
		}
		return null;
	}

	public static void SendModChipTriggerTickConfirm(int characterUid, int triggerType, int seed, bool isConfirmed)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendModChipTriggerTickConfirm(characterUid, triggerType, seed, isConfirmed);
		}
	}

	public static void SendBattlePickupCollected(int pickupType)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendBattlePickupCollected(pickupType);
		}
	}

	public static void SendEmoteSent(EmoteType emoteType)
	{
		UnityEngine.Debug.Log("SendEmoteSent: " + emoteType);
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendEmoteSent(emoteType);
		}
	}

	public static void SendPlayingAction(bool isPlaying, RpcTarget target = RpcTarget.AllViaServer)
	{
		if (Instance != null && Instance.myNetworkPlayer != null)
		{
			Instance.myNetworkPlayer.SendPlayingAction(isPlaying, target);
		}
	}

	private void OnGUI()
	{
		if (false)
		{
			string message = string.Format("Realtime | Status: {0}    Region: {1}", PhotonNetwork.NetworkClientState, (!string.IsNullOrEmpty(PhotonNetwork.CloudRegion)) ? PhotonNetwork.CloudRegion : "N/A");
			DrawDebugMessage(new Rect(5f, 3f, Screen.width, 22f), message);
			message = string.Format("Chat | Status: {0}    Region: {1}", (!TT2NetChat.Instance.IsConnected) ? "Not Connected" : "Connected", (!string.IsNullOrEmpty(TT2NetChat.Instance.ChatRegion)) ? TT2NetChat.Instance.ChatRegion : "N/A");
			DrawDebugMessage(new Rect(5f, 21f, Screen.width, 22f), message);
			if (IsSearchingForMatch)
			{
				message = "Matchmaking: ";
				DrawDebugMessage(message: (waitToRetryCoroutine != null) ? (message + "Waiting to Retry") : ((waitForOtherPlayerCoroutine == null) ? (message + "Searching") : (message + "Waiting for other player to join us")), position: new Rect(5f, 39f, Screen.width, 22f));
			}
			if (TT2NetMatch.MatchExists)
			{
				message = string.Format("Match | State: {0}, Is Match Ready? {1}", TT2NetMatch.Instance.MatchState, TT2NetMatch.IsMatchReady());
				DrawDebugMessage(new Rect(5f, 39 + (IsSearchingForMatch ? 18 : 0), Screen.width, 22f), message);
			}
		}
	}

	private void DrawDebugMessage(Rect position, string message)
	{
		position.x++;
		position.y++;
		GUI.color = Color.black;
		GUI.Label(position, message);
		position.x--;
		position.y--;
		GUI.color = Color.white;
		GUI.Label(position, message);
	}

	public static void Log(string msg)
	{
		UnityEngine.Debug.Log(msg);
		string text = msg.Replace("\r", " ");
		text = text.Replace("\n", " ");
		Console.WriteLine("[APP] " + text);
		log.Add(text);
		msgCount++;
		if (msgCount > maxLines)
		{
			log.RemoveAt(0);
		}
	}
}

