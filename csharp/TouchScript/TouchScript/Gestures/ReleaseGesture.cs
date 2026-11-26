using System;
using System.Collections.Generic;
using TouchScript.Utils;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Release Gesture")]
public class ReleaseGesture : Gesture
{
	public const string RELEASE_MESSAGE = "OnRelease";

	private EventHandler<EventArgs> releasedInvoker;

	[SerializeField]
	[ToggleLeft]
	private bool ignoreChildren;

	public bool IgnoreChildren
	{
		get
		{
			return ignoreChildren;
		}
		set
		{
			ignoreChildren = value;
		}
	}

	public event EventHandler<EventArgs> Released
	{
		add
		{
			releasedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(releasedInvoker, value);
		}
		remove
		{
			releasedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(releasedInvoker, value);
		}
	}

	public override bool ShouldReceiveTouch(ITouch touch)
	{
		if (!IgnoreChildren)
		{
			return base.ShouldReceiveTouch(touch);
		}
		if (!base.ShouldReceiveTouch(touch))
		{
			return false;
		}
		if (touch.Target != cachedTransform)
		{
			return false;
		}
		return true;
	}

	public override bool CanPreventGesture(Gesture gesture)
	{
		if (Delegate == null)
		{
			return false;
		}
		return Delegate.ShouldRecognizeSimultaneously(this, gesture);
	}

	public override bool CanBePreventedByGesture(Gesture gesture)
	{
		if (Delegate == null)
		{
			return false;
		}
		return !Delegate.ShouldRecognizeSimultaneously(this, gesture);
	}

	protected override void touchesEnded(IList<ITouch> touches)
	{
		base.touchesEnded(touches);
		if (activeTouches.Count == 0)
		{
			setState(GestureState.Ended);
		}
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		releasedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnRelease", this, SendMessageOptions.DontRequireReceiver);
		}
	}
}
