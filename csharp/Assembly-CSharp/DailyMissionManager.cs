using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMissionManager : MonoBehaviour
{
	public Action DailyMissionGenerated;

	public Action<DailyMissionData> DailyMissionExpired;

	private bool isInitialized;

	private DailyMissionConfig config;

	private static bool didApplicationQuit;

	private static DailyMissionManager _instance;

	public DailyMissionData currentDailyMission => Player.instance.currentDailyMission;

	public static bool debugMissions { get; set; }

	public static TimeSpan debugTimeOffset { get; set; }

	public static DailyMissionManager instance
	{
		get
		{
			if (_instance == null && !didApplicationQuit)
			{
				_instance = new GameObject("DailyMissionManager").AddComponent<DailyMissionManager>();
			}
			return _instance;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		EventMessenger.Instance.AddListener<LanguageChangedEvent>(HandleLanguageChangedEvent);
	}

	private void OnEnable()
	{
		StartCoroutine(UpdateRemainingTimeRoutine());
	}

	private void OnApplicationQuit()
	{
		didApplicationQuit = true;
	}

	private void OnDestroy()
	{
		EventMessenger.Instance.RemoveListener<LanguageChangedEvent>(HandleLanguageChangedEvent);
		if (Player.instance != null)
		{
			Player.instance.DailyMissionCompleted -= HandleDailyMissionCompleted;
		}
	}

	public void Init()
	{
		if (!isInitialized)
		{
			config = Resources.Load<DailyMissionConfig>("DailyMissionConfig");
			isInitialized = true;
			if (Player.instance.currentDailyMission == null)
			{
				GenerateMission();
			}
			else
			{
				GameManager.instance.communicatorDatabase.AddDailyMission(Player.instance.currentDailyMission);
			}
			Player.instance.DailyMissionCompleted += HandleDailyMissionCompleted;
		}
	}

	private void HandleLanguageChangedEvent(LanguageChangedEvent evt)
	{
		if (isInitialized && currentDailyMission != null)
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(currentDailyMission.uid);
			if (mission != null)
			{
				mission.description = currentDailyMission.description;
			}
		}
	}

	private void HandleDailyMissionCompleted(int uid)
	{
	}

	private IEnumerator UpdateRemainingTimeRoutine()
	{
		while (true)
		{
			if (currentDailyMission != null && currentDailyMission.GetTimeSpanRemaining().TotalSeconds <= 0.0)
			{
				OnDailyMissionExpired(currentDailyMission);
				EventMessenger.Instance.Raise(new DailyMissionExpiredEvent(currentDailyMission));
				Player.instance.SetCurrentDailyMission(null);
				GenerateMission();
			}
			yield return new WaitForSeconds(1f);
		}
	}

	private void GenerateMission()
	{
		if (!isInitialized)
		{
			return;
		}
		Debug.Log("[DailyMissionManager] - Generating Daily Mission...");
		if (!config.totallyRandom && !debugMissions)
		{
			DateTime dateTime = DateTime.Now.AddDays(config.debugDaysOffset).Add(debugTimeOffset);
			UnityEngine.Random.InitState(dateTime.Year * 1000 + dateTime.DayOfYear);
		}
		else
		{
			UnityEngine.Random.InitState(Environment.TickCount);
		}
		DailyMissionData dailyMissionData = new DailyMissionData();
		dailyMissionData.uid = (int)(10000 + DateTime.Now.DayOfWeek);
		DailyMissionData dailyMissionData2 = dailyMissionData;
		DailyMissionType[] array = (DailyMissionType[])Enum.GetValues(typeof(DailyMissionType));
		do
		{
			dailyMissionData2.type = array[UnityEngine.Random.Range(0, array.Length)];
		}
		while (dailyMissionData2.type == DailyMissionType.ScavengerHunt);
		DailyMissionSetup randomSetupByType = config.GetRandomSetupByType(dailyMissionData2.type);
		if (randomSetupByType == null)
		{
			Debug.LogError(string.Concat("[DailyMissionManager] - There are no daily mission setups for the mission type of ", dailyMissionData2.type, ". Please remedy this."));
		}
		dailyMissionData2.difficulty = randomSetupByType.difficultyLevel;
		NPCDefinition nPCDefinition = null;
		List<CharacterClass> list = new List<CharacterClass>((CharacterClass[])Enum.GetValues(typeof(CharacterClass)));
		list.Remove(CharacterClass.None);
		List<string> list2 = new List<string>(config.GetAllValidNPCs());
		DailyMissionType type = dailyMissionData2.type;
		if (type != DailyMissionType.DefeatNPC)
		{
			switch (type)
			{
			case DailyMissionType.DefeatNPCType:
				dailyMissionData2.battleConfig = new MissionTypeConfigBattle();
				dailyMissionData2.battleConfig.targetType = MissionTypeConfigBattle.TargetType.DefeatNPCTag;
				dailyMissionData2.battleConfig.npcTag = config.validNPCTags[UnityEngine.Random.Range(0, config.validNPCTags.Count)];
				dailyMissionData2.battleConfig.count = randomSetupByType.rangedIntValue1.GetRandomValue();
				break;
			case DailyMissionType.Shopping:
				dailyMissionData2.buyConfig = new MissionTypeConfigBuy();
				dailyMissionData2.buyConfig.toyType = ToyType.Figure;
				dailyMissionData2.buyConfig.figureConfig = new MissionTypeConfigBuy.FigureConfig();
				if (UnityEngine.Random.Range(0, 2) == 0)
				{
					dailyMissionData2.buyConfig.figureConfig.type = MissionTypeConfigBuy.FigureConfig.Type.Class;
					dailyMissionData2.buyConfig.figureConfig.validUids = new int[1] { (int)list[UnityEngine.Random.Range(0, list.Count)] };
				}
				else
				{
					dailyMissionData2.buyConfig.figureConfig.type = MissionTypeConfigBuy.FigureConfig.Type.Tag;
					dailyMissionData2.buyConfig.figureConfig.validTags = new string[1] { config.validFigTags[UnityEngine.Random.Range(0, config.validFigTags.Count)] };
				}
				dailyMissionData2.buyConfig.count = randomSetupByType.rangedIntValue1.GetRandomValue();
				while (list2.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, list2.Count);
					nPCDefinition = GameManager.instance.npcDatabase.GetNPCDef(list2[index]);
					list2.RemoveAt(index);
					if (nPCDefinition != null)
					{
						break;
					}
				}
				if (nPCDefinition != null)
				{
					dailyMissionData2.missionLocationNPC = nPCDefinition.guid;
				}
				break;
			}
		}
		else
		{
			while (list2.Count > 0)
			{
				int index2 = UnityEngine.Random.Range(0, list2.Count);
				nPCDefinition = GameManager.instance.npcDatabase.GetNPCDef(list2[index2]);
				list2.RemoveAt(index2);
				if (nPCDefinition != null)
				{
					break;
				}
			}
			if (nPCDefinition != null)
			{
				dailyMissionData2.battleConfig = new MissionTypeConfigBattle();
				dailyMissionData2.battleConfig.targetType = MissionTypeConfigBattle.TargetType.DefeatNPC;
				dailyMissionData2.battleConfig.npcGuid = nPCDefinition.guid;
				dailyMissionData2.battleConfig.count = 1;
				dailyMissionData2.battleCondition = new BattleCondition();
				switch (UnityEngine.Random.Range(0, 3))
				{
				case 0:
					dailyMissionData2.battleCondition.conditionType = BattleCondition.ConditionType.FigureClass;
					dailyMissionData2.battleCondition.comparison1 = Comparison.Exactly;
					dailyMissionData2.battleCondition.intValue1 = 3;
					dailyMissionData2.battleCondition.intValue2 = (int)list[UnityEngine.Random.Range(0, list.Count)];
					break;
				case 1:
					dailyMissionData2.battleCondition.conditionType = BattleCondition.ConditionType.FigureTags;
					dailyMissionData2.battleCondition.comparison1 = Comparison.Exactly;
					dailyMissionData2.battleCondition.intValue1 = 3;
					dailyMissionData2.battleCondition.stringValue1 = config.validFigTags[UnityEngine.Random.Range(0, config.validFigTags.Count)];
					break;
				default:
					dailyMissionData2.battleCondition.conditionType = BattleCondition.ConditionType.FigureName;
					dailyMissionData2.battleCondition.comparison1 = Comparison.AtLeast;
					dailyMissionData2.battleCondition.intValue1 = 1;
					dailyMissionData2.battleCondition.intValue2 = GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition x) => !config.excludeCharacterUids.Contains(x.uid) && x.isActive && x.canUseInBattle).uid;
					break;
				}
				dailyMissionData2.figureOverrides = new TournamentRoundFigureData[3];
				for (int num = 0; num < dailyMissionData2.figureOverrides.Length; num++)
				{
					TournamentRoundFigureData tournamentRoundFigureData = new TournamentRoundFigureData();
					tournamentRoundFigureData.characterUID = GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition x) => !config.excludeCharacterUids.Contains(x.uid) && x.isActive && x.canUseInBattle).uid;
					tournamentRoundFigureData.level = randomSetupByType.characterRollOptions.level.GetRandomValue();
					CharacterRoller.RollTournamentFigure(tournamentRoundFigureData);
					tournamentRoundFigureData.shinyRank = randomSetupByType.characterRollOptions.shinyRank.GetRandomValue();
					dailyMissionData2.figureOverrides[num] = tournamentRoundFigureData;
					Debug.Log(tournamentRoundFigureData);
				}
			}
		}
		dailyMissionData2.coinReward = 0;
		dailyMissionData2.rainbowCoinReward = 0;
		for (int num2 = 0; num2 < randomSetupByType.rewards.Length; num2++)
		{
			PlayerReward playerReward = randomSetupByType.rewards[num2];
			switch (playerReward.type)
			{
			case PlayerRewardType.Currency:
				if (playerReward.currencyType == CurrencyType.Coins)
				{
					dailyMissionData2.coinReward += playerReward.amount;
				}
				else if (playerReward.currencyType == CurrencyType.RainbowCoins)
				{
					dailyMissionData2.rainbowCoinReward += playerReward.amount;
				}
				break;
			case PlayerRewardType.Item:
				if (playerReward.itemUID > -1 && GameManager.instance.itemDatabase.GetItem(playerReward.itemUID) is ModChipDefinition)
				{
					dailyMissionData2.modChipReward = playerReward.itemUID;
				}
				break;
			case PlayerRewardType.Figure:
				if (dailyMissionData2.characterRewards == null)
				{
					dailyMissionData2.characterRewards = new List<CharacterRewardData>();
				}
				dailyMissionData2.characterRewards.Add(playerReward.figure);
				break;
			}
		}
		Debug.Log(string.Concat("[DailyMissionManager] - New Daily Mission Generated: ", dailyMissionData2.type, "\n", dailyMissionData2.description));
		GameManager.instance.communicatorDatabase.AddDailyMission(dailyMissionData2);
		Player.instance.SetCurrentDailyMission(dailyMissionData2);
		UnityEngine.Random.InitState(Environment.TickCount);
		OnDailyMissionGenerated();
	}

	public bool IsDailyMissionAvailable()
	{
		return currentDailyMission != null && !Player.instance.HasActiveMission(currentDailyMission.uid) && !Player.instance.HasMissionCompleted(currentDailyMission.uid);
	}

	public bool IsDailyMissionComplete()
	{
		return currentDailyMission != null && Player.instance.HasMissionCompleted(currentDailyMission.uid);
	}

	public void ForceGenerateMission()
	{
		Debug.Log("[DailyMissionManager] - Force Generating Daily Mission...");
		if (currentDailyMission != null)
		{
			Player.instance.SetCurrentDailyMission(null);
		}
		GenerateMission();
	}

	private void OnDailyMissionGenerated()
	{
		if (DailyMissionGenerated != null)
		{
			DailyMissionGenerated();
		}
	}

	private void OnDailyMissionExpired(DailyMissionData dailyMissionData)
	{
		if (DailyMissionExpired != null)
		{
			DailyMissionExpired(dailyMissionData);
		}
	}
}
