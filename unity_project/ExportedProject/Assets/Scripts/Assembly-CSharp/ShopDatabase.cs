using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopDatabase : ScriptableObject
{
	[SerializeField]
	private List<ShopDefinition> _shops;

	[SerializeField]
	private int _nextId = -1;

	public List<ShopDefinition> shops
	{
		get
		{
			return _shops;
		}
	}

	private int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	public ShopDefinition GetShopDef(int uid)
	{
		int count = _shops.Count;
		for (int i = 0; i < count; i++)
		{
			if (_shops[i].uid == uid)
			{
				return _shops[i];
			}
		}
		return null;
	}

	public ShopDefinition GetShopDefByType(ShopType type)
	{
		int count = _shops.Count;
		for (int i = 0; i < count; i++)
		{
			if (_shops[i].shopType == type)
			{
				return _shops[i];
			}
		}
		return null;
	}
}
