using System;
using UnityEngine;

[Serializable]
public class CityEntityState
{
	public enum MissionState
	{
		Unassigned,
		Active,
		Complete
	}

	public enum FigureConditionType
	{
		Character,
		Tag,
		MinLevel,
		ShinyRank
	}

	[Serializable]
	public class Condition_TutorialStep
	{
		public CoreGameTutorial.Step step;

		public IsOrNot isOrNot;
	}

	[Serializable]
	public class Condition_Mission
	{
		public int missionUID = -1;

		public IsOrNot isOrNot;

		public MissionState state;

		public int daysAfter;
	}

	[Serializable]
	public class Condition_GUIDCollected
	{
		public int missionUID = -1;

		public IsOrNot isOrNot;
	}

	[Serializable]
	public class Condition_Item
	{
		public int itemUID = -1;

		public Comparison comparison;

		public int amount;
	}

	[Serializable]
	public class Condition_Currency
	{
		public CurrencyType type;

		public Comparison comparison;

		public int amount;
	}

	[Serializable]
	public class Condition_NPCDefeated
	{
		public string npcGuid;

		public IsOrNot isOrNot;
	}

	[Serializable]
	public class Condition_Figure
	{
		public FigureConditionType type;

		public Comparison comparison;

		public int count = 1;

		public int figureUID;

		public int intValue1;

		public string stringValue1;

		public bool inParty;
	}

	[Serializable]
	public class Condition_Tournament
	{
		public string tournamentName;

		public HasOrNot hasOrNot;
	}

	[Serializable]
	public class Condition_NewsFlash
	{
		public int uid;

		public IsOrNot isOrNot;

		public MissionState state;
	}

	[SerializeField]
	private bool _isActive = true;

	[SerializeField]
	private string _name;

	[SerializeField]
	private bool _useTimeOfDay;

	[SerializeField]
	private TimeOfDay _timeOfDay;

	[SerializeField]
	private bool _useTrigger;

	[SerializeField]
	private string _triggerName;

	[SerializeField]
	private bool _useTutorialStep;

	[SerializeField]
	private Condition_TutorialStep[] _tutorialStepConditions;

	[SerializeField]
	private bool _useMissionConditions;

	[SerializeField]
	private Condition_Mission[] _missionConditions;

	[SerializeField]
	private bool _useGUIDCollectedConditions;

	[SerializeField]
	private Condition_GUIDCollected[] _guidCollectedConditions;

	[SerializeField]
	private bool _useItemCondition;

	[SerializeField]
	private Condition_Item _itemCondition;

	[SerializeField]
	private bool _useCurrencyCondition;

	[SerializeField]
	private Condition_Currency _currencyCondition;

	[SerializeField]
	private bool _usePlayerCharacterType;

	[SerializeField]
	private CityCharacterType _playerCharacterType;

	[SerializeField]
	private bool _useHasDefeatedNPC;

	[SerializeField]
	private Condition_NPCDefeated[] _npcDefeatedConditions;

	[SerializeField]
	private bool _useFigureConditions;

	[SerializeField]
	private Condition_Figure[] _figureConditions;

	[SerializeField]
	private bool _useConversationPlayed;

	[SerializeField]
	private Conversation _conversationPlayed;

	[SerializeField]
	private bool _useTournamentCondition;

	[SerializeField]
	private Condition_Tournament _tournamentCondition;

	[SerializeField]
	private bool _useNewsFlash;

	[SerializeField]
	private Condition_NewsFlash _newsFlash;

	public bool isActive => _isActive;

	public string name => _name;

	public bool useTimeOfDay => _useTimeOfDay;

	public TimeOfDay timeOfDay => _timeOfDay;

	public bool useTrigger => _useTrigger;

	public string triggerName => _triggerName;

	public bool useTutorialStep => _useTutorialStep;

	public Condition_TutorialStep[] tutorialStepConditions => _tutorialStepConditions;

	public bool useMissionConditions => _useMissionConditions;

	public Condition_Mission[] missionConditions => _missionConditions;

	public bool useGUIDCollectedConditions => _useGUIDCollectedConditions;

	public Condition_GUIDCollected[] guidCollectedConditions => _guidCollectedConditions;

	public bool useItemCondition => _useItemCondition;

	public Condition_Item itemCondition => _itemCondition;

	public bool useCurrencyCondition => _useCurrencyCondition;

	public Condition_Currency currencyCondition => _currencyCondition;

	public bool usePlayerCharacterType => _usePlayerCharacterType;

	public CityCharacterType playerCharacterType => _playerCharacterType;

	public bool useHasDefeatedNPC => _useHasDefeatedNPC;

	public Condition_NPCDefeated[] npcDefeatedConditions => _npcDefeatedConditions;

	public bool useFigureConditions => _useFigureConditions;

	public Condition_Figure[] figureConditions => _figureConditions;

	public bool useConversationPlayed => _useConversationPlayed;

	public Conversation conversationPlayed => _conversationPlayed;

	public bool useTournamentCondition => _useTournamentCondition;

	public Condition_Tournament tournamentCondition => _tournamentCondition;

	public bool useNewsFlash => _useNewsFlash;

	public Condition_NewsFlash newsFlash => _newsFlash;

	public CityEntityState(string name)
	{
		_name = name;
		_missionConditions = new Condition_Mission[0];
		_figureConditions = new Condition_Figure[0];
	}
}
