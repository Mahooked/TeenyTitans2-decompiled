using System;
using UnityEngine;

[Serializable]
public class MissionTypeConfigBuy
{
	[Serializable]
	public class FigureConfig
	{
		public enum Type
		{
			Name = 0,
			Class = 1,
			Tag = 2,
			Level = 3,
			Rarity = 4
		}

		[SerializeField]
		private Type _type;

		[SerializeField]
		private int[] _validUids;

		[SerializeField]
		private string[] _validTags;

		[SerializeField]
		private int _intValue1;

		[SerializeField]
		private Comparison _comparison1;

		public Type type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		public int[] validUids
		{
			get
			{
				return _validUids;
			}
			set
			{
				_validUids = value;
			}
		}

		public string[] validTags
		{
			get
			{
				return _validTags;
			}
			set
			{
				_validTags = value;
			}
		}

		public int intValue1
		{
			get
			{
				return _intValue1;
			}
			set
			{
				_intValue1 = value;
			}
		}

		public Comparison comparison1
		{
			get
			{
				return _comparison1;
			}
			set
			{
				_comparison1 = value;
			}
		}

		public bool IsCharacterValid(Character character)
		{
			switch (_type)
			{
			case Type.Name:
			{
				for (int j = 0; j < _validUids.Length; j++)
				{
					if (character.characterDefinition.uid == _validUids[j])
					{
						return true;
					}
				}
				break;
			}
			case Type.Class:
			{
				for (int k = 0; k < _validUids.Length; k++)
				{
					if (character.characterDefinition.characterClass == (CharacterClass)_validUids[k])
					{
						return true;
					}
				}
				break;
			}
			case Type.Tag:
			{
				for (int l = 0; l < _validTags.Length; l++)
				{
					if (character.characterDefinition.tags.Contains(_validTags[l]))
					{
						return true;
					}
				}
				break;
			}
			case Type.Level:
				if (ComparisonUtil.IsComparisonValid(character.level, _comparison1, _intValue1))
				{
					return true;
				}
				break;
			case Type.Rarity:
			{
				for (int i = 0; i < _validUids.Length; i++)
				{
					if (character.characterDefinition.characterRarity == (CharacterRarity)_validUids[i])
					{
						return true;
					}
				}
				break;
			}
			}
			return false;
		}

		public bool IsCharacterValid(int uid, int level)
		{
			CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(uid);
			switch (_type)
			{
			case Type.Name:
			{
				for (int j = 0; j < _validUids.Length; j++)
				{
					if (uid == _validUids[j])
					{
						return true;
					}
				}
				break;
			}
			case Type.Class:
			{
				for (int k = 0; k < _validUids.Length; k++)
				{
					if (character.characterClass == (CharacterClass)_validUids[k])
					{
						return true;
					}
				}
				break;
			}
			case Type.Tag:
			{
				for (int l = 0; l < _validTags.Length; l++)
				{
					if (character.tags.Contains(_validTags[l]))
					{
						return true;
					}
				}
				break;
			}
			case Type.Level:
				if (ComparisonUtil.IsComparisonValid(level, _comparison1, _intValue1))
				{
					return true;
				}
				break;
			case Type.Rarity:
			{
				for (int i = 0; i < _validUids.Length; i++)
				{
					if (character.characterRarity == (CharacterRarity)_validUids[i])
					{
						return true;
					}
				}
				break;
			}
			}
			return false;
		}

		public FigureConfig Copy()
		{
			FigureConfig figureConfig = new FigureConfig();
			figureConfig._type = _type;
			if (_validUids != null)
			{
				figureConfig._validUids = new int[_validUids.Length];
				Array.Copy(_validUids, figureConfig._validUids, _validUids.Length);
			}
			if (_validTags != null)
			{
				figureConfig._validTags = new string[_validTags.Length];
				Array.Copy(_validTags, figureConfig._validTags, _validTags.Length);
			}
			figureConfig._intValue1 = _intValue1;
			figureConfig._comparison1 = _comparison1;
			return figureConfig;
		}
	}

	[SerializeField]
	private ToyType _toyType;

	[SerializeField]
	private int _count;

	[SerializeField]
	private bool _mintInBox = true;

	[SerializeField]
	private FigureConfig _figureConfig;

	[SerializeField]
	private int _intValue1;

	[SerializeField]
	private bool _boolValue1;

	public ToyType toyType
	{
		get
		{
			return _toyType;
		}
		set
		{
			_toyType = value;
		}
	}

	public int count
	{
		get
		{
			return _count;
		}
		set
		{
			_count = value;
		}
	}

	public bool mintInBox
	{
		get
		{
			return _mintInBox;
		}
		set
		{
			_mintInBox = value;
		}
	}

	public FigureConfig figureConfig
	{
		get
		{
			return _figureConfig;
		}
		set
		{
			_figureConfig = value;
		}
	}

	public int intValue1
	{
		get
		{
			return _intValue1;
		}
		set
		{
			_intValue1 = value;
		}
	}

	public bool boolValue1
	{
		get
		{
			return _boolValue1;
		}
		set
		{
			_boolValue1 = value;
		}
	}

	public MissionTypeConfigBuy Copy()
	{
		MissionTypeConfigBuy missionTypeConfigBuy = new MissionTypeConfigBuy();
		missionTypeConfigBuy._toyType = _toyType;
		missionTypeConfigBuy._count = _count;
		missionTypeConfigBuy._mintInBox = _mintInBox;
		missionTypeConfigBuy._figureConfig = ((_figureConfig == null) ? null : _figureConfig.Copy());
		missionTypeConfigBuy._intValue1 = _intValue1;
		return missionTypeConfigBuy;
	}
}
