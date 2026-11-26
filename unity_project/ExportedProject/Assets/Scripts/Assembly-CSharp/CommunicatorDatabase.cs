using System;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatorDatabase : ScriptableObject
{
	[NonSerialized]
	private List<Mission> _dailyMissions = new List<Mission>();

	[SerializeField]
	private List<Mission> _missions = new List<Mission>();

	[SerializeField]
	private List<MailMessage> _mail = new List<MailMessage>();

	[SerializeField]
	private List<NewsFlash> _newsFlashes = new List<NewsFlash>();

	[SerializeField]
	private int _nextMissionId = -1;

	[SerializeField]
	private int _nextMailId = -1;

	[SerializeField]
	private int _nextNewsFlashId = -1;

	public List<Mission> missions
	{
		get
		{
			return _missions;
		}
	}

	public List<MailMessage> mail
	{
		get
		{
			return _mail;
		}
	}

	public List<NewsFlash> newsFlashes
	{
		get
		{
			return _newsFlashes;
		}
	}

	private int nextMissionId
	{
		get
		{
			_nextMissionId++;
			return _nextMissionId;
		}
	}

	private int nextMailId
	{
		get
		{
			_nextMailId++;
			return _nextMailId;
		}
	}

	private int nextNewsFlashId
	{
		get
		{
			_nextNewsFlashId++;
			return _nextNewsFlashId;
		}
	}

	public void AddDailyMission(DailyMissionData missionData)
	{
		if (missionData == null)
		{
			return;
		}
		Debug.Log("Adding Daily Mission to Communicator Database...");
		RemoveDailyMission(missionData.uid);
		Mission mission = ScriptableObject.CreateInstance<Mission>();
		mission.uid = missionData.uid;
		mission.type = MissionType.Side;
		mission.isDaily = true;
		mission.autoCollect = false;
		mission.title = "Daily Mission";
		mission.description = missionData.description;
		mission.descriptionOverrides = true;
		mission.targetMsg = string.Empty;
		mission.coinReward = missionData.coinReward;
		mission.modChipReward = missionData.modChipReward;
		mission.characterRewards = new List<CharacterRewardData>(missionData.characterRewards);
		mission.missionHintType = MissionHintType.None;
		if (missionData.type == DailyMissionType.DefeatNPC || missionData.type == DailyMissionType.DefeatNPCType)
		{
			mission.sideType = MissionSideType.Battle;
			mission.battleConfigs = new MissionTypeConfigBattle[1] { missionData.battleConfig };
			mission.battleConditions = ((missionData.battleCondition == null) ? new BattleCondition[0] : new BattleCondition[1] { missionData.battleCondition });
			mission.buyConfigs = new MissionTypeConfigBuy[0];
			if (missionData.type == DailyMissionType.DefeatNPC)
			{
				mission.useFigureOverrides = true;
				mission.figureOverrides = missionData.figureOverrides;
			}
		}
		else if (missionData.type == DailyMissionType.Shopping)
		{
			mission.sideType = MissionSideType.Buy;
			mission.buyConfigs = new MissionTypeConfigBuy[1] { missionData.buyConfig };
			mission.battleConfigs = new MissionTypeConfigBattle[0];
			mission.battleConditions = new BattleCondition[0];
			mission.deliveryNPC = missionData.missionLocationNPC;
		}
		_dailyMissions.Add(mission);
	}

	public void RemoveDailyMission(int uid)
	{
		for (int i = 0; i < _dailyMissions.Count; i++)
		{
			if (_dailyMissions[i].uid == uid)
			{
				_dailyMissions.RemoveAt(i);
				break;
			}
		}
	}

	public bool HasMission(int uid)
	{
		int count = _missions.Count;
		for (int i = 0; i < count; i++)
		{
			if (_missions[i].uid == uid)
			{
				return true;
			}
		}
		for (int j = 0; j < _dailyMissions.Count; j++)
		{
			if (_dailyMissions[j].uid == uid)
			{
				return true;
			}
		}
		return false;
	}

	public Mission GetMission(int uid)
	{
		int count = _missions.Count;
		for (int i = 0; i < count; i++)
		{
			if (_missions[i].uid == uid)
			{
				return _missions[i];
			}
		}
		for (int j = 0; j < _dailyMissions.Count; j++)
		{
			if (_dailyMissions[j].uid == uid)
			{
				return _dailyMissions[j];
			}
		}
		return null;
	}

	public List<Mission> GetAllMissions(MissionType type)
	{
		List<Mission> list = new List<Mission>();
		for (int i = 0; i < _missions.Count; i++)
		{
			if (_missions[i].type == type)
			{
				list.Add(_missions[i]);
			}
		}
		for (int j = 0; j < _dailyMissions.Count; j++)
		{
			if (_dailyMissions[j].type == type)
			{
				list.Add(_dailyMissions[j]);
			}
		}
		return list;
	}

	public List<Mission> GetAllSideMissions(bool includeDailyMissions)
	{
		List<Mission> list = new List<Mission>();
		for (int i = 0; i < _missions.Count; i++)
		{
			Mission mission = _missions[i];
			if (mission.type == MissionType.Side)
			{
				list.Add(mission);
			}
		}
		if (includeDailyMissions)
		{
			for (int j = 0; j < _dailyMissions.Count; j++)
			{
				if (_dailyMissions[j].type == MissionType.Side)
				{
					list.Add(_dailyMissions[j]);
				}
			}
		}
		return list;
	}

	public List<Mission> GetAllMissionsOwnedByNPC(CharacterName npcName)
	{
		List<Mission> list = new List<Mission>();
		for (int i = 0; i < _missions.Count; i++)
		{
			Mission mission = _missions[i];
			if (mission.npcOwner == npcName)
			{
				list.Add(mission);
			}
		}
		return list;
	}

	public int GetMissionCountOwnedByNPC(CharacterName npcName)
	{
		int num = 0;
		for (int i = 0; i < _missions.Count; i++)
		{
			if (_missions[i].npcOwner == npcName)
			{
				num++;
			}
		}
		return num;
	}

	public MailMessage GetMailMessage(int uid)
	{
		int count = _mail.Count;
		for (int i = 0; i < count; i++)
		{
			if (_mail[i].uid == uid)
			{
				return _mail[i];
			}
		}
		return null;
	}

	public NewsFlash GetNewsFlash(int uid)
	{
		int count = _newsFlashes.Count;
		for (int i = 0; i < count; i++)
		{
			if (_newsFlashes[i].uid == uid)
			{
				return _newsFlashes[i];
			}
		}
		return null;
	}
}
