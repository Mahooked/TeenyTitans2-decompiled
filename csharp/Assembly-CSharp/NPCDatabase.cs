using System.Collections.Generic;
using UnityEngine;

public class NPCDatabase : ScriptableObject
{
	[SerializeField]
	private List<NPCDefinition> _npcs;

	[SerializeField]
	private int _nextId = -1;

	public List<NPCDefinition> npcs => _npcs;

	private int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	public NPCDefinition GetNPCDef(int uid)
	{
		int count = _npcs.Count;
		for (int i = 0; i < count; i++)
		{
			if (_npcs[i].uid == uid)
			{
				return _npcs[i];
			}
		}
		return null;
	}

	public NPCDefinition GetNPCDef(string guid)
	{
		int count = _npcs.Count;
		for (int i = 0; i < count; i++)
		{
			if (_npcs[i].guid == guid)
			{
				return _npcs[i];
			}
		}
		return null;
	}

	public NPCDefinition GetNPCDefByType(CharacterName characterType)
	{
		int count = _npcs.Count;
		for (int i = 0; i < count; i++)
		{
			if (_npcs[i].characterType == characterType)
			{
				return _npcs[i];
			}
		}
		return null;
	}

	public NPCDefinition GetNPCDefByName(string characterName)
	{
		int count = _npcs.Count;
		for (int i = 0; i < count; i++)
		{
			if (_npcs[i].name.Equals(characterName))
			{
				return _npcs[i];
			}
		}
		return null;
	}
}
