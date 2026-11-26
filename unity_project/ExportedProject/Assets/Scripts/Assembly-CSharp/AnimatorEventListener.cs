using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class AnimatorEventListener : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AnimationEvent> EventTriggered__BackingField;

	public event Action<AnimationEvent> EventTriggered
	{
		add
		{
			Action<AnimationEvent> action = EventTriggered__BackingField;
			Action<AnimationEvent> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref EventTriggered__BackingField, (Action<AnimationEvent>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AnimationEvent> action = EventTriggered__BackingField;
			Action<AnimationEvent> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref EventTriggered__BackingField, (Action<AnimationEvent>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void HandleEvent(AnimationEvent evt)
	{
		if (EventTriggered__BackingField != null)
		{
			EventTriggered__BackingField(evt);
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
