using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LODRequestFlag : MonoBehaviour
{
	private enum ConfirmationMessageType
	{
		AcceptedFriend = 0,
		ReturnToCity = 1,
		TournamentExit = 2
	}

	private static LODRequestFlag instance;

	public GameObject battleInviteObj;

	public GameObject friendRequestObj;

	public tk2dUIItem uiItem;

	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public tk2dSprite blackBg;

	[Header("Single Invite Screen")]
	public GameObject singleInviteObj;

	public Animator singleInviteAnimator;

	public AnimatorEventListener singleInviteAnimEventListener;

	public TextMeshPro playerNameTextMesh;

	public TextMeshPro descriptionTextMesh;

	public TextMeshPro subDescriptionTextMesh;

	public tk2dUIItem acceptBtn;

	public tk2dUIItem declineBtn;

	[Header("Confirmation Window")]
	public GameObject confirmationWindowObj;

	public Animator confirmationWindowAnimator;

	public AnimatorEventListener confirmationAnimEventListener;

	public TextMeshPro confirmationTextMesh;

	public tk2dUIItem okayBtn;

	public tk2dUIItem loseProgressAcceptBtn;

	public tk2dUIItem loseProgressDeclineBtn;

	private Coroutine delayShowRoutine;

	private BattleRequest battleRequest;

	private FriendRequest friendRequest;

	private bool singleInviteScreenShown;

	private bool initiallyAcceptedBattleInv;

	public static bool isInviteScreenActive
	{
		get
		{
			return instance != null && instance.singleInviteScreenShown;
		}
	}

	private void OnEnable()
	{
		uiItem.enabled = true;
		uiItem.OnClick += HandleFlagClicked;
		animatorEventListener.EventTriggered += HandleAnimEvent;
		acceptBtn.OnClick += HandleAcceptInviteClicked;
		declineBtn.OnClick += HandleDeclineInviteClicked;
		okayBtn.OnClick += HandleOkayClicked;
		loseProgressAcceptBtn.OnClick += HandleLoseProgressAcceptClicked;
		loseProgressDeclineBtn.OnClick += HandleLoseProgressDeclineClicked;
		singleInviteAnimEventListener.EventTriggered += HandleInviteWidowAnimEvent;
		confirmationAnimEventListener.EventTriggered += HandleConfirmationAnimEvent;
		if (CityController.instance != null && Communicator.instance != null)
		{
			CityController.instance.pauseScreen.TitleClicked += HandleExitToTitleScreen;
			Communicator.instance.lodCityMenu.Opened += HandleLODCityMenuOpened;
		}
		blackBg.color = Color.clear;
		blackBg.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		uiItem.OnClick -= HandleFlagClicked;
		animatorEventListener.EventTriggered -= HandleAnimEvent;
		acceptBtn.OnClick -= HandleAcceptInviteClicked;
		declineBtn.OnClick -= HandleDeclineInviteClicked;
		okayBtn.OnClick -= HandleOkayClicked;
		loseProgressAcceptBtn.OnClick -= HandleLoseProgressAcceptClicked;
		loseProgressDeclineBtn.OnClick -= HandleLoseProgressDeclineClicked;
		singleInviteAnimEventListener.EventTriggered -= HandleInviteWidowAnimEvent;
		confirmationAnimEventListener.EventTriggered -= HandleConfirmationAnimEvent;
		if (CityController.instance != null && Communicator.instance != null)
		{
			CityController.instance.pauseScreen.TitleClicked -= HandleExitToTitleScreen;
			Communicator.instance.lodCityMenu.Opened -= HandleLODCityMenuOpened;
		}
	}

	public static void ShowInvite(BattleRequest battleRequest)
	{
		if (CanShow())
		{
			if (instance == null)
			{
				instance = ObjectPool.GetObjectFromResource<LODRequestFlag>("Prefabs/GUI/LODRequestFlag");
			}
			instance.ShowInvite_Inst(battleRequest, null);
		}
	}

	public static void ShowInvite(FriendRequest friendRequest)
	{
		if (CanShow())
		{
			if (instance == null)
			{
				instance = ObjectPool.GetObjectFromResource<LODRequestFlag>("Prefabs/GUI/LODRequestFlag");
			}
			instance.ShowInvite_Inst(null, friendRequest);
		}
	}

	private void ShowInvite_Inst(BattleRequest newBattleRequest, FriendRequest newFriendRequest)
	{
		singleInviteScreenShown = false;
		initiallyAcceptedBattleInv = false;
		battleRequest = newBattleRequest;
		friendRequest = newFriendRequest;
		friendRequestObj.SetActive(friendRequest != null);
		battleInviteObj.SetActive(battleRequest != null);
		animator.Play("LOD_RequestFlag_anim", 0, 0f);
	}

	private void Hide()
	{
		animator.Play((!animator.IsName("LOD_RequestFlag_anim")) ? "LOD_RequestFlag_TabExit" : "LOD_RequestFlag_ExitFull");
		uiItem.enabled = false;
	}

	private void HandleAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroFinished" && !singleInviteScreenShown)
		{
			PoolSelf();
		}
	}

	private void HandleFlagClicked()
	{
		if (CityController.instance != null && Communicator.instance != null && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy))
		{
			if (CityController.instance.pauseScreen.gameObject.activeInHierarchy)
			{
				CityController.instance.pauseScreen.HandleOnPlayClick();
			}
			Communicator.instance.ForceShowLegionOfDoom();
			return;
		}
		if (BattleController.instance != null)
		{
			PauseManager.instance.Pause();
		}
		if (IsGameStateSafe())
		{
			ShowSingleInviteWindow();
		}
		else
		{
			ShowConfirmationWindow(ConfirmationMessageType.ReturnToCity);
		}
	}

	private void HandleLODCityMenuOpened()
	{
		Hide();
	}

	private void HandleExitToTitleScreen()
	{
		Hide();
	}

	private void ShowSingleInviteWindow()
	{
		singleInviteScreenShown = true;
		singleInviteObj.SetActive(true);
		singleInviteAnimator.transform.localScale = Vector3.zero;
		blackBg.gameObject.SetActive(true);
		DOTween.Kill(blackBg);
		DOTween.ToAlpha(() => blackBg.color, (Color x) =>
		{
			blackBg.color = x;
		}, 0.5f, 0.33f).SetTarget(blackBg).SetUpdate(true);
		Hide();
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			Debug.Log("Set Battle Request...");
			playerNameTextMesh.text = Player.instance.pvpData.GetFriend(battleRequest.playerId).name;
			descriptionTextMesh.text = Language.Get("WANTSTOBATTLE_SHORT", "MENUS,GUI");
			subDescriptionTextMesh.text = Language.Get("OHSNAP", "MENUS,GUI");
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			Debug.Log("Set Friend Request...");
			playerNameTextMesh.text = friendRequest.playerName;
			descriptionTextMesh.text = Language.Get("WANTSTOBEFRIENDS_SHORT", "MENUS,GUI");
			subDescriptionTextMesh.text = Language.Get("AWW", "MENUS,GUI");
		}
	}

	private void HideSingleInviteWindow()
	{
		if (friendRequest == null || string.IsNullOrEmpty(friendRequest.playerId) || friendRequest.status == FriendRequest.Status.Declined)
		{
			blackBg.gameObject.SetActive(true);
			DOTween.Kill(blackBg);
			DOTween.ToAlpha(() => blackBg.color, (Color x) =>
			{
				blackBg.color = x;
			}, 0f, 0.15f).SetTarget(blackBg).SetUpdate(true);
		}
		singleInviteAnimator.Play("communicator_emailpopup_Outro");
	}

	private void HandleInviteWidowAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroFinished")
		{
			singleInviteObj.SetActive(false);
			if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId) && friendRequest.status == FriendRequest.Status.Accepted)
			{
				ShowConfirmationWindow(ConfirmationMessageType.AcceptedFriend);
			}
			else if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId) && initiallyAcceptedBattleInv && TournamentTracker.isTournamentActive)
			{
				ShowConfirmationWindow(ConfirmationMessageType.TournamentExit);
			}
			else
			{
				PoolSelf();
			}
		}
	}

	private void HandleAcceptInviteClicked()
	{
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			if (!TournamentTracker.isTournamentActive)
			{
				Player.instance.pvpData.AcceptBattleRequest(battleRequest.playerId);
			}
			else
			{
				initiallyAcceptedBattleInv = true;
			}
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			Player.instance.pvpData.AcceptFriendRequest(friendRequest.playerId);
		}
		HideSingleInviteWindow();
	}

	private void HandleDeclineInviteClicked()
	{
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			Player.instance.pvpData.DeclineBattleRequest(battleRequest.playerId);
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			Player.instance.pvpData.DeclineFriendRequest(friendRequest.playerId);
		}
		HideSingleInviteWindow();
	}

	private void ShowConfirmationWindow(ConfirmationMessageType confirmationMessageType)
	{
		confirmationWindowObj.SetActive(true);
		confirmationWindowAnimator.transform.localScale = Vector3.zero;
		string text;
		switch (confirmationMessageType)
		{
		case ConfirmationMessageType.ReturnToCity:
			singleInviteScreenShown = true;
			blackBg.gameObject.SetActive(true);
			DOTween.Kill(blackBg);
			DOTween.ToAlpha(() => blackBg.color, (Color x) =>
			{
				blackBg.color = x;
			}, 0.5f, 0.33f).SetTarget(blackBg).SetUpdate(true);
			Hide();
			okayBtn.gameObject.SetActive(true);
			loseProgressAcceptBtn.gameObject.SetActive(false);
			loseProgressDeclineBtn.gameObject.SetActive(false);
			text = Language.Get("REVIEWINVITES", "MENUS,GUI");
			break;
		case ConfirmationMessageType.TournamentExit:
			blackBg.gameObject.SetActive(true);
			DOTween.Kill(blackBg);
			DOTween.ToAlpha(() => blackBg.color, (Color x) =>
			{
				blackBg.color = x;
			}, 0.5f, 0.33f).SetTarget(blackBg).SetUpdate(true);
			okayBtn.gameObject.SetActive(false);
			loseProgressAcceptBtn.gameObject.SetActive(true);
			loseProgressDeclineBtn.gameObject.SetActive(true);
			text = Language.Get((confirmationMessageType != ConfirmationMessageType.TournamentExit) ? string.Empty : "TOURNYEXITWARNING", "MENUS,GUI");
			break;
		case ConfirmationMessageType.AcceptedFriend:
			okayBtn.gameObject.SetActive(true);
			loseProgressAcceptBtn.gameObject.SetActive(false);
			loseProgressDeclineBtn.gameObject.SetActive(false);
			text = Language.Get("NEWFRIENDADDED", "MENUS,GUI");
			break;
		default:
			throw new ArgumentOutOfRangeException("confirmationMessageType", confirmationMessageType, null);
		}
		confirmationTextMesh.text = text;
	}

	private void HideConfirmationWindow()
	{
		blackBg.gameObject.SetActive(true);
		DOTween.Kill(blackBg);
		DOTween.ToAlpha(() => blackBg.color, (Color x) =>
		{
			blackBg.color = x;
		}, 0f, 0.15f).SetTarget(blackBg).SetUpdate(true);
		confirmationWindowAnimator.Play("communicator_emailpopup_Outro");
	}

	private void HandleConfirmationAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroFinished")
		{
			confirmationWindowObj.SetActive(false);
			PoolSelf();
		}
	}

	private void HandleOkayClicked()
	{
		HideConfirmationWindow();
	}

	private void HandleLoseProgressAcceptClicked()
	{
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId) && initiallyAcceptedBattleInv)
		{
			Player.instance.pvpData.AcceptBattleRequest(battleRequest.playerId);
		}
		HideConfirmationWindow();
	}

	private void HandleLoseProgressDeclineClicked()
	{
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			Player.instance.pvpData.DeclineBattleRequest(battleRequest.playerId);
		}
		HideConfirmationWindow();
	}

	private void PoolSelf()
	{
		singleInviteScreenShown = false;
		ObjectPool.PoolObject(base.gameObject);
		instance = null;
	}

	private static bool CanShow()
	{
		return LoadingScreenCover.allowInit && LegionOfDoomController.instance == null && ((CityController.instance != null && Communicator.instance != null && !Communicator.instance.lodCityMenu.gameObject.activeInHierarchy && (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive)) || (BattleController.instance != null && BattleController.battleDefinition != null && BattleController.battleDefinition.type != BattleType.Pvp && BattleController.battleDefinition.type != BattleType.PvpCpu && BattleController.battleDefinition.type != BattleType.PvpFriend) || (PreBattleController.Instance != null && PreBattleController.battleDefinition != null && PreBattleController.battleDefinition.type != BattleType.Pvp && PreBattleController.battleDefinition.type != BattleType.PvpCpu && PreBattleController.battleDefinition.type != BattleType.PvpFriend) || TournamentController.instance != null || TitanTowerController.instance != null || ShopController.instance != null || RepaintController.instance != null);
	}

	private bool IsGameStateSafe()
	{
		return (TutorialManager.instance == null || TutorialManager.instance.activeTutorial == null) && ((CityController.instance != null && !CityScriptedSequence.scriptedSequenceActive && !GameUI.instance.showingCinemaBars && !CityReward.instance.showingRewards) || (BattleController.instance != null && BattleController.battleDefinition != null && (BattleController.battleDefinition.type == BattleType.Normal || BattleController.battleDefinition.type == BattleType.Tournament)) || (PreBattleController.Instance != null && PreBattleController.battleDefinition != null && (PreBattleController.battleDefinition.type == BattleType.Normal || PreBattleController.battleDefinition.type == BattleType.Tournament)) || TournamentController.instance != null || (TitanTowerController.instance != null && !TitanTowerController.instance.combineScreen.rankUpSequenceActive) || ShopController.instance != null || RepaintController.instance != null);
	}
}
