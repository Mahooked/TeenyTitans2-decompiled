using System.Collections.Generic;

public static class UniqueIdRegistry
{
	public static Dictionary<string, int> mapping = new Dictionary<string, int>();

	public static void Deregister(string key)
	{
		if (!string.IsNullOrEmpty(key))
		{
			mapping.Remove(key);
		}
	}

	public static void Register(string key, int value)
	{
		if (!mapping.ContainsKey(key))
		{
			mapping.Add(key, value);
		}
	}

	public static int GetInstanceId(string key)
	{
		if (mapping.ContainsKey(key))
		{
			return mapping[key];
		}
		return -1;
	}
}
