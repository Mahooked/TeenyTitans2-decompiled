using System;
using UnityEngine;

public class AnimatorEventListener : MonoBehaviour
{
	public event Action<AnimationEvent> EventTriggered;

	private void HandleEvent(AnimationEvent evt)
	{
		if (EventTriggered != null)
		{
			EventTriggered(evt);
		}
	}

	private void PlaySound(AnimationEvent evt)
	{
		GFAudioManager.instance.PlayClipFromResources(evt.stringParameter, GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void PlayRandomSound(AnimationEvent evt)
	{
		int num = UnityEngine.Random.Range((int)evt.floatParameter, evt.intParameter + 1);
		string clipPath = evt.stringParameter + num;
		GFAudioManager.instance.PlayClipFromResources(clipPath, GFAudioManager.SFX_GROUP, string.Empty);
	}
}
