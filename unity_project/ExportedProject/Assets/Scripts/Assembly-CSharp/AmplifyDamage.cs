using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AmplifyDamage : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private float damageMultiplier;

	private string defUpPrefabName;

	private string defDownPrefabName;

	private BattleController battleController;

	private Character target;

	private Transform particle;

	private float deathCounter;

	private List<Character> targetParty = new List<Character>();

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type
	{
		get
		{
			return (!(damageMultiplier >= 1f)) ? StatusEffectType.Buff : StatusEffectType.Debuff;
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

	public float DamageMultiplier
	{
		get
		{
			return damageMultiplier;
		}
	}

	public AmplifyDamage(ActionMeter targetMeter, float duration, float damageMultiplier, string defUpPrefabName, string defDownPrefabName, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.damageMultiplier = damageMultiplier;
		this.defUpPrefabName = defUpPrefabName;
		this.defDownPrefabName = defDownPrefabName;
		if (targetMeter == BattleController.instance.playerActionMeter)
		{
			targetParty = BattleController.instance.playerParty;
		}
		if (targetMeter == BattleController.instance.enemyActionMeter)
		{
			targetParty = BattleController.instance.enemyParty;
		}
	}

	private IEnumerator WaitForExpireConfirmation(float prevDeathCounter)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(base.uid, Environment.TickCount);
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

	private void HandleIncomingDamage(DamageResult obj)
	{
		obj.damage = Mathf.CeilToInt((float)obj.damage * damageMultiplier);
	}

	protected override void InitEffect()
	{
		battleController = BattleController.instance;
		for (int i = 0; i < targetParty.Count; i++)
		{
			Debug.Log(targetParty[i].characterDefinition.characterName + ", damage amplified");
			targetParty[i].IncomingDamage += HandleIncomingDamage;
		}
		deathCounter = duration;
		if (string.IsNullOrEmpty(defDownPrefabName))
		{
			defDownPrefabName = "Prefabs/FX/BB_defDown_Particle";
		}
		if (string.IsNullOrEmpty(defUpPrefabName))
		{
			defUpPrefabName = "Prefabs/FX/BB_defDown_Particle";
		}
		if (damageMultiplier >= 1f)
		{
			particle = ObjectPool.GetObjectFromResource(defDownPrefabName).transform;
			particle.localScale = targetMeter.character.transform.localScale;
			BattleDisplayManager.instance.ShowMessage(Language.Get("VULNERABLE", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
		}
		else
		{
			particle = ObjectPool.GetObjectFromResource(defUpPrefabName).transform;
			particle.localScale = targetMeter.character.transform.localScale;
			BattleDisplayManager.instance.ShowMessage(Language.Get("DEFENSEUP", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
		}
	}

	protected override void UpdateEffect()
	{
		if (battleController.waiting || !(duration >= 0f))
		{
			return;
		}
		float prevDeathCounter = deathCounter;
		deathCounter -= base.DeltaTime;
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
		for (int i = 0; i < targetParty.Count; i++)
		{
			targetParty[i].IncomingDamage -= HandleIncomingDamage;
		}
		battleController.StartCoroutine(RemoveParticles());
		battleController = null;
	}

	protected override void CleanUp()
	{
	}

	private IEnumerator RemoveParticles()
	{
		ParticleSystem pSystem = particle.GetComponentInChildren<ParticleSystem>();
		pSystem.EnableEmission(false);
		DOTween.To(() => pSystem.playbackSpeed, (float x) =>
		{
			pSystem.playbackSpeed = x;
		}, 4f, 0.5f).SetEase(Ease.Linear);
		while (pSystem.particleCount > 0)
		{
			yield return null;
		}
		pSystem.EnableEmission(true);
		pSystem.playbackSpeed = 1f;
		ObjectPool.PoolObject(particle.gameObject);
	}
}
