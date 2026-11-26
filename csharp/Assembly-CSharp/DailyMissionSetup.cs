using System;
using UnityEngine;

[Serializable]
public class DailyMissionSetup
{
	[Serializable]
	public class CharacterRollOptions
	{
		public RangedInt level;

		public RangedInt shinyRank;
	}

	public DailyMissionType type;

	public int difficultyLevel;

	public RangedInt rangedIntValue1;

	public PlayerReward[] rewards;

	[SerializeField]
	private CharacterRollOptions _characterRollOptions;

	public CharacterRollOptions characterRollOptions => _characterRollOptions;
}
