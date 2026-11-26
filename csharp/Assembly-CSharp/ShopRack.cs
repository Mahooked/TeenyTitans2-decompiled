using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopRack
{
	[SerializeField]
	public List<ShopHanger> hangers = new List<ShopHanger>();

	public bool glassCase;
}
