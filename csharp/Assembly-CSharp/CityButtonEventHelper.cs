using System;
using System.Collections.Generic;
using UnityEngine;

public class CityButtonEventHelper : MonoBehaviour
{
	public static CityButtonEventHelper instance;

	public List<tk2dUIItem> touchDownEvents = new List<tk2dUIItem>();

	public event Action<tk2dUIItem> tk2dUIItemOnDown;

	public event Action<tk2dUIItem> tk2dUIItemOnRelease;

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
		tk2dUIItemOnDown = null;
		tk2dUIItemOnRelease = null;
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
		if (tk2dUIItemOnDown != null)
		{
			tk2dUIItemOnDown(uiItem);
		}
	}

	private void HandleOnRelease(tk2dUIItem uiItem)
	{
		if (tk2dUIItemOnRelease != null)
		{
			tk2dUIItemOnRelease(uiItem);
		}
	}
}
