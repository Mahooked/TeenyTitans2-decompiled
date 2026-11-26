using System;
using System.Collections;
using System.Collections.Generic;
using Crosstales.OnlineCheck;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class LegionOfDoomController : MonoBehaviour
{
	public enum PageName
	{
		None,
		Connect,
		Base,
		Matchmaking,
		MatchmakingNoInternet,
		FriendsList,
		FriendsListNoInternet,
		BattleLocal,
		RankUp,
		PortraitSelector,
		NameSelector,
		AddFriend,
		Invites,
		WaitingForFriend,
		AcceptingBattleRequest
	}

	public static LegionOfDoomController instance;

	public static BattleRequest initBattleRequest;

	public bool useAutoSave = true;

	public CurrencyGUI currencyGUI;

	[Header("Exit Button")]
	public tk2dUIItem exitBtn;

	public tk2dSprite exitBtnSprite;

	public Animator exitBtnTVAnimator;

	[Header("Titan Tower Button")]
	public tk2dUIItem titanTowerBtn;

	public Animator titanTowerTVAnimator;

	[Header("Connect Screen")]
	public GameObject connectScreen;

	[Header("Base Menu")]
	public GameObject baseMenu;

	public tk2dUIItem battleOnlineBtn;

	public tk2dUIItem battleNearbyBtn;

	public tk2dUIItem friendsListBtn;

	public tk2dUIItem battleCPUBtn;

	public TextMeshPro skillRatingTextMesh;

	public Animator wifiTVAnimator;

	public Animator lowerRightTVAnimator;

	public Animator upperCenterTVAnimator;

	[Header("Rank Up Menu")]
	public LegionOfDoomRewardMenu rewardMenu;

	[Header("Rank Up Bar")]
	public RankUpBar rankUpBar;

	[Header("Matchmaking Menu")]
	public GameObject matchmakingMenu;

	public MatchmakingScreen matchmakingScreen;

	public GameObject lowerRightTVMatchmakingObj;

	public tk2dUIItem battleCPUConfirmButton;

	[Header("Friends List")]
	public FriendsListMenu friendsListMenu;

	public tk2dUIItem addFriendBtn;

	public Animator addFriendTVAnimator;

	public GameObject friendCodeObj;

	public TextMeshPro friendCodeText;

	[Header("Add Friend Page")]
	public AddFriendMenu addFriendMenu;

	[Header("Battle Local")]
	public BattleLocalScreen battleLocalMenu;

	public GameObject localPlayersFoundObj;

	public TextMeshPro localPlayersFoundTextMesh;

	public TT2NetNetworkDiscovery networkDiscoveryServer;

	public TT2NetNetworkDiscovery networkDiscoveryClient;

	[Header("Portrait Selector")]
	public LegionOfDoomPortraitSelector portraitSelector;

	public GameObject lowerRightTVPurchaseObj;

	public tk2dUIItem confirmPurchaseYesBtn;

	public tk2dUIItem confirmPurchaseNoBtn;

	[Header("Name Selector")]
	public ChooseNameMenu nameSelector;

	public tk2dUIItem saveNameBtn;

	[Header("Invite Menu")]
	public tk2dUIItem inviteMenuBtn;

	public TextMeshPro inviteNumTextMesh;

	public Animator inviteAntennaAnimator;

	public AnimatorEventListener lowerRightAnimatorEventListener;

	public InviteMenu inviteMenu;

	[Header("Dialogue")]
	public Transform dialogueSystemParent;

	public Conversation introConvoPart1;

	public Conversation introConvoPart2;

	public Conversation requirementsNotMetConvo;

	[Header("Music")]
	public AudioSource themeMusic;

	[Header("Sound Effects")]
	public AudioClip allTVsIntroSFX;

	public AudioClip friendScreenTVSFX;

	public AudioClip genericTVSFX;

	[Header("Debug")]
	public int debugXPGain;

	public bool debugResetLevelOnStart;

	public tk2dUIItem debugLevelUpBtn;

	public bool debugResetUnlockedPortraits;

	public tk2dUIItem debugClearFriendsList;

	public bool debugResetFriendsList;

	private PageName curPage;

	private List<PageName> prevPages;

	private LegionOfDoomPortrait portraitSelectedForPurchase;

	private bool startedExitingScene;

	private bool firstTimeInLODMode;

	private bool initialInternetCheckComplete;

	private readonly Vector3 dialogueTreeRankUpLocalPos = new Vector3(0f, 6f, -400f);

	private bool debugDialogueToggle;

	private DialogueTree _dialogueTree;

	private DialogueTree dialogueTree
	{
		get
		{
			if (_dialogueTree == null)
			{
				_dialogueTree = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
				_dialogueTree.transform.parent = dialogueSystemParent;
				_dialogueTree.transform.localPosition = dialogueTreeRankUpLocalPos;
				_dialogueTree.leftButton.gameObject.SetActive(value: false);
				_dialogueTree.rightButton.gameObject.SetActive(value: false);
			}
			return _dialogueTree;
		}
	}

	public event Action<PageName> PageUpdated;

	private void Awake()
	{
		instance = this;
		themeMusic.Stop();
	}

	private void OnEnable()
	{
		TT2NetManager.Instance.ConnectedToNetworkEvent += HandleConnectedToNetworkEvent;
		TT2NetManager.Instance.OnMatchFound += HandleOnMatchFound;
		TT2NetManager.Instance.OnPrepareForBattleEvent += HandleOnPrepareForBattleEvent;
		TT2NetManager.Instance.SearchForMatchCanceled += HandleSearchForMatchCanceled;
		Player.instance.pvpData.BattleRequestAdded += HandleBattleRequestAdded;
		Player.instance.pvpData.BattleRequestsUpdated += HandleRequestsUpdated;
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
		TT2NetChat.Instance.BattleRequestCanceled += HandleBattleRequestCanceled;
		Player.instance.pvpData.FriendRequestAdded += HandleFriendRequestedAdded;
		Player.instance.pvpData.FriendRequestsUpdated += HandleRequestsUpdated;
		Player.instance.pvpData.FriendRequestAccepted += HandleFriendRequestAccepted;
		Player.instance.pvpData.FriendRequestDeclined += HandleFriendRequestDeclined;
		OnlineCheck.OnOnlineStatusChange += OnlineCheck_OnlineStatusChange;
		OnlineCheck.OnOnlineCheckComplete += OnlineCheck_OnlineCheckComplete;
		TT2NetBluetooth.Instance.Initialized += Instance_ServerInitialized;
	}

	private void OnDisable()
	{
		if (TT2NetManager.Instance != null)
		{
			TT2NetManager.Instance.ConnectedToNetworkEvent -= HandleConnectedToNetworkEvent;
			TT2NetManager.Instance.OnMatchFound -= HandleOnMatchFound;
			TT2NetManager.Instance.OnPrepareForBattleEvent -= HandleOnPrepareForBattleEvent;
			TT2NetManager.Instance.SearchForMatchCanceled -= HandleSearchForMatchCanceled;
		}
		Player.instance.pvpData.BattleRequestAdded -= HandleBattleRequestAdded;
		Player.instance.pvpData.BattleRequestsUpdated -= HandleRequestsUpdated;
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
		if (TT2NetChat.Instance != null)
		{
			TT2NetChat.Instance.BattleRequestCanceled -= HandleBattleRequestCanceled;
		}
		Player.instance.pvpData.FriendRequestAdded -= HandleFriendRequestedAdded;
		Player.instance.pvpData.FriendRequestsUpdated -= HandleRequestsUpdated;
		Player.instance.pvpData.FriendRequestAccepted -= HandleFriendRequestAccepted;
		Player.instance.pvpData.FriendRequestDeclined -= HandleFriendRequestDeclined;
		OnlineCheck.OnOnlineStatusChange -= OnlineCheck_OnlineStatusChange;
		OnlineCheck.OnOnlineCheckComplete -= OnlineCheck_OnlineCheckComplete;
		if (TT2NetBluetooth.Instance != null)
		{
			TT2NetBluetooth.Instance.Initialized -= Instance_ServerInitialized;
		}
		if (networkDiscoveryServer.running)
		{
			networkDiscoveryServer.StopBroadcast();
		}
		if (networkDiscoveryClient.running)
		{
			networkDiscoveryClient.StopBroadcast();
		}
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		ShowPage(PageName.AcceptingBattleRequest, false, battleRequest);
	}

	private IEnumerator Start()
	{
		prevPages = new List<PageName>();
		startedExitingScene = false;
		exitBtnTVAnimator.speed = 0f;
		titanTowerTVAnimator.speed = 0f;
		upperCenterTVAnimator.speed = 0f;
		wifiTVAnimator.speed = 0f;
		lowerRightTVAnimator.speed = 0f;
		addFriendTVAnimator.speed = 0f;
		baseMenu.SetActive(value: false);
		portraitSelector.gameObject.SetActive(value: false);
		nameSelector.PrepareMenu();
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		themeMusic.Play();
		currencyGUI.Init(Player.instance);
		friendCodeText.text = FriendCodeGenerator.GetFormattedCode(Player.instance.pvpData.playerID);
		skillRatingTextMesh.text = Player.instance.pvpData.skillRating.ToString();
		OnlineCheck.isEndlessMode = true;
		OnlineCheck.Refresh();
		yield return new WaitUntil(() => initialInternetCheckComplete);
		PageName firstPage = ((initBattleRequest == null) ? PageName.Base : PageName.AcceptingBattleRequest);
		object[] firstPageArgs = null;
		if (firstPage == PageName.AcceptingBattleRequest)
		{
			prevPages.Add(PageName.Base);
			firstPageArgs = new object[1] { initBattleRequest };
		}
		else
		{
			PvpData pvpData = Player.instance.pvpData;
			if (pvpData.currentXP != pvpData.PreviousXP || pvpData.level != pvpData.PreviousLevel)
			{
				prevPages.Add(firstPage);
				firstPage = PageName.RankUp;
			}
			if (!Player.instance.HasTriggered("EnteredLegionOfDoom"))
			{
				rankUpBar.rankUpBarAnimator.Play("LOD_Menu_RankBar_Outro", -1, 1f);
				firstTimeInLODMode = true;
				yield return StartCoroutine(ShowDialogue(introConvoPart1, showExitButton: false));
			}
			if (string.IsNullOrEmpty(pvpData.PlayerName))
			{
				rankUpBar.rankUpBarAnimator.Play("LOD_Menu_RankBar_Outro", -1, 1f);
				firstPage = PageName.NameSelector;
			}
			else if (!TT2NetManager.Instance.IsConnected)
			{
				PhotonNetwork.NickName = pvpData.PlayerName;
			}
		}
		HandleRequestsUpdated();
		while (!LoadingScreenCover.outroFinished)
		{
			yield return null;
		}
		exitBtnTVAnimator.speed = 1f;
		titanTowerTVAnimator.speed = 1f;
		upperCenterTVAnimator.speed = 1f;
		wifiTVAnimator.speed = 1f;
		lowerRightTVAnimator.speed = 1f;
		addFriendTVAnimator.speed = 1f;
		upperCenterTVAnimator.Play("LOD_Menu_TopMiddleTV_Outro", -1, 1f);
		upperCenterTVAnimator.Update(0f);
		addFriendTVAnimator.Play("LOD_Menu_TitanTower_Outro", -1, 1f);
		addFriendTVAnimator.Update(0f);
		rankUpBar.OnOpenPortraitSelectorClicked += HandleOpenPortraitSelectorClicked;
		rankUpBar.OnPlayerNameBtnClicked += HandlePlayerNameBtnClicked;
		exitBtn.OnClick += HandleExitBtnClicked;
		titanTowerBtn.OnClick += HandleTitanTowerBtnClicked;
		battleOnlineBtn.OnClick += HandleBattleOnlineBtnClicked;
		battleNearbyBtn.OnClick += HandleBattleNearbyBtnClicked;
		friendsListBtn.OnClick += HandleFriendsListBtnClicked;
		battleCPUBtn.OnClick += HandleBattleCpuBtnClicked;
		battleCPUConfirmButton.OnClick += HandleBattleCPUConfirmClick;
		portraitSelector.PortraitSelected += HandleUnlockedPortraitSelected;
		portraitSelector.PortraitSelectedForPurchase += HandlePortraitSelectedForPurchase;
		confirmPurchaseYesBtn.OnClick += HandleConfirmPurchaseYesClicked;
		confirmPurchaseNoBtn.OnClick += HandleConfirmPurchaseNoClicked;
		addFriendBtn.OnClick += HandleAddFriendClicked;
		friendsListMenu.BattleClicked += FriendsListMenu_BattleClicked;
		battleLocalMenu.BattleClicked += BattleLocalMenu_BattleClicked;
		debugLevelUpBtn.OnClick += HandleDebugLevelUpBtnClicked;
		debugClearFriendsList.OnClick += HandleDebugClearFriendsListClicked;
		inviteMenuBtn.OnClick += HandleInviteMenuBtnClicked;
		lowerRightAnimatorEventListener.EventTriggered += HandleLowerRightAnimEvent;
		Player.instance.SetTrigger("EnteredLegionOfDoom");
		GFAudioManager.instance.PlayClip(allTVsIntroSFX, 0.8f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		CheckUnlockedPortraits();
		if (firstPageArgs == null)
		{
			ShowPage(firstPage, true);
		}
		else
		{
			ShowPage(firstPage, backPressed: true, firstPageArgs);
		}
		LogPlayerStatus(Player.instance.pvpData, "Start");
		yield return null;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			if (networkDiscoveryServer.running)
			{
				networkDiscoveryServer.StopBroadcast();
			}
			if (networkDiscoveryClient.running)
			{
				networkDiscoveryClient.StopBroadcast();
			}
			return;
		}
		if (!networkDiscoveryServer.running)
		{
			TT2NetLocalBroadcastData tT2NetLocalBroadcastData = new TT2NetLocalBroadcastData();
			tT2NetLocalBroadcastData.friendData = Player.instance.pvpData.MyFriendData();
			tT2NetLocalBroadcastData.appVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
			networkDiscoveryServer.broadcastData = JsonUtility.ToJson(tT2NetLocalBroadcastData);
			networkDiscoveryServer.Initialize();
			networkDiscoveryServer.StartAsServer();
		}
		if (!networkDiscoveryClient.running)
		{
			networkDiscoveryClient.Initialize();
			networkDiscoveryClient.StartAsClient();
		}
	}

	private void OnDestroy()
	{
		initBattleRequest = null;
		OnlineCheck.isEndlessMode = false;
		if (TT2NetBluetooth.Instance != null)
		{
			TT2NetBluetooth.Instance.Initialized -= Instance_ServerInitialized;
		}
		for (int num = Player.instance.pvpData.BattleRequests.Count - 1; num >= 0; num--)
		{
			BattleRequest battleRequest = Player.instance.pvpData.BattleRequests[num];
			if (battleRequest.isLocal)
			{
				Player.instance.pvpData.DeclineBattleRequest(battleRequest.playerId);
			}
		}
		rankUpBar.OnOpenPortraitSelectorClicked -= HandleOpenPortraitSelectorClicked;
		exitBtn.OnClick -= HandleExitBtnClicked;
		titanTowerBtn.OnClick -= HandleTitanTowerBtnClicked;
		battleOnlineBtn.OnClick -= HandleBattleOnlineBtnClicked;
		friendsListBtn.OnClick -= HandleFriendsListBtnClicked;
		battleCPUBtn.OnClick -= HandleBattleCpuBtnClicked;
		battleCPUConfirmButton.OnClick -= HandleBattleCPUConfirmClick;
		confirmPurchaseYesBtn.OnClick -= HandleConfirmPurchaseYesClicked;
		confirmPurchaseNoBtn.OnClick -= HandleConfirmPurchaseNoClicked;
		addFriendBtn.OnClick -= HandleAddFriendClicked;
		friendsListMenu.BattleClicked -= FriendsListMenu_BattleClicked;
		battleLocalMenu.BattleClicked -= BattleLocalMenu_BattleClicked;
		debugLevelUpBtn.OnClick -= HandleDebugLevelUpBtnClicked;
		inviteMenuBtn.OnClick -= HandleInviteMenuBtnClicked;
		debugClearFriendsList.OnClick -= HandleDebugClearFriendsListClicked;
	}

	private void Update()
	{
		if (curPage == PageName.Base)
		{
			if (Input.GetKeyDown(KeyCode.Keypad1))
			{
				LegionOfDoomSettings legionOfDoomSettings = GameManager.instance.legionOfDoomSettings;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					Player.instance.pvpData.currentWinStreak = 0;
					PvpData pvpData = Player.instance.pvpData;
					pvpData.coinsReceived = pvpData.coinsReceived;
					Player.instance.pvpData.lastMatchIsLoss = true;
					Player.instance.AddPvpXP(legionOfDoomSettings.lossXP, 0);
				}
				else
				{
					Player.instance.pvpData.currentWinStreak++;
					Player.instance.pvpData.coinsReceived += legionOfDoomSettings.coinRewardWin;
					Player.instance.pvpData.lastMatchIsLoss = false;
					Player.instance.AddPvpXP(debugXPGain, legionOfDoomSettings.humanWinXP);
				}
				LogPlayerStatus(Player.instance.pvpData, $"Gain {debugXPGain + legionOfDoomSettings.humanWinXP + ((Player.instance.pvpData.currentWinStreak > 1) ? (legionOfDoomSettings.winStreakXP * Player.instance.pvpData.currentWinStreak) : 0)} PvP XP");
				ShowPage(PageName.RankUp, false);
			}
			if (Input.GetKeyDown(KeyCode.Keypad2))
			{
				StartCoroutine(DebugTransition());
			}
			if (Input.GetKeyDown(KeyCode.Keypad3))
			{
				debugDialogueToggle = !debugDialogueToggle;
				if (debugDialogueToggle)
				{
					StartCoroutine(BringInCharacters());
				}
				else
				{
					StartCoroutine(ExitDialogueCharacters());
				}
			}
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				ShowPage(PageName.NameSelector, false);
			}
			if (Input.GetKeyDown(KeyCode.Keypad5))
			{
				LogPlayerStatus(Player.instance.pvpData, "Manual Check");
			}
			if (Input.GetKeyDown(KeyCode.B))
			{
				Player.instance.AddCurrency(1000);
			}
		}
		if (Input.GetKeyDown(KeyCode.Keypad6))
		{
			Debug.Log("Create Test Battle Request...");
			if (Player.instance.pvpData.friends.Count > 0)
			{
				BattleRequest battleRequest = new BattleRequest();
				battleRequest.playerId = Player.instance.pvpData.friends[0].playerID;
				battleRequest.region = "US";
				battleRequest.roomName = "TestRoom";
				BattleRequest battleRequest2 = battleRequest;
				Player.instance.pvpData.AddBattleRequest(battleRequest2);
			}
			else
			{
				Debug.LogError("You need at least 1 friend to test a battle request...");
			}
		}
		if (Input.GetKeyDown(KeyCode.Keypad7))
		{
			Debug.Log("Create Test Friend Request...");
			FriendRequest friendRequest = new FriendRequest();
			friendRequest.playerId = FriendCodeGenerator.Generate();
			friendRequest.playerName = "TestFriend" + UnityEngine.Random.Range(0, 101);
			friendRequest.level = UnityEngine.Random.Range(1, GameManager.instance.legionOfDoomLevelTable.MaxLevel + 1);
			friendRequest.portraitSpriteName = GameManager.instance.legionOfDoomSettings.GetRandomPortraitSprite();
			friendRequest.skillRating = UnityEngine.Random.Range(1000, 4001);
			FriendRequest friendRequest2 = friendRequest;
			Player.instance.pvpData.AddFriendRequest(friendRequest2);
		}
		if (Input.GetKeyDown(KeyCode.Keypad8))
		{
			CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(0);
			CharacterRoller.RollCharacterDataWeighted(character, 1, character.uid, 20, GameManager.instance.legionOfDoomSettings.cpuStatDecisionWeights, GameManager.instance.legionOfDoomSettings.overLevel);
		}
		if (Input.GetKeyDown(KeyCode.Keypad9))
		{
			string text = "All Viewed Portrait Entries:\n";
			for (int i = 0; i < Player.instance.pvpData.viewedPortraits.Count; i++)
			{
				text += $"{i}: {Player.instance.pvpData.viewedPortraits[i].portrait}, isViewed: {Player.instance.pvpData.viewedPortraits[i].isViewed} \n";
			}
			Debug.Log(text);
		}
	}

	private IEnumerator DebugTransition()
	{
		ShowPage(PageName.Matchmaking, false);
		yield return new WaitForSeconds(0.266f);
		HandleExitBtnClicked();
	}

	private void Instance_ServerInitialized()
	{
		TT2NetBluetooth.Instance.StartServer();
		TT2NetBluetooth.Instance.StartClient();
	}

	private void OnlineCheck_OnlineStatusChange(bool isConnected)
	{
		if (isConnected && !TT2NetChat.Instance.IsConnected)
		{
			TT2NetChat.Instance.Connect(Player.instance.pvpData.playerID);
		}
	}

	private void OnlineCheck_OnlineCheckComplete(bool isConnected, NetworkReachability networkReachability)
	{
		if (!initialInternetCheckComplete)
		{
			initialInternetCheckComplete = true;
		}
	}

	private void ShowPage(PageName page, bool backPressed = false, params object[] args)
	{
		if (curPage == page)
		{
			return;
		}
		if (!backPressed && !prevPages.Contains(curPage))
		{
			prevPages.Add(curPage);
		}
		switch (curPage)
		{
		case PageName.Connect:
			connectScreen.SetActive(value: false);
			break;
		case PageName.Base:
			baseMenu.SetActive(value: false);
			break;
		case PageName.Matchmaking:
			matchmakingMenu.SetActive(value: false);
			lowerRightTVMatchmakingObj.SetActive(value: false);
			break;
		case PageName.MatchmakingNoInternet:
			matchmakingMenu.SetActive(value: false);
			lowerRightTVMatchmakingObj.SetActive(value: false);
			break;
		case PageName.FriendsList:
			friendsListMenu.gameObject.SetActive(value: false);
			addFriendBtn.gameObject.SetActive(value: false);
			break;
		case PageName.FriendsListNoInternet:
			friendsListMenu.gameObject.SetActive(value: false);
			addFriendBtn.gameObject.SetActive(value: false);
			break;
		case PageName.AddFriend:
			addFriendMenu.gameObject.SetActive(value: false);
			addFriendMenu.FriendAdded -= HandleFriendAdded;
			addFriendMenu.Canceled -= HandleAddFriendCanceled;
			break;
		case PageName.BattleLocal:
			battleLocalMenu.gameObject.SetActive(value: false);
			break;
		case PageName.RankUp:
			currencyGUI.Hide();
			rewardMenu.gameObject.SetActive(value: false);
			break;
		case PageName.PortraitSelector:
			currencyGUI.Hide();
			portraitSelector.gameObject.SetActive(value: false);
			lowerRightTVPurchaseObj.SetActive(value: false);
			break;
		case PageName.NameSelector:
			nameSelector.gameObject.SetActive(value: false);
			saveNameBtn.gameObject.SetActive(value: false);
			nameSelector.NameSelected -= HandleNameSelected;
			break;
		case PageName.Invites:
			inviteMenu.gameObject.SetActive(value: false);
			break;
		case PageName.WaitingForFriend:
			matchmakingMenu.SetActive(value: false);
			inviteMenuBtn.enabled = true;
			rankUpBar.playerNameBtn.GetComponent<Collider2D>().enabled = true;
			rankUpBar.portraitSelectorBtn.GetComponent<Collider2D>().enabled = true;
			break;
		case PageName.AcceptingBattleRequest:
			matchmakingMenu.SetActive(value: false);
			inviteMenuBtn.enabled = true;
			rankUpBar.playerNameBtn.GetComponent<Collider2D>().enabled = true;
			rankUpBar.portraitSelectorBtn.GetComponent<Collider2D>().enabled = true;
			break;
		}
		inviteMenuBtn.gameObject.SetActive(value: true);
		curPage = page;
		switch (curPage)
		{
		case PageName.Connect:
			connectScreen.SetActive(value: true);
			SetRankBarShown(show: false);
			SetLowerLeftTVShown(show: false);
			SetLowerRightTVShown(show: false);
			TT2NetManager.Instance.Connect(Player.instance.pvpData.playerID, string.Empty);
			break;
		case PageName.Base:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit01");
			baseMenu.SetActive(value: true);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetTitanTowerTVShown(show: true);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			if (!networkDiscoveryServer.running)
			{
				TT2NetLocalBroadcastData tT2NetLocalBroadcastData = new TT2NetLocalBroadcastData();
				tT2NetLocalBroadcastData.friendData = Player.instance.pvpData.MyFriendData();
				tT2NetLocalBroadcastData.appVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
				networkDiscoveryServer.broadcastData = JsonUtility.ToJson(tT2NetLocalBroadcastData);
				networkDiscoveryServer.Initialize();
				networkDiscoveryServer.StartAsServer();
			}
			if (!networkDiscoveryClient.running)
			{
				networkDiscoveryClient.Initialize();
				networkDiscoveryClient.StartAsClient();
			}
			break;
		case PageName.Matchmaking:
			matchmakingMenu.SetActive(value: true);
			matchmakingScreen.InitSearching();
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			break;
		case PageName.MatchmakingNoInternet:
			matchmakingMenu.SetActive(value: true);
			matchmakingScreen.InitNoInternet();
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			break;
		case PageName.FriendsList:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			localPlayersFoundObj.SetActive(value: false);
			saveNameBtn.gameObject.SetActive(value: false);
			SetLowerLeftTVShown(show: true);
			SetExitTVShown(show: true);
			SetUpperCenterTVShown(show: true);
			SetAddFriendTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			friendCodeObj.SetActive(value: true);
			addFriendBtn.gameObject.SetActive(value: true);
			friendsListMenu.gameObject.SetActive(value: true);
			break;
		case PageName.FriendsListNoInternet:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetAddFriendTVShown(show: false);
			localPlayersFoundObj.SetActive(value: false);
			saveNameBtn.gameObject.SetActive(value: false);
			addFriendBtn.gameObject.SetActive(value: false);
			SetLowerLeftTVShown(show: true);
			SetExitTVShown(show: true);
			SetUpperCenterTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			friendCodeObj.SetActive(value: true);
			friendsListMenu.gameObject.SetActive(value: true);
			friendsListMenu.InitForNoInternet();
			break;
		case PageName.AddFriend:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetLowerLeftTVShown(show: false);
			SetLowerRightTVShown(show: false);
			SetRankBarShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetUpperCenterTVShown(show: true);
			addFriendMenu.gameObject.SetActive(value: true);
			addFriendMenu.Init();
			addFriendMenu.FriendAdded += HandleFriendAdded;
			addFriendMenu.Canceled += HandleAddFriendCanceled;
			break;
		case PageName.BattleLocal:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			friendCodeObj.SetActive(value: false);
			localPlayersFoundObj.SetActive(value: true);
			battleLocalMenu.gameObject.SetActive(value: true);
			battleLocalMenu.Init();
			break;
		case PageName.RankUp:
			SetExitTVShown(show: false);
			SetTitanTowerTVShown(show: false);
			SetLowerLeftTVShown(show: false);
			SetLowerRightTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetRankBarShown(show: true);
			currencyGUI.Show();
			rewardMenu.gameObject.SetActive(value: true);
			StartCoroutine(BeginRankUpSequence());
			break;
		case PageName.PortraitSelector:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetLowerLeftTVShown(show: false);
			SetLowerRightTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetRankBarShown(show: true);
			inviteMenuBtn.gameObject.SetActive(value: false);
			lowerRightTVPurchaseObj.SetActive(value: true);
			currencyGUI.Show();
			portraitSelector.gameObject.SetActive(value: true);
			portraitSelector.Init();
			break;
		case PageName.NameSelector:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetExitTVShown(!string.IsNullOrEmpty(Player.instance.pvpData.PlayerName));
			SetTitanTowerTVShown(show: false);
			SetLowerLeftTVShown(show: false);
			SetLowerRightTVShown(show: false);
			SetRankBarShown(!string.IsNullOrEmpty(Player.instance.pvpData.PlayerName));
			SetUpperCenterTVShown(show: false);
			addFriendBtn.gameObject.SetActive(value: false);
			SetAddFriendTVShown(show: true);
			saveNameBtn.gameObject.SetActive(value: true);
			nameSelector.gameObject.SetActive(value: true);
			nameSelector.Init();
			nameSelector.NameSelected += HandleNameSelected;
			break;
		case PageName.Invites:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			inviteMenu.gameObject.SetActive(value: true);
			inviteMenu.Init();
			break;
		case PageName.WaitingForFriend:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: true);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			inviteMenuBtn.enabled = false;
			rankUpBar.playerNameBtn.GetComponent<Collider2D>().enabled = false;
			rankUpBar.portraitSelectorBtn.GetComponent<Collider2D>().enabled = false;
			matchmakingMenu.SetActive(value: true);
			matchmakingScreen.InitWaitingForFriend((FriendData)args[0], (bool)args[1]);
			break;
		case PageName.AcceptingBattleRequest:
			exitBtnSprite.SetSprite("multiplayermenu_topleft_tv_exit02");
			SetTitanTowerTVShown(show: false);
			SetUpperCenterTVShown(show: false);
			SetAddFriendTVShown(show: false);
			SetExitTVShown(show: false);
			SetLowerLeftTVShown(show: true);
			SetLowerRightTVShown(show: true);
			SetRankBarShown(show: true);
			inviteMenuBtn.enabled = false;
			rankUpBar.playerNameBtn.GetComponent<Collider2D>().enabled = false;
			rankUpBar.portraitSelectorBtn.GetComponent<Collider2D>().enabled = false;
			matchmakingMenu.SetActive(value: true);
			matchmakingScreen.InitAcceptingBattleRequest((BattleRequest)args[0]);
			break;
		}
		if (PageUpdated != null)
		{
			PageUpdated(curPage);
		}
	}

	private IEnumerator BeginRankUpSequence()
	{
		Debug.Log("Begin Rank Up Sequence...");
		dialogueTree.gameObject.SetActive(value: true);
		string emptyConvo = "CONVO_LOD_EMPTY";
		rewardMenu.Init();
		yield return StartCoroutine(dialogueTree.BringInCharacters(Resources.Load<Conversation>("Data/TT2/" + emptyConvo), GUICamera.instance.camera));
		yield return new WaitUntil(() => rewardMenu.outroing);
		if (useAutoSave)
		{
			Player.instance.Save();
		}
		if (!debugResetUnlockedPortraits)
		{
			List<LegionOfDoomTriggeredConvo> convos = GameManager.instance.legionOfDoomSettings.conversations;
			int len = convos.Count;
			int curLvl = Player.instance.pvpData.level;
			bool convoTriggered = false;
			for (int i = 0; i < len; i++)
			{
				LegionOfDoomTriggeredConvo convo = convos[i];
				if (curLvl >= convo.requiredPvpRank && !Player.instance.pvpData.triggeredConvosPlayed.Contains(convo.id))
				{
					Player.instance.pvpData.triggeredConvosPlayed.Add(convo.id);
					convoTriggered = true;
					yield return StartCoroutine(ShowDialogue(convo.convo, showExitButton: false));
				}
			}
			if (!convoTriggered)
			{
				yield return StartCoroutine(dialogueTree.ExitSequenceDoNotDestroy());
			}
		}
		else
		{
			yield return StartCoroutine(dialogueTree.ExitSequenceDoNotDestroy());
		}
		while (dialogueTree.gameObject.activeSelf)
		{
			yield return null;
		}
		yield return new WaitUntil(() => rewardMenu.outroFinished);
		LogPlayerStatus(Player.instance.pvpData, "Finshed Rank Up Sequence...");
		CheckUnlockedPortraits();
		Player.instance.Save();
		HandleExitBtnClicked();
	}

	private void HandleConnectedToNetworkEvent()
	{
		Player.instance.pvpData.hasConnected = true;
	}

	private void HandleSearchForMatchCanceled()
	{
		if (curPage == PageName.Matchmaking)
		{
			ShowPage(PageName.Base, true);
		}
	}

	private void HandleOnMatchFound(TT2NetPlayer otherPlayer)
	{
		Debug.Log("[LegionOfDoomController::HandleOnMatchFound] - We found a match, so we should now move to the Pre Battle screen");
		exitBtn.enabled = false;
		exitBtnSprite.color = new Color(0.5f, 0.5f, 0.5f);
		int uid = GameManager.instance.battleArenaDatabase.GetRandomArena().uid;
		TT2NetManager.SendPrepareForBattle(uid);
		StartCoroutine(WaitToBeginBattle(otherPlayer));
	}

	private void HandleOnPrepareForBattleEvent(TT2NetPlayer otherPlayer, int arenaUid)
	{
		Debug.Log("[LegionOfDoomController::HandleOnPrepareForBattleEvent] - The master client says we should move onto the Pre Battle screen");
		StartCoroutine(WaitToBeginBattle(otherPlayer));
	}

	private IEnumerator WaitToBeginBattle(TT2NetPlayer otherPlayer)
	{
		yield return new WaitUntil(() => otherPlayer.Figures.Count > 0);
		BeginBattle(otherPlayer);
	}

	private void BeginBattle(TT2NetPlayer otherPlayer)
	{
		BattleDefinition battleDefinition = new BattleDefinition();
		battleDefinition.type = ((!PhotonNetwork.CurrentRoom.IsVisible) ? BattleType.PvpFriend : BattleType.Pvp);
		if (battleDefinition.type == BattleType.Pvp)
		{
			BattleCondition battleCondition = new BattleCondition();
			battleCondition.conditionType = BattleCondition.ConditionType.FigureLevel;
			battleCondition.comparison1 = Comparison.AtLeast;
			battleCondition.intValue1 = 1;
			battleCondition.comparison2 = Comparison.AtLeast;
			battleCondition.intValue2 = GameManager.instance.matchmakingSettings.minFigureLevel;
			battleDefinition.newBattleConditions = new List<BattleCondition> { battleCondition };
		}
		battleDefinition.playerAIType = PlayerAIType.Network;
		battleDefinition.enemyCharacterName = CharacterName.None;
		battleDefinition.enemyName = otherPlayer.PlayerName;
		battleDefinition.enemyActiveParty = new List<Character>(otherPlayer.Figures);
		battleDefinition.districtLocation = CityLocation.None;
		battleDefinition.arenaUID = (int)PhotonNetwork.CurrentRoom.CustomProperties["BattleArenaUid"];
		battleDefinition.pvpPortraitSpriteCollection = rankUpBar.portrait.Collection;
		battleDefinition.cityName = CityName.JumpCity;
		PreBattleController.battleDefinition = battleDefinition;
		TT2NetMatch.SendStartMatch();
		StartCoroutine("BeginPreBattle");
	}

	private IEnumerator BeginPreBattle()
	{
		Debug.Log("BeginPreBattle");
		yield return new WaitForSeconds(0.3f);
		yield return new WaitForEndOfFrame();
		ChallengeModeManager.isNonJLChallenge = false;
		ChallengeModeManager.challengeModeDirty = false;
		ChallengeModeManager.isChallengeModeActive = false;
		TT2NetChat.Instance.UpdateUserStatus(6);
		LoadingScreenCover.LoadScene("PreBattleLOD", showLoadingScreen: false);
	}

	private void HandleExitBtnClicked()
	{
		if (startedExitingScene)
		{
			return;
		}
		if (curPage == PageName.Matchmaking)
		{
			if (!matchmakingScreen.didCancel)
			{
				prevPages.Pop();
				matchmakingScreen.CancelSearch();
			}
		}
		else if (curPage == PageName.WaitingForFriend)
		{
			if (!matchmakingScreen.didCancel)
			{
				prevPages.Pop();
				matchmakingScreen.CancelWaiting();
			}
		}
		else if (prevPages.Count == 0)
		{
			startedExitingScene = true;
			Player.instance.Save();
			TT2NetManager.Instance.Disconnect();
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
		else
		{
			ShowPage(prevPages.Pop(), true);
		}
	}

	private void HandleTitanTowerBtnClicked()
	{
		Debug.Log("Handle Titan Tower Btn Clicked...");
		if (!startedExitingScene)
		{
			startedExitingScene = true;
			TitanTowerController.fromLegionOfDoomScene = true;
			LoadingScreenCover.LoadScene("TitanTower");
		}
	}

	private void HandleBattleOnlineBtnClicked()
	{
		if (!OnlineCheck.isInternetAvailable)
		{
			ShowPage(PageName.MatchmakingNoInternet, false);
			return;
		}
		int num = 0;
		for (int i = 0; i < Player.instance.activeParty.Count; i++)
		{
			if (Player.instance.activeParty[i].level >= GameManager.instance.matchmakingSettings.minFigureLevel)
			{
				num++;
			}
		}
		if (num >= 3)
		{
			ShowPage(PageName.Matchmaking, false);
		}
		else
		{
			StartCoroutine(WaitForRequirementsConvo());
		}
	}

	private IEnumerator WaitForRequirementsConvo()
	{
		SetRankBarShown(show: false);
		yield return StartCoroutine(ShowDialogue(requirementsNotMetConvo, showExitButton: false));
		SetRankBarShown(show: true);
	}

	private void HandleBattleNearbyBtnClicked()
	{
		ShowPage(PageName.BattleLocal, false);
	}

	private void HandleFriendsListBtnClicked()
	{
		if (!OnlineCheck.isInternetAvailable)
		{
			ShowPage(PageName.FriendsListNoInternet, false);
		}
		else
		{
			ShowPage(PageName.FriendsList, false);
		}
	}

	private void HandleBattleCpuBtnClicked()
	{
		FriendData friendData = new FriendData();
		friendData.name = "Mr. C.P.U.";
		friendData.playerID = "CPUPlayer";
		friendData.portraitSpriteName = "multiplayermenu_battleCPU";
		friendData.level = UnityEngine.Random.Range(1, 11);
		friendData.skillRating = UnityEngine.Random.Range(1500, 4001);
		friendData.requestAccepted = false;
		FriendData playerData = friendData;
		ChallengeModeManager.isNonJLChallenge = true;
		ChallengeModeManager.challengeModeDirty = true;
		ChallengeModeManager.isChallengeModeActive = true;
		ChallengeModeManager.challengeModeHard = false;
		StartBattleWithPlayerRandomSettings(playerData);
	}

	private void HandleOpenPortraitSelectorClicked()
	{
		ShowPage(PageName.PortraitSelector, false);
	}

	private void HandleBattleCPUConfirmClick()
	{
		matchmakingScreen.CancelSearch();
		FriendData friendData = new FriendData();
		friendData.name = "Mr. C.P.U.";
		friendData.playerID = "CPUPlayer";
		friendData.portraitSpriteName = "multiplayermenu_battleCPU";
		friendData.level = UnityEngine.Random.Range(1, 11);
		friendData.skillRating = UnityEngine.Random.Range(1500, 4001);
		friendData.requestAccepted = false;
		FriendData playerData = friendData;
		ChallengeModeManager.isNonJLChallenge = true;
		ChallengeModeManager.challengeModeDirty = true;
		ChallengeModeManager.isChallengeModeActive = true;
		ChallengeModeManager.challengeModeHard = false;
		StartBattleWithPlayerRandomSettings(playerData);
	}

	private void HandleUnlockedPortraitSelected()
	{
		SetLowerRightTVShown(show: false);
	}

	private void HandlePortraitSelectedForPurchase(LegionOfDoomPortrait selectedPortrait)
	{
		SetLowerRightTVShown(show: true);
		portraitSelectedForPurchase = selectedPortrait;
	}

	private void HandleConfirmPurchaseYesClicked()
	{
		if (Player.instance.currency >= portraitSelectedForPurchase.unlockData.goldCost)
		{
			Player.instance.RemoveCurrency(portraitSelectedForPurchase.unlockData.goldCost);
			Player.instance.pvpData.UnlockPortrait(portraitSelectedForPurchase.unlockData.portraitSprite);
			Player.instance.pvpData.PortraitSpriteName = portraitSelectedForPurchase.unlockData.portraitSprite;
			Player.instance.Save();
			portraitSelector.Init();
			SetLowerRightTVShown(show: false);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_collect_rainbowcoin", GFAudioManager.SFX_GROUP, string.Empty);
			return;
		}
		Color endValue = new Color(1f, 0.22f, 0.22f, 1f);
		DOTween.Kill(confirmPurchaseYesBtn, complete: true);
		tk2dSprite sprite = confirmPurchaseYesBtn.GetComponent<tk2dSprite>();
		DOTween.To(() => sprite.color, (Color x) =>
		{
			sprite.color = x;
		}, endValue, 0.1f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)
			.SetTarget(confirmPurchaseYesBtn);
		TextMeshPro componentInChildren = confirmPurchaseYesBtn.GetComponentInChildren<TextMeshPro>();
		componentInChildren.DOColor(endValue, 0.1f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)
			.SetTarget(confirmPurchaseYesBtn);
		currencyGUI.PlayBump();
		portraitSelector.ShowNotEnoughChangeMessage();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void HandleConfirmPurchaseNoClicked()
	{
		Debug.Log("Confirm Purchase, No");
		SetLowerRightTVShown(show: false);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void HandleAddFriendClicked()
	{
		ShowPage(PageName.AddFriend, false);
	}

	private void HandleFriendAdded()
	{
		HandleExitBtnClicked();
	}

	private void HandleAddFriendCanceled()
	{
		HandleExitBtnClicked();
	}

	private void HandlePlayerNameBtnClicked()
	{
		ShowPage(PageName.NameSelector, false);
	}

	private void HandleNameSelected(string selectedName)
	{
		Player.instance.pvpData.PlayerName = selectedName;
		Player.instance.Save();
		PhotonNetwork.NickName = selectedName;
		if (firstTimeInLODMode)
		{
			firstTimeInLODMode = false;
			StartCoroutine(OutroFirstTimeNameSelectedSequence());
		}
		else
		{
			HandleExitBtnClicked();
		}
	}

	private IEnumerator OutroFirstTimeNameSelectedSequence()
	{
		SetAddFriendTVShown(show: false);
		nameSelector.gameObject.SetActive(value: false);
		yield return StartCoroutine(ShowDialogue(introConvoPart2, showExitButton: false));
		ShowPage(PageName.Base, true);
	}

	private void HandleInviteMenuBtnClicked()
	{
		ShowPage(PageName.Invites, false);
	}

	private void HandleRequestsUpdated()
	{
		int pendingRequestCount = Player.instance.pvpData.PendingRequestCount;
		if (pendingRequestCount == 0)
		{
			inviteAntennaAnimator.Play("LOD_Menu_AntennaOff_Anim");
		}
		inviteNumTextMesh.text = pendingRequestCount.ToString();
	}

	private void HandleBattleRequestAdded(BattleRequest battleRequest)
	{
		inviteAntennaAnimator.Play("LOD_Menu_AntennaTrigger_Anim");
	}

	private void HandleBattleRequestCanceled(string userId)
	{
		if (!Player.instance.pvpData.HasBattleRequest(userId))
		{
			if (TT2NetManager.Instance.IsConnectedToMaster && TT2NetManager.Instance.IsConnectedAndReady)
			{
				ShowPage(PageName.Base, true);
			}
			else
			{
				StartCoroutine(WaitToGoToPage(PageName.Base, true));
			}
		}
	}

	private IEnumerator WaitToGoToPage(PageName page, bool backPressed = true, params object[] args)
	{
		yield return new WaitUntil(() => TT2NetManager.Instance.IsConnectedToMaster && TT2NetManager.Instance.IsConnectedAndReady);
		ShowPage(page, backPressed, args);
	}

	private void HandleFriendRequestedAdded(FriendRequest friendRequest)
	{
		inviteAntennaAnimator.Play("LOD_Menu_AntennaTrigger_Anim");
	}

	private void HandleFriendRequestAccepted(string playerId)
	{
		HandleRequestsUpdated();
	}

	private void HandleFriendRequestDeclined(string playerId)
	{
		HandleRequestsUpdated();
	}

	private void HandleLowerRightAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "introFinished")
		{
			lowerRightAnimatorEventListener.EventTriggered -= HandleLowerRightAnimEvent;
			if (Player.instance.pvpData.PendingRequestCount > 0 && inviteAntennaAnimator.IsName("LOD_Menu_AntennaOff_Anim"))
			{
				inviteAntennaAnimator.Play("LOD_Menu_AntennaTrigger_Anim");
			}
		}
	}

	private IEnumerator BringInCharacters()
	{
		dialogueTree.gameObject.SetActive(value: true);
		StartCoroutine(dialogueTree.BringInCharacters(Resources.Load("Data/Conversations/CONVO_JLA_EMPTY") as Conversation, GUICamera.instance.camera));
		yield return null;
	}

	private IEnumerator ExitDialogueCharacters()
	{
		yield return StartCoroutine(dialogueTree.ExitSequenceDoNotDestroy());
		while (dialogueTree.gameObject.activeSelf)
		{
			yield return null;
		}
	}

	private void FriendsListMenu_BattleClicked(FriendData friendData)
	{
		if (Player.instance.pvpData.HasBattleRequest(friendData.playerID))
		{
			Player.instance.pvpData.AcceptBattleRequest(friendData.playerID);
			return;
		}
		ShowPage(PageName.WaitingForFriend, false, friendData, false);
	}

	private void BattleLocalMenu_BattleClicked(FriendData friendData)
	{
		if (Player.instance.pvpData.HasBattleRequest(friendData.playerID))
		{
			Player.instance.pvpData.AcceptBattleRequest(friendData.playerID);
			return;
		}
		ShowPage(PageName.WaitingForFriend, false, friendData, true);
	}

	public void StartBattleWithPlayerRandomSettings(FriendData playerData)
	{
		BattleDefinition battleDefinition = new BattleDefinition(BattleType.PvpCpu);
		NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.MrNPC);
		battleDefinition.enemyName = nPCDefByType.name;
		battleDefinition.enemyCharacterName = CharacterName.MrNPC;
		battleDefinition.enemyGUID = nPCDefByType.guid;
		battleDefinition.cityName = CityName.JumpCity;
		battleDefinition.districtLocation = CityLocation.None;
		battleDefinition.xpReward = 25;
		battleDefinition.coinReward = 75;
		battleDefinition.opponentData = playerData;
		battleDefinition.pvpPortraitSpriteCollection = rankUpBar.portrait.Collection;
		battleDefinition.isHumanOpponent = false;
		battleDefinition.useDefaultPreBattleSound = true;
		battleDefinition.soundData = null;
		battleDefinition.countdownCharacterUID = nPCDefByType.countdownCharacterUID;
		battleDefinition.battleConditions = null;
		battleDefinition.enemyTeam = new List<Character>();
		int num = 1;
		int count = Player.instance.activeParty.Count;
		for (int i = 0; i < count; i++)
		{
			int level = Player.instance.activeParty[i].level;
			if (level > num)
			{
				num = level;
			}
		}
		List<CharacterDefinition> randomCharacters = GameManager.instance.characterDatabase.GetRandomCharacters(6, battleReady: true);
		List<Character> list = new List<Character>();
		for (int j = 0; j < 6; j++)
		{
			CharacterDefinition characterDefinition = randomCharacters[j];
			int level2 = Mathf.Clamp(num + UnityEngine.Random.Range(0, 3), 1, characterDefinition.levelTable.maxLevel);
			list.Add(new Character(CharacterRoller.RollCharacterDataWeighted(characterDefinition, 1, characterDefinition.uid, level2, GameManager.instance.legionOfDoomSettings.cpuStatDecisionWeights, GameManager.instance.legionOfDoomSettings.overLevel)));
		}
		battleDefinition.enemyActiveParty = new List<Character>(list);
		for (int k = 0; k < 3; k++)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			battleDefinition.enemyTeam.Add(list[index]);
			list.RemoveAt(index);
		}
		PreBattleController.battleDefinition = battleDefinition;
		TT2NetChat.Instance.UpdateUserStatus(6);
		LoadingScreenCover.LoadScene("PreBattleLOD");
	}

	private void CheckUnlockedPortraits()
	{
		bool flag = false;
		int level = Player.instance.pvpData.level;
		for (int i = 0; i < GameManager.instance.legionOfDoomSettings.portraitUnlocks.Count; i++)
		{
			LegionOfDoomPortraitUnlock portraitUnlock = GameManager.instance.legionOfDoomSettings.portraitUnlocks[i];
			if (Player.instance.pvpData.IsPortraitUnlocked(portraitUnlock.portraitSprite))
			{
				continue;
			}
			if ((portraitUnlock.unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.Level && level >= portraitUnlock.requiredLevel) || portraitUnlock.unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.None)
			{
				Player.instance.pvpData.UnlockPortrait(portraitUnlock.portraitSprite);
				Player.instance.pvpData.AddViewablePortrait(portraitUnlock.portraitSprite);
				if (portraitUnlock.unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.None)
				{
					Player.instance.pvpData.SetPortraitViewed(portraitUnlock.portraitSprite, isViewed: true);
				}
				flag = true;
			}
			else if (((portraitUnlock.unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.LevelAndGold && level >= portraitUnlock.requiredLevel) || portraitUnlock.unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.Gold) && Player.instance.pvpData.viewedPortraits.Find((PvpData.ViewedPortrait vp) => vp.portrait == portraitUnlock.portraitSprite) == null)
			{
				Player.instance.pvpData.AddViewablePortrait(portraitUnlock.portraitSprite);
				if (portraitUnlock.unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.Gold)
				{
					Player.instance.pvpData.SetPortraitViewed(portraitUnlock.portraitSprite, isViewed: true);
				}
				flag = true;
			}
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private void SetRankBarShown(bool show)
	{
		if (show)
		{
			if (rankUpBar.rankUpBarAnimator.IsName("LOD_Menu_RankBar_Outro"))
			{
				if (rankUpBar.rankUpBarAnimator.NormalizedTime() < 1f)
				{
					rankUpBar.rankUpBarAnimator.CrossFade("LOD_Menu_RankBar_Intro", 0.12f, -1, 1f - rankUpBar.rankUpBarAnimator.NormalizedTime());
				}
				else
				{
					rankUpBar.rankUpBarAnimator.Play("LOD_Menu_RankBar_Intro");
				}
			}
		}
		else if (rankUpBar.rankUpBarAnimator.IsName("LOD_Menu_RankBar_Intro") && rankUpBar.rankUpBarAnimator.NormalizedTime() < 1f)
		{
			rankUpBar.rankUpBarAnimator.CrossFade("LOD_Menu_RankBar_Outro", 0.12f, -1, 1f - rankUpBar.rankUpBarAnimator.NormalizedTime());
		}
		else
		{
			rankUpBar.rankUpBarAnimator.Play("LOD_Menu_RankBar_Outro");
		}
	}

	public void SetExitTVShown(bool show)
	{
		if (show)
		{
			if (exitBtnTVAnimator.IsName("LOD_Menu_Evit_TV_Exit"))
			{
				if (exitBtnTVAnimator.NormalizedTime() < 1f)
				{
					exitBtnTVAnimator.CrossFade("LOD_Menu_Evit_TV_Intro", 0.12f, -1, 1f - exitBtnTVAnimator.NormalizedTime());
				}
				else
				{
					exitBtnTVAnimator.Play("LOD_Menu_Evit_TV_Intro");
				}
			}
		}
		else if (exitBtnTVAnimator.IsName("LOD_Menu_Evit_TV_Intro") && exitBtnTVAnimator.NormalizedTime() < 1f)
		{
			exitBtnTVAnimator.CrossFade("LOD_Menu_Evit_TV_Exit", 0.12f, -1, 1f - exitBtnTVAnimator.NormalizedTime());
		}
		else
		{
			exitBtnTVAnimator.Play("LOD_Menu_Evit_TV_Exit");
		}
		GFAudioManager.instance.PlayClip(genericTVSFX, 0.75f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
	}

	private void SetTitanTowerTVShown(bool show)
	{
		if (show)
		{
			if (titanTowerTVAnimator.IsName("LOD_Menu_TitanTower_Outro"))
			{
				if (titanTowerTVAnimator.NormalizedTime() < 1f)
				{
					titanTowerTVAnimator.CrossFade("LOD_Menu_TitanTower_Intro", 0.12f, -1, 1f - titanTowerTVAnimator.NormalizedTime());
				}
				else
				{
					titanTowerTVAnimator.Play("LOD_Menu_TitanTower_Intro");
				}
			}
		}
		else if (titanTowerTVAnimator.IsName("LOD_Menu_TitanTower_Intro") && titanTowerTVAnimator.NormalizedTime() < 1f)
		{
			titanTowerTVAnimator.CrossFade("LOD_Menu_TitanTower_Outro", 0.12f, -1, 1f - titanTowerTVAnimator.NormalizedTime());
		}
		else
		{
			titanTowerTVAnimator.Play("LOD_Menu_TitanTower_Outro");
		}
		GFAudioManager.instance.PlayClip(genericTVSFX, 0.75f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
	}

	private void SetLowerLeftTVShown(bool show)
	{
		if (show)
		{
			if (wifiTVAnimator.IsName("LOD_Menu_LowerLeft_Outro"))
			{
				if (wifiTVAnimator.NormalizedTime() < 1f)
				{
					wifiTVAnimator.CrossFade("LOD_Menu_LowerLeft_Intro", 0.12f, -1, 1f - wifiTVAnimator.NormalizedTime());
				}
				else
				{
					wifiTVAnimator.Play("LOD_Menu_LowerLeft_Intro");
				}
			}
		}
		else if (wifiTVAnimator.IsName("LOD_Menu_LowerLeft_Intro") && wifiTVAnimator.NormalizedTime() < 1f)
		{
			wifiTVAnimator.CrossFade("LOD_Menu_LowerLeft_Outro", 0.12f, -1, 1f - wifiTVAnimator.NormalizedTime());
		}
		else
		{
			wifiTVAnimator.Play("LOD_Menu_LowerLeft_Outro");
		}
		GFAudioManager.instance.PlayClip(genericTVSFX, 0.75f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
	}

	public void SetLowerRightTVShown(bool show)
	{
		if (show)
		{
			if (lowerRightTVAnimator.IsName("LOD_Menu_SkillRating_Outro"))
			{
				if (lowerRightTVAnimator.NormalizedTime() < 1f)
				{
					lowerRightTVAnimator.CrossFade("LOD_Menu_SkillRating_Intro", 0.12f, -1, 1f - lowerRightTVAnimator.NormalizedTime());
				}
				else
				{
					lowerRightTVAnimator.Play("LOD_Menu_SkillRating_Intro");
				}
			}
		}
		else if (lowerRightTVAnimator.IsName("LOD_Menu_SkillRating_Intro") && lowerRightTVAnimator.NormalizedTime() < 1f)
		{
			lowerRightTVAnimator.CrossFade("LOD_Menu_SkillRating_Outro", 0.12f, -1, 1f - lowerRightTVAnimator.NormalizedTime());
		}
		else
		{
			lowerRightTVAnimator.Play("LOD_Menu_SkillRating_Outro");
		}
		GFAudioManager.instance.PlayClip(genericTVSFX, 0.75f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
	}

	public void SetUpperCenterTVShown(bool show)
	{
		if (show)
		{
			if (upperCenterTVAnimator.IsName("LOD_Menu_TopMiddleTV_Outro"))
			{
				if (upperCenterTVAnimator.NormalizedTime() < 1f)
				{
					upperCenterTVAnimator.CrossFade("LOD_Menu_TopMiddleTV_Intro", 0.12f, -1, 1f - upperCenterTVAnimator.NormalizedTime());
				}
				else
				{
					upperCenterTVAnimator.Play("LOD_Menu_TopMiddleTV_Intro");
				}
			}
		}
		else if (upperCenterTVAnimator.IsName("LOD_Menu_TopMiddleTV_Intro") && upperCenterTVAnimator.NormalizedTime() < 1f)
		{
			upperCenterTVAnimator.CrossFade("LOD_Menu_TopMiddleTV_Outro", 0.12f, -1, 1f - upperCenterTVAnimator.NormalizedTime());
		}
		else
		{
			upperCenterTVAnimator.Play("LOD_Menu_TopMiddleTV_Outro");
		}
	}

	public void SetAddFriendTVShown(bool show)
	{
		if (show)
		{
			if (addFriendTVAnimator.IsName("LOD_Menu_TitanTower_Outro"))
			{
				if (addFriendTVAnimator.NormalizedTime() < 1f)
				{
					addFriendTVAnimator.CrossFade("LOD_Menu_TitanTower_Intro", 0.12f, -1, 1f - addFriendTVAnimator.NormalizedTime());
				}
				else
				{
					addFriendTVAnimator.Play("LOD_Menu_TitanTower_Intro");
				}
			}
		}
		else if (addFriendTVAnimator.IsName("LOD_Menu_TitanTower_Intro") && addFriendTVAnimator.NormalizedTime() < 1f)
		{
			addFriendTVAnimator.CrossFade("LOD_Menu_TitanTower_Outro", 0.12f, -1, 1f - addFriendTVAnimator.NormalizedTime());
		}
		else
		{
			addFriendTVAnimator.Play("LOD_Menu_TitanTower_Outro");
		}
		GFAudioManager.instance.PlayClip(genericTVSFX, 0.75f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
	}

	private void HandleDebugLevelUpBtnClicked()
	{
		if (curPage != PageName.RankUp && curPage != PageName.Connect)
		{
			PvpData pvpData = Player.instance.pvpData;
			pvpData.currentWinStreak++;
			pvpData.coinsReceived += GameManager.instance.legionOfDoomSettings.coinRewardWin;
			int num = GameManager.instance.legionOfDoomLevelTable.GetLevelData(pvpData.level).xpToLevelUp - pvpData.currentXP;
			Player.instance.AddPvpXP(num, 0);
			LogPlayerStatus(Player.instance.pvpData, $"Gain {num} PvP XP");
			ShowPage(PageName.RankUp, false);
		}
	}

	private void HandleDebugClearFriendsListClicked()
	{
		if (friendCodeText.text != "COPIED")
		{
			UniClipboard.SetText(FriendCodeGenerator.GetFormattedCode(Player.instance.pvpData.playerID));
			StartCoroutine(ShowCopiedMessage());
		}
	}

	private IEnumerator ShowCopiedMessage()
	{
		friendCodeText.text = Language.Get("COPIED", "MENUS,GUI");
		yield return new WaitForSeconds(1f);
		friendCodeText.text = FriendCodeGenerator.GetFormattedCode(Player.instance.pvpData.playerID);
	}

	private IEnumerator ShowDialogue(Conversation conversation, bool showExitButton = true, float delay = 0f, bool showBacking = true)
	{
		if (conversation == null)
		{
			Debug.LogError("Convo is null");
			yield break;
		}
		yield return new WaitForSeconds(delay);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		dialogueTree.gameObject.SetActive(value: true);
		dialogueTree.Init(GUICamera.instance.camera, conversation, null, null, showExitButton);
		dialogueTree.backing.gameObject.SetActive(showBacking);
		while (dialogueTree != null && dialogueTree.gameObject.activeSelf)
		{
			yield return null;
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void LogPlayerStatus(PvpData pvpData, string extraIdentifier)
	{
		string text = "Player Status, " + extraIdentifier + ": \n";
		text = text + "ID: " + pvpData.playerID + "\n";
		text = text + "Name: " + pvpData.PlayerName + "\n";
		string text2 = text;
		text = text2 + "Level: " + pvpData.level + "\n";
		text2 = text;
		text = text2 + "XP: " + pvpData.currentXP + "\n";
		text2 = text;
		text = text2 + "Skill: " + pvpData.skillRating + "\n";
		text = text + "Portrait: " + pvpData.PortraitSpriteName + "\n";
		text2 = text;
		text = text2 + "Prev Level: " + pvpData.PreviousLevel + "\n";
		text2 = text;
		text = text2 + "Prev XP: " + pvpData.PreviousXP + "\n";
		text2 = text;
		text = text2 + "Base XP  Received: " + pvpData.winXPReceived + "\n";
		text2 = text;
		text = text2 + "Human Win XP Received: " + pvpData.humanWinXPReceived + "\n";
		text2 = text;
		text = text2 + "Win Streak XP Received: " + pvpData.winStreakXPReceived + "\n";
		text2 = text;
		text = text2 + "Coins Received: " + pvpData.coinsReceived + "\n";
		Debug.Log(text);
	}
}
