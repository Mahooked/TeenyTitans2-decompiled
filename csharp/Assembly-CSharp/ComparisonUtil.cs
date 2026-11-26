public static class ComparisonUtil
{
	public static bool IsComparisonValid(int val1, Comparison comparison, int val2)
	{
		return comparison switch
		{
			Comparison.Exactly => val1 == val2, 
			Comparison.MoreThan => val1 > val2, 
			Comparison.LessThan => val1 < val2, 
			Comparison.AtLeast => val1 >= val2, 
			Comparison.AtMost => val1 <= val2, 
			_ => false, 
		};
	}
}
