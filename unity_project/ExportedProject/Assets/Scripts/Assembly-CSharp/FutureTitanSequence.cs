using System.Collections;
using UnityEngine;

public class FutureTitanSequence : CityScriptedSequence
{
	[SerializeField]
	private GameObject character;

	[SerializeField]
	private AEAnimatorTk2d characterAnimator;

	[SerializeField]
	private Conversation conversation;

	protected override IEnumerator Play()
	{
		GameUI.instance.communicator.SetActive(false);
		float prevTargetFOVSmoothTime = cameraFollow.targetFOVSmoothTime;
		float prevTargetFOV = cameraFollow.targetFOV;
		GameUI.instance.ShowCinemaBars(1f);
		yield return new WaitForSeconds(1f);
		cameraFollow.targetFOVSmoothTime = 0.3f;
		cameraFollow.targetFOV = 10f;
		cameraFollow.follow = base.transform;
		yield return new WaitForSeconds(0.75f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_city_teleporter", GFAudioManager.SFX_GROUP, string.Empty);
		character.SetActive(true);
		characterAnimator.RefreshFlipped();
		ScreenFill.instance.Flash(Color.white, 0.35f);
		yield return StartCoroutine(CityController.instance.PlayConvo(conversation));
		yield return new WaitForSeconds(1f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_city_teleporter", GFAudioManager.SFX_GROUP, string.Empty);
		ScreenFill.instance.Flash(Color.white, 0.35f);
		character.SetActive(false);
		cameraFollow.targetFOV = prevTargetFOV;
		cameraFollow.targetFOVSmoothTime = prevTargetFOVSmoothTime;
		GameUI.instance.HideCinemaBars(1f);
		yield return new WaitForSeconds(1f);
	}

	protected override void OnSequenceFinished()
	{
		GameUI.instance.communicator.SetActive(true);
	}
}
