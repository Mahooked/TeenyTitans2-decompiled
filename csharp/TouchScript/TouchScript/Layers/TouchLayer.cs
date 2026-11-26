using System;
using System.Collections;
using TouchScript.Hit;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Layers;

[ExecuteInEditMode]
public abstract class TouchLayer : MonoBehaviour
{
	public enum LayerHitResult
	{
		Error,
		Hit,
		Miss
	}

	private EventHandler<TouchLayerEventArgs> touchBeganInvoker;

	public string Name;

	public virtual Vector3 WorldProjectionNormal => transform.forward;

	public event EventHandler<TouchLayerEventArgs> TouchBegan
	{
		add
		{
			touchBeganInvoker = (EventHandler<TouchLayerEventArgs>)Delegate.Combine(touchBeganInvoker, value);
		}
		remove
		{
			touchBeganInvoker = (EventHandler<TouchLayerEventArgs>)Delegate.Remove(touchBeganInvoker, value);
		}
	}

	public virtual LayerHitResult Hit(Vector2 position, out ITouchHit hit)
	{
		hit = null;
		if (!enabled || !gameObject.activeInHierarchy)
		{
			return LayerHitResult.Miss;
		}
		return LayerHitResult.Error;
	}

	public virtual Vector3 ProjectTo(Vector2 screenPosition, Plane projectionPlane)
	{
		return ProjectionUtils.ScreenToPlaneProjection(screenPosition, projectionPlane);
	}

	protected virtual void Awake()
	{
		setName();
		if (Application.isPlaying)
		{
			StartCoroutine(lateAwake());
		}
	}

	protected virtual void OnDestroy()
	{
		if (Application.isPlaying && TouchManager.Instance != null)
		{
			TouchManager.Instance.RemoveLayer(this);
		}
	}

	internal bool BeginTouch(TouchPoint touch)
	{
		LayerHitResult layerHitResult = beginTouch(touch, out var hit);
		if (layerHitResult == LayerHitResult.Hit)
		{
			touch.Layer = this;
			touch.Hit = hit;
			if (hit != null)
			{
				touch.Target = hit.Transform;
			}
			touchBeganInvoker.InvokeHandleExceptions(this, new TouchLayerEventArgs(touch));
			return true;
		}
		return false;
	}

	internal void UpdateTouch(ITouch touch)
	{
		updateTouch(touch);
	}

	internal void EndTouch(ITouch touch)
	{
		endTouch(touch);
	}

	internal void CancelTouch(ITouch touch)
	{
		cancelTouch(touch);
	}

	protected virtual void setName()
	{
		Name = "undefined";
	}

	protected virtual LayerHitResult beginTouch(ITouch touch, out ITouchHit hit)
	{
		return Hit(touch.Position, out hit);
	}

	protected virtual void updateTouch(ITouch touch)
	{
	}

	protected virtual void endTouch(ITouch touch)
	{
	}

	protected virtual void cancelTouch(ITouch touch)
	{
	}

	private IEnumerator lateAwake()
	{
		yield return new WaitForEndOfFrame();
		TouchManager.Instance.AddLayer(this);
	}
}
