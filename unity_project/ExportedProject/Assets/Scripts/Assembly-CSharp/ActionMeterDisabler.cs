using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ActionMeterDisabler : StatusEffect
{
	private ActionMeter _actionMeter;

	private PortraitSwitcher _portraits;

	private AccessoryBattleButton _accessoryBattleButton;

	private float timeRemaining;

	private Character character;

	private DizzySymbol dizzySymbol;

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
			return false;
		}
	}

	public override bool isZoomedOut
	{
		get
		{
			return false;
		}
	}

	public ActionMeterDisabler(ActionMeter meter, PortraitSwitcher portraits, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		_actionMeter = meter;
		_portraits = portraits;
		timeRemaining = duration;
		_accessoryBattleButton = ((!(_actionMeter.character == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyAccessoryButton : BattleController.instance.playerAccessoryButton);
		Debug.Log("Duration of Stun is " + timeRemaining);
	}

	private IEnumerator WaitForExpireConfirmation(float prevTimeRemaining)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(base.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			timeRemaining = prevTimeRemaining;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		_actionMeter.Pause();
		_portraits.isEnabled = false;
		_accessoryBattleButton.isEnabled = false;
		_actionMeter.Resumed += HandleResumed;
		_portraits.Enabled += HandleEnabled;
		DOTween.To(() => _actionMeter.character.dizzyWeight, (float x) =>
		{
			_actionMeter.character.dizzyWeight = x;
		}, 1f, 0.25f);
		dizzySymbol = ObjectPool.GetObjectFromResource<DizzySymbol>("Prefabs/FX/Dizzy_Symbol", default(Vector3), false);
		dizzySymbol.Init(_actionMeter.character);
		dizzySymbol.gameObject.SetActive(true);
		character = _actionMeter.character.character;
		character.Death += HandleDeath;
		character.IncomingDamage += HandleIncomingDamage;
		character.DamageReceived += HandleDamageReceived;
		character.DoInterrupt(new InterruptResult(true, false));
		if (character.isDead)
		{
			HandleDeath();
		}
	}

	private void HandleResumed()
	{
		_actionMeter.Pause();
	}

	private void HandleEnabled()
	{
		_portraits.isEnabled = false;
	}

	private void HandleDeath()
	{
		Remove();
	}

	private void HandleIncomingDamage(DamageResult result)
	{
		result.isDodged = false;
	}

	private void HandleDamageReceived(DamageResult result)
	{
		if (!result.isDodged && result.totalDamage > 0)
		{
			Remove();
		}
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || !(timeRemaining > 0f))
		{
			return;
		}
		float prevTimeRemaining = timeRemaining;
		timeRemaining -= base.DeltaTime;
		if (!(timeRemaining <= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevTimeRemaining);
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
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		_actionMeter.Resumed -= HandleResumed;
		_portraits.Enabled -= HandleEnabled;
		DOTween.To(() => _actionMeter.character.dizzyWeight, (float x) =>
		{
			_actionMeter.character.dizzyWeight = x;
		}, 0f, 0.25f);
		if (dizzySymbol != null)
		{
			ObjectPool.PoolObject(dizzySymbol.gameObject);
		}
		if (character != null)
		{
			character.Death -= HandleDeath;
			character.IncomingDamage -= HandleIncomingDamage;
			character.DamageReceived -= HandleDamageReceived;
			character = null;
		}
		if (!BattleController.instance.waiting)
		{
			_actionMeter.Resume();
			_portraits.isEnabled = true;
		}
		_accessoryBattleButton.isEnabled = true;
	}

	protected override void CleanUp()
	{
	}
}
