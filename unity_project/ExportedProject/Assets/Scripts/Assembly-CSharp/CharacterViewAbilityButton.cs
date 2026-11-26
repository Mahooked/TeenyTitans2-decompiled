using UnityEngine;

public class CharacterViewAbilityButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dSprite backingSprite;

	public tk2dSprite iconSprite;

	public tk2dSprite lockSprite;

	private AbilityData _ability;

	public AbilityData ability
	{
		get
		{
			return _ability;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		Refresh();
	}

	public void Init(AbilityData ability, bool isShiny = false)
	{
		if (ability == null)
		{
			backingSprite.SetSprite("TTG_battlebar_abilityicon_backing");
		}
		else if (ability.castTime > 0f)
		{
			backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_" + ((!isShiny) ? "cast" : "goldcast"));
		}
		else if (ability.numChargesRequired > 0)
		{
			backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple" + ((!isShiny) ? string.Empty : "_gold"));
		}
		else if (ability.channelingData.speed > 0f)
		{
			backingSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel" + ((!isShiny) ? string.Empty : "_gold"));
		}
		else
		{
			backingSprite.SetSprite("TTG_battlebar_abilityicon_backing" + ((!isShiny) ? string.Empty : "_gold"));
		}
		if (!(ability == _ability))
		{
			_ability = ability;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (_ability != null)
		{
			iconSprite.gameObject.SetActive(true);
			iconSprite.SetSprite(_ability.iconSpriteId);
			lockSprite.gameObject.SetActive(false);
		}
		else
		{
			iconSprite.gameObject.SetActive(false);
			lockSprite.gameObject.SetActive(true);
		}
	}
}
