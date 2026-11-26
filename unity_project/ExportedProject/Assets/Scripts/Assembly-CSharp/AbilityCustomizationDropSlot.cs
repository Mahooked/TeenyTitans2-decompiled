using System;
using TMPro;
using UnityEngine;

public class AbilityCustomizationDropSlot : MonoBehaviour
{
	private static Color fontColorPositive = new Color(109f / 255f, 225f / 255f, 110f / 255f);

	private static Color fontColorNegative = new Color(13f / 15f, 93f / 255f, 93f / 255f);

	public tk2dSprite sprite;

	public tk2dSprite lockSprite;

	public tk2dTintAdapter tintAdapter;

	public BoxCollider2D boxCollider2D;

	public TextMeshPro[] textLines;

	private int nextTextLineIndex;

	private AbilityCustomizationAbilityButton _button;

	private Character _character;

	private bool _isHighlighted;

	private bool _isLocked;

	public AbilityCustomizationAbilityButton button
	{
		get
		{
			return _button;
		}
		set
		{
			SetButton(value);
		}
	}

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			_character = value;
		}
	}

	public bool isHighlighted
	{
		get
		{
			return _isHighlighted;
		}
		set
		{
			_isHighlighted = value;
			sprite.SetSprite((!_isHighlighted) ? "TT2_gui_custommove_iconbase_lower" : "TT2_gui_custommove_iconbase_highlight_lower");
		}
	}

	public bool isLocked
	{
		get
		{
			return _isLocked;
		}
		set
		{
			_isLocked = value;
			if (_isLocked)
			{
				boxCollider2D.enabled = false;
				lockSprite.gameObject.SetActive(true);
			}
			else
			{
				boxCollider2D.enabled = true;
				lockSprite.gameObject.SetActive(false);
			}
		}
	}

	private void Awake()
	{
		if (tintAdapter == null)
		{
			tintAdapter = GetComponent<tk2dTintAdapter>();
		}
		Refresh();
	}

	public void SetButton(AbilityCustomizationAbilityButton button)
	{
		if (!object.ReferenceEquals(button, _button))
		{
			_button = button;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (_button != null)
		{
			boxCollider2D.enabled = true;
		}
		else
		{
			boxCollider2D.enabled = !_isLocked;
		}
		RefreshTextLines();
	}

	private void RefreshTextLines()
	{
		ResetTextLines();
		if (!(_button != null) || _button.position == _button.originalPosition)
		{
			return;
		}
		bool flag = _button.position > _button.originalPosition;
		Color fontColor = ((!flag) ? fontColorNegative : fontColorPositive);
		AbilityData abilityData = _button.abilityData;
		if (abilityData.type == AbilityType.StatusEffect)
		{
			switch (abilityData.statusEffect)
			{
			case StatusEffectName.ActionMeterSpeedModifier:
			case StatusEffectName.ProtectionBubble:
			case StatusEffectName.Smokescreen:
			case StatusEffectName.Cute:
			case StatusEffectName.KnockedOut:
			case StatusEffectName.AbilityBlocker:
			case StatusEffectName.Immunity:
			case StatusEffectName.Cuteness:
			case StatusEffectName.HealAndBuffBlocker:
			case StatusEffectName.AmplifyDamage:
			case StatusEffectName.Flight:
			case StatusEffectName.ActionMeterElectrify:
			case StatusEffectName.Root:
			case StatusEffectName.AttackMinion:
			case StatusEffectName.Counterattack:
			case StatusEffectName.ActionMeterHack:
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("DURATION", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
				break;
			case StatusEffectName.ActionMeterDisabler:
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("STUN", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
				break;
			case StatusEffectName.AttackDrone:
			case StatusEffectName.Poison:
			case StatusEffectName.RemoteBomb:
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("DAMAGE", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
				break;
			case StatusEffectName.BuffDropper:
			case StatusEffectName.ActionMeterFiller:
			case StatusEffectName.ActionMeterDepleter:
			case StatusEffectName.ActionMeterFreeze:
			case StatusEffectName.HealOverTime:
			case StatusEffectName.Fear:
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("AMOUNT", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
				break;
			case StatusEffectName.TeamStatBuff:
				if (abilityData.rangedIntValue1.GetInterpolatedValue(0.5f) > 0f)
				{
					SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("BUFF", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
				}
				else
				{
					SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("DEBUFF", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case StatusEffectName.CharacterStatBuff:
			case StatusEffectName.HiveCoin:
				break;
			}
		}
		else if (abilityData.type == AbilityType.ActionMeter)
		{
			ActionMeterEffect actionMeterEffect = abilityData.actionMeterEffect;
			if (actionMeterEffect == ActionMeterEffect.Fill)
			{
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("AMOUNT", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			}
		}
		else if (abilityData.type == AbilityType.DropBuff)
		{
			SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("AMOUNT", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
		}
		else if (abilityData.type == AbilityType.Custom)
		{
			SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("RARITY", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
		}
		if (abilityData.secondaryStatusEffect == StatusEffectName.None)
		{
			return;
		}
		switch (abilityData.secondaryStatusEffect)
		{
		case StatusEffectName.ActionMeterSpeedModifier:
		case StatusEffectName.ProtectionBubble:
		case StatusEffectName.Smokescreen:
		case StatusEffectName.Cute:
		case StatusEffectName.KnockedOut:
		case StatusEffectName.AbilityBlocker:
		case StatusEffectName.Immunity:
		case StatusEffectName.Cuteness:
		case StatusEffectName.HealAndBuffBlocker:
		case StatusEffectName.AmplifyDamage:
		case StatusEffectName.Flight:
		case StatusEffectName.ActionMeterElectrify:
		case StatusEffectName.Root:
		case StatusEffectName.AttackMinion:
		case StatusEffectName.Counterattack:
		case StatusEffectName.ActionMeterHack:
			SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("DURATION", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			break;
		case StatusEffectName.ActionMeterDisabler:
			SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("STUN", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			break;
		case StatusEffectName.AttackDrone:
		case StatusEffectName.Poison:
		case StatusEffectName.RemoteBomb:
			SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("DAMAGE", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			break;
		case StatusEffectName.BuffDropper:
		case StatusEffectName.ActionMeterFiller:
		case StatusEffectName.ActionMeterDepleter:
		case StatusEffectName.ActionMeterFreeze:
		case StatusEffectName.HealOverTime:
		case StatusEffectName.Fear:
			SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("AMOUNT", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			break;
		case StatusEffectName.TeamStatBuff:
			if (abilityData.rangedIntValue1.GetInterpolatedValue(0.5f) > 0f)
			{
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("BUFF", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			}
			else
			{
				SetNextTextLine(string.Format("{0} <sprite name=\"{1}\" tint=1>", Language.Get("DEBUFF", "MENUS,GUI"), (!flag) ? "arrow_down" : "arrow_up"), fontColor);
			}
			break;
		case StatusEffectName.CharacterStatBuff:
		case StatusEffectName.HiveCoin:
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	private void ResetTextLines()
	{
		nextTextLineIndex = 0;
		for (int i = 0; i < textLines.Length; i++)
		{
			textLines[i].gameObject.SetActive(false);
			textLines[i].text = string.Empty;
		}
	}

	private bool SetNextTextLine(string text, Color fontColor)
	{
		if (nextTextLineIndex < textLines.Length)
		{
			textLines[nextTextLineIndex].gameObject.SetActive(true);
			textLines[nextTextLineIndex].color = fontColor;
			textLines[nextTextLineIndex].text = text;
			nextTextLineIndex++;
			return true;
		}
		return false;
	}
}
