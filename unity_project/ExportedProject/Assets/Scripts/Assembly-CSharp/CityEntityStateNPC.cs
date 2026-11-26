using System;
using UnityEngine;

[Serializable]
public class CityEntityStateNPC : CityEntityState
{
	[Serializable]
	public class BattleRequirements
	{
		public bool useRequirements;

		public bool extendIntoBattle;

		public Conversation conversation;

		public NPCBattleCondition[] conditions;
	}

	[Serializable]
	public class NPCSpawnGroupConfig
	{
		public bool overrideSpawnManager;

		public int spawnGroupOverrideIndex;
	}

	[SerializeField]
	private string _prefabPathOverride;

	[SerializeField]
	private Conversation _immediateConversation;

	[SerializeField]
	private Conversation _mainConversation;

	[SerializeField]
	private Conversation _greetingConversation;

	[SerializeField]
	private Conversation _preBattleConversation;

	[SerializeField]
	private Conversation _defeatedConversation;

	[SerializeField]
	private BattleConfiguration _battleConfig;

	[SerializeField]
	private BattleRequirements _battleRequirements;

	[SerializeField]
	private NPCSpawnGroupConfig _npcSpawnGroupConfig;

	public string prefabPathOverride
	{
		get
		{
			return _prefabPathOverride;
		}
	}

	public Conversation immediateConversation
	{
		get
		{
			return _immediateConversation;
		}
	}

	public Conversation mainConversation
	{
		get
		{
			return _mainConversation;
		}
	}

	public Conversation greetingConversation
	{
		get
		{
			return _greetingConversation;
		}
	}

	public Conversation preBattleConversation
	{
		get
		{
			return _preBattleConversation;
		}
	}

	public Conversation defeatedConversation
	{
		get
		{
			return _defeatedConversation;
		}
	}

	public BattleConfiguration battleConfig
	{
		get
		{
			return _battleConfig;
		}
	}

	public BattleRequirements battleRequirements
	{
		get
		{
			return _battleRequirements;
		}
	}

	public NPCSpawnGroupConfig npcSpawnGroupConfig
	{
		get
		{
			return _npcSpawnGroupConfig;
		}
	}

	public bool hasFigures
	{
		get
		{
			return _battleConfig.figures.Length > 0;
		}
	}

	public CityEntityStateNPC(string name)
		: base(name)
	{
	}
}
