using System;
using UnityEngine;

[Serializable]
public class CreatedNPC
{
	public GameObject npcObj;

	public NPCDefinition npcDef;

	public PathNode nearestNode;

	public bool willRespawn;

	public bool destroyDirty;

	public SpawnGroupManager spawnGroup;

	public CreatedNPC(GameObject newNPCObj, NPCDefinition newNpcDef, PathNode newNearestNode, SpawnGroupManager spawnGroupManager)
	{
		npcObj = newNPCObj;
		npcDef = newNpcDef;
		spawnGroup = spawnGroupManager;
		nearestNode = newNearestNode;
		willRespawn = true;
	}

	public void UpdateNearestNode(PathNode newNearestNode)
	{
		nearestNode = newNearestNode;
	}
}
