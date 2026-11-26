using System;
using System.Collections;
using UnityEngine;

public class Tutorial_AccessoryPreBattleEquip : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Accessory PreBattle Equip and Usage")
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
			return PreBattleController.Instance != null && LoadingScreenCover.allowInit && Player.instance.activeAccessories.Count > 0;
		}
	}

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		Debug.Log("Prebattle Equip Sequence");
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

	public override void CleanUp()
	{
	}
}
