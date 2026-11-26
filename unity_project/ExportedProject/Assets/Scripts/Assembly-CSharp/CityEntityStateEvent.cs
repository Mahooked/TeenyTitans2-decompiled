using System;
using UnityEngine;

[Serializable]
public class CityEntityStateEvent : CityEntityState
{
	[SerializeField]
	private bool[] _visualObjectActiveStates;

	[SerializeField]
	private CityEntityEventConfig _eventConfig;

	[SerializeField]
	private CityEntityEventConfig[] _eventConfigs;

	public bool[] visualObjectActiveStates
	{
		get
		{
			return _visualObjectActiveStates;
		}
	}

	public CityEntityEventConfig eventConfig
	{
		get
		{
			return _eventConfig;
		}
	}

	public CityEntityEventConfig[] eventConfigs
	{
		get
		{
			return _eventConfigs;
		}
	}

	public CityEntityStateEvent(string name)
		: base(name)
	{
	}
}
