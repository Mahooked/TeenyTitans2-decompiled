public class TeleportEvent : GameEvent
{
	public PathNode pathNode { get; private set; }

	public bool stopMovement { get; private set; }

	public TeleportEvent(PathNode pathNode, bool stopMovement = true)
	{
		this.pathNode = pathNode;
		this.stopMovement = stopMovement;
	}
}
