using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleArenaDatabase : ScriptableObject
{
	[SerializeField]
	private List<BattleArenaData> _arenas;

	[SerializeField]
	private int _nextUid;

	public List<BattleArenaData> arenas
	{
		get
		{
			return _arenas;
		}
	}

	public int nextUid
	{
		get
		{
			_nextUid++;
			return _nextUid - 1;
		}
	}

	private void OnEnable()
	{
		if (_arenas == null)
		{
			_arenas = new List<BattleArenaData>();
		}
	}

	public BattleArenaData GetArenaByUID(int uid)
	{
		return _arenas.Find((BattleArenaData arena) => arena.uid == uid);
	}

	public BattleArenaData GetRandomArena()
	{
		if (_arenas.Count > 0)
		{
			return _arenas[UnityEngine.Random.Range(0, _arenas.Count)];
		}
		return null;
	}
}
