using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
	private class BoundsRay
	{
		public string name;

		public Ray ray;

		public float distToHitBounds;

		public float distToCamera;

		public BoundsRay(string newName, Ray newRay)
		{
			name = newName;
			ray = newRay;
			distToHitBounds = 0f;
			distToCamera = 0f;
		}
	}

	private static bool didShowInterstitial;

	public static bool shouldShowTitleScreen;

	public static TitleScreenController instance;

	public MenuScreen activeMenuScreen;

	public MenuScreen titleScreenMenu;

	public MenuScreen settingsMenu;

	public MenuScreen creditsScreen;

	public MenuScreen gameSettingsMenu;

	public MenuScreen unableToConnectAlert;

	public TextMeshPro languageTextMesh;

	public tk2dUIItem backButton;

	public GameObject[] upperRightGameUI;

	public GameObject termsOfServiceBtn;

	public GameObject privacyPolicyBtn;

	public List<LanguageCode> languages = new List<LanguageCode>();

	public float moveSpeed = 0.33f;

	public float turnSpeedDegrees = 22f;

	[Tooltip("When the camera is moving around the current district, this is how far 'in' bounds the camera will stay. This needs to be greater than 0.")]
	public float innerOffset = 3f;

	public MediaPlayerCtrl videoManager;

	public CaptionsManager captionsManager;

	public GameObject videoGUICamera;

	public tk2dUIItem skipButton;

	public TextMeshPro skipText;

	public string cinematicPart1Clip;

	public string cinematicPart2Clip;

	public VideoCaption cinematicPart1CaptionAsset;

	public VideoCaption cinematicPart2CaptionAsset;

	public GameObject mainMenuTouchBlocker;

	public AudioClip titleMusic;

	public AudioClip titleIntroSfx;

	public AudioClip titleGOSfx;

	public AudioClip[] titleVOClips;

	public PopupDialog exitGamePopup;

	public AchievementScreen achievementScreen;

	private Vector3 curDirection;

	private Vector3 targetPosition;

	private bool cameraMoving;

	private Transform cameraHolder;

	private float originalApproachFactor;

	private List<MenuScreen> menuObjects = new List<MenuScreen>();

	private int currentLanguageIndex = -1;

	private bool hidingTitleScreen;

	private bool preLoadedMoreGames;

	private bool exitPopUpShowing;

	private bool isVideoDone;

	private bool _titleScreenActive;

	public bool titleScreenActive => _titleScreenActive;

	public event Action LanguageChanged;

	public event Action AudioMutingChanged;

	public event Action TitleScreenClosed;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (titleScreenMenu != null)
		{
			menuObjects.Add(titleScreenMenu);
		}
		menuObjects.Add(settingsMenu);
		menuObjects.Add(unableToConnectAlert);
		menuObjects.Add(creditsScreen);
		menuObjects.Add(gameSettingsMenu);
		DisableAllMenuObjects();
		backButton.gameObject.SetActive(value: false);
		currentLanguageIndex = GetIndexForLanguage(Language.CurrentLanguage());
		mainMenuTouchBlocker.SetActive(value: false);
		MediaPlayerCtrl mediaPlayerCtrl = videoManager;
		mediaPlayerCtrl.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Combine(mediaPlayerCtrl.OnEnd, new MediaPlayerCtrl.VideoEnd(OnVideoEnd));
	}

	private void OnDestroy()
	{
		MediaPlayerCtrl mediaPlayerCtrl = videoManager;
		mediaPlayerCtrl.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Remove(mediaPlayerCtrl.OnEnd, new MediaPlayerCtrl.VideoEnd(OnVideoEnd));
		instance = null;
		LanguageChanged = null;
		AudioMutingChanged = null;
		TitleScreenClosed = null;
	}

	private void Start()
	{
		if (shouldShowTitleScreen)
		{
			ShowTitleScreen(!CityController.instance.isInterior, cameFromLaunchScreen: true);
		}
		else
		{
			_titleScreenActive = false;
		}
	}

	public void ShowTitleScreen(bool setRandomCameraPos = false, bool cameFromLaunchScreen = false)
	{
		_titleScreenActive = true;
		StartCoroutine(ShowTitleScreenSequence(setRandomCameraPos, cameFromLaunchScreen));
	}

	private IEnumerator ShowTitleScreenSequence(bool setRandomCameraPos, bool cameFromLaunchScreen)
	{
		yield return new WaitForEndOfFrame();
		shouldShowTitleScreen = false;
		cameraHolder = CityController.instance.cameraHolder;
		PauseManager.instance.Resume();
		SwipeManager.Instance.enabled = false;
		CityController.instance.zoomType = ZoomType.TitleScreen;
		originalApproachFactor = CityController.instance.pinchZoomer.approachFactor;
		CityController.instance.pinchZoomer.approachFactor = 1f;
		CityController.instance.pinchZoomer.ZoomOut();
		CityController.instance.pinchZoomer.ZoomIn(20f);
		CityController.instance.pinchZoomer.allowPinchZoom = false;
		CityController.instance.viewDeobstructer.paused = true;
		CityController.instance.viewDeobstructer.ResetAllObstructionsNow();
		cameraHolder.GetComponent<CameraFollow>().enabled = false;
		backButton.gameObject.SetActive(value: false);
		SetGameUIActive(active: false);
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		if (!Player.instance.HasTriggered("PLAYER_PLAYED_INTRO_SEQUENCE"))
		{
			Debug.Log("Disable Intro Button, you haven't done intro tutorial yet...");
			TitleScreen component = titleScreenMenu.GetComponent<TitleScreen>();
			component.introButton.enabled = false;
			tk2dSprite[] componentsInChildren = component.introButton.GetComponentsInChildren<tk2dSprite>();
			int num = componentsInChildren.Length;
			for (int i = 0; i < num; i++)
			{
				componentsInChildren[i].color = Color.clear;
			}
		}
		ActivateMenuObject(titleScreenMenu);
		yield return new WaitForEndOfFrame();
		innerOffset = Mathf.Max(innerOffset, 0.1f);
		if (setRandomCameraPos)
		{
			Bounds bounds = CityController.instance.CurrentCityLocationBounds.bounds;
			CityController.instance.cameraHolder.position = new Vector3(UnityEngine.Random.Range(bounds.min.x + innerOffset, bounds.max.x - innerOffset), 0f, UnityEngine.Random.Range(bounds.min.z + innerOffset, bounds.max.z - innerOffset));
		}
		CityController.instance.cityCharacter.StopMove();
		CityController.instance.PlayMusic(titleMusic, crossFade: true);
		GFAudioManager.instance.PlayClip(titleIntroSfx, 1f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		if (!CityController.instance.isInterior)
		{
			MoveCameraToRandomPos(isInitialMove: true);
		}
		yield return new WaitForSeconds(0.15f);
		GFAudioManager.instance.PlayClip(titleVOClips[UnityEngine.Random.Range(0, titleVOClips.Length)], 1f, loop: false, 0f, 128, GFAudioManager.VOICEOVER_GROUP);
		if (!didShowInterstitial)
		{
			didShowInterstitial = true;
			gameObject.AddComponent<InterstitialOnPlay>();
		}
		if (PlayerPrefs.GetInt("AcceptedIAP", 0) == 0)
		{
			GameLinkPopupDialog.Show(Language.Get("IAP_POPUP_FIRSTLAUNCH_HEADER", "MENUS,GUI"), Language.Get("IAP_POPUP_FIRSTLAUNCH_MESSAGE", "MENUS,GUI"), Language.Get("IAP_POPUP_FIRSTLAUNCH_BUTTON_OKAY", "MENUS,GUI"), "Prefabs/GUI/RiddlerShopPopupDialog");
			yield return new WaitWhile(() => GameLinkPopupDialog.Instance != null);
			PlayerPrefs.SetInt("AcceptedIAP", 1);
			PlayerPrefs.Save();
		}
	}

	public void HideTitleScreen()
	{
		if (!hidingTitleScreen)
		{
			StartCoroutine("HideTitleScreenSequence");
		}
	}

	private IEnumerator HideTitleScreenSequence()
	{
		hidingTitleScreen = true;
		TitleScreen titleScreen = titleScreenMenu.GetComponent<TitleScreen>();
		titleScreen.PlayOutro();
		GFAudioManager.instance.PlayClip(titleGOSfx, 1f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		yield return new WaitForEndOfFrame();
		while (!titleScreen.IsOutroFinished())
		{
			yield return null;
		}
		if (!Player.instance.HasTriggered("PLAYER_PLAYED_INTRO_SEQUENCE"))
		{
			LoadingScreenCover.LoadScene("NewGameIntro", showLoadingScreen: true, null, showDefaultSkin: true);
			yield break;
		}
		NewGameIntroController.completeAllTutorialsMode = false;
		DisableAllMenuObjects();
		if (CityController.instance != null)
		{
			CityController.instance.zoomType = ZoomType.Normal;
			CityController.instance.pinchZoomer.approachFactor = originalApproachFactor;
			CityController.instance.pinchZoomer.ZoomOut();
			CityController.instance.pinchZoomer.ZoomIn();
			CityController.instance.pinchZoomer.allowPinchZoom = true;
			CityController.instance.viewDeobstructer.paused = false;
			cameraHolder.GetComponent<CameraFollow>().enabled = true;
			CityController.instance.PlayCurrentLocationMusic(crossFade: true);
			CityController.instance.pinchZoomer.CamReachedTargetPos += CameraReachedPlayerStartTutorial;
		}
		cameraMoving = false;
		yield return new WaitForSeconds(1f);
		_titleScreenActive = false;
		if (SaveUtility.GetInt("GAME_PLAYED_INITIAL_DIALOGUE") != 0 && CoreGameTutorial.instance != null && (CoreGameTutorial.instance.GetPreviousTutorialStep() >= CoreGameTutorial.Step.CityTutorialViewMapStart || CoreGameTutorial.instance.GetPreviousTutorialStep() == CoreGameTutorial.Step.None))
		{
			SetGameUIActive(active: true);
		}
		else
		{
			SetGameUIActiveTutorial(active: true);
		}
		SetGameUIActive(active: true);
		backButton.gameObject.SetActive(value: false);
		EventMessenger.Instance.Raise(new RevealPurchasedRewardsEvent());
		if (PlayerRewardManager.instance != null)
		{
			yield return new WaitWhile(() => PlayerRewardManager.instance.isShowingRewards);
		}
		if (SwipeManager.Instance != null)
		{
			SwipeManager.Instance.enabled = true;
		}
		hidingTitleScreen = false;
		if (Player.instance.pvpData.hasConnected)
		{
			ServerSettings.ResetBestRegionCodeInPreferences();
			TT2NetChat.Instance.Connect(Player.instance.pvpData.playerID);
		}
		yield return new WaitForSeconds(1f);
		yield return Resources.UnloadUnusedAssets();
	}

	private void CameraReachedPlayerStartTutorial()
	{
		CityController.instance.pinchZoomer.CamReachedTargetPos -= CameraReachedPlayerStartTutorial;
		if (TitleScreenClosed != null)
		{
			TitleScreenClosed();
		}
	}

	public void ShowMainSettingsMenu()
	{
		ActivateMenuObject(settingsMenu);
		SetBackButtonOnClick(CloseMainSettingsMenu);
	}

	public void ShowGameSettingsMenu()
	{
		UpdateCurrentLanguage();
		ActivateMenuObject(gameSettingsMenu);
		SetBackButtonOnClick(CloseGameSettingsMenu);
	}

	public void ShowCredits()
	{
		ActivateMenuObject(creditsScreen);
		SetBackButtonOnClick(CloseCredits);
	}

	public void ShowAchievementsMenu()
	{
		SocialSystem.ShowAchievementsUI();
		backButton.gameObject.SetActive(value: false);
		termsOfServiceBtn.SetActive(value: false);
		privacyPolicyBtn.SetActive(value: false);
		this.achievementScreen.Init();
		AchievementScreen achievementScreen = this.achievementScreen;
		achievementScreen.Closed = (Action)Delegate.Combine(achievementScreen.Closed, new Action(HandleAchievementScreenClosed));
	}

	private void HandleAchievementScreenClosed()
	{
		AchievementScreen achievementScreen = this.achievementScreen;
		achievementScreen.Closed = (Action)Delegate.Remove(achievementScreen.Closed, new Action(HandleAchievementScreenClosed));
		backButton.gameObject.SetActive(value: true);
		termsOfServiceBtn.SetActive(value: true);
		privacyPolicyBtn.SetActive(value: true);
	}

	public void ShowMoreGamesMenu()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ActivateMenuObject(unableToConnectAlert);
			SetBackButtonOnClick(ShowMainSettingsMenu);
		}
		else
		{
			Debug.Log("********* Show More Games!");
			Debug.Log("********* Show More Games 2, Amazon!");
			SocialSystem.DisplayMoreCNGames();
		}
	}

	public void ResetDataClicked()
	{
		StartCoroutine("ShowResetDataWarning");
	}

	private void ShowTermsOfService()
	{
		SocialSystem.OpenTermsOfUse();
	}

	private void ShowPrivacyPolicy()
	{
		SocialSystem.OpenPrivacyPolicy();
	}

	private void CloseMainSettingsMenu()
	{
		ActivateMenuObject(titleScreenMenu);
		backButton.gameObject.SetActive(value: false);
	}

	private void CloseGameSettingsMenu()
	{
		ActivateMenuObject(settingsMenu);
		SetBackButtonOnClick(CloseMainSettingsMenu);
	}

	private void CloseCredits()
	{
		ActivateMenuObject(settingsMenu);
		SetBackButtonOnClick(CloseMainSettingsMenu);
	}

	private void CloseAchievementsMenu()
	{
	}

	private void CloseMoreGamesMenu()
	{
	}

	private void CloseUnableToConnect()
	{
		ActivateMenuObject(settingsMenu);
		SetBackButtonOnClick(CloseMainSettingsMenu);
	}

	private void ActivateMenuObject(MenuScreen menuObj)
	{
		for (int i = 0; i < menuObjects.Count; i++)
		{
			MenuScreen menuScreen = menuObjects[i];
			if (menuScreen == menuObj)
			{
				menuScreen.SetScreenActive(isActive: true);
			}
			else
			{
				menuScreen.SetScreenActive(isActive: false);
			}
		}
		activeMenuScreen = menuObj;
	}

	private void DisableAllMenuObjects()
	{
		foreach (MenuScreen menuObject in menuObjects)
		{
			menuObject.SetScreenActive(isActive: false);
		}
	}

	private void SetBackButtonOnClick(Action method)
	{
		backButton.gameObject.SetActive(value: true);
		backButton.SendMessageOnClickMethodName = method.Method.Name;
	}

	private void MoveCameraToRandomPos(bool isInitialMove = false)
	{
		float boundsDiagonalLength = CityController.instance.CurrentCityLocationBounds.boundsDiagonalLength;
		List<BoundsRay> list = new List<BoundsRay>();
		Vector3 origin = cameraHolder.transform.position + Vector3.forward * boundsDiagonalLength;
		Vector3 origin2 = cameraHolder.transform.position + (Vector3.forward + Vector3.right) * boundsDiagonalLength;
		Vector3 origin3 = cameraHolder.transform.position + Vector3.right * boundsDiagonalLength;
		Vector3 origin4 = cameraHolder.transform.position + (Vector3.back + Vector3.right) * boundsDiagonalLength;
		Vector3 origin5 = cameraHolder.transform.position + Vector3.back * boundsDiagonalLength;
		Vector3 origin6 = cameraHolder.transform.position + (Vector3.back + Vector3.left) * boundsDiagonalLength;
		Vector3 origin7 = cameraHolder.transform.position + Vector3.left * boundsDiagonalLength;
		Vector3 origin8 = cameraHolder.transform.position + (Vector3.forward + Vector3.left) * boundsDiagonalLength;
		list.Add(new BoundsRay("up", new Ray(origin, Vector3.back)));
		list.Add(new BoundsRay("upRight", new Ray(origin2, Vector3.back + Vector3.left)));
		list.Add(new BoundsRay("right", new Ray(origin3, Vector3.left)));
		list.Add(new BoundsRay("downRight", new Ray(origin4, Vector3.forward + Vector3.left)));
		list.Add(new BoundsRay("down", new Ray(origin5, Vector3.forward)));
		list.Add(new BoundsRay("downLeft", new Ray(origin6, Vector3.forward + Vector3.right)));
		list.Add(new BoundsRay("left", new Ray(origin7, Vector3.right)));
		list.Add(new BoundsRay("upLeft", new Ray(origin8, Vector3.back + Vector3.right)));
		for (int i = 0; i < list.Count; i++)
		{
			BoundsRay boundsRay = list[i];
			CityController.instance.CurrentCityLocationBounds.bounds.IntersectRay(boundsRay.ray, out boundsRay.distToHitBounds);
			boundsRay.distToCamera = Vector3.Distance(boundsRay.ray.GetPoint(boundsRay.distToHitBounds), cameraHolder.position);
		}
		list.Sort((BoundsRay x, BoundsRay y) => y.distToCamera.CompareTo(x.distToCamera));
		BoundsRay boundsRay2 = list[UnityEngine.Random.Range(0, 3)];
		targetPosition = boundsRay2.ray.GetPoint(boundsRay2.distToHitBounds + innerOffset);
		if (isInitialMove)
		{
			curDirection = (targetPosition - cameraHolder.position).normalized;
		}
		cameraMoving = true;
	}

	private void Update()
	{
		if (cameraMoving)
		{
			float num = Vector3.Distance(cameraHolder.position, targetPosition);
			float num2 = moveSpeed * Time.deltaTime;
			if (num2 < num)
			{
				cameraHolder.position += curDirection * num2;
				Vector3 normalized = (targetPosition - cameraHolder.position).normalized;
				curDirection = Vector3.RotateTowards(curDirection, normalized, turnSpeedDegrees * Time.deltaTime * ((float)Math.PI / 180f), 0f);
			}
			else
			{
				cameraHolder.position = targetPosition;
				MoveCameraToRandomPos();
			}
		}
	}

	private void LateUpdate()
	{
		if (titleScreenActive && activeMenuScreen != null && activeMenuScreen == titleScreenMenu && !hidingTitleScreen && !exitPopUpShowing && Input.GetKeyDown(KeyCode.Escape) && Time.time - AndroidBackListener.lastUsed > 0.01f)
		{
			exitPopUpShowing = true;
			exitGamePopup.gameObject.SetActive(value: true);
			exitGamePopup.Confirmed += () =>
			{
				exitPopUpShowing = false;
				Application.Quit();
			};
			exitGamePopup.Canceled += () =>
			{
				exitPopUpShowing = false;
			};
		}
	}

	private void SetGameUIActive(bool active)
	{
		CityController.instance.pauseButton.gameObject.SetActive(active);
		GameObject[] array = upperRightGameUI;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(active);
		}
		tk2dCameraAnchor[] componentsInChildren = CityController.instance.guiCamera2.GetComponentsInChildren<tk2dCameraAnchor>(includeInactive: true);
		foreach (tk2dCameraAnchor tk2dCameraAnchor2 in componentsInChildren)
		{
			tk2dCameraAnchor2.gameObject.SetActive(active);
		}
		Debug.Log("SetGameUIActive:" + active);
		if (CityController.instance.communicator != null)
		{
			CityController.instance.communicator.gameObject.SetActive(active);
		}
	}

	private void SetGameUIActiveTutorial(bool active)
	{
		CityController.instance.pauseButton.gameObject.SetActive(active);
		tk2dCameraAnchor[] componentsInChildren = CityController.instance.guiCamera2.GetComponentsInChildren<tk2dCameraAnchor>(includeInactive: true);
		foreach (tk2dCameraAnchor tk2dCameraAnchor2 in componentsInChildren)
		{
			if (tk2dCameraAnchor2.AnchorPoint != tk2dBaseSprite.Anchor.LowerCenter)
			{
				tk2dCameraAnchor2.gameObject.SetActive(active);
			}
		}
	}

	private IEnumerator ShowResetDataWarning()
	{
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default, isActive: false);
		dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
		dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
		dialog.headerText.text = Language.Get("RESET_DATA_2", "MENUS,GUI");
		dialog.descriptionText.text = Language.Get("RESET_CONFIRMATION", "MENUS,GUI");
		dialog.gameObject.SetActive(value: true);
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			ObjectPool.PoolObject(dialog.gameObject, instant: true);
			dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default, isActive: false);
			dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
			dialog.headerText.text = Language.Get("RESET_DATA_2", "MENUS,GUI");
			dialog.descriptionText.text = Language.Get("RESET_CONFIRMATION_EXTRAMESSAGE", "MENUS,GUI");
			dialog.gameObject.SetActive(value: true);
			while (dialog.gameObject.activeSelf)
			{
				yield return null;
			}
			if (dialog.isConfirmed)
			{
				Player.instance.ResetPlayerData();
				PauseManager.instance.Resume();
				shouldShowTitleScreen = true;
				CityController.previousCityScene = "City_JumpCityNew";
				LoadingScreenCover.LoadScene("City_JumpCityNew");
			}
			else
			{
				ObjectPool.PoolObject(dialog.gameObject);
			}
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PlayerPrefs.Save();
		}
	}

	public void PreviousLanguage()
	{
		currentLanguageIndex--;
		if (currentLanguageIndex < 0)
		{
			currentLanguageIndex = languages.Count - 1;
		}
		UpdateCurrentLanguage();
		StopCoroutine("UnloadUnusedAssets");
		StartCoroutine("UnloadUnusedAssets");
		if (LanguageChanged != null)
		{
			LanguageChanged();
		}
		EventMessenger.Instance.Raise(new LanguageChangedEvent(languages[currentLanguageIndex]));
	}

	public void NextLanguage()
	{
		currentLanguageIndex++;
		if (currentLanguageIndex > languages.Count - 1)
		{
			currentLanguageIndex = 0;
		}
		UpdateCurrentLanguage();
		StopCoroutine("UnloadUnusedAssets");
		StartCoroutine("UnloadUnusedAssets");
		if (LanguageChanged != null)
		{
			LanguageChanged();
		}
		EventMessenger.Instance.Raise(new LanguageChangedEvent(languages[currentLanguageIndex]));
	}

	private IEnumerator UnloadUnusedAssets()
	{
		yield return new WaitForSeconds(1f);
		yield return Resources.UnloadUnusedAssets();
	}

	private string GetLanguageNameForCode(LanguageCode langCode)
	{
		return langCode switch
		{
			LanguageCode.EN => Language.Get("ENGLISH", "MENUS,GUI"), 
			LanguageCode.FR => Language.Get("FRENCH", "MENUS,GUI"), 
			LanguageCode.IT => Language.Get("ITALIAN", "MENUS,GUI"), 
			LanguageCode.RU => Language.Get("RUSSIAN", "MENUS,GUI"), 
			LanguageCode.TR => Language.Get("TURKISH", "MENUS,GUI"), 
			LanguageCode.ES => Language.Get("SPANISH", "MENUS,GUI"), 
			LanguageCode.PT => Language.Get("PORTUGUESE", "MENUS,GUI"), 
			LanguageCode.KO => Language.Get("KOREAN", "MENUS,GUI"), 
			LanguageCode.JA => Language.Get("JAPANESE", "MENUS,GUI"), 
			_ => "English", 
		};
	}

	private int GetIndexForLanguage(LanguageCode langCode)
	{
		for (int i = 0; i < languages.Count; i++)
		{
			if (languages[i] == langCode)
			{
				return i;
			}
		}
		return 0;
	}

	private void UpdateCurrentLanguage()
	{
		try
		{
			Language.SwitchLanguage(languages[Mathf.Max(0, currentLanguageIndex)]);
			languageTextMesh.text = GetLanguageNameForCode(languages[currentLanguageIndex]).ToUpper();
		}
		catch
		{
		}
	}

	private void OnVideoEnd()
	{
		isVideoDone = true;
	}

	private void ReplayIntroPressed()
	{
		StartCoroutine("StartIntroVideo");
	}

	private IEnumerator StartIntroVideo()
	{
		int previouslyMuted = PlayerPrefs.GetInt("Game_Music_Off");
		PlayerPrefs.SetInt("Game_Music_Off", 1);
		GFAudioManager.instance.MuteGroup(GFAudioManager.MUSIC_GROUP);
		mainMenuTouchBlocker.SetActive(value: true);
		TitleScreen titleScreen = titleScreenMenu.GetComponent<TitleScreen>();
		titleScreen.settingsButton.enabled = false;
		titleScreen.introButton.enabled = false;
		titleScreen.goAnimator.gameObject.SetActive(value: false);
		titleScreen.privacyPolicyButton.enabled = false;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
		skipButton.enabled = false;
		skipText.color = new Color(1f, 1f, 1f, 0f);
		videoManager.Load(cinematicPart1Clip);
		yield return new WaitForEndOfFrame();
		videoManager.transform.parent.gameObject.SetActive(value: true);
		if (Language.CurrentLanguage() != LanguageCode.EN)
		{
			captionsManager.gameObject.SetActive(value: true);
			captionsManager.StartVideoCaption(cinematicPart1CaptionAsset);
		}
		if (GUICamera.instance != null)
		{
			GUICamera.instance.camera.enabled = false;
		}
		if (GameUI.instance != null)
		{
			if (GameUI.instance.guiCamera2 != null)
			{
				GameUI.instance.guiCamera2.enabled = false;
			}
			if (GameUI.instance.missionRevealCamera != null)
			{
				GameUI.instance.missionRevealCamera.enabled = false;
			}
		}
		if (CityController.instance != null && CityController.instance.pinchZoomer != null && CityController.instance.pinchZoomer.thisCamera != null)
		{
			CityController.instance.pinchZoomer.thisCamera.enabled = false;
		}
		Sequence skipSequence = null;
		bool skipVideo = false;
		bool showingSkip = false;
		Action handleSkipButtonClicked = () =>
		{
			skipVideo = true;
		};
		skipButton.gameObject.SetActive(value: true);
		videoGUICamera.SetActive(value: true);
		skipButton.OnClick += handleSkipButtonClicked;
		while (!isVideoDone && !skipVideo)
		{
			if (!showingSkip && Input.GetMouseButtonDown(0))
			{
				skipSequence?.Kill();
				showingSkip = true;
				skipSequence = DOTween.Sequence();
				skipSequence.Append(skipText.DOFade(1f, 0.33f).OnComplete(() =>
				{
					skipButton.enabled = true;
				}));
				skipSequence.AppendInterval(2f);
				skipSequence.AppendCallback(() =>
				{
					skipButton.enabled = false;
				});
				skipSequence.Append(skipText.DOFade(0f, 0.33f));
				skipSequence.AppendCallback(() =>
				{
					showingSkip = false;
				});
				skipSequence.ForceInit();
			}
			yield return null;
		}
		skipButton.OnClick -= handleSkipButtonClicked;
		skipSequence?.Kill();
		skipText.color = new Color(1f, 1f, 1f, 0f);
		skipButton.enabled = false;
		skipButton.gameObject.SetActive(value: false);
		videoGUICamera.SetActive(value: false);
		isVideoDone = false;
		videoManager.Load(cinematicPart2Clip);
		yield return null;
		if (Language.CurrentLanguage() != LanguageCode.EN)
		{
			captionsManager.StartVideoCaption(cinematicPart2CaptionAsset);
		}
		skipVideo = false;
		showingSkip = false;
		skipButton.gameObject.SetActive(value: true);
		videoGUICamera.SetActive(value: true);
		skipButton.OnClick += handleSkipButtonClicked;
		while (!isVideoDone && !skipVideo)
		{
			if (!showingSkip && Input.GetMouseButtonDown(0))
			{
				skipSequence?.Kill();
				showingSkip = true;
				skipSequence = DOTween.Sequence();
				skipSequence.Append(skipText.DOFade(1f, 0.5f).OnComplete(() =>
				{
					skipButton.enabled = true;
				}));
				skipSequence.AppendInterval(2f);
				skipSequence.AppendCallback(() =>
				{
					skipButton.enabled = false;
				});
				skipSequence.Append(skipText.DOFade(0f, 0.5f));
				skipSequence.AppendCallback(() =>
				{
					showingSkip = false;
				});
				skipSequence.ForceInit();
			}
			yield return null;
		}
		skipButton.OnClick -= handleSkipButtonClicked;
		skipSequence?.Kill();
		skipText.color = new Color(1f, 1f, 1f, 0f);
		skipButton.enabled = false;
		skipButton.gameObject.SetActive(value: false);
		videoGUICamera.SetActive(value: false);
		PlayerPrefs.SetInt("Game_Music_Off", previouslyMuted);
		GFAudioManager.instance.MuteGroup(GFAudioManager.MUSIC_GROUP, PlayerPrefs.GetInt("Game_Music_Off") == 1);
		CityController.instance.cameraHolder.GetComponentInChildren<Camera>().enabled = true;
		if (GUICamera.instance != null)
		{
			GUICamera.instance.camera.enabled = true;
		}
		if (GameUI.instance != null)
		{
			if (GameUI.instance.guiCamera2 != null)
			{
				GameUI.instance.guiCamera2.enabled = true;
			}
			if (GameUI.instance.missionRevealCamera != null)
			{
				GameUI.instance.missionRevealCamera.enabled = true;
			}
		}
		if (CityController.instance != null && CityController.instance.pinchZoomer != null && CityController.instance.pinchZoomer.thisCamera != null)
		{
			CityController.instance.pinchZoomer.thisCamera.enabled = true;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Screen.autorotateToLandscapeLeft = false;
			Screen.autorotateToLandscapeRight = false;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			if (Input.deviceOrientation != DeviceOrientation.Portrait || Input.deviceOrientation != DeviceOrientation.PortraitUpsideDown)
			{
				Screen.orientation = ScreenOrientation.Portrait;
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
				Screen.orientation = ScreenOrientation.AutoRotation;
			}
		}
		videoManager.UnLoad();
		isVideoDone = false;
		captionsManager.gameObject.SetActive(value: false);
		videoManager.transform.parent.gameObject.SetActive(value: false);
		videoManager.gameObject.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Textures/Black") as Texture2D;
		titleScreen.settingsButton.enabled = true;
		titleScreen.introButton.enabled = true;
		titleScreen.goAnimator.gameObject.SetActive(value: true);
		titleScreen.privacyPolicyButton.enabled = true;
		yield return new WaitForSeconds(0.1f);
		mainMenuTouchBlocker.SetActive(value: false);
	}

	private void ShowTermsPrivacyMessage()
	{
	}

	public void AcceptedTerms()
	{
	}

	public void TermsOfUseClicked()
	{
	}

	private void onBillboardAppearEvent(string scope)
	{
		if (scope.Equals("game_moregames"))
		{
			preLoadedMoreGames = false;
		}
	}

	private void onBillboardDismissEvent(string scope)
	{
	}
}
