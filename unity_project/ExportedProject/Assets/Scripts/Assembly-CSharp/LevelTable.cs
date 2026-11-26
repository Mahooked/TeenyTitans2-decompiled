using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelTable : ScriptableObject
{
	private static Dictionary<CharacterStatGrowth, Roll> statGrowths = new Dictionary<CharacterStatGrowth, Roll>
	{
		{
			CharacterStatGrowth.None,
			new Roll(0)
		},
		{
			CharacterStatGrowth.Slow,
			new Roll(1, 2, -1)
		},
		{
			CharacterStatGrowth.Medium,
			new Roll(1, 2)
		},
		{
			CharacterStatGrowth.Fast,
			new Roll(2, 2)
		}
	};

	[HideInInspector]
	public int nextRewardId;

	public List<LevelData> levelData;

	public int levelOffset;

	public bool isManual;

	public int baseXP = 10;

	public float exponent = 1.5f;

	public float divider = 1f;

	public CharacterStatGrowth hitPointsGrowth;

	public CharacterStatGrowth attackGrowth;

	public CharacterStatGrowth speedGrowth;

	public CharacterStatGrowth luckGrowth;

	public CharacterStatGrowth dodgeGrowth;

	public List<LevelUpReward> hitPointsUpgradeRewards = new List<LevelUpReward>();

	public List<LevelUpReward> attackUpgradeRewards = new List<LevelUpReward>();

	public List<LevelUpReward> speedUpgradeRewards = new List<LevelUpReward>();

	public List<LevelUpReward> luckUpgradeRewards = new List<LevelUpReward>();

	public List<LevelUpReward> dodgeUpgradeRewards = new List<LevelUpReward>();

	public List<LevelUpReward> miscRewards = new List<LevelUpReward>();

	public List<string> miscRewardsKeys = new List<string>();

	public int maxLevel
	{
		get
		{
			return levelData.Count;
		}
	}

	public LevelTable()
	{
		levelData = new List<LevelData>
		{
			new LevelData()
		};
	}

	public LevelData GetDataForLevel(int level)
	{
		int num = level - 1;
		if (num < 0 || num >= levelData.Count)
		{
			return null;
		}
		return levelData[num];
	}

	public int GetLevelByXP(int xp)
	{
		if (xp < baseXP)
		{
			xp = baseXP;
		}
		if (xp >= levelData[levelData.Count - 1].xpStart)
		{
			return maxLevel;
		}
		if (isManual)
		{
			return levelData.Find((LevelData obj) => xp >= obj.xpStart && xp < obj.xpToNextLevel).level;
		}
		return Mathf.FloorToInt(Mathf.Pow((float)xp * divider / (float)baseXP, 1f / exponent));
	}

	public int GetXPByLevel(int level)
	{
		if (level < 1 || level > maxLevel)
		{
			return 0;
		}
		if (isManual)
		{
			return levelData[level - 1].xpStart;
		}
		return Mathf.CeilToInt((float)baseXP * Mathf.Pow(level, exponent) / divider);
	}

	public int GetGrowthRoll(CharacterStatGrowth growth)
	{
		return statGrowths[growth].RollDie();
	}

	public LevelUpReward GetRewardById(int rewardId)
	{
		LevelUpReward levelUpReward = null;
		List<LevelUpReward> list = null;
		int count = levelData.Count;
		for (int i = 0; i < count; i++)
		{
			list = levelData[i].rewards;
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].uid.Equals(rewardId))
				{
					return list[j];
				}
			}
		}
		levelUpReward = hitPointsUpgradeRewards.Find((LevelUpReward x) => x.uid.Equals(rewardId));
		if (levelUpReward != null)
		{
			return levelUpReward;
		}
		levelUpReward = attackUpgradeRewards.Find((LevelUpReward x) => x.uid.Equals(rewardId));
		if (levelUpReward != null)
		{
			return levelUpReward;
		}
		levelUpReward = speedUpgradeRewards.Find((LevelUpReward x) => x.uid.Equals(rewardId));
		if (levelUpReward != null)
		{
			return levelUpReward;
		}
		levelUpReward = luckUpgradeRewards.Find((LevelUpReward x) => x.uid.Equals(rewardId));
		if (levelUpReward != null)
		{
			return levelUpReward;
		}
		levelUpReward = dodgeUpgradeRewards.Find((LevelUpReward x) => x.uid.Equals(rewardId));
		if (levelUpReward != null)
		{
			return levelUpReward;
		}
		count = miscRewards.Count;
		for (int num = 0; num < count; num++)
		{
			if (miscRewards[num].uid.Equals(rewardId))
			{
				return miscRewards[num];
			}
		}
		return null;
	}

	public LevelUpReward GetMiscReward(string key)
	{
		if (miscRewardsKeys.Contains(key))
		{
			return miscRewards[miscRewardsKeys.IndexOf(key)];
		}
		return null;
	}

	public List<LevelUpReward> GetAllRewardsByType(LevelUpRewardType type, bool includeMisc = true)
	{
		List<LevelUpReward> list = new List<LevelUpReward>();
		int count = levelData.Count;
		for (int i = 0; i < count; i++)
		{
			list.AddRange(levelData[i].rewards.FindAll((LevelUpReward obj) => obj.type == LevelUpRewardType.AbilityUnlock));
		}
		if (type == LevelUpRewardType.StatUpgrade)
		{
			list.AddRange(hitPointsUpgradeRewards);
			list.AddRange(attackUpgradeRewards);
			list.AddRange(speedUpgradeRewards);
			list.AddRange(luckUpgradeRewards);
			list.AddRange(dodgeUpgradeRewards);
		}
		if (includeMisc)
		{
			count = miscRewards.Count;
			for (int num = 0; num < count; num++)
			{
				if (miscRewards[num].type.Equals(type))
				{
					list.Add(miscRewards[num]);
				}
			}
		}
		return list;
	}
}
