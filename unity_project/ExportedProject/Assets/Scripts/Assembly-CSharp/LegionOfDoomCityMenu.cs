using System;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class LegionOfDoomCityMenu : MonoBehaviour
{
	public enum State
	{
		MainMenu = 0,
		SingleInvite = 1,
		Confirmation = 2
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action EnterLODClicked__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ExitClicked__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Opened__BackingField;

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

	public event Action EnterLODClicked
	{
		add
		{
			Action action = EnterLODClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref EnterLODClicked__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = EnterLODClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref EnterLODClicked__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ExitClicked
	{
		add
		{
			Action action = ExitClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ExitClicked__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ExitClicked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ExitClicked__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Opened
	{
		add
		{
			Action action = Opened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Opened__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Opened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Opened__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
		if (Opened__BackingField != null)
		{
			Opened__BackingField();
		}
	}

	public void Hide(bool fireExitEvent)
	{
		if (base.gameObject.activeInHierarchy)
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
		if (fireExitEvent && ExitClicked__BackingField != null)
		{
			ExitClicked__BackingField();
		}
	}

	private void HandleEnterLODBtnClicked()
	{
		if (EnterLODClicked__BackingField != null)
		{
			EnterLODClicked__BackingField();
		}
	}

	private void HandleExitBtnClicked()
	{
		Hide(true);
	}

	private void ShowMainMenu()
	{
		curState = State.MainMenu;
		mainMenuAnimator.gameObject.SetActive(true);
		mainMenuAnimator.transform.localScale = Vector3.zero;
		mainMenuAnimator.Play("communicator_emailpopup_intro");
		PvpData pvpData = Player.instance.pvpData;
		playerNameTextMesh.text = pvpData.PlayerName;
		rankTextMesh.text = string.Format("Rank {0}", pvpData.level);
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
			base.gameObject.SetActive(false);
		}
	}

	private void ShowSingleInviteWindow()
	{
		curState = State.SingleInvite;
		singleInviteWindowObj.SetActive(true);
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
			UnityEngine.Debug.Log("Set Battle Request...");
			invitePlayerNameTextMesh.text = Player.instance.pvpData.GetFriend(battleRequest.playerId).name;
			inviteDescriptionTextMesh.text = Language.Get("WANTSTOBATTLE_SHORT", "MENUS,GUI");
			inviteSubDescriptionTextMesh.text = Language.Get("OHSNAP", "MENUS,GUI");
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			UnityEngine.Debug.Log("Set Friend Request...");
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
			singleInviteWindowObj.SetActive(false);
			if (hiding)
			{
				UnityEngine.Debug.Log("Disable main gameobject");
				base.gameObject.SetActive(false);
			}
			else if (curState == State.Confirmation)
			{
				UnityEngine.Debug.Log("Cur State is Confirmation...");
				ShowConfirmationWindow();
			}
			else
			{
				UnityEngine.Debug.Log("Invite Window Anim Event, Show Main Menu");
				ShowMainMenu();
			}
		}
	}

	private void HandleAcceptInviteClicked()
	{
		UnityEngine.Debug.Log("Accept Invite 1, friend request exists? " + (friendRequest != null));
		if (battleRequest != null && !string.IsNullOrEmpty(battleRequest.playerId))
		{
			UnityEngine.Debug.Log("Accept Invite 2a");
			Player.instance.pvpData.AcceptBattleRequest(battleRequest.playerId);
		}
		else if (friendRequest != null && !string.IsNullOrEmpty(friendRequest.playerId))
		{
			Player.instance.pvpData.AcceptFriendRequest(friendRequest.playerId);
			UnityEngine.Debug.Log("Accept Invite 2b");
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
		UnityEngine.Debug.Log("Show Confirmation Window...");
		curState = State.Confirmation;
		confirmationWindowObj.SetActive(true);
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
			confirmationWindowObj.SetActive(false);
			if (hiding)
			{
				base.gameObject.SetActive(false);
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
