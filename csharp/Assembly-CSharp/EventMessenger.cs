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
		if (_delegates.TryGetValue(typeof(T), out var value))
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
		if (_delegates.TryGetValue(typeof(T), out var value))
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
		if (_delegates.TryGetValue(typeof(T), out var value) && value is EventDelegate<T> eventDelegate)
		{
			eventDelegate(e);
		}
	}
}
