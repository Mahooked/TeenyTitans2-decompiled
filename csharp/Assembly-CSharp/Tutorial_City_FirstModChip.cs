using System.Collections;
using UnityEngine;

public class Tutorial_City_FirstModChip : TutorialBase
{
	public CityController cityController;

	public TitleScreenController titleScreenController;

	private bool isTitleScreenClosed;

	protected override bool CanRunTutorial()
	{
		return Player.instance.HasItemOfType<ModChipDefinition>();
	}

	protected override IEnumerator RunTutorial()
	{
		if (cityController == null)
		{
			cityController = CityController.instance;
		}
		if (titleScreenController == null)
		{
			titleScreenController = TitleScreenController.instance;
		}
		if (titleScreenController.titleScreenActive)
		{
			titleScreenController.TitleScreenClosed += HandleTitleScreenClosed;
			while (!isTitleScreenClosed)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
		{
			yield return new WaitForEndOfFrame();
		}
		while (cityController.communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		while (cityController.pauseScreen.gameObject.activeSelf)
		{
			yield return new WaitForEndOfFrame();
		}
		while (GameUI.instance.showingCinemaBars)
		{
			yield return new WaitForEndOfFrame();
		}
		while (CityReward.instance.showingRewards)
		{
			yield return new WaitForEndOfFrame();
		}
		while (!cityController.cityCharacter.AllowInput)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
		cityController.cityCharacter.AllowInput = false;
		cityController.PreventManualWayPoint(prevent: true);
		cityController.communicator.pauseTime = true;
		cityController.cityCharacter.StopMove();
		cityController.pinchZoomer.allowPinchZoom = false;
		cityController.pinchZoomer.allowPan = false;
		cityController.pinchZoomer.allowDoubleClickZoomIn = false;
		cityController.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
		GameUI.instance.HideMySquad();
		GameUI.instance.HideAllLandmarksBut(null);
		GameUI.instance.communicator.SetActive(value: false);
		GameUI.instance.pauseButton.SetActive(value: false);
		PauseManager.instance.isEnabled = false;
		CoreGameTutorialManager.ShowBlackFade(0.3f);
		CoreGameTutorialManager.ShowHeader(Language.Get("TUTORIAL_MODCHIP1", "TUTORIALS"), 234f);
		yield return new WaitForTap();
		if (Player.instance.HasCharacterAtLevel(5))
		{
			CoreGameTutorialManager.RemoveBlackFade();
			CoreGameTutorialManager.RemoveHeader();
			CoreGameTutorialManager.ShowPopup(Language.Get("TUTORIAL_REMOTETOWER", "TUTORIALS"), cityController.titanTowerBtnObj.transform, TutorialPopup.TailPosition.Top, new Vector3(-10f, -25f, 0f));
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
		}
		PauseManager.instance.isEnabled = true;
		cityController.cityCharacter.AllowInput = true;
		cityController.PreventManualWayPoint(prevent: false);
		cityController.communicator.pauseTime = false;
		cityController.pinchZoomer.allowPinchZoom = true;
		cityController.pinchZoomer.allowPan = true;
		cityController.pinchZoomer.allowDoubleClickZoomIn = true;
		cityController.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		GameUI.instance.ShowMySquad();
		GameUI.instance.ShowAllLandmarks();
		GameUI.instance.communicator.SetActive(value: true);
		GameUI.instance.pauseButton.SetActive(value: true);
		Player.instance.SetTrigger("runTutorialFirstModChip");
	}

	private void HandleTitleScreenClosed()
	{
		titleScreenController.TitleScreenClosed -= HandleTitleScreenClosed;
		isTitleScreenClosed = true;
	}
}
