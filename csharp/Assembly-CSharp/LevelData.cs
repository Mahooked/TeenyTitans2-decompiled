using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
	public int level = 1;

	public int xpStart;

	public int xpToNextLevel = 100;

	public List<LevelUpReward> rewards = new List<LevelUpReward>();

	public bool fillWithStats = true;
}
