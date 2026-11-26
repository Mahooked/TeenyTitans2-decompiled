using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	private static bool applicationDidQuit;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<bool> Paused__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<bool> IsEnabledChanged__BackingField;

	private bool _isPaused;

	private bool _isEnabled = true;

	private static PauseManager _instance;

	public bool isPaused
	{
		get
		{
			return _isPaused;
		}
	}

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			if (value != _isEnabled)
			{
				_isEnabled = value;
				if (IsEnabledChanged__BackingField != null)
				{
					IsEnabledChanged__BackingField(_isEnabled);
				}
			}
		}
	}

	public static PauseManager instance
	{
		get
		{
			if (_instance == null && !applicationDidQuit)
			{
				_instance = new GameObject("PauseManager").AddComponent<PauseManager>();
			}
			return _instance;
		}
	}

	public event Action<bool> Paused
	{
		add
		{
			Action<bool> action = Paused__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Paused__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = Paused__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Paused__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool> IsEnabledChanged
	{
		add
		{
			Action<bool> action = IsEnabledChanged__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref IsEnabledChanged__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = IsEnabledChanged__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref IsEnabledChanged__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		_instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Pause();
		}
	}

	private void OnApplicationQuit()
	{
		applicationDidQuit = true;
	}

	public void Pause()
	{
		string text = SceneManager.GetActiveScene().name;
		if (!text.Equals("ShopTest") && !text.Equals("PreBattle") && !text.Equals("Loading") && (!(TitleScreenController.instance != null) || !TitleScreenController.instance.titleScreenActive) && !LoadingScreenCover.isLoading && (!(DialogueTree.instance != null) || !DialogueTree.instance.gameObject.activeInHierarchy) && (!(CoreGameTutorial.instance != null) || !CoreGameTutorial.instance.isActive) && _isEnabled && !_isPaused)
		{
			_isPaused = true;
			if (Paused__BackingField != null)
			{
				Paused__BackingField(true);
			}
		}
	}

	public void Resume()
	{
		if (_isEnabled && _isPaused)
		{
			_isPaused = false;
			if (Paused__BackingField != null)
			{
				Paused__BackingField(false);
			}
		}
	}

	public void Toggle()
	{
		if (_isPaused)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}
}
