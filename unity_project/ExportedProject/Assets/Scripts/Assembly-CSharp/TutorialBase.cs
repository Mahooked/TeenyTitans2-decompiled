using System.Collections;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
	private static TutorialBase tutorialRunning;

	public string triggerName = string.Empty;

	public string[] triggersRequired;

	private Player player;

	private void Awake()
	{
		player = Player.instance;
		if (player.HasTriggered(triggerName))
		{
			Object.Destroy(this);
		}
	}

	private void OnDestroy()
	{
		if (player != null)
		{
			player.TriggerSet -= HandleTriggerSet;
		}
	}

	private IEnumerator Start()
	{
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		if (tutorialRunning != null)
		{
			StartCoroutine(WaitInLine());
			yield break;
		}
		int len = triggersRequired.Length;
		for (int i = 0; i < len; i++)
		{
			if (!player.HasTriggered(triggersRequired[i]))
			{
				player.TriggerSet += HandleTriggerSet;
				yield break;
			}
		}
		if (CanRunTutorial())
		{
			StartCoroutine(WaitForTutorial());
		}
	}

	private IEnumerator WaitForTutorial()
	{
		tutorialRunning = this;
		player.isSavingEnabled = false;
		yield return StartCoroutine(RunTutorial());
		player.SetTrigger(triggerName);
		player.isSavingEnabled = true;
		player.Save();
		tutorialRunning = null;
	}

	private IEnumerator WaitInLine()
	{
		while (tutorialRunning != null)
		{
			yield return null;
		}
		StartCoroutine(Start());
	}

	private void HandleTriggerSet(string obj)
	{
		player.TriggerSet -= HandleTriggerSet;
		StartCoroutine(Start());
	}

	protected abstract bool CanRunTutorial();

	protected abstract IEnumerator RunTutorial();
}
