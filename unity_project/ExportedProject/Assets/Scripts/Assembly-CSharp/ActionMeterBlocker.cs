using System;
using System.Collections;
using UnityEngine;

public class ActionMeterBlocker : StatusEffect
{
	private ActionMeter _actionMeter;

	private float amount;

	private float timeRemaining;

	private Transform waffle;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type
	{
		get
		{
			return StatusEffectType.Debuff;
		}
	}

	public override bool isStackable
	{
		get
		{
			return false;
		}
	}

	public override bool isResettable
	{
		get
		{
			return false;
		}
	}

	public override bool isZoomedOut
	{
		get
		{
			return false;
		}
	}

	public ActionMeterBlocker(ActionMeter meter, float amount, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		_actionMeter = meter;
		this.amount = amount;
		timeRemaining = duration;
	}

	private IEnumerator WaitForExpireConfirmation(float prevTimeRemaining)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(base.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			timeRemaining = prevTimeRemaining;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		_actionMeter.maxScaleFactor -= amount;
		waffle = ObjectPool.GetObjectFromResource("Prefabs/FX/BarWaffle", default(Vector3), false).transform;
		waffle.parent = _actionMeter.transform;
		waffle.localPosition = new Vector3(175f, 2f, -6f);
		waffle.gameObject.SetActive(true);
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || !(timeRemaining > 0f))
		{
			return;
		}
		float prevTimeRemaining = timeRemaining;
		timeRemaining -= base.DeltaTime;
		if (!(timeRemaining <= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevTimeRemaining);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		_actionMeter.maxScaleFactor += amount;
		ObjectPool.PoolObject(waffle.gameObject);
		waffle = null;
	}

	protected override void CleanUp()
	{
	}
}
