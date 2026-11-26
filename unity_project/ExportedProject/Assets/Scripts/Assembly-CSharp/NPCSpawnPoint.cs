using System;
using UnityEngine;

[Serializable]
public class NPCSpawnPoint
{
	public bool isActive = true;

	public CityEntityNPC npcEntity;

	public Vector3 position;

	public PathNode nearestPathNode;
}
