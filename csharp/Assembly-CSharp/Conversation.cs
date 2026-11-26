using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/Conversation")]
public class Conversation : ScriptableObject
{
	[Serializable]
	public class CharacterConversation
	{
		public CityCharacterType characterType;

		public List<ConversationItem> items;

		public CharacterName initialLeftCharacter;

		public CharacterName initialRightCharacter;

		public int initialLeftBodySpriteId = -1;

		public int initialLeftFaceSpriteId = -1;

		public string initialLeftBodySpriteName;

		public string initialLeftFaceSpriteName;

		public int initialRightBodySpriteId = -1;

		public int initialRightFaceSpriteId = -1;

		public string initialRightBodySpriteName;

		public string initialRightFaceSpriteName;

		public CharacterConversation(CityCharacterType characterType)
		{
			this.characterType = characterType;
			items = new List<ConversationItem>();
		}
	}

	public bool useCharacterItems;

	public bool tintScreen = true;

	public bool triggersImmediately;

	public bool triggersBattle;

	public bool triggersTournament;

	public TournamentDefinition tournament;

	public bool triggersLoadScene;

	public string sceneName;

	public string pathNodeName;

	public bool assignInitialFaces;

	public CharacterName initialLeftCharacter;

	public CharacterName initialRightCharacter;

	public int initialLeftBodySpriteId = -1;

	public int initialLeftFaceSpriteId = -1;

	public string initialLeftBodySpriteName;

	public string initialLeftFaceSpriteName;

	public int initialRightBodySpriteId = -1;

	public int initialRightFaceSpriteId = -1;

	public string initialRightBodySpriteName;

	public string initialRightFaceSpriteName;

	public int missionUID = -1;

	public int completesMissionUID = -1;

	public string achievementId = string.Empty;

	public bool hideLeftCharacter;

	public bool hideRightCharacter;

	public bool useRewards;

	public int coinReward;

	public int rainbowCoinReward;

	public int modChipRewardUID = -1;

	public int accessoryRewardUID = -1;

	public List<CharacterRewardData> characterRewards = new List<CharacterRewardData>();

	public List<ConversationItem> items = new List<ConversationItem>();

	public List<CharacterConversation> charItems = new List<CharacterConversation>();

	public CharacterConversation GetCharacterConversation(CityCharacterType characterType)
	{
		for (int i = 0; i < charItems.Count; i++)
		{
			if (charItems[i].characterType == characterType)
			{
				return charItems[i];
			}
		}
		return null;
	}
}
