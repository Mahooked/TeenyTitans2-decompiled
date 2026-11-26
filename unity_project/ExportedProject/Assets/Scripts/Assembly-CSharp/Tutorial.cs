using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : ScriptableObject
{
	public string tutorialName;

	public string[] tutorialPrerequisites;

	[HideInInspector]
	public string targetScene;

	[HideInInspector]
	public List<string> targetScenes;

	[HideInInspector]
	public bool debugTutorial;

	[HideInInspector]
	public int debugStartAtStep;

	[HideInInspector]
	public bool debugMeetPrerequisites;

	public abstract TutorialStep[] tutorialSteps { get; set; }

	public abstract bool isGameStateValid { get; }

	public abstract IEnumerator PlayTutorial(int stepIndex);

	public abstract void CleanUp();

	public bool ArePrerequisitesMet()
	{
		if (debugTutorial && debugMeetPrerequisites)
		{
			Debug.LogFormat("TUTORIAL DEBUG: Forcing Tutorial {0} to meet its prerequisites.", tutorialName);
			return true;
		}
		int num = tutorialPrerequisites.Length;
		for (int i = 0; i < num; i++)
		{
			string text = tutorialPrerequisites[i];
			bool flag = text.Contains("!");
			text = text.Replace("!", string.Empty);
			if (flag)
			{
				if (Player.instance.HasTriggered(text))
				{
					return false;
				}
			}
			else if (!Player.instance.HasTriggered(text))
			{
				return false;
			}
		}
		return true;
	}

	public void DebugPrerequisites()
	{
		string text = "-----------------" + tutorialName + " Prerequisites-----------------\n";
		for (int i = 0; i < tutorialPrerequisites.Length; i++)
		{
			string text2 = text;
			text = text2 + "\t " + i + ": " + tutorialPrerequisites[i] + ", Complete? " + Player.instance.HasTriggered(tutorialPrerequisites[i]) + "\n";
		}
		Debug.Log(text);
	}

	public void CompleteTutorial()
	{
		Debug.Log("COMPLETE TUTORIAL: " + tutorialName);
		SaveUtility.SetInt(tutorialName + "_Complete", 1);
		Player.instance.SetTrigger(tutorialName + "_Complete");
	}

	public bool IsComplete()
	{
		return SaveUtility.GetInt(string.Format("{0}_Complete", tutorialName)) == 1 && !debugTutorial;
	}
}
