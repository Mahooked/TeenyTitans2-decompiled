using UnityEngine;

public class AccessorySlot : MonoBehaviour
{
	public tk2dSprite sprite;

	public tk2dSprite backgroundSprite;

	public tk2dUIItem uiItem;

	private AccessoryItemDefinition _accessoryItemDefinition;

	public AccessoryItemDefinition accessoryItemDefinition => _accessoryItemDefinition;

	public void Init(AccessoryItemDefinition itemDef)
	{
		_accessoryItemDefinition = itemDef;
		if (sprite == null)
		{
			Debug.LogError("Accessory Slot has a NULL Sprite reference... " + gameObject.name, gameObject);
		}
		else if (_accessoryItemDefinition != null)
		{
			sprite.gameObject.SetActive(value: true);
			sprite.SetSprite(_accessoryItemDefinition.iconSprite);
		}
		else
		{
			sprite.gameObject.SetActive(value: false);
		}
	}

	public void SetGrayedOut(bool grayedOut)
	{
		if (grayedOut)
		{
			uiItem.enabled = false;
			tk2dSprite tk2dSprite2 = backgroundSprite;
			Color gray = Color.gray;
			sprite.color = gray;
			tk2dSprite2.color = gray;
		}
		else
		{
			uiItem.enabled = true;
			tk2dSprite tk2dSprite3 = backgroundSprite;
			Color gray = Color.white;
			sprite.color = gray;
			tk2dSprite3.color = gray;
		}
	}
}
