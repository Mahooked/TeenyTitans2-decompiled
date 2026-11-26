using System;

[Serializable]
public class AbilityShinyEffect
{
	public AbilityShinyEffectTriggerType triggerType;

	public AbilityShinyEffectType effectType;

	public float probability = 1f;

	public TargetType targetType;

	public StatusEffectName statusEffect;

	public StatusEffectData statusEffectData;

	public bool isDodgeable = true;

	public int intValue1;

	public int intValue2;

	public float floatValue1;

	public float floatValue2;

	public string stringValue1;

	public string stringValue2;

	public RangedInt rangedIntValue1;

	public RangedInt rangedIntValue2;

	public RangedFloat rangedFloatValue1;

	public RangedFloat rangedFloatValue2;

	public RangedFloat rangedFloatValue3;

	public RangedFloat rangedFloatValue4;

	public bool boolValue1;

	public BeatBoxType beatBoxType
	{
		get
		{
			return (BeatBoxType)intValue2;
		}
		set
		{
			intValue2 = (int)value;
		}
	}

	public CharacterStatType statType
	{
		get
		{
			return (CharacterStatType)intValue2;
		}
		set
		{
			intValue2 = (int)value;
		}
	}
}
