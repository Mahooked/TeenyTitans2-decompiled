using TMPro;
using UnityEngine;

public class LocalizedObjToText : LocalizedText
{
	public GameObject baseGameObject;

	public TextMeshPro textMeshNonEN;

	public GameObject activateObjectWithText;

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
			baseGameObject.SetActive(value: true);
			textMesh.gameObject.SetActive(value: false);
			activateObjectWithText.SetActive(value: false);
		}
		else
		{
			baseGameObject.SetActive(value: false);
			textMesh.gameObject.SetActive(value: true);
			activateObjectWithText.SetActive(value: true);
		}
	}
}
