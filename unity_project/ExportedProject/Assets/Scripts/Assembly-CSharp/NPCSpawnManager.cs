using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnManager : MonoBehaviour
{
	public bool showDebug;

	public CityEntityNPC[] npcEntities;

	public NPCSpawnGroup[] spawnGroups;

	private Communicator communicator;

	private bool isDayTime = true;

	private int prevDaySpawnGroupIndex = -1;

	private int prevNightSpawnGroupIndex = -1;

	public NPCSpawnGroup activeSpawnGroup { get; private set; }

	private void OnGUI()
	{
		if (showDebug)
		{
			GUI.contentColor = Color.black;
			GUI.Label(new Rect(11f, 11f, Screen.width - 20, 20f), string.Format("Active Spawn Group: {0} ({1})", (activeSpawnGroup == null) ? "None" : activeSpawnGroup.name, (activeSpawnGroup == null) ? "N/A" : activeSpawnGroup.timeOfDay.ToString()));
			GUI.contentColor = Color.white;
			GUI.Label(new Rect(10f, 10f, Screen.width - 20, 20f), string.Format("Active Spawn Group: {0} ({1})", (activeSpawnGroup == null) ? "None" : activeSpawnGroup.name, (activeSpawnGroup == null) ? "N/A" : activeSpawnGroup.timeOfDay.ToString()));
		}
	}

	private void Start()
	{
		communicator = Communicator.instance;
		for (int i = 0; i < npcEntities.Length; i++)
		{
			npcEntities[i].StateChanged += HandleNPCEntityStateChanged;
		}
		if (communicator != null)
		{
			communicator.StartDayTime += HandleTimeOfDayChange;
			communicator.StartNightTime += HandleTimeOfDayChange;
			HandleTimeOfDayChange();
		}
		else
		{
			StartCoroutine(DelayNewSpawnGroup());
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < npcEntities.Length; i++)
		{
			npcEntities[i].StateChanged -= HandleNPCEntityStateChanged;
		}
		if (communicator != null)
		{
			communicator.StartDayTime -= HandleTimeOfDayChange;
			communicator.StartNightTime -= HandleTimeOfDayChange;
			communicator = null;
		}
	}

	private void HandleTimeOfDayChange()
	{
		isDayTime = communicator.isDayTime;
		StartCoroutine(DelayNewSpawnGroup());
	}

	private IEnumerator DelayNewSpawnGroup()
	{
		yield return new WaitForEndOfFrame();
		ChooseNewSpawnGroup();
	}

	private void ChooseNewSpawnGroup()
	{
		int num = ((!isDayTime) ? SaveUtility.GetInt("curNightSpawnGroupIndex", -1) : SaveUtility.GetInt("curDaySpawnGroupIndex", -1));
		if (num > -1 && num < spawnGroups.Length)
		{
			SetActiveSpawnGroup(spawnGroups[num]);
			return;
		}
		int num2 = SaveUtility.GetInt("prevDaySpawnGroupIndex", -1);
		int num3 = SaveUtility.GetInt("prevNightSpawnGroupIndex", -1);
		List<NPCSpawnGroup> list = new List<NPCSpawnGroup>();
		for (int i = 0; i < spawnGroups.Length; i++)
		{
			if ((spawnGroups[i].timeOfDay == TimeOfDay.Both && ((isDayTime && i != num2) || (!isDayTime && i != num3))) || (spawnGroups[i].timeOfDay == TimeOfDay.Day && isDayTime && i != num2) || (spawnGroups[i].timeOfDay == TimeOfDay.Night && !isDayTime && i != num3))
			{
				list.Add(spawnGroups[i]);
			}
		}
		if (list.Count > 0)
		{
			SetActiveSpawnGroup(list[UnityEngine.Random.Range(0, list.Count)]);
		}
		else
		{
			SetActiveSpawnGroup(null);
		}
	}

	private void SetActiveSpawnGroup(NPCSpawnGroup spawnGroup)
	{
		if (activeSpawnGroup != null && spawnGroup == activeSpawnGroup)
		{
			return;
		}
		activeSpawnGroup = spawnGroup;
		if (activeSpawnGroup != null)
		{
			if (isDayTime)
			{
				SaveUtility.SetInt("prevDaySpawnGroupIndex", Array.IndexOf(spawnGroups, activeSpawnGroup), false);
				SaveUtility.SetInt("curDaySpawnGroupIndex", Array.IndexOf(spawnGroups, activeSpawnGroup), false);
				SaveUtility.SetInt("curNightSpawnGroupIndex", -1, false);
			}
			else
			{
				SaveUtility.SetInt("prevNightSpawnGroupIndex", Array.IndexOf(spawnGroups, activeSpawnGroup), false);
				SaveUtility.SetInt("curNightSpawnGroupIndex", Array.IndexOf(spawnGroups, activeSpawnGroup), false);
				SaveUtility.SetInt("curDaySpawnGroupIndex", -1, false);
			}
			NPCSpawnPoint nPCSpawnPoint = null;
			CityEntityStateNPC.NPCSpawnGroupConfig nPCSpawnGroupConfig = null;
			for (int i = 0; i < activeSpawnGroup.spawnPoints.Length; i++)
			{
				nPCSpawnPoint = activeSpawnGroup.spawnPoints[i];
				if (nPCSpawnPoint.npcEntity.activeState != null)
				{
					nPCSpawnGroupConfig = ((CityEntityStateNPC)nPCSpawnPoint.npcEntity.activeState).npcSpawnGroupConfig;
					if (nPCSpawnGroupConfig.overrideSpawnManager)
					{
						nPCSpawnPoint = spawnGroups[nPCSpawnGroupConfig.spawnGroupOverrideIndex].GetSpawnPointForNPCEntity(nPCSpawnPoint.npcEntity);
					}
				}
				nPCSpawnPoint.npcEntity.isHidden = !nPCSpawnPoint.isActive;
				nPCSpawnPoint.npcEntity.transform.position = nPCSpawnPoint.position;
				nPCSpawnPoint.npcEntity.nearestNode = nPCSpawnPoint.nearestPathNode;
			}
			return;
		}
		if (isDayTime)
		{
			SaveUtility.SetInt("prevDaySpawnGroupIndex", -1, false);
			SaveUtility.SetInt("curDaySpawnGroupIndex", -1, false);
			SaveUtility.SetInt("curNightSpawnGroupIndex", -1, false);
		}
		else
		{
			SaveUtility.SetInt("prevNightSpawnGroupIndex", -1, false);
			SaveUtility.SetInt("curNightSpawnGroupIndex", -1, false);
			SaveUtility.SetInt("curDaySpawnGroupIndex", -1, false);
		}
		CityEntityStateNPC.NPCSpawnGroupConfig nPCSpawnGroupConfig2 = null;
		int num = npcEntities.Length;
		for (int j = 0; j < num; j++)
		{
			if (npcEntities[j].activeState != null)
			{
				nPCSpawnGroupConfig2 = ((CityEntityStateNPC)npcEntities[j].activeState).npcSpawnGroupConfig;
				if (nPCSpawnGroupConfig2.overrideSpawnManager)
				{
					npcEntities[j].isHidden = false;
					NPCSpawnPoint spawnPointForNPCEntity = spawnGroups[nPCSpawnGroupConfig2.spawnGroupOverrideIndex].GetSpawnPointForNPCEntity(npcEntities[j]);
					npcEntities[j].transform.position = spawnPointForNPCEntity.position;
					npcEntities[j].nearestNode = spawnPointForNPCEntity.nearestPathNode;
				}
				else
				{
					npcEntities[j].isHidden = true;
				}
			}
			else
			{
				npcEntities[j].isHidden = true;
			}
		}
	}

	private void HandleNPCEntityStateChanged(CityEntity entity)
	{
		CityEntityNPC cityEntityNPC = (CityEntityNPC)entity;
		if (cityEntityNPC.activeState != null)
		{
			CityEntityStateNPC cityEntityStateNPC = (CityEntityStateNPC)cityEntityNPC.activeState;
			if (cityEntityStateNPC.npcSpawnGroupConfig.overrideSpawnManager)
			{
				NPCSpawnPoint spawnPointForNPCEntity = spawnGroups[cityEntityStateNPC.npcSpawnGroupConfig.spawnGroupOverrideIndex].GetSpawnPointForNPCEntity(cityEntityNPC);
				cityEntityNPC.isHidden = !spawnPointForNPCEntity.isActive;
				cityEntityNPC.transform.position = spawnPointForNPCEntity.position;
				cityEntityNPC.nearestNode = spawnPointForNPCEntity.nearestPathNode;
			}
		}
	}
}
