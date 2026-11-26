using System.Collections;
using UnityEngine;

public class Tutorial_TitanTower_ModChip : TutorialBase
{
	public TitanTowerController titanTowerController;

	private CharacterSelect characterSelect;

	private CharacterView characterView;

	private ModChipScreen modChipScreen;

	protected override bool CanRunTutorial()
	{
		return Player.instance.HasCharacterAtLevelAndModChipUnlocked(5);
	}

	protected override IEnumerator RunTutorial()
	{
		if (titanTowerController == null)
		{
			titanTowerController = TitanTowerController.instance;
		}
		characterSelect = titanTowerController.characterSelect;
		characterView = titanTowerController.characterView;
		modChipScreen = titanTowerController.modChipScreen;
		if (titanTowerController.topMenu.activeSelf)
		{
			titanTowerController.libraryButton.enabled = false;
			titanTowerController.awardsButton.enabled = false;
			titanTowerController.exitButton.enabled = false;
			while (titanTowerController.topMenuAnimator.IsPlaying())
			{
				yield return null;
			}
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_MANAGEFIGS", "TUTORIALS"), titanTowerController.toyboxButton.transform, TutorialPopup.TailPosition.Top, new Vector3(20f, -30f, 0f));
			while (!titanTowerController.characterSelect.gameObject.activeSelf)
			{
				yield return null;
			}
			CoreGameTutorialManager.RemoveAllPopups();
			titanTowerController.libraryButton.enabled = true;
			titanTowerController.awardsButton.enabled = true;
			characterSelect.EnableFilterButtons(enable: false);
			characterSelect.EnableSortButtons(enable: false);
			characterSelect.pageController.dragEnabled = false;
			characterSelect.pageController.dragNavigationEnabled = false;
			characterSelect.pageController.dotNavigationEnabled = false;
			characterSelect.pageController.portraitSelectionEnabled = false;
			titanTowerController.mySquad.EnableSlotButtons(enable: false);
			yield return new WaitForSeconds(0.3f);
			Character character = characterSelect.pageController.filteredCharacters.Find((Character obj) => obj.level >= 5);
			if (character != null)
			{
				characterSelect.pageController.GoToPageWithCharacter(character);
				yield return new WaitForSeconds(0.3f);
				characterSelect.pageController.portraitSelectionEnabled = true;
				Page page = characterSelect.pageController.GetPageWithCharacter(character);
				CharacterSelectPortrait portrait = null;
				TutorialPopup.TailPosition tailPosition = TutorialPopup.TailPosition.Bottom;
				Vector3 offset = Vector3.zero;
				int len = page.items.Count;
				for (int num = 0; num < len; num++)
				{
					if (page.items[num].character != character)
					{
						page.items[num].uiItem.enabled = false;
						continue;
					}
					portrait = page.items[num];
					tailPosition = ((num < 6) ? TutorialPopup.TailPosition.Top : TutorialPopup.TailPosition.Bottom);
					offset = ((num >= 6) ? new Vector3(0f, 25f, 0f) : new Vector3(0f, -25f, 0f));
				}
				CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_VIEWINFO", "TUTORIALS"), portrait.transform, tailPosition, offset);
				while (!characterView.gameObject.activeSelf)
				{
					yield return null;
				}
				CoreGameTutorialManager.RemoveAllPopups();
				for (int num2 = 0; num2 < len; num2++)
				{
					page.items[num2].uiItem.enabled = true;
				}
				characterSelect.EnableFilterButtons();
				characterSelect.EnableSortButtons();
				characterSelect.pageController.dragEnabled = true;
				characterSelect.pageController.dragNavigationEnabled = true;
				characterSelect.pageController.dotNavigationEnabled = true;
			}
		}
		else
		{
			while (!characterView.gameObject.activeSelf)
			{
				yield return null;
			}
			titanTowerController.exitButton.enabled = false;
		}
		characterView.EnableButtons(enable: false);
		characterView.modChipButton.uiItem.enabled = true;
		yield return new WaitForSeconds(0.3f);
		CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_MODCHIP3", "TUTORIALS"), characterView.modChipButton.transform, TutorialPopup.TailPosition.Top, new Vector3(0f, -25f, 0f));
		while (!modChipScreen.gameObject.activeSelf)
		{
			yield return null;
		}
		CoreGameTutorialManager.RemoveAllPopups();
		characterView.EnableButtons();
		modChipScreen.modChipSelector.uiItem.enabled = false;
		yield return new WaitForSeconds(0.3f);
		CoreGameTutorialManager.ShowBlackFade(0.3f);
		CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_MODCHIP4B", "TUTORIALS"));
		do
		{
			yield return null;
		}
		while (!Input.GetMouseButtonDown(0));
		CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_MODCHIP5", "TUTORIALS"));
		do
		{
			yield return null;
		}
		while (!Input.GetMouseButtonDown(0));
		CoreGameTutorialManager.RemoveBlackFade();
		CoreGameTutorialManager.RemoveHeader();
		titanTowerController.exitButton.enabled = true;
		modChipScreen.modChipSelector.uiItem.enabled = true;
		titanTowerController.mySquad.EnableSlotButtons();
	}
}
