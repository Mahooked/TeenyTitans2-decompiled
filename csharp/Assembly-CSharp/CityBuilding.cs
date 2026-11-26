using UnityEngine;

public class CityBuilding : MonoBehaviour
{
	[SerializeField]
	private tk2dUIItem _uiItem;

	[SerializeField]
	private BoxCollider _boxCollider;

	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private CityShopSign _sign;

	[SerializeField]
	private BuildingPad _buildingPad;

	[SerializeField]
	private bool _isOpen = true;

	public tk2dUIItem uiItem => _uiItem;

	public BoxCollider boxCollider => _boxCollider;

	public Animator animator => _animator;

	public CityShopSign sign => _sign;

	public BuildingPad buildingPad => _buildingPad;

	public bool isOpen
	{
		get
		{
			return _isOpen;
		}
		set
		{
			SetOpen(value);
		}
	}

	private void Awake()
	{
		Refresh();
	}

	public void SetOpen(bool isOpen)
	{
		_isOpen = isOpen;
		Refresh();
	}

	private void Refresh()
	{
		if (_buildingPad != null)
		{
			_buildingPad.isOn = _isOpen;
		}
		if (_isOpen)
		{
			if (_animator != null)
			{
				_animator.enabled = true;
			}
			_boxCollider.enabled = true;
		}
		else
		{
			if (_animator != null)
			{
				_animator.enabled = false;
			}
			_boxCollider.enabled = false;
		}
	}
}
