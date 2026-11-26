using System.Collections;
using UnityEngine;

public class CommunicatorEventListener : MonoBehaviour
{
	public string listenForEventName;

	public string playAnimation;

	public string queueAnimation;

	public bool replayOnStart;

	public GameObject enableObject;

	public Animator animator;

	public AEControllerTk2d animationController;

	private bool isSetup;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		Setup();
	}

	private void OnEnable()
	{
		Setup();
		if (animationController != null && SaveUtility.GetInt(listenForEventName) == 1 && replayOnStart)
		{
			PlayAnimationForEventName(listenForEventName, save: false);
		}
	}

	private void OnDestroy()
	{
		if (CommunicatorManager.instance != null)
		{
			CommunicatorManager.instance.AnimationEvent -= HandleAnimationEvent;
			isSetup = false;
		}
	}

	private void Setup()
	{
		if (!isSetup)
		{
			isSetup = true;
			CommunicatorManager.instance.AnimationEvent += HandleAnimationEvent;
			if (enableObject != null)
			{
				enableObject.SetActive(value: false);
			}
			if (SaveUtility.GetInt(listenForEventName) == 1 && replayOnStart)
			{
				PlayAnimationForEventName(listenForEventName, save: false);
			}
		}
	}

	private void HandleAnimationEvent(string eventName)
	{
		PlayAnimationForEventName(eventName);
	}

	private void PlayAnimationForEventName(string eventName, bool save = true)
	{
		if (!listenForEventName.Equals(eventName))
		{
			return;
		}
		if (save)
		{
			SaveUtility.SetInt(listenForEventName, 1);
		}
		if (animator != null)
		{
			if (replayOnStart && !save)
			{
				animator.Play(playAnimation, 0, 1f);
			}
			else
			{
				animator.Play(playAnimation);
			}
		}
		if (animationController != null)
		{
			if (!replayOnStart && !save && !string.IsNullOrEmpty(queueAnimation))
			{
				animationController.PlayFromFrame(queueAnimation, animationController.GetClipByName(queueAnimation).animation.totalFrames - 1, clearQueue: true);
			}
			else
			{
				animationController.PlayFromFrame(playAnimation, 0, clearQueue: true);
				if (!string.IsNullOrEmpty(queueAnimation))
				{
					animationController.AddToQueue(queueAnimation);
				}
			}
		}
		if (enableObject != null)
		{
			enableObject.SetActive(value: true);
		}
	}
}
