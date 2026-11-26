public class ViewedCharacterInLibraryEvent : GameEvent
{
	public int uid { get; private set; }

	public ViewedCharacterInLibraryEvent(int uid)
	{
		this.uid = uid;
	}
}
