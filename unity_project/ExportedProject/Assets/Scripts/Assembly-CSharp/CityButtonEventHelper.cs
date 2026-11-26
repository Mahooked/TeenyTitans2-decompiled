using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class CityButtonEventHelper : MonoBehaviour
{
	public static CityButtonEventHelper instance;

	public List<tk2dUIItem> touchDownEvents = new List<tk2dUIItem>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<tk2dUIItem> tk2dUIItemOnDown__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<tk2dUIItem> tk2dUIItemOnRelease__BackingField;

	public event Action<tk2dUIItem> tk2dUIItemOnDown
	{
		add
		{
			Action<tk2dUIItem> action = tk2dUIItemOnDown__BackingField;
			Action<tk2dUIItem> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref tk2dUIItemOnDown__BackingField, (Action<tk2dUIItem>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<tk2dUIItem> action = tk2dUIItemOnDown__BackingField;
			Action<tk2dUIItem> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref tk2dUIItemOnDown__BackingField, (Action<tk2dUIItem>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<tk2dUIItem> tk2dUIItemOnRelease
	{
		add
		{
			Action<tk2dUIItem> action = tk2dUIItemOnRelease__BackingField;
			Action<tk2dUIItem> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref tk2dUIItemOnRelease__BackingField, (Action<tk2dUIItem>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<tk2dUIItem> action = tk2dUIItemOnRelease__BackingField;
			Action<tk2dUIItem> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref tk2dUIItemOnRelease__BackingField, (Action<tk2dUIItem>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
		tk2dUIItemOnDown__BackingField = null;
		tk2dUIItemOnRelease__BackingField = null;
		for (int i = 0; i < touchDownEvents.Count; i++)
		{
			if (touchDownEvents[i] != null)
			{
				touchDownEvents[i].OnDownUIItem -= HandleOnDown;
				touchDownEvents[i].OnReleaseUIItem -= HandleOnRelease;
			}
		}
	}

	private void Start()
	{
		for (int i = 0; i < touchDownEvents.Count; i++)
		{
			if (touchDownEvents[i] != null)
			{
				touchDownEvents[i].OnDownUIItem += HandleOnDown;
				touchDownEvents[i].OnReleaseUIItem += HandleOnRelease;
			}
		}
	}

	private void HandleOnDown(tk2dUIItem uiItem)
	{
		if (tk2dUIItemOnDown__BackingField != null)
		{
			tk2dUIItemOnDown__BackingField(uiItem);
		}
	}

	private void HandleOnRelease(tk2dUIItem uiItem)
	{
		if (tk2dUIItemOnRelease__BackingField != null)
		{
			tk2dUIItemOnRelease__BackingField(uiItem);
		}
	}
}
