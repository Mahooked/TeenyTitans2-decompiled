using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealBuffBlocker : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private BattleController battleController;

	private Character target;

	private Transform barCleanse;

	private Transform particle;

	private float deathCounter;

	private List<StatusEffect> statusEffectsToRemove = new List<StatusEffect>();

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

	public HealBuffBlocker(ActionMeter targetMeter, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
		base.casterAccessory = casterAccessory;
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
		if (statusEffectsToRemove.Count > 0)
		{
			while (statusEffectsToRemove.Count > 0)
			{
				statusEffectsToRemove.Pop().Remove();
			}
			BattleDisplayManager.instance.ShowMessage(Language.Get("BLOCKED", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
		}
	}

	private void HandleCharacterChanged()
	{
		if (target != null)
		{
			target.StatusEffectAdded -= HandleStatusEffectAdded;
			target.IncomingHeal -= HandleIncomingHeal;
		}
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
		if (target != null)
		{
			target.StatusEffectAdded += HandleStatusEffectAdded;
			target.IncomingHeal += HandleIncomingHeal;
		}
	}

	private void HandleStatusEffectAdded(StatusEffect effect)
	{
		if (effect.type == StatusEffectType.Buff && !(effect is Flight) && effect.isCleansable)
		{
			if (battleController.waiting)
			{
				statusEffectsToRemove.Add(effect);
				return;
			}
			effect.Remove();
			BattleDisplayManager.instance.ShowMessage(Language.Get("BLOCKED", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
		}
	}

	private void HandleIncomingHeal(HealResult healResult)
	{
		healResult.amount = 0;
		healResult.adjustedAmount = 0;
		healResult.display = false;
		BattleDisplayManager.instance.ShowMessage(Language.Get("BLOCKED", "ABILS,CHIPS,ACHVS,LDBS"), targetMeter.character);
	}

	protected override void InitEffect()
	{
		battleController = BattleController.instance;
		battleController.WaitingEnded += HandleWaitingEnded;
		List<Character> list = ((!(targetMeter == battleController.playerActionMeter)) ? battleController.enemyParty : battleController.playerParty);
		for (int i = 0; i < list.Count; i++)
		{
			Character character = list[i];
			if (character.isDead)
			{
				continue;
			}
			for (int j = 0; j < character.statusEffects.Count; j++)
			{
				StatusEffect statusEffect = character.statusEffects[j];
				if (statusEffect.type == StatusEffectType.Buff && !(statusEffect is Flight) && statusEffect.isCleansable)
				{
					statusEffect.Remove();
				}
			}
		}
		targetMeter.RemoveAllStatusEffectsByType(StatusEffectType.Buff, true);
		Vector3 position = battleController.splitCameras.activeMainCamera.WorldToScreenPoint(targetMeter.character.center.position);
		Vector3 destination = battleController.guiCamera.ScreenToWorldPoint(position);
		destination.z = -100f;
		ObjectPool.GetObjectFromResource("Prefabs/FX/HealAndBuffBlocker_FX", destination);
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/HealAndBuffBlocker_Text").transform;
		transform.SetParent(battleController.guiCamera.transform, true);
		transform.localPosition = new Vector3(0f, 0f, 50f);
		targetMeter.StatusEffectAdded += HandleStatusEffectAdded;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		barCleanse = ObjectPool.GetObjectFromResource("Prefabs/FX/HealAndBufBlocker_Bar_FX", default(Vector3), false).transform;
		barCleanse.SetParent(targetMeter.transform, true);
		barCleanse.localScale = Vector3.one;
		destination = targetMeter.backSprite.transform.localPosition + new Vector3(targetMeter.backSprite.dimensions.x * 0.5f, 0f, 0f);
		barCleanse.localPosition = destination;
		barCleanse.gameObject.SetActive(true);
		particle = ObjectPool.GetObjectFromResource("Prefabs/FX/HQ_noBuff_Particle").transform;
		particle.localScale = targetMeter.character.transform.localScale;
		deathCounter = duration;
	}

	protected override void UpdateEffect()
	{
		if (target == null || battleController.waiting || !(deathCounter > 0f))
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
		targetMeter.StatusEffectAdded -= HandleStatusEffectAdded;
		targetMeter.CharacterChanged -= HandleCharacterChanged;
		if (target != null)
		{
			target.StatusEffectAdded -= HandleStatusEffectAdded;
			target.IncomingHeal -= HandleIncomingHeal;
		}
		Animator animator = barCleanse.GetComponentInChildren<Animator>();
		AnimatorEventListener animatorEventListener = barCleanse.GetComponentInChildren<AnimatorEventListener>();
		Action<AnimationEvent> completeDelegate = null;
		completeDelegate = (AnimationEvent obj) =>
		{
			if (obj.stringParameter.Equals("loopComplete"))
			{
				animator.Play("outro");
			}
			else if (obj.stringParameter.Equals("outroComplete"))
			{
				animatorEventListener.EventTriggered -= completeDelegate;
				ObjectPool.PoolObject(barCleanse.gameObject);
			}
		};
		animatorEventListener.EventTriggered += completeDelegate;
		battleController.StartCoroutine(RemoveParticles());
		battleController.WaitingEnded -= HandleWaitingEnded;
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
