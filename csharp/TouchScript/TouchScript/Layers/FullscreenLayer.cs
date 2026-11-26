using TouchScript.Hit;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Layers;

[AddComponentMenu("TouchScript/Layers/Fullscreen Layer")]
public sealed class FullscreenLayer : TouchLayer
{
	public enum LayerType
	{
		MainCamera,
		Camera,
		Global
	}

	[SerializeField]
	private LayerType type;

	[SerializeField]
	private Camera _camera;

	private Transform cameraTransform;

	public LayerType Type
	{
		get
		{
			return type;
		}
		set
		{
			if (value != type)
			{
				type = value;
				updateCamera();
				cacheCameraTransform();
			}
		}
	}

	public Camera Camera
	{
		get
		{
			return _camera;
		}
		set
		{
			if (!(value == _camera))
			{
				_camera = value;
				if (_camera == null)
				{
					Type = LayerType.Global;
				}
				else
				{
					Type = LayerType.Camera;
				}
				setName();
			}
		}
	}

	public override Vector3 WorldProjectionNormal
	{
		get
		{
			if (cameraTransform == null)
			{
				return transform.forward;
			}
			return cameraTransform.forward;
		}
	}

	public override LayerHitResult Hit(Vector2 position, out ITouchHit hit)
	{
		if (base.Hit(position, out hit) == LayerHitResult.Miss)
		{
			return LayerHitResult.Miss;
		}
		if (_camera != null && !_camera.pixelRect.Contains(position))
		{
			return LayerHitResult.Miss;
		}
		hit = TouchHitFactory.Instance.GetTouchHit(transform);
		HitTest[] components = transform.GetComponents<HitTest>();
		if (components.Length == 0)
		{
			return LayerHitResult.Hit;
		}
		HitTest[] array = components;
		foreach (HitTest hitTest in array)
		{
			if (hitTest.enabled)
			{
				HitTest.ObjectHitResult objectHitResult = hitTest.IsHit(hit);
				if (objectHitResult == HitTest.ObjectHitResult.Miss || objectHitResult == HitTest.ObjectHitResult.Discard)
				{
					return LayerHitResult.Miss;
				}
			}
		}
		return LayerHitResult.Hit;
	}

	public override Vector3 ProjectTo(Vector2 screenPosition, Plane projectionPlane)
	{
		if (_camera == null)
		{
			return base.ProjectTo(screenPosition, projectionPlane);
		}
		return ProjectionUtils.CameraToPlaneProjection(screenPosition, _camera, projectionPlane);
	}

	protected override void Awake()
	{
		updateCamera();
		cacheCameraTransform();
		base.Awake();
	}

	private void OnEnable()
	{
	}

	protected override void setName()
	{
		if (_camera == null)
		{
			Name = "Global Fullscreen";
		}
		else
		{
			Name = "Fullscreen @ " + _camera.name;
		}
	}

	private void updateCamera()
	{
		switch (type)
		{
		case LayerType.Global:
			_camera = null;
			break;
		case LayerType.MainCamera:
			_camera = Camera.main;
			if (_camera == null)
			{
				Debug.LogError("No Main camera found!");
			}
			break;
		}
		setName();
	}

	private void cacheCameraTransform()
	{
		if (_camera == null)
		{
			cameraTransform = null;
		}
		else
		{
			cameraTransform = _camera.transform;
		}
	}
}
