using System.Collections;
using UnityEngine;

public class Tutorial_TitanTower_Enhancement : TutorialBase
{
	public TitanTowerController titanTowerController;

	private CharacterSelect characterSelect;

	private CharacterView characterView;

	private CombineScreen combineScreen;

	protected override bool CanRunTutorial()
	{
		return Player.instance.HasCharacterAtLevelAndNotShinyRank(5, 3);
	}

	protected override IEnumerator RunTutorial()
	{
		if (titanTowerController == null)
		{
			titanTowerController = TitanTowerController.instance;
		}
		characterSelect = titanTowerController.characterSelect;
		characterView = titanTowerController.characterView;
		combineScreen = titanTowerController.combineScreen;
		combineScreen.showWarning = false;
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
		Character character = characterSelect.pageController.filteredCharacters.Find((Character obj) => obj.level >= 5 && obj.shinyRank != 3);
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
			characterView.EnableButtons(enable: false);
			characterView.combineButton.uiItem.enabled = true;
			yield return new WaitForSeconds(0.3f);
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_ENHANCE1", "TUTORIALS"), characterView.combineButton.transform, TutorialPopup.TailPosition.Top, new Vector3(0f, -25f, 0f));
			while (!combineScreen.gameObject.activeSelf)
			{
				yield return null;
			}
			CoreGameTutorialManager.RemoveAllPopups();
			characterView.EnableButtons();
			combineScreen.pageController.dotNavigationEnabled = false;
			combineScreen.pageController.dragEnabled = false;
			combineScreen.pageController.dragNavigationEnabled = false;
			combineScreen.pageController.portraitSelectionEnabled = false;
			combineScreen.EnableSortButtons(enable: false);
			combineScreen.slotCharButton.enabled = false;
			combineScreen.silkieButton.enabled = false;
			yield return new WaitForSeconds(0.3f);
			CoreGameTutorialManager.ShowBlackFade(0.3f);
			CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_ENHANCE2B", "TUTORIALS"), 234f);
			do
			{
				yield return null;
			}
			while (!Input.GetMouseButtonDown(0));
			CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_ENHANCE2", "TUTORIALS"), 234f);
			do
			{
				yield return null;
			}
			while (!Input.GetMouseButtonDown(0));
			CoreGameTutorialManager.RemoveBlackFade();
			CoreGameTutorialManager.RemoveHeader();
			Player.instance.AddCharacter(GameManager.instance.characterDatabase.GetCharacter(0), 1);
			Character testFigure = Player.instance.characters.GetLastItem();
			combineScreen.pageController.GoToPageWithCharacter(testFigure);
			yield return new WaitForSeconds(0.3f);
			combineScreen.pageController.portraitSelectionEnabled = true;
			page = combineScreen.pageController.GetPageWithCharacter(testFigure);
			len = page.items.Count;
			for (int num3 = 0; num3 < len; num3++)
			{
				if (page.items[num3].character != testFigure)
				{
					page.items[num3].uiItem.enabled = false;
				}
				else
				{
					portrait = page.items[num3];
				}
			}
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_ENHANCE3", "TUTORIALS"), portrait.transform, TutorialPopup.TailPosition.Bottom, new Vector3(0f, 25f, 0f));
			while (combineScreen.childCombineCharacter == null)
			{
				yield return null;
			}
			CoreGameTutorialManager.RemoveAllPopups();
			CoreGameTutorialManager.ShowArrow(combineScreen.feedButton.transform, TutorialArrowDirection.Up, new Vector3(0f, -15f, 0f));
			while (combineScreen.feedButton.gameObject.activeSelf)
			{
				yield return null;
			}
			CoreGameTutorialManager.RemoveArrow();
			while (!combineScreen.backButton.enabled)
			{
				yield return null;
			}
			combineScreen.backButton.enabled = false;
			combineScreen.pageController.dotNavigationEnabled = false;
			combineScreen.pageController.dragEnabled = false;
			combineScreen.pageController.dragNavigationEnabled = false;
			combineScreen.pageController.portraitSelectionEnabled = false;
			combineScreen.EnableSortButtons(enable: false);
			combineScreen.slotCharButton.enabled = false;
			yield return new WaitForSeconds(1f);
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_ENHANCE4", "TUTORIALS"), combineScreen.currentShininess.transform, TutorialPopup.TailPosition.Top, new Vector3(combineScreen.currentShininess.dimensions.x, -8f, 0f));
			do
			{
				yield return null;
			}
			while (!Input.GetMouseButtonDown(0));
			CoreGameTutorialManager.RemoveAllPopups();
			combineScreen.silkieButton.enabled = true;
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_ENHANCE5", "TUTORIALS"), combineScreen.silkieAnimator.transform, TutorialPopup.TailPosition.Top, new Vector3(0f, -50f, 0f));
			CoreGameTutorialManager.ShowTap(combineScreen.silkieButton.transform, new Vector3(0f, -20f, 0f));
			while (combineScreen.silkieCheatSheet == null)
			{
				yield return null;
			}
			CoreGameTutorialManager.RemoveAllPopups();
			CoreGameTutorialManager.RemoveTutorialHands();
			while (combineScreen.silkieCheatSheet != null)
			{
				yield return null;
			}
			CoreGameTutorialManager.ShowBlackFade(0.3f);
			CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_ENHANCE6B", "TUTORIALS"), 234f);
			do
			{
				yield return null;
			}
			while (!Input.GetMouseButtonDown(0));
			CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_ENHANCE8", "TUTORIALS"), 234f);
			do
			{
				yield return null;
			}
			while (!Input.GetMouseButtonDown(0));
			CoreGameTutorialManager.RemoveBlackFade();
			CoreGameTutorialManager.RemoveHeader();
			for (int num4 = 0; num4 < len; num4++)
			{
				page.items[num4].uiItem.enabled = true;
			}
		}
		combineScreen.showWarning = true;
		combineScreen.backButton.enabled = true;
		combineScreen.pageController.dotNavigationEnabled = true;
		combineScreen.pageController.dragEnabled = true;
		combineScreen.pageController.dragNavigationEnabled = true;
		combineScreen.pageController.portraitSelectionEnabled = true;
		combineScreen.EnableSortButtons();
		combineScreen.slotCharButton.enabled = true;
		titanTowerController.mySquad.EnableSlotButtons();
	}
}
