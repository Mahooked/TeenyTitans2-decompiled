using System;
using System.Collections;
using UnityEngine;

public class Tutorial_BattleFirstChannel : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("First Channel")
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

	public override bool isGameStateValid => BattleController.instance != null && BattleController.instance.isBattleRunning;

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		PauseManager.instance.isEnabled = false;
		bool continueSequence = false;
		Character casterCharacter = null;
		Action<AbilityData, Character> handleChannelStarted = (AbilityData ability, Character character) =>
		{
			continueSequence = true;
			casterCharacter = character;
		};
		BattleController.instance.playerActionMeter.ChannelStarted += handleChannelStarted;
		yield return new WaitUntil(() => continueSequence);
		BattleController.instance.playerActionMeter.ChannelStarted -= handleChannelStarted;
		PlayerAI enemyAI = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI>();
		enemyAI.isEnabled = false;
		yield return new WaitForSeconds(1f);
		Time.timeScale = 0f;
		ActionMeter actionMeter = ((casterCharacter != BattleController.instance.playerCharacter.character) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
		Vector3 bubblePosition = actionMeter.fillChannelSprite.transform.position + new Vector3(actionMeter.fillChannelSprite.width, 0f);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		popUpBubble.Init(Language.Get("TUTORIAL_CHANNEL1", "TUTORIALS"), bubblePosition, GUICamera.instance.camera);
		yield return new WaitForTap();
		popUpBubble.Init(Language.Get("TUTORIAL_CHANNEL2", "TUTORIALS"), bubblePosition, GUICamera.instance.camera);
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		Time.timeScale = 1f;
		PauseManager.instance.isEnabled = true;
		enemyAI.isEnabled = true;
	}

	public override void CleanUp()
	{
	}
}
