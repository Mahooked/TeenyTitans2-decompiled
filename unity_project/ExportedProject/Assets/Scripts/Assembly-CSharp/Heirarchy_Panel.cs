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
		startHeight = base.transform.localPosition.y;
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
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = Mathf.Lerp(base.transform.localPosition.y, b, Time.deltaTime * JumpSpeed);
		base.transform.localPosition = localPosition;
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
