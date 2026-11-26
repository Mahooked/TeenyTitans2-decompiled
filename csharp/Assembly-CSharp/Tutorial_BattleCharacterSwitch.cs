using System;
using System.Collections;
using UnityEngine;

public class Tutorial_BattleCharacterSwitch : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Character Switch")
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

	public override bool isGameStateValid => BattleController.instance != null;

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		PortraitSwitcher playerPortraits = BattleController.instance.playerPortraits;
		playerPortraits.isEnabled = false;
		yield return new WaitUntil(() => BattleController.instance.isBattleRunning);
		PauseManager.instance.isEnabled = false;
		BattleController.instance.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = false;
		yield return new WaitUntil(() => BattleController.instance.playerActionMeter.progress >= 0.15f && !BattleController.instance.waiting);
		bool continueSequence = false;
		Character playerCharacter = BattleController.instance.playerCharacter.character;
		playerPortraits.isEnabled = true;
		Time.timeScale = 0f;
		BattleController.instance.playerActionMeter.EnableAllButtons(enable: false);
		BattleController.instance.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = true;
		Character characterBeforeSwitch = BattleController.instance.playerCharacter.character;
		TutorialHands tutorialHands;
		TutorialPopup popup;
		if (!playerPortraits.IsSelectMode)
		{
			tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
			tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
			tutorialHands.ShowArrow(playerPortraits.transform.position + new Vector3(0f, 20f, 0f));
			popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
			popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
			popup.SetMessage(Language.Get("TUTORIAL_BATTLE_8", "TUTORIALS"));
			popup.transform.position = playerPortraits.transform.position + new Vector3(20f, 20f, 0f);
			playerPortraits.boxCollider2D.enabled = true;
			Action selectModeEnteredDelegate = () =>
			{
				continueSequence = true;
			};
			playerPortraits.SelectModeEntered += selectModeEnteredDelegate;
			yield return new WaitWhile(() => !continueSequence);
			playerPortraits.SelectModeEntered -= selectModeEnteredDelegate;
			ObjectPool.PoolObject(tutorialHands.gameObject);
			ObjectPool.PoolObject(popup.gameObject);
		}
		int len = playerPortraits.portraits.Count;
		for (int num = 0; num < len; num++)
		{
			if (playerPortraits.portraits[num].character == playerCharacter)
			{
				playerPortraits.portraits[num].uiItemCollider.enabled = false;
			}
		}
		playerPortraits.blackFade.collider2D.enabled = false;
		playerPortraits.boxCollider2D.enabled = false;
		yield return new WaitForSecondsIndependent(0.25f);
		Transform portraitTransform = playerPortraits.portraits.Find((Portrait obj) => obj.character != playerCharacter && Mathf.RoundToInt(obj.alpha) == 1).transform;
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		tutorialHands.ShowTap(portraitTransform.position);
		continueSequence = false;
		Action<Character> characterChangedDelegate = (Character character) =>
		{
			continueSequence = true;
		};
		playerPortraits.CharacterChanged += characterChangedDelegate;
		while (!continueSequence)
		{
			if (characterBeforeSwitch != BattleController.instance.playerCharacter.character)
			{
				continueSequence = true;
			}
			yield return null;
		}
		playerPortraits.CharacterChanged -= characterChangedDelegate;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		BattleController.instance.playerActionMeter.EnableAllButtons();
		len = playerPortraits.portraits.Count;
		for (int num2 = 0; num2 < len; num2++)
		{
			playerPortraits.portraits[num2].uiItemCollider.enabled = false;
		}
		playerPortraits.blackFade.collider2D.enabled = true;
		yield return new WaitWhile(() => BattleController.instance.waiting);
		Time.timeScale = 0f;
		Vector3 position = BattleController.instance.playerActionMeter.fillSprite.transform.position + new Vector3(BattleController.instance.playerActionMeter.fillSprite.width, 0f, 0f);
		popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		popup.SetMessage(Language.Get("TUTORIAL_BATTLE_11", "TUTORIALS"));
		popup.transform.position = position + new Vector3(0f, 10f, 0f);
		yield return new WaitForTap();
		ObjectPool.PoolObject(popup.gameObject);
		Time.timeScale = 1f;
		PauseManager.instance.isEnabled = true;
		playerPortraits.boxCollider2D.enabled = true;
		BattleObjectManager.instance.ResetSpawnChances();
	}

	public override void CleanUp()
	{
	}
}
