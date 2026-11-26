using System;
using System.Collections.Generic;

[Serializable]
public class ShopFigure
{
	public int characterUID;

	public bool foldOut;

	public int level = 1;

	public int cost;

	public int ticketCost;

	public bool modchip;

	public ToyType toyType;

	public int modchipUID;

	public int accessoryUID;

	public bool blindBox;

	public bool goldBox;

	public int blindBoxCost = 100;

	public int blindBoxSize = 1;

	public bool overrideFigure;

	public int overrideCollectedUID;

	public int overrideNewUID;

	public int overrideNewCost = 300;

	public int overrideNewLevel = 1;

	public List<BlindBoxShopFigure> blindBoxShopFigures = new List<BlindBoxShopFigure>();

	public ModChipQuality modchipQuality;

	public string characterName => GameManager.instance.characterDatabase.GetCharacter(characterUID).characterName;

	public string modChipName => GameManager.instance.itemDatabase.GetItem(modchipUID).itemName;

	public string characterAssetName
	{
		get
		{
			if (!string.IsNullOrEmpty(GameManager.instance.characterDatabase.GetCharacter(characterUID).characterNameKey))
			{
				return GameManager.instance.characterDatabase.GetCharacter(characterUID).characterNameKey.ToLower();
			}
			string text = GameManager.instance.characterDatabase.GetCharacter(characterUID).characterName.ToLower();
			text = text.Replace(" ", string.Empty);
			return text.Replace("'", string.Empty);
		}
	}

	public string characterAssetNameKey => GameManager.instance.characterDatabase.GetCharacter(characterUID).characterNameKey;

	public ShopFigure Clone(ShopFigure shopFigure)
	{
		shopFigure.characterUID = characterUID;
		shopFigure.level = level;
		shopFigure.cost = cost;
		shopFigure.ticketCost = ticketCost;
		shopFigure.modchip = modchip;
		shopFigure.toyType = toyType;
		shopFigure.modchipUID = modchipUID;
		shopFigure.accessoryUID = accessoryUID;
		shopFigure.blindBox = blindBox;
		shopFigure.goldBox = goldBox;
		shopFigure.blindBoxCost = blindBoxCost;
		shopFigure.blindBoxSize = blindBoxSize;
		shopFigure.overrideFigure = overrideFigure;
		shopFigure.overrideCollectedUID = overrideCollectedUID;
		shopFigure.overrideNewUID = overrideNewUID;
		shopFigure.overrideNewCost = overrideNewCost;
		shopFigure.overrideNewLevel = overrideNewLevel;
		shopFigure.modchipQuality = modchipQuality;
		for (int i = 0; i < blindBoxShopFigures.Count; i++)
		{
			BlindBoxShopFigure blindBoxShopFigure = new BlindBoxShopFigure();
			blindBoxShopFigure.characterUID = blindBoxShopFigures[i].characterUID;
			blindBoxShopFigure.level = blindBoxShopFigures[i].level;
			shopFigure.blindBoxShopFigures.Add(blindBoxShopFigure);
		}
		return shopFigure;
	}
}
