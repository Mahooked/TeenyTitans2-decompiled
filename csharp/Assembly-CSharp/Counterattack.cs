using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Counterattack : StatusEffect
{
	private Character character;

	private ActionMeter actionMeter;

	private BattleCamera targetCamera;

	private int counterAbilityUID;

	private float duration;

	private float damageReduction;

	private string attackAnim;

	private float damageReflection;

	private float durationTimer;

	private bool exitingStance;

	private bool queueAttack;

	private int attackDamage;

	private Coroutine damageReceivedRoutine;

	private Shader originalShader;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public override bool isCleansable => false;

	public Counterattack(Character character, ActionMeter actionMeter, BattleCamera targetCamera, int counterAbilityUID, float duration, float damageReflection, float damageReduction, string attackAnim, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.actionMeter = actionMeter;
		this.duration = duration;
		this.targetCamera = targetCamera;
		this.counterAbilityUID = counterAbilityUID;
		this.damageReduction = damageReduction;
		this.attackAnim = attackAnim;
		this.damageReflection = damageReflection;
	}

	public override void Expire()
	{
		exitingStance = true;
		if (durationTimer <= 0f)
		{
			AbilityData abilityData = actionMeter.character.character.abilities.Find((AbilityData x) => x.type == AbilityType.StatusEffect && x.statusEffect == StatusEffectName.Counterattack);
			actionMeter.character.Play(abilityData.animName + " outro");
		}
		Remove();
	}

	protected override void InitEffect()
	{
		durationTimer = duration;
		exitingStance = false;
		attackDamage = 0;
		character.IncomingDamage += HandleIncomingDamage;
		character.DamageReceived += HandleDamageReceived;
		character.StatusEffectAdded += HandleStatusEffectAdded;
		actionMeter.AbilityFinished += HandleAbilityFinished;
		actionMeter.StatusEffectAdded += HandleStatusEffectAdded;
		BattleController.instance.CharacterSwapOutStart += HandleCharacterSwapOutStarted;
		AbilityButton abilityButton = actionMeter.GetAbilityButton(counterAbilityUID);
		if (abilityButton != null)
		{
			abilityButton.canActivate = false;
		}
		originalShader = actionMeter.character.sharedMat.shader;
		Shader shader = Shader.Find("Toon/Basic Tint SF");
		actionMeter.character.sharedMat.shader = shader;
		actionMeter.character.sharedMat.SetFloat("_Fresnel", 0f);
		actionMeter.character.sharedMat.DOFloat(1f, "_Fresnel", 0.3f).SetId("counterFresnel");
	}

	private void HandleIncomingDamage(DamageResult damageResult)
	{
		if (damageResult.damage > 0)
		{
			attackDamage += Mathf.RoundToInt((float)damageResult.totalDamage * damageReflection);
			damageResult.damage = Mathf.Max(1, Mathf.RoundToInt((float)damageResult.damage * (1f - damageReduction)));
			damageResult.isDodged = false;
			if (damageResult.abilityData == null || damageResult.abilityData.type != AbilityType.StatusEffect || damageResult.abilityData.secondaryStatusEffect != StatusEffectName.ActionMeterDisabler || (damageResult.abilityData.type == AbilityType.StatusEffect && damageResult.abilityData.statusEffect != StatusEffectName.ActionMeterDisabler))
			{
				damageResult.impactType = ImpactType.Small;
			}
		}
	}

	private void HandleDamageReceived(DamageResult damageResult)
	{
		if (damageResult.totalDamage <= 0)
		{
			return;
		}
		if (character.isAlive)
		{
			if (damageReceivedRoutine != null)
			{
				actionMeter.StopCoroutine(damageReceivedRoutine);
			}
			damageReceivedRoutine = actionMeter.StartCoroutine(DamageReceivedSequence());
		}
		queueAttack = true;
	}

	private IEnumerator DamageReceivedSequence()
	{
		yield return null;
		actionMeter.character.Play("counter hit");
		damageReceivedRoutine = null;
	}

	private void HandleStatusEffectAdded(StatusEffect statusEffect)
	{
		if (!(statusEffect is ActionMeterDisabler) && !(statusEffect is KnockedOut))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(durationTimer);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	private void HandleAbilityFinished(ActionMeter meter, Character caster, AbilityData abilityData)
	{
		if (caster != character || !(meter == actionMeter) || abilityData.uid == counterAbilityUID)
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(durationTimer);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	private void HandleCharacterSwapOutStarted(Character character)
	{
		if (this.character != character)
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(durationTimer);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	private IEnumerator CounterAttackSequence()
	{
		while (BattleController.instance.waiting)
		{
			yield return null;
		}
		BattleController.instance.ForceStartWaiting();
		UnityEngine.Random.State prevState = UnityEngine.Random.state;
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			UnityEngine.Random.InitState(TT2NetManager.Instance.LastSeed);
		}
		yield return new WaitWhile(() => damageReceivedRoutine != null);
		yield return null;
		while (actionMeter != null && actionMeter.character.animator.IsName("counter hit") && actionMeter.character.animator.IsPlaying())
		{
			yield return null;
		}
		if (actionMeter == null)
		{
			yield break;
		}
		actionMeter.character.Play(attackAnim, isAbilityAnimation: true);
		BattleCamera battleCamera = ((!(actionMeter == BattleController.instance.playerActionMeter)) ? BattleController.instance.enemyBattleCamera : BattleController.instance.playerBattleCamera);
		battleCamera.Play(attackAnim, isAbilityAnimation: true);
		bool continueSequence = false;
		Action<AnimationEvent> handleHitEvent = (AnimationEvent evt) =>
		{
			string[] array = evt.stringParameter.Split(':');
			string text = array[0];
			if (text == "hit")
			{
				continueSequence = true;
			}
		};
		actionMeter.character.animatorEventListener.EventTriggered += handleHitEvent;
		while (!continueSequence)
		{
			yield return null;
		}
		actionMeter.character.animatorEventListener.EventTriggered -= handleHitEvent;
		Character target = ((!(actionMeter == BattleController.instance.playerActionMeter)) ? BattleController.instance.playerCharacter.character : BattleController.instance.enemyCharacter.character);
		DamageResult damageResult = GetDamageResult(character, target, attackDamage);
		target.DoDamage(damageResult);
		while (BattleController.instance.playerBattleCamera.isPlaying || BattleController.instance.enemyBattleCamera.isPlaying)
		{
			yield return null;
		}
		if (target.isDead)
		{
			BattleController.instance.CheckCharacterDeath();
		}
		else
		{
			BattleController.instance.ForceEndWaiting();
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			UnityEngine.Random.state = prevState;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(durationTimer);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	private IEnumerator WaitForExpireConfirmation(float prevDurationTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			durationTimer = prevDurationTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || exitingStance || !(duration >= 0f))
		{
			return;
		}
		if (queueAttack)
		{
			queueAttack = false;
			BattleController.instance.StartCoroutine(CounterAttackSequence());
		}
		else
		{
			if (!(durationTimer > 0f))
			{
				return;
			}
			float prevDurationTimer = durationTimer;
			durationTimer -= DeltaTime;
			if (!(durationTimer <= 0f))
			{
				return;
			}
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
				{
					waitForExpireConfirmation = WaitForExpireConfirmation(prevDurationTimer);
					BattleController.instance.StartCoroutine(waitForExpireConfirmation);
				}
			}
			else
			{
				Expire();
			}
		}
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null && BattleController.instance != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		character.IncomingDamage -= HandleIncomingDamage;
		character.DamageReceived -= HandleDamageReceived;
		character.StatusEffectAdded -= HandleStatusEffectAdded;
		actionMeter.AbilityFinished -= HandleAbilityFinished;
		actionMeter.StatusEffectAdded -= HandleStatusEffectAdded;
		BattleController.instance.CharacterSwapOutStart -= HandleCharacterSwapOutStarted;
		if (damageReceivedRoutine != null)
		{
			actionMeter.StopCoroutine(damageReceivedRoutine);
		}
		AbilityButton abilityButton = actionMeter.GetAbilityButton(counterAbilityUID);
		if (abilityButton != null && !AbilityBlocker.IsButtonBlocked(abilityButton))
		{
			abilityButton.canActivate = true;
		}
		DOTween.Kill("counterFresnel");
		actionMeter.character.sharedMat.DOFloat(0f, "_Fresnel", 0.3f).OnComplete(() =>
		{
			actionMeter.character.sharedMat.shader = originalShader;
		});
	}

	protected override void CleanUp()
	{
		character = null;
		actionMeter = null;
		targetCamera = null;
		damageReceivedRoutine = null;
	}

	protected override void ResetEffect()
	{
		durationTimer = duration;
	}

	protected DamageResult GetDamageResult(Character caster, Character target, int initialDamage, ImpactType impactType = ImpactType.Normal, bool isKillingBlow = true)
	{
		AbilityData abilityData = actionMeter.character.character.abilities.Find((AbilityData x) => x.type == AbilityType.StatusEffect && x.statusEffect == StatusEffectName.Counterattack);
		DamageResult damageResult = new DamageResult(caster, target);
		damageResult.impactType = impactType;
		damageResult.isKillingBlow = isKillingBlow;
		damageResult.abilityData = abilityData;
		damageResult.damage = initialDamage;
		if (UnityEngine.Random.Range(1, 100) <= Mathf.FloorToInt((float)caster.luck * caster.luckModifier) + caster.tempLuckBonus)
		{
			damageResult.luckBonusDamage = Mathf.CeilToInt((float)initialDamage * GameManager.instance.gameSettings.luckyBonusDamage);
		}
		float num = 0f;
		if (isKillingBlow && AbilityScript.classStrengths.ContainsKey(caster.characterDefinition.characterClass) && AbilityScript.classStrengths[caster.characterDefinition.characterClass] == target.characterDefinition.characterClass)
		{
			num = (int)Mathf.Max(1f, (float)initialDamage * GameManager.instance.gameSettings.classBonusDamage);
		}
		damageResult.classBonusDamage = (int)num;
		damageResult.bonusClass = caster.characterDefinition.characterClass;
		if (((!caster.IsAbilityShiny(abilityData) || abilityData.shinyEffect.effectType != AbilityShinyEffectType.ModifyAbilityValues) ? abilityData.isDodgeable : abilityData.shinyEffect.isDodgeable) && !target.isDead && UnityEngine.Random.Range(1, 100) <= Mathf.FloorToInt((float)target.dodge * target.dodgeModifier) + target.tempDodgeBonus)
		{
			damageResult.isDodged = true;
		}
		return damageResult;
	}
}
