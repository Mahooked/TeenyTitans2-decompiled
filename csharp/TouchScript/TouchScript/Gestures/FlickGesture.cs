using System;
using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Flick Gesture")]
public class FlickGesture : Gesture
{
	public enum GestureDirection
	{
		Any,
		Horizontal,
		Vertical
	}

	public const string FLICK_MESSAGE = "OnFlick";

	private EventHandler<EventArgs> flickedInvoker;

	[SerializeField]
	private float flickTime = 0.1f;

	[SerializeField]
	private float minDistance = 1f;

	[SerializeField]
	private float movementThreshold = 0.5f;

	[SerializeField]
	private GestureDirection direction;

	private bool moving;

	private Vector2 movementBuffer = Vector2.zero;

	private bool isActive;

	private TimedSequence<Vector2> deltaSequence = new TimedSequence<Vector2>();

	public float FlickTime
	{
		get
		{
			return flickTime;
		}
		set
		{
			flickTime = value;
		}
	}

	public float MinDistance
	{
		get
		{
			return minDistance;
		}
		set
		{
			minDistance = value;
		}
	}

	public float MovementThreshold
	{
		get
		{
			return movementThreshold;
		}
		set
		{
			movementThreshold = value;
		}
	}

	public GestureDirection Direction
	{
		get
		{
			return direction;
		}
		set
		{
			direction = value;
		}
	}

	public Vector2 ScreenFlickVector { get; private set; }

	public float ScreenFlickTime { get; private set; }

	public event EventHandler<EventArgs> Flicked
	{
		add
		{
			flickedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(flickedInvoker, value);
		}
		remove
		{
			flickedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(flickedInvoker, value);
		}
	}

	protected void LateUpdate()
	{
		if (isActive)
		{
			deltaSequence.Add(ScreenPosition - PreviousScreenPosition);
		}
	}

	protected override void touchesBegan(IList<ITouch> touches)
	{
		base.touchesBegan(touches);
		if (activeTouches.Count == touches.Count)
		{
			isActive = true;
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		base.touchesMoved(touches);
		if (!moving)
		{
			movementBuffer += ScreenPosition - PreviousScreenPosition;
			float num = MovementThreshold * touchManager.DotsPerCentimeter;
			if (movementBuffer.sqrMagnitude >= num * num)
			{
				moving = true;
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
		isActive = false;
		if (!moving)
		{
			setState(GestureState.Failed);
			return;
		}
		deltaSequence.Add(ScreenPosition - PreviousScreenPosition);
		IList<Vector2> list = deltaSequence.FindElementsLaterThan(Time.time - FlickTime, out var lastTime);
		Vector2 zero = Vector2.zero;
		foreach (Vector2 item in list)
		{
			zero += item;
		}
		switch (Direction)
		{
		case GestureDirection.Horizontal:
			zero.y = 0f;
			break;
		case GestureDirection.Vertical:
			zero.x = 0f;
			break;
		}
		if (zero.magnitude < MinDistance * touchManager.DotsPerCentimeter)
		{
			setState(GestureState.Failed);
			return;
		}
		ScreenFlickVector = zero;
		ScreenFlickTime = Time.time - lastTime;
		setState(GestureState.Ended);
	}

	protected override void touchesCancelled(IList<ITouch> touches)
	{
		base.touchesCancelled(touches);
		touchesEnded(touches);
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		flickedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnFlick", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void reset()
	{
		base.reset();
		isActive = false;
		moving = false;
		movementBuffer = Vector2.zero;
	}
}
