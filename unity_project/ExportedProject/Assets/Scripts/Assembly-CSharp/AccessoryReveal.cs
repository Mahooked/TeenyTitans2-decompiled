using TMPro;
using UnityEngine;

public class AccessoryReveal : MonoBehaviour
{
	public Animator animator;

	public TextMeshPro nameText;

	public TextMeshPro descriptionText;

	public Transform accessoryParent;

	private GameObject accessoryObj;

	private AccessoryItemDefinition _accessory;

	public AccessoryItemDefinition accessory
	{
		get
		{
			return _accessory;
		}
		set
		{
			_accessory = value;
		}
	}

	public void Init(AccessoryItemDefinition accessory)
	{
		if (!(accessory == _accessory))
		{
			_accessory = accessory;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (_accessory == null)
		{
			nameText.text = "n/a";
			descriptionText.text = "n/a";
			if (accessoryObj != null)
			{
				Object.Destroy(accessoryObj);
			}
			return;
		}
		nameText.text = Language.Get(_accessory.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS");
		descriptionText.text = TTGParseHelper.GetAccessoryDescription(_accessory);
		if (accessoryObj != null)
		{
			Object.Destroy(accessoryObj);
		}
		accessoryObj = Object.Instantiate(Resources.Load(_accessory.modelPath)) as GameObject;
		accessoryObj.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		if (accessoryObj == null)
		{
			Debug.LogError("Shop Toy Box, UpdateBoxForAccessory(), Accessory Model is NULL", base.gameObject);
			return;
		}
		accessoryObj.transform.parent = accessoryParent;
		accessoryObj.transform.localPosition = _accessory.revealOffsetPosition;
		accessoryObj.transform.localRotation = Quaternion.Euler(_accessory.revealRotation);
		accessoryObj.transform.localScale = _accessory.revealScale;
		Animator componentInChildren = accessoryObj.GetComponentInChildren<Animator>();
		if (componentInChildren != null)
		{
			componentInChildren.Play(_accessory.revealAnimation, 0, 0f);
			componentInChildren.Update(0f);
			componentInChildren.speed = 0f;
		}
	}
}
