using UnityEngine;

namespace TouchScript.Utils;

internal static class TransformUtils
{
	public static Vector3 GlobalToLocalPosition(Transform transform, Vector3 global)
	{
		if (transform.parent == null)
		{
			return global;
		}
		return transform.parent.InverseTransformPoint(global);
	}

	public static Vector3 GlobalToLocalDirection(Transform transform, Vector3 global)
	{
		if (transform.parent == null)
		{
			return global;
		}
		return transform.parent.InverseTransformDirection(global);
	}

	public static Vector3 GlobalToLocalVector(Transform transform, Vector3 global)
	{
		Transform parent = transform.parent;
		if (parent == null)
		{
			return global;
		}
		Vector3 localScale = parent.localScale;
		Vector3 vector = GlobalToLocalVector(parent, global);
		vector = Quaternion.Inverse(parent.localRotation) * vector;
		vector.x = ((!Mathf.Approximately(localScale.x, 0f)) ? (vector.x / localScale.x) : 0f);
		vector.y = ((!Mathf.Approximately(localScale.y, 0f)) ? (vector.y / localScale.y) : 0f);
		vector.z = ((!Mathf.Approximately(localScale.z, 0f)) ? (vector.z / localScale.z) : 0f);
		return vector;
	}
}
