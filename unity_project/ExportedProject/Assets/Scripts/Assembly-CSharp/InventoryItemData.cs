using System;

[Serializable]
public class InventoryItemData
{
	public int itemID = -1;

	public int count = 1;

	public InventoryItemData(int itemID, int count)
	{
		this.itemID = itemID;
		this.count = count;
	}
}
