using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelUpScreenButton : MonoBehaviour, IPoolable
{
	private static Dictionary<CharacterStatType, string> statIconSpriteNames = new Dictionary<CharacterStatType, string>
	{
		{
			CharacterStatType.HitPoints,
			"menu_upgrade_icon_health"
		},
		{
			CharacterStatType.Attack,
			"menu_upgrade_icon_strength"
		},
		{
			CharacterStatType.Speed,
			"menu_upgrade_icon_speed"
		},
		{
			CharacterStatType.Luck,
			"menu_upgrade_icon_luck"
		},
		{
			CharacterStatType.Dodge,
			"menu_upgrade_icon_speed"
		}
	};

	private static Dictionary<CharacterStatType, string> statDisplayKeys = new Dictionary<CharacterStatType, string>
	{
		{
			CharacterStatType.HitPoints,
			"HEALTH"
		},
		{
			CharacterStatType.Attack,
			"POWER"
		},
		{
			CharacterStatType.Speed,
			"SPEED"
		},
		{
			CharacterStatType.Luck,
			"LUCK"
		},
		{
			CharacterStatType.Dodge,
			"DODGESTAT"
		}
	};

	public tk2dUIItem uiItem;

	public Collider2D collider2D;

	public Animator animator;

	public tk2dSprite iconBackingSprite;

	public tk2dSprite iconSprite;

	public tk2dSprite modChipSprite;

	public tk2dSprite customizeSprite;

	public TextMeshPro headerText;

	public TextMeshPro numberText;

	public TextMeshPro totalText;

	private Character _character;

	private LevelUpReward _reward;

	public Character character => _character;

	public LevelUpReward reward => _reward;

	public bool ignoreRefresh { get; set; }

	private void Awake()
	{
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
		if (collider2D == null)
		{
			collider2D = GetComponent<Collider2D>();
		}
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
	}

	public void Init(Character character, LevelUpReward reward)
	{
		if (character != _character || reward != _reward)
		{
			if (_character != null)
			{
				_character.Updated -= HandleCharacterUpdated;
			}
			_character = character;
			_reward = reward;
			if (_character != null && _reward != null)
			{
				_character.Updated += HandleCharacterUpdated;
				HandleCharacterUpdated();
			}
		}
	}

	public void Select()
	{
		if (_reward != null && _reward.type == LevelUpRewardType.StatUpgrade)
		{
			totalText.text = string.Format("{0}: <color=#FFFF00>{1}</color>", Language.Get(statDisplayKeys[_reward.statType], "MENUS,GUI"), _character.GetBaseStatPlusOffsetByType(_reward.statType));
			totalText.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.2f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo);
		}
	}

	private void HandleCharacterUpdated()
	{
		Refresh();
	}

	private void Refresh()
	{
		if (ignoreRefresh)
		{
			return;
		}
		iconBackingSprite.gameObject.SetActive(value: false);
		iconSprite.gameObject.SetActive(value: true);
		if (modChipSprite != null)
		{
			modChipSprite.gameObject.SetActive(value: false);
		}
		if (customizeSprite != null)
		{
			customizeSprite.gameObject.SetActive(value: false);
		}
		switch (_reward.type)
		{
		case LevelUpRewardType.StatUpgrade:
			iconSprite.SetSprite(statIconSpriteNames[_reward.statType]);
			headerText.text = Language.Get(statDisplayKeys[_reward.statType], "MENUS,GUI");
			numberText.text = $"+{Mathf.Min(_reward.statValueInt, _character.characterDefinition.GetMaxStatByType(_reward.statType) - _character.GetBaseStatPlusOffsetByType(_reward.statType))}";
			totalText.text = string.Format("{0}: {1}", Language.Get(statDisplayKeys[_reward.statType], "MENUS,GUI"), _character.GetBaseStatPlusOffsetByType(_reward.statType));
			break;
		case LevelUpRewardType.AbilityUnlock:
		{
			AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_character.characterDefinition.abilitySlots.GetLastItem().abilityId);
			iconBackingSprite.gameObject.SetActive(value: true);
			iconSprite.SetSprite(ability.iconSpriteId);
			headerText.text = Language.Get(ability.abilityNameKey, "ABILS,CHIPS,ACHVS,LDBS");
			numberText.text = TTGParseHelper.GetAbilityEffectText(ability, _character, _character.characterDefinition.abilitySlots.GetLastItem().position);
			totalText.text = string.Empty;
			break;
		}
		case LevelUpRewardType.ModChipUnlock:
			if (modChipSprite != null)
			{
				modChipSprite.gameObject.SetActive(value: true);
			}
			iconSprite.gameObject.SetActive(value: false);
			headerText.text = Language.Get("FIGURECUSTOM", "MENUS,GUI");
			numberText.text = Language.Get("FIGURECUSTOM2", "MENUS,GUI");
			totalText.text = string.Empty;
			break;
		case LevelUpRewardType.CustomizationUnlock:
			if (customizeSprite != null)
			{
				customizeSprite.gameObject.SetActive(value: true);
			}
			iconSprite.gameObject.SetActive(value: false);
			headerText.text = Language.Get("ABILITYCUSTOM", "MENUS,GUI");
			numberText.text = Language.Get("ABILITYCUSTOM2", "MENUS,GUI");
			totalText.text = string.Empty;
			break;
		}
	}

	public void ResetObject()
	{
		transform.localScale = Vector3.one;
		collider2D.enabled = true;
	}
}
