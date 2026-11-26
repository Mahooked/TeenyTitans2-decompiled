using System;
using System.Collections;
using UnityEngine;

public class HealOverTime : StatusEffect
{
	private Character character;

	private ActionMeter actionMeter;

	private float tickRate;

	private float duration;

	private int healAmt;

	private float tickTimer;

	private float timer;

	private IEnumerator waitForTickConfirmation;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public HealOverTime(Character character, ActionMeter actionMeter, int healAmt, float tickRate, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.actionMeter = actionMeter;
		this.tickRate = tickRate;
		this.duration = duration;
		this.healAmt = healAmt;
	}

	public override void Tick()
	{
		character.DoHeal(new HealResult(healAmt));
	}

	private IEnumerator WaitForTickConfirmation(float prevTickTimer, float prevTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectTick(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForTickConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			tickTimer = prevTickTimer;
			timer = prevTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Tick();
		UnityEngine.Random.state = prevRandomState;
	}

	private IEnumerator WaitForExpireConfirmation(float prevTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			timer = prevTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		tickTimer = tickRate;
		timer = duration;
		actionMeter.CharacterChanged += HandleCharacterChanged;
	}

	private void HandleCharacterChanged()
	{
		if (actionMeter.character.character != character)
		{
			Remove();
		}
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || (!(timer > 0f) && !(duration < 0f)))
		{
			return;
		}
		float prevTickTimer = tickTimer;
		float prevTimer = timer;
		tickTimer -= DeltaTime;
		if (tickTimer <= 0f)
		{
			tickTimer += tickRate;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForTickConfirmation == null)
				{
					waitForTickConfirmation = WaitForTickConfirmation(prevTickTimer, prevTimer);
					BattleController.instance.StartCoroutine(waitForTickConfirmation);
				}
			}
			else
			{
				Tick();
			}
		}
		timer -= DeltaTime;
		if (!(timer <= 0f) || !(duration >= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevTimer);
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
		timer = duration;
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
	}

	protected override void CleanUp()
	{
		actionMeter.CharacterChanged -= HandleCharacterChanged;
	}
}
