using System;
using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Meta Gesture")]
public sealed class MetaGesture : Gesture
{
	public const string TOUCH_BEGAN_MESSAGE = "OnTouchBegan";

	public const string TOUCH_MOVED_MESSAGE = "OnTouchMoved";

	public const string TOUCH_ENDED_MESSAGE = "OnTouchEnded";

	public const string TOUCH_CANCELLED_MESSAGE = "OnTouchCancelled";

	private EventHandler<MetaGestureEventArgs> touchBeganInvoker;

	private EventHandler<MetaGestureEventArgs> touchMovedInvoker;

	private EventHandler<MetaGestureEventArgs> touchEndedInvoker;

	private EventHandler<MetaGestureEventArgs> touchCancelledInvoker;

	public event EventHandler<MetaGestureEventArgs> TouchBegan
	{
		add
		{
			touchBeganInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Combine(touchBeganInvoker, value);
		}
		remove
		{
			touchBeganInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Remove(touchBeganInvoker, value);
		}
	}

	public event EventHandler<MetaGestureEventArgs> TouchMoved
	{
		add
		{
			touchMovedInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Combine(touchMovedInvoker, value);
		}
		remove
		{
			touchMovedInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Remove(touchMovedInvoker, value);
		}
	}

	public event EventHandler<MetaGestureEventArgs> TouchEnded
	{
		add
		{
			touchEndedInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Combine(touchEndedInvoker, value);
		}
		remove
		{
			touchEndedInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Remove(touchEndedInvoker, value);
		}
	}

	public event EventHandler<MetaGestureEventArgs> TouchCancelled
	{
		add
		{
			touchCancelledInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Combine(touchCancelledInvoker, value);
		}
		remove
		{
			touchCancelledInvoker = (EventHandler<MetaGestureEventArgs>)System.Delegate.Remove(touchCancelledInvoker, value);
		}
	}

	protected override void touchesBegan(IList<ITouch> touches)
	{
		base.touchesBegan(touches);
		if (State == GestureState.Possible)
		{
			setState(GestureState.Began);
		}
		int count = touches.Count;
		for (int i = 0; i < count; i++)
		{
			touchBeganInvoker.InvokeHandleExceptions(this, new MetaGestureEventArgs(touches[i]));
		}
		if (UseSendMessage && SendMessageTarget != null)
		{
			for (int j = 0; j < count; j++)
			{
				SendMessageTarget.SendMessage("OnTouchBegan", touches[j], SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		base.touchesMoved(touches);
		if (State == GestureState.Began || State == GestureState.Changed)
		{
			setState(GestureState.Changed);
		}
		int count = touches.Count;
		for (int i = 0; i < count; i++)
		{
			touchMovedInvoker.InvokeHandleExceptions(this, new MetaGestureEventArgs(touches[i]));
		}
		if (UseSendMessage && SendMessageTarget != null)
		{
			for (int j = 0; j < count; j++)
			{
				SendMessageTarget.SendMessage("OnTouchMoved", touches[j], SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void touchesEnded(IList<ITouch> touches)
	{
		base.touchesEnded(touches);
		if ((State == GestureState.Began || State == GestureState.Changed) && activeTouches.Count == 0)
		{
			setState(GestureState.Ended);
		}
		int count = touches.Count;
		for (int i = 0; i < count; i++)
		{
			touchEndedInvoker.InvokeHandleExceptions(this, new MetaGestureEventArgs(touches[i]));
		}
		if (UseSendMessage && SendMessageTarget != null)
		{
			for (int j = 0; j < count; j++)
			{
				SendMessageTarget.SendMessage("OnTouchEnded", touches[j], SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void touchesCancelled(IList<ITouch> touches)
	{
		base.touchesCancelled(touches);
		if ((State == GestureState.Began || State == GestureState.Changed) && activeTouches.Count == 0)
		{
			setState(GestureState.Ended);
		}
		int count = touches.Count;
		for (int i = 0; i < count; i++)
		{
			touchCancelledInvoker.InvokeHandleExceptions(this, new MetaGestureEventArgs(touches[i]));
		}
		if (UseSendMessage && SendMessageTarget != null)
		{
			for (int j = 0; j < count; j++)
			{
				SendMessageTarget.SendMessage("OnTouchCancelled", touches[j], SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
