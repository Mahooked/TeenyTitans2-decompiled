using System;
using UnityEngine;

[Serializable]
public class SpawnPointNPC
{
	public int id;

	public int spawnSetupId;

	public GameObject disablesGameObject;

	public GameObject moveToPosition;

	public bool flipCharacter;
}
