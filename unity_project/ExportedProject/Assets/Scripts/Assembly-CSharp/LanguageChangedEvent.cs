public class LanguageChangedEvent : GameEvent
{
	public LanguageCode languageCode { get; private set; }

	public LanguageChangedEvent(LanguageCode languageCode)
	{
		this.languageCode = languageCode;
	}
}
