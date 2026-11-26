using System;

[Serializable]
public class ShopInventorySetup
{
	public ShopInventory inventoryFile;

	public bool timeInitiated;

	public bool newsFlashInitiated;

	public bool missionActiveInitiated;

	public bool missionCompleteInitiated;

	public int missionUID;

	public int newsFlashUID;

	public int priority;
}
