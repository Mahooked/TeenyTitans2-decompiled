using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using TouchScript;
using TouchScript.Gestures;
using UnityEngine;

[DisallowMultipleComponent]
public class CameraPinchZoomer : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ZoomStarted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ZoomFinished__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ZoomedOut__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ZoomedIn__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ZoomedOutMoveFinished__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ZoomedInMoveFinished__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action CamReachedTargetPos__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Vector2> SingleTap__BackingField;

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

	public CameraFollow cameraFollow
	{
		get
		{
			return _cameraFollow;
		}
	}

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
				base.transform.DOLocalRotate(homeRotation, 0.4f).SetEase(Ease.OutSine);
			}
		}
	}

	public bool isZoomedOut
	{
		get
		{
			return _isZoomedOut;
		}
	}

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

	public event Action ZoomStarted
	{
		add
		{
			Action action = ZoomStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ZoomStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ZoomFinished
	{
		add
		{
			Action action = ZoomFinished__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomFinished__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ZoomFinished__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomFinished__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ZoomedOut
	{
		add
		{
			Action action = ZoomedOut__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedOut__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ZoomedOut__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedOut__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ZoomedIn
	{
		add
		{
			Action action = ZoomedIn__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedIn__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ZoomedIn__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedIn__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ZoomedOutMoveFinished
	{
		add
		{
			Action action = ZoomedOutMoveFinished__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedOutMoveFinished__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ZoomedOutMoveFinished__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedOutMoveFinished__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ZoomedInMoveFinished
	{
		add
		{
			Action action = ZoomedInMoveFinished__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedInMoveFinished__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ZoomedInMoveFinished__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ZoomedInMoveFinished__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action CamReachedTargetPos
	{
		add
		{
			Action action = CamReachedTargetPos__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CamReachedTargetPos__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = CamReachedTargetPos__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CamReachedTargetPos__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Vector2> SingleTap
	{
		add
		{
			Action<Vector2> action = SingleTap__BackingField;
			Action<Vector2> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SingleTap__BackingField, (Action<Vector2>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Vector2> action = SingleTap__BackingField;
			Action<Vector2> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SingleTap__BackingField, (Action<Vector2>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		homePosition = (_targetPosition = cameraTransform.localPosition);
		homeRotation = base.transform.localRotation.eulerAngles;
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
		ZoomStarted__BackingField = null;
		ZoomFinished__BackingField = null;
		ZoomedOut__BackingField = null;
		ZoomedIn__BackingField = null;
		ZoomedOutMoveFinished__BackingField = null;
		ZoomedInMoveFinished__BackingField = null;
		CamReachedTargetPos__BackingField = null;
		SingleTap__BackingField = null;
	}

	private void Update()
	{
		if (!scaleGesture.enabled && Input.touchCount == 0)
		{
			UnityEngine.Debug.Log("ENABLED SCALE AND PAN GESTURES");
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
			if (CamReachedTargetPos__BackingField != null)
			{
				CamReachedTargetPos__BackingField();
			}
		}
		if (underInteria && smoothTime <= smoothDuration)
		{
			smoothTime += Time.smoothDeltaTime;
			smoothDampTime += Time.smoothDeltaTime / smoothDuration;
			Vector3 position = base.transform.position;
			position.x += 0f - panDelta.x;
			position.z += 0f - panDelta.z;
			position.x = Mathf.Clamp(position.x, panMinX, panMaxX);
			position.z = Mathf.Clamp(position.z, panMinZ, panMaxZ);
			base.transform.position = position;
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
			base.transform.DOKill();
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
			zoomRotateTween = base.transform.DOLocalRotate(homeRotation, 0.4f).SetEase(Ease.OutSine).OnComplete(OnZoomedInMoveFinished);
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
			Vector3 position = base.transform.position;
			position.x = Mathf.Clamp(position.x, panMinX, panMaxX);
			position.z = Mathf.Clamp(position.z, panMinZ, panMaxZ);
			zoomPositionTween = base.transform.DOMove(position, 0.4f).SetEase(Ease.OutSine);
			zoomRotateTween = base.transform.DOLocalRotate(zoomedOutRotation, 0.4f).SetEase(Ease.OutSine).OnComplete(OnZoomedOutMoveFinished);
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
			float enter;
			plane.Raycast(ray, out enter);
			Vector3 point = ray.GetPoint(enter);
			ray = thisCamera.ScreenPointToRay(panGesture.PreviousScreenPosition);
			plane.Raycast(ray, out enter);
			Vector3 point2 = ray.GetPoint(enter);
			panDelta = point - point2;
			Vector3 position = base.transform.position;
			position.x += 0f - panDelta.x;
			position.z += 0f - panDelta.z;
			position.x = Mathf.Clamp(position.x, panMinX, panMaxX);
			position.z = Mathf.Clamp(position.z, panMinZ, panMaxZ);
			base.transform.position = position;
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
		if (SingleTap__BackingField != null)
		{
			SingleTap__BackingField(screenPosition);
		}
	}

	protected void OnZoomStarted()
	{
		if (ZoomStarted__BackingField != null)
		{
			ZoomStarted__BackingField();
		}
	}

	protected void OnZoomFinished()
	{
		if (ZoomFinished__BackingField != null)
		{
			ZoomFinished__BackingField();
		}
	}

	protected void OnZoomedOut()
	{
		if (ZoomedOut__BackingField != null)
		{
			ZoomedOut__BackingField();
		}
	}

	protected void OnZoomedIn()
	{
		if (ZoomedIn__BackingField != null)
		{
			ZoomedIn__BackingField();
		}
	}

	private void OnZoomedOutMoveFinished()
	{
		zoomPositionTween = null;
		zoomRotateTween = null;
		if (ZoomedOutMoveFinished__BackingField != null)
		{
			ZoomedOutMoveFinished__BackingField();
		}
	}

	private void OnZoomedInMoveFinished()
	{
		if (ZoomedInMoveFinished__BackingField != null)
		{
			ZoomedInMoveFinished__BackingField();
		}
	}
}
