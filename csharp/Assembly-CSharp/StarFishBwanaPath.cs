using System.Collections;
using UnityEngine;

public class StarFishBwanaPath : MonoBehaviour
{
	public GameObject[] starFish;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		CommunicatorManager.instance.AnimationEvent += FireStarFishPathAnimation;
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
		if (eventName.Equals("starfish1Bwana"))
		{
			StartCoroutine(starfishAnimation(0, eventName));
		}
		if (eventName.Equals("starfish2Bwana"))
		{
			StartCoroutine(starfishAnimation(1, eventName));
		}
		if (eventName.Equals("starfish3Bwana"))
		{
			StartCoroutine(starfishAnimation(2, eventName));
		}
		if (eventName.Equals("starfish4Bwana"))
		{
			StartCoroutine(starfishAnimation(3, eventName));
		}
		if (eventName.Equals("starfish5Bwana"))
		{
			StartCoroutine(starfishAnimation(4, eventName));
		}
		if (eventName.Equals("starfish6Bwana"))
		{
			StartCoroutine(starfishAnimation(5, eventName));
		}
		if (eventName.Equals("starfish7Bwana"))
		{
			StartCoroutine(starfishAnimation(6, eventName));
		}
	}

	private IEnumerator starfishAnimation(int starFishIndex, string starFishName)
	{
		SaveUtility.SetInt(starFishName, Player.instance.currentDay, save: false);
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.cityCharacter.StopMove();
		GameUI.instance.HideAll();
		GameUI.instance.ShowCinemaBars(1f);
		GameUI.instance.communicator.SetActive(value: false);
		GameUI.instance.HideAllLandmarksBut(null);
		CameraFollow cameraFollow = CityController.instance.cameraHolder.gameObject.GetComponent<CameraFollow>();
		float previousApproachFactor = cameraFollow.approachFactor;
		cameraFollow.approachFactor = 1f;
		cameraFollow.follow = starFish[starFishIndex].transform;
		yield return new WaitForSeconds(0.9f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_onestarfish", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(0.1f);
		CommunicatorManager.instance.FireAnimationEvent(starFishName + "Enable");
		yield return new WaitForSeconds(2f);
		GameUI.instance.HideCinemaBars(1f);
		GameUI.instance.ShowAll();
		GameUI.instance.communicator.SetActive(value: true);
		GameUI.instance.ShowAllLandmarks();
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		CityController.instance.cityCharacter.AllowInput = true;
	}
}
