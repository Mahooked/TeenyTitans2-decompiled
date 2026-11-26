using System;
using System.Collections;
using UnityEngine;

public class ProtectionBubble : StatusEffect
{
	private static Vector3 targetScale = new Vector3(0.8f, 0.8f, 0.8f);

	private string bubblePath = string.Empty;

	private ActionMeter targetMeter;

	private BattleCharacter targetBC;

	private float duration;

	private int healOnEnd;

	private float bubbleHealth;

	private GameObject bubblePrefab;

	private BurpBubble bubble;

	private Character target;

	private float deathCounter;

	private float currentBubbleHealth;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => true;

	public ProtectionBubble(string bubblePath, ActionMeter targetMeter, float duration, float bubbleHealth, int healOnEnd = 0, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.bubblePath = bubblePath;
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.healOnEnd = healOnEnd;
		this.bubbleHealth = bubbleHealth;
	}

	public ProtectionBubble(GameObject bubblePrefab, ActionMeter targetMeter, float duration, float bubbleHealth, int healOnEnd = 0, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.bubblePrefab = bubblePrefab;
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.healOnEnd = healOnEnd;
		this.bubbleHealth = bubbleHealth;
	}

	public override void Expire()
	{
		if (healOnEnd > 0)
		{
			target.DoHeal(new HealResult(target, target, healOnEnd));
		}
		Remove();
	}

	private void HandleCharacterChanged()
	{
		if (target != null)
		{
			target.IncomingDamage -= HandleIncomingDamage;
		}
		targetBC = targetMeter.character;
		target = ((!(targetBC == null)) ? targetBC.character : null);
		if (target != null)
		{
			target.IncomingDamage += HandleIncomingDamage;
		}
	}

	private void HandleIncomingDamage(DamageResult damageResult)
	{
		currentBubbleHealth -= damageResult.damage;
		if (damageResult.abilityData == null || (damageResult.source != null && (!damageResult.source.IsAbilityShiny(damageResult.abilityData) || damageResult.abilityData.shinyEffect.effectType != AbilityShinyEffectType.AddPiercingDamage)))
		{
			damageResult.damage = 0;
			damageResult.classBonusDamage = 0;
			damageResult.luckBonusDamage = 0;
			damageResult.isDodged = false;
		}
		if (currentBubbleHealth <= 0f)
		{
			Remove();
		}
	}

	private void UpdateBubbleSize()
	{
		if (bubble != null)
		{
			Vector3 localScale = bubble.scaleGroup.localScale;
			localScale = targetScale + (Vector3.one - targetScale) * (deathCounter / duration);
			bubble.scaleGroup.localScale = localScale;
		}
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
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		currentBubbleHealth = bubbleHealth;
		deathCounter = duration;
		if (bubblePrefab != null)
		{
			ObjectPool.instance.AddType(bubblePrefab, 1);
			bubble = ObjectPool.GetObject<BurpBubble>(bubblePrefab.name);
			if (bubble != null)
			{
				bubble.transform.localScale = targetMeter.character.transform.localScale;
			}
		}
		else if (!string.IsNullOrEmpty(bubblePath))
		{
			bubble = ObjectPool.GetObjectFromResource<BurpBubble>(bubblePath);
			if (bubble != null)
			{
				bubble.transform.localScale = targetMeter.character.transform.localScale;
			}
		}
		UpdateBubbleSize();
	}

	protected override void UpdateEffect()
	{
		if (target == null || BattleController.instance.waiting || !(deathCounter > 0f))
		{
			return;
		}
		float prevDeathCounter = deathCounter;
		deathCounter -= DeltaTime;
		UpdateBubbleSize();
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
		currentBubbleHealth = bubbleHealth;
		deathCounter = duration;
		UpdateBubbleSize();
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		targetMeter.CharacterChanged -= HandleCharacterChanged;
		if (target != null)
		{
			target.IncomingDamage -= HandleIncomingDamage;
		}
		if (bubble != null)
		{
			bubble.Pop();
		}
		targetBC = null;
	}

	protected override void CleanUp()
	{
	}
}
