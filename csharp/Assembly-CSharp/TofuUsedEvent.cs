public class TofuUsedEvent : GameEvent
{
	public Character character { get; private set; }

	public TofuType tofuType { get; private set; }

	public TofuUsedEvent(Character character, TofuType tofuType)
	{
		this.character = character;
		this.tofuType = tofuType;
	}
}
