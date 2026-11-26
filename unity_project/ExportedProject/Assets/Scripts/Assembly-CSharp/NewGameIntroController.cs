using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameIntroController : MonoBehaviour
{
	[Serializable]
	public class CharacterMission
	{
		public CityCharacterType characterType;

		public List<int> missionUIDs;

		public CharacterMission(CityCharacterType characterType, List<int> missionUIDs)
		{
			this.characterType = characterType;
			this.missionUIDs = missionUIDs;
		}
	}

	public static bool completeAllTutorialsMode;

	[Header("Video Player Stuff")]
	public AudioListener audioListener;

	public string cinematicPart1;

	public string cinematicPart2;

	public string characterSelectSceneName;

	public VideoCaption cinematicPart1Caption;

	public VideoCaption cinematicPart2Caption;

	public CaptionsManager captionManager;

	public MediaPlayerCtrl mediaController;

	public Renderer blackBG;

	[Header("Skip Button References")]
	public GameObject guiCamera;

	public tk2dUIItem skipButton;

	public TextMeshPro skipText;

	public tk2dCameraAnchorOffsetDeviceOverride skipBtnAnchor;

	[Header("Sequence Properties")]
	public float fadeOutTimeToFirstBattle = 1f;

	public float endOfBattleDelayToSecondCinematic = 3.5f;

	public float endOfBattleFadeInTime = 1f;

	public float fadeOutTimeToSelectScreen = 2.5f;

	[Header("Battle Definition")]
	public string enemyPortraitSprite;

	public string battleThemePath;

	public int battleArenaUID;

	public BattleConfiguration playerBattleConfiguration;

	public BattleConfiguration enemyBattleConfiguration;

	public List<CharacterMission> characterMissions = new List<CharacterMission>();

	private BoxCollider2D skipCollider;

	private bool showingSkip;

	private float skipVisibleDuration = 2f;

	private Coroutine orientationCheckRoutine;

	private DeviceOrientation curOrientation;

	private IEnumerator Start()
	{
		if (completeAllTutorialsMode)
		{
			completeAllTutorialsMode = false;
			StartCoroutine(CompleteAllTutorialsSequence());
			yield break;
		}
		bool videoFailed = false;
		skipButton.enabled = false;
		skipText.color = new Color(1f, 1f, 1f, 0f);
		guiCamera.SetActive(false);
		if (LoadingScreenCover.isActive)
		{
			yield return new WaitUntil(() => LoadingScreenCover.outroFinished);
		}
		else
		{
			Debug.Log("New Game Intro, Loading Screen Cover is NOT loading, skipping waiting for it to outro.");
		}
		yield return new WaitForSeconds(0.25f);
		try
		{
			Debug.Log("Try to Load Cinematic Part 1");
			mediaController.Load(cinematicPart1);
		}
		catch
		{
			Debug.Log("Failed trying to load Cinematic Part 1! Playing video, alternative way");
			Handheld.PlayFullScreenMovie(cinematicPart1, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
			videoFailed = true;
		}
		if (Language.CurrentLanguage() != LanguageCode.EN)
		{
			captionManager.gameObject.SetActive(true);
			captionManager.StartVideoCaption(cinematicPart1Caption);
		}
		else
		{
			captionManager.gameObject.SetActive(false);
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.orientation = ScreenOrientation.AutoRotation;
			orientationCheckRoutine = StartCoroutine(CheckForOrientationChange());
		}
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		AsyncOperation battleSceneAsync = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
		battleSceneAsync.allowSceneActivation = false;
		BattleDefinition battleDefinition = new BattleDefinition
		{
			accessoryUID = -1,
			battleThemePath = battleThemePath,
			arenaUID = battleArenaUID,
			coinReward = 0,
			countdownCharacterUID = -1,
			enemyAccessoryUID = -1,
			enemyCharacterName = CharacterName.HoodedHood,
			enemyName = "The Hooded Hood",
			enemyPortraitSprite = enemyPortraitSprite,
			enemyTeam = new List<Character>(),
			playerTeamOverride = new List<Character>(),
			playerAIType = PlayerAIType.Default,
			type = BattleType.Normal
		};
		for (int num = 0; num < playerBattleConfiguration.figures.Length; num++)
		{
			TournamentRoundFigureData figureData = playerBattleConfiguration.figures[num];
			battleDefinition.playerTeamOverride.Add(new Character(num, figureData));
		}
		for (int num2 = 0; num2 < enemyBattleConfiguration.figures.Length; num2++)
		{
			TournamentRoundFigureData figureData2 = enemyBattleConfiguration.figures[num2];
			battleDefinition.enemyTeam.Add(new Character(num2, figureData2));
		}
		BattleController.battleDefinition = battleDefinition;
		Texture2D blackTexture = Resources.Load<Texture2D>("Textures/Black");
		MeshRenderer mediaControllerRenderer = mediaController.gameObject.GetComponent<MeshRenderer>();
		Camera mediaControllerCamera = mediaController.transform.parent.GetComponent<Camera>();
		mediaControllerCamera.gameObject.SetActive(true);
		bool videoFinished = false;
		MediaPlayerCtrl.VideoEnd handleVideoEnd = () =>
		{
			videoFinished = true;
		};
		MediaPlayerCtrl mediaPlayerCtrl = mediaController;
		mediaPlayerCtrl.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Combine(mediaPlayerCtrl.OnEnd, handleVideoEnd);
		Sequence skipSequence = null;
		bool skipVideo = false;
		Action handleSkipButtonClicked = () =>
		{
			Debug.Log("Skip Button Clicked...");
			skipVideo = true;
		};
		guiCamera.SetActive(true);
		skipButton.OnClick += handleSkipButtonClicked;
		Debug.Log("Skip Button On Click, Registered...");
		while (!videoFinished && !videoFailed && !skipVideo)
		{
			if (!showingSkip && Input.GetMouseButtonDown(0))
			{
				if (skipSequence != null)
				{
					skipSequence.Kill();
				}
				showingSkip = true;
				try
				{
					skipSequence = DOTween.Sequence();
					skipSequence.Append(skipText.DOFade(1f, 0.33f).OnComplete(() =>
					{
						Debug.Log("Skip Button Enabled...");
						skipButton.enabled = true;
					}));
					skipSequence.AppendInterval(skipVisibleDuration);
					skipSequence.AppendCallback(() =>
					{
						Debug.Log("Skip Button Disabled...");
						skipButton.enabled = false;
					});
					skipSequence.Append(skipText.DOFade(0f, 0.33f));
					skipSequence.AppendCallback(() =>
					{
						Debug.Log("Showing Skip false...");
						showingSkip = false;
					});
					skipSequence.ForceInit();
				}
				catch
				{
					Debug.LogError("Error in Skip Text tween sequence, recovering...");
					showingSkip = true;
					skipButton.enabled = true;
					skipText.color = Color.white;
				}
			}
			yield return null;
		}
		skipButton.OnClick -= handleSkipButtonClicked;
		if (skipSequence != null)
		{
			skipSequence.Kill();
		}
		skipText.color = new Color(1f, 1f, 1f, 0f);
		skipButton.enabled = false;
		skipButton.gameObject.SetActive(false);
		guiCamera.SetActive(false);
		MediaPlayerCtrl mediaPlayerCtrl2 = mediaController;
		mediaPlayerCtrl2.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Remove(mediaPlayerCtrl2.OnEnd, handleVideoEnd);
		captionManager.gameObject.SetActive(false);
		mediaController.Stop();
		mediaControllerRenderer.material.mainTexture = blackTexture;
		mediaController.gameObject.SetActive(false);
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
		while (battleSceneAsync.progress < 0.9f)
		{
			yield return null;
		}
		battleSceneAsync.allowSceneActivation = true;
		Player.instance.SetTrigger("FirstBattleTutorial");
		yield return new WaitUntil(() => battleSceneAsync.isDone);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Battle"));
		audioListener.gameObject.SetActive(false);
		Tweener fadeTween = blackBG.material.DOFade(0f, fadeOutTimeToFirstBattle).SetEase(Ease.Linear);
		yield return fadeTween.WaitForCompletion();
		mediaControllerCamera.gameObject.SetActive(false);
		while (BattleController.instance == null)
		{
			yield return null;
		}
		BattleController.instance.useFlashbackBattleEnding = true;
		bool continueSequence = false;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		BattleController.instance.BattleEnded += continueSequenceAction;
		yield return new WaitUntil(() => continueSequence);
		BattleController.instance.BattleEnded -= continueSequenceAction;
		yield return new WaitForSeconds(endOfBattleDelayToSecondCinematic);
		GFAudioManager.instance.FadeOutGroup(GFAudioManager.MUSIC_GROUP, endOfBattleFadeInTime);
		GFAudioManager.instance.FadeOutGroup(GFAudioManager.SFX_GROUP, endOfBattleFadeInTime);
		GFAudioManager.instance.FadeOutGroup(GFAudioManager.VOICEOVER_GROUP, endOfBattleFadeInTime);
		blackBG.material.color = Color.clear;
		Tweener fadeInTween = blackBG.material.DOFade(1f, endOfBattleFadeInTime).SetEase(Ease.Linear);
		yield return fadeInTween.WaitForCompletion();
		mediaControllerCamera.gameObject.SetActive(true);
		mediaController.gameObject.SetActive(true);
		videoFailed = false;
		audioListener.gameObject.SetActive(true);
		BattleController.instance.enemyBattleCamera.camera.enabled = false;
		BattleController.instance.guiCamera.enabled = false;
		BattleController.instance.playerBattleCamera.camera.enabled = false;
		BattleController.instance.splitCameras.activeMainCamera.enabled = false;
		BattleController.instance.splitCameras.mainCamera.enabled = false;
		BattleController.instance.splitCameras.secondaryCamera.enabled = false;
		try
		{
			mediaController.Load(cinematicPart2);
		}
		catch
		{
			Handheld.PlayFullScreenMovie(cinematicPart2, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
			videoFailed = true;
		}
		if (Language.CurrentLanguage() != LanguageCode.EN)
		{
			captionManager.gameObject.SetActive(true);
			captionManager.StartVideoCaption(cinematicPart2Caption);
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
		AsyncOperation characterSelectOp = SceneManager.LoadSceneAsync(characterSelectSceneName, LoadSceneMode.Additive);
		characterSelectOp.allowSceneActivation = false;
		videoFinished = false;
		MediaPlayerCtrl mediaPlayerCtrl3 = mediaController;
		mediaPlayerCtrl3.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Combine(mediaPlayerCtrl3.OnEnd, handleVideoEnd);
		skipVideo = false;
		showingSkip = false;
		skipButton.gameObject.SetActive(true);
		guiCamera.SetActive(true);
		skipButton.OnClick += handleSkipButtonClicked;
		while (!videoFinished && !videoFailed && !skipVideo)
		{
			if (!showingSkip && Input.GetMouseButtonDown(0))
			{
				if (skipSequence != null)
				{
					skipSequence.Kill();
				}
				showingSkip = true;
				skipSequence = DOTween.Sequence();
				skipSequence.Append(skipText.DOFade(1f, 0.33f).OnComplete(() =>
				{
					skipButton.enabled = true;
				}));
				skipSequence.AppendInterval(skipVisibleDuration);
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
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (orientationCheckRoutine != null)
			{
				StopCoroutine(orientationCheckRoutine);
			}
			orientationCheckRoutine = null;
		}
		skipButton.OnClick -= handleSkipButtonClicked;
		if (skipSequence != null)
		{
			skipSequence.Kill();
		}
		skipText.color = new Color(1f, 1f, 1f, 0f);
		skipButton.enabled = false;
		skipButton.gameObject.SetActive(false);
		guiCamera.SetActive(false);
		MediaPlayerCtrl mediaPlayerCtrl4 = mediaController;
		mediaPlayerCtrl4.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Remove(mediaPlayerCtrl4.OnEnd, handleVideoEnd);
		captionManager.gameObject.SetActive(false);
		mediaController.Stop();
		mediaControllerRenderer.material.mainTexture = blackTexture;
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
		characterSelectOp.allowSceneActivation = true;
		yield return new WaitUntil(() => characterSelectOp.isDone);
		audioListener.gameObject.SetActive(false);
		mediaController.gameObject.SetActive(false);
		mediaControllerCamera.gameObject.SetActive(false);
		Tween fadeTween2 = blackBG.material.DOFade(0f, fadeOutTimeToSelectScreen).SetEase(Ease.Linear);
		yield return fadeTween2.WaitForCompletion();
		GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 1f);
		GFAudioManager.instance.SetGroupVolume(GFAudioManager.SFX_GROUP, 1f);
		GFAudioManager.instance.SetGroupVolume(GFAudioManager.VOICEOVER_GROUP, 1f);
		continueSequence = false;
		CharacterSelectElevator.instance.CharacterSelected += continueSequenceAction;
		yield return new WaitUntil(() => continueSequence);
		CharacterSelectElevator.instance.CharacterSelected -= continueSequenceAction;
		CityCharacterType chosenCharacterType = CityCharacter.CityCharacterTypeFromSkinName(GameSkinManager.instance.CurrentSkinName);
		CharacterMission characterMission = characterMissions.Find((CharacterMission x) => x.characterType == chosenCharacterType);
		if (characterMission != null)
		{
			int count = characterMission.missionUIDs.Count;
			for (int num3 = 0; num3 < count; num3++)
			{
				Player.instance.SilentlyCompleteMission(characterMission.missionUIDs[num3], num3 == count - 1);
			}
		}
		string debugString = "Printing all completed missions... \n";
		int count2 = Player.instance.completedMissions.Count;
		for (int num4 = 0; num4 < count2; num4++)
		{
			MissionCompletedData missionCompletedData = Player.instance.completedMissions[num4];
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionCompletedData.uid);
			string text = debugString;
			debugString = text + num4 + ": " + mission.title + ", UID: " + mission.uid + "\n";
		}
		Debug.Log(debugString);
		yield return new WaitForSeconds(0.5f);
		Player.instance.SetTrigger("PLAYER_PLAYED_INTRO_SEQUENCE");
		if (!string.IsNullOrEmpty(SaveUtility.GetString("PlayerScene")))
		{
			Debug.Log("Load Initial Scene" + SaveUtility.GetString("PlayerScene"));
			LoadingScreenCover.LoadScene(SaveUtility.GetString("PlayerScene"));
		}
		else
		{
			LoadingScreenCover.LoadScene(GameManager.instance.gameSettings.citySceneName);
		}
	}

	private void OnDestroy()
	{
		Application.backgroundLoadingPriority = ThreadPriority.Normal;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Screen.autorotateToLandscapeLeft = false;
			Screen.autorotateToLandscapeRight = false;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			if (orientationCheckRoutine != null)
			{
				StopCoroutine(orientationCheckRoutine);
			}
		}
	}

	private IEnumerator CheckForOrientationChange()
	{
		while (true)
		{
			yield return null;
			skipBtnAnchor.ForceOffsetUpdate();
		}
	}

	private IEnumerator CompleteAllTutorialsSequence()
	{
		guiCamera.SetActive(false);
		blackBG.material.DOFade(0f, 0.01f);
		AsyncOperation characterSelectOp = SceneManager.LoadSceneAsync(characterSelectSceneName, LoadSceneMode.Additive);
		characterSelectOp.allowSceneActivation = true;
		yield return new WaitUntil(() => characterSelectOp.isDone);
		audioListener.gameObject.SetActive(false);
		mediaController.gameObject.SetActive(false);
		mediaController.transform.parent.GetComponent<Camera>().gameObject.SetActive(false);
		bool continueSequence = false;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		CharacterSelectElevator.instance.CharacterSelected += continueSequenceAction;
		yield return new WaitUntil(() => continueSequence);
		CharacterSelectElevator.instance.CharacterSelected -= continueSequenceAction;
		CityCharacterType chosenCharacterType = CityCharacter.CityCharacterTypeFromSkinName(GameSkinManager.instance.CurrentSkinName);
		CharacterMission characterMission = characterMissions.Find((CharacterMission x) => x.characterType == chosenCharacterType);
		if (characterMission != null)
		{
			int count = characterMission.missionUIDs.Count;
			for (int num = 0; num < count; num++)
			{
				Player.instance.SilentlyCompleteMission(characterMission.missionUIDs[num], num == count - 1);
			}
		}
		string debugString = "Printing all completed missions... \n";
		int count2 = Player.instance.completedMissions.Count;
		for (int num2 = 0; num2 < count2; num2++)
		{
			MissionCompletedData missionCompletedData = Player.instance.completedMissions[num2];
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionCompletedData.uid);
			string text = debugString;
			debugString = text + num2 + ": " + mission.title + ", UID: " + mission.uid + "\n";
		}
		Debug.Log(debugString);
		Player.instance.SetTrigger("PLAYER_PLAYED_INTRO_SEQUENCE");
		if (!string.IsNullOrEmpty(SaveUtility.GetString("PlayerScene")))
		{
			Debug.Log("Load Initial Scene" + SaveUtility.GetString("PlayerScene"));
			LoadingScreenCover.LoadScene(SaveUtility.GetString("PlayerScene"));
		}
		else
		{
			LoadingScreenCover.LoadScene(GameManager.instance.gameSettings.citySceneName);
		}
	}
}
