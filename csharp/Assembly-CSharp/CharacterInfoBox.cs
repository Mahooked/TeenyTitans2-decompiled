using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfoBox : MonoBehaviour
{
	public Camera thisCamera;

	public Transform mainBox;

	public tk2dSlicedSprite backingSprite;

	public TextMeshPro healthText;

	public TextMeshPro attackText;

	public TextMeshPro speedText;

	public TextMeshPro luckText;

	public List<CharacterViewAbilityButton> abilityButtons;

	public AbilityInfoBox abilityInfoBox;

	public tk2dSprite classIconSprite;

	public tk2dUIItem classIconButton;

	public ClassBonusBox classBonusBox;

	public Transform modChipContainer;

	public TextMeshPro modChipDescriptionText;

	public tk2dUIItem touchCover;

	private Transform thisTransform;

	private Vector3 prevPosition;

	private ModChipSelectPortrait modChipPortrait;

	private Character _character;

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		if (abilityInfoBox.gameObject.activeSelf)
		{
			abilityInfoBox.gameObject.SetActive(value: false);
		}
		if (classBonusBox.gameObject.activeSelf)
		{
			classBonusBox.gameObject.SetActive(value: false);
		}
		thisTransform = transform;
	}

	private void OnEnable()
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].uiItem.OnClickUIItem += HandleOnAbilityButtonClick;
		}
		classIconButton.OnClick += HandleOnClassIconButtonClick;
	}

	private void OnDisable()
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].uiItem.OnClickUIItem -= HandleOnAbilityButtonClick;
		}
		classIconButton.OnClick -= HandleOnClassIconButtonClick;
	}

	private void OnDestroy()
	{
		_character = null;
	}

	private void LateUpdate()
	{
		if (thisTransform.position != prevPosition)
		{
			Vector3 localPosition = mainBox.localPosition;
			localPosition.x = 0f;
			mainBox.localPosition = localPosition;
			Vector3 vector = backingSprite.transform.position - new Vector3(backingSprite.dimensions.x * 0.5f, 0f);
			Vector3 vector2 = backingSprite.transform.position + new Vector3(backingSprite.dimensions.x * 0.5f, 0f);
			Vector3 vector3 = thisCamera.WorldToScreenPoint(vector);
			Vector3 vector4 = thisCamera.WorldToScreenPoint(vector2);
			if (vector3.x < 5f)
			{
				Vector3 vector5 = thisCamera.ScreenToWorldPoint(new Vector3(5f, vector3.y, vector3.z));
				localPosition.x += (vector5 - vector).x;
				mainBox.localPosition = localPosition;
			}
			else if (vector4.x > (float)(Screen.width - 5))
			{
				Vector3 vector6 = thisCamera.ScreenToWorldPoint(new Vector3(Screen.width - 5, vector4.y, vector4.z));
				localPosition.x += (vector6 - vector2).x;
				mainBox.localPosition = localPosition;
			}
		}
		prevPosition = thisTransform.position;
	}

	public void Init(Character character)
	{
		if (character != _character)
		{
			_character = character;
			HandleCloseAbilityInfo();
			HandleCloseClassBonusBox();
			if (_character != null)
			{
				UpdateView();
			}
		}
	}

	public void EnableButtons(bool enable = true)
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].uiItem.enabled = enable;
		}
		classIconButton.enabled = enable;
	}

	private void UpdateView()
	{
		healthText.text = (_character.maxHitPoints + _character.characterDefinition.baseHitPointsOffset).ToString();
		attackText.text = (_character.attack + _character.characterDefinition.baseAttackOffset).ToString();
		speedText.text = (_character.dodge + _character.characterDefinition.baseDodgeOffset).ToString();
		luckText.text = (_character.luck + _character.characterDefinition.baseLuckOffset).ToString();
		for (int i = 0; i < abilityButtons.Count; i++)
		{
			if (i < _character.abilities.Count)
			{
				abilityButtons[i].Init(_character.abilities[i], _character.IsAbilityShiny(_character.abilities[i]));
			}
			else
			{
				abilityButtons[i].Init(null);
			}
		}
		classIconSprite.SetSprite(ClassBonusBox.classIconSpriteNames[_character.characterDefinition.characterClass]);
		if (_character.modChip == null)
		{
			modChipDescriptionText.text = Language.Get("NOMODCHIP", "MENUS,GUI");
			if (modChipPortrait != null)
			{
				ObjectPool.PoolObject(modChipPortrait.gameObject);
				modChipPortrait = null;
			}
			return;
		}
		modChipDescriptionText.text = TTGParseHelper.GetModChipDescription(_character.modChip);
		if (modChipPortrait == null)
		{
			modChipPortrait = ObjectPool.GetObjectFromResource<ModChipSelectPortrait>("Prefabs/GUI/ModChipGUI");
			modChipPortrait.gameObject.SetLayerRecursively(modChipContainer.gameObject.layer);
			modChipPortrait.transform.parent = modChipContainer;
			modChipPortrait.transform.localPosition = Vector3.zero;
			modChipPortrait.transform.localScale = Vector3.one;
		}
		modChipPortrait.Init(_character.modChip);
	}

	private string GetFormattedModChipDescription(ModChipDefinition modChip)
	{
		int count = modChip.effects.Count;
		string[] array = new string[count];
		int i;
		for (i = 0; i < count; i++)
		{
			ModChipEffect modChipEffect = modChip.effects.Find((ModChipEffect x) => x.effectOrderIntVal == i || x.effectOrderFloatVal == i);
			if (modChipEffect != null)
			{
				string text = string.Empty;
				bool flag = false;
				if (modChipEffect.effectType == ModChipEffectType.CastTimeMultiplier || modChipEffect.effectType == ModChipEffectType.StatMultiplier)
				{
					flag = true;
				}
				if (modChipEffect.effectOrderIntVal == i)
				{
					text = (flag ? (modChipEffect.intValue * 100 + "%") : modChipEffect.intValue.ToString());
				}
				else if (modChipEffect.effectOrderFloatVal == i)
				{
					text = (flag ? (modChipEffect.floatValue * 100f + "%") : modChipEffect.floatValue.ToString());
				}
				array[i] = text;
			}
		}
		return string.Format(Language.Get(modChip.itemDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS").Trim(), array);
	}

	private void HandleOnAbilityButtonClick(tk2dUIItem button)
	{
		HandleCloseClassBonusBox();
		CharacterViewAbilityButton component = button.GetComponent<CharacterViewAbilityButton>();
		if (!(component.ability != null))
		{
			return;
		}
		if (component.ability == abilityInfoBox.abilityData)
		{
			HandleCloseAbilityInfo();
		}
		else
		{
			abilityInfoBox.Init(component.ability, _character);
			Vector3 position = abilityInfoBox.tailSprite.transform.position;
			position.x = component.transform.position.x;
			abilityInfoBox.tailSprite.transform.position = position;
			if (!abilityInfoBox.gameObject.activeSelf)
			{
				if (touchCover != null)
				{
					touchCover.gameObject.SetActive(value: true);
					touchCover.OnDown += HandleCloseAbilityInfo;
				}
				abilityInfoBox.gameObject.SetActive(value: true);
			}
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void HandleCloseAbilityInfo()
	{
		if (abilityInfoBox.gameObject.activeSelf)
		{
			if (touchCover != null)
			{
				touchCover.OnDown -= HandleCloseAbilityInfo;
				touchCover.gameObject.SetActive(value: false);
			}
			abilityInfoBox.gameObject.SetActive(value: false);
			abilityInfoBox.Init(null, null);
		}
	}

	private void HandleOnClassIconButtonClick()
	{
		HandleCloseAbilityInfo();
		if (_character == null)
		{
			return;
		}
		classBonusBox.Init(_character);
		if (!classBonusBox.gameObject.activeSelf)
		{
			if (touchCover != null)
			{
				touchCover.gameObject.SetActive(value: true);
				touchCover.OnDown += HandleCloseClassBonusBox;
			}
			classBonusBox.gameObject.SetActive(value: true);
		}
		else
		{
			HandleCloseClassBonusBox();
		}
	}

	private void HandleCloseClassBonusBox()
	{
		if (classBonusBox.gameObject.activeSelf)
		{
			if (touchCover != null)
			{
				touchCover.OnDown -= HandleCloseClassBonusBox;
				touchCover.gameObject.SetActive(value: false);
			}
			classBonusBox.gameObject.SetActive(value: false);
		}
	}
}
