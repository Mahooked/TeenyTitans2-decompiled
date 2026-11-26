using System;
using TMPro;
using UnityEngine;

public class LegionOfDoomCityMenu : MonoBehaviour
{
	public enum State
	{
		MainMenu,
		SingleInvite,
		Confirmation
	}

	public TextMeshPro playerNameTextMesh;

	public TextMeshPro rankTextMesh;

	public TextMeshPro skillRatingTextMesh;

	public tk2dUIItem exitBtn;

	public tk2dUIItem exitAreaBtn;

	public tk2dUIItem enterLODBtn;

	public Animator mainMenuAnimator;

	public AnimatorEventListener mainMenuAnimatorEventListener;

	public InviteMenu inviteMenu;

	[Header("Single Invite Window")]
	public GameObject singleInviteWindowObj;

	public Animator singleInviteWindowAnimator;

	public AnimatorEventListener inviteWindowAnimatorEventListener;

	public TextMeshPro invitePlayerNameTextMesh;

	public TextMeshPro inviteDescriptionTextMesh;

	public TextMeshPro inviteSubDescriptionTextMesh;

	public tk2dUIItem acceptBtn;

	public tk2dUIItem declineBtn;

	[Header("Confirmation Window")]
	public GameObject confirmationWindowObj;

	public Animator confirmationWindowAnimator;

	public AnimatorEventListener confirmationWindowAnimatorEventListener;

	public TextMeshPro confirmationTextMesh;

	public tk2dUIItem okayBtn;

	private State curState;

	private BattleRequest battleRequest;

	private FriendRequest friendRequest;

	private bool hiding;

	public event Action EnterLODClicked;

	public event Action ExitClicked;

	public event Action Opened;

	private void OnEnable()
	{
		exitBtn.OnClick += HandleExitBtnClicked;
		exitAreaBtn.OnClick += HandleExitBtnClicked;
		enterLODBtn.OnClick += HandleEnterLODBtnClicked;
		acceptBtn.OnClick += HandleAcceptInviteClicked;
		declineBtn.OnClick += HandleDeclineInviteClicked;
		okayBtn.OnClick += HandleDismissClicked;
		mainMenuAnimatorEventListener.EventTriggered += HandleMainMenuAnimEvent;
		inviteWindowAnimatorEventListener.EventTriggered += HandleInviteWidowAnimEvent;
		confirmationWindowAnimatorEventListener.EventTriggered += HandleConfirmationAnimEvent;
	}

	private void OnDisable()
	{
		exitBtn.OnClick -= HandleExitBtnClicked;
		exitAreaBtn.OnClick -= HandleExitBtnClicked;
		enterLODBtn.OnClick -= HandleEnterLODBtnClicked;
		acceptBtn.OnClick -= HandleAcceptInviteClicked;
		declineBtn.OnClick -= HandleDeclineInviteClicked;
		okayBtn.OnClick -= HandleDismissClicked;
		mainMenuAnimatorEventListener.EventTriggered -= HandleMainMenuAnimEvent;
		inviteWindowAnimatorEventListener.EventTriggered -= HandleInviteWidowAnimEvent;
		confirmationWindowAnimatorEventListener.EventTriggered -= HandleConfirmationAnimEvent;
		hiding = false;
	}

	public void Init()
	{
		ShowMainMenu();
		if (Opened != null)
		{
			Opened();
		}
	}

	public void Hide(bool fireExitEvent)
	{
		if (gameObject.activeInHierarchy)
		{
			hiding = true;
			switch (curState)
			{
			case State.MainMenu:
				HideMainMenu();
				break;
			case State.SingleInvite:
				HideSingleInviteWindow();
				break;
			case State.Confirmation:
				HideConfirmationWindow();
				break;
			}
		}
		if (fireExitEvent && ExitClicked != null)
		{
			ExitClicked();
		}
	}

	private void HandleEnterLODBtnClicked()
	{
		if (EnterLODClicked != null)
		{
			EnterLODClicked();
		}
	}

	private void HandleExitBtnClicked()
	{
		Hide(fireExitEvent: true);
	}

	private void ShowMainMenu()
	{
		curState = State.MainMenu;
		mainMenuAnimator.gameObject.SetActive(value: true);
		mainMenuAnimator.transform.localScale = Vector3.zero;
		mainMenuAnimator.Play("communicator_emailpopup_intro");
		PvpData pvpData = Player.instance.pvpData;
		playerNameTextMesh.text = pvpData.PlayerName;
		rankTextMesh.text = $"Rank {pvpData.level}";
		skillRatingTextMesh.text = pvpData.skillRating.ToString();
		inviteMenu.Init();
	}

	private void HideMainMenu()
	{
		mainMenuAnimator.Play("communicator_emailpopup_Outro");
	}

	private void HandleMainMenuAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroFinished")
		{
			gameObject.SetActive(value: false);
		}
	}

	private void ShowSingleInviteWindow()
	{
		curState = State.SingleInvite;
		singleInviteWindowObj.SetActive(value: true);
		singleInviteWindowAnimator.transform.localScale = Vector3.zero;
		if (battleRequest == null)
		{
			battleRequest = new BattleRequest();
		}
		if (friendRequest == null)
		{
			friendRequest = new FriendRequest();
		}
		battleRequest.playerId = string.Empty;
		friendRequest.playerId = string.Empty;
		if (Player.instance.pvpData.BattleRequests.Count > 0)
		{
			battleRequest = Player.instance.pvpData.BattleRequests[0];
		}
		else
		{
			friendRequest = Player.instance.pvpData.friendRequests.Find((FriendRequest x) => x.status == FriendRequest.Status.Pending);
		}
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			Debug.Log("Set Battle Request...");
			invitePlayerNameTextMesh.text = Player.instance.pvpData.GetFriend(battleRequest.playerId).name;
			inviteDescriptionTextMesh.text = Language.Get("WANTSTOBATTLE_SHORT", "MENUS,GUI");
			inviteSubDescriptionTextMesh.text = Language.Get("OHSNAP", "MENUS,GUI");
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			Debug.Log("Set Friend Request...");
			invitePlayerNameTextMesh.text = friendRequest.playerName;
			inviteDescriptionTextMesh.text = Language.Get("WANTSTOBEFRIENDS_SHORT", "MENUS,GUI");
			inviteSubDescriptionTextMesh.text = Language.Get("AWW", "MENUS,GUI");
		}
	}

	private void HideSingleInviteWindow()
	{
		singleInviteWindowAnimator.Play("communicator_emailpopup_Outro");
	}

	private void HandleInviteWidowAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroFinished")
		{
			singleInviteWindowObj.SetActive(value: false);
			if (hiding)
			{
				Debug.Log("Disable main gameobject");
				gameObject.SetActive(value: false);
			}
			else if (curState == State.Confirmation)
			{
				Debug.Log("Cur State is Confirmation...");
				ShowConfirmationWindow();
			}
			else
			{
				Debug.Log("Invite Window Anim Event, Show Main Menu");
				ShowMainMenu();
			}
		}
	}

	private void HandleAcceptInviteClicked()
	{
		Debug.Log("Accept Invite 1, friend request exists? " + (friendRequest != null));
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			Debug.Log("Accept Invite 2a");
			Player.instance.pvpData.AcceptBattleRequest(battleRequest.playerId);
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			Player.instance.pvpData.AcceptFriendRequest(friendRequest.playerId);
			Debug.Log("Accept Invite 2b");
			curState = State.Confirmation;
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

	private void ShowConfirmationWindow()
	{
		Debug.Log("Show Confirmation Window...");
		curState = State.Confirmation;
		confirmationWindowObj.SetActive(value: true);
		confirmationWindowAnimator.transform.localScale = Vector3.zero;
		confirmationTextMesh.text = Language.Get("NEWFRIENDADDED", "MENUS,GUI");
	}

	private void HideConfirmationWindow()
	{
		confirmationWindowAnimator.Play("communicator_emailpopup_Outro");
	}

	private void HandleConfirmationAnimEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "outroFinished")
		{
			confirmationWindowObj.SetActive(value: false);
			if (hiding)
			{
				gameObject.SetActive(value: false);
			}
			else
			{
				ShowMainMenu();
			}
		}
	}

	private void HandleDismissClicked()
	{
		HideConfirmationWindow();
	}
}
