using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectMinion : StatusEffect
{
	private static readonly Dictionary<ActionMeter, List<StatusEffectMinion>> activeMinions = new Dictionary<ActionMeter, List<StatusEffectMinion>>();

	private const int maxMinions = 2;

	protected ActionMeter targetMeter;

	private float duration;

	protected Vector3 targetMinionPosition;

	protected Character target;

	protected float deathTimer;

	protected bool diedFromDamage;

	private int minionPositionIndex;

	private IEnumerator waitForExpireConfirmation;

	protected abstract List<Vector3> minionPositions { get; }

	protected abstract Vector3 minionLocalPosition { get; }

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
			return true;
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
			return true;
		}
	}

	protected StatusEffectMinion(ActionMeter targetMeter, float duration, Accessory casterAccessory)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
	}

	private IEnumerator WaitForExpireConfirmation(float prevDeathTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(base.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			deathTimer = prevDeathTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		deathTimer = duration;
		diedFromDamage = false;
		if (activeMinions.ContainsKey(targetMeter))
		{
			List<StatusEffectMinion> list = activeMinions[targetMeter];
			if (list.Count == 2)
			{
				StatusEffectMinion statusEffectMinion = list[0];
				for (int i = 1; i < list.Count; i++)
				{
					if (list[i].deathTimer < statusEffectMinion.deathTimer)
					{
						statusEffectMinion = list[i];
					}
				}
				minionPositionIndex = statusEffectMinion.minionPositionIndex;
				targetMinionPosition = minionPositions[statusEffectMinion.minionPositionIndex];
				Debug.Log("A Target Minion Position Set to: " + targetMinionPosition);
				statusEffectMinion.Remove();
				if (!activeMinions.ContainsKey(targetMeter))
				{
					activeMinions.Add(targetMeter, new List<StatusEffectMinion>());
				}
				activeMinions[targetMeter].Add(this);
			}
			else
			{
				for (int j = 0; j < minionPositions.Count; j++)
				{
					bool flag = true;
					for (int k = 0; k < list.Count; k++)
					{
						StatusEffectMinion statusEffectMinion2 = list[k];
						if (j == statusEffectMinion2.minionPositionIndex)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						minionPositionIndex = j;
						targetMinionPosition = minionPositions[j];
						break;
					}
				}
				Debug.Log("B Target Minion Position Set to: " + targetMinionPosition);
				activeMinions[targetMeter].Add(this);
			}
		}
		else
		{
			int index = (minionPositionIndex = 0);
			targetMinionPosition = minionPositions[index];
			Debug.Log("C Target Minion Position Set to: " + targetMinionPosition);
			activeMinions.Add(targetMeter, new List<StatusEffectMinion> { this });
		}
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		InitMinionEffect();
	}

	protected override void UpdateEffect()
	{
		if (target == null || BattleController.instance.waiting)
		{
			return;
		}
		if (deathTimer > 0f)
		{
			float prevDeathTimer = deathTimer;
			deathTimer -= base.DeltaTime;
			if (deathTimer <= 0f)
			{
				if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
				{
					if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
					{
						waitForExpireConfirmation = WaitForExpireConfirmation(prevDeathTimer);
						BattleController.instance.StartCoroutine(waitForExpireConfirmation);
					}
				}
				else
				{
					Expire();
				}
			}
		}
		UpdateMinionEffect();
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
			target.DamageReceived -= HandleDamageReceived;
		}
		activeMinions[targetMeter].Remove(this);
		if (activeMinions[targetMeter].Count == 0)
		{
			activeMinions.Remove(targetMeter);
		}
		RemoveMinionEffect();
	}

	protected override void CleanUp()
	{
	}

	protected override void ResetEffect()
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
		}
	}

	private void HandleDamageReceived(DamageResult result)
	{
		if (result.abilityData != null && result.abilityData.type == AbilityType.Attack && result.abilityData.targetCount == TargetCount.All && result.damage > 0)
		{
			diedFromDamage = true;
			Remove();
		}
	}

	protected abstract void InitMinionEffect();

	protected abstract void UpdateMinionEffect();

	protected abstract void RemoveMinionEffect();
}
