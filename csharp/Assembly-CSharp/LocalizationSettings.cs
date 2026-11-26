using System;
using UnityEngine;

[Serializable]
public class LocalizationSettings : ScriptableObject
{
	public string[] sheetTitles;

	public bool useSystemLanguagePerDefault = true;

	public string defaultLangCode = "EN";

	public static LanguageCode GetLanguageEnum(string langCode)
	{
		langCode = langCode.ToUpper();
		foreach (LanguageCode value in Enum.GetValues(typeof(LanguageCode)))
		{
			if (string.Concat(value, string.Empty) == langCode)
			{
				return value;
			}
		}
		Debug.LogError("ERORR: There is no language: [" + langCode + "]");
		return LanguageCode.EN;
	}
}
