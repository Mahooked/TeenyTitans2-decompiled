using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ModChipTowerEquip : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	public Vector3 mainMenuBubblePositionOffset;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Mod Chip Tower Equip")
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

	public override bool isGameStateValid => TitanTowerController.instance != null && Player.instance.HasCharacterAtLevelAndModChipUnlocked(5) && (TitanTowerController.instance.CurPage == "topMenu" || TitanTowerController.instance.CurPage == "characterView");

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		PopUpBubble popUpBubble = null;
		Camera popupCamera = null;
		bool continueSequence;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		TitanTowerController towerController = TitanTowerController.instance;
		Color gray = Color.gray;
		CharacterView characterView = towerController.characterView;
		tk2dSprite exitSprite = towerController.exitButton.GetComponentInChildren<tk2dSprite>();
		exitSprite.color = gray;
		towerController.exitButton.enabled = false;
		if (towerController.CurPage == "topMenu")
		{
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
			popupCamera = CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject);
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
			accessoryBtn.SetButtonEnabled(enabled: true);
			libraryBtn.SetButtonEnabled(enabled: true);
			awardsBtn.SetButtonEnabled(enabled: true);
			popUpBubble.OutroBubble();
		}
		Character modChipCharacter = Player.instance.characters.Find((Character character) => character.modChipUnlocked);
		characterView.Init(modChipCharacter, Player.instance.characters, Player.instance);
		towerController.ShowPage("characterView");
		characterView.combineButton.SetEnabled(enabled: false);
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
		if (popUpBubble == null)
		{
			popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
			popUpBubble.SetLayer(LayerMask.NameToLayer("UI4"));
		}
		yield return popUpBubble.WaitUntilOutroComplete();
		yield return null;
		if (popupCamera == null)
		{
			popupCamera = CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject);
		}
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(characterView.modChipButton.gameObject), position: characterView.modChipButton.transform.position, targetCamera: popupCamera), message: Language.Get("TUTORIAL_MODCHIP3", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		continueSequence = false;
		characterView.modChipButton.uiItem.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		characterView.modChipButton.uiItem.OnClick -= continueSequenceAction;
		popUpBubble.OutroBubble();
		yield return popUpBubble.WaitUntilOutroComplete();
		ModChipScreen modChipScreen = towerController.modChipScreen;
		modChipScreen.modChipSelector.ignorePageDragging = true;
		modChipScreen.modChipSelector.ignorePortraitDragging = true;
		modChipScreen.dropTarget.enabled = false;
		List<PageModChips> modChipPages = modChipScreen.modChipSelector.pages;
		ModChipSelectPortrait modChipPortrait = modChipPages[modChipScreen.modChipSelector.pageIndex].items[0];
		popUpBubble.Init(targetWorldPosition: modChipPortrait.transform.position, message: Language.Get("TUTORIAL_MODCHIP4B", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		continueSequence = false;
		Action<ModChipSelectPortrait> continueSequenceDrag = (ModChipSelectPortrait x) =>
		{
			continueSequence = true;
		};
		modChipScreen.modChipSelector.DragStarted += continueSequenceDrag;
		yield return new WaitUntil(() => continueSequence || Input.GetMouseButtonDown(0));
		modChipScreen.modChipSelector.DragStarted -= continueSequenceDrag;
		yield return popUpBubble.WaitUntilIntroComplete();
		popUpBubble.OutroBubble();
		yield return popUpBubble.WaitUntilOutroComplete();
		Vector3 scrPos = modChipScreen.modelViewerCamera.camera.WorldToScreenPoint(modChipScreen.dropTarget.transform.position);
		popUpBubble.Init(targetWorldPosition: GUICamera.instance.camera.ScreenToWorldPoint(scrPos), message: Language.Get("TUTORIAL_MODCHIP5", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		modChipScreen.modChipSelector.ignorePageDragging = false;
		modChipScreen.modChipSelector.ignorePortraitDragging = false;
		modChipScreen.dropTarget.enabled = true;
		Color white = Color.white;
		towerController.exitButton.enabled = true;
		exitSprite.color = white;
		modChipScreen.modChipSelector.uiItem.enabled = true;
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
}
