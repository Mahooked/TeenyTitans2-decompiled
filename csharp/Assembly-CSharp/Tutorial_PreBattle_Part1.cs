using System;
using System.Collections;
using UnityEngine;

public class Tutorial_PreBattle_Part1 : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Drag Character to Handheld")
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
		Vector3 screenPoint = preBattleParty.lowerCamera.WorldToScreenPoint(preBattleParty.slots[0].transform.position);
		Vector3 startPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint);
		screenPoint = Camera.main.WorldToScreenPoint(preBattleController.handheldConsole.centerArrow.transform.position);
		Vector3 endPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint) + new Vector3(0f, -80f, 0f);
		endPoint.z = startPoint.z;
		TutorialHands tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		tutorialHands.ShowDrag(startPoint, endPoint, 300f, 0.25f);
		preBattleParty.slots[0].boxCollider2D.enabled = true;
		bool dragStarted = false;
		Action dragStartedDelegate = () =>
		{
			dragStarted = true;
		};
		preBattleParty.DragStarted += dragStartedDelegate;
		yield return new WaitWhile(() => !dragStarted && Player.instance.activeTeam.Count == 0);
		preBattleParty.DragStarted -= dragStartedDelegate;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		yield return new WaitUntil(() => Player.instance.activeTeam.Count > 0);
		preBattleParty.slots[0].boxCollider2D.enabled = false;
		preBattleController.handheldConsole.leftArrow.uiItem.enabled = false;
		preBattleController.handheldConsole.centerArrow.uiItem.enabled = false;
		preBattleController.handheldConsole.rightArrow.uiItem.enabled = false;
		screenPoint = preBattleParty.lowerCamera.WorldToScreenPoint(preBattleController.teamSelectScreen.goButton.transform.position + new Vector3(0f, 30f, 0f));
		startPoint = preBattleParty.upperCamera.ScreenToWorldPoint(screenPoint);
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		tutorialHands.ShowArrow(startPoint);
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
