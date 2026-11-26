using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CounterattackAccessory : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private int attackDamage;

	private string attackAnimation;

	private string onTickSoundEffect;

	private BattleController battleController;

	private Character target;

	private ActionMeter otherMeter;

	private PortraitSwitcher otherPortaits;

	private float deathCounter;

	private float cutenessAttackModifier = 1f;

	private Coroutine waitRoutine;

	private Coroutine counterRoutine;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public CounterattackAccessory(ActionMeter targetMeter, float duration, int attackDamage, string attackAnimation, string onTickSoundEffect, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.attackDamage = attackDamage;
		this.attackAnimation = attackAnimation;
		this.onTickSoundEffect = onTickSoundEffect;
	}

	private IEnumerator WaitForExpireConfirmation(float prevDeathCounter)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			deathCounter = prevDeathCounter;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		battleController = BattleController.instance;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		targetMeter.AbilityUsed += HandleAbilityUsed;
		HandleCharacterChanged();
		otherMeter = ((!(targetMeter == battleController.playerActionMeter)) ? battleController.playerActionMeter : battleController.enemyActionMeter);
		otherPortaits = ((!(targetMeter == battleController.playerActionMeter)) ? battleController.playerPortraits : battleController.enemyPortraits);
		otherMeter.AbilityUsed += HandleAbilityUsed;
		deathCounter = duration;
		cutenessAttackModifier = 1f;
		waitRoutine = null;
		counterRoutine = null;
	}

	protected override void UpdateEffect()
	{
		if (battleController.waiting || !(duration >= 0f) || !(deathCounter > 0f))
		{
			return;
		}
		float prevDeathCounter = deathCounter;
		deathCounter -= DeltaTime;
		if (!(deathCounter <= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevDeathCounter);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	protected override void ResetEffect()
	{
		deathCounter = duration;
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		targetMeter.CharacterChanged -= HandleCharacterChanged;
		targetMeter.AbilityUsed -= HandleAbilityUsed;
		if (targetMeter.character != null && targetMeter.character.character == target)
		{
			DOTween.To(() => targetMeter.character.cuteWeight, (float x) =>
			{
				targetMeter.character.cuteWeight = x;
			}, 0f, 0.25f);
		}
		target.DamageReceived -= HandleDamageReceived;
		target = null;
		otherMeter.AbilityUsed -= HandleAbilityUsed;
		battleController = null;
	}

	protected override void CleanUp()
	{
	}

	private void HandleCharacterChanged()
	{
		if (target != null)
		{
			target.DamageReceived -= HandleDamageReceived;
		}
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
		if (target != null)
		{
			target.DamageReceived += HandleDamageReceived;
			targetMeter.character.cuteWeight = 1f;
		}
	}

	private void HandleDamageReceived(DamageResult damageResult)
	{
		if (damageResult.isDodged || damageResult.isCounter || damageResult.totalDamage <= 0 || counterRoutine != null)
		{
			return;
		}
		if (damageResult.isCutenessDamage)
		{
			cutenessAttackModifier = Mathf.Clamp01(cutenessAttackModifier * 0.5f);
			if (Mathf.FloorToInt((float)attackDamage * cutenessAttackModifier) <= 0)
			{
				return;
			}
		}
		else
		{
			cutenessAttackModifier = 1f;
		}
		if (battleController.waiting)
		{
			if (waitRoutine == null)
			{
				waitRoutine = battleController.StartCoroutine(WaitToCounter());
			}
		}
		else
		{
			counterRoutine = battleController.StartCoroutine(DoCounterDamage());
		}
	}

	private IEnumerator WaitToCounter()
	{
		yield return new WaitWhile(() => battleController.waiting);
		waitRoutine = null;
		counterRoutine = battleController.StartCoroutine(DoCounterDamage());
	}

	private IEnumerator DoCounterDamage()
	{
		if (otherMeter.character != null && otherMeter.character.character != null)
		{
			while (battleController.waiting)
			{
				yield return null;
			}
			battleController.ForceStartWaiting();
			UnityEngine.Random.State prevState = UnityEngine.Random.state;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				UnityEngine.Random.InitState(TT2NetManager.Instance.LastSeed);
			}
			casterAccessory.accessoryBattleItem.PlayAnimation(attackAnimation, allowBattleWaitingPause: false, forcePlay: true);
			GFAudioManager.instance.PlayClipFromResources(onTickSoundEffect, GFAudioManager.SFX_GROUP, string.Empty);
			bool continueSequence = false;
			Action<AnimationEvent> handleEvent = (AnimationEvent animEvent) =>
			{
				if (animEvent.stringParameter == "hit")
				{
					continueSequence = true;
				}
			};
			casterAccessory.accessoryBattleItem.animatorEventListener.EventTriggered += handleEvent;
			while (!continueSequence)
			{
				yield return null;
			}
			casterAccessory.accessoryBattleItem.animatorEventListener.EventTriggered -= handleEvent;
			DamageResult damageResult = new DamageResult(Mathf.RoundToInt((float)attackDamage * cutenessAttackModifier), ImpactType.Small)
			{
				isCutenessDamage = true
			};
			otherMeter.character.character.DoDamage(damageResult);
			if (otherMeter.character.character.isDead)
			{
				battleController.CheckCharacterDeath();
			}
			else
			{
				battleController.ForceEndWaiting();
			}
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				UnityEngine.Random.state = prevState;
			}
		}
		counterRoutine = null;
	}

	private void HandleAbilityUsed(ActionMeter actionMeter, Character character, AbilityData arg3)
	{
		cutenessAttackModifier = 1f;
	}
}
