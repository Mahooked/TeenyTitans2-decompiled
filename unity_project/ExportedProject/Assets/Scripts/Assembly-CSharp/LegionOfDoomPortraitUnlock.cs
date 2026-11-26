using System;

[Serializable]
public class LegionOfDoomPortraitUnlock
{
	public enum UnlockCondition
	{
		None = 0,
		Gold = 1,
		Level = 2,
		LevelAndGold = 3
	}

	public string portraitSprite;

	public int portraitSpriteID;

	public UnlockCondition unlockCondition;

	public int goldCost;

	public int requiredLevel;
}
