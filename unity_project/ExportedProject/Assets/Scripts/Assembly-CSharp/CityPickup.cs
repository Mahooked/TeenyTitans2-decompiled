using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public abstract class CityPickup : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnPlayerCollected__BackingField;

	protected bool _collected;

	public bool collected
	{
		get
		{
			return _collected;
		}
		set
		{
			_collected = value;
		}
	}

	public event Action OnPlayerCollected
	{
		add
		{
			Action action = OnPlayerCollected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerCollected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnPlayerCollected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlayerCollected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	protected virtual void Start()
	{
	}

	protected virtual void OnEnable()
	{
		_collected = false;
	}

	protected virtual void OnDisable()
	{
	}

	protected abstract void PlayerCollected();

	public void Collected()
	{
		if (!_collected)
		{
			_collected = true;
			PlayerCollected();
			if (OnPlayerCollected__BackingField != null)
			{
				OnPlayerCollected__BackingField();
			}
		}
	}
}
