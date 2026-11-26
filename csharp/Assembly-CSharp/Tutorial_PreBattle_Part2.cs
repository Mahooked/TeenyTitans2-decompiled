using System;
using System.Collections;
using UnityEngine;

public class Tutorial_PreBattle_Part2 : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Check Character Pre-Battle Info")
	};

	private PreBattleController preBattleController;

	private PreBattleParty preBattleParty;

	private Animator handheldAnimator;

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

	public override bool isGameStateValid => PreBattleController.Instance != null;

	public override IEnumerator PlayTutorial(int step)
	{
		preBattleController = PreBattleController.Instance;
		preBattleParty = preBattleController.teamSelectScreen.preBattleParty;
		handheldAnimator = preBattleController.handheldConsole.model.animator;
		preBattleController.exitButtonSprite.gameObject.SetActive(value: false);
		yield return null;
		int len = preBattleParty.slots.Count;
		for (int i = 0; i < len; i++)
		{
			preBattleParty.slots[i].boxCollider2D.enabled = false;
		}
		while (!handheldAnimator.GetCurrentAnimatorStateInfo(0).IsName("appear_loop"))
		{
			yield return null;
		}
		Vector3 screenPoint = preBattleParty.lowerCamera.WorldToScreenPoint(preBattleParty.slots[1].transform.position + new Vector3(0f, 20f, 0f));
		Vector3 worldPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint);
		TutorialHands tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		tutorialHands.ShowArrow(worldPoint);
		preBattleParty.slots[1].boxCollider2D.enabled = true;
		preBattleParty.isDragEnabled = false;
		TutorialPopup popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		popup.SetMessage(Language.Get("TAPVIEWFIG", "TUTORIALS"));
		popup.transform.position = worldPoint + new Vector3(20f, 12f, 0f);
		bool figSelected = false;
		Action<Character> figSelectedDelegate = (Character character) =>
		{
			figSelected = true;
		};
		preBattleParty.CharacterSelected += figSelectedDelegate;
		yield return new WaitWhile(() => !figSelected);
		preBattleParty.CharacterSelected -= figSelectedDelegate;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		ObjectPool.PoolObject(popup.gameObject);
		preBattleParty.slots[1].boxCollider2D.enabled = false;
		preBattleParty.handheldConsole.leftArrow.uiItem.enabled = false;
		preBattleParty.handheldConsole.centerArrow.uiItem.enabled = false;
		preBattleParty.handheldConsole.rightArrow.uiItem.enabled = false;
		yield return null;
		preBattleParty.characterInfoBox.EnableButtons(enable: false);
		CharacterViewAbilityButton abilityButton = preBattleParty.characterInfoBox.abilityButtons.Find((CharacterViewAbilityButton obj) => obj.ability != null);
		abilityButton.uiItem.enabled = true;
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		tutorialHands.ShowArrow(abilityButton.transform.position + new Vector3(0f, 10f, 0f));
		popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		popup.SetMessage(Language.Get("TAPLEARNABILITIES", "TUTORIALS"));
		popup.transform.position = abilityButton.transform.position + new Vector3(12f, 12f, 0f);
		bool abilitySelected = false;
		Action abilitySelectedDelegate = () =>
		{
			abilitySelected = true;
		};
		abilityButton.uiItem.OnClick += abilitySelectedDelegate;
		yield return new WaitWhile(() => !abilitySelected);
		abilityButton.uiItem.OnClick -= abilitySelectedDelegate;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		ObjectPool.PoolObject(popup.gameObject);
		preBattleController.teamSelectScreen.minFigsToGo = 2;
		preBattleParty.characterInfoBox.EnableButtons();
		preBattleParty.handheldConsole.leftArrow.uiItem.enabled = true;
		preBattleParty.handheldConsole.centerArrow.uiItem.enabled = true;
		preBattleParty.handheldConsole.rightArrow.uiItem.enabled = true;
		preBattleParty.slots[0].boxCollider2D.enabled = true;
		preBattleParty.slots[1].boxCollider2D.enabled = true;
		preBattleParty.isDragEnabled = true;
		yield return new WaitWhile(() => Player.instance.activeTeam.Count < 1);
		if (preBattleController.handheldConsole.leftArrow.character != null)
		{
			preBattleController.handheldConsole.leftArrow.uiItem.enabled = false;
		}
		else if (preBattleController.handheldConsole.centerArrow.character != null)
		{
			preBattleController.handheldConsole.centerArrow.uiItem.enabled = false;
		}
		else
		{
			preBattleController.handheldConsole.rightArrow.uiItem.enabled = false;
		}
		screenPoint = preBattleParty.lowerCamera.WorldToScreenPoint(preBattleParty.slots.Find((MySquadSlot obj) => !Player.instance.activeTeam.Contains(obj.character)).transform.position);
		Vector3 startPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint);
		screenPoint = Camera.main.WorldToScreenPoint((preBattleController.handheldConsole.centerArrow.character != null) ? preBattleController.handheldConsole.leftArrow.transform.position : preBattleController.handheldConsole.centerArrow.transform.position);
		Vector3 endPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint) + new Vector3(0f, -80f, 0f);
		endPoint.z = startPoint.z;
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		tutorialHands.ShowDrag(startPoint, endPoint, 300f, 0.25f);
		for (int num = 0; num < len; num++)
		{
			preBattleParty.slots[num].boxCollider2D.enabled = true;
		}
		yield return new WaitWhile(() => Player.instance.activeTeam.Count < 2);
		ObjectPool.PoolObject(tutorialHands.gameObject);
		preBattleController.handheldConsole.leftArrow.uiItem.enabled = false;
		preBattleController.handheldConsole.centerArrow.uiItem.enabled = false;
		preBattleController.handheldConsole.rightArrow.uiItem.enabled = false;
		screenPoint = preBattleParty.lowerCamera.WorldToScreenPoint(preBattleController.teamSelectScreen.goButton.transform.position + new Vector3(0f, 30f, 0f));
		worldPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint);
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		tutorialHands.ShowArrow(worldPoint);
		bool goClicked = false;
		Action goClickedDelegate = () =>
		{
			goClicked = true;
		};
		preBattleController.teamSelectScreen.goButton.OnClick += goClickedDelegate;
		yield return new WaitWhile(() => !goClicked);
		preBattleController.teamSelectScreen.goButton.OnClick -= goClickedDelegate;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		yield return new WaitWhile(() => PreBattleController.Instance != null);
	}

	public override void CleanUp()
	{
	}
}
