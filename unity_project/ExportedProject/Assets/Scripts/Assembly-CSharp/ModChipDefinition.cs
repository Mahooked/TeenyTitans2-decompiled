using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModChipDefinition : ItemDefinition
{
	[SerializeField]
	private int _iconRow;

	[SerializeField]
	private int _iconColumn;

	[SerializeField]
	private string _codeName = string.Empty;

	[SerializeField]
	private string _codeNameKey = string.Empty;

	[SerializeField]
	private ModChipQuality _quality;

	[SerializeField]
	private int _minLevel = 1;

	[SerializeField]
	private int _minShinyRank;

	[SerializeField]
	private int _cost;

	[SerializeField]
	private int _ticketCost;

	[SerializeField]
	private bool _castingRestriction;

	[SerializeField]
	private AbilityType _abilityTypeRestriction = AbilityType.None;

	[SerializeField]
	private List<CharacterClass> _classRestrictions = new List<CharacterClass>();

	[SerializeField]
	private List<ModChipEffect> _effects = new List<ModChipEffect>();

	public int iconRow
	{
		get
		{
			return _iconRow;
		}
	}

	public int iconColumn
	{
		get
		{
			return _iconColumn;
		}
	}

	public string codeName
	{
		get
		{
			return _codeName;
		}
	}

	public string codeNameKey
	{
		get
		{
			return _codeNameKey;
		}
	}

	public ModChipQuality quality
	{
		get
		{
			return _quality;
		}
	}

	public int minLevel
	{
		get
		{
			return _minLevel;
		}
	}

	public int minShinyRank
	{
		get
		{
			return _minShinyRank;
		}
	}

	public int cost
	{
		get
		{
			return _cost;
		}
	}

	public int ticketCost
	{
		get
		{
			return _ticketCost;
		}
	}

	public bool castingRestriction
	{
		get
		{
			return _castingRestriction;
		}
	}

	public AbilityType abilityTypeRestriction
	{
		get
		{
			return _abilityTypeRestriction;
		}
	}

	public List<CharacterClass> classRestrictions
	{
		get
		{
			return _classRestrictions;
		}
	}

	public List<ModChipEffect> effects
	{
		get
		{
			return _effects;
		}
	}
}
