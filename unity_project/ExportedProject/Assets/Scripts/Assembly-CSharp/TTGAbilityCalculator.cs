using UnityEngine;

public static class TTGAbilityCalculator
{
	public static int CalculateDamage(AbilityData ability, Character character, int slotPosition, bool useAttackStat = false, bool useAttackModifier = false, bool useTempBonus = false, bool useShiny = false, bool useRangedIntValue2 = false, int minDamage = 1)
	{
		if (ability.type != AbilityType.Attack && (ability.type != AbilityType.StatusEffect || (ability.statusEffect != StatusEffectName.AttackDrone && ability.statusEffect != StatusEffectName.Poison)))
		{
			Debug.LogError("The supplied ability is not of the right type for calculating total damage");
			return 0;
		}
		float percent = (float)slotPosition / 100f;
		int num = Mathf.RoundToInt(useRangedIntValue2 ? ability.rangedIntValue2.GetInterpolatedValue(percent) : ability.rangedIntValue1.GetInterpolatedValue(percent));
		if (useShiny && ability.shinyEffect.effectType == AbilityShinyEffectType.ModifyAbilityValues)
		{
			num = Mathf.RoundToInt(useRangedIntValue2 ? ability.shinyEffect.rangedIntValue2.GetInterpolatedValue(percent) : ability.shinyEffect.rangedIntValue1.GetInterpolatedValue(percent));
		}
		int num2 = 0;
		if (useAttackStat)
		{
			num2 = character.AttackAbilityPositionModified(slotPosition);
			if (useAttackModifier)
			{
				num2 = Mathf.FloorToInt((float)num2 * character.attackModifier);
			}
		}
		int num3 = 0;
		if (useTempBonus)
		{
			num3 = character.GetTempAttackBonusForSlotPosition(slotPosition);
		}
		int num4 = Mathf.Max(minDamage, Mathf.RoundToInt((float)(num + num2 + num3) * character.finalDamageMultiplier));
		if (ability.channelingData.speed > 0f)
		{
			num4 = Mathf.RoundToInt((float)num4 * ability.channelingData.channelingSteps[0].valueModifier);
		}
		return num4;
	}

	public static int CalculateTossObjectDamage(TossObjectData tossObjectData, Character character, int slotPosition, bool useAttackStat = false, bool useAttackModifier = false, bool useTempBonus = false, bool useShiny = false)
	{
		int num = Mathf.RoundToInt(tossObjectData.damageRange.GetInterpolatedValue((float)slotPosition / 100f));
		int num2 = 0;
		if (useAttackStat)
		{
			num2 = character.AttackAbilityPositionModified(slotPosition);
			if (useAttackModifier)
			{
				num2 = Mathf.FloorToInt((float)num2 * character.attackModifier);
			}
		}
		int num3 = 0;
		if (useTempBonus)
		{
			num3 = character.GetTempAttackBonusForSlotPosition(slotPosition);
		}
		return Mathf.Max(1, Mathf.RoundToInt((float)(num + num2 + num3) * character.finalDamageMultiplier));
	}

	public static int CalculateHealAmount(AbilityData ability, Character character, int slotPosition, bool useAttackStat = false, bool useAttackModifier = false, bool useTempBonus = false, bool useShiny = false)
	{
		if (ability.type != AbilityType.Heal)
		{
			Debug.LogError("The supplied ability is not of the right type for calculating total heal amount");
			return 0;
		}
		int num = ((!useShiny || ability.shinyEffect.effectType != AbilityShinyEffectType.ModifyAbilityValues) ? Mathf.RoundToInt(ability.rangedIntValue1.GetInterpolatedValue((float)slotPosition / 100f)) : Mathf.RoundToInt(ability.shinyEffect.rangedIntValue1.GetInterpolatedValue((float)slotPosition / 100f)));
		int num2 = 0;
		if (useAttackStat)
		{
			num2 = character.AttackAbilityPositionModified(slotPosition);
			if (useAttackModifier)
			{
				num2 = Mathf.FloorToInt((float)num2 * character.attackModifier);
			}
		}
		int num3 = 0;
		if (useTempBonus)
		{
			character.GetTempAttackBonusForSlotPosition(slotPosition);
		}
		int num4 = Mathf.Max(1, Mathf.RoundToInt((float)(num + num2 + num3) * character.finalDamageMultiplier));
		if (ability.channelingData.speed > 0f)
		{
			num4 = Mathf.RoundToInt((float)num4 * ability.channelingData.channelingSteps[0].valueModifier);
		}
		return num4;
	}

	public static int CalculateTeamStatBonus(StatusEffectData statusEffectData, Character character, int slotPosition)
	{
		Debug.Log("Calculate Team Stat Bonus");
		int num = 0;
		CharacterStatType statType = statusEffectData.statType;
		if (statType == CharacterStatType.Attack)
		{
			if (statusEffectData.rangedIntValue1.GetInterpolatedValue(0.5f) > 0f)
			{
				return Mathf.RoundToInt(statusEffectData.rangedIntValue1.GetInterpolatedValue((float)slotPosition / 100f)) + Mathf.RoundToInt((float)character.baseAttack * GameManager.instance.gameSettings.actionMeterMultiplierRange.GetInterpolatedValue((float)slotPosition / 100f));
			}
			return Mathf.RoundToInt(statusEffectData.rangedIntValue1.GetInterpolatedValue((float)slotPosition / 100f)) - Mathf.RoundToInt((float)character.baseAttack * GameManager.instance.gameSettings.actionMeterMultiplierRange.GetInterpolatedValue((float)slotPosition / 100f) * 0.5f);
		}
		return Mathf.Abs(Mathf.RoundToInt(statusEffectData.rangedIntValue1.GetInterpolatedValue((float)slotPosition / 100f)));
	}
}
