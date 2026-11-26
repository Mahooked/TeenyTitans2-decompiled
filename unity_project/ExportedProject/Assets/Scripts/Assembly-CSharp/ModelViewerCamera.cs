using System;
using TouchScript;
using TouchScript.Gestures;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ModelViewerCamera : MonoBehaviour
{
	[SerializeField]
	private Camera _camera;

	public Transform target;

	public Vector3 targetOffset;

	public Vector3 targetRotationOffset;

	public float defaultDistance = 3f;

	public float defaultFOV = 60f;

	public float maxDistance = 5f;

	public float minDistance = 1.5f;

	public float maxFOV = 80f;

	public float minFOV = 30f;

	public int zoomRate = 10;

	public float zoomDampening = 10f;

	public bool zoomEnabled = true;

	public ScaleGesture scaleGesture;

	public bool showDebugGUI;

	private float currentDistance;

	private float desiredDistance;

	private float currentFOV;

	private float desiredFOV;

	private float zoomDelta;

	private bool isInitialized;

	public float DesiredDistance
	{
		get
		{
			return desiredDistance;
		}
		set
		{
			desiredDistance = value;
		}
	}

	public float DesiredFOV
	{
		get
		{
			return desiredFOV;
		}
		set
		{
			desiredFOV = value;
		}
	}

	public Camera camera
	{
		get
		{
			return _camera;
		}
	}

	private void Awake()
	{
		if (scaleGesture == null)
		{
			scaleGesture = GetComponent<ScaleGesture>();
		}
	}

	private void Start()
	{
		Init();
	}

	private void OnEnable()
	{
		Init();
	}

	public void Init()
	{
		if (!isInitialized)
		{
			if (_camera == null)
			{
				_camera = GetComponent<Camera>();
			}
			if (target == null)
			{
				target = UnityEngine.Object.FindObjectOfType<ModelViewer>().transform;
			}
			currentDistance = (desiredDistance = defaultDistance);
			currentFOV = (desiredFOV = defaultFOV);
			target.position = base.transform.position + base.transform.forward * currentDistance;
			_camera.fieldOfView = currentFOV;
			scaleGesture.ScaleStarted += HandleScaleStarted;
			scaleGesture.Scaled += HandleScaled;
			scaleGesture.ScaleCompleted += HandleScaleCompleted;
			isInitialized = true;
		}
	}

	public void Reset()
	{
		targetOffset = Vector3.zero;
		desiredDistance = defaultDistance;
		desiredFOV = defaultFOV;
	}

	private void HandleScaleStarted(object sender, EventArgs e)
	{
		zoomDelta = 0f;
	}

	private void HandleScaled(object sender, EventArgs e)
	{
		if (zoomEnabled)
		{
			ITouch touch = scaleGesture.ActiveTouches[0];
			ITouch touch2 = scaleGesture.ActiveTouches[1];
			zoomDelta = (Vector2.Distance(touch.Position, touch2.Position) - Vector2.Distance(touch.PreviousPosition, touch2.PreviousPosition)) / Screen.dpi;
		}
	}

	private void HandleScaleCompleted(object sender, EventArgs e)
	{
		zoomDelta = 0f;
	}

	private void OnGUI()
	{
		if (showDebugGUI)
		{
			GUI.Label(new Rect(5f, 0f, 100f, 20f), "Distance: " + Mathf.Floor(currentDistance * 100f) / 100f);
			GUI.Label(new Rect(5f, 20f, 100f, 20f), "FOV: " + Mathf.Floor(currentFOV * 100f) / 100f);
		}
	}

	private void LateUpdate()
	{
		desiredDistance -= zoomDelta * Time.deltaTime * (float)zoomRate * Mathf.Abs(desiredDistance);
		desiredFOV -= zoomDelta * Time.deltaTime * (float)zoomRate * Mathf.Abs(desiredFOV);
		desiredFOV = Mathf.Clamp(desiredFOV, minFOV, maxFOV);
		currentFOV = Mathf.Lerp(_camera.fieldOfView, desiredFOV, Time.deltaTime * zoomDampening);
		desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
		currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
		base.transform.position = target.position - (base.transform.rotation * Vector3.forward * currentDistance + targetOffset);
		_camera.fieldOfView = currentFOV;
		base.transform.localRotation = Quaternion.Euler(targetRotationOffset);
		zoomDelta = 0f;
	}
}
