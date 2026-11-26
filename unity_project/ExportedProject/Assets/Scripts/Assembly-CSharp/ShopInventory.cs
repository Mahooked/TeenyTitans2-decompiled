using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopInventory : ScriptableObject
{
	[SerializeField]
	public string shopName;

	[SerializeField]
	public List<ShopRack> racks = new List<ShopRack>();
}
