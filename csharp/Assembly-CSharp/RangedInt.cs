using System;
using UnityEngine;

[Serializable]
public struct RangedInt(int min, int max)
{
	public int minValue = min;

	public int maxValue = max;

	public float GetInterpolatedValue(float percent)
	{
		return Mathf.Lerp(minValue, maxValue, Mathf.Clamp01(percent));
	}

	public float GetPosition(int value)
	{
		return (float)(Mathf.Clamp(value, minValue, maxValue) - minValue) / (float)(maxValue - minValue);
	}

	public int GetRandomValue()
	{
		return UnityEngine.Random.Range(minValue, maxValue + 1);
	}

	public int GetDiff()
	{
		return maxValue - minValue;
	}

	public bool IsInRange(int value)
	{
		return value >= minValue && value <= maxValue;
	}

	public override bool Equals(object obj)
	{
		return obj is RangedInt && (RangedInt)obj == this;
	}

	public override int GetHashCode()
	{
		return minValue.GetHashCode() ^ maxValue.GetHashCode();
	}

	public static bool operator ==(RangedInt x, RangedInt y)
	{
		return x.minValue == y.minValue && x.maxValue == y.maxValue;
	}

	public static bool operator !=(RangedInt x, RangedInt y)
	{
		return !(x == y);
	}
}
