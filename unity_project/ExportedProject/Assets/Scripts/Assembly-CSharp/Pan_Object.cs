using System;
using TouchScript.Gestures;
using UnityEngine;

public class Pan_Object : MonoBehaviour
{
	private Quaternion targetRotation;

	private void Awake()
	{
		targetRotation = base.transform.localRotation;
	}

	private void OnEnable()
	{
		base.transform.Find("Button").GetComponent<TapGesture>().Tapped += tapStateChangeHandler;
	}

	private void OnDisable()
	{
		base.transform.Find("Button").GetComponent<TapGesture>().Tapped -= tapStateChangeHandler;
	}

	private void Update()
	{
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, targetRotation, 0.1f);
	}

	private void tapStateChangeHandler(object sender, EventArgs e)
	{
		targetRotation = Quaternion.Euler(0f, 90f, 0f) * targetRotation;
	}
}
