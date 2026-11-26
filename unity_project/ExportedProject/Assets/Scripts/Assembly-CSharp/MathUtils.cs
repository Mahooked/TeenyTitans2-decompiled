public static class MathUtils
{
	public static bool IsInRange(float value, float min, float max, bool inclusive = false)
	{
		if (inclusive)
		{
			return value >= min && value <= max;
		}
		return value > min && value < max;
	}

	public static bool IsInRange(int value, int min, int max, bool inclusive = false)
	{
		if (inclusive)
		{
			return value >= min && value <= max;
		}
		return value > min && value < max;
	}

	public static float GetDecimalPart(float value)
	{
		return value - (float)(int)value;
	}
}
