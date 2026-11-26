using System;
using UnityEngine;

namespace ProBuilder2.Common;

public static class pb_Vector
{
	public const float FLT_COMPARE_RESOLUTION = 1000f;

	public static int GetHashCode(Vector2 v)
	{
		int num = 27;
		num = num * 29 + Convert.ToInt32(v.x * 1000f);
		return num * 29 + Convert.ToInt32(v.y * 1000f);
	}

	public static int GetHashCode(Vector3 v)
	{
		int num = 27;
		num = num * 29 + Convert.ToInt32(v.x * 1000f);
		num = num * 29 + Convert.ToInt32(v.y * 1000f);
		return num * 29 + Convert.ToInt32(v.z * 1000f);
	}

	public static int GetHashCode(Vector4 v)
	{
		int num = 27;
		num = num * 29 + Convert.ToInt32(v.x * 1000f);
		num = num * 29 + Convert.ToInt32(v.y * 1000f);
		num = num * 29 + Convert.ToInt32(v.z * 1000f);
		return num * 29 + Convert.ToInt32(v.w * 1000f);
	}
}
