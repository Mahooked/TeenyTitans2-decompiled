using System.Collections;
using UnityEngine;

public class JailArea : MonoBehaviour
{
	public GameObject explosion;

	public GameObject wallToExplode1;

	public GameObject wallToExplode2;

	public GameObject wallExploded;

	public tk2dUIItem whaleUIItem;

	public Material jailAreaMaterial;

	private void OnEnable()
	{
		CityController.instance.communicator.StartDayTime += HandleStartDayTime;
		CityController.instance.communicator.StartNightTime += HandleStartNightTime;
	}

	private void OnDisable()
	{
		if (CityController.instance != null)
		{
			CityController.instance.communicator.StartDayTime -= HandleStartDayTime;
			CityController.instance.communicator.StartNightTime -= HandleStartNightTime;
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		whaleUIItem.OnClick += CityController.instance.TravelToCity;
		CommunicatorManager.instance.AnimationEvent += FireWallExplodeAnimation;
		if (SaveUtility.GetInt("JailWallExplodeAnimation") == 1)
		{
			ShowJailExplodedPath();
		}
		if (CityController.instance.communicator.isDayTime)
		{
			HandleStartDayTime();
		}
		else
		{
			HandleStartNightTime();
		}
	}

	private void OnDestroy()
	{
		if (CommunicatorManager.instance != null)
		{
			CommunicatorManager.instance.AnimationEvent -= FireWallExplodeAnimation;
		}
		if (CityController.instance != null)
		{
			whaleUIItem.OnClick -= CityController.instance.TravelToCity;
		}
	}

	private void HandleStartDayTime()
	{
		string text = jailAreaMaterial.mainTexture.name.Replace("night_", string.Empty);
		Resources.UnloadAsset(jailAreaMaterial.mainTexture);
		jailAreaMaterial.mainTexture = (Texture)Resources.Load("Textures/" + text);
	}

	private void HandleStartNightTime()
	{
		string text = jailAreaMaterial.mainTexture.name.Replace("night_", string.Empty);
		Resources.UnloadAsset(jailAreaMaterial.mainTexture);
		text = "night_" + text;
		jailAreaMaterial.mainTexture = (Texture)Resources.Load("Textures/" + text);
	}

	public void FireWallExplodeAnimation(string eventName)
	{
		if (eventName.Equals("JailWallExplodeAnimation"))
		{
			StartCoroutine(eventName);
		}
	}

	private IEnumerator JailWallExplodeAnimation()
	{
		SaveUtility.SetInt("JailWallExplodeAnimation", 1);
		CityController.instance.cityCharacter.AllowInput = false;
		GameUI.instance.HideAll();
		GameUI.instance.ShowCinemaBars(1f);
		GameUI.instance.HideAllLandmarksBut(null);
		GameUI.instance.communicator.SetActive(false);
		yield return new WaitForSeconds(0.8f);
		CameraFollow cameraFollow = CityController.instance.cameraHolder.gameObject.GetComponent<CameraFollow>();
		float previousApproachFactor = cameraFollow.approachFactor;
		cameraFollow.approachFactor = 2f;
		cameraFollow.follow = wallExploded.transform;
		yield return new WaitForSeconds(2.5f);
		explosion.SetActive(true);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_boom", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(0.15f);
		wallToExplode1.SetActive(false);
		wallToExplode2.SetActive(false);
		wallExploded.SetActive(true);
		yield return new WaitForSeconds(2f);
		GameUI.instance.HideCinemaBars(1f);
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		yield return CityController.instance.PlayDialogue("CONVO_HOOD_AFTERJAIL_WALLBREAK");
		yield return new WaitForSeconds(0.3f);
		GameObject loadingScreenDummy = ObjectPool.GetObjectFromResource("Prefabs/GUI/LoadingScreenDummy");
		Animator animator = loadingScreenDummy.GetComponent<Animator>();
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("intro"))
		{
			yield return null;
		}
		cameraFollow.approachFactor = 100f;
		cameraFollow.follow = FinalTournament.instance.cameraTarget;
		yield return new WaitForSeconds(1f);
		animator.Play("outro");
		FinalTournament.instance.animator.Play("finalTourny_intro", 0, 0f);
		yield return new WaitForEndOfFrame();
		FinalTournament.instance.animator.enabled = false;
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("outro") && animator.NormalizedTime() < 0.9f)
		{
			yield return null;
		}
		Object.Destroy(loadingScreenDummy);
		Debug.Log("FireFinalTournamnentIntro");
		FinalTournament.instance.animator.enabled = true;
		FinalTournament.instance.FireFinalTournamnentIntro();
		yield return new WaitForSeconds(5.5f);
		loadingScreenDummy = ObjectPool.GetObjectFromResource("Prefabs/GUI/LoadingScreenDummy");
		animator = loadingScreenDummy.GetComponent<Animator>();
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("intro"))
		{
			yield return null;
		}
		cameraFollow.approachFactor = 100f;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		yield return new WaitForSeconds(1f);
		animator.Play("outro");
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("outro") && animator.NormalizedTime() < 0.9f)
		{
			yield return null;
		}
		Object.Destroy(loadingScreenDummy);
		yield return new WaitForSeconds(1f);
		GameUI.instance.communicator.SetActive(true);
		CityController.instance.communicator.ActivateMissions();
		CityController.instance.communicator.OnClickMissions();
		GameUI.instance.ShowAll();
		GameUI.instance.ShowAllLandmarks();
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		CityController.instance.cityCharacter.AllowInput = true;
	}

	private void ShowJailExplodedPath()
	{
		wallToExplode1.SetActive(false);
		wallToExplode2.SetActive(false);
		wallExploded.SetActive(true);
	}
}
