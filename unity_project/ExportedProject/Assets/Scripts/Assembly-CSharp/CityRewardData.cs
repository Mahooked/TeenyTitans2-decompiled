using System.Collections.Generic;

public class CityRewardData
{
	public int coinAmount;

	public int rainbowCoinAmount;

	public int characterUID = -1;

	public int characterLvl;

	public int chipUID = -1;

	public int accessoryUID = -1;

	public int questItemUID = -1;

	public int repaintUID = -1;

	public bool blindBox;

	public bool goldBox;

	public List<PlayerReward> eggRewards;

	public CityRewardData()
	{
		coinAmount = 0;
		rainbowCoinAmount = 0;
		characterUID = -1;
		characterLvl = 1;
		chipUID = -1;
		accessoryUID = -1;
		repaintUID = -1;
		eggRewards = null;
	}

	public CityRewardData(int coin, int character, int charLvl, int chip, bool isBlindBox = false, bool isGoldBox = false, int rainbowCoin = 0, int repaintUID = -1)
	{
		coinAmount = coin;
		rainbowCoinAmount = rainbowCoin;
		characterUID = character;
		characterLvl = charLvl;
		chipUID = chip;
		blindBox = isBlindBox;
		goldBox = isGoldBox;
		this.repaintUID = repaintUID;
	}

	public CityRewardData(int accessory, bool isQuestItem = false)
	{
		if (isQuestItem)
		{
			questItemUID = accessory;
		}
		else
		{
			accessoryUID = accessory;
		}
		coinAmount = 0;
		rainbowCoinAmount = 0;
		characterUID = -1;
		characterLvl = 0;
		chipUID = -1;
		repaintUID = -1;
		blindBox = false;
		goldBox = false;
	}

	public CityRewardData(List<PlayerReward> eggRewards)
	{
		this.eggRewards = eggRewards;
	}
}
