using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChallengeModeDatabase : ScriptableObject
{
	public int HardModeBonus = 30;

	public int ConsecutiveBonusMultiplier = 5;

	public int MaxConsecutiveWins = 10;

	public int minHardModeHitPointsIncrease = 50;

	public int maxHardModeHitPointsIncrease = 90;

	public int minHardModeAttackIncrease = 5;

	public int maxHardModeAttackIncrease = 10;

	public int minNormalModeEnemyLvl = 8;

	public int maxNormalModeEnemyLvl = 13;

	[SerializeField]
	private List<ChallengeModeDefinition> _items;

	[SerializeField]
	private int _nextId = -1;

	public List<ChallengeModeDefinition> items => _items;

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
		if (_items == null)
		{
			_items = new List<ChallengeModeDefinition>();
		}
	}

	public ChallengeModeDefinition GetItem(int uid)
	{
		return _items.Find((ChallengeModeDefinition obj) => obj.uid == uid);
	}

	public List<ChallengeModeDefinition> GetItems(List<int> uids)
	{
		List<ChallengeModeDefinition> list = new List<ChallengeModeDefinition>();
		int count = uids.Count;
		for (int i = 0; i < count; i++)
		{
			list.Add(_items.Find((ChallengeModeDefinition obj) => obj.uid == uids[i]));
		}
		return list;
	}

	public ChallengeModeDefinition GetItem(ChallengeModeType type)
	{
		return _items.Find((ChallengeModeDefinition obj) => obj.challengeModeType == type);
	}
}
