using System;
using UnityEngine;

[Serializable]
public class CityEntityState
{
	public enum MissionState
	{
		Unassigned = 0,
		Active = 1,
		Complete = 2
	}

	public enum FigureConditionType
	{
		Character = 0,
		Tag = 1,
		MinLevel = 2,
		ShinyRank = 3
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

	public bool isActive
	{
		get
		{
			return _isActive;
		}
	}

	public string name
	{
		get
		{
			return _name;
		}
	}

	public bool useTimeOfDay
	{
		get
		{
			return _useTimeOfDay;
		}
	}

	public TimeOfDay timeOfDay
	{
		get
		{
			return _timeOfDay;
		}
	}

	public bool useTrigger
	{
		get
		{
			return _useTrigger;
		}
	}

	public string triggerName
	{
		get
		{
			return _triggerName;
		}
	}

	public bool useTutorialStep
	{
		get
		{
			return _useTutorialStep;
		}
	}

	public Condition_TutorialStep[] tutorialStepConditions
	{
		get
		{
			return _tutorialStepConditions;
		}
	}

	public bool useMissionConditions
	{
		get
		{
			return _useMissionConditions;
		}
	}

	public Condition_Mission[] missionConditions
	{
		get
		{
			return _missionConditions;
		}
	}

	public bool useGUIDCollectedConditions
	{
		get
		{
			return _useGUIDCollectedConditions;
		}
	}

	public Condition_GUIDCollected[] guidCollectedConditions
	{
		get
		{
			return _guidCollectedConditions;
		}
	}

	public bool useItemCondition
	{
		get
		{
			return _useItemCondition;
		}
	}

	public Condition_Item itemCondition
	{
		get
		{
			return _itemCondition;
		}
	}

	public bool useCurrencyCondition
	{
		get
		{
			return _useCurrencyCondition;
		}
	}

	public Condition_Currency currencyCondition
	{
		get
		{
			return _currencyCondition;
		}
	}

	public bool usePlayerCharacterType
	{
		get
		{
			return _usePlayerCharacterType;
		}
	}

	public CityCharacterType playerCharacterType
	{
		get
		{
			return _playerCharacterType;
		}
	}

	public bool useHasDefeatedNPC
	{
		get
		{
			return _useHasDefeatedNPC;
		}
	}

	public Condition_NPCDefeated[] npcDefeatedConditions
	{
		get
		{
			return _npcDefeatedConditions;
		}
	}

	public bool useFigureConditions
	{
		get
		{
			return _useFigureConditions;
		}
	}

	public Condition_Figure[] figureConditions
	{
		get
		{
			return _figureConditions;
		}
	}

	public bool useConversationPlayed
	{
		get
		{
			return _useConversationPlayed;
		}
	}

	public Conversation conversationPlayed
	{
		get
		{
			return _conversationPlayed;
		}
	}

	public bool useTournamentCondition
	{
		get
		{
			return _useTournamentCondition;
		}
	}

	public Condition_Tournament tournamentCondition
	{
		get
		{
			return _tournamentCondition;
		}
	}

	public bool useNewsFlash
	{
		get
		{
			return _useNewsFlash;
		}
	}

	public Condition_NewsFlash newsFlash
	{
		get
		{
			return _newsFlash;
		}
	}

	public CityEntityState(string name)
	{
		_name = name;
		_missionConditions = new Condition_Mission[0];
		_figureConditions = new Condition_Figure[0];
	}
}
