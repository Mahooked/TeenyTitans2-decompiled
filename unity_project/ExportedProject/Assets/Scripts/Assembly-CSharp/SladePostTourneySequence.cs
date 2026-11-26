using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SladePostTourneySequence : CityScriptedSequence
{
	[SerializeField]
	private Conversation postTourneyConvoPart1;

	[SerializeField]
	private Conversation postTourneyConvoPart2;

	[SerializeField]
	private Animator sladeBuildingAnimator;

	[SerializeField]
	private Vector3 targetCameraOffsetOnStart = new Vector3(0.15f, 0.91f, -6.68f);

	[SerializeField]
	private float vibrationStrength = 0.1f;

	[SerializeField]
	private float cameraReturnDuration = 3f;

	[Space]
	[SerializeField]
	private AudioClip outroSound1;

	[SerializeField]
	private float outroSound1Delay;

	protected override IEnumerator Play()
	{
		GameUI.instance.communicator.SetActive(false);
		Transform prevFollowTarget = cameraFollow.follow;
		cameraFollow.follow = null;
		cameraPinchZoomer.targetOffset = targetCameraOffsetOnStart;
		yield return StartCoroutine(CityController.instance.PlayConvo(postTourneyConvoPart1));
		GFAudioManager.instance.PlayClip(outroSound1, 1f, false, outroSound1Delay, 128, GFAudioManager.SFX_GROUP);
		Player.instance.SetTrigger("exitSladeBuilding");
		yield return new WaitForSeconds(1.5f);
		Tween vibration = cameraFollow.camera.DOShakePosition(2f, vibrationStrength, 30).SetEase(Ease.InSine);
		yield return new WaitWhile(() => sladeBuildingAnimator.IsPlaying());
		yield return DOTween.To(() => cameraPinchZoomer.targetOffset, (Vector3 x) =>
		{
			cameraPinchZoomer.targetOffset = x;
		}, Vector3.zero, cameraReturnDuration).SetEase(Ease.InOutSine).WaitForCompletion();
		cameraFollow.follow = prevFollowTarget;
		yield return StartCoroutine(CityController.instance.PlayConvo(postTourneyConvoPart2));
	}

	protected override void OnSequenceFinished()
	{
		GameUI.instance.communicator.SetActive(true);
	}
}
