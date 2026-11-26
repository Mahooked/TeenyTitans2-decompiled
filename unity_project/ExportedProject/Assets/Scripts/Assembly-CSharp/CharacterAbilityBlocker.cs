using System;
using System.Collections;
using UnityEngine;

public class CharacterAbilityBlocker : StatusEffect
{
	private static Color chilledColor = new Color(0f, 178f / 255f, 1f, 1f);

	private Character character;

	private ActionMeter meter;

	private AbilityData ability;

	private float duration;

	private BattleController controller;

	private AbilityButton _button;

	private SnowFlakeParticles snowFlakeParticles;

	private float deathCounter;

	private IEnumerator waitForExpireConfirmation;

	public AbilityButton button
	{
		get
		{
			return _button;
		}
	}

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
			return true;
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

	public CharacterAbilityBlocker(Character character, ActionMeter meter, AbilityData ability, float duration, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.meter = meter;
		this.ability = ability;
		this.duration = duration;
	}

	private void HandleCharacterChanged()
	{
		if (!(meter.character != null) || meter.character.character == null)
		{
			return;
		}
		if (meter.character.character == character)
		{
			if (button != null)
			{
				bool canActivate = true;
				if (StatusEffectManager.instance.MeterHasAbilityBlockerOnButton(meter, button))
				{
					canActivate = false;
				}
				button.canActivate = canActivate;
			}
			_button = meter.abilityButtons.Find((AbilityButton obj) => obj.abilityData.uid == ability.uid);
			if (button != null)
			{
				if (snowFlakeParticles == null)
				{
					snowFlakeParticles = ObjectPool.GetObjectFromResource<SnowFlakeParticles>("Prefabs/GUI/Snowflake_PS");
					snowFlakeParticles.transform.parent = button.transform;
					snowFlakeParticles.transform.localScale = Vector3.one;
					snowFlakeParticles.transform.localPosition = new Vector3(0f, button.uiItem.transform.localPosition.y, -10f);
				}
				button.SetStandardSprites();
				button.colorSet = new AbilityButton.ColorSet(chilledColor);
				button.canActivate = false;
			}
			else if (snowFlakeParticles != null)
			{
				snowFlakeParticles.transform.parent = null;
				snowFlakeParticles.EndParticles();
				snowFlakeParticles = null;
			}
			return;
		}
		if (snowFlakeParticles != null)
		{
			snowFlakeParticles.transform.parent = null;
			snowFlakeParticles.EndParticles();
			snowFlakeParticles = null;
		}
		if (button != null)
		{
			button.ResetColorSet();
			button.ResetSprites();
			bool canActivate2 = true;
			if (StatusEffectManager.instance.MeterHasAbilityBlockerOnButton(meter, button))
			{
				canActivate2 = false;
			}
			button.canActivate = canActivate2;
			_button = null;
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

	protected override void InitEffect()
	{
		controller = BattleController.instance;
		deathCounter = duration;
		meter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
	}

	protected override void UpdateEffect()
	{
		if (button != null && button.canActivate)
		{
			button.canActivate = false;
		}
		if (controller.waiting || !(deathCounter > 0f))
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
		if (button != null)
		{
			button.ResetColorSet();
			button.ResetSprites();
			bool canActivate = true;
			if (StatusEffectManager.instance.MeterHasAbilityBlockerOnButton(meter, button))
			{
				canActivate = false;
			}
			button.canActivate = canActivate;
			_button = null;
		}
		if (snowFlakeParticles != null)
		{
			snowFlakeParticles.transform.parent = null;
			snowFlakeParticles.EndParticles();
			snowFlakeParticles = null;
		}
		meter.CharacterChanged -= HandleCharacterChanged;
		character = null;
		meter = null;
		ability = null;
		controller = null;
	}

	protected override void CleanUp()
	{
	}
}
