using System;

[Serializable]
public class PlayerReward
{
	public PlayerRewardType type;

	public CurrencyType currencyType;

	public RiddleEggType riddleEggType;

	public int amount;

	public int itemUID = -1;

	public CharacterRewardData figure;

	public PlayerReward[] eggRewards;

	public int characterUID = -1;

	public int repaintUID = -1;

	public bool isRepaintRandom;

	public bool isRepaintRare;

	public PlayerReward Copy()
	{
		PlayerReward playerReward = new PlayerReward();
		playerReward.type = type;
		playerReward.currencyType = currencyType;
		playerReward.riddleEggType = riddleEggType;
		playerReward.amount = amount;
		playerReward.itemUID = itemUID;
		playerReward.figure = ((figure == null) ? null : figure.Copy());
		if (eggRewards != null)
		{
			playerReward.eggRewards = new PlayerReward[eggRewards.Length];
			for (int i = 0; i < eggRewards.Length; i++)
			{
				playerReward.eggRewards[i] = eggRewards[i].Copy();
			}
		}
		playerReward.characterUID = characterUID;
		playerReward.repaintUID = repaintUID;
		playerReward.isRepaintRandom = isRepaintRandom;
		playerReward.isRepaintRare = isRepaintRare;
		return playerReward;
	}
}
