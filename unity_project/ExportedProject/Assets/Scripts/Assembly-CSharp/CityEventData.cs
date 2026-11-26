using System;
using UnityEngine;

[Serializable]
public class CityEventData
{
	public string guid = string.Empty;

	public int activeOnCompletedMissionUID = -1;

	public int hidesOnCompletedMissionUID = -1;

	public int completesMissionUID = -1;

	public Conversation initialConversation;

	public Conversation activeConversation;

	public Conversation missionCompleteConversation;

	public TournamentDefinition activatesTournament;

	public bool hidesChildrenWhenInvalid;

	public bool disablesGameObject;

	public GameObject disabledGameObject;

	public string activatesScene = string.Empty;
}
