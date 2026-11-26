using System.Collections;
using TMPro;
using UnityEngine;

public class CityEntityNPC : CityEntity
{
	[SerializeField]
	private CityEntityStateNPC[] _states;

	[SerializeField]
	private tk2dUIItem _uiItem;

	[SerializeField]
	private string _npcUID;

	[SerializeField]
	private PathNode _nearestNode;

	private NPCDefinition npcDef;

	private BoxCollider boxCollider;

	private Animator npcAnimator;

	private AEControllerTk2d npcAnimatorTk2d;

	private bool _isHidden;

	private Camera _mainCamera;

	private float prevLandmarkMinScale;

	private GameObject landmarkIconTimer;

	private Coroutine updateTimerRoutine;

	public PathNode nearestNode
	{
		get
		{
			return _nearestNode;
		}
		set
		{
			_nearestNode = value;
		}
	}

	public NPCDefinition npcDefinition => npcDef;

	public string npcGuid => _npcUID;

	public string characterName => (!string.IsNullOrEmpty(npcDef.nameKey)) ? Language.Get(npcDef.nameKey, "NPCS,FIGS") : npcDef.name;

	public CityNPCNew cityNPC { get; private set; }

	public bool isHidden
	{
		get
		{
			return _isHidden;
		}
		set
		{
			if (value == _isHidden)
			{
				return;
			}
			_isHidden = value;
			if (_isHidden)
			{
				if (cityNPC != null)
				{
					cityNPC.gameObject.SetActive(value: false);
				}
				_uiItem.enabled = false;
				boxCollider.enabled = false;
				if (CityController.instance != null)
				{
					CityController.instance.RemoveActiveNPCEntity(this);
				}
				return;
			}
			if (cityNPC != null)
			{
				cityNPC.gameObject.SetActive(value: true);
			}
			if (activeState != null)
			{
				_uiItem.enabled = true;
				boxCollider.enabled = true;
				if (CityController.instance != null)
				{
					CityController.instance.AddActiveNPCEntity(this);
				}
			}
			else if (CityController.instance != null)
			{
				CityController.instance.RemoveActiveNPCEntity(this);
			}
		}
	}

	private Camera mainCamera
	{
		get
		{
			if (_mainCamera == null)
			{
				_mainCamera = ((!(CityController.instance != null)) ? Camera.main : CityController.instance.pinchZoomer.thisCamera);
			}
			return _mainCamera;
		}
	}

	public override CityEntityState[] states => _states;

	protected override void Awake()
	{
		base.Awake();
		if (_uiItem == null)
		{
			_uiItem = GetComponent<tk2dUIItem>();
		}
		npcDef = GameManager.instance.npcDatabase.GetNPCDef(_npcUID);
		boxCollider = GetComponent<BoxCollider>();
	}

	protected override void Start()
	{
		base.Start();
		TransformLink component = GetComponent<TransformLink>();
		if (component != null)
		{
			component.LinkTo = cameraPinchZoomer.transform;
		}
		cameraPinchZoomer.ZoomedIn += HandleZoomedIn;
		cameraPinchZoomer.ZoomedOut += HandleZoomedOut;
		if (communicator != null)
		{
			communicator.NewsFlashStarted += HandleNewsFlashStarted;
			communicator.NewsFlashEnded += HandleNewsFlashEnded;
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

	private void OnDestroy()
	{
		if (cameraPinchZoomer != null)
		{
			cameraPinchZoomer.ZoomedIn -= HandleZoomedIn;
			cameraPinchZoomer.ZoomedOut -= HandleZoomedOut;
		}
		if (communicator != null)
		{
			communicator.NewsFlashStarted -= HandleNewsFlashStarted;
			communicator.NewsFlashEnded -= HandleNewsFlashEnded;
		}
	}

	private void HandleOnClick()
	{
		if (!(cameraPinchZoomer != null) || !cameraPinchZoomer.isZoomedOut)
		{
			if (cityNPC.animator != null)
			{
				cityNPC.animator.Play("NPC_City_tap");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_city_touchnpc", GFAudioManager.SFX_GROUP, string.Empty);
			}
			Interact();
		}
	}

	private void HandleZoomedIn()
	{
		if (cityNPC != null)
		{
			if (cityNPC.animatorController != null)
			{
				cityNPC.animatorTk2d.renderersEnabled = true;
			}
			else if (cityNPC.staticSprite != null)
			{
				cityNPC.staticSpriteRenderer.enabled = true;
			}
			cityNPC.iconRenderer.enabled = true;
		}
	}

	private void HandleZoomedOut()
	{
		if (cityNPC != null)
		{
			if (cityNPC.animatorTk2d != null)
			{
				cityNPC.animatorTk2d.renderersEnabled = false;
			}
			else if (cityNPC.staticSprite != null)
			{
				cityNPC.staticSpriteRenderer.enabled = false;
			}
			cityNPC.iconRenderer.enabled = false;
		}
	}

	private void Interact()
	{
		EventMessenger.Instance.Raise(new NPCEntityInteractEvent(this));
	}

	public void FaceTarget(Transform target)
	{
		Vector3 vector = Camera.main.WorldToViewportPoint(transform.position);
		if (Camera.main.WorldToViewportPoint(target.position).x < vector.x)
		{
			if (!npcDef.prefabPreventFlip && cityNPC != null)
			{
				if (cityNPC.animatorTk2d != null)
				{
					cityNPC.animatorTk2d.isFlipped = !npcDef.prefabFacingLeft;
				}
				else if (cityNPC.staticSprite != null)
				{
					cityNPC.staticSprite.FlipX = !npcDef.prefabFacingLeft;
				}
			}
		}
		else if (!npcDef.prefabPreventFlip && cityNPC != null)
		{
			if (cityNPC.animatorTk2d != null)
			{
				cityNPC.animatorTk2d.isFlipped = (npcDef.prefabFacingLeft ? true : false);
			}
			else if (cityNPC.staticSprite != null)
			{
				cityNPC.staticSprite.FlipX = (npcDef.prefabFacingLeft ? true : false);
			}
		}
	}

	public bool NeededInActiveDailyMission()
	{
		if (DailyMissionManager.instance.currentDailyMission == null)
		{
			return false;
		}
		DailyMissionData currentDailyMission = DailyMissionManager.instance.currentDailyMission;
		if (currentDailyMission.type == DailyMissionType.DefeatNPC && currentDailyMission.battleConfig.npcGuid == npcDef.guid)
		{
			return true;
		}
		if (currentDailyMission.type == DailyMissionType.Shopping && currentDailyMission.missionLocationNPC == npcDef.guid)
		{
			return true;
		}
		return false;
	}

	private void HandleNewsFlashStarted(NewsFlash newsFlash)
	{
		if (npcDef == null || cityNPC == null || cityNPC.landmark == null)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < npcDef.convoButtonConfigs.Length; i++)
		{
			if (npcDef.convoButtonConfigs[i].type == ConvoButtonType.Shop)
			{
				num = npcDef.convoButtonConfigs[i].shopUID;
				break;
			}
		}
		if (num == -1 || num != newsFlash.targetShopUID)
		{
			return;
		}
		cityNPC.landmark.UpdateSpriteIcon("gui_citynotifiers_newsflash");
		cityNPC.landmark.zoomedInSettings.hideAtMinScale = false;
		prevLandmarkMinScale = cityNPC.landmark.zoomedInSettings.minScale;
		cityNPC.landmark.zoomedInSettings.minScale = 0.85f;
		cityNPC.landmark.UpdateZoomSettings();
		if (newsFlash.showTimer)
		{
			landmarkIconTimer = ObjectPool.GetObjectFromResource("Prefabs/GUI/LandmarkIconCountdownText");
			landmarkIconTimer.transform.parent = cityNPC.landmark.icon.sprite.transform;
			landmarkIconTimer.transform.localPosition = new Vector3(0f, -14.8f, -0.01f);
			landmarkIconTimer.transform.localScale = Vector3.one;
			if (updateTimerRoutine != null)
			{
				StopCoroutine(updateTimerRoutine);
			}
			updateTimerRoutine = StartCoroutine(UpdateLandmarkIconTimer(newsFlash));
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
		updateTimerRoutine = null;
	}

	private void HandleNewsFlashEnded(NewsFlash newsFlash)
	{
		if (npcDef == null || cityNPC == null || cityNPC.landmark == null)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < npcDef.convoButtonConfigs.Length; i++)
		{
			if (npcDef.convoButtonConfigs[i].type == ConvoButtonType.Shop)
			{
				num = npcDef.convoButtonConfigs[i].shopUID;
				break;
			}
		}
		if (num != -1 && num == newsFlash.targetShopUID)
		{
			if (updateTimerRoutine != null)
			{
				StopCoroutine(updateTimerRoutine);
			}
			cityNPC.landmark.ResetSpriteIcon();
			cityNPC.landmark.zoomedInSettings.hideAtMinScale = true;
			cityNPC.landmark.zoomedInSettings.minScale = prevLandmarkMinScale;
			cityNPC.landmark.UpdateZoomSettings();
			if (landmarkIconTimer != null)
			{
				ObjectPool.PoolObject(landmarkIconTimer);
			}
		}
	}

	protected override void InitState(CityEntityState state)
	{
		CityEntityStateNPC cityEntityStateNPC = (CityEntityStateNPC)state;
		string text;
		if (string.IsNullOrEmpty(cityEntityStateNPC.prefabPathOverride))
		{
			text = ((!(npcDef != null)) ? string.Empty : npcDef.prefabPath);
		}
		else
		{
			text = cityEntityStateNPC.prefabPathOverride;
		}
		if (!string.IsNullOrEmpty(text))
		{
			if (cityNPC != null)
			{
				ObjectPool.PoolObject(cityNPC.gameObject, instant: true);
			}
			cityNPC = ObjectPool.GetObjectFromResource<CityNPCNew>(text);
			if (cityNPC != null)
			{
				cityNPC.transform.SetParent(transform, worldPositionStays: true);
				cityNPC.transform.localPosition = Vector3.zero;
				cityNPC.transform.localRotation = Quaternion.identity;
				cityNPC.transform.localScale = Vector3.one;
				if (cityNPC.boxCollider != null)
				{
					boxCollider.center = cityNPC.boxCollider.center;
					boxCollider.size = cityNPC.boxCollider.size;
				}
				if (_isHidden)
				{
					cityNPC.gameObject.SetActive(value: false);
				}
			}
			else
			{
				Debug.LogError("There is no valid prefab for this City NPC Entity in its currenty state");
			}
		}
		_uiItem.enabled = !_isHidden;
		boxCollider.enabled = !_isHidden;
		if (isHidden)
		{
			CityController.instance.RemoveActiveNPCEntity(this);
		}
		else
		{
			CityController.instance.AddActiveNPCEntity(this);
		}
		CheckForMissionActivation(cityEntityStateNPC);
		if (cityEntityStateNPC.immediateConversation != null)
		{
			CityController.instance.HandleShowDialogue(cityEntityStateNPC.immediateConversation, showExitButton: false, this, cityEntityStateNPC);
		}
	}

	protected override void InitNullState()
	{
		if (cityNPC != null)
		{
			ObjectPool.PoolObject(cityNPC.gameObject, instant: true);
			cityNPC = null;
		}
		CityController.instance.RemoveActiveNPCEntity(this);
		_uiItem.enabled = false;
		boxCollider.enabled = false;
	}

	private void CheckForMissionActivation(CityEntityStateNPC state)
	{
		if (cityNPC == null || cityNPC.iconHolder == null)
		{
			return;
		}
		int num = -1;
		if (state.greetingConversation != null && state.greetingConversation.missionUID > -1)
		{
			num = state.greetingConversation.missionUID;
		}
		else if (state.mainConversation != null && state.mainConversation.missionUID > -1)
		{
			num = state.mainConversation.missionUID;
		}
		else if (state.preBattleConversation != null && state.preBattleConversation.missionUID > -1)
		{
			num = state.preBattleConversation.missionUID;
		}
		if (num > -1)
		{
			MissionData missionData = player.GetMissionData(num);
			MissionCompletedData missionCompletedData = player.GetMissionCompletedData(num);
			if (GameManager.instance.communicatorDatabase.GetMission(num).type == MissionType.Side && missionData == null && missionCompletedData == null)
			{
				cityNPC.iconHolder.gameObject.SetActive(value: true);
				if (cityNPC.landmark != null)
				{
					cityNPC.landmark.Show("gui_citynotifiers_missionbubble");
				}
			}
			else
			{
				cityNPC.iconHolder.gameObject.SetActive(value: false);
				if (cityNPC.landmark != null)
				{
					cityNPC.landmark.ResetSpriteIcon();
				}
			}
		}
		else
		{
			cityNPC.iconHolder.gameObject.SetActive(value: false);
			if (cityNPC.landmark != null)
			{
				cityNPC.landmark.ResetSpriteIcon();
			}
		}
	}

	protected override void Player_MissionAdded(int arg1, bool arg2)
	{
		CityEntityState objA = activeState;
		base.Player_MissionAdded(arg1, arg2);
		if (object.ReferenceEquals(objA, activeState))
		{
			CheckForMissionActivation((CityEntityStateNPC)activeState);
		}
	}
}
