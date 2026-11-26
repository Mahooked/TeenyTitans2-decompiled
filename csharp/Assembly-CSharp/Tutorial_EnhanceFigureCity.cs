using System;
using System.Collections;
using UnityEngine;

public class Tutorial_EnhanceFigureCity : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Enhance Figure City Step")
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

	public override bool isGameStateValid => CityController.instance != null && !TitleScreenController.instance.titleScreenActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !CityController.instance.communicator.MissionScrollerActive() && !CityController.instance.pauseScreen.gameObject.activeSelf && !GameUI.instance.showingCinemaBars && !CityReward.instance.showingRewards && CityController.instance.cityCharacter.AllowInput;

	public override IEnumerator PlayTutorial(int stepIndex)
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
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(CityController.instance.titanTowerBtnObj), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: CityController.instance.titanTowerBtnObj.transform.position + cityTowerBtnBubblePositionOffset), message: Language.Get("TUTORIAL_SHORTCUTTOWER", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
		tk2dUIItem towerBtnUIItem = CityController.instance.titanTowerBtnObj.GetComponent<tk2dUIItem>();
		towerBtnUIItem.enabled = false;
		yield return popUpBubble.WaitUntilIntroComplete();
		towerBtnUIItem.enabled = true;
		bool continueSequence = false;
		Action continueSequenceAction = () =>
		{
			continueSequence = true;
		};
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

	public override void CleanUp()
	{
	}
}
