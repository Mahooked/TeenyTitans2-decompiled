using System;
using System.Collections.Generic;

[Serializable]
public class ShopPurchaseData
{
	public string storeInventoryName;

	public int lastRestockDay;

	public List<string> purchasedFigures = new List<string>();
}
