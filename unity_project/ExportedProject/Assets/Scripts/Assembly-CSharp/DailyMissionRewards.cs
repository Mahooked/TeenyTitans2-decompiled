using System;
using UnityEngine;

public class DailyMissionRewards : ScriptableObject
{
	[Serializable]
	public class DailyMissionReward
	{
		public DailyMissionRewardType rewardType;

		public int rewardAmount;
	}

	[Serializable]
	public enum DailyMissionRewardType
	{
		RainbowCoins = 0,
		RiddlerEggs = 1
	}

	[SerializeField]
	private DailyMissionReward[] _rewards = new DailyMissionReward[5];

	public DailyMissionReward[] rewards
	{
		get
		{
			return _rewards;
		}
	}
}
