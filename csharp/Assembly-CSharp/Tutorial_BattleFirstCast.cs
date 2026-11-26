using System;
using System.Collections;
using UnityEngine;

public class Tutorial_BattleFirstCast : Tutorial
{
	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("First Cast")
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
		bool continueSequence = false;
		ActionMeter actionMeter = null;
		AbilityButton abilityButton = null;
		Action<ActionMeter, AbilityButton> handleCastStarted = (ActionMeter meter, AbilityButton btn) =>
		{
			continueSequence = true;
			actionMeter = meter;
			abilityButton = btn;
		};
		BattleController.instance.playerActionMeter.CastStartedAbility += handleCastStarted;
		yield return new WaitUntil(() => continueSequence);
		BattleController.instance.playerActionMeter.CastStartedAbility -= handleCastStarted;
		PlayerAI enemyAI = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI>();
		enemyAI.isEnabled = false;
		yield return new WaitForSeconds(abilityButton.abilityData.castTime * 0.5f);
		PauseManager.instance.isEnabled = false;
		Time.timeScale = 0f;
		Vector3 bubblePosition = actionMeter.fillCastSprite.transform.position + new Vector3(actionMeter.fillCastSprite.width, 0f);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		popUpBubble.Init(Language.Get("PURPLECHARGE", "TUTORIALS"), bubblePosition, GUICamera.instance.camera);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.Init(Language.Get("PURPLECHARGE2", "TUTORIALS"), bubblePosition, GUICamera.instance.camera);
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		PauseManager.instance.isEnabled = true;
		Time.timeScale = 1f;
		continueSequence = false;
		Action<ActionMeter, Character, AbilityData> handleAbilityUsed = (ActionMeter x, Character y, AbilityData z) =>
		{
			continueSequence = true;
		};
		actionMeter.AbilityUsed += handleAbilityUsed;
		yield return new WaitUntil(() => continueSequence);
		actionMeter.AbilityUsed -= handleAbilityUsed;
		enemyAI.isEnabled = true;
	}

	public override void CleanUp()
	{
	}
}
