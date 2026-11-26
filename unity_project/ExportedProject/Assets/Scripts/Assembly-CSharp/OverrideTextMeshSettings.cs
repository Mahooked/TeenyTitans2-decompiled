using System;
using TMPro;
using UnityEngine;

[Serializable]
public struct OverrideTextMeshSettings
{
	public LanguageCode language;

	public bool enableAutoSizing;

	public float minSize;

	public float maxSize;

	public FontStyles fontStyle;

	public Color fontColor;

	public bool overrideDilate;

	public float faceDilate;

	public bool overrideLineSpace;

	public float lineSpace;

	public bool overrideFaceColor;

	public Color faceColor;

	public bool overrideCharacterSpace;

	public float characterSpace;

	public OverrideTextMeshSettings(LanguageCode newLang, bool shouldEnableAutoSizing, float newMinSize, float newMaxSize, FontStyles newFontStyle, Color newColor, float newTextDilation, float newLineSpacing, float newOutlineThickness, Color newFaceColor, float newCharacterSpace)
	{
		language = newLang;
		enableAutoSizing = shouldEnableAutoSizing;
		minSize = newMinSize;
		maxSize = newMaxSize;
		fontStyle = newFontStyle;
		fontColor = newColor;
		faceDilate = newTextDilation;
		lineSpace = newLineSpacing;
		faceColor = newFaceColor;
		characterSpace = newCharacterSpace;
		overrideDilate = false;
		overrideLineSpace = false;
		overrideFaceColor = false;
		overrideCharacterSpace = false;
	}
}
