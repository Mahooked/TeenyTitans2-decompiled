using System;
using System.Collections.Generic;

public class EventMessenger
{
	public delegate void EventDelegate<T>(T e) where T : GameEvent;

	private static EventMessenger _instance;

	private readonly Dictionary<Type, Delegate> _delegates = new Dictionary<Type, Delegate>();

	public static EventMessenger Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new EventMessenger();
			}
			return _instance;
		}
	}

	public void AddListener<T>(EventDelegate<T> listener) where T : GameEvent
	{
		Delegate value;
		if (_delegates.TryGetValue(typeof(T), out value))
		{
			_delegates[typeof(T)] = Delegate.Combine(value, listener);
		}
		else
		{
			_delegates[typeof(T)] = listener;
		}
	}

	public void RemoveListener<T>(EventDelegate<T> listener) where T : GameEvent
	{
		Delegate value;
		if (_delegates.TryGetValue(typeof(T), out value))
		{
			Delegate obj = Delegate.Remove(value, listener);
			if ((object)obj == null)
			{
				_delegates.Remove(typeof(T));
			}
			else
			{
				_delegates[typeof(T)] = obj;
			}
		}
	}

	public void Raise<T>(T e) where T : GameEvent
	{
		if (e == null)
		{
			throw new ArgumentNullException("e");
		}
		Delegate value;
		if (_delegates.TryGetValue(typeof(T), out value))
		{
			EventDelegate<T> eventDelegate = value as EventDelegate<T>;
			if (eventDelegate != null)
			{
				eventDelegate(e);
			}
		}
	}
}
