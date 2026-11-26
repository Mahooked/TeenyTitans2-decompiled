using System;
using System.Collections;
using UnityEngine;

public class AmplifyDamageAccessory : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private float damageMultiplier;

	private float deathCounter;

	private Character targetCharacter;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public AmplifyDamageAccessory(ActionMeter targetMeter, float duration, float damageMultiplier, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.damageMultiplier = damageMultiplier;
		targetCharacter = targetMeter.character.character;
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
		targetCharacter.IncomingDamage += HandleIncomingDamage;
		targetMeter.CharacterChanged += HandleTargetCharacterChanged;
		deathCounter = duration;
		BattleDisplayManager.instance.ShowMessage(Language.Get("VULNERABLE", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
	}

	private void HandleTargetCharacterChanged()
	{
		targetCharacter.IncomingDamage -= HandleIncomingDamage;
		targetCharacter = targetMeter.character.character;
		targetCharacter.IncomingDamage += HandleIncomingDamage;
	}

	private void HandleIncomingDamage(DamageResult obj)
	{
		obj.damage = Mathf.CeilToInt((float)obj.damage * damageMultiplier);
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || !(duration >= 0f))
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
		targetCharacter.IncomingDamage -= HandleIncomingDamage;
		targetMeter.CharacterChanged -= HandleTargetCharacterChanged;
	}

	protected override void CleanUp()
	{
	}
}
