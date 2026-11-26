using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMeterHack : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private RangedFloat damageModifierRange;

	private float abilitySwapDelay;

	private float abilityBlockDelay;

	private float abilityBlockDuration;

	private Character target;

	private int curAttackBonus;

	private float durationTimer;

	private bool ascend;

	private const float speed = 1.2f;

	private float abilityBlockTimer;

	private AbilityBlocker curBlockerEffect;

	private List<ParticleSystem> particleSystems;

	private bool targetIsPlayer;

	private IEnumerator waitForTickConfirmation;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type
	{
		get
		{
			return StatusEffectType.Debuff;
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

	public ActionMeterHack(ActionMeter targetMeter, float duration, RangedFloat damageModifierRange, float abilitySwapDelay, float abilityBlockDelay, float abilityBlockDuration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.damageModifierRange = damageModifierRange;
		this.abilitySwapDelay = abilitySwapDelay;
		this.abilityBlockDelay = abilityBlockDelay;
		this.abilityBlockDuration = abilityBlockDuration;
	}

	public override void Tick()
	{
		curBlockerEffect = new AbilityBlocker(targetMeter, abilityBlockDuration, AbilityBlocker.VisualType.Hack);
		StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, curBlockerEffect);
	}

	private IEnumerator WaitForTickConfirmation(float prevAbilityBlockTimer, float prevDurationTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectTick(base.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForTickConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			abilityBlockTimer = prevAbilityBlockTimer;
			durationTimer = prevDurationTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Tick();
		UnityEngine.Random.state = prevRandomState;
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
		particleSystems = new List<ParticleSystem>();
		durationTimer = duration;
		abilityBlockTimer = 0f;
		targetIsPlayer = targetMeter == BattleController.instance.playerActionMeter;
		float num = (targetIsPlayer ? 1f : 0.75f);
		for (int i = 0; i < targetMeter.abilityButtons.Count; i++)
		{
			AbilityButton abilityButton = targetMeter.abilityButtons[i];
			abilityButton.ResetSprites();
			abilityButton.showDamageNumber = true;
			ParticleSystem objectFromResource = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/FX/RiddlerHacked_ParticleSystem");
			objectFromResource.transform.SetParent(abilityButton.iconSprite.transform);
			objectFromResource.transform.localPosition = Vector3.back * 6f;
			objectFromResource.transform.localScale = Vector3.one * num;
			particleSystems.Add(objectFromResource);
		}
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
	}

	private void HandleCharacterChanged()
	{
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting)
		{
			return;
		}
		if (target != null)
		{
			if (ascend)
			{
				target.finalDamageMultiplier = Mathf.Min(target.finalDamageMultiplier + 1.2f * base.DeltaTime, damageModifierRange.maxValue);
				if (target.finalDamageMultiplier >= damageModifierRange.maxValue)
				{
					ascend = false;
				}
			}
			else
			{
				target.finalDamageMultiplier = Mathf.Max(target.finalDamageMultiplier - 1.2f * base.DeltaTime, damageModifierRange.minValue);
				if (target.finalDamageMultiplier <= damageModifierRange.minValue)
				{
					ascend = true;
				}
			}
		}
		if (!(durationTimer > 0f))
		{
			return;
		}
		float prevAbilityBlockTimer = abilityBlockTimer;
		float prevDurationTimer = durationTimer;
		float deltaTime = base.DeltaTime;
		abilityBlockTimer -= deltaTime;
		if (abilityBlockTimer <= 0f)
		{
			abilityBlockTimer += abilityBlockDelay;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForTickConfirmation == null)
				{
					waitForTickConfirmation = WaitForTickConfirmation(prevAbilityBlockTimer, prevDurationTimer);
					BattleController.instance.StartCoroutine(waitForTickConfirmation);
				}
			}
			else
			{
				Tick();
			}
		}
		durationTimer -= deltaTime;
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

	protected override void RemoveEffect()
	{
		if (waitForTickConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForTickConfirmation);
			waitForTickConfirmation = null;
		}
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		if (target != null)
		{
			target.finalDamageMultiplier = 1f;
		}
		if (curBlockerEffect != null && !curBlockerEffect.isRemoved)
		{
			curBlockerEffect.Remove();
		}
		BattleController.instance.StartCoroutine(RemoveParticles());
		for (int i = 0; i < targetMeter.abilityButtons.Count; i++)
		{
			AbilityButton abilityButton = targetMeter.abilityButtons[i];
			abilityButton.showDamageNumber = targetIsPlayer;
		}
		targetMeter.CharacterChanged += HandleCharacterChanged;
	}

	private IEnumerator RemoveParticles()
	{
		for (int i = 0; i < particleSystems.Count; i++)
		{
			ParticleSystem particleSystem = particleSystems[i];
			ParticleSystem.EmissionModule emission = particleSystem.emission;
			emission.enabled = false;
		}
		bool particlesAlive = true;
		int len = particleSystems.Count;
		while (particlesAlive)
		{
			particlesAlive = false;
			for (int j = 0; j < len; j++)
			{
				if (particleSystems[j].particleCount > 0)
				{
					particlesAlive = true;
				}
			}
			yield return null;
		}
		for (int k = 0; k < len; k++)
		{
			ParticleSystem particleSystem2 = particleSystems[k];
			ParticleSystem.EmissionModule emission2 = particleSystem2.emission;
			emission2.enabled = true;
			ObjectPool.PoolObject(particleSystem2.gameObject);
		}
	}

	protected override void ResetEffect()
	{
	}

	protected override void CleanUp()
	{
	}
}
