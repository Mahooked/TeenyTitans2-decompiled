using System.Text.RegularExpressions;
using UnityEngine;

public static class TTGParseHelper
{
	public static string GetAbilityEffectText(AbilityData ability, Character character, int slotPosition = -1)
	{
		string text = ((!string.IsNullOrEmpty(ability.effectDescriptionKey)) ? Language.Get(ability.effectDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS").Trim() : ability.effectDescription);
		MatchCollection matchCollection = Regex.Matches(text, "{[0-9]}");
		int count = matchCollection.Count;
		if (count > 0)
		{
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				AbilitySlot abilitySlot = character.GetAbilitySlot(ability);
				switch (ability.type)
				{
				case AbilityType.Attack:
				{
					int num = TTGAbilityCalculator.CalculateDamage(ability, character, (slotPosition <= -1) ? abilitySlot.position : slotPosition, useAttackStat: true, useAttackModifier: true, useTempBonus: true);
					array[i] = num;
					if (ability.damageType == DamageType.LifeSteal && i == 1)
					{
						array[i] = Mathf.Max(ability.minLifeSteal, Mathf.FloorToInt((float)num * ability.lifeStealPercent));
					}
					break;
				}
				case AbilityType.Heal:
					array[i] = TTGAbilityCalculator.CalculateHealAmount(ability, character, (slotPosition <= -1) ? abilitySlot.position : slotPosition, useAttackStat: true, useAttackModifier: false, useTempBonus: true);
					break;
				case AbilityType.ActionMeter:
				{
					ActionMeterEffect actionMeterEffect = ability.actionMeterEffect;
					if (actionMeterEffect == ActionMeterEffect.Fill)
					{
						array[i] = Mathf.Abs(ability.GetHealthCost(character));
					}
					break;
				}
				case AbilityType.StatusEffect:
				{
					StatusEffectName statusEffect = ability.statusEffect;
					if (statusEffect != StatusEffectName.AbilityBlocker && statusEffect != StatusEffectName.ActionMeterDepleter && statusEffect == StatusEffectName.TeamStatBuff)
					{
						StatusEffectData statusEffectData = new StatusEffectData();
						statusEffectData.intValue1 = ability.intValue1;
						statusEffectData.intValue2 = (int)ability.statType;
						statusEffectData.floatValue1 = ability.floatValue1;
						statusEffectData.floatValue2 = ability.floatValue2;
						statusEffectData.stringValue1 = ability.stringValue1;
						statusEffectData.stringValue2 = ability.stringValue2;
						statusEffectData.rangedIntValue1 = ability.rangedIntValue1;
						statusEffectData.rangedIntValue2 = ability.rangedIntValue2;
						statusEffectData.rangedFloatValue1 = ability.rangedFloatValue1;
						statusEffectData.rangedFloatValue2 = ability.rangedFloatValue2;
						statusEffectData.rangedFloatValue3 = ability.rangedFloatValue3;
						statusEffectData.rangedFloatValue4 = ability.rangedFloatValue4;
						statusEffectData.boolValue1 = ability.boolValue1;
						StatusEffectData statusEffectData2 = statusEffectData;
						array[i] = TTGAbilityCalculator.CalculateTeamStatBonus(statusEffectData2, character, (slotPosition <= -1) ? abilitySlot.position : slotPosition);
					}
					break;
				}
				}
			}
			text = string.Format(text, array);
		}
		return text;
	}

	public static string GetShinyEffectDescription(AbilityData ability, Character character)
	{
		string text = (string.IsNullOrEmpty(ability.shinyEffectDescriptionKey) ? string.Empty : Language.Get(ability.shinyEffectDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS").Trim());
		AbilitySlot abilitySlot = character.GetAbilitySlot(ability);
		MatchCollection matchCollection = Regex.Matches(text, "{[0-9]}");
		int count = matchCollection.Count;
		if (count > 0)
		{
			AbilityShinyEffect shinyEffect = ability.shinyEffect;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				switch (shinyEffect.effectType)
				{
				case AbilityShinyEffectType.AddStatusEffect:
				{
					StatusEffectName statusEffect = shinyEffect.statusEffect;
					if (statusEffect == StatusEffectName.ActionMeterDepleter)
					{
						array[i] = Mathf.RoundToInt(shinyEffect.statusEffectData.rangedFloatValue1.GetInterpolatedValue((float)abilitySlot.position / 100f) * 100f) + "%";
					}
					break;
				}
				case AbilityShinyEffectType.ModifyAbilityValues:
					switch (ability.type)
					{
					case AbilityType.Heal:
					{
						int num7 = TTGAbilityCalculator.CalculateHealAmount(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: false, useTempBonus: true);
						int num8 = TTGAbilityCalculator.CalculateHealAmount(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: false, useTempBonus: true, useShiny: true);
						array[i] = Mathf.Abs(num8 - num7);
						break;
					}
					case AbilityType.Attack:
					{
						int num5 = TTGAbilityCalculator.CalculateDamage(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: true, useTempBonus: true);
						int num6 = TTGAbilityCalculator.CalculateDamage(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: true, useTempBonus: true, useShiny: true);
						array[i] = Mathf.Abs(num6 - num5);
						break;
					}
					case AbilityType.StatusEffect:
						switch (ability.statusEffect)
						{
						case StatusEffectName.Poison:
							if (ability.floatValue1 != shinyEffect.floatValue1)
							{
								array[i] = Mathf.Abs(shinyEffect.floatValue1 - ability.floatValue1);
							}
							else if (ability.intValue1 != shinyEffect.intValue1)
							{
								int num3 = TTGAbilityCalculator.CalculateDamage(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: true);
								int num4 = TTGAbilityCalculator.CalculateDamage(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: true, useTempBonus: false, useShiny: true);
								array[i] = Mathf.Abs(num4 - num3);
							}
							else if (ability.floatValue2 != shinyEffect.floatValue2)
							{
								array[i] = Mathf.Abs(shinyEffect.floatValue2 - ability.floatValue2);
							}
							break;
						case StatusEffectName.AttackDrone:
							if (ability.rangedIntValue1 != shinyEffect.rangedIntValue1)
							{
								int num = TTGAbilityCalculator.CalculateDamage(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: true);
								int num2 = TTGAbilityCalculator.CalculateDamage(ability, character, abilitySlot.position, useAttackStat: true, useAttackModifier: true, useTempBonus: false, useShiny: true);
								array[i] = Mathf.Abs(num2 - num);
							}
							break;
						}
						break;
					}
					break;
				}
			}
			text = string.Format(text, array);
		}
		return text;
	}

	public static string GetModChipDescription(ModChipDefinition modChip)
	{
		string text = Language.Get(modChip.itemDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS").Trim();
		int num = text.Split('{').Length - 1;
		if (num > 0)
		{
			string[] array = new string[num];
			int num2 = 0;
			int count = modChip.effects.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				ModChipEffect modChipEffect = modChip.effects.Find((ModChipEffect x) => x.effectOrderIntVal == i || x.effectOrderFloatVal == i);
				if (modChipEffect == null)
				{
					continue;
				}
				string text2 = string.Empty;
				if (modChipEffect.useProbabilityInDescription)
				{
					text2 = modChipEffect.probability * 100f + "%";
				}
				else
				{
					bool flag = modChipEffect.effectType == ModChipEffectType.CastTimeMultiplier || modChipEffect.effectType == ModChipEffectType.StatMultiplier || modChipEffect.effectType == ModChipEffectType.InstantCastChance || modChipEffect.effectType == ModChipEffectType.AccessoryChargeTimeMultiplier;
					if (modChipEffect.effectOrderIntVal == i)
					{
						int num3 = ((modChipEffect.effectType != ModChipEffectType.AccessoryChargeTimeMultiplier) ? modChipEffect.intValue : Mathf.Abs(modChipEffect.intValue));
						text2 = (flag ? (num3 * 100 + "%") : num3.ToString());
					}
					else if (modChipEffect.effectOrderFloatVal == i)
					{
						float num4 = ((modChipEffect.effectType != ModChipEffectType.AccessoryChargeTimeMultiplier) ? modChipEffect.floatValue : Mathf.Abs(modChipEffect.floatValue));
						text2 = (flag ? (num4 * 100f + "%") : num4.ToString());
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					array[num2] = text2;
					num2++;
				}
			}
			text = string.Format(text, array);
		}
		return text;
	}

	public static string GetAccessoryDescription(AccessoryItemDefinition accessory)
	{
		string text = Language.Get(accessory.itemDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS").Trim();
		int num = text.Split('{').Length - 1;
		if (num > 0)
		{
			string[] array = new string[num];
			int num2 = 0;
			int count = accessory.effects.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				AccessoryItemEffect accessoryItemEffect = accessory.effects.Find((AccessoryItemEffect x) => x.effectOrderIntVal == i || x.effectOrderFloatVal == i);
				if (accessoryItemEffect == null)
				{
					continue;
				}
				string text2 = string.Empty;
				bool flag = accessoryItemEffect.effectType == ModChipEffectType.CastTimeMultiplier || accessoryItemEffect.effectType == ModChipEffectType.StatMultiplier;
				if (accessoryItemEffect.effectOrderIntVal == i)
				{
					text2 = (flag ? (accessoryItemEffect.intValue * 100 + "%") : accessoryItemEffect.intValue.ToString());
				}
				else if (accessoryItemEffect.effectOrderFloatVal == i)
				{
					text2 = (flag ? (accessoryItemEffect.floatValue * 100f + "%") : accessoryItemEffect.floatValue.ToString());
				}
				if (!string.IsNullOrEmpty(text2))
				{
					array[num2] = text2;
					num2++;
				}
				if (accessoryItemEffect.factionOverrides.Count <= 0)
				{
					continue;
				}
				ModChipEffectFactionOverride modChipEffectFactionOverride = accessoryItemEffect.factionOverrides.Find((ModChipEffectFactionOverride x) => x.effectOrderIntVal == i + 1 || x.effectOrderFloatVal == i + 1);
				if (modChipEffectFactionOverride != null)
				{
					text2 = string.Empty;
					if (modChipEffectFactionOverride.effectOrderIntVal == i + 1)
					{
						text2 = (flag ? (modChipEffectFactionOverride.intValue * 100 + "%") : modChipEffectFactionOverride.intValue.ToString());
					}
					else if (modChipEffectFactionOverride.effectOrderFloatVal == i + 1)
					{
						text2 = (flag ? (modChipEffectFactionOverride.floatValue * 100f + "%") : modChipEffectFactionOverride.floatValue.ToString());
					}
					if (!string.IsNullOrEmpty(text2))
					{
						array[num2] = text2;
						num2++;
					}
				}
			}
			text = string.Format(text, array);
		}
		return text;
	}
}
