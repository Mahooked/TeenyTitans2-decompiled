using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ActionMeterSpeedModifier : StatusEffect
{
	private static List<ActionMeterSpeedModifier> _instances = new List<ActionMeterSpeedModifier>();

	private ActionMeter _actionMeter;

	private float duration;

	private int bonus;

	private float modifier;

	private float timeRemaining;

	private ActionMeterArrows barArrows;

	private ParticleSystem barParticles;

	private Color white = Color.white;

	private IEnumerator waitForExpireConfirmation;

	public bool isBonus { get; private set; }

	public bool isDestroyed { get; private set; }

	public override StatusEffectType type
	{
		get
		{
			return (bonus > 0) ? StatusEffectType.Buff : StatusEffectType.Debuff;
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

	public static int instanceCount
	{
		get
		{
			return _instances.Count;
		}
	}

	public ActionMeterSpeedModifier(ActionMeter meter, int bonus, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		_actionMeter = meter;
		this.bonus = bonus;
		this.duration = duration;
		isBonus = true;
	}

	public ActionMeterSpeedModifier(ActionMeter meter, float modifier, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		_actionMeter = meter;
		this.modifier = modifier;
		this.duration = duration;
		isBonus = false;
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
		_instances.Add(this);
		timeRemaining = duration;
		if (isBonus)
		{
			_actionMeter.fillSpeedBonus += bonus;
		}
		else
		{
			_actionMeter.fillSpeedModifier += modifier;
		}
		if ((isBonus && bonus > 0) || (!isBonus && modifier > 0f))
		{
			_actionMeter.fillSprite.slicedSprite.SetSprite("TTG_battlebar_fill_white");
			_actionMeter.backSprite.SetSprite("TTG_battlebar_bar_dance");
			Transform transform = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/BarSpeedUpArrow").transform;
			transform.localPosition = _actionMeter.transform.position + ((!(_actionMeter.tag == "Enemy")) ? new Vector3(60f, 125f, -140f) : new Vector3(-60f, -75f, -140f));
			barArrows = ObjectPool.GetObjectFromResource<ActionMeterArrows>("Prefabs/GUI/Battle/ActionMeterArrows");
			barArrows.transform.parent = _actionMeter.transform;
			barArrows.transform.localPosition = Vector3.zero;
			barArrows.transform.localScale = Vector3.one;
			barArrows.speedDownArrows.SetActive(false);
			barArrows.speedUpArrows.SetActive(true);
			UpdateBarArrows();
			barParticles = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/FX/DanceBar_Music_ParticleSystem");
			barParticles.transform.parent = _actionMeter.transform;
			barParticles.transform.localScale = Vector3.one;
			if (!GFAudioManager.instance.IsPlaying("danceloop") && string.IsNullOrEmpty(BattleController.instance.arenaData.battleThemePath))
			{
				GFAudioManager.instance.Pause("battleTheme");
				GFAudioManager.instance.PlayClipFromResources("Audio/Music/music_danceloop", GFAudioManager.MUSIC_GROUP, "danceloop", 1f, 0f, true);
			}
		}
		else
		{
			_actionMeter.fillSprite.slicedSprite.SetSprite("TTG_battlebar_fill_pink");
			_actionMeter.backSprite.SetSprite("TTG_battlebar_bar_curse");
			Transform transform2 = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/BarSpeedDownArrow").transform;
			transform2.localPosition = _actionMeter.transform.position + ((!(_actionMeter.tag == "Enemy")) ? new Vector3(60f, 75f, -140f) : new Vector3(-60f, -125f, -140f));
			barArrows = ObjectPool.GetObjectFromResource<ActionMeterArrows>("Prefabs/GUI/Battle/ActionMeterArrows");
			barArrows.transform.parent = _actionMeter.transform;
			barArrows.transform.localPosition = Vector3.zero;
			barArrows.transform.localScale = Vector3.one;
			barArrows.speedDownArrows.SetActive(true);
			barArrows.speedUpArrows.SetActive(false);
			UpdateBarArrows();
			barParticles = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/FX/CurseBar_Skulls_ParticleSystem");
			barParticles.transform.parent = _actionMeter.transform;
			barParticles.transform.localScale = Vector3.one;
		}
		_actionMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		BattleController.instance.BattleEnded += HandleOnBattleEnded;
	}

	private void HandleCharacterChanged()
	{
		if ((isBonus && bonus > 0) || (!isBonus && modifier > 0f))
		{
			DOTween.To(() => _actionMeter.character.dancingWeight, (float x) =>
			{
				_actionMeter.character.dancingWeight = x;
			}, 1f, 0.25f);
		}
		else
		{
			_actionMeter.character.SetFloatParameter("idleSpeed", 0.5f);
		}
		if (barParticles != null)
		{
			ParticleSystem.ShapeModule shape = barParticles.shape;
			Vector3 scale = shape.scale;
			scale.x = _actionMeter.backSprite.dimensions.x;
			shape.scale = scale;
			Vector3 localPosition = barParticles.transform.localPosition;
			localPosition.x = _actionMeter.backSprite.transform.localPosition.x + _actionMeter.backSprite.dimensions.x * 0.5f;
			localPosition.y = 0f;
			localPosition.z = -20f;
			barParticles.transform.localPosition = localPosition;
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
			timeRemaining -= base.DeltaTime;
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
		if (isBonus)
		{
			_actionMeter.fillSpeedBonus += -bonus;
		}
		else
		{
			_actionMeter.fillSpeedModifier += 0f - modifier;
		}
		_actionMeter.fillSprite.slicedSprite.SetSprite((_actionMeter.color != ActionMeterColor.Green) ? "TTG_battlebar_fill_red" : "TTG_battlebar_fill_green");
		_actionMeter.backSprite.SetSprite("TTG_battlebar_bar");
		if (_instances.Count == 1 && GFAudioManager.instance.IsPlaying("danceloop"))
		{
			GFAudioManager.instance.StopById("danceloop");
			GFAudioManager.instance.UnPause("battleTheme");
		}
		_actionMeter.CharacterChanged -= HandleCharacterChanged;
		BattleController.instance.BattleEnded -= HandleOnBattleEnded;
		if (bonus > 0)
		{
			if (BattleController.instance.battleEnded)
			{
				_actionMeter.character.dancingWeight = 0f;
			}
			else
			{
				DOTween.To(() => _actionMeter.character.dancingWeight, (float x) =>
				{
					_actionMeter.character.dancingWeight = x;
				}, 0f, 0.25f);
			}
		}
		else
		{
			_actionMeter.character.SetFloatParameter("idleSpeed", 1f);
		}
		if (barArrows != null)
		{
			ObjectPool.PoolObject(barArrows.gameObject);
		}
		if (barParticles != null)
		{
			ObjectPool.PoolObject(barParticles.gameObject);
		}
		_instances.Remove(this);
	}

	protected override void CleanUp()
	{
	}
}
