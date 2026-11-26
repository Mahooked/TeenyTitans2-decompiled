using UnityEngine;

public class AccessorySlot : MonoBehaviour
{
	public tk2dSprite sprite;

	public tk2dSprite backgroundSprite;

	public tk2dUIItem uiItem;

	private AccessoryItemDefinition _accessoryItemDefinition;

	public AccessoryItemDefinition accessoryItemDefinition
	{
		get
		{
			return _accessoryItemDefinition;
		}
	}

	public void Init(AccessoryItemDefinition itemDef)
	{
		_accessoryItemDefinition = itemDef;
		if (sprite == null)
		{
			Debug.LogError("Accessory Slot has a NULL Sprite reference... " + base.gameObject.name, base.gameObject);
		}
		else if (_accessoryItemDefinition != null)
		{
			sprite.gameObject.SetActive(true);
			sprite.SetSprite(_accessoryItemDefinition.iconSprite);
		}
		else
		{
			sprite.gameObject.SetActive(false);
		}
	}

	public void SetGrayedOut(bool grayedOut)
	{
		if (grayedOut)
		{
			uiItem.enabled = false;
			tk2dSprite obj = backgroundSprite;
			Color gray = Color.gray;
			sprite.color = gray;
			obj.color = gray;
		}
		else
		{
			uiItem.enabled = true;
			tk2dSprite obj2 = backgroundSprite;
			Color gray = Color.white;
			sprite.color = gray;
			obj2.color = gray;
		}
	}
}
