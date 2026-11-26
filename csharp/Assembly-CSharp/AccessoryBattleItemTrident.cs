using System.Collections;
using UnityEngine;

public class AccessoryBattleItemTrident : AccessoryBattleItem
{
	public GameObject tickEffect;

	public Animator tickEffectAnimator;

	public Transform tickEffectSpawnPosition;

	private Camera tempCamera;

	private Coroutine disableCameraRoutine;

	private void Start()
	{
		tickEffect.gameObject.SetActive(value: false);
		BattleController.instance.WaitingStarted += HandleWaitingStarted;
		BattleController.instance.WaitingEnded += HandleWaitingEnded;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		tickEffect.SetLayerRecursively(LayerMask.NameToLayer("Default"));
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		tickEffect.gameObject.SetActive(value: false);
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
		if (evt.stringParameter.Equals("hit") || evt.stringParameter.Equals("introTick"))
		{
			tickEffect.gameObject.SetActive(value: true);
			tickEffect.transform.position = tickEffectSpawnPosition.position;
			tickEffect.SetLayerRecursively(LayerMask.NameToLayer("Overlay"));
			if (tempCamera == null)
			{
				GameObject gameObject = new GameObject("TempCamera");
				tempCamera = gameObject.AddComponent<Camera>();
			}
			else
			{
				tempCamera.gameObject.SetActive(value: true);
			}
			BattleCamera battleCamera = ((!(targetCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
			tempCamera.CopyFrom(battleCamera.camera);
			tempCamera.transform.parent = battleCamera.camera.transform;
			tempCamera.cullingMask = LayerMask.GetMask("Overlay");
			tempCamera.depth = 3f;
		}
		if (evt.stringParameter.Equals("complete") || evt.stringParameter.Equals("introComplete"))
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
		while (tickEffectAnimator.IsPlaying())
		{
			yield return null;
		}
		tickEffect.gameObject.SetActive(value: false);
		if (tempCamera != null)
		{
			tempCamera.gameObject.SetActive(value: false);
			tempCamera.transform.parent = null;
		}
		disableCameraRoutine = null;
	}

	private void HandleWaitingStarted()
	{
		if (tempCamera != null)
		{
			tempCamera.gameObject.SetActive(value: false);
		}
	}

	private void HandleWaitingEnded()
	{
		if (tempCamera != null)
		{
			tempCamera.gameObject.SetActive(value: true);
		}
	}
}
