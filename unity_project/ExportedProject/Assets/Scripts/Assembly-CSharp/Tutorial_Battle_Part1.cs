using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tutorial_Battle_Part1 : Tutorial
{
	private BattleController battleController;

	private Character playerCharacter;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("First Battle")
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
		Debug.Log("START TUTORIAL BATTLE PART 1");
		battleController = BattleController.instance;
		battleController.pauseScreen.showHomeButton = false;
		bool continueSequence = false;
		Action continueAction = () =>
		{
			continueSequence = true;
		};
		BattleController.instance.playerActionMeter.Initialized += continueAction;
		yield return new WaitUntil(() => continueSequence);
		BattleController.instance.playerActionMeter.Initialized -= continueAction;
		AbilityButton focusedAbilityButton = null;
		for (int num = 0; num < battleController.playerActionMeter.abilityButtons.Count; num++)
		{
			AbilityButton abilityButton = battleController.playerActionMeter.abilityButtons[num];
			if (abilityButton != null)
			{
				if (abilityButton.canActivate && abilityButton.abilityData != null && abilityButton.abilityData.type == AbilityType.Attack && focusedAbilityButton == null)
				{
					focusedAbilityButton = abilityButton;
					continue;
				}
				abilityButton.canActivate = false;
				abilityButton.gameObject.SetActive(false);
			}
		}
		yield return new WaitWhile(() => !battleController.battleStarted);
		BattleController.instance.CharacterSwappedOut += HandleCharacterSwappedOut;
		BattleController.instance.CharacterSwappedIn += HandleCharacterSwappedIn;
		HandleCharacterSwappedIn(battleController.playerCharacter.character);
		battleController.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = false;
		battleController.playerPortraits.boxCollider2D.enabled = false;
		battleController.ignoreLongPress = true;
		BattleObjectManager.instance.spawnChanceTofu = 0f;
		BattleObjectManager.instance.spawnChanceCoin = 0f;
		BattleObjectManager.instance.spawnChanceBattery = 0f;
		int len = battleController.playerParty.Count;
		for (int num2 = 0; num2 < len; num2++)
		{
			battleController.playerParty[num2].luckModifier = 0f;
			battleController.playerParty[num2].dodgeModifier = 0f;
		}
		len = battleController.enemyParty.Count;
		for (int num3 = 0; num3 < len; num3++)
		{
			battleController.enemyParty[num3].luckModifier = 0f;
			battleController.enemyParty[num3].dodgeModifier = 0f;
		}
		yield return new WaitUntil(() => focusedAbilityButton.isActivated);
		TutorialHands tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		tutorialHands.ShowArrow(focusedAbilityButton.uiItem.transform.position + new Vector3(0f, 20f, 0f));
		bool abilityUsed = false;
		Action<ActionMeter, Character, AbilityData> abilityUsedDelegate = (ActionMeter meter, Character character, AbilityData abilityData) =>
		{
			abilityUsed = abilityData.uid == focusedAbilityButton.abilityData.uid;
		};
		battleController.playerActionMeter.AbilityUsed += abilityUsedDelegate;
		yield return new WaitWhile(() => !abilityUsed);
		battleController.playerActionMeter.AbilityUsed -= abilityUsedDelegate;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		PauseManager.instance.isEnabled = false;
		Time.timeScale = 0f;
		GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		TutorialPopup popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		popup.SetMessage(Language.Get("TUTORIAL_BATTLE_5", "TUTORIALS"));
		popup.transform.position = focusedAbilityButton.abilityButtonNumber.transform.position + new Vector3(7f, 12f, 0f);
		yield return new WaitForTap();
		ObjectPool.PoolObject(popup.gameObject);
		PauseManager.instance.isEnabled = true;
		Time.timeScale = 1f;
		GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
		BattleController.instance.enemyCharacter.character.IncomingDamage += HandleEnemyCharacterIncomingDamage;
		yield return new WaitWhile(() => battleController.waiting);
		float tweenDuration = 0.33f;
		for (int num4 = 0; num4 < battleController.playerActionMeter.abilityButtons.Count; num4++)
		{
			AbilityButton abilityButton2 = battleController.playerActionMeter.abilityButtons[num4];
			abilityButton2.canActivate = !StatusEffectManager.instance.CharacterHasAbilityBlockerOnButton(playerCharacter, abilityButton2);
			if (!abilityButton2.gameObject.activeInHierarchy)
			{
				abilityButton2.gameObject.SetActive(true);
				abilityButton2.transform.localScale = Vector3.zero;
				abilityButton2.transform.DOScale(1f, tweenDuration).SetEase(Ease.OutBounce);
			}
		}
		yield return new WaitForSeconds(tweenDuration);
		PauseManager.instance.isEnabled = false;
		Time.timeScale = 0f;
		GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		AbilityButton firstAbilityButton = battleController.playerActionMeter.abilityButtons[0];
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		tutorialHands.ShowDrag(firstAbilityButton.uiItem.transform.position, firstAbilityButton.uiItem.transform.position, 1f, 1f);
		popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		popup.SetMessage(Language.Get("TUTORIAL_BATTLE_6", "TUTORIALS"));
		popup.transform.position = firstAbilityButton.uiItem.transform.position + new Vector3(0f, 20f, 0f);
		battleController.ignoreLongPress = false;
		for (int num5 = 0; num5 < battleController.playerActionMeter.abilityButtons.Count; num5++)
		{
			AbilityButton abilityButton3 = battleController.playerActionMeter.abilityButtons[num5];
			abilityButton3.ignorePress = true;
		}
		yield return new WaitUntil(() => battleController.isViewingAbilityDescriptions);
		for (int num6 = 0; num6 < battleController.playerActionMeter.abilityButtons.Count; num6++)
		{
			AbilityButton abilityButton4 = battleController.playerActionMeter.abilityButtons[num6];
			abilityButton4.ignorePress = false;
		}
		ObjectPool.PoolObject(tutorialHands.gameObject);
		ObjectPool.PoolObject(popup.gameObject);
		yield return new WaitWhile(() => Mathf.Approximately(Time.timeScale, 0f));
		PauseManager.instance.isEnabled = true;
		BattleObjectManager.instance.spawnChanceCoin = 1f;
		battleController.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = true;
		bool coinsSpawned = false;
		Action coinsSpawnedDelegate = () =>
		{
			coinsSpawned = true;
		};
		BattleObjectManager.instance.SpawnedCoins += coinsSpawnedDelegate;
		yield return new WaitUntil(() => coinsSpawned);
		BattleObjectManager.instance.SpawnedCoins -= coinsSpawnedDelegate;
		yield return null;
		battleController.playerPortraits.boxCollider2D.enabled = true;
		PauseManager.instance.isEnabled = false;
		List<CoinPickup> allCoins = BattleObjectManager.instance.GetAllObjects<CoinPickup>();
		int len2 = allCoins.Count;
		for (int num7 = 0; num7 < len2; num7++)
		{
			allCoins[num7].boxCollider2D.enabled = false;
		}
		yield return new WaitWhile(() => battleController.waiting);
		Time.timeScale = 0f;
		GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		CoinPickup coinPickup = allCoins[0];
		tutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tutorialHands.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		tutorialHands.ShowTap(coinPickup.transform.position);
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		popUpBubble.Init(targetWorldPosition: CameraUtility.GetWorldPositionInCamera(sourceCamera: CameraUtility.GetCameraRenderingObject(coinPickup.gameObject), targetCamera: CameraUtility.GetCameraRenderingObject(popUpBubble.gameObject), position: coinPickup.transform.position + new Vector3(0f, 0f, -30f)), message: Language.Get("TUTORIAL_BATTLE_7", "TUTORIALS"), targetCamera: GUICamera.instance.camera, forcedBubbleType: PopUpBubble.BubbleType.UpsideDown, flipTailX: true);
		for (int num8 = 0; num8 < len2; num8++)
		{
			allCoins[num8].boxCollider2D.enabled = true;
		}
		battleController.ignoreLongPress = true;
		for (int num9 = 0; num9 < battleController.playerActionMeter.abilityButtons.Count; num9++)
		{
			AbilityButton abilityButton5 = battleController.playerActionMeter.abilityButtons[num9];
			abilityButton5.ignorePress = true;
		}
		yield return new WaitUntil(() => coinPickup.collected);
		battleController.ignoreLongPress = false;
		for (int num10 = 0; num10 < battleController.playerActionMeter.abilityButtons.Count; num10++)
		{
			AbilityButton abilityButton6 = battleController.playerActionMeter.abilityButtons[num10];
			abilityButton6.ignorePress = false;
		}
		BattleController.instance.enemyCharacter.character.IncomingDamage -= HandleEnemyCharacterIncomingDamage;
		ObjectPool.PoolObject(tutorialHands.gameObject);
		popUpBubble.OutroBubble(0f, true);
		BattleObjectManager.instance.ResetSpawnChances();
		BattleObjectManager.instance.spawnChanceTofu = 0f;
		Time.timeScale = 1f;
		GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
		PauseManager.instance.isEnabled = true;
		yield return new WaitUntil(() => BattleController.instance.battleEnded);
		BattleController.instance.CharacterSwappedOut -= HandleCharacterSwappedOut;
		BattleController.instance.CharacterSwappedIn -= HandleCharacterSwappedIn;
		BattleController.instance.playerCharacter.character.IncomingDamage -= HandleCharacterIncomingDamage;
		len = battleController.playerParty.Count;
		for (int num11 = 0; num11 < len; num11++)
		{
			battleController.playerParty[num11].luckModifier = 1f;
			battleController.playerParty[num11].dodgeModifier = 1f;
		}
		len = battleController.enemyParty.Count;
		for (int num12 = 0; num12 < len; num12++)
		{
			battleController.enemyParty[num12].luckModifier = 1f;
			battleController.enemyParty[num12].dodgeModifier = 1f;
		}
	}

	private void HandleCharacterSwappedOut(Character character)
	{
		if (battleController.playerCharacter.character == character)
		{
			character.IncomingDamage -= HandleCharacterIncomingDamage;
		}
	}

	private void HandleCharacterSwappedIn(Character character)
	{
		if (battleController.playerCharacter.character == character)
		{
			character.IncomingDamage += HandleCharacterIncomingDamage;
		}
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

	private void HandleEnemyCharacterIncomingDamage(DamageResult damageResult)
	{
		if (damageResult.totalDamage >= damageResult.target.hitPoints)
		{
			damageResult.damage = Mathf.Max(1, damageResult.target.hitPoints - 1);
			damageResult.classBonusDamage = 0;
			damageResult.luckBonusDamage = 0;
		}
	}

	public override void CleanUp()
	{
	}
}
