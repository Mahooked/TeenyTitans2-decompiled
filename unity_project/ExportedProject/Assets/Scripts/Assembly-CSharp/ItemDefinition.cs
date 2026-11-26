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

	public int uid
	{
		get
		{
			return _uid;
		}
	}

	public string itemName
	{
		get
		{
			return _itemName;
		}
	}

	public string itemNameKey
	{
		get
		{
			return _itemNameKey;
		}
	}

	public string itemDescription
	{
		get
		{
			return _itemDescription;
		}
	}

	public string itemDescriptionKey
	{
		get
		{
			return _itemDescriptionKey;
		}
	}

	public string itemShortDescription
	{
		get
		{
			return _itemShortDescription;
		}
	}

	public string itemShortDescriptionKey
	{
		get
		{
			return _itemShortDescriptionKey;
		}
	}

	public int stackSize
	{
		get
		{
			return _stackSize;
		}
	}

	public string iconSprite
	{
		get
		{
			return _iconSprite;
		}
	}

	public int iconSpriteId
	{
		get
		{
			return _iconSpriteId;
		}
	}
}
