using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
	private List<int> processedRewards = new List<int>();

	private int _level;

	private int _xp;

	private int _repaintColorID;

	private CharacterDefinition _characterDefinition;

	private LevelTable _levelTable;

	private List<int> _abilityUIDs;

	private List<AbilityData> _abilities;

	private List<int> _selectedRewards;

	private List<int> _repaintColorsUnlocked;

	private ModChipDefinition _modChip;

	private CharacterModChipSlot _modChipSlot;

	private float _baseHitPointsMultiplier = 1f;

	[NonSerialized]
	public int hitPointsTempIncrease;

	private int _hitPointsBonus;

	private int _hitPoints;

	private bool _isKnockedOut;

	private List<StatusEffect> _statusEffects = new List<StatusEffect>();

	[NonSerialized]
	public int attackTempIncrease;

	private float _baseAttackMultiplier = 1f;

	private float _attackModifier = 1f;

	private int _attackBonus;

	private int _tempAttackBonus;

	private float _finalDamageMultiplier = 1f;

	private float _baseSpeedMultiplier = 1f;

	private int _speedBonus;

	private int _tempSpeedBonus;

	private float _baseLuckMultiplier = 1f;

	private float _luckModifier = 1f;

	private int _luckBonus;

	private int _tempLuckBonus;

	private float _baseDodgeMultiplier = 1f;

	private float _dodgeModifier = 1f;

	private int _dodgeBonus;

	private int _tempDodgeBonus;

	private bool _isFavorite;

	private bool _isViewed;

	private int _lastRewardLevel = 1;

	private int _hitPointsTier;

	private int _attackTier;

	private int _speedTier;

	private int _luckTier;

	private int _dodgeTier;

	private bool _modChipUnlocked;

	private bool _abilityCustomizationUnlocked;

	private float _speedModifier = 1f;

	private float _castTimeModifier = 1f;

	private float _channelTimeModifier = 1f;

	private float _accessoryChargeTimeModifier = 1f;

	private float _stunResistanceChance;

	private float _instantCastChance;

	private int _shinyPoints;

	private int _shinyRank;

	private CharacterTransformation _activeTransformation;

	public int level
	{
		get
		{
			return _level;
		}
		set
		{
			_level = value;
		}
	}

	public int xp
	{
		get
		{
			return _xp;
		}
		set
		{
			_xp = value;
		}
	}

	public int repaintColorID
	{
		get
		{
			return _repaintColorID;
		}
		private set
		{
			if (_repaintColorID != value)
			{
				_repaintColorID = value;
				OnUpdated();
			}
		}
	}

	public CharacterDefinition characterDefinition => _characterDefinition;

	public LevelTable levelTable => _levelTable;

	public LevelData currentLevelData => _levelTable.GetDataForLevel(_level);

	public float currentLevelProgress
	{
		get
		{
			LevelData levelData = currentLevelData;
			return Mathf.Clamp01((float)(_xp - levelData.xpStart) / (float)(levelData.xpToNextLevel - levelData.xpStart));
		}
	}

	public bool isMaxLevel => _level >= _levelTable.maxLevel;

	public List<int> abilityUIDs => _abilityUIDs;

	public List<AbilityData> abilities => _abilities;

	public List<int> selectedRewards => _selectedRewards;

	public List<int> repaintColorsUnlocked => _repaintColorsUnlocked;

	public ModChipDefinition modChip => _modChip;

	public CharacterModChipSlot modChipSlot => _modChipSlot;

	public int uid { get; private set; }

	public int baseHitPoints { get; private set; }

	public int baseAttack { get; private set; }

	public int baseSpeed { get; private set; }

	public int baseLuck { get; private set; }

	public int baseDodge { get; private set; }

	public int maxHitPoints => Mathf.CeilToInt((float)baseHitPoints * Mathf.Max(0f, _baseHitPointsMultiplier)) + _hitPointsBonus + hitPointsTempIncrease + hitPointsTeamBoost;

	public float baseHitPointsMultiplier
	{
		get
		{
			return _baseHitPointsMultiplier;
		}
		set
		{
			if (value != _baseHitPointsMultiplier)
			{
				_baseHitPointsMultiplier = value;
				OnUpdated();
			}
		}
	}

	public int hitPointsTeamBoost
	{
		get
		{
			if (BattleController.battleDefinition != null)
			{
				if (BattleController.battleDefinition.playerCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.playerStatTeamBoosted == CharacterStatType.HitPoints)
				{
					return BattleController.battleDefinition.playerStatTeamBoostAmount;
				}
				if (BattleController.battleDefinition.enemyCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.enemyStatTeamBoosted == CharacterStatType.HitPoints)
				{
					return BattleController.battleDefinition.enemyStatTeamBoostAmount;
				}
			}
			return 0;
		}
	}

	public int hitPointsBonus
	{
		get
		{
			return _hitPointsBonus;
		}
		set
		{
			if (value != _hitPointsBonus)
			{
				_hitPointsBonus = value;
				OnUpdated();
			}
		}
	}

	public int hitPoints
	{
		get
		{
			return _hitPoints;
		}
		set
		{
			if (value == _hitPoints)
			{
				return;
			}
			int num = _hitPoints;
			if (GameManager.instance.gameSettings.healthCapEnabled)
			{
				_hitPoints = Mathf.Clamp(value, 0, maxHitPoints);
			}
			else
			{
				_hitPoints = Mathf.Max(value, 0);
			}
			OnUpdated();
			if (_hitPoints < num)
			{
				OnHurt();
				if (_hitPoints <= 0)
				{
					RemoveAllStatusEffects();
					OnDeath();
				}
			}
			else if (_hitPoints > num)
			{
				OnHealed();
			}
		}
	}

	public float hitPointsRatio => (float)_hitPoints / (float)maxHitPoints;

	public bool isDead => _hitPoints <= 0;

	public bool isAlive => _hitPoints > 0;

	public bool isKnockedOut
	{
		get
		{
			return _isKnockedOut;
		}
		set
		{
			if (value != _isKnockedOut)
			{
				_isKnockedOut = value;
				OnUpdated();
			}
		}
	}

	public List<StatusEffect> statusEffects => _statusEffects;

	public int attack => Mathf.CeilToInt((float)baseAttack * Mathf.Max(0f, _baseAttackMultiplier)) + _attackBonus + attackTempIncrease;

	public int attackTeamBoost
	{
		get
		{
			if (BattleController.battleDefinition != null)
			{
				if (BattleController.battleDefinition.playerCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.playerStatTeamBoosted == CharacterStatType.Attack)
				{
					return BattleController.battleDefinition.playerStatTeamBoostAmount;
				}
				if (BattleController.battleDefinition.enemyCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.enemyStatTeamBoosted == CharacterStatType.Attack)
				{
					return BattleController.battleDefinition.enemyStatTeamBoostAmount;
				}
			}
			return 0;
		}
	}

	public float baseAttackMultiplier
	{
		get
		{
			return _baseAttackMultiplier;
		}
		set
		{
			if (value != _baseAttackMultiplier)
			{
				_baseAttackMultiplier = value;
				OnUpdated();
			}
		}
	}

	public float attackModifier
	{
		get
		{
			return _attackModifier;
		}
		set
		{
			if (value != _attackModifier)
			{
				_attackModifier = value;
				OnUpdated();
			}
		}
	}

	public int attackBonus
	{
		get
		{
			return _attackBonus;
		}
		set
		{
			if (value != _attackBonus)
			{
				_attackBonus = value;
				OnUpdated();
			}
		}
	}

	public int tempAttackBonus
	{
		get
		{
			return _tempAttackBonus;
		}
		set
		{
			if (value != _tempAttackBonus)
			{
				_tempAttackBonus = value;
				OnUpdated();
			}
		}
	}

	public float finalDamageMultiplier
	{
		get
		{
			return _finalDamageMultiplier;
		}
		set
		{
			if (value != _finalDamageMultiplier)
			{
				_finalDamageMultiplier = value;
				OnUpdated();
			}
		}
	}

	public int speed => Mathf.CeilToInt((float)baseSpeed * Mathf.Max(0f, _baseSpeedMultiplier)) + _speedBonus + speedTeamBoost;

	public float baseSpeedMultiplier
	{
		get
		{
			return _baseSpeedMultiplier;
		}
		set
		{
			if (value != _baseSpeedMultiplier)
			{
				_baseSpeedMultiplier = value;
				OnUpdated();
			}
		}
	}

	public int speedBonus
	{
		get
		{
			return _speedBonus;
		}
		set
		{
			if (value != _speedBonus)
			{
				_speedBonus = value;
				OnUpdated();
			}
		}
	}

	public int tempSpeedBonus
	{
		get
		{
			return _tempSpeedBonus;
		}
		set
		{
			if (value != _tempSpeedBonus)
			{
				_tempSpeedBonus = value;
				OnUpdated();
			}
		}
	}

	public int speedTeamBoost
	{
		get
		{
			if (BattleController.battleDefinition != null)
			{
				if (BattleController.battleDefinition.playerCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.playerStatTeamBoosted == CharacterStatType.Speed)
				{
					return BattleController.battleDefinition.playerStatTeamBoostAmount;
				}
				if (BattleController.battleDefinition.enemyCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.enemyStatTeamBoosted == CharacterStatType.Speed)
				{
					return BattleController.battleDefinition.enemyStatTeamBoostAmount;
				}
			}
			return 0;
		}
	}

	public int luck => Mathf.CeilToInt((float)baseLuck * Mathf.Max(0f, _baseLuckMultiplier)) + _luckBonus + luckTeamBoost;

	public float baseLuckMultiplier
	{
		get
		{
			return _baseLuckMultiplier;
		}
		set
		{
			if (value != _baseLuckMultiplier)
			{
				_baseLuckMultiplier = value;
				OnUpdated();
			}
		}
	}

	public float luckModifier
	{
		get
		{
			return _luckModifier;
		}
		set
		{
			if (value != _luckModifier)
			{
				_luckModifier = value;
				OnUpdated();
			}
		}
	}

	public int luckBonus
	{
		get
		{
			return _luckBonus;
		}
		set
		{
			if (value != _luckBonus)
			{
				_luckBonus = value;
				OnUpdated();
			}
		}
	}

	public int tempLuckBonus
	{
		get
		{
			return _tempLuckBonus;
		}
		set
		{
			if (value != _tempLuckBonus)
			{
				_tempLuckBonus = value;
				OnUpdated();
			}
		}
	}

	public int luckTeamBoost
	{
		get
		{
			if (BattleController.battleDefinition != null)
			{
				if (BattleController.battleDefinition.playerCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.playerStatTeamBoosted == CharacterStatType.Luck)
				{
					return BattleController.battleDefinition.playerStatTeamBoostAmount;
				}
				if (BattleController.battleDefinition.enemyCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.enemyStatTeamBoosted == CharacterStatType.Luck)
				{
					return BattleController.battleDefinition.enemyStatTeamBoostAmount;
				}
			}
			return 0;
		}
	}

	public int dodge => Mathf.CeilToInt((float)baseDodge * Mathf.Max(0f, _baseDodgeMultiplier)) + _dodgeBonus + dodgeTeamBoost;

	public float baseDodgeMultiplier
	{
		get
		{
			return _baseDodgeMultiplier;
		}
		set
		{
			if (value != _baseDodgeMultiplier)
			{
				_baseDodgeMultiplier = value;
				OnUpdated();
			}
		}
	}

	public float dodgeModifier
	{
		get
		{
			return _dodgeModifier;
		}
		set
		{
			if (value != _dodgeModifier)
			{
				_dodgeModifier = value;
				OnUpdated();
			}
		}
	}

	public int dodgeBonus
	{
		get
		{
			return _dodgeBonus;
		}
		set
		{
			if (value != _dodgeBonus)
			{
				_dodgeBonus = value;
				OnUpdated();
			}
		}
	}

	public int tempDodgeBonus
	{
		get
		{
			return _tempDodgeBonus;
		}
		set
		{
			if (value != _tempDodgeBonus)
			{
				_tempDodgeBonus = value;
				OnUpdated();
			}
		}
	}

	public int dodgeTeamBoost
	{
		get
		{
			if (BattleController.battleDefinition != null)
			{
				if (BattleController.battleDefinition.playerCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.playerStatTeamBoosted == CharacterStatType.Dodge)
				{
					return BattleController.battleDefinition.playerStatTeamBoostAmount;
				}
				if (BattleController.battleDefinition.enemyCharactersTeamBoosted.Contains(this) && BattleController.battleDefinition.enemyStatTeamBoosted == CharacterStatType.Dodge)
				{
					return BattleController.battleDefinition.enemyStatTeamBoostAmount;
				}
			}
			return 0;
		}
	}

	public bool isFavorite
	{
		get
		{
			return _isFavorite;
		}
		set
		{
			if (value != _isFavorite)
			{
				_isFavorite = value;
				OnUpdated();
			}
		}
	}

	public bool isViewed
	{
		get
		{
			return _isViewed;
		}
		set
		{
			if (value != _isViewed)
			{
				_isViewed = value;
				OnUpdated();
			}
		}
	}

	public int lastRewardLevel => _lastRewardLevel;

	public int hitPointsTier => _hitPointsTier;

	public int attackTier => _attackTier;

	public int speedTier => _speedTier;

	public int luckTier => _luckTier;

	public int dodgeTier => _dodgeTier;

	public bool modChipUnlocked => _modChipUnlocked;

	public bool abilityCustomizationUnlocked => _abilityCustomizationUnlocked;

	public float speedModifier
	{
		get
		{
			return _speedModifier;
		}
		set
		{
			if (value != _speedModifier)
			{
				_speedModifier = value;
				OnUpdated();
			}
		}
	}

	public float castTimeModifier
	{
		get
		{
			return _castTimeModifier;
		}
		set
		{
			if (value != _castTimeModifier)
			{
				_castTimeModifier = value;
			}
		}
	}

	public float channelTimeModifier
	{
		get
		{
			return _channelTimeModifier;
		}
		set
		{
			if (value != _channelTimeModifier)
			{
				_channelTimeModifier = value;
			}
		}
	}

	public float accessoryChargeTimeModifier
	{
		get
		{
			return _accessoryChargeTimeModifier;
		}
		set
		{
			_accessoryChargeTimeModifier = value;
		}
	}

	public float stunResistanceChance
	{
		get
		{
			return _stunResistanceChance;
		}
		set
		{
			_stunResistanceChance = value;
		}
	}

	public float instantCastChance
	{
		get
		{
			return _instantCastChance;
		}
		set
		{
			_instantCastChance = value;
		}
	}

	public int shinyPoints
	{
		get
		{
			return _shinyPoints;
		}
		set
		{
			_shinyPoints = value;
			OnUpdated();
		}
	}

	public int shinyRank
	{
		get
		{
			return _shinyRank;
		}
		set
		{
			_shinyRank = value;
			OnUpdated();
		}
	}

	public string battleCharacterPath => (_activeTransformation != null) ? _activeTransformation.battleCharacterPath : _characterDefinition.battleCharacterPath;

	public CharacterClass characterClass => _characterDefinition.characterClass;

	public string portraitSprite => (_activeTransformation != null) ? _activeTransformation.portraitSprite : _characterDefinition.portraitSprite;

	public ActionMeterSize actionMeterSize => (_activeTransformation != null) ? _activeTransformation.actionMeterSize : _characterDefinition.actionMeterSize;

	public List<AbilitySlot> abilitySlots => (_activeTransformation != null) ? _activeTransformation.abilitySlots : _characterDefinition.abilitySlots;

	public CharacterTransformation activeTransformation => _activeTransformation;

	public Vector3 playerCameraOffsetPosition => (_activeTransformation != null) ? _activeTransformation.battleOffsets.playerCameraOffsetPosition : _characterDefinition.playerCameraOffsetPosition;

	public Vector3 playerCameraOffsetRotation => (_activeTransformation != null) ? _activeTransformation.battleOffsets.playerCameraOffsetRotation : _characterDefinition.playerCameraOffsetRotation;

	public Vector3 playerCameraOffsetPositionZoomedOut => (_activeTransformation != null) ? _activeTransformation.battleOffsets.playerCameraOffsetPositionZoomedOut : _characterDefinition.playerCameraOffsetPositionZoomedOut;

	public Vector3 playerCameraOffsetRotationZoomedOut => (_activeTransformation != null) ? _activeTransformation.battleOffsets.playerCameraOffsetRotationZoomedOut : _characterDefinition.playerCameraOffsetRotationZoomedOut;

	public Vector3 playerFlightCameraOffsetPosition => _characterDefinition.playerFlightCameraOffsetPosition;

	public Vector3 playerFlightCameraOffsetRotation => _characterDefinition.playerFlightCameraOffsetRotation;

	public Vector3 playerFlightCameraOffsetPositionZoomedOut => _characterDefinition.playerFlightCameraOffsetPositionZoomedOut;

	public Vector3 playerFlightCameraOffsetRotationZoomedOut => _characterDefinition.playerFlightCameraOffsetRotationZoomedOut;

	public Vector3 enemyCameraOffsetPosition => (_activeTransformation != null) ? _activeTransformation.battleOffsets.enemyCameraOffsetPosition : _characterDefinition.enemyCameraOffsetPosition;

	public Vector3 enemyCameraOffsetRotation => (_activeTransformation != null) ? _activeTransformation.battleOffsets.enemyCameraOffsetRotation : _characterDefinition.enemyCameraOffsetRotation;

	public Vector3 enemyCameraOffsetPositionZoomedOut => (_activeTransformation != null) ? _activeTransformation.battleOffsets.enemyCameraOffsetPositionZoomedOut : _characterDefinition.enemyCameraOffsetPositionZoomedOut;

	public Vector3 enemyCameraOffsetRotationZoomedOut => (_activeTransformation != null) ? _activeTransformation.battleOffsets.enemyCameraOffsetRotationZoomedOut : _characterDefinition.enemyCameraOffsetRotationZoomedOut;

	public Vector3 enemyFlightCameraOffsetPosition => _characterDefinition.enemyFlightCameraOffsetPosition;

	public Vector3 enemyFlightCameraOffsetRotation => _characterDefinition.enemyFlightCameraOffsetRotation;

	public Vector3 enemyFlightCameraOffsetPositionZoomedOut => _characterDefinition.enemyFlightCameraOffsetPositionZoomedOut;

	public Vector3 enemyFlightCameraOffsetRotationZoomedOut => _characterDefinition.enemyFlightCameraOffsetRotationZoomedOut;

	public Vector3 tofuBubbleOffsetPosition => (_activeTransformation != null) ? _activeTransformation.battleOffsets.tofuBubbleOffsetPosition : _characterDefinition.tofuBubbleOffsetPosition;

	public event Action Updated;

	public event Action<Character> UpdatedCharacter;

	public event Action Death;

	public event Action<Character> DeathCharacter;

	public event Action DeathComplete;

	public event Action<DamageResult> IncomingDamage;

	public event Action<DamageResult> DamageReceived;

	public event Action<DamageResult> OutgoingDamage;

	public event Action<DamageResult> DamageDealt;

	public event Action Hurt;

	public event Action<HealResult> IncomingHeal;

	public event Action<HealResult> HealReceived;

	public event Action<HealResult> OutgoingHeal;

	public event Action<HealResult> HealDealt;

	public event Action Healed;

	public event Action<BuffResult> IncomingBuff;

	public event Action<BuffResult> BuffReceived;

	public event Action<DamageResult> Dodged;

	public event Action<InterruptResult> IncomingInterrupt;

	public event Action<InterruptResult> InterruptReceived;

	public event Action<StatusEffect> StatusEffectAdded;

	public event Action<StatusEffect> StatusEffectRemoved;

	public event Action Transformed;

	public event Action GainedXP;

	public event Action Leveled;

	public event Action<ModChipDefinition> ModChipEquipped;

	public Character(int uid)
	{
		this.uid = uid;
		_modChipSlot = new CharacterModChipSlot(this);
	}

	public Character(CharacterData data)
	{
		_modChipSlot = new CharacterModChipSlot(this);
		if (data != null)
		{
			Init(data);
		}
	}

	public Character(int uid, CharacterDefinition definition)
	{
		this.uid = uid;
		_modChipSlot = new CharacterModChipSlot(this);
		_characterDefinition = definition;
		_levelTable = _characterDefinition.levelTable;
		_level = 1;
		_xp = _levelTable.GetDataForLevel(_level).xpStart;
		_shinyPoints = 0;
		_shinyRank = 0;
		_isFavorite = false;
		_isViewed = false;
		_lastRewardLevel = 1;
		_repaintColorID = 0;
		hitPointsTempIncrease = 0;
		attackTempIncrease = 0;
		_abilities = new List<AbilityData>();
		for (int i = 0; i < 2; i++)
		{
			if (definition.abilitySlots.Count > 0)
			{
				_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(definition.abilitySlots[i].abilityId));
			}
		}
		_abilityUIDs = new List<int>();
		_selectedRewards = new List<int>();
		_repaintColorsUnlocked = new List<int>();
		baseHitPoints = definition.baseHitPoints;
		baseAttack = definition.baseAttack;
		baseSpeed = definition.baseSpeed;
		baseLuck = definition.baseLuck;
		baseDodge = definition.baseDodge;
		_hitPoints = maxHitPoints;
		_modChip = null;
	}

	public Character(int uid, TT2NetFigureData figureData)
	{
		this.uid = uid;
		_modChipSlot = new CharacterModChipSlot(this);
		_characterDefinition = GameManager.instance.characterDatabase.GetCharacter(figureData.uid);
		_levelTable = _characterDefinition.levelTable;
		_level = figureData.level;
		_shinyRank = figureData.shinyRank;
		_repaintColorID = figureData.repaintUid;
		_abilities = new List<AbilityData>();
		for (int i = 0; i < 2; i++)
		{
			if (i < _characterDefinition.abilitySlots.Count)
			{
				_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots[i].abilityId));
			}
		}
		baseHitPoints = _characterDefinition.baseHitPoints;
		baseAttack = _characterDefinition.baseAttack;
		baseSpeed = _characterDefinition.baseSpeed;
		baseLuck = _characterDefinition.baseLuck;
		baseDodge = _characterDefinition.baseDodge;
		processedRewards.Clear();
		int count = figureData.selectedRewards.Count;
		for (int j = 0; j < count; j++)
		{
			LevelUpReward rewardById = _levelTable.GetRewardById(figureData.selectedRewards[j]);
			if (rewardById.type == LevelUpRewardType.AbilityUnlock)
			{
				processedRewards.Add(rewardById.uid);
				AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots.GetLastItem().abilityId);
				if (!_abilities.Contains(ability))
				{
					_abilities.Add(ability);
				}
			}
			else
			{
				ProcessReward(rewardById);
			}
		}
		_abilityUIDs = new List<int>(figureData.abilityUIDs);
		List<AbilityData> list = new List<AbilityData>();
		bool flag = true;
		if (_abilityUIDs.Count > 0)
		{
			for (int k = 0; k < figureData.abilityUIDs.Count; k++)
			{
				int num = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_abilityUIDs[k]));
				if (num > -1)
				{
					list.Add(_abilities[num]);
				}
			}
			flag = list.Count != _abilityUIDs.Count;
		}
		if (flag)
		{
			for (int l = 0; l < _characterDefinition.orderedSlots.Count; l++)
			{
				int num2 = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.orderedSlots[l].abilityId));
				if (num2 > -1)
				{
					list.Add(_abilities[num2]);
				}
			}
		}
		_abilities = list;
		if (_modChipUnlocked)
		{
			_modChip = ((figureData.modChipUid <= -1) ? null : ((ModChipDefinition)GameManager.instance.itemDatabase.GetItem(figureData.modChipUid)));
			_modChipSlot.Insert(_modChip);
		}
		_hitPoints = maxHitPoints;
	}

	public Character(int uid, TournamentRoundFigureData figureData)
	{
		this.uid = uid;
		_modChipSlot = new CharacterModChipSlot(this);
		_characterDefinition = GameManager.instance.characterDatabase.GetCharacter(figureData.characterUID);
		_levelTable = _characterDefinition.levelTable;
		_level = figureData.level;
		_xp = _levelTable.GetDataForLevel(_level).xpStart;
		_shinyRank = figureData.shinyRank;
		_repaintColorID = figureData.repaintUID;
		_abilities = new List<AbilityData>();
		for (int i = 0; i < 2; i++)
		{
			_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots[i].abilityId));
		}
		int count = figureData.selectedRewards.Count;
		LevelUpReward levelUpReward = null;
		for (int j = 0; j < count; j++)
		{
			levelUpReward = _levelTable.GetRewardById(figureData.selectedRewards[j]);
			if (levelUpReward.type == LevelUpRewardType.AbilityUnlock)
			{
				_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots.GetLastItem().abilityId));
			}
		}
		_abilityUIDs = new List<int>(figureData.abilityUIDs);
		List<AbilityData> list = new List<AbilityData>();
		bool flag = true;
		if (_abilityUIDs.Count > 0)
		{
			for (int k = 0; k < _abilityUIDs.Count; k++)
			{
				int num = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_abilityUIDs[k]));
				if (num > -1)
				{
					list.Add(_abilities[num]);
				}
			}
			flag = list.Count != _abilityUIDs.Count;
		}
		if (flag)
		{
			for (int l = 0; l < _characterDefinition.orderedSlots.Count; l++)
			{
				int num2 = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.orderedSlots[l].abilityId));
				if (num2 > -1)
				{
					list.Add(_abilities[num2]);
				}
			}
		}
		_abilities = list;
		baseHitPoints = figureData.hitPoints;
		baseAttack = figureData.attack;
		baseSpeed = figureData.speed;
		baseLuck = figureData.luck;
		baseDodge = figureData.dodge;
		_hitPoints = maxHitPoints;
		hitPointsTempIncrease = 0;
		attackTempIncrease = 0;
		_modChip = ((figureData.modChipUID <= -1) ? null : ((ModChipDefinition)GameManager.instance.itemDatabase.GetItem(figureData.modChipUID)));
		_modChipSlot.Insert(_modChip);
	}

	public void Init(CharacterData data)
	{
		uid = data.uid;
		_level = data.level;
		_repaintColorID = data.repaintColorID;
		_repaintColorsUnlocked = new List<int>(data.repaintColorsUnlocked);
		_xp = data.xp;
		_shinyPoints = data.shinyPoints;
		_shinyRank = data.shinyRank;
		_isFavorite = data.isFavorite;
		_isViewed = data.isViewed;
		_lastRewardLevel = data.lastRewardLevel;
		_repaintColorID = data.repaintColorID;
		_characterDefinition = GameManager.instance.characterDatabase.GetCharacter(data.characterUID);
		_levelTable = _characterDefinition.levelTable;
		_selectedRewards = new List<int>(data.selectedRewards);
		_repaintColorsUnlocked = new List<int>(data.repaintColorsUnlocked);
		_abilities = new List<AbilityData>();
		for (int i = 0; i < 2; i++)
		{
			if (i < _characterDefinition.abilitySlots.Count)
			{
				_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots[i].abilityId));
			}
		}
		baseHitPoints = _characterDefinition.baseHitPoints;
		baseAttack = _characterDefinition.baseAttack;
		baseSpeed = _characterDefinition.baseSpeed;
		baseLuck = _characterDefinition.baseLuck;
		baseDodge = _characterDefinition.baseDodge;
		_hitPointsTier = data.hitPointsTier;
		_attackTier = data.attackTier;
		_speedTier = data.speedTier;
		_luckTier = data.luckTier;
		_dodgeTier = data.dodgeTier;
		hitPointsTempIncrease = 0;
		attackTempIncrease = 0;
		LevelData dataForLevel = _levelTable.GetDataForLevel(_level);
		if (_xp < dataForLevel.xpStart || _xp >= dataForLevel.xpToNextLevel)
		{
			_xp = dataForLevel.xpStart;
		}
		processedRewards.Clear();
		int count = data.selectedRewards.Count;
		LevelUpReward levelUpReward = null;
		for (int j = 0; j < count; j++)
		{
			levelUpReward = _levelTable.GetRewardById(data.selectedRewards[j]);
			if (levelUpReward.type == LevelUpRewardType.AbilityUnlock)
			{
				processedRewards.Add(levelUpReward.uid);
				AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots.GetLastItem().abilityId);
				if (!_abilities.Contains(ability))
				{
					_abilities.Add(ability);
				}
			}
			else
			{
				ProcessReward(levelUpReward);
			}
		}
		_abilityUIDs = new List<int>(data.abilityUIDs);
		List<AbilityData> list = new List<AbilityData>();
		bool flag = true;
		if (_abilityUIDs.Count > 0)
		{
			for (int k = 0; k < data.abilityUIDs.Count; k++)
			{
				int num = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_abilityUIDs[k]));
				if (num > -1)
				{
					list.Add(_abilities[num]);
				}
			}
			flag = list.Count != _abilityUIDs.Count;
		}
		if (flag)
		{
			for (int l = 0; l < _characterDefinition.orderedSlots.Count; l++)
			{
				int num2 = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.orderedSlots[l].abilityId));
				if (num2 > -1)
				{
					list.Add(_abilities[num2]);
				}
			}
		}
		_abilities = list;
		if (_modChipUnlocked)
		{
			_modChip = ((data.modChipUID <= -1) ? null : ((ModChipDefinition)GameManager.instance.itemDatabase.GetItem(data.modChipUID)));
			_modChipSlot.Insert(_modChip);
		}
		if (data.hitPoints > -1)
		{
			baseHitPoints = data.hitPoints;
		}
		if (data.attack > -1)
		{
			baseAttack = data.attack;
		}
		if (data.luck > -1)
		{
			baseLuck = data.luck;
		}
		if (data.dodge > -1)
		{
			baseDodge = data.dodge;
		}
		_hitPoints = maxHitPoints;
	}

	public CharacterData GetCharacterData()
	{
		CharacterData characterData = new CharacterData();
		characterData.uid = uid;
		characterData.level = _level;
		characterData.xp = _xp;
		characterData.shinyPoints = _shinyPoints;
		characterData.shinyRank = _shinyRank;
		characterData.abilityUIDs = new List<int>(_abilityUIDs);
		characterData.isFavorite = _isFavorite;
		characterData.isViewed = _isViewed;
		characterData.characterUID = _characterDefinition.uid;
		characterData.lastRewardLevel = _lastRewardLevel;
		characterData.selectedRewards = _selectedRewards;
		characterData.hitPointsTier = _hitPointsTier;
		characterData.attackTier = _attackTier;
		characterData.speedTier = _speedTier;
		characterData.luckTier = _luckTier;
		characterData.dodgeTier = _dodgeTier;
		characterData.repaintColorsUnlocked = _repaintColorsUnlocked;
		characterData.repaintColorID = _repaintColorID;
		if (_modChip != null)
		{
			characterData.modChipUID = _modChip.uid;
		}
		return characterData;
	}

	public void EquipModChip(ModChipDefinition modChipDef)
	{
		if (!(modChipDef == null) && _modChipUnlocked)
		{
			_modChip = modChipDef;
			_modChipSlot.Insert(_modChip);
			OnUpdated();
			OnModChipEquipped(modChipDef);
		}
	}

	public void SetRepaintColorID(int id)
	{
		repaintColorID = id;
	}

	public void SetNewAbilityOrder(List<AbilityData> newOrder)
	{
		_abilities.Clear();
		_abilityUIDs.Clear();
		for (int i = 0; i < newOrder.Count; i++)
		{
			Debug.LogFormat("Ability #{0}: {1}", i + 1, newOrder[i].abilityName);
			_abilities.Add(newOrder[i]);
			_abilityUIDs.Add(newOrder[i].uid);
		}
		OnUpdated();
	}

	public void AddXP(int amount)
	{
		if (isMaxLevel)
		{
			return;
		}
		_xp += amount;
		OnGainedXP();
		while (_xp >= currentLevelData.xpToNextLevel)
		{
			_level++;
			if (_level >= 5)
			{
				Player.instance.SetTrigger("EnhancementUnlocked");
			}
			if (_level >= 10)
			{
				Player.instance.SetTrigger("Level10FigAcquired");
			}
			OnLeveled();
			if (isMaxLevel)
			{
				break;
			}
		}
	}

	public void Transform(string id)
	{
		CharacterTransformation characterTransformationById = _characterDefinition.GetCharacterTransformationById(id);
		if (characterTransformationById == _activeTransformation)
		{
			return;
		}
		_activeTransformation = characterTransformationById;
		int count = _abilities.Count;
		_abilities.Clear();
		for (int i = 0; i < count; i++)
		{
			_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots[i].abilityId));
		}
		List<AbilityData> newAbilities = new List<AbilityData>();
		bool flag = true;
		if (_abilityUIDs.Count > 0)
		{
			for (int j = 0; j < _abilityUIDs.Count; j++)
			{
				int num = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_abilityUIDs[j]));
				if (num > -1)
				{
					newAbilities.Add(_abilities[num]);
				}
			}
			flag = newAbilities.Count != _abilityUIDs.Count;
		}
		if (flag)
		{
			for (int k = 0; k < _characterDefinition.orderedSlots.Count; k++)
			{
				int num2 = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.orderedSlots[k].abilityId));
				if (num2 > -1)
				{
					newAbilities.Add(_abilities[num2]);
				}
			}
		}
		if (_activeTransformation != null)
		{
			for (int l = 0; l < newAbilities.Count; l++)
			{
				_abilities[l] = GameManager.instance.abilityDatabase.GetAbility(_activeTransformation.orderedSlots[_characterDefinition.orderedSlots.FindIndex((AbilitySlot x) => x.abilityId == newAbilities[l].uid)].abilityId);
			}
		}
		else
		{
			_abilities = newAbilities;
		}
		OnTransformed();
	}

	public void ResetTransformation()
	{
		if (_activeTransformation == null)
		{
			return;
		}
		int count = _abilities.Count;
		_abilities.Clear();
		for (int i = 0; i < count; i++)
		{
			_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots[i].abilityId));
		}
		List<AbilityData> list = new List<AbilityData>();
		bool flag = true;
		if (_abilityUIDs.Count > 0)
		{
			for (int j = 0; j < _abilityUIDs.Count; j++)
			{
				int num = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_abilityUIDs[j]));
				if (num > -1)
				{
					list.Add(_abilities[num]);
				}
			}
			flag = list.Count != _abilityUIDs.Count;
		}
		if (flag)
		{
			for (int k = 0; k < _characterDefinition.orderedSlots.Count; k++)
			{
				int num2 = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.orderedSlots[k].abilityId));
				if (num2 > -1)
				{
					list.Add(_abilities[num2]);
				}
			}
		}
		_abilities = list;
		_activeTransformation = null;
	}

	private IEnumerator DoDamageCalculation()
	{
		yield return null;
	}

	public void DoDamage(DamageResult damageResult)
	{
		if (damageResult.source != null)
		{
			damageResult.source.OnOutgoingDamage(damageResult);
		}
		OnIncomingDamage(damageResult);
		if (damageResult.isDodged)
		{
			OnDodged(damageResult);
		}
		else
		{
			Debug.LogFormat("<color=yellow>[Character::DoDamage]</color> - Doing {0} damage to {1}", damageResult.totalDamage, characterDefinition.characterName);
			hitPoints -= damageResult.totalDamage;
			if (damageResult.totalDamage > 0)
			{
				DoInterrupt(new InterruptResult());
			}
		}
		OnDamageReceived(damageResult);
		if (damageResult.source != null)
		{
			damageResult.source.OnDamageDealt(damageResult);
		}
	}

	public void DoHeal(HealResult healResult)
	{
		if (healResult.source != null)
		{
			healResult.source.OnOutgoingHeal(healResult);
		}
		OnIncomingHeal(healResult);
		hitPoints += healResult.amount;
		OnHealReceived(healResult);
		if (healResult.source != null)
		{
			healResult.source.OnHealDealt(healResult);
		}
	}

	public void DoBuff(BuffResult buffResult)
	{
		OnIncomingBuff(buffResult);
		switch (buffResult.statType)
		{
		case CharacterStatType.Attack:
			if (buffResult.isBonus)
			{
				tempAttackBonus += buffResult.amount;
			}
			else
			{
				attackModifier += buffResult.modifier;
			}
			break;
		case CharacterStatType.Speed:
			if (buffResult.isBonus)
			{
				tempSpeedBonus += buffResult.amount;
			}
			else
			{
				speedModifier += buffResult.modifier;
			}
			break;
		case CharacterStatType.Dodge:
			if (buffResult.isBonus)
			{
				tempDodgeBonus += buffResult.amount;
			}
			else
			{
				dodgeModifier += buffResult.modifier;
			}
			break;
		case CharacterStatType.Luck:
			if (buffResult.isBonus)
			{
				tempLuckBonus += buffResult.amount;
				break;
			}
			luckModifier += buffResult.modifier;
			Debug.Log($"Modified Luck Modifier by {buffResult.modifier}. New luckModifier value is {luckModifier}");
			break;
		}
		OnBuffReceived(buffResult);
	}

	public void DoInterrupt(InterruptResult result)
	{
		OnIncomingInterrupt(result);
		OnInterruptReceived(result);
	}

	public void AddStatusEffect(StatusEffect effect)
	{
		effect.Removed += HandleStatusEffectRemoved;
		_statusEffects.Add(effect);
		OnStatusEffectAdded(effect);
		OnUpdated();
	}

	public void RemoveStatusEffect(Type type)
	{
		StatusEffect statusEffect = _statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(type));
		if (statusEffect != null)
		{
			statusEffect.Remove();
			OnUpdated();
		}
	}

	public void RemoveStatusEffect<T>()
	{
		StatusEffect statusEffect = _statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(typeof(T)));
		if (statusEffect != null)
		{
			statusEffect.Remove();
			OnUpdated();
		}
	}

	public void RemoveAllStatusEffectsByType(StatusEffectType type, bool isCleanse)
	{
		int num = _statusEffects.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (_statusEffects[num2].type == type)
			{
				_statusEffects[num2].Remove();
			}
		}
		OnUpdated();
	}

	public void RemoveAllStatusEffects()
	{
		int num = _statusEffects.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			_statusEffects[num2].Remove();
		}
		OnUpdated();
	}

	public StatusEffect GetStatusEffect(Type type)
	{
		return _statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(type));
	}

	public T GetStatusEffect<T>() where T : StatusEffect
	{
		return (T)_statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(typeof(T)));
	}

	public bool HasStatusEffect(Type type)
	{
		return GetStatusEffect(type) != null;
	}

	public bool HasStatusEffect<T>() where T : StatusEffect
	{
		return GetStatusEffect<T>() != null;
	}

	public bool HasStatusEffect(StatusEffectType type)
	{
		return _statusEffects.Find((StatusEffect obj) => obj.type == type) != null;
	}

	public bool HasZoomedOutStatusEffect()
	{
		return _statusEffects.Find((StatusEffect obj) => obj.isZoomedOut) != null;
	}

	private void HandleStatusEffectRemoved(StatusEffect effect)
	{
		effect.Removed -= HandleStatusEffectRemoved;
		_statusEffects.Remove(effect);
		OnStatusEffectRemoved(effect);
		OnUpdated();
	}

	public int GetBaseStatByType(CharacterStatType type)
	{
		return type switch
		{
			CharacterStatType.HitPoints => baseHitPoints, 
			CharacterStatType.Attack => baseAttack, 
			CharacterStatType.Speed => baseSpeed, 
			CharacterStatType.Luck => baseLuck, 
			_ => baseDodge, 
		};
	}

	public int GetModifiedBaseStatByType(CharacterStatType type)
	{
		return type switch
		{
			CharacterStatType.HitPoints => maxHitPoints, 
			CharacterStatType.Attack => attack, 
			CharacterStatType.Speed => speed, 
			CharacterStatType.Luck => luck, 
			_ => dodge, 
		};
	}

	public int GetBaseStatOffsetByType(CharacterStatType type)
	{
		return type switch
		{
			CharacterStatType.HitPoints => characterDefinition.baseHitPointsOffset, 
			CharacterStatType.Attack => characterDefinition.baseAttackOffset, 
			CharacterStatType.Speed => characterDefinition.baseSpeedOffset, 
			CharacterStatType.Luck => characterDefinition.baseLuckOffset, 
			_ => characterDefinition.baseDodgeOffset, 
		};
	}

	public int GetBaseStatPlusOffsetByType(CharacterStatType type)
	{
		return type switch
		{
			CharacterStatType.HitPoints => baseHitPoints + characterDefinition.baseHitPointsOffset, 
			CharacterStatType.Attack => baseAttack + characterDefinition.baseAttackOffset, 
			CharacterStatType.Speed => baseSpeed + characterDefinition.baseSpeedOffset, 
			CharacterStatType.Luck => baseLuck + characterDefinition.baseLuckOffset, 
			_ => baseDodge + characterDefinition.baseDodgeOffset, 
		};
	}

	public int GetStatBonusByType(CharacterStatType type)
	{
		return type switch
		{
			CharacterStatType.HitPoints => hitPointsBonus, 
			CharacterStatType.Attack => attackBonus, 
			CharacterStatType.Speed => speedBonus, 
			CharacterStatType.Luck => luckBonus, 
			_ => dodgeBonus, 
		};
	}

	public void SelectReward(LevelUpReward reward)
	{
		if (_selectedRewards.Contains(reward.uid))
		{
			return;
		}
		_selectedRewards.Add(reward.uid);
		ProcessReward(reward);
		if (reward.type == LevelUpRewardType.StatUpgrade)
		{
			switch (reward.statType)
			{
			case CharacterStatType.HitPoints:
				_hitPointsTier++;
				Debug.Log("Increased HitPoints tier level");
				break;
			case CharacterStatType.Attack:
				_attackTier++;
				Debug.Log("Increased Attack tier level");
				break;
			case CharacterStatType.Speed:
				_speedTier++;
				Debug.Log("Increased Speed tier level");
				break;
			case CharacterStatType.Luck:
				_luckTier++;
				Debug.Log("Increased Luck tier level");
				break;
			case CharacterStatType.Dodge:
				_dodgeTier++;
				Debug.Log("Increased Dodge tier level");
				break;
			}
		}
		_lastRewardLevel++;
		Debug.Log("lastRewardLevel is now " + _lastRewardLevel);
		OnUpdated();
	}

	public bool IsAbilityShiny(AbilityData ability)
	{
		if (ability.type == AbilityType.Attack && ability.damageType == DamageType.RemoteBomb)
		{
			ability = GameManager.instance.abilityDatabase.GetAbility(ability.intValue1);
			if (ability == null)
			{
				Debug.Log("Could not find Remote Bomb Place Ability...");
				return false;
			}
		}
		List<AbilitySlot> list = ((_activeTransformation != null) ? _activeTransformation.orderedSlots : _characterDefinition.orderedSlots);
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			if (list[i].abilityId == ability.uid)
			{
				return shinyRank >= i + 1;
			}
		}
		return false;
	}

	public AbilitySlot GetAbilitySlot(AbilityData ability)
	{
		List<AbilitySlot> list = ((_activeTransformation != null) ? _activeTransformation.orderedSlots : _characterDefinition.orderedSlots);
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			AbilitySlot abilitySlot = list[i];
			if (abilitySlot.abilityId == ability.uid)
			{
				return abilitySlot;
			}
		}
		return null;
	}

	public int GetAbilityPosition(AbilityData abilityData)
	{
		List<AbilitySlot> list = ((_activeTransformation != null) ? _activeTransformation.orderedSlots : _characterDefinition.orderedSlots);
		for (int i = 0; i < abilities.Count; i++)
		{
			if (abilities[i] == abilityData)
			{
				return list[i].position;
			}
		}
		return -1;
	}

	private void ProcessReward(LevelUpReward reward)
	{
		if (processedRewards.Contains(reward.uid))
		{
			return;
		}
		switch (reward.type)
		{
		case LevelUpRewardType.StatUpgrade:
			switch (reward.statType)
			{
			case CharacterStatType.HitPoints:
				baseHitPoints = Mathf.Min(baseHitPoints + reward.statValueInt, _characterDefinition.maxHitPoints - _characterDefinition.baseHitPointsOffset);
				break;
			case CharacterStatType.Attack:
				baseAttack = Mathf.Min(baseAttack + reward.statValueInt, _characterDefinition.maxAttack - _characterDefinition.baseAttackOffset);
				break;
			case CharacterStatType.Speed:
				baseSpeed = Mathf.Min(baseSpeed + reward.statValueInt, _characterDefinition.maxSpeed - _characterDefinition.baseSpeedOffset);
				break;
			case CharacterStatType.Luck:
				baseLuck = Mathf.Min(baseLuck + reward.statValueInt, _characterDefinition.maxLuck - _characterDefinition.baseLuckOffset);
				break;
			case CharacterStatType.Dodge:
				baseDodge = Mathf.Min(baseDodge + reward.statValueInt, _characterDefinition.maxDodge - _characterDefinition.baseDodgeOffset);
				break;
			}
			break;
		case LevelUpRewardType.AbilityUnlock:
		{
			_abilities.Add(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.abilitySlots.GetLastItem().abilityId));
			List<AbilityData> list = new List<AbilityData>();
			for (int i = 0; i < _characterDefinition.orderedSlots.Count; i++)
			{
				int num = _abilities.IndexOf(GameManager.instance.abilityDatabase.GetAbility(_characterDefinition.orderedSlots[i].abilityId));
				if (num > -1)
				{
					list.Add(_abilities[num]);
				}
			}
			_abilities = list;
			break;
		}
		case LevelUpRewardType.ModChipUnlock:
			_modChipUnlocked = true;
			break;
		case LevelUpRewardType.CustomizationUnlock:
			_abilityCustomizationUnlocked = true;
			break;
		}
		processedRewards.Add(reward.uid);
	}

	protected void OnUpdated()
	{
		if (Updated != null)
		{
			Updated();
		}
		if (UpdatedCharacter != null)
		{
			UpdatedCharacter(this);
		}
	}

	protected void OnDeath()
	{
		if (Death != null)
		{
			Death();
		}
		if (DeathCharacter != null)
		{
			DeathCharacter(this);
		}
	}

	public void OnDeathComplete()
	{
		if (DeathComplete != null)
		{
			DeathComplete();
		}
	}

	protected void OnOutgoingDamage(DamageResult damageResult)
	{
		if (OutgoingDamage != null)
		{
			OutgoingDamage(damageResult);
		}
	}

	protected void OnIncomingDamage(DamageResult damageResult)
	{
		if (IncomingDamage != null)
		{
			IncomingDamage(damageResult);
		}
	}

	protected void OnDamageReceived(DamageResult damageResult)
	{
		if (DamageReceived != null)
		{
			DamageReceived(damageResult);
		}
	}

	protected void OnDamageDealt(DamageResult damageResult)
	{
		if (DamageDealt != null)
		{
			DamageDealt(damageResult);
		}
	}

	protected void OnOutgoingHeal(HealResult healResult)
	{
		if (OutgoingHeal != null)
		{
			OutgoingHeal(healResult);
		}
	}

	protected void OnIncomingHeal(HealResult healResult)
	{
		if (IncomingHeal != null)
		{
			IncomingHeal(healResult);
		}
	}

	protected void OnHealReceived(HealResult healResult)
	{
		if (HealReceived != null)
		{
			HealReceived(healResult);
		}
	}

	protected void OnHealDealt(HealResult healResult)
	{
		if (HealDealt != null)
		{
			HealDealt(healResult);
		}
	}

	protected void OnIncomingBuff(BuffResult buffResult)
	{
		if (IncomingBuff != null)
		{
			IncomingBuff(buffResult);
		}
	}

	protected void OnBuffReceived(BuffResult buffResult)
	{
		if (BuffReceived != null)
		{
			BuffReceived(buffResult);
		}
	}

	protected void OnHurt()
	{
		if (Hurt != null)
		{
			Hurt();
		}
	}

	protected void OnHealed()
	{
		if (Healed != null)
		{
			Healed();
		}
	}

	protected void OnDodged(DamageResult result)
	{
		if (Dodged != null)
		{
			Dodged(result);
		}
	}

	protected void OnIncomingInterrupt(InterruptResult result)
	{
		if (IncomingInterrupt != null)
		{
			IncomingInterrupt(result);
		}
	}

	protected void OnInterruptReceived(InterruptResult result)
	{
		if (InterruptReceived != null)
		{
			InterruptReceived(result);
		}
	}

	protected void OnStatusEffectAdded(StatusEffect effect)
	{
		if (StatusEffectAdded != null)
		{
			StatusEffectAdded(effect);
		}
	}

	protected void OnStatusEffectRemoved(StatusEffect effect)
	{
		if (StatusEffectRemoved != null)
		{
			StatusEffectRemoved(effect);
		}
	}

	protected void OnTransformed()
	{
		if (Transformed != null)
		{
			Transformed();
		}
	}

	protected void OnGainedXP()
	{
		if (GainedXP != null)
		{
			GainedXP();
		}
	}

	protected void OnLeveled()
	{
		if (Leveled != null)
		{
			Leveled();
		}
	}

	protected void OnModChipEquipped(ModChipDefinition modChipDef)
	{
		if (ModChipEquipped != null)
		{
			ModChipEquipped(modChipDef);
		}
	}

	public int AttackAbilityPositionModified(float abilitySlotPosition)
	{
		float num = Mathf.Min(GameManager.instance.gameSettings.actionMeterMultiplierRange.maxValue - 1f, Mathf.FloorToInt(GameManager.instance.gameSettings.actionMeterMultiplierRange.GetInterpolatedValue(abilitySlotPosition / 100f)));
		int num2 = Mathf.FloorToInt((float)(baseAttack + attackTeamBoost) * num);
		return Mathf.Max(0, Mathf.FloorToInt((float)num2 * Mathf.Max(0f, _baseAttackMultiplier)) + _attackBonus + attackTempIncrease);
	}

	public int AttackAbilityPositionModified(AbilityData ability)
	{
		AbilitySlot abilitySlot = abilitySlots.Find((AbilitySlot obj) => obj.abilityId == ability.uid);
		if (abilitySlot != null)
		{
			return AttackAbilityPositionModified(abilitySlot.position);
		}
		return 0;
	}

	public int GetTempAttackBonusForSlotPosition(float abilitySlotPosition)
	{
		return tempAttackBonus;
	}

	public int GetTempAttackBonusForSlotPosition(AbilityData ability)
	{
		AbilitySlot abilitySlot = abilitySlots.Find((AbilitySlot obj) => obj.abilityId == ability.uid);
		if (abilitySlot != null)
		{
			return GetTempAttackBonusForSlotPosition(abilitySlot.position);
		}
		return 0;
	}

	public int GetPositionMultiplier(AbilityData ability)
	{
		return GetAbilitySlot(ability)?.positionMultiplier ?? 1;
	}
}
