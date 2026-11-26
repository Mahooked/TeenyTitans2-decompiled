using System;
using TouchScript;
using TouchScript.Gestures;
using UnityEngine;

public class MaxCamera : MonoBehaviour
{
	public Transform target;

	public Vector3 targetOffset;

	public float defaultDistance = 3f;

	public float defaultFOV = 60f;

	public float maxDistance = 20f;

	public float minDistance = 0.6f;

	public float maxFOV = 70f;

	public float minFOV = 50f;

	public int zoomRate = 40;

	public float zoomDampening = 5f;

	public ScaleGesture scaleGesture;

	private Camera mainCamera;

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
			mainCamera = Camera.main;
			if (target == null)
			{
				target = new GameObject("Cam Target").transform;
			}
			currentDistance = (desiredDistance = defaultDistance);
			currentFOV = (desiredFOV = defaultFOV);
			target.position = transform.position + transform.forward * currentDistance;
			mainCamera.fieldOfView = currentFOV;
			scaleGesture.ScaleStarted += HandleScaleStarted;
			scaleGesture.Scaled += HandleScaled;
			scaleGesture.ScaleCompleted += HandleScaleCompleted;
			isInitialized = true;
		}
	}

	private void HandleScaleStarted(object sender, EventArgs e)
	{
		zoomDelta = 0f;
	}

	private void HandleScaled(object sender, EventArgs e)
	{
		ITouch touch = scaleGesture.ActiveTouches[0];
		ITouch touch2 = scaleGesture.ActiveTouches[1];
		zoomDelta = (Vector2.Distance(touch.Position, touch2.Position) - Vector2.Distance(touch.PreviousPosition, touch2.PreviousPosition)) / Screen.dpi;
	}

	private void HandleScaleCompleted(object sender, EventArgs e)
	{
		zoomDelta = 0f;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(5f, 0f, 100f, 20f), "Distance: " + Mathf.Floor(currentDistance * 100f) / 100f);
		GUI.Label(new Rect(5f, 20f, 100f, 20f), "FOV: " + Mathf.Floor(currentFOV * 100f) / 100f);
	}

	private void LateUpdate()
	{
		desiredDistance -= zoomDelta * Time.deltaTime * (float)zoomRate * Mathf.Abs(desiredDistance);
		desiredFOV -= zoomDelta * Time.deltaTime * (float)zoomRate * Mathf.Abs(desiredFOV);
		desiredFOV = Mathf.Clamp(desiredFOV, minFOV, maxFOV);
		currentFOV = Mathf.Lerp(mainCamera.fieldOfView, desiredFOV, Time.deltaTime * zoomDampening);
		desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
		currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
		transform.position = target.position - (transform.rotation * Vector3.forward * currentDistance + targetOffset);
		mainCamera.fieldOfView = currentFOV;
		zoomDelta = 0f;
	}
}
