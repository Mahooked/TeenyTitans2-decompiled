using System;
using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Gestures.Simple;

[AddComponentMenu("TouchScript/Gestures/Simple Scale Gesture")]
public class SimpleScaleGesture : TwoPointTransform2DGestureBase
{
	public const string SCALE_START_MESSAGE = "OnScaleStart";

	public const string SCALE_MESSAGE = "OnScale";

	public const string SCALE_COMPLETE_MESSAGE = "OnScaleComplete";

	private EventHandler<EventArgs> scaleStartedInvoker;

	private EventHandler<EventArgs> scaledInvoker;

	private EventHandler<EventArgs> scaleCompletedInvoker;

	[SerializeField]
	private float scalingThreshold = 0.5f;

	private float scalingBuffer;

	private bool isScaling;

	public float ScalingThreshold
	{
		get
		{
			return scalingThreshold;
		}
		set
		{
			scalingThreshold = value;
		}
	}

	public float LocalDeltaScale { get; private set; }

	public event EventHandler<EventArgs> ScaleStarted
	{
		add
		{
			scaleStartedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(scaleStartedInvoker, value);
		}
		remove
		{
			scaleStartedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(scaleStartedInvoker, value);
		}
	}

	public event EventHandler<EventArgs> Scaled
	{
		add
		{
			scaledInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(scaledInvoker, value);
		}
		remove
		{
			scaledInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(scaledInvoker, value);
		}
	}

	public event EventHandler<EventArgs> ScaleCompleted
	{
		add
		{
			scaleCompletedInvoker = (EventHandler<EventArgs>)System.Delegate.Combine(scaleCompletedInvoker, value);
		}
		remove
		{
			scaleCompletedInvoker = (EventHandler<EventArgs>)System.Delegate.Remove(scaleCompletedInvoker, value);
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		if (!gotEnoughTouches() || !relevantTouches(touches))
		{
			return;
		}
		float num = 1f;
		Vector2 pointScreenPosition = getPointScreenPosition(0);
		Vector2 pointScreenPosition2 = getPointScreenPosition(1);
		Vector2 vector = pointScreenPosition2 - pointScreenPosition;
		if (vector.sqrMagnitude < minPixelDistanceSquared)
		{
			return;
		}
		base.touchesMoved(touches);
		Vector2 pointPreviousScreenPosition = getPointPreviousScreenPosition(0);
		Vector2 pointPreviousScreenPosition2 = getPointPreviousScreenPosition(1);
		Vector3 b = projectionLayer.ProjectTo(pointPreviousScreenPosition, WorldTransformPlane);
		Vector3 a = projectionLayer.ProjectTo(pointPreviousScreenPosition2, WorldTransformPlane);
		Vector3 vector2 = projectionLayer.ProjectTo(pointScreenPosition, WorldTransformPlane);
		Vector3 vector3 = projectionLayer.ProjectTo(pointScreenPosition2, WorldTransformPlane);
		Vector3 vector4 = vector3 - vector2;
		Vector2 vector5 = (pointPreviousScreenPosition + pointPreviousScreenPosition2) * 0.5f;
		Vector2 vector6 = (pointScreenPosition + pointScreenPosition2) * 0.5f;
		if (isScaling)
		{
			float num2 = Vector3.Distance(a, b);
			num = ((!(num2 > 0f)) ? 1f : (vector4.magnitude / num2));
		}
		else
		{
			float num3 = Vector2.Distance(pointPreviousScreenPosition, pointPreviousScreenPosition2);
			float magnitude = vector.magnitude;
			float num4 = magnitude - num3;
			scalingBuffer += num4;
			float num5 = ScalingThreshold * touchManager.DotsPerCentimeter;
			if (scalingBuffer * scalingBuffer >= num5 * num5)
			{
				isScaling = true;
				Vector2 normalized = (pointPreviousScreenPosition2 - pointPreviousScreenPosition).normalized;
				float num6 = (magnitude - scalingBuffer) * 0.5f;
				Vector2 vector7 = normalized * num6;
				num = vector4.magnitude / (projectionLayer.ProjectTo(vector5 + vector7, WorldTransformPlane) - projectionLayer.ProjectTo(vector5 - vector7, WorldTransformPlane)).magnitude;
			}
		}
		Vector3 previousWorldTransformCenter = projectionLayer.ProjectTo(vector5, WorldTransformPlane);
		Vector3 worldTransformCenter = projectionLayer.ProjectTo(vector6, WorldTransformPlane);
		if (!((double)Mathf.Abs(num - 1f) > 1E-05))
		{
			return;
		}
		switch (State)
		{
		case GestureState.Possible:
		case GestureState.Began:
		case GestureState.Changed:
			screenPosition = vector6;
			previousScreenPosition = vector5;
			PreviousWorldTransformCenter = previousWorldTransformCenter;
			WorldTransformCenter = worldTransformCenter;
			LocalDeltaScale = num;
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
		scaleStartedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		scaledInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnScaleStart", this, SendMessageOptions.DontRequireReceiver);
			SendMessageTarget.SendMessage("OnScale", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onChanged()
	{
		base.onChanged();
		scaledInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnScale", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onRecognized()
	{
		base.onRecognized();
		scaleCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		if (UseSendMessage && SendMessageTarget != null)
		{
			SendMessageTarget.SendMessage("OnScaleComplete", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	protected override void onFailed()
	{
		base.onFailed();
		if (PreviousState != GestureState.Possible)
		{
			scaleCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
			if (UseSendMessage && SendMessageTarget != null)
			{
				SendMessageTarget.SendMessage("OnScaleComplete", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void onCancelled()
	{
		base.onCancelled();
		if (PreviousState != GestureState.Possible)
		{
			scaleCompletedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
			if (UseSendMessage && SendMessageTarget != null)
			{
				SendMessageTarget.SendMessage("OnScaleComplete", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected override void reset()
	{
		base.reset();
		scalingBuffer = 0f;
		isScaling = false;
	}

	protected override void restart()
	{
		base.restart();
		LocalDeltaScale = 1f;
	}
}
