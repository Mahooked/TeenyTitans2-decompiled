using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;

public class Accessory
{
	private class ItemEffectTimer
	{
		public AccessoryItemEffect effect;

		public float duration;

		public float timer;

		public bool triggered;

		public bool isOneShot;

		public ItemEffectTimer(AccessoryItemEffect effect, float duration, bool tickImmediately, bool isOneShot)
		{
			this.effect = effect;
			this.duration = duration;
			timer = ((!tickImmediately) ? duration : 0f);
			triggered = false;
			this.isOneShot = isOneShot;
		}
	}

	private class AnimationCueTimer
	{
		public AccessoryAnimationCue animationCue;

		public float duration;

		public float timer;

		public bool triggered;

		public bool sfxTriggered;

		public bool isOneShot;

		public AnimationCueTimer(AccessoryAnimationCue animationCue, float duration, bool isOneShot)
		{
			this.animationCue = animationCue;
			this.duration = duration;
			timer = duration;
			triggered = false;
			sfxTriggered = false;
			this.isOneShot = isOneShot;
		}
	}

	private Transform particle;

	private AccessoryBattleButton accessoryBattleButton;

	private BattleCharacter currentBattleCharacter;

	private Character currentCharacter;

	private ActionMeter actionMeter;

	private IEnumerator updateCoroutine;

	private bool waitingForTargetDeath;

	private bool modifiedCameraAnimationController;

	private List<AccessoryItemEffect> timedEffects;

	private List<ItemEffectTimer> effectTimers;

	private Dictionary<int, TT2NetPlayerAction> waitForTickActions;

	private List<BattleCharacter> otherBattleCharacters;

	public AccessoryBattleItem accessoryBattleItem { get; private set; }

	private AccessoryItemDefinition accessoryItemDef { get; set; }

	private bool isPlayer
	{
		get
		{
			if (BattleController.instance != null)
			{
				return currentBattleCharacter == BattleController.instance.playerCharacter;
			}
			return false;
		}
	}

	private float DeltaTime
	{
		get
		{
			if (BattleController.battleDefinition != null && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				return TT2NetManager.Instance.DeltaTimeSeconds;
			}
			return Time.deltaTime;
		}
	}

	public event Action<bool> Activated;

	public event Action<bool> IntroFinished;

	public event Action Destroyed;

	public Accessory(AccessoryBattleButton accessoryButton, AccessoryBattleItem battleItem, ActionMeter meter)
	{
		accessoryBattleButton = accessoryButton;
		accessoryBattleItem = battleItem;
		actionMeter = meter;
		accessoryItemDef = accessoryBattleButton.itemDef;
	}

	public void Activate()
	{
		for (int i = 0; i < accessoryItemDef.effects.Count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			accessoryItemEffect.isApplied = false;
			accessoryItemEffect.hasTriggered = false;
		}
		currentBattleCharacter = actionMeter.character;
		currentCharacter = currentBattleCharacter.character;
		currentCharacter.DeathCharacter += HandleCharacterDeath;
		currentCharacter.DamageReceived += HandleIncomingDamage;
		BattleController.instance.CharacterSwappedOut += HandleCharacterSwappedOut;
		BattleController.instance.CharacterSwappedIn += HandleCharacterSwappedIn;
		actionMeter.CharacterChanged += HandleCurrentCharacterChanged;
		ProcessEffectsOfTriggerType(AccessoryEffectTriggerType.OnActivation);
		if (accessoryItemDef.effects.Find((AccessoryItemEffect x) => x.accessoryTriggerType == AccessoryEffectTriggerType.OnIncomingCharacterDamage) != null)
		{
			currentCharacter.IncomingDamage += HandleIncomingDamageProcessEffects;
		}
		if (accessoryItemDef.effects.Find((AccessoryItemEffect x) => x.accessoryTriggerType == AccessoryEffectTriggerType.OnCharacterDamageTaken) != null)
		{
			currentCharacter.DamageReceived += HandleCharacterDamageTaken;
		}
		if (!string.IsNullOrEmpty(accessoryItemDef.introSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(accessoryItemDef.introSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		}
		accessoryBattleItem.animatorEventListener.EventTriggered += HandleAnimationEvent;
		accessoryBattleItem.PlayAnimation((isPlayer || string.IsNullOrEmpty(accessoryItemDef.introAnimationEnemy)) ? accessoryItemDef.introAnimation : accessoryItemDef.introAnimationEnemy, allowBattleWaitingPause: false);
		if (isPlayer)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_ACCESSORIZE", 1);
		}
		GFAudioManager.instance.PlayClipFromResources(accessoryItemDef.introSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		int num = accessoryItemDef.introVOPaths.Length;
		if (num > 0)
		{
			string clipPath = accessoryItemDef.introVOPaths[UnityEngine.Random.Range(0, num)];
			AudioSource audioSource = GFAudioManager.instance.PlayClipFromResources(clipPath, GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
		if (Activated != null)
		{
			Activated(accessoryBattleButton.isPlayer);
		}
		EventMessenger.Instance.Raise(new AccessoryActivatedEvent(this));
		updateCoroutine = UpdateRoutine();
		actionMeter.StartCoroutine(updateCoroutine);
		BattleController.instance.BattleEnded += CleanUpAccessory;
	}

	private void HandleAnimationEvent(AnimationEvent animationEvent)
	{
		BattleCamera battleCamera = null;
		switch (animationEvent.stringParameter)
		{
		case "outro":
			if (IntroFinished != null)
			{
				IntroFinished(accessoryBattleButton.isPlayer);
			}
			break;
		case "completeCameraAnim":
			if (BattleController.instance.waiting && !waitingForTargetDeath)
			{
				BattleController.instance.ForceEndWaiting();
			}
			waitingForTargetDeath = false;
			if (modifiedCameraAnimationController)
			{
				modifiedCameraAnimationController = false;
				if (!BattleController.instance.battleEnded)
				{
					battleCamera = ((!isPlayer) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
					battleCamera.animator.runtimeAnimatorController = ((!isPlayer) ? BattleController.instance.enemyCharacter.cameraController : BattleController.instance.playerCharacter.cameraController);
				}
			}
			break;
		case "centerBattleItem":
			accessoryBattleItem.transform.position = Vector3.zero;
			battleCamera = ((!isPlayer) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
			battleCamera.offset.localPosition = Vector3.zero;
			break;
		case "resetBattleItemPos":
		{
			accessoryBattleItem.transform.DOKill();
			battleCamera = ((!isPlayer) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
			battleCamera.offset.DOKill();
			Vector3 vector = ((!isPlayer) ? accessoryItemDef.enemyOffsetPosition : accessoryItemDef.offsetPosition);
			accessoryBattleItem.transform.position = actionMeter.character.model.position + vector;
			break;
		}
		case "showEnemyTeam":
		{
			BattleCharacter target = ((!isPlayer) ? BattleController.instance.playerCharacter : BattleController.instance.enemyCharacter);
			List<Character> targetParty = ((!isPlayer) ? BattleController.instance.playerParty : BattleController.instance.enemyParty);
			HandleShowingTeam(targetParty, target, TargetType.Enemy);
			break;
		}
		case "hideEnemyTeam":
			HandleRemovingTeam();
			break;
		case "fadeInBattleTheme":
			GFAudioManager.instance.UnPause((ActionMeterSpeedModifier.instanceCount <= 0) ? "battleTheme" : "danceloop");
			break;
		}
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			UnityEngine.Random.InitState(TT2NetManager.Instance.LastSeed);
		}
		for (int i = 0; i < accessoryItemDef.effects.Count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.accessoryTriggerType == AccessoryEffectTriggerType.OnAnimationEvent && accessoryItemEffect.animEventString == animationEvent.stringParameter)
			{
				ProcessAccessoryItemEffect(accessoryItemEffect, false);
			}
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			UnityEngine.Random.state = state;
		}
		for (int j = 0; j < accessoryItemDef.animationCues.Count; j++)
		{
			AccessoryAnimationCue accessoryAnimationCue = accessoryItemDef.animationCues[j];
			if (accessoryAnimationCue.animationTriggerType == AccessoryEffectTriggerType.OnAnimationEvent && accessoryAnimationCue.animEventString == animationEvent.stringParameter)
			{
				TriggerAnimation(accessoryAnimationCue);
			}
		}
	}

	private void HandleCharacterDeath(Character character)
	{
		if (character == currentCharacter)
		{
			currentCharacter.DeathCharacter -= HandleCharacterDeath;
			currentCharacter.DamageReceived -= HandleIncomingDamage;
			currentCharacter.IncomingDamage -= HandleIncomingDamageProcessEffects;
			currentCharacter.DamageReceived -= HandleCharacterDamageTaken;
		}
	}

	public void Tick(int effectIndex, int seed = -1)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (seed != -1)
		{
			UnityEngine.Random.InitState(seed);
		}
		ItemEffectTimer itemEffectTimer = effectTimers[effectIndex];
		itemEffectTimer.triggered = true;
		ProcessAccessoryItemEffect(itemEffectTimer.effect, false);
		if (seed != -1)
		{
			UnityEngine.Random.state = state;
		}
	}

	private IEnumerator WaitForTickConfirm(int effectIndex, float prevTickTimer)
	{
		if (waitForTickActions == null)
		{
			waitForTickActions = new Dictionary<int, TT2NetPlayerAction>();
		}
		TT2NetPlayerAction playerAction = TT2NetManager.SendAccessoryTick(effectIndex, Environment.TickCount);
		waitForTickActions.Add(effectIndex, playerAction);
		yield return new WaitWhile(() => playerAction.isWaiting);
		if (!playerAction.isConfirmed)
		{
			ItemEffectTimer itemEffectTimer = effectTimers[effectIndex];
			itemEffectTimer.timer = prevTickTimer;
		}
		else
		{
			Tick(effectIndex, playerAction.seed);
			waitForTickActions.Remove(effectIndex);
		}
	}

	private IEnumerator UpdateRoutine()
	{
		timedEffects = accessoryItemDef.effects.FindAll((AccessoryItemEffect x) => x.accessoryTriggerType == AccessoryEffectTriggerType.EveryXSeconds || x.accessoryTriggerType == AccessoryEffectTriggerType.OnXSecondsElapsed);
		effectTimers = new List<ItemEffectTimer>();
		for (int num = 0; num < timedEffects.Count; num++)
		{
			AccessoryItemEffect accessoryItemEffect = timedEffects[num];
			effectTimers.Add(new ItemEffectTimer(accessoryItemEffect, accessoryItemEffect.timerDuration.GetRandomValue(), accessoryItemEffect.tickImmediately, accessoryItemEffect.accessoryTriggerType == AccessoryEffectTriggerType.OnXSecondsElapsed));
		}
		List<AccessoryAnimationCue> timedCues = accessoryItemDef.animationCues.FindAll((AccessoryAnimationCue x) => x.animationTriggerType == AccessoryEffectTriggerType.EveryXSeconds || x.animationTriggerType == AccessoryEffectTriggerType.OnXSecondsElapsed);
		List<AnimationCueTimer> cueTimers = new List<AnimationCueTimer>();
		for (int num2 = 0; num2 < timedCues.Count; num2++)
		{
			AccessoryAnimationCue accessoryAnimationCue = timedCues[num2];
			cueTimers.Add(new AnimationCueTimer(accessoryAnimationCue, accessoryAnimationCue.timerDuration, accessoryAnimationCue.animationTriggerType == AccessoryEffectTriggerType.OnXSecondsElapsed));
		}
		int effectLen = effectTimers.Count;
		int cueLen = cueTimers.Count;
		while (true)
		{
			float dt = DeltaTime;
			if (!BattleController.instance.waiting)
			{
				for (int num3 = 0; num3 < effectLen; num3++)
				{
					ItemEffectTimer itemEffectTimer = effectTimers[num3];
					if ((itemEffectTimer.isOneShot && itemEffectTimer.triggered) || (itemEffectTimer.effect.effectType == ModChipEffectType.ActionMeter && (this.actionMeter.isCasting || this.actionMeter.isChanneling)))
					{
						continue;
					}
					float timer = itemEffectTimer.timer;
					itemEffectTimer.timer -= dt;
					if (itemEffectTimer.timer <= itemEffectTimer.effect.animationPlayOffset)
					{
						if (!isPlayer && !string.IsNullOrEmpty(itemEffectTimer.effect.animationNameEnemy))
						{
							accessoryBattleItem.PlayAnimation(itemEffectTimer.effect.animationNameEnemy);
						}
						else
						{
							accessoryBattleItem.PlayAnimation(itemEffectTimer.effect.animationName);
						}
					}
					TT2NetPlayerAction value = null;
					if (waitForTickActions != null)
					{
						waitForTickActions.TryGetValue(num3, out value);
					}
					if (!(itemEffectTimer.timer <= 0f) || (value != null && value.isWaiting))
					{
						continue;
					}
					itemEffectTimer.timer = itemEffectTimer.duration;
					if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
					{
						if (isPlayer)
						{
							BattleController.instance.StartCoroutine(WaitForTickConfirm(num3, timer));
						}
					}
					else
					{
						Tick(num3);
					}
				}
				for (int num4 = 0; num4 < cueLen; num4++)
				{
					AnimationCueTimer animationCueTimer = cueTimers[num4];
					if (animationCueTimer.isOneShot && animationCueTimer.triggered)
					{
						continue;
					}
					animationCueTimer.timer -= dt;
					if (animationCueTimer.animationCue.playSoundEffect && !animationCueTimer.sfxTriggered && animationCueTimer.timer <= animationCueTimer.animationCue.soundEffectPlayOffset)
					{
						animationCueTimer.sfxTriggered = true;
						GFAudioManager.instance.PlayClipFromResources(animationCueTimer.animationCue.soundEffectPath, GFAudioManager.SFX_GROUP, string.Empty);
						if (animationCueTimer.animationCue.fadeOutMusicGroup)
						{
							GFAudioManager.instance.Pause((ActionMeterSpeedModifier.instanceCount <= 0) ? "battleTheme" : "danceloop");
						}
					}
					if (animationCueTimer.timer <= 0f)
					{
						animationCueTimer.timer = animationCueTimer.duration;
						animationCueTimer.triggered = true;
						animationCueTimer.sfxTriggered = false;
						TriggerAnimation(animationCueTimer.animationCue);
					}
				}
				UnityEngine.Random.State state = UnityEngine.Random.state;
				if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
				{
					UnityEngine.Random.InitState(TT2NetManager.Instance.LastSeed);
				}
				for (int num5 = 0; num5 < accessoryItemDef.effects.Count; num5++)
				{
					AccessoryItemEffect accessoryItemEffect2 = accessoryItemDef.effects[num5];
					if (accessoryItemEffect2.isApplied || !accessoryItemEffect2.autoReapplyEffect || !accessoryItemEffect2.hasTriggered)
					{
						continue;
					}
					if (accessoryItemEffect2.targetType == TargetType.Enemy)
					{
						ActionMeter actionMeter = ((!(this.actionMeter == BattleController.instance.playerActionMeter)) ? BattleController.instance.playerActionMeter : BattleController.instance.enemyActionMeter);
						if (!actionMeter.HasStatusEffect<Immunity>())
						{
							ProcessAccessoryItemEffect(accessoryItemEffect2, false);
						}
					}
					else
					{
						ProcessAccessoryItemEffect(accessoryItemEffect2, false);
					}
				}
				if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
				{
					UnityEngine.Random.state = state;
				}
			}
			yield return null;
		}
	}

	private void HandleIncomingDamage(DamageResult damageResult)
	{
		bool flag = false;
		int count = accessoryItemDef.effects.Count;
		for (int i = 0; i < count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.accessoryTriggerType == AccessoryEffectTriggerType.OnIncomingCharacterDamage && accessoryItemEffect.effectType == ModChipEffectType.HandleDamage)
			{
				flag = true;
			}
		}
		TriggerAnimationCuesOfTriggerType(AccessoryEffectTriggerType.OnIncomingCharacterDamage);
		if (!flag && damageResult.abilityData != null && damageResult.abilityData.targetCount == TargetCount.All)
		{
			accessoryBattleButton.RemoveChargeHealth(damageResult.damage);
			TriggerAnimationCuesOfTriggerType(AccessoryEffectTriggerType.OnDamageTaken);
			BattleDisplayManager.instance.ShowDamageResult(damageResult, actionMeter.character, accessoryBattleItem.transform.position + new Vector3(0f, 4f, 0f), "DamageText_AccessoryDamage_Anim");
		}
	}

	private void HandleIncomingDamageProcessEffects(DamageResult damageResult)
	{
		int count = accessoryItemDef.effects.Count;
		for (int i = 0; i < count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.effectType == ModChipEffectType.HandleDamage)
			{
				int num = Mathf.RoundToInt((float)damageResult.damage * accessoryItemEffect.floatValue);
				BattleDisplayManager.instance.ShowDamageResult(damageResult, actionMeter.character, accessoryBattleItem.transform.position + new Vector3(0f, 4f, 0f), "DamageText_AccessoryDamage_Anim");
				if (damageResult.abilityData == null || (damageResult.source != null && (!damageResult.source.IsAbilityShiny(damageResult.abilityData) || damageResult.abilityData.shinyEffect.effectType != AbilityShinyEffectType.AddPiercingDamage)))
				{
					damageResult.damage = Mathf.Max(0, damageResult.damage - num);
					damageResult.classBonusDamage = 0;
					damageResult.luckBonusDamage = 0;
					damageResult.isDodged = false;
				}
				accessoryBattleButton.RemoveChargeHealth(num);
				TriggerAnimationCuesOfTriggerType(AccessoryEffectTriggerType.OnDamageTaken);
				if (accessoryItemEffect.playSoundEffect)
				{
					GFAudioManager.instance.PlayClipFromResources(accessoryItemEffect.soundEffectPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, accessoryItemEffect.soundEffectDelay);
				}
			}
		}
	}

	private void HandleCharacterDamageTaken(DamageResult damageResult)
	{
		ProcessEffectsOfTriggerType(AccessoryEffectTriggerType.OnCharacterDamageTaken);
	}

	private void ProcessEffectsOfTriggerType(AccessoryEffectTriggerType triggerType)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			UnityEngine.Random.InitState(TT2NetManager.Instance.LastSeed);
		}
		for (int i = 0; i < accessoryItemDef.effects.Count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.accessoryTriggerType == triggerType)
			{
				ProcessAccessoryItemEffect(accessoryItemEffect, reverse: false, null);
			}
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			UnityEngine.Random.state = state;
		}
	}

	private void RemoveEffectsOfTriggerType(AccessoryEffectTriggerType triggerType)
	{
		for (int i = 0; i < accessoryItemDef.effects.Count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.accessoryTriggerType == triggerType)
			{
				ProcessAccessoryItemEffect(accessoryItemEffect, reverse: true, null);
			}
		}
	}

	private void TriggerAnimationCuesOfTriggerType(AccessoryEffectTriggerType triggerType)
	{
		for (int i = 0; i < accessoryItemDef.animationCues.Count; i++)
		{
			AccessoryAnimationCue accessoryAnimationCue = accessoryItemDef.animationCues[i];
			if (accessoryAnimationCue.animationTriggerType == triggerType)
			{
				TriggerAnimation(accessoryAnimationCue);
			}
		}
	}

	private void ProcessAccessoryItemEffect(AccessoryItemEffect effect, bool reverse = false, params object[] args)
	{
		BattleCharacter battleCharacter = null;
		ActionMeter actionMeter = null;
		PortraitSwitcher portraitSwitcher = null;
		switch (effect.targetType)
		{
		case TargetType.Enemy:
			battleCharacter = ((!(currentBattleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.playerCharacter : BattleController.instance.enemyCharacter);
			actionMeter = ((!(currentBattleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.playerActionMeter : BattleController.instance.enemyActionMeter);
			portraitSwitcher = ((!(currentBattleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.playerPortraits : BattleController.instance.enemyPortraits);
			break;
		case TargetType.Friendly:
			battleCharacter = ((!(currentBattleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyCharacter : BattleController.instance.playerCharacter);
			actionMeter = ((!(currentBattleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
			portraitSwitcher = ((!(currentBattleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
			break;
		}
		if (battleCharacter == null || actionMeter == null || portraitSwitcher == null)
		{
			return;
		}
		Character character = ((battleCharacter.character != null) ? battleCharacter.character : portraitSwitcher.selectedCharacter);
		if (effect.playSoundEffect)
		{
			GFAudioManager.instance.PlayClipFromResources(effect.soundEffectPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, effect.soundEffectDelay);
		}
		AccessoryItemEffect accessoryItemEffect = effect;
		if (effect.factionOverrides.Count > 0)
		{
			for (int i = 0; i < effect.factionOverrides.Count; i++)
			{
				ModChipEffectFactionOverride modChipEffectFactionOverride = effect.factionOverrides[i];
				if (character.characterDefinition.characterFaction == modChipEffectFactionOverride.faction)
				{
					AccessoryItemEffect accessoryItemEffect2 = new AccessoryItemEffect();
					accessoryItemEffect2.triggerType = effect.triggerType;
					accessoryItemEffect2.effectType = effect.effectType;
					accessoryItemEffect2.probability = modChipEffectFactionOverride.probability;
					accessoryItemEffect2.statType = effect.statType;
					accessoryItemEffect2.actionMeterEffect = effect.actionMeterEffect;
					accessoryItemEffect2.statusEffect = effect.statusEffect;
					accessoryItemEffect2.targetType = effect.targetType;
					accessoryItemEffect2.intValue = modChipEffectFactionOverride.intValue;
					accessoryItemEffect2.intValue2 = modChipEffectFactionOverride.intValue2;
					accessoryItemEffect2.intValue3 = modChipEffectFactionOverride.intValue3;
					accessoryItemEffect2.intValue4 = modChipEffectFactionOverride.intValue4;
					accessoryItemEffect2.floatValue = modChipEffectFactionOverride.floatValue;
					accessoryItemEffect2.boolValue = modChipEffectFactionOverride.boolValue;
					accessoryItemEffect2.boolValue2 = modChipEffectFactionOverride.boolValue2;
					accessoryItemEffect2.boolValue3 = modChipEffectFactionOverride.boolValue3;
					accessoryItemEffect2.boolValue4 = modChipEffectFactionOverride.boolValue4;
					accessoryItemEffect2.colorValue = modChipEffectFactionOverride.colorValue;
					accessoryItemEffect2.statusEffectData = modChipEffectFactionOverride.statusEffectData;
					accessoryItemEffect2.accessoryTriggerType = effect.accessoryTriggerType;
					accessoryItemEffect2.animEventString = effect.animEventString;
					accessoryItemEffect2.timerDuration = effect.timerDuration;
					accessoryItemEffect2.playAnimation = effect.playAnimation;
					accessoryItemEffect2.animationName = effect.animationName;
					accessoryItemEffect2.animationNameEnemy = effect.animationNameEnemy;
					accessoryItemEffect2.animationPlayOffset = effect.animationPlayOffset;
					accessoryItemEffect2.autoReapplyEffect = effect.autoReapplyEffect;
					accessoryItemEffect = accessoryItemEffect2;
					break;
				}
			}
		}
		effect.isApplied = !reverse;
		if (effect.isApplied && !effect.hasTriggered)
		{
			effect.hasTriggered = true;
		}
		Debug.Log(string.Concat(effect.statusEffect, ", applied: ", effect.isApplied, ", hasTriggered: ", effect.hasTriggered));
		switch (accessoryItemEffect.effectType)
		{
		case ModChipEffectType.StatBonus:
			ApplyAccessoryStatBonus(accessoryItemEffect, reverse, character);
			break;
		case ModChipEffectType.StatMultiplier:
			ApplyAccessoryStatMultiplier(accessoryItemEffect, reverse, character);
			break;
		case ModChipEffectType.ActionMeter:
			ApplyAccessoryActionMeterEffect(accessoryItemEffect, reverse, actionMeter);
			break;
		case ModChipEffectType.CastTimeMultiplier:
			break;
		case ModChipEffectType.DropCoins:
			break;
		case ModChipEffectType.DropTofu:
			break;
		case ModChipEffectType.CoinDropChance:
			break;
		case ModChipEffectType.TofuDropChance:
			break;
		case ModChipEffectType.CancelInterrupt:
			break;
		case ModChipEffectType.DoDamage:
		{
			if (reverse)
			{
				break;
			}
			int intValue = accessoryItemEffect.intValue;
			ImpactType intValue2 = (ImpactType)accessoryItemEffect.intValue2;
			bool boolValue = accessoryItemEffect.boolValue;
			if (accessoryItemEffect.intValue3 == 2)
			{
				List<Character> list = new List<Character>((!(battleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyParty : BattleController.instance.playerParty);
				int num = 0;
				for (int j = 0; j < list.Count; j++)
				{
					if (!list[j].isDead && (list[j].characterDefinition.characterName != "Shadow Fig" || list[j] == character))
					{
						num++;
					}
				}
				num = Mathf.Max(1, num);
				int num2 = Mathf.FloorToInt((float)intValue / (float)num);
				int damage = num2 + intValue % num;
				for (int k = 0; k < list.Count; k++)
				{
					DamageResult damageResult = new DamageResult(num2, intValue2, boolValue);
					if (list[k] == character)
					{
						damageResult.damage = damage;
					}
					damageResult.useRandomPositionOffset = false;
					list[k].DoDamage(damageResult);
				}
			}
			else if (accessoryItemEffect.intValue3 == 1)
			{
				character.DoDamage(new DamageResult(intValue, intValue2, boolValue));
			}
			if (character.isDead && boolValue)
			{
				waitingForTargetDeath = true;
				BattleController.instance.CheckCharacterDeath();
			}
			break;
		}
		case ModChipEffectType.StatusEffect:
			if (!reverse)
			{
				ApplyAccessoryStatusEffect(accessoryItemEffect, currentBattleCharacter, battleCharacter, actionMeter, portraitSwitcher, reverse: false);
			}
			break;
		case ModChipEffectType.DoHeal:
			break;
		case ModChipEffectType.ModifyHealResult:
			break;
		case ModChipEffectType.GoBonus:
			break;
		case ModChipEffectType.HandleDamage:
			break;
		case ModChipEffectType.DropItem:
			ApplyDropItemEffect(accessoryItemEffect, reverse, actionMeter);
			break;
		case ModChipEffectType.DoAccessoryDamage:
			if (!reverse)
			{
				AccessoryBattleButton accessoryBattleButton = null;
				if (accessoryItemEffect.targetType != TargetType.Enemy)
				{
					accessoryBattleButton = ((!isPlayer) ? BattleController.instance.enemyAccessoryButton : BattleController.instance.playerAccessoryButton);
				}
				else
				{
					accessoryBattleButton = ((!isPlayer) ? BattleController.instance.playerAccessoryButton : BattleController.instance.enemyAccessoryButton);
				}
				float healthLost = accessoryItemEffect.intValue;
				accessoryBattleButton.RemoveChargeHealth(healthLost);
			}
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	private void ApplyAccessoryStatBonus(AccessoryItemEffect effect, bool reverse, Character targetCharacter)
	{
		switch (effect.statType)
		{
		case CharacterStatType.Attack:
			targetCharacter.DoBuff(new BuffResult(CharacterStatType.Attack, (!reverse) ? effect.intValue : (-effect.intValue), targetCharacter, targetCharacter, !effect.hasTriggered));
			break;
		case CharacterStatType.HitPoints:
			targetCharacter.DoBuff(new BuffResult(CharacterStatType.HitPoints, (!reverse) ? effect.intValue : (-effect.intValue), targetCharacter, targetCharacter, !effect.hasTriggered));
			break;
		case CharacterStatType.Luck:
			targetCharacter.DoBuff(new BuffResult(CharacterStatType.Luck, (!reverse) ? effect.intValue : (-effect.intValue), targetCharacter, targetCharacter, !effect.hasTriggered));
			break;
		case CharacterStatType.Speed:
			targetCharacter.DoBuff(new BuffResult(CharacterStatType.Speed, (!reverse) ? effect.intValue : (-effect.intValue), targetCharacter, targetCharacter, !effect.hasTriggered));
			break;
		case CharacterStatType.Dodge:
			targetCharacter.DoBuff(new BuffResult(CharacterStatType.Dodge, (!reverse) ? effect.intValue : (-effect.intValue), targetCharacter, targetCharacter, !effect.hasTriggered));
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	private void ApplyAccessoryStatMultiplier(AccessoryItemEffect effect, bool reverse, Character targetCharacter)
	{
		switch (effect.statType)
		{
		case CharacterStatType.Attack:
			targetCharacter.baseAttackMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.HitPoints:
		{
			int maxHitPoints = targetCharacter.maxHitPoints;
			targetCharacter.baseHitPointsMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			if (!targetCharacter.isDead)
			{
				int num = targetCharacter.maxHitPoints - maxHitPoints;
				if (targetCharacter.hitPoints + num <= 0)
				{
					targetCharacter.hitPoints = 1;
				}
				else
				{
					targetCharacter.hitPoints += num;
				}
			}
			break;
		}
		case CharacterStatType.Luck:
			targetCharacter.baseLuckMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.Speed:
			targetCharacter.baseSpeedMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.Dodge:
			targetCharacter.baseDodgeMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	private void ApplyAccessoryStatusEffect(AccessoryItemEffect effect, BattleCharacter casterBattleCharacter, BattleCharacter targetBattleCharacter, ActionMeter targetMeter, PortraitSwitcher targetPortraits, bool reverse)
	{
		Character character = targetBattleCharacter.character;
		StatusEffectData statusEffectData = effect.statusEffectData;
		StatusEffect statusEffect = null;
		switch (effect.statusEffect)
		{
		case StatusEffectName.ActionMeterDisabler:
			if (UnityEngine.Random.value >= targetMeter.character.character.stunResistanceChance)
			{
				statusEffect = new ActionMeterDisabler(targetMeter, targetPortraits, effect.floatValue);
				StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			}
			else
			{
				BattleDisplayManager.instance.ShowMessage(Language.Get("RESIST", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
			}
			break;
		case StatusEffectName.ActionMeterSpeedModifier:
			statusEffect = new ActionMeterSpeedModifier(targetMeter, effect.intValue, effect.floatValue, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.HiveCoin:
			statusEffect = new ActionMeterSpeedModifierAccessory(targetMeter, effect.intValue, effect.intValue2, effect.floatValue, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.AttackDrone:
		{
			Drone objectFromResource = ObjectPool.GetObjectFromResource<Drone>(statusEffectData.stringValue1, casterBattleCharacter.model.position);
			if (objectFromResource != null)
			{
				objectFromResource.transform.localScale = casterBattleCharacter.transform.localScale;
			}
			statusEffect = new AttackDrone(objectFromResource, targetMeter, casterBattleCharacter.character, statusEffectData.floatValue1, statusEffectData.intValue1, statusEffectData.floatValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		}
		case StatusEffectName.BuffDropper:
		{
			Beatbox beatbox = null;
			beatbox = (string.IsNullOrEmpty(statusEffectData.stringValue1) ? ObjectPool.GetObjectFromResource<Beatbox>("Prefabs/FX/BeatBox", targetBattleCharacter.model.position) : ObjectPool.GetObjectFromResource<Beatbox>(statusEffectData.stringValue1, targetBattleCharacter.model.position));
			if (beatbox != null)
			{
				beatbox.transform.localScale = targetBattleCharacter.transform.localScale;
			}
			statusEffect = new BuffDropper(beatbox, targetMeter, statusEffectData.beatBoxType, statusEffectData.intValue1, statusEffectData.floatValue1, statusEffectData.floatValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		}
		case StatusEffectName.AttackMinion:
			statusEffect = new AttackMinion((AttackMinion.MinionType)statusEffectData.intValue2, casterBattleCharacter, targetMeter, statusEffectData.floatValue1, statusEffectData.intValue1, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.Cute:
			statusEffect = new Cute(character, targetMeter, effect.floatValue, this);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, statusEffect);
			break;
		case StatusEffectName.KnockedOut:
			statusEffect = new KnockedOut(character, effect.floatValue, this);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, statusEffect);
			break;
		case StatusEffectName.ProtectionBubble:
			statusEffect = new ProtectionBubble(statusEffectData.stringValue1, targetMeter, statusEffectData.rangedFloatValue1.GetRandomValue(), statusEffectData.floatValue2, statusEffectData.intValue1);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.Smokescreen:
		{
			SmokeBomb objectFromResource2 = ObjectPool.GetObjectFromResource<SmokeBomb>(statusEffectData.stringValue1, targetBattleCharacter.model.position);
			if (objectFromResource2 != null)
			{
				objectFromResource2.InitWithCharacter(targetBattleCharacter);
			}
			statusEffect = new Smokescreen(objectFromResource2, targetMeter, statusEffectData.floatValue1, Mathf.RoundToInt(statusEffectData.floatValue2 * 100f), statusEffectData.intValue1, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		}
		case StatusEffectName.TeamStatBuff:
			statusEffect = new TeamStatBuff(targetMeter, effect.statType, effect.intValue, 0f, removeOnlyOnDurationElapsed: false, -1f, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.Cuteness:
			statusEffect = new Cuteness(targetMeter, effect.floatValue, 0f, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.AbilityBlocker:
			statusEffect = new AbilityBlocker(targetMeter, statusEffectData.floatValue1, AbilityBlocker.VisualType.Waffle, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.Immunity:
		{
			List<Character> list;
			List<Character> list2;
			ActionMeter actionMeter;
			ActionMeter actionMeter2;
			BattleCamera battleCamera;
			if (casterBattleCharacter == BattleController.instance.playerCharacter)
			{
				list = BattleController.instance.playerParty;
				list2 = BattleController.instance.enemyParty;
				actionMeter = BattleController.instance.playerActionMeter;
				actionMeter2 = BattleController.instance.enemyActionMeter;
				battleCamera = BattleController.instance.playerBattleCamera;
			}
			else
			{
				list = BattleController.instance.enemyParty;
				list2 = BattleController.instance.playerParty;
				actionMeter = BattleController.instance.enemyActionMeter;
				actionMeter2 = BattleController.instance.playerActionMeter;
				battleCamera = BattleController.instance.enemyBattleCamera;
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (!list[i].isDead)
				{
					list[i].RemoveAllStatusEffectsByType(StatusEffectType.Debuff, isCleanse: true);
				}
			}
			actionMeter.RemoveAllStatusEffectsByType(StatusEffectType.Debuff, isCleanse: true);
			count = list2.Count;
			for (int j = 0; j < count; j++)
			{
				Character character2 = list2[j];
				if (character2.isDead)
				{
					continue;
				}
				for (int k = 0; k < character2.statusEffects.Count; k++)
				{
					StatusEffect statusEffect2 = character2.statusEffects[k];
					if (statusEffect2.type == StatusEffectType.Buff)
					{
						if (statusEffect2 is Flight)
						{
							((Flight)statusEffect2).CleanseFlight();
						}
						else if (statusEffect2.isCleansable)
						{
							statusEffect2.Remove();
						}
					}
				}
			}
			actionMeter2.RemoveAllStatusEffectsByType(StatusEffectType.Buff, isCleanse: true);
			Camera guiCamera = BattleController.instance.guiCamera;
			Vector3 position = battleCamera.camera.WorldToScreenPoint(casterBattleCharacter.center.position);
			Vector3 destination = guiCamera.ScreenToWorldPoint(position);
			destination.z = -100f;
			ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Tofu_FX", destination);
			Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Text").transform;
			transform.parent = BattleController.instance.guiCamera.transform;
			transform.localPosition = new Vector3(-56f, 110f, 50f);
			statusEffect = new Immunity(targetMeter, statusEffectData.floatValue1, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		}
		case StatusEffectName.ActionMeterFiller:
			statusEffect = new ActionMeterFiller(targetMeter, statusEffectData.floatValue1, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.ActionMeterDepleter:
			statusEffect = new ActionMeterDepleter(targetMeter, statusEffectData.floatValue1, statusEffectData.floatValue2, statusEffectData.intValue1, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.HealAndBuffBlocker:
			statusEffect = new HealBuffBlocker(targetMeter, statusEffectData.floatValue1, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.AmplifyDamage:
			statusEffect = new AmplifyDamageAccessory(targetMeter, statusEffectData.floatValue1, statusEffectData.floatValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.CharacterStatBuff:
			statusEffect = new CharacterStatBuff(character, targetMeter, statusEffectData.statType, statusEffectData.intValue1, statusEffectData.floatValue1, this);
			StatusEffectManager.instance.AddCharacterStatusEffect(character, statusEffect);
			break;
		case StatusEffectName.ActionMeterFreeze:
			statusEffect = new ActionMeterFreeze(targetMeter, statusEffectData.floatValue1, statusEffectData.floatValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.ActionMeterElectrify:
			statusEffect = new ActionMeterElectrify(targetMeter, character, statusEffectData.floatValue1, statusEffectData.rangedFloatValue1.minValue, statusEffectData.floatValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.Root:
			statusEffect = new Root(targetMeter, targetPortraits, statusEffectData.floatValue1, statusEffectData.stringValue1, statusEffectData.stringValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		case StatusEffectName.Counterattack:
			statusEffect = new CounterattackAccessory(targetMeter, statusEffectData.floatValue1, statusEffectData.intValue1, statusEffectData.stringValue1, statusEffectData.stringValue2, this);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, statusEffect);
			break;
		}
		if (statusEffect != null)
		{
			statusEffect.Removed -= HandleAccessoryStatusEffectRemoved;
			if (!statusEffect.isRemoved)
			{
				statusEffect.Removed += HandleAccessoryStatusEffectRemoved;
			}
			else
			{
				HandleAccessoryStatusEffectRemoved(statusEffect);
			}
		}
		if (!string.IsNullOrEmpty(accessoryItemDef.particlePath))
		{
			if (particle != null)
			{
				ObjectPool.PoolObject(particle.gameObject);
				particle = null;
			}
			if (statusEffect != null && !statusEffect.isRemoved)
			{
				particle = ObjectPool.GetObjectFromResource(accessoryItemDef.particlePath).transform;
				particle.localScale = targetMeter.character.transform.localScale;
			}
		}
	}

	private void HandleAccessoryStatusEffectRemoved(StatusEffect statusEffect)
	{
		StatusEffectName statusEffectName = StatusEffectName.None;
		if (statusEffect is ActionMeterDisabler)
		{
			statusEffectName = StatusEffectName.ActionMeterDisabler;
		}
		else if (statusEffect is ActionMeterSpeedModifier)
		{
			statusEffectName = StatusEffectName.ActionMeterSpeedModifier;
		}
		else if (statusEffect is AttackDrone)
		{
			statusEffectName = StatusEffectName.AttackDrone;
		}
		else if (statusEffect is BuffDropper)
		{
			statusEffectName = StatusEffectName.BuffDropper;
		}
		else if (statusEffect is AttackMinion)
		{
			statusEffectName = StatusEffectName.AttackMinion;
		}
		else if (statusEffect is Cute)
		{
			statusEffectName = StatusEffectName.Cute;
		}
		else if (statusEffect is KnockedOut)
		{
			statusEffectName = StatusEffectName.KnockedOut;
		}
		else if (statusEffect is Poison)
		{
			statusEffectName = StatusEffectName.Poison;
		}
		else if (statusEffect is ProtectionBubble)
		{
			statusEffectName = StatusEffectName.ProtectionBubble;
		}
		else if (statusEffect is Smokescreen)
		{
			statusEffectName = StatusEffectName.Smokescreen;
		}
		else if (statusEffect is TeamStatBuff)
		{
			statusEffectName = StatusEffectName.TeamStatBuff;
		}
		else if (statusEffect is Cuteness)
		{
			statusEffectName = StatusEffectName.Cuteness;
		}
		else if (statusEffect is AbilityBlocker)
		{
			statusEffectName = StatusEffectName.AbilityBlocker;
		}
		else if (statusEffect is Immunity)
		{
			statusEffectName = StatusEffectName.Immunity;
		}
		else if (statusEffect is ActionMeterFiller)
		{
			statusEffectName = StatusEffectName.ActionMeterFiller;
		}
		else if (statusEffect is ActionMeterDepleter)
		{
			statusEffectName = StatusEffectName.ActionMeterDepleter;
		}
		else if (statusEffect is HealBuffBlocker)
		{
			statusEffectName = StatusEffectName.HealAndBuffBlocker;
		}
		else if (statusEffect is AmplifyDamageAccessory)
		{
			statusEffectName = StatusEffectName.AmplifyDamage;
		}
		else if (statusEffect is CharacterStatBuff)
		{
			statusEffectName = StatusEffectName.CharacterStatBuff;
		}
		else if (statusEffect is ActionMeterFreeze)
		{
			statusEffectName = StatusEffectName.ActionMeterFreeze;
		}
		else if (statusEffect is ActionMeterElectrify)
		{
			statusEffectName = StatusEffectName.ActionMeterElectrify;
		}
		else if (statusEffect is Root)
		{
			statusEffectName = StatusEffectName.Root;
		}
		else if (statusEffect is CounterattackAccessory)
		{
			statusEffectName = StatusEffectName.Counterattack;
		}
		if (statusEffectName == StatusEffectName.None)
		{
			return;
		}
		int count = accessoryItemDef.effects.Count;
		for (int i = 0; i < count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.effectType == ModChipEffectType.StatusEffect && accessoryItemEffect.statusEffect == statusEffectName)
			{
				accessoryItemEffect.isApplied = false;
			}
		}
		statusEffect.Removed -= HandleAccessoryStatusEffectRemoved;
	}

	private void ApplyAccessoryActionMeterEffect(AccessoryItemEffect effect, bool reverse, ActionMeter targetActionMeter)
	{
		switch (effect.actionMeterEffect)
		{
		case ActionMeterEffect.SpeedBonus:
			if (!reverse)
			{
				targetActionMeter.fillSpeedBonus += effect.intValue;
			}
			else
			{
				targetActionMeter.fillSpeedBonus -= effect.intValue;
			}
			break;
		case ActionMeterEffect.Fill:
		{
			float progress = targetActionMeter.progress;
			targetActionMeter.progress += effect.floatValue;
			float num = targetActionMeter.progress - progress;
			if (num > 0f)
			{
				actionMeter.StartCoroutine(FillEffectFeedback(num, effect.colorValue));
			}
			break;
		}
		case ActionMeterEffect.Speed:
			break;
		case ActionMeterEffect.Block:
			break;
		case ActionMeterEffect.Disable:
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	private IEnumerator FillEffectFeedback(float progressAdded, Color barColor)
	{
		tk2dSlicedSpriteHelper barChunk2 = UnityEngine.Object.Instantiate(actionMeter.fillSprite);
		barChunk2.transform.parent = actionMeter.fillSprite.transform.parent;
		barChunk2.transform.localPosition = actionMeter.fillSprite.transform.localPosition;
		barChunk2.transform.localRotation = actionMeter.fillSprite.transform.localRotation;
		barChunk2.transform.localScale = actionMeter.fillSprite.transform.localScale;
		barChunk2.width = actionMeter.maxWidth * progressAdded;
		Vector3 newPosition2 = actionMeter.fillSprite.transform.localPosition;
		newPosition2.x = actionMeter.fillSprite.width - barChunk2.width;
		newPosition2.z -= 0.1f;
		barChunk2.transform.localPosition = newPosition2;
		barChunk2.slicedSprite.color = barColor;
		Tweener widthTweener = DOTween.To(() => barChunk2.width, (float x) =>
		{
			barChunk2.width = x;
		}, 0f, 0.75f);
		widthTweener.ForceInit();
		yield return null;
		while (widthTweener.IsPlaying())
		{
			newPosition2 = actionMeter.fillSprite.transform.localPosition;
			newPosition2.x = actionMeter.fillSprite.width - barChunk2.width;
			newPosition2.z -= 0.1f;
			barChunk2.transform.localPosition = newPosition2;
			yield return null;
		}
		UnityEngine.Object.Destroy(barChunk2.gameObject);
	}

	private void ApplyDropItemEffect(AccessoryItemEffect effect, bool reverse, ActionMeter targetMeter)
	{
		if (reverse)
		{
			return;
		}
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		if (effect.boolValue)
		{
			dictionary.Add("heart", effect.intValue);
		}
		if (effect.boolValue2)
		{
			dictionary.Add("attack", effect.intValue2);
		}
		if (effect.boolValue3)
		{
			dictionary.Add("tofu", effect.intValue3);
		}
		if (effect.boolValue4)
		{
			dictionary.Add("battery", effect.intValue4);
		}
		int num = 0;
		foreach (KeyValuePair<string, int> item in dictionary)
		{
			num += item.Value;
		}
		int num2 = UnityEngine.Random.Range(0, num);
		bool flag = targetMeter == BattleController.instance.playerActionMeter;
		Camera camera = ((!flag) ? BattleController.instance.enemyBattleCamera.camera : BattleController.instance.playerBattleCamera.camera);
		Vector3 position = camera.WorldToScreenPoint(accessoryBattleItem.itemSpawnPosition.position);
		Vector3 position2 = tk2dCamera.CameraForLayer(LayerMask.NameToLayer("UI")).ScreenCamera.ScreenToWorldPoint(position);
		foreach (KeyValuePair<string, int> item2 in dictionary)
		{
			if (num2 < item2.Value)
			{
				switch (item2.Key)
				{
				case "heart":
				{
					HeartPickup heartPickup = BattleObjectManager.instance.SpawnPickupAt<HeartPickup>("Prefabs/GUI/Pickups/HeartPickup", position2, flag);
					break;
				}
				case "attack":
				{
					FirePickup firePickup = BattleObjectManager.instance.SpawnPickupAt<FirePickup>("Prefabs/GUI/Pickups/FirePickup", position2, flag);
					break;
				}
				case "tofu":
					BattleObjectManager.instance.SpawnTofuAt(position2, flag);
					break;
				case "battery":
					BattleObjectManager.instance.SpawnBatteryAt(position2, flag);
					break;
				}
				break;
			}
			num2 -= item2.Value;
		}
	}

	private void HandleCharacterSwappedOut(Character character)
	{
		if (character != currentCharacter)
		{
			return;
		}
		currentCharacter.DeathCharacter -= HandleCharacterDeath;
		currentCharacter.DamageReceived -= HandleIncomingDamage;
		currentCharacter.IncomingDamage -= HandleIncomingDamageProcessEffects;
		currentCharacter.DamageReceived -= HandleCharacterDamageTaken;
		RemoveEffectsOfTriggerType(AccessoryEffectTriggerType.OnActivation);
		int count = accessoryItemDef.effects.Count;
		for (int i = 0; i < count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			if (accessoryItemEffect.accessoryTriggerType != AccessoryEffectTriggerType.OnActivation && (accessoryItemEffect.effectType == ModChipEffectType.StatBonus || accessoryItemEffect.effectType == ModChipEffectType.StatMultiplier))
			{
				ProcessAccessoryItemEffect(accessoryItemEffect, true);
			}
		}
	}

	private void HandleCurrentCharacterChanged()
	{
		if (currentCharacter == actionMeter.character.character)
		{
			currentBattleCharacter = actionMeter.character;
			return;
		}
		currentBattleCharacter = actionMeter.character;
		currentCharacter = currentBattleCharacter.character;
		currentCharacter.DeathCharacter += HandleCharacterDeath;
		currentCharacter.DamageReceived += HandleIncomingDamage;
		if (accessoryItemDef.effects.Find((AccessoryItemEffect x) => x.accessoryTriggerType == AccessoryEffectTriggerType.OnIncomingCharacterDamage) != null)
		{
			currentCharacter.IncomingDamage += HandleIncomingDamageProcessEffects;
		}
		if (accessoryItemDef.effects.Find((AccessoryItemEffect x) => x.accessoryTriggerType == AccessoryEffectTriggerType.OnCharacterDamageTaken) != null)
		{
			currentCharacter.DamageReceived += HandleCharacterDamageTaken;
		}
		ProcessEffectsOfTriggerType(AccessoryEffectTriggerType.OnActivation);
	}

	private void HandleCharacterSwappedIn(Character character)
	{
	}

	public void DestroyAccessory()
	{
		accessoryBattleButton.StartCoroutine(DestroyAccessorySequence());
	}

	private IEnumerator DestroyAccessorySequence()
	{
		yield return new WaitWhile(() => BattleController.instance.waiting);
		CleanUpAccessory();
		accessoryBattleItem.PlayOutro();
		if (Destroyed != null)
		{
			Destroyed();
		}
	}

	private void CleanUpAccessory()
	{
		int count = accessoryItemDef.effects.Count;
		for (int i = 0; i < count; i++)
		{
			AccessoryItemEffect accessoryItemEffect = accessoryItemDef.effects[i];
			ProcessAccessoryItemEffect(accessoryItemEffect, true);
			accessoryItemEffect.hasTriggered = false;
		}
		BattleController.instance.StartCoroutine(RemoveParticles());
		if (currentCharacter != null)
		{
			currentCharacter.DeathCharacter -= HandleCharacterDeath;
			currentCharacter.DamageReceived -= HandleIncomingDamage;
			currentCharacter.DamageReceived -= HandleCharacterDamageTaken;
			currentCharacter.IncomingDamage -= HandleIncomingDamageProcessEffects;
		}
		BattleController.instance.CharacterSwappedOut -= HandleCharacterSwappedOut;
		BattleController.instance.CharacterSwappedIn -= HandleCharacterSwappedIn;
		actionMeter.CharacterChanged -= HandleCurrentCharacterChanged;
		accessoryBattleItem.animatorEventListener.EventTriggered -= HandleAnimationEvent;
		if (updateCoroutine != null)
		{
			actionMeter.StopCoroutine(updateCoroutine);
		}
		updateCoroutine = null;
		BattleController.instance.BattleEnded -= CleanUpAccessory;
	}

	private IEnumerator RemoveParticles()
	{
		if (particle == null)
		{
			yield break;
		}
		ParticleSystem pSystem = particle.GetComponentInChildren<ParticleSystem>();
		if (!(pSystem == null))
		{
			pSystem.EnableEmission(enabled: false);
			ParticleSystem.MainModule mainModule = pSystem.main;
			DOTween.To(() => mainModule.simulationSpeed, (float x) =>
			{
				mainModule.simulationSpeed = x;
			}, 4f, 0.5f).SetEase(Ease.Linear);
			while (pSystem.particleCount > 0)
			{
				yield return null;
			}
			pSystem.EnableEmission(enabled: true);
			mainModule.simulationSpeed = 1f;
			ObjectPool.PoolObject(particle.gameObject);
			particle = null;
		}
	}

	private void TriggerAnimation(AccessoryAnimationCue animationCue)
	{
		if (animationCue.triggerBattleWait && !BattleController.instance.waiting)
		{
			BattleController.instance.ForceStartWaiting();
		}
		if (animationCue.triggerCameraAnimation)
		{
			BattleCamera battleCamera = ((!isPlayer) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
			battleCamera.animator.runtimeAnimatorController = animationCue.cameraAnimatorController;
			battleCamera.Play(animationCue.cameraAnimation);
			battleCamera.OffsetInComplete += HandleOffsetInComplete;
			modifiedCameraAnimationController = true;
		}
		accessoryBattleItem.PlayAnimationCue(animationCue);
	}

	private void HandleOffsetInComplete()
	{
		BattleCamera battleCamera = ((!isPlayer) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
		battleCamera.OffsetInComplete -= HandleOffsetInComplete;
		battleCamera.offset.DOLocalMove(accessoryBattleItem.transform.localPosition, 0.2f).SetEase(Ease.Linear);
	}

	private void HandleShowingTeam(List<Character> _targetParty, BattleCharacter _target, TargetType targetType)
	{
		int num = 0;
		if (otherBattleCharacters == null)
		{
			otherBattleCharacters = new List<BattleCharacter>();
		}
		otherBattleCharacters.Clear();
		int num2 = _targetParty.IndexOf(_target.character) + 1;
		if (num2 >= _targetParty.Count)
		{
			num2 = 0;
		}
		for (int i = 0; i < _targetParty.Count - 1; i++)
		{
			if (!_targetParty[num2].isDead && _targetParty[num2].characterDefinition.characterName != "Shadow Fig")
			{
				CharacterTransformation activeTransformation = _targetParty[num2].activeTransformation;
				string path;
				if (_targetParty == BattleController.instance.playerParty)
				{
					path = ((activeTransformation != null) ? activeTransformation.battleCharacterPath : _targetParty[num2].characterDefinition.battleCharacterPath);
				}
				else if (activeTransformation != null)
				{
					path = ((!string.IsNullOrEmpty(activeTransformation.battleCharacterPathP2)) ? activeTransformation.battleCharacterPathP2 : activeTransformation.battleCharacterPath);
				}
				else
				{
					path = ((!string.IsNullOrEmpty(_targetParty[num2].characterDefinition.battleCharacterPathP2)) ? _targetParty[num2].characterDefinition.battleCharacterPathP2 : _targetParty[num2].characterDefinition.battleCharacterPath);
				}
				BattleCharacter battleCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(path));
				battleCharacter.transform.localScale = _target.transform.localScale;
				if (isPlayer)
				{
					battleCharacter.transform.localPosition = _target.transform.localPosition + ((num != 0) ? (BattleController.instance.thirdCharacterOffset * -1f) : (BattleController.instance.secondCharacterOffset * -1f));
				}
				else
				{
					battleCharacter.transform.localPosition = _target.transform.localPosition + ((num != 0) ? BattleController.instance.secondCharacterOffset : BattleController.instance.thirdCharacterOffset);
				}
				battleCharacter.character = _targetParty[num2];
				battleCharacter.Play("swap in");
				if (Mathf.Approximately(_target.dancingWeight, 1f))
				{
					battleCharacter.dancingWeight = 1f;
				}
				else if (targetType == TargetType.Enemy)
				{
					battleCharacter.straightLookWeight = 1f;
				}
				otherBattleCharacters.Add(battleCharacter);
				num++;
			}
			num2++;
			if (num2 >= _targetParty.Count)
			{
				num2 = 0;
			}
		}
	}

	private void HandleRemovingTeam()
	{
		int count = otherBattleCharacters.Count;
		for (int i = 0; i < count; i++)
		{
			BattleController.instance.StartCoroutine(CharacterExitSequence(otherBattleCharacters[i]));
		}
	}

	private IEnumerator CharacterExitSequence(BattleCharacter character)
	{
		if (!character.character.isDead)
		{
			character.Play("swap out");
			yield return null;
			yield return null;
			while (character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
		}
		character.character = null;
		ObjectPool.PoolObject(character.gameObject);
	}
}
