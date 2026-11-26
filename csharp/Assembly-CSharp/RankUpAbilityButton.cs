using UnityEngine;

public class RankUpAbilityButton : MonoBehaviour
{
	public tk2dSprite backingSprite;

	public tk2dSprite backingGoldSprite;

	public tk2dSprite backingGoldWhiteSprite;

	public tk2dSprite iconSprite;

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

	public void Init(AbilityData ability)
	{
		if (!(ability == null))
		{
			if (ability.castTime > 0f)
			{
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_cast");
				backingGoldSprite.SetSprite("TTG_battlebar_abilityicon_backing_goldcast");
				backingGoldWhiteSprite.SetSprite("TTG_battlebar_abilityicon_backing_goldcast");
			}
			else if (ability.numChargesRequired > 0)
			{
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple");
				backingGoldSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple_gold");
				backingGoldWhiteSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple_gold");
			}
			else if (ability.channelingData.speed > 0f)
			{
				backingSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel");
				backingGoldSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel_gold");
				backingGoldWhiteSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel_gold");
			}
			else
			{
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing");
				backingGoldSprite.SetSprite("TTG_battlebar_abilityicon_backing_gold");
				backingGoldWhiteSprite.SetSprite("TTG_battlebar_abilityicon_backing_gold");
			}
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
			iconSprite.SetSprite(_ability.iconSprite);
		}
	}
}
