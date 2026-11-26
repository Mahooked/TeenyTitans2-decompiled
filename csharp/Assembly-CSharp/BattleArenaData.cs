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

	public int uid => _uid;

	public string arenaName => _arenaName;

	public string prefabPath => _prefabPath;

	public string battleThemePath => _battleThemePath;

	public float battleThemeVolume => _battleThemeVolume;

	public bool overrideResultsMusic => _overrideResultsMusic;
}
