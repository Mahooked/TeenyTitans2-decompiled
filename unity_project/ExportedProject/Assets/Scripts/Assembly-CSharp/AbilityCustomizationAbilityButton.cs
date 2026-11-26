using UnityEngine;

public class AbilityCustomizationAbilityButton : MonoBehaviour
{
	public tk2dSprite backingSprite;

	public tk2dSprite iconSprite;

	public tk2dSprite lockSprite;

	public tk2dUIItem uiItem;

	public Transform buttonContainer;

	public AbilityButtonNumber abilityButtonNumber;

	public BoxCollider2D boxCollider2D;

	public bool overrideNumberColors;

	public Color normalColor = Color.white;

	public Color bonusColor;

	public Color reducedColor;

	private Character _character;

	private AbilityData _abilityData;

	private bool _isEnabled = true;

	public Character character
	{
		get
		{
			return _character;
		}
	}

	public AbilityData abilityData
	{
		get
		{
			return _abilityData;
		}
		set
		{
			Init(_character, value);
		}
	}

	public bool isShinyAbility
	{
		get
		{
			if (_character != null && _abilityData != null)
			{
				return _character.IsAbilityShiny(_abilityData);
			}
			return false;
		}
	}

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			SetEnabled(value);
		}
	}

	public int originalPosition { get; set; }

	public int position { get; set; }

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
	}

	public void Init(Character character, AbilityData abilityData)
	{
		if (character != _character || !(abilityData == _abilityData))
		{
			if (_character != null)
			{
				_character.Updated -= HandleCharacterUpdated;
			}
			_character = character;
			_abilityData = abilityData;
			if (_character != null && _abilityData != null)
			{
				iconSprite.gameObject.SetActive(true);
				lockSprite.gameObject.SetActive(false);
				abilityButtonNumber.gameObject.SetActive(true);
				boxCollider2D.enabled = _isEnabled;
				iconSprite.SetSprite(_abilityData.iconSprite);
				ResetSprites();
			}
			else
			{
				iconSprite.gameObject.SetActive(false);
				lockSprite.gameObject.SetActive(true);
				abilityButtonNumber.gameObject.SetActive(false);
				boxCollider2D.enabled = false;
			}
			if (_character != null)
			{
				_character.Updated += HandleCharacterUpdated;
				HandleCharacterUpdated();
			}
		}
	}

	public void SetEnabled(bool enabled)
	{
		if (enabled != _isEnabled)
		{
			_isEnabled = enabled;
			if (_isEnabled)
			{
				backingSprite.color = Color.white;
				iconSprite.color = Color.white;
				lockSprite.color = Color.white;
				boxCollider2D.enabled = true;
			}
			else
			{
				backingSprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);
				iconSprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);
				lockSprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);
				boxCollider2D.enabled = false;
			}
		}
	}

	public void ResetSprites()
	{
		if (!(abilityData == null))
		{
			bool flag = isShinyAbility;
			if (abilityData.castTime > 0f)
			{
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_" + ((!flag) ? "cast" : "goldcast"));
			}
			else if (abilityData.numChargesRequired > 0)
			{
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple" + ((!flag) ? string.Empty : "_gold"));
			}
			else if (abilityData.channelingData.speed > 0f)
			{
				backingSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel" + ((!flag) ? string.Empty : "_gold"));
			}
			else
			{
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing" + ((!flag) ? string.Empty : "_gold"));
			}
		}
	}

	public void ForceUpdate()
	{
		HandleCharacterUpdated();
	}

	private void HandleCharacterUpdated()
	{
		if (base.gameObject == null || !base.gameObject.activeSelf || _character == null || _abilityData == null)
		{
			return;
		}
		if (_abilityData.type == AbilityType.Attack || _abilityData.type == AbilityType.Heal || (_abilityData.type == AbilityType.StatusEffect && (_abilityData.statusEffect == StatusEffectName.AttackDrone || _abilityData.statusEffect == StatusEffectName.Poison)))
		{
			abilityButtonNumber.Init(_abilityData, _character, BattleController.instance, position);
			abilityButtonNumber.gameObject.SetActive(_abilityData.type == AbilityType.Attack || _abilityData.type == AbilityType.Heal);
			if (overrideNumberColors)
			{
				Color color = ((position > originalPosition) ? bonusColor : ((position >= originalPosition) ? normalColor : reducedColor));
				abilityButtonNumber.mainNumberText.color = color;
				abilityButtonNumber.leftNumberText.color = color;
				abilityButtonNumber.rightNumberText.color = color;
				abilityButtonNumber.leftPlus.color = color;
				abilityButtonNumber.rightPlus.color = color;
			}
		}
		else
		{
			abilityButtonNumber.gameObject.SetActive(false);
		}
	}
}
