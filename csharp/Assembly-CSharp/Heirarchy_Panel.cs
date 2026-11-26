using System;
using TouchScript.Gestures;
using UnityEngine;

public class Heirarchy_Panel : MonoBehaviour
{
	public float MaxHeight = 2f;

	public float JumpSpeed = 5f;

	private bool selected;

	private float startHeight;

	private void Start()
	{
		startHeight = transform.localPosition.y;
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
	}

	private void Update()
	{
		float b = startHeight;
		if (selected)
		{
			b = startHeight + MaxHeight;
		}
		Vector3 localPosition = transform.localPosition;
		localPosition.y = Mathf.Lerp(transform.localPosition.y, b, Time.deltaTime * JumpSpeed);
		transform.localPosition = localPosition;
	}

	private void releasedHandler(object sender, EventArgs eventArgs)
	{
		selected = false;
	}

	private void pressedHandler(object sender, EventArgs eventArgs)
	{
		selected = true;
	}
}
