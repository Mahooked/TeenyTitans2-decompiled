using System.Collections.Generic;
using UnityEngine;

public class MissionDebugManager : MonoBehaviour
{
	public static MissionDebugManager instance;

	public int activeStoryMission = -1;

	public List<int> activeSideMissions = new List<int>();

	public List<int> completedMissions = new List<int>();

	public List<TournamentDefinition> completedTournaments = new List<TournamentDefinition>();

	public bool debugOn;

	public static bool DebugOn
	{
		get
		{
			if (instance == null)
			{
				return false;
			}
			if (instance.debugOn)
			{
				return true;
			}
			return false;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public static bool HasMissionCompleted(int missionUID)
	{
		if (instance != null && instance.completedMissions.Contains(missionUID))
		{
			return true;
		}
		return false;
	}

	public static MissionCompletedData GetMissionCompletedData(int missionUID)
	{
		if (instance != null)
		{
			for (int i = 0; i < instance.completedMissions.Count; i++)
			{
				if (instance.completedMissions[i] == missionUID)
				{
					MissionCompletedData missionCompletedData = new MissionCompletedData();
					missionCompletedData.uid = missionUID;
					return missionCompletedData;
				}
			}
		}
		return null;
	}

	public static void AddMission(int missionUID)
	{
		if (instance != null)
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
			if (mission.type == MissionType.Story)
			{
				instance.activeStoryMission = missionUID;
			}
			if (mission.type == MissionType.Side && !instance.activeSideMissions.Contains(missionUID))
			{
				instance.activeSideMissions.Add(missionUID);
			}
		}
	}

	public static void CompleteMission(int missionUID)
	{
		if (!(instance != null))
		{
			return;
		}
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (mission.type == MissionType.Story)
		{
			if (instance.activeStoryMission == missionUID)
			{
				instance.activeStoryMission = -1;
			}
			if (!instance.completedMissions.Contains(missionUID))
			{
				instance.completedMissions.Add(missionUID);
			}
		}
		if (mission.type == MissionType.Side)
		{
			instance.activeSideMissions.Remove(missionUID);
			if (!instance.completedMissions.Contains(missionUID))
			{
				instance.completedMissions.Add(missionUID);
			}
		}
	}

	public static List<MissionData> ActiveMissions()
	{
		List<MissionData> list = new List<MissionData>();
		if (instance != null)
		{
			if (instance.activeStoryMission > -1)
			{
				MissionData missionData = new MissionData();
				missionData.uid = instance.activeStoryMission;
				missionData.active = true;
				list.Add(missionData);
			}
			for (int i = 0; i < instance.activeSideMissions.Count; i++)
			{
				MissionData missionData2 = new MissionData();
				missionData2.uid = instance.activeSideMissions[i];
				missionData2.active = true;
				list.Add(missionData2);
			}
		}
		return list;
	}

	public static List<MissionCompletedData> CompletedMissions()
	{
		List<MissionCompletedData> list = new List<MissionCompletedData>();
		if (instance != null)
		{
			for (int i = 0; i < instance.completedMissions.Count; i++)
			{
				MissionCompletedData missionCompletedData = new MissionCompletedData();
				missionCompletedData.uid = instance.completedMissions[i];
				list.Add(missionCompletedData);
			}
		}
		return list;
	}

	public static TournamentCompletedData GetTournamentCompletedData(TournamentDefinition tournamentDef)
	{
		if (instance != null)
		{
			for (int i = 0; i < instance.completedTournaments.Count; i++)
			{
				if (instance.completedTournaments[i].tournamentName.Equals(tournamentDef.tournamentName))
				{
					TournamentCompletedData tournamentCompletedData = new TournamentCompletedData();
					tournamentCompletedData.tournamentName = tournamentDef.tournamentName;
					return tournamentCompletedData;
				}
			}
		}
		return null;
	}
}
