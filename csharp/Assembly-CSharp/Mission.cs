using System.Collections.Generic;
using UnityEngine;

public class Mission : ScriptableObject
{
	[SerializeField]
	private int _uid = -1;

	[SerializeField]
	private string _title = string.Empty;

	[SerializeField]
	private string _titleKey = string.Empty;

	[SerializeField]
	private MissionType _type;

	[SerializeField]
	private string _targetClass;

	[SerializeField]
	private string _targetMsg;

	[SerializeField]
	private MissionSideType _sideType;

	[SerializeField]
	private string _description = string.Empty;

	[SerializeField]
	private string _descriptionKey = string.Empty;

	[SerializeField]
	private bool _descriptionOverrides;

	[SerializeField]
	private bool _skipMissionBanner;

	[SerializeField]
	private string _shortTypeName = string.Empty;

	[SerializeField]
	private string _shortTypeNameKey = string.Empty;

	[SerializeField]
	private string _iconSpriteName = string.Empty;

	[SerializeField]
	private tk2dSpriteCollectionData _iconSpriteCollection;

	[SerializeField]
	private int _iconSpriteID;

	[SerializeField]
	private Vector3 _iconScale = Vector3.one;

	[SerializeField]
	private int _itemToCollectUID = -1;

	[SerializeField]
	private string _scavengerHuntButtonSpriteName;

	[SerializeField]
	private LocalizedString _scavengerHuntButtonLabel;

	[SerializeField]
	private Color _scavengerHuntButtonLabelOutlineColor;

	[SerializeField]
	private string[] _scavengerHuntConvoPaths;

	[SerializeField]
	private int _amountNeeded;

	[SerializeField]
	private int _coinReward;

	[SerializeField]
	private int _rainbowCoinReward;

	[SerializeField]
	private int _modChipReward = -1;

	[SerializeField]
	private int _accessoryReward = -1;

	[SerializeField]
	private int _questItemReward = -1;

	[SerializeField]
	private PlayerReward _repaintReward;

	[SerializeField]
	private PlayerReward _riddleEggReward;

	[SerializeField]
	private int _activatesMission = -1;

	[SerializeField]
	private ScriptableObject _completionConvo;

	[SerializeField]
	private string _completionAnimation;

	[SerializeField]
	private List<CharacterRewardData> _characterRewards = new List<CharacterRewardData>();

	[SerializeField]
	private List<int> _collectUIDS = new List<int>();

	[SerializeField]
	private CharacterName _npcOwner = CharacterName.None;

	[SerializeField]
	private TournamentDefinition _tournamentDef;

	[SerializeField]
	private MissionHintType _missionHintType;

	[SerializeField]
	private string _missionHintNPC;

	[SerializeField]
	private string _missionLocation = string.Empty;

	[SerializeField]
	private int _missionLocationShop = -1;

	[SerializeField]
	private TournamentDefinition _missionTournamentLocation;

	[SerializeField]
	private Conversation _missionHintConvo;

	[SerializeField]
	private bool _autoCollect;

	[SerializeField]
	private bool _playCompletionConvoEnteringCity;

	[SerializeField]
	private CharacterClass _buyCharacterClass = CharacterClass.None;

	[SerializeField]
	private string _buyTag = string.Empty;

	[SerializeField]
	private string _npcTag = string.Empty;

	[SerializeField]
	private CityName _missionLocationCity = CityName.Any;

	[SerializeField]
	private MissionTypeConfigBuy[] _buyConfigs;

	[SerializeField]
	private string _deliveryNPC;

	[SerializeField]
	private MissionTypeConfigBattle[] _battleConfigs;

	[SerializeField]
	private BattleCondition[] _battleConditions;

	[SerializeField]
	private Conversation _battleConditionFailConvo;

	[SerializeField]
	private bool _useFigureOverrides;

	[SerializeField]
	private TournamentRoundFigureData[] _figureOverrides;

	[SerializeField]
	private MissionTypeConfigMailbox _mailboxConfig;

	[SerializeField]
	private bool _isDaily;

	public int uid
	{
		get
		{
			return _uid;
		}
		set
		{
			_uid = value;
		}
	}

	public string title
	{
		get
		{
			return _title;
		}
		set
		{
			_title = value;
		}
	}

	public string titleKey => _titleKey;

	public MissionType type
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

	public string targetClass => _targetClass;

	public string targetMsg
	{
		get
		{
			return _targetMsg;
		}
		set
		{
			_targetMsg = value;
		}
	}

	public MissionSideType sideType
	{
		get
		{
			return _sideType;
		}
		set
		{
			_sideType = value;
		}
	}

	public string description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	public string descriptionKey
	{
		get
		{
			return _descriptionKey;
		}
		set
		{
			_descriptionKey = value;
		}
	}

	public bool descriptionOverrides
	{
		get
		{
			return _descriptionOverrides;
		}
		set
		{
			_descriptionOverrides = value;
		}
	}

	public bool skipMissionBanner => _skipMissionBanner;

	public string shortTypeName => _shortTypeName;

	public string shortTypeNameKey => _shortTypeNameKey;

	public string iconSpriteName
	{
		get
		{
			return _iconSpriteName;
		}
		set
		{
			_iconSpriteName = value;
		}
	}

	public tk2dSpriteCollectionData iconSpriteCollection => _iconSpriteCollection;

	public int iconSpriteID => _iconSpriteID;

	public Vector3 iconScale => _iconScale;

	public int itemToCollectUID => _itemToCollectUID;

	public string scavengerHuntButtonSpriteName => _scavengerHuntButtonSpriteName;

	public LocalizedString scavengerHuntButtonLabel => _scavengerHuntButtonLabel;

	public Color scavengerHuntButtonLabelOutlineColor => _scavengerHuntButtonLabelOutlineColor;

	public string[] scavengerHuntConvoPaths => _scavengerHuntConvoPaths;

	public int amountNeeded
	{
		get
		{
			return _amountNeeded;
		}
		set
		{
			_amountNeeded = value;
		}
	}

	public int coinReward
	{
		get
		{
			return _coinReward;
		}
		set
		{
			_coinReward = value;
		}
	}

	public int rainbowCoinReward
	{
		get
		{
			return _rainbowCoinReward;
		}
		set
		{
			_rainbowCoinReward = value;
		}
	}

	public int modChipReward
	{
		get
		{
			return _modChipReward;
		}
		set
		{
			_modChipReward = value;
		}
	}

	public int accessoryReward
	{
		get
		{
			return _accessoryReward;
		}
		set
		{
			_accessoryReward = value;
		}
	}

	public int questItemReward
	{
		get
		{
			return _questItemReward;
		}
		set
		{
			_questItemReward = value;
		}
	}

	public PlayerReward repaintReward => _repaintReward;

	public PlayerReward riddleEggReward
	{
		get
		{
			return _riddleEggReward;
		}
		set
		{
			_riddleEggReward = value;
		}
	}

	public int activatesMission => _activatesMission;

	public ScriptableObject completionConvo => _completionConvo;

	public string completionAnimation => _completionAnimation;

	public List<CharacterRewardData> characterRewards
	{
		get
		{
			return _characterRewards;
		}
		set
		{
			_characterRewards = value;
		}
	}

	public List<int> collectUIDS
	{
		get
		{
			return _collectUIDS;
		}
		set
		{
			_collectUIDS = value;
		}
	}

	public CharacterName npcOwner => _npcOwner;

	public TournamentDefinition tournamentDef => _tournamentDef;

	public MissionHintType missionHintType
	{
		get
		{
			return _missionHintType;
		}
		set
		{
			_missionHintType = value;
		}
	}

	public string missionHintNPC
	{
		get
		{
			return _missionHintNPC;
		}
		set
		{
			_missionHintNPC = value;
		}
	}

	public string missionLocation => _missionLocation;

	public int missionLocationShop => _missionLocationShop;

	public TournamentDefinition missionTournamentLocation => _missionTournamentLocation;

	public Conversation missionHintConvo => _missionHintConvo;

	public bool autoCollect
	{
		get
		{
			return _autoCollect;
		}
		set
		{
			_autoCollect = value;
		}
	}

	public bool playCompletionConvoEnteringCity => _playCompletionConvoEnteringCity;

	public CharacterClass buyCharacterClass => _buyCharacterClass;

	public string buyTag
	{
		get
		{
			return _buyTag;
		}
		set
		{
			_buyTag = value;
		}
	}

	public string npcTag
	{
		get
		{
			return _npcTag;
		}
		set
		{
			_npcTag = value;
		}
	}

	public CityName missionLocationCity
	{
		get
		{
			return _missionLocationCity;
		}
		set
		{
			_missionLocationCity = value;
		}
	}

	public MissionTypeConfigBuy[] buyConfigs
	{
		get
		{
			return _buyConfigs;
		}
		set
		{
			_buyConfigs = value;
		}
	}

	public string deliveryNPC
	{
		get
		{
			return _deliveryNPC;
		}
		set
		{
			_deliveryNPC = value;
		}
	}

	public MissionTypeConfigBattle[] battleConfigs
	{
		get
		{
			return _battleConfigs;
		}
		set
		{
			_battleConfigs = value;
		}
	}

	public BattleCondition[] battleConditions
	{
		get
		{
			return _battleConditions;
		}
		set
		{
			_battleConditions = value;
		}
	}

	public Conversation battleConditionFailConvo => _battleConditionFailConvo;

	public bool useFigureOverrides
	{
		get
		{
			return _useFigureOverrides;
		}
		set
		{
			_useFigureOverrides = value;
		}
	}

	public TournamentRoundFigureData[] figureOverrides
	{
		get
		{
			return _figureOverrides;
		}
		set
		{
			_figureOverrides = value;
		}
	}

	public MissionTypeConfigMailbox mailboxConfig => _mailboxConfig;

	public bool isDaily
	{
		get
		{
			return _isDaily;
		}
		set
		{
			_isDaily = value;
		}
	}

	public bool HasMetBuyConfigs(MissionData missionData)
	{
		if (missionData.uid != _uid || _sideType != MissionSideType.Buy)
		{
			return false;
		}
		for (int i = 0; i < _buyConfigs.Length; i++)
		{
			if (missionData.counts[i] < _buyConfigs[i].count)
			{
				return false;
			}
		}
		return true;
	}
}
