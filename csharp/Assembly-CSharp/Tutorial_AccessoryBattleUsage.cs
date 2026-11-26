using System.Collections;
using UnityEngine;

public class Tutorial_AccessoryBattleUsage : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Accessory Battle How to Charge")
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

	public override bool isGameStateValid => BattleController.instance != null && BattleController.instance.battleStarted && BattleController.instance.playerAccessoryButton.accessoryUid >= 0;

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		BattleController.instance.ForceStartWaiting();
		yield return new WaitUntil(() => BattleController.instance.playerAccessoryButton.entered);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		tk2dBaseSprite accessoryBtnSprite = BattleController.instance.playerAccessoryButton.backingFillSprite;
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(accessoryBtnSprite.gameObject), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: accessoryBtnSprite.transform.position + accessoryBtnSprite.GetBounds().max), message: Language.Get("TUTORIAL_ACCESSORYBATTLE3", "TUTORIALS"), targetCamera: GUICamera.instance.camera);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		BattleController.instance.ForceEndWaiting();
	}

	public override void CleanUp()
	{
	}
}
