using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IAPRiddleEggOdds
{
	[Serializable]
	public class RewardEntry
	{
		public bool isActive;

		public PlayerReward reward;

		public bool isGuaranteed;

		public int weight;

		public FigureWeight[] figureWeights;
	}

	[Serializable]
	public class FigureWeight
	{
		public CharacterRarity rarity;

		public int weight;
	}

	[SerializeField]
	private RewardEntry[] _rewardEntries;

	public RewardEntry[] rewardEntries
	{
		get
		{
			return _rewardEntries;
		}
	}

	public List<RewardEntry> GetGuaranteedRewardEntries()
	{
		List<RewardEntry> list = new List<RewardEntry>();
		for (int i = 0; i < _rewardEntries.Length; i++)
		{
			if (_rewardEntries[i].isGuaranteed)
			{
				list.Add(_rewardEntries[i]);
			}
		}
		return list;
	}

	public List<RewardEntry> GetNonGuaranteedRewardEntries()
	{
		List<RewardEntry> list = new List<RewardEntry>();
		for (int i = 0; i < _rewardEntries.Length; i++)
		{
			if (!_rewardEntries[i].isGuaranteed)
			{
				list.Add(_rewardEntries[i]);
			}
		}
		return list;
	}
}
