using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CityEntityEventConfig
{
	public CityEntityEventTriggerType trigger;

	public CityEntityEventType type;

	public UnityEvent unityEvent;

	public Conversation conversation;

	public int shopUID;

	public string sceneName;

	public string pathNodeName;

	public int missionUID;

	public string targetMessage;

	public string triggerName;

	public PlayerReward[] rewards;

	public PathNode pathNode;

	public Animator animator;

	public string stateName;

	public bool gotoEndIfFirst;

	public string soundPath;

	public string soundGroup;

	[Range(0f, 1f)]
	public float volume = 1f;

	public bool loop;

	public TournamentDefinition tournament;
}
