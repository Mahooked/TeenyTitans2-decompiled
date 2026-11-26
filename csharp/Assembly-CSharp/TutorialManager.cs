using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
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
				if (ActiveTutorialChanged != null)
				{
					ActiveTutorialChanged(_activeTutorial);
				}
			}
		}
	}

	public event Action<Tutorial> ActiveTutorialChanged;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			List<Tutorial> list = new List<Tutorial>(Resources.LoadAll<Tutorial>("Data/Tutorials"));
			List<Tutorial> allDebugTuts = new List<Tutorial>(list.FindAll((Tutorial tut) => tut.debugTutorial));
			list.RemoveAll((Tutorial tut) => allDebugTuts.Contains(tut));
			list.InsertRange(0, allDebugTuts);
			allTutorials = list.ToArray();
			SceneManager.activeSceneChanged += HandleActiveSceneChanged;
		}
		else
		{
			UnityEngine.Object.Destroy(gameObject);
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
		Debug.Log("Tutorial Manager, Trigget Set: " + trigger);
		CheckForTutorials();
	}

	private void HandleDataReset()
	{
		Debug.Log("Tutorial Manager, Handle Data Reset...");
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
		Debug.LogFormat("[TutorialManager::PlayTutorial] - Play tutorial \"{0}\"", tutorial.name);
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
			Debug.Log("Override starting step, Debugging starting step");
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
			Debug.LogFormat("Current Active Tutorial, {0}, will not be valid in next scene, {1}, aborting tutorial...", activeTutorial.name, next.name);
			CancelActiveTutorial();
		}
		currentActiveSceneName = next.name;
	}

	private void CancelActiveTutorial()
	{
		Debug.LogFormat("Cancel Active Tutorial, {0}", (!(activeTutorial != null)) ? "NULL" : activeTutorial.name);
		if (tutorialStepRoutine != null)
		{
			Debug.Log("Stop Tutorial Step Routine...");
			StopCoroutine(tutorialStepRoutine);
		}
		if (playTutorialRoutine != null)
		{
			Debug.Log("Stop Play Tutorial Routine...");
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
