using System;
using UnityEngine;

public class GameSkinManager : MonoBehaviour
{
	private static bool isAppQuitting;

	private static GameSkinManager _instance;

	public string CurrentSkinName
	{
		get
		{
			if (!SaveUtility.HasKey("GAME_SKIN_NAME"))
			{
				SaveUtility.SetString("GAME_SKIN_NAME", PlayerPrefs.GetString("GAME_SKIN_NAME", "ROBIN"));
			}
			return SaveUtility.GetString("GAME_SKIN_NAME", "ROBIN");
		}
		private set
		{
			SaveUtility.SetString("GAME_SKIN_NAME", value);
		}
	}

	public static GameSkinManager instance
	{
		get
		{
			if (_instance == null && !isAppQuitting)
			{
				_instance = new GameObject("GameSkinManager").AddComponent<GameSkinManager>();
			}
			return _instance;
		}
	}

	public event Action OnSkinChange;

	private void OnDestroy()
	{
		_instance = null;
		OnSkinChange = null;
	}

	private void OnApplicationQuit()
	{
		isAppQuitting = true;
	}

	public void ChangeGameSkin(string newSkinName)
	{
		CurrentSkinName = newSkinName;
		if (OnSkinChange != null)
		{
			OnSkinChange();
		}
	}
}
