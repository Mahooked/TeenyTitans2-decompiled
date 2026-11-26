using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class GameSkinManager : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnSkinChange__BackingField;

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

	public event Action OnSkinChange
	{
		add
		{
			Action action = OnSkinChange__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSkinChange__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnSkinChange__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSkinChange__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void OnDestroy()
	{
		_instance = null;
		OnSkinChange__BackingField = null;
	}

	private void OnApplicationQuit()
	{
		isAppQuitting = true;
	}

	public void ChangeGameSkin(string newSkinName)
	{
		CurrentSkinName = newSkinName;
		if (OnSkinChange__BackingField != null)
		{
			OnSkinChange__BackingField();
		}
	}
}
