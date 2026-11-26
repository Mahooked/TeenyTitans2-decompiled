using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ActionMeterElectrify : StatusEffect
{
	private ActionMeter actionMeter;

	private float meterPauseDuration;

	private float tickRate;

	private float duration;

	private float pauseTimer;

	private float tickTimer;

	private float durationTimer;

	private GameObject electricEffect;

	private tk2dSprite electricEffectSprite;

	private tk2dSpriteAnimator electricEffectAnimator;

	private bool isMeterPaused;

	private tk2dSlicedSpriteHelper flashBar;

	private tk2dTintAdapter flashBarTintAdapter;

	private Tweener flashBarFlashTween;

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

	public ActionMeterElectrify(ActionMeter actionMeter, Character character, float duration, float tickRate, float meterPauseDuration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.actionMeter = actionMeter;
		this.meterPauseDuration = meterPauseDuration;
		this.tickRate = tickRate;
		this.duration = duration;
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
		durationTimer = duration;
		tickTimer = 0f;
		isMeterPaused = false;
		flashBar = UnityEngine.Object.Instantiate(actionMeter.fillSprite);
		flashBar.transform.parent = actionMeter.fillSprite.transform.parent;
		flashBar.transform.localPosition = actionMeter.fillSprite.transform.localPosition + new Vector3(0f, 0f, -13f);
		flashBar.transform.localRotation = actionMeter.fillSprite.transform.localRotation;
		flashBar.transform.localScale = actionMeter.fillSprite.transform.localScale;
		flashBar.width = actionMeter.progress * actionMeter.maxWidth;
		flashBar.slicedSprite.scale = actionMeter.fillSprite.slicedSprite.scale;
		flashBarTintAdapter = flashBar.GetComponent<tk2dTintAdapter>();
		flashBar.gameObject.SetActive(false);
		electricEffect = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/ElectricityBarEffect");
		electricEffect.transform.parent = actionMeter.transform;
		electricEffect.transform.localPosition = Vector3.zero;
		electricEffect.transform.localScale = Vector3.one;
		if (actionMeter == BattleController.instance.enemyActionMeter)
		{
			electricEffect.transform.localPosition = new Vector3(0.8f, 0f);
		}
		electricEffectSprite = electricEffect.GetComponentInChildren<tk2dSprite>();
		electricEffectAnimator = electricEffectSprite.GetComponent<tk2dSpriteAnimator>();
		electricEffectAnimator.Play("battlebar_electricity_activate");
		tk2dSpriteAnimator obj = electricEffectAnimator;
		obj.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(obj.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(HandleActivateAnimComplete));
		BattleDisplayManager.instance.ShowMessage(Language.Get("SHOCKED", "ABILS,CHIPS,ACHVS,LDBS"), actionMeter.character);
		BattleController.instance.WaitingStarted += HandleWaitingStarted;
		actionMeter.Resumed += HandleWaitingEnded;
		PauseManager.instance.Paused += HandleGamePaused;
	}

	private void HandleActivateAnimComplete(tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2)
	{
		tk2dSpriteAnimator obj = electricEffectAnimator;
		obj.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(obj.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(HandleActivateAnimComplete));
		electricEffectAnimator.Play("battlebar_electricity");
	}

	protected override void UpdateEffect()
	{
		if (!BattleController.instance.waiting && durationTimer > 0f)
		{
			float prevDurationTimer = durationTimer;
			float deltaTime = base.DeltaTime;
			if (isMeterPaused)
			{
				pauseTimer -= deltaTime;
				if (pauseTimer <= 0f)
				{
					ResumeActionMeter();
				}
			}
			else
			{
				tickTimer -= deltaTime;
				if (tickTimer <= 0f)
				{
					tickTimer = tickRate;
					PauseActionMeter();
				}
			}
			durationTimer -= deltaTime;
			if (durationTimer <= 0f)
			{
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
		}
		if (flashBarTintAdapter.spriteRenderer.material == null)
		{
			Debug.Log("Flash Bar Material is NULL, Force Building Sprite...");
			flashBarTintAdapter.sprite.ForceBuild();
		}
	}

	private void PauseActionMeter()
	{
		actionMeter.Pause();
		electricEffectAnimator.Play("battlebar_electricity_activate");
		tk2dSpriteAnimator obj = electricEffectAnimator;
		obj.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(obj.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(HandleActivateAnimComplete));
		tk2dSpriteAnimator obj2 = electricEffectAnimator;
		obj2.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(obj2.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(HandleActivateAnimComplete));
		float num = 0.1f;
		int num2 = Mathf.RoundToInt(meterPauseDuration / num);
		if (num2 % 2 != 0)
		{
			num2++;
		}
		tk2dSlicedSpriteHelper targetFillSprite;
		if (actionMeter.isCasting)
		{
			targetFillSprite = actionMeter.fillCastSprite;
		}
		else if (actionMeter.isChanneling)
		{
			targetFillSprite = actionMeter.fillChannelSprite;
		}
		else
		{
			targetFillSprite = actionMeter.fillSprite;
		}
		flashBar.gameObject.SetActive(true);
		flashBar.slicedSprite.SetSprite(targetFillSprite.slicedSprite.spriteId);
		flashBar.width = targetFillSprite.width;
		flashBar.slicedSprite.scale = targetFillSprite.slicedSprite.scale;
		flashBarTintAdapter.tintColor = Color.white;
		flashBarFlashTween = DOTween.To(() => flashBarTintAdapter.tintColor, (Color x) =>
		{
			flashBarTintAdapter.tintColor = x;
		}, new Color(0f, 0f, 0f, 0.75f), num).SetLoops(num2, LoopType.Yoyo).OnUpdate(() =>
		{
			flashBar.width = targetFillSprite.width;
		})
			.OnComplete(() =>
			{
				flashBarTintAdapter.tintColor = Color.clear;
			});
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_abilities_randomshock", GFAudioManager.SFX_GROUP, string.Empty);
		isMeterPaused = true;
		pauseTimer = meterPauseDuration;
	}

	private void ResumeActionMeter()
	{
		isMeterPaused = false;
		if (!actionMeter.HasStatusEffect<ActionMeterDisabler>() && !actionMeter.HasStatusEffect<ActionMeterFreeze>())
		{
			actionMeter.Resume();
		}
		if (flashBarFlashTween != null)
		{
			flashBarFlashTween.Kill();
		}
		flashBarFlashTween = null;
		flashBar.gameObject.SetActive(false);
	}

	protected override void ResetEffect()
	{
		durationTimer = duration;
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		tk2dSpriteAnimator obj = electricEffectAnimator;
		obj.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(obj.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(HandleActivateAnimComplete));
		BattleController.instance.WaitingStarted -= HandleWaitingStarted;
		actionMeter.Resumed -= HandleWaitingEnded;
		PauseManager.instance.Paused -= HandleGamePaused;
		ResumeActionMeter();
		UnityEngine.Object.Destroy(flashBar.gameObject);
		actionMeter = null;
		electricEffectSprite.color = Color.white;
		ObjectPool.PoolObject(electricEffect);
		electricEffect = null;
		Debug.Log("Action Meter Electrify, Remove Effect");
	}

	private void HandleWaitingStarted()
	{
		if (isMeterPaused)
		{
			SetFlashTweensPaused(true);
		}
	}

	private void HandleWaitingEnded()
	{
		if (isMeterPaused)
		{
			actionMeter.Pause();
			SetFlashTweensPaused(false);
		}
	}

	private void HandleGamePaused(bool paused)
	{
		if (isMeterPaused)
		{
			SetFlashTweensPaused(paused);
			flashBarTintAdapter.sprite.ForceBuild();
		}
	}

	private void SetFlashTweensPaused(bool paused)
	{
		if (paused)
		{
			if (flashBarFlashTween != null && flashBarFlashTween.IsPlaying())
			{
				flashBarFlashTween.Pause();
			}
		}
		else if (flashBarFlashTween != null)
		{
			flashBarFlashTween.Play();
		}
	}

	protected override void CleanUp()
	{
	}
}
