using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerTestData : ScriptableObject
{
	public string playerName = "Default";

	public int currency = 500;

	public int rainbowCurrency;

	public List<InventoryItemData> inventory;

	public List<CharacterData> characters;

	public List<int> activeParty;

	public List<int> activeTeam;

	public List<int> charactersAcquired;

	public List<string> unlockedTunnels;

	public List<int> viewedLibraryCharacters;

	public List<MissionData> activeMissions;

	public List<MissionCompletedData> completedMissions;

	public List<string> triggers;

	public List<int> accessoriesAcquired;

	public List<int> activeAccessories;

	public PlayerStats playerStats;

	public PvpData pvpData;

	public GameData gameData;

	[SerializeField]
	private int _nextPlayerCharacterUID;

	public int nextPlayerCharacterUID
	{
		get
		{
			_nextPlayerCharacterUID++;
			return _nextPlayerCharacterUID - 1;
		}
	}

	public CharacterData GetCharacterDataByUid(int uid)
	{
		return characters.Find((CharacterData obj) => obj.uid == uid);
	}
}
