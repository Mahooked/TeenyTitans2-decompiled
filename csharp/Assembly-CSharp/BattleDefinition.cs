using System.Collections.Generic;

public class BattleDefinition
{
	public BattleType type;

	public CityName cityName = CityName.Any;

	public string enemyName = string.Empty;

	public string enemyPortraitSprite = string.Empty;

	public string enemyGUID = string.Empty;

	public string enemyDefeatedMsg = string.Empty;

	public CharacterName enemyCharacterName;

	public List<Character> enemyTeam;

	public List<BattleCondition> newBattleConditions;

	public List<NPCBattleCondition> battleConditions;

	public List<Character> playerCharactersTeamBoosted = new List<Character>();

	public CharacterStatType playerStatTeamBoosted;

	public int playerStatTeamBoostAmount;

	public List<Character> enemyCharactersTeamBoosted = new List<Character>();

	public CharacterStatType enemyStatTeamBoosted;

	public int enemyStatTeamBoostAmount;

	public string battleThemePath = "Audio/Music/music_battle1";

	public int xpReward = 50;

	public int coinReward = 50;

	public int accessoryUID = -1;

	public int enemyAccessoryUID = -1;

	public PlayerAIType playerAIType;

	public int startingAttackBonus;

	public bool useDefaultPreBattleSound;

	public BattleCharacterSounds.BattleCharacterSoundData soundData;

	public int countdownCharacterUID = -1;

	public CityLocation districtLocation;

	public List<Character> playerTeamOverride;

	public int arenaUID = -1;

	public FriendData opponentData;

	public tk2dSpriteCollectionData pvpPortraitSpriteCollection;

	public bool isHumanOpponent;

	public List<Character> enemyActiveParty;

	public bool IsLODBattle => type == BattleType.Pvp || type == BattleType.PvpCpu || type == BattleType.PvpFriend;

	public BattleDefinition(BattleType type = BattleType.Normal)
	{
		this.type = type;
		enemyTeam = new List<Character>();
	}
}
