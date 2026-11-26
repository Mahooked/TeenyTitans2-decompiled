using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DailyMissionProgressMenu : MonoBehaviour
{
	[Serializable]
	public class DailyMissionBox
	{
		public Animator animator;

		public TextMeshPro costTextMesh;

		public TextMeshPro dayTextMesh;

		public string incompleteAnimation;

		public string completeAnimation;

		public string activateAnimation;
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Closed__BackingField;

	public Animator animator;

	public Animator bgAnimator;

	public AnimatorEventListener animatorEventListener;

	public tk2dUIItem exitButton;

	public DailyMissionBox[] dailyMissionBoxes;

	[Header("Audio")]
	public AudioClip openSFX;

	public AudioClip closeSFX;

	public AudioClip checkmarkSFX;

	public AudioClip rewardSFX;

	[Header("DEBUG")]
	public tk2dUIItem debugNextAwardBtn;

	public tk2dUIItem debugResetAwardsBtn;

	private tk2dBaseSprite[] bgSprites;

	private tk2dAnimationAdapter exitSprite;

	private bool openedAsSubmenu;

	private static DailyMissionProgressMenu _instance;

	private bool inDebugMode { get; set; }

	private int debugDailyMissionsCompleted { get; set; }

	public static DailyMissionProgressMenu instance
	{
		get
		{
			return _instance;
		}
		private set
		{
			_instance = value;
		}
	}

	public event Action Closed
	{
		add
		{
			Action action = Closed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Closed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Closed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Closed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void OnEnable()
	{
		animator.Play("DailyMissionProgress_Outro", 0, 1f);
		animator.Update(0f);
	}

	private void Start()
	{
		Player.instance.DailyMissionWeeklyProgress += HandleWeeklyProgressMade;
		DailyMissionManager dailyMissionManager = DailyMissionManager.instance;
		dailyMissionManager.DailyMissionGenerated = (Action)Delegate.Combine(dailyMissionManager.DailyMissionGenerated, new Action(HandleDailyMissionGenerated));
		exitButton.OnClick += HandleExitClicked;
		exitSprite = exitButton.GetComponentInChildren<tk2dAnimationAdapter>();
		bgSprites = bgAnimator.GetComponentsInChildren<tk2dBaseSprite>();
		animator.Play("DailyMissionProgress_Outro", 0, 1f);
		animatorEventListener.EventTriggered += HandleAnimationEvent;
		animator.Update(0f);
		for (int i = 0; i < bgSprites.Length; i++)
		{
			bgSprites[i].color = new Color(1f, 1f, 1f, 0f);
		}
		debugNextAwardBtn.OnClick += HandleDebugNextAward;
		debugResetAwardsBtn.OnClick += HandleDebugResetAwards;
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.DailyMissionWeeklyProgress -= HandleWeeklyProgressMade;
		}
		if (DailyMissionManager.instance != null)
		{
			DailyMissionManager dailyMissionManager = DailyMissionManager.instance;
			dailyMissionManager.DailyMissionGenerated = (Action)Delegate.Remove(dailyMissionManager.DailyMissionGenerated, new Action(HandleDailyMissionGenerated));
		}
		animatorEventListener.EventTriggered -= HandleAnimationEvent;
		debugNextAwardBtn.OnClick -= HandleDebugNextAward;
		debugResetAwardsBtn.OnClick -= HandleDebugResetAwards;
		instance = null;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Keypad0))
		{
			HandleWeeklyProgressMade(0);
		}
	}

	private void HandleExitClicked()
	{
		Hide();
	}

	private void HandleWeeklyProgressMade(int completedMissionUID)
	{
		StartCoroutine(WaitUntilInCitySequence());
	}

	private void HandleDailyMissionGenerated()
	{
		if (animator.IsName("DailyMissionProgress_Intro") && !animator.IsPlaying() && exitButton.enabled)
		{
			UnityEngine.Debug.Log("Show the Daily Mission Progress Menu");
			StartCoroutine(ShowSequence(false));
		}
	}

	private IEnumerator WaitUntilInCitySequence()
	{
		while (CityController.instance == null)
		{
			yield return null;
		}
		Show(false, true);
	}

	public void Show(bool openAsSubmenu, bool revealLatest = false)
	{
		openedAsSubmenu = openAsSubmenu;
		if (!openedAsSubmenu)
		{
			CityController.instance.cityCharacter.AllowInput = false;
			PauseManager.instance.isEnabled = false;
			Communicator.instance.pauseTime = true;
			Communicator.instance.SetCommunicatorShown(false);
			CityController.instance.SetMagnifyingGlassActive(false);
			CityController.instance.SetTitanTowerBtnActive(false);
			CityController.instance.mySquad.SetInteractive(false);
		}
		StartCoroutine(ShowSequence(revealLatest));
	}

	private IEnumerator ShowSequence(bool revealLatest)
	{
		UnityEngine.Debug.Log("Show Progress Menu, 1");
		while ((TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive) || (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy) || CityController.instance.communicator.MissionScrollerActive() || CityController.instance.pauseScreen.gameObject.activeSelf || GameUI.instance.showingCinemaBars || CityReward.instance.showingRewards)
		{
			yield return null;
		}
		if (!openedAsSubmenu)
		{
			Communicator.instance.SetCommunicatorShown(false);
		}
		yield return null;
		UnityEngine.Debug.Log("Show Progress Menu, 2");
		animator.Play("DailyMissionProgress_Intro");
		GFAudioManager.instance.PlayClip(openSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		for (int i = 0; i < bgSprites.Length; i++)
		{
			DOTween.Kill(bgSprites[i]);
			int closedIndex = i;
			bgSprites[closedIndex].color = new Color(1f, 1f, 1f, 0f);
			DOTween.ToAlpha(() => bgSprites[closedIndex].color, (Color newColor) =>
			{
				bgSprites[closedIndex].color = newColor;
			}, 1f, 0.1625f).SetEase(Ease.Linear).SetTarget(bgSprites[closedIndex]);
		}
		yield return null;
		int missionIndex = 0;
		int completedMissionCount = ((!inDebugMode) ? (Player.instance.playerStats.numDailyMissionsCompleted % 5) : debugDailyMissionsCompleted);
		if (!inDebugMode)
		{
			if (revealLatest && completedMissionCount == 0)
			{
				completedMissionCount = 5;
			}
			else if (!revealLatest && completedMissionCount == 0 && Player.instance.lastCompletedDailyMission != null && Player.instance.currentDailyMission.startDateTime.Date == Player.instance.lastCompletedDailyMission.startDateTime.Date)
			{
				completedMissionCount = 5;
			}
		}
		for (int num = 0; num < dailyMissionBoxes.Length; num++)
		{
			DailyMissionBox dailyMissionBox = dailyMissionBoxes[num];
			if (missionIndex < completedMissionCount)
			{
				if (revealLatest && missionIndex == completedMissionCount - 1)
				{
					dailyMissionBox.animator.Play(dailyMissionBox.incompleteAnimation);
				}
				else
				{
					dailyMissionBox.animator.Play(dailyMissionBox.completeAnimation);
				}
				missionIndex++;
			}
			else
			{
				dailyMissionBox.animator.Play(dailyMissionBox.incompleteAnimation);
			}
			if (dailyMissionBox.costTextMesh != null)
			{
				dailyMissionBox.costTextMesh.text = ((GameManager.instance.dailyMissionRewards.rewards.Length <= num) ? "0" : GameManager.instance.dailyMissionRewards.rewards[num].rewardAmount.ToString());
			}
			dailyMissionBox.dayTextMesh.text = string.Format(Language.Get("DAILY_MISSION_DAY", "MENUS,GUI"), num + 1);
		}
		if (revealLatest && completedMissionCount > 0)
		{
			exitButton.enabled = false;
			exitSprite.color = Color.gray;
			while (animator.IsPlaying())
			{
				yield return null;
			}
			DailyMissionBox revealBox = dailyMissionBoxes[missionIndex - 1];
			revealBox.animator.Play(revealBox.activateAnimation);
			GFAudioManager.instance.PlayClip(checkmarkSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			yield return null;
			while (revealBox.animator.IsPlaying())
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.5f);
			GFAudioManager.instance.PlayClip(rewardSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			DailyMissionRewards.DailyMissionReward reward = GameManager.instance.dailyMissionRewards.rewards[missionIndex - 1];
			if (reward.rewardType == DailyMissionRewards.DailyMissionRewardType.RainbowCoins)
			{
				PlayerReward playerReward = new PlayerReward();
				playerReward.currencyType = CurrencyType.RainbowCoins;
				playerReward.amount = reward.rewardAmount;
				playerReward.type = PlayerRewardType.Currency;
				PlayerReward reward2 = playerReward;
				CityRewardReveal.Show(reward2);
			}
			else
			{
				RiddleEggReveal.Show(GameManager.instance.iapConfig.GenerateRiddleEggReward(RiddleEggType.Green));
				AchievementsDatabase.UnlockAchievementId("TT2_DAILYPLANIT");
			}
			exitButton.enabled = true;
			exitSprite.color = Color.white;
		}
	}

	private void Hide()
	{
		UnityEngine.Debug.Log("Hide Progress Menu");
		animator.Play("DailyMissionProgress_Outro");
		GFAudioManager.instance.PlayClip(closeSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		for (int i = 0; i < bgSprites.Length; i++)
		{
			DOTween.Kill(bgSprites[i]);
			int closedIndex = i;
			DOTween.ToAlpha(() => bgSprites[closedIndex].color, (Color newColor) =>
			{
				bgSprites[closedIndex].color = newColor;
			}, 0f, 0.1625f).SetDelay(0.55f).SetTarget(bgSprites[closedIndex]);
		}
		if (!openedAsSubmenu)
		{
			CityController.instance.cityCharacter.AllowInput = true;
			PauseManager.instance.isEnabled = true;
			Communicator.instance.pauseTime = false;
			Communicator.instance.SetCommunicatorShown(true);
			CityController.instance.SetMagnifyingGlassActive(true);
			CityController.instance.SetTitanTowerBtnActive(true);
			CityController.instance.mySquad.SetInteractive(true);
		}
	}

	private void HandleAnimationEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "Closed" && Closed__BackingField != null)
		{
			Closed__BackingField();
		}
	}

	private void HandleDebugNextAward()
	{
		inDebugMode = true;
		debugDailyMissionsCompleted = ((debugDailyMissionsCompleted < GameManager.instance.dailyMissionRewards.rewards.Length) ? (debugDailyMissionsCompleted + 1) : 0);
		Hide();
		Show(false, true);
	}

	private void HandleDebugResetAwards()
	{
		inDebugMode = false;
		debugDailyMissionsCompleted = 0;
		Hide();
		Show(false);
	}
}
