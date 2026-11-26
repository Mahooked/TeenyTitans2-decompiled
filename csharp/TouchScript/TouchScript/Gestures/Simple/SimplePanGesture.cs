using System;
using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Gestures.Simple;

[AddComponentMenu("TouchScript/Gestures/Simple Pan Gesture")]
public class SimplePanGesture : Transform2DGestureBase
{
	public const string PAN_START_MESSAGE = "OnPanStart";

	public const string PAN_MESSAGE = "OnPan";

	public const string PAN_COMPLETE_MESSAGE = "OnPanComplete";

	private EventHandler<EventArgs> panStartedInvoker;

	private EventHandler<EventArgs> pannedInvoker;

	private EventHandler<EventArgs> panCompletedInvoker;

	[SerializeField]
	private float movementThreshold = 0.5f;

	private Vector2 movementBuffer;

	private bool isMoving;

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

	public Vector3 WorldDeltaPosition { get; private set; }

	public Vector3 LocalDeltaPosition => TransformUtils.GlobalToLocalVector(cachedTransform, WorldDeltaPosition);

	public override Vector2 ScreenPosition
	{
		get
		{
			if (activeTouches.Count < 2)
			{
				return base.ScreenPosition;
			}
			return (activeTouches[0].Position + activeTouches[1].Position) * 0.5f;
		}
	}

	public override Vector2 PreviousScreenPosition
	{
		get
		{
			if (activeTouches.Count < 2)
			{
				return base.PreviousScreenPosition;
			}
			return (activeTouches[0].PreviousPosition + activeTouches[1].PreviousPosition) * 0.5f;
		}
	}

	public event EventHandler<EventArgs> PanStarted
	{
		add
		{
			panStartedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(panStartedInvoker, value);
		}
		remove
		{
			panStartedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(panStartedInvoker, value);
		}
	}

	public event EventHandler<EventArgs> Panned
	{
		add
		{
			pannedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(pannedInvoker, value);
		}
		remove
		{
			pannedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(pannedInvoker, value);
		}
	}

	public event EventHandler<EventArgs> PanCompleted
	{
		add
		{
			panCompletedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(panCompletedInvoker, value);
		}
		remove
		{
			panCompletedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(panCompletedInvoker, value);
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		base.touchesMoved(touches);
		Vector3 vector = Vector3.zero;
		Vector2 previousScreenPosition = PreviousScreenPosition;
		Vector2 screenPosition = ScreenPosition;
		Vector3 vector2;
		Vector3 vector3;
		if (isMoving)
		{
			vector2 = projectionLayer.ProjectTo(previousScreenPosition, WorldTransformPlane);
			vector3 = projectionLayer.ProjectTo(screenPosition, WorldTransformPlane);
			vector = vector3 - vector2;
		}
		else
		{
			movementBuffer += screenPosition - previousScreenPosition;
			float num = MovementThreshold * touchManager.DotsPerCentimeter;
			if (movementBuffer.sqrMagnitude > num * num)
			{
				isMoving = true;
				vector2 = projectionLayer.ProjectTo(previousScreenPosition - movementBuffer, WorldTransformPlane);
				vector3 = projectionLayer.ProjectTo(screenPosition, WorldTransformPlane);
				vector = vector3 - vector2;
			}
			else
			{
				vector3 = projectionLayer.ProjectTo(screenPosition - movementBuffer, WorldTransformPlane);
				vector2 = vector3;
			}
		}
		if (!(vector != Vector3.zero))
		{
			return;
		}
		switch (State)
		{
		case GestureState.Possible:
		case GestureState.Began:
		case GestureState.Changed:
			PreviousWorldTransformCenter = vector2;
			WorldTransformCenter = vector3;
			WorldDeltaPosition = vector;
			if (State == GestureState.Possible)
			{
				setState(GestureState.Began);
			}
			else
			{
				setState(GestureState.Changed);
			}
			break;
		}
	}

	protected override void onBegan()
	{
		base.onBegan();
		panStartedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		pannedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnPanStart", this, SendMessageOptions.DontRequireReceiver);
			SendMessageTarget.SendMessage("OnPan", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onChanged()
	{
		base.onChanged();
		pannedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnPan", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		panCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnPanComplete", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onFailed()
	{
		base.onFailed();
		if (PreviousState != GestureState.Possible)
		{
			panCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
			if (UseSendMessage && SendMessageTarget != null)
			{
				SendMessageTarget.SendMessage("OnPanComplete", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void onCancelled()
	{
		base.onCancelled();
		if (PreviousState != GestureState.Possible)
		{
			panCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
			if (UseSendMessage && SendMessageTarget != null)
			{
				SendMessageTarget.SendMessage("OnPanComplete", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void reset()
	{
		base.reset();
		WorldDeltaPosition = Vector3.zero;
		movementBuffer = Vector2.zero;
		isMoving = false;
	}
}
