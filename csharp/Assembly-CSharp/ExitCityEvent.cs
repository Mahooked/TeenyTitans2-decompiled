public class ExitCityEvent : GameEvent
{
	public string destSceneName { get; private set; }

	public string startAtNodeNamed { get; private set; }

	public bool startFacingRight { get; private set; }

	public ExitCityEvent(string destSceneName, string startAtNodeNamed = "", bool startFacingRight = false)
	{
		this.destSceneName = destSceneName;
		this.startAtNodeNamed = startAtNodeNamed;
		this.startFacingRight = startFacingRight;
	}
}
