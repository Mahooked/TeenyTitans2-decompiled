using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModChipEffect
{
	public ModChipEffectTriggerType triggerType;

	public ModChipEffectType effectType;

	public float probability = 1f;

	public bool useProbabilityInDescription;

	public CharacterStatType statType;

	public ActionMeterEffect actionMeterEffect;

	public StatusEffectName statusEffect;

	public TargetType targetType;

	public int intValue;

	public int intValue2;

	public int intValue3;

	public int intValue4;

	public float floatValue;

	public bool boolValue;

	public bool boolValue2;

	public bool boolValue3;

	public bool boolValue4;

	public Color colorValue;

	public StatusEffectData statusEffectData;

	public int effectOrderIntVal = -1;

	public int effectOrderFloatVal = -1;

	public List<ModChipEffectFactionOverride> factionOverrides;
}
