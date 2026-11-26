using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SladeLawOfficeRevealSequence : CityScriptedSequence
{
	[SerializeField]
	private Conversation preTourneyConvoPart1;

	[SerializeField]
	private Conversation preTourneyConvoPart2;

	[SerializeField]
	private Conversation preTourneyConvoPart3;

	[SerializeField]
	private Animator sladeBuildingAnimator;

	[SerializeField]
	private PathNode playerMoveToNode;

	[SerializeField]
	private GameObject buildingGlint;

	[Space]
	[SerializeField]
	private float musicFadeDuration = 2f;

	[SerializeField]
	private float vibrationStrength = 0.1f;

	[SerializeField]
	[Tooltip("The number of seconds to wait after vibrating begins before playing the second conversation.")]
	private float delayAfterVibrationStart = 2f;

	[SerializeField]
	private Vector3 targetCameraOffsetOnReveal = new Vector3(0.15f, 0.91f, -6.68f);

	[SerializeField]
	private float cameraMoveDuration = 5f;

	[SerializeField]
	private float delayBeforeOpenAnimation;

	[Space]
	[SerializeField]
	private AudioClip rumbleSound;

	[SerializeField]
	private AudioClip evilMusic;

	[SerializeField]
	private AudioClip openSound1;

	[SerializeField]
	private float openSound1Delay;

	[SerializeField]
	private AudioClip openSound2;

	[SerializeField]
	private float openSound2Delay;

	protected override void OnDestroy()
	{
		base.OnDestroy();
		GFAudioManager.instance.FadeOut("evilMusic", 0.25f);
	}

	protected override IEnumerator Play()
	{
		GameUI.instance.communicator.SetActive(value: false);
		if (buildingGlint != null)
		{
			buildingGlint.SetActive(value: false);
		}
		CityController.instance.FadeOutMusic(musicFadeDuration);
		yield return StartCoroutine(CityController.instance.PlayConvo(preTourneyConvoPart1));
		Tween vibration = cameraFollow.camera.DOShakePosition(1000f, vibrationStrength, 30).SetEase(Ease.Linear);
		GFAudioManager.instance.PlayClip(rumbleSound, 1f, loop: true, 0f, 128, GFAudioManager.SFX_GROUP);
		yield return new WaitForSeconds(delayAfterVibrationStart);
		yield return StartCoroutine(CityController.instance.PlayConvo(preTourneyConvoPart2));
		GFAudioManager.instance.PlayClip(evilMusic, "evilMusic", 1f, loop: true, 0f, 128, GFAudioManager.MUSIC_GROUP);
		GameUI.instance.ShowCinemaBars(1f);
		yield return new WaitForSeconds(1f);
		cameraFollow.follow = null;
		if (playerMoveToNode != null)
		{
			CityController.instance.cityCharacter.MoveToNode(playerMoveToNode);
			yield return new WaitWhile(() => CityController.instance.cityCharacter.IsMoving);
		}
		Tweener cameraMove = DOTween.To(() => cameraPinchZoomer.targetOffset, (Vector3 x) =>
		{
			cameraPinchZoomer.targetOffset = x;
		}, targetCameraOffsetOnReveal, cameraMoveDuration).SetEase(Ease.InOutSine);
		GFAudioManager.instance.PlayClip(openSound1, 1f, loop: false, openSound1Delay, 128, GFAudioManager.SFX_GROUP);
		GFAudioManager.instance.PlayClip(openSound2, 1f, loop: false, openSound2Delay, 128, GFAudioManager.SFX_GROUP);
		yield return new WaitForSeconds(delayBeforeOpenAnimation);
		Player.instance.SetTrigger("openSladeBuilding");
		yield return new WaitWhile(() => sladeBuildingAnimator.IsPlaying());
		vibration.Kill(complete: true);
		GFAudioManager.instance.StopByClip(rumbleSound);
		if (cameraMove.IsPlaying())
		{
			yield return new WaitWhile(() => cameraMove.IsPlaying());
		}
		yield return StartCoroutine(CityController.instance.PlayConvo(preTourneyConvoPart3));
		yield return DOTween.To(() => cameraPinchZoomer.targetOffset, (Vector3 x) =>
		{
			cameraPinchZoomer.targetOffset = x;
		}, Vector3.zero, 1f).SetEase(Ease.InOutSine).WaitForCompletion();
		GFAudioManager.instance.FadeOut("evilMusic", 0.25f);
		GameUI.instance.HideCinemaBars(1f);
		yield return new WaitForSeconds(1f);
		CityController.instance.PlayCurrentLocationMusic();
	}

	protected override void OnSequenceFinished()
	{
		GameUI.instance.communicator.SetActive(value: true);
	}
}
