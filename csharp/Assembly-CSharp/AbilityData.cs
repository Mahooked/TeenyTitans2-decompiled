using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityData : ScriptableObject
{
	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	[SerializeField]
	private string _abilityName = string.Empty;

	[SerializeField]
	private string _abilityNameKey = string.Empty;

	[SerializeField]
	private string _abilityDescription = string.Empty;

	[SerializeField]
	private string _abilityDescriptionKey = string.Empty;

	[SerializeField]
	private string _effectDescription = string.Empty;

	[SerializeField]
	private string _effectDescriptionKey = string.Empty;

	[SerializeField]
	private string _iconSprite = string.Empty;

	[SerializeField]
	private int _iconSpriteId = -1;

	[SerializeField]
	private AbilityType _type;

	[SerializeField]
	private float _castTime;

	[SerializeField]
	private int _numChargesRequired;

	[SerializeField]
	private ChannelingData _channelingData = new ChannelingData();

	[SerializeField]
	private float _cooldownTime;

	[SerializeField]
	private bool _isDodgeable = true;

	[SerializeField]
	private string _animName;

	[SerializeField]
	private int _numUses = -1;

	[SerializeField]
	private int _healthCost;

	[SerializeField]
	private float _healthCostPercent;

	[SerializeField]
	private TargetCount _targetCount;

	[SerializeField]
	private TargetType _targetType;

	[SerializeField]
	private int _intValue1;

	[SerializeField]
	private int _intValue2;

	[SerializeField]
	private float _floatValue1;

	[SerializeField]
	private float _floatValue2;

	[SerializeField]
	private RangedInt _rangedIntValue1;

	[SerializeField]
	private RangedInt _rangedIntValue2;

	[SerializeField]
	private RangedFloat _rangedFloatValue1;

	[SerializeField]
	private RangedFloat _rangedFloatValue2;

	[SerializeField]
	private RangedFloat _rangedFloatValue3;

	[SerializeField]
	private RangedFloat _rangedFloatValue4;

	[SerializeField]
	private bool _boolValue1;

	[SerializeField]
	private Vector3 _vector3Value1;

	[SerializeField]
	private string _stringValue1 = string.Empty;

	[SerializeField]
	private string _stringValue2 = string.Empty;

	[SerializeField]
	private DamageType _damageType;

	[SerializeField]
	private CharacterStatType _statType;

	[SerializeField]
	private ActionMeterEffect _actionMeterEffect;

	[SerializeField]
	private StatusEffectName _statusEffect;

	[SerializeField]
	private StatusEffectName _secondaryStatusEffect;

	[SerializeField]
	private StatusEffectData _secondaryStatusEffectData;

	[SerializeField]
	private BeatBoxType _beatBoxType;

	[SerializeField]
	private List<TossObjectData> _tossObjects = new List<TossObjectData>();

	[SerializeField]
	private string _shinyEffectDescription = string.Empty;

	[SerializeField]
	private string _shinyEffectDescriptionKey = string.Empty;

	[SerializeField]
	private AbilityShinyEffect _shinyEffect;

	[SerializeField]
	private List<AbilityShinyEffect> _batteryEffects = new List<AbilityShinyEffect>();

	[SerializeField]
	private string _scriptName;

	[SerializeField]
	private string _castSoundPath = string.Empty;

	[SerializeField]
	private float _castSoundDelay;

	[SerializeField]
	private List<string> _castVOSoundPaths = new List<string>();

	[SerializeField]
	private List<float> _castVOSoundDelays = new List<float>();

	[SerializeField]
	private float _castVOSoundProbability = 1f;

	[SerializeField]
	private string _soundPath;

	[SerializeField]
	private float _soundDelay;

	[SerializeField]
	private List<string> _voSoundPaths = new List<string>();

	[SerializeField]
	private List<float> _voSoundDelays = new List<float>();

	[SerializeField]
	private float _voSoundProbability = 1f;

	[SerializeField]
	private string _hitSoundPath = string.Empty;

	[SerializeField]
	private float _hitSoundDelay;

	[SerializeField]
	private BeatBoxType _buffDropType;

	[SerializeField]
	private AbilityRangeType _rangeType;

	[SerializeField]
	private bool _pauseGameplayOnUse = true;

	[SerializeField]
	private bool _useDiminishingReturns = true;

	[SerializeField]
	private bool _overrideAbilityUseLimit;

	[SerializeField]
	private int _abilityUseLimitOverride;

	[NonSerialized]
	public float cooldownTimer;

	public int uid => _uid;

	public string abilityName => _abilityName;

	public string abilityNameKey => _abilityNameKey;

	public string abilityDescription => _abilityDescription;

	public string abilityDescriptionKey => _abilityDescriptionKey;

	public string effectDescription => _effectDescription;

	public string effectDescriptionKey => _effectDescriptionKey;

	public string iconSprite => _iconSprite;

	public int iconSpriteId => _iconSpriteId;

	public AbilityType type => _type;

	public float castTime => _castTime;

	public int numChargesRequired => _numChargesRequired;

	public ChannelingData channelingData => _channelingData;

	public float cooldownTime => _cooldownTime;

	public bool isDodgeable => _isDodgeable;

	public string animName => _animName;

	public int numUses => _numUses;

	public int healthCost => _healthCost;

	public float healthCostPercent => _healthCostPercent;

	public TargetCount targetCount => _targetCount;

	public TargetType targetType => _targetType;

	public int intValue1 => _intValue1;

	public int intValue2 => _intValue2;

	public float floatValue1 => _floatValue1;

	public float floatValue2 => _floatValue2;

	public RangedInt rangedIntValue1 => _rangedIntValue1;

	public RangedInt rangedIntValue2 => _rangedIntValue2;

	public RangedFloat rangedFloatValue1 => _rangedFloatValue1;

	public RangedFloat rangedFloatValue2 => _rangedFloatValue2;

	public RangedFloat rangedFloatValue3 => _rangedFloatValue3;

	public RangedFloat rangedFloatValue4 => _rangedFloatValue4;

	public bool boolValue1
	{
		get
		{
			return _boolValue1;
		}
		set
		{
			_boolValue1 = value;
		}
	}

	public Vector3 vector3Value1 => _vector3Value1;

	public string stringValue1 => _stringValue1;

	public string stringValue2 => _stringValue2;

	public DamageType damageType => _damageType;

	public CharacterStatType statType => _statType;

	public ActionMeterEffect actionMeterEffect => _actionMeterEffect;

	public StatusEffectName statusEffect => _statusEffect;

	public StatusEffectName secondaryStatusEffect => _secondaryStatusEffect;

	public StatusEffectData secondaryStatusEffectData => _secondaryStatusEffectData;

	public BeatBoxType beatBoxType => _beatBoxType;

	public List<TossObjectData> tossObjects => _tossObjects;

	public string shinyEffectDescription => _shinyEffectDescription;

	public string shinyEffectDescriptionKey => _shinyEffectDescriptionKey;

	public AbilityShinyEffect shinyEffect => _shinyEffect;

	public List<AbilityShinyEffect> batteryEffects => _batteryEffects;

	public string scriptName => _scriptName;

	public string castSoundPath => _castSoundPath;

	public float castSoundDelay => _castSoundDelay;

	public List<string> castVOSoundPaths => _castVOSoundPaths;

	public List<float> castVOSoundDelays => _castVOSoundDelays;

	public float castVOSoundProbability => _castVOSoundProbability;

	public string soundPath => _soundPath;

	public float soundDelay => _soundDelay;

	public List<string> voSoundPaths => _voSoundPaths;

	public List<float> voSoundDelays => _voSoundDelays;

	public float voSoundProbability => _voSoundProbability;

	public string hitSoundPath => _hitSoundPath;

	public float hitSoundDelay => _hitSoundDelay;

	public BeatBoxType buffDropType => _buffDropType;

	public AbilityRangeType rangeType => _rangeType;

	public bool pauseGameplayOnUse
	{
		get
		{
			return _pauseGameplayOnUse;
		}
		set
		{
			_pauseGameplayOnUse = value;
		}
	}

	public bool useDiminishingReturns
	{
		get
		{
			return _useDiminishingReturns;
		}
		set
		{
			_useDiminishingReturns = value;
		}
	}

	public bool overrideAbilityUseLimit => _overrideAbilityUseLimit;

	public int abilityUseLimitOverride => _abilityUseLimitOverride;

	public int damage => _intValue1;

	public int splashDamage => _intValue2;

	public float splashChance => _floatValue2;

	public int numHits => _intValue2;

	public int bonus => _intValue1;

	public int healAmount => _intValue1;

	public float modifier => _floatValue1;

	public int minLifeSteal => _intValue2;

	public float lifeStealPercent => _floatValue1;

	public int GetHealthCost(Character character)
	{
		if (_healthCost > 0 || _healthCostPercent > 0f)
		{
			return Mathf.Max(_healthCost, Mathf.FloorToInt((float)character.maxHitPoints * _healthCostPercent));
		}
		return 0;
	}
}
