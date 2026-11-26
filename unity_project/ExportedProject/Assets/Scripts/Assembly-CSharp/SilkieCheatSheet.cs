using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SilkieCheatSheet : MonoBehaviour, IPoolable
{
	public tk2dSprite largePortraitSprite;

	public tk2dSprite smallPortraitSprite;

	public tk2dSprite classIconSprite;

	public TextMeshPro toEnhanceText;

	public TextMeshPro feedMeText;

	public GameObject questionMarkObj;

	public GameObject enhanceTable;

	public tk2dSprite[] remainingClassIconSprites = new tk2dSprite[5];

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
		if (_character == null)
		{
			return;
		}
		largePortraitSprite.SetSprite(_character.characterDefinition.portraitSprite);
		smallPortraitSprite.SetSprite(_character.characterDefinition.portraitSprite);
		if (_character.characterDefinition.characterClass != CharacterClass.None)
		{
			questionMarkObj.SetActive(false);
			enhanceTable.SetActive(true);
			classIconSprite.SetSprite(ClassBonusBox.classIconSpriteNames[_character.characterDefinition.characterClass]);
			Dictionary<CharacterClass, string> dictionary = new Dictionary<CharacterClass, string>(ClassBonusBox.classIconSpriteNames);
			dictionary.Remove(_character.characterDefinition.characterClass);
			List<string> list = new List<string>(dictionary.Values);
			tk2dSprite[] array = remainingClassIconSprites;
			foreach (tk2dSprite tk2dSprite2 in array)
			{
				tk2dSprite2.SetSprite(list[0]);
				list.RemoveAt(0);
			}
		}
		else
		{
			questionMarkObj.SetActive(true);
			enhanceTable.SetActive(false);
		}
	}

	public void ResetObject()
	{
		base.transform.localScale = Vector3.one;
	}
}
