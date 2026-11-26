public class InventoryItem
{
	public ItemDefinition item;

	public int count;

	public InventoryItem(ItemDefinition item)
	{
		this.item = item;
		count = 1;
	}

	public InventoryItem(ItemDefinition item, int count)
	{
		this.item = item;
		this.count = count;
	}

	public InventoryItem(InventoryItemData data)
	{
		Init(data);
	}

	public void Init(InventoryItemData data)
	{
		item = GameManager.instance.itemDatabase.GetItem(data.itemID);
		count = data.count;
	}

	public InventoryItemData GetInventoryItemData()
	{
		return new InventoryItemData(item.uid, count);
	}
}
