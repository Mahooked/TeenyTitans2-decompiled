using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettings : ScriptableObject
{
	[SerializeField]
	private PlayerTestData _playerTestData;

	[SerializeField]
	private bool _useTestDataInBuild;

	[SerializeField]
	private string _citySceneName = "CityTest";

	[SerializeField]
	[Tooltip("How long the player must hold a button down for the action to be a Long Press.")]
	private float _buttonLongPressTime = 0.5f;

	[Space]
	[Header("Communicator")]
	[SerializeField]
	private float _minNewsFlashQueueDelay = 20f;

	[Space]
	[Header("Costs")]
	[SerializeField]
	private int _spaceCabbieTravelCost = 50;

	[SerializeField]
	private int _surpriseRepaintCost = 20;

	[SerializeField]
	[Tooltip("Maximum amount of currency that can be received when pawning a figure in the tower")]
	private int _maxPawnAmount = 1500;

	[Space]
	[Header("Battle Specific Settings")]
	[SerializeField]
	private bool _easyBattles;

	[SerializeField]
	private float _goTapBonus = 0.25f;

	[SerializeField]
	private int _currencyPerBattle = 500;

	[SerializeField]
	private bool _healthCapEnabled;

	[SerializeField]
	[HideInInspector]
	private TofuSettings _tofuSettings;

	[SerializeField]
	[HideInInspector]
	private BonusDropSettings _bonusDropSettings;

	[SerializeField]
	private float _stunDurationReduction = -0.1f;

	[SerializeField]
	private float _minStunDurationFactor = 0.5f;

	[SerializeField]
	private bool _showBattleMessages = true;

	[SerializeField]
	private bool _showEnemyAccessoryUI;

	[SerializeField]
	private List<DiminishingReturnsSetting> _diminishingReturnsSettings;

	[SerializeField]
	private StatWeights _debugLevelUpStatWeights;

	[Space]
	[Header("Bonus Damage Settings")]
	[SerializeField]
	private float _classBonusDamage = 0.25f;

	[SerializeField]
	private float _luckyBonusDamage = 1.5f;

	[Space]
	[Header("Action Meter Settings")]
	[SerializeField]
	private bool _actionMeterSizeEnabled = true;

	[SerializeField]
	private bool _resetActionMeterOnSwitch;

	[SerializeField]
	[HideInInspector]
	private int _actionMeterMultiplierTiers = 4;

	[SerializeField]
	private RangedFloat _actionMeterMultiplierRange;

	[SerializeField]
	[Tooltip("The range of seconds it takes to completely fill the Action Meter")]
	private RangedFloat _actionMeterFillTimeRange;

	[Space]
	[Header("Drop Chance Settings")]
	[Range(0f, 1f)]
	[SerializeField]
	private float _defaultCoinDropChance = 0.8f;

	[Range(0f, 1f)]
	[SerializeField]
	private float _defaultTofuDropChance = 0.4f;

	[Range(0f, 1f)]
	[SerializeField]
	private float _defaultBatteryDropChance = 0.4f;

	[Space]
	[Header("Accessory/Battery Settings")]
	[Range(0f, 100f)]
	[SerializeField]
	[Tooltip("The amount of Accessory charge gained from batteries while the Accessory is NOT active")]
	private float _batteryPickupChargeUpPercent = 20f;

	[SerializeField]
	[Tooltip("The amount of battery charge gained from using an ability equals: ability Position (0-100) x this modifier")]
	private float _accessoryChargeOnAbilityUsedModifier = 0.2f;

	[Range(0f, 100f)]
	[SerializeField]
	[Tooltip("The amount of Accessory charge gained from picking up batteries while the Accessory is active")]
	private float _batteryPickupChargeUpPercentActiveAccessory = 20f;

	[SerializeField]
	[Tooltip("The amount of battery charge gained from using an ability equals: ability Position (0-100) x this modifier. This is used only while the Accessory is active.")]
	private float _accessoryChargeOnAbilityUsedModifierActiveAccessory = 0.2f;

	[SerializeField]
	private int _minAbilitiesUsedToShowTic = 3;

	[Range(0f, 1f)]
	[SerializeField]
	private float _defaultBatteryChargeTicChance = 0.5f;

	[SerializeField]
	private float _defaultBatteryChargeUpRate = 2f;

	[Range(0f, 100f)]
	[SerializeField]
	private float _defaultBatteryChargeUpPercent = 1f;

	[SerializeField]
	private float _defaultBatteryDrainRate = 0.2f;

	[Range(0f, 100f)]
	[SerializeField]
	private float _defaultBatteryDrainPercent = 1f;

	[SerializeField]
	private int _maxBatteryDrop = 2;

	[SerializeField]
	private int _batteryDropOnPlayerDeath = 1;

	[Space]
	[Header("Class Phrases Settings")]
	[SerializeField]
	private List<ClassPhrase> _cutePhrases;

	[SerializeField]
	private List<string> _cuteSounds;

	[SerializeField]
	private List<ClassPhrase> _darkArtsPhrases;

	[SerializeField]
	private List<string> _darkArtsSounds;

	[SerializeField]
	private List<ClassPhrase> _superPhrases;

	[SerializeField]
	private List<string> _superSounds;

	[SerializeField]
	private List<ClassPhrase> _techPhrases;

	[SerializeField]
	private List<string> _techSounds;

	[SerializeField]
	private List<ClassPhrase> _martialArtsPhrases;

	[SerializeField]
	private List<string> _martialArtsSounds;

	[SerializeField]
	private List<ClassPhrase> _beastPhrases;

	[SerializeField]
	private List<string> _beastSounds;

	[Space]
	[SerializeField]
	private int _startingCurrency = 1000;

	[SerializeField]
	private int _startingRainbowCurrency;

	[SerializeField]
	private List<int> _startingCharacters;

	[SerializeField]
	[HideInInspector]
	private List<CountdownSoundSet> _countdownSoundSets;

	[SerializeField]
	[Range(0f, 1f)]
	private float _defaultPrebattleVOProbability = 0.5f;

	[SerializeField]
	[Range(0f, 1f)]
	private float _enterCityVOProbability = 0.5f;

	[SerializeField]
	private int _hotelCost = 50;

	[SerializeField]
	private int _abilityCustomizationCost = 500;

	[SerializeField]
	[HideInInspector]
	private string _healthStatDescription = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _healthStatDescriptionKey = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _attackStatDescription = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _attackStatDescriptionKey = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _dodgeStatDescription = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _dodgeStatDescriptionKey = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _luckStatDescription = string.Empty;

	[SerializeField]
	[HideInInspector]
	private string _luckStatDescriptionKey = string.Empty;

	[SerializeField]
	private string[] _tapToMoveSFX;

	[SerializeField]
	private string[] _swipeSFX;

	[SerializeField]
	private bool _useSwipeFXOnlyOnSwipe;

	[SerializeField]
	private int _networkMaxPlayersPerRoom = 2;

	[SerializeField]
	private float _networkUpdateRoomHeartbeatSeconds = 3f;

	public PlayerTestData playerTestData
	{
		get
		{
			return _playerTestData;
		}
	}

	public bool useTestDataInBuild
	{
		get
		{
			return _useTestDataInBuild;
		}
	}

	public string citySceneName
	{
		get
		{
			return _citySceneName;
		}
	}

	public float buttonLongPressTime
	{
		get
		{
			return _buttonLongPressTime;
		}
	}

	public float minNewsFlashQueueDelay
	{
		get
		{
			return _minNewsFlashQueueDelay;
		}
	}

	public int spaceCabbieTravelCost
	{
		get
		{
			return _spaceCabbieTravelCost;
		}
	}

	public int surpriseRepaintCost
	{
		get
		{
			return _surpriseRepaintCost;
		}
	}

	public int maxPawnAmount
	{
		get
		{
			return _maxPawnAmount;
		}
	}

	public bool easyBattles
	{
		get
		{
			return _easyBattles;
		}
	}

	public float goTapBonus
	{
		get
		{
			return _goTapBonus;
		}
	}

	public int currencyPerBattle
	{
		get
		{
			return _currencyPerBattle;
		}
	}

	public bool healthCapEnabled
	{
		get
		{
			return _healthCapEnabled;
		}
	}

	public TofuSettings tofuSettings
	{
		get
		{
			return _tofuSettings;
		}
	}

	public BonusDropSettings bonusDropSettings
	{
		get
		{
			return _bonusDropSettings;
		}
	}

	public float stunDurationReduction
	{
		get
		{
			return _stunDurationReduction;
		}
	}

	public float minStunDurationFactor
	{
		get
		{
			return _minStunDurationFactor;
		}
	}

	public bool showBattleMessages
	{
		get
		{
			return _showBattleMessages;
		}
	}

	public bool showEnemyAccessoryUI
	{
		get
		{
			return _showEnemyAccessoryUI;
		}
	}

	public List<DiminishingReturnsSetting> diminishingReturnsSettings
	{
		get
		{
			return _diminishingReturnsSettings;
		}
	}

	public StatWeights debugLevelUpStatWeights
	{
		get
		{
			return _debugLevelUpStatWeights;
		}
	}

	public float classBonusDamage
	{
		get
		{
			return _classBonusDamage;
		}
	}

	public float luckyBonusDamage
	{
		get
		{
			return _luckyBonusDamage;
		}
	}

	public bool actionMeterSizeEnabled
	{
		get
		{
			return _actionMeterSizeEnabled;
		}
	}

	public bool resetActionMeterOnSwitch
	{
		get
		{
			return _resetActionMeterOnSwitch;
		}
	}

	public int actionMeterMultiplierTiers
	{
		get
		{
			return _actionMeterMultiplierTiers;
		}
	}

	public RangedFloat actionMeterMultiplierRange
	{
		get
		{
			return _actionMeterMultiplierRange;
		}
	}

	public RangedFloat actionMeterFillTimeRange
	{
		get
		{
			return _actionMeterFillTimeRange;
		}
	}

	public float defaultCoinDropChance
	{
		get
		{
			return _defaultCoinDropChance;
		}
	}

	public float defaultTofuDropChance
	{
		get
		{
			return _defaultTofuDropChance;
		}
	}

	public float defaultBatteryDropChance
	{
		get
		{
			return _defaultBatteryDropChance;
		}
	}

	public float batteryPickupChargeUpPercent
	{
		get
		{
			return _batteryPickupChargeUpPercent;
		}
	}

	public float accessoryChargeOnAbilityUsedModifier
	{
		get
		{
			return _accessoryChargeOnAbilityUsedModifier;
		}
	}

	public float batteryPickupChargeUpPercentActiveAccessory
	{
		get
		{
			return _batteryPickupChargeUpPercentActiveAccessory;
		}
	}

	public float accessoryChargeOnAbilityUsedModifierActiveAccessory
	{
		get
		{
			return _accessoryChargeOnAbilityUsedModifierActiveAccessory;
		}
	}

	public int minAbilitiesUsedToShowTic
	{
		get
		{
			return _minAbilitiesUsedToShowTic;
		}
	}

	public float defaultBatteryChargeTicChance
	{
		get
		{
			return _defaultBatteryChargeTicChance;
		}
	}

	public float defaultBatteryChargeUpRate
	{
		get
		{
			return _defaultBatteryChargeUpRate;
		}
	}

	public float defaultBatteryChargeUpPercent
	{
		get
		{
			return _defaultBatteryChargeUpPercent;
		}
	}

	public float defaultBatteryDrainRate
	{
		get
		{
			return _defaultBatteryDrainRate;
		}
	}

	public float defaultBatteryDrainPercent
	{
		get
		{
			return _defaultBatteryDrainPercent;
		}
	}

	public int maxBatteryDrop
	{
		get
		{
			return _maxBatteryDrop;
		}
	}

	public int batteryDropOnPlayerDeath
	{
		get
		{
			return _batteryDropOnPlayerDeath;
		}
	}

	public List<ClassPhrase> cutePhrases
	{
		get
		{
			return _cutePhrases;
		}
	}

	public List<string> cuteSounds
	{
		get
		{
			return _cuteSounds;
		}
	}

	public List<ClassPhrase> darkArtsPhrases
	{
		get
		{
			return _darkArtsPhrases;
		}
	}

	public List<string> darkArtsSounds
	{
		get
		{
			return _darkArtsSounds;
		}
	}

	public List<ClassPhrase> superPhrases
	{
		get
		{
			return _superPhrases;
		}
	}

	public List<string> superSounds
	{
		get
		{
			return _superSounds;
		}
	}

	public List<ClassPhrase> techPhrases
	{
		get
		{
			return _techPhrases;
		}
	}

	public List<string> techSounds
	{
		get
		{
			return _techSounds;
		}
	}

	public List<ClassPhrase> martialArtsPhrases
	{
		get
		{
			return _martialArtsPhrases;
		}
	}

	public List<string> martialArtsSounds
	{
		get
		{
			return _martialArtsSounds;
		}
	}

	public List<ClassPhrase> beastPhrases
	{
		get
		{
			return _beastPhrases;
		}
	}

	public List<string> beastSounds
	{
		get
		{
			return _beastSounds;
		}
	}

	public int startingCurrency
	{
		get
		{
			return _startingCurrency;
		}
	}

	public int startingRainbowCurrency
	{
		get
		{
			return _startingRainbowCurrency;
		}
	}

	public List<int> startingCharacters
	{
		get
		{
			return _startingCharacters;
		}
	}

	public List<CountdownSoundSet> countdownSoundSets
	{
		get
		{
			return _countdownSoundSets;
		}
	}

	public float defaultPrebattleVOProbability
	{
		get
		{
			return _defaultPrebattleVOProbability;
		}
	}

	public float enterCityVOProbability
	{
		get
		{
			return _enterCityVOProbability;
		}
	}

	public int hotelCost
	{
		get
		{
			return _hotelCost;
		}
	}

	public int abilityCustomizationCost
	{
		get
		{
			return _abilityCustomizationCost;
		}
	}

	public string healthStatDescription
	{
		get
		{
			return _healthStatDescription;
		}
		set
		{
			_healthStatDescription = value;
		}
	}

	public string healthStatDescriptionKey
	{
		get
		{
			return _healthStatDescriptionKey;
		}
		set
		{
			_healthStatDescriptionKey = value;
		}
	}

	public string attackStatDescription
	{
		get
		{
			return _attackStatDescription;
		}
		set
		{
			_attackStatDescription = value;
		}
	}

	public string attackStatDescriptionKey
	{
		get
		{
			return _attackStatDescriptionKey;
		}
		set
		{
			_attackStatDescriptionKey = value;
		}
	}

	public string dodgeStatDescription
	{
		get
		{
			return _dodgeStatDescription;
		}
		set
		{
			_dodgeStatDescription = value;
		}
	}

	public string dodgeStatDescriptionKey
	{
		get
		{
			return _dodgeStatDescriptionKey;
		}
		set
		{
			_dodgeStatDescriptionKey = value;
		}
	}

	public string luckStatDescription
	{
		get
		{
			return _luckStatDescription;
		}
		set
		{
			_luckStatDescription = value;
		}
	}

	public string luckStatDescriptionKey
	{
		get
		{
			return _luckStatDescriptionKey;
		}
		set
		{
			_luckStatDescriptionKey = value;
		}
	}

	public string[] tapToMoveSFX
	{
		get
		{
			return _tapToMoveSFX;
		}
		set
		{
			_tapToMoveSFX = value;
		}
	}

	public string[] swipeSFX
	{
		get
		{
			return _swipeSFX;
		}
		set
		{
			_swipeSFX = value;
		}
	}

	public bool useSwipeFXOnlyOnSwipe
	{
		get
		{
			return _useSwipeFXOnlyOnSwipe;
		}
		set
		{
			_useSwipeFXOnlyOnSwipe = value;
		}
	}

	public int networkMaxPlayersPerRoom
	{
		get
		{
			return _networkMaxPlayersPerRoom;
		}
		set
		{
			_networkMaxPlayersPerRoom = value;
		}
	}

	public float networkUpdateRoomHeartbeatSeconds
	{
		get
		{
			return _networkUpdateRoomHeartbeatSeconds;
		}
		set
		{
			_networkUpdateRoomHeartbeatSeconds = value;
		}
	}

	private void OnEnable()
	{
	}

	public string GetRandomClassPhrase(CharacterClass characterClass)
	{
		string result = string.Empty;
		switch (characterClass)
		{
		case CharacterClass.Beast:
			result = Language.Get(_beastPhrases[UnityEngine.Random.Range(0, _beastPhrases.Count)].key, "MENUS,GUI");
			break;
		case CharacterClass.Cute:
			result = Language.Get(_cutePhrases[UnityEngine.Random.Range(0, _cutePhrases.Count)].key, "MENUS,GUI");
			break;
		case CharacterClass.DarkArts:
			result = Language.Get(_darkArtsPhrases[UnityEngine.Random.Range(0, _darkArtsPhrases.Count)].key, "MENUS,GUI");
			break;
		case CharacterClass.MartialArts:
			result = Language.Get(_martialArtsPhrases[UnityEngine.Random.Range(0, _martialArtsPhrases.Count)].key, "MENUS,GUI");
			break;
		case CharacterClass.Super:
			result = Language.Get(_superPhrases[UnityEngine.Random.Range(0, _superPhrases.Count)].key, "MENUS,GUI");
			break;
		case CharacterClass.Tech:
			result = Language.Get(_techPhrases[UnityEngine.Random.Range(0, _techPhrases.Count)].key, "MENUS,GUI");
			break;
		}
		return result;
	}

	public string GetRandomClassSound(CharacterClass characterClass)
	{
		string text = string.Empty;
		switch (characterClass)
		{
		case CharacterClass.Beast:
			text = _beastSounds[UnityEngine.Random.Range(0, _beastSounds.Count)];
			break;
		case CharacterClass.Cute:
			text = _cuteSounds[UnityEngine.Random.Range(0, _cuteSounds.Count)];
			break;
		case CharacterClass.DarkArts:
			text = _darkArtsSounds[UnityEngine.Random.Range(0, _darkArtsSounds.Count)];
			break;
		case CharacterClass.MartialArts:
			text = _martialArtsSounds[UnityEngine.Random.Range(0, _martialArtsSounds.Count)];
			break;
		case CharacterClass.Super:
			text = _superSounds[UnityEngine.Random.Range(0, _superSounds.Count)];
			break;
		case CharacterClass.Tech:
			text = _techSounds[UnityEngine.Random.Range(0, _techSounds.Count)];
			break;
		}
		return (!string.IsNullOrEmpty(text)) ? text : string.Empty;
	}

	public CountdownSoundSet GetSoundSetForCharacter(int characterUID)
	{
		return _countdownSoundSets.Find((CountdownSoundSet obj) => obj.characterUID == characterUID);
	}

	public CountdownSoundSet GetRandomCountdownSoundSet()
	{
		if (_countdownSoundSets.Count > 0)
		{
			return _countdownSoundSets[UnityEngine.Random.Range(0, _countdownSoundSets.Count)];
		}
		return null;
	}
}
