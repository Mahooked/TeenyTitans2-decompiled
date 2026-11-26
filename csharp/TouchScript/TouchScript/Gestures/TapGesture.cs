using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Utils;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Tap Gesture")]
public class TapGesture : Gesture
{
	public const string TAP_MESSAGE = "OnTap";

	private EventHandler<EventArgs> tappedInvoker;

	[SerializeField]
	private int numberOfTapsRequired = 1;

	[NullToggle(NullFloatValue = float.PositiveInfinity)]
	[SerializeField]
	private float timeLimit = float.PositiveInfinity;

	[NullToggle(NullFloatValue = float.PositiveInfinity)]
	[SerializeField]
	private float distanceLimit = float.PositiveInfinity;

	private float distanceLimitInPixelsSquared;

	private int tapsDone;

	private Vector2 startPosition;

	private Vector2 totalMovement;

	public int NumberOfTapsRequired
	{
		get
		{
			return numberOfTapsRequired;
		}
		set
		{
			if (value <= 0)
			{
				numberOfTapsRequired = 1;
			}
			else
			{
				numberOfTapsRequired = value;
			}
		}
	}

	public float TimeLimit
	{
		get
		{
			return timeLimit;
		}
		set
		{
			timeLimit = value;
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

	public event EventHandler<EventArgs> Tapped
	{
		add
		{
			tappedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(tappedInvoker, value);
		}
		remove
		{
			tappedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(tappedInvoker, value);
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
		if (activeTouches.Count != touches.Count)
		{
			return;
		}
		if (tapsDone == 0)
		{
			startPosition = touches[0].Position;
			if (timeLimit < float.PositiveInfinity)
			{
				StartCoroutine("wait");
			}
		}
		else if (tapsDone >= numberOfTapsRequired)
		{
			setState(GestureState.Possible);
			reset();
			startPosition = touches[0].Position;
			if (timeLimit < float.PositiveInfinity)
			{
				StartCoroutine("wait");
			}
		}
		else if (distanceLimit < float.PositiveInfinity && (touches[0].Position - startPosition).sqrMagnitude > distanceLimitInPixelsSquared)
		{
			setState(GestureState.Failed);
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
		if (activeTouches.Count != 0)
		{
			return;
		}
		if (TouchManager.IsInvalidPosition(ScreenPosition))
		{
			setState(GestureState.Failed);
			return;
		}
		tapsDone++;
		if (tapsDone >= numberOfTapsRequired)
		{
			setState(GestureState.Ended);
		}
	}

	protected override void touchesCancelled(IList<ITouch> touches)
	{
		base.touchesCancelled(touches);
		setState(GestureState.Failed);
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		StopCoroutine("wait");
		tappedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnTap", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void reset()
	{
		base.reset();
		totalMovement = Vector2.zero;
		StopCoroutine("wait");
		tapsDone = 0;
	}

	protected override bool shouldCacheTouchPosition(ITouch value)
	{
		return GetTargetHitResult(value.Position);
	}

	private IEnumerator wait()
	{
		float targetTime = Time.unscaledTime + TimeLimit;
		while (targetTime > Time.unscaledTime)
		{
			yield return null;
		}
		if (State == GestureState.Possible)
		{
			setState(GestureState.Failed);
		}
	}
}
