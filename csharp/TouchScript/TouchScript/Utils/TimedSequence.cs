using System;
using System.Collections.Generic;
using UnityEngine;

namespace TouchScript.Utils;

internal sealed class TimedSequence<T>
{
	private List<T> points = new List<T>();

	private List<float> timestamps = new List<float>();

	public void Add(T element)
	{
		Add(element, Time.time);
	}

	public void Add(T element, float time)
	{
		points.Add(element);
		timestamps.Add(time);
	}

	public void Clear()
	{
		points.Clear();
		timestamps.Clear();
	}

	public IList<T> FindElementsLaterThan(float time)
	{
		List<T> list = new List<T>();
		int num = points.Count - 1;
		while (num >= 0 && timestamps[num] > time)
		{
			list.Add(points[num]);
			num--;
		}
		list.Reverse();
		return list;
	}

	public IList<T> FindElementsLaterThan(float time, out float lastTime)
	{
		List<T> list = new List<T>();
		int num = points.Count - 1;
		while (num >= 0 && timestamps[num] > time)
		{
			list.Add(points[num]);
			num--;
		}
		list.Reverse();
		if (num < points.Count - 1)
		{
			lastTime = timestamps[num + 1];
		}
		else
		{
			lastTime = time;
		}
		return list;
	}

	public IList<T> FindElementsLaterThan(float time, Predicate<T> predicate)
	{
		List<T> list = new List<T>();
		int num = points.Count - 1;
		while (num >= 0 && timestamps[num] > time)
		{
			if (predicate(points[num]))
			{
				list.Add(points[num]);
			}
			num--;
		}
		list.Reverse();
		return list;
	}
}
