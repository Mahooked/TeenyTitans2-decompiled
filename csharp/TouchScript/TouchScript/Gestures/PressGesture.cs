using System;
using System.Collections.Generic;
using TouchScript.Utils;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Press Gesture")]
public class PressGesture : Gesture
{
	public const string PRESS_MESSAGE = "OnPress";

	private EventHandler<EventArgs> pressedInvoker;

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

	public event EventHandler<EventArgs> Pressed
	{
		add
		{
			pressedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(pressedInvoker, value);
		}
		remove
		{
			pressedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(pressedInvoker, value);
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

	protected override void touchesBegan(IList<ITouch> touches)
	{
		base.touchesBegan(touches);
		if (activeTouches.Count == touches.Count)
		{
			setState(GestureState.Ended);
		}
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		pressedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnPress", this, SendMessageOptions.DontRequireReceiver);
		}
	}
}
