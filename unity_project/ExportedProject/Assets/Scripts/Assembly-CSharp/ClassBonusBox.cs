using System.Collections.Generic;
using UnityEngine;

public class ClassBonusBox : MonoBehaviour
{
	public static Dictionary<CharacterClass, string> classIconSpriteNames = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"menu_classdescription_icon_beast"
		},
		{
			CharacterClass.Cute,
			"menu_classdescription_icon_cute"
		},
		{
			CharacterClass.DarkArts,
			"menu_classdescription_icon_darkarts"
		},
		{
			CharacterClass.MartialArts,
			"menu_classdescription_icon_martialarts"
		},
		{
			CharacterClass.Super,
			"menu_classdescription_icon_super"
		},
		{
			CharacterClass.Tech,
			"menu_classdescription_icon_tech"
		},
		{
			CharacterClass.None,
			"menu_classdescription_icon_none"
		}
	};

	public tk2dSlicedSprite backingSprite;

	public tk2dSprite tailSprite;

	public tk2dSprite classIconGoodSprite;

	public tk2dSprite classIconBadSprite;

	private Character _character;

	private void Awake()
	{
		Refresh();
	}

	public void Init(Character character)
	{
		if (character != _character)
		{
			_character = character;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (_character != null && _character.characterDefinition.characterClass != CharacterClass.None)
		{
			CharacterClass key = AbilityScript.classStrengths[_character.characterDefinition.characterClass];
			classIconGoodSprite.SetSprite(classIconSpriteNames[key]);
			CharacterClass key2 = AbilityScript.classWeaknesses[_character.characterDefinition.characterClass];
			classIconBadSprite.SetSprite(classIconSpriteNames[key2]);
		}
	}
}
