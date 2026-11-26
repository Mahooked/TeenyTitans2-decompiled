using UnityEngine;

public class BuildingPad : MonoBehaviour
{
	public GameObject buildingObj;

	public tk2dUIItem uiItem;

	public GameObject openModel;

	public GameObject closedModel;

	private IInteractiveBuilding building;

	private bool _isOn;

	public bool isOn
	{
		get
		{
			return _isOn;
		}
		set
		{
			_isOn = value;
			HandleOpenStatusUpdated(_isOn);
		}
	}

	private void HandleOpenStatusUpdated(bool isOpen)
	{
		openModel.SetActive(isOpen);
		closedModel.SetActive(!isOpen);
	}
}
