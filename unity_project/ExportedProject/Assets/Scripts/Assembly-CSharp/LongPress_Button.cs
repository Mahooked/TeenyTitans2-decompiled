using System;
using System.Collections;
using TouchScript.Gestures;
using UnityEngine;

public class LongPress_Button : MonoBehaviour
{
	public GameObject Plane;

	private Transform button;

	private Transform thebase;

	private float timeToPress;

	private Vector3 startScale;

	private Vector3 targetScale;

	private void Awake()
	{
		button = base.transform.Find("Button");
		thebase = base.transform.Find("Base");
		startScale = button.localScale;
		targetScale = thebase.localScale;
	}

	private void OnEnable()
	{
		timeToPress = GetComponent<LongPressGesture>().TimeToPress;
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
		GetComponent<LongPressGesture>().StateChanged += longPressStateChangedHandler;
	}

	private void OnDisable()
	{
		GetComponent<PressGesture>().Pressed -= pressedHandler;
		GetComponent<ReleaseGesture>().Released -= releasedHandler;
		GetComponent<LongPressGesture>().StateChanged -= longPressStateChangedHandler;
	}

	private void press()
	{
		button.transform.localPosition = new Vector3(0f, (0f - button.transform.localScale.y) * 0.4f, 0f);
	}

	private void release()
	{
		button.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	private void reset()
	{
		button.transform.localScale = startScale;
		StopCoroutine("grow");
	}

	private void changeColor()
	{
		if (!(Plane == null))
		{
			Plane.GetComponent<Renderer>().material.color = button.GetComponent<Renderer>().sharedMaterial.color;
		}
	}

	private IEnumerator grow()
	{
		while (true)
		{
			button.transform.localScale += (targetScale.x - startScale.x) / timeToPress * Time.unscaledDeltaTime * new Vector3(1f, 0f, 1f);
			yield return null;
		}
	}

	private void longPressStateChangedHandler(object sender, GestureStateChangeEventArgs e)
	{
		Gesture.GestureState state = e.State;
		if (state == Gesture.GestureState.Ended || state == Gesture.GestureState.Failed || state == Gesture.GestureState.Cancelled)
		{
			reset();
		}
		if (e.State == Gesture.GestureState.Ended)
		{
			changeColor();
		}
	}

	private void pressedHandler(object sender, EventArgs e)
	{
		press();
		StartCoroutine("grow");
	}

	private void releasedHandler(object sender, EventArgs e)
	{
		release();
	}
}
