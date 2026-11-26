using System;
using UnityEngine;

[Serializable]
public class CityEntityStateBuilding : CityEntityState
{
	[Serializable]
	public class CityBuildingOverrideSettings
	{
		public bool overrideIsOpen;

		public bool isOpenOverride;

		public bool overrideSignState;

		public CityShopSign.State signStateOverride;
	}

	[SerializeField]
	private CityBuildingOverrideSettings _overrideSettings;

	[SerializeField]
	private CityEntityInteractConfig _interactConfig;

	public CityBuildingOverrideSettings overrideSettings => _overrideSettings;

	public CityEntityInteractConfig interactConfig => _interactConfig;

	public CityEntityStateBuilding(string name)
		: base(name)
	{
	}
}
