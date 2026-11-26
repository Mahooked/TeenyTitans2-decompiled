using System;
using System.Collections;
using UnityEngine;

public class Flight : StatusEffect
{
	private Character character;

	private ActionMeter actionMeter;

	private BattleCamera targetCamera;

	private int flightAbilityUID;

	private float duration;

	private float durationTimer;

	private bool exitingFlight;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type
	{
		get
		{
			return StatusEffectType.Buff;
		}
	}

	public override bool isStackable
	{
		get
		{
			return false;
		}
	}

	public override bool isResettable
	{
		get
		{
			return true;
		}
	}

	public override bool isZoomedOut
	{
		get
		{
			return false;
		}
	}

	public bool allowFlightCameraOffset { get; private set; }

	public Flight(Character character, ActionMeter actionMeter, BattleCamera targetCamera, int flightAbilityUID, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.actionMeter = actionMeter;
		this.duration = duration;
		this.targetCamera = targetCamera;
		this.flightAbilityUID = flightAbilityUID;
	}

	public override void Expire()
	{
		exitingFlight = true;
		actionMeter.StartCoroutine(EndFlight());
	}

	private IEnumerator WaitForExpireConfirmation(float prevDurationTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(base.uid, Environment.TickCount);
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

	protected override void InitEffect()
	{
		Debug.Log("Flight Init effect");
		durationTimer = duration;
		exitingFlight = false;
		allowFlightCameraOffset = true;
		character.DamageReceived += HandleDamageReceived;
		character.IncomingDamage += HandleIncomingDamage;
		character.StatusEffectAdded += HandleStatusEffectAdded;
		actionMeter.character.AbilityAnimationPlayed += HandleAbilityAnimationPlayed;
		actionMeter.StatusEffectAdded += HandleStatusEffectAdded;
		actionMeter.AbilityUsed += HandleAbilityUsed;
		actionMeter.AbilityFinished += HandleAbilityFinished;
		targetCamera.AbilityAnimationPlayed += HandleCameraAbilityAnimationPlayed;
		AbilityButton abilityButton = actionMeter.GetAbilityButton(flightAbilityUID);
		if (abilityButton != null)
		{
			abilityButton.canActivate = false;
		}
	}

	private void HandleDamageReceived(DamageResult damageResult)
	{
		if (!damageResult.isDodged && damageResult.damage > 0)
		{
			if (damageResult.abilityData != null)
			{
				Remove();
			}
			else
			{
				ForceEndFlightSquence(true);
			}
		}
	}

	private void HandleIncomingDamage(DamageResult damageResult)
	{
		if ((damageResult.abilityData == null || damageResult.abilityData.targetCount != TargetCount.All) && damageResult.source != character && !damageResult.isCutenessDamage)
		{
			damageResult.isDodged = true;
			damageResult.useMissedMessage = true;
		}
	}

	private void HandleStatusEffectAdded(StatusEffect statusEffect)
	{
		if (statusEffect.type == StatusEffectType.Debuff && !(statusEffect is ActionMeterSpeedModifier) && !(statusEffect is AttackDrone) && !(statusEffect is Poison) && !(statusEffect is AbilityBlocker) && !(statusEffect is RemoteBomb))
		{
			statusEffect.Remove();
			BattleDisplayManager.instance.ShowMessage("MISS!", actionMeter.character);
		}
	}

	private void HandleAbilityAnimationPlayed(string animName)
	{
		string text = "flight " + animName;
		if (actionMeter.character.animator.HasState(0, Animator.StringToHash("Base Layer." + text)))
		{
			actionMeter.character.Play(text);
		}
	}

	private void HandleCameraAbilityAnimationPlayed(string animName)
	{
		string text = "flight " + animName;
		if (targetCamera.animator.HasState(0, Animator.StringToHash("Base Layer." + text)))
		{
			targetCamera.Play(text);
		}
	}

	private void HandleAbilityUsed(ActionMeter meter, Character caster, AbilityData usedAbilityData)
	{
		if (caster == character && meter == actionMeter && usedAbilityData.uid != flightAbilityUID && usedAbilityData.channelingData.speed <= 0f)
		{
			allowFlightCameraOffset = false;
		}
	}

	private void HandleAbilityFinished(ActionMeter meter, Character caster, AbilityData abilityData)
	{
		Debug.Log("Handle Ability Finished while in Flight, potentially removing it, " + abilityData.abilityName);
		if (caster == character && meter == actionMeter && abilityData.uid != flightAbilityUID && abilityData.channelingData.speed <= 0f)
		{
			Debug.Log("\t Handle Ability Finished while in Flight, WILL REMOVE FLIGHT EFFECT!");
			Remove();
		}
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		Debug.Log("Remove Flight Effect");
		character.DamageReceived -= HandleDamageReceived;
		character.IncomingDamage -= HandleIncomingDamage;
		character.StatusEffectAdded -= HandleStatusEffectAdded;
		actionMeter.character.AbilityAnimationPlayed -= HandleAbilityAnimationPlayed;
		actionMeter.StatusEffectAdded -= HandleStatusEffectAdded;
		actionMeter.AbilityUsed -= HandleAbilityUsed;
		actionMeter.AbilityFinished -= HandleAbilityFinished;
		targetCamera.AbilityAnimationPlayed -= HandleCameraAbilityAnimationPlayed;
		AbilityButton abilityButton = actionMeter.GetAbilityButton(flightAbilityUID);
		if (abilityButton != null && !AbilityBlocker.IsButtonBlocked(abilityButton))
		{
			abilityButton.canActivate = true;
		}
		allowFlightCameraOffset = false;
	}

	protected override void ResetEffect()
	{
		durationTimer = duration;
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || exitingFlight || !(durationTimer > 0f))
		{
			return;
		}
		float prevDurationTimer = durationTimer;
		durationTimer -= base.DeltaTime;
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

	private IEnumerator EndFlight(bool updateWaitingState = true)
	{
		if (updateWaitingState)
		{
			BattleController.instance.ForceStartWaiting();
		}
		allowFlightCameraOffset = false;
		yield return null;
		string outroAnim = string.Empty;
		if (actionMeter.isChanneling)
		{
			string name = actionMeter.character.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
			outroAnim = name.Replace("loop", "exit");
			actionMeter.character.Play(outroAnim);
			targetCamera.Play(outroAnim);
		}
		else
		{
			outroAnim = "flight outro";
			actionMeter.character.Play(outroAnim);
			targetCamera.Play(outroAnim);
		}
		yield return null;
		yield return new WaitWhile(() => actionMeter.character.animator.IsName(outroAnim));
		if (updateWaitingState)
		{
			BattleController.instance.ForceEndWaiting();
		}
		Remove();
	}

	public void ForceEndFlightSquence(bool allowChangingBattleWaitingState = false)
	{
		exitingFlight = true;
		actionMeter.StartCoroutine(EndFlight(allowChangingBattleWaitingState));
	}

	public void CleanseFlight()
	{
		durationTimer = 0f;
	}

	protected override void CleanUp()
	{
		character.DamageReceived -= HandleDamageReceived;
		character.IncomingDamage -= HandleIncomingDamage;
		character.StatusEffectAdded -= HandleStatusEffectAdded;
		actionMeter.character.AbilityAnimationPlayed -= HandleAbilityAnimationPlayed;
		actionMeter.StatusEffectAdded -= HandleStatusEffectAdded;
		actionMeter.AbilityUsed -= HandleAbilityUsed;
		actionMeter.AbilityFinished -= HandleAbilityFinished;
		targetCamera.AbilityAnimationPlayed -= HandleCameraAbilityAnimationPlayed;
		character = null;
		actionMeter = null;
		targetCamera = null;
	}
}
