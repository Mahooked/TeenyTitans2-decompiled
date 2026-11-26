using UnityEngine;

public class TitleScreenLogo : MonoBehaviour
{
	public tk2dSprite teenySprite;

	public tk2dSprite titansSprite;

	public tk2dSprite figureSprite;

	public tk2dSprite figureTextSprite;

	private void Awake()
	{
		EventMessenger.Instance.AddListener<LanguageChangedEvent>(HandleLanguageChangedEvent);
		Refresh();
	}

	private void OnDestroy()
	{
		EventMessenger.Instance.RemoveListener<LanguageChangedEvent>(HandleLanguageChangedEvent);
	}

	private void HandleLanguageChangedEvent(LanguageChangedEvent evt)
	{
		Refresh();
	}

	private void Refresh()
	{
		switch (Language.CurrentLanguage())
		{
		case LanguageCode.ES:
			figureTextSprite.SetSprite("title_logo_figuretext_es");
			break;
		case LanguageCode.PT:
			figureTextSprite.SetSprite("title_logo_figuretext_pt");
			break;
		default:
			figureTextSprite.SetSprite("title_logo_figuretext");
			break;
		}
	}
}
