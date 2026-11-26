using System;
using UnityEngine;

public abstract class CityPickup : MonoBehaviour
{
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

	public event Action OnPlayerCollected;

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
			if (OnPlayerCollected != null)
			{
				OnPlayerCollected();
			}
		}
	}
}
