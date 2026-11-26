using System.Collections;
using UnityEngine;

public class Tutorial_Shop_FirstModChip : TutorialBase
{
	public ShopController shopController;

	public ShopUI shopUI;

	protected override bool CanRunTutorial()
	{
		return Player.instance.HasItemOfType<ModChipDefinition>();
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
		shopUI.receiptContinueButton.GetComponent<tk2dUIItem>().enabled = false;
		shopUI.receiptExitButton.GetComponent<tk2dUIItem>().enabled = false;
		shopUI.receiptContinueButtonBehind.GetComponent<tk2dUIItem>().enabled = false;
		CoreGameTutorialManager.ShowBlackFade(0.3f);
		CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_MODCHIP1", "TUTORIALS"), 234f);
		yield return new WaitForTap();
		if (Player.instance.HasCharacterAtLevel(5))
		{
			CoreGameTutorialManager.RemoveBlackFade();
			CoreGameTutorialManager.RemoveHeader();
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_REMOTETOWER", "TUTORIALS"), shopUI.receiptTowerButton.transform, TutorialPopup.TailPosition.Bottom, new Vector3(0f, 25f, 0f));
			while (!LoadingScreenCover.isLoading)
			{
				yield return null;
			}
			CoreGameTutorialManager.RemoveAllPopups();
		}
		else
		{
			CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_MODCHIP2B", "TUTORIALS"), 234f);
			yield return new WaitForTap();
			CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_MODCHIP6", "TUTORIALS"), 234f);
			yield return new WaitForTap();
			CoreGameTutorialManager.RemoveBlackFade();
			CoreGameTutorialManager.RemoveHeader();
			shopUI.receiptContinueButton.GetComponent<tk2dUIItem>().enabled = true;
			shopUI.receiptContinueButtonBehind.GetComponent<tk2dUIItem>().enabled = true;
			shopUI.receiptExitButton.GetComponent<tk2dUIItem>().enabled = true;
		}
		Player.instance.SetTrigger("runTutorialFirstModChip");
	}
}
