using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_EnhanceFigureTower : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	public Vector3 mainMenuBubblePositionOffset;

	public Vector3 silkieBubblePositionOffset;

	private TutorialHands activeArrow;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Enhance Fig Tower")
	};

	public override TutorialStep[] tutorialSteps
	{
		get
		{
			return _tutorialSteps;
		}
		set
		{
			_tutorialSteps = value;
		}
	}

	public override bool isGameStateValid => ((CityController.instance != null && !TitleScreenController.instance.titleScreenActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !CityController.instance.communicator.MissionScrollerActive() && !CityController.instance.pauseScreen.gameObject.activeSelf && !GameUI.instance.showingCinemaBars && !CityReward.instance.showingRewards && CityController.instance.cityCharacter.AllowInput) || TitanTowerController.instance != null) && Player.instance.HasCharacterAtLevel(5);

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		bool continueSequence;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		PopUpBubble popUpBubble;
		if (CityController.instance != null && TitanTowerController.instance == null)
		{
			CityController cityController = CityController.instance;
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
			popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
			popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
			popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(CityController.instance.titanTowerBtnObj), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: CityController.instance.titanTowerBtnObj.transform.position + cityTowerBtnBubblePositionOffset), message: Language.Get("TUTORIAL_SHORTCUTTOWER", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
			tk2dUIItem towerBtnUIItem = CityController.instance.titanTowerBtnObj.GetComponent<tk2dUIItem>();
			towerBtnUIItem.enabled = false;
			yield return popUpBubble.WaitUntilIntroComplete();
			towerBtnUIItem.enabled = true;
			continueSequence = false;
			towerBtnUIItem.OnClick += continueSequenceAction;
			while (!continueSequence)
			{
				yield return null;
			}
			towerBtnUIItem.OnClick -= continueSequenceAction;
			popUpBubble.OutroBubble();
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
		}
		yield return new WaitUntil(() => TitanTowerController.instance != null);
		TitanTowerController towerController = TitanTowerController.instance;
		while (towerController.CurPage != "topMenu")
		{
			yield return null;
		}
		Color gray = Color.gray;
		towerController.exitButton.enabled = false;
		tk2dSprite exitSprite = towerController.exitButton.GetComponentInChildren<tk2dSprite>();
		exitSprite.color = gray;
		TowerMainMenuButton accessoryBtn = towerController.accessoriesButton.GetComponent<TowerMainMenuButton>();
		accessoryBtn.SetButtonEnabled(enabled: false);
		TowerMainMenuButton libraryBtn = towerController.libraryButton.GetComponent<TowerMainMenuButton>();
		libraryBtn.SetButtonEnabled(enabled: false);
		TowerMainMenuButton awardsBtn = towerController.awardsButton.GetComponent<TowerMainMenuButton>();
		awardsBtn.SetButtonEnabled(enabled: false);
		towerController.riddlerShopButton.gameObject.SetActive(value: false);
		TowerMainMenuButton toyBoxBtn = towerController.toyboxButton.GetComponent<TowerMainMenuButton>();
		Camera toyBoxBtnCamera = CameraUtility.GetCameraRenderingObject(toyBoxBtn.backingSprite.gameObject);
		popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI4"));
		Camera popupCamera = CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject);
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(toyBoxBtn.backingSprite.transform.position + mainMenuBubblePositionOffset, toyBoxBtnCamera, popupCamera), message: Language.Get("TUTORIAL_MANAGEFIGS", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown);
		while (towerController.topMenuAnimator.IsPlaying())
		{
			yield return null;
		}
		continueSequence = false;
		towerController.toyboxButton.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		towerController.toyboxButton.OnClick -= continueSequenceAction;
		Debug.Log("ToyBox Button Clicked");
		accessoryBtn.SetButtonEnabled(enabled: true);
		libraryBtn.SetButtonEnabled(enabled: true);
		awardsBtn.SetButtonEnabled(enabled: true);
		popUpBubble.OutroBubble();
		Character enhanceCharacter = Player.instance.characters.Find((Character character) => character.level >= 5);
		CharacterView characterView = towerController.characterView;
		characterView.Init(enhanceCharacter, Player.instance.characters, Player.instance);
		towerController.ShowPage("characterView");
		characterView.modChipButton.SetEnabled(enabled: false);
		characterView.customizeButton.SetEnabled(enabled: false);
		characterView.leftNavButton.enabled = false;
		tk2dSprite leftNavSprite = characterView.leftNavButton.GetComponentInChildren<tk2dSprite>();
		leftNavSprite.color = gray;
		characterView.rightNavButton.enabled = false;
		tk2dSprite rightNavSprite = characterView.rightNavButton.GetComponentInChildren<tk2dSprite>();
		rightNavSprite.color = gray;
		characterView.equipButton.enabled = false;
		characterView.equipArrowSprite.color = gray;
		characterView.equipText.color = gray;
		characterView.favoriteButton.toggleButton.uiItem.enabled = false;
		characterView.repaintButton.enabled = false;
		characterView.scrapButton.enabled = false;
		characterView.viewInLibraryButton.enabled = false;
		characterView.attackStat.uiItem.enabled = false;
		characterView.dodgeStat.uiItem.enabled = false;
		characterView.healthStat.uiItem.enabled = false;
		characterView.luckStat.uiItem.enabled = false;
		characterView.abilities.SetInteractive(interactive: false);
		characterView.modChipSlot.uiItem.enabled = false;
		characterView.classIconButton.enabled = false;
		towerController.mySquad.SetInteractive(interactive: false);
		yield return popUpBubble.WaitUntilOutroComplete();
		yield return null;
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(characterView.combineButton.gameObject), position: characterView.combineButton.transform.position, targetCamera: popupCamera), message: Language.Get("TUTORIAL_ENHANCE1", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		continueSequence = false;
		characterView.combineButton.uiItem.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		characterView.combineButton.uiItem.OnClick -= continueSequenceAction;
		popUpBubble.OutroBubble();
		yield return popUpBubble.WaitUntilOutroComplete();
		CombineScreen combineScreen = towerController.combineScreen;
		combineScreen.pageController.dragEnabled = false;
		combineScreen.pageController.dragNavigationEnabled = false;
		combineScreen.pageController.dotNavigationEnabled = false;
		combineScreen.pageController.portraitSelectionEnabled = false;
		combineScreen.silkieButton.enabled = false;
		Vector3 modelScrPos = towerController.modelViewerCamera.camera.WorldToScreenPoint(towerController.modelViewer.transform.position);
		Vector3 worldPos = GUICamera.instance.camera.ScreenToWorldPoint(modelScrPos);
		Vector3 popUpTargetPos = worldPos;
		popUpBubble.Init(Language.Get("TUTORIAL_ENHANCE2B", "TUTORIALS"), popUpTargetPos, GUICamera.instance.camera, PopUpBubble.BubbleType.UpsideDown, flipTailX: false, debug: true);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.Init(targetWorldPosition: combineScreen.silkieAnimator.transform.position + silkieBubblePositionOffset, message: Language.Get("TUTORIAL_ENHANCE2", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true, debug: true);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		Player.instance.AddCharacter(GameManager.instance.characterDatabase.GetCharacter(0), 1);
		Character testFigure = Player.instance.characters.GetLastItem();
		List<Character> characters = new List<Character>(combineScreen.pageController.filteredCharacters);
		characters.Remove(testFigure);
		characters.Insert(0, testFigure);
		combineScreen.pageController.SetList(characters, PageController.ViewMode.Single);
		combineScreen.pageController.portraitSelectionEnabled = true;
		Page page = combineScreen.pageController.GetPageWithCharacter(testFigure);
		int len = page.items.Count;
		for (int num = 0; num < len; num++)
		{
			if (page.items[num].character != testFigure)
			{
				page.items[num].uiItem.enabled = false;
			}
		}
		CharacterSelectPortrait portrait = page.items.Find((CharacterSelectPortrait x) => x.character == testFigure);
		popUpBubble.Init(targetWorldPosition: portrait.transform.position, message: Language.Get("TUTORIAL_ENHANCE3", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		while (combineScreen.childCombineCharacter == null)
		{
			yield return null;
		}
		popUpBubble.OutroBubble();
		ShowArrow(combineScreen.feedButton.transform, TutorialArrowDirection.Up, new Vector3(0f, -15f));
		continueSequence = false;
		combineScreen.feedButton.OnClick += continueSequenceAction;
		yield return new WaitUntil(() => continueSequence);
		combineScreen.feedButton.OnClick -= continueSequenceAction;
		RemoveArrow();
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
		yield return new WaitUntil(() => combineScreen.IsCurrentShininessAtTarget());
		popUpBubble.Init(targetWorldPosition: combineScreen.currentShininess.transform.position, message: Language.Get("TUTORIAL_ENHANCE4", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		combineScreen.silkieButton.enabled = true;
		popUpBubble.Init(targetWorldPosition: combineScreen.silkieAnimator.transform.position + silkieBubblePositionOffset, message: Language.Get("TUTORIAL_ENHANCE5", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true, debug: true);
		while (combineScreen.silkieCheatSheet == null)
		{
			yield return null;
		}
		yield return popUpBubble.WaitUntilIntroComplete();
		popUpBubble.OutroBubble();
		while (combineScreen.silkieCheatSheet != null)
		{
			yield return null;
		}
		combineScreen.silkieButton.enabled = false;
		popUpBubble.Init(targetWorldPosition: combineScreen.silkieAnimator.transform.position + silkieBubblePositionOffset, message: Language.Get("TUTORIAL_ENHANCE6B", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true, debug: true);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.Init(targetWorldPosition: combineScreen.silkieAnimator.transform.position + silkieBubblePositionOffset, message: Language.Get("TUTORIAL_ENHANCE8", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true, debug: true);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		for (int num2 = 0; num2 < len; num2++)
		{
			page.items[num2].uiItem.enabled = true;
		}
		combineScreen.silkieButton.enabled = true;
		combineScreen.showWarning = true;
		combineScreen.backButton.enabled = true;
		combineScreen.EnableSortButtons();
		combineScreen.slotCharButton.enabled = true;
		combineScreen.pageController.dragEnabled = true;
		combineScreen.pageController.dragNavigationEnabled = true;
		combineScreen.pageController.dotNavigationEnabled = true;
		combineScreen.pageController.portraitSelectionEnabled = true;
		Color white = Color.white;
		towerController.exitButton.enabled = true;
		exitSprite.color = white;
		characterView.combineButton.SetEnabled(enabled: true);
		characterView.customizeButton.SetEnabled(enabled: true);
		characterView.leftNavButton.enabled = true;
		leftNavSprite.color = white;
		characterView.rightNavButton.enabled = true;
		rightNavSprite.color = white;
		characterView.equipButton.enabled = true;
		characterView.equipArrowSprite.color = white;
		characterView.equipText.color = white;
		characterView.favoriteButton.toggleButton.uiItem.enabled = true;
		characterView.repaintButton.enabled = true;
		characterView.scrapButton.enabled = true;
		characterView.viewInLibraryButton.enabled = true;
		characterView.attackStat.uiItem.enabled = true;
		characterView.dodgeStat.uiItem.enabled = true;
		characterView.healthStat.uiItem.enabled = true;
		characterView.luckStat.uiItem.enabled = true;
		characterView.abilities.SetInteractive(interactive: true);
		characterView.modChipSlot.uiItem.enabled = true;
		characterView.classIconButton.enabled = true;
		towerController.mySquad.SetInteractive(interactive: true);
	}

	public override void CleanUp()
	{
	}

	private void ShowArrow(Transform target, TutorialArrowDirection direction, Vector3 offset)
	{
		Debug.Log("Show Arrow...");
		RemoveArrow();
		activeArrow = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		activeArrow.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI3"));
		tk2dCamera component = CameraUtility.GetCameraRenderingObject(target.gameObject).GetComponent<tk2dCamera>();
		tk2dCamera component2 = CameraUtility.GetCameraRenderingObject(activeArrow.gameObject).GetComponent<tk2dCamera>();
		Vector3 position = component.ScreenCamera.WorldToScreenPoint(target.position);
		Vector3 vector = component2.ScreenCamera.ScreenToWorldPoint(position);
		Vector3 position2 = vector + offset;
		activeArrow.ShowArrow(position2, direction);
	}

	private void RemoveArrow()
	{
		Debug.Log("Remove Arrow...");
		if (activeArrow != null)
		{
			ObjectPool.PoolObject(activeArrow.gameObject);
			activeArrow = null;
		}
	}
}
