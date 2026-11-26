using System.Collections.Generic;
using UnityEngine;

public class ShopReceipt : MonoBehaviour
{
	public Transform lineItemStartPosition;

	public GameObject figureLineItemPrefab;

	public tk2dUIItem titanTowerBtn;

	private List<GameObject> figureLineItems = new List<GameObject>();

	public void BuildReceiptWithFigures()
	{
		foreach (GameObject figureLineItem in figureLineItems)
		{
			Object.Destroy(figureLineItem);
		}
		figureLineItems.Clear();
		List<ShopToyBox> list = ShopController.instance.FiguresInCart();
		int count = list.Count;
		float num = 1f;
		float num2 = 100f;
		if (count > 3)
		{
			if (count == 4)
			{
				num2 = 75f;
				num = 0.8f;
			}
			if (count >= 5)
			{
				num2 = 58f;
				num = 0.65f;
			}
		}
		Vector3 localPosition = lineItemStartPosition.localPosition;
		for (int i = 0; i < count; i++)
		{
			ShopToyBox shopToyBox = list[i];
			Player.instance.AddFigureToPurchaseData(ShopController.inventory.name, shopToyBox.hangerRack + "-" + shopToyBox.hangerNumber + "-" + shopToyBox.hangerSpot);
			if (shopToyBox.toyType == ToyType.ModChip)
			{
				GameObject gameObject = Object.Instantiate(figureLineItemPrefab);
				ShopReceiptFigureLineItem component = gameObject.GetComponent<ShopReceiptFigureLineItem>();
				component.figureIcon.gameObject.SetActive(false);
				component.figureName.text = Language.Get(shopToyBox.modchipSelectPortrait.modChip.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS");
				component.figureLvl.text = string.Empty;
				component.modchipPortrait.gameObject.SetActive(true);
				component.modchipPortrait.gameObject.transform.localScale = new Vector3(num, num, num);
				component.modchipPortrait.modChip = shopToyBox.modchipSelectPortrait.modChip;
				gameObject.transform.parent = lineItemStartPosition.parent;
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.SetActive(true);
				figureLineItems.Add(gameObject);
				localPosition += new Vector3(0f, 0f - num2, 0f);
				Player.instance.AddItem(shopToyBox.modchipSelectPortrait.modChip);
			}
			else if (shopToyBox.toyType == ToyType.Accessory)
			{
				GameObject gameObject2 = Object.Instantiate(figureLineItemPrefab);
				ShopReceiptFigureLineItem component2 = gameObject2.GetComponent<ShopReceiptFigureLineItem>();
				component2.figureIcon.gameObject.SetActive(false);
				component2.figureName.text = Language.Get(shopToyBox.accessoryItemDefinition.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS");
				component2.figureLvl.text = string.Empty;
				component2.accessorySprite.gameObject.SetActive(true);
				component2.accessorySprite.SetSprite(shopToyBox.accessoryItemDefinition.iconSpriteId);
				gameObject2.transform.parent = lineItemStartPosition.parent;
				gameObject2.transform.localPosition = localPosition;
				gameObject2.transform.localEulerAngles = Vector3.zero;
				gameObject2.SetActive(true);
				figureLineItems.Add(gameObject2);
				localPosition += new Vector3(0f, 0f - num2, 0f);
				Player.instance.AddAccessory(shopToyBox.accessoryItemDefinition.uid);
				Player.instance.AddToEquippedAccessories(shopToyBox.accessoryItemDefinition.uid);
			}
			else
			{
				ShopFigure figureData = shopToyBox.figureData;
				GameObject gameObject3 = Object.Instantiate(figureLineItemPrefab);
				ShopReceiptFigureLineItem component3 = gameObject3.GetComponent<ShopReceiptFigureLineItem>();
				component3.figureIcon.SetSprite(shopToyBox.charDef.portraitSprite);
				component3.figureIcon.gameObject.transform.localScale = new Vector3(num, num, 1f);
				component3.figureName.text = ((!string.IsNullOrEmpty(figureData.characterAssetNameKey)) ? Language.Get(figureData.characterAssetNameKey, "NPCS,FIGS") : figureData.characterAssetName);
				component3.figureLvl.text = ". . . " + Language.Get("LVL", "MENUS,GUI") + " " + figureData.level;
				gameObject3.transform.parent = lineItemStartPosition.parent;
				gameObject3.transform.localPosition = localPosition;
				gameObject3.transform.localEulerAngles = Vector3.zero;
				gameObject3.SetActive(true);
				figureLineItems.Add(gameObject3);
				localPosition += new Vector3(0f, 0f - num2, 0f);
				Player.instance.AddCharacter(shopToyBox.rolledCharacter.GetCharacterData());
				Player.instance.AcquireCharacter(shopToyBox.charDef.uid);
				Player.instance.AddToParty(Player.instance.characters.GetLastItem());
				Player.instance.playerStats.numFiguresPurchased++;
			}
			Player.instance.Save();
		}
	}
}
