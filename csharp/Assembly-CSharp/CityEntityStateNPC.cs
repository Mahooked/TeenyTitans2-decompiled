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

	public string prefabPathOverride => _prefabPathOverride;

	public Conversation immediateConversation => _immediateConversation;

	public Conversation mainConversation => _mainConversation;

	public Conversation greetingConversation => _greetingConversation;

	public Conversation preBattleConversation => _preBattleConversation;

	public Conversation defeatedConversation => _defeatedConversation;

	public BattleConfiguration battleConfig => _battleConfig;

	public BattleRequirements battleRequirements => _battleRequirements;

	public NPCSpawnGroupConfig npcSpawnGroupConfig => _npcSpawnGroupConfig;

	public bool hasFigures => _battleConfig.figures.Length > 0;

	public CityEntityStateNPC(string name)
		: base(name)
	{
	}
}
