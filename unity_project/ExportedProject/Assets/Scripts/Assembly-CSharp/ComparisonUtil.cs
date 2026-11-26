public static class ComparisonUtil
{
	public static bool IsComparisonValid(int val1, Comparison comparison, int val2)
	{
		switch (comparison)
		{
		case Comparison.Exactly:
			return val1 == val2;
		case Comparison.MoreThan:
			return val1 > val2;
		case Comparison.LessThan:
			return val1 < val2;
		case Comparison.AtLeast:
			return val1 >= val2;
		case Comparison.AtMost:
			return val1 <= val2;
		default:
			return false;
		}
	}
}
