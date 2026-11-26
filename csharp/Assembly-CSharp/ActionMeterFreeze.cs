using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ActionMeterFreeze : StatusEffect
{
	private ActionMeter targetMeter;

	private float freezeDuration;

	private float barAmount;

	private BattleController battleController;

	private FreezeBar freezeBar;

	private SnowFlakeParticles freezeParticle;

	private SnowFlakeParticles freezeExplosion;

	private Coroutine freezeRoutine;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => false;

	public override bool isZoomedOut => false;

	public ActionMeterFreeze(ActionMeter targetMeter, float freezeDuration, float barAmount, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.targetMeter = targetMeter;
		this.freezeDuration = freezeDuration;
		this.barAmount = barAmount;
	}

	private IEnumerator PlayFreezeSequence()
	{
		tk2dSlicedSpriteHelper targetFillSprite = ((!targetMeter.isChanneling) ? targetMeter.fillSprite : targetMeter.fillChannelSprite);
		freezeBar = ObjectPool.GetObjectFromResource<FreezeBar>("Prefabs/GUI/Battle/FreezeBar");
		freezeBar.transform.parent = targetMeter.transform;
		freezeBar.transform.localPosition = new Vector3(Mathf.Max(0f, targetFillSprite.width - targetMeter.maxWidth * barAmount), 0f, -15f);
		freezeBar.transform.localScale = Vector3.one;
		freezeBar.width = targetMeter.maxWidth * barAmount;
		freezeBar.alpha = 0f;
		freezeParticle = ObjectPool.GetObjectFromResource<SnowFlakeParticles>("Prefabs/FX/Battle/freezeBar_particleEffect");
		freezeParticle.transform.parent = targetMeter.transform;
		Vector3 newPosition = freezeParticle.transform.localPosition;
		newPosition.x = targetFillSprite.transform.localPosition.x + targetMeter.maxWidth * 0.5f;
		newPosition.y = 0f;
		newPosition.z = -20f;
		freezeParticle.transform.localPosition = newPosition;
		freezeParticle.transform.localScale = Vector3.one;
		float boxWidth = targetMeter.maxWidth;
		ParticleSystem.ShapeModule newShape = freezeParticle.snowFlakePS.shape;
		Vector3 newBox = newShape.scale;
		newBox.x = boxWidth;
		newShape.scale = newBox;
		newShape = freezeParticle.snowFogPS.shape;
		newBox = newShape.scale;
		newBox.x = boxWidth;
		newShape.scale = newBox;
		newShape = freezeParticle.snowSparklePS.shape;
		newBox = newShape.scale;
		newBox.x = boxWidth;
		newShape.scale = newBox;
		targetMeter.Pause();
		Tweener tween = DOTween.To(() => freezeBar.alpha, (float x) =>
		{
			freezeBar.alpha = x;
		}, 1f, 1f).SetEase(Ease.Linear);
		DOTween.To(() => targetMeter.backSprite.color, (Color x) =>
		{
			targetMeter.backSprite.color = x;
		}, new Color(0f, 178f / 255f, 1f, 1f), 0.25f);
		yield return tween.WaitForCompletion();
		targetMeter.progress = Mathf.Clamp01(targetMeter.progress - barAmount);
		if (targetMeter.character != null && targetMeter.character.character != null)
		{
			targetMeter.character.character.DoInterrupt(new InterruptResult(isInterrupted: true, isCausedByDamage: false));
		}
		yield return new WaitForSeconds(freezeDuration);
		tween = freezeBar.transform.DOLocalMoveY(2.5f, 0.05f).SetRelative().SetLoops(10, LoopType.Yoyo)
			.SetEase(Ease.Linear);
		yield return tween.WaitForCompletion();
		freezeExplosion = ObjectPool.GetObjectFromResource<SnowFlakeParticles>("Prefabs/FX/Battle/freezeBar_particleExplosion");
		freezeExplosion.transform.parent = targetMeter.transform;
		freezeExplosion.transform.localPosition = freezeParticle.transform.localPosition;
		freezeExplosion.transform.localScale = Vector3.one;
		newShape = freezeExplosion.snowFlakePS.shape;
		newBox = newShape.scale;
		newBox.x = boxWidth;
		newShape.scale = newBox;
		newShape = freezeExplosion.snowFogPS.shape;
		newBox = newShape.scale;
		newBox.x = boxWidth;
		newShape.scale = newBox;
		newShape = freezeExplosion.snowSparklePS.shape;
		newBox = newShape.scale;
		newBox.x = boxWidth;
		newShape.scale = newBox;
		freezeBar.gameObject.SetActive(value: false);
		freezeParticle.EndParticles();
		freezeExplosion.EndParticles();
		yield return new WaitForSeconds(0.1f);
		DOTween.To(() => targetMeter.backSprite.color, (Color x) =>
		{
			targetMeter.backSprite.color = x;
		}, Color.white, 0.25f);
		freezeRoutine = null;
		Remove();
	}

	protected override void InitEffect()
	{
		battleController = BattleController.instance;
		battleController.WaitingEnded += HandleWaitingEnded;
		freezeRoutine = battleController.StartCoroutine(PlayFreezeSequence());
	}

	private void HandleWaitingEnded()
	{
		if (!targetMeter.isPaused && !BattleController.instance.waiting)
		{
			targetMeter.Pause();
			targetMeter.backSprite.color = new Color(0f, 178f / 255f, 1f, 1f);
		}
	}

	protected override void UpdateEffect()
	{
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
		Debug.Log("Freeze, Remove Effect");
		battleController.WaitingEnded -= HandleWaitingEnded;
		if (freezeRoutine != null)
		{
			Debug.Log("Freeze Routine ended early");
			battleController.StopCoroutine(freezeRoutine);
			freezeRoutine = null;
			if (freezeParticle != null)
			{
				freezeParticle.EndParticles();
			}
			if (freezeExplosion != null)
			{
				freezeExplosion.EndParticles();
			}
		}
		if (freezeBar != null)
		{
			ObjectPool.PoolObject(freezeBar.gameObject);
			freezeBar = null;
		}
		if (targetMeter.isPaused && !BattleController.instance.waiting)
		{
			targetMeter.Resume();
		}
	}

	protected override void CleanUp()
	{
	}
}
