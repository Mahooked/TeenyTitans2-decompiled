using System;
using TouchScript.Gestures;
using UnityEngine;

public class Pan_Button : MonoBehaviour
{
	private float startY;

	private void Awake()
	{
		startY = base.transform.localPosition.y;
	}

	private void OnEnable()
	{
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
	}

	private void OnDisable()
	{
		GetComponent<PressGesture>().Pressed -= pressedHandler;
		GetComponent<ReleaseGesture>().Released -= releasedHandler;
	}

	private void releasedHandler(object sender, EventArgs e)
	{
		base.transform.localPosition = new Vector3(0f, startY, 0f);
	}

	private void pressedHandler(object sender, EventArgs e)
	{
		base.transform.localPosition = new Vector3(0f, startY - base.transform.localScale.y * 0.9f, 0f);
	}
}
