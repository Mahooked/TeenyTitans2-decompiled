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

	public int uid
	{
		get
		{
			return _uid;
		}
	}

	public string abilityName
	{
		get
		{
			return _abilityName;
		}
	}

	public string abilityNameKey
	{
		get
		{
			return _abilityNameKey;
		}
	}

	public string abilityDescription
	{
		get
		{
			return _abilityDescription;
		}
	}

	public string abilityDescriptionKey
	{
		get
		{
			return _abilityDescriptionKey;
		}
	}

	public string effectDescription
	{
		get
		{
			return _effectDescription;
		}
	}

	public string effectDescriptionKey
	{
		get
		{
			return _effectDescriptionKey;
		}
	}

	public string iconSprite
	{
		get
		{
			return _iconSprite;
		}
	}

	public int iconSpriteId
	{
		get
		{
			return _iconSpriteId;
		}
	}

	public AbilityType type
	{
		get
		{
			return _type;
		}
	}

	public float castTime
	{
		get
		{
			return _castTime;
		}
	}

	public int numChargesRequired
	{
		get
		{
			return _numChargesRequired;
		}
	}

	public ChannelingData channelingData
	{
		get
		{
			return _channelingData;
		}
	}

	public float cooldownTime
	{
		get
		{
			return _cooldownTime;
		}
	}

	public bool isDodgeable
	{
		get
		{
			return _isDodgeable;
		}
	}

	public string animName
	{
		get
		{
			return _animName;
		}
	}

	public int numUses
	{
		get
		{
			return _numUses;
		}
	}

	public int healthCost
	{
		get
		{
			return _healthCost;
		}
	}

	public float healthCostPercent
	{
		get
		{
			return _healthCostPercent;
		}
	}

	public TargetCount targetCount
	{
		get
		{
			return _targetCount;
		}
	}

	public TargetType targetType
	{
		get
		{
			return _targetType;
		}
	}

	public int intValue1
	{
		get
		{
			return _intValue1;
		}
	}

	public int intValue2
	{
		get
		{
			return _intValue2;
		}
	}

	public float floatValue1
	{
		get
		{
			return _floatValue1;
		}
	}

	public float floatValue2
	{
		get
		{
			return _floatValue2;
		}
	}

	public RangedInt rangedIntValue1
	{
		get
		{
			return _rangedIntValue1;
		}
	}

	public RangedInt rangedIntValue2
	{
		get
		{
			return _rangedIntValue2;
		}
	}

	public RangedFloat rangedFloatValue1
	{
		get
		{
			return _rangedFloatValue1;
		}
	}

	public RangedFloat rangedFloatValue2
	{
		get
		{
			return _rangedFloatValue2;
		}
	}

	public RangedFloat rangedFloatValue3
	{
		get
		{
			return _rangedFloatValue3;
		}
	}

	public RangedFloat rangedFloatValue4
	{
		get
		{
			return _rangedFloatValue4;
		}
	}

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

	public Vector3 vector3Value1
	{
		get
		{
			return _vector3Value1;
		}
	}

	public string stringValue1
	{
		get
		{
			return _stringValue1;
		}
	}

	public string stringValue2
	{
		get
		{
			return _stringValue2;
		}
	}

	public DamageType damageType
	{
		get
		{
			return _damageType;
		}
	}

	public CharacterStatType statType
	{
		get
		{
			return _statType;
		}
	}

	public ActionMeterEffect actionMeterEffect
	{
		get
		{
			return _actionMeterEffect;
		}
	}

	public StatusEffectName statusEffect
	{
		get
		{
			return _statusEffect;
		}
	}

	public StatusEffectName secondaryStatusEffect
	{
		get
		{
			return _secondaryStatusEffect;
		}
	}

	public StatusEffectData secondaryStatusEffectData
	{
		get
		{
			return _secondaryStatusEffectData;
		}
	}

	public BeatBoxType beatBoxType
	{
		get
		{
			return _beatBoxType;
		}
	}

	public List<TossObjectData> tossObjects
	{
		get
		{
			return _tossObjects;
		}
	}

	public string shinyEffectDescription
	{
		get
		{
			return _shinyEffectDescription;
		}
	}

	public string shinyEffectDescriptionKey
	{
		get
		{
			return _shinyEffectDescriptionKey;
		}
	}

	public AbilityShinyEffect shinyEffect
	{
		get
		{
			return _shinyEffect;
		}
	}

	public List<AbilityShinyEffect> batteryEffects
	{
		get
		{
			return _batteryEffects;
		}
	}

	public string scriptName
	{
		get
		{
			return _scriptName;
		}
	}

	public string castSoundPath
	{
		get
		{
			return _castSoundPath;
		}
	}

	public float castSoundDelay
	{
		get
		{
			return _castSoundDelay;
		}
	}

	public List<string> castVOSoundPaths
	{
		get
		{
			return _castVOSoundPaths;
		}
	}

	public List<float> castVOSoundDelays
	{
		get
		{
			return _castVOSoundDelays;
		}
	}

	public float castVOSoundProbability
	{
		get
		{
			return _castVOSoundProbability;
		}
	}

	public string soundPath
	{
		get
		{
			return _soundPath;
		}
	}

	public float soundDelay
	{
		get
		{
			return _soundDelay;
		}
	}

	public List<string> voSoundPaths
	{
		get
		{
			return _voSoundPaths;
		}
	}

	public List<float> voSoundDelays
	{
		get
		{
			return _voSoundDelays;
		}
	}

	public float voSoundProbability
	{
		get
		{
			return _voSoundProbability;
		}
	}

	public string hitSoundPath
	{
		get
		{
			return _hitSoundPath;
		}
	}

	public float hitSoundDelay
	{
		get
		{
			return _hitSoundDelay;
		}
	}

	public BeatBoxType buffDropType
	{
		get
		{
			return _buffDropType;
		}
	}

	public AbilityRangeType rangeType
	{
		get
		{
			return _rangeType;
		}
	}

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

	public bool overrideAbilityUseLimit
	{
		get
		{
			return _overrideAbilityUseLimit;
		}
	}

	public int abilityUseLimitOverride
	{
		get
		{
			return _abilityUseLimitOverride;
		}
	}

	public int damage
	{
		get
		{
			return _intValue1;
		}
	}

	public int splashDamage
	{
		get
		{
			return _intValue2;
		}
	}

	public float splashChance
	{
		get
		{
			return _floatValue2;
		}
	}

	public int numHits
	{
		get
		{
			return _intValue2;
		}
	}

	public int bonus
	{
		get
		{
			return _intValue1;
		}
	}

	public int healAmount
	{
		get
		{
			return _intValue1;
		}
	}

	public float modifier
	{
		get
		{
			return _floatValue1;
		}
	}

	public int minLifeSteal
	{
		get
		{
			return _intValue2;
		}
	}

	public float lifeStealPercent
	{
		get
		{
			return _floatValue1;
		}
	}

	public int GetHealthCost(Character character)
	{
		if (_healthCost > 0 || _healthCostPercent > 0f)
		{
			return Mathf.Max(_healthCost, Mathf.FloorToInt((float)character.maxHitPoints * _healthCostPercent));
		}
		return 0;
	}
}
