using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSkinDatabase : ScriptableObject
{
	[SerializeField]
	private List<GameSkinData> _skinData;

	[SerializeField]
	private int _nextUid;

	public List<GameSkinData> skinData
	{
		get
		{
			return _skinData;
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
		if (_skinData == null)
		{
			_skinData = new List<GameSkinData>();
		}
	}

	public GameSkinData GetSkinByUID(int uid)
	{
		return _skinData.Find((GameSkinData obj) => obj.uid == uid);
	}
}
