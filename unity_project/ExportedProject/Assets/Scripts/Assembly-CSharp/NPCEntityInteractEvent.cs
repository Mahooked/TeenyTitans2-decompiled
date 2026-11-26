public class NPCEntityInteractEvent : GameEvent
{
	public CityEntityNPC npcEntity { get; private set; }

	public NPCEntityInteractEvent(CityEntityNPC npcEntity)
	{
		this.npcEntity = npcEntity;
	}
}
