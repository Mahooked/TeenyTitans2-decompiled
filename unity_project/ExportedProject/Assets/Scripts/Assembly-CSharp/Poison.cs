using System;
using System.Collections;
using UnityEngine;

public class Poison : StatusEffect
{
	private PoisonPellet poisonPellet;

	private ActionMeter targetMeter;

	private Character caster;

	private float duration;

	private int damage;

	private float damageInterval;

	private Character target;

	private float deathCounter;

	private float attackCounter;

	private int damagePerAttack;

	private IEnumerator waitForTickConfirmation;

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
			return true;
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
			return true;
		}
	}

	public Poison(PoisonPellet poisonPellet, ActionMeter targetMeter, Character caster, float duration = 12f, int damage = 1, float damageInterval = 3f, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.poisonPellet = poisonPellet;
		this.targetMeter = targetMeter;
		this.caster = caster;
		this.duration = duration;
		this.damage = damage;
		this.damageInterval = damageInterval;
	}

	private void HandleCharacterChanged()
	{
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
	}

	public override void Tick()
	{
		DamageResult damageResult = GetDamageResult();
		target.DoDamage(damageResult);
	}

	private IEnumerator WaitForTickConfirmation(float prevAttackCounter, float prevDeathCounter)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectTick(base.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForTickConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			attackCounter = prevAttackCounter;
			deathCounter = prevDeathCounter;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Tick();
		UnityEngine.Random.state = prevRandomState;
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
		deathCounter = duration;
		attackCounter = damageInterval;
		int num = (int)(duration / damageInterval);
		damagePerAttack = damage / num;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
	}

	protected override void UpdateEffect()
	{
		if (target == null || BattleController.instance.waiting || !(deathCounter > 0f))
		{
			return;
		}
		float prevAttackCounter = attackCounter;
		float prevDeathCounter = deathCounter;
		attackCounter -= base.DeltaTime;
		if (attackCounter <= 0f)
		{
			attackCounter += damageInterval;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForTickConfirmation == null)
				{
					waitForTickConfirmation = WaitForTickConfirmation(prevAttackCounter, deathCounter);
					BattleController.instance.StartCoroutine(waitForTickConfirmation);
				}
			}
			else
			{
				Tick();
			}
		}
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
	}

	protected override void RemoveEffect()
	{
		if (waitForTickConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForTickConfirmation);
			waitForTickConfirmation = null;
		}
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		targetMeter.CharacterChanged -= HandleCharacterChanged;
	}

	protected override void CleanUp()
	{
		if (poisonPellet != null)
		{
			poisonPellet.TurnOff();
		}
	}

	private DamageResult GetDamageResult()
	{
		DamageResult damageResult = new DamageResult(damagePerAttack, ImpactType.Small);
		damageResult.source = caster;
		damageResult.target = target;
		damageResult.isDirect = false;
		if (UnityEngine.Random.Range(1, 100) <= Mathf.FloorToInt((float)caster.luck * caster.luckModifier) + caster.tempLuckBonus)
		{
			damageResult.luckBonusDamage = Mathf.CeilToInt((float)damagePerAttack * GameManager.instance.gameSettings.luckyBonusDamage);
		}
		if (AbilityScript.classStrengths.ContainsKey(caster.characterDefinition.characterClass) && AbilityScript.classStrengths[caster.characterDefinition.characterClass] == target.characterDefinition.characterClass)
		{
			int num = (int)Mathf.Max(1f, (float)damagePerAttack * GameManager.instance.gameSettings.classBonusDamage);
			if (ChallengeModeManager.isChallengeModeActive)
			{
				num = num;
			}
			damageResult.classBonusDamage = num;
		}
		damageResult.bonusClass = caster.characterDefinition.characterClass;
		if (target.isAlive && UnityEngine.Random.Range(1, 100) <= Mathf.FloorToInt((float)target.dodge * target.dodgeModifier) + target.tempDodgeBonus)
		{
			damageResult.isDodged = true;
		}
		return damageResult;
	}
}
