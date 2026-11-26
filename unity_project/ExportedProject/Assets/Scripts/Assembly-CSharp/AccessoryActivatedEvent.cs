public class AccessoryActivatedEvent : GameEvent
{
	public Accessory accessory { get; private set; }

	public AccessoryActivatedEvent(Accessory accessory)
	{
		this.accessory = accessory;
	}
}
