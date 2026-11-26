using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityDatabase : ScriptableObject
{
	[SerializeField]
	private List<AbilityData> _abilities;

	[SerializeField]
	private int _nextId = -1;

	public List<AbilityData> abilities
	{
		get
		{
			return _abilities;
		}
	}

	private int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	private void OnEnable()
	{
		if (_abilities == null)
		{
			_abilities = new List<AbilityData>();
		}
	}

	public AbilityData GetAbility(int uid)
	{
		return _abilities.Find((AbilityData obj) => obj.uid == uid);
	}

	public List<AbilityData> GetAbilities(List<int> uids)
	{
		List<AbilityData> list = new List<AbilityData>();
		int count = uids.Count;
		for (int i = 0; i < count; i++)
		{
			list.Add(_abilities.Find((AbilityData obj) => obj.uid == uids[i]));
		}
		return list;
	}
}
