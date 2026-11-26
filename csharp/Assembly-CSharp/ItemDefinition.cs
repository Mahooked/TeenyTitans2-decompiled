using System;
using UnityEngine;

[Serializable]
public class ItemDefinition : ScriptableObject
{
	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	[SerializeField]
	private string _itemName = string.Empty;

	[SerializeField]
	private string _itemNameKey = string.Empty;

	[SerializeField]
	private string _itemDescription = string.Empty;

	[SerializeField]
	private string _itemDescriptionKey = string.Empty;

	[SerializeField]
	private string _itemShortDescription = string.Empty;

	[SerializeField]
	private string _itemShortDescriptionKey = string.Empty;

	[SerializeField]
	private int _stackSize = 1;

	[SerializeField]
	private string _iconSprite = string.Empty;

	[SerializeField]
	private int _iconSpriteId = -1;

	public int uid => _uid;

	public string itemName => _itemName;

	public string itemNameKey => _itemNameKey;

	public string itemDescription => _itemDescription;

	public string itemDescriptionKey => _itemDescriptionKey;

	public string itemShortDescription => _itemShortDescription;

	public string itemShortDescriptionKey => _itemShortDescriptionKey;

	public int stackSize => _stackSize;

	public string iconSprite => _iconSprite;

	public int iconSpriteId => _iconSpriteId;
}
