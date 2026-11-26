using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class CommunicatorManager : MonoBehaviour
{
	private static bool didApplicationQuit;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> AnimationEvent__BackingField;

	private static CommunicatorManager _instance;

	public static CommunicatorManager instance
	{
		get
		{
			if (_instance == null && !didApplicationQuit)
			{
				_instance = new GameObject("CommunicatorManager").AddComponent<CommunicatorManager>();
			}
			return _instance;
		}
	}

	public event Action<string> AnimationEvent
	{
		add
		{
			Action<string> action = AnimationEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AnimationEvent__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = AnimationEvent__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AnimationEvent__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Player.instance.MissionCompleted += MissionCompleted;
		Player.instance.DailyMissionCompleted += HandleDailyMissionCompleted;
		EventMessenger.Instance.AddListener<AccessoryActivatedEvent>(HandleAccessoryActivatedEvent);
		EventMessenger.Instance.AddListener<TofuUsedEvent>(HandleTofuUsedEvent);
		EventMessenger.Instance.AddListener<BattlePickupCollectedEvent>(HandleBattlePickupCollectedEvent);
		EventMessenger.Instance.AddListener<BattleEndedEvent>(HandleBattleEndedEvent);
		EventMessenger.Instance.AddListener<ViewedCharacterInLibraryEvent>(HandleViewedCharacterInLibraryEvent);
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.MissionCompleted -= MissionCompleted;
			Player.instance.DailyMissionCompleted -= HandleDailyMissionCompleted;
		}
		EventMessenger.Instance.RemoveListener<AccessoryActivatedEvent>(HandleAccessoryActivatedEvent);
		EventMessenger.Instance.RemoveListener<TofuUsedEvent>(HandleTofuUsedEvent);
		EventMessenger.Instance.RemoveListener<BattlePickupCollectedEvent>(HandleBattlePickupCollectedEvent);
		EventMessenger.Instance.RemoveListener<BattleEndedEvent>(HandleBattleEndedEvent);
		EventMessenger.Instance.RemoveListener<ViewedCharacterInLibraryEvent>(HandleViewedCharacterInLibraryEvent);
	}

	private void OnApplicationQuit()
	{
		didApplicationQuit = true;
	}

	public void FireAnimationEvent(string eventName)
	{
		if (AnimationEvent__BackingField != null)
		{
			AnimationEvent__BackingField(eventName);
		}
	}

	public void CompleteMission(int missionUid, bool allowSave = true)
	{
		CompleteMission(Player.instance.GetMissionData(missionUid), allowSave);
	}

	public void CompleteMission(MissionData missionData, bool allowSave = true)
	{
		if (missionData != null && missionData.active && !missionData.completed)
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			missionData.completed = true;
			if (mission.autoCollect)
			{
				AutoCollect(missionData.uid);
			}
			if (allowSave)
			{
				Player.instance.Save();
			}
		}
	}

	public void UpdateMission(MissionData missionData, int currentAmtIncrement = 1)
	{
		bool flag = false;
		if (missionData.active && !missionData.completed)
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			int amountNeeded = mission.amountNeeded;
			bool flag2 = true;
			if (flag2)
			{
				missionData.currentAmount += currentAmtIncrement;
			}
			if (missionData.currentAmount >= amountNeeded && flag2)
			{
				missionData.completed = true;
				if (mission.autoCollect)
				{
					AutoCollect(missionData.uid);
				}
			}
			flag = true;
			Player.instance.UpdateMission(missionData.uid);
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	public void UpdateMissionWithTargetMessage(string message, int currentAmtIncrement = 1)
	{
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (!missionData.active || missionData.completed)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			bool flag2 = false;
			if (mission != null)
			{
				if (mission.sideType == MissionSideType.Tournament && mission.tournamentDef != null)
				{
					if (mission.tournamentDef.tournamentName.Equals(message))
					{
						flag2 = true;
					}
				}
				else if (!string.IsNullOrEmpty(mission.targetMsg) && mission.targetMsg.Equals(message))
				{
					flag2 = true;
				}
			}
			else
			{
				UnityEngine.Debug.LogErrorFormat("Mission with UID {0} was not found. It's possible you have an active mission in your save data that is no longer in the mission database. Non-fatal error.", missionData.uid);
			}
			if (!flag2)
			{
				continue;
			}
			int amountNeeded = mission.amountNeeded;
			bool flag3 = true;
			if (flag3)
			{
				missionData.currentAmount += currentAmtIncrement;
			}
			if (missionData.currentAmount >= amountNeeded && flag3)
			{
				missionData.completed = true;
				if (mission.autoCollect)
				{
					AutoCollect(missionData.uid);
				}
			}
			flag = true;
			Player.instance.UpdateMission(missionData.uid);
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private bool CanUseCharacterInBattle(Character character, List<NPCBattleCondition> battleConditions)
	{
		bool result = character.characterDefinition.canUseInBattle && !character.isDead;
		if (battleConditions != null && battleConditions != null && battleConditions.Count > 0)
		{
			bool flag = false;
			for (int i = 0; i < battleConditions.Count; i++)
			{
				NPCBattleCondition nPCBattleCondition = battleConditions[i];
				if (nPCBattleCondition.type == NPCBattleConditionType.Character)
				{
					CharacterDefinition character2 = GameManager.instance.characterDatabase.GetCharacter(nPCBattleCondition.uid);
					if (character.characterDefinition.characterName.ToLower().Contains(character2.characterName.ToLower()))
					{
						flag = true;
					}
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.MinLevel && character.level >= nPCBattleCondition.amount)
				{
					flag = true;
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.GoldAbility && character.shinyRank >= nPCBattleCondition.amount)
				{
					flag = true;
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.Is80sFigure && character.characterDefinition.is80sFigure)
				{
					flag = true;
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.IsVillainFigure && character.characterDefinition.isVillainFigure)
				{
					flag = true;
				}
			}
			return flag && !character.isDead;
		}
		return result;
	}

	private void AutoCollect(int missionUID)
	{
		if (Player.instance.GetMissionData(missionUID) != null)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_communicator_missions_collect", GFAudioManager.SFX_GROUP, string.Empty);
			StartCoroutine(CollectReward(missionUID));
		}
	}

	public IEnumerator CollectReward(int missionUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (mission.completionConvo != null && CityController.instance != null)
		{
			yield return CityController.instance.PlayConvo((Conversation)mission.completionConvo);
		}
		if (CityReward.instance != null)
		{
			CityReward.instance.ShowRewardForMission(missionUID);
		}
		else
		{
			CityRewardReveal.Show(missionUID);
			yield return new WaitWhile(() => CityRewardReveal.Instance != null);
			if (mission.riddleEggReward != null && mission.riddleEggReward.amount > 0)
			{
				List<PlayerReward> eggRewards = new List<PlayerReward>();
				for (int num = 0; num < mission.riddleEggReward.amount; num++)
				{
					eggRewards.Add(GameManager.instance.iapConfig.GenerateRiddleEggReward(mission.riddleEggReward.riddleEggType));
				}
				RiddleEggReveal.Show(eggRewards);
				yield return new WaitWhile(() => RiddleEggReveal.Instance != null);
			}
		}
		Player.instance.CollectMission(missionUID);
		yield return new WaitForEndOfFrame();
	}

	public void MissionCompleted(int missionUID)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionUID);
		if (!(mission != null))
		{
			return;
		}
		if (!string.IsNullOrEmpty(mission.completionAnimation))
		{
			StartCoroutine(StartAnimationEvent(mission.completionAnimation));
		}
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null || missionData.completed)
			{
				continue;
			}
			Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission2 != null) || mission2.sideType != MissionSideType.MissionChecklist || !mission2.collectUIDS.Contains(missionUID))
			{
				continue;
			}
			bool flag2 = false;
			if (!missionData.collectedUIDs.Contains(missionUID))
			{
				missionData.currentAmount++;
				missionData.collectedUIDs.Add(missionUID);
				flag = (flag2 = true);
			}
			if (!flag2)
			{
				continue;
			}
			if (missionData.currentAmount >= mission2.amountNeeded)
			{
				missionData.completed = true;
				if (mission2.autoCollect)
				{
					AutoCollect(missionData.uid);
				}
			}
			Player.instance.UpdateMission(missionData.uid);
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private void HandleDailyMissionCompleted(int missionUid)
	{
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null || missionData.completed)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.DailyMissionCount)
			{
				continue;
			}
			flag = true;
			missionData.currentAmount++;
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				missionData.completed = true;
				if (mission.autoCollect)
				{
					AutoCollect(missionData.uid);
				}
			}
			Player.instance.UpdateMission(missionData.uid);
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	public void CheckForBattleMissionComplete(Mission mission, bool allowSave = true)
	{
		if (mission.sideType != MissionSideType.Battle)
		{
			UnityEngine.Debug.Log("Mission is not a Battle mission, so ignoring it");
			return;
		}
		MissionData missionData = Player.instance.GetMissionData(mission.uid);
		if (missionData == null || missionData.completed)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < mission.battleConfigs.Length; i++)
		{
			if (missionData.configBattleCounts[i] < mission.battleConfigs[i].count)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			Player.instance.CompleteMission(missionData.uid);
			if (mission.autoCollect)
			{
				AutoCollect(missionData.uid);
			}
			Player.instance.UpdateMission(missionData.uid);
			if (allowSave)
			{
				Player.instance.Save();
			}
		}
	}

	private void HandleAccessoryActivatedEvent(AccessoryActivatedEvent evt)
	{
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Battle)
			{
				continue;
			}
			bool flag2 = false;
			for (int j = 0; j < mission.battleConfigs.Length; j++)
			{
				if (mission.battleConfigs[j].targetType == MissionTypeConfigBattle.TargetType.UseAccessory && missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
				{
					missionData.configBattleCounts[j]++;
					flag = (flag2 = true);
				}
			}
			if (flag2)
			{
				CheckForBattleMissionComplete(mission, false);
			}
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private void HandleTofuUsedEvent(TofuUsedEvent evt)
	{
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Battle)
			{
				continue;
			}
			bool flag2 = false;
			for (int j = 0; j < mission.battleConfigs.Length; j++)
			{
				if (mission.battleConfigs[j].targetType == MissionTypeConfigBattle.TargetType.UseTofu && missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
				{
					missionData.configBattleCounts[j]++;
					flag = (flag2 = true);
				}
			}
			if (flag2)
			{
				Player.instance.UpdateMission(mission.uid);
				CheckForBattleMissionComplete(mission, false);
			}
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private void HandleBattlePickupCollectedEvent(BattlePickupCollectedEvent evt)
	{
		Type type = evt.pickup.GetType();
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Battle)
			{
				continue;
			}
			bool flag2 = false;
			for (int j = 0; j < mission.battleConfigs.Length; j++)
			{
				if (mission.battleConfigs[j].targetType == MissionTypeConfigBattle.TargetType.CollectBatteries && type == typeof(BatteryPickup) && missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
				{
					missionData.configBattleCounts[j]++;
					flag = (flag2 = true);
				}
			}
			if (flag2)
			{
				Player.instance.UpdateMission(mission.uid);
				CheckForBattleMissionComplete(mission, false);
			}
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private void HandleBattleEndedEvent(BattleEndedEvent evt)
	{
		if (evt.battleResult == null || !evt.battleResult.isVictory)
		{
			return;
		}
		NPCDefinition nPCDef = GameManager.instance.npcDatabase.GetNPCDef(evt.battleDefinition.enemyGUID);
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Battle || (mission.missionLocationCity != CityName.Any && mission.missionLocationCity != evt.battleDefinition.cityName) || !PlayerHasMetBattleConditions(mission.battleConditions, evt.battleDefinition, true))
			{
				continue;
			}
			bool flag2 = false;
			for (int j = 0; j < mission.battleConfigs.Length; j++)
			{
				switch (mission.battleConfigs[j].targetType)
				{
				case MissionTypeConfigBattle.TargetType.DefeatNPC:
					if (mission.battleConfigs[j].npcGuid.Equals(nPCDef.guid) && missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
					{
						missionData.configBattleCounts[j]++;
						flag = (flag2 = true);
					}
					break;
				case MissionTypeConfigBattle.TargetType.DefeatNPCTag:
					if (nPCDef.tags.Contains(mission.battleConfigs[j].npcTag) && missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
					{
						missionData.configBattleCounts[j]++;
						flag = (flag2 = true);
					}
					break;
				case MissionTypeConfigBattle.TargetType.BeVictorious:
					if (missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
					{
						missionData.configBattleCounts[j]++;
						flag = (flag2 = true);
					}
					break;
				case MissionTypeConfigBattle.TargetType.WinPVPBattle:
					if ((evt.battleDefinition.type == BattleType.Pvp || evt.battleDefinition.type == BattleType.PvpCpu || evt.battleDefinition.type == BattleType.PvpFriend) && missionData.configBattleCounts[j] < mission.battleConfigs[j].count)
					{
						missionData.configBattleCounts[j]++;
						flag = (flag2 = true);
					}
					break;
				}
			}
			if (flag2)
			{
				Player.instance.UpdateMission(mission.uid);
				CheckForBattleMissionComplete(mission, false);
			}
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private void HandleViewedCharacterInLibraryEvent(ViewedCharacterInLibraryEvent evt)
	{
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.ViewFigureWiki)
			{
				continue;
			}
			bool flag2 = false;
			if (missionData.currentAmount < mission.amountNeeded && !missionData.collectedUIDs.Contains(evt.uid) && Player.instance.HasAcquiredCharacter(evt.uid))
			{
				missionData.currentAmount++;
				missionData.collectedUIDs.Add(evt.uid);
				flag = (flag2 = true);
			}
			if (!flag2)
			{
				continue;
			}
			if (missionData.currentAmount >= mission.amountNeeded)
			{
				missionData.completed = true;
				if (mission.autoCollect)
				{
					AutoCollect(missionData.uid);
				}
			}
			Player.instance.UpdateMission(missionData.uid);
		}
		if (flag)
		{
			Player.instance.Save();
		}
	}

	private bool PlayerHasMetBattleConditions(BattleCondition[] conditions, BattleDefinition battleDefinition, bool checkTeamOnly = false)
	{
		List<Character> list = ((!checkTeamOnly) ? Player.instance.activeParty : Player.instance.activeTeam);
		foreach (BattleCondition battleCondition in conditions)
		{
			if (battleCondition == null)
			{
				continue;
			}
			int num = 0;
			switch (battleCondition.conditionType)
			{
			case BattleCondition.ConditionType.FigureName:
			{
				for (int num2 = 0; num2 < list.Count; num2++)
				{
					if (list[num2].characterDefinition.uid == battleCondition.intValue2)
					{
						num++;
					}
				}
				if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			}
			case BattleCondition.ConditionType.FigureTags:
			{
				for (int m = 0; m < list.Count; m++)
				{
					if (list[m].characterDefinition.tags.Contains(battleCondition.stringValue1))
					{
						num++;
					}
				}
				if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			}
			case BattleCondition.ConditionType.FigureLevel:
			{
				for (int k = 0; k < list.Count; k++)
				{
					if (ComparisonUtil.IsComparisonValid(list[k].level, battleCondition.comparison2, battleCondition.intValue2))
					{
						num++;
					}
				}
				if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			}
			case BattleCondition.ConditionType.FigureShinyRank:
			{
				for (int n = 0; n < list.Count; n++)
				{
					if (ComparisonUtil.IsComparisonValid(list[n].shinyRank, battleCondition.comparison2, battleCondition.intValue2))
					{
						num++;
					}
				}
				if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			}
			case BattleCondition.ConditionType.Accessory:
				if (battleDefinition == null)
				{
					break;
				}
				if (battleCondition.isOrNot1 == IsOrNot.Is)
				{
					if (battleDefinition.accessoryUID != battleCondition.intValue1)
					{
						return false;
					}
				}
				else if (battleDefinition.accessoryUID == battleCondition.intValue1)
				{
					return false;
				}
				break;
			case BattleCondition.ConditionType.TeamSize:
				if (!ComparisonUtil.IsComparisonValid(Player.instance.activeTeam.Count, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			case BattleCondition.ConditionType.FigureClass:
			{
				for (int l = 0; l < list.Count; l++)
				{
					if (list[l].characterDefinition.characterClass == (CharacterClass)battleCondition.intValue2)
					{
						num++;
					}
				}
				if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			}
			case BattleCondition.ConditionType.FigureRepainted:
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].repaintColorID > 0)
					{
						num++;
					}
				}
				if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
				{
					return false;
				}
				break;
			}
			}
		}
		return true;
	}

	private IEnumerator StartAnimationEvent(string eventName)
	{
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		FireAnimationEvent(eventName);
		yield return new WaitForEndOfFrame();
	}
}
