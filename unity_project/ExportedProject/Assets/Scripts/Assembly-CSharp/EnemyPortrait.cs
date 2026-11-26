using TMPro;
using UnityEngine;

public class EnemyPortrait : MonoBehaviour
{
	public tk2dSprite portraitSprite;

	public TextMeshPro levelText;

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
			if (_character != null)
			{
				portraitSprite.SetSprite(character.characterDefinition.portraitSpriedId);
				levelText.text = Language.Get("LVL", "MENUS,GUI") + " " + _character.level;
				levelText.color = ((_character.level < _character.levelTable.maxLevel) ? Color.white : TTGColors.MAX_LEVEL_TEXT_COLOR);
			}
		}
	}
}
