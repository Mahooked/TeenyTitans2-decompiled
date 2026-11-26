using UnityEngine;

public class CharacterStatBuff : StatusEffect
{
	private Character targetCharacter;

	private ActionMeter targetMeter;

	private CharacterStatType statType;

	private int statBonus;

	private float statModifier;

	public override StatusEffectType type
	{
		get
		{
			return (statBonus > 0 || statModifier > 0f) ? StatusEffectType.Buff : StatusEffectType.Debuff;
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
			return false;
		}
	}

	public CharacterStatBuff(Character targetCharacter, ActionMeter targetMeter, CharacterStatType statType, int statBonus, float statModifier = 0f, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetCharacter = targetCharacter;
		this.targetMeter = targetMeter;
		this.statType = statType;
		this.statBonus = statBonus;
		this.statModifier = statModifier;
	}

	protected virtual void HandleDamageDealt(DamageResult damageResult)
	{
	}

	protected virtual void HandleHealDealt(HealResult healResult)
	{
	}

	protected override void InitEffect()
	{
		if (statBonus != 0)
		{
			targetCharacter.DoBuff(new BuffResult(statType, statBonus, null, null, statType == CharacterStatType.Attack));
		}
		if (statModifier != 0f)
		{
			targetCharacter.DoBuff(new BuffResult(statModifier, statType, null, null, false));
		}
		targetCharacter.DamageDealt += HandleDamageDealt;
		targetCharacter.HealDealt += HandleHealDealt;
		int count = targetMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			targetMeter.abilityButtons[i].buffFireSprite.color = Color.magenta;
		}
	}

	protected override void UpdateEffect()
	{
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
		if (statBonus != 0)
		{
			targetCharacter.DoBuff(new BuffResult(statType, -statBonus, null, null, false));
		}
		if (statModifier != 0f)
		{
			targetCharacter.DoBuff(new BuffResult(0f - statModifier, statType, null, null, false));
		}
		targetCharacter.DamageDealt -= HandleDamageDealt;
		targetCharacter.HealDealt -= HandleHealDealt;
		int count = targetMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			targetMeter.abilityButtons[i].buffFireSprite.color = Color.white;
		}
	}

	protected override void CleanUp()
	{
	}
}
