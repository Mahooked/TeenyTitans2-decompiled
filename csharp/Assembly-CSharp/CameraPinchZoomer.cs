using System;
using System.Collections.Generic;
using DG.Tweening;
using TouchScript;
using TouchScript.Gestures;
using UnityEngine;

[DisallowMultipleComponent]
public class CameraPinchZoomer : MonoBehaviour
{
	public Camera thisCamera;

	public Transform cameraTransform;

	public ScaleGesture scaleGesture;

	public PanGesture panGesture;

	public TapGesture doubleTapGesture;

	public TapGesture singleTapGesture;

	public float zoomFactor = 1f;

	public float approachFactor = 9f;

	public float maxZDistance = -12f;

	public float dampening = 0.15f;

	public float swipeTimeRange = 0.2f;

	public float swipeDistance = 0.3f;

	public float defaultZoomedOutDistance = 50f;

	public float zoomedInMinDistance = -10f;

	public float zoomedInMaxDistance = -14f;

	public float zoomedOutMinDistance = -30f;

	public float zoomedOutMaxDistance = -70f;

	public float panMinX = -13f;

	public float panMaxX = 22.5f;

	public float panMinZ = -28f;

	public float panMaxZ = 16f;

	public Vector3 zoomedOutRotation = new Vector3(75f, 0f, 0f);

	private CameraFollow _cameraFollow;

	public bool snapBackToDefaultZoomLevel;

	private Vector3 homePosition;

	private Vector3 homeRotation;

	private Vector3 defaultZoomOutPosition;

	private float zoomDelta;

	private Vector3 panDelta;

	private List<float> deltas;

	private List<float> timestamps;

	private Tweener zoomPositionTween;

	private Tweener zoomRotateTween;

	private bool zoomStarted;

	private float tolerance = 0.02f;

	public Vector3 targetOffset = Vector3.zero;

	private bool underInteria;

	private float smoothTime;

	private float smoothDuration = 1f;

	private float smoothDampTime;

	private float lastPanTime;

	private bool _isZoomedOut;

	private bool _allowPinchZoom = true;

	private bool _allowPan = true;

	private bool _allowDoubleClickZoomIn = true;

	private bool _allowInteraction = true;

	private Vector3 _targetPosition;

	public CameraFollow cameraFollow => _cameraFollow;

	public Vector3 HomeRotation
	{
		get
		{
			return homeRotation;
		}
		set
		{
			homeRotation = value;
			if (!_isZoomedOut)
			{
				transform.DOLocalRotate(homeRotation, 0.4f).SetEase(Ease.OutSine);
			}
		}
	}

	public bool isZoomedOut => _isZoomedOut;

	public bool allowPinchZoom
	{
		get
		{
			return _allowPinchZoom;
		}
		set
		{
			_allowPinchZoom = value;
		}
	}

	public bool allowPan
	{
		get
		{
			return _allowPan;
		}
		set
		{
			_allowPan = value;
		}
	}

	public bool allowDoubleClickZoomIn
	{
		get
		{
			return _allowDoubleClickZoomIn;
		}
		set
		{
			_allowDoubleClickZoomIn = value;
		}
	}

	public bool allowInteraction
	{
		get
		{
			return _allowInteraction;
		}
		set
		{
			_allowInteraction = value;
		}
	}

	public Vector3 targetPosition
	{
		get
		{
			return _targetPosition;
		}
		set
		{
			_targetPosition = value;
		}
	}

	public event Action ZoomStarted;

	public event Action ZoomFinished;

	public event Action ZoomedOut;

	public event Action ZoomedIn;

	public event Action ZoomedOutMoveFinished;

	public event Action ZoomedInMoveFinished;

	public event Action CamReachedTargetPos;

	public event Action<Vector2> SingleTap;

	private void Awake()
	{
		homePosition = (_targetPosition = cameraTransform.localPosition);
		homeRotation = transform.localRotation.eulerAngles;
		if (defaultZoomedOutDistance < 0f)
		{
			defaultZoomedOutDistance = 0f - defaultZoomedOutDistance;
		}
		defaultZoomOutPosition = homePosition;
		defaultZoomOutPosition.z = 0f - defaultZoomedOutDistance;
		deltas = new List<float>();
		timestamps = new List<float>();
		_cameraFollow = GetComponent<CameraFollow>();
		TapGesture[] componentsInChildren = GetComponentsInChildren<TapGesture>();
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			if (componentsInChildren[i].NumberOfTapsRequired == 2)
			{
				doubleTapGesture = componentsInChildren[i];
			}
			else if (componentsInChildren[i].NumberOfTapsRequired == 1)
			{
				singleTapGesture = componentsInChildren[i];
			}
		}
	}

	private void OnEnable()
	{
		scaleGesture.ScaleStarted += HandleScaleStarted;
		scaleGesture.Scaled += HandleScaled;
		scaleGesture.ScaleCompleted += HandleScaleCompleted;
	}

	private void OnDisable()
	{
		scaleGesture.ScaleStarted -= HandleScaleStarted;
		scaleGesture.Scaled -= HandleScaled;
		scaleGesture.ScaleCompleted -= HandleScaleCompleted;
	}

	private void OnDestroy()
	{
		ClearEvents();
	}

	private void ClearEvents()
	{
		ZoomStarted = null;
		ZoomFinished = null;
		ZoomedOut = null;
		ZoomedIn = null;
		ZoomedOutMoveFinished = null;
		ZoomedInMoveFinished = null;
		CamReachedTargetPos = null;
		SingleTap = null;
	}

	private void Update()
	{
		if (!scaleGesture.enabled && Input.touchCount == 0)
		{
			Debug.Log("ENABLED SCALE AND PAN GESTURES");
			scaleGesture.enabled = true;
			panGesture.enabled = true;
		}
	}

	private void LateUpdate()
	{
		if (cameraTransform.localPosition != _targetPosition + targetOffset)
		{
			zoomStarted = Vector3.Distance(cameraTransform.localPosition, _targetPosition + targetOffset) > tolerance;
			cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _targetPosition + targetOffset, Time.deltaTime * approachFactor);
		}
		if (zoomStarted && Vector3.Distance(cameraTransform.localPosition, _targetPosition + targetOffset) <= tolerance)
		{
			zoomStarted = false;
			if (CamReachedTargetPos != null)
			{
				CamReachedTargetPos();
			}
		}
		if (underInteria && smoothTime <= smoothDuration)
		{
			smoothTime += Time.smoothDeltaTime;
			smoothDampTime += Time.smoothDeltaTime / smoothDuration;
			Vector3 position = transform.position;
			position.x += 0f - panDelta.x;
			position.z += 0f - panDelta.z;
			position.x = Mathf.Clamp(position.x, panMinX, panMaxX);
			position.z = Mathf.Clamp(position.z, panMinZ, panMaxZ);
			transform.position = position;
			panDelta = Vector3.Lerp(panDelta, Vector3.zero, Mathf.SmoothStep(0f, 1f, smoothDampTime));
		}
		else
		{
			underInteria = false;
			smoothTime = 0f;
			smoothDampTime = 0f;
		}
	}

	private void ToggleZoom()
	{
		if (_isZoomedOut)
		{
			ZoomIn();
		}
		else
		{
			ZoomOut();
		}
	}

	public void ZoomIn(bool playSFX = true)
	{
		ZoomIn(0f - homePosition.z, playSFX);
	}

	public void ZoomIn(float zoomInDistance, bool playSFX = true)
	{
		if (_isZoomedOut)
		{
			if (playSFX)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_map_zoom-in", GFAudioManager.SFX_GROUP, string.Empty);
			}
			transform.DOKill();
			_targetPosition.z = 0f - zoomInDistance;
			_isZoomedOut = false;
			zoomFactor /= 2f;
			if (zoomPositionTween != null)
			{
				zoomPositionTween.Kill();
				zoomPositionTween = null;
			}
			if (zoomRotateTween != null)
			{
				zoomRotateTween.Kill();
			}
			zoomRotateTween = transform.DOLocalRotate(homeRotation, 0.4f).SetEase(Ease.OutSine).OnComplete(OnZoomedInMoveFinished);
			panGesture.PanStarted -= HandlePanStarted;
			panGesture.Panned -= HandlePanned;
			panGesture.PanCompleted -= HandlePanCompleted;
			if (doubleTapGesture != null)
			{
				doubleTapGesture.Tapped -= HandleDoubleTapped;
			}
			if (singleTapGesture != null)
			{
				singleTapGesture.Tapped -= HandleSingleTapped;
			}
			if (_cameraFollow != null)
			{
				_cameraFollow.enabled = true;
			}
			OnZoomedIn();
		}
	}

	public void ZoomOut(bool playSFX = true)
	{
		ZoomOut(0f - defaultZoomOutPosition.z, playSFX);
	}

	public void ZoomOut(float zoomOutDistance, bool playSFX = true)
	{
		if (!_isZoomedOut)
		{
			if (playSFX)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_map_zoom-out", GFAudioManager.SFX_GROUP, string.Empty);
			}
			_targetPosition.z = 0f - zoomOutDistance;
			_isZoomedOut = true;
			zoomFactor *= 2f;
			if (zoomPositionTween != null)
			{
				zoomPositionTween.Kill();
			}
			if (zoomRotateTween != null)
			{
				zoomRotateTween.Kill();
			}
			Vector3 position = transform.position;
			position.x = Mathf.Clamp(position.x, panMinX, panMaxX);
			position.z = Mathf.Clamp(position.z, panMinZ, panMaxZ);
			zoomPositionTween = transform.DOMove(position, 0.4f).SetEase(Ease.OutSine);
			zoomRotateTween = transform.DOLocalRotate(zoomedOutRotation, 0.4f).SetEase(Ease.OutSine).OnComplete(OnZoomedOutMoveFinished);
			panGesture.PanStarted += HandlePanStarted;
			panGesture.Panned += HandlePanned;
			panGesture.PanCompleted += HandlePanCompleted;
			if (doubleTapGesture != null)
			{
				doubleTapGesture.Tapped += HandleDoubleTapped;
			}
			if (singleTapGesture != null)
			{
				singleTapGesture.Tapped += HandleSingleTapped;
			}
			if (_cameraFollow != null)
			{
				_cameraFollow.enabled = false;
			}
			OnZoomedOut();
		}
	}

	private void HandleDoubleTapped(object sender, EventArgs e)
	{
		if (_allowInteraction && _allowDoubleClickZoomIn)
		{
			ZoomIn();
		}
	}

	private void HandleSingleTapped(object sender, EventArgs e)
	{
		if (_allowInteraction)
		{
			OnSingleTap(singleTapGesture.ScreenPosition);
		}
	}

	private void HandleScaleStarted(object sender, EventArgs e)
	{
		if (_allowInteraction && _allowPinchZoom)
		{
			zoomDelta = 0f;
			SwipeManager.Instance.enabled = false;
			deltas.Clear();
			timestamps.Clear();
			OnZoomStarted();
		}
	}

	private void HandleScaled(object sender, EventArgs e)
	{
		if (_allowInteraction && _allowPinchZoom)
		{
			ITouch touch = scaleGesture.ActiveTouches[0];
			ITouch touch2 = scaleGesture.ActiveTouches[1];
			zoomDelta = (Vector2.Distance(touch.Position, touch2.Position) - Vector2.Distance(touch.PreviousPosition, touch2.PreviousPosition)) / Screen.dpi;
			deltas.Add(zoomDelta);
			timestamps.Add(Time.unscaledTime);
			if (zoomDelta > 0f && _targetPosition.z > maxZDistance)
			{
				zoomDelta *= dampening;
			}
			_targetPosition.z += zoomDelta * zoomFactor;
			if (_isZoomedOut)
			{
				_targetPosition.z = Mathf.Clamp(_targetPosition.z, zoomedOutMaxDistance, zoomedOutMinDistance);
			}
			else
			{
				_targetPosition.z = Mathf.Clamp(_targetPosition.z, zoomedInMaxDistance, zoomedInMinDistance);
			}
		}
	}

	private void TestPinchZoomIn()
	{
		zoomDelta = 0.1f;
		if (zoomDelta > 0f && _targetPosition.z > maxZDistance)
		{
			zoomDelta *= dampening;
		}
		_targetPosition.z += zoomDelta * zoomFactor;
		if (_isZoomedOut)
		{
			_targetPosition.z = Mathf.Clamp(_targetPosition.z, zoomedOutMaxDistance, zoomedOutMinDistance);
		}
		else
		{
			_targetPosition.z = Mathf.Clamp(_targetPosition.z, zoomedInMaxDistance, zoomedInMinDistance);
		}
	}

	private void TestPinchZoomOut()
	{
		zoomDelta = -0.1f;
		if (zoomDelta > 0f && _targetPosition.z > maxZDistance)
		{
			zoomDelta *= dampening;
		}
		_targetPosition.z += zoomDelta * zoomFactor;
		if (_isZoomedOut)
		{
			_targetPosition.z = Mathf.Clamp(_targetPosition.z, zoomedOutMaxDistance, zoomedOutMinDistance);
		}
		else
		{
			_targetPosition.z = Mathf.Clamp(_targetPosition.z, zoomedInMaxDistance, zoomedInMinDistance);
		}
	}

	private void HandleScaleCompleted(object sender, EventArgs e)
	{
		if (_allowInteraction && _allowPinchZoom)
		{
			zoomDelta = 0f;
			float num = Time.unscaledTime - swipeTimeRange;
			float num2 = 0f;
			float num3 = Mathf.Sign(deltas[deltas.Count - 1]);
			int num4 = deltas.Count - 1;
			int num5 = num4;
			while (num5 >= 0 && !(timestamps[num5] < num) && Mathf.Sign(deltas[num5]) == num3)
			{
				num2 += deltas[num5];
				num5--;
			}
			float num6 = Mathf.Abs(num2);
			if (num6 > swipeDistance && num2 > 0f && _isZoomedOut)
			{
				ZoomIn();
			}
			else if (num6 > swipeDistance && num2 < 0f && !_isZoomedOut)
			{
				ZoomOut();
			}
			else if (snapBackToDefaultZoomLevel)
			{
				_targetPosition = ((!_isZoomedOut) ? homePosition : defaultZoomOutPosition);
			}
			SwipeManager.Instance.enabled = true;
			OnZoomFinished();
		}
	}

	private void HandlePanStarted(object sender, EventArgs e)
	{
		underInteria = false;
	}

	private void HandlePanned(object sender, EventArgs e)
	{
		if (_allowInteraction && _allowPan)
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = thisCamera.ScreenPointToRay(panGesture.ScreenPosition);
			plane.Raycast(ray, out var enter);
			Vector3 point = ray.GetPoint(enter);
			ray = thisCamera.ScreenPointToRay(panGesture.PreviousScreenPosition);
			plane.Raycast(ray, out enter);
			Vector3 point2 = ray.GetPoint(enter);
			panDelta = point - point2;
			Vector3 position = transform.position;
			position.x += 0f - panDelta.x;
			position.z += 0f - panDelta.z;
			position.x = Mathf.Clamp(position.x, panMinX, panMaxX);
			position.z = Mathf.Clamp(position.z, panMinZ, panMaxZ);
			transform.position = position;
			lastPanTime = Time.time;
		}
	}

	private void HandlePanCompleted(object sender, EventArgs e)
	{
		if (_allowInteraction && _allowPan)
		{
			bool flag = true;
			if ((double)(Time.time - lastPanTime) > 0.1)
			{
				flag = false;
			}
			if (panDelta.x <= 0.01f && panDelta.x >= -0.01f && panDelta.z <= 0.01f && panDelta.z >= -0.01f)
			{
				flag = false;
			}
			if (flag)
			{
				underInteria = true;
				return;
			}
			underInteria = false;
			panDelta = Vector3.zero;
		}
	}

	protected void OnSingleTap(Vector2 screenPosition)
	{
		if (SingleTap != null)
		{
			SingleTap(screenPosition);
		}
	}

	protected void OnZoomStarted()
	{
		if (ZoomStarted != null)
		{
			ZoomStarted();
		}
	}

	protected void OnZoomFinished()
	{
		if (ZoomFinished != null)
		{
			ZoomFinished();
		}
	}

	protected void OnZoomedOut()
	{
		if (ZoomedOut != null)
		{
			ZoomedOut();
		}
	}

	protected void OnZoomedIn()
	{
		if (ZoomedIn != null)
		{
			ZoomedIn();
		}
	}

	private void OnZoomedOutMoveFinished()
	{
		zoomPositionTween = null;
		zoomRotateTween = null;
		if (ZoomedOutMoveFinished != null)
		{
			ZoomedOutMoveFinished();
		}
	}

	private void OnZoomedInMoveFinished()
	{
		if (ZoomedInMoveFinished != null)
		{
			ZoomedInMoveFinished();
		}
	}
}
