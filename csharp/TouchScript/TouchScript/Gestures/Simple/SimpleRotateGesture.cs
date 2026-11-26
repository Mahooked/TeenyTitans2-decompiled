using System;
using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Gestures.Simple;

[AddComponentMenu("TouchScript/Gestures/Simple Rotate Gesture")]
public class SimpleRotateGesture : TwoPointTransform2DGestureBase
{
	public const string ROTATE_START_MESSAGE = "OnRotateStart";

	public const string ROTATE_MESSAGE = "OnRotate";

	public const string ROTATE_COMPLETE_MESSAGE = "OnRotateComplete";

	private EventHandler<EventArgs> rotateStartedInvoker;

	private EventHandler<EventArgs> rotatedInvoker;

	private EventHandler<EventArgs> rotateCompletedInvoker;

	[SerializeField]
	private float rotationThreshold = 3f;

	private float rotationBuffer;

	private bool isRotating;

	public float RotationThreshold
	{
		get
		{
			return rotationThreshold;
		}
		set
		{
			rotationThreshold = value;
		}
	}

	public float DeltaRotation { get; private set; }

	public Vector3 RotationAxis => worldTransformPlane.normal;

	public event EventHandler<EventArgs> RotateStarted
	{
		add
		{
			rotateStartedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(rotateStartedInvoker, value);
		}
		remove
		{
			rotateStartedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(rotateStartedInvoker, value);
		}
	}

	public event EventHandler<EventArgs> Rotated
	{
		add
		{
			rotatedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(rotatedInvoker, value);
		}
		remove
		{
			rotatedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(rotatedInvoker, value);
		}
	}

	public event EventHandler<EventArgs> RotateCompleted
	{
		add
		{
			rotateCompletedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(rotateCompletedInvoker, value);
		}
		remove
		{
			rotateCompletedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(rotateCompletedInvoker, value);
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		if (!gotEnoughTouches() || !relevantTouches(touches))
		{
			return;
		}
		float num = 0f;
		Vector2 pointScreenPosition = getPointScreenPosition(0);
		Vector2 pointScreenPosition2 = getPointScreenPosition(1);
		if ((pointScreenPosition2 - pointScreenPosition).sqrMagnitude < minPixelDistanceSquared)
		{
			return;
		}
		base.touchesMoved(touches);
		Vector2 pointPreviousScreenPosition = getPointPreviousScreenPosition(0);
		Vector2 pointPreviousScreenPosition2 = getPointPreviousScreenPosition(1);
		Vector3 vector = projectionLayer.ProjectTo(pointPreviousScreenPosition, WorldTransformPlane);
		Vector3 vector2 = projectionLayer.ProjectTo(pointPreviousScreenPosition2, WorldTransformPlane);
		Vector3 vector3 = projectionLayer.ProjectTo(pointScreenPosition, WorldTransformPlane);
		Vector3 vector4 = projectionLayer.ProjectTo(pointScreenPosition2, WorldTransformPlane);
		Vector3 vector5 = vector4 - vector3;
		Vector3 vector6 = vector2 - vector;
		Vector2 vector7 = (pointPreviousScreenPosition + pointPreviousScreenPosition2) * 0.5f;
		Vector2 vector8 = (pointScreenPosition + pointScreenPosition2) * 0.5f;
		float num2 = Vector3.Angle(vector6, vector5);
		if (Vector3.Dot(Vector3.Cross(vector6, vector5), WorldTransformPlane.normal) < 0f)
		{
			num2 = 0f - num2;
		}
		if (isRotating)
		{
			num = num2;
		}
		else
		{
			rotationBuffer += num2;
			if (rotationBuffer * rotationBuffer >= RotationThreshold * RotationThreshold)
			{
				isRotating = true;
				num = rotationBuffer;
			}
		}
		Vector3 previousWorldTransformCenter = projectionLayer.ProjectTo(vector7, WorldTransformPlane);
		Vector3 worldTransformCenter = projectionLayer.ProjectTo(vector8, WorldTransformPlane);
		if (!((double)Math.Abs(num) > 1E-05))
		{
			return;
		}
		switch (State)
		{
		case GestureState.Possible:
		case GestureState.Began:
		case GestureState.Changed:
			screenPosition = vector8;
			previousScreenPosition = vector7;
			PreviousWorldTransformCenter = previousWorldTransformCenter;
			WorldTransformCenter = worldTransformCenter;
			DeltaRotation = num;
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

	protected override void reset()
	{
		base.reset();
		rotationBuffer = 0f;
		isRotating = false;
	}

	protected override void onBegan()
	{
		base.onBegan();
		rotateStartedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		rotatedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnRotateStart", this, SendMessageOptions.DontRequireReceiver);
			SendMessageTarget.SendMessage("OnRotate", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onChanged()
	{
		base.onChanged();
		rotatedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnRotate", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		rotateCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnRotateComplete", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onFailed()
	{
		base.onFailed();
		if (PreviousState != GestureState.Possible)
		{
			rotateCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
			if (UseSendMessage && SendMessageTarget != null)
			{
				SendMessageTarget.SendMessage("OnRotateComplete", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void onCancelled()
	{
		base.onCancelled();
		if (PreviousState != GestureState.Possible)
		{
			rotateCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
			if (UseSendMessage && SendMessageTarget != null)
			{
				SendMessageTarget.SendMessage("OnRotateComplete", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void restart()
	{
		base.restart();
		DeltaRotation = 0f;
	}
}
