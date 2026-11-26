using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : StatusEffect
{
	private ActionMeter targetMeter;

	private float duration;

	private BattleController battleController;

	private Character target;

	private Transform barCleanse;

	private Transform particleCleanse;

	private float deathCounter;

	private List<StatusEffect> statusEffectsToRemove = new List<StatusEffect>();

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

	public Immunity(ActionMeter targetMeter, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.duration = duration;
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
			BattleDisplayManager.instance.ShowMessage(Language.Get("IMMUNE", "MENUS,GUI"), targetMeter.character);
		}
	}

	private void HandleCharacterChanged()
	{
		if (target != null)
		{
			target.StatusEffectAdded -= HandleStatusEffectAdded;
		}
		target = ((!(targetMeter.character == null)) ? targetMeter.character.character : null);
		if (target != null)
		{
			target.StatusEffectAdded += HandleStatusEffectAdded;
		}
	}

	private void HandleStatusEffectAdded(StatusEffect effect)
	{
		if (effect.type == StatusEffectType.Debuff)
		{
			if (battleController.waiting)
			{
				statusEffectsToRemove.Add(effect);
				return;
			}
			effect.Remove();
			BattleDisplayManager.instance.ShowMessage(Language.Get("IMMUNE", "MENUS,GUI"), targetMeter.character);
		}
	}

	protected override void InitEffect()
	{
		battleController = BattleController.instance;
		battleController.WaitingEnded += HandleWaitingEnded;
		targetMeter.StatusEffectAdded += HandleStatusEffectAdded;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		barCleanse = ObjectPool.GetObjectFromResource("Prefabs/FX/Cleanse_Bar_FX", default(Vector3), false).transform;
		barCleanse.parent = targetMeter.transform;
		barCleanse.localScale = Vector3.one;
		Vector3 localPosition = targetMeter.backSprite.transform.localPosition + new Vector3(targetMeter.backSprite.dimensions.x * 0.5f, 0f, 0f);
		barCleanse.localPosition = localPosition;
		barCleanse.gameObject.SetActive(true);
		particleCleanse = ObjectPool.GetObjectFromResource("Prefabs/FX/CleanseFX_Particle System").transform;
		particleCleanse.localScale = targetMeter.character.transform.localScale;
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
		ObjectPool.PoolObject(particleCleanse.gameObject);
		battleController.WaitingEnded -= HandleWaitingEnded;
		battleController = null;
	}

	protected override void CleanUp()
	{
	}
}
