using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	private static bool applicationDidQuit;

	private bool _isPaused;

	private bool _isEnabled = true;

	private static PauseManager _instance;

	public bool isPaused => _isPaused;

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
				if (IsEnabledChanged != null)
				{
					IsEnabledChanged(_isEnabled);
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

	public event Action<bool> Paused;

	public event Action<bool> IsEnabledChanged;

	private void Awake()
	{
		_instance = this;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
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
			if (Paused != null)
			{
				Paused(obj: true);
			}
		}
	}

	public void Resume()
	{
		if (_isEnabled && _isPaused)
		{
			_isPaused = false;
			if (Paused != null)
			{
				Paused(obj: false);
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
