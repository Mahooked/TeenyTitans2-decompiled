using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class PlayerData
{
	public string name;

	public int nextPlayerCharacterUID;

	public List<CharacterData> characters;

	public List<int> activeParty;

	public List<int> activeTeam;

	public List<int> charactersAcquired;

	public List<string> defeatedNPCs;

	public List<string> triggers;

	public List<int> accessoriesAcquired;

	[OptionalField]
	public List<int> repaintSlotsAcquired;

	public List<int> activeAccessories;

	public int currentDay;

	public int currentTime;

	[OptionalField]
	public List<KeyValuePair<CityName, int>> daysPassedInCity;

	public List<InventoryItemData> inventory;

	public List<MailData> activeMail;

	public List<MissionData> activeMissions;

	public List<NewsFlashData> activeNewsFlashes;

	[OptionalField]
	public List<MissionCompletedData> completedMissions;

	[OptionalField]
	public List<TournamentCompletedData> completedTournaments;

	public List<int> completedNewsFlashes;

	public List<string> unlockedTunnels;

	public List<int> viewedLibraryCharacters;

	public List<ShopPurchaseData> shopPurchaseData;

	[OptionalField]
	public DailyMissionData currentDailyMission;

	[OptionalField]
	public DailyMissionData lastCompletedDailyMission;

	[OptionalField]
	public List<DailyMissionData> dailyMissions;

	[OptionalField]
	public List<DailyMissionData> completedDailyMissions;

	[OptionalField]
	public List<DailyMissionData> weeklyCompletedDailyMissions;

	[OptionalField]
	public DailyMissionData currentlyAvailableDailyMission;

	[OptionalField]
	public List<PlayerReward> purchasedRewardsQueue;

	[OptionalField]
	public List<FreeRepaintSlot> freeRepaintSlots;

	[OptionalField]
	public PlayerStats playerStats;

	public int currency;

	public int rainbowCurrency;

	[OptionalField]
	public PvpData pvpData;

	public PlayerData()
	{
		characters = new List<CharacterData>();
		activeParty = new List<int>();
		activeTeam = new List<int>();
		charactersAcquired = new List<int>();
		defeatedNPCs = new List<string>();
		triggers = new List<string>();
		inventory = new List<InventoryItemData>();
		activeMissions = new List<MissionData>();
		activeNewsFlashes = new List<NewsFlashData>();
		activeMail = new List<MailData>();
		completedMissions = new List<MissionCompletedData>();
		completedTournaments = new List<TournamentCompletedData>();
		completedNewsFlashes = new List<int>();
		unlockedTunnels = new List<string>();
		viewedLibraryCharacters = new List<int>();
		shopPurchaseData = new List<ShopPurchaseData>();
		currentDailyMission = null;
		lastCompletedDailyMission = null;
		dailyMissions = new List<DailyMissionData>();
		completedDailyMissions = new List<DailyMissionData>();
		weeklyCompletedDailyMissions = new List<DailyMissionData>();
		currentlyAvailableDailyMission = null;
		purchasedRewardsQueue = new List<PlayerReward>();
		accessoriesAcquired = new List<int>();
		repaintSlotsAcquired = new List<int>();
		freeRepaintSlots = new List<FreeRepaintSlot>();
		playerStats = new PlayerStats();
		daysPassedInCity = new List<KeyValuePair<CityName, int>>();
		currentDay = 0;
		currentTime = 0;
		currency = 0;
		rainbowCurrency = 0;
		pvpData = new PvpData();
	}

	[OnDeserializing]
	private void HandleSerializationVersioning(StreamingContext context)
	{
		completedMissions = new List<MissionCompletedData>();
		completedTournaments = new List<TournamentCompletedData>();
		dailyMissions = new List<DailyMissionData>();
		completedDailyMissions = new List<DailyMissionData>();
		weeklyCompletedDailyMissions = new List<DailyMissionData>();
		purchasedRewardsQueue = new List<PlayerReward>();
		repaintSlotsAcquired = new List<int>();
		freeRepaintSlots = new List<FreeRepaintSlot>();
		playerStats = new PlayerStats();
	}
}
