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
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(languageFileContents)))
			{
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
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["MENUS,GUI"][key]), false, false);
		}
		foreach (string key2 in currentEntrySheets["TUTORIALS"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["TUTORIALS"][key2]), false, false);
		}
		foreach (string key3 in currentEntrySheets["ITEMS,ABILITIES,ACHIEVEMENTS"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["ITEMS,ABILITIES,ACHIEVEMENTS"][key3]), false, false);
		}
		foreach (string key4 in currentEntrySheets["DIALOGUE TEXT"].Keys)
		{
			text += ArabicFixer.Fix(ReverseText(currentEntrySheets["DIALOGUE TEXT"][key4]), false, false);
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
				return ArabicFixer.Fix(ReverseText(currentEntrySheets[sheetTitle][key]), false, false).ToUpper();
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
		switch (name)
		{
		case SystemLanguage.Afrikaans:
			return LanguageCode.AF;
		case SystemLanguage.Arabic:
			return LanguageCode.AR;
		case SystemLanguage.Basque:
			return LanguageCode.BA;
		case SystemLanguage.Belarusian:
			return LanguageCode.BE;
		case SystemLanguage.Bulgarian:
			return LanguageCode.BG;
		case SystemLanguage.Catalan:
			return LanguageCode.CA;
		case SystemLanguage.Chinese:
			return LanguageCode.ZH;
		case SystemLanguage.Czech:
			return LanguageCode.CS;
		case SystemLanguage.Danish:
			return LanguageCode.DA;
		case SystemLanguage.Dutch:
			return LanguageCode.NL;
		case SystemLanguage.English:
			return LanguageCode.EN;
		case SystemLanguage.Estonian:
			return LanguageCode.ET;
		case SystemLanguage.Faroese:
			return LanguageCode.FA;
		case SystemLanguage.Finnish:
			return LanguageCode.FI;
		case SystemLanguage.French:
			return LanguageCode.FR;
		case SystemLanguage.German:
			return LanguageCode.DE;
		case SystemLanguage.Greek:
			return LanguageCode.EL;
		case SystemLanguage.Hebrew:
			return LanguageCode.HE;
		case SystemLanguage.Hungarian:
			return LanguageCode.HU;
		case SystemLanguage.Icelandic:
			return LanguageCode.IS;
		case SystemLanguage.Indonesian:
			return LanguageCode.ID;
		case SystemLanguage.Italian:
			return LanguageCode.IT;
		case SystemLanguage.Japanese:
			return LanguageCode.JA;
		case SystemLanguage.Korean:
			return LanguageCode.KO;
		case SystemLanguage.Latvian:
			return LanguageCode.LA;
		case SystemLanguage.Lithuanian:
			return LanguageCode.LT;
		case SystemLanguage.Norwegian:
			return LanguageCode.NO;
		case SystemLanguage.Polish:
			return LanguageCode.PL;
		case SystemLanguage.Portuguese:
			return LanguageCode.PT;
		case SystemLanguage.Romanian:
			return LanguageCode.RO;
		case SystemLanguage.Russian:
			return LanguageCode.RU;
		case SystemLanguage.SerboCroatian:
			return LanguageCode.SH;
		case SystemLanguage.Slovak:
			return LanguageCode.SK;
		case SystemLanguage.Slovenian:
			return LanguageCode.SL;
		case SystemLanguage.Spanish:
			return LanguageCode.ES;
		case SystemLanguage.Swedish:
			return LanguageCode.SW;
		case SystemLanguage.Thai:
			return LanguageCode.TH;
		case SystemLanguage.Turkish:
			return LanguageCode.TR;
		case SystemLanguage.Ukrainian:
			return LanguageCode.UK;
		case SystemLanguage.Vietnamese:
			return LanguageCode.VI;
		default:
			switch (name)
			{
			case SystemLanguage.Hungarian:
				return LanguageCode.HU;
			case SystemLanguage.Unknown:
				return LanguageCode.N;
			default:
				return LanguageCode.N;
			}
		}
	}
}
