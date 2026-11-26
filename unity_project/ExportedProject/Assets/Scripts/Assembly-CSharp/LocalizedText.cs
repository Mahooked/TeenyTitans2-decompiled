using System.Linq;
using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
	public string sheet = "MENUS,GUI";

	public string key;

	public int sheetIndex;

	public bool ignoreKey;

	public bool ignoreFontUpdate;

	public bool customColor;

	public bool enUseFredHeadline;

	public bool overrideTextAutoSizing;

	public OverrideTextMeshSettings[] textMeshSettingsOverride;

	protected TMP_Text textMesh;

	protected TMP_FontAsset fontAsset;

	public Color faceColor;

	public Texture faceTexture;

	public Color outlineColor;

	public float outlineThickness;

	private int stencilID;

	private int stencilComp;

	private string originalFontAsset;

	protected Color underlayColor;

	protected float faceDialate;

	protected float underlayOffsetX;

	protected float underlayOffsetY;

	protected bool underlayOn;

	private OverrideTextMeshSettings originalTextSettings;

	private bool originalSettingsInit;

	protected virtual void Awake()
	{
		textMesh = GetComponent<TMP_Text>();
	}

	protected virtual void Start()
	{
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged += UpdateFont;
		}
		if (!ignoreFontUpdate)
		{
			ForceInitFontProperties();
		}
		InitOriginalTextSettings();
		UpdateFont();
	}

	protected virtual void OnDestroy()
	{
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged -= UpdateFont;
		}
	}

	public void ForceInitFontProperties()
	{
		if (textMesh == null)
		{
			textMesh = GetComponent<TMP_Text>();
		}
		originalFontAsset = ((!(textMesh.font == null)) ? textMesh.font.name : string.Empty);
		if (!(textMesh.fontSharedMaterial == null))
		{
			faceColor = textMesh.fontSharedMaterial.GetColor(ShaderUtilities.ID_FaceColor);
			faceTexture = textMesh.fontSharedMaterial.GetTexture(ShaderUtilities.ID_FaceTex);
			faceDialate = textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
			outlineColor = textMesh.fontSharedMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
			outlineThickness = textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_OutlineWidth);
			stencilID = (int)textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_StencilID);
			stencilComp = (int)textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_StencilComp);
			underlayOn = textMesh.fontSharedMaterial.shaderKeywords.Contains("UNDERLAY_ON");
			underlayColor = textMesh.fontSharedMaterial.GetColor(ShaderUtilities.ID_UnderlayColor);
			underlayOffsetX = textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_UnderlayOffsetX);
			underlayOffsetY = textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_UnderlayOffsetY);
		}
	}

	private void InitOriginalTextSettings()
	{
		if (textMesh != null && !originalSettingsInit)
		{
			originalSettingsInit = true;
			originalTextSettings = default(OverrideTextMeshSettings);
			originalTextSettings.enableAutoSizing = textMesh.enableAutoSizing;
			if (textMesh.fontSharedMaterial != null)
			{
				originalTextSettings.faceDilate = textMesh.fontSharedMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
			}
			originalTextSettings.fontColor = textMesh.color;
			originalTextSettings.fontStyle = textMesh.fontStyle;
			originalTextSettings.lineSpace = textMesh.lineSpacing;
			originalTextSettings.maxSize = textMesh.fontSizeMax;
			originalTextSettings.minSize = textMesh.fontSizeMin;
		}
	}

	public void UpdateBakedTextAndMaterial(LanguageCode bakedLanguage)
	{
		InitOriginalTextSettings();
		LanguageCode code = Language.CurrentLanguage();
		Language.SwitchLanguage(bakedLanguage);
		string text = Language.Get(key, sheet);
		TMP_FontAsset font = Resources.Load("Fonts/" + GetLocalizedFontAsset()) as TMP_FontAsset;
		textMesh = GetComponent<TMP_Text>();
		textMesh.font = font;
		if (textMesh is TextMeshPro)
		{
			((TextMeshPro)textMesh).UpdateFontAsset();
		}
		else if (textMesh is TextMeshProUGUI)
		{
			((TextMeshProUGUI)textMesh).UpdateFontAsset();
		}
		Language.SwitchLanguage(code);
		textMesh.text = text;
		ignoreKey = true;
		ignoreFontUpdate = true;
	}

	public void UpdateFont()
	{
		if (!ignoreFontUpdate)
		{
			TMP_FontAsset font = Resources.Load("Fonts/" + GetLocalizedFontAsset()) as TMP_FontAsset;
			textMesh.font = font;
			if (textMesh is TextMeshPro)
			{
				((TextMeshPro)textMesh).UpdateFontAsset();
			}
			else if (textMesh is TextMeshProUGUI)
			{
				((TextMeshProUGUI)textMesh).UpdateFontAsset();
			}
		}
		if (!ignoreKey)
		{
			textMesh.text = Language.Get(key, sheet);
		}
		if (!ignoreFontUpdate)
		{
			Material fontMaterial = textMesh.fontMaterial;
			fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, faceColor);
			fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, outlineColor);
			if (fontMaterial.HasProperty("_FaceTex"))
			{
				fontMaterial.SetTexture(ShaderUtilities.ID_FaceTex, faceTexture);
			}
			if (Language.CurrentLanguage() != LanguageCode.KO)
			{
				if (Language.CurrentLanguage() == LanguageCode.JA)
				{
					fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Min(0.3f, faceDialate));
				}
				else if (GetLocalizedFontAsset().Contains("FREDFREDBURGERHEADLINE"))
				{
					fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Min(0.2f, faceDialate));
				}
				else
				{
					fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, faceDialate);
				}
			}
			else if (key.Equals("PLAY"))
			{
				fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -0.007f);
			}
			if (underlayOn)
			{
				fontMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, underlayColor);
				fontMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, underlayOffsetX);
				fontMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, underlayOffsetY);
			}
			if (Language.CurrentLanguage() == LanguageCode.KO || Language.CurrentLanguage() == LanguageCode.JA)
			{
				if (outlineThickness <= 0f)
				{
					fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineThickness * 0.7f);
				}
				else
				{
					fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineThickness * 0.35f);
				}
			}
			else if (GetLocalizedFontAsset().Equals("FREDFREDBURGERHEADLINE-BLACK SDF"))
			{
				fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineThickness * 0.7f);
			}
			else
			{
				fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineThickness);
			}
			fontMaterial.SetFloat(ShaderUtilities.ID_StencilID, stencilID);
			fontMaterial.SetFloat(ShaderUtilities.ID_StencilComp, stencilComp);
			textMesh.fontMaterial = fontMaterial;
		}
		if (customColor)
		{
			Material fontMaterial2 = textMesh.fontMaterial;
			fontMaterial2.SetColor(ShaderUtilities.ID_FaceColor, faceColor);
			fontMaterial2.SetColor(ShaderUtilities.ID_OutlineColor, outlineColor);
			fontMaterial2.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineThickness);
			textMesh.fontMaterial = fontMaterial2;
		}
		if (overrideTextAutoSizing)
		{
			bool flag = false;
			Material fontMaterial3 = textMesh.fontMaterial;
			OverrideTextMeshSettings[] array = textMeshSettingsOverride;
			for (int i = 0; i < array.Length; i++)
			{
				OverrideTextMeshSettings overrideTextMeshSettings = array[i];
				if (Language.CurrentLanguage() == overrideTextMeshSettings.language)
				{
					textMesh.enableAutoSizing = overrideTextMeshSettings.enableAutoSizing;
					textMesh.fontSizeMin = overrideTextMeshSettings.minSize;
					textMesh.fontSizeMax = overrideTextMeshSettings.maxSize;
					textMesh.fontStyle = overrideTextMeshSettings.fontStyle;
					if (!overrideTextMeshSettings.fontColor.CompareRGB(new Color(0f, 0f, 0f, 0f)))
					{
						textMesh.color = overrideTextMeshSettings.fontColor;
					}
					if (overrideTextMeshSettings.overrideDilate)
					{
						fontMaterial3.SetFloat(ShaderUtilities.ID_FaceDilate, overrideTextMeshSettings.faceDilate);
					}
					if (overrideTextMeshSettings.overrideLineSpace)
					{
						textMesh.lineSpacing = overrideTextMeshSettings.lineSpace;
					}
					if (overrideTextMeshSettings.overrideFaceColor)
					{
						fontMaterial3.SetColor(ShaderUtilities.ID_FaceColor, overrideTextMeshSettings.faceColor);
					}
					if (overrideTextMeshSettings.overrideCharacterSpace)
					{
						textMesh.characterSpacing = overrideTextMeshSettings.characterSpace;
					}
					flag = true;
					textMesh.fontMaterial = fontMaterial3;
					break;
				}
			}
			if (!flag)
			{
				if (!originalSettingsInit)
				{
					InitOriginalTextSettings();
				}
				textMesh.enableAutoSizing = originalTextSettings.enableAutoSizing;
				textMesh.fontSizeMin = originalTextSettings.minSize;
				textMesh.fontSizeMax = originalTextSettings.maxSize;
				textMesh.fontStyle = originalTextSettings.fontStyle;
				textMesh.color = originalTextSettings.fontColor;
				textMesh.lineSpacing = originalTextSettings.lineSpace;
				if (textMesh.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate) != originalTextSettings.faceDilate)
				{
					fontMaterial3.SetFloat(ShaderUtilities.ID_FaceDilate, originalTextSettings.faceDilate);
				}
				textMesh.fontMaterial = fontMaterial3;
			}
		}
		textMesh.havePropertiesChanged = true;
	}

	protected virtual string GetLocalizedFontAsset()
	{
		if (Language.CurrentLanguage() == LanguageCode.EN)
		{
			if (enUseFredHeadline)
			{
				return "FREDFREDBURGERHEADLINE-BLACK SDF";
			}
			if (!string.IsNullOrEmpty(originalFontAsset))
			{
				return originalFontAsset;
			}
			return "baveuse SDF1";
		}
		if (Language.CurrentLanguage() == LanguageCode.FR || Language.CurrentLanguage() == LanguageCode.IT || Language.CurrentLanguage() == LanguageCode.ES || Language.CurrentLanguage() == LanguageCode.PT || Language.CurrentLanguage() == LanguageCode.TR)
		{
			return "FREDFREDBURGERHEADLINE-BLACK SDF";
		}
		if (Language.CurrentLanguage() == LanguageCode.RU)
		{
			return "FREDFREDBURGERHEADLINE-BLACK SDF";
		}
		if (Language.CurrentLanguage() == LanguageCode.KO)
		{
			return "AdobeGothicStd-Bold KO SDF";
		}
		if (Language.CurrentLanguage() == LanguageCode.JA)
		{
			return "JA Pro W6 SDF";
		}
		return "baveuse SDF1";
	}
}
