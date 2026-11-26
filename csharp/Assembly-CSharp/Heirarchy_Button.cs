using System;
using TouchScript.Gestures;
using UnityEngine;

public class Heirarchy_Button : MonoBehaviour
{
	private Vector3 startPosition;

	private void Awake()
	{
		startPosition = transform.localPosition;
	}

	private void OnEnable()
	{
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}

	private void tappedHandler(object sender, EventArgs eventArgs)
	{
		Color color = transform.parent.GetComponent<Renderer>().material.color;
		transform.parent.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = color;
	}

	private void releasedHandler(object sender, EventArgs e)
	{
		transform.localPosition = startPosition;
	}

	private void pressedHandler(object sender, EventArgs e)
	{
		transform.localPosition = startPosition - new Vector3(0f, transform.localScale.y * 0.9f, 0f);
	}
}
