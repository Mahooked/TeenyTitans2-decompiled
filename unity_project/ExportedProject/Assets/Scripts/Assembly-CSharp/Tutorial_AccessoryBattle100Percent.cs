using System;
using System.Collections;
using UnityEngine;

public class Tutorial_AccessoryBattle100Percent : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
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
			return BattleController.instance != null && BattleController.instance.battleStarted && BattleController.instance.playerAccessoryButton.accessoryUid >= 0 && BattleController.instance.playerAccessoryButton.currentFillAmount >= 1f && !BattleController.instance.waiting;
		}
	}

	public override IEnumerator PlayTutorial(int stepIndex)
	{
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
	}
}
