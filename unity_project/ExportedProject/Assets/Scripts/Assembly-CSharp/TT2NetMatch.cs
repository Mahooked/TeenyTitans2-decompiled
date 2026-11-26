using Photon.Pun;
using UnityEngine;

public class TT2NetMatch : MonoBehaviour
{
	private static TT2NetMatch _instance;

	private static bool didApplicationQuit;

	public string MatchState = string.Empty;

	public bool isMatchReady;

	private bool startedMatch;

	private bool endedMatch;

	private double startTime;

	public TT2NetPlayer player1;

	public TT2NetPlayer player2;

	private float lastBattleMatchDataUpdate;

	private float lastPreBattleCheck;

	private PhotonView _photonView;

	public static TT2NetMatch Instance
	{
		get
		{
			if (_instance == null && !didApplicationQuit)
			{
				_instance = new GameObject("TT2NetMatch").AddComponent<TT2NetMatch>();
			}
			return _instance;
		}
	}

	public static bool MatchExists
	{
		get
		{
			return _instance != null;
		}
	}

	private PhotonView photonView
	{
		get
		{
			if (_photonView == null)
			{
				_photonView = GetComponent<PhotonView>();
				if (_photonView == null)
				{
					_photonView = base.gameObject.AddComponent<PhotonView>();
				}
				_photonView.ViewID = 333;
			}
			return _photonView;
		}
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		Object.DontDestroyOnLoad(base.gameObject);
		Debug.Log("Awake photonView id" + photonView.ViewID);
	}

	private void OnEnable()
	{
		if (TT2NetManager.Instance != null)
		{
			TT2NetManager.Instance.DisconnectedFromNetworkEvent += Instance_DisconnectedFromNetworkEvent;
		}
	}

	private void OnDisable()
	{
		if (TT2NetManager.Instance != null)
		{
			TT2NetManager.Instance.DisconnectedFromNetworkEvent -= Instance_DisconnectedFromNetworkEvent;
		}
	}

	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
		player1 = null;
		player2 = null;
	}

	private void OnApplicationQuit()
	{
		didApplicationQuit = true;
	}

	public void ResetMatch()
	{
		Debug.Log("[TT2NetMatch::ResetMatch] - Setting MatchState to \"\"");
		MatchState = string.Empty;
		isMatchReady = false;
		startedMatch = false;
		endedMatch = false;
		player1 = null;
		player2 = null;
		startTime = 0.0;
		lastBattleMatchDataUpdate = 0f;
		lastPreBattleCheck = 0f;
	}

	private void Instance_DisconnectedFromNetworkEvent()
	{
		if (MatchState.Equals("Ready"))
		{
			PlayerAI_Network component = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI_Network>();
			if (component != null)
			{
				Object.Destroy(component);
			}
			if (BattleController.instance != null)
			{
				BattleController.instance.enemyActionMeter.gameObject.AddComponent<PlayerAI>();
			}
			BattleController.battleDefinition.playerAIType = PlayerAIType.Default;
			if (TT2NetManager.Instance != null && TT2NetManager.Instance.myNetworkPlayer != null)
			{
				TT2NetManager.Instance.myNetworkPlayer.ResolveQueuedActions();
			}
			Debug.Log("[TT2NetMatch::Instance_DisconnectedFromNetworkEvent] - Setting MatchState to \"\"");
			MatchState = string.Empty;
		}
	}

	public static void SendEndMatch()
	{
		if (!(Instance == null))
		{
			Instance.EndMatch();
		}
	}

	public static void SendStartMatch()
	{
		if (!(Instance == null))
		{
			Instance.StartMatch();
		}
	}

	public static void SendUpdateBattleMatchData()
	{
		if (!(Instance == null))
		{
			Instance.UpdateBattleMatchData();
		}
	}

	public static bool IsPlayer2Available()
	{
		if (_instance == null)
		{
			return false;
		}
		if (Instance.player2 != null)
		{
			return true;
		}
		return false;
	}

	public static bool IsMatchReady()
	{
		if (_instance == null)
		{
			return false;
		}
		return _instance.isMatchReady;
	}

	private void Update()
	{
		if (!startedMatch || endedMatch)
		{
			return;
		}
		if (!isMatchReady)
		{
			if (MatchState != "Waiting")
			{
				Debug.Log("[TT2NetMatch::Update] - Setting MatchState to \"Waiting\"");
				MatchState = "Waiting";
			}
			if (player1 != null && player2 != null && player1.battleReady && player2.battleReady)
			{
				Debug.Log("[TT2NetMatch::Update] - Setting MatchState to \"Ready\"");
				MatchState = "Ready";
				isMatchReady = true;
				startTime = PhotonNetwork.Time;
			}
			bool flag = false;
			if (Time.time - lastPreBattleCheck > 3f)
			{
				lastPreBattleCheck = Time.time;
				if (PhotonNetwork.PlayerList.Length < GameManager.instance.gameSettings.networkMaxPlayersPerRoom)
				{
					flag = true;
				}
				if (player2 == null)
				{
					flag = true;
				}
				if (PreBattleController.Instance != null && PreBattleController.Instance.exiting && PhotonNetwork.InRoom)
				{
					flag = false;
				}
				if (flag && !LoadingScreenCover.isLoading)
				{
					Debug.Log("[TT2NetMatch::Update] - Setting MatchState to \"\"");
					MatchState = string.Empty;
					startedMatch = false;
					isMatchReady = false;
					TT2NetManager.Instance.LeaveRoom();
					EndMatch();
					LoadingScreenCover.LoadScene("LegionOfDoomMode");
				}
			}
		}
		if (MatchState.Equals("Ready"))
		{
			if (BattleController.instance == null)
			{
				return;
			}
			if (BattleController.instance != null && BattleController.battleDefinition != null)
			{
				if (PhotonNetwork.PlayerList.Length == 1)
				{
					PlayerAI_Network component = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI_Network>();
					if (component != null)
					{
						Object.Destroy(component);
					}
					BattleController.instance.enemyActionMeter.gameObject.AddComponent<PlayerAI>();
					BattleController.battleDefinition.playerAIType = PlayerAIType.Default;
					if (PhotonNetwork.InRoom)
					{
						PhotonNetwork.LeaveRoom();
					}
					TT2NetManager.Instance.myNetworkPlayer.ResolveQueuedActions();
					Debug.Log("[TT2NetMatch::Update] - Setting MatchState to \"\"");
					MatchState = string.Empty;
				}
				if (Time.time - lastBattleMatchDataUpdate > GameManager.instance.gameSettings.networkUpdateRoomHeartbeatSeconds)
				{
					UpdateBattleMatchData();
				}
			}
			if (Time.time - lastPreBattleCheck > 3f)
			{
				lastPreBattleCheck = Time.time;
				if (PreBattleController.Instance != null && !PreBattleController.Instance.exiting && PhotonNetwork.InRoom && PhotonNetwork.PlayerList.Length < GameManager.instance.gameSettings.networkMaxPlayersPerRoom && !LoadingScreenCover.isLoading)
				{
					Debug.Log("[TT2NetMatch::Update] - Setting MatchState to \"\"");
					MatchState = string.Empty;
					startedMatch = false;
					TT2NetManager.Instance.LeaveRoom();
					EndMatch();
					PreBattleController.Instance.exiting = true;
					LoadingScreenCover.LoadScene("LegionOfDoomMode");
				}
			}
		}
		if (!MatchState.Equals("CPU"))
		{
		}
	}

	public void StartMatch()
	{
		Debug.Log("Attempt StartMatch");
		TT2NetPlayer myNetworkPlayer = TT2NetManager.Instance.myNetworkPlayer;
		TT2NetPlayer otherNetworkPlayer = TT2NetManager.Instance.otherNetworkPlayer;
		if (myNetworkPlayer == null)
		{
			Debug.Log("player1 == null");
		}
		else if (otherNetworkPlayer == null)
		{
			Debug.Log("player2 == null");
		}
		else if (!(myNetworkPlayer == null) && !(otherNetworkPlayer == null))
		{
			startedMatch = true;
			endedMatch = false;
			player1 = myNetworkPlayer;
			player2 = otherNetworkPlayer;
		}
	}

	public void EndMatch()
	{
		Debug.Log("End the match");
		endedMatch = true;
		Debug.Log("[TT2NetMatch::EndMatch] - Setting MatchState to \"\"");
		MatchState = string.Empty;
		isMatchReady = false;
		if (player1 != null)
		{
			player1.battleReady = false;
		}
		if (player2 != null)
		{
			player2.battleReady = false;
		}
		TT2NetChat.Instance.UpdateUserStatus(2);
	}

	public void SendMatchData(TT2NetMatchData netMatchData, RpcTarget tagets)
	{
		if (netMatchData != null)
		{
			photonView.RPC("UpdateMatchWithData", tagets, netMatchData.Serialize());
		}
	}

	[PunRPC]
	public void BeginMatchWithData(byte[] matchData)
	{
		TT2NetMatchData matchData2 = TT2NetMatchData.Desserialize(matchData);
		ProcessMatchData(matchData2);
	}

	[PunRPC]
	public void UpdateMatchWithData(byte[] matchData)
	{
		if (!(this == null) && matchData != null)
		{
			TT2NetMatchData matchData2 = TT2NetMatchData.Desserialize(matchData);
			ProcessMatchData(matchData2);
		}
	}

	private void ProcessMatchData(TT2NetMatchData matchData)
	{
		if (matchData != null && matchData.state.Equals("UpdateBattleMatchData") && BattleController.instance != null && BattleController.instance.enemyCharacter != null && BattleController.instance.enemyParty != null)
		{
			BattleController.instance.enemyActionMeter.progress = (float)matchData.properties.senderActionMeterProgress;
		}
	}

	public void UpdateBattleMatchData()
	{
		if (MatchState.Equals("Ready"))
		{
			lastBattleMatchDataUpdate = Time.time;
			TT2NetMatchData tT2NetMatchData = new TT2NetMatchData();
			tT2NetMatchData.state = "UpdateBattleMatchData";
			tT2NetMatchData.properties.senderPlayerID = player1.ID;
			tT2NetMatchData.properties.targetPlayerID = player2.ID;
			if (BattleController.instance != null && BattleController.instance.playerActionMeter != null)
			{
				tT2NetMatchData.properties.senderActionMeterProgress = BattleController.instance.playerActionMeter.progress;
			}
			SendMatchData(tT2NetMatchData, RpcTarget.Others);
		}
	}
}
