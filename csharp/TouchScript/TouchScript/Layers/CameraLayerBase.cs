using TouchScript.Hit;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Layers;

public abstract class CameraLayerBase : TouchLayer
{
	[SerializeField]
	private LayerMask layerMask = -1;

	protected Camera _camera;

	public LayerMask LayerMask
	{
		get
		{
			return layerMask;
		}
		set
		{
			layerMask = value;
		}
	}

	public override Vector3 WorldProjectionNormal
	{
		get
		{
			if (_camera == null)
			{
				return Vector3.forward;
			}
			return _camera.transform.forward;
		}
	}

	public override LayerHitResult Hit(Vector2 position, out ITouchHit hit)
	{
		if (base.Hit(position, out hit) == LayerHitResult.Miss)
		{
			return LayerHitResult.Miss;
		}
		if (_camera == null)
		{
			return LayerHitResult.Error;
		}
		if (!_camera.enabled || !_camera.gameObject.activeInHierarchy)
		{
			return LayerHitResult.Miss;
		}
		if (!_camera.pixelRect.Contains(position))
		{
			return LayerHitResult.Miss;
		}
		Ray ray = _camera.ScreenPointToRay(position);
		return castRay(ray, out hit);
	}

	public override Vector3 ProjectTo(Vector2 screenPosition, Plane projectionPlane)
	{
		return ProjectionUtils.CameraToPlaneProjection(screenPosition, _camera, projectionPlane);
	}

	protected override void Awake()
	{
		updateCamera();
		base.Awake();
	}

	protected override void setName()
	{
		if (string.IsNullOrEmpty(Name) && _camera != null)
		{
			Name = _camera.name;
		}
	}

	protected virtual void updateCamera()
	{
		_camera = GetComponent<Camera>();
		if (_camera == null)
		{
			_camera = Camera.main;
		}
		if (!(_camera != null))
		{
			Debug.LogError("No Camera found for CameraLayer '" + name + "'.");
			enabled = false;
		}
	}

	protected abstract LayerHitResult castRay(Ray ray, out ITouchHit hit);
}
