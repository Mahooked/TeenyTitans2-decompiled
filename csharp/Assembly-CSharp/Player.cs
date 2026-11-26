using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;

public class Player
{
	public sealed class VersionDeserializationBinder : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
			{
				assemblyName = Assembly.GetExecutingAssembly().FullName;
				return Type.GetType($"{typeName}, {assemblyName}");
			}
			return null;
		}
	}

	private static string filename = "PlayerInfo.ttd";

	private string _name;

	private int _nextPlayerCharacterUID;

	private List<Character> _characters;

	private List<Character> _activeParty;

	private List<Character> _activeTeam;

	private List<int> _accessories;

	private List<int> _activeAccessories;

	private List<int> _charactersAcquired;

	private List<string> _defeatedNPCs;

	private List<string> _triggers;

	private List<int> _repaintSlotsAcquired;

	private List<PlayerReward> _purchasedRewardsQueue;

	private List<FreeRepaintSlot> _freeRepaintSlots;

	private List<InventoryItem> _inventory;

	private List<MailData> _activeMail;

	private List<MissionData> _activeMissions;

	private List<NewsFlashData> _newsFlashes;

	private List<int> _completedNewsFlashes;

	private List<MissionCompletedData> _completedMissions;

	private List<TournamentCompletedData> _completedTournaments;

	private DailyMissionData _currentDailyMission;

	private DailyMissionData _lastCompletedDailyMission;

	private List<string> _unlockedTunnels;

	private List<int> _viewedLibraryCharacters;

	private List<ShopPurchaseData> _shopPurchaseData;

	private PlayerStats _playerStats;

	private int _currency;

	private int _currencyTickets;

	private int _currentDay;

	private int _currentTime;

	private List<KeyValuePair<CityName, int>> _daysPassedInCity;

	private int _rainbowCurrency;

	private int _batteries;

	private PvpData _pvpData;

	private bool usingTestData;

	private bool _isSavingEnabled = true;

	private static Player _instance;

	private int nextPlayerCharacterUID
	{
		get
		{
			_nextPlayerCharacterUID++;
			return _nextPlayerCharacterUID - 1;
		}
	}

	public string name => _name;

	public List<Character> characters => _characters;

	public List<Character> activeParty => _activeParty;

	public List<Character> activeTeam => _activeTeam;

	public List<int> accessories => _accessories;

	public List<int> activeAccessories => _activeAccessories;

	public List<int> charactersAcquired => _charactersAcquired;

	public List<string> defeatedNPCs => _defeatedNPCs;

	public List<string> triggers => _triggers;

	public List<int> repaintSlotsAcquired => _repaintSlotsAcquired;

	public List<FreeRepaintSlot> freeRepaintSlots => _freeRepaintSlots;

	public List<PlayerReward> purchasedRewardsQueue => _purchasedRewardsQueue;

	public PlayerStats playerStats => _playerStats;

	public int currency => _currency;

	public int rainbowCurrency => _rainbowCurrency;

	public int currencyTickets => _currencyTickets;

	public int batteries => _batteries;

	public int currentDay => _currentDay;

	public int currentTime => _currentTime;

	public List<InventoryItem> inventory => _inventory;

	public List<InventoryItem> modChipItems => _inventory.FindAll((InventoryItem x) => x.item is ModChipDefinition);

	public List<InventoryItem> accessoryItems => _inventory.FindAll((InventoryItem x) => x.item is AccessoryItemDefinition);

	public List<InventoryItem> questItems => _inventory.FindAll((InventoryItem x) => x.item is QuestItemDefinition);

	public List<MissionData> activeMissions => _activeMissions;

	public List<MissionCompletedData> completedMissions => _completedMissions;

	public List<TournamentCompletedData> completedTournaments => _completedTournaments;

	public DailyMissionData currentDailyMission => _currentDailyMission;

	public DailyMissionData lastCompletedDailyMission => _lastCompletedDailyMission;

	public List<MailData> activeMail => _activeMail;

	public int totalUnreadMail => _activeMail.FindAll((MailData obj) => !obj.viewed).Count;

	public int totalNewCharacters => _characters.FindAll((Character obj) => !obj.isViewed).Count;

	public bool allPartyAlive
	{
		get
		{
			int count = _activeParty.Count;
			for (int i = 0; i < count; i++)
			{
				if (_activeParty[i].isDead)
				{
					return false;
				}
			}
			return true;
		}
	}

	public bool isActivePartyFull => _activeParty.Count >= 6;

	public bool isSavingEnabled
	{
		get
		{
			return _isSavingEnabled;
		}
		set
		{
			_isSavingEnabled = value;
		}
	}

	public PvpData pvpData => _pvpData;

	public static Player instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new Player();
				_instance.Load();
			}
			return _instance;
		}
	}

	public event Action<Character> CharacterAdded;

	public event Action<Character> CharacterRemoved;

	public event Action CharacterCollectionChanged;

	public event Action PartyChanged;

	public event Action TeamChanged;

	public event Action EquippedAccessoriesChanged;

	public event Action<int> CharacterAcquired;

	public event Action<int> CurrencyUpdated;

	public event Action<int> RainbowCurrencyUpdated;

	public event Action<CurrencyType, int> CurrencyUpdatedType;

	public event Action<int> BatteriesAdded;

	public event Action<int> BatteriesRemoved;

	public event Action<int, bool> MissionAdded;

	public event Action<int> MissionUpdated;

	public event Action<int> MissionCompleted;

	public event Action<int> MissionCollected;

	public event Action<int> MissionViewed;

	public event Action<int> MissionPinned;

	public event Action<int> DailyMissionCompleted;

	public event Action<int> DailyMissionWeeklyProgress;

	public event Action InventoryChanged;

	public event Action GameKeyTotalChange;

	public event Action<string> TriggerSet;

	public event Action<string> TriggerUnset;

	public event Action<string> DefeatedNPC;

	public event Action<Conversation> ConversationPlayed;

	public event Action<int> FreeRepaintSlotAdded;

	public event Action<int> FreeRepaintSlotSpent;

	public event Action<int> MailAdded;

	public event Action<int> MailViewed;

	public event Action DataReset;

	public Player()
	{
		_name = "Robin";
		_nextPlayerCharacterUID = 0;
		_characters = new List<Character>();
		_activeParty = new List<Character>();
		_activeTeam = new List<Character>();
		_accessories = new List<int>();
		_activeAccessories = new List<int>();
		_charactersAcquired = new List<int>();
		_defeatedNPCs = new List<string>();
		_triggers = new List<string>();
		_repaintSlotsAcquired = new List<int>();
		_purchasedRewardsQueue = new List<PlayerReward>();
		_freeRepaintSlots = new List<FreeRepaintSlot>();
		_currency = GameManager.instance.gameSettings.startingCurrency;
		_rainbowCurrency = GameManager.instance.gameSettings.startingRainbowCurrency;
		_inventory = new List<InventoryItem>();
		_activeMail = new List<MailData>();
		_activeMissions = new List<MissionData>();
		_newsFlashes = new List<NewsFlashData>();
		_completedMissions = new List<MissionCompletedData>();
		_completedTournaments = new List<TournamentCompletedData>();
		_completedNewsFlashes = new List<int>();
		_unlockedTunnels = new List<string>();
		_viewedLibraryCharacters = new List<int>();
		_shopPurchaseData = new List<ShopPurchaseData>();
		_currentDailyMission = null;
		_lastCompletedDailyMission = null;
		_playerStats = new PlayerStats();
		_currentDay = 0;
		_currentTime = 0;
		_daysPassedInCity = new List<KeyValuePair<CityName, int>>();
		_pvpData = new PvpData();
	}

	public void Init()
	{
	}

	public void AddCharacter(ShopFigure shopFigure)
	{
		Character character = new Character(nextPlayerCharacterUID);
		character.Init(CharacterRoller.RollCharacterData(GameManager.instance.characterDatabase.GetCharacter(shopFigure.characterUID), character.uid, shopFigure.characterUID, shopFigure.level));
		_characters.Add(character);
		OnCharacterAdded(character);
		OnCharacterCollectionChanged();
	}

	public void AddCharacter(CharacterDefinition definition, int characterLevel)
	{
		Character character = new Character(nextPlayerCharacterUID, definition);
		character.level = characterLevel;
		_characters.Add(character);
		OnCharacterAdded(character);
		OnCharacterCollectionChanged();
	}

	public void AddCharacter(CharacterDefinition definition)
	{
		Character character = new Character(nextPlayerCharacterUID, definition);
		_characters.Add(character);
		OnCharacterAdded(character);
		OnCharacterCollectionChanged();
	}

	public void AddCharacter(CharacterData data)
	{
		data.uid = nextPlayerCharacterUID;
		Character character = new Character(data);
		_characters.Add(character);
		OnCharacterAdded(character);
		OnCharacterCollectionChanged();
	}

	public void RemoveCharacter(Character character)
	{
		if (_characters.Contains(character))
		{
			_characters.Remove(character);
			OnCharacterRemoved(character);
			OnCharacterCollectionChanged();
		}
	}

	public void RemoveCharacterFromAll(Character character)
	{
		RemoveFromParty(character);
		RemoveCharacter(character);
	}

	public void RemoveLowestLevelCharacterWithUID(int playerCharacterUID)
	{
		Character character = null;
		for (int i = 0; i < _characters.Count; i++)
		{
			Character character2 = _characters[i];
			if (character2.characterDefinition.uid == playerCharacterUID)
			{
				if (character == null)
				{
					character = character2;
				}
				else if (character2.level < character.level)
				{
					character = character2;
				}
			}
		}
		if (character != null)
		{
			RemoveCharacterFromAll(character);
		}
	}

	public void AddToParty(Character character)
	{
		if (_characters.Contains(character) && !_activeParty.Contains(character))
		{
			if (_activeParty.Count < 6)
			{
				character.isViewed = true;
				_activeParty.Add(character);
				OnPartyChanged();
			}
			else
			{
				SetTrigger("runTutorialSquadFull");
			}
		}
	}

	public void RemoveFromParty(Character character)
	{
		if (_activeParty.Contains(character))
		{
			RemoveFromTeam(character);
			_activeParty.Remove(character);
			OnPartyChanged();
		}
	}

	public void AddToTeam(Character character)
	{
		if (_activeParty.Contains(character) && !_activeTeam.Contains(character) && _activeTeam.Count < 3)
		{
			_activeTeam.Add(character);
			OnTeamChanged();
		}
	}

	public void RemoveFromTeam(Character character)
	{
		if (_activeTeam.Contains(character))
		{
			_activeTeam.Remove(character);
			OnTeamChanged();
		}
	}

	public void AddAccessory(int accessoryUID)
	{
		if (!_accessories.Contains(accessoryUID))
		{
			_accessories.Add(accessoryUID);
		}
	}

	public void RemoveAccessory(int accessoryUID)
	{
		if (_accessories.Contains(accessoryUID))
		{
			_accessories.Remove(accessoryUID);
		}
	}

	public void AddToEquippedAccessories(int accessoryUID)
	{
		if (_accessories.Contains(accessoryUID) && !_activeAccessories.Contains(accessoryUID))
		{
			if (_accessories.Count >= 1)
			{
				SetTrigger("ActivateAccessoryTutorial");
			}
			if (_activeAccessories.Count < 6)
			{
				_activeAccessories.Add(accessoryUID);
				OnAccessoriesChanged();
			}
		}
	}

	public void RemoveFromEquippedAccessories(int accessoryUID)
	{
		if (_activeAccessories.Contains(accessoryUID))
		{
			_activeAccessories.Remove(accessoryUID);
			OnAccessoriesChanged();
		}
	}

	public void UnequipAllAccessories()
	{
		for (int i = 0; i < _activeAccessories.Count; i++)
		{
			RemoveFromEquippedAccessories(_activeAccessories[i]);
		}
	}

	public void HealActiveParty()
	{
		int count = _activeParty.Count;
		for (int i = 0; i < count; i++)
		{
			_activeParty[i].hitPoints = _activeParty[i].maxHitPoints;
		}
	}

	public Character GetCharacter(int playerCharacterUID)
	{
		return _characters.Find((Character obj) => obj.uid == playerCharacterUID);
	}

	public int GetHighestFigLevelInParty()
	{
		if (activeParty == null || activeParty.Count == 0)
		{
			return 1;
		}
		int level = activeParty[0].level;
		for (int i = 1; i < activeParty.Count; i++)
		{
			if (activeParty[i].level > level)
			{
				level = activeParty[i].level;
			}
		}
		return level;
	}

	public int GetAverageLevelInParty()
	{
		int num = 0;
		for (int i = 0; i < activeParty.Count; i++)
		{
			num += activeParty[i].level;
		}
		return num / activeParty.Count;
	}

	public bool IsCharacterInParty(Character character)
	{
		return _activeParty.Contains(character);
	}

	public bool IsCharacterInParty(int uid)
	{
		return _activeParty.Find((Character x) => x.uid == uid) != null;
	}

	public bool IsCharacterInTeam(Character character)
	{
		return _activeTeam.Contains(character);
	}

	public bool IsCharacterInTeam(int uid)
	{
		return _activeTeam.FindAll((Character x) => x.uid == uid) != null;
	}

	public void AcquireCharacter(int characterUID)
	{
		if (_charactersAcquired.Contains(characterUID))
		{
			return;
		}
		_charactersAcquired.Add(characterUID);
		OnCharacterAcquired(characterUID);
		int num = 0;
		int count = _charactersAcquired.Count;
		for (int i = 0; i < count; i++)
		{
			CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(_charactersAcquired[i]);
			if (character.isActive && !character.isSecret)
			{
				num++;
			}
		}
		int num2 = 0;
		count = GameManager.instance.characterDatabase.characters.Count;
		for (int j = 0; j < count; j++)
		{
			CharacterDefinition characterDefinition = GameManager.instance.characterDatabase.characters[j];
			if (characterDefinition.isActive && !characterDefinition.isSecret)
			{
				num2++;
			}
		}
		if (num >= num2)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_GOTTASNATCHEMALLTHESEQUEL");
		}
		int amountNeeded = GameManager.instance.achievementsDatabase.AchievementDataForId("TT2_JUSTLIKEOLDTIMES").amountNeeded;
		if (num >= amountNeeded)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_JUSTLIKEOLDTIMES", amountNeeded);
		}
		CheckRadicalFigureAchievement();
	}

	public void CheckRadicalFigureAchievement()
	{
		bool flag = true;
		List<CharacterDefinition> list = GameManager.instance.characterDatabase.characters.FindAll((CharacterDefinition x) => x.tags.Contains("Radical"));
		for (int num = 0; num < list.Count; num++)
		{
			if (!HasAcquiredCharacter(list[num]))
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_THENIGHTBEGINSTOSHINE");
		}
	}

	public void UnacquireCharacter(int characterUID)
	{
		if (_charactersAcquired.Contains(characterUID))
		{
			_charactersAcquired.Remove(characterUID);
		}
	}

	public bool HasAcquiredCharacter(CharacterDefinition definition)
	{
		return _charactersAcquired.Contains(definition.uid);
	}

	public bool HasAcquiredCharacter(int characterUID)
	{
		return _charactersAcquired.Contains(characterUID);
	}

	public bool HasCharacter(int defUID)
	{
		for (int i = 0; i < _characters.Count; i++)
		{
			if (_characters[i].characterDefinition.uid == defUID)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCharacterAtLevel(int level)
	{
		int count = _characters.Count;
		for (int i = 0; i < count; i++)
		{
			if (_characters[i].level >= level)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCharacterAtLevelAndNotShinyRank(int level, int shinyRank)
	{
		int count = _characters.Count;
		for (int i = 0; i < count; i++)
		{
			if (_characters[i].level >= level && _characters[i].shinyRank != shinyRank)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCharacterAtLevelAndModChipUnlocked(int level)
	{
		int count = _characters.Count;
		for (int i = 0; i < count; i++)
		{
			if (_characters[i].level >= level && _characters[i].modChipUnlocked)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasAcquiredAccessory(int accessoryUID)
	{
		return _accessories.Contains(accessoryUID);
	}

	public bool IsAccessoryEquipped(int accessoryUID)
	{
		return _activeAccessories.Contains(accessoryUID);
	}

	public void AddCurrency(int amount)
	{
		_currency += amount;
		OnCurrencyUpdated();
	}

	public void AddCurrency(CurrencyType type, int amount)
	{
		switch (type)
		{
		case CurrencyType.Coins:
			AddCurrency(amount);
			break;
		case CurrencyType.RainbowCoins:
			AddRainbowCurrency(amount);
			break;
		}
	}

	public void AddCurrencyNoEvent(int amount)
	{
		_currency += amount;
	}

	public void RemoveCurrency(int amount)
	{
		_currency = Mathf.Max(_currency - amount, 0);
		OnCurrencyUpdated();
	}

	public void AddRainbowCurrency(int amount)
	{
		_rainbowCurrency += amount;
		OnRainbowCurrencyUpdated();
	}

	public void AddRainbowCurrencyNoEvent(int amount)
	{
		_rainbowCurrency += amount;
	}

	public void RemoveRainbowCurrency(int amount)
	{
		_rainbowCurrency = Mathf.Max(_rainbowCurrency - amount, 0);
		OnRainbowCurrencyUpdated();
	}

	public void RemoveCurrency(CurrencyType type, int amount)
	{
		switch (type)
		{
		case CurrencyType.Coins:
			RemoveCurrency(amount);
			break;
		case CurrencyType.RainbowCoins:
			RemoveRainbowCurrency(amount);
			break;
		}
	}

	public bool HasCurrency(CurrencyType type, int amount)
	{
		return type switch
		{
			CurrencyType.Coins => _currency >= amount, 
			CurrencyType.RainbowCoins => _rainbowCurrency >= amount, 
			_ => false, 
		};
	}

	public void AddCurrencyTickets(int amount)
	{
		_currencyTickets += amount;
	}

	public void RemoveCurrencyTickets(int amount)
	{
		_currencyTickets = Mathf.Max(_currencyTickets - amount, 0);
	}

	public void AddBattery()
	{
		_batteries++;
		if (BatteriesAdded != null)
		{
			BatteriesAdded(_batteries);
		}
	}

	public void RemoveBatteries(int amount)
	{
		_batteries = Mathf.Max(_batteries - amount, 0);
		if (BatteriesRemoved != null)
		{
			BatteriesRemoved(_batteries);
		}
	}

	public void AddToDefeatedNPCs(string npcGUID)
	{
		if (!string.IsNullOrEmpty(npcGUID) && !_defeatedNPCs.Contains(npcGUID))
		{
			_defeatedNPCs.Add(npcGUID);
			if (DefeatedNPC != null)
			{
				DefeatedNPC(npcGUID);
			}
		}
	}

	public bool HasDefeatedNPC(string npcGUID)
	{
		if (_defeatedNPCs.Contains(npcGUID))
		{
			return true;
		}
		return false;
	}

	public void ClearDefeatedNPCs()
	{
		_defeatedNPCs.Clear();
	}

	public void TrackConversationPlayed(Conversation conversation)
	{
		if (SaveUtility.GetInt(conversation.name, 0) == 0)
		{
			SaveUtility.SetInt(conversation.name, 1, save: false);
			OnConversationPlayed(conversation);
		}
	}

	public bool IsConversationPlayed(Conversation conversation)
	{
		return SaveUtility.GetInt(conversation.name, 0) == 1;
	}

	protected virtual void OnConversationPlayed(Conversation conversation)
	{
		if (ConversationPlayed != null)
		{
			ConversationPlayed(conversation);
		}
	}

	private ShopPurchaseData GetShopPurchaseData(string inventoryName)
	{
		for (int i = 0; i < _shopPurchaseData.Count; i++)
		{
			if (_shopPurchaseData[i].storeInventoryName.Equals(inventoryName))
			{
				return _shopPurchaseData[i];
			}
		}
		return null;
	}

	public void AddFigureToPurchaseData(string inventoryName, string figureSpot)
	{
		ShopPurchaseData shopPurchaseData = GetShopPurchaseData(inventoryName);
		if (shopPurchaseData == null)
		{
			shopPurchaseData = new ShopPurchaseData();
			shopPurchaseData.storeInventoryName = inventoryName;
			shopPurchaseData.lastRestockDay = currentDay;
			_shopPurchaseData.Add(shopPurchaseData);
		}
		if (!shopPurchaseData.purchasedFigures.Contains(figureSpot))
		{
			shopPurchaseData.purchasedFigures.Add(figureSpot);
		}
	}

	public bool HasPurchasedShopFigure(string inventoryName, string figureSpot)
	{
		ShopPurchaseData shopPurchaseData = GetShopPurchaseData(inventoryName);
		if (shopPurchaseData == null)
		{
			return false;
		}
		if (shopPurchaseData.purchasedFigures.Contains(figureSpot))
		{
			return true;
		}
		return false;
	}

	public void ClearShopPurchaseData()
	{
		_shopPurchaseData.Clear();
	}

	public void SetRepaintColorPreference(Character character, int colorID)
	{
		character.SetRepaintColorID(colorID);
	}

	public int GetRepaintColorPreferenceForCharacterUID(int characterUID)
	{
		for (int i = 0; i < characters.Count; i++)
		{
			if (characters[i].uid == characterUID)
			{
				return characters[i].repaintColorID;
			}
		}
		return 0;
	}

	public void AddToRepaintColorsPurchased(Character character, int colorID)
	{
		if (!character.repaintColorsUnlocked.Contains(colorID))
		{
			character.repaintColorsUnlocked.Add(colorID);
		}
		AcquireRepaintSlot(colorID);
	}

	public bool HasPurchasedRepaintColor(Character character, int colorID)
	{
		return character.repaintColorsUnlocked.Contains(colorID);
	}

	public void AcquireRepaintSlot(int colorID)
	{
		if (!_repaintSlotsAcquired.Contains(colorID))
		{
			_repaintSlotsAcquired.Add(colorID);
		}
	}

	public bool HasAcquiredRepaintSlot(int colorID)
	{
		return _repaintSlotsAcquired.Contains(colorID);
	}

	public void AddFreeRepaintSlot(int colorID)
	{
		AcquireRepaintSlot(colorID);
		int num = _freeRepaintSlots.FindIndex((FreeRepaintSlot x) => x.repaintID == colorID);
		if (num > -1)
		{
			_freeRepaintSlots[num].count++;
		}
		else
		{
			_freeRepaintSlots.Add(new FreeRepaintSlot(colorID, 1));
		}
		if (FreeRepaintSlotAdded != null)
		{
			FreeRepaintSlotAdded(colorID);
		}
	}

	public void SpendFreeRepaintSlot(int colorID)
	{
		int num = _freeRepaintSlots.FindIndex((FreeRepaintSlot x) => x.repaintID == colorID);
		if (num != -1)
		{
			_freeRepaintSlots[num].count--;
			if (_freeRepaintSlots[num].count == 0)
			{
				_freeRepaintSlots.RemoveAt(num);
			}
			if (FreeRepaintSlotSpent != null)
			{
				FreeRepaintSlotSpent(num);
			}
		}
	}

	public bool HasFreeRepaint(int colorID)
	{
		return _freeRepaintSlots.FindIndex((FreeRepaintSlot x) => x.repaintID == colorID) > -1;
	}

	public void AddToPurchasedRewardsQueue(PlayerReward reward)
	{
		if (reward != null)
		{
			_purchasedRewardsQueue.Add(reward.Copy());
		}
	}

	public void AddToPurchasedRewardsQueue(List<PlayerReward> rewards)
	{
		for (int i = 0; i < rewards.Count; i++)
		{
			if (rewards[i] != null)
			{
				_purchasedRewardsQueue.Add(rewards[i].Copy());
			}
		}
	}

	public void SetCurrentDay(int day)
	{
		_currentDay = day;
	}

	public void SetCurrentTime(int time)
	{
		_currentTime = time;
	}

	public void IncreaseCurrentTime(int increaseTime)
	{
		List<NewsFlash> newsFlashes = GameManager.instance.communicatorDatabase.newsFlashes;
		List<NewsFlash> list = new List<NewsFlash>();
		for (int i = 0; i < newsFlashes.Count; i++)
		{
			NewsFlash newsFlash = newsFlashes[i];
			NewsFlashData newsFlashData = GetNewsFlashData(newsFlash.uid);
			MissionCompletedData missionCompletedData = GetMissionCompletedData(newsFlash.missionUIDRequired);
			bool flag = false;
			if (newsFlashData != null && _currentDay > newsFlashData.dayActive + newsFlash.durationDays)
			{
				flag = true;
			}
			if (!flag && missionCompletedData != null && _currentDay >= missionCompletedData.dayCompleted + newsFlash.daysAfter && (_currentTime <= newsFlash.timeOfDay || (_currentTime >= newsFlash.timeOfDay && _currentTime <= newsFlash.timeOfDay + newsFlash.durationTime)) && !list.Contains(newsFlash) && (newsFlashData == null || (newsFlashData != null && !newsFlashData.completed)))
			{
				list.Add(newsFlash);
			}
		}
		int num = _currentTime + increaseTime;
		bool flag2 = true;
		for (int j = 0; j < list.Count; j++)
		{
			NewsFlash newsFlash2 = list[j];
			if (num >= newsFlash2.timeOfDay && num <= newsFlash2.timeOfDay + newsFlash2.durationTime)
			{
				Debug.Log("Increasing time will pass news flash: " + newsFlash2.title);
				flag2 = false;
			}
		}
		if (flag2)
		{
			_currentTime += increaseTime;
		}
		else
		{
			Debug.Log("Do not allow time increase");
		}
	}

	public void SetDaysPassedInCity(CityName city, int day)
	{
		int num = _daysPassedInCity.FindIndex((KeyValuePair<CityName, int> x) => x.Key == city);
		if (num >= 0)
		{
			_daysPassedInCity[num] = new KeyValuePair<CityName, int>(city, day);
		}
		else
		{
			_daysPassedInCity.Add(new KeyValuePair<CityName, int>(city, day));
		}
	}

	public void SetDaysPassedInCity(Dictionary<CityName, int> daysPassedInCities)
	{
		foreach (KeyValuePair<CityName, int> daysPassedInCity in daysPassedInCities)
		{
			SetDaysPassedInCity(daysPassedInCity.Key, daysPassedInCity.Value);
		}
	}

	public int GetDaysPassedInCity(CityName city)
	{
		int num = _daysPassedInCity.FindIndex((KeyValuePair<CityName, int> x) => x.Key == city);
		if (num >= 0)
		{
			return _daysPassedInCity[num].Value;
		}
		_daysPassedInCity.Add(new KeyValuePair<CityName, int>(city, 0));
		return _daysPassedInCity[_daysPassedInCity.Count - 1].Value;
	}

	public Dictionary<CityName, int> GetDaysPassedInCities()
	{
		Dictionary<CityName, int> dictionary = new Dictionary<CityName, int>();
		for (int i = 0; i < _daysPassedInCity.Count; i++)
		{
			KeyValuePair<CityName, int> keyValuePair = _daysPassedInCity[i];
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		return dictionary;
	}

	public void AddItem(ItemDefinition item, int count)
	{
		if (item == null || count < 1)
		{
			return;
		}
		if (item.stackSize == 1)
		{
			for (int i = 0; i < count; i++)
			{
				_inventory.Add(new InventoryItem(item, 1));
			}
		}
		else
		{
			List<InventoryItem> list = _inventory.FindAll((InventoryItem obj) => obj.item == item);
			int num = 0;
			int count2 = list.Count;
			for (int num2 = 0; num2 < count2; num2++)
			{
				num = item.stackSize - list[num2].count;
				if (num >= count)
				{
					list[num2].count += count;
					count = 0;
					break;
				}
				list[num2].count += num;
				count -= num;
			}
			if (count > 0)
			{
				int num3 = Mathf.CeilToInt((float)count / (float)item.stackSize);
				for (int num4 = 0; num4 < num3; num4++)
				{
					if (num4 == num3 - 1)
					{
						_inventory.Add(new InventoryItem(item, count));
						count = 0;
					}
					else
					{
						_inventory.Add(new InventoryItem(item, item.stackSize));
						count -= item.stackSize;
					}
				}
			}
		}
		if (item is ModChipDefinition)
		{
			SetTrigger("ModChipRecieved");
		}
		if (item is AccessoryItemDefinition)
		{
			SetTrigger("ActivateAccessoryTutorial");
		}
		OnInventoryChanged();
	}

	public void AddItem(int uid, int count)
	{
		AddItem(GameManager.instance.itemDatabase.GetItem(uid), count);
	}

	public void AddItem(ItemDefinition item)
	{
		AddItem(item, 1);
	}

	public void RemoveItem(ItemDefinition item, int count)
	{
		if (item == null || count < 1)
		{
			return;
		}
		List<InventoryItem> list = _inventory.FindAll((InventoryItem obj) => obj.item == item);
		if (list.Count == 0)
		{
			Debug.LogError("There are no instances of this item in the player's inventory to remove");
			return;
		}
		if (item.stackSize == 1)
		{
			if (list.Count < count)
			{
				Debug.LogError("The player doesn't have the sufficient number of items that need to be removed");
				return;
			}
			for (int num = 0; num < count; num++)
			{
				_inventory.Remove(list[num]);
			}
		}
		else
		{
			int num2 = 0;
			int count2 = list.Count;
			for (int num3 = 0; num3 < count2; num3++)
			{
				num2 += list[num3].count;
			}
			if (num2 < count)
			{
				Debug.LogError("The player doesn't have enough to remove");
				return;
			}
			for (int num4 = 0; num4 < count2; num4++)
			{
				if (list[num4].count < count)
				{
					count -= list[num4].count;
					_inventory.Remove(list[num4]);
					continue;
				}
				list[num4].count -= count;
				count = 0;
				if (list[num4].count == 0)
				{
					_inventory.Remove(list[num4]);
				}
				break;
			}
		}
		OnInventoryChanged();
	}

	public void RemoveItem(int uid, int count)
	{
		RemoveItem(GameManager.instance.itemDatabase.GetItem(uid), count);
	}

	public void RemoveItem(ItemDefinition item)
	{
		RemoveItem(item, 1);
	}

	public int GetItemCount(int uid)
	{
		int num = 0;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].item.uid == uid)
			{
				num += inventory[i].count;
			}
		}
		return num;
	}

	public bool HasItemOfType(Type type)
	{
		return inventory.Find((InventoryItem obj) => obj.item.GetType().Equals(type)) != null;
	}

	public bool HasItemOfType<T>() where T : ItemDefinition
	{
		return HasItemOfType(typeof(T));
	}

	public bool HasAcquiredItem(ItemDefinition itemDef)
	{
		for (int i = 0; i < _inventory.Count; i++)
		{
			InventoryItem inventoryItem = _inventory[i];
			if (inventoryItem.item.Equals(itemDef))
			{
				return true;
			}
		}
		return false;
	}

	public bool HasActiveMission(int missionUID)
	{
		for (int i = 0; i < _activeMissions.Count; i++)
		{
			if (_activeMissions[i].uid == missionUID)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasActiveMissionOfSideType(MissionSideType sideType)
	{
		for (int i = 0; i < _activeMissions.Count; i++)
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(_activeMissions[i].uid);
			if (mission.sideType == sideType && !activeMissions[i].completed)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasMissionCompleted(int missionUID)
	{
		for (int i = 0; i < _completedMissions.Count; i++)
		{
			MissionCompletedData missionCompletedData = _completedMissions[i];
			if (missionCompletedData.uid.Equals(missionUID))
			{
				return true;
			}
		}
		return false;
	}

	public MissionCompletedData GetMissionCompletedData(int missionUID)
	{
		for (int i = 0; i < _completedMissions.Count; i++)
		{
			MissionCompletedData missionCompletedData = _completedMissions[i];
			if (missionCompletedData.uid.Equals(missionUID))
			{
				return missionCompletedData;
			}
		}
		return null;
	}

	public void DeleteCompletedMission(int missionUID)
	{
		for (int i = 0; i < _completedMissions.Count; i++)
		{
			MissionCompletedData missionCompletedData = _completedMissions[i];
			if (missionCompletedData.uid == missionUID)
			{
				_completedMissions.RemoveAt(i);
			}
		}
	}

	public MissionData GetMissionData(int missionUID)
	{
		for (int i = 0; i < activeMissions.Count; i++)
		{
			if (activeMissions[i].uid == missionUID)
			{
				return activeMissions[i];
			}
		}
		return null;
	}

	public MissionData GetMissionDataByType(MissionSideType sideType)
	{
		for (int i = 0; i < activeMissions.Count; i++)
		{
			if (GameManager.instance.communicatorDatabase.GetMission(_activeMissions[i].uid).sideType == sideType)
			{
				return activeMissions[i];
			}
		}
		return null;
	}

	public List<MissionData> GetMissionDatasByType(MissionSideType sideType)
	{
		List<MissionData> list = new List<MissionData>();
		for (int i = 0; i < _activeMissions.Count; i++)
		{
			if (GameManager.instance.communicatorDatabase.GetMission(_activeMissions[i].uid).sideType == sideType)
			{
				list.Add(_activeMissions[i]);
			}
		}
		return list;
	}

	public MissionData GetPinnedMission()
	{
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			if (missionData.pinned && !missionData.collected)
			{
				return missionData;
			}
		}
		return null;
	}

	public void AddMission(int missionUID, bool isDaily = false)
	{
		if (GetMissionData(missionUID) != null || GetMissionCompletedData(missionUID) != null)
		{
			return;
		}
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		bool flag = false;
		MissionData missionData = new MissionData();
		missionData.uid = missionUID;
		missionData.active = true;
		missionData.isDaily = isDaily;
		missionData.Initialize();
		if (mission.sideType == MissionSideType.Battle)
		{
			missionData.configBattleCounts = ((mission.battleConfigs == null) ? new int[0] : new int[mission.battleConfigs.Length]);
		}
		else if (mission.sideType == MissionSideType.Buy)
		{
			missionData.counts = new int[mission.buyConfigs.Length];
		}
		else if (mission.sideType == MissionSideType.MissionChecklist)
		{
			for (int i = 0; i < _activeMissions.Count; i++)
			{
				if (mission.collectUIDS.Contains(_activeMissions[i].uid) && _activeMissions[i].completed)
				{
					missionData.collectedUIDs.Add(_activeMissions[i].uid);
					missionData.currentAmount++;
					flag = true;
				}
			}
		}
		_activeMissions.Add(missionData);
		OnMissionAdded(missionUID);
		if (activeMissions.Count == 1)
		{
			PinMission(missionUID);
		}
		if (flag)
		{
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				CommunicatorManager.instance.CompleteMission(missionData, allowSave: false);
			}
			UpdateMission(missionData.uid);
		}
		Save();
	}

	public void AddDialogueMission(int missionUID)
	{
		if (GetMissionData(missionUID) != null || GetMissionCompletedData(missionUID) != null)
		{
			return;
		}
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		bool flag = false;
		MissionData missionData = new MissionData();
		missionData.uid = missionUID;
		missionData.active = true;
		if (mission.sideType == MissionSideType.Battle)
		{
			missionData.configBattleCounts = new int[mission.battleConfigs.Length];
		}
		else if (mission.sideType == MissionSideType.Buy)
		{
			missionData.counts = new int[mission.buyConfigs.Length];
		}
		else if (mission.sideType == MissionSideType.MissionChecklist)
		{
			for (int i = 0; i < _activeMissions.Count; i++)
			{
				if (mission.collectUIDS.Contains(_activeMissions[i].uid) && _activeMissions[i].completed)
				{
					missionData.collectedUIDs.Add(_activeMissions[i].uid);
					missionData.currentAmount++;
					flag = true;
				}
			}
		}
		_activeMissions.Add(missionData);
		OnDialogueMissionAdded(missionUID);
		if (activeMissions.Count == 1)
		{
			PinMission(missionUID);
		}
		if (flag)
		{
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				CommunicatorManager.instance.CompleteMission(missionData, allowSave: false);
			}
			UpdateMission(missionData.uid);
		}
		Save();
	}

	public void CompleteMission(int missionUID, bool autoCollect = false)
	{
		if (autoCollect)
		{
			MissionData missionData = GetMissionData(missionUID);
			CollectMission(missionUID);
			OnMissionCompleted(missionUID);
			if (missionData != null && missionData.pinned)
			{
				PinFirstMissionIfAvailable(missionUID);
			}
		}
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData2 = activeMissions[i];
			if (missionData2.uid == missionUID && !missionData2.completed)
			{
				Debug.Log("Complete Mission J");
				missionData2.completed = true;
				OnMissionCompleted(missionUID);
				if (missionData2.pinned)
				{
					PinFirstMissionIfAvailable(missionUID);
				}
				break;
			}
		}
		Save();
	}

	public void RemoveMission(int missionUID)
	{
		Debug.Log("REMOVE MISSION " + missionUID);
		int num = -1;
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			if (missionData.uid == missionUID)
			{
				num = i;
				if (missionData.pinned)
				{
					PinFirstMissionIfAvailable(missionUID);
				}
			}
		}
		if (num > -1)
		{
			_activeMissions.RemoveAt(num);
		}
		OnMissionUpdated(missionUID);
	}

	public void UpdateMission(int missionUID)
	{
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			if (missionData.uid == missionUID)
			{
				OnMissionUpdated(missionUID);
			}
		}
	}

	public void CollectMission(int missionUID, bool notify = true)
	{
		int num = -1;
		int missionUID2 = -1;
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			if (missionData.uid == missionUID)
			{
				missionData.collected = true;
				MissionCompletedData missionCompletedData = new MissionCompletedData();
				missionCompletedData.uid = missionUID;
				missionCompletedData.dayCompleted = currentDay;
				missionCompletedData.jumpCityDayCompleted = GetDaysPassedInCity(CityName.JumpCity);
				missionCompletedData.gothamDayCompleted = GetDaysPassedInCity(CityName.Gotham);
				missionCompletedData.metropolisDayCompleted = GetDaysPassedInCity(CityName.Metropolis);
				if (!_completedMissions.Contains(missionCompletedData))
				{
					_completedMissions.Add(missionCompletedData);
				}
				if (missionData.isDaily)
				{
					CompleteDailyMission(_currentDailyMission, missionData.debugCollected);
				}
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
				if (mission.activatesMission > -1)
				{
					AddMission(mission.activatesMission);
				}
				num = i;
				missionUID2 = missionUID;
				if (missionData.pinned)
				{
					PinFirstMissionIfAvailable(missionUID);
				}
			}
		}
		if (num > -1)
		{
			_activeMissions.RemoveAt(num);
			_activeMissions.TrimExcess();
			if (notify)
			{
				OnMissionUpdated(missionUID2);
			}
		}
		if (activeMissions.Count == 0)
		{
			OnMissionPinned(-1);
		}
		OnMissionCollected(missionUID);
		if (Communicator.instance != null)
		{
			Communicator.instance.CheckCollectedMissionNewsFlashes();
		}
		if (GameManager.instance != null)
		{
			Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(missionUID);
			if (mission2.type == MissionType.Side)
			{
				CheckIfAllSideMissionsComplete();
			}
		}
		Save();
	}

	public void CheckIfAllSideMissionsComplete()
	{
		List<Mission> allSideMissions = GameManager.instance.communicatorDatabase.GetAllSideMissions(includeDailyMissions: false);
		bool flag = true;
		for (int i = 0; i < allSideMissions.Count; i++)
		{
			if (!HasMissionCompleted(allSideMissions[i].uid))
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_NOFIGUNTURNED");
		}
	}

	public void SilentlyCompleteMission(int missionUID, bool save = true)
	{
		Debug.Log("Silently Complete Mission: " + missionUID);
		if (GetMissionCompletedData(missionUID) != null)
		{
			return;
		}
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		MissionData missionData = GetMissionData(missionUID);
		if (missionData == null)
		{
			MissionData missionData2 = new MissionData();
			missionData2.uid = missionUID;
			missionData2.active = true;
			missionData2.isDaily = false;
			missionData = missionData2;
			missionData.Initialize();
			switch (mission.sideType)
			{
			case MissionSideType.Buy:
				missionData.counts = new int[mission.buyConfigs.Length];
				break;
			case MissionSideType.Battle:
				missionData.configBattleCounts = ((mission.battleConfigs == null) ? new int[0] : new int[mission.battleConfigs.Length]);
				break;
			case MissionSideType.MissionChecklist:
			{
				for (int i = 0; i < _activeMissions.Count; i++)
				{
					if (mission.collectUIDS.Contains(_activeMissions[i].uid) && _activeMissions[i].completed)
					{
						missionData.collectedUIDs.Add(_activeMissions[i].uid);
						missionData.currentAmount++;
					}
				}
				break;
			}
			case MissionSideType.DailyMissionCount:
				missionData.currentAmount = mission.amountNeeded;
				break;
			}
			_activeMissions.Add(missionData);
		}
		missionData.completed = true;
		CollectMission(missionData.uid, notify: false);
		if (save)
		{
			Save();
		}
	}

	public void PinFirstMissionIfAvailable(int unpinnedMissionUID)
	{
		bool flag = false;
		int num = -1;
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (mission.type == MissionType.Story && unpinnedMissionUID != mission.uid)
			{
				flag = true;
				num = missionData.uid;
				break;
			}
		}
		if (!flag)
		{
			for (int j = 0; j < activeMissions.Count; j++)
			{
				int uid = activeMissions[j].uid;
				if (uid != unpinnedMissionUID)
				{
					num = uid;
					break;
				}
			}
		}
		if (num > -1)
		{
			MissionData missionData2 = GetMissionData(num);
			missionData2.pinned = true;
			if (missionData2 != null)
			{
				OnMissionPinned(missionData2.uid);
			}
		}
		if (activeMissions.Count == 0)
		{
			OnMissionPinned(-1);
		}
	}

	public void PinMission(int missionUID)
	{
		if (activeMissions.Count == 1)
		{
			_activeMissions[0].pinned = true;
			OnMissionPinned(activeMissions[0].uid);
			return;
		}
		bool flag = false;
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			if (missionData.uid == missionUID && !missionData.pinned)
			{
				missionData.pinned = true;
				continue;
			}
			missionData.pinned = false;
			if (missionData.uid == missionUID && missionUID > 0)
			{
				flag = true;
			}
		}
		OnMissionPinned(missionUID);
		if (flag)
		{
			PinFirstMissionIfAvailable(missionUID);
		}
	}

	public void ViewMission(int missionUID)
	{
		for (int i = 0; i < activeMissions.Count; i++)
		{
			MissionData missionData = activeMissions[i];
			if (missionData.uid == missionUID)
			{
				missionData.viewed = true;
				OnMissionViewed(missionUID);
			}
		}
	}

	public MailData GetMailData(int mailUID)
	{
		for (int i = 0; i < _activeMail.Count; i++)
		{
			if (_activeMail[i].uid == mailUID)
			{
				return _activeMail[i];
			}
		}
		return null;
	}

	public int GetMailIndex(int mailUID)
	{
		for (int i = 0; i < _activeMail.Count; i++)
		{
			if (_activeMail[i].uid == mailUID)
			{
				return i;
			}
		}
		return -1;
	}

	public void AddMail(int mailUID)
	{
		bool flag = false;
		for (int i = 0; i < _activeMail.Count; i++)
		{
			if (_activeMail[i].uid == mailUID)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			MailData mailData = new MailData();
			mailData.uid = mailUID;
			_activeMail.Add(mailData);
			OnMailAdded(mailUID);
		}
		else
		{
			OnMailAdded(mailUID);
		}
	}

	public void ViewMail(int mailUID)
	{
		int num = -1;
		for (int i = 0; i < _activeMail.Count; i++)
		{
			MailData mailData = _activeMail[i];
			if (mailData.uid == mailUID)
			{
				mailData.viewed = true;
			}
		}
		OnMailViewed(mailUID);
	}

	public NewsFlashData GetNewsFlashData(int newsFlashUID)
	{
		for (int i = 0; i < _newsFlashes.Count; i++)
		{
			if (_newsFlashes[i].uid == newsFlashUID)
			{
				return _newsFlashes[i];
			}
		}
		return null;
	}

	public bool HasCompletedNewsFlash(int newsFlashUID)
	{
		for (int i = 0; i < _newsFlashes.Count; i++)
		{
			NewsFlashData newsFlashData = _newsFlashes[i];
			if (newsFlashData.uid == newsFlashUID && newsFlashData.completed)
			{
				return true;
			}
		}
		return false;
	}

	public void AddNewsFlash(int newsFlashUID)
	{
		NewsFlash newsFlash = GameManager.instance.communicatorDatabase.GetNewsFlash(newsFlashUID);
		if (GetNewsFlashData(newsFlashUID) == null)
		{
			NewsFlashData newsFlashData = new NewsFlashData();
			newsFlashData.uid = newsFlashUID;
			newsFlashData.dayActive = GetDaysPassedInCity(newsFlash.cityName);
			newsFlashData.timeActive = currentTime;
			newsFlashData.active = true;
			NewsFlashData item = newsFlashData;
			_newsFlashes.Add(item);
		}
	}

	public void CompleteNewsFlash(int newsFlashUID)
	{
		NewsFlash newsFlash = GameManager.instance.communicatorDatabase.GetNewsFlash(newsFlashUID);
		for (int i = 0; i < _newsFlashes.Count; i++)
		{
			NewsFlashData newsFlashData = _newsFlashes[i];
			if (newsFlashData.uid == newsFlashUID)
			{
				newsFlashData.completed = true;
				newsFlashData.dayCompleted = GetDaysPassedInCity(newsFlash.cityName);
				newsFlashData.timeCompleted = DateTime.Now;
			}
		}
	}

	public void CollectGlint(string guid, int dayCollected)
	{
		SaveUtility.SetInt(guid, dayCollected);
	}

	public bool HasCollectedGlintGUID(string guid)
	{
		return SaveUtility.GetInt(guid, -1) >= 0;
	}

	public int GetCollectedGlintGUIDDay(string guid)
	{
		return SaveUtility.GetInt(guid);
	}

	public void ClearCollectedGlintGUID(string guid)
	{
		SaveUtility.SetInt(guid, -1);
	}

	public void AddUnlockedTunnel(string guid)
	{
		if (!_unlockedTunnels.Contains(guid))
		{
			_unlockedTunnels.Add(guid);
		}
	}

	public bool HasUnlockedTunnel(string guid)
	{
		return _unlockedTunnels.Contains(guid);
	}

	public int GetUnlockedTunnelCount()
	{
		return _unlockedTunnels.Count;
	}

	public void AddViewedLibraryCharacter(int uid)
	{
		if (!_viewedLibraryCharacters.Contains(uid))
		{
			_viewedLibraryCharacters.Add(uid);
		}
	}

	public bool HasViewedLibraryCharacter(int uid)
	{
		return _viewedLibraryCharacters.Contains(uid);
	}

	public int GetViewedLibraryCharacterCount()
	{
		return _viewedLibraryCharacters.Count;
	}

	public void SetTrigger(string trigger)
	{
		if (_triggers.Contains(trigger))
		{
			Debug.Log($"Trigger '{trigger}' has already been set");
			return;
		}
		_triggers.Add(trigger);
		OnTriggerSet(trigger);
	}

	public void UnsetTrigger(string trigger)
	{
		if (_triggers.Contains(trigger))
		{
			_triggers.Remove(trigger);
			OnTriggerUnset(trigger);
		}
	}

	public bool HasTriggered(string trigger)
	{
		return _triggers.Contains(trigger);
	}

	public void AddToKeyTotal(string keyName)
	{
		int value = SaveUtility.GetInt(keyName) + 1;
		SaveUtility.SetInt(keyName, value, save: false);
		OnGameKeyTotalChange();
	}

	public void RemoveFromKeyTotal(string keyName)
	{
		int b = SaveUtility.GetInt(keyName) - 1;
		SaveUtility.SetInt(keyName, Mathf.Max(0, b), save: false);
		OnGameKeyTotalChange();
	}

	public int TotalForKey(string keyName)
	{
		return SaveUtility.GetInt(keyName);
	}

	public void AddCompletedTournament(TournamentDefinition tournamentDef)
	{
		if (!HasCompletedTournament(tournamentDef))
		{
			TournamentCompletedData tournamentCompletedData = new TournamentCompletedData();
			tournamentCompletedData.dayCompleted = currentDay;
			tournamentCompletedData.tournamentName = tournamentDef.tournamentName;
			_completedTournaments.Add(tournamentCompletedData);
			Save();
		}
	}

	public TournamentCompletedData GetTournamentCompletedData(TournamentDefinition tournamentDef)
	{
		for (int i = 0; i < _completedTournaments.Count; i++)
		{
			if (_completedTournaments[i].tournamentName.Equals(tournamentDef.tournamentName))
			{
				return _completedTournaments[i];
			}
		}
		return null;
	}

	public bool HasCompletedTournament(string tournamentName)
	{
		for (int i = 0; i < _completedTournaments.Count; i++)
		{
			if (_completedTournaments[i].tournamentName.Equals(tournamentName))
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCompletedTournament(TournamentDefinition tournamentDef)
	{
		return HasCompletedTournament(tournamentDef.tournamentName);
	}

	public void SetCurrentDailyMission(DailyMissionData newCurrentDailyMission)
	{
		if (_currentDailyMission != null)
		{
			RemoveMission(_currentDailyMission.uid);
			for (int i = 0; i < _completedMissions.Count; i++)
			{
				if (_completedMissions[i].uid == _currentDailyMission.uid)
				{
					_completedMissions.RemoveAt(i);
					break;
				}
			}
		}
		_currentDailyMission = newCurrentDailyMission;
	}

	private void CompleteDailyMission(DailyMissionData dailyMissionData, bool debugCompleted)
	{
		if (dailyMissionData != null)
		{
			Debug.Log("Complete Daily Mission...");
			dailyMissionData.RecordCompletedTime();
			_playerStats.numDailyMissionsCompleted++;
			_lastCompletedDailyMission = dailyMissionData;
			if (DailyMissionWeeklyProgress != null)
			{
				DailyMissionWeeklyProgress(dailyMissionData.uid);
			}
			if (DailyMissionCompleted != null)
			{
				DailyMissionCompleted(dailyMissionData.uid);
			}
		}
	}

	public void AddPvpXP(int baseXPGained, int humanXPGained)
	{
		LegionOfDoomSettings legionOfDoomSettings = GameManager.instance.legionOfDoomSettings;
		int num = ((_pvpData.currentWinStreak > 1) ? (legionOfDoomSettings.winStreakXP * Mathf.Min(legionOfDoomSettings.maxWinStreak, _pvpData.currentWinStreak)) : 0);
		_pvpData.winXPReceived += baseXPGained;
		_pvpData.humanWinXPReceived += humanXPGained;
		_pvpData.winStreakXPReceived += num;
		LegionOfDoomLevelData levelData = GameManager.instance.legionOfDoomLevelTable.GetLevelData(_pvpData.level);
		int num2 = baseXPGained + humanXPGained + num;
		int num3 = levelData.xpToLevelUp - _pvpData.currentXP;
		while (num2 - num3 >= 0)
		{
			_pvpData.currentXP = 0;
			_pvpData.level++;
			levelData = GameManager.instance.legionOfDoomLevelTable.GetLevelData(_pvpData.level);
			num2 -= num3;
			num3 = levelData.xpToLevelUp - _pvpData.currentXP;
		}
		if (num2 > 0)
		{
			_pvpData.currentXP += num2;
		}
	}

	public void AddSkillRating(int rating, bool updatePrevRating = false)
	{
		_pvpData.skillRating = Mathf.Clamp(_pvpData.skillRating + rating, GameManager.instance.legionOfDoomSettings.minSkillRating, GameManager.instance.legionOfDoomSettings.maxSkillRating);
		if (updatePrevRating)
		{
			_pvpData.previousSkillRating = _pvpData.skillRating;
		}
	}

	public void Save()
	{
		if (usingTestData)
		{
			Debug.Log("Skipping Save since using test data");
			return;
		}
		if (!isSavingEnabled)
		{
			Debug.Log("Saving is currently disabled");
			return;
		}
		PlayerData playerData = new PlayerData();
		playerData.name = _name;
		playerData.nextPlayerCharacterUID = _nextPlayerCharacterUID;
		playerData.currency = _currency;
		playerData.rainbowCurrency = _rainbowCurrency;
		playerData.currentDay = _currentDay;
		playerData.currentTime = _currentTime;
		int count = _daysPassedInCity.Count;
		for (int i = 0; i < count; i++)
		{
			playerData.daysPassedInCity.Add(_daysPassedInCity[i]);
		}
		count = _characters.Count;
		for (int j = 0; j < count; j++)
		{
			playerData.characters.Add(_characters[j].GetCharacterData());
		}
		count = _activeParty.Count;
		for (int k = 0; k < count; k++)
		{
			playerData.activeParty.Add(_activeParty[k].uid);
		}
		count = _activeTeam.Count;
		for (int l = 0; l < count; l++)
		{
			playerData.activeTeam.Add(_activeTeam[l].uid);
		}
		count = _charactersAcquired.Count;
		for (int m = 0; m < count; m++)
		{
			playerData.charactersAcquired.Add(_charactersAcquired[m]);
		}
		playerData.repaintSlotsAcquired.AddRange(_repaintSlotsAcquired);
		playerData.freeRepaintSlots.AddRange(_freeRepaintSlots);
		playerData.purchasedRewardsQueue.AddRange(_purchasedRewardsQueue);
		count = _activeMissions.Count;
		for (int n = 0; n < count; n++)
		{
			playerData.activeMissions.Add(_activeMissions[n]);
		}
		count = _activeMail.Count;
		for (int num = 0; num < count; num++)
		{
			playerData.activeMail.Add(_activeMail[num]);
		}
		count = _inventory.Count;
		for (int num2 = 0; num2 < count; num2++)
		{
			playerData.inventory.Add(_inventory[num2].GetInventoryItemData());
		}
		count = _newsFlashes.Count;
		for (int num3 = 0; num3 < count; num3++)
		{
			playerData.activeNewsFlashes.Add(_newsFlashes[num3]);
		}
		count = _completedMissions.Count;
		for (int num4 = 0; num4 < count; num4++)
		{
			playerData.completedMissions.Add(_completedMissions[num4]);
		}
		count = _completedTournaments.Count;
		for (int num5 = 0; num5 < count; num5++)
		{
			playerData.completedTournaments.Add(_completedTournaments[num5]);
		}
		playerData.currentDailyMission = ((_currentDailyMission == null) ? null : _currentDailyMission.Copy());
		playerData.lastCompletedDailyMission = ((_lastCompletedDailyMission == null) ? null : _lastCompletedDailyMission.Copy());
		count = _completedNewsFlashes.Count;
		for (int num6 = 0; num6 < count; num6++)
		{
			playerData.completedNewsFlashes.Add(_completedNewsFlashes[num6]);
		}
		count = _defeatedNPCs.Count;
		for (int num7 = 0; num7 < count; num7++)
		{
			playerData.defeatedNPCs.Add(_defeatedNPCs[num7]);
		}
		count = _unlockedTunnels.Count;
		for (int num8 = 0; num8 < count; num8++)
		{
			playerData.unlockedTunnels.Add(_unlockedTunnels[num8]);
		}
		count = _viewedLibraryCharacters.Count;
		for (int num9 = 0; num9 < count; num9++)
		{
			playerData.viewedLibraryCharacters.Add(_viewedLibraryCharacters[num9]);
		}
		count = _shopPurchaseData.Count;
		for (int num10 = 0; num10 < count; num10++)
		{
			playerData.shopPurchaseData.Add(_shopPurchaseData[num10]);
		}
		playerData.triggers.AddRange(_triggers);
		if (playerData.accessoriesAcquired == null)
		{
			playerData.accessoriesAcquired = new List<int>();
		}
		if (playerData.activeAccessories == null)
		{
			playerData.activeAccessories = new List<int>();
		}
		playerData.accessoriesAcquired.AddRange(_accessories);
		playerData.activeAccessories.AddRange(_activeAccessories);
		playerData.playerStats = _playerStats.Copy();
		if (!FriendCodeGenerator.IsValidFriendCode(_pvpData.playerID))
		{
			_pvpData.playerID = FriendCodeGenerator.Generate();
		}
		playerData.pvpData = _pvpData.Copy();
		SaveUtility.SavePlayerData(playerData);
	}

	public bool Load()
	{
		if (GameManager.instance.gameSettings.useTestDataInBuild && GameManager.instance.gameSettings.playerTestData != null)
		{
			_name = GameManager.instance.gameSettings.playerTestData.playerName;
			_currency = GameManager.instance.gameSettings.playerTestData.currency;
			_rainbowCurrency = GameManager.instance.gameSettings.playerTestData.rainbowCurrency;
			_nextPlayerCharacterUID = GameManager.instance.gameSettings.playerTestData.nextPlayerCharacterUID;
			_characters.Clear();
			_activeParty.Clear();
			_activeTeam.Clear();
			_charactersAcquired.Clear();
			_defeatedNPCs.Clear();
			_triggers.Clear();
			_repaintSlotsAcquired.Clear();
			_freeRepaintSlots.Clear();
			_purchasedRewardsQueue.Clear();
			_inventory.Clear();
			_activeMissions.Clear();
			_activeMail.Clear();
			_newsFlashes.Clear();
			_completedMissions.Clear();
			_completedTournaments.Clear();
			_completedNewsFlashes.Clear();
			_unlockedTunnels.Clear();
			_viewedLibraryCharacters.Clear();
			_shopPurchaseData.Clear();
			int count = GameManager.instance.gameSettings.playerTestData.characters.Count;
			for (int i = 0; i < count; i++)
			{
				_characters.Add(new Character(GameManager.instance.gameSettings.playerTestData.characters[i]));
			}
			count = GameManager.instance.gameSettings.playerTestData.activeParty.Count;
			for (int j = 0; j < count; j++)
			{
				_activeParty.Add(GetCharacter(GameManager.instance.gameSettings.playerTestData.activeParty[j]));
			}
			count = GameManager.instance.gameSettings.playerTestData.activeTeam.Count;
			for (int k = 0; k < count; k++)
			{
				_activeTeam.Add(GetCharacter(GameManager.instance.gameSettings.playerTestData.activeTeam[k]));
			}
			count = GameManager.instance.gameSettings.playerTestData.charactersAcquired.Count;
			for (int l = 0; l < count; l++)
			{
				_charactersAcquired.Add(GameManager.instance.gameSettings.playerTestData.charactersAcquired[l]);
			}
			count = GameManager.instance.gameSettings.playerTestData.inventory.Count;
			for (int m = 0; m < count; m++)
			{
				_inventory.Add(new InventoryItem(GameManager.instance.gameSettings.playerTestData.inventory[m]));
			}
			count = GameManager.instance.gameSettings.playerTestData.unlockedTunnels.Count;
			for (int n = 0; n < count; n++)
			{
				_unlockedTunnels.Add(GameManager.instance.gameSettings.playerTestData.unlockedTunnels[n]);
			}
			count = GameManager.instance.gameSettings.playerTestData.viewedLibraryCharacters.Count;
			for (int num = 0; num < count; num++)
			{
				_viewedLibraryCharacters.Add(GameManager.instance.gameSettings.playerTestData.viewedLibraryCharacters[num]);
			}
			count = GameManager.instance.gameSettings.playerTestData.activeMissions.Count;
			for (int num2 = 0; num2 < count; num2++)
			{
				_activeMissions.Add(GameManager.instance.gameSettings.playerTestData.activeMissions[num2].Copy());
			}
			count = GameManager.instance.gameSettings.playerTestData.completedMissions.Count;
			for (int num3 = 0; num3 < count; num3++)
			{
				_completedMissions.Add(GameManager.instance.gameSettings.playerTestData.completedMissions[num3].Copy());
			}
			count = GameManager.instance.gameSettings.playerTestData.triggers.Count;
			for (int num4 = 0; num4 < count; num4++)
			{
				_triggers.Add(GameManager.instance.gameSettings.playerTestData.triggers[num4]);
			}
			_accessories.AddRange(GameManager.instance.gameSettings.playerTestData.accessoriesAcquired);
			_activeAccessories.AddRange(GameManager.instance.gameSettings.playerTestData.activeAccessories);
			_currentDailyMission = null;
			_lastCompletedDailyMission = null;
			_playerStats = GameManager.instance.gameSettings.playerTestData.playerStats.Copy();
			_pvpData = GameManager.instance.gameSettings.playerTestData.pvpData.Copy();
			if (!FriendCodeGenerator.IsValidFriendCode(_pvpData.playerID))
			{
				_pvpData.playerID = FriendCodeGenerator.Generate();
			}
			if (_pvpData.isInPvpBattle)
			{
				if (_pvpData.isInPvpBattleType == BattleType.Pvp)
				{
					AddSkillRating(-GameManager.instance.legionOfDoomSettings.lossSkillRating, updatePrevRating: true);
					_pvpData.currentWinStreak = 0;
					_pvpData.lastMatchIsLoss = true;
				}
				_pvpData.isInPvpBattle = false;
			}
			SaveUtility.LoadTestGameData(GameManager.instance.gameSettings.playerTestData.gameData);
			usingTestData = true;
			Debug.Log("Loaded Player Test Data for Player Named " + _name);
			return true;
		}
		if (SaveUtility.PlayerDataAvailable())
		{
			PlayerData playerData = SaveUtility.LoadPlayerData();
			if (playerData == null)
			{
				Debug.Log("NOTIFY THE PLAYER THAT THEIR SAVE DATA IS INCOMPATIBLE WITH THIS VERSION OF THE GAME. PLEASE UPGRADE TO THE LATEST VERSION.");
				return false;
			}
			_name = playerData.name;
			_nextPlayerCharacterUID = playerData.nextPlayerCharacterUID;
			_currency = playerData.currency;
			_rainbowCurrency = playerData.rainbowCurrency;
			_characters.Clear();
			_activeParty.Clear();
			_activeTeam.Clear();
			_charactersAcquired.Clear();
			_defeatedNPCs.Clear();
			_triggers.Clear();
			_repaintSlotsAcquired.Clear();
			_freeRepaintSlots.Clear();
			_purchasedRewardsQueue.Clear();
			_inventory.Clear();
			_activeMissions.Clear();
			_activeMail.Clear();
			_newsFlashes.Clear();
			_completedMissions.Clear();
			_completedTournaments.Clear();
			_completedNewsFlashes.Clear();
			_unlockedTunnels.Clear();
			_viewedLibraryCharacters.Clear();
			_shopPurchaseData.Clear();
			_daysPassedInCity.Clear();
			_accessories.Clear();
			_activeAccessories.Clear();
			_currentDay = playerData.currentDay;
			_currentTime = playerData.currentTime;
			if (playerData.daysPassedInCity == null)
			{
				playerData.daysPassedInCity = new List<KeyValuePair<CityName, int>>();
			}
			int count2 = playerData.daysPassedInCity.Count;
			for (int num5 = 0; num5 < count2; num5++)
			{
				_daysPassedInCity.Add(playerData.daysPassedInCity[num5]);
			}
			if (playerData.characters == null)
			{
				playerData.characters = new List<CharacterData>();
			}
			count2 = playerData.characters.Count;
			for (int num6 = 0; num6 < count2; num6++)
			{
				if (playerData.characters[num6] != null && playerData.characters[num6].uid > -1)
				{
					_characters.Add(new Character(playerData.characters[num6]));
				}
			}
			if (playerData.activeParty == null)
			{
				playerData.activeParty = new List<int>();
			}
			count2 = playerData.activeParty.Count;
			List<int> list = new List<int>();
			for (int num7 = 0; num7 < count2; num7++)
			{
				Character character = GetCharacter(playerData.activeParty[num7]);
				if (character != null)
				{
					_activeParty.Add(character);
				}
				else
				{
					list.Add(playerData.activeParty[num7]);
				}
			}
			for (int num8 = 0; num8 < list.Count; num8++)
			{
				playerData.activeParty.Remove(list[num8]);
			}
			if (playerData.activeTeam == null)
			{
				playerData.activeTeam = new List<int>();
			}
			if (playerData.charactersAcquired == null)
			{
				playerData.charactersAcquired = new List<int>();
			}
			count2 = playerData.charactersAcquired.Count;
			for (int num9 = 0; num9 < count2; num9++)
			{
				if (playerData.charactersAcquired[num9] > -1)
				{
					_charactersAcquired.Add(playerData.charactersAcquired[num9]);
				}
			}
			if (playerData.defeatedNPCs == null)
			{
				playerData.defeatedNPCs = new List<string>();
			}
			count2 = playerData.defeatedNPCs.Count;
			for (int num10 = 0; num10 < count2; num10++)
			{
				if (!string.IsNullOrEmpty(playerData.defeatedNPCs[num10]))
				{
					_defeatedNPCs.Add(playerData.defeatedNPCs[num10]);
				}
			}
			if (playerData.triggers == null)
			{
				playerData.triggers = new List<string>();
			}
			_triggers.AddRange(playerData.triggers);
			if (_charactersAcquired.Contains(80) && !_triggers.Contains("secret_figure_unlocked_80"))
			{
				_triggers.Add("secret_figure_unlocked_80");
			}
			if (_charactersAcquired.Contains(81) && !_triggers.Contains("secret_figure_unlocked_81"))
			{
				_triggers.Add("secret_figure_unlocked_81");
			}
			if (playerData.repaintSlotsAcquired == null)
			{
				playerData.repaintSlotsAcquired = new List<int>();
			}
			_repaintSlotsAcquired.AddRange(playerData.repaintSlotsAcquired);
			if (playerData.freeRepaintSlots == null)
			{
				playerData.freeRepaintSlots = new List<FreeRepaintSlot>();
			}
			_freeRepaintSlots.AddRange(playerData.freeRepaintSlots);
			if (playerData.purchasedRewardsQueue == null)
			{
				playerData.purchasedRewardsQueue = new List<PlayerReward>();
			}
			for (int num11 = 0; num11 < playerData.purchasedRewardsQueue.Count; num11++)
			{
				_purchasedRewardsQueue.Add(playerData.purchasedRewardsQueue[num11].Copy());
			}
			if (playerData.inventory == null)
			{
				playerData.inventory = new List<InventoryItemData>();
			}
			count2 = playerData.inventory.Count;
			for (int num12 = 0; num12 < count2; num12++)
			{
				if (playerData.inventory[num12] != null)
				{
					_inventory.Add(new InventoryItem(playerData.inventory[num12]));
				}
			}
			_currentDailyMission = playerData.currentDailyMission;
			_lastCompletedDailyMission = playerData.lastCompletedDailyMission;
			if (_currentDailyMission != null && _currentDailyMission.isExpired)
			{
				for (int num13 = 0; num13 < playerData.activeMissions.Count; num13++)
				{
					if (playerData.activeMissions[num13].uid == _currentDailyMission.uid)
					{
						playerData.activeMissions.RemoveAt(num13);
						break;
					}
				}
				for (int num14 = 0; num14 < playerData.completedMissions.Count; num14++)
				{
					if (playerData.completedMissions[num14].uid == _currentDailyMission.uid)
					{
						playerData.completedMissions.RemoveAt(num14);
						break;
					}
				}
				_currentDailyMission = null;
			}
			else if (_currentDailyMission != null && _currentDailyMission.startDateTime > DateTime.Now)
			{
				for (int num15 = 0; num15 < playerData.activeMissions.Count; num15++)
				{
					if (playerData.activeMissions[num15].uid == _currentDailyMission.uid)
					{
						playerData.activeMissions.RemoveAt(num15);
						break;
					}
				}
				for (int num16 = 0; num16 < playerData.completedMissions.Count; num16++)
				{
					if (playerData.completedMissions[num16].uid == _currentDailyMission.uid)
					{
						playerData.completedMissions.RemoveAt(num16);
						break;
					}
				}
				_currentDailyMission = null;
			}
			DailyMissionManager.instance.Init();
			if (playerData.activeMissions == null)
			{
				playerData.activeMissions = new List<MissionData>();
			}
			count2 = playerData.activeMissions.Count;
			for (int num17 = 0; num17 < count2; num17++)
			{
				MissionData missionData = playerData.activeMissions[num17];
				if (missionData == null || missionData.uid <= -1 || (!GameManager.instance.communicatorDatabase.HasMission(missionData.uid) && !missionData.isDaily))
				{
					continue;
				}
				_activeMissions.Add(missionData);
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
				if (!(mission == null))
				{
					if (mission.sideType == MissionSideType.Battle && (missionData.configBattleCounts == null || missionData.configBattleCounts.Length != mission.battleConfigs.Length))
					{
						missionData.configBattleCounts = new int[mission.battleConfigs.Length];
					}
					else if (mission.sideType == MissionSideType.Buy && (missionData.counts == null || missionData.counts.Length != mission.buyConfigs.Length))
					{
						missionData.counts = new int[mission.buyConfigs.Length];
					}
				}
			}
			if (playerData.activeMail == null)
			{
				playerData.activeMail = new List<MailData>();
			}
			count2 = playerData.activeMail.Count;
			for (int num18 = 0; num18 < count2; num18++)
			{
				if (playerData.activeMail[num18] != null && playerData.activeMail[num18].uid > -1)
				{
					_activeMail.Add(playerData.activeMail[num18]);
				}
			}
			if (playerData.activeNewsFlashes == null)
			{
				playerData.activeNewsFlashes = new List<NewsFlashData>();
			}
			count2 = playerData.activeNewsFlashes.Count;
			for (int num19 = 0; num19 < count2; num19++)
			{
				if (playerData.activeNewsFlashes[num19] != null && playerData.activeNewsFlashes[num19].uid > -1)
				{
					_newsFlashes.Add(playerData.activeNewsFlashes[num19]);
				}
			}
			if (playerData.completedMissions == null)
			{
				playerData.completedMissions = new List<MissionCompletedData>();
			}
			count2 = playerData.completedMissions.Count;
			for (int num20 = 0; num20 < count2; num20++)
			{
				if (playerData.completedMissions[num20] != null && playerData.completedMissions[num20].uid > -1)
				{
					_completedMissions.Add(playerData.completedMissions[num20]);
				}
			}
			if (playerData.completedTournaments == null)
			{
				playerData.completedTournaments = new List<TournamentCompletedData>();
			}
			count2 = playerData.completedTournaments.Count;
			for (int num21 = 0; num21 < count2; num21++)
			{
				if (playerData.completedTournaments[num21] != null)
				{
					_completedTournaments.Add(playerData.completedTournaments[num21]);
				}
			}
			if (playerData.completedNewsFlashes == null)
			{
				playerData.completedNewsFlashes = new List<int>();
			}
			count2 = playerData.completedNewsFlashes.Count;
			for (int num22 = 0; num22 < count2; num22++)
			{
				if (playerData.completedNewsFlashes[num22] > -1)
				{
					_completedNewsFlashes.Add(playerData.completedNewsFlashes[num22]);
				}
			}
			if (playerData.unlockedTunnels == null)
			{
				playerData.unlockedTunnels = new List<string>();
			}
			count2 = playerData.unlockedTunnels.Count;
			for (int num23 = 0; num23 < count2; num23++)
			{
				if (!string.IsNullOrEmpty(playerData.unlockedTunnels[num23]))
				{
					_unlockedTunnels.Add(playerData.unlockedTunnels[num23]);
				}
			}
			if (playerData.viewedLibraryCharacters == null)
			{
				playerData.viewedLibraryCharacters = new List<int>();
			}
			count2 = playerData.viewedLibraryCharacters.Count;
			for (int num24 = 0; num24 < count2; num24++)
			{
				_viewedLibraryCharacters.Add(playerData.viewedLibraryCharacters[num24]);
			}
			if (playerData.shopPurchaseData == null)
			{
				playerData.shopPurchaseData = new List<ShopPurchaseData>();
			}
			count2 = playerData.shopPurchaseData.Count;
			for (int num25 = 0; num25 < count2; num25++)
			{
				if (playerData.shopPurchaseData[num25] != null)
				{
					_shopPurchaseData.Add(playerData.shopPurchaseData[num25]);
				}
			}
			if (playerData.accessoriesAcquired == null)
			{
				playerData.accessoriesAcquired = new List<int>();
			}
			_accessories.AddRange(playerData.accessoriesAcquired);
			_accessories = _accessories.Distinct().ToList();
			if (playerData.activeAccessories == null)
			{
				playerData.activeAccessories = new List<int>();
			}
			_activeAccessories.AddRange(playerData.activeAccessories);
			_activeAccessories = _activeAccessories.Distinct().ToList();
			for (int num26 = 0; num26 < _activeMissions.Count; num26++)
			{
				MissionData missionData2 = _activeMissions[num26];
				Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(missionData2.uid);
				if (mission2.sideType != MissionSideType.MissionChecklist)
				{
					continue;
				}
				for (int num27 = 0; num27 < mission2.collectUIDS.Count; num27++)
				{
					if (!missionData2.collectedUIDs.Contains(mission2.collectUIDS[num27]) && _completedMissions.FindIndex((MissionCompletedData obj) => obj.uid == mission2.collectUIDS[num27]) > -1)
					{
						missionData2.collectedUIDs.Add(mission2.collectUIDS[num27]);
						missionData2.currentAmount++;
					}
				}
			}
			_playerStats = playerData.playerStats.Copy();
			if (playerData.pvpData == null)
			{
				playerData.pvpData = new PvpData(FriendCodeGenerator.Generate(), string.Empty);
			}
			_pvpData = playerData.pvpData.Copy();
			if (_pvpData.isInPvpBattle)
			{
				if (_pvpData.isInPvpBattleType == BattleType.Pvp)
				{
					AddSkillRating(-GameManager.instance.legionOfDoomSettings.lossSkillRating, updatePrevRating: true);
					_pvpData.currentWinStreak = 0;
					_pvpData.lastMatchIsLoss = true;
				}
				_pvpData.isInPvpBattle = false;
			}
			Debug.Log("Player.cs, Load Player Data, pvpPlayerID: " + _pvpData.playerID);
			if (SaveUtility.GetInt(string.Concat(CoreGameTutorial.Step.CityTutorialViewMapStart, "_Complete"), 0) == 1)
			{
				SaveUtility.SetInt(string.Concat(CoreGameTutorial.Step.CityTutorialAfterMap, "_Complete"), 1, save: false);
			}
			MissionIntegrityChecker.CheckStoryMissionIntegrityForPlayer(this);
		}
		else
		{
			Debug.Log("Save File Doesn't Exist. Creating One...");
			DailyMissionManager.instance.Init();
			Save();
		}
		return true;
	}

	public void ResetPlayerData()
	{
		_name = "Robin";
		_nextPlayerCharacterUID = 0;
		_characters.Clear();
		_activeParty.Clear();
		_activeTeam.Clear();
		_charactersAcquired.Clear();
		_defeatedNPCs.Clear();
		_triggers.Clear();
		_repaintSlotsAcquired.Clear();
		_freeRepaintSlots.Clear();
		_purchasedRewardsQueue.Clear();
		_inventory.Clear();
		_activeMail.Clear();
		_activeMissions.Clear();
		_newsFlashes.Clear();
		_completedMissions.Clear();
		_completedTournaments.Clear();
		_completedNewsFlashes.Clear();
		_unlockedTunnels.Clear();
		_viewedLibraryCharacters.Clear();
		_shopPurchaseData.Clear();
		_accessories.Clear();
		_activeAccessories.Clear();
		_daysPassedInCity.Clear();
		_currentDailyMission = null;
		_lastCompletedDailyMission = null;
		DailyMissionManager.instance.ForceGenerateMission();
		_playerStats = new PlayerStats();
		_currentDay = 0;
		_currentTime = 0;
		_currency = GameManager.instance.gameSettings.startingCurrency;
		_rainbowCurrency = GameManager.instance.gameSettings.startingRainbowCurrency;
		List<CharacterDefinition> list = GameManager.instance.gameSettings.startingCharacters.ConvertAll((int input) => GameManager.instance.characterDatabase.GetCharacter(input));
		int count = list.Count;
		for (int num = 0; num < count; num++)
		{
			AddCharacter(list[num]);
			if (!_charactersAcquired.Contains(list[num].uid))
			{
				_charactersAcquired.Add(list[num].uid);
			}
			if (_activeParty.Count < 6)
			{
				_activeParty.Add(_characters[_characters.Count - 1]);
			}
		}
		_pvpData = new PvpData(FriendCodeGenerator.Generate(), string.Empty);
		Save();
		SaveUtility.DeleteAll();
		string value = PlayerPrefs.GetString("M2H_lastLanguage");
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetString("M2H_lastLanguage", value);
		PlayerPrefs.Save();
		if (DataReset != null)
		{
			DataReset();
		}
		Debug.Log("Player's data has been reset");
	}

	protected void OnCharacterAdded(Character character)
	{
		if (character.level >= 5)
		{
			instance.SetTrigger("EnhancementUnlocked");
		}
		if (character.level >= 10)
		{
			instance.SetTrigger("Level10FigAcquired");
		}
		if (CharacterAdded != null)
		{
			CharacterAdded(character);
		}
	}

	protected void OnCharacterRemoved(Character character)
	{
		if (CharacterRemoved != null)
		{
			CharacterRemoved(character);
		}
	}

	protected void OnCharacterCollectionChanged()
	{
		if (CharacterCollectionChanged != null)
		{
			CharacterCollectionChanged();
		}
	}

	protected void OnPartyChanged()
	{
		if (PartyChanged != null)
		{
			PartyChanged();
		}
	}

	protected void OnTeamChanged()
	{
		if (TeamChanged != null)
		{
			TeamChanged();
		}
	}

	protected void OnAccessoriesChanged()
	{
		if (EquippedAccessoriesChanged != null)
		{
			EquippedAccessoriesChanged();
		}
	}

	protected void OnCharacterAcquired(int characterUID)
	{
		if (CharacterAcquired != null)
		{
			CharacterAcquired(characterUID);
		}
	}

	protected void OnCurrencyUpdated()
	{
		if (CurrencyUpdated != null)
		{
			CurrencyUpdated(_currency);
		}
		if (CurrencyUpdatedType != null)
		{
			CurrencyUpdatedType(CurrencyType.Coins, _currency);
		}
	}

	protected void OnRainbowCurrencyUpdated()
	{
		if (RainbowCurrencyUpdated != null)
		{
			RainbowCurrencyUpdated(_rainbowCurrency);
		}
		if (CurrencyUpdatedType != null)
		{
			CurrencyUpdatedType(CurrencyType.RainbowCoins, _rainbowCurrency);
		}
	}

	protected void OnMissionAdded(int missionUID)
	{
		if (MissionAdded != null)
		{
			MissionAdded(missionUID, arg2: false);
		}
	}

	protected void OnDialogueMissionAdded(int missionUID)
	{
		if (MissionAdded != null)
		{
			MissionAdded(missionUID, arg2: true);
		}
	}

	protected void OnMissionUpdated(int missionUID)
	{
		if (MissionUpdated != null)
		{
			MissionUpdated(missionUID);
		}
	}

	protected void OnMissionCompleted(int missionUID)
	{
		if (MissionCompleted != null)
		{
			MissionCompleted(missionUID);
		}
	}

	protected void OnMissionCollected(int missionUID)
	{
		if (MissionCollected != null)
		{
			MissionCollected(missionUID);
		}
	}

	protected void OnMissionViewed(int missionUID)
	{
		if (MissionViewed != null)
		{
			MissionViewed(missionUID);
		}
	}

	protected void OnMissionPinned(int missionUID)
	{
		if (MissionPinned != null)
		{
			MissionPinned(missionUID);
		}
	}

	protected void OnMailAdded(int mailUID)
	{
		if (MailAdded != null)
		{
			MailAdded(mailUID);
		}
	}

	protected void OnMailViewed(int mailUID)
	{
		if (MailViewed != null)
		{
			MailViewed(mailUID);
		}
	}

	protected void OnInventoryChanged()
	{
		if (InventoryChanged != null)
		{
			InventoryChanged();
		}
	}

	protected void OnGameKeyTotalChange()
	{
		if (GameKeyTotalChange != null)
		{
			GameKeyTotalChange();
		}
	}

	protected void OnTriggerSet(string trigger)
	{
		if (TriggerSet != null)
		{
			TriggerSet(trigger);
		}
	}

	protected void OnTriggerUnset(string trigger)
	{
		if (TriggerUnset != null)
		{
			TriggerUnset(trigger);
		}
	}

	public static bool LoadData()
	{
		return instance.Load();
	}
}
