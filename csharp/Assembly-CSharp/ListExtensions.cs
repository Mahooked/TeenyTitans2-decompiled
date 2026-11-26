using System;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
	private static System.Random rng = new System.Random();

	public static T Pop<T>(this List<T> list)
	{
		if (list.Count == 0)
		{
			return default;
		}
		T result = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
		return result;
	}

	public static T Shift<T>(this List<T> list)
	{
		if (list.Count == 0)
		{
			Debug.LogException(new Exception("The list is empty."));
			return default;
		}
		T result = list[0];
		list.RemoveAt(0);
		return result;
	}

	public static List<T> ShiftAll<T>(this List<T> list)
	{
		List<T> result = new List<T>(list);
		list.Clear();
		return result;
	}

	public static int Unshift<T>(this List<T> list, T element)
	{
		list.Insert(0, element);
		return list.Count;
	}

	public static List<T> Concat<T>(this List<T> list, List<T> otherList)
	{
		List<T> list2 = new List<T>(list);
		list2.AddRange(otherList);
		return list2;
	}

	public static T GetFirstItem<T>(this List<T> list)
	{
		if (list.Count == 0)
		{
			return default;
		}
		return list[0];
	}

	public static T GetLastItem<T>(this List<T> list)
	{
		if (list.Count == 0)
		{
			return default;
		}
		return list[list.Count - 1];
	}

	public static T GetRandomItem<T>(this List<T> list)
	{
		if (list.Count == 0)
		{
			return default;
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		int num = list.Count;
		while (num > 1)
		{
			num--;
			int index = rng.Next(num + 1);
			T value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
	}
}
