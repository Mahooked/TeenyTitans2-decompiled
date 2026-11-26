using System;
using UnityEngine;

[Serializable]
public class AEFrame
{
	public int index;

	public Vector3 scale;

	public Vector3 pivot;

	public float rotation;

	public float opacity;

	public Vector3 position;

	public Vector3 positionUnity;

	public bool isVisible;

	public bool isNothingChanged;

	public bool isScaleChanged = true;

	public bool isPivotChanged = true;

	public bool isRotationChanged = true;

	public bool isOpacityChanged = true;

	public bool isPositionChanged = true;

	public bool isVisibilityChanged = true;

	public void SetPosition(Vector3 pos)
	{
		position = pos;
		positionUnity = position;
		positionUnity.y = 0f - positionUnity.y;
	}

	public void CompareToFrame(AEFrame lastFrame)
	{
		if (lastFrame != null)
		{
			isScaleChanged = !V3Equal(scale, lastFrame.scale);
			isPivotChanged = !V3Equal(pivot, lastFrame.pivot);
			isPositionChanged = !V3Equal(positionUnity, lastFrame.positionUnity);
			isRotationChanged = !FCompare(rotation, lastFrame.rotation);
			isOpacityChanged = !FCompare(opacity, lastFrame.opacity);
			isVisibilityChanged = isVisible != lastFrame.isVisible;
			isNothingChanged = !isScaleChanged && !isPivotChanged && !isPositionChanged && !isRotationChanged && !isOpacityChanged && !isVisibilityChanged;
		}
	}

	private static bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < 0.0001f;
	}

	private static bool FCompare(float a, float b)
	{
		return Mathf.Abs(a - b) < 0.0001f;
	}
}
