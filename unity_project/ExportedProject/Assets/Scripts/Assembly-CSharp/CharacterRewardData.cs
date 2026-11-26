using System;
using System.Collections.Generic;

[Serializable]
public class CharacterRewardData
{
	public int characterUID;

	public int characterLvl = 1;

	public bool blindBox;

	public bool goldBox;

	public List<BlindBoxShopFigure> blindBoxShopFigures = new List<BlindBoxShopFigure>();

	public CharacterRewardData()
	{
	}

	public CharacterRewardData(int characterUID)
	{
		this.characterUID = characterUID;
	}

	public CharacterRewardData Copy()
	{
		CharacterRewardData characterRewardData = new CharacterRewardData();
		characterRewardData.characterUID = characterUID;
		characterRewardData.characterLvl = characterLvl;
		characterRewardData.blindBox = blindBox;
		characterRewardData.goldBox = goldBox;
		for (int i = 0; i < blindBoxShopFigures.Count; i++)
		{
			characterRewardData.blindBoxShopFigures.Add(blindBoxShopFigures[i].Copy());
		}
		return characterRewardData;
	}
}
