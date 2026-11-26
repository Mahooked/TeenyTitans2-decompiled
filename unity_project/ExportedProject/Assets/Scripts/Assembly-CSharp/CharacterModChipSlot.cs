using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModChipSlot
{
	private Dictionary<ModChipEffectTriggerType, bool> triggerCheckList = new Dictionary<ModChipEffectTriggerType, bool>();

	private Coroutine danceTickCoroutine;

	private int counterDamage;

	private Character counterTarget;

	private float danceTickCounter;

	private IEnumerator waitForTickConfirmation;

	private int currencyIncrement;

	private int leftover;

	private BattleController _battleController;

	private ActionMeter _actionMeter;

	private Character _character;

	private ModChipDefinition _modChip;

	public bool isActivated { get; private set; }

	public bool isBattleReady { get; private set; }

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

	public CharacterModChipSlot(Character character)
	{
		_character = character;
	}

	public void Insert(ModChipDefinition modChip)
	{
		if (!(modChip == _modChip))
		{
			if (_modChip != null)
			{
				Remove();
			}
			_modChip = modChip;
			ActivateChip();
		}
	}

	public void Remove()
	{
		if (!(_modChip == null))
		{
			DeactivateChip();
			_modChip = null;
		}
	}

	public void PrepareChipForBattle(BattleController battleController, ActionMeter actionMeter)
	{
		if (!isBattleReady)
		{
			_battleController = battleController;
			_actionMeter = actionMeter;
			isBattleReady = true;
			ActivateChipForBattle();
		}
	}

	public void UnprepareChipForBattle()
	{
		if (isBattleReady)
		{
			DeactivateChipForBattle();
			_battleController = null;
			_actionMeter = null;
			isBattleReady = false;
		}
	}

	public void ActivateChip()
	{
		if (_modChip == null || isActivated)
		{
			return;
		}
		triggerCheckList.Clear();
		ModChipEffect modChipEffect = null;
		int count = _modChip.effects.Count;
		for (int i = 0; i < count; i++)
		{
			modChipEffect = _modChip.effects[i];
			if (modChipEffect.triggerType == ModChipEffectTriggerType.OnEquip)
			{
				ProcessEffect(modChipEffect, false);
			}
		}
		isActivated = true;
		if (isBattleReady)
		{
			ActivateChipForBattle();
		}
	}

	public void DeactivateChip()
	{
		if (_modChip == null || !isActivated)
		{
			return;
		}
		ModChipEffect modChipEffect = null;
		int count = _modChip.effects.Count;
		for (int i = 0; i < count; i++)
		{
			modChipEffect = _modChip.effects[i];
			if (modChipEffect.triggerType == ModChipEffectTriggerType.OnEquip)
			{
				ProcessEffect(modChipEffect, true);
			}
		}
		if (isBattleReady)
		{
			DeactivateChipForBattle();
		}
		isActivated = false;
	}

	private void ActivateChipForBattle()
	{
		if (_modChip == null || !isActivated || !isBattleReady)
		{
			return;
		}
		_battleController.WaitingEnded += HandleWaitingEnded;
		ModChipEffect modChipEffect = null;
		int count = _modChip.effects.Count;
		for (int i = 0; i < count; i++)
		{
			modChipEffect = _modChip.effects[i];
			if (!triggerCheckList.ContainsKey(modChipEffect.triggerType) && modChipEffect.triggerType != ModChipEffectTriggerType.OnEquip)
			{
				switch (modChipEffect.triggerType)
				{
				case ModChipEffectTriggerType.OnActive:
					_battleController.CharacterSwappedIn += HandleOnActive;
					_battleController.CharacterSwappedOut += HandleOnActiveReversed;
					break;
				case ModChipEffectTriggerType.OnFirstActive:
					_battleController.CharacterSwappedIn += HandleOnFirstActive;
					break;
				case ModChipEffectTriggerType.OnDefeatOpponent:
					_character.DamageDealt += HandleOnDefeatOpponent;
					break;
				case ModChipEffectTriggerType.OnDefeated:
					_character.Death += HandleOnDefeated;
					break;
				case ModChipEffectTriggerType.OnIncomingInterrupt:
					_character.IncomingInterrupt += HandleOnIncomingInterrupt;
					break;
				case ModChipEffectTriggerType.OnDamageReceived:
					_character.DamageReceived += HandleOnDamageReceived;
					break;
				case ModChipEffectTriggerType.OnLuckyDuckHit:
					_character.DamageDealt += HandleOnLuckyDuckHit;
					break;
				case ModChipEffectTriggerType.OnDodge:
					_character.Dodged += HandleOnDodge;
					break;
				case ModChipEffectTriggerType.OnOutgoingHeal:
					_character.OutgoingHeal += HandleOnOutgoingHeal;
					break;
				case ModChipEffectTriggerType.OnBattleIntroStarted:
					_battleController.BattleIntroStarted += HandleOnBattleIntroStarted;
					break;
				case ModChipEffectTriggerType.OnDeathComplete:
					_character.DeathComplete += HandleOnDeathComplete;
					break;
				case ModChipEffectTriggerType.OnDanceTick:
					danceTickCounter = 3f;
					danceTickCoroutine = _battleController.StartCoroutine(DanceTick());
					break;
				default:
					continue;
				}
				triggerCheckList.Add(modChipEffect.triggerType, true);
			}
		}
	}

	private void DeactivateChipForBattle()
	{
		if (_modChip == null || !isActivated || !isBattleReady)
		{
			return;
		}
		_battleController.WaitingEnded -= HandleWaitingEnded;
		ModChipEffect modChipEffect = null;
		int count = _modChip.effects.Count;
		for (int i = 0; i < count; i++)
		{
			modChipEffect = _modChip.effects[i];
			if (modChipEffect.triggerType == ModChipEffectTriggerType.OnEquip)
			{
				continue;
			}
			ProcessEffect(modChipEffect, true);
			if (!triggerCheckList.ContainsKey(modChipEffect.triggerType))
			{
				continue;
			}
			switch (modChipEffect.triggerType)
			{
			case ModChipEffectTriggerType.OnActive:
				_battleController.CharacterSwappedIn -= HandleOnActive;
				_battleController.CharacterSwappedOut -= HandleOnActiveReversed;
				break;
			case ModChipEffectTriggerType.OnFirstActive:
				_battleController.CharacterSwappedIn -= HandleOnFirstActive;
				break;
			case ModChipEffectTriggerType.OnDefeatOpponent:
				_character.DamageDealt -= HandleOnDefeatOpponent;
				break;
			case ModChipEffectTriggerType.OnDefeated:
				_character.Death -= HandleOnDefeated;
				break;
			case ModChipEffectTriggerType.OnIncomingInterrupt:
				_character.IncomingInterrupt -= HandleOnIncomingInterrupt;
				break;
			case ModChipEffectTriggerType.OnDamageReceived:
				_character.DamageReceived -= HandleOnDamageReceived;
				break;
			case ModChipEffectTriggerType.OnLuckyDuckHit:
				_character.DamageDealt -= HandleOnLuckyDuckHit;
				break;
			case ModChipEffectTriggerType.OnDodge:
				_character.Dodged -= HandleOnDodge;
				break;
			case ModChipEffectTriggerType.OnOutgoingHeal:
				_character.OutgoingHeal -= HandleOnOutgoingHeal;
				break;
			case ModChipEffectTriggerType.OnBattleIntroStarted:
				_battleController.BattleIntroStarted -= HandleOnBattleIntroStarted;
				break;
			case ModChipEffectTriggerType.OnDeathComplete:
				_character.DeathComplete -= HandleOnDeathComplete;
				break;
			case ModChipEffectTriggerType.OnDanceTick:
				if (danceTickCoroutine != null)
				{
					_battleController.StopCoroutine(danceTickCoroutine);
					danceTickCoroutine = null;
				}
				break;
			default:
				continue;
			}
			triggerCheckList.Remove(modChipEffect.triggerType);
		}
	}

	private void HandleWaitingEnded()
	{
		if (counterDamage > 0)
		{
			if (counterTarget != null && !counterTarget.isDead && !_character.isDead)
			{
				DamageResult damageResult = new DamageResult(counterDamage, ImpactType.Small);
				damageResult.isCounter = true;
				counterTarget.DoDamage(damageResult);
			}
			counterDamage = 0;
			counterTarget = null;
		}
	}

	private void ProcessEffects(ModChipEffectTriggerType triggerType, bool reverse = false, int seed = -1, params object[] args)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network || seed != -1)
		{
			UnityEngine.Random.InitState((seed == -1) ? TT2NetManager.Instance.LastSeed : seed);
		}
		ModChipEffect modChipEffect = null;
		int count = _modChip.effects.Count;
		for (int i = 0; i < count; i++)
		{
			modChipEffect = _modChip.effects[i];
			if (modChipEffect.triggerType == triggerType && modChipEffect.probability > 0f && UnityEngine.Random.value <= modChipEffect.probability)
			{
				ProcessEffect(modChipEffect, reverse, args);
			}
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network || seed != -1)
		{
			UnityEngine.Random.state = state;
		}
	}

	private void ProcessEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		switch (effect.effectType)
		{
		case ModChipEffectType.ActionMeter:
			ProcessActionMeterEffect(effect, reverse, args);
			break;
		case ModChipEffectType.StatBonus:
			ProcessStatBonusEffect(effect, reverse, args);
			break;
		case ModChipEffectType.StatMultiplier:
			ProcessStatMultiplierEffect(effect, reverse, args);
			break;
		case ModChipEffectType.CastTimeMultiplier:
			ProcessCastTimeMultiplierEffect(effect, reverse, args);
			break;
		case ModChipEffectType.DropCoins:
			ProcessDropCoinsEffect(effect, reverse, args);
			break;
		case ModChipEffectType.DropTofu:
			ProcessDropTofuEffect(effect, reverse, args);
			break;
		case ModChipEffectType.CoinDropChance:
			ProcessCoinDropChanceEffect(effect, reverse, args);
			break;
		case ModChipEffectType.TofuDropChance:
			ProcessTofuDropChanceEffect(effect, reverse, args);
			break;
		case ModChipEffectType.CancelInterrupt:
			ProcessCancelInterrupt(effect, reverse, args);
			break;
		case ModChipEffectType.DoDamage:
			ProcessDoDamageEffect(effect, reverse, args);
			break;
		case ModChipEffectType.StatusEffect:
			ProcessStatusEffectEffect(effect, reverse, args);
			break;
		case ModChipEffectType.DoHeal:
			ProcessDoHealEffect(effect, reverse, args);
			break;
		case ModChipEffectType.ModifyHealResult:
			ProcessModifyHealResultEffect(effect, reverse, args);
			break;
		case ModChipEffectType.GoBonus:
			ProcessGoBonusEffect(effect, reverse, args);
			break;
		case ModChipEffectType.AccessoryChargeTimeMultiplier:
			ProcessAccessoryChargeTimeMultiplierEffect(effect, reverse, args);
			break;
		case ModChipEffectType.StunResistanceChance:
			ProcessStunResistanceChanceEffect(effect, reverse, args);
			break;
		case ModChipEffectType.InstantCastChance:
			ProcessInstantCastChanceEffect(effect, reverse, args);
			break;
		case ModChipEffectType.AddAccessoryCharge:
			ProcessAddAccessoryCharge(effect, reverse, args);
			break;
		case ModChipEffectType.ModifySpecificTofuRollChance:
			ProcessModifySpecificTofuRollChance(effect, reverse, args);
			break;
		case ModChipEffectType.HandleDamage:
		case ModChipEffectType.DropItem:
		case ModChipEffectType.DoAccessoryDamage:
			break;
		}
	}

	private void HandleOnDefeated()
	{
		ProcessEffects(ModChipEffectTriggerType.OnDefeated, false, -1);
	}

	private void HandleOnActive(Character character)
	{
		if (character == _character)
		{
			ProcessEffects(ModChipEffectTriggerType.OnActive, false, -1);
		}
	}

	private void HandleOnActiveReversed(Character character)
	{
		if (character == _character)
		{
			ProcessEffects(ModChipEffectTriggerType.OnActive, true, -1);
		}
	}

	private void HandleOnFirstActive(Character character)
	{
		if (character == _character && _battleController.GetSwapInCount(character) == 1)
		{
			ProcessEffects(ModChipEffectTriggerType.OnFirstActive, false, -1);
		}
	}

	private void HandleOnDefeatOpponent(DamageResult damageResult)
	{
		if (damageResult.isKillingBlow && damageResult.target != null && damageResult.target.hitPoints <= 0 && !BattleController.instance.isEnemyPartyDead)
		{
			Debug.Log("On Opponent Defeat - Mod Chip Owner: " + _character.characterDefinition.characterName + ", Defeated Target: " + damageResult.target.characterDefinition.characterName);
			ProcessEffects(ModChipEffectTriggerType.OnDefeatOpponent, false, -1, damageResult.target);
		}
	}

	private void HandleOnIncomingInterrupt(InterruptResult result)
	{
		ProcessEffects(ModChipEffectTriggerType.OnIncomingInterrupt, false, -1, result);
	}

	private void HandleOnDamageReceived(DamageResult result)
	{
		if (!result.isDodged && !result.isCounter && result.isKillingBlow)
		{
			ProcessEffects(ModChipEffectTriggerType.OnDamageReceived, false, -1);
		}
	}

	private void HandleOnLuckyDuckHit(DamageResult result)
	{
		if (!result.isDodged && result.luckBonusDamage > 0)
		{
			ProcessEffects(ModChipEffectTriggerType.OnLuckyDuckHit, false, -1);
		}
	}

	private void HandleOnDodge(DamageResult damageResult)
	{
		ProcessEffects(ModChipEffectTriggerType.OnDodge, false, -1);
	}

	private void HandleOnOutgoingHeal(HealResult result)
	{
		ProcessEffects(ModChipEffectTriggerType.OnOutgoingHeal, false, -1, result);
	}

	private void HandleOnBattleIntroStarted()
	{
		ProcessEffects(ModChipEffectTriggerType.OnBattleIntroStarted, false, -1);
	}

	private IEnumerator DanceTick()
	{
		while (true)
		{
			yield return null;
			if (!(_actionMeter.character != null) || _character != _actionMeter.character.character || _battleController.waiting)
			{
				continue;
			}
			float prevTickCounter = danceTickCounter;
			danceTickCounter -= DeltaTime;
			if (!(danceTickCounter <= 0f))
			{
				continue;
			}
			danceTickCounter += 3f;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				if (_battleController.playerParty.Contains(_character) && waitForTickConfirmation == null)
				{
					waitForTickConfirmation = WaitForTickConfirmation(prevTickCounter, ModChipEffectTriggerType.OnDanceTick);
					BattleController.instance.StartCoroutine(waitForTickConfirmation);
				}
			}
			else
			{
				DoTriggerTick(ModChipEffectTriggerType.OnDanceTick);
			}
		}
	}

	public void DoTriggerTick(ModChipEffectTriggerType triggerType, int seed = -1)
	{
		bool flag = false;
		if (triggerType == ModChipEffectTriggerType.OnDanceTick)
		{
			ActionMeterSpeedModifier statusEffect = _actionMeter.GetStatusEffect<ActionMeterSpeedModifier>();
			if (statusEffect != null && statusEffect.type == StatusEffectType.Buff)
			{
				flag = true;
			}
		}
		if (flag)
		{
			ProcessEffects(triggerType, false, seed);
		}
	}

	private IEnumerator WaitForTickConfirmation(float prevTickCounter, ModChipEffectTriggerType triggerType)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendModChipTriggerTick(_character.uid, (int)triggerType, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForTickConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			danceTickCounter = prevTickCounter;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		DoTriggerTick(triggerType, playerAction.seed);
		UnityEngine.Random.state = prevRandomState;
	}

	private void HandleOnDeathComplete()
	{
		ProcessEffects(ModChipEffectTriggerType.OnDeathComplete, false, -1);
	}

	private void ProcessActionMeterEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (!isBattleReady)
		{
			return;
		}
		ActionMeterSpeedModifier actionMeterSpeedModifier = null;
		ActionMeterEffect actionMeterEffect = effect.actionMeterEffect;
		if (actionMeterEffect == ActionMeterEffect.SpeedBonus)
		{
			if (!reverse)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(_actionMeter, new ActionMeterSpeedModifier(_actionMeter, effect.intValue, effect.floatValue));
			}
			else
			{
				_actionMeter.RemoveStatusEffect<ActionMeterSpeedModifier>();
			}
		}
	}

	private void ProcessStatBonusEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		switch (effect.statType)
		{
		case CharacterStatType.Attack:
			_character.attackBonus += ((!reverse) ? effect.intValue : (-effect.intValue));
			break;
		case CharacterStatType.HitPoints:
			_character.hitPointsBonus += ((!reverse) ? effect.intValue : (-effect.intValue));
			break;
		case CharacterStatType.Luck:
			_character.luckBonus += ((!reverse) ? effect.intValue : (-effect.intValue));
			break;
		case CharacterStatType.Speed:
			_character.speedBonus += ((!reverse) ? effect.intValue : (-effect.intValue));
			break;
		case CharacterStatType.Dodge:
			_character.dodgeBonus += ((!reverse) ? effect.intValue : (-effect.intValue));
			break;
		}
	}

	private void ProcessStatMultiplierEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		switch (effect.statType)
		{
		case CharacterStatType.Attack:
			_character.baseAttackMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.HitPoints:
			_character.baseHitPointsMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.Luck:
			_character.baseLuckMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.Speed:
			_character.baseSpeedMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		case CharacterStatType.Dodge:
			_character.baseDodgeMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
			break;
		}
	}

	private void ProcessCastTimeMultiplierEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		_character.castTimeModifier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
		Debug.Log("Adjust Cast Time Modifier by " + ((!reverse) ? effect.floatValue : (0f - effect.floatValue)));
	}

	private void ProcessDropCoinsEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (!reverse)
		{
			_battleController.StartCoroutine(PlayCoinsplosion(effect.intValue, (Character)args[0]));
		}
	}

	private IEnumerator PlayCoinsplosion(int count, Character character)
	{
		bool targetIsPlayer = character == _battleController.playerCharacter.character;
		BattleCharacter battleCharacter = ((!targetIsPlayer) ? _battleController.enemyCharacter : _battleController.playerCharacter);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_coinsadd", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.1f);
		ParticleSystem pSystem = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/GUI/VictoryCoin_Particle System", default(Vector3), false);
		currencyIncrement = Mathf.Max(1, Mathf.FloorToInt(count / pSystem.maxParticles));
		leftover = ((count > pSystem.maxParticles) ? (count % pSystem.maxParticles) : 0);
		pSystem.emission.SetBursts(new ParticleSystem.Burst[1]
		{
			new ParticleSystem.Burst(0f, (short)count)
		});
		Vector3 newPosition = BattleDisplayManager.instance.GetPositionInGUICamera(battleCharacter, true);
		newPosition.z = -300f;
		pSystem.transform.position = newPosition;
		pSystem.gameObject.SetActive(true);
		if (!targetIsPlayer)
		{
			WalletGUI walletGUI = _battleController.walletGUI;
			Debug.Log("Create Coin Explosion going to player");
			walletGUI.particleAttractor.ParticleDestroyed += HandleOnParticleDestroyed;
			walletGUI.particleAttractor.AttractParticleSystem(pSystem);
			while (walletGUI.particleAttractor.numParticleSystems > 0)
			{
				yield return null;
			}
			walletGUI.particleAttractor.ParticleDestroyed -= HandleOnParticleDestroyed;
			ObjectPool.PoolObject(pSystem.gameObject);
			yield break;
		}
		Debug.Log("Create Coin Explosion going to enemy");
		PortraitSwitcher portraitSwitcher = _battleController.enemyPortraits;
		Transform portraitParent = portraitSwitcher.portraits[0].transform.parent;
		ParticleAttractor particleAttractor = portraitParent.gameObject.AddComponent<ParticleAttractor>();
		particleAttractor.startSpeed = 10f;
		particleAttractor.acceleration = 1.05f;
		particleAttractor.delay = 0.1f;
		ParticleSystemRenderer pSysRenderer = pSystem.GetComponent<ParticleSystemRenderer>();
		Material originalMat = pSysRenderer.sharedMaterial;
		pSysRenderer.material.color = new Color(1f, 1f, 1f, 0.35f);
		yield return null;
		particleAttractor.AttractParticleSystem(pSystem);
		yield return new WaitUntil(() => particleAttractor.numParticleSystems <= 0);
		pSysRenderer.material = originalMat;
		UnityEngine.Object.Destroy(particleAttractor);
		ObjectPool.PoolObject(pSystem.gameObject);
	}

	private void HandleOnParticleDestroyed()
	{
		int num = currencyIncrement;
		if (leftover > 0)
		{
			num++;
			leftover--;
		}
		_battleController.walletGUI.AddToWallet(num);
	}

	private void ProcessDropTofuEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (reverse)
		{
		}
	}

	private void ProcessCoinDropChanceEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (isBattleReady)
		{
			float num = (float)effect.intValue * 0.01f;
			BattleObjectManager.instance.spawnChanceCoinBonus += ((!reverse) ? num : (0f - num));
		}
	}

	private void ProcessTofuDropChanceEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (isBattleReady)
		{
			float num = (float)effect.intValue * 0.01f;
			BattleObjectManager.instance.spawnChanceTofuBonus += ((!reverse) ? num : (0f - num));
		}
	}

	private void ProcessCancelInterrupt(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (isBattleReady && !reverse)
		{
			InterruptResult interruptResult = (InterruptResult)args[0];
			interruptResult.isInterrupted = false;
		}
	}

	private void ProcessDoDamageEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (!isBattleReady || reverse)
		{
			return;
		}
		if (counterTarget == null)
		{
			if (effect.targetType == TargetType.Enemy)
			{
				counterTarget = ((!_battleController.playerParty.Contains(_character)) ? _battleController.playerCharacter.character : _battleController.enemyCharacter.character);
			}
			else if (effect.targetType == TargetType.Friendly)
			{
				counterTarget = ((!_battleController.playerParty.Contains(_character)) ? _battleController.enemyCharacter.character : _battleController.playerCharacter.character);
			}
		}
		counterDamage += effect.intValue;
		if (!_battleController.waiting)
		{
			HandleWaitingEnded();
		}
	}

	private void ProcessStatusEffectEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (!isBattleReady)
		{
			return;
		}
		BattleCharacter battleCharacter = null;
		BattleCharacter battleCharacter2 = null;
		Character character = null;
		ActionMeter actionMeter = null;
		PortraitSwitcher portraits = null;
		battleCharacter = ((_character != _battleController.playerCharacter.character) ? _battleController.enemyCharacter : _battleController.playerCharacter);
		if (effect.targetType == TargetType.Enemy)
		{
			battleCharacter2 = ((_character != _battleController.playerCharacter.character) ? _battleController.playerCharacter : _battleController.enemyCharacter);
			character = battleCharacter2.character;
			actionMeter = ((_character != _battleController.playerCharacter.character) ? _battleController.playerActionMeter : _battleController.enemyActionMeter);
			portraits = ((_character != _battleController.playerCharacter.character) ? _battleController.playerPortraits : _battleController.enemyPortraits);
		}
		else if (effect.targetType == TargetType.Friendly)
		{
			battleCharacter2 = ((_character != _battleController.playerCharacter.character) ? _battleController.enemyCharacter : _battleController.playerCharacter);
			character = battleCharacter2.character;
			actionMeter = ((_character != _battleController.playerCharacter.character) ? _battleController.enemyActionMeter : _battleController.playerActionMeter);
			portraits = ((_character != _battleController.playerCharacter.character) ? _battleController.enemyPortraits : _battleController.playerPortraits);
		}
		StatusEffectData statusEffectData = effect.statusEffectData;
		switch (effect.statusEffect)
		{
		case StatusEffectName.ActionMeterDisabler:
			if (!reverse)
			{
				if (UnityEngine.Random.value >= actionMeter.character.character.stunResistanceChance)
				{
					StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new ActionMeterDisabler(actionMeter, portraits, effect.floatValue));
				}
				else
				{
					BattleDisplayManager.instance.ShowMessage(Language.Get("RESIST", "ABILS,CHIPS,ACHVS,LDBS"), actionMeter.character);
				}
			}
			break;
		case StatusEffectName.ActionMeterSpeedModifier:
			if (!reverse)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new ActionMeterSpeedModifier(actionMeter, effect.intValue, effect.floatValue));
			}
			break;
		case StatusEffectName.AttackDrone:
			if (!reverse)
			{
				Drone objectFromResource2 = ObjectPool.GetObjectFromResource<Drone>(statusEffectData.stringValue1, battleCharacter.model.position);
				if (objectFromResource2 != null)
				{
					objectFromResource2.transform.localScale = battleCharacter.transform.localScale;
				}
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new AttackDrone(objectFromResource2, actionMeter, _character, statusEffectData.floatValue1, statusEffectData.intValue1, statusEffectData.floatValue2));
			}
			break;
		case StatusEffectName.BuffDropper:
			if (!reverse)
			{
				Beatbox beatbox = null;
				beatbox = (string.IsNullOrEmpty(statusEffectData.stringValue1) ? ObjectPool.GetObjectFromResource<Beatbox>("Prefabs/FX/BeatBox", battleCharacter2.model.position) : ObjectPool.GetObjectFromResource<Beatbox>(statusEffectData.stringValue1, battleCharacter2.model.position));
				if (beatbox != null)
				{
					beatbox.transform.localScale = battleCharacter2.transform.localScale;
				}
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new BuffDropper(beatbox, actionMeter, statusEffectData.beatBoxType, statusEffectData.intValue1, statusEffectData.floatValue1, statusEffectData.floatValue2));
			}
			break;
		case StatusEffectName.Cute:
			if (!reverse)
			{
				StatusEffectManager.instance.AddCharacterStatusEffect(character, new Cute(character, actionMeter, effect.floatValue));
			}
			break;
		case StatusEffectName.KnockedOut:
			if (!reverse)
			{
				StatusEffectManager.instance.AddCharacterStatusEffect(character, new KnockedOut(character, effect.floatValue));
			}
			break;
		case StatusEffectName.Poison:
			break;
		case StatusEffectName.ProtectionBubble:
			break;
		case StatusEffectName.Smokescreen:
			if (!reverse)
			{
				SmokeBomb objectFromResource = ObjectPool.GetObjectFromResource<SmokeBomb>(statusEffectData.stringValue1, battleCharacter2.model.position);
				if (objectFromResource != null)
				{
					objectFromResource.InitWithCharacter(battleCharacter2);
				}
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new Smokescreen(objectFromResource, actionMeter, statusEffectData.floatValue1, Mathf.RoundToInt(statusEffectData.floatValue2 * 100f), statusEffectData.intValue1));
			}
			break;
		case StatusEffectName.TeamStatBuff:
			if (!reverse)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new TeamStatBuff(actionMeter, effect.statType, effect.intValue));
			}
			break;
		case StatusEffectName.Cuteness:
			if (!reverse)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new Cuteness(actionMeter, statusEffectData.floatValue1));
			}
			break;
		case StatusEffectName.AbilityBlocker:
			if (!reverse)
			{
				Debug.Log(actionMeter.tag);
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new AbilityBlocker(actionMeter, statusEffectData.floatValue1));
			}
			break;
		case StatusEffectName.HealOverTime:
			if (!reverse)
			{
				StatusEffectManager.instance.AddCharacterStatusEffect(character, new HealOverTime(character, actionMeter, statusEffectData.intValue1, statusEffectData.floatValue2, statusEffectData.floatValue1));
			}
			break;
		case StatusEffectName.AttackMinion:
			if (!reverse)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(_actionMeter, new AttackMinion((AttackMinion.MinionType)statusEffectData.intValue2, battleCharacter, _actionMeter, statusEffectData.floatValue1, statusEffectData.intValue1));
			}
			break;
		case StatusEffectName.Immunity:
		case StatusEffectName.ActionMeterFiller:
		case StatusEffectName.ActionMeterDepleter:
		case StatusEffectName.HealAndBuffBlocker:
		case StatusEffectName.AmplifyDamage:
		case StatusEffectName.CharacterStatBuff:
		case StatusEffectName.ActionMeterFreeze:
		case StatusEffectName.Flight:
		case StatusEffectName.ActionMeterElectrify:
		case StatusEffectName.Root:
		case StatusEffectName.Counterattack:
		case StatusEffectName.RemoteBomb:
			break;
		}
	}

	private void ProcessDoHealEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (isBattleReady && !reverse)
		{
			Character character = null;
			if (effect.targetType == TargetType.Enemy)
			{
				character = ((_character != _battleController.playerCharacter.character) ? _character : _battleController.enemyCharacter.character);
			}
			else if (effect.targetType == TargetType.Friendly)
			{
				character = ((_character != _battleController.playerCharacter.character) ? _battleController.enemyCharacter.character : _character);
			}
			HealResult healResult = new HealResult(null, character, effect.intValue);
			character.DoHeal(healResult);
		}
	}

	private void ProcessModifyHealResultEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (isBattleReady && !reverse)
		{
			HealResult healResult = (HealResult)args[0];
			healResult.amount = Mathf.CeilToInt((float)(healResult.amount + effect.intValue) * effect.floatValue);
		}
	}

	private void ProcessGoBonusEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		if (isBattleReady)
		{
			_battleController.goTapBonusMultiplier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
		}
	}

	private void ProcessAccessoryChargeTimeMultiplierEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		_character.accessoryChargeTimeModifier += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
		Debug.Log("Adjust Accessory Character Time Modifier by " + ((!reverse) ? effect.floatValue : (0f - effect.floatValue)));
	}

	private void ProcessStunResistanceChanceEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		_character.stunResistanceChance += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
		Debug.Log("Adjust Stun Time Modifier by " + ((!reverse) ? effect.floatValue : (0f - effect.floatValue)));
	}

	private void ProcessInstantCastChanceEffect(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		_character.instantCastChance += ((!reverse) ? effect.floatValue : (0f - effect.floatValue));
		Debug.Log("Adjust Instant Cast Chance by " + ((!reverse) ? effect.floatValue : (0f - effect.floatValue)));
	}

	private void ProcessAddAccessoryCharge(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		Debug.Log("Process Add Accessory Charge");
		AccessoryBattleButton accessoryBattleButton = ((!(_actionMeter == _battleController.playerActionMeter)) ? _battleController.enemyAccessoryButton : _battleController.playerAccessoryButton);
		float num = ((!reverse) ? effect.floatValue : (0f - effect.floatValue)) * 100f;
		if (num > 0f)
		{
			accessoryBattleButton.AddChargeHealthPercentage(num, true, true);
		}
		else
		{
			accessoryBattleButton.RemoveChargeHealthPercentage(0f - num);
		}
	}

	private void ProcessModifySpecificTofuRollChance(ModChipEffect effect, bool reverse = false, params object[] args)
	{
		TofuBubble.tofuDropWeightOffsets[(TofuType)effect.intValue] += ((!reverse) ? effect.intValue2 : (-effect.intValue2));
		Debug.Log(string.Concat("Post Drop Offset: ", (TofuType)effect.intValue, ", value: ", TofuBubble.tofuDropWeightOffsets[(TofuType)effect.intValue]));
	}
}
