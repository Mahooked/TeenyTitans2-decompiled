public class EventEntityInteractEvent : GameEvent
{
	public CityEntityEvent eventEntity { get; private set; }

	public EventEntityInteractEvent(CityEntityEvent eventEntity)
	{
		this.eventEntity = eventEntity;
	}
}
