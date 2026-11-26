using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerHubController : MonoBehaviour
{
	private const string gameVersion = "1";

	public GameObject connectPanel;

	public GameObject topMenuPanel;

	public GameObject mainMenuPanel;

	public GameObject searchingPanel;

	public GameObject versusPanel;

	public Text searchProgressLabel;

	public Text player1NameLabel;

	public Text player2NameLabel;

	public Text messageLabel;

	private void Awake()
	{
		connectPanel.SetActive(false);
		topMenuPanel.SetActive(false);
		mainMenuPanel.SetActive(false);
		searchingPanel.SetActive(false);
		versusPanel.SetActive(false);
		messageLabel.gameObject.SetActive(false);
	}

	private void Start()
	{
		if (TT2NetManager.Instance.IsConnected)
		{
			mainMenuPanel.SetActive(true);
		}
		else
		{
			connectPanel.SetActive(true);
		}
		topMenuPanel.SetActive(true);
	}

	private void OnEnable()
	{
		TT2NetManager.Instance.ConnectedToNetworkEvent += HandleConnectedToNetworkEvent;
		TT2NetManager.Instance.OnMatchFound += HandleOnMatchFound;
		TT2NetManager.Instance.OnPrepareForBattleEvent += HandleOnPrepareForBattleEvent;
	}

	private void OnDisable()
	{
		TT2NetManager.Instance.ConnectedToNetworkEvent -= HandleConnectedToNetworkEvent;
		TT2NetManager.Instance.OnMatchFound -= HandleOnMatchFound;
		TT2NetManager.Instance.OnPrepareForBattleEvent -= HandleOnPrepareForBattleEvent;
	}

	public void Connect()
	{
		connectPanel.SetActive(false);
		if (!TT2NetManager.Instance.IsConnected)
		{
			messageLabel.gameObject.SetActive(true);
			messageLabel.text = "Connecting...";
			TT2NetManager.Instance.Connect();
		}
		if (!TT2NetChat.Instance.IsConnected)
		{
			TT2NetChat.Instance.Connect(PhotonNetwork.NickName);
		}
	}

	public void OnBattleOnlineClick()
	{
		mainMenuPanel.SetActive(false);
		searchProgressLabel.text = "Searching...";
		searchingPanel.SetActive(true);
		TT2NetManager.Instance.StartSearchingForMatch();
	}

	public void OnFriendsListClick()
	{
	}

	public void OnBattleLocalClick()
	{
	}

	public void OnExitClick()
	{
		Player.instance.Save();
		LoadingScreenCover.LoadScene(CityController.previousCityScene);
	}

	public void OnTowerClick()
	{
	}

	public void OnCancelClick()
	{
		searchingPanel.SetActive(false);
		if (TT2NetManager.Instance.InRoom)
		{
			TT2NetManager.Instance.LeaveRoom();
			return;
		}
		messageLabel.gameObject.SetActive(true);
		messageLabel.text = "Cancelling...";
		StartCoroutine(WaitToCancelSearch());
	}

	private IEnumerator WaitToCancelSearch()
	{
		yield return new WaitUntil(() => TT2NetManager.Instance.InRoom);
		messageLabel.gameObject.SetActive(false);
		TT2NetManager.Instance.LeaveRoom();
	}

	private void HandleConnectedToNetworkEvent()
	{
		messageLabel.gameObject.SetActive(false);
		mainMenuPanel.SetActive(true);
	}

	private void HandleOnMatchFound(TT2NetPlayer otherPlayer)
	{
		Debug.Log("[MultiplayerHubController::HandleOnMatchFound] - We found a match, so we should now move to the Pre Battle screen");
		searchingPanel.SetActive(false);
		int uid = GameManager.instance.battleArenaDatabase.GetRandomArena().uid;
		TT2NetManager.SendPrepareForBattle(uid);
		TT2NetChat.Instance.SendPrivateMessage(otherPlayer.PlayerName, new FriendData
		{
			portraitSpriteName = "master_client_sprite_name"
		});
		BeginBattle(otherPlayer, uid);
	}

	private void HandleOnPrepareForBattleEvent(TT2NetPlayer otherPlayer, int arenaUid)
	{
		Debug.Log("[MultiplayerHubController::HandleOnPrepareForBattleEvent] - The master client says we should move onto the Pre Battle screen");
		searchingPanel.SetActive(false);
		TT2NetChat.Instance.SendPrivateMessage(otherPlayer.PlayerName, new FriendData
		{
			portraitSpriteName = "slave_client_sprite_name"
		});
		BeginBattle(otherPlayer, arenaUid);
	}

	private void BeginBattle(TT2NetPlayer otherPlayer, int arenaUid = -1)
	{
		BattleDefinition battleDefinition = new BattleDefinition();
		battleDefinition.type = BattleType.Normal;
		battleDefinition.playerAIType = PlayerAIType.Network;
		battleDefinition.enemyCharacterName = CharacterName.None;
		battleDefinition.enemyName = otherPlayer.PlayerName;
		battleDefinition.districtLocation = CityLocation.None;
		battleDefinition.arenaUID = arenaUid;
		PreBattleController.battleDefinition = battleDefinition;
		TT2NetMatch.SendStartMatch();
		StartCoroutine("BeginPreBattle");
	}

	private IEnumerator BeginPreBattle()
	{
		Debug.Log("BeginPreBattle");
		yield return new WaitForSeconds(0.3f);
		yield return new WaitForEndOfFrame();
		LoadingScreenCover.LoadScene("PreBattle", false);
	}
}
