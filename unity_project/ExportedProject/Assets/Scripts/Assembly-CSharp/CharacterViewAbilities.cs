using System.Collections.Generic;
using UnityEngine;

public class CharacterViewAbilities : MonoBehaviour
{
	public List<CharacterViewAbilityButton> buttons;

	public bool trackCharacterUpdates = true;

	private AbilityInfoBox infoBox;

	private Camera buttonCamera;

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

	private void Start()
	{
		buttonCamera = tk2dCamera.CameraForLayer(buttons[0].uiItem.gameObject.layer).ScreenCamera;
	}

	private void OnEnable()
	{
		int count = buttons.Count;
		for (int i = 0; i < count; i++)
		{
			buttons[i].uiItem.OnClickUIItem += HandleAbilityClick;
		}
	}

	private void OnDisable()
	{
		int count = buttons.Count;
		for (int i = 0; i < count; i++)
		{
			buttons[i].uiItem.OnClickUIItem -= HandleAbilityClick;
		}
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
			_character = null;
		}
	}

	private void Update()
	{
		if (!(infoBox != null) || !Input.GetMouseButtonUp(0))
		{
			return;
		}
		Collider2D collider2D = Physics2D.OverlapPoint(buttonCamera.ScreenToWorldPoint(Input.mousePosition));
		if (collider2D != null)
		{
			tk2dUIItem uiItem = collider2D.GetComponent<tk2dUIItem>();
			if (uiItem == null || buttons.Find((CharacterViewAbilityButton obj) => obj.uiItem == uiItem) == null)
			{
				HideInfoBox();
			}
		}
		else
		{
			HideInfoBox();
		}
	}

	public void Init(Character character)
	{
		if (character == _character)
		{
			return;
		}
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
		_character = character;
		if (_character != null)
		{
			if (trackCharacterUpdates)
			{
				_character.Updated += HandleCharacterUpdated;
			}
			HandleCharacterUpdated();
		}
	}

	public void ForceRefresh()
	{
		if (_character != null)
		{
			HandleCharacterUpdated();
		}
	}

	public void EnableButtons(bool enable = true)
	{
		int count = buttons.Count;
		for (int i = 0; i < count; i++)
		{
			buttons[i].uiItem.enabled = enable;
		}
	}

	public void SetInteractive(bool interactive)
	{
		EnableButtons(interactive);
		Color color = ((!interactive) ? Color.gray : Color.white);
		int count = buttons.Count;
		for (int i = 0; i < count; i++)
		{
			CharacterViewAbilityButton characterViewAbilityButton = buttons[i];
			characterViewAbilityButton.iconSprite.color = color;
			characterViewAbilityButton.backingSprite.color = color;
		}
	}

	public void HideInfoBox()
	{
		if (infoBox != null)
		{
			infoBox.Init(null, null);
			ObjectPool.PoolObject(infoBox.gameObject);
			infoBox = null;
		}
	}

	private void HandleCharacterUpdated()
	{
		int num = 0;
		for (int i = 0; i < buttons.Count; i++)
		{
			if (_character.abilities.Contains(GameManager.instance.abilityDatabase.GetAbility(_character.characterDefinition.orderedSlots[i].abilityId)))
			{
				buttons[i].Init(_character.abilities[num], _character.IsAbilityShiny(_character.abilities[num]));
				num++;
			}
			else
			{
				buttons[i].Init(null);
			}
		}
	}

	private void HandleAbilityClick(tk2dUIItem uiItem)
	{
		CharacterViewAbilityButton component = uiItem.GetComponent<CharacterViewAbilityButton>();
		if (component != null)
		{
			ShowInfoBox(component);
		}
	}

	private void ShowInfoBox(CharacterViewAbilityButton button)
	{
		if (infoBox == null)
		{
			infoBox = ObjectPool.GetObjectFromResource<AbilityInfoBox>("Prefabs/GUI/AbilityInfoBox_CharacterView");
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		if (infoBox.abilityData == button.ability || button.ability == null)
		{
			HideInfoBox();
			return;
		}
		infoBox.Init(button.ability, _character);
		infoBox.transform.parent = button.transform;
		infoBox.transform.localPosition = new Vector3(0f, 0f, 6f);
		infoBox.transform.localRotation = Quaternion.identity;
		infoBox.transform.localScale = Vector3.one;
	}
}
