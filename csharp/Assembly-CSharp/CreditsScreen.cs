using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
	public Transform animGroup;

	public Transform creditsGroup;

	public TMP_FontAsset font;

	public Material headerFontMaterial;

	public Material nameFontMaterial;

	public Material titleFontMaterial;

	public float headerSpacingBefore = 30f;

	public float headerSpacingAfter = 20f;

	public float headerFontSize = 200f;

	public float nameSpacing = 10f;

	public float nameFontSize = 150f;

	public float titleSpacingBefore = 20f;

	public float titleSpacingAfter = 30f;

	public float titleFontSize = 200f;

	public float scrollSpeed = 1f;

	public Transform cameraUpperCenter;

	public Transform cameraLowerCenter;

	public Transform lastItem;

	public List<CreditItem> credits = new List<CreditItem>();

	private string[] cyborgVO = new string[4] { "sfx_vo_cyborg_endcredits_kpr1", "sfx_vo_cyborg_endcredits_kpr2", "sfx_vo_cyborg_endcredits_kpr3", "sfx_vo_cyborg_endcredits_kpr4" };

	private string[] ravenVO = new string[4] { "sfx_vo_raven_endcredits_1", "sfx_vo_raven_endcredits_2", "sfx_vo_raven_endcredits_3", "sfx_vo_raven_endcredits_4" };

	private void OnEnable()
	{
		ResetScroller();
	}

	private void OnDisable()
	{
		StopCoroutine("PlayCreditsVO");
	}

	public void ResetScroller()
	{
		tk2dUIScrollableArea component = GetComponent<tk2dUIScrollableArea>();
		component.enabled = false;
		component.contentContainer.transform.localPosition = new Vector3(0f, 0f, 0f);
		component.enabled = true;
		creditsGroup.position = new Vector3(0f, cameraLowerCenter.position.y - 15f, 0f);
		StopCoroutine("PlayCreditsVO");
		StartCoroutine("PlayCreditsVO");
		StopCoroutine("Scroll");
		StartCoroutine("Scroll");
	}

	private IEnumerator Scroll()
	{
		while (true)
		{
			creditsGroup.localPosition = new Vector3(0f, creditsGroup.localPosition.y + scrollSpeed, 0f);
			if (lastItem.position.y > cameraUpperCenter.position.y + 15f)
			{
				ResetScroller();
			}
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator PlayCreditsVO()
	{
		yield return new WaitForSeconds(20f);
		string[] chosenVO = ((!(Random.value <= 0.5f)) ? ravenVO : cyborgVO);
		for (int i = 0; i < chosenVO.Length; i++)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/VO/" + chosenVO[i], GFAudioManager.VOICEOVER_GROUP, string.Empty);
			yield return new WaitForSeconds(5f);
		}
	}

	public void Rebuild()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Transform item in creditsGroup)
		{
			if (!item.gameObject.name.ToLower().Contains("sprite") && !item.gameObject.name.ToLower().Contains("buildnumber"))
			{
				list.Add(item.gameObject);
			}
		}
		list.ForEach((GameObject child) =>
		{
			Object.DestroyImmediate(child);
		});
		float num = 0f;
		CreditType creditType = CreditType.Space;
		for (int num2 = 0; num2 < credits.Count; num2++)
		{
			GameObject gameObject = new GameObject(string.Concat(credits[num2].type, " ", credits[num2].key));
			gameObject.transform.parent = creditsGroup;
			gameObject.layer = LayerMask.NameToLayer("UI");
			if (credits[num2].type != CreditType.Space)
			{
				LocalizedText localizedText = gameObject.AddComponent<LocalizedText>();
				localizedText.key = credits[num2].key;
				if (credits[num2].type != CreditType.Name)
				{
					localizedText.overrideTextAutoSizing = true;
					localizedText.textMeshSettingsOverride = new OverrideTextMeshSettings[2];
					localizedText.textMeshSettingsOverride[0].language = LanguageCode.JA;
					localizedText.textMeshSettingsOverride[0].overrideDilate = true;
					localizedText.textMeshSettingsOverride[0].faceDilate = 0.12f;
					localizedText.textMeshSettingsOverride[1].language = LanguageCode.KO;
					localizedText.textMeshSettingsOverride[1].overrideDilate = true;
					localizedText.textMeshSettingsOverride[1].faceDilate = 0.12f;
				}
			}
			if (credits[num2].type == CreditType.Header)
			{
				num -= headerSpacingBefore;
			}
			if (credits[num2].type == CreditType.Title)
			{
				num -= titleSpacingBefore;
			}
			gameObject.transform.localPosition = new Vector3(0f, num, 0f);
			TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
			textMeshPro.font = font;
			textMeshPro.alignment = TextAlignmentOptions.Midline;
			if (credits[num2].type == CreditType.Header)
			{
				textMeshPro.text = Language.Get(credits[num2].key, "MENUS,GUI");
				num -= headerSpacingAfter;
				textMeshPro.fontSharedMaterial = headerFontMaterial;
				textMeshPro.fontSize = headerFontSize;
			}
			if (credits[num2].type == CreditType.Name)
			{
				textMeshPro.text = Language.Get(credits[num2].key, "MENUS,GUI");
				num -= nameSpacing;
				textMeshPro.fontSharedMaterial = nameFontMaterial;
				textMeshPro.fontSize = nameFontSize;
			}
			if (credits[num2].type == CreditType.Title)
			{
				textMeshPro.text = Language.Get(credits[num2].key, "MENUS,GUI");
				num -= titleSpacingAfter;
				textMeshPro.fontSharedMaterial = titleFontMaterial;
				textMeshPro.fontSize = titleFontSize;
				textMeshPro.enableAutoSizing = true;
				textMeshPro.fontSizeMin = titleFontSize - 60f;
				textMeshPro.fontSizeMax = titleFontSize;
				textMeshPro.rectTransform.sizeDelta = new Vector3(textMeshPro.rectTransform.sizeDelta.x, 30f);
			}
			if (credits[num2].type == CreditType.Space)
			{
				num -= credits[num2].spaceAmount;
			}
			if (credits[num2].changeFontMaterial && credits[num2].fontMaterial != null)
			{
				textMeshPro.fontSharedMaterial = credits[num2].fontMaterial;
			}
			textMeshPro.rectTransform.sizeDelta = new Vector2(350f, textMeshPro.rectTransform.sizeDelta.y);
			textMeshPro.enableWordWrapping = true;
			if (num2 == credits.Count - 1)
			{
				lastItem = gameObject.transform;
			}
		}
	}
}
