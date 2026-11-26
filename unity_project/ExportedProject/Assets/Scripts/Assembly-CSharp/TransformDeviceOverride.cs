using System;
using UnityEngine;

[ExecuteInEditMode]
public class TransformDeviceOverride : MonoBehaviour
{
	[Serializable]
	public struct TransformData
	{
		public Vector3 localPosition;

		public Vector3 localRotation;

		public Vector3 localScale;
	}

	public bool ignorePosition;

	public bool ignoreRotation;

	public bool ignoreScale;

	public TransformData defaultTransform;

	public TransformData iPhoneXTransform;

	private void Start()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		if (Screen.height == 2436 || Screen.height == 609)
		{
			if (!ignorePosition)
			{
				base.transform.localPosition = iPhoneXTransform.localPosition;
			}
			if (!ignoreRotation)
			{
				base.transform.localRotation = Quaternion.Euler(iPhoneXTransform.localRotation);
			}
			if (!ignoreScale)
			{
				base.transform.localScale = iPhoneXTransform.localScale;
			}
		}
		else
		{
			if (!ignorePosition)
			{
				base.transform.localPosition = defaultTransform.localPosition;
			}
			if (!ignoreRotation)
			{
				base.transform.localRotation = Quaternion.Euler(defaultTransform.localRotation);
			}
			if (!ignoreScale)
			{
				base.transform.localScale = defaultTransform.localScale;
			}
		}
	}
}
