using System.Collections;
using UnityEngine;

public class CityEntityEvent : CityEntity
{
	[SerializeField]
	private CityEntityStateEvent[] _states;

	[SerializeField]
	private tk2dUIItem _uiItem;

	[SerializeField]
	private BoxCollider _boxCollider;

	[SerializeField]
	private PathNode _interactNode;

	[SerializeField]
	private GameObject[] _visualObjects;

	public PathNode interactNode
	{
		get
		{
			return _interactNode;
		}
	}

	public GameObject[] visualObjects
	{
		get
		{
			return _visualObjects;
		}
	}

	public override CityEntityState[] states
	{
		get
		{
			return _states;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (_uiItem == null)
		{
			_uiItem = GetComponent<tk2dUIItem>();
		}
		if (_boxCollider == null)
		{
			_boxCollider = GetComponent<BoxCollider>();
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (_uiItem != null)
		{
			_uiItem.OnClick += HandleOnClick;
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (_uiItem != null)
		{
			_uiItem.OnClick -= HandleOnClick;
		}
	}

	private void HandleOnClick()
	{
		if (!(cameraPinchZoomer != null) || !cameraPinchZoomer.isZoomedOut)
		{
			EventMessenger.Instance.Raise(new EventEntityInteractEvent(this));
		}
	}

	public void Interact()
	{
		CityEntityStateEvent cityEntityStateEvent = (CityEntityStateEvent)base.activeState;
		if (cityEntityStateEvent.eventConfig.trigger == CityEntityEventTriggerType.Interact)
		{
			ProcessEvent(cityEntityStateEvent.eventConfig);
		}
		for (int i = 0; i < cityEntityStateEvent.eventConfigs.Length; i++)
		{
			if (cityEntityStateEvent.eventConfigs[i].trigger == CityEntityEventTriggerType.Interact)
			{
				ProcessEvent(cityEntityStateEvent.eventConfigs[i]);
			}
		}
	}

	private void ProcessEvent(CityEntityEventConfig config)
	{
		switch (config.type)
		{
		case CityEntityEventType.UnityEvent:
			config.unityEvent.Invoke();
			break;
		case CityEntityEventType.StartConversation:
			EventMessenger.Instance.Raise(new StartConversationEvent(config.conversation));
			break;
		case CityEntityEventType.EnterShop:
			EventMessenger.Instance.Raise(new EnterShopEvent(GameManager.instance.shopDatabase.GetShopDef(config.shopUID)));
			break;
		case CityEntityEventType.LoadScene:
			EventMessenger.Instance.Raise(new ExitCityEvent(config.sceneName, config.pathNodeName));
			break;
		case CityEntityEventType.ActivateMission:
			Player.instance.AddMission(config.missionUID);
			break;
		case CityEntityEventType.SetTrigger:
			Player.instance.SetTrigger(config.triggerName);
			break;
		case CityEntityEventType.RewardPlayer:
			CityController.instance.StartCoroutine(ShowPlayerRewards(config.rewards));
			break;
		case CityEntityEventType.Teleport:
			EventMessenger.Instance.Raise(new TeleportEvent(config.pathNode));
			break;
		case CityEntityEventType.PlayAnimation:
			if (config.animator != null)
			{
				if (config.gotoEndIfFirst && base.isFirstState)
				{
					config.animator.Play(config.stateName, 0, 1f);
				}
				else
				{
					config.animator.Play(config.stateName);
				}
				config.animator.Update(0f);
			}
			break;
		case CityEntityEventType.UpdateMission:
			CommunicatorManager.instance.UpdateMissionWithTargetMessage(config.targetMessage);
			break;
		case CityEntityEventType.Save:
			Player.instance.Save();
			break;
		case CityEntityEventType.PlaySound:
			GFAudioManager.instance.PlayClipFromResources(config.soundPath, config.soundGroup, string.Empty, config.volume, 0f, config.loop);
			break;
		case CityEntityEventType.StartTournament:
		{
			if (Player.instance.isActivePartyFull)
			{
				EventMessenger.Instance.Raise(new StartTournamentEvent(config.tournament));
				break;
			}
			Conversation conversation = Resources.Load<Conversation>("Data/TT2/CONVO_TOURNYREQ_SIXFIGS");
			if (conversation != null)
			{
				EventMessenger.Instance.Raise(new StartConversationEvent(conversation));
			}
			break;
		}
		case CityEntityEventType.CompleteMission:
			Player.instance.CompleteMission(config.missionUID);
			break;
		case CityEntityEventType.CollectGUIDForMission:
		{
			MissionData missionData = Player.instance.GetMissionData(config.missionUID);
			if (missionData != null && !missionData.collectedGUIDs.Contains(base.guid))
			{
				Debug.Log("Add GUID: " + base.guid + " to collected GUIDs for mission: " + GameManager.instance.communicatorDatabase.GetMission(config.missionUID).title);
				missionData.collectedGUIDs.Add(base.guid);
			}
			break;
		}
		case CityEntityEventType.None:
			break;
		}
	}

	private IEnumerator ShowPlayerRewards(PlayerReward[] rewards)
	{
		for (int i = 0; i < rewards.Length; i++)
		{
			Debug.Log("Start showing reward from entity event");
			yield return CityController.instance.StartCoroutine(CityReward.instance.ShowPlayerReward(rewards[i]));
		}
	}

	protected override void InitState(CityEntityState state)
	{
		CityEntityStateEvent cityEntityStateEvent = (CityEntityStateEvent)base.activeState;
		for (int i = 0; i < _visualObjects.Length; i++)
		{
			if (_visualObjects[i] != null)
			{
				_visualObjects[i].SetActive(cityEntityStateEvent.visualObjectActiveStates[i]);
			}
		}
		bool flag = false;
		if (cityEntityStateEvent.eventConfig.trigger == CityEntityEventTriggerType.Auto)
		{
			ProcessEvent(cityEntityStateEvent.eventConfig);
		}
		else
		{
			flag = true;
		}
		for (int j = 0; j < cityEntityStateEvent.eventConfigs.Length; j++)
		{
			if (cityEntityStateEvent.eventConfigs[j].trigger == CityEntityEventTriggerType.Auto)
			{
				ProcessEvent(cityEntityStateEvent.eventConfigs[j]);
			}
			else
			{
				flag = true;
			}
		}
		if (_boxCollider != null)
		{
			_boxCollider.enabled = flag;
		}
	}

	protected override void InitNullState()
	{
		if (_boxCollider != null)
		{
			_boxCollider.enabled = false;
		}
		for (int i = 0; i < _visualObjects.Length; i++)
		{
			if (_visualObjects[i] != null)
			{
				_visualObjects[i].gameObject.SetActive(false);
			}
		}
	}
}
