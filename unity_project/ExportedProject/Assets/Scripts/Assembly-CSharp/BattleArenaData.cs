using System;
using UnityEngine;

[Serializable]
public class BattleArenaData
{
	[SerializeField]
	private int _uid = -1;

	[SerializeField]
	private string _arenaName = string.Empty;

	[SerializeField]
	private string _prefabPath = string.Empty;

	[SerializeField]
	private string _battleThemePath = string.Empty;

	[SerializeField]
	private float _battleThemeVolume = 1f;

	[SerializeField]
	private bool _overrideResultsMusic;

	public int uid
	{
		get
		{
			return _uid;
		}
	}

	public string arenaName
	{
		get
		{
			return _arenaName;
		}
	}

	public string prefabPath
	{
		get
		{
			return _prefabPath;
		}
	}

	public string battleThemePath
	{
		get
		{
			return _battleThemePath;
		}
	}

	public float battleThemeVolume
	{
		get
		{
			return _battleThemeVolume;
		}
	}

	public bool overrideResultsMusic
	{
		get
		{
			return _overrideResultsMusic;
		}
	}
}
