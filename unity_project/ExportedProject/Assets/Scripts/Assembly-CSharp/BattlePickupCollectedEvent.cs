public class BattlePickupCollectedEvent : GameEvent
{
	public BattlePickup pickup { get; private set; }

	public BattlePickupCollectedEvent(BattlePickup pickup)
	{
		this.pickup = pickup;
	}
}
