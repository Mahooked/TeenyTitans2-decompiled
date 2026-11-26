using System;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
	public float swipeTimeRange = 0.2f;

	public float swipeDistance = 0.3f;

	public bool drawRaycast;

	private Vector3 prevMousePosition;

	private Vector3 curMousePosition;

	private Vector3 dragDiff;

	private bool isDown;

	private List<Vector3> points = new List<Vector3>();

	private List<float> timestamps = new List<float>();

	private SwipeResult _swipeResult;

	private float startTime;

	public static SwipeManager Instance { get; private set; }

	public event Action<SwipeResult> SwipeOccurred;

	private void Awake()
	{
		Instance = this;
		_swipeResult = default;
	}

	private void OnDestroy()
	{
		Instance = null;
		SwipeOccurred = null;
	}

	private void Update()
	{
		prevMousePosition = curMousePosition;
		curMousePosition = InputHelper.GetPosition(0);
		if (InputHelper.GetButtonDown(0))
		{
			_swipeResult.start = curMousePosition;
			dragDiff = Vector3.zero;
			isDown = true;
			points.Clear();
			timestamps.Clear();
			startTime = Time.time;
		}
		else if (InputHelper.GetButton(0))
		{
			if (isDown)
			{
				dragDiff = curMousePosition - prevMousePosition;
				dragDiff.z = 0f;
				points.Add(dragDiff);
				timestamps.Add(Time.unscaledTime);
			}
		}
		else if (InputHelper.GetButtonUp(0))
		{
			_swipeResult.end = curMousePosition;
			Released();
		}
	}

	private void Released()
	{
		dragDiff = curMousePosition - prevMousePosition;
		points.Add(dragDiff);
		timestamps.Add(Time.unscaledTime);
		float num = Time.unscaledTime - swipeTimeRange;
		Vector3 zero = Vector3.zero;
		int num2 = points.Count - 1;
		int num3 = 0;
		int num4 = num2;
		while (num4 >= 0 && !(timestamps[num4] < num))
		{
			zero += points[num4];
			num3++;
			num4--;
		}
		_swipeResult.direction = (zero / num3).normalized;
		_swipeResult.magnitude = zero.magnitude;
		_swipeResult.length = _swipeResult.magnitude / Screen.dpi;
		_swipeResult.worldStart = InputHelper.instance.ScreenToWorldPoint(_swipeResult.start);
		_swipeResult.worldEnd = InputHelper.instance.ScreenToWorldPoint(_swipeResult.end);
		_swipeResult.worldDirection = Quaternion.Euler(0f, 0f, InputHelper.instance.mainCamera.transform.localRotation.eulerAngles.z) * _swipeResult.direction;
		isDown = false;
		if (_swipeResult.length >= swipeDistance && Time.time - startTime < 2f)
		{
			OnSwipeOccurred();
		}
	}

	protected void OnSwipeOccurred()
	{
		if (SwipeOccurred != null)
		{
			SwipeOccurred(_swipeResult);
		}
	}
}
