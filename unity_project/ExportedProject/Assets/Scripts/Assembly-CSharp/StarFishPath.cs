using System.Collections;
using DG.Tweening;
using UnityEngine;

public class StarFishPath : MonoBehaviour
{
	public GameObject[] starFish;

	public GameObject beachTournament;

	public GameObject beachTournamentPad;

	public GameObject beachTournamentPath;

	public Animator beachTournamentAnimator;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		CommunicatorManager.instance.AnimationEvent += FireStarFishPathAnimation;
		if (Player.instance.GetMissionCompletedData(16) != null)
		{
			SaveUtility.SetInt("StarFishPathAnimation", 1, false);
		}
		if (SaveUtility.GetInt("StarFishPathAnimation") == 1)
		{
			ShowBeachTournament();
		}
		else
		{
			beachTournamentPath.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		if (CommunicatorManager.instance != null)
		{
			CommunicatorManager.instance.AnimationEvent -= FireStarFishPathAnimation;
		}
	}

	public void FireStarFishPathAnimation(string eventName)
	{
		if (eventName.Equals("StarFishPathAnimation"))
		{
			StartCoroutine(eventName);
		}
	}

	private IEnumerator StarFishPathAnimation()
	{
		SaveUtility.SetInt("StarFishPathAnimation", 1);
		CityController.instance.cityCharacter.AllowInput = false;
		GameUI.instance.HideAll();
		GameUI.instance.ShowCinemaBars(1f);
		GameUI.instance.communicator.SetActive(false);
		GameUI.instance.HideAllLandmarksBut(null);
		yield return new WaitForSeconds(1f);
		CommunicatorManager.instance.FireAnimationEvent("PlayClamSummon");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_jumpcity_atlantisrises", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(1f);
		CameraFollow cameraFollow = CityController.instance.cameraHolder.gameObject.GetComponent<CameraFollow>();
		float previousApproachFactor = cameraFollow.approachFactor;
		cameraFollow.approachFactor = 1f;
		yield return new WaitForEndOfFrame();
		cameraFollow.follow = starFish[0].transform;
		CommunicatorManager.instance.FireAnimationEvent("starfish1");
		yield return new WaitForSeconds(1f);
		cameraFollow.follow = starFish[1].transform;
		CommunicatorManager.instance.FireAnimationEvent("starfish2");
		yield return new WaitForSeconds(1f);
		cameraFollow.follow = starFish[2].transform;
		CommunicatorManager.instance.FireAnimationEvent("starfish3");
		yield return new WaitForSeconds(1f);
		cameraFollow.follow = starFish[3].transform;
		CommunicatorManager.instance.FireAnimationEvent("starfish4");
		yield return new WaitForSeconds(1f);
		cameraFollow.follow = starFish[4].transform;
		CommunicatorManager.instance.FireAnimationEvent("starfish5");
		yield return new WaitForSeconds(1f);
		cameraFollow.follow = beachTournament.transform;
		beachTournament.SetActive(true);
		beachTournamentAnimator.Play("beachTourny_intro");
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		beachTournament.GetComponent<BoxCollider>().enabled = false;
		Landmark landmark = beachTournament.GetComponent<Landmark>();
		if (landmark != null && landmark.icon != null)
		{
			landmark.icon.gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(4.8f);
		DOTween.To(() => CityController.instance.pinchZoomer.targetOffset, (Vector3 x) =>
		{
			CityController.instance.pinchZoomer.targetOffset = x;
		}, new Vector3(0f, 0.4f, -4f), 1f);
		yield return new WaitForSeconds(3.2f);
		if (landmark != null && landmark.icon != null)
		{
			landmark.icon.gameObject.SetActive(true);
			landmark.icon.hideAtMinScale = false;
			landmark.icon.hideWhenInView = false;
		}
		yield return new WaitForSeconds(1f);
		CommunicatorManager.instance.FireAnimationEvent("PlayClamIdleClosed");
		CityController.instance.pinchZoomer.targetOffset = Vector3.zero;
		GameUI.instance.HideCinemaBars(1f);
		GameUI.instance.ShowAll();
		GameUI.instance.communicator.SetActive(true);
		GameUI.instance.ShowAllLandmarks();
		beachTournamentPath.SetActive(true);
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		CityController.instance.cityCharacter.AllowInput = true;
		yield return new WaitForSeconds(3f);
		beachTournament.GetComponent<BoxCollider>().enabled = true;
		if (landmark != null && landmark.icon != null)
		{
			landmark.icon.hideAtMinScale = true;
			landmark.icon.hideWhenInView = true;
		}
	}

	private void ShowBeachTournament()
	{
		beachTournamentPath.SetActive(true);
		beachTournament.SetActive(true);
		GameObject[] array = starFish;
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponentInChildren<Animator>().Play("starfish_loop", 0, 1f);
		}
		StartCoroutine("ShowBeachTournamentRoutine");
	}

	private IEnumerator ShowBeachTournamentRoutine()
	{
		beachTournamentAnimator.Play("beachTourny_loop", 0, 1f);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		beachTournamentAnimator.enabled = true;
	}
}
