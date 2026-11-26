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

	public bool[] visualObjectActiveStates => _visualObjectActiveStates;

	public CityEntityEventConfig eventConfig => _eventConfig;

	public CityEntityEventConfig[] eventConfigs => _eventConfigs;

	public CityEntityStateEvent(string name)
		: base(name)
	{
	}
}
