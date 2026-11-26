using System;

[Serializable]
public class LegionOfDoomPortraitUnlock
{
	public enum UnlockCondition
	{
		None,
		Gold,
		Level,
		LevelAndGold
	}

	public string portraitSprite;

	public int portraitSpriteID;

	public UnlockCondition unlockCondition;

	public int goldCost;

	public int requiredLevel;
}
