public class StartConversationEvent : GameEvent
{
	public Conversation conversation { get; private set; }

	public StartConversationEvent(Conversation conversation)
	{
		this.conversation = conversation;
	}
}
