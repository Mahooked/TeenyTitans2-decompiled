using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Utils;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Long Press Gesture")]
public class LongPressGesture : Gesture
{
	public const string LONG_PRESS_MESSAGE = "OnLongPress";

	private EventHandler<EventArgs> longPressedInvoker;

	[SerializeField]
	[NullToggle(NullIntValue = int.MaxValue)]
	private int maxTouches = int.MaxValue;

	[SerializeField]
	private float timeToPress = 1f;

	[NullToggle(NullFloatValue = float.PositiveInfinity)]
	[SerializeField]
	private float distanceLimit = float.PositiveInfinity;

	private float distanceLimitInPixelsSquared;

	private Vector2 totalMovement;

	public int MaxTouches
	{
		get
		{
			return maxTouches;
		}
		set
		{
			maxTouches = value;
		}
	}

	public float TimeToPress
	{
		get
		{
			return timeToPress;
		}
		set
		{
			timeToPress = value;
		}
	}

	public float DistanceLimit
	{
		get
		{
			return distanceLimit;
		}
		set
		{
			distanceLimit = value;
			distanceLimitInPixelsSquared = Mathf.Pow(distanceLimit * touchManager.DotsPerCentimeter, 2f);
		}
	}

	public event EventHandler<EventArgs> LongPressed
	{
		add
		{
			longPressedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(longPressedInvoker, value);
		}
		remove
		{
			longPressedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(longPressedInvoker, value);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		distanceLimitInPixelsSquared = Mathf.Pow(distanceLimit * touchManager.DotsPerCentimeter, 2f);
	}

	protected override void touchesBegan(IList<ITouch> touches)
	{
		base.touchesBegan(touches);
		if (activeTouches.Count > MaxTouches)
		{
			setState(GestureState.Failed);
		}
		else if (activeTouches.Count == touches.Count)
		{
			StartCoroutine("wait");
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		base.touchesMoved(touches);
		if (distanceLimit < float.PositiveInfinity)
		{
			totalMovement += ScreenPosition - PreviousScreenPosition;
			if (totalMovement.sqrMagnitude > distanceLimitInPixelsSquared)
			{
				setState(GestureState.Failed);
			}
		}
	}

	protected override void touchesEnded(IList<ITouch> touches)
	{
		base.touchesEnded(touches);
		if (activeTouches.Count == 0)
		{
			StopCoroutine("wait");
			setState(GestureState.Failed);
		}
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		longPressedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnLongPress", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void reset()
	{
		base.reset();
		totalMovement = Vector2.zero;
		StopCoroutine("wait");
	}

	private IEnumerator wait()
	{
		float targetTime = Time.unscaledTime + TimeToPress;
		while (targetTime > Time.unscaledTime)
		{
			yield return null;
		}
		if (State == GestureState.Possible)
		{
			if (base.GetTargetHitResult())
			{
				setState(GestureState.Ended);
			}
			else
			{
				setState(GestureState.Failed);
			}
		}
	}
}
