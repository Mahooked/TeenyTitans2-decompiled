using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cute : StatusEffect
{
	private Character character;

	private ActionMeter actionMeter;

	private float duration;

	private float deathCounter;

	private DamageResult resultToCounter;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type
	{
		get
		{
			return StatusEffectType.Buff;
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
			return true;
		}
	}

	public override bool isZoomedOut
	{
		get
		{
			return false;
		}
	}

	public Cute(Character character, ActionMeter actionMeter, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.actionMeter = actionMeter;
		this.duration = duration;
	}

	private void HandleDamageReceived(DamageResult damageResult)
	{
		if (damageResult.source != null && damageResult.isDirect)
		{
			resultToCounter = damageResult;
		}
	}

	private void HandleCharacterChanged()
	{
		if (actionMeter.character != null && actionMeter.character.character == character)
		{
			actionMeter.character.cuteWeight = 1f;
		}
	}

	private IEnumerator WaitForExpireConfirmation(float prevDeathCounter)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(base.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			deathCounter = prevDeathCounter;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		character.DamageReceived += HandleDamageReceived;
		actionMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		deathCounter = duration;
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting)
		{
			return;
		}
		if (resultToCounter != null)
		{
			resultToCounter.source.DoDamage(new DamageResult(Mathf.CeilToInt((float)resultToCounter.totalDamage * 0.5f), ImpactType.Small));
			resultToCounter = null;
		}
		if (!(deathCounter > 0f))
		{
			return;
		}
		float prevDeathCounter = deathCounter;
		deathCounter -= base.DeltaTime;
		if (!(deathCounter <= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevDeathCounter);
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
		deathCounter = duration;
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		actionMeter.CharacterChanged -= HandleCharacterChanged;
		if (actionMeter.character != null && actionMeter.character.character == character)
		{
			DOTween.To(() => actionMeter.character.cuteWeight, (float x) =>
			{
				actionMeter.character.cuteWeight = x;
			}, 0f, 0.25f);
		}
		character.DamageReceived -= HandleDamageReceived;
		character = null;
	}

	protected override void CleanUp()
	{
	}
}
