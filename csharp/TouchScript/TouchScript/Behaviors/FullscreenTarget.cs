using System;
using UnityEngine;

namespace TouchScript.Behaviors;

[Obsolete("FullscreenTarget is obsolete. Please use FullscreenLayer instead.")]
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Camera))]
public class FullscreenTarget : MonoBehaviour
{
	public enum TargetType
	{
		Background,
		Foreground
	}

	public TargetType Type;

	private void Awake()
	{
		Debug.LogWarning("FullscreenTarget is obsolete. Please use FullscreenLayer instead.");
	}

	private void Update()
	{
		BoxCollider component = GetComponent<BoxCollider>();
		Camera component2 = GetComponent<Camera>();
		float num = 2f * Mathf.Tan(component2.fieldOfView / 360f * (float)Math.PI);
		if (Type == TargetType.Background)
		{
			num *= component2.farClipPlane;
			component.center = new Vector3(0f, 0f, component2.farClipPlane);
		}
		else if (Type == TargetType.Foreground)
		{
			num *= component2.nearClipPlane + 0.0051f;
			component.center = new Vector3(0f, 0f, component2.nearClipPlane + 0.0051f);
		}
		float x = (float)Screen.width / (float)Screen.height * num;
		component.size = new Vector3(x, num, 0.01f);
	}
}
