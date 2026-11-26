public class RiddlerShopFigureButton : RiddlerShopProductButton
{
	public tk2dSprite iconSprite;

	public override void Init(IAPProduct product)
	{
		base.Init(product);
		PlayerReward playerReward = product.rewards[0];
		CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(playerReward.figure.characterUID);
		headerText.text = ((!string.IsNullOrEmpty(character.characterNameKey)) ? Language.Get(character.characterNameKey, "NPCS,FIGS") : character.characterName);
		if (string.IsNullOrEmpty(character.iapShopIconSprite))
		{
			iconSprite.gameObject.SetActive(value: false);
			return;
		}
		iconSprite.gameObject.SetActive(value: true);
		iconSprite.SetSprite(character.iapShopIconSprite);
	}
}
