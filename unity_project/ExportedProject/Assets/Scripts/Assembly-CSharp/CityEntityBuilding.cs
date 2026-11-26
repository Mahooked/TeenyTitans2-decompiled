using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CityBuilding))]
public class CityEntityBuilding : CityEntity
{
	[SerializeField]
	private CityEntityStateBuilding[] _states;

	[SerializeField]
	private CityBuilding _cityBuilding;

	[SerializeField]
	private PathNode _entranceNode;

	[SerializeField]
	private Landmark _landmark;

	private CityEntityStateBuilding.CityBuildingOverrideSettings originalOverrideSettings;

	private float prevLandmarkMinScale;

	private GameObject landmarkIconTimer;

	private Coroutine updateTimerRoutine;

	public CityBuilding cityBuilding
	{
		get
		{
			return _cityBuilding;
		}
	}

	public PathNode entranceNode
	{
		get
		{
			return _entranceNode;
		}
	}

	public Landmark landmark
	{
		get
		{
			return _landmark;
		}
	}

	public override CityEntityState[] states
	{
		get
		{
			return _states;
		}
	}

	protected override void Start()
	{
		originalOverrideSettings = new CityEntityStateBuilding.CityBuildingOverrideSettings();
		originalOverrideSettings.isOpenOverride = _cityBuilding.isOpen;
		if (_cityBuilding.sign != null)
		{
			originalOverrideSettings.signStateOverride = _cityBuilding.sign.activeState;
		}
		if (_landmark == null)
		{
			_landmark = GetComponent<Landmark>();
		}
		base.Start();
		if (communicator != null)
		{
			communicator.NewsFlashStarted += HandleNewsFlashStarted;
			communicator.NewsFlashEnded += HandleNewsFlashEnded;
		}
	}

	private void OnDestroy()
	{
		if (communicator != null)
		{
			communicator.NewsFlashStarted -= HandleNewsFlashStarted;
			communicator.NewsFlashEnded -= HandleNewsFlashEnded;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		_cityBuilding.uiItem.OnClick += HandleOnClick;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		_cityBuilding.uiItem.OnClick -= HandleOnClick;
	}

	private void HandleOnClick()
	{
		if (!(cameraPinchZoomer != null) || !cameraPinchZoomer.isZoomedOut)
		{
			if (_cityBuilding.animator != null)
			{
				_cityBuilding.animator.Play("ShopBuilding_AnimTap");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_wes_boing", GFAudioManager.SFX_GROUP, string.Empty);
			}
			EventMessenger.Instance.Raise(new BuildingEntityInteractEvent(this));
		}
	}

	public void Interact()
	{
		CityEntityStateBuilding cityEntityStateBuilding = (CityEntityStateBuilding)base.activeState;
		switch (cityEntityStateBuilding.interactConfig.type)
		{
		case CityEntityInteractType.StartConversation:
			EventMessenger.Instance.Raise(new StartConversationEvent(cityEntityStateBuilding.interactConfig.conversation));
			break;
		case CityEntityInteractType.OpenShop:
			EventMessenger.Instance.Raise(new EnterShopEvent(GameManager.instance.shopDatabase.GetShopDef(cityEntityStateBuilding.interactConfig.shopUID), cityEntityStateBuilding.interactConfig.shopInventoryOverride));
			break;
		case CityEntityInteractType.LoadScene:
			EventMessenger.Instance.Raise(new ExitCityEvent(cityEntityStateBuilding.interactConfig.sceneName, cityEntityStateBuilding.interactConfig.pathNodeName));
			break;
		case CityEntityInteractType.StartTournament:
		{
			if (Player.instance.isActivePartyFull)
			{
				EventMessenger.Instance.Raise(new StartTournamentEvent(cityEntityStateBuilding.interactConfig.tournament));
				break;
			}
			Conversation conversation = Resources.Load<Conversation>("Data/TT2/CONVO_TOURNYREQ_SIXFIGS");
			if (conversation != null)
			{
				EventMessenger.Instance.Raise(new StartConversationEvent(conversation));
			}
			break;
		}
		case CityEntityInteractType.UnityEvent:
			cityEntityStateBuilding.interactConfig.unityEvent.Invoke();
			break;
		case CityEntityInteractType.Teleport:
			EventMessenger.Instance.Raise(new TeleportEvent(cityEntityStateBuilding.interactConfig.pathNode));
			if (!string.IsNullOrEmpty(cityEntityStateBuilding.interactConfig.teleportSound))
			{
				GFAudioManager.instance.PlayClipFromResources(cityEntityStateBuilding.interactConfig.teleportSound, GFAudioManager.SFX_GROUP, string.Empty);
			}
			break;
		case CityEntityInteractType.None:
			break;
		}
	}

	private void HandleNewsFlashStarted(NewsFlash newsFlash)
	{
		CityEntityStateBuilding cityEntityStateBuilding = base.activeState as CityEntityStateBuilding;
		if (cityEntityStateBuilding == null)
		{
			Debug.LogError("City Entity Building, Handle News Flash, name: " + base.gameObject.name + ", Active State CANNOT be cast to City Entity State Building, Type: " + ((base.activeState == null) ? "NULL" : base.activeState.GetType().ToString()));
		}
		else if (cityEntityStateBuilding.interactConfig == null)
		{
			Debug.LogError("City Entity Building, Handle News Flash, name: " + base.gameObject.name + ", BuildingState.interactConfig is NULL");
		}
		else
		{
			if (cityEntityStateBuilding.interactConfig.type != CityEntityInteractType.OpenShop || newsFlash.targetShopUID != cityEntityStateBuilding.interactConfig.shopUID)
			{
				return;
			}
			_landmark.UpdateSpriteIcon("gui_citynotifiers_newsflash");
			_landmark.zoomedInSettings.hideAtMinScale = false;
			prevLandmarkMinScale = _landmark.zoomedInSettings.minScale;
			_landmark.zoomedInSettings.minScale = 0.85f;
			_landmark.UpdateZoomSettings();
			if (newsFlash.showTimer)
			{
				landmarkIconTimer = ObjectPool.GetObjectFromResource("Prefabs/GUI/LandmarkIconCountdownText");
				landmarkIconTimer.transform.parent = _landmark.icon.sprite.transform;
				landmarkIconTimer.transform.localPosition = new Vector3(0f, -14.8f, -0.01f);
				landmarkIconTimer.transform.localScale = Vector3.one;
				if (updateTimerRoutine != null)
				{
					StopCoroutine(updateTimerRoutine);
				}
				updateTimerRoutine = StartCoroutine(UpdateLandmarkIconTimer(newsFlash));
			}
		}
	}

	private IEnumerator UpdateLandmarkIconTimer(NewsFlash newsFlash)
	{
		TextMeshPro timerText = landmarkIconTimer.GetComponent<TextMeshPro>();
		NewsFlashData newsFlashData = Player.instance.GetNewsFlashData(newsFlash.uid);
		float displayTime = newsFlashData.timeActive;
		float timeLeft = (float)newsFlash.durationTime - ((float)communicator.GetCurrentTime() - displayTime);
		while (timeLeft > 0f)
		{
			timeLeft = (float)newsFlash.durationTime - ((float)communicator.GetCurrentTime() - displayTime);
			if (timeLeft < 0f)
			{
				timeLeft = 0f;
			}
			int mins = (int)timeLeft / 60;
			int sec = (int)(timeLeft % 60f);
			if (mins < 0)
			{
				mins = 0;
			}
			if (sec < 0)
			{
				sec = 0;
			}
			string minStr = ((mins >= 10) ? mins.ToString() : ("0" + mins));
			string secStr = ((sec >= 10) ? sec.ToString() : ("0" + sec));
			timerText.text = minStr + ":" + secStr;
			yield return null;
		}
	}

	private void HandleNewsFlashEnded(NewsFlash newsFlash)
	{
		CityEntityStateBuilding cityEntityStateBuilding = base.activeState as CityEntityStateBuilding;
		if (cityEntityStateBuilding != null && cityEntityStateBuilding.interactConfig != null && cityEntityStateBuilding.interactConfig.type == CityEntityInteractType.OpenShop && newsFlash.targetShopUID == cityEntityStateBuilding.interactConfig.shopUID)
		{
			_landmark.ResetSpriteIcon();
			_landmark.zoomedInSettings.hideAtMinScale = true;
			_landmark.zoomedInSettings.minScale = prevLandmarkMinScale;
			_landmark.UpdateZoomSettings();
			if (landmarkIconTimer != null)
			{
				ObjectPool.PoolObject(landmarkIconTimer);
			}
		}
	}

	protected override void InitState(CityEntityState state)
	{
		CityEntityStateBuilding cityEntityStateBuilding = (CityEntityStateBuilding)state;
		if (cityEntityStateBuilding.overrideSettings.overrideIsOpen)
		{
			_cityBuilding.isOpen = cityEntityStateBuilding.overrideSettings.isOpenOverride;
		}
		else
		{
			_cityBuilding.isOpen = originalOverrideSettings.isOpenOverride;
		}
		if (_cityBuilding.sign != null)
		{
			if (cityEntityStateBuilding.overrideSettings.overrideSignState)
			{
				_cityBuilding.sign.SetState(cityEntityStateBuilding.overrideSettings.signStateOverride);
			}
			else
			{
				_cityBuilding.sign.SetState(originalOverrideSettings.signStateOverride);
			}
		}
	}

	protected override void InitNullState()
	{
		_cityBuilding.isOpen = originalOverrideSettings.isOpenOverride;
		if (_cityBuilding.sign != null)
		{
			_cityBuilding.sign.SetState(originalOverrideSettings.signStateOverride);
		}
	}
}
