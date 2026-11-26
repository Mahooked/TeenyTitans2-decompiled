using UnityEngine;

public class AccessorySelectPortrait : MonoBehaviour
{
	public Animator animator;

	public tk2dUIItem uiItem;

	public tk2dSprite portraitSprite;

	public tk2dSprite equippedSprite;

	public Transform newTag;

	private CharacterSelectPortraitMode _mode;

	private AccessoryItemDefinition _accessoryItemDefinition;

	public CharacterSelectPortraitMode mode
	{
		get
		{
			return _mode;
		}
		set
		{
			_mode = value;
			UpdateMode();
		}
	}

	public AccessoryItemDefinition accessoryItemDefinition
	{
		get
		{
			return _accessoryItemDefinition;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		newTag.gameObject.SetActive(false);
	}

	public void Init(AccessoryItemDefinition accessoryItemDefinition)
	{
		if (!(accessoryItemDefinition == _accessoryItemDefinition))
		{
			_accessoryItemDefinition = accessoryItemDefinition;
			if (_accessoryItemDefinition != null)
			{
				HandleUpdated();
			}
		}
	}

	private void HandleUpdated()
	{
		portraitSprite.SetSprite(_accessoryItemDefinition.iconSpriteId);
	}

	private void UpdateMode()
	{
		equippedSprite.gameObject.SetActive(false);
		portraitSprite.color = Color.white;
		switch (_mode)
		{
		case CharacterSelectPortraitMode.Equipped:
			equippedSprite.gameObject.SetActive(true);
			break;
		case CharacterSelectPortraitMode.Unknown:
			portraitSprite.color = Color.black;
			break;
		}
	}
}
