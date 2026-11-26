using System;
using System.Collections.Generic;

[Serializable]
public class LegionOfDoomLevelData
{
	public RangedInt levelRange;

	public int xpToLevelUp;

	public List<LevelMilestone> milestones = new List<LevelMilestone>();

	public PlayerReward[] rewards = new PlayerReward[0];
}
