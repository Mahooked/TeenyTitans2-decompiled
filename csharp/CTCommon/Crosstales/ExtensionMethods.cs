using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crosstales;

public static class ExtensionMethods
{
	private static readonly System.Random rd = new System.Random();

	public static string CTToTitleCase(this string str)
	{
		return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
	}

	public static string CTReplace(this string str, string oldString, string newString, StringComparison comp = StringComparison.OrdinalIgnoreCase)
	{
		int num = str.IndexOf(oldString, comp);
		if (num >= 0)
		{
			str = str.Remove(num, oldString.Length);
			str = str.Insert(num, newString);
		}
		return str;
	}

	public static bool CTEquals(this string str, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
	{
		if (str == null)
		{
			throw new ArgumentNullException("str");
		}
		return str.Equals(toCheck, comp);
	}

	public static bool CTContains(this string str, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
	{
		if (str == null)
		{
			throw new ArgumentNullException("str");
		}
		return str.IndexOf(toCheck, comp) >= 0;
	}

	public static bool CTContainsAny(this string str, string searchTerms, char splitChar = ' ')
	{
		if (str == null)
		{
			throw new ArgumentNullException("str");
		}
		if (string.IsNullOrEmpty(searchTerms))
		{
			return true;
		}
		char[] separator = new char[1] { splitChar };
		return searchTerms.Split(separator, StringSplitOptions.RemoveEmptyEntries).Any((string searchTerm) => str.CTContains(searchTerm));
	}

	public static bool CTContainsAll(this string str, string searchTerms, char splitChar = ' ')
	{
		if (str == null)
		{
			throw new ArgumentNullException("str");
		}
		if (string.IsNullOrEmpty(searchTerms))
		{
			return true;
		}
		char[] separator = new char[1] { splitChar };
		return searchTerms.Split(separator, StringSplitOptions.RemoveEmptyEntries).All((string searchTerm) => str.CTContains(searchTerm));
	}

	public static void CTShuffle<T>(this T[] array)
	{
		if (array == null || array.Length == 0)
		{
			throw new ArgumentNullException("array");
		}
		int num = array.Length;
		while (num > 1)
		{
			int num2 = rd.Next(num--);
			T val = array[num];
			array[num] = array[num2];
			array[num2] = val;
		}
	}

	public static string CTDump<T>(this T[] array)
	{
		if (array == null || array.Length == 0)
		{
			throw new ArgumentNullException("array");
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			T val = array[i];
			if (0 < stringBuilder.Length)
			{
				stringBuilder.Append(Environment.NewLine);
			}
			stringBuilder.Append(val.ToString());
		}
		return stringBuilder.ToString();
	}

	public static string[] CTToString<T>(this T[] array)
	{
		if (array == null || array.Length == 0)
		{
			throw new ArgumentNullException("array");
		}
		string[] array2 = new string[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].ToString();
		}
		return array2;
	}

	public static void CTShuffle<T>(this IList<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		int count = list.Count;
		while (count > 1)
		{
			int index = rd.Next(count--);
			T value = list[count];
			list[count] = list[index];
			list[index] = value;
		}
	}

	public static string CTDump<T>(this List<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (T item in list)
		{
			if (0 < stringBuilder.Length)
			{
				stringBuilder.Append(Environment.NewLine);
			}
			stringBuilder.Append(item.ToString());
		}
		return stringBuilder.ToString();
	}

	public static List<string> CTToString<T>(this List<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		List<string> list2 = new List<string>(list.Count);
		foreach (T item in list)
		{
			list2.Add(item.ToString());
		}
		return list2;
	}

	public static void CTAddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException("collection");
		}
		foreach (KeyValuePair<T, S> item in collection)
		{
			if (!source.ContainsKey(item.Key))
			{
				source.Add(item.Key, item.Value);
			}
			else
			{
				Debug.LogWarning("Duplicate key found: " + item.Key);
			}
		}
	}
}
