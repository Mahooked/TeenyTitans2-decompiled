using System;
using UnityEngine;

[Serializable]
public class ConversationEventConfig
{
	public ConversationEventType type;

	public int shopUID;

	public string sceneName;

	public string pathNodeName;

	public int missionUID;

	public string targetMessage;

	public string triggerName;

	public string soundPath;

	public string soundGroup;

	[Range(0f, 1f)]
	public float volume = 1f;

	public bool loop;

	public ConversationEventConfig Copy()
	{
		ConversationEventConfig conversationEventConfig = new ConversationEventConfig();
		conversationEventConfig.type = type;
		conversationEventConfig.shopUID = shopUID;
		conversationEventConfig.sceneName = sceneName;
		conversationEventConfig.pathNodeName = pathNodeName;
		conversationEventConfig.missionUID = missionUID;
		conversationEventConfig.targetMessage = targetMessage;
		conversationEventConfig.triggerName = triggerName;
		conversationEventConfig.soundPath = soundPath;
		conversationEventConfig.soundGroup = soundGroup;
		conversationEventConfig.volume = volume;
		conversationEventConfig.loop = loop;
		return conversationEventConfig;
	}
}
