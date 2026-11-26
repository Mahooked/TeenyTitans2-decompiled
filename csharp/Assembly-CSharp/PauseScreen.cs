using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseScreen : MonoBehaviour
{
	public static bool isDebugVisible;

	private const int debugPressCountRequired = 10;

	private int debugPressCount;

	private const float debugPressResetDelay = 0.65f;

	private float debugPressResetTimer;

	public tk2dSprite blackFade;

	public tk2dUIItem homeButton;

	public tk2dUIItem playButton;

	[FormerlySerializedAs("retryButton")]
	public tk2dUIItem classChartButton;

	public tk2dUIItem titleButton;

	public SimpleTextButton killPlayerButton;

	public SimpleTextButton killEnemyButton;

	public SimpleTextButton playerVictoryButton;

	public SimpleTextButton enemyVictoryButton;

	public SimpleTextButton playerAIToggleButton;

	public SimpleTextButton enemyAIToggleButton;

	public SimpleTextButton batteryModePauseToggleButton;

	public SimpleTextButton batteryDepleteWhileActiveToggle;

	public SimpleTextButton batteryAutoChargeWhileActiveToggle;

	public SimpleTextButton batteryFullyChargeButton;

	public SimpleTextButton chatToggleButton;

	public SimpleTextButton debugToggleButton;

	public tk2dSprite buttonRobinSprite;

	public tk2dSprite buttonBeastBoySprite;

	public tk2dSprite buttonRavenSprite;

	public tk2dSprite buttonStarfireSprite;

	public tk2dSprite buttonCyborgSprite;

	public tk2dUIItem pausedHeaderButton;

	public GameObject increaseTimeOfDayButton;

	public bool showDebugButtonsAtStart;

	public GameObject debugButtonsGroup;

	public GameObject[] debugButtonGroups;

	public float buttonSpacing = 102f;

	public MenuScreen extraMenuItems;

	private bool usedButton;

	private tk2dSprite[] titleButtonSprites;

	public tk2dUIItem debugIncreaseTimeOfDay;

	public tk2dUIItem debugMoreMoney;

	public tk2dUIItem debugUnlockAllFigures;

	public tk2dUIItem debugUnlockAllTunnels;

	public tk2dUIItem debugUnlockAllModChips;

	public tk2dUIItem debugResetTowerTutorials;

	public tk2dUIItem debugLoadPaxSave;

	public tk2dUIItem debugToggleBtn;

	public tk2dUIItem debugUnlockAllAccessories;

	public tk2dUIItem debugUnlockAllRepaints;

	public tk2dUIItem debugAddRainbowCoins;

	public tk2dUIItem debugMartianShop;

	public tk2dUIItem debugUploadMissionLog;

	public GameObject debugMissionLog;

	public tk2dUIItem debugLegionOfDoom;

	public tk2dUIItem debugTriggerLODBuildingIntro;

	public tk2dUIItem debugCompleteTutorials;

	public TextMeshPro currentTutorialText;

	[SerializeField]
	[FormerlySerializedAs("showHomeButton")]
	private bool _showHomeButton = true;

	[SerializeField]
	[FormerlySerializedAs("showPlayButton")]
	private bool _showPlayButton = true;

	[SerializeField]
	[FormerlySerializedAs("_showRetryButton")]
	private bool _showClassChartButton = true;

	[SerializeField]
	private bool _showTitleButton;

	private bool _allowTitleButtonUse = true;

	public bool showHomeButton
	{
		get
		{
			return _showHomeButton;
		}
		set
		{
			if (value != _showHomeButton)
			{
				_showHomeButton = value;
				RefreshButtons();
			}
		}
	}

	public bool showPlayButton
	{
		get
		{
			return _showPlayButton;
		}
		set
		{
			if (value != _showPlayButton)
			{
				_showPlayButton = value;
				RefreshButtons();
			}
		}
	}

	public bool showClassChartButton
	{
		get
		{
			return _showClassChartButton;
		}
		set
		{
			if (value != _showClassChartButton)
			{
				_showClassChartButton = value;
				RefreshButtons();
			}
		}
	}

	public bool showTitleButton
	{
		get
		{
			return _showTitleButton;
		}
		set
		{
			if (value != _showTitleButton)
			{
				_showTitleButton = value;
				RefreshButtons();
			}
		}
	}

	public bool allowTitleButtonUse => _allowTitleButtonUse;

	public event Action HomeClicked;

	public event Action PlayClicked;

	public event Action ClassChartClicked;

	public event Action TitleClicked;

	private void Awake()
	{
		if (!showHomeButton)
		{
			homeButton.gameObject.SetActive(value: false);
		}
		if (!showPlayButton)
		{
			playButton.gameObject.SetActive(value: false);
		}
		if (!showClassChartButton)
		{
			classChartButton.gameObject.SetActive(value: false);
		}
		if (!showTitleButton && titleButton != null)
		{
			titleButton.gameObject.SetActive(value: false);
		}
		if (debugToggleButton != null)
		{
		}
		if (debugToggleBtn != null)
		{
		}
		if (debugButtonsGroup != null)
		{
			debugButtonsGroup.SetActive(showDebugButtonsAtStart && isDebugVisible);
		}
		for (int i = 0; i < debugButtonGroups.Length; i++)
		{
			if (debugButtonGroups[i] != null)
			{
				debugButtonGroups[i].SetActive(showDebugButtonsAtStart && isDebugVisible);
			}
		}
		RegisterDebugButtonCallbacks();
	}

	private void OnDestroy()
	{
		HomeClicked = null;
		PlayClicked = null;
		ClassChartClicked = null;
		TitleClicked = null;
		UnRegisterDebugButtonCallbacks();
	}

	private IEnumerator Start()
	{
		if (titleButton != null)
		{
			titleButtonSprites = titleButton.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
		}
		yield break;
	}

	private void OnEnable()
	{
		homeButton.OnClick += HandleOnHomeClick;
		playButton.OnClick += HandleOnPlayClick;
		classChartButton.OnClick += HandleOnClassChartClick;
		if (TitleScreenController.instance != null)
		{
			titleButton.OnClick += HandleOnTitleClick;
		}
		extraMenuItems.SetScreenActive(isActive: true);
		RefreshButtons();
		if (TutorialManager.instance != null)
		{
			TutorialManager.instance.ActiveTutorialChanged += HandleActiveTutorialChanged;
			HandleActiveTutorialChanged(TutorialManager.instance.activeTutorial);
		}
	}

	private void OnDisable()
	{
		homeButton.OnClick -= HandleOnHomeClick;
		playButton.OnClick -= HandleOnPlayClick;
		classChartButton.OnClick -= HandleOnClassChartClick;
		if (TitleScreenController.instance != null)
		{
			titleButton.OnClick -= HandleOnTitleClick;
		}
		extraMenuItems.SetScreenActive(isActive: false);
		if (TutorialManager.instance != null)
		{
			TutorialManager.instance.ActiveTutorialChanged -= HandleActiveTutorialChanged;
		}
	}

	private void Update()
	{
		if (debugPressResetTimer > 0f)
		{
			debugPressResetTimer -= Time.unscaledDeltaTime;
			if (debugPressResetTimer <= 0f)
			{
				debugPressCount = 0;
				Debug.Log("Debug Press Timer reached 0. Resetting the debug press count.");
			}
		}
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		if (mousePosition.x >= (float)Screen.width * 0.85f && mousePosition.y <= (float)Screen.height * 0.1f)
		{
			debugPressCount++;
			Debug.Log("Tapped in Corner. Incrementing debug press count to " + debugPressCount);
			if (debugPressCount >= 10)
			{
				isDebugVisible = !isDebugVisible;
				debugPressCount = 0;
				debugPressResetTimer = 0f;
				if (debugToggleButton != null)
				{
					debugToggleButton.gameObject.SetActive(isDebugVisible);
				}
				else if (debugToggleBtn != null)
				{
					debugToggleBtn.gameObject.SetActive(isDebugVisible);
				}
				ShowPauseDebugButtons(show: false);
			}
			else
			{
				debugPressResetTimer = 0.65f;
			}
		}
		else
		{
			debugPressCount = 0;
			debugPressResetTimer = 0f;
		}
	}

	private void RefreshButtons()
	{
		int num = 0;
		if (showHomeButton)
		{
			num++;
		}
		if (showPlayButton)
		{
			num++;
		}
		if (showClassChartButton)
		{
			num++;
		}
		if (showTitleButton)
		{
			num++;
		}
		Vector3 localPosition = new Vector3(buttonSpacing * (float)(num - 1) * -0.5f, 0f, -6f);
		if (showHomeButton)
		{
			homeButton.gameObject.SetActive(value: true);
			homeButton.transform.localPosition = localPosition;
			localPosition.x += buttonSpacing;
		}
		else
		{
			homeButton.gameObject.SetActive(value: false);
		}
		if (showPlayButton)
		{
			playButton.gameObject.SetActive(value: true);
			playButton.transform.localPosition = localPosition;
			localPosition.x += buttonSpacing;
		}
		else
		{
			playButton.gameObject.SetActive(value: false);
		}
		if (showClassChartButton)
		{
			classChartButton.gameObject.SetActive(value: true);
			classChartButton.transform.localPosition = localPosition;
			localPosition.x += buttonSpacing;
		}
		else
		{
			classChartButton.gameObject.SetActive(value: false);
		}
		if (showTitleButton && titleButton != null)
		{
			titleButton.gameObject.SetActive(value: true);
			titleButton.transform.localPosition = localPosition;
			localPosition.x += buttonSpacing;
		}
		else if (titleButton != null)
		{
			titleButton.gameObject.SetActive(value: false);
		}
	}

	private void PausedHeaderButton_OnClick()
	{
	}

	public void ShowPauseDebugButtons(bool show)
	{
		if (debugButtonsGroup != null)
		{
			debugButtonsGroup.SetActive(show);
		}
		for (int i = 0; i < debugButtonGroups.Length; i++)
		{
			if (debugButtonGroups[i] != null)
			{
				debugButtonGroups[i].SetActive(show);
			}
		}
	}

	public void SetTitleButtonAllowed(bool allowed)
	{
		if (titleButton != null && titleButtonSprites == null)
		{
			titleButtonSprites = titleButton.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
		}
		if (titleButtonSprites == null)
		{
			return;
		}
		_allowTitleButtonUse = allowed;
		if (_allowTitleButtonUse)
		{
			for (int i = 0; i < titleButtonSprites.Length; i++)
			{
				titleButtonSprites[i].color = Color.white;
			}
		}
		else
		{
			for (int j = 0; j < titleButtonSprites.Length; j++)
			{
				titleButtonSprites[j].color = Color.gray;
			}
		}
	}

	private void HandleOnHomeClick()
	{
		if (!usedButton)
		{
			if (BattleController.instance != null && BattleController.battleDefinition.type == BattleType.Tournament)
			{
				StartCoroutine(ShowTournamentExitWarningDialog());
			}
			else if (BattleController.instance != null && BattleController.battleDefinition.type == BattleType.Challenge && !ChallengeModeManager.isNonJLChallenge)
			{
				usedButton = true;
				OnHomeClicked();
				PauseManager.instance.Resume();
				SaveUtility.SetInt("GAME_CHALLENGE_CONSECUTIVE_WINS", 0);
				LoadingScreenCover.LoadScene("Challenge");
			}
			else if (BattleController.instance != null && (BattleController.battleDefinition.type == BattleType.Pvp || BattleController.battleDefinition.type == BattleType.PvpCpu || BattleController.battleDefinition.type == BattleType.PvpFriend))
			{
				usedButton = true;
				OnHomeClicked();
				PauseManager.instance.Resume();
				TT2NetManager.Instance.LeaveRoom();
				TT2NetMatch.Instance.ResetMatch();
				LoadingScreenCover.LoadScene("LegionOfDoomMode");
			}
			else
			{
				usedButton = true;
				OnHomeClicked();
				Communicator.afterBattleConvoName = string.Empty;
				PauseManager.instance.Resume();
				LoadingScreenCover.LoadScene(CityController.previousCityScene);
			}
		}
	}

	private IEnumerator ShowTournamentExitWarningDialog()
	{
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default, isActive: false);
		dialog.transform.parent = transform.parent;
		dialog.transform.localPosition = new Vector3(0f, 0f, -980f);
		dialog.descriptionText.text = Language.Get("TOURNYEXITWARNING", "MENUS,GUI");
		dialog.gameObject.SetActive(value: true);
		yield return new WaitForInactive(dialog.gameObject);
		if (dialog.isConfirmed)
		{
			usedButton = true;
			OnHomeClicked();
			TournamentTracker.FinishTournament();
			Communicator.afterBattleConvoName = string.Empty;
			PauseManager.instance.Resume();
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
	}

	public void HandleOnPlayClick()
	{
		OnPlayClicked();
		PauseManager.instance.Resume();
		gameObject.SetActive(value: false);
	}

	private void HandleOnClassChartClick()
	{
		OnClassChartClicked();
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/ClassChart").transform;
		transform.parent = base.transform.parent;
		transform.localPosition = new Vector3(0f, 0f, -600f);
	}

	private void HandleOnTitleClick()
	{
		if (_allowTitleButtonUse)
		{
			OnTitleClicked();
			TT2NetChat.Instance.Disconnect();
			PauseManager.instance.Resume();
			gameObject.SetActive(value: false);
			if (TitleScreenController.instance != null)
			{
				TitleScreenController.instance.ShowTitleScreen();
			}
		}
	}

	public void HandleOnGothamClicked()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		CityController.startFromNodeNamed = "PathNode_BridgeStart (1)";
		PauseManager.instance.Resume();
		LoadingScreenCover.LoadScene("City_Gotham");
	}

	public void HandleOnMetropolisClicked()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		CityController.startFromNodeNamed = "PathNode_MetroExit";
		PauseManager.instance.Resume();
		LoadingScreenCover.LoadScene("City_Metropolis");
	}

	public void HandleOnJumpCityClicked()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		CityController.startFromNodeNamed = "TT2_NewStartNode";
		PauseManager.instance.Resume();
		LoadingScreenCover.LoadScene("City_JumpCityNew");
	}

	protected void OnHomeClicked()
	{
		if (HomeClicked != null)
		{
			HomeClicked();
		}
	}

	protected void OnPlayClicked()
	{
		if (PlayClicked != null)
		{
			PlayClicked();
		}
	}

	protected void OnClassChartClicked()
	{
		if (ClassChartClicked != null)
		{
			ClassChartClicked();
		}
	}

	protected void OnTitleClicked()
	{
		if (TitleClicked != null)
		{
			TitleClicked();
		}
	}

	private void RegisterDebugButtonCallbacks()
	{
		if (CityController.instance != null)
		{
			if (debugIncreaseTimeOfDay != null)
			{
				debugIncreaseTimeOfDay.OnClick += CityController.instance.communicator.DebugIncreaseTimeOfDay;
			}
			if (debugMoreMoney != null)
			{
				debugMoreMoney.OnClick += CityController.instance.walletGUI.Add200Coins;
			}
			if (debugUnlockAllFigures != null)
			{
				debugUnlockAllFigures.OnClick += CityController.instance.AcquireAllCharacters;
			}
			if (debugUnlockAllTunnels != null)
			{
				debugUnlockAllTunnels.OnClick += CityController.instance.UnlockAllTunnels;
			}
			if (debugUnlockAllModChips != null)
			{
				debugUnlockAllModChips.OnClick += CityController.instance.UnlockAllModChips;
			}
			if (debugResetTowerTutorials != null)
			{
				debugResetTowerTutorials.OnClick += CityController.instance.ResetTowerTutorials;
			}
			if (debugLoadPaxSave != null)
			{
				debugLoadPaxSave.OnClick += CityController.instance.LoadPaxSave;
			}
			if (debugToggleBtn != null)
			{
				debugToggleBtn.OnClick += CityController.instance.ToggleDebugButtons;
			}
			if (debugUnlockAllAccessories != null)
			{
				debugUnlockAllAccessories.OnClick += CityController.instance.UnlockAllAccessories;
			}
			if (debugUnlockAllRepaints != null)
			{
				debugUnlockAllRepaints.OnClick += CityController.instance.UnlockAllRepaints;
			}
			if (debugAddRainbowCoins != null)
			{
				debugAddRainbowCoins.OnClick += Add10RainbowCoins;
			}
			if (debugMartianShop != null)
			{
				debugMartianShop.OnClick += HandleDebugOpenMartianShop;
			}
			if (debugUploadMissionLog != null)
			{
				debugUploadMissionLog.OnClick += HandleDebugUploadMissionLog;
			}
			if (debugLegionOfDoom != null)
			{
				debugLegionOfDoom.OnClick += HandleDebugLegionOfDoom;
			}
			if (debugTriggerLODBuildingIntro != null)
			{
				debugTriggerLODBuildingIntro.OnClick += HandleLODBuildingIntroTriggered;
			}
			if (debugCompleteTutorials != null)
			{
				debugCompleteTutorials.OnClick += HandleCompleteTutorialsClicked;
			}
		}
	}

	private void UnRegisterDebugButtonCallbacks()
	{
		if (CityController.instance != null)
		{
			if (debugIncreaseTimeOfDay != null)
			{
				debugIncreaseTimeOfDay.OnClick -= CityController.instance.communicator.DebugIncreaseTimeOfDay;
			}
			if (debugMoreMoney != null)
			{
				debugMoreMoney.OnClick -= CityController.instance.walletGUI.Add200Coins;
			}
			if (debugUnlockAllFigures != null)
			{
				debugUnlockAllFigures.OnClick -= CityController.instance.AcquireAllCharacters;
			}
			if (debugUnlockAllTunnels != null)
			{
				debugUnlockAllTunnels.OnClick -= CityController.instance.UnlockAllTunnels;
			}
			if (debugUnlockAllModChips != null)
			{
				debugUnlockAllModChips.OnClick -= CityController.instance.UnlockAllModChips;
			}
			if (debugResetTowerTutorials != null)
			{
				debugResetTowerTutorials.OnClick -= CityController.instance.ResetTowerTutorials;
			}
			if (debugLoadPaxSave != null)
			{
				debugLoadPaxSave.OnClick -= CityController.instance.LoadPaxSave;
			}
			if (debugToggleBtn != null)
			{
				debugToggleBtn.OnClick -= CityController.instance.ToggleDebugButtons;
			}
			if (debugUnlockAllAccessories != null)
			{
				debugUnlockAllAccessories.OnClick -= CityController.instance.UnlockAllAccessories;
			}
			if (debugUnlockAllRepaints != null)
			{
				debugUnlockAllRepaints.OnClick -= CityController.instance.UnlockAllRepaints;
			}
			if (debugAddRainbowCoins != null)
			{
				debugAddRainbowCoins.OnClick -= Add10RainbowCoins;
			}
			if (debugMartianShop != null)
			{
				debugMartianShop.OnClick -= HandleDebugOpenMartianShop;
			}
			if (debugUploadMissionLog != null)
			{
				debugUploadMissionLog.OnClick -= HandleDebugUploadMissionLog;
			}
			if (debugLegionOfDoom != null)
			{
				debugLegionOfDoom.OnClick -= HandleDebugLegionOfDoom;
			}
			if (debugTriggerLODBuildingIntro != null)
			{
				debugTriggerLODBuildingIntro.OnClick -= HandleLODBuildingIntroTriggered;
			}
			if (debugCompleteTutorials != null)
			{
				debugCompleteTutorials.OnClick -= HandleCompleteTutorialsClicked;
			}
		}
	}

	private void Add10RainbowCoins()
	{
		Player.instance.AddRainbowCurrency(10);
	}

	private void HandleDebugUploadMissionLog()
	{
		debugMissionLog.gameObject.SetActive(value: true);
	}

	private void HandleDebugOpenMartianShop()
	{
		HandleOnPlayClick();
		EventMessenger.Instance.Raise(new EnterShopEvent(GameManager.instance.shopDatabase.GetShopDef(20)));
	}

	private void HandleDebugLegionOfDoom()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		CityController.startFromNodeNamed = "TT2_NewStartNode";
		PauseManager.instance.Resume();
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}

	private void HandleLODBuildingIntroTriggered()
	{
		int missionUID = 101;
		if (Player.instance.HasActiveMission(missionUID))
		{
			Player.instance.CompleteMission(missionUID, autoCollect: true);
		}
	}

	private void HandleCompleteTutorialsClicked()
	{
		Debug.Log("Pause Screen, Complete All Tutorials");
		if (CoreGameTutorial.instance != null)
		{
			CoreGameTutorial.instance.CompleteAllTutorials();
		}
		if (TutorialManager.instance != null)
		{
			TutorialManager.instance.CompleteAllTutorials();
		}
		Player.instance.SetTrigger("PLAYER_PLAYED_INTRO_SEQUENCE");
		CharacterDefinition characterDefinition = CityCharacter.characterType switch
		{
			CityCharacterType.Robin => GameManager.instance.characterDatabase.GetCharacter(93), 
			CityCharacterType.BeastBoy => GameManager.instance.characterDatabase.GetCharacter(94), 
			CityCharacterType.Raven => GameManager.instance.characterDatabase.GetCharacter(98), 
			CityCharacterType.Starfire => GameManager.instance.characterDatabase.GetCharacter(99), 
			CityCharacterType.Cyborg => GameManager.instance.characterDatabase.GetCharacter(97), 
			_ => throw new ArgumentOutOfRangeException(), 
		};
		if (characterDefinition != null && !Player.instance.HasCharacter(characterDefinition.uid))
		{
			Player.instance.AddCharacter(characterDefinition);
			Player.instance.AcquireCharacter(characterDefinition.uid);
			Player.instance.AddToParty(Player.instance.characters.GetLastItem());
		}
		Player.instance.SilentlyCompleteMission(4);
		Player.instance.AddMission(101);
		PauseManager.instance.Resume();
		LoadingScreenCover.LoadScene(CityController.previousCityScene);
	}

	private void HandleActiveTutorialChanged(Tutorial tutorial)
	{
		if (currentTutorialText != null)
		{
			currentTutorialText.text = string.Format("Current Tut: {0}", (!(tutorial != null)) ? "NULL" : tutorial.name);
		}
	}
}
