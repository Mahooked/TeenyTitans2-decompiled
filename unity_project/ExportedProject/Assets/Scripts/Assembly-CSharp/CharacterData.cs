using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class CharacterData
{
	public int uid;

	public int level = 1;

	public int xp;

	public int shinyPoints;

	public int shinyRank;

	public int repaintColorID;

	[OptionalField]
	public int hitPoints = -1;

	[OptionalField]
	public int attack = -1;

	[OptionalField]
	public int speed = -1;

	[OptionalField]
	public int luck = -1;

	[OptionalField]
	public int dodge = -1;

	public bool isViewed;

	public bool isFavorite;

	public List<int> abilityUIDs;

	public int characterUID = -1;

	[OptionalField]
	public int lastRewardLevel = 1;

	[OptionalField]
	public List<int> selectedRewards;

	public List<int> earnedRewards;

	public List<int> repaintColorsUnlocked;

	public int hitPointsTier;

	public int attackTier;

	public int speedTier;

	public int luckTier;

	public int dodgeTier;

	[OptionalField]
	public bool modChipUnlocked;

	[OptionalField]
	public int modChipUID = -1;

	public CharacterData()
	{
		abilityUIDs = new List<int>();
		selectedRewards = new List<int>();
		earnedRewards = new List<int>();
		repaintColorsUnlocked = new List<int>();
	}

	[OnDeserializing]
	private void HandleSerializationVersioning(StreamingContext context)
	{
		lastRewardLevel = 1;
		selectedRewards = new List<int>();
		repaintColorsUnlocked = new List<int>();
		modChipUnlocked = false;
		modChipUID = -1;
	}
}
