using System;

[Serializable]
public class BlindBoxShopFigure
{
	public int characterUID;

	public int level = 1;

	public BlindBoxShopFigure Copy()
	{
		BlindBoxShopFigure blindBoxShopFigure = new BlindBoxShopFigure();
		blindBoxShopFigure.characterUID = characterUID;
		blindBoxShopFigure.level = level;
		return blindBoxShopFigure;
	}
}
