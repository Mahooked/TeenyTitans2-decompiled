using UnityEngine;

public class RiddleEggRevealRewardRepaint : RiddleEggRevealReward
{
	[Space]
	public Transform figureContainer;

	public RepaintColorSlotGUI colorSlot;

	private ShopFigureModel figureModel;

	public override void Init(PlayerReward reward)
	{
		base.Init(reward);
		CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(reward.characterUID);
		RepaintColorSlot repaintColorSlotForID = character.GetRepaintColorSlotForID(reward.repaintUID);
		colorSlot.Init(repaintColorSlotForID);
		ShopFigureModel shopFigureModel = Resources.Load<ShopFigureModel>(character.shopFigurePath);
		if (shopFigureModel != null)
		{
			figureModel = Object.Instantiate(shopFigureModel);
			figureModel.transform.SetParent(figureContainer, true);
			figureModel.transform.localPosition = Vector3.zero;
			figureModel.transform.localRotation = Quaternion.identity;
			figureModel.transform.localScale = Vector3.one;
		}
		if (repaintColorSlotForID != null && figureModel != null)
		{
			Texture texture = Resources.Load<Texture>(repaintColorSlotForID.texturePathScaled);
			if (texture != null)
			{
				figureModel.UpdateTexture(texture);
			}
		}
	}

	public override void ResetObject()
	{
		base.ResetObject();
		if (figureModel != null)
		{
			Object.Destroy(figureModel.gameObject);
			figureModel = null;
		}
	}
}
