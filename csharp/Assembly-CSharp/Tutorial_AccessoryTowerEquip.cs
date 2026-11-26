using System;
using System.Collections;
using UnityEngine;

public class Tutorial_AccessoryTowerEquip : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	public Vector3 mainMenuBubblePositionOffset;

	[NonSerialized]
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Accessory Tower Equip")
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

	public override bool isGameStateValid => ((CityController.instance != null && !TitleScreenController.instance.titleScreenActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !CityController.instance.communicator.MissionScrollerActive() && !CityController.instance.pauseScreen.gameObject.activeSelf && !GameUI.instance.showingCinemaBars && !CityReward.instance.showingRewards && CityController.instance.cityCharacter.AllowInput) || TitanTowerController.instance != null) && Player.instance.accessories.Count > 0;

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		Debug.Log("Tower Equip Sequence");
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
			popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(CityController.instance.titanTowerBtnObj), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: CityController.instance.titanTowerBtnObj.transform.position + cityTowerBtnBubblePositionOffset), message: Language.Get("TUTORIAL_ACCESSORYEQUIP1", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
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
		while (towerController.CurPage != "topMenu")
		{
			yield return null;
		}
		towerController.exitButton.enabled = false;
		tk2dSprite exitSprite = towerController.exitButton.GetComponentInChildren<tk2dSprite>();
		exitSprite.color = gray;
		TowerMainMenuButton toyboxBtn = towerController.toyboxButton.GetComponent<TowerMainMenuButton>();
		toyboxBtn.SetButtonEnabled(enabled: false);
		TowerMainMenuButton libraryBtn = towerController.libraryButton.GetComponent<TowerMainMenuButton>();
		libraryBtn.SetButtonEnabled(enabled: false);
		TowerMainMenuButton awardsBtn = towerController.awardsButton.GetComponent<TowerMainMenuButton>();
		awardsBtn.SetButtonEnabled(enabled: false);
		towerController.riddlerShopButton.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.5f);
		Player.instance.UnequipAllAccessories();
		TowerMainMenuButton accessoriesBtn = towerController.accessoriesButton.GetComponent<TowerMainMenuButton>();
		Camera accessoriesBtnCamera = CameraUtility.GetCameraRenderingObject(accessoriesBtn.backingSprite.gameObject);
		popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI4"));
		Camera popupCamera = CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject);
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(accessoriesBtn.backingSprite.transform.position + mainMenuBubblePositionOffset, accessoriesBtnCamera, popupCamera), message: Language.Get("TUTORIAL_ACCESSORYEQUIP2", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown);
		while (towerController.topMenuAnimator.IsPlaying())
		{
			yield return null;
		}
		continueSequence = false;
		towerController.accessoriesButton.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		towerController.accessoriesButton.OnClick -= continueSequenceAction;
		Debug.Log("Accessory Button Clicked");
		toyboxBtn.SetButtonEnabled(enabled: true);
		libraryBtn.SetButtonEnabled(enabled: true);
		awardsBtn.SetButtonEnabled(enabled: true);
		towerController.accessoryEquipBar.SetGrayedOut(grayedOut: true);
		towerController.accessorySelect.accessoryPageController.dragNavigationEnabled = false;
		AccessorySelectPortrait accessorySelectPortrait = towerController.accessorySelect.accessoryPageController.pages[0].items[0];
		Camera accessorySelectPortraitCamera = CameraUtility.GetCameraRenderingObject(accessorySelectPortrait.gameObject);
		Vector3 portraitBtnOffset = accessorySelectPortrait.portraitSprite.GetBounds().min + new Vector3(accessorySelectPortrait.portraitSprite.GetBounds().extents.x, 0f);
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(accessorySelectPortrait.transform.position + portraitBtnOffset, accessorySelectPortraitCamera, popupCamera), message: Language.Get("TUTORIAL_ACCESSORYEQUIP3", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown);
		continueSequence = false;
		Action<AccessoryItemDefinition> accessorySelected = (AccessoryItemDefinition x) =>
		{
			continueSequence = true;
		};
		towerController.accessorySelect.accessoryPageController.AccessorySelected += accessorySelected;
		while (!continueSequence)
		{
			yield return null;
		}
		towerController.accessorySelect.accessoryPageController.AccessorySelected -= accessorySelected;
		towerController.accessoryEquipBar.SetGrayedOut(grayedOut: false);
		Camera accessoryViewArrowBtnCamera = CameraUtility.GetCameraRenderingObject(towerController.accessoriesView.equipButton.gameObject);
		Vector3 equipBtnOffset = new Vector3(0f, towerController.accessoriesView.equipArrowSprite.GetBounds().extents.y * 0.5f);
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(towerController.accessoriesView.equipButton.transform.position + equipBtnOffset, accessoryViewArrowBtnCamera, popupCamera), message: Language.Get("TUTORIAL_ACCESSORYEQUIP4", "TUTORIALS"), targetCamera: GUICamera.instance.camera);
		continueSequence = false;
		towerController.accessoriesView.equipButton.OnClick += continueSequenceAction;
		float timer = 1.5f;
		bool outroStarted = false;
		while (!continueSequence)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f && !outroStarted)
			{
				outroStarted = true;
				popUpBubble.OutroBubble();
			}
			yield return null;
		}
		towerController.accessoriesView.equipButton.OnClick -= continueSequenceAction;
		if (popUpBubble.IsShowing)
		{
			popUpBubble.OutroBubble();
		}
		towerController.exitButton.enabled = true;
		exitSprite.color = white;
		while (!towerController.accessorySelect.gameObject.activeInHierarchy)
		{
			yield return null;
		}
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(towerController.accessoryEquipBar.backgroundSprite.gameObject), position: accessorySelectPortrait.transform.position + portraitBtnOffset, targetCamera: popupCamera), message: Language.Get("TUTORIAL_ACCESSORYEQUIP5", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown);
		while (popUpBubble.IsOutroing && !towerController.topMenu.activeInHierarchy)
		{
			yield return null;
		}
		timer = 2f;
		while (!towerController.topMenu.activeInHierarchy)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				break;
			}
			yield return null;
		}
		popUpBubble.OutroBubble();
	}

	public override void CleanUp()
	{
	}
}
