using System;

[Serializable]
public class NPCSpawnPointSetup
{
	public int id;

	public string name;

	[NonSerialized]
	public bool foldout = true;

	public bool disablesGameObject;

	public bool activatesManually;

	public bool movesAfterConversation;

	public string invalidAfterSpawnSetup = string.Empty;

	public TimeOfDay timeOfDay;

	public CityNPCData npcData = new CityNPCData();
}
