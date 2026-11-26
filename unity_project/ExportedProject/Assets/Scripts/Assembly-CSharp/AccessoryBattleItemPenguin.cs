using System.Collections;
using UnityEngine;

public class AccessoryBattleItemPenguin : AccessoryBattleItem
{
	public ParticleSystem frostParticleSystem;

	private Camera tempCamera;

	private Coroutine disableCameraRoutine;

	private void Start()
	{
		BattleController.instance.WaitingStarted += HandleWaitingStarted;
		BattleController.instance.WaitingEnded += HandleWaitingEnded;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		frostParticleSystem.gameObject.layer = LayerMask.NameToLayer("Default");
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		frostParticleSystem.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		if (BattleController.instance != null)
		{
			BattleController.instance.WaitingStarted += HandleWaitingStarted;
			BattleController.instance.WaitingEnded += HandleWaitingEnded;
		}
		if (tempCamera != null)
		{
			Object.Destroy(tempCamera);
		}
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		if (evt.stringParameter.Equals("particleOn"))
		{
			frostParticleSystem.Clear();
			frostParticleSystem.Play();
			frostParticleSystem.gameObject.layer = LayerMask.NameToLayer("Overlay");
			if (tempCamera == null)
			{
				GameObject gameObject = new GameObject("TempCamera");
				tempCamera = gameObject.AddComponent<Camera>();
			}
			else
			{
				tempCamera.gameObject.SetActive(true);
			}
			BattleCamera battleCamera = ((!(targetCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
			tempCamera.CopyFrom(battleCamera.camera);
			tempCamera.transform.parent = battleCamera.camera.transform;
			tempCamera.cullingMask = LayerMask.GetMask("Overlay");
			tempCamera.depth = 3f;
		}
		if (evt.stringParameter.Equals("complete"))
		{
			if (disableCameraRoutine != null)
			{
				StopCoroutine(disableCameraRoutine);
			}
			disableCameraRoutine = StartCoroutine(WaitToDisableCamera());
		}
		base.HandleEventTriggered(evt);
	}

	private IEnumerator WaitToDisableCamera()
	{
		while (frostParticleSystem.particleCount > 0)
		{
			yield return null;
		}
		frostParticleSystem.gameObject.SetActive(false);
		if (tempCamera != null)
		{
			tempCamera.gameObject.SetActive(false);
			tempCamera.transform.parent = null;
		}
		disableCameraRoutine = null;
	}

	private void HandleWaitingStarted()
	{
		if (tempCamera != null)
		{
			tempCamera.gameObject.SetActive(false);
		}
	}

	private void HandleWaitingEnded()
	{
		if (tempCamera != null)
		{
			tempCamera.gameObject.SetActive(true);
		}
	}
}
