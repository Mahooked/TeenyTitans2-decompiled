using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementsDatabase : ScriptableObject
{
	[SerializeField]
	public List<AchievementData> achievements = new List<AchievementData>();

	private static List<string> reportedAchievements = new List<string>();

	public void ResetAchievements()
	{
		foreach (AchievementData achievement in achievements)
		{
			SaveUtility.SetInt(achievement.id.ToString(), 0, false);
			SaveUtility.SetInt(achievement.id.ToString() + "_PROGRESS", 0, false);
		}
	}

	public AchievementData AchievementDataForId(string achievementId)
	{
		foreach (AchievementData achievement in achievements)
		{
			if (achievement.id.ToLower() == achievementId.ToLower())
			{
				return achievement;
			}
		}
		return null;
	}

	public bool IsAchievementUnlocked(string achievemntId)
	{
		return SaveUtility.GetInt(achievemntId, 0) == 1;
	}

	public static void UnlockAchievementId(string achievementId, int amount = 0, float bannerDelay = 0f)
	{
		if (reportedAchievements.Contains(achievementId) && !GameManager.instance.achievementsDatabase.AchievementDataForId(achievementId).reportProgress)
		{
			return;
		}
		AchievementData achievementData = GameManager.instance.achievementsDatabase.AchievementDataForId(achievementId);
		if (achievementData == null)
		{
			return;
		}
		bool flag = true;
		if (achievementData.reportProgress)
		{
			int num = SaveUtility.GetInt(achievementId + "_PROGRESS") + amount;
			SaveUtility.SetInt(achievementId + "_PROGRESS", num, false);
			int amountNeeded = achievementData.amountNeeded;
			float b = (float)num / (float)amountNeeded * 100f;
			b = Mathf.Min(100f, b);
			ReportAchievementId(achievementId, b);
			if (b < 100f)
			{
				flag = false;
			}
			else if (reportedAchievements.Contains(achievementId))
			{
				return;
			}
		}
		if (flag)
		{
			if (SaveUtility.GetInt(achievementId) != 1)
			{
				SaveUtility.SetInt(achievementId, 1, false);
				AchievementBanner.instance.DisplayAchievementID(achievementId, bannerDelay);
			}
			reportedAchievements.Add(achievementId);
		}
	}

	private static void ReportAchievementId(string achievementId, double progress)
	{
	}

	public void ReportAllUnlockedAchievements()
	{
	}

	public void UnlockAllAchievements()
	{
		foreach (AchievementData achievement in achievements)
		{
			UnlockAchievementId(achievement.id);
		}
	}
}
