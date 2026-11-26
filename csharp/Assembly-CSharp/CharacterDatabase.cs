using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterDatabase : ScriptableObject
{
	private int _totalActiveCharacters;

	[SerializeField]
	private List<CharacterDefinition> _characters;

	[SerializeField]
	private int _nextId = -1;

	[SerializeField]
	private int _nextRepaintId;

	public int totalCharacters => _characters.Count;

	public int totalActiveCharacters => _totalActiveCharacters;

	public List<CharacterDefinition> characters => _characters;

	private int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	public int nextRepaintId
	{
		get
		{
			_nextRepaintId++;
			return _nextRepaintId;
		}
	}

	private void OnEnable()
	{
		if (Application.isPlaying)
		{
			_totalActiveCharacters = _characters.FindAll((CharacterDefinition obj) => obj.isActive).Count;
			Debug.Log("Total Active Characters: " + _totalActiveCharacters);
		}
	}

	public CharacterDefinition GetCharacter(int uid)
	{
		int count = _characters.Count;
		for (int i = 0; i < count; i++)
		{
			if (_characters[i].uid == uid)
			{
				return _characters[i];
			}
		}
		return null;
	}

	public CharacterDefinition GetCharacterWithRepaintColorSlot(RepaintColorSlot colorSlot)
	{
		int count = _characters.Count;
		for (int i = 0; i < count; i++)
		{
			if (_characters[i].repaintSlots.Contains(colorSlot))
			{
				return _characters[i];
			}
		}
		return null;
	}

	public CharacterDefinition GetRandomCharacter(Predicate<CharacterDefinition> match)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < _characters.Count; i++)
		{
			if (match(_characters[i]))
			{
				list.Add(i);
			}
		}
		return _characters[list[UnityEngine.Random.Range(0, list.Count)]];
	}

	public List<CharacterDefinition> GetRandomCharacters(int count, bool battleReady = false)
	{
		if (count > _characters.Count)
		{
			count = _characters.Count;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < _characters.Count; i++)
		{
			if (!battleReady || _characters[i].canUseInBattle)
			{
				list.Add(i);
			}
		}
		List<CharacterDefinition> list2 = new List<CharacterDefinition>();
		while (list2.Count < count)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			list2.Add(_characters[list[index]]);
			list.RemoveAt(index);
		}
		return list2;
	}

	public int GetTotalDiscoverableCharactersForPlayer(Player player)
	{
		int num = 0;
		int count = characters.Count;
		for (int i = 0; i < count; i++)
		{
			if (characters[i].isActive && (!characters[i].isSecret || player.HasAcquiredCharacter(characters[i])))
			{
				num++;
			}
		}
		return num;
	}
}
