using System;
using TMPro;
using UnityEngine;

[Serializable]
public struct OverrideTextMeshSettings(LanguageCode newLang, bool shouldEnableAutoSizing, float newMinSize, float newMaxSize, FontStyles newFontStyle, Color newColor, float newTextDilation, float newLineSpacing, float newOutlineThickness, Color newFaceColor, float newCharacterSpace)
{
	public LanguageCode language = newLang;

	public bool enableAutoSizing = shouldEnableAutoSizing;

	public float minSize = newMinSize;

	public float maxSize = newMaxSize;

	public FontStyles fontStyle = newFontStyle;

	public Color fontColor = newColor;

	public bool overrideDilate = false;

	public float faceDilate = newTextDilation;

	public bool overrideLineSpace = false;

	public float lineSpace = newLineSpacing;

	public bool overrideFaceColor = false;

	public Color faceColor = newFaceColor;

	public bool overrideCharacterSpace = false;

	public float characterSpace = newCharacterSpace;
}
