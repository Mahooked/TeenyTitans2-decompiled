using System;
using System.Collections;
using UnityEngine;

public class ActionMeterSpeedModifierAccessory : StatusEffect
{
	private ActionMeter _actionMeter;

	private float duration;

	private int bonus;

	private int hiveBonus;

	private float timeRemaining;

	private int currentBonus;

	private bool effectSuppressed;

	private ActionMeterArrows barArrows;

	private ParticleSystem barParticles;

	private Color white = Color.white;

	private IEnumerator waitForExpireConfirmation;

	private int id;

	private static int nextID;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public override bool isCleansable => false;

	public ActionMeterSpeedModifierAccessory(ActionMeter meter, int bonus, int hiveBonus, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		_actionMeter = meter;
		this.bonus = bonus;
		this.hiveBonus = hiveBonus;
		this.duration = duration;
		id = nextID++;
	}

	private IEnumerator WaitForExpireConfirmation(float prevTimeRemaining)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
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
		timeRemaining = duration;
		effectSuppressed = false;
		ApplySpeedBonus();
		_actionMeter.fillSprite.slicedSprite.SetSprite("TTG_battlebar_fill_white");
		_actionMeter.backSprite.SetSprite("TTG_battlebar_bar_dance");
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/BarSpeedUpArrow").transform;
		transform.localPosition = _actionMeter.transform.position + ((!_actionMeter.CompareTag("Enemy")) ? new Vector3(60f, 125f, -140f) : new Vector3(-60f, -75f, -140f));
		barArrows = ObjectPool.GetObjectFromResource<ActionMeterArrows>("Prefabs/GUI/Battle/ActionMeterArrows");
		barArrows.transform.parent = _actionMeter.transform;
		barArrows.transform.localPosition = Vector3.zero;
		barArrows.transform.localScale = Vector3.one;
		barArrows.speedDownArrows.SetActive(value: false);
		barArrows.speedUpArrows.SetActive(value: true);
		UpdateBarArrows();
		barParticles = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/FX/HiveCoin_Bar_ParticleSystem");
		barParticles.transform.parent = _actionMeter.transform;
		barParticles.transform.localScale = Vector3.one;
		ParticleSystem.ShapeModule shape = barParticles.shape;
		Vector3 scale = shape.scale;
		scale.x = _actionMeter.backSprite.dimensions.x;
		shape.scale = scale;
		Vector3 localPosition = barParticles.transform.localPosition;
		localPosition.x = _actionMeter.backSprite.transform.localPosition.x + _actionMeter.backSprite.dimensions.x * 0.5f;
		localPosition.y = 0f;
		localPosition.z = -20f;
		barParticles.transform.localPosition = localPosition;
		BattleController.instance.BattleEnded += HandleOnBattleEnded;
		_actionMeter.StatusEffectAdded += HandleActionMeterEffectAdded;
		_actionMeter.StatusEffectRemoved += HandleActionMeterEffectRemoved;
		_actionMeter.CharacterChanged += HandleCharacterChanged;
		ActionMeterSpeedModifier statusEffect = _actionMeter.GetStatusEffect<ActionMeterSpeedModifier>();
		if (statusEffect != null)
		{
			HandleActionMeterEffectAdded(statusEffect);
		}
	}

	private void ApplySpeedBonus()
	{
		currentBonus = ((!_actionMeter.character.characterDefinition.tags.Contains("HIVE")) ? bonus : hiveBonus);
		_actionMeter.fillSpeedBonus += currentBonus;
	}

	private void HandleActionMeterEffectAdded(StatusEffect statusEffect)
	{
		if (statusEffect is ActionMeterSpeedModifier && !effectSuppressed)
		{
			_actionMeter.fillSpeedBonus -= currentBonus;
			effectSuppressed = true;
			barArrows.speedUpArrows.SetActive(value: false);
			barParticles.EnableEmission(enabled: false);
		}
	}

	private void HandleActionMeterEffectRemoved(StatusEffect statusEffect)
	{
		if (statusEffect is ActionMeterSpeedModifier && !_actionMeter.HasStatusEffect<ActionMeterSpeedModifier>() && effectSuppressed)
		{
			effectSuppressed = false;
			ApplySpeedBonus();
			_actionMeter.fillSprite.slicedSprite.SetSprite("TTG_battlebar_fill_white");
			_actionMeter.backSprite.SetSprite("TTG_battlebar_bar_dance");
			barArrows.speedUpArrows.SetActive(value: true);
			barParticles.EnableEmission(enabled: true);
		}
	}

	private void HandleCharacterChanged()
	{
		if (!effectSuppressed)
		{
			_actionMeter.fillSpeedBonus -= currentBonus;
			ApplySpeedBonus();
		}
	}

	private void HandleOnBattleEnded()
	{
		Remove();
	}

	private void UpdateBarArrows()
	{
		if (barArrows != null)
		{
			tk2dSlicedSpriteHelper tk2dSlicedSpriteHelper2 = ((!_actionMeter.isChanneling) ? _actionMeter.fillSprite : _actionMeter.fillChannelSprite);
			Vector3 localPosition = barArrows.leftMask.localPosition;
			localPosition.x = tk2dSlicedSpriteHelper2.transform.localPosition.x;
			barArrows.leftMask.localPosition = localPosition;
			localPosition = barArrows.rightMask.localPosition;
			localPosition.x = tk2dSlicedSpriteHelper2.width;
			barArrows.rightMask.localPosition = localPosition;
			barArrows.speedDownArrowsSprite.color = ((!_actionMeter.isChanneling) ? white : barArrows.speedDownArrowColorChanneling);
			barArrows.speedUpArrowsSprite.color = ((!_actionMeter.isChanneling) ? white : barArrows.speedUpArrowColorChanneling);
		}
	}

	protected override void UpdateEffect()
	{
		if (!BattleController.instance.waiting && duration >= 0f && timeRemaining > 0f)
		{
			float prevTimeRemaining = timeRemaining;
			timeRemaining -= DeltaTime;
			if (timeRemaining <= 0f)
			{
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
		}
		UpdateBarArrows();
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
		if (!effectSuppressed)
		{
			_actionMeter.fillSpeedBonus -= bonus;
			_actionMeter.fillSprite.slicedSprite.SetSprite((_actionMeter.color != ActionMeterColor.Green) ? "TTG_battlebar_fill_red" : "TTG_battlebar_fill_green");
			_actionMeter.backSprite.SetSprite("TTG_battlebar_bar");
		}
		if (barArrows != null)
		{
			ObjectPool.PoolObject(barArrows.gameObject);
		}
		if (barParticles != null)
		{
			ObjectPool.PoolObject(barParticles.gameObject);
		}
		BattleController.instance.BattleEnded -= HandleOnBattleEnded;
		_actionMeter.StatusEffectAdded -= HandleActionMeterEffectAdded;
		_actionMeter.StatusEffectRemoved -= HandleActionMeterEffectRemoved;
		_actionMeter.CharacterChanged -= HandleCharacterChanged;
	}

	protected override void CleanUp()
	{
	}
}
