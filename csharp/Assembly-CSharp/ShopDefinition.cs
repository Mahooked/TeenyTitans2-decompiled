using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopDefinition : ScriptableObject
{
	[SerializeField]
	public TimeOfDay timeOfDay;

	public ShopType shopType;

	public CityLocation district;

	public bool opensWithNewsFlash;

	public int opensWithNewsFlashUID;

	public int restocksInDays = 1;

	public List<int> newsFlashes = new List<int>();

	public ShopInventory defaultInventory;

	public List<ShopInventorySetup> additionalInventories = new List<ShopInventorySetup>();

	public string shopModelPrefab;

	public string shopWallPrefab;

	public string shopRackPrefab;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _nameKey = string.Empty;

	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	public new string name => _name;

	public string nameKey => _nameKey;

	public int uid => _uid;
}
