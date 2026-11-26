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

	public int iconRow => _iconRow;

	public int iconColumn => _iconColumn;

	public string codeName => _codeName;

	public string codeNameKey => _codeNameKey;

	public ModChipQuality quality => _quality;

	public int minLevel => _minLevel;

	public int minShinyRank => _minShinyRank;

	public int cost => _cost;

	public int ticketCost => _ticketCost;

	public bool castingRestriction => _castingRestriction;

	public AbilityType abilityTypeRestriction => _abilityTypeRestriction;

	public List<CharacterClass> classRestrictions => _classRestrictions;

	public List<ModChipEffect> effects => _effects;
}
