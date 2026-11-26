using System.Collections;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

public class LODBuildingRevealSequence : CityScriptedSequence
{
	public Animator buildingAnimator;

	[Header("Conversations")]
	public Conversation lodBuildingConvo1;

	public Conversation lodBuildingConvo2;

	public Conversation lodBuildingConvo3;

	[Header("Camera Fields")]
	public float shakeStrength = 0.015f;

	public Vector3 cameraOffsetBuildingRising;

	public Vector3 cameraOffsetBuildingTalking;

	public float cameraPanToBuildingSpeed;

	public float cameraPanToBuildingTalkingSpeed;

	public float cameraPanToCharSpeed;

	public float cameraBuildingTargetFOV = 25f;

	public float cameraBuildingTargetFOVSmoothTime = 0.3f;

	public float cameraBuildingTargetFOVSmoothTimeToChar = 0.6f;

	[Header("Sound Effects")]
	public AudioClip rumbleSFX;

	public AudioClip broadcastSFX;

	public AudioClip buildingRisingSFX;

	protected override IEnumerator Play()
	{
		buildingAnimator.Play("LOD_building_intro", 0, 0f);
		buildingAnimator.speed = 0f;
		CityController.instance.FadeOutMusic(2.5f);
		Tweener shakeTween = cameraFollow.camera.DOShakePosition(1000f, shakeStrength).SetEase(Ease.Linear);
		AudioSource rumbleAudioSource = GFAudioManager.instance.PlayClip(rumbleSFX, 1f, true, 0f, 128, GFAudioManager.SFX_GROUP);
		yield return new WaitForSeconds(1.5f);
		CityController.instance.preventShowingCommunicatorAfterDialogue = true;
		yield return StartCoroutine(CityController.instance.PlayConvo(lodBuildingConvo1));
		GameUI.instance.ShowCinemaBars(1f);
		cameraFollow.follow = null;
		Tweener panTween = cameraFollow.transform.DOMove(base.transform.position + cameraOffsetBuildingRising, cameraPanToBuildingSpeed).SetSpeedBased().SetEase(Ease.InOutCubic);
		float prevFOV = cameraFollow.targetFOV;
		float prevFOVTime = cameraFollow.targetFOVSmoothTime;
		cameraFollow.targetFOV = cameraBuildingTargetFOV;
		cameraFollow.targetFOVSmoothTime = cameraBuildingTargetFOVSmoothTime;
		panTween.ForceInit();
		rumbleAudioSource.DOFade(0.5f, panTween.Duration()).SetEase(Ease.Linear);
		yield return panTween.WaitForCompletion();
		buildingAnimator.Play("LOD_building_intro", 0, 0f);
		buildingAnimator.speed = 1f;
		GFAudioManager.instance.PlayClip(buildingRisingSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		yield return null;
		yield return new WaitUntil(() => !buildingAnimator.IsName("LOD_building_intro"));
		rumbleAudioSource.DOFade(0f, 1f).SetEase(Ease.Linear).OnComplete(() =>
		{
			rumbleAudioSource.Stop();
			rumbleAudioSource.volume = 1f;
		});
		shakeTween.Kill(true);
		shakeTween = cameraFollow.camera.DOShakePosition(2f, shakeStrength).SetEase(Ease.Linear);
		panTween = cameraFollow.transform.DOMove(base.transform.position + cameraOffsetBuildingTalking, cameraPanToBuildingTalkingSpeed).SetSpeedBased().SetEase(Ease.OutSine);
		yield return panTween.WaitForCompletion();
		buildingAnimator.Play("LOD_building_announcementIntro");
		AudioSource broadcastSource = GFAudioManager.instance.PlayClip(broadcastSFX, 1f, true, 0f, 128, GFAudioManager.SFX_GROUP);
		CityController.instance.preventShowingCommunicatorAfterDialogue = true;
		yield return StartCoroutine(CityController.instance.ShowDialogue(lodBuildingConvo2, false, null, null, false, 0f, false));
		buildingAnimator.Play("LOD_building_announcementOutro");
		GameUI.instance.HideCinemaBars(1f);
		if (broadcastSource != null)
		{
			broadcastSource.DOFade(0f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
			{
				broadcastSource.Stop();
				broadcastSource.volume = 1f;
			});
		}
		yield return new WaitForSeconds(0.5f);
		yield return new WaitUntil(() => !buildingAnimator.IsPlaying());
		panTween = cameraFollow.transform.DOMove(CityController.instance.cityCharacter.transform.position, cameraPanToCharSpeed).SetSpeedBased().SetEase(Ease.InOutCubic);
		cameraFollow.targetFOV = prevFOV;
		cameraFollow.targetFOVSmoothTime = cameraBuildingTargetFOVSmoothTimeToChar;
		yield return panTween.WaitForCompletion();
		cameraFollow.targetFOVSmoothTime = prevFOVTime;
		CityController.instance.PlayCurrentLocationMusic();
		CityController.instance.preventShowingCommunicatorAfterDialogue = true;
		yield return StartCoroutine(CityController.instance.PlayConvo(lodBuildingConvo3));
		SaveUtility.SetString("PlayerPosition", CityController.instance.cityCharacter.transform.position.ToString(), false);
		SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, false);
		Player.instance.SetTrigger("LODBuildingActivated");
		Player.instance.Save();
	}
}
