using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager
{
	private Dictionary<string, UnityEvent> events;

	private static EventManager _instance;

	public static EventManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new EventManager();
			}
			return _instance;
		}
	}

	public EventManager()
	{
		events = new Dictionary<string, UnityEvent>();
	}

	public static void AddListener(string eventType, UnityAction listener)
	{
		UnityEvent value = null;
		if (instance.events.TryGetValue(eventType, out value))
		{
			value.AddListener(listener);
			return;
		}
		value = new UnityEvent();
		value.AddListener(listener);
		instance.events.Add(eventType, value);
	}

	public static void RemoveListener(string eventType, UnityAction listener)
	{
		UnityEvent value = null;
		if (instance.events.TryGetValue(eventType, out value))
		{
			value.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventType)
	{
		UnityEvent value = null;
		if (instance.events.TryGetValue(eventType, out value))
		{
			value.Invoke();
		}
	}
}
