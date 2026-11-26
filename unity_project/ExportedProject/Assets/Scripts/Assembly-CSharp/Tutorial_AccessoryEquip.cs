using System;
using System.Collections;
using UnityEngine;

public class Tutorial_AccessoryEquip : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	public Vector3 mainMenuBubblePositionOffset;

	public Vector3 accessoryViewBubblePositionOffset;

	public Vector3 accessorySelectDragBubblePositionOffset;

	[NonSerialized]
	private TutorialStep[] _tutorialSteps = new TutorialStep[4]
	{
		new TutorialStep("Accessory Tower Equip"),
		new TutorialStep("Accessory PreBattle Equip and Usage"),
		new TutorialStep("Accessory Battle How to Charge"),
		new TutorialStep("Accessory Battle Use At 100%")
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

	public override bool isGameStateValid
	{
		get
		{
			return true;
		}
	}

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		Debug.LogFormat("PLAY TUTORIAL, Tutorial Accessory Equip, step: {0}", stepIndex);
		switch (stepIndex)
		{
		case 0:
			yield return TutorialManager.instance.StartCoroutine(TowerEquipSequence());
			yield break;
		case 1:
			yield return TutorialManager.instance.StartCoroutine(PrebattleEquipSequence());
			yield break;
		case 2:
			yield return TutorialManager.instance.StartCoroutine(BattleHowtoUseSequence());
			yield break;
		case 3:
			yield return TutorialManager.instance.StartCoroutine(Battle100PercentUseSequence());
			yield break;
		}
		Debug.LogErrorFormat("Attempting to play tutorial, {0}, at step index {1}. Step Index out of range...", GetType(), stepIndex);
	}

	private IEnumerator TowerEquipSequence()
	{
		Debug.Log("Tower Equip Sequence");
		while (CityController.instance == null && TitanTowerController.instance == null)
		{
			yield return null;
		}
		bool continueSequence;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		PopUpBubble popUpBubble;
		if (TitanTowerController.instance == null)
		{
			if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
			{
				continueSequence = false;
				TitleScreenController.instance.TitleScreenClosed += continueSequenceAction;
				while (!continueSequence)
				{
					yield return null;
				}
				TitleScreenController.instance.TitleScreenClosed -= continueSequenceAction;
			}
			yield return null;
			while ((DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy) || CityController.instance.communicator.MissionScrollerActive() || CityController.instance.pauseScreen.gameObject.activeSelf || GameUI.instance.showingCinemaBars || CityReward.instance.showingRewards || !CityController.instance.cityCharacter.AllowInput)
			{
				yield return null;
			}
			CityController.instance.cityCharacter.AllowInput = false;
			CityController.instance.PreventManualWayPoint(true);
			CityController.instance.communicator.pauseTime = true;
			CityController.instance.cityCharacter.StopMove();
			CityController.instance.pinchZoomer.allowPinchZoom = false;
			CityController.instance.pinchZoomer.allowPan = false;
			CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
			CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
			GameUI.instance.HideMySquad();
			GameUI.instance.HideAllLandmarksBut(null);
			GameUI.instance.communicator.SetActive(false);
			GameUI.instance.pauseButton.SetActive(false);
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
			CityController.instance.PreventManualWayPoint(false);
			CityController.instance.communicator.pauseTime = false;
			CityController.instance.pinchZoomer.allowPinchZoom = true;
			CityController.instance.pinchZoomer.allowPan = false;
			CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
			CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
			GameUI.instance.ShowMySquad();
			GameUI.instance.ShowAllLandmarks();
			GameUI.instance.communicator.SetActive(true);
			GameUI.instance.pauseButton.SetActive(true);
			PauseManager.instance.isEnabled = true;
			while (TitanTowerController.instance == null)
			{
				yield return null;
			}
		}
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
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
		toyboxBtn.SetButtonEnabled(false);
		TowerMainMenuButton libraryBtn = towerController.libraryButton.GetComponent<TowerMainMenuButton>();
		libraryBtn.SetButtonEnabled(false);
		TowerMainMenuButton awardsBtn = towerController.awardsButton.GetComponent<TowerMainMenuButton>();
		awardsBtn.SetButtonEnabled(false);
		towerController.riddlerShopButton.gameObject.SetActive(false);
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
		toyboxBtn.SetButtonEnabled(true);
		libraryBtn.SetButtonEnabled(true);
		awardsBtn.SetButtonEnabled(true);
		towerController.accessoryEquipBar.SetGrayedOut(true);
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
		towerController.accessoryEquipBar.SetGrayedOut(false);
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

	private IEnumerator PrebattleEquipSequence()
	{
		Debug.Log("Prebattle Equip Sequence");
		while (PreBattleController.Instance == null || !LoadingScreenCover.allowInit || Player.instance.activeAccessories.Count <= 0)
		{
			yield return null;
		}
		yield return null;
		PreBattleController.Instance.preBattleParty.SetInteractive(false);
		PreBattleController.Instance.exitButton.enabled = false;
		PreBattleController.Instance.exitButtonSprite.color = Color.gray;
		bool continueSequence = false;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		PreBattleController.Instance.FinishedIntro += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		PreBattleController.Instance.FinishedIntro -= continueSequenceAction;
		PreBattleController.Instance.preBattleParty.EnableButtons(false);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI2"));
		AccessorySelectorBar accessorySelectorBar = PreBattleController.Instance.accessorySelectorBar;
		tk2dSprite accessorySelectedSprite = accessorySelectorBar.accessorySelectedSprite;
		Camera accessorySelectCamera = CameraUtility.GetCameraRenderingObject(accessorySelectedSprite.gameObject);
		Camera popupCamera = CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject);
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(accessorySelectedSprite.transform.position + accessorySelectedSprite.GetBounds().max, accessorySelectCamera, popupCamera), message: Language.Get("TUTORIAL_ACCESSORYBATTLE1", "TUTORIALS"), targetCamera: GUICamera.instance.camera);
		continueSequence = false;
		accessorySelectorBar.barOpenBtn.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		accessorySelectorBar.barOpenBtn.OnClick -= continueSequenceAction;
		popUpBubble.OutroBubble();
		continueSequence = false;
		accessorySelectorBar.BarOpened += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		accessorySelectorBar.BarOpened -= continueSequenceAction;
		AccessoryButton firstAccessoryBtn = accessorySelectorBar.accessoryButtons[0];
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(firstAccessoryBtn.transform.position + firstAccessoryBtn.accessorySprite.GetBounds().max, accessorySelectCamera, popupCamera), message: Language.Get("TUTORIAL_ACCESSORYBATTLE2", "TUTORIALS"), targetCamera: GUICamera.instance.camera);
		accessorySelectorBar.allowNonEquipExit = false;
		continueSequence = false;
		firstAccessoryBtn.uiItem.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		firstAccessoryBtn.uiItem.OnClick -= continueSequenceAction;
		popUpBubble.OutroBubble();
		PreBattleController.Instance.preBattleParty.SetInteractive(true);
		PreBattleController.Instance.exitButton.enabled = true;
		PreBattleController.Instance.exitButtonSprite.color = Color.white;
		continueSequence = false;
		PreBattleController.Instance.teamSelectScreen.goButton.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		PreBattleController.Instance.teamSelectScreen.goButton.OnClick -= continueSequenceAction;
		accessorySelectorBar.allowNonEquipExit = true;
	}

	private IEnumerator BattleHowtoUseSequence()
	{
		Debug.Log("Battle How to Use Sequence");
		while (BattleController.instance == null || !BattleController.instance.battleStarted || BattleController.instance.playerAccessoryButton.accessoryUid < 0)
		{
			yield return null;
		}
		BattleController.instance.ForceStartWaiting();
		yield return new WaitUntil(() => BattleController.instance.playerAccessoryButton.entered);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		tk2dBaseSprite accessoryBtnSprite = BattleController.instance.playerAccessoryButton.backingFillSprite;
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(accessoryBtnSprite.gameObject), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: accessoryBtnSprite.transform.position + accessoryBtnSprite.GetBounds().max), message: Language.Get("TUTORIAL_ACCESSORYBATTLE3", "TUTORIALS"), targetCamera: GUICamera.instance.camera);
		yield return new WaitForSeconds(0.5f);
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		BattleController.instance.ForceEndWaiting();
	}

	private IEnumerator Battle100PercentUseSequence()
	{
		Debug.Log("Battle 100% Use Sequence");
		while (BattleController.instance == null || !BattleController.instance.battleStarted || BattleController.instance.playerAccessoryButton.accessoryUid < 0 || BattleController.instance.playerAccessoryButton.currentFillAmount < 1f || BattleController.instance.waiting)
		{
			yield return null;
		}
		Debug.Log("Tutorial, Force Start Waiting");
		BattleController.instance.ForceStartWaiting();
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		tk2dBaseSprite accessoryBtnSprite = BattleController.instance.playerAccessoryButton.backingFillSprite;
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(accessoryBtnSprite.gameObject), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: accessoryBtnSprite.transform.position + accessoryBtnSprite.GetBounds().max), message: Language.Get("TUTORIAL_ACCESSORYBATTLE4", "TUTORIALS"), targetCamera: GUICamera.instance.camera);
		bool continueSequence = false;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
		BattleController.instance.playerAccessoryButton.uiItem.OnClick += continueSequenceAction;
		while (!continueSequence)
		{
			yield return null;
		}
		BattleController.instance.playerAccessoryButton.uiItem.OnClick -= continueSequenceAction;
		BattleController.instance.playerAccessoryButton.ForceActivateAccessory();
		popUpBubble.OutroBubble();
	}

	public override void CleanUp()
	{
		Debug.Log("Tutorial Accessory equip CLEAN UP");
	}
}
