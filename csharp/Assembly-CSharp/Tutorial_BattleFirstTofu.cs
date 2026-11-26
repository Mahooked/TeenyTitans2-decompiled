using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_BattleFirstTofu : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("First Tofu")
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

	public override bool isGameStateValid => BattleController.instance != null && BattleController.instance.isBattleRunning && BattleObjectManager.instance.GetObject<TofuPickup>() != null;

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		BattleObjectManager.instance.ResetSpawnChances();
		List<TofuPickup> allTofuPickups = BattleObjectManager.instance.GetAllObjects<TofuPickup>();
		int len = allTofuPickups.Count;
		for (int i = 0; i < len; i++)
		{
			allTofuPickups[i].uiItem.enabled = false;
		}
		PauseManager.instance.isEnabled = false;
		yield return new WaitWhile(() => BattleController.instance.waiting);
		Time.timeScale = 0f;
		GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		TofuPickup firstPickup = allTofuPickups[0];
		Vector3 uiOffset = new Vector3(0f, 0f, -30f);
		TutorialHands tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		tutorialHands.ShowTap(firstPickup.transform.position + uiOffset);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(firstPickup.gameObject), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: firstPickup.transform.position + uiOffset), message: Language.Get("TUTORIAL_TOFU1", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
		for (int num = 0; num < len; num++)
		{
			allTofuPickups[num].uiItem.enabled = true;
		}
		yield return new WaitUntil(() => firstPickup.collected);
		GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
		ObjectPool.PoolObject(tutorialHands.gameObject);
		popUpBubble.OutroBubble(0f, poolWhenFinished: true);
		Time.timeScale = 1f;
		PauseManager.instance.isEnabled = true;
	}

	public override void CleanUp()
	{
	}
}
