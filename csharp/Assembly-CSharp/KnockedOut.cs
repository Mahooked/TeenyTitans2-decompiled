using System;
using System.Collections;
using UnityEngine;

public class KnockedOut : StatusEffect
{
	private Character character;

	private float duration;

	private bool isPermanent;

	private IEnumerator waitForExpireConfirmation;

	public float Duration => duration;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => false;

	public override bool isZoomedOut => false;

	public KnockedOut(Character character, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.duration = duration;
		if (duration < 0f)
		{
			duration = float.MaxValue;
			isPermanent = true;
		}
	}

	private IEnumerator WaitForExpireConfirmation(float prevDuration)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			duration = prevDuration;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		character.isKnockedOut = true;
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || isPermanent || !(duration > 0f))
		{
			return;
		}
		float prevDuration = duration;
		duration -= DeltaTime;
		if (!(duration <= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevDuration);
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
		character.isKnockedOut = false;
	}

	protected override void CleanUp()
	{
	}
}
