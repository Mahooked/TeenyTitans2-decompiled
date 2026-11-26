using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using ArabicSupport;
using UnityEngine;

public static class Language
{
	private static List<LanguageCode> excludedLanguages;

	public static string settingsAssetPath;

	private static LocalizationSettings _settings;

	private static List<string> availableLanguages;

	private static LanguageCode currentLanguage;

	private static Dictionary<string, Dictionary<string, string>> currentEntrySheets;

	public static LocalizationSettings settings
	{
		get
		{
			if (_settings == null)
			{
				try
				{
					string path = "Languages/" + Path.GetFileNameWithoutExtension(settingsAssetPath);
					_settings = (LocalizationSettings)Resources.Load(path, typeof(LocalizationSettings));
				}
				catch
				{
				}
			}
			return _settings;
		}
	}

	static Language()
	{
		excludedLanguages = new List<LanguageCode>
		{
			LanguageCode.JA,
			LanguageCode.KO
		};
		settingsAssetPath = "Assets/Localization/Resources/Languages/LocalizationSettings.asset";
		_settings = null;
		currentLanguage = LanguageCode.N;
		Setup();
	}

	public static void Setup()
	{
		LoadAvailableLanguages();
		bool useSystemLanguagePerDefault = settings.useSystemLanguagePerDefault;
		LanguageCode code = LocalizationSettings.GetLanguageEnum(settings.defaultLangCode);
		string text = PlayerPrefs.GetString("M2H_lastLanguage", string.Empty);
		if (text != string.Empty && availableLanguages.Contains(text))
		{
			SwitchLanguage(text);
			return;
		}
		if (useSystemLanguagePerDefault)
		{
			LanguageCode languageCode = LanguageNameToCode(Application.systemLanguage);
			if (languageCode == LanguageCode.N)
			{
				string twoLetterISOLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
				if (twoLetterISOLanguageName != "iv")
				{
					languageCode = LocalizationSettings.GetLanguageEnum(twoLetterISOLanguageName);
				}
			}
			if (availableLanguages.Contains(string.Concat(languageCode, string.Empty)))
			{
				code = languageCode;
			}
			else
			{
				switch (languageCode)
				{
				case LanguageCode.PT:
					if (availableLanguages.Contains(string.Concat(LanguageCode.PT_BR, string.Empty)))
					{
						code = LanguageCode.PT_BR;
					}
					break;
				case LanguageCode.EN:
					if (availableLanguages.Contains(string.Concat(LanguageCode.EN_GB, string.Empty)))
					{
						code = LanguageCode.EN_GB;
					}
					break;
				default:
					if (languageCode == LanguageCode.EN && availableLanguages.Contains(string.Concat(LanguageCode.EN_US, string.Empty)))
					{
						code = LanguageCode.EN_US;
					}
					break;
				}
			}
		}
		SwitchLanguage(code);
	}

	public static LanguageCode GetSystemLanguageCode()
	{
		LanguageCode languageEnum = LocalizationSettings.GetLanguageEnum(settings.defaultLangCode);
		LanguageCode languageCode = LanguageNameToCode(Application.systemLanguage);
		if (languageCode == LanguageCode.N)
		{
			string twoLetterISOLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			if (twoLetterISOLanguageName != "iv")
			{
				languageCode = LocalizationSettings.GetLanguageEnum(twoLetterISOLanguageName);
			}
		}
		return languageCode;
	}

	private static void LoadAvailableLanguages()
	{
		availableLanguages = new List<string>();
		if (settings.sheetTitles == null || settings.sheetTitles.Length <= 0)
		{
			Debug.Log("None available");
			return;
		}
		foreach (LanguageCode value in Enum.GetValues(typeof(LanguageCode)))
		{
			if (HasLanguageFile(string.Concat(value, string.Empty), settings.sheetTitles[0]) && !excludedLanguages.Contains(value))
			{
				availableLanguages.Add(string.Concat(value, string.Empty));
			}
		}
		Resources.UnloadUnusedAssets();
	}

	public static string[] GetLanguages()
	{
		return availableLanguages.ToArray();
	}

	public static bool SwitchLanguage(string langCode)
	{
		return SwitchLanguage(LocalizationSettings.GetLanguageEnum(langCode));
	}

	public static bool SwitchLanguage(LanguageCode code)
	{
		if (availableLanguages.Contains(string.Concat(code, string.Empty)))
		{
			DoSwitch(code);
			return true;
		}
		Debug.LogError(string.Concat("Could not switch from language ", currentLanguage, " to ", code));
		if (currentLanguage == LanguageCode.N)
		{
			if (availableLanguages.Count > 0)
			{
				DoSwitch(LocalizationSettings.GetLanguageEnum(availableLanguages[0]));
				Debug.LogError(string.Concat("Switched to ", currentLanguage, " instead"));
			}
			else
			{
				Debug.LogError(string.Concat("Please verify that you have the file: Resources/Languages/", code, string.Empty));
				Debug.Break();
			}
		}
		return false;
	}

	private static void DoSwitch(LanguageCode newLang)
	{
		PlayerPrefs.SetString("M2H_lastLanguage", string.Concat(newLang, string.Empty));
		currentLanguage = newLang;
		currentEntrySheets = new Dictionary<string, Dictionary<string, string>>();
		string[] sheetTitles = settings.sheetTitles;
		foreach (string text in sheetTitles)
		{
			currentEntrySheets[text] = new Dictionary<string, string>();
			string languageFileContents = GetLanguageFileContents(text);
			if (!(languageFileContents != string.Empty))
			{
				continue;
			}
			using XmlReader xmlReader = XmlReader.Create(new StringReader(languageFileContents));
			while (xmlReader.ReadToFollowing("entry"))
			{
				xmlReader.MoveToFirstAttribute();
				string value = xmlReader.Value;
				xmlReader.MoveToElement();
				string s = xmlReader.ReadElementContentAsString().Trim();
				s = s.UnescapeXML();
				currentEntrySheets[text][value] = s;
			}
		}
		LocalizedAsset[] array = (LocalizedAsset[])UnityEngine.Object.FindObjectsOfType(typeof(LocalizedAsset));
		LocalizedAsset[] array2 = array;
		foreach (LocalizedAsset localizedAsset in array2)
		{
			localizedAsset.LocalizeAsset();
		}
		SendMonoMessage("ChangedLanguage", currentLanguage);
	}

	public static UnityEngine.Object GetAsset(string name)
	{
		return Resources.Load(string.Concat("Languages/Assets/", CurrentLanguage(), "/", name));
	}

	private static bool HasLanguageFile(string lang, string sheetTitle)
	{
		return (TextAsset)Resources.Load("Languages/" + lang + "_" + sheetTitle, typeof(TextAsset)) != null;
	}

	private static string GetLanguageFileContents(string sheetTitle)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(string.Concat("Languages/", currentLanguage, "_", sheetTitle), typeof(TextAsset));
		return (!(textAsset != null)) ? string.Empty : textAsset.text;
	}

	public static LanguageCode CurrentLanguage()
	{
		return currentLanguage;
	}

	public static string Get(string key)
	{
		return Get(key, settings.sheetTitles[0]);
	}

	public static string ReverseText(string input)
	{
		char[] array = input.ToCharArray();
		Array.Reverse(array);
		return new string(array);
	}

	public static void GetAllArabic()
	{
		string text = " ";
		SwitchLanguage(LanguageCode.AR);
		foreach (string key in currentEntrySheets["MENUS,GUI"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["MENUS,GUI"][key]), showTashkeel: false, useHinduNumbers: false);
		}
		foreach (string key2 in currentEntrySheets["TUTORIALS"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["TUTORIALS"][key2]), showTashkeel: false, useHinduNumbers: false);
		}
		foreach (string key3 in currentEntrySheets["ITEMS,ABILITIES,ACHIEVEMENTS"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["ITEMS,ABILITIES,ACHIEVEMENTS"][key3]), showTashkeel: false, useHinduNumbers: false);
		}
		foreach (string key4 in currentEntrySheets["DIALOGUE TEXT"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["DIALOGUE TEXT"][key4]), showTashkeel: false, useHinduNumbers: false);
		}
		Debug.Log(text);
	}

	public static string Get(string key, string sheetTitle)
	{
		if (currentEntrySheets == null || !currentEntrySheets.ContainsKey(sheetTitle))
		{
			Debug.LogError("The sheet with title \"" + sheetTitle + "\" does not exist!");
			return string.Empty;
		}
		if (currentEntrySheets[sheetTitle].ContainsKey(key))
		{
			if (CurrentLanguage() == LanguageCode.AR)
			{
				return ArabicFixer.Fix(ReverseText(currentEntrySheets[sheetTitle][key]), showTashkeel: false, useHinduNumbers: false).ToUpper();
			}
			return currentEntrySheets[sheetTitle][key];
		}
		return "#!#" + key + "#!#";
	}

	private static bool ForceUppercase()
	{
		if (CurrentLanguage() == LanguageCode.FR || CurrentLanguage() == LanguageCode.IT || CurrentLanguage() == LanguageCode.ES || CurrentLanguage() == LanguageCode.PT || CurrentLanguage() == LanguageCode.RU)
		{
			return true;
		}
		return false;
	}

	public static bool Has(string key)
	{
		return Has(key, settings.sheetTitles[0]);
	}

	public static bool Has(string key, string sheetTitle)
	{
		if (currentEntrySheets == null || !currentEntrySheets.ContainsKey(sheetTitle))
		{
			return false;
		}
		return currentEntrySheets[sheetTitle].ContainsKey(key);
	}

	private static void SendMonoMessage(string methodString, params object[] parameters)
	{
		if (parameters != null && parameters.Length > 1)
		{
			Debug.LogError("We cannot pass more than one argument currently!");
		}
		GameObject[] array = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if ((bool)gameObject && gameObject.transform.parent == null)
			{
				if (parameters != null && parameters.Length == 1)
				{
					gameObject.gameObject.BroadcastMessage(methodString, parameters[0], SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					gameObject.gameObject.BroadcastMessage(methodString, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	public static LanguageCode LanguageNameToCode(SystemLanguage name)
	{
		return name switch
		{
			SystemLanguage.Afrikaans => LanguageCode.AF, 
			SystemLanguage.Arabic => LanguageCode.AR, 
			SystemLanguage.Basque => LanguageCode.BA, 
			SystemLanguage.Belarusian => LanguageCode.BE, 
			SystemLanguage.Bulgarian => LanguageCode.BG, 
			SystemLanguage.Catalan => LanguageCode.CA, 
			SystemLanguage.Chinese => LanguageCode.ZH, 
			SystemLanguage.Czech => LanguageCode.CS, 
			SystemLanguage.Danish => LanguageCode.DA, 
			SystemLanguage.Dutch => LanguageCode.NL, 
			SystemLanguage.English => LanguageCode.EN, 
			SystemLanguage.Estonian => LanguageCode.ET, 
			SystemLanguage.Faroese => LanguageCode.FA, 
			SystemLanguage.Finnish => LanguageCode.FI, 
			SystemLanguage.French => LanguageCode.FR, 
			SystemLanguage.German => LanguageCode.DE, 
			SystemLanguage.Greek => LanguageCode.EL, 
			SystemLanguage.Hebrew => LanguageCode.HE, 
			SystemLanguage.Hungarian => LanguageCode.HU, 
			SystemLanguage.Icelandic => LanguageCode.IS, 
			SystemLanguage.Indonesian => LanguageCode.ID, 
			SystemLanguage.Italian => LanguageCode.IT, 
			SystemLanguage.Japanese => LanguageCode.JA, 
			SystemLanguage.Korean => LanguageCode.KO, 
			SystemLanguage.Latvian => LanguageCode.LA, 
			SystemLanguage.Lithuanian => LanguageCode.LT, 
			SystemLanguage.Norwegian => LanguageCode.NO, 
			SystemLanguage.Polish => LanguageCode.PL, 
			SystemLanguage.Portuguese => LanguageCode.PT, 
			SystemLanguage.Romanian => LanguageCode.RO, 
			SystemLanguage.Russian => LanguageCode.RU, 
			SystemLanguage.SerboCroatian => LanguageCode.SH, 
			SystemLanguage.Slovak => LanguageCode.SK, 
			SystemLanguage.Slovenian => LanguageCode.SL, 
			SystemLanguage.Spanish => LanguageCode.ES, 
			SystemLanguage.Swedish => LanguageCode.SW, 
			SystemLanguage.Thai => LanguageCode.TH, 
			SystemLanguage.Turkish => LanguageCode.TR, 
			SystemLanguage.Ukrainian => LanguageCode.UK, 
			SystemLanguage.Vietnamese => LanguageCode.VI, 
			_ => name switch
			{
				SystemLanguage.Hungarian => LanguageCode.HU, 
				SystemLanguage.Unknown => LanguageCode.N, 
				_ => LanguageCode.N, 
			}, 
		};
	}
}
