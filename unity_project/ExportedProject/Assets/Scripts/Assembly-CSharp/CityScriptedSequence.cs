using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CityScriptedSequence : MonoBehaviour
{
	private static List<CityScriptedSequence> activeSequences = new List<CityScriptedSequence>();

	private IEnumerator runningRoutine;

	protected CameraFollow cameraFollow;

	protected CameraPinchZoomer cameraPinchZoomer;

	private Coroutine debugRoutine;

	public static bool scriptedSequenceActive
	{
		get
		{
			return activeSequences.Count > 0;
		}
	}

	public void StartSequence()
	{
		if (runningRoutine != null)
		{
			Debug.Log("This sequence is already running. Ignoring this call to start.");
		}
		else
		{
			StartCoroutine(RunSequence());
		}
	}

	private IEnumerator RunSequence()
	{
		activeSequences.Add(this);
		yield return new WaitForEndOfFrame();
		yield return new WaitUntil(() => CityController.instance.IsPlayerAvailable() && activeSequences[0] == this);
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.cityCharacter.StopMove();
		GameUI.instance.HideAll();
		GameUI.instance.HideAllLandmarksBut(null);
		cameraFollow = CityController.instance.cameraHolder.GetComponent<CameraFollow>();
		cameraPinchZoomer = CityController.instance.pinchZoomer;
		float previousApproachFactor = cameraFollow.approachFactor;
		Transform prevFollowTarget = cameraFollow.follow;
		runningRoutine = Play();
		yield return StartCoroutine(runningRoutine);
		GameUI.instance.ShowAll();
		GameUI.instance.ShowAllLandmarks();
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = prevFollowTarget;
		CityController.instance.cityCharacter.AllowInput = true;
		runningRoutine = null;
		OnSequenceFinished();
		activeSequences.Remove(this);
	}

	protected virtual void OnDestroy()
	{
		activeSequences.Remove(this);
	}

	private IEnumerator DebugRunSequence()
	{
		while (true)
		{
			Debug.LogFormat("RUN SEQUENCE Loop, IsPlayerAvailable: {0}", CityController.instance.IsPlayerAvailable());
			string debugString = "Active Sequences: \n";
			for (int i = 0; i < activeSequences.Count; i++)
			{
				string text = debugString;
				debugString = string.Concat(text, i, ": ", activeSequences[i].GetType(), "\n");
			}
			Debug.Log(debugString);
			yield return null;
		}
	}

	protected abstract IEnumerator Play();

	protected virtual void OnSequenceFinished()
	{
	}
}
