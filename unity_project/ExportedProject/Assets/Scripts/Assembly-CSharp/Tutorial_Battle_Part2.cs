using System.Collections;
using UnityEngine;

public class Tutorial_Battle_Part2 : Tutorial
{
	private BattleController battleController;

	private const float LOW_HEALTH_THRESHOLD = 0.2f;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("Battle Tutorial Part 2")
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
			return BattleController.instance != null;
		}
	}

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		battleController = BattleController.instance;
		battleController.pauseScreen.showHomeButton = false;
		yield return new WaitUntil(() => battleController.isBattleRunning);
		battleController.CharacterSwappedOut += HandleCharacterSwappedOut;
		battleController.CharacterSwappedIn += HandleCharacterSwappedIn;
		HandleCharacterSwappedIn(battleController.playerCharacter.character);
		BattleObjectManager.instance.spawnChanceTofu = 1f;
		battleController.BattleEnded += HandleBattleEnded;
	}

	private void HandleBattleEnded()
	{
		battleController.BattleEnded -= HandleBattleEnded;
		battleController.CharacterSwappedOut -= HandleCharacterSwappedOut;
		battleController.CharacterSwappedIn -= HandleCharacterSwappedIn;
		HandleCharacterSwappedOut(battleController.playerCharacter.character);
	}

	private void HandleCharacterSwappedOut(Character character)
	{
		character.IncomingDamage -= HandleCharacterIncomingDamage;
		character.UpdatedCharacter -= HandlePlayerCharacterUpdated;
	}

	private void HandleCharacterSwappedIn(Character character)
	{
		character.IncomingDamage += HandleCharacterIncomingDamage;
		character.UpdatedCharacter += HandlePlayerCharacterUpdated;
	}

	private void HandleCharacterIncomingDamage(DamageResult damageResult)
	{
		if (damageResult.target != null && (damageResult.target.hitPointsRatio <= 0.2f || damageResult.totalDamage >= damageResult.target.hitPoints))
		{
			damageResult.damage = 0;
			damageResult.classBonusDamage = 0;
			damageResult.luckBonusDamage = 0;
		}
	}

	private void HandlePlayerCharacterUpdated(Character character)
	{
		if (battleController.numAliveInPlayerParty == 1 && character.hitPointsRatio <= 0.2f)
		{
			battleController.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = false;
		}
		else
		{
			battleController.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = true;
		}
	}

	public override void CleanUp()
	{
	}
}
