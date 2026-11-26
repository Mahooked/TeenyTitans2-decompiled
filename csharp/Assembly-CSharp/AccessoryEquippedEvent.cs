public class AccessoryEquippedEvent : GameEvent
{
	public int uid { get; private set; }

	public AccessoryEquippedEvent(int uid)
	{
		this.uid = uid;
	}
}
