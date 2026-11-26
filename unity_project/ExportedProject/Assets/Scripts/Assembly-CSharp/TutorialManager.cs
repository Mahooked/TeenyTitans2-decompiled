using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Tutorial> ActiveTutorialChanged__BackingField;

	private Tutorial[] allTutorials;

	private Coroutine playTutorialRoutine;

	private Coroutine tutorialStepRoutine;

	public static TutorialManager instance;

	private Tutorial _activeTutorial;

	private string currentActiveSceneName { get; set; }

	public Tutorial activeTutorial
	{
		get
		{
			return _activeTutorial;
		}
		private set
		{
			if (_activeTutorial != value)
			{
				_activeTutorial = value;
				if (ActiveTutorialChanged__BackingField != null)
				{
					ActiveTutorialChanged__BackingField(_activeTutorial);
				}
			}
		}
	}

	public event Action<Tutorial> ActiveTutorialChanged
	{
		add
		{
			Action<Tutorial> action = ActiveTutorialChanged__BackingField;
			Action<Tutorial> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActiveTutorialChanged__BackingField, (Action<Tutorial>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Tutorial> action = ActiveTutorialChanged__BackingField;
			Action<Tutorial> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActiveTutorialChanged__BackingField, (Action<Tutorial>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			List<Tutorial> list = new List<Tutorial>(Resources.LoadAll<Tutorial>("Data/Tutorials"));
			List<Tutorial> allDebugTuts = new List<Tutorial>(list.FindAll((Tutorial tut) => tut.debugTutorial));
			list.RemoveAll((Tutorial tut) => allDebugTuts.Contains(tut));
			list.InsertRange(0, allDebugTuts);
			allTutorials = list.ToArray();
			SceneManager.activeSceneChanged += HandleActiveSceneChanged;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		if (!(instance != this))
		{
			Player.instance.TriggerSet += HandleTriggerSet;
			Player.instance.DataReset += HandleDataReset;
			CheckForTutorials();
		}
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.TriggerSet -= HandleTriggerSet;
			Player.instance.DataReset -= HandleDataReset;
		}
		SceneManager.activeSceneChanged -= HandleActiveSceneChanged;
	}

	private void HandleTriggerSet(string trigger)
	{
		UnityEngine.Debug.Log("Tutorial Manager, Trigget Set: " + trigger);
		CheckForTutorials();
	}

	private void HandleDataReset()
	{
		UnityEngine.Debug.Log("Tutorial Manager, Handle Data Reset...");
		CancelActiveTutorial();
	}

	private void Update()
	{
		CheckForTutorials();
	}

	private void CheckForTutorials()
	{
		if (LoadingScreenCover.isLoading || InPvpBattle())
		{
			return;
		}
		int num = allTutorials.Length;
		for (int i = 0; i < num; i++)
		{
			Tutorial tutorial = allTutorials[i];
			if (tutorial.targetScenes.Contains(currentActiveSceneName) && tutorial.ArePrerequisitesMet() && tutorial.isGameStateValid && !tutorial.IsComplete() && tutorial != activeTutorial && activeTutorial == null)
			{
				playTutorialRoutine = StartCoroutine(PlayTutorial(tutorial));
			}
		}
	}

	private IEnumerator PlayTutorial(Tutorial tutorial)
	{
		UnityEngine.Debug.LogFormat("[TutorialManager::PlayTutorial] - Play tutorial \"{0}\"", tutorial.name);
		activeTutorial = tutorial;
		int len = activeTutorial.tutorialSteps.Length;
		int startingStep = 0;
		for (int i = 0; i < len; i++)
		{
			TutorialStep tutorialStep = activeTutorial.tutorialSteps[i];
			if (SaveUtility.GetInt(tutorialStep.name + "_Complete") == 0)
			{
				startingStep = i;
				break;
			}
		}
		if (tutorial.debugTutorial)
		{
			startingStep = tutorial.debugStartAtStep;
			UnityEngine.Debug.Log("Override starting step, Debugging starting step");
		}
		int currentStep = startingStep;
		while (currentStep < activeTutorial.tutorialSteps.Length)
		{
			for (int j = 0; j < len; j++)
			{
				activeTutorial.tutorialSteps[j].isPlaying = j == currentStep;
			}
			tutorialStepRoutine = StartCoroutine(activeTutorial.PlayTutorial(currentStep));
			yield return tutorialStepRoutine;
			tutorialStepRoutine = null;
			SaveUtility.SetInt(activeTutorial.tutorialSteps[currentStep++].name + "_Complete", 1);
		}
		activeTutorial.CleanUp();
		activeTutorial.CompleteTutorial();
		activeTutorial = null;
		playTutorialRoutine = null;
	}

	private void HandleActiveSceneChanged(Scene current, Scene next)
	{
		if (next.name != "Transition" && activeTutorial != null && !activeTutorial.targetScenes.Contains(next.name))
		{
			UnityEngine.Debug.LogFormat("Current Active Tutorial, {0}, will not be valid in next scene, {1}, aborting tutorial...", activeTutorial.name, next.name);
			CancelActiveTutorial();
		}
		currentActiveSceneName = next.name;
	}

	private void CancelActiveTutorial()
	{
		UnityEngine.Debug.LogFormat("Cancel Active Tutorial, {0}", (!(activeTutorial != null)) ? "NULL" : activeTutorial.name);
		if (tutorialStepRoutine != null)
		{
			UnityEngine.Debug.Log("Stop Tutorial Step Routine...");
			StopCoroutine(tutorialStepRoutine);
		}
		if (playTutorialRoutine != null)
		{
			UnityEngine.Debug.Log("Stop Play Tutorial Routine...");
			StopCoroutine(playTutorialRoutine);
		}
		playTutorialRoutine = null;
		tutorialStepRoutine = null;
		activeTutorial = null;
	}

	public void CompleteAllTutorials()
	{
		CancelActiveTutorial();
		int num = allTutorials.Length;
		for (int i = 0; i < num; i++)
		{
			if (allTutorials[i].tutorialName != "LODCommunicatorTutorial")
			{
				allTutorials[i].CompleteTutorial();
			}
		}
	}

	public bool IsTutorialComplete(string tutoriaName)
	{
		int num = allTutorials.Length;
		for (int i = 0; i < num; i++)
		{
			if (allTutorials[i].tutorialName == tutoriaName)
			{
				return allTutorials[i].IsComplete();
			}
		}
		return false;
	}

	private bool InPvpBattle()
	{
		BattleDefinition battleDefinition = null;
		if (PreBattleController.Instance != null && PreBattleController.battleDefinition != null)
		{
			battleDefinition = PreBattleController.battleDefinition;
		}
		else if (BattleController.instance != null && BattleController.battleDefinition != null)
		{
			battleDefinition = BattleController.battleDefinition;
		}
		return battleDefinition != null && (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend);
	}
}
