using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CityNPCData
{
	public string guid = string.Empty;

	public int activeOnCompletedMissionUID = -1;

	public int activeOnActiveMissionUID = -1;

	public int hidesOnCompletedMissionUID = -1;

	public int daysAfterMission;

	public int daysAfterTournament;

	public int daysAfterMissionPlayAlternateConvo;

	public bool triggersEnteringCityScene;

	public bool requiresPreviousConvoPlayed;

	public bool requiresMissionItem;

	public bool requiresBattleCondition;

	public bool battleConditionsExtendIntoBattle;

	public bool alternateConvoAfterDays;

	public string requiredMissionItemName;

	public bool forceDuringMission;

	public bool poofsIn;

	public bool poofsOut;

	public bool movesAfterConversation;

	public int coinReward = 50;

	public int xpReward = 50;

	public GameObject disablesGameObject;

	public GameObject moveToPosition;

	public List<TournamentRoundFigureData> figures = new List<TournamentRoundFigureData>();

	public int teamSizeWeight1 = 50;

	public int teamSizeWeight2 = 50;

	public int teamSizeWeight3 = 50;

	public int equippedAccessoryUID = -1;

	public Conversation requiredConversation;

	public Conversation alternateConversation;

	public Conversation initialConversation;

	public Conversation activeConversation;

	public Conversation missionCompleteConversation;

	public List<Conversation> firstMessage = new List<Conversation>();

	public List<Conversation> battleComplete = new List<Conversation>();

	public List<Conversation> preBattleConversations = new List<Conversation>();

	public List<NPCBattleCondition> battleConditions = new List<NPCBattleCondition>();

	public TournamentDefinition tournamentDef;

	public TournamentDefinition hidesAfterTournamentDef;

	public bool challengesOn;

	public List<int> challengesIncluded;
}
