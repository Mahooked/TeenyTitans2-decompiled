using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

public class Communicator : MonoBehaviour
{
	public enum ViewType
	{
		DayNight = 0,
		Mail = 1,
		Missions = 2,
		FlashSale = 3,
		LOD = 4
	}

	public static string afterBattleConvoName;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action StartDayTime__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action StartNightTime__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<NewsFlash> NewsFlashStarted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<NewsFlash> NewsFlashEnded__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action MissionScrollerWillAppear__BackingField;

	public float fullCycleRealSeconds = 480f;

	public bool allowTimePassage = true;

	[HideInInspector]
	public bool isDayTime = true;

	[Header("Debug Options")]
	public bool debugDayTime;

	public int debugDay;

	public int debugTime;

	public int[] debugCompleteMissions;

	[Header("Objects")]
	public Transform dayNightCycle;

	public GameObject dayNight;

	public GameObject mail;

	public GameObject mailPopUp;

	public GameObject mailBadge;

	public GameObject missions;

	public GameObject missionsPopUp;

	public GameObject missionTicker;

	public GameObject newsTicker;

	public GameObject sideMissionHeader;

	public GameObject legionOfDoom;

	public GameObject lodPopup;

	[Header("Colliders")]
	public BoxCollider2D newsFlashBtnCollider;

	public BoxCollider2D missionsBtnCollider;

	public BoxCollider2D mailBtnCollider;

	public BoxCollider2D antennaBtnCollider;

	[Header("Sprites")]
	public tk2dSprite leftBtnSprite;

	public tk2dSprite rightBtnSprite;

	public tk2dSprite antennaLight;

	public tk2dSprite antennaTail;

	[Header("TextMeshPro")]
	public TextMeshPro missionsText;

	public TextMeshPro missionsNumber;

	public TextMeshPro mailNumber;

	public TextMeshPro debugDailyMissionsText;

	[Header("Animators")]
	public Animator animator;

	public Animator missionsPopUpAnimator;

	public Animator mailPopUpAnimator;

	public Animator missionTickerAnimator;

	public Animator newsTickerAnimator;

	[Header("Scroll Areas")]
	public tk2dUIScrollableArea missionsScroller;

	public tk2dUIScrollableArea mailScroller;

	[Header("Mail PopUp")]
	public TextMeshPro mailPopUpTitle;

	public TextMeshPro mailPopUpDescr;

	public TextMeshPro mailPopUpSignature;

	public TextMeshPro mailPopUpNewMsgs;

	[Header("Mission PopUp")]
	public tk2dUIScrollbar missionScrollbar;

	public CommunicatorMission storyMission;

	public GameObject sideMissionPrototype;

	public GameObject sideMissionDailyPrototype;

	public GameObject sideMissionDailyBanner;

	private List<CommunicatorMission> sideMissions = new List<CommunicatorMission>();

	[Header("Mission Ticker PopUp")]
	public TextMeshPro missionTickerHeader;

	public TextMeshPro missionTickerType;

	public TextMeshPro missionTickerType2;

	public TextMeshPro missionTickerNumber;

	public TextMeshPro missionTickerDescText;

	public tk2dSprite missionTickerPin;

	public tk2dSprite missionTickerVersusIcon;

	public GameObject missionTickerVersus;

	public GameObject missionTickerCollect1;

	public GameObject missionTickerCollect2;

	public GameObject missionTickerCollect3;

	public GameObject missionTickerCollect4;

	public GameObject missionTickerCollect5;

	[Header("News Ticker PopUp")]
	public TextMeshPro newsTickerHeader;

	public TextMeshPro newsTickerDescription;

	public Marquee newsTickerMarquee;

	[Header("LOD Pop Up")]
	public LegionOfDoomCityMenu lodCityMenu;

	public TextMeshPro inviteCountTextMesh;

	public TextMeshPro multiplayerTextMesh;

	[Space]
	public GameObject lowerRightAnchor;

	[Header("Flash Sale")]
	public TextMeshPro flashSaleTimerText;

	public Animator flashSaleTimerAnimator;

	private float flashSaleTimer;

	private bool inFlashSaleMode;

	private int flashSaleTimerPrevSeconds;

	private bool addedMailMission;

	private int addedMailMissionUid = -1;

	private MissionsScrollerController missionsScrollerController;

	[Header("Properties")]
	public float deltaRotation = 0.01f;

	public float elapsedTime;

	public int currentDay;

	public int currentTime;

	public int previousTime;

	public int previousDay;

	private Dictionary<CityName, int> daysPassedInCity = new Dictionary<CityName, int>();

	private List<int> currentMail = new List<int>();

	private List<GameObject> currentNewsFlashSpriteObjs = new List<GameObject>();

	private List<MailMessage> mailMessagesForDay = new List<MailMessage>();

	public NewsFlash activeNewsFlash;

	public NewsFlash prevActiveNewsFlash;

	private List<NewsFlash> currentDayPossibleNewsFlashes = new List<NewsFlash>();

	private List<NewsFlash> newsFlashesShownToday = new List<NewsFlash>();

	private List<NewsFlash> queuedNewsFlashes = new List<NewsFlash>();

	private ViewType activeViewType;

	private ViewType prevViewType;

	private bool _pauseTime;

	private CityController _cityController;

	private int currentMailIndex;

	public List<string> convosPlayed = new List<string>();

	private bool waitingForNewsFlash;

	private bool debugDailyMissions;

	public List<CommunicatorMission> SideMissions
	{
		get
		{
			return sideMissions;
		}
	}

	public bool pauseTime
	{
		get
		{
			return _pauseTime;
		}
		set
		{
			_pauseTime = value;
		}
	}

	private CityController cityController
	{
		get
		{
			if (_cityController == null && CityController.instance != null)
			{
				_cityController = CityController.instance;
			}
			return _cityController;
		}
	}

	public static Communicator instance { get; private set; }

	public bool PopUpActive
	{
		get
		{
			return MissionScrollerActive() || MailScrollerActive() || LODCityMenuActive();
		}
	}

	public event Action StartDayTime
	{
		add
		{
			Action action = StartDayTime__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StartDayTime__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = StartDayTime__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StartDayTime__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action StartNightTime
	{
		add
		{
			Action action = StartNightTime__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StartNightTime__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = StartNightTime__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StartNightTime__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<NewsFlash> NewsFlashStarted
	{
		add
		{
			Action<NewsFlash> action = NewsFlashStarted__BackingField;
			Action<NewsFlash> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref NewsFlashStarted__BackingField, (Action<NewsFlash>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<NewsFlash> action = NewsFlashStarted__BackingField;
			Action<NewsFlash> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref NewsFlashStarted__BackingField, (Action<NewsFlash>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<NewsFlash> NewsFlashEnded
	{
		add
		{
			Action<NewsFlash> action = NewsFlashEnded__BackingField;
			Action<NewsFlash> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref NewsFlashEnded__BackingField, (Action<NewsFlash>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<NewsFlash> action = NewsFlashEnded__BackingField;
			Action<NewsFlash> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref NewsFlashEnded__BackingField, (Action<NewsFlash>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action MissionScrollerWillAppear
	{
		add
		{
			Action action = MissionScrollerWillAppear__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MissionScrollerWillAppear__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = MissionScrollerWillAppear__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MissionScrollerWillAppear__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		instance = this;
		missionsScrollerController = missionsScroller.GetComponent<MissionsScrollerController>();
	}

	private void OnEnable()
	{
		Player.instance.MissionAdded += HandleMissionAdded;
		Player.instance.MissionCompleted += HandleMissionCompleted;
		Player.instance.MissionUpdated += HandleMissionUpdated;
		Player.instance.MissionPinned += HandleMissionPinned;
		Player.instance.MailAdded += HandleMailAdded;
		Player.instance.MailViewed += HandleMailViewed;
		Player.instance.CharacterAdded += HandleCharacterAdded;
		Player.instance.pvpData.BattleRequestsUpdated += HandlePvpRequestsUpdated;
		Player.instance.pvpData.FriendRequestsUpdated += HandlePvpRequestsUpdated;
		Player.instance.pvpData.FriendRequestAccepted += HandleFriendRequestAcceptedDeclined;
		Player.instance.pvpData.FriendRequestDeclined += HandleFriendRequestAcceptedDeclined;
		Player.instance.TriggerSet += HandleTriggerSet;
		PauseManager.instance.Paused += HandlePaused;
		sideMissionPrototype.SetActive(false);
		sideMissionDailyPrototype.SetActive(false);
		missionTickerAnimator.Play("Communicator_MissionTicker_Outro", 0, 1f);
		missionTicker.SetActive(false);
		newsTicker.SetActive(false);
		HandlePvpRequestsUpdated();
	}

	private void OnDisable()
	{
		Player.instance.MissionAdded -= HandleMissionAdded;
		Player.instance.MissionCompleted -= HandleMissionCompleted;
		Player.instance.MissionUpdated -= HandleMissionUpdated;
		Player.instance.MissionPinned -= HandleMissionPinned;
		Player.instance.MailAdded -= HandleMailAdded;
		Player.instance.MailViewed -= HandleMailViewed;
		Player.instance.CharacterAdded -= HandleCharacterAdded;
		Player.instance.pvpData.BattleRequestsUpdated -= HandlePvpRequestsUpdated;
		Player.instance.pvpData.FriendRequestsUpdated -= HandlePvpRequestsUpdated;
		Player.instance.pvpData.FriendRequestAccepted -= HandleFriendRequestAcceptedDeclined;
		Player.instance.pvpData.FriendRequestDeclined -= HandleFriendRequestAcceptedDeclined;
		Player.instance.TriggerSet -= HandleTriggerSet;
		PauseManager.instance.Paused -= HandlePaused;
		if (Player.instance.currentDay != 0 && Player.instance.currentTime != 0)
		{
			Player.instance.SetCurrentDay(currentDay);
			Player.instance.SetCurrentTime(currentTime);
			Player.instance.SetDaysPassedInCity(daysPassedInCity);
		}
		waitingForNewsFlash = false;
	}

	private void OnDestroy()
	{
		StartDayTime__BackingField = null;
		StartNightTime__BackingField = null;
		NewsFlashStarted__BackingField = null;
		NewsFlashEnded__BackingField = null;
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged -= HandleLanguageChanged;
		}
		lodCityMenu.EnterLODClicked -= HandleEnterLODBtnClicked;
		lodCityMenu.ExitClicked -= HandleExitLODPopupBtnClicked;
		instance = null;
	}

	public void SaveDayAndTime(bool save = true)
	{
		Player.instance.SetCurrentDay(currentDay);
		Player.instance.SetCurrentTime(currentTime);
		Player.instance.SetDaysPassedInCity(daysPassedInCity);
		if (save)
		{
			Player.instance.Save();
		}
	}

	public void SetCurrentTime(int newTime)
	{
		currentTime = newTime;
		float num = 360f / (fullCycleRealSeconds / (float)currentTime);
		dayNightCycle.transform.localEulerAngles = new Vector3(0f, 0f, 0f - num);
	}

	public void SetCommunicatorShown(bool show, bool animate = true)
	{
		if (show)
		{
			if (animator.IsNamed("communicator_hide_intro", "communicator_vibrate"))
			{
				if (animate)
				{
					animator.Play("communicator_hide_outro");
				}
				else
				{
					animator.Play("communicator_hide_outro", 0, 1f);
				}
				CheckForUnreadMail();
				CheckForCompletedMissions();
				CheckForPinnedMission();
			}
		}
		else if (animator.IsNamed("communicator_hide_outro", "communicator_vibrate"))
		{
			if (animate)
			{
				animator.Play("communicator_hide_intro");
			}
			else
			{
				animator.Play("communicator_hide_intro", 0, 1f);
			}
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
			HidePopUps();
		}
	}

	public void ResolveActiveNewsFlash()
	{
		if (activeNewsFlash != null)
		{
			EndBreakingNews();
		}
	}

	private IEnumerator Start()
	{
		if (cityController != null && cityController.mySquad != null)
		{
			cityController.mySquad.OnHide += HandleMySquadOnHide;
			cityController.mySquad.OnShow += HandleMySquadOnShow;
		}
		if (CommunicatorManager.instance != null)
		{
			UnityEngine.Debug.Log("Communicator Manager Initialized");
		}
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged += HandleLanguageChanged;
		}
		currentDay = Player.instance.currentDay;
		currentTime = Player.instance.currentTime;
		daysPassedInCity = Player.instance.GetDaysPassedInCities();
		string ds = "Start, Days Passed in Cities: \n";
		foreach (KeyValuePair<CityName, int> item in daysPassedInCity)
		{
			string text = ds;
			ds = string.Concat(text, " --- ", item.Key, ", days passed: ", item.Value);
		}
		UnityEngine.Debug.Log(ds);
		previousDay = currentDay;
		previousTime = currentTime;
		float timeOfDayInSeconds = 360f / (fullCycleRealSeconds / (float)currentTime);
		dayNightCycle.transform.localEulerAngles = new Vector3(0f, 0f, 0f - timeOfDayInSeconds);
		if ((int)(float)currentTime >= (int)(fullCycleRealSeconds / 2f))
		{
			isDayTime = false;
			if (StartNightTime__BackingField != null)
			{
				StartNightTime__BackingField();
			}
		}
		else
		{
			isDayTime = true;
			if (StartDayTime__BackingField != null)
			{
				StartDayTime__BackingField();
			}
		}
		DayNightCard.instance.UpdateForTimeOfDay(isDayTime);
		flashSaleTimerText.transform.parent.gameObject.SetActive(false);
		Communicator communicator = this;
		Communicator communicator2 = this;
		ViewType viewType = ViewType.DayNight;
		communicator2.prevViewType = ViewType.DayNight;
		communicator.activeViewType = viewType;
		lodCityMenu.EnterLODClicked += HandleEnterLODBtnClicked;
		lodCityMenu.ExitClicked += HandleExitLODPopupBtnClicked;
		if (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive)
		{
			yield return new WaitForEndOfFrame();
			UpdateMail();
			UpdateMissions(false, (!(cityController != null)) ? CityName.Any : cityController.cityName);
			UpdateNews();
			StartCoroutine("CheckForUnCollectedMission");
		}
		else
		{
			TitleScreenController.instance.TitleScreenClosed += DelayedStartAfterTitleScreen;
		}
	}

	private void DelayedStartAfterTitleScreen()
	{
		TitleScreenController.instance.TitleScreenClosed -= DelayedStartAfterTitleScreen;
		UpdateMail();
		UpdateMissions();
		UpdateNews();
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine("CheckForUnCollectedMission");
		}
	}

	private IEnumerator CheckForUnCollectedMission()
	{
		yield return new WaitForEndOfFrame();
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		for (int i = 0; i < sideMissions.Count; i++)
		{
			if (!(sideMissions[i] != null))
			{
				continue;
			}
			CommunicatorMission sideMission = sideMissions[i].GetComponent<CommunicatorMission>();
			if (sideMission != null)
			{
				MissionData missionData = Player.instance.GetMissionData(sideMission.GetUID);
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(sideMission.GetUID);
				if (mission != null && missionData != null && missionData.active && missionData.completed && !missionData.collected)
				{
					yield return new WaitForSeconds(1f);
					ActivateMissions();
					OnClickMissions();
					float scrollBarPercent = Mathf.Abs(sideMission.transform.localPosition.y) / (missionsScroller.ContentLength - missionsScroller.VisibleAreaLength);
					missionsScroller.SetScrollPercentWithoutEvent(scrollBarPercent);
					break;
				}
			}
		}
	}

	public void StartFlashSale()
	{
		inFlashSaleMode = true;
	}

	public void ShowFlashSaleTimer(float duration)
	{
		ActivateFlashSale(duration);
	}

	public void AddFlashSaleTime(float timeAdded)
	{
		flashSaleTimer += timeAdded;
	}

	public void EndFlashSale()
	{
		inFlashSaleMode = false;
		activeViewType = prevViewType;
		switch (activeViewType)
		{
		case ViewType.DayNight:
			ActivateDayNightView();
			break;
		case ViewType.Mail:
			ActivateMail();
			break;
		case ViewType.Missions:
			ActivateMissions();
			break;
		}
	}

	private void Update()
	{
		if ((!(DialogueTree.instance != null) || !DialogueTree.instance.gameObject.activeInHierarchy) && (!(cityController != null) || !(cityController.pauseScreen != null) || !cityController.pauseScreen.gameObject.activeInHierarchy) && !CityDialogBox.IsDialogBoxActive && !_pauseTime && !PopUpActive)
		{
			if (allowTimePassage)
			{
				deltaRotation = 360f / (fullCycleRealSeconds / Time.deltaTime);
				dayNightCycle.transform.localEulerAngles = new Vector3(0f, 0f, dayNightCycle.transform.localEulerAngles.z - deltaRotation);
			}
			CheckTimeInitiatedEvents();
			if (missionsPopUp.activeSelf && missionsPopUpAnimator.IsName("communicator_missionpopup_Outro") && missionsPopUpAnimator.NormalizedTime() > 0.96f)
			{
				missionsPopUp.SetActive(false);
			}
			if (mailPopUp.activeSelf && mailPopUpAnimator.IsName("communicator_emailpopup_Outro") && mailPopUpAnimator.NormalizedTime() > 0.96f)
			{
				mailPopUp.SetActive(false);
			}
		}
	}

	public void DebugIncreaseTimeOfDay()
	{
		currentTime += 25;
		if ((float)currentTime >= fullCycleRealSeconds)
		{
			currentTime -= (int)fullCycleRealSeconds;
			currentDay++;
			int value;
			if (daysPassedInCity.TryGetValue(CityController.instance.cityName, out value))
			{
				daysPassedInCity[CityController.instance.cityName] = value + 1;
			}
			else
			{
				daysPassedInCity[CityController.instance.cityName] = 1;
			}
		}
		float num = 360f / (fullCycleRealSeconds / (float)currentTime);
		dayNightCycle.transform.localEulerAngles = new Vector3(0f, 0f, 0f - num);
		CheckForMailEvents();
	}

	public void DebugDecreaseTimeOfDay()
	{
		currentTime -= 25;
		dayNightCycle.transform.localEulerAngles = new Vector3(0f, 0f, 0f - 360f / (fullCycleRealSeconds / (float)currentTime));
	}

	private void CheckTimeInitiatedEvents()
	{
		elapsedTime += Time.deltaTime;
		previousTime = currentTime;
		if (elapsedTime > 1f)
		{
			elapsedTime = 0f;
			currentTime += (allowTimePassage ? 1 : 0);
			if ((float)currentTime >= fullCycleRealSeconds)
			{
				currentDay++;
				currentTime = 0;
				deltaRotation = 0f;
				int value;
				if (daysPassedInCity.TryGetValue(CityController.instance.cityName, out value))
				{
					daysPassedInCity[CityController.instance.cityName] = value + 1;
				}
				else
				{
					daysPassedInCity[CityController.instance.cityName] = 1;
				}
			}
		}
		if (previousTime != currentTime)
		{
			CheckForMailEvents();
			for (int i = 0; i < currentDayPossibleNewsFlashes.Count; i++)
			{
				NewsFlash newsFlash = currentDayPossibleNewsFlashes[i];
				if (activeNewsFlash != newsFlash && !queuedNewsFlashes.Contains(newsFlash) && currentTime >= newsFlash.timeOfDay && currentTime <= newsFlash.timeOfDay + newsFlash.durationTime)
				{
					ActivateBreakingNews(newsFlash);
				}
			}
			if (activeNewsFlash != null)
			{
				NewsFlashData newsFlashData = Player.instance.GetNewsFlashData(activeNewsFlash.uid);
				if (daysPassedInCity[activeNewsFlash.cityName] >= newsFlashData.dayActive + activeNewsFlash.durationDays && currentTime > newsFlashData.timeActive + activeNewsFlash.durationTime)
				{
					EndBreakingNews();
				}
			}
			if (activeNewsFlash == null && queuedNewsFlashes.Count > 0)
			{
				NewsFlashData newsFlashData2 = Player.instance.GetNewsFlashData(prevActiveNewsFlash.uid);
				if ((DateTime.Now - newsFlashData2.timeCompleted).TotalSeconds >= (double)GameManager.instance.gameSettings.minNewsFlashQueueDelay)
				{
					SetActiveNewsFlash(queuedNewsFlashes.Shift());
				}
			}
			if (currentTime >= (int)(fullCycleRealSeconds / 2f) && isDayTime)
			{
				isDayTime = false;
				DayNightCard.instance.ShowNightCard();
				StartCoroutine("DelayNotifyStartNight");
			}
		}
		if (previousDay != currentDay)
		{
			isDayTime = true;
			newsFlashesShownToday.Clear();
			DayNightCard.instance.ShowDayCard();
			StartCoroutine("DelayNotifyStartDay");
			Player.instance.ClearDefeatedNPCs();
			Player.instance.ClearShopPurchaseData();
			UpdateMail();
			SaveDayAndTime();
		}
		previousDay = currentDay;
	}

	private void CheckForMailEvents()
	{
		int num = mailMessagesForDay.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			MailMessage mailMessage = mailMessagesForDay[num2];
			switch (mailMessage.triggerType)
			{
			case MailMessage.TriggerType.Mission:
			{
				MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(mailMessage.missionUIDRequired);
				if (missionCompletedData == null || currentDay < missionCompletedData.dayCompleted + mailMessage.daysAfter || currentTime < mailMessage.timeOfDay)
				{
					break;
				}
				bool flag = true;
				int count = mailMessage.additionalMissionUIDsRequired.Count;
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						if (!Player.instance.HasMissionCompleted(mailMessage.additionalMissionUIDsRequired[i]))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					Player.instance.AddMail(mailMessage.uid);
					mailMessagesForDay.RemoveAt(num2);
				}
				break;
			}
			case MailMessage.TriggerType.FigurePurchaseCount:
				if (IsComparisonValid(Player.instance.playerStats.numFiguresPurchased, mailMessage.comparison1, mailMessage.intValue1))
				{
					Player.instance.AddMail(mailMessage.uid);
					mailMessagesForDay.RemoveAt(num2);
				}
				break;
			case MailMessage.TriggerType.FigureCollectionSize:
				if (IsComparisonValid(Player.instance.characters.Count, mailMessage.comparison1, mailMessage.intValue1))
				{
					Player.instance.AddMail(mailMessage.uid);
					mailMessagesForDay.RemoveAt(num2);
				}
				break;
			case MailMessage.TriggerType.TutorialComplete:
				if (TutorialManager.instance.IsTutorialComplete(mailMessage.tutorialName))
				{
					Player.instance.AddMail(mailMessage.uid);
					mailMessagesForDay.RemoveAt(num2);
				}
				break;
			}
		}
	}

	private IEnumerator DelayNotifyStartDay()
	{
		yield return new WaitForSeconds(0.5f);
		UpdateNews();
		if (StartDayTime__BackingField != null)
		{
			StartDayTime__BackingField();
		}
	}

	private IEnumerator DelayNotifyStartNight()
	{
		yield return new WaitForSeconds(0.5f);
		ClearDayNewsFlashSprites();
		if (StartNightTime__BackingField != null)
		{
			StartNightTime__BackingField();
		}
	}

	private void CreateDayNightNewFlashSprites()
	{
		foreach (GameObject currentNewsFlashSpriteObj in currentNewsFlashSpriteObjs)
		{
			UnityEngine.Object.Destroy(currentNewsFlashSpriteObj);
		}
		currentNewsFlashSpriteObjs.Clear();
		for (int i = 0; i < currentDayPossibleNewsFlashes.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Communicator/DayNightEvent") as GameObject);
			gameObject.transform.parent = dayNightCycle;
			gameObject.transform.localPosition = Vector3.zero;
			float z = 360f / (fullCycleRealSeconds / (float)currentDayPossibleNewsFlashes[i].timeOfDay);
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, z);
			currentNewsFlashSpriteObjs.Add(gameObject);
		}
	}

	private void ClearDayNewsFlashSprites()
	{
		foreach (GameObject currentNewsFlashSpriteObj in currentNewsFlashSpriteObjs)
		{
			UnityEngine.Object.Destroy(currentNewsFlashSpriteObj);
		}
		currentNewsFlashSpriteObjs.Clear();
		for (int i = 0; i < currentDayPossibleNewsFlashes.Count; i++)
		{
			if ((float)currentDayPossibleNewsFlashes[i].timeOfDay > fullCycleRealSeconds / 2f)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Communicator/DayNightEvent") as GameObject);
				gameObject.transform.parent = dayNightCycle;
				gameObject.transform.localPosition = Vector3.zero;
				float z = 360f / (fullCycleRealSeconds / (float)currentDayPossibleNewsFlashes[i].timeOfDay);
				gameObject.transform.localEulerAngles = new Vector3(0f, 0f, z);
				currentNewsFlashSpriteObjs.Add(gameObject);
			}
		}
	}

	private void CheckMailUID(int mailUID)
	{
		MailData mailData = Player.instance.GetMailData(mailUID);
		if (mailData != null && !mailData.viewed)
		{
			animator.Play("communicator_vibrate");
			antennaLight.gameObject.SetActive(true);
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_Blink");
			antennaTail.gameObject.SetActive(false);
			antennaLight.SetSprite("gui_communicator_antenna_green");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_ringtone_newemail", GFAudioManager.SFX_GROUP, string.Empty);
			if (!currentMail.Contains(mailUID))
			{
				currentMail.Add(mailUID);
			}
		}
	}

	private void HandlePaused(bool pauseStatus)
	{
		if (pauseStatus)
		{
			HidePopUps();
		}
	}

	private void HandleMySquadOnShow()
	{
		SetCommunicatorShown(false);
	}

	private void HandleMySquadOnHide()
	{
		SetCommunicatorShown(true);
	}

	private void HandleMissionAdded(int missionUID, bool dialogueMission)
	{
		animator.Play("communicator_vibrate");
		antennaLight.gameObject.SetActive(true);
		antennaTail.gameObject.SetActive(false);
		antennaLight.SetSprite("gui_communicator_antenna_purple");
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (dialogueMission)
		{
			if (mission.type == MissionType.Side || mission.type == MissionType.Story)
			{
				ActivateMissions();
				if (!missionsPopUp.activeSelf || missionsPopUpAnimator.IsName("communicator_missionpopup_Outro"))
				{
					ShowMissionsPopup(mission.missionLocationCity, missionUID);
				}
			}
			else if (CoreGameTutorial.instance != null)
			{
				CoreGameTutorial.instance.StartCommunicatorTutorial();
			}
		}
		else
		{
			UpdateMissions();
			if (SaveUtility.GetInt("GAME_PLAYED_COMM_TUTORIAL") == 0 && mission.type == MissionType.Side && CoreGameTutorial.instance != null)
			{
				CoreGameTutorial.instance.StartCommunicatorTutorial();
			}
		}
		if (mission.type == MissionType.Story)
		{
			missionsScroller.SetScrollPercentWithoutEvent(0f);
		}
		CheckNewsFlashActivatedByMission(missionUID);
	}

	private void HandleMissionCompleted(int missionUID)
	{
		UpdateMissions();
		UpdateMail();
		UpdateNews();
	}

	public void CheckCollectedMissionNewsFlashes()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		List<NewsFlash> newsFlashes = GameManager.instance.communicatorDatabase.newsFlashes;
		for (int i = 0; i < newsFlashes.Count; i++)
		{
			NewsFlash newsFlash = newsFlashes[i];
			if (newsFlash.activateOnMissionComplete && !Player.instance.HasCompletedNewsFlash(newsFlash.uid) && Player.instance.HasMissionCompleted(newsFlash.missionUIDRequired) && activeNewsFlash != newsFlash && !queuedNewsFlashes.Contains(newsFlash))
			{
				ActivateBreakingNews(newsFlash, false);
			}
		}
	}

	private void HandleMissionUpdated(int missionUID)
	{
		UpdateMissions();
	}

	private void HandleMissionPinned(int missionUID)
	{
		if (missions.activeSelf)
		{
			if (Player.instance.activeMissions.Count == 0)
			{
				HideMissionTicker();
				return;
			}
			StopCoroutine("ShowMissionTicker");
			StartCoroutine("ShowMissionTicker");
		}
	}

	private void HandleMailAdded(int mailUID)
	{
		CheckMailUID(mailUID);
		UpdateMail(false);
		Player.instance.Save();
		CheckForCompletedMissions();
	}

	private void HandleMailViewed(int mailUID)
	{
		UpdateMail(false);
		MailMessage mailMessage = GameManager.instance.communicatorDatabase.GetMailMessage(mailUID);
		if (mailMessage.missionUID > -1)
		{
			Player.instance.AddMission(mailMessage.missionUID);
			addedMailMission = true;
			addedMailMissionUid = mailMessage.missionUID;
		}
	}

	private void HandleCharacterAdded(Character character)
	{
		UpdateMail();
	}

	private void UpdateMail(bool updateDaily = true)
	{
		if (Player.instance.totalUnreadMail <= 0)
		{
			mailBadge.SetActive(false);
		}
		else
		{
			mailBadge.SetActive(true);
			mailNumber.text = Player.instance.totalUnreadMail.ToString();
		}
		if (Language.CurrentLanguage() == LanguageCode.RU || Language.CurrentLanguage() == LanguageCode.KO || Language.CurrentLanguage() == LanguageCode.JA)
		{
			LocalizedText component = mailPopUpNewMsgs.GetComponent<LocalizedText>();
			component.ignoreFontUpdate = false;
			component.ForceInitFontProperties();
			component.UpdateFont();
		}
		mailPopUpNewMsgs.text = string.Format(Language.Get("NEWMESSAGES", "MENUS,GUI"), Player.instance.totalUnreadMail.ToString());
		if (!updateDaily)
		{
			return;
		}
		List<MailMessage> list = GameManager.instance.communicatorDatabase.mail;
		for (int i = 0; i < list.Count; i++)
		{
			MailMessage mailMessage = list[i];
			MailData mailData = Player.instance.GetMailData(mailMessage.uid);
			if (mailData != null)
			{
				if (!mailData.viewed)
				{
					CheckMailUID(mailMessage.uid);
				}
				continue;
			}
			switch (mailMessage.triggerType)
			{
			case MailMessage.TriggerType.Mission:
			{
				MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(mailMessage.missionUIDRequired);
				if (missionCompletedData == null || currentDay < missionCompletedData.dayCompleted + mailMessage.daysAfter)
				{
					break;
				}
				bool flag = true;
				int count = mailMessage.additionalMissionUIDsRequired.Count;
				if (count > 0)
				{
					for (int j = 0; j < count; j++)
					{
						if (!Player.instance.HasMissionCompleted(mailMessage.additionalMissionUIDsRequired[j]))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					UnityEngine.Debug.LogFormat("Completed missions required for {0} email, adding to mail messages for the day....", mailMessage.title);
					if (!mailMessagesForDay.Contains(mailMessage))
					{
						mailMessagesForDay.Add(mailMessage);
					}
				}
				break;
			}
			case MailMessage.TriggerType.FigurePurchaseCount:
				if (IsComparisonValid(Player.instance.playerStats.numFiguresPurchased, mailMessage.comparison1, mailMessage.intValue1) && !mailMessagesForDay.Contains(mailMessage))
				{
					mailMessagesForDay.Add(mailMessage);
				}
				break;
			case MailMessage.TriggerType.FigureCollectionSize:
				if (IsComparisonValid(Player.instance.characters.Count, mailMessage.comparison1, mailMessage.intValue1) && !mailMessagesForDay.Contains(mailMessage))
				{
					mailMessagesForDay.Add(mailMessage);
				}
				break;
			case MailMessage.TriggerType.TutorialComplete:
				if (TutorialManager.instance.IsTutorialComplete(mailMessage.tutorialName) && !mailMessagesForDay.Contains(mailMessage))
				{
					mailMessagesForDay.Add(mailMessage);
				}
				break;
			}
		}
	}

	private bool IsComparisonValid(int val1, Comparison comparison, int val2)
	{
		switch (comparison)
		{
		case Comparison.Exactly:
			return val1 == val2;
		case Comparison.MoreThan:
			return val1 > val2;
		case Comparison.LessThan:
			return val1 < val2;
		case Comparison.AtLeast:
			return val1 >= val2;
		case Comparison.AtMost:
			return val1 <= val2;
		default:
			return false;
		}
	}

	private void UpdateMailPopUpWithUID(int mailUID)
	{
		MailMessage mailMessage = GameManager.instance.communicatorDatabase.GetMailMessage(mailUID);
		if (mailMessage != null)
		{
			if (Language.CurrentLanguage() == LanguageCode.RU || Language.CurrentLanguage() == LanguageCode.TR || Language.CurrentLanguage() == LanguageCode.KO || Language.CurrentLanguage() == LanguageCode.JA)
			{
				LocalizedText component = mailPopUpTitle.GetComponent<LocalizedText>();
				LocalizedText component2 = mailPopUpDescr.GetComponent<LocalizedText>();
				LocalizedText component3 = mailPopUpSignature.GetComponent<LocalizedText>();
				component.ignoreFontUpdate = false;
				component.ForceInitFontProperties();
				component.UpdateFont();
				component2.ignoreFontUpdate = false;
				component2.ForceInitFontProperties();
				component2.UpdateFont();
				mailPopUpDescr.enableAutoSizing = true;
				if (Language.CurrentLanguage() == LanguageCode.JA)
				{
					mailPopUpDescr.enableAutoSizing = false;
					mailPopUpDescr.fontSize = 15f;
				}
				component3.ignoreFontUpdate = false;
				component3.ForceInitFontProperties();
				component3.UpdateFont();
			}
			mailPopUpTitle.text = ((!string.IsNullOrEmpty(mailMessage.titleKey)) ? Language.Get(mailMessage.titleKey, "DIALOGUE") : mailMessage.title);
			StringBuilder text = new StringBuilder((!string.IsNullOrEmpty(mailMessage.messageKey)) ? Language.Get(mailMessage.messageKey, "DIALOGUE") : mailMessage.message);
			mailPopUpDescr.SetText(text);
			mailPopUpDescr.ForceMeshUpdate();
			float num = mailPopUpDescr.transform.localPosition.y - mailPopUpDescr.bounds.size.y - 20f;
			mailPopUpSignature.text = ((!string.IsNullOrEmpty(mailMessage.signatureKey)) ? Language.Get(mailMessage.signatureKey, "DIALOGUE") : mailMessage.signature);
			mailPopUpSignature.transform.localPosition = new Vector3(mailPopUpSignature.transform.localPosition.x, num, mailPopUpSignature.transform.localPosition.z);
			currentMailIndex = Player.instance.GetMailIndex(mailUID);
			MailData mailData = Player.instance.GetMailData(mailUID);
			mailData.viewed = true;
			mailScroller.ContentLength = Mathf.Max(0f, Mathf.Abs(num - 20f));
			mailScroller.SetScrollPercentWithoutEvent(0f);
		}
		if (Language.CurrentLanguage() == LanguageCode.RU || Language.CurrentLanguage() == LanguageCode.KO || Language.CurrentLanguage() == LanguageCode.JA)
		{
			LocalizedText component4 = mailPopUpNewMsgs.GetComponent<LocalizedText>();
			component4.ignoreFontUpdate = false;
			component4.ForceInitFontProperties();
			component4.UpdateFont();
		}
		mailPopUpNewMsgs.text = string.Format(Language.Get("NEWMESSAGES", "MENUS,GUI"), Player.instance.totalUnreadMail.ToString());
	}

	public void UpdateMissions(bool playMissionSideIntro = false, CityName cityNameFilter = CityName.Any, bool notify = false)
	{
		CheckForCompletedMissions();
		for (int i = 0; i < sideMissions.Count; i++)
		{
			missionsScroller.OnScroll -= sideMissions[i].gameObject.GetComponent<CommunicatorMission>().ScollerScrolled;
			UnityEngine.Object.Destroy(sideMissions[i].gameObject);
		}
		missionsScrollerController.UpdateCityButton(cityNameFilter);
		sideMissions.Clear();
		bool flag = DailyMissionManager.instance.IsDailyMissionAvailable();
		float num = -68f;
		int count = Player.instance.activeMissions.Count;
		bool flag2 = false;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		List<MissionData> list = new List<MissionData>(Player.instance.activeMissions);
		list.Sort(MissionsSort);
		for (int j = 0; j < count; j++)
		{
			MissionData missionData = list[j];
			if (missionData == null)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (mission.type == MissionType.Story && missionData.active && !missionData.collected)
			{
				flag2 = true;
				storyMission.UpdateWithMissionUID(mission.uid);
				StartCoroutine(PlayMissionIntro(storyMission));
			}
			if (mission.type == MissionType.Side && missionData.active && !missionData.collected)
			{
				num2++;
			}
			if (mission.type == MissionType.Side && missionData.active && !missionData.collected && (mission.missionLocationCity == CityName.Any || cityNameFilter == CityName.Any || cityNameFilter == mission.missionLocationCity || missionData.debugCollected))
			{
				num3++;
				CommunicatorMission component = UnityEngine.Object.Instantiate((!mission.isDaily) ? sideMissionPrototype : sideMissionDailyPrototype).GetComponent<CommunicatorMission>();
				component.gameObject.SetActive(true);
				component.transform.parent = sideMissionPrototype.transform.parent;
				component.transform.localPosition = new Vector3(sideMissionPrototype.transform.localPosition.x, num, sideMissionPrototype.transform.localPosition.z);
				component.transform.localScale = sideMissionPrototype.transform.localScale;
				component.UpdateWithMissionUID(mission.uid);
				missionsScroller.OnScroll += component.ScollerScrolled;
				sideMissions.Add(component);
				if (cityController != null)
				{
					cityController.StartCoroutine(PlayMissionIntro(component));
				}
				num -= 102f;
			}
		}
		if (flag)
		{
			sideMissionDailyBanner.SetActive(true);
			Vector3 localPosition = sideMissionDailyBanner.transform.localPosition;
			localPosition.y = num + 22f;
			sideMissionDailyBanner.transform.localPosition = localPosition;
		}
		else
		{
			sideMissionDailyBanner.SetActive(false);
		}
		if (num2 > 0 || flag)
		{
			sideMissionHeader.gameObject.SetActive(true);
			missionsScrollerController.buttonJumpCity.transform.parent.gameObject.SetActive(true);
		}
		else
		{
			sideMissionHeader.gameObject.SetActive(false);
			missionsScrollerController.buttonJumpCity.transform.parent.gameObject.SetActive(false);
		}
		float num5 = 115f;
		if (num2 > 0 || flag)
		{
			num5 += 47f;
			num5 += (float)(102 * num3);
			if (flag)
			{
				num5 += 58f;
			}
		}
		missionsScroller.ContentLength = Mathf.Max(0f, num5);
		missionScrollbar.thumbLength = Mathf.Max(30f, missionScrollbar.scrollBarLength * (missionsScroller.VisibleAreaLength / missionsScroller.ContentLength));
		tk2dSlicedSprite component2 = missionScrollbar.thumbTransform.GetComponent<tk2dSlicedSprite>();
		BoxCollider2D component3 = missionScrollbar.thumbTransform.GetComponent<BoxCollider2D>();
		component2.dimensions = new Vector2(component2.dimensions.x, missionScrollbar.thumbLength);
		component3.offset = new Vector2(0f, component2.dimensions.y * -0.5f);
		component3.size = new Vector2(component3.size.x, component2.dimensions.y);
		missionScrollbar.Value = missionScrollbar.Value;
		for (int k = 0; k < Player.instance.activeMissions.Count; k++)
		{
			if (Player.instance.activeMissions[k].completed)
			{
				CheckMissionForConvo(Player.instance.activeMissions[k].uid);
			}
		}
		for (int l = 0; l < Player.instance.completedMissions.Count; l++)
		{
			CheckMissionForConvo(Player.instance.completedMissions[l].uid);
		}
		if (!string.IsNullOrEmpty(afterBattleConvoName))
		{
			Conversation conversation = Resources.Load<Conversation>("Data/TT2/" + afterBattleConvoName);
			if (conversation != null && cityController != null)
			{
				cityController.HandleShowDialogue(conversation, false, null, null, true);
			}
			afterBattleConvoName = string.Empty;
		}
		if (Player.instance.GetMissionCompletedData(129) == null)
		{
			return;
		}
		for (int m = 0; m < Player.instance.activeMissions.Count; m++)
		{
			MissionData missionData2 = Player.instance.activeMissions[m];
			if (missionData2 != null)
			{
				Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(missionData2.uid);
				if (mission2 != null && mission2.type == MissionType.Story && missionData2.active && !missionData2.collected)
				{
					Player.instance.CollectMission(mission2.uid, false);
				}
			}
		}
		storyMission.UpdateWithEndGameDescription();
	}

	private int MissionsSort(MissionData x, MissionData y)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(x.uid);
		Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(y.uid);
		if (mission.type == MissionType.Story && mission2.type == MissionType.Story)
		{
			return 0;
		}
		if (mission.type == MissionType.Story)
		{
			return -1;
		}
		if (mission2.type == MissionType.Story)
		{
			return 1;
		}
		if (x.isDaily == y.isDaily)
		{
			if (mission.missionLocationCity == mission2.missionLocationCity)
			{
				return 0;
			}
			if (mission.missionLocationCity == CityName.Any)
			{
				return 1;
			}
			if (mission2.missionLocationCity == CityName.Any)
			{
				return -1;
			}
			return 0;
		}
		if (x.isDaily)
		{
			return -1;
		}
		return 1;
	}

	public void CheckForNewMissions()
	{
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (missionData == null || missionData.uid <= -1 || !(mission != null) || mission.type != MissionType.Side || missionData.viewed)
			{
				continue;
			}
			if (activeViewType != ViewType.Missions)
			{
				ActivateMissions();
			}
			if (!MissionScrollerActive())
			{
				OnClickMissions();
			}
			CommunicatorMission communicatorMission = null;
			for (int j = 0; j < sideMissions.Count; j++)
			{
				if (sideMissions[j].GetUID == mission.uid)
				{
					communicatorMission = sideMissions[j];
					break;
				}
			}
			if (communicatorMission != null)
			{
				float num = 0f - communicatorMission.transform.localPosition.y;
				float scrollPercentWithoutEvent = Mathf.Clamp01(num / (missionsScroller.ContentLength - missionsScroller.VisibleAreaLength));
				missionsScroller.SetScrollPercentWithoutEvent(scrollPercentWithoutEvent);
			}
			break;
		}
	}

	private IEnumerator PlayMissionIntro(CommunicatorMission communicatorMission)
	{
		if (!(communicatorMission == null))
		{
			yield return new WaitForEndOfFrame();
			while (!base.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
			while (communicatorMission != null && !communicatorMission.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
			if (communicatorMission != null)
			{
				communicatorMission.PlayIntroAnimation();
			}
		}
	}

	private void CheckMissionForConvo(int missionUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (mission != null && mission.type == MissionType.Story && mission.completionConvo != null)
		{
			Conversation conversation = (Conversation)mission.completionConvo;
			string cityCharacterConvoName = CityCharacter.GetCityCharacterConvoName(conversation.name);
			conversation = Resources.Load("Data/TT2/" + cityCharacterConvoName) as Conversation;
			if (conversation != null && !Player.instance.IsConversationPlayed(conversation) && !convosPlayed.Contains(cityCharacterConvoName) && cityController != null)
			{
				convosPlayed.Add(cityCharacterConvoName);
				cityController.HandleShowDialogue(conversation, false, null, null, true);
				afterBattleConvoName = string.Empty;
			}
		}
		if (mission != null && mission.type == MissionType.Side && mission.completionConvo != null && mission.playCompletionConvoEnteringCity && mission.autoCollect && mission.uid > 79)
		{
			Conversation conversation2 = (Conversation)mission.completionConvo;
			string cityCharacterConvoName2 = CityCharacter.GetCityCharacterConvoName(conversation2.name);
			conversation2 = Resources.Load("Data/TT2/" + cityCharacterConvoName2) as Conversation;
			if (conversation2 != null && !Player.instance.IsConversationPlayed(conversation2) && !convosPlayed.Contains(cityCharacterConvoName2) && cityController != null)
			{
				convosPlayed.Add(cityCharacterConvoName2);
				cityController.HandleShowDialogue(conversation2, false, null, null, true, 0.1f);
				afterBattleConvoName = string.Empty;
			}
		}
	}

	private void CheckNewsFlashActivatedByMission(int missionUID)
	{
		List<NewsFlash> newsFlashes = GameManager.instance.communicatorDatabase.newsFlashes;
		for (int i = 0; i < newsFlashes.Count; i++)
		{
			NewsFlash newsFlash = newsFlashes[i];
			if (newsFlash.missionUIDRequired == missionUID && newsFlash.activateWithMission)
			{
				ActivateBreakingNews(newsFlash);
			}
		}
	}

	private void UpdateNews(bool updateDaily = true)
	{
		if (!updateDaily)
		{
			return;
		}
		currentDayPossibleNewsFlashes.Clear();
		List<NewsFlash> newsFlashes = GameManager.instance.communicatorDatabase.newsFlashes;
		for (int i = 0; i < newsFlashes.Count; i++)
		{
			NewsFlash newsFlash = newsFlashes[i];
			NewsFlashData newsFlashData = Player.instance.GetNewsFlashData(newsFlash.uid);
			MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(newsFlash.missionUIDRequired);
			if (newsFlash.cityName == CityController.instance.cityName && missionCompletedData != null)
			{
				int num = 0;
				switch (newsFlash.cityName)
				{
				case CityName.Gotham:
					num = missionCompletedData.gothamDayCompleted;
					break;
				case CityName.JumpCity:
					num = missionCompletedData.jumpCityDayCompleted;
					break;
				case CityName.Metropolis:
					num = missionCompletedData.metropolisDayCompleted;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				int value;
				daysPassedInCity.TryGetValue(newsFlash.cityName, out value);
				if ((newsFlashData == null || (!newsFlashData.completed && value <= newsFlashData.dayActive + newsFlash.durationDays) || (newsFlashData.completed && newsFlash.repeatable && value >= newsFlashData.dayCompleted + newsFlash.dayRepeatInterval)) && value >= num + newsFlash.daysAfter && (currentTime <= newsFlash.timeOfDay || (currentTime >= newsFlash.timeOfDay && currentTime <= newsFlash.timeOfDay + newsFlash.durationTime)) && !currentDayPossibleNewsFlashes.Contains(newsFlash) && (newsFlashData == null || !newsFlashData.completed || newsFlash.repeatable) && (newsFlash.newsFlashUIDRequired < 0 || Player.instance.GetNewsFlashData(newsFlash.newsFlashUIDRequired) != null))
				{
					currentDayPossibleNewsFlashes.Add(newsFlash);
				}
			}
		}
		CreateDayNightNewFlashSprites();
	}

	private void ActivateFlashSale(float duration)
	{
		prevViewType = activeViewType;
		activeViewType = ViewType.FlashSale;
		dayNight.SetActive(false);
		mail.SetActive(false);
		missions.SetActive(false);
		flashSaleTimerText.transform.parent.gameObject.SetActive(true);
		legionOfDoom.SetActive(false);
		HidePopUps();
		flashSaleTimer = duration;
		int num = (int)flashSaleTimer / 60;
		int num2 = (int)(flashSaleTimer % 60f);
		string text = ((num >= 10) ? num.ToString() : ("0" + num));
		string text2 = ((num2 >= 10) ? num2.ToString() : ("0" + num2));
		flashSaleTimerText.text = text + ":" + text2;
		if (mailPopUpAnimator.IsNamed("communicator_emailpopup_intro", "communicator_emailpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		if (missionsPopUpAnimator.IsNamed("communicator_missionpopup_intro", "communicator_missionpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
	}

	private void ActivateDayNightView()
	{
		prevViewType = activeViewType;
		activeViewType = ViewType.DayNight;
		dayNight.SetActive(true);
		mail.SetActive(false);
		missions.SetActive(false);
		flashSaleTimerText.transform.parent.gameObject.SetActive(false);
		legionOfDoom.SetActive(false);
		HidePopUps();
		if (mailPopUpAnimator.IsNamed("communicator_emailpopup_intro", "communicator_emailpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		if (missionsPopUpAnimator.IsNamed("communicator_missionpopup_intro", "communicator_missionpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		if (activeNewsFlash != null)
		{
			antennaLight.gameObject.SetActive(true);
			antennaLight.SetSprite("gui_communicator_antenna_blue");
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_On");
			newsTicker.SetActive(true);
			if (newsTickerAnimator.IsName("Communicator_NewsTicker_Outro") && missionTickerAnimator.NormalizedTime() > 0.7f)
			{
				newsTickerAnimator.Play("Communicator_NewsTicker_Intro");
			}
		}
		else
		{
			CheckForUnreadMail();
			CheckForCompletedMissions();
		}
	}

	public void ActivateMissions()
	{
		prevViewType = activeViewType;
		activeViewType = ViewType.Missions;
		dayNight.SetActive(false);
		mail.SetActive(false);
		missions.SetActive(true);
		flashSaleTimerText.transform.parent.gameObject.SetActive(false);
		legionOfDoom.SetActive(false);
		HidePopUps();
		if (mailPopUpAnimator.IsNamed("communicator_emailpopup_intro", "communicator_emailpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		StartCoroutine("ShowMissionTicker");
		CheckForUnreadMail();
		CheckForCompletedMissions();
	}

	private void ActivateMail()
	{
		prevViewType = activeViewType;
		activeViewType = ViewType.Mail;
		dayNight.SetActive(false);
		mail.SetActive(true);
		missions.SetActive(false);
		flashSaleTimerText.transform.parent.gameObject.SetActive(false);
		legionOfDoom.SetActive(false);
		HidePopUps();
		if (mailPopUpAnimator.IsNamed("communicator_emailpopup_intro", "communicator_emailpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		if (missionsPopUpAnimator.IsNamed("communicator_missionpopup_intro", "communicator_missionpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		CheckForUnreadMail();
		CheckForCompletedMissions();
		if (currentMail.Count > 0)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_emailscreen_newmessage", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void ActivateLegionOfDoom()
	{
		prevViewType = activeViewType;
		activeViewType = ViewType.LOD;
		dayNight.SetActive(false);
		mail.SetActive(false);
		missions.SetActive(false);
		flashSaleTimerText.transform.parent.gameObject.SetActive(false);
		legionOfDoom.SetActive(true);
		HidePopUps();
		if (mailPopUpAnimator.IsNamed("communicator_emailpopup_intro", "communicator_emailpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		if (missionsPopUpAnimator.IsNamed("communicator_missionpopup_intro", "communicator_missionpopup_loop"))
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
	}

	private void ActivateBreakingNews(NewsFlash newsFlash, bool delayBanner = true)
	{
		if (!newsFlashesShownToday.Contains(newsFlash))
		{
			if (activeNewsFlash == null && queuedNewsFlashes.Count == 0)
			{
				SetActiveNewsFlash(newsFlash, delayBanner);
			}
			else if (!queuedNewsFlashes.Contains(newsFlash))
			{
				queuedNewsFlashes.Add(newsFlash);
			}
		}
	}

	private void SetActiveNewsFlash(NewsFlash newsFlash, bool delayBanner = true)
	{
		NewsFlashData newsFlashData = Player.instance.GetNewsFlashData(newsFlash.uid);
		if (newsFlashData == null)
		{
			SaveDayAndTime(false);
			Player.instance.AddNewsFlash(newsFlash.uid);
		}
		else
		{
			if (!newsFlash.repeatable)
			{
				UnityEngine.Debug.LogErrorFormat("Attempted to Set News Flash, {0}, active but aborted. This news flash has already been set as the active news flash before and is NOT repeatable.", newsFlash.title);
				return;
			}
			newsFlashData.dayActive = daysPassedInCity[newsFlash.cityName];
			newsFlashData.timeActive = currentTime;
		}
		activeNewsFlash = newsFlash;
		StartCoroutine(DelayBannerForNewsFlash(newsFlash, delayBanner));
		if (NewsFlashStarted__BackingField != null)
		{
			NewsFlashStarted__BackingField(newsFlash);
		}
	}

	private void EndBreakingNews()
	{
		if (NewsFlashEnded__BackingField != null)
		{
			NewsFlashEnded__BackingField(activeNewsFlash);
		}
		currentDayPossibleNewsFlashes.Remove(activeNewsFlash);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_newsflash_closeorend", GFAudioManager.SFX_GROUP, string.Empty);
		if (newsTickerAnimator.IsNamed("Communicator_NewsTicker_Intro", "Communicator_NewsTicker_Breakingnews"))
		{
			newsTickerAnimator.Play("Communicator_NewsTicker_Outro");
		}
		if (dayNight.activeSelf)
		{
			antennaTail.gameObject.SetActive(false);
			antennaLight.gameObject.SetActive(false);
		}
		Player.instance.CompleteNewsFlash(activeNewsFlash.uid);
		prevActiveNewsFlash = activeNewsFlash;
		activeNewsFlash = null;
		CheckForUnreadMail();
		CheckForCompletedMissions();
	}

	public bool IsActiveNewsFlash(int newsFlashUID)
	{
		return activeNewsFlash != null && activeNewsFlash.uid == newsFlashUID;
	}

	private IEnumerator DelayBannerForNewsFlash(NewsFlash newsFlash, bool useDelay = true)
	{
		newsFlashesShownToday.Add(newsFlash);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_ringtone_newsflash", GFAudioManager.SFX_GROUP, string.Empty);
		animator.Play("communicator_vibrate", 0, 0f);
		antennaLight.gameObject.SetActive(true);
		antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_Blink");
		antennaTail.gameObject.SetActive(false);
		antennaLight.SetSprite("gui_communicator_antenna_blue");
		waitingForNewsFlash = true;
		if (useDelay)
		{
			yield return new WaitForSeconds(1f);
		}
		while (PopUpActive)
		{
			yield return null;
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_newsflash_appear", GFAudioManager.SFX_GROUP, string.Empty);
		if (activeViewType != ViewType.DayNight)
		{
			ActivateDayNightView();
		}
		antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_On");
		yield return new WaitForEndOfFrame();
		if (missionTickerAnimator.IsName("Communicator_MissionTicker_Intro"))
		{
			missionTickerAnimator.Play("Communicator_MissionTicker_Outro");
		}
		if (newsTickerAnimator.IsNamed("Communicator_NewsTicker_Intro", "Communicator_NewsTicker_Breakingnews"))
		{
			newsTickerAnimator.Play("Communicator_NewsTicker_Outro");
			yield return new WaitForEndOfFrame();
			while (newsTickerAnimator.IsName("Communicator_NewsTicker_Outro") && newsTickerAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		newsTickerHeader.text = (string.IsNullOrEmpty(newsFlash.titleKey) ? newsFlash.title : Language.Get(newsFlash.titleKey, "MENUS,GUI"));
		if (newsFlash.newsFlashType != NewsFlashType.Generic)
		{
			ShopDefinition shopDef = GameManager.instance.shopDatabase.GetShopDef(newsFlash.targetShopUID);
			string arg = ((!(shopDef != null)) ? string.Empty : Language.Get(shopDef.nameKey, "MENUS,GUI"));
			if (shopDef == null)
			{
				UnityEngine.Debug.LogError("News Flash " + newsFlash.title + ", uid: " + newsFlash.uid + ", has a NULL reference to a shop. Update Shop reference in Communicator Database.");
			}
			if (!string.IsNullOrEmpty(newsFlash.messageKey))
			{
				newsTickerDescription.text = string.Format(Language.Get(newsFlash.messageKey, "MENUS,GUI"), arg);
			}
			else
			{
				newsTickerDescription.text = string.Format(newsFlash.message, arg);
			}
		}
		else
		{
			newsTickerDescription.text = (string.IsNullOrEmpty(newsFlash.messageKey) ? newsFlash.message : Language.Get(newsFlash.messageKey, "MENUS,GUI"));
		}
		waitingForNewsFlash = false;
		newsTicker.SetActive(true);
		newsTickerAnimator.Play("Communicator_NewsTicker_Breakingnews");
		if (newsTickerMarquee != null)
		{
			newsTickerMarquee.ResetPosition();
		}
	}

	public void HidePopUps()
	{
		cityController.pinchZoomer.allowPan = true;
		if (missionsPopUpAnimator.IsNamed("communicator_missionpopup_intro", "communicator_missionpopup_loop"))
		{
			missionsPopUpAnimator.Play("communicator_missionpopup_Outro");
		}
		if (missionTickerAnimator.IsName("Communicator_MissionTicker_Intro"))
		{
			missionTickerAnimator.Play("Communicator_MissionTicker_Outro");
		}
		if (newsTickerAnimator.IsNamed("Communicator_NewsTicker_Intro", "Communicator_NewsTicker_Breakingnews"))
		{
			newsTickerAnimator.Play("Communicator_NewsTicker_Outro");
		}
		if (mailPopUpAnimator.IsNamed("communicator_emailpopup_intro", "communicator_emailpopup_loop"))
		{
			mailPopUpAnimator.Play("communicator_emailpopup_Outro");
		}
		lodCityMenu.Hide(false);
	}

	public void CheckForPinnedMission()
	{
		if (missions.activeSelf)
		{
			StartCoroutine("ShowMissionTicker");
		}
	}

	private void CheckForCompletedMissions()
	{
		int count = Player.instance.activeMissions.Count;
		if (count > 0)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				MissionData missionData = Player.instance.activeMissions[i];
				if (missionData.completed && !missionData.collected)
				{
					num++;
				}
			}
			if (num > 0)
			{
				missionsNumber.faceColor = Color.yellow;
				antennaLight.gameObject.SetActive(true);
				antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_Blink");
				antennaLight.SetSprite("gui_communicator_antenna_purple");
				if (num > 1)
				{
					missionsText.text = Language.Get("REWARD_PLURAL", "MENUS,GUI");
				}
				else
				{
					missionsText.text = Language.Get("REWARD", "MENUS,GUI");
				}
				missionsNumber.text = num.ToString();
			}
			else
			{
				missionsNumber.faceColor = Color.white;
				if (count > 1)
				{
					missionsText.text = Language.Get("MISSION_PLURAL", "MENUS,GUI");
				}
				else
				{
					missionsText.text = Language.Get("MISSION", "MENUS,GUI");
				}
				missionsNumber.text = count.ToString();
			}
		}
		else
		{
			missionsNumber.faceColor = Color.white;
			missionsText.text = Language.Get("MISSION_PLURAL", "MENUS,GUI");
			missionsNumber.text = "0";
		}
	}

	private void CheckForUnreadMail()
	{
		if (currentMail.Count > 0)
		{
			antennaLight.gameObject.SetActive(true);
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_Blink");
			antennaTail.gameObject.SetActive(false);
			antennaLight.SetSprite("gui_communicator_antenna_green");
		}
		else
		{
			antennaLight.gameObject.SetActive(false);
			antennaTail.gameObject.SetActive(false);
		}
	}

	private void OnClickClock()
	{
		cityController.cityCharacter.StopMove();
		newsTicker.SetActive(true);
		if (activeNewsFlash == null && !waitingForNewsFlash)
		{
			newsTickerHeader.text = string.Empty;
			newsTickerDescription.text = Language.Get("NEWS_NOTHING", "MENUS,GUI");
		}
		if (newsTickerAnimator.IsName("Communicator_NewsTicker_Outro") && newsTickerAnimator.NormalizedTime() > 0.7f)
		{
			newsTickerAnimator.Play("Communicator_NewsTicker_Intro");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_newsflash_closeorend", GFAudioManager.SFX_GROUP, string.Empty);
		}
		if (newsTickerAnimator.IsNamed("Communicator_NewsTicker_Intro", "Communicator_NewsTicker_Breakingnews") && newsTickerAnimator.NormalizedTime() > 0.7f)
		{
			newsTickerAnimator.Play("Communicator_NewsTicker_Outro");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_newsflash_closeorend", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public void ShowCurrentTicker()
	{
		if (activeViewType == ViewType.Missions && missions.activeSelf)
		{
			missionTickerAnimator.Play("Communicator_MissionTicker_Outro", 0, 1f);
			StopCoroutine("ShowMissionTicker");
			StartCoroutine("ShowMissionTicker");
		}
		if (activeViewType == ViewType.DayNight && dayNight.activeSelf)
		{
			OnClickClock();
		}
	}

	public void HideMissionTicker()
	{
		missionTickerAnimator.Play("Communicator_MissionTicker_Outro", 0, 1f);
	}

	private IEnumerator ShowMissionTicker()
	{
		yield return new WaitForEndOfFrame();
		if (Player.instance.activeMissions.Count == 0)
		{
			missionTicker.SetActive(false);
			yield break;
		}
		missionTicker.SetActive(true);
		while (missionsPopUp.activeSelf && missionsPopUpAnimator.IsName("communicator_missionpopup_intro") && missionsPopUpAnimator.NormalizedTime() < 0.95f && missionTickerAnimator.IsName("Communicator_MissionTicker_Intro") && missionTickerAnimator.NormalizedTime() < 0.95f)
		{
			yield return new WaitForEndOfFrame();
		}
		if (missionTickerAnimator.IsName("Communicator_MissionTicker_Intro") && missionTickerAnimator.NormalizedTime() > 0.95f)
		{
			missionTickerAnimator.Play("Communicator_MissionTicker_Outro");
			yield return new WaitForEndOfFrame();
			while (missionTickerAnimator.NormalizedTime() < 0.98f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		else if (missionTickerAnimator.IsName("Communicator_MissionTicker_Intro") && missionTickerAnimator.NormalizedTime() <= 0.95f)
		{
			missionTickerAnimator.Play("Communicator_MissionTicker_Outro", 0, 0.91f);
			yield return new WaitForEndOfFrame();
		}
		MissionData pinnedMission;
		if (((!missionTickerAnimator.IsName("Communicator_MissionTicker_Outro") || !(missionTickerAnimator.NormalizedTime() > 0.9f)) && !missionTickerAnimator.IsName("empty")) || (pinnedMission = Player.instance.GetPinnedMission()) == null)
		{
			yield break;
		}
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(pinnedMission.uid);
		missionTickerHeader.text = ((!string.IsNullOrEmpty(mission.titleKey)) ? Language.Get(mission.titleKey, "MISSIONS") : mission.title);
		missionTickerAnimator.Play("Communicator_MissionTicker_Intro");
		missionTickerPin.SetSprite("gui_communicator_missions_pin");
		missionTickerDescText.gameObject.SetActive(false);
		missionTickerVersus.SetActive(false);
		missionTickerCollect1.SetActive(false);
		missionTickerCollect2.SetActive(false);
		missionTickerCollect3.SetActive(false);
		missionTickerCollect4.SetActive(false);
		missionTickerCollect5.SetActive(false);
		if (mission.descriptionOverrides)
		{
			missionTickerDescText.gameObject.SetActive(true);
			if (mission.sideType == MissionSideType.Find || mission.sideType == MissionSideType.NPCScavengerHunt)
			{
				missionTickerDescText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description).Replace("{0}", Mathf.Max(0, mission.amountNeeded - pinnedMission.currentAmount).ToString());
			}
			else
			{
				missionTickerDescText.text = ((!string.IsNullOrEmpty(mission.descriptionKey)) ? Language.Get(mission.descriptionKey, "MISSIONS") : mission.description);
			}
		}
		else if (mission.sideType == MissionSideType.Battle)
		{
			missionTickerVersus.SetActive(true);
			missionTickerVersusIcon.gameObject.SetActive(true);
			if (mission.iconSpriteCollection != null)
			{
				missionTickerVersusIcon.SetSprite(mission.iconSpriteCollection, mission.iconSpriteName);
			}
			else
			{
				missionTickerVersusIcon.SetSprite(mission.iconSpriteName);
			}
			missionTickerVersusIcon.transform.localScale = mission.iconScale;
			missionTickerNumber.text = Mathf.Max(0, mission.amountNeeded - pinnedMission.currentAmount).ToString();
			missionTickerType.text = Language.Get(mission.shortTypeNameKey, "MENUS,GUI");
			missionTickerType2.gameObject.SetActive(false);
		}
		else if (mission.sideType == MissionSideType.Investigate || mission.sideType == MissionSideType.TravelTo)
		{
			missionTickerVersusIcon.gameObject.SetActive(false);
		}
		else if (mission.sideType == MissionSideType.Buy)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < mission.buyConfigs.Length; i++)
			{
				num += pinnedMission.counts[i];
				num2 += mission.buyConfigs[i].count;
			}
			missionTickerType.text = Language.Get("BUY", "MENUS,GUI");
			MissionTypeConfigBuy.FigureConfig.Type type = mission.buyConfigs[0].figureConfig.type;
			int num3 = mission.buyConfigs.Length;
			CharacterDatabase characterDatabase = GameManager.instance.characterDatabase;
			GameObject gameObject = missionTickerCollect1;
			if (num3 == 1)
			{
				gameObject = missionTickerCollect1;
			}
			if (num3 == 2)
			{
				gameObject = missionTickerCollect2;
			}
			if (num3 == 3)
			{
				gameObject = missionTickerCollect3;
			}
			if (num3 == 4)
			{
				gameObject = missionTickerCollect4;
			}
			if (num3 == 5)
			{
				gameObject = missionTickerCollect5;
			}
			gameObject.SetActive(true);
			MissionCollect[] componentsInChildren = gameObject.GetComponentsInChildren<MissionCollect>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				MissionCollect missionCollect = componentsInChildren[j];
				if (missionCollect.figsText != null)
				{
					missionCollect.figsText.renderer.enabled = false;
				}
				int num4 = pinnedMission.counts[j];
				int count = mission.buyConfigs[j].count;
				switch (type)
				{
				case MissionTypeConfigBuy.FigureConfig.Type.Name:
				{
					CharacterDefinition character = characterDatabase.GetCharacter(mission.buyConfigs[j].figureConfig.validUids[0]);
					missionCollect.iconSprite.SetSprite(character.portraitSprite);
					missionCollect.iconSprite.GetComponent<tk2dAnimationAdapter>().scale = new Vector3(0.5f, 0.5f, 1f);
					missionCollect.nameText.text = Language.Get(character.characterNameKey, "NPCS,FIGS");
					missionCollect.nameText.transform.localPosition = new Vector3(missionCollect.nameText.transform.localPosition.x, -22.4f, missionCollect.nameText.transform.localPosition.z);
					missionCollect.nameText.rectTransform.sizeDelta = new Vector2(36f, 17f);
					missionCollect.nameText.enableWordWrapping = true;
					missionCollect.nameText.fontSizeMin = 4f;
					break;
				}
				case MissionTypeConfigBuy.FigureConfig.Type.Class:
					missionCollect.iconSprite.SetSprite(ClassBonusBox.classIconSpriteNames[(CharacterClass)mission.buyConfigs[j].figureConfig.validUids[0]]);
					missionCollect.iconSprite.GetComponent<tk2dAnimationAdapter>().scale = new Vector3(1f, 1f, 1f);
					missionCollect.nameText.text = string.Empty;
					if (componentsInChildren.Length == 1 && missionCollect.figsText != null)
					{
						missionCollect.figsText.renderer.enabled = true;
					}
					break;
				}
				if (num4 >= count)
				{
					missionCollect.checkmark.SetActive(true);
					if (missionCollect.countText != null)
					{
						missionCollect.countText.text = string.Empty;
					}
				}
				else
				{
					missionCollect.checkmark.SetActive(false);
					if (missionCollect.countText != null)
					{
						missionCollect.countText.text = (count - num4).ToString();
					}
				}
			}
		}
		else
		{
			if (mission.sideType != MissionSideType.Collect)
			{
				yield break;
			}
			int count2 = mission.collectUIDS.Count;
			CharacterDatabase characterDatabase2 = GameManager.instance.characterDatabase;
			GameObject gameObject2 = missionTickerCollect1;
			if (count2 == 1)
			{
				gameObject2 = missionTickerCollect1;
			}
			if (count2 == 2)
			{
				gameObject2 = missionTickerCollect2;
			}
			if (count2 == 3)
			{
				gameObject2 = missionTickerCollect3;
			}
			if (count2 == 4)
			{
				gameObject2 = missionTickerCollect4;
			}
			if (count2 == 5)
			{
				gameObject2 = missionTickerCollect5;
			}
			gameObject2.SetActive(true);
			MissionCollect[] componentsInChildren2 = gameObject2.GetComponentsInChildren<MissionCollect>(true);
			for (int k = 0; k < componentsInChildren2.Length; k++)
			{
				MissionCollect missionCollect2 = componentsInChildren2[k];
				CharacterDefinition character2 = characterDatabase2.GetCharacter(mission.collectUIDS[k]);
				missionCollect2.iconSprite.SetSprite(character2.portraitSprite);
				missionCollect2.nameText.text = Language.Get(character2.characterNameKey, "NPCS,FIGS");
				missionCollect2.checkmark.SetActive(false);
				missionCollect2.nameText.transform.localPosition = new Vector3(missionCollect2.nameText.transform.localPosition.x, -22.4f, missionCollect2.nameText.transform.localPosition.z);
				missionCollect2.nameText.rectTransform.sizeDelta = new Vector2(36f, 17f);
				missionCollect2.nameText.enableWordWrapping = true;
				missionCollect2.nameText.fontSizeMin = 4f;
			}
			List<int> list = new List<int>();
			for (int l = 0; l < pinnedMission.collectedUIDs.Count; l++)
			{
				bool flag = false;
				for (int m = 0; m < componentsInChildren2.Length; m++)
				{
					if (!flag)
					{
						MissionCollect missionCollect3 = componentsInChildren2[m];
						int num5 = mission.collectUIDS[m];
						if (pinnedMission.collectedUIDs[l] == num5 && !list.Contains(m))
						{
							flag = true;
							list.Add(m);
							missionCollect3.checkmark.SetActive(true);
						}
					}
				}
			}
		}
	}

	public void ShowMissionsPopup(CityName cityName, int centerOnMissionUid = -1)
	{
		cityController.cityCharacter.StopMove();
		if (missionsPopUp.activeSelf && !missionsPopUpAnimator.IsName("communicator_missionpopup_Outro"))
		{
			return;
		}
		if (MissionScrollerWillAppear__BackingField != null)
		{
			MissionScrollerWillAppear__BackingField();
		}
		UpdateMissions(true, (cityName != CityName.Any) ? cityName : CityController.instance.cityName);
		missionsPopUp.SetActive(true);
		antennaLight.gameObject.SetActive(true);
		antennaTail.gameObject.SetActive(true);
		antennaLight.SetSprite("gui_communicator_antenna_purple");
		antennaTail.SetSprite("gui_communicator_missions_tail");
		missionsPopUpAnimator.Play("communicator_missionpopup_intro");
		antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_On");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowopen", GFAudioManager.SFX_GROUP, string.Empty);
		cityController.pinchZoomer.allowPan = false;
		if (centerOnMissionUid > -1)
		{
			for (int i = 0; i < sideMissions.Count; i++)
			{
				CommunicatorMission communicatorMission = sideMissions[i];
				if (communicatorMission != null && communicatorMission.GetUID == centerOnMissionUid)
				{
					float scrollPercentWithoutEvent = Mathf.Clamp01(Mathf.Abs(communicatorMission.transform.localPosition.y) / (missionsScroller.ContentLength - missionsScroller.VisibleAreaLength));
					missionsScroller.SetScrollPercentWithoutEvent(scrollPercentWithoutEvent);
					break;
				}
			}
			return;
		}
		for (int j = 0; j < sideMissions.Count; j++)
		{
			CommunicatorMission communicatorMission2 = sideMissions[j];
			if (communicatorMission2 != null)
			{
				MissionData missionData = Player.instance.GetMissionData(communicatorMission2.GetUID);
				if (missionData != null && missionData.active && missionData.completed && !missionData.collected)
				{
					float scrollPercentWithoutEvent2 = Mathf.Clamp01(Mathf.Abs(communicatorMission2.transform.localPosition.y) / (missionsScroller.ContentLength - missionsScroller.VisibleAreaLength));
					missionsScroller.SetScrollPercentWithoutEvent(scrollPercentWithoutEvent2);
					break;
				}
			}
		}
	}

	public void OnClickMissions()
	{
		cityController.cityCharacter.StopMove();
		if (!missionsPopUp.activeSelf || missionsPopUpAnimator.IsName("communicator_missionpopup_Outro"))
		{
			if (MissionScrollerWillAppear__BackingField != null)
			{
				MissionScrollerWillAppear__BackingField();
			}
			UpdateMissions(true, CityController.instance.cityName);
			missionsPopUp.SetActive(true);
			antennaLight.gameObject.SetActive(true);
			antennaTail.gameObject.SetActive(true);
			antennaLight.SetSprite("gui_communicator_antenna_purple");
			antennaTail.SetSprite("gui_communicator_missions_tail");
			missionsPopUpAnimator.Play("communicator_missionpopup_intro");
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_On");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowopen", GFAudioManager.SFX_GROUP, string.Empty);
			cityController.pinchZoomer.allowPan = false;
			for (int i = 0; i < sideMissions.Count; i++)
			{
				if (!(sideMissions[i] != null))
				{
					continue;
				}
				CommunicatorMission component = sideMissions[i].GetComponent<CommunicatorMission>();
				if (component != null)
				{
					MissionData missionData = Player.instance.GetMissionData(component.GetUID);
					Mission mission = GameManager.instance.communicatorDatabase.GetMission(component.GetUID);
					if (mission != null && missionData != null && missionData.active && missionData.completed && !missionData.collected)
					{
						float scrollPercentWithoutEvent = Mathf.Abs(component.transform.localPosition.y) / (missionsScroller.ContentLength - missionsScroller.VisibleAreaLength);
						missionsScroller.SetScrollPercentWithoutEvent(scrollPercentWithoutEvent);
						break;
					}
				}
			}
		}
		else
		{
			OnClickCloseMissions();
		}
	}

	private void OnClickCloseMissions()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowclose", GFAudioManager.SFX_GROUP, string.Empty);
		cityController.pinchZoomer.allowPan = true;
		if (currentMail.Count > 0)
		{
			animator.Play("communicator_vibrate");
			antennaLight.gameObject.SetActive(true);
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_Blink");
			antennaTail.gameObject.SetActive(false);
			antennaLight.SetSprite("gui_communicator_antenna_green");
		}
		else
		{
			antennaLight.gameObject.SetActive(false);
			antennaTail.gameObject.SetActive(false);
		}
		missionsPopUpAnimator.Play("communicator_missionpopup_Outro");
	}

	private void OnClickMail()
	{
		cityController.cityCharacter.StopMove();
		if (!mailPopUp.activeSelf || mailPopUpAnimator.IsName("communicator_emailpopup_Outro"))
		{
			mailPopUp.SetActive(true);
			if (currentMail.Count > 0)
			{
				Player.instance.ViewMail(currentMail[0]);
				UpdateMailPopUpWithUID(currentMail[0]);
				currentMail.RemoveAt(0);
			}
			else if (Player.instance.activeMail.Count - 1 >= currentMailIndex)
			{
				UpdateMailPopUpWithUID(Player.instance.activeMail[currentMailIndex].uid);
			}
			else
			{
				mailPopUpTitle.text = string.Empty;
				mailPopUpDescr.text = Language.Get("NOEMAILS", "MENUS,GUI");
				mailPopUpSignature.text = string.Empty;
			}
			antennaLight.gameObject.SetActive(true);
			antennaTail.gameObject.SetActive(true);
			antennaLight.SetSprite("gui_communicator_antenna_green");
			antennaTail.SetSprite("gui_communicator_mail_tail");
			mailPopUpAnimator.Play("communicator_emailpopup_intro");
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_On");
			UpdateMail();
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowopen", GFAudioManager.SFX_GROUP, string.Empty);
			cityController.pinchZoomer.allowPan = false;
		}
		else
		{
			OnClickCloseMail();
		}
	}

	private void OnClickMailLeft()
	{
		currentMailIndex--;
		if (currentMailIndex < 0)
		{
			currentMailIndex = Mathf.Max(0, Player.instance.activeMail.Count - 1);
		}
		if (Player.instance.activeMail.Count - 1 >= currentMailIndex)
		{
			int uid = Player.instance.activeMail[currentMailIndex].uid;
			MailData mailData = Player.instance.GetMailData(uid);
			if (!mailData.viewed)
			{
				Player.instance.ViewMail(uid);
			}
			UpdateMailPopUpWithUID(uid);
		}
		UpdateMail();
	}

	private void OnClickMailRight()
	{
		currentMailIndex++;
		if (currentMailIndex > Player.instance.activeMail.Count - 1)
		{
			currentMailIndex = 0;
		}
		if (currentMailIndex < Player.instance.activeMail.Count)
		{
			int uid = Player.instance.activeMail[currentMailIndex].uid;
			MailData mailData = Player.instance.GetMailData(uid);
			if (!mailData.viewed)
			{
				Player.instance.ViewMail(uid);
			}
			UpdateMailPopUpWithUID(uid);
		}
		UpdateMail();
	}

	private void OnClickCloseMail()
	{
		mailPopUpAnimator.Play("communicator_emailpopup_Outro");
		antennaLight.gameObject.SetActive(false);
		antennaTail.gameObject.SetActive(false);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowclose", GFAudioManager.SFX_GROUP, string.Empty);
		cityController.pinchZoomer.allowPan = true;
		if (addedMailMission)
		{
			addedMailMission = false;
			ActivateMissions();
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(addedMailMissionUid);
			if (mission != null)
			{
				ShowMissionsPopup(mission.missionLocationCity, addedMailMissionUid);
			}
			else
			{
				OnClickMissions();
			}
			addedMailMissionUid = -1;
		}
	}

	private void OnClickUnPin()
	{
	}

	private void OnDownLeft()
	{
		if (!inFlashSaleMode)
		{
			leftBtnSprite.SetSprite("gui_communicator_button_left_press");
		}
	}

	private void OnUpLeft()
	{
		if (!inFlashSaleMode)
		{
			leftBtnSprite.SetSprite("gui_communicator_button_left");
		}
	}

	private void OnDownRight()
	{
		if (!inFlashSaleMode)
		{
			rightBtnSprite.SetSprite("gui_communicator_button_right_press");
		}
	}

	private void OnUpRight()
	{
		if (!inFlashSaleMode)
		{
			rightBtnSprite.SetSprite("gui_communicator_button_right");
		}
	}

	private void OnClickLight()
	{
		if (antennaLight.CurrentSprite.name.Equals("gui_communicator_antenna_purple") && antennaLight.gameObject.GetComponent<Animator>().IsName("Communicator_Antenna_Light_Blink"))
		{
			ActivateMissions();
			OnClickMissions();
		}
		if (antennaLight.CurrentSprite.name.Equals("gui_communicator_antenna_green") && antennaLight.gameObject.GetComponent<Animator>().IsName("Communicator_Antenna_Light_Blink"))
		{
			ActivateMail();
			OnClickMail();
		}
	}

	private void OnClickLeft()
	{
		cityController.cityCharacter.StopMove();
		if (inFlashSaleMode)
		{
			return;
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_screenswitchbutton", GFAudioManager.SFX_GROUP, string.Empty);
		if (dayNight.activeSelf)
		{
			ActivateMail();
		}
		else if (mail.activeSelf)
		{
			ActivateMissions();
		}
		else if (missions.activeSelf)
		{
			if (Player.instance.HasTriggered("EnteredLegionOfDoom"))
			{
				ActivateLegionOfDoom();
			}
			else
			{
				ActivateDayNightView();
			}
		}
		else if (legionOfDoom.activeSelf)
		{
			ActivateDayNightView();
		}
	}

	private void OnClickRight()
	{
		cityController.cityCharacter.StopMove();
		if (inFlashSaleMode)
		{
			return;
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_screenswitchbutton", GFAudioManager.SFX_GROUP, string.Empty);
		if (dayNight.activeSelf)
		{
			if (Player.instance.HasTriggered("EnteredLegionOfDoom"))
			{
				ActivateLegionOfDoom();
			}
			else
			{
				ActivateMissions();
			}
		}
		else if (missions.activeSelf)
		{
			ActivateMail();
		}
		else if (mail.activeSelf)
		{
			ActivateDayNightView();
		}
		else if (legionOfDoom.activeSelf)
		{
			ActivateMissions();
		}
	}

	public void ForceShowLegionOfDoom()
	{
		ActivateLegionOfDoom();
		OnClickLegionOfDoom();
	}

	private void OnClickLegionOfDoom()
	{
		cityController.cityCharacter.StopMove();
		if (!lodCityMenu.gameObject.activeSelf || lodCityMenu.mainMenuAnimator.IsName("communicator_emailpopup_Outro"))
		{
			lodCityMenu.gameObject.SetActive(true);
			lodCityMenu.Init();
			antennaTail.gameObject.SetActive(true);
			antennaTail.SetSprite("gui_communicator_red_tail");
			antennaLight.gameObject.SetActive(true);
			antennaLight.SetSprite("gui_communicator_antenna_red");
			antennaLight.gameObject.GetComponent<Animator>().Play("Communicator_Antenna_Light_On");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowopen", GFAudioManager.SFX_GROUP, string.Empty);
			cityController.pinchZoomer.allowPan = false;
		}
		else
		{
			lodCityMenu.Hide(true);
		}
	}

	private void HandleEnterLODBtnClicked()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}

	private void HandleExitLODPopupBtnClicked()
	{
		if (lodCityMenu.gameObject.activeInHierarchy)
		{
			antennaLight.gameObject.SetActive(false);
			antennaTail.gameObject.SetActive(false);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_communicator_windowclose", GFAudioManager.SFX_GROUP, string.Empty);
			cityController.pinchZoomer.allowPan = true;
		}
	}

	private void HandlePvpRequestsUpdated()
	{
		int pendingRequestCount = Player.instance.pvpData.PendingRequestCount;
		if (pendingRequestCount > 0)
		{
			inviteCountTextMesh.gameObject.SetActive(true);
			inviteCountTextMesh.text = pendingRequestCount.ToString();
			multiplayerTextMesh.text = ((pendingRequestCount <= 1) ? Language.Get("INVITE", "MENUS,GUI") : Language.Get("INVITES", "MENUS,GUI"));
		}
		else
		{
			inviteCountTextMesh.gameObject.SetActive(false);
			multiplayerTextMesh.text = Language.Get("MULTIPLAYER", "MENUS,GUI");
		}
	}

	private void HandleFriendRequestAcceptedDeclined(string playerId)
	{
		HandlePvpRequestsUpdated();
	}

	private void HandleTriggerSet(string trigger)
	{
		UpdateMail();
		CheckForMailEvents();
	}

	public bool MissionScrollerActive()
	{
		if (missionsPopUp.activeInHierarchy && (!missionsPopUpAnimator.IsName("communicator_missionpopup_Outro") || (missionsPopUpAnimator.IsName("communicator_missionpopup_Outro") && missionsPopUpAnimator.NormalizedTime() < 0.9f)))
		{
			return true;
		}
		return false;
	}

	public bool MailScrollerActive()
	{
		if (mailPopUp.activeInHierarchy && (!mailPopUpAnimator.IsName("communicator_emailpopup_Outro") || (mailPopUpAnimator.IsName("communicator_emailpopup_Outro") && mailPopUpAnimator.NormalizedTime() < 0.9f)))
		{
			return true;
		}
		return false;
	}

	public bool LODCityMenuActive()
	{
		return lodCityMenu.gameObject.activeInHierarchy && !lodCityMenu.mainMenuAnimator.IsName("communicator_emailpopup_Outro");
	}

	private void HandleLanguageChanged()
	{
		UpdateMail();
		CheckForCompletedMissions();
	}

	public int GetCurrentDay()
	{
		return currentDay;
	}

	public int GetCurrentTime()
	{
		return currentTime;
	}
}
