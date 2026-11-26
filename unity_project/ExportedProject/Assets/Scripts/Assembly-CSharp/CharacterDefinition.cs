using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterDefinition : ScriptableObject
{
	private List<AbilitySlot> _orderedSlots = new List<AbilitySlot>();

	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	[SerializeField]
	private string _characterName = string.Empty;

	[SerializeField]
	private string _characterNameKey = string.Empty;

	[SerializeField]
	private CharacterClass _characterClass;

	[SerializeField]
	private CharacterRarity _characterRarity;

	[SerializeField]
	private CharacterFaction _characterFaction;

	[SerializeField]
	private int _baseHitPoints = 20;

	[SerializeField]
	private int _baseAttack = 5;

	[SerializeField]
	private int _baseLuck;

	[SerializeField]
	private int _baseDodge;

	[SerializeField]
	private int _maxHitPoints = 50;

	[SerializeField]
	private int _maxAttack = 20;

	[SerializeField]
	private int _maxSpeed = 50;

	[SerializeField]
	private int _maxLuck = 30;

	[SerializeField]
	private int _maxDodge = 30;

	[SerializeField]
	private int _baseHitPointsOffset;

	[SerializeField]
	private int _baseAttackOffset;

	[SerializeField]
	private int _baseSpeedOffset;

	[SerializeField]
	private int _baseLuckOffset;

	[SerializeField]
	private int _baseDodgeOffset;

	[SerializeField]
	[HideInInspector]
	private int _baseDefense;

	[SerializeField]
	private int _baseSpeed = 5;

	[SerializeField]
	private bool _isActive = true;

	[SerializeField]
	private bool _canUseInBattle;

	[SerializeField]
	private bool _isSecret;

	[SerializeField]
	private LevelTable _levelTable;

	[SerializeField]
	private int _libraryNumber;

	[SerializeField]
	private string _libraryDescription = string.Empty;

	[SerializeField]
	private string _libraryDescriptionKey = string.Empty;

	[SerializeField]
	private string _shopDescription = string.Empty;

	[SerializeField]
	private string _shopDescriptionKey = string.Empty;

	[SerializeField]
	private List<string> _tags = new List<string>();

	[SerializeField]
	private ActionMeterSize _actionMeterSize = ActionMeterSize.Medium;

	[SerializeField]
	private int _actionMeterSpeedRating = 5;

	[SerializeField]
	[HideInInspector]
	private List<AbilitySlot> _abilitySlots = new List<AbilitySlot>();

	[SerializeField]
	private string _portraitSprite = string.Empty;

	[SerializeField]
	private int _portraitSpriteId = -1;

	[SerializeField]
	private string _iapShopIconSprite = string.Empty;

	[SerializeField]
	private int _iapShopIconSpriteId;

	[SerializeField]
	private Vector3 _playerCameraOffsetPosition;

	[SerializeField]
	private Vector3 _playerCameraOffsetRotation;

	[SerializeField]
	private Vector3 _playerCameraOffsetPositionZoomedOut;

	[SerializeField]
	private Vector3 _playerCameraOffsetRotationZoomedOut;

	[SerializeField]
	private Vector3 _playerFlightCameraOffsetPosition;

	[SerializeField]
	private Vector3 _playerFlightCameraOffsetRotation;

	[SerializeField]
	private Vector3 _playerFlightCameraOffsetPositionZoomedOut;

	[SerializeField]
	private Vector3 _playerFlightCameraOffsetRotationZoomedOut;

	[SerializeField]
	private Vector3 _enemyCameraOffsetPosition;

	[SerializeField]
	private Vector3 _enemyCameraOffsetRotation;

	[SerializeField]
	private Vector3 _enemyCameraOffsetPositionZoomedOut;

	[SerializeField]
	private Vector3 _enemyCameraOffsetRotationZoomedOut;

	[SerializeField]
	private Vector3 _enemyFlightCameraOffsetPosition;

	[SerializeField]
	private Vector3 _enemyFlightCameraOffsetRotation;

	[SerializeField]
	private Vector3 _enemyFlightCameraOffsetPositionZoomedOut;

	[SerializeField]
	private Vector3 _enemyFlightCameraOffsetRotationZoomedOut;

	[SerializeField]
	private Vector3 _topRightPortraitOffset;

	[SerializeField]
	private Vector3 _bottomLeftPortraitOffset;

	[SerializeField]
	private Vector3 _tofuBubbleOffsetPosition;

	[SerializeField]
	private string _battleCharacterPath = string.Empty;

	[SerializeField]
	private string _battleCharacterPathP2 = string.Empty;

	[SerializeField]
	private string _shopFigurePath = string.Empty;

	[SerializeField]
	private string _shopFigureBoxPath = string.Empty;

	[SerializeField]
	private Vector3 _shopFigureScale = Vector3.one;

	[SerializeField]
	private Vector3 _shopFigureOffset;

	[SerializeField]
	private Vector3 _shopFigureRotationOffset = Vector3.zero;

	[SerializeField]
	private int[] _shopFigureCosts = new int[15];

	[SerializeField]
	private int[] _shopFigureTicketCosts = new int[15];

	[SerializeField]
	private Color _shopStripeColor = new Color(1f, 1f, 1f, 1f);

	[SerializeField]
	private Color _shopTextFaceColor = new Color(1f, 1f, 1f, 1f);

	[SerializeField]
	private Color _shopTextOutlineColor = new Color(0f, 0f, 0f, 1f);

	[SerializeField]
	private string _shopRevealNameSoundPath = string.Empty;

	[SerializeField]
	private List<CharacterTransformation> _transformations = new List<CharacterTransformation>();

	[SerializeField]
	private bool _is80sFigure;

	[SerializeField]
	private bool _isVillainFigure;

	[SerializeField]
	private BattleCharacterSounds _sounds;

	[SerializeField]
	private List<RepaintColorSlot> _repaintSlots = new List<RepaintColorSlot>();

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

	public int uid
	{
		get
		{
			return _uid;
		}
	}

	public string characterName
	{
		get
		{
			return _characterName;
		}
	}

	public string characterNameKey
	{
		get
		{
			return _characterNameKey;
		}
	}

	public CharacterClass characterClass
	{
		get
		{
			return _characterClass;
		}
	}

	public CharacterRarity characterRarity
	{
		get
		{
			return _characterRarity;
		}
	}

	public CharacterFaction characterFaction
	{
		get
		{
			return _characterFaction;
		}
	}

	public int baseHitPoints
	{
		get
		{
			return _baseHitPoints;
		}
	}

	public int baseAttack
	{
		get
		{
			return _baseAttack;
		}
	}

	public int baseLuck
	{
		get
		{
			return _baseLuck;
		}
	}

	public int baseDodge
	{
		get
		{
			return _baseDodge;
		}
	}

	public int maxHitPoints
	{
		get
		{
			return _maxHitPoints;
		}
	}

	public int maxAttack
	{
		get
		{
			return _maxAttack;
		}
	}

	public int maxSpeed
	{
		get
		{
			return _maxSpeed;
		}
	}

	public int maxLuck
	{
		get
		{
			return _maxLuck;
		}
	}

	public int maxDodge
	{
		get
		{
			return _maxDodge;
		}
	}

	public int baseHitPointsOffset
	{
		get
		{
			return _baseHitPointsOffset;
		}
	}

	public int baseAttackOffset
	{
		get
		{
			return _baseAttackOffset;
		}
	}

	public int baseSpeedOffset
	{
		get
		{
			return _baseSpeedOffset;
		}
	}

	public int baseLuckOffset
	{
		get
		{
			return _baseLuckOffset;
		}
	}

	public int baseDodgeOffset
	{
		get
		{
			return _baseDodgeOffset;
		}
	}

	public int baseDefense
	{
		get
		{
			return _baseDefense;
		}
	}

	public int baseSpeed
	{
		get
		{
			return _baseSpeed;
		}
	}

	public bool isActive
	{
		get
		{
			return _isActive;
		}
	}

	public bool canUseInBattle
	{
		get
		{
			return _canUseInBattle;
		}
	}

	public bool isSecret
	{
		get
		{
			return _isSecret;
		}
	}

	public LevelTable levelTable
	{
		get
		{
			return _levelTable;
		}
	}

	public int libraryNumber
	{
		get
		{
			return _libraryNumber;
		}
	}

	public string libraryDescription
	{
		get
		{
			return _libraryDescription;
		}
	}

	public string libraryDescriptionKey
	{
		get
		{
			return _libraryDescriptionKey;
		}
	}

	public string shopDescription
	{
		get
		{
			return _shopDescription;
		}
	}

	public string shopDescriptionKey
	{
		get
		{
			return _shopDescriptionKey;
		}
	}

	public List<string> tags
	{
		get
		{
			return _tags;
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

	public string iapShopIconSprite
	{
		get
		{
			return _iapShopIconSprite;
		}
		set
		{
			_iapShopIconSprite = value;
		}
	}

	public int iapShopIconSpriteId
	{
		get
		{
			return _iapShopIconSpriteId;
		}
		set
		{
			_iapShopIconSpriteId = value;
		}
	}

	public Vector3 playerCameraOffsetPosition
	{
		get
		{
			return _playerCameraOffsetPosition;
		}
		set
		{
			_playerCameraOffsetPosition = value;
		}
	}

	public Vector3 playerCameraOffsetRotation
	{
		get
		{
			return _playerCameraOffsetRotation;
		}
		set
		{
			_playerCameraOffsetRotation = value;
		}
	}

	public Vector3 playerCameraOffsetPositionZoomedOut
	{
		get
		{
			return _playerCameraOffsetPositionZoomedOut;
		}
		set
		{
			_playerCameraOffsetPositionZoomedOut = value;
		}
	}

	public Vector3 playerCameraOffsetRotationZoomedOut
	{
		get
		{
			return _playerCameraOffsetRotationZoomedOut;
		}
		set
		{
			_playerCameraOffsetRotationZoomedOut = value;
		}
	}

	public Vector3 playerFlightCameraOffsetPosition
	{
		get
		{
			return _playerFlightCameraOffsetPosition;
		}
		set
		{
			_playerFlightCameraOffsetPosition = value;
		}
	}

	public Vector3 playerFlightCameraOffsetRotation
	{
		get
		{
			return _playerFlightCameraOffsetRotation;
		}
		set
		{
			_playerFlightCameraOffsetRotation = value;
		}
	}

	public Vector3 playerFlightCameraOffsetPositionZoomedOut
	{
		get
		{
			return _playerFlightCameraOffsetPositionZoomedOut;
		}
		set
		{
			_playerFlightCameraOffsetPositionZoomedOut = value;
		}
	}

	public Vector3 playerFlightCameraOffsetRotationZoomedOut
	{
		get
		{
			return _playerFlightCameraOffsetRotationZoomedOut;
		}
		set
		{
			_playerFlightCameraOffsetRotationZoomedOut = value;
		}
	}

	public Vector3 enemyCameraOffsetPosition
	{
		get
		{
			return _enemyCameraOffsetPosition;
		}
		set
		{
			_enemyCameraOffsetPosition = value;
		}
	}

	public Vector3 enemyCameraOffsetRotation
	{
		get
		{
			return _enemyCameraOffsetRotation;
		}
		set
		{
			_enemyCameraOffsetRotation = value;
		}
	}

	public Vector3 enemyCameraOffsetPositionZoomedOut
	{
		get
		{
			return _enemyCameraOffsetPositionZoomedOut;
		}
		set
		{
			_enemyCameraOffsetPositionZoomedOut = value;
		}
	}

	public Vector3 enemyCameraOffsetRotationZoomedOut
	{
		get
		{
			return _enemyCameraOffsetRotationZoomedOut;
		}
		set
		{
			_enemyCameraOffsetRotationZoomedOut = value;
		}
	}

	public Vector3 enemyFlightCameraOffsetPosition
	{
		get
		{
			return _enemyFlightCameraOffsetPosition;
		}
		set
		{
			_enemyFlightCameraOffsetPosition = value;
		}
	}

	public Vector3 enemyFlightCameraOffsetRotation
	{
		get
		{
			return _enemyFlightCameraOffsetRotation;
		}
		set
		{
			_enemyFlightCameraOffsetRotation = value;
		}
	}

	public Vector3 enemyFlightCameraOffsetPositionZoomedOut
	{
		get
		{
			return _enemyFlightCameraOffsetPositionZoomedOut;
		}
		set
		{
			_enemyFlightCameraOffsetPositionZoomedOut = value;
		}
	}

	public Vector3 enemyFlightCameraOffsetRotationZoomedOut
	{
		get
		{
			return _enemyFlightCameraOffsetRotationZoomedOut;
		}
		set
		{
			_enemyFlightCameraOffsetRotationZoomedOut = value;
		}
	}

	public Vector3 topRightPortraitOffset
	{
		get
		{
			return _topRightPortraitOffset;
		}
	}

	public Vector3 bottomLeftPortraitOffset
	{
		get
		{
			return _bottomLeftPortraitOffset;
		}
	}

	public Vector3 tofuBubbleOffsetPosition
	{
		get
		{
			return _tofuBubbleOffsetPosition;
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

	public string shopFigurePath
	{
		get
		{
			return _shopFigurePath;
		}
	}

	public string shopFigureBoxPath
	{
		get
		{
			return _shopFigureBoxPath;
		}
	}

	public Vector3 shopFigureScale
	{
		get
		{
			return _shopFigureScale;
		}
	}

	public Vector3 shopFigureOffset
	{
		get
		{
			return _shopFigureOffset;
		}
	}

	public Vector3 shopFigureRotationOffset
	{
		get
		{
			return _shopFigureRotationOffset;
		}
	}

	public int[] shopFigureCosts
	{
		get
		{
			return _shopFigureCosts;
		}
	}

	public int[] shopFigureTicketCosts
	{
		get
		{
			return _shopFigureTicketCosts;
		}
	}

	public Color shopStripeColor
	{
		get
		{
			return _shopStripeColor;
		}
	}

	public Color shopTextFaceColor
	{
		get
		{
			return _shopTextFaceColor;
		}
	}

	public Color shopTextOutlineColor
	{
		get
		{
			return _shopTextOutlineColor;
		}
	}

	public string shopRevealNameSoundPath
	{
		get
		{
			return _shopRevealNameSoundPath;
		}
	}

	public List<CharacterTransformation> transformations
	{
		get
		{
			return _transformations;
		}
	}

	public bool is80sFigure
	{
		get
		{
			return _is80sFigure;
		}
		set
		{
			_is80sFigure = value;
		}
	}

	public bool isVillainFigure
	{
		get
		{
			return _isVillainFigure;
		}
		set
		{
			_isVillainFigure = value;
		}
	}

	public BattleCharacterSounds sounds
	{
		get
		{
			return _sounds;
		}
	}

	public List<RepaintColorSlot> repaintSlots
	{
		get
		{
			return _repaintSlots;
		}
		set
		{
			_repaintSlots = value;
		}
	}

	public CharacterTransformation GetCharacterTransformationById(string id)
	{
		int count = _transformations.Count;
		for (int i = 0; i < count; i++)
		{
			if (_transformations[i].id.Equals(id))
			{
				return _transformations[i];
			}
		}
		return null;
	}

	public int GetBaseStatByType(CharacterStatType type)
	{
		switch (type)
		{
		case CharacterStatType.HitPoints:
			return _baseHitPoints;
		case CharacterStatType.Attack:
			return _baseAttack;
		case CharacterStatType.Speed:
			return _baseSpeed;
		case CharacterStatType.Dodge:
			return _baseDodge;
		default:
			return _baseLuck;
		}
	}

	public int GetBaseStatOffsetByType(CharacterStatType type)
	{
		switch (type)
		{
		case CharacterStatType.HitPoints:
			return _baseHitPointsOffset;
		case CharacterStatType.Attack:
			return _baseAttackOffset;
		case CharacterStatType.Speed:
			return _baseSpeedOffset;
		case CharacterStatType.Dodge:
			return _baseDodgeOffset;
		default:
			return _baseLuckOffset;
		}
	}

	public int GetMaxStatByType(CharacterStatType type)
	{
		switch (type)
		{
		case CharacterStatType.HitPoints:
			return _maxHitPoints;
		case CharacterStatType.Attack:
			return _maxAttack;
		case CharacterStatType.Speed:
			return _maxSpeed;
		case CharacterStatType.Dodge:
			return _maxDodge;
		default:
			return _maxLuck;
		}
	}

	public RepaintColorSlot GetRepaintColorSlotForID(int id)
	{
		for (int i = 0; i < repaintSlots.Count; i++)
		{
			RepaintColorSlot repaintColorSlot = repaintSlots[i];
			if (repaintColorSlot.id == id)
			{
				return repaintColorSlot;
			}
		}
		return null;
	}

	public RepaintColorSlot GetRandomRepaintSlot(bool rareOnly = false)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < _repaintSlots.Count; i++)
		{
			if (!rareOnly || _repaintSlots[i].rare)
			{
				list.Add(i);
			}
		}
		return _repaintSlots[list[UnityEngine.Random.Range(0, list.Count)]];
	}
}
