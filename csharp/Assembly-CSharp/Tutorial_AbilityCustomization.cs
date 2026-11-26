using System;
using System.Collections;
using UnityEngine;

public class Tutorial_AbilityCustomization : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	public Vector3 mainMenuBubblePositionOffset;

	[NonSerialized]
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Customize Abilities")
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

	public override bool isGameStateValid => ((CityController.instance != null && !TitleScreenController.instance.titleScreenActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !CityController.instance.communicator.MissionScrollerActive() && !CityController.instance.pauseScreen.gameObject.activeSelf && !GameUI.instance.showingCinemaBars && !CityReward.instance.showingRewards && CityController.instance.cityCharacter.AllowInput) || TitanTowerController.instance != null) && Player.instance.HasCharacterAtLevel(10);

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
			CityController.instance.cityCharacter.AllowInput = false;
			CityController.instance.PreventManualWayPoint(prevent: true);
			CityController.instance.communicator.pauseTime = true;
			CityController.instance.cityCharacter.StopMove();
			CityController.instance.pinchZoomer.allowPinchZoom = false;
			CityController.instance.pinchZoomer.allowPan = false;
			CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
			CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
			GameUI.instance.HideMySquad();
			GameUI.instance.HideAllLandmarksBut(null);
			GameUI.instance.communicator.SetActive(value: false);
			GameUI.instance.pauseButton.SetActive(value: false);
			PauseManager.instance.isEnabled = false;
			popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
			popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
			popUpBubble.Init(targetWorldPosition: CityController.instance.titanTowerBtnObj.transform.position + cityTowerBtnBubblePositionOffset, message: Language.Get("TUTORIAL_CUSTOMIZE1", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
			tk2dUIItem towerBtnUIItem = CityController.instance.titanTowerBtnObj.GetComponent<tk2dUIItem>();
			continueSequence = false;
			towerBtnUIItem.OnClick += continueSequenceAction;
			while (!continueSequence)
			{
				yield return null;
			}
			towerBtnUIItem.OnClick -= continueSequenceAction;
			CityController.instance.cityCharacter.AllowInput = true;
			CityController.instance.PreventManualWayPoint(prevent: false);
			CityController.instance.communicator.pauseTime = false;
			CityController.instance.pinchZoomer.allowPinchZoom = true;
			CityController.instance.pinchZoomer.allowPan = false;
			CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
			CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
			GameUI.instance.ShowMySquad();
			GameUI.instance.ShowAllLandmarks();
			GameUI.instance.communicator.SetActive(value: true);
			GameUI.instance.pauseButton.SetActive(value: true);
			PauseManager.instance.isEnabled = true;
			while (TitanTowerController.instance == null || !LoadingScreenCover.allowInit)
			{
				yield return null;
			}
		}
		TitanTowerController towerController = TitanTowerController.instance;
		Color gray = Color.gray;
		Color white = Color.white;
		towerController.exitButton.enabled = false;
		tk2dSprite exitSprite = towerController.exitButton.GetComponentInChildren<tk2dSprite>();
		exitSprite.color = gray;
		TowerMainMenuButton accessoriesBtn = towerController.accessoriesButton.GetComponent<TowerMainMenuButton>();
		accessoriesBtn.SetButtonEnabled(enabled: false);
		TowerMainMenuButton libraryBtn = towerController.libraryButton.GetComponent<TowerMainMenuButton>();
		libraryBtn.SetButtonEnabled(enabled: false);
		TowerMainMenuButton awardsBtn = towerController.awardsButton.GetComponent<TowerMainMenuButton>();
		awardsBtn.SetButtonEnabled(enabled: false);
		towerController.riddlerShopButton.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.5f);
		TowerMainMenuButton toyboxBtn = towerController.toyboxButton.GetComponent<TowerMainMenuButton>();
		Camera toyBoxBtnCamera = CameraUtility.GetCameraRenderingObject(toyboxBtn.backingSprite.gameObject);
		popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI4"));
		Camera popupCamera = CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject);
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(toyboxBtn.backingSprite.transform.position + mainMenuBubblePositionOffset, toyBoxBtnCamera, popupCamera), message: Language.Get("TUTORIAL_CUSTOMIZE2", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown);
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
		accessoriesBtn.SetButtonEnabled(enabled: true);
		libraryBtn.SetButtonEnabled(enabled: true);
		awardsBtn.SetButtonEnabled(enabled: true);
		CharacterView characterView = towerController.characterView;
		Character level10Character = Player.instance.characters.Find((Character character) => character.level >= 10);
		Debug.Log("Level 10 Character Found: " + level10Character.characterDefinition.characterName);
		characterView.Init(level10Character, Player.instance.characters, Player.instance);
		towerController.ShowPage("characterView");
		characterView.combineButton.SetEnabled(enabled: false);
		characterView.modChipButton.SetEnabled(enabled: false);
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
		yield return null;
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(characterView.customizeButton.gameObject), position: characterView.customizeButton.transform.position, targetCamera: popupCamera), message: Language.Get("TUTORIAL_CUSTOMIZE3", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.None, flipTailX: false, debug: true);
		continueSequence = false;
		characterView.customizeButton.uiItem.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		characterView.customizeButton.uiItem.OnClick -= continueSequenceAction;
		characterView.abilityCustomization.SetCloseButtonInteractive(interactive: false);
		characterView.abilityCustomization.SetFactoryResetInteractive(interactive: false);
		Vector3 popUpTargetPos = CameraUtility.GetWorldPositionInCamera(characterView.abilityCustomization.dropSlots[2].transform.position, towerController.guiCamera4b.ScreenCamera, towerController.guiCamera4b.ScreenCamera);
		popUpBubble.transform.localScale = Vector3.one * 0.75f;
		popUpBubble.Init(Language.Get("TUTORIAL_CUSTOMIZE4", "TUTORIALS"), popUpTargetPos, GUICamera.instance.camera, PopUpBubble.BubbleType.UpsideDown);
		continueSequence = false;
		Action<AbilityCustomizationAbilityButton> customizeContinueSequence = (AbilityCustomizationAbilityButton button) =>
		{
			continueSequence = true;
		};
		characterView.abilityCustomization.DragStarted += customizeContinueSequence;
		while (!continueSequence)
		{
			yield return null;
		}
		characterView.abilityCustomization.DragStarted -= customizeContinueSequence;
		characterView.abilityCustomization.SetCloseButtonInteractive(interactive: true);
		characterView.abilityCustomization.SetFactoryResetInteractive(interactive: true);
		popUpBubble.OutroBubble(0f, poolWhenFinished: true);
		towerController.exitButton.enabled = true;
		exitSprite.color = white;
		characterView.combineButton.SetEnabled(enabled: true);
		characterView.modChipButton.SetEnabled(enabled: true);
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
