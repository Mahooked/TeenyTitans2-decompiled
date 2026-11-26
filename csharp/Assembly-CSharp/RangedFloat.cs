using System;
using UnityEngine;

[Serializable]
public struct RangedFloat(float min, float max)
{
	public float minValue = min;

	public float maxValue = max;

	public float GetInterpolatedValue(float percent)
	{
		return Mathf.Lerp(minValue, maxValue, Mathf.Clamp01(percent));
	}

	public float GetPosition(float value)
	{
		return (Mathf.Clamp(value, minValue, maxValue) - minValue) / (maxValue - minValue);
	}

	public float GetRandomValue()
	{
		return UnityEngine.Random.Range(minValue, maxValue);
	}

	public float GetDiff()
	{
		return maxValue - minValue;
	}

	public override bool Equals(object obj)
	{
		return obj is RangedFloat && (RangedFloat)obj == this;
	}

	public override int GetHashCode()
	{
		return minValue.GetHashCode() ^ maxValue.GetHashCode();
	}

	public static bool operator ==(RangedFloat x, RangedFloat y)
	{
		return x.minValue == y.minValue && x.maxValue == y.maxValue;
	}

	public static bool operator !=(RangedFloat x, RangedFloat y)
	{
		return !(x == y);
	}
}
