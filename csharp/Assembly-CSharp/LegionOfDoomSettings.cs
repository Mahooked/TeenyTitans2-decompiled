using System.Collections.Generic;
using UnityEngine;

public class LegionOfDoomSettings : ScriptableObject
{
	public int coinRewardWin;

	public int coinRewardLoss;

	[Header("XP Settings")]
	public int winXP;

	public int lossXP;

	public int humanWinXP;

	public int winStreakXP;

	public int maxWinStreak;

	[Header("Skill Rating Settings")]
	public int winSkillRating;

	public int lossSkillRating;

	public int minSkillRating;

	public int maxSkillRating = 5000;

	[Header("CPU Settings")]
	public StatWeights cpuStatDecisionWeights;

	[Tooltip("When Mr CPU plays with max level figs, those fig's stats are rolled as if their level was (Max Level + Over Level).")]
	public int overLevel;

	[Header("Conversation Triggers")]
	public List<LegionOfDoomTriggeredConvo> conversations;

	[Header("Portrait Unlocks")]
	public List<LegionOfDoomPortraitUnlock> portraitUnlocks;

	[Header("Portrait Border Unlocks")]
	public List<LegionOfDoomBorderUnlock> borderUnlocks;

	[Header("Player Name Options")]
	public List<string> namePart1Options;

	public List<string> namePart2Options;

	public List<string> namePart3Options;

	[SerializeField]
	private int _nextTriggeredConvoId = -1;

	private int nextTriggeredConvoId
	{
		get
		{
			_nextTriggeredConvoId++;
			return _nextTriggeredConvoId;
		}
	}

	public string GenerateRandomPlayerName()
	{
		string text = namePart1Options[Random.Range(0, namePart1Options.Count)];
		string text2 = namePart2Options[Random.Range(0, namePart2Options.Count)];
		string text3 = namePart3Options[Random.Range(0, namePart3Options.Count)];
		return text + text2 + text3;
	}

	public string GetRandomPortraitSprite()
	{
		return portraitUnlocks[Random.Range(0, portraitUnlocks.Count)].portraitSprite;
	}

	public string GetBorderSpriteForRank(int rank)
	{
		List<LegionOfDoomBorderUnlock> list = new List<LegionOfDoomBorderUnlock>(borderUnlocks);
		list.Sort((LegionOfDoomBorderUnlock x, LegionOfDoomBorderUnlock y) => x.level.CompareTo(y.level));
		for (int num = list.Count - 1; num >= 0; num--)
		{
			LegionOfDoomBorderUnlock legionOfDoomBorderUnlock = list[num];
			if (rank >= legionOfDoomBorderUnlock.level)
			{
				return legionOfDoomBorderUnlock.borderSprite;
			}
		}
		return (list.Count <= 0) ? string.Empty : list[0].borderSprite;
	}

	public string GetNumberPlateSpriteForRank(int rank)
	{
		string borderSpriteForRank = GetBorderSpriteForRank(rank);
		if (!string.IsNullOrEmpty(borderSpriteForRank))
		{
			if (borderSpriteForRank.Contains("gold05"))
			{
				return "portrait_numberplate_gold05";
			}
			if (borderSpriteForRank.Contains("gold"))
			{
				return "portrait_numberplate_gold";
			}
			if (borderSpriteForRank.Contains("silver"))
			{
				return "portrait_numberplate_silver";
			}
		}
		return "portrait_numberplate_bronze";
	}

	public LegionOfDoomTriggeredConvo GetConvoById(int id)
	{
		for (int i = 0; i < conversations.Count; i++)
		{
			if (conversations[i].id == id)
			{
				return conversations[i];
			}
		}
		return null;
	}
}
