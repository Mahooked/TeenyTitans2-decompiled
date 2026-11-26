using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterTransformation
{
	[SerializeField]
	private string _id = string.Empty;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _portraitSprite = string.Empty;

	[SerializeField]
	private int _portraitSpriteId = -1;

	[SerializeField]
	private ActionMeterSize _actionMeterSize;

	[SerializeField]
	private int _actionMeterSpeedRating = 5;

	[SerializeField]
	private List<AbilitySlot> _abilitySlots = new List<AbilitySlot>();

	[SerializeField]
	private BattleCharacterOffsets _battleOffsets = new BattleCharacterOffsets();

	[SerializeField]
	private string _battleCharacterPath = string.Empty;

	[SerializeField]
	private string _battleCharacterPathP2 = string.Empty;

	private List<AbilitySlot> _orderedSlots = new List<AbilitySlot>();

	public string id
	{
		get
		{
			return _id;
		}
	}

	public string name
	{
		get
		{
			return _name;
		}
	}

	public string portraitSprite
	{
		get
		{
			return _portraitSprite;
		}
	}

	public int portraitSpriedId
	{
		get
		{
			return _portraitSpriteId;
		}
	}

	public ActionMeterSize actionMeterSize
	{
		get
		{
			return _actionMeterSize;
		}
	}

	public int actionMeterSpeedRating
	{
		get
		{
			return _actionMeterSpeedRating;
		}
		set
		{
			_actionMeterSpeedRating = value;
		}
	}

	public List<AbilitySlot> abilitySlots
	{
		get
		{
			return _abilitySlots;
		}
	}

	public BattleCharacterOffsets battleOffsets
	{
		get
		{
			return _battleOffsets;
		}
	}

	public string battleCharacterPath
	{
		get
		{
			return _battleCharacterPath;
		}
	}

	public string battleCharacterPathP2
	{
		get
		{
			return _battleCharacterPathP2;
		}
	}

	public List<AbilitySlot> orderedSlots
	{
		get
		{
			if (_orderedSlots == null)
			{
				_orderedSlots = new List<AbilitySlot>();
			}
			_orderedSlots.Clear();
			_orderedSlots.AddRange(_abilitySlots);
			_orderedSlots.Sort((AbilitySlot x, AbilitySlot y) => x.position.CompareTo(y.position));
			return _orderedSlots;
		}
	}

	public CharacterTransformation()
	{
	}

	public CharacterTransformation(string newID)
	{
		_id = newID;
	}

	public CharacterTransformation(CharacterDefinition definition)
	{
		_id = "transformation_" + (definition.transformations.Count + 1);
		_name = definition.characterName + " Transformation";
		_portraitSprite = definition.portraitSprite;
		_portraitSpriteId = definition.portraitSpriedId;
		_actionMeterSize = definition.actionMeterSize;
	}
}
