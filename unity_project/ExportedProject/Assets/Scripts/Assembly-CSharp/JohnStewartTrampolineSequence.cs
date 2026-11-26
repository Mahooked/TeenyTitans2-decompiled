using System.Collections;
using UnityEngine;

public class JohnStewartTrampolineSequence : CityScriptedSequence
{
	[SerializeField]
	private CityEntityNPC johnStewartEntity;

	[SerializeField]
	private AudioClip beamSound;

	[SerializeField]
	private float beamSoundDelay;

	[SerializeField]
	private Conversation afterTrampolineSpawnConvo;

	[SerializeField]
	private PathNode playerMoveToNode;

	protected override IEnumerator Play()
	{
		GameUI.instance.communicator.SetActive(false);
		GameUI.instance.ShowCinemaBars(1f);
		yield return new WaitForSeconds(1f);
		float prevTargetFOVSmoothTime = cameraFollow.targetFOVSmoothTime;
		float prevTargetFOV = cameraFollow.targetFOV;
		cameraFollow.follow = null;
		if (playerMoveToNode != null)
		{
			CityController.instance.cityCharacter.MoveToNode(playerMoveToNode);
			yield return new WaitWhile(() => CityController.instance.cityCharacter.IsMoving);
		}
		cameraFollow.targetFOVSmoothTime = 0.3f;
		cameraFollow.targetFOV = 8f;
		if (johnStewartEntity != null && johnStewartEntity.cityNPC != null && johnStewartEntity.cityNPC.animatorController != null)
		{
			johnStewartEntity.cityNPC.animatorController.Play("beam", true);
			johnStewartEntity.cityNPC.animatorController.AddToQueue("idle");
		}
		GFAudioManager.instance.PlayClip(beamSound, 1f, false, beamSoundDelay, 128, GFAudioManager.SFX_GROUP);
		yield return new WaitForSeconds(0.7f);
		Player.instance.SetTrigger("HawkgirlTrampolineSpawned");
		yield return new WaitForSeconds(1f);
		GameUI.instance.HideCinemaBars(1f);
		yield return new WaitForSeconds(0.5f);
		cameraFollow.targetFOV = prevTargetFOV;
		yield return StartCoroutine(CityController.instance.PlayConvo(afterTrampolineSpawnConvo));
		cameraFollow.targetFOVSmoothTime = prevTargetFOVSmoothTime;
	}

	protected override void OnSequenceFinished()
	{
		GameUI.instance.communicator.SetActive(true);
	}
}
