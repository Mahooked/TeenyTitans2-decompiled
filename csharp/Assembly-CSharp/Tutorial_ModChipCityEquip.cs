using System;
using System.Collections;
using UnityEngine;

public class Tutorial_ModChipCityEquip : Tutorial
{
	[Space]
	public Vector3 cityTowerBtnBubblePositionOffset;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Equip New ModChip")
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
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		Vector3 popUpTargetPos = CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(CityController.instance.titanTowerBtnObj), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: CityController.instance.titanTowerBtnObj.transform.position + cityTowerBtnBubblePositionOffset);
		popUpBubble.InitWithoutTail(Language.Get("TUTORIAL_MODCHIP1", "TUTORIALS"), popUpTargetPos, GUICamera.instance.camera, PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
		tk2dUIItem towerBtnUIItem = CityController.instance.titanTowerBtnObj.GetComponent<tk2dUIItem>();
		towerBtnUIItem.enabled = false;
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		if (Player.instance.HasCharacterAtLevel(5))
		{
			towerBtnUIItem.enabled = true;
			popUpBubble.Init(Language.Get("TUTORIAL_REMOTETOWER", "TUTORIALS"), popUpTargetPos, GUICamera.instance.camera, PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
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
		}
		else
		{
			popUpBubble.InitWithoutTail(Language.Get("TUTORIAL_MODCHIP2B", "TUTORIALS"), popUpTargetPos, GUICamera.instance.camera, PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
			yield return popUpBubble.WaitUntilIntroComplete();
			yield return new WaitForTap();
			popUpBubble.Init(Language.Get("TUTORIAL_MODCHIP6", "TUTORIALS"), popUpTargetPos, GUICamera.instance.camera, PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
			yield return popUpBubble.WaitUntilIntroComplete();
			yield return new WaitForTap();
			popUpBubble.OutroBubble(0f, poolWhenFinished: true);
			towerBtnUIItem.enabled = true;
			yield return popUpBubble.WaitUntilOutroComplete();
		}
		PauseManager.instance.isEnabled = true;
		CityController.instance.cityCharacter.AllowInput = true;
		CityController.instance.PreventManualWayPoint(prevent: false);
		CityController.instance.communicator.pauseTime = false;
		CityController.instance.pinchZoomer.allowPinchZoom = true;
		CityController.instance.pinchZoomer.allowPan = true;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = true;
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		GameUI.instance.ShowMySquad();
		GameUI.instance.ShowAllLandmarks();
		GameUI.instance.communicator.SetActive(value: true);
		GameUI.instance.pauseButton.SetActive(value: true);
	}

	public override void CleanUp()
	{
	}
}
