using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class MatchmakingScreen : MonoBehaviour
{
	[Header("Searching")]
	public GameObject searchingObj;

	public TextMeshPro searchingTextMesh;

	public float searchTimeout = 10f;

	[Header("No Internet")]
	public GameObject noInternetObj;

	[Header("Battle CPU")]
	public GameObject battleCPUObj;

	public tk2dUIItem yesBtn;

	public tk2dUIItem noBtn;

	[Header("Match Found")]
	public GameObject matchFoundObj;

	public tk2dSprite portraitSprite;

	public tk2dSprite borderSprite;

	public TextMeshPro levelTextMesh;

	public TextMeshPro playerNameTextMesh;

	[Header("Waiting for Friend")]
	public GameObject waitingForFriendObj;

	public GameObject waitingForFriendWaitingGroup;

	public TextMeshPro waitingTextMesh;

	public GameObject waitingForFriendAcceptedGroup;

	public TextMeshPro waitingForFriendAcceptedTextMesh;

	public GameObject waitingForFriendDeclinedGroup;

	public TextMeshPro waitingForFriendDeclinedTextMesh;

	[Header("Accepting Battle Request")]
	public GameObject acceptingBattleRequestObj;

	public GameObject acceptingBattleRequestWaitingGroup;

	public TextMeshPro acceptingBattleRequestWaitingTextMesh;

	public GameObject acceptingBattleRequestDeclinedGroup;

	public TextMeshPro acceptingBattleRequestDeclinedTextMesh;

	private FriendData friendData;

	private BattleRequest battleRequest;

	private bool waitingForResponse;

	private bool didAcceptBattleRequest;

	public bool didCancel { get; private set; }

	public void InitSearching()
	{
		searchingObj.SetActive(value: true);
		noInternetObj.SetActive(value: false);
		battleCPUObj.SetActive(value: false);
		matchFoundObj.SetActive(value: false);
		waitingForFriendObj.SetActive(value: false);
		acceptingBattleRequestObj.SetActive(value: false);
		StartCoroutine("StartMatchmaking");
	}

	public void InitNoInternet()
	{
		searchingObj.SetActive(value: false);
		noInternetObj.SetActive(value: true);
		battleCPUObj.SetActive(value: false);
		matchFoundObj.SetActive(value: false);
		waitingForFriendObj.SetActive(value: false);
		acceptingBattleRequestObj.SetActive(value: false);
		LegionOfDoomController.instance.addFriendBtn.gameObject.SetActive(value: false);
		LegionOfDoomController.instance.lowerRightTVMatchmakingObj.SetActive(value: true);
		LegionOfDoomController.instance.SetAddFriendTVShown(show: true);
	}

	public void InitWaitingForFriend(FriendData friendData, bool isLocal = false)
	{
		this.friendData = friendData;
		searchingObj.SetActive(value: false);
		noInternetObj.SetActive(value: false);
		battleCPUObj.SetActive(value: false);
		matchFoundObj.SetActive(value: false);
		waitingForFriendObj.SetActive(value: true);
		acceptingBattleRequestObj.SetActive(value: false);
		StartCoroutine(StartWaitingForFriend(isLocal));
	}

	public void InitAcceptingBattleRequest(BattleRequest battleRequest)
	{
		this.battleRequest = battleRequest;
		searchingObj.SetActive(value: false);
		noInternetObj.SetActive(value: false);
		battleCPUObj.SetActive(value: false);
		matchFoundObj.SetActive(value: false);
		waitingForFriendObj.SetActive(value: false);
		acceptingBattleRequestObj.SetActive(value: true);
		if (battleRequest.isLocal)
		{
			FriendData friendData = LegionOfDoomController.instance.networkDiscoveryClient.GetFriendData(battleRequest.playerId);
			if (friendData == null)
			{
				friendData = TT2NetBluetooth.Instance.GetFriendData(battleRequest.playerId);
			}
			acceptingBattleRequestWaitingTextMesh.text = string.Format(Language.Get("WAITINGFOROPPONENT2", "MENUS,GUI") + "...", (friendData == null) ? "N/A" : friendData.name);
		}
		else
		{
			acceptingBattleRequestWaitingTextMesh.text = string.Format(Language.Get("WAITINGFOROPPONENT2", "MENUS,GUI") + "...", Player.instance.pvpData.GetFriend(battleRequest.playerId).name);
		}
		StartCoroutine("StartAcceptingBattleRequest");
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		friendData = null;
		didCancel = false;
		waitingForResponse = false;
	}

	private void OnDestroy()
	{
		if (TT2NetChat.Instance != null)
		{
			TT2NetChat.Instance.PlayerAcceptedBattleRequest -= Instance_PlayerAcceptedBattleRequest;
			TT2NetChat.Instance.PlayerDeclinedBattleRequest -= Instance_PlayerDeclinedBattleRequest;
		}
	}

	private IEnumerator StartMatchmaking()
	{
		float timer = searchTimeout;
		float periodTimer = 0.5f;
		searchingTextMesh.text = Language.Get("SEARCHING", "MENUS,GUI");
		yield return StartCoroutine(WaitForConnection(string.Empty));
		TT2NetManager.Instance.StartSearchingForMatch();
		while (true)
		{
			if (timer > 0f)
			{
				timer -= Time.deltaTime;
				if (timer <= 0f)
				{
					LegionOfDoomController.instance.addFriendBtn.gameObject.SetActive(value: false);
					LegionOfDoomController.instance.lowerRightTVMatchmakingObj.SetActive(value: true);
					LegionOfDoomController.instance.SetAddFriendTVShown(show: true);
				}
			}
			periodTimer -= Time.deltaTime;
			if (periodTimer <= 0f)
			{
				periodTimer = 0.5f;
				searchingTextMesh.text += ".";
				if (searchingTextMesh.text.Contains("...."))
				{
					searchingTextMesh.text = ((!didCancel) ? Language.Get("SEARCHING", "MENUS,GUI") : Language.Get("CANCELLING", "MENUS,GUI"));
				}
			}
			yield return null;
		}
	}

	private IEnumerator StartWaitingForFriend(bool isLocal)
	{
		float periodTimer = 0.5f;
		waitingForFriendWaitingGroup.SetActive(value: true);
		waitingForFriendAcceptedGroup.SetActive(value: false);
		waitingForFriendDeclinedGroup.SetActive(value: false);
		waitingTextMesh.text = string.Format(Language.Get("WAITINGFOROPPONENT2", "MENUS,GUI"), friendData.name);
		yield return StartCoroutine(WaitForConnection(string.Empty));
		if (Player.instance.pvpData.HasBattleRequest(friendData.playerID))
		{
			PvpData_BattleRequestAdded(Player.instance.pvpData.GetBattleRequest(friendData.playerID));
			yield break;
		}
		string roomName = Player.instance.pvpData.playerID + "_" + friendData.playerID;
		TT2NetManager.Instance.CreateRoomForFriendBattle(friendData.playerID, roomName);
		yield return new WaitUntil(() => TT2NetManager.Instance.InRoom);
		if (Player.instance.pvpData.HasBattleRequest(friendData.playerID))
		{
			PvpData_BattleRequestAdded(Player.instance.pvpData.GetBattleRequest(friendData.playerID));
			yield break;
		}
		Player.instance.pvpData.BattleRequestAdded += PvpData_BattleRequestAdded;
		TT2NetChat.Instance.PlayerAcceptedBattleRequest += Instance_PlayerAcceptedBattleRequest;
		TT2NetChat.Instance.PlayerDeclinedBattleRequest += Instance_PlayerDeclinedBattleRequest;
		TT2NetChat.Instance.SendBattleRequest(friendData.playerID, roomName, isLocal);
		waitingForResponse = true;
		while (waitingForResponse && (isLocal || (friendData.IsOnline && !friendData.IsInBattle)))
		{
			periodTimer -= Time.deltaTime;
			if (periodTimer <= 0f)
			{
				periodTimer = 0.5f;
				waitingTextMesh.text += ".";
				if (waitingTextMesh.text.Contains("...."))
				{
					waitingTextMesh.text = ((!didCancel) ? string.Format(Language.Get("WAITINGFOROPPONENT2", "MENUS,GUI"), friendData.name) : Language.Get("CANCELLING", "MENUS,GUI"));
				}
			}
			yield return null;
		}
		waitingForFriendWaitingGroup.SetActive(value: false);
		if (!isLocal && !friendData.IsOnline)
		{
			waitingForFriendDeclinedGroup.SetActive(value: true);
			waitingForFriendDeclinedTextMesh.text = Language.Get("INVITECANCELLED", "MENUS,GUI");
		}
		else if (!isLocal && friendData.IsInBattle)
		{
			waitingForFriendDeclinedGroup.SetActive(value: true);
			waitingForFriendDeclinedTextMesh.text = Language.Get("INVITECANCELLED", "MENUS,GUI");
		}
		else if (didAcceptBattleRequest)
		{
			waitingForFriendAcceptedGroup.SetActive(value: true);
			waitingForFriendAcceptedTextMesh.text = string.Format(Language.Get("HASACCEPTEDBATTLE", "MENUS,GUI"), friendData.name);
		}
		else
		{
			waitingForFriendDeclinedGroup.SetActive(value: true);
			waitingForFriendDeclinedTextMesh.text = string.Format(Language.Get("DECLINEDINVITE", "MENUS,GUI"), friendData.name);
		}
	}

	private void PvpData_BattleRequestAdded(BattleRequest request)
	{
		if (request.playerId == friendData.playerID && (!waitingForResponse || request.playerId.GetHashCode() < Player.instance.pvpData.playerID.GetHashCode()))
		{
			Player.instance.pvpData.BattleRequestAdded -= PvpData_BattleRequestAdded;
			TT2NetChat.Instance.PlayerAcceptedBattleRequest -= Instance_PlayerAcceptedBattleRequest;
			TT2NetChat.Instance.PlayerDeclinedBattleRequest -= Instance_PlayerDeclinedBattleRequest;
			StartCoroutine(InterruptAcceptBattleRequest());
		}
	}

	private void Instance_PlayerAcceptedBattleRequest(string playerId)
	{
		Player.instance.pvpData.BattleRequestAdded -= PvpData_BattleRequestAdded;
		TT2NetChat.Instance.PlayerAcceptedBattleRequest -= Instance_PlayerAcceptedBattleRequest;
		TT2NetChat.Instance.PlayerDeclinedBattleRequest -= Instance_PlayerDeclinedBattleRequest;
		didAcceptBattleRequest = true;
		waitingForResponse = false;
	}

	private void Instance_PlayerDeclinedBattleRequest(string playerId)
	{
		Player.instance.pvpData.BattleRequestAdded -= PvpData_BattleRequestAdded;
		TT2NetChat.Instance.PlayerAcceptedBattleRequest -= Instance_PlayerAcceptedBattleRequest;
		TT2NetChat.Instance.PlayerDeclinedBattleRequest -= Instance_PlayerDeclinedBattleRequest;
		didAcceptBattleRequest = false;
		waitingForResponse = false;
	}

	private IEnumerator StartAcceptingBattleRequest()
	{
		acceptingBattleRequestWaitingGroup.SetActive(value: true);
		acceptingBattleRequestDeclinedGroup.SetActive(value: false);
		yield return StartCoroutine(WaitForConnection(battleRequest.region));
		TT2NetManager.Instance.JoinRoomFailed += Instance_JoinRoomFailed;
		TT2NetManager.Instance.JoinRoom(battleRequest.roomName);
		bool didTimeout = false;
		float timeoutTimer = 5f;
		while (true)
		{
			yield return null;
			if (TT2NetManager.Instance.InRoom)
			{
				break;
			}
			timeoutTimer -= Time.deltaTime;
			if (timeoutTimer <= 0f)
			{
				didTimeout = true;
				break;
			}
		}
		TT2NetManager.Instance.JoinRoomFailed -= Instance_JoinRoomFailed;
		if (didTimeout)
		{
			TT2NetManager.Instance.Disconnect();
			Instance_JoinRoomFailed();
		}
	}

	private void Instance_JoinRoomFailed()
	{
		TT2NetManager.Instance.JoinRoomFailed -= Instance_JoinRoomFailed;
		StopCoroutine("StartAcceptingBattleRequest");
		acceptingBattleRequestWaitingGroup.SetActive(value: false);
		acceptingBattleRequestDeclinedGroup.SetActive(value: true);
		acceptingBattleRequestDeclinedTextMesh.text = Language.Get("INVITECANCELLED", "MENUS,GUI");
		LegionOfDoomController.instance.SetExitTVShown(show: true);
	}

	private IEnumerator InterruptAcceptBattleRequest()
	{
		TT2NetChat.Instance.CancelBattleRequest(friendData.playerID);
		if (waitingForResponse || TT2NetManager.Instance.InRoom)
		{
			yield return new WaitUntil(() => TT2NetManager.Instance.InRoom);
			TT2NetManager.Instance.LeaveRoom();
		}
		yield return new WaitUntil(() => TT2NetManager.Instance.IsConnectedToMaster && TT2NetManager.Instance.IsConnectedAndReady);
		Player.instance.pvpData.AcceptBattleRequest(friendData.playerID);
	}

	public void CancelSearch()
	{
		if (!didCancel)
		{
			didCancel = true;
			searchingTextMesh.text = Language.Get("CANCELLING", "MENUS,GUI");
			StartCoroutine(WaitToCancelSearch());
		}
	}

	private IEnumerator WaitToCancelSearch()
	{
		yield return new WaitUntil(() => TT2NetManager.Instance.IsSearchingForMatch);
		StopCoroutine("StartMatchmaking");
		TT2NetManager.Instance.CancelSearchingForMatch();
	}

	public void CancelWaiting()
	{
		if (!didCancel)
		{
			didCancel = true;
			Player.instance.pvpData.BattleRequestAdded -= PvpData_BattleRequestAdded;
			TT2NetChat.Instance.PlayerAcceptedBattleRequest -= Instance_PlayerAcceptedBattleRequest;
			TT2NetChat.Instance.PlayerDeclinedBattleRequest -= Instance_PlayerDeclinedBattleRequest;
			TT2NetChat.Instance.CancelBattleRequest(friendData.playerID);
			waitingTextMesh.text = Language.Get("CANCELLING", "MENUS,GUI");
			StartCoroutine(WaitToCancelWaiting());
		}
	}

	private IEnumerator WaitToCancelWaiting()
	{
		yield return new WaitUntil(() => TT2NetManager.Instance.InRoom);
		TT2NetManager.Instance.LeaveRoom();
		yield return new WaitUntil(() => TT2NetManager.Instance.IsConnectedToMaster && TT2NetManager.Instance.IsConnectedAndReady);
	}

	private IEnumerator WaitForConnection(string region = "")
	{
		if (!TT2NetManager.Instance.IsConnected)
		{
			Debug.Log("We are currently not connected, so we will connect.");
			PhotonNetwork.NickName = Player.instance.pvpData.PlayerName;
			TT2NetManager.Instance.Connect(Player.instance.pvpData.playerID, string.Empty);
			yield return new WaitUntil(() => TT2NetManager.Instance.IsConnectedToMaster && TT2NetManager.Instance.IsConnectedAndReady);
		}
		if (!string.IsNullOrEmpty(region) && TT2NetManager.Instance.CurrentRegion != region)
		{
			Debug.Log("We are currently in a region different from that specified in the parameter, so we need to switch regions.");
			TT2NetManager.Instance.Disconnect();
			yield return new WaitUntil(() => !TT2NetManager.Instance.IsConnected);
			TT2NetManager.Instance.Connect(Player.instance.pvpData.playerID, battleRequest.region);
			yield return new WaitUntil(() => TT2NetManager.Instance.IsConnectedToMaster && TT2NetManager.Instance.IsConnectedAndReady);
		}
		else
		{
			Debug.Log("We are already in the same region as the Battle Request.");
		}
		Debug.Log("We are connected and ready.");
	}
}
