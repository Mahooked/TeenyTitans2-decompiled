using System.Collections.Generic;
using TouchScript.Layers;
using UnityEngine;

namespace TouchScript.Gestures.Simple;

public abstract class Transform2DGestureBase : Gesture
{
	public enum ProjectionType
	{
		Layer,
		Local,
		Global
	}

	[SerializeField]
	private ProjectionType projection;

	[SerializeField]
	private Vector3 projectionNormal = Vector3.forward;

	private Collider cachedCollider;

	protected TouchLayer projectionLayer;

	protected Plane worldTransformPlane;

	public ProjectionType Projection
	{
		get
		{
			return projection;
		}
		set
		{
			if (projection != value)
			{
				projection = value;
				if (Application.isPlaying)
				{
					updateProjectionPlane();
				}
			}
		}
	}

	public Vector3 ProjectionNormal
	{
		get
		{
			if (projection == ProjectionType.Layer)
			{
				return projectionLayer.WorldProjectionNormal;
			}
			return projectionNormal;
		}
		set
		{
			if (projection == ProjectionType.Layer)
			{
				projection = ProjectionType.Local;
			}
			value.Normalize();
			if (!(projectionNormal == value))
			{
				projectionNormal = value;
				if (Application.isPlaying)
				{
					updateProjectionPlane();
				}
			}
		}
	}

	public Vector3 PreviousWorldTransformCenter { get; protected set; }

	public Vector3 WorldTransformCenter { get; protected set; }

	public Plane WorldTransformPlane => worldTransformPlane;

	protected override void Awake()
	{
		base.Awake();
		worldTransformPlane = default;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		cachedCollider = GetComponent<Collider>();
		updateProjectionPlane();
	}

	protected override void touchesBegan(IList<ITouch> touches)
	{
		base.touchesMoved(touches);
		if (touches.Count == activeTouches.Count)
		{
			projectionLayer = activeTouches[0].Layer;
			updateProjectionPlane();
		}
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		base.touchesMoved(touches);
		updateProjectionPlane();
	}

	protected override void touchesEnded(IList<ITouch> touches)
	{
		base.touchesEnded(touches);
		if (activeTouches.Count == 0)
		{
			GestureState gestureState = State;
			if (gestureState == GestureState.Began || gestureState == GestureState.Changed)
			{
				setState(GestureState.Ended);
			}
		}
	}

	protected override void touchesCancelled(IList<ITouch> touches)
	{
		base.touchesCancelled(touches);
		touchesEnded(touches);
	}

	protected override void reset()
	{
		base.reset();
		WorldTransformCenter = TouchManager.INVALID_POSITION;
		PreviousWorldTransformCenter = TouchManager.INVALID_POSITION;
	}

	protected void updateProjectionPlane()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Vector3 inPoint = ((!(cachedCollider != null)) ? cachedTransform.position : cachedCollider.bounds.center);
		switch (projection)
		{
		case ProjectionType.Layer:
			if (projectionLayer == null)
			{
				worldTransformPlane = new Plane(cachedTransform.TransformDirection(Vector3.forward), inPoint);
			}
			else
			{
				worldTransformPlane = new Plane(projectionLayer.WorldProjectionNormal, inPoint);
			}
			break;
		case ProjectionType.Local:
			worldTransformPlane = new Plane(cachedTransform.TransformDirection(projectionNormal), inPoint);
			break;
		case ProjectionType.Global:
			worldTransformPlane = new Plane(projectionNormal, inPoint);
			break;
		}
	}
}
