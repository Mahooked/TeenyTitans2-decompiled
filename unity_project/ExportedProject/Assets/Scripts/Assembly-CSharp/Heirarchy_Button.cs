using System;
using TouchScript.Gestures;
using UnityEngine;

public class Heirarchy_Button : MonoBehaviour
{
	private Vector3 startPosition;

	private void Awake()
	{
		startPosition = base.transform.localPosition;
	}

	private void OnEnable()
	{
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}

	private void tappedHandler(object sender, EventArgs eventArgs)
	{
		Color color = base.transform.parent.GetComponent<Renderer>().material.color;
		base.transform.parent.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = color;
	}

	private void releasedHandler(object sender, EventArgs e)
	{
		base.transform.localPosition = startPosition;
	}

	private void pressedHandler(object sender, EventArgs e)
	{
		base.transform.localPosition = startPosition - new Vector3(0f, base.transform.localScale.y * 0.9f, 0f);
	}
}
