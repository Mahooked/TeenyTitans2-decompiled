using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroupManager : MonoBehaviour
{
	private List<GameObject> spawnGroups = new List<GameObject>();

	private List<SpawnPoint> spawnPointsToConsider = new List<SpawnPoint>();

	private bool isDayTime;

	public CityLocation district;

	public Transform npcHolder;

	private bool isNightGroup;

	public bool debugOn;

	public GameObject debugGroup;

	private SpawnGroupMaster spawnGroupMaster;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		if (CityController.instance != null)
		{
			spawnGroupMaster = CityController.instance.spawnGroupMaster;
			CityController.instance.communicator.StartDayTime += ChangeTimeOfDay;
			CityController.instance.communicator.StartNightTime += ChangeTimeOfDay;
			isDayTime = CityController.instance.communicator.isDayTime;
		}
		Player.instance.MissionAdded += HandleMissionAdded;
		Player.instance.MissionCompleted += HandleMissionCompleted;
		Player.instance.MissionCollected += HandleMissionCollected;
		if (base.gameObject.name.Contains("NIGHT"))
		{
			isNightGroup = true;
		}
		ChooseSpawnGroup();
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.MissionAdded -= HandleMissionAdded;
			Player.instance.MissionCompleted -= HandleMissionCompleted;
			Player.instance.MissionCollected -= HandleMissionCollected;
		}
		if (CityController.instance != null)
		{
			CityController.instance.communicator.StartDayTime -= ChangeTimeOfDay;
			CityController.instance.communicator.StartNightTime -= ChangeTimeOfDay;
		}
	}

	private void HandleMissionAdded(int missionUID, bool dialogueMission)
	{
		ChooseSpawnGroup();
	}

	private void HandleMissionCompleted(int missionUID)
	{
		ChooseSpawnGroup();
	}

	private void HandleMissionCollected(int missionUID)
	{
		ChooseSpawnGroup();
	}

	private void ChangeTimeOfDay()
	{
		isDayTime = CityController.instance.communicator.isDayTime;
		if (isDayTime)
		{
			SaveUtility.SetString(base.gameObject.name + "_previous", SaveUtility.GetString(base.gameObject.name), false);
			SaveUtility.SetString(base.gameObject.name, string.Empty, false);
		}
		ChooseSpawnGroup();
	}

	private void ChooseSpawnGroup()
	{
		bool flag = true;
		string value = SaveUtility.GetString(base.gameObject.name);
		if (!string.IsNullOrEmpty(value))
		{
			flag = false;
		}
		GameObject gameObject = null;
		spawnGroups.Clear();
		foreach (Transform item in base.transform)
		{
			if (item.gameObject.name.Contains("Mission"))
			{
				item.gameObject.SetActive(true);
				gameObject = item.gameObject;
			}
			else
			{
				spawnGroups.Add(item.gameObject);
				item.gameObject.SetActive(false);
			}
		}
		if ((isNightGroup && isDayTime) || (!isNightGroup && !isDayTime))
		{
			ClearPreviouslyCreatedSpawns();
			{
				foreach (Transform item2 in base.transform)
				{
					item2.gameObject.SetActive(false);
				}
				return;
			}
		}
		int num = -1;
		bool flag2 = false;
		if (num > -1)
		{
			MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(num);
			if (missionCompletedData != null)
			{
				flag2 = true;
			}
		}
		if (!flag2)
		{
			ClearPreviouslyCreatedSpawns();
			return;
		}
		spawnPointsToConsider.Clear();
		if (spawnGroups.Count > 0)
		{
			if (flag)
			{
				int index = Random.Range(0, spawnGroups.Count);
				GameObject gameObject2 = spawnGroups[index];
				string text = SaveUtility.GetString(base.gameObject.name + "_previous");
				string text2 = gameObject2.name;
				while (text == text2 && spawnGroups.Count > 1 && !string.IsNullOrEmpty(text))
				{
					index = Random.Range(0, spawnGroups.Count);
					gameObject2 = spawnGroups[index];
					text2 = gameObject2.name;
				}
				if (debugOn && debugGroup != null)
				{
					gameObject2 = debugGroup;
				}
				foreach (Transform item3 in gameObject2.transform)
				{
					SpawnPoint component = item3.gameObject.GetComponent<SpawnPoint>();
					if (component != null)
					{
						spawnPointsToConsider.Add(component);
					}
				}
				SaveUtility.SetString(base.gameObject.name + "_previous", gameObject2.name, false);
				SaveUtility.SetString(base.gameObject.name, gameObject2.name, false);
				gameObject2.SetActive(true);
			}
			else
			{
				bool flag3 = false;
				for (int i = 0; i < spawnGroups.Count; i++)
				{
					GameObject gameObject3 = spawnGroups[i];
					if (!gameObject3.name.Equals(value) || flag3)
					{
						continue;
					}
					flag3 = true;
					foreach (Transform item4 in gameObject3.transform)
					{
						SpawnPoint component2 = item4.gameObject.GetComponent<SpawnPoint>();
						if (component2 != null)
						{
							spawnPointsToConsider.Add(component2);
						}
					}
					gameObject3.SetActive(true);
				}
				if (!flag3)
				{
					int index2 = Random.Range(0, spawnGroups.Count);
					GameObject gameObject4 = spawnGroups[index2];
					foreach (Transform item5 in gameObject4.transform)
					{
						SpawnPoint component3 = item5.gameObject.GetComponent<SpawnPoint>();
						if (component3 != null)
						{
							spawnPointsToConsider.Add(component3);
						}
					}
					SaveUtility.SetString(base.gameObject.name, gameObject4.name, false);
					gameObject4.SetActive(true);
				}
			}
		}
		if (gameObject != null)
		{
			foreach (Transform item6 in gameObject.transform)
			{
				SpawnPoint component4 = item6.gameObject.GetComponent<SpawnPoint>();
				if (component4 != null)
				{
					spawnPointsToConsider.Add(component4);
				}
			}
		}
		CheckSpawnPointToConsider();
	}

	private void ClearPreviouslyCreatedSpawns(bool forceDestroy = true)
	{
		List<CreatedNPC> list = new List<CreatedNPC>();
		for (int i = 0; i < spawnGroupMaster.npcsCreated.Count; i++)
		{
			CreatedNPC createdNPC = spawnGroupMaster.npcsCreated[i];
			if ((!createdNPC.willRespawn || forceDestroy) && createdNPC.spawnGroup == this)
			{
				createdNPC.destroyDirty = true;
				list.Add(createdNPC);
				if (createdNPC.nearestNode != null)
				{
					createdNPC.nearestNode.SetForceStop(false);
				}
				Object.Destroy(createdNPC.npcObj);
			}
			createdNPC.willRespawn = false;
		}
		for (int num = spawnGroupMaster.npcsCreated.Count - 1; num >= 0; num--)
		{
			if (spawnGroupMaster.npcsCreated[num].npcObj == null || spawnGroupMaster.npcsCreated[num].destroyDirty)
			{
				spawnGroupMaster.npcsCreated.RemoveAt(num);
			}
		}
		spawnGroupMaster.npcsCreated.TrimExcess();
	}

	private CreatedNPC GetPreviouslyCreatedNPC(NPCDefinition npcDef)
	{
		if (npcDef.characterType == CharacterName.ADog)
		{
			return null;
		}
		for (int i = 0; i < spawnGroupMaster.npcsCreated.Count; i++)
		{
			CreatedNPC createdNPC = spawnGroupMaster.npcsCreated[i];
			if (createdNPC != null && createdNPC.npcObj != null && createdNPC.npcDef == npcDef)
			{
				return createdNPC;
			}
		}
		return null;
	}

	private void CheckSpawnPointToConsider()
	{
		List<NPCDefinition> list = new List<NPCDefinition>();
		List<SpawnPointNPCSetup> list2 = new List<SpawnPointNPCSetup>();
		int num = 0;
		if (CityController.instance != null)
		{
			num = CityController.instance.communicator.GetCurrentDay();
		}
		NPCDatabase npcDatabase = GameManager.instance.npcDatabase;
		for (int i = 0; i < spawnPointsToConsider.Count; i++)
		{
			SpawnPoint spawnPoint = spawnPointsToConsider[i];
			bool flag = false;
			for (int j = 0; j < spawnPoint.npcs.Count; j++)
			{
				SpawnPointNPC spawnPointNPC = spawnPoint.npcs[j];
				NPCDefinition nPCDef = npcDatabase.GetNPCDef(spawnPointNPC.id);
				NPCSpawnPointSetup nPCSpawnPointSetup = null;
				if (nPCDef == null)
				{
					continue;
				}
				for (int k = 0; k < nPCDef.spawnSetups.Count; k++)
				{
					if (nPCDef.spawnSetups[k].id != spawnPointNPC.spawnSetupId)
					{
						continue;
					}
					NPCSpawnPointSetup nPCSpawnPointSetup2 = nPCDef.spawnSetups[k];
					if ((nPCSpawnPointSetup2.timeOfDay == TimeOfDay.Day && !isDayTime) || (nPCSpawnPointSetup2.timeOfDay == TimeOfDay.Night && isDayTime) || nPCSpawnPointSetup2.activatesManually)
					{
						continue;
					}
					if (nPCSpawnPointSetup2.npcData.activeOnActiveMissionUID > -1 && nPCSpawnPointSetup2.npcData.forceDuringMission)
					{
						MissionData missionData = Player.instance.GetMissionData(nPCSpawnPointSetup2.npcData.activeOnActiveMissionUID);
						MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(nPCSpawnPointSetup2.npcData.hidesOnCompletedMissionUID);
						if (missionData != null && (missionCompletedData == null || nPCSpawnPointSetup2.npcData.hidesOnCompletedMissionUID == -1))
						{
							if (AllowNPCAdd(nPCDef, nPCSpawnPointSetup2, list, list2))
							{
								nPCSpawnPointSetup = nPCSpawnPointSetup2;
								if (!list.Contains(nPCDef))
								{
									list.Add(nPCDef);
								}
								flag = true;
							}
							continue;
						}
					}
					if (nPCSpawnPointSetup2.npcData.activeOnCompletedMissionUID > -1 && nPCSpawnPointSetup2.npcData.forceDuringMission)
					{
						MissionCompletedData missionCompletedData2 = Player.instance.GetMissionCompletedData(nPCSpawnPointSetup2.npcData.activeOnCompletedMissionUID);
						MissionCompletedData missionCompletedData3 = Player.instance.GetMissionCompletedData(nPCSpawnPointSetup2.npcData.hidesOnCompletedMissionUID);
						if (missionCompletedData2 != null && (missionCompletedData3 == null || nPCSpawnPointSetup2.npcData.hidesOnCompletedMissionUID == -1) && num >= missionCompletedData2.dayCompleted + nPCSpawnPointSetup2.npcData.daysAfterMission)
						{
							if (!AllowNPCAdd(nPCDef, nPCSpawnPointSetup2, list, list2))
							{
								continue;
							}
							nPCSpawnPointSetup = nPCSpawnPointSetup2;
							if (!list.Contains(nPCDef))
							{
								list.Add(nPCDef);
							}
							flag = true;
						}
					}
					if (nPCSpawnPointSetup2.npcData.activeOnActiveMissionUID != -1 || nPCSpawnPointSetup2.npcData.activeOnCompletedMissionUID != -1 || nPCSpawnPointSetup2.npcData.hidesOnCompletedMissionUID < -1 || !nPCSpawnPointSetup2.npcData.forceDuringMission)
					{
						continue;
					}
					MissionCompletedData missionCompletedData4 = Player.instance.GetMissionCompletedData(nPCSpawnPointSetup2.npcData.hidesOnCompletedMissionUID);
					if (missionCompletedData4 == null && AllowNPCAdd(nPCDef, nPCSpawnPointSetup2, list, list2))
					{
						nPCSpawnPointSetup = nPCSpawnPointSetup2;
						if (!list.Contains(nPCDef))
						{
							list.Add(nPCDef);
						}
						flag = true;
					}
				}
				for (int l = 0; l < nPCDef.spawnSetups.Count; l++)
				{
					NPCSpawnPointSetup value = nPCDef.spawnSetups[l];
					int index = Random.Range(l, nPCDef.spawnSetups.Count);
					nPCDef.spawnSetups[l] = nPCDef.spawnSetups[index];
					nPCDef.spawnSetups[index] = value;
				}
				for (int m = 0; m < nPCDef.spawnSetups.Count; m++)
				{
					if (nPCDef.spawnSetups[m].id != spawnPointNPC.spawnSetupId)
					{
						continue;
					}
					NPCSpawnPointSetup nPCSpawnPointSetup3 = nPCDef.spawnSetups[m];
					if ((nPCSpawnPointSetup3.timeOfDay == TimeOfDay.Day && !isDayTime) || (nPCSpawnPointSetup3.timeOfDay == TimeOfDay.Night && isDayTime) || nPCSpawnPointSetup3.npcData.forceDuringMission || nPCSpawnPointSetup3.activatesManually)
					{
						continue;
					}
					if (nPCSpawnPointSetup3.npcData.hidesOnCompletedMissionUID > -1)
					{
						MissionCompletedData missionCompletedData5 = Player.instance.GetMissionCompletedData(nPCSpawnPointSetup3.npcData.hidesOnCompletedMissionUID);
						if (missionCompletedData5 != null)
						{
							continue;
						}
					}
					if (nPCSpawnPointSetup3.npcData.hidesAfterTournamentDef != null)
					{
						TournamentCompletedData tournamentCompletedData = Player.instance.GetTournamentCompletedData(nPCSpawnPointSetup3.npcData.tournamentDef);
						if (tournamentCompletedData != null)
						{
							continue;
						}
					}
					if (nPCSpawnPointSetup3.npcData.activeOnActiveMissionUID == -1 && nPCSpawnPointSetup3.npcData.activeOnCompletedMissionUID == -1)
					{
						if (!AllowNPCAdd(nPCDef, nPCSpawnPointSetup3, list, list2))
						{
							continue;
						}
						nPCSpawnPointSetup = nPCSpawnPointSetup3;
						if (!list.Contains(nPCDef))
						{
							list.Add(nPCDef);
						}
						flag = true;
					}
					MissionData missionData2 = Player.instance.GetMissionData(nPCSpawnPointSetup3.npcData.activeOnActiveMissionUID);
					if (nPCSpawnPointSetup3.npcData.activeOnActiveMissionUID > -1 && missionData2 != null)
					{
						if (!AllowNPCAdd(nPCDef, nPCSpawnPointSetup3, list, list2))
						{
							continue;
						}
						nPCSpawnPointSetup = nPCSpawnPointSetup3;
						if (!list.Contains(nPCDef))
						{
							list.Add(nPCDef);
						}
						flag = true;
					}
					MissionCompletedData missionCompletedData6 = Player.instance.GetMissionCompletedData(nPCSpawnPointSetup3.npcData.activeOnCompletedMissionUID);
					if (nPCSpawnPointSetup3.npcData.activeOnCompletedMissionUID > -1 && missionCompletedData6 != null && num >= missionCompletedData6.dayCompleted + nPCSpawnPointSetup3.npcData.daysAfterMission)
					{
						if (!AllowNPCAdd(nPCDef, nPCSpawnPointSetup3, list, list2))
						{
							continue;
						}
						nPCSpawnPointSetup = nPCSpawnPointSetup3;
						if (!list.Contains(nPCDef))
						{
							list.Add(nPCDef);
						}
						flag = true;
					}
					if (!(nPCSpawnPointSetup3.npcData.tournamentDef != null))
					{
						continue;
					}
					TournamentCompletedData tournamentCompletedData2 = Player.instance.GetTournamentCompletedData(nPCSpawnPointSetup3.npcData.tournamentDef);
					if (tournamentCompletedData2 != null && num >= tournamentCompletedData2.dayCompleted + nPCSpawnPointSetup3.npcData.daysAfterTournament && AllowNPCAdd(nPCDef, nPCSpawnPointSetup3, list, list2))
					{
						nPCSpawnPointSetup = nPCSpawnPointSetup3;
						if (!list.Contains(nPCDef))
						{
							list.Add(nPCDef);
						}
						flag = true;
					}
				}
				if (nPCSpawnPointSetup != null)
				{
					SpawnPointNPCSetup spawnPointNPCSetup = new SpawnPointNPCSetup();
					spawnPointNPCSetup.npc = nPCDef;
					spawnPointNPCSetup.position = spawnPoint.transform.position;
					spawnPointNPCSetup.spawnPointNPC = spawnPointNPC;
					spawnPointNPCSetup.npcSpawnPointSetup = nPCSpawnPointSetup;
					spawnPointNPCSetup.nearestPathNode = spawnPoint.nearestPathNode;
					list2.Add(spawnPointNPCSetup);
				}
			}
		}
		for (int n = 0; n < list2.Count; n++)
		{
			SpawnPointNPCSetup spawnPointNPCSetup2 = list2[n];
			GameObject gameObject = null;
			CityNPC cityNPC = null;
			CreatedNPC previouslyCreatedNPC = GetPreviouslyCreatedNPC(spawnPointNPCSetup2.npc);
			bool flag2 = false;
			if (previouslyCreatedNPC != null && previouslyCreatedNPC.npcObj != null)
			{
				flag2 = true;
				gameObject = previouslyCreatedNPC.npcObj;
				cityNPC = gameObject.GetComponent<CityNPC>();
				if (cityNPC.spawnSetupName.Equals(spawnPointNPCSetup2.npcSpawnPointSetup.invalidAfterSpawnSetup))
				{
					continue;
				}
				previouslyCreatedNPC.willRespawn = true;
				previouslyCreatedNPC.UpdateNearestNode(spawnPointNPCSetup2.nearestPathNode);
				previouslyCreatedNPC.spawnGroup = this;
			}
			else
			{
				gameObject = new GameObject(spawnPointNPCSetup2.npc.name);
				cityNPC = gameObject.AddComponent<CityNPC>();
				cityNPC.flipCharacter = spawnPointNPCSetup2.spawnPointNPC.flipCharacter;
			}
			gameObject.transform.position = spawnPointNPCSetup2.position;
			gameObject.transform.parent = npcHolder;
			cityNPC.character = spawnPointNPCSetup2.npc.characterType;
			cityNPC.npcData.Clear();
			cityNPC.npcData.Add(spawnPointNPCSetup2.npcSpawnPointSetup.npcData);
			cityNPC.spawnSetupName = spawnPointNPCSetup2.npcSpawnPointSetup.name;
			if (spawnPointNPCSetup2.npcSpawnPointSetup.disablesGameObject)
			{
				cityNPC.npcData[0].disablesGameObject = spawnPointNPCSetup2.spawnPointNPC.disablesGameObject;
			}
			else
			{
				cityNPC.npcData[0].disablesGameObject = null;
			}
			cityNPC.npcData[0].movesAfterConversation = spawnPointNPCSetup2.npcSpawnPointSetup.movesAfterConversation;
			if (spawnPointNPCSetup2.npcSpawnPointSetup.movesAfterConversation)
			{
				cityNPC.npcData[0].moveToPosition = spawnPointNPCSetup2.spawnPointNPC.moveToPosition;
			}
			else
			{
				cityNPC.npcData[0].moveToPosition = null;
			}
			if (spawnPointNPCSetup2.nearestPathNode != null)
			{
				spawnPointNPCSetup2.nearestPathNode.SetForceStop(true);
			}
			if (!flag2)
			{
				spawnGroupMaster.npcsCreated.Add(new CreatedNPC(gameObject, cityNPC.npcDef, spawnPointNPCSetup2.nearestPathNode, this));
			}
		}
		ClearPreviouslyCreatedSpawns(false);
	}

	private bool AllowNPCAdd(NPCDefinition npcDef, NPCSpawnPointSetup npcSpawnPointSetup, List<NPCDefinition> npcsAdded, List<SpawnPointNPCSetup> spawnPointNPCSetups)
	{
		bool result = true;
		if (npcsAdded.Contains(npcDef) && npcDef.characterType != CharacterName.ADog)
		{
			result = false;
			int num = -1;
			for (int i = 0; i < spawnPointNPCSetups.Count; i++)
			{
				if (spawnPointNPCSetups[i].npcSpawnPointSetup.invalidAfterSpawnSetup.Equals(npcSpawnPointSetup.name))
				{
					num = i;
					result = true;
				}
			}
			if (num > -1)
			{
				spawnPointNPCSetups.RemoveAt(num);
			}
		}
		return result;
	}
}
