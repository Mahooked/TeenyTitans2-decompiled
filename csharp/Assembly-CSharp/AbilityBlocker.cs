using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AbilityBlocker : StatusEffect
{
	public enum VisualType
	{
		Waffle,
		Hack
	}

	private static Dictionary<AbilityButton, AbilityBlocker> blockedButtons = new Dictionary<AbilityButton, AbilityBlocker>();

	private static float targetYDistance = 16f;

	private ActionMeter _actionMeter;

	private AbilityButton _button;

	private float duration;

	private Transform waffle;

	private float deathCounter;

	private VisualType visualType;

	private IEnumerator waitForExpireConfirmation;

	public AbilityButton button => _button;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => true;

	public override bool isResettable => false;

	public override bool isZoomedOut => false;

	public AbilityBlocker(ActionMeter meter, float duration, VisualType visualType = VisualType.Waffle, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		_actionMeter = meter;
		this.duration = duration;
		this.visualType = visualType;
	}

	private void UpdateWafflePosition()
	{
		Vector3 localPosition = waffle.localPosition;
		localPosition.y = 0f - targetYDistance + targetYDistance * (deathCounter / duration);
		waffle.localPosition = localPosition;
	}

	public static bool IsButtonBlocked(AbilityButton btn)
	{
		return blockedButtons.ContainsKey(btn);
	}

	private IEnumerator WaitForExpireConfirmation(float prevDeathCounter)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
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
		deathCounter = duration;
		List<AbilityButton> list = _actionMeter.abilityButtons.FindAll((AbilityButton obj) => obj.gameObject.activeSelf && !blockedButtons.ContainsKey(obj));
		if (list.Count == 0)
		{
			List<AbilityButton> list2 = new List<AbilityButton>(blockedButtons.Keys);
			AbilityButton key = list2[0];
			for (int num = 1; num < list2.Count; num++)
			{
				if (blockedButtons[list2[num]].deathCounter < blockedButtons[key].deathCounter)
				{
					key = list2[num];
				}
			}
			blockedButtons[key].Remove();
			_button = key;
		}
		else
		{
			_button = list[UnityEngine.Random.Range(0, list.Count)];
		}
		blockedButtons.Add(_button, this);
		_button.canActivate = false;
		if (_actionMeter.isCasting && _actionMeter.castingAbility == _button.abilityData)
		{
			_actionMeter.character.character.DoInterrupt(new InterruptResult(isInterrupted: true, isCausedByDamage: false));
		}
		else if (_actionMeter.isChanneling && _actionMeter.channelingAbility == _button.abilityData)
		{
			_actionMeter.character.character.DoInterrupt(new InterruptResult(isInterrupted: true, isCausedByDamage: false));
		}
		if (visualType == VisualType.Waffle)
		{
			waffle = ObjectPool.GetObjectFromResource("Prefabs/FX/BarWaffle").transform;
			waffle.parent = _button.uiItem.transform;
			waffle.localScale = Vector3.one;
			waffle.localPosition = new Vector3(0f, 0f, -15f);
			waffle.gameObject.SetActive(value: true);
		}
		else if (visualType == VisualType.Hack)
		{
			_button.iconSprite.SetSprite("TTG_battlebar_riddler_hacked");
		}
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || !(deathCounter > 0f))
		{
			return;
		}
		float prevDeathCounter = deathCounter;
		deathCounter -= DeltaTime;
		if (visualType == VisualType.Waffle)
		{
			UpdateWafflePosition();
		}
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
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		if (_button != null)
		{
			bool canActivate = true;
			if (_actionMeter != null && _actionMeter.character != null && StatusEffectManager.instance.CharacterHasAbilityBlockerOnButton(_actionMeter.character.character, button))
			{
				canActivate = false;
			}
			_button.canActivate = canActivate;
			blockedButtons.Remove(_button);
		}
		if (visualType == VisualType.Waffle)
		{
			if (waffle != null)
			{
				waffle.parent = null;
				waffle.DOMoveY(-600f, 700f).SetRelative().SetSpeedBased()
					.SetEase(Ease.InSine)
					.OnComplete(() =>
					{
						waffle.GetComponentInChildren<Animator>().Play("intro", 0, 0f);
						ObjectPool.PoolObject(waffle.gameObject, 0.01f);
					});
			}
		}
		else if (visualType == VisualType.Hack && _button != null)
		{
			_button.iconSprite.SetSprite(_button.abilityData.iconSprite);
		}
	}

	protected override void CleanUp()
	{
	}
}
