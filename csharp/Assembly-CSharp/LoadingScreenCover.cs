using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenCover : MonoBehaviour
{
	private static bool showCover = true;

	public static bool isLoading;

	public static bool allowInit = true;

	public static bool outroFinished = true;

	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public Transform ttIcon;

	public AudioListener audioListener;

	public tk2dSpriteCollectionData spriteCollectionData;

	public List<GameObject> skinnedSprites = new List<GameObject>();

	public static string prevScene { get; private set; }

	public static string nextScene { get; private set; }

	public static string lastScene { get; private set; }

	public static IEnumerator finalWait { get; private set; }

	public static bool isActive { get; private set; }

	private void Reset()
	{
		animator = GetComponent<Animator>();
	}

	private void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		animator.gameObject.SetActive(showCover);
		Object.DontDestroyOnLoad(gameObject);
		isActive = true;
	}

	private void OnDestroy()
	{
		isActive = false;
	}

	private IEnumerator Start()
	{
		isLoading = true;
		allowInit = false;
		outroFinished = false;
		if (showCover)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_loadingscreen_intro", GFAudioManager.SFX_GROUP, string.Empty);
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("intro") && animator.NormalizedTime() < 1f)
			{
				yield return null;
			}
		}
		if (showCover)
		{
			Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
			Handheld.StartActivityIndicator();
		}
		TransitionScene.isDone = false;
		AsyncOperation loader = SceneManager.LoadSceneAsync("Transition");
		while (!loader.isDone)
		{
			yield return null;
		}
		while (!TransitionScene.isDone)
		{
			yield return null;
		}
		if (!TitleScreenController.shouldShowTitleScreen)
		{
			GFAudioManager.instance.UnloadAllPreloadedClips();
			if (CityModel.instance != null)
			{
				CityModel.instance.gameObject.SetActive(value: false);
			}
			Player.instance.Save();
		}
		loader = SceneManager.LoadSceneAsync(nextScene);
		loader.allowSceneActivation = false;
		while (loader.progress < 0.9f)
		{
			yield return null;
		}
		loader.allowSceneActivation = true;
		while (!loader.isDone)
		{
			yield return null;
		}
		isLoading = false;
		if (showCover)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			Handheld.StopActivityIndicator();
			StartCoroutine(DelayedOutro());
			animatorEventListener.EventTriggered += HandleEventTriggered;
		}
		else
		{
			allowInit = true;
			prevScene = nextScene;
			nextScene = string.Empty;
			Object.Destroy(gameObject);
		}
	}

	private void HandleEventTriggered(AnimationEvent evt)
	{
		switch (evt.stringParameter)
		{
		case "loopComplete":
			StartCoroutine(DelayedOutro());
			break;
		case "outroComplete":
			animatorEventListener.EventTriggered -= HandleEventTriggered;
			prevScene = nextScene;
			nextScene = string.Empty;
			if (spriteCollectionData != null)
			{
				spriteCollectionData.UnloadTextures();
				spriteCollectionData.ResetPlatformData();
			}
			outroFinished = true;
			Object.Destroy(gameObject);
			break;
		}
	}

	private IEnumerator DelayedOutro()
	{
		animator.enabled = false;
		yield return Resources.UnloadUnusedAssets();
		allowInit = true;
		yield return new WaitForSecondsIndependent(0.2f);
		if (finalWait != null)
		{
			yield return StartCoroutine(finalWait);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_loadingscreen_outro", GFAudioManager.SFX_GROUP, string.Empty);
		animator.enabled = true;
		animator.Play("outro", 0, 0f);
	}

	public static void LoadScene(string sceneName, bool showLoadingScreen = true, string previousScene = null, bool showDefaultSkin = false, IEnumerator finalWait = null)
	{
		showCover = showLoadingScreen;
		nextScene = sceneName;
		LoadingScreenCover.finalWait = finalWait;
		if (!string.IsNullOrEmpty(previousScene))
		{
			prevScene = previousScene;
		}
		lastScene = SceneManager.GetActiveScene().name;
		GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/GUI/LoadingScreenCover");
		if (objectFromResource != null && !showDefaultSkin)
		{
			objectFromResource.GetComponent<LoadingScreenCover>().AddGameSkinSprites();
		}
	}

	private void AddGameSkinSprites()
	{
		for (int i = 0; i < skinnedSprites.Count; i++)
		{
			GameSkinSprite component = skinnedSprites[i].GetComponent<GameSkinSprite>();
			if (component == null)
			{
				skinnedSprites[i].AddComponent<GameSkinSprite>();
			}
		}
	}

	public static void ReloadActiveScene(bool showLoadingScreen = true)
	{
		LoadScene(SceneManager.GetActiveScene().name, showLoadingScreen);
	}
}
