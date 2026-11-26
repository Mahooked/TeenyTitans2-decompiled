using System.Collections;
using UnityEngine;

public class UptownGate : MonoBehaviour
{
	public Animator animator;

	public Transform gateZoomTarget;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		CommunicatorManager.instance.AnimationEvent += FireOpenUptownGateAnimation;
		if (Player.instance.GetMissionCompletedData(19) != null)
		{
			SaveUtility.SetInt("OpenUptownGateAnimation", 1, false);
		}
		if (SaveUtility.GetInt("OpenUptownGateAnimation") == 1)
		{
			OpenGate();
		}
	}

	private void OnDestroy()
	{
		if (CommunicatorManager.instance != null)
		{
			CommunicatorManager.instance.AnimationEvent -= FireOpenUptownGateAnimation;
		}
	}

	public void FireOpenUptownGateAnimation(string eventName)
	{
		if (eventName.Equals("OpenUptownGateAnimation"))
		{
			StartCoroutine(eventName);
		}
	}

	private IEnumerator OpenUptownGateAnimation()
	{
		SaveUtility.SetInt("OpenUptownGateAnimation", 1);
		yield return new WaitForEndOfFrame();
		CityController.instance.cityCharacter.AllowInput = false;
		yield return new WaitForSeconds(1f);
		GameUI.instance.HideAll();
		GameUI.instance.ShowCinemaBars(0.8f);
		GameUI.instance.HideAllLandmarksBut(null);
		CameraFollow cameraFollow = CityController.instance.cameraHolder.gameObject.GetComponent<CameraFollow>();
		float previousApproachFactor = cameraFollow.approachFactor;
		cameraFollow.approachFactor = 2f;
		cameraFollow.follow = gateZoomTarget;
		yield return new WaitForSeconds(0.8f);
		animator.Play("uptownGate_open");
		yield return new WaitForSeconds(1.8f);
		GameUI.instance.HideCinemaBars(0.8f);
		yield return new WaitForSeconds(0.8f);
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		CityController.instance.cityCharacter.AllowInput = true;
		yield return CityController.instance.PlayDialogue("CONVO_ROBIN_UPTOWNGATECODE2");
		GameUI.instance.ShowAll();
		GameUI.instance.ShowAllLandmarks();
	}

	private void OpenGate()
	{
		animator.Play("uptownGate_open", 0, 1f);
	}
}
