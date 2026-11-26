using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class InitController : MonoBehaviour
{
	[Range(15f, 60f)]
	public int targetFrameRate = 60;

	public bool multiTouchEnabled = true;

	public float durationLogos = 2f;

	public float durationLogoFadeIn = 0.3f;

	public float durationLogoFadeOut = 0.5f;

	public float durationBetweenLogos = 0.3f;

	public MediaPlayerCtrl videoManager;

	public CaptionsManager captionsManager;

	public TutorialManager tutorialManager;

	public GameObject splashImagesObj;

	public ToSPopup tosPopup;

	public TTGDFPBanner dfpBanner;

	[Header("Debug")]
	public bool overridePlatformScale;

	public int platformScaleOverride = 1;

	private bool isVideoDone;

	private void Awake()
	{
		MediaPlayerCtrl mediaPlayerCtrl = videoManager;
		mediaPlayerCtrl.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Combine(mediaPlayerCtrl.OnEnd, new MediaPlayerCtrl.VideoEnd(OnVideoEnd));
		dfpBanner.gameObject.SetActive(value: false);
	}

	private void OnDestroy()
	{
		MediaPlayerCtrl mediaPlayerCtrl = videoManager;
		mediaPlayerCtrl.OnEnd = (MediaPlayerCtrl.VideoEnd)Delegate.Remove(mediaPlayerCtrl.OnEnd, new MediaPlayerCtrl.VideoEnd(OnVideoEnd));
	}

	private void OnVideoEnd()
	{
		isVideoDone = true;
	}

	private IEnumerator Start()
	{
		Application.targetFrameRate = targetFrameRate;
		Screen.sleepTimeout = -1;
		Input.multiTouchEnabled = multiTouchEnabled;
		if (overridePlatformScale)
		{
			tk2dSystem.CurrentPlatform = platformScaleOverride + "x";
		}
		else
		{
			tk2dSystem.CurrentPlatform = GetScale() + "x";
		}
		Debug.Log("Platform Scale " + tk2dSystem.CurrentPlatform);
		if (SystemInfo.SupportsTextureFormat(TextureFormat.ETC2_RGBA8))
		{
			Debug.Log("Platform Supports TextureFormat.ETC2_RGBA8");
		}
		if (SystemInfo.SupportsTextureFormat(TextureFormat.PVRTC_RGBA4))
		{
			Debug.Log("Platform Supports TextureFormat.PVRTC_RGBA4");
		}
		if (SystemInfo.SupportsTextureFormat(TextureFormat.DXT5))
		{
			Debug.Log("Platform Supports TextureFormat.DXT5");
		}
		if (SystemInfo.SupportsTextureFormat(TextureFormat.ATC_RGBA8))
		{
			Debug.Log("Platform Supports TextureFormat.ATC_RGBA8");
		}
		DOTween.Init();
		GameManager.instance.Init();
		yield return new WaitForEndOfFrame();
		Debug.Log("GameSaveData.saveVersion " + SaveUtility.GameSaveData.saveVersion);
		if (tutorialManager != null)
		{
			tutorialManager.gameObject.SetActive(value: true);
		}
		Debug.LogFormat("Has player accepted the ToS and Privacy Policy?: {0}", (PlayerPrefs.GetInt("AcceptedTerms") != 0) ? "YES" : "NO");
		if (PlayerPrefs.GetInt("AcceptedTerms", 0) == 0)
		{
			tosPopup.gameObject.SetActive(value: true);
			yield return new WaitWhile(() => tosPopup.gameObject.activeSelf);
		}
		bool videoError = false;
		bool didTimeOut = false;
		float timeStart = Time.time;
		try
		{
			videoManager.Load("Videos/loader_cartoonnetwork.mp4");
		}
		catch
		{
			didTimeOut = true;
		}
		if (!didTimeOut)
		{
			yield return new WaitForEndOfFrame();
			videoManager.transform.parent.gameObject.SetActive(value: true);
		}
		while (!isVideoDone && !didTimeOut)
		{
			yield return null;
			if (videoManager.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING && Input.GetMouseButtonDown(0))
			{
				videoManager.Stop();
				break;
			}
			if (videoManager.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.ERROR)
			{
				videoError = true;
				videoManager.Stop();
				break;
			}
			if (Time.time - timeStart > 3f && Input.GetMouseButtonDown(0))
			{
				videoManager.Stop();
				break;
			}
			if (Time.time - timeStart > 10f)
			{
				didTimeOut = true;
				videoManager.Stop();
				break;
			}
		}
		isVideoDone = false;
		videoManager.transform.parent.gameObject.SetActive(value: false);
		videoManager.gameObject.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Textures/Black") as Texture2D;
		if (videoError || didTimeOut)
		{
			videoError = false;
			Handheld.PlayFullScreenMovie("Videos/loader_cartoonnetwork.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFill);
		}
		yield return new WaitForSeconds(durationBetweenLogos);
		SplashImages splashImages = splashImagesObj.GetComponent<SplashImages>();
		tk2dSprite dcLogo = splashImages.dcLogo;
		tk2dSprite wbLogo = splashImages.wbLogo;
		dcLogo.color = new Color(1f, 1f, 1f, 0f);
		dcLogo.gameObject.SetActive(value: true);
		DOTween.ToAlpha(() => dcLogo.color, (Color x) =>
		{
			dcLogo.color = x;
		}, 1f, durationLogoFadeIn).SetEase(Ease.OutSine);
		bool stayInLoop = true;
		float startTime = Time.time;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0))
			{
				stayInLoop = false;
			}
			if (Time.time - startTime > durationLogos)
			{
				stayInLoop = false;
			}
			yield return null;
		}
		DOTween.ToAlpha(() => dcLogo.color, (Color x) =>
		{
			dcLogo.color = x;
		}, 0f, durationLogoFadeOut).SetEase(Ease.OutSine);
		yield return new WaitForSeconds(durationLogoFadeOut + durationBetweenLogos);
		wbLogo.color = new Color(1f, 1f, 1f, 0f);
		wbLogo.gameObject.SetActive(value: true);
		DOTween.ToAlpha(() => wbLogo.color, (Color x) =>
		{
			wbLogo.color = x;
		}, 1f, durationLogoFadeIn).SetEase(Ease.OutSine);
		stayInLoop = true;
		startTime = Time.time;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0))
			{
				stayInLoop = false;
			}
			if (Time.time - startTime > durationLogos)
			{
				stayInLoop = false;
			}
			yield return null;
		}
		DOTween.ToAlpha(() => wbLogo.color, (Color x) =>
		{
			wbLogo.color = x;
		}, 0f, durationLogoFadeOut).SetEase(Ease.OutSine);
		yield return new WaitForSeconds(durationLogoFadeOut + durationBetweenLogos);
		dcLogo.gameObject.SetActive(value: false);
		wbLogo.gameObject.SetActive(value: false);
		if (!didTimeOut)
		{
			videoManager.Load("Videos/loader_grumpyface.mp4");
			yield return new WaitForEndOfFrame();
			videoManager.transform.parent.gameObject.SetActive(value: true);
		}
		while (!isVideoDone && !didTimeOut)
		{
			yield return null;
			if (videoManager.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING && Input.GetMouseButtonDown(0))
			{
				videoManager.Stop();
				break;
			}
			if (videoManager.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.ERROR)
			{
				videoError = true;
				videoManager.Stop();
				break;
			}
			if (Time.time - timeStart > 3f && Input.GetMouseButtonDown(0))
			{
				videoManager.Stop();
				break;
			}
		}
		if (videoError || didTimeOut)
		{
			videoError = false;
			Handheld.PlayFullScreenMovie("Videos/Videos/loader_grumpyface.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFill);
		}
		yield return null;
		isVideoDone = false;
		videoManager.gameObject.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Textures/Black") as Texture2D;
		yield return new WaitForEndOfFrame();
		if (DFP.Instance.bannerLoaded)
		{
			dfpBanner.gameObject.SetActive(value: true);
			yield return StartCoroutine(dfpBanner.Show());
			dfpBanner.gameObject.SetActive(value: false);
		}
		TitleScreenController.shouldShowTitleScreen = true;
		CityController.fromInitScene = true;
		Player.instance.Init();
		if (SaveUtility.GetInt("GAME_CHALLENGE_IN_PROGRESS") == 1)
		{
			SaveUtility.SetInt("GAME_CHALLENGE_CONSECUTIVE_WINS", 0);
		}
		if (!string.IsNullOrEmpty(SaveUtility.GetString("PlayerScene")))
		{
			Debug.Log("Load Initial Scene" + SaveUtility.GetString("PlayerScene"));
			LoadingScreenCover.LoadScene(SaveUtility.GetString("PlayerScene"), showLoadingScreen: true, "Init", showDefaultSkin: true);
		}
		else
		{
			LoadingScreenCover.LoadScene(GameManager.instance.gameSettings.citySceneName, showLoadingScreen: true, "Init", showDefaultSkin: true);
		}
	}

	public static int GetScale(bool calculateScaleInEditor = false)
	{
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		num3 = 593920;
		num4 = 2304000;
		num2 = Screen.width * Screen.height;
		num = ((num2 < num3) ? 1 : ((num2 >= num4) ? 4 : 2));
		if (num == 4 && SystemInfo.systemMemorySize <= 1024)
		{
			num = 2;
			Debug.Log("Setting the Scale to 2x since this device has at most 1GB of memory available");
		}
		return num;
	}
}
