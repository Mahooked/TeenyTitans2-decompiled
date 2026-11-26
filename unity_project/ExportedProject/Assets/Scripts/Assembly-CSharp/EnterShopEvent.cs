public class EnterShopEvent : GameEvent
{
	public ShopDefinition shopDef { get; private set; }

	public ShopInventory shopInventory { get; private set; }

	public EnterShopEvent(ShopDefinition shopDef, ShopInventory shopInventory = null)
	{
		this.shopDef = shopDef;
		this.shopInventory = shopInventory;
	}
}
