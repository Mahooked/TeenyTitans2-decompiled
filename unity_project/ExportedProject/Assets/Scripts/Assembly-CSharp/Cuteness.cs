using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cuteness : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private float stunDuration;

	private BattleController battleController;

	private Character target;

	private ActionMeter otherMeter;

	private PortraitSwitcher otherPortaits;

	private Transform kittenParticle;

	private int counterDamage;

	private float deathCounter;

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

	public Cuteness(ActionMeter targetMeter, float duration, float stunDuration = 0f, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
		this.stunDuration = stunDuration;
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

	private void HandleWaitingEnded()
	{
		if (counterDamage > 0)
		{
			battleController.StartCoroutine(DoCounterDamage(counterDamage));
			counterDamage = 0;
		}
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
		if (damageResult.isDodged || damageResult.isCounter)
		{
			return;
		}
		if (battleController.waiting)
		{
			if (damageResult.totalDamage > 1)
			{
				counterDamage += damageResult.totalDamage;
			}
		}
		else if (damageResult.totalDamage > 0)
		{
			battleController.StartCoroutine(DoCounterDamage(damageResult.totalDamage));
		}
	}

	private IEnumerator DoCounterDamage(int damage)
	{
		if (!(otherMeter.character != null) || otherMeter.character.character == null)
		{
			yield break;
		}
		while (battleController.waiting)
		{
			yield return null;
		}
		battleController.ForceStartWaiting();
		Transform kittenHead = ObjectPool.GetObjectFromResource("Prefabs/FX/KittenHead_Activate").transform;
		Vector3 screenPoint = ((!(targetMeter == battleController.playerActionMeter)) ? battleController.playerBattleCamera.camera.WorldToScreenPoint(otherMeter.character.center.position) : battleController.enemyBattleCamera.camera.WorldToScreenPoint(otherMeter.character.center.position));
		Vector3 worldPoint = battleController.guiCamera.ScreenToWorldPoint(screenPoint);
		worldPoint.z = -200f;
		kittenHead.position = worldPoint;
		yield return new WaitForInactive(kittenHead.gameObject);
		DamageResult damageResult = new DamageResult(Mathf.CeilToInt((float)damage * 0.5f), ImpactType.Small)
		{
			isCutenessDamage = true
		};
		otherMeter.character.character.DoDamage(damageResult);
		if (stunDuration > 0f)
		{
			if (UnityEngine.Random.value >= targetMeter.character.character.stunResistanceChance)
			{
				StatusEffectManager.instance.AddActionMeterStatusEffect(otherMeter, new ActionMeterDisabler(otherMeter, otherPortaits, stunDuration));
			}
			else
			{
				BattleDisplayManager.instance.ShowMessage(Language.Get("RESIST", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
			}
		}
		if (otherMeter.character.character.isDead)
		{
			battleController.CheckCharacterDeath();
		}
		else
		{
			battleController.ForceEndWaiting();
		}
	}

	protected override void InitEffect()
	{
		battleController = BattleController.instance;
		battleController.WaitingEnded += HandleWaitingEnded;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		otherMeter = ((!(targetMeter == battleController.playerActionMeter)) ? battleController.playerActionMeter : battleController.enemyActionMeter);
		otherPortaits = ((!(targetMeter == battleController.playerActionMeter)) ? battleController.playerPortraits : battleController.enemyPortraits);
		kittenParticle = ObjectPool.GetObjectFromResource("Prefabs/FX/KittenHead_Particle").transform;
		kittenParticle.localScale = targetMeter.character.transform.localScale;
		deathCounter = duration;
		counterDamage = 0;
	}

	protected override void UpdateEffect()
	{
		if (battleController.waiting || !(deathCounter > 0f))
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
		targetMeter.CharacterChanged -= HandleCharacterChanged;
		if (targetMeter.character != null && targetMeter.character.character == target)
		{
			DOTween.To(() => targetMeter.character.cuteWeight, (float x) =>
			{
				targetMeter.character.cuteWeight = x;
			}, 0f, 0.25f);
		}
		target.DamageReceived -= HandleDamageReceived;
		target = null;
		battleController.StartCoroutine(RemoveParticles());
		battleController.WaitingEnded -= HandleWaitingEnded;
		battleController = null;
	}

	protected override void CleanUp()
	{
	}

	private IEnumerator RemoveParticles()
	{
		ParticleSystem pSystem = kittenParticle.GetComponentInChildren<ParticleSystem>();
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
		ObjectPool.PoolObject(kittenParticle.gameObject);
	}
}
