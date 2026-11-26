using System;
using System.Collections.Generic;

[Serializable]
public class JusticePointsLevelData
{
	public bool foldout;

	public int low;

	public int high;

	public int amount = 10;

	public bool newRank;

	public string rankNameKey;

	public string rankBadgeSprite;

	public int rankBadgeSpriteId = -1;

	public List<JusticePointsLevelReward> rewards = new List<JusticePointsLevelReward>();

	public Conversation conversation;
}
