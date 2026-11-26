using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelectPortrait : MonoBehaviour
{
	public enum ViewMode
	{
		Single = 0,
		Folder = 1
	}

	public Animator animator;

	public tk2dUIItem uiItem;

	public tk2dSprite portraitSprite;

	public tk2dSprite equippedSprite;

	public tk2dSprite xpBackSprite;

	public tk2dSlicedSpriteHelper xpFillSprite;

	public TextMeshPro levelText;

	public TextMeshPro questionMarkText;

	public tk2dSprite favoriteIcon;

	public tk2dSprite repaintIcon;

	public tk2dSprite repaintIconBacking;

	public Transform newTag;

	public tk2dSprite newIcon;

	public float xpFillMaxWidth;

	public tk2dSprite folderIcon;

	public TextMeshPro folderNumText;

	public TextMeshPro xText;

	private bool isPlayerCharacter = true;

	private CharacterSelectPortraitMode _mode;

	private Character _character;

	private CharacterDefinition _characterDefinition;

	public CharacterSelectPortraitMode mode
	{
		get
		{
			return _mode;
		}
		set
		{
			_mode = value;
			UpdateMode();
		}
	}

	public ViewMode viewMode { get; private set; }

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

	public CharacterDefinition characterDefinition
	{
		get
		{
			return _characterDefinition;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		questionMarkText.gameObject.SetActive(false);
		newIcon.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
		}
	}

	public void Init(Character newCharacter)
	{
		if (!isPlayerCharacter || newCharacter != _character)
		{
			if (_character != null)
			{
				_character.Updated -= HandleUpdated;
			}
			isPlayerCharacter = true;
			_character = newCharacter;
			_characterDefinition = null;
			if (_character != null)
			{
				_character.Updated += HandleUpdated;
				folderIcon.gameObject.SetActive(false);
				HandleUpdated();
				viewMode = ViewMode.Single;
			}
		}
	}

	public void Init(CharacterDefinition newCharacterDefinition)
	{
		if (isPlayerCharacter || !(newCharacterDefinition == _characterDefinition))
		{
			isPlayerCharacter = false;
			_characterDefinition = newCharacterDefinition;
			if (_character != null)
			{
				_character.Updated -= HandleUpdated;
			}
			_character = null;
			if (_characterDefinition != null)
			{
				portraitSprite.SetSprite(_characterDefinition.portraitSpriedId);
				levelText.gameObject.SetActive(false);
				xpBackSprite.gameObject.SetActive(false);
				xpFillSprite.gameObject.SetActive(false);
				favoriteIcon.gameObject.SetActive(false);
				newIcon.gameObject.SetActive(false);
				folderIcon.gameObject.SetActive(false);
				viewMode = ViewMode.Single;
			}
		}
	}

	public void InitAsFolder(CharacterDefinition newCharacterDefinition, int characterCount)
	{
		if (newCharacterDefinition == _characterDefinition)
		{
			FolderNewIconCheck(newCharacterDefinition);
			return;
		}
		isPlayerCharacter = false;
		_characterDefinition = newCharacterDefinition;
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
		}
		_character = null;
		if (_characterDefinition != null)
		{
			FolderNewIconCheck(newCharacterDefinition);
			portraitSprite.SetSprite(_characterDefinition.portraitSpriedId);
			levelText.gameObject.SetActive(false);
			xpBackSprite.gameObject.SetActive(false);
			xpFillSprite.gameObject.SetActive(false);
			favoriteIcon.gameObject.SetActive(false);
			repaintIcon.gameObject.SetActive(false);
			repaintIconBacking.gameObject.SetActive(false);
			favoriteIcon.gameObject.SetActive(false);
			folderIcon.gameObject.SetActive(true);
			folderNumText.text = characterCount.ToString();
			viewMode = ViewMode.Folder;
		}
	}

	private void FolderNewIconCheck(CharacterDefinition newCharacterDefinition)
	{
		List<Character> list = Player.instance.characters.FindAll((Character x) => x != null && x.characterDefinition.uid == newCharacterDefinition.uid);
		int count = list.Count;
		bool active = false;
		for (int num = 0; num < count; num++)
		{
			if (!list[num].isViewed)
			{
				active = true;
				break;
			}
		}
		newIcon.gameObject.SetActive(active);
	}

	private void HandleUpdated()
	{
		portraitSprite.SetSprite(_character.characterDefinition.portraitSpriedId);
		levelText.gameObject.SetActive(true);
		levelText.text = Language.Get("LVL", "MENUS,GUI") + " " + _character.level;
		levelText.color = ((_character.level < _character.levelTable.maxLevel) ? Color.white : TTGColors.MAX_LEVEL_TEXT_COLOR);
		xpBackSprite.gameObject.SetActive(true);
		xpFillSprite.gameObject.SetActive(true);
		xpFillSprite.width = ((!_character.isMaxLevel) ? (xpFillMaxWidth * _character.currentLevelProgress) : xpFillMaxWidth);
		favoriteIcon.gameObject.SetActive(character.isFavorite);
		bool flag = character.repaintColorID > 0;
		repaintIcon.gameObject.SetActive(flag);
		repaintIconBacking.gameObject.SetActive(flag);
		if (flag)
		{
			repaintIcon.SetSprite(character.characterDefinition.GetRepaintColorSlotForID(character.repaintColorID).colorSprite);
			repaintIcon.transform.position = favoriteIcon.transform.position + ((!character.isFavorite) ? Vector3.zero : new Vector3(0f, -21f));
			repaintIconBacking.transform.position = repaintIcon.transform.position + new Vector3(0f, 0f, 1f);
		}
		newIcon.gameObject.SetActive(!character.isViewed);
	}

	private void UpdateMode()
	{
		questionMarkText.gameObject.SetActive(false);
		equippedSprite.gameObject.SetActive(false);
		portraitSprite.color = Color.white;
		switch (_mode)
		{
		case CharacterSelectPortraitMode.Equipped:
			equippedSprite.gameObject.SetActive(true);
			break;
		case CharacterSelectPortraitMode.Unknown:
			portraitSprite.color = Color.black;
			questionMarkText.gameObject.SetActive(true);
			break;
		}
	}
}
