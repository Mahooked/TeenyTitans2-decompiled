using System;
using System.Collections;
using UnityEngine;

public class Smokescreen : StatusEffect
{
	private SmokeBomb smokeBomb;

	private ActionMeter targetMeter;

	private float duration = 5f;

	private int dodgeBonus = 25;

	private int counterDamage;

	private BattleController battleController;

	private Character target;

	private float deathCounter;

	private Character counterTarget;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => true;

	public override bool isResettable => false;

	public override bool isZoomedOut => true;

	public Smokescreen(SmokeBomb smokeBomb, ActionMeter targetMeter, float duration = 5f, int dodgeBonus = 25, int counterDamage = 0, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.smokeBomb = smokeBomb;
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.dodgeBonus = dodgeBonus;
		this.counterDamage = counterDamage;
	}

	private void HandleCharacterChanged()
	{
		if (target != null)
		{
			target.tempDodgeBonus -= dodgeBonus;
			target.Dodged -= HandleDodged;
			target.Hurt -= HandleHurt;
		}
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
		if (target != null)
		{
			target.tempDodgeBonus += dodgeBonus;
			target.Dodged += HandleDodged;
			target.Hurt += HandleHurt;
		}
	}

	private void HandleDodged(DamageResult damageResult)
	{
		if (counterDamage > 0 && damageResult.source != null)
		{
			if (battleController.waiting)
			{
				counterTarget = damageResult.source;
				battleController.WaitingEnded += HandleWaitingEnded;
			}
			else
			{
				damageResult.source.DoDamage(new DamageResult(counterDamage, ImpactType.Small));
			}
		}
		Remove();
	}

	private void HandleHurt()
	{
		Remove();
	}

	private void HandleWaitingEnded()
	{
		battleController.WaitingEnded -= HandleWaitingEnded;
		if (counterTarget != null)
		{
			counterTarget.DoDamage(new DamageResult(counterDamage, ImpactType.Small));
		}
	}

	private IEnumerator WaitForExpireConfirmation(float prevDeathCounter)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
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
		deathCounter = duration;
		battleController = BattleController.instance;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || !(deathCounter > 0f))
		{
			return;
		}
		float prevDeathCounter = deathCounter;
		deathCounter -= DeltaTime;
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
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		targetMeter.CharacterChanged -= HandleCharacterChanged;
		if (target != null)
		{
			target.tempDodgeBonus -= dodgeBonus;
			target.Dodged -= HandleDodged;
			target.Hurt -= HandleHurt;
		}
	}

	protected override void CleanUp()
	{
		smokeBomb.TurnOff();
	}
}
