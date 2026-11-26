using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDatabase : ScriptableObject
{
	[SerializeField]
	private List<ItemDefinition> _items;

	[SerializeField]
	private int _nextId = -1;

	public List<ModChipDefinition> modChips
	{
		get
		{
			return _items.FindAll((ItemDefinition obj) => obj is ModChipDefinition).ConvertAll((ItemDefinition input) => (ModChipDefinition)input);
		}
	}

	public IEnumerable<AccessoryItemDefinition> accessories
	{
		get
		{
			return _items.FindAll((ItemDefinition x) => x is AccessoryItemDefinition).ConvertAll((ItemDefinition x) => (AccessoryItemDefinition)x);
		}
	}

	public List<ItemDefinition> items
	{
		get
		{
			return _items;
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

	private void OnEnable()
	{
		if (_items == null)
		{
			_items = new List<ItemDefinition>();
		}
	}

	public ItemDefinition GetItem(int uid)
	{
		return _items.Find((ItemDefinition obj) => obj.uid == uid);
	}

	public List<ItemDefinition> GetItems(List<int> uids)
	{
		List<ItemDefinition> list = new List<ItemDefinition>();
		int count = uids.Count;
		for (int i = 0; i < count; i++)
		{
			list.Add(_items.Find((ItemDefinition obj) => obj.uid == uids[i]));
		}
		return list;
	}

	public List<ItemDefinition> GetAllItems<T>() where T : ItemDefinition
	{
		return _items.FindAll((ItemDefinition x) => x.GetType().Equals(typeof(T)));
	}
}
