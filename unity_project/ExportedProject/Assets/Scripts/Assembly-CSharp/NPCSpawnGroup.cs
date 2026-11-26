using System;

[Serializable]
public class NPCSpawnGroup
{
	public string name;

	public TimeOfDay timeOfDay;

	public NPCSpawnPoint[] spawnPoints;

	public NPCSpawnPoint GetSpawnPointForNPCEntity(CityEntityNPC npcEntity)
	{
		int num = spawnPoints.Length;
		for (int i = 0; i < num; i++)
		{
			if (object.ReferenceEquals(spawnPoints[i].npcEntity, npcEntity))
			{
				return spawnPoints[i];
			}
		}
		return null;
	}
}
