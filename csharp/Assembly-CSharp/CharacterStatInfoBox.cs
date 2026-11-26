using TMPro;
using UnityEngine;

public class CharacterStatInfoBox : MonoBehaviour
{
	public Animator animator;

	public TextMeshPro headerTextMesh;

	public TextMeshPro messageTextMesh;

	private CharacterViewStat _characterViewStat;

	public CharacterViewStat characterViewStat
	{
		get
		{
			return _characterViewStat;
		}
		set
		{
			Init(value);
		}
	}

	public void Init(CharacterViewStat stat)
	{
		if (stat == _characterViewStat)
		{
			return;
		}
		_characterViewStat = stat;
		if (_characterViewStat != null)
		{
			headerTextMesh.text = _characterViewStat.GetLocalizedStatType().ToUpper();
			switch (stat.statType)
			{
			case CharacterStatType.HitPoints:
				messageTextMesh.text = Language.Get(GameManager.instance.gameSettings.healthStatDescriptionKey, "MENUS,GUI");
				break;
			case CharacterStatType.Attack:
				messageTextMesh.text = Language.Get(GameManager.instance.gameSettings.attackStatDescriptionKey, "MENUS,GUI");
				break;
			case CharacterStatType.Dodge:
				messageTextMesh.text = Language.Get(GameManager.instance.gameSettings.dodgeStatDescriptionKey, "MENUS,GUI");
				break;
			case CharacterStatType.Luck:
				messageTextMesh.text = Language.Get(GameManager.instance.gameSettings.luckStatDescriptionKey, "MENUS,GUI");
				break;
			case CharacterStatType.Speed:
				break;
			}
		}
	}
}
