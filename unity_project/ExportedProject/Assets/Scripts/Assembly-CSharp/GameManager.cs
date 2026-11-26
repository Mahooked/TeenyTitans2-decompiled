using UnityEngine;

public class GameManager
{
	private CharacterDatabase _characterDatabase;

	private CommunicatorDatabase _communicatorDatabase;

	private ShopDatabase _shopDatabase;

	private AbilityDatabase _abilityDatabase;

	private ItemDatabase _itemDatabase;

	private NPCDatabase _npcDatabase;

	private BattleArenaDatabase _battleArenaDatabase;

	private GameSettings _gameSettings;

	private FlashSaleSettings _flashSaleSettings;

	private AchievementsDatabase _achievementsDatabase;

	private JusticePointsLevel _justiceLevels;

	private ChallengeModeDatabase _challengeModeDatabase;

	private TeamStatBonusDatabase _teamStatBonusDatabase;

	private GameSkinDatabase _gameSkinDatabase;

	private TagCollection _tagCollection;

	private IAPConfiguration _iapConfig;

	private DailyMissionRewards _dailyMissionRewards;

	private CityLocationMusicDatabase _cityLocationMusicDatabase;

	private CityCharacterVODatabase _characterVODatabase;

	private AIConfig _aiConfig;

	private StoryMissionPaths _storyMissionPaths;

	private LegionOfDoomLevelTable _legionOfDoomLevelTable;

	private LegionOfDoomSettings _legionOfDoomSettings;

	private TT2NetMatchmakingSettings _matchmakingSettings;

	private static GameManager _instance;

	public CharacterDatabase characterDatabase
	{
		get
		{
			if (_characterDatabase == null)
			{
				_characterDatabase = Resources.Load<CharacterDatabase>("Data/Characters/AllCharacters");
			}
			return _characterDatabase;
		}
	}

	public CommunicatorDatabase communicatorDatabase
	{
		get
		{
			if (_communicatorDatabase == null)
			{
				_communicatorDatabase = Resources.Load<CommunicatorDatabase>("Data/Communicator/CommunicatorDatabase");
			}
			return _communicatorDatabase;
		}
	}

	public ShopDatabase shopDatabase
	{
		get
		{
			if (_shopDatabase == null)
			{
				_shopDatabase = Resources.Load<ShopDatabase>("Data/ShopDatabase");
			}
			return _shopDatabase;
		}
	}

	public AbilityDatabase abilityDatabase
	{
		get
		{
			if (_abilityDatabase == null)
			{
				_abilityDatabase = Resources.Load<AbilityDatabase>("Data/Abilities/AllAbilities");
			}
			return _abilityDatabase;
		}
	}

	public ItemDatabase itemDatabase
	{
		get
		{
			if (_itemDatabase == null)
			{
				_itemDatabase = Resources.Load<ItemDatabase>("Data/Items/ItemDatabase");
			}
			return _itemDatabase;
		}
	}

	public NPCDatabase npcDatabase
	{
		get
		{
			if (_npcDatabase == null)
			{
				_npcDatabase = Resources.Load<NPCDatabase>("Data/NPC/NPCDatabase");
			}
			return _npcDatabase;
		}
	}

	public BattleArenaDatabase battleArenaDatabase
	{
		get
		{
			if (_battleArenaDatabase == null)
			{
				_battleArenaDatabase = Resources.Load<BattleArenaDatabase>("Data/BattleArenaDatabase");
			}
			return _battleArenaDatabase;
		}
	}

	public GameSettings gameSettings
	{
		get
		{
			if (_gameSettings == null)
			{
				_gameSettings = Resources.Load<GameSettings>("Data/GameSettings");
			}
			return _gameSettings;
		}
	}

	public FlashSaleSettings flashSaleSettings
	{
		get
		{
			if (_flashSaleSettings == null)
			{
				_flashSaleSettings = Resources.Load<FlashSaleSettings>("Data/FlashSaleSettings");
			}
			return _flashSaleSettings;
		}
	}

	public AchievementsDatabase achievementsDatabase
	{
		get
		{
			if (_achievementsDatabase == null)
			{
				_achievementsDatabase = Resources.Load<AchievementsDatabase>("Data/AchievementsDatabase");
			}
			return _achievementsDatabase;
		}
	}

	public JusticePointsLevel justiceLevels
	{
		get
		{
			if (_justiceLevels == null)
			{
				_justiceLevels = Resources.Load<JusticePointsLevel>("Data/Challenges/JusticePointsLevels");
			}
			return _justiceLevels;
		}
	}

	public ChallengeModeDatabase challengeModeDatabase
	{
		get
		{
			if (_challengeModeDatabase == null)
			{
				_challengeModeDatabase = Resources.Load<ChallengeModeDatabase>("Data/Challenges/ChallengeModeDatabase");
			}
			return _challengeModeDatabase;
		}
	}

	public TeamStatBonusDatabase teamStatBonusDatabase
	{
		get
		{
			if (_teamStatBonusDatabase == null)
			{
				_teamStatBonusDatabase = Resources.Load<TeamStatBonusDatabase>("Data/TeamStatBonusDatabase");
			}
			return _teamStatBonusDatabase;
		}
	}

	public GameSkinDatabase gameSkinDatabase
	{
		get
		{
			if (_gameSkinDatabase == null)
			{
				_gameSkinDatabase = Resources.Load<GameSkinDatabase>("Data/GameSkinDatabase");
			}
			return _gameSkinDatabase;
		}
	}

	public TagCollection tagCollection
	{
		get
		{
			if (_tagCollection == null)
			{
				_tagCollection = Resources.Load<TagCollection>("Data/Tags");
			}
			return _tagCollection;
		}
	}

	public IAPConfiguration iapConfig
	{
		get
		{
			if (_iapConfig == null)
			{
				_iapConfig = Resources.Load<IAPConfiguration>("Data/IAPConfig");
			}
			return _iapConfig;
		}
	}

	public DailyMissionRewards dailyMissionRewards
	{
		get
		{
			if (_dailyMissionRewards == null)
			{
				_dailyMissionRewards = Resources.Load<DailyMissionRewards>("Data/DailyMissionRewards");
			}
			return _dailyMissionRewards;
		}
	}

	public CityLocationMusicDatabase cityLocationMusicDatabase
	{
		get
		{
			if (_cityLocationMusicDatabase == null)
			{
				_cityLocationMusicDatabase = Resources.Load<CityLocationMusicDatabase>("Data/CityLocationMusicDatabase");
			}
			return _cityLocationMusicDatabase;
		}
	}

	public CityCharacterVODatabase characterVODatabase
	{
		get
		{
			if (_characterVODatabase == null)
			{
				_characterVODatabase = Resources.Load<CityCharacterVODatabase>("Data/CityCharacterVODatabase");
			}
			return _characterVODatabase;
		}
	}

	public AIConfig aiConfig
	{
		get
		{
			if (_aiConfig == null)
			{
				_aiConfig = Resources.Load<AIConfig>("Data/AIConfig");
			}
			return _aiConfig;
		}
	}

	public StoryMissionPaths storyMissionPaths
	{
		get
		{
			if (_storyMissionPaths == null)
			{
				_storyMissionPaths = Resources.Load<StoryMissionPaths>("Data/StoryMissionPaths");
			}
			return _storyMissionPaths;
		}
	}

	public LegionOfDoomLevelTable legionOfDoomLevelTable
	{
		get
		{
			if (_legionOfDoomLevelTable == null)
			{
				_legionOfDoomLevelTable = Resources.Load<LegionOfDoomLevelTable>("Data/LegionOfDoom/LegionOfDoomLevelTable");
			}
			return _legionOfDoomLevelTable;
		}
	}

	public LegionOfDoomSettings legionOfDoomSettings
	{
		get
		{
			if (_legionOfDoomSettings == null)
			{
				_legionOfDoomSettings = Resources.Load<LegionOfDoomSettings>("Data/LegionOfDoom/LegionOfDoomSettings");
			}
			return _legionOfDoomSettings;
		}
	}

	public TT2NetMatchmakingSettings matchmakingSettings
	{
		get
		{
			if (_matchmakingSettings == null)
			{
				_matchmakingSettings = Resources.Load<TT2NetMatchmakingSettings>("Data/MatchmakingSettings");
			}
			return _matchmakingSettings;
		}
	}

	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameManager();
			}
			return _instance;
		}
	}

	public void Init()
	{
		if (_characterDatabase == null)
		{
			_characterDatabase = Resources.Load<CharacterDatabase>("Data/Characters/AllCharacters");
		}
		if (_abilityDatabase == null)
		{
			_abilityDatabase = Resources.Load<AbilityDatabase>("Data/Abilities/AllAbilities");
		}
		if (_itemDatabase == null)
		{
			_itemDatabase = Resources.Load<ItemDatabase>("Data/Items/ItemDatabase");
		}
		if (_battleArenaDatabase == null)
		{
			_battleArenaDatabase = Resources.Load<BattleArenaDatabase>("Data/BattleArenaDatabase");
		}
		if (_gameSettings == null)
		{
			_gameSettings = Resources.Load<GameSettings>("Data/GameSettings");
		}
		if (_communicatorDatabase == null)
		{
			_communicatorDatabase = Resources.Load<CommunicatorDatabase>("Data/Communicator/CommunicatorDatabase");
		}
		if (_shopDatabase == null)
		{
			_shopDatabase = Resources.Load<ShopDatabase>("Data/ShopDatabase");
		}
		if (_npcDatabase == null)
		{
			_npcDatabase = Resources.Load<NPCDatabase>("Data/NPC/NPCDatabase");
		}
		if (_achievementsDatabase == null)
		{
			_achievementsDatabase = Resources.Load<AchievementsDatabase>("Data/Achievements");
		}
		if (_gameSkinDatabase == null)
		{
			_gameSkinDatabase = Resources.Load<GameSkinDatabase>("Data/GameSkinDatabase");
		}
		if (_tagCollection == null)
		{
			_tagCollection = Resources.Load<TagCollection>("Data/Tags");
		}
		if (_dailyMissionRewards == null)
		{
			_dailyMissionRewards = Resources.Load<DailyMissionRewards>("Data/DailyMissionRewards");
		}
		if (_cityLocationMusicDatabase == null)
		{
			_cityLocationMusicDatabase = Resources.Load<CityLocationMusicDatabase>("Data/CityLocationMusicDatabase");
		}
		if (_characterVODatabase == null)
		{
			_characterVODatabase = Resources.Load<CityCharacterVODatabase>("Data/CityCharacterVODatabase");
		}
		if (_aiConfig == null)
		{
			_aiConfig = Resources.Load<AIConfig>("Data/AIConfig");
		}
		if (_storyMissionPaths == null)
		{
			_storyMissionPaths = Resources.Load<StoryMissionPaths>("Data/StoryMissionPaths");
		}
		if (_legionOfDoomLevelTable == null)
		{
			_legionOfDoomLevelTable = Resources.Load<LegionOfDoomLevelTable>("Data/LegionOfDoom/LegionOfDoomLevelTable");
		}
		if (_legionOfDoomSettings == null)
		{
			_legionOfDoomSettings = Resources.Load<LegionOfDoomSettings>("Data/LegionOfDoom/LegionOfDoomSettings");
		}
		if (_matchmakingSettings == null)
		{
			_matchmakingSettings = Resources.Load<TT2NetMatchmakingSettings>("Data/MatchmakingSettings");
		}
	}
}
