using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConversationItem
{
	public string id;

	public bool isEnd;

	public bool isPrismCreature;

	public bool useAlienLanguage;

	public bool isWildcard;

	public CharacterName character;

	public AvatarPosition position;

	public int bodySpriteId = -1;

	public string bodySpriteName;

	public int faceSpriteId = -1;

	public string faceSpriteName;

	public int poseSpriteId = -1;

	public string poseSpriteName;

	public string message = string.Empty;

	public string key = string.Empty;

	public string[] formatArgs;

	public AudioClip clip;

	public int otherFaceSpriteId = -1;

	public string otherFaceSpriteName;

	public List<Choice> choices;

	public PlayerReward[] rewards;

	public LocalizedString bribeHeader;

	public LocalizedString bribeMessageBody;

	public CurrencyType bribeCurrencyType;

	public int bribeAmount;

	public int bribeCompletesMissionUid = -1;

	public ConversationEventConfig eventConfig;

	[SerializeField]
	private ConversationItemType _type;

	public ConversationItemType type => _type;

	public ConversationItem(ConversationItemType type)
	{
		_type = type;
		switch (type)
		{
		case ConversationItemType.Dialogue:
			break;
		case ConversationItemType.ChoiceList:
			choices = new List<Choice>
			{
				new Choice(),
				new Choice(),
				new Choice()
			};
			break;
		case ConversationItemType.Reward:
			rewards = new PlayerReward[0];
			break;
		case ConversationItemType.Bribe:
			break;
		case ConversationItemType.Event:
			eventConfig = new ConversationEventConfig();
			break;
		}
	}

	public ConversationItem Copy()
	{
		ConversationItem conversationItem = new ConversationItem(_type);
		conversationItem.id = id;
		conversationItem.isEnd = isEnd;
		conversationItem.isPrismCreature = isPrismCreature;
		conversationItem.useAlienLanguage = useAlienLanguage;
		conversationItem.character = character;
		conversationItem.position = position;
		conversationItem.bodySpriteId = bodySpriteId;
		conversationItem.bodySpriteName = bodySpriteName;
		conversationItem.faceSpriteId = faceSpriteId;
		conversationItem.poseSpriteId = poseSpriteId;
		conversationItem.faceSpriteName = faceSpriteName;
		conversationItem.poseSpriteName = poseSpriteName;
		conversationItem.message = message;
		conversationItem.key = key;
		conversationItem.clip = clip;
		conversationItem.otherFaceSpriteId = otherFaceSpriteId;
		conversationItem.otherFaceSpriteName = otherFaceSpriteName;
		if (conversationItem.choices != null)
		{
			conversationItem.choices.Clear();
			for (int i = 0; i < choices.Count; i++)
			{
				conversationItem.choices.Add(choices[i].Copy());
			}
		}
		if (conversationItem.rewards != null)
		{
			conversationItem.rewards = new PlayerReward[rewards.Length];
			for (int j = 0; j < rewards.Length; j++)
			{
				conversationItem.rewards[j] = rewards[j].Copy();
			}
		}
		conversationItem.bribeHeader = bribeHeader.Copy();
		conversationItem.bribeMessageBody = bribeMessageBody.Copy();
		conversationItem.bribeCurrencyType = bribeCurrencyType;
		conversationItem.bribeAmount = bribeAmount;
		conversationItem.bribeCompletesMissionUid = bribeCompletesMissionUid;
		conversationItem.eventConfig = eventConfig.Copy();
		return conversationItem;
	}
}
