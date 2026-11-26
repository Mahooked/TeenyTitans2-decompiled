using System.Collections;
using UnityEngine;

public class Tutorial_Shop_SquadFull : TutorialBase
{
	public ShopController shopController;

	public ShopUI shopUI;

	protected override bool CanRunTutorial()
	{
		return true;
	}

	protected override IEnumerator RunTutorial()
	{
		if (shopController == null)
		{
			shopController = ShopController.instance;
		}
		if (shopUI == null)
		{
			shopUI = ShopUI.instance;
		}
		while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
		{
			yield return new WaitForEndOfFrame();
		}
		while (!shopUI.receiptGroup.activeSelf)
		{
			yield return new WaitForEndOfFrame();
		}
		CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_SHIPPEDTOWER", "TUTORIALS"), shopUI.receiptTowerButton.transform, TutorialPopup.TailPosition.Bottom, new Vector3(0f, 20f, 0f));
		yield return new WaitForTap();
		CoreGameTutorialManager.RemoveAllPopups();
	}
}
