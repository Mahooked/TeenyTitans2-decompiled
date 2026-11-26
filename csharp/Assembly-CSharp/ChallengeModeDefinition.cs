using System;
using UnityEngine;

[Serializable]
public class ChallengeModeDefinition : ScriptableObject
{
	public string challengeModeSprite;

	public int challengeModeSpriteId;

	public bool isAvailable;

	public int minChallengesBetween = 1;

	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	[SerializeField]
	private string _challengeModeName = string.Empty;

	[SerializeField]
	private string _challengeModeNameKey = string.Empty;

	[SerializeField]
	private string _challengeModeDescription = string.Empty;

	[SerializeField]
	private string _challengeModeDescriptionKey = string.Empty;

	[SerializeField]
	private ChallengeModeType _challengeModeType;

	[SerializeField]
	private ChallengeModeValues _hardModeValues = new ChallengeModeValues();

	[SerializeField]
	private ChallengeModeValues _easyModeValues = new ChallengeModeValues();

	public int uid => _uid;

	public string challengeModeName => _challengeModeName;

	public string challengeModeNameKey => _challengeModeNameKey;

	public string challengeModeDescription => _challengeModeDescription;

	public string challengeModeDescriptionKey => _challengeModeDescriptionKey;

	public ChallengeModeType challengeModeType => _challengeModeType;

	public ChallengeModeValues hardModeValues => _hardModeValues;

	public ChallengeModeValues easyModeValues => _easyModeValues;
}
