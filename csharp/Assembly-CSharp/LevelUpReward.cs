using System;

[Serializable]
public class LevelUpReward
{
	public int uid;

	public LevelUpRewardType type;

	public bool isForced;

	public bool isUnique;

	public bool isPermanent;

	public CharacterStatType statType;

	public int statValueInt;

	public int abilityID = -1;

	public LevelUpReward(int uid)
	{
		this.uid = uid;
	}
}
