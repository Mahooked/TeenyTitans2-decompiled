using TMPro;
using UnityEngine;

public class LocalizedSpriteToText : LocalizedText
{
	public tk2dSprite baseENSprite;

	public TextMeshPro textMeshNonEN;

	protected override void Awake()
	{
		base.Awake();
		textMesh = textMeshNonEN;
	}

	protected override void Start()
	{
		base.Start();
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged += UpdateSprite;
		}
		UpdateSprite();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged -= UpdateSprite;
		}
	}

	private void UpdateSprite()
	{
		if (Language.CurrentLanguage() == LanguageCode.EN || (Language.CurrentLanguage() == LanguageCode.JA && key == "GO"))
		{
			baseENSprite.GetComponent<Renderer>().enabled = true;
			textMesh.gameObject.SetActive(false);
		}
		else
		{
			baseENSprite.GetComponent<Renderer>().enabled = false;
			textMesh.gameObject.SetActive(true);
		}
	}
}
