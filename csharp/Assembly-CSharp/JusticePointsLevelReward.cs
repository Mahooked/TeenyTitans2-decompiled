using System;

[Serializable]
public class JusticePointsLevelReward
{
	public JusticePointsLevelUpRewardType rewardType;

	public CharacterRewardData characterReward = new CharacterRewardData();

	public int modchipUID;

	public int coins;
}
