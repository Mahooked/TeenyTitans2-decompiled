using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(tk2dUIItem))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(TransformLink))]
public class CityNPC : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public Conversation conversationDefeated;

	private bool defeatedNPC;

	public GameObject missionIndicator;

	public CharacterName character;

	public string spawnSetupName = string.Empty;

	public int databaseID;

	public int instanceID;

	public TimeOfDay timeOfDay;

	public List<CityNPCData> npcData = new List<CityNPCData>();

	public bool flipCharacter;

	public CityNPCData activeNPCData;

	private AEControllerTk2d animationController;

	private tk2dSprite placeholderSprite;

	private Animator npcAnimator;

	private bool isDayTime;

	private GameObject npcPrefab;

	private Landmark landmarkIcon;

	private float minTalkingDist = 0.5f;

	private PathNode closestNode;

	private static int totalIndex;

	private Vector2 renderRange = new Vector2(-0.2f, 1.2f);

	private int checkIndex;

	private bool disabled;

	private float lastCheck;

	private float distanceFactor = 1f;

	private AudioSource cityIdleSFXSource;

	private float idleSFXTimer;

	private bool ignoreHideNPCPrefab;

	private NPCDefinition _npcDef;

	private Conversation _conversation;

	private TitleScreenController _titleScreenController;

	private Camera _mainCamera;

	private bool _shouldEnable;

	private bool hasPoofedIn;

	private bool isPoofing;

	private Coroutine moveAfterConvoRoutine;

	public bool dayOnly => timeOfDay == TimeOfDay.Day || timeOfDay == TimeOfDay.Both;

	public bool nightOnly => timeOfDay == TimeOfDay.Night || timeOfDay == TimeOfDay.Both;

	public NPCDefinition npcDef
	{
		get
		{
			if (_npcDef == null)
			{
				_npcDef = GameManager.instance.npcDatabase.GetNPCDefByType(character);
			}
			return _npcDef;
		}
	}

	public Conversation conversation
	{
		get
		{
			Conversation conversation = CheckForActiveMissionConvo();
			if (conversation != null)
			{
				return (Conversation)Resources.Load("Data/TT2/" + CityCharacter.GetCityCharacterConvoName(conversation.name));
			}
			if (_conversation != null)
			{
				return (Conversation)Resources.Load("Data/TT2/" + CityCharacter.GetCityCharacterConvoName(_conversation.name));
			}
			return _conversation;
		}
		set
		{
			_conversation = value;
		}
	}

	public AEControllerTk2d AnimController => animationController;

	private TitleScreenController titleScreenController
	{
		get
		{
			if (_titleScreenController == null)
			{
				_titleScreenController = TitleScreenController.instance;
			}
			return _titleScreenController;
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

	private bool shouldEnable
	{
		get
		{
			return _shouldEnable;
		}
		set
		{
			_shouldEnable = value;
		}
	}

	public virtual List<TournamentRoundFigureData> figures
	{
		get
		{
			if (activeNPCData != null)
			{
				return activeNPCData.figures;
			}
			List<TournamentRoundFigureData> list = new List<TournamentRoundFigureData>();
			for (int i = 0; i < 3; i++)
			{
				TournamentRoundFigureData tournamentRoundFigureData = new TournamentRoundFigureData();
				tournamentRoundFigureData.characterUID = i;
				list.Add(tournamentRoundFigureData);
			}
			return list;
		}
	}

	public string preBattleSprite
	{
		get
		{
			if (npcDef != null)
			{
				return npcDef.preBattleSprite;
			}
			return "menu_vsscreen_portrait_questionmark";
		}
	}

	public string npcPrefabPath
	{
		get
		{
			if (npcDef != null)
			{
				return npcDef.prefabPath;
			}
			return "Prefabs/Characters/City/OldMan";
		}
	}

	public virtual string characterName
	{
		get
		{
			if (npcDef != null)
			{
				return (!string.IsNullOrEmpty(npcDef.nameKey)) ? Language.Get(npcDef.nameKey, "NPCS,FIGS") : npcDef.name;
			}
			return "Dude";
		}
	}

	public string Guid
	{
		get
		{
			if (npcDef != null)
			{
				if (character == CharacterName.ADog)
				{
					return npcDef.guid + spawnSetupName;
				}
				return npcDef.guid;
			}
			return string.Empty;
		}
	}

	protected virtual void OnEnable()
	{
		Player.instance.MissionAdded += HandleMissionAdded;
		Player.instance.MissionCompleted += HandleMissionCompleted;
		Player.instance.MissionCollected += HandleMissionCollected;
		Player.instance.MissionUpdated += HandleMissionUpdated;
		Player.instance.GameKeyTotalChange += HandleGameKeyTotalChange;
		if (FlashSaleController.instance != null)
		{
			FlashSaleController.instance.FlashSaleStarted += HandleFlashSaleStarted;
			FlashSaleController.instance.FlashSaleEnded += HandleFlashSaleEnded;
		}
		if (CityController.instance != null)
		{
			CityController.instance.FinishedDialogue += CheckShouldEnable;
			CityController.instance.pinchZoomer.ZoomedIn += HandleZoomedIn;
		}
		isPoofing = false;
	}

	protected virtual void OnDisable()
	{
		Player.instance.MissionAdded -= HandleMissionAdded;
		Player.instance.MissionCompleted -= HandleMissionCompleted;
		Player.instance.MissionCollected -= HandleMissionCollected;
		Player.instance.MissionUpdated -= HandleMissionUpdated;
		Player.instance.GameKeyTotalChange -= HandleGameKeyTotalChange;
		if (FlashSaleController.instance != null)
		{
			FlashSaleController.instance.FlashSaleStarted -= HandleFlashSaleStarted;
			FlashSaleController.instance.FlashSaleEnded -= HandleFlashSaleEnded;
		}
		if (CityController.instance != null)
		{
			CityController.instance.FinishedDialogue -= CheckShouldEnable;
			CityController.instance.pinchZoomer.ZoomedIn -= HandleZoomedIn;
		}
	}

	protected virtual void OnDestroy()
	{
		if (CityController.instance != null && CityController.instance.communicator != null)
		{
			CityController.instance.communicator.StartDayTime -= CheckShouldEnable;
			CityController.instance.communicator.StartNightTime -= CheckShouldEnable;
			if (CityController.instance.activeNpcs.Contains(this))
			{
				CityController.instance.activeNpcs.Remove(this);
			}
		}
		if (animationController != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.RemoveAnimationControllerToTint(animationController);
		}
	}

	private void HandleZoomedIn()
	{
		StopCoroutine("IgnoreHideNPCPrefab");
		StartCoroutine("IgnoreHideNPCPrefab");
	}

	private IEnumerator IgnoreHideNPCPrefab()
	{
		ignoreHideNPCPrefab = true;
		if (npcPrefab != null)
		{
			npcPrefab.SetActive(value: true);
		}
		yield return new WaitForSeconds(3f);
		ignoreHideNPCPrefab = false;
	}

	private void HandleGameKeyTotalChange()
	{
		CheckShouldEnable();
	}

	private void HandleMissionAdded(int missionUID, bool dialogueMission)
	{
		CheckShouldEnable();
	}

	private void HandleMissionCompleted(int missionUID)
	{
		CheckShouldEnable();
	}

	private void HandleMissionCollected(int missionUID)
	{
		CheckShouldEnable();
	}

	private void HandleMissionUpdated(int missionUID)
	{
		CheckShouldEnable();
	}

	private void HandleFlashSaleStarted()
	{
		CheckShouldEnable();
	}

	private void HandleFlashSaleEnded()
	{
		CheckShouldEnable();
	}

	private IEnumerator Start()
	{
		GetComponent<TransformLink>().LinkTo = mainCamera.transform.parent;
		if (CityController.instance != null)
		{
			CityController.instance.communicator.StartDayTime += CheckShouldEnable;
			CityController.instance.communicator.StartNightTime += CheckShouldEnable;
		}
		uiItem.OnClickUIItem += OnNPCClicked;
		uiItem.enabled = false;
		CheckShouldEnable();
		SetupComponentValues();
		checkIndex = totalIndex++;
		if (totalIndex > 12)
		{
			totalIndex = 2;
		}
		if (cityIdleSFXSource == null && (npcDef.characterType == CharacterName.Robin || npcDef.characterType == CharacterName.Cyborg || npcDef.characterType == CharacterName.Starfire || npcDef.characterType == CharacterName.BeastBoy || npcDef.characterType == CharacterName.Raven))
		{
			cityIdleSFXSource = ObjectPool.GetObjectFromResource("Prefabs/FX/CityIdleAudioFX").GetComponent<AudioSource>();
			cityIdleSFXSource.transform.parent = transform;
			cityIdleSFXSource.transform.localPosition = Vector3.zero;
		}
		idleSFXTimer = 10f;
		npcAnimator = GetComponentInChildren<Animator>();
		yield return new WaitForEndOfFrame();
	}

	private void Update()
	{
		if (character == CharacterName.HoodedHood || character == CharacterName.MrChibi || character == CharacterName.SpaceCabbie)
		{
			if (npcPrefab != null && !isPoofing)
			{
				npcPrefab.SetActive(value: true);
				if (animationController != null)
				{
					animationController.gameObject.SetActive(value: true);
				}
			}
		}
		else
		{
			if (isPoofing || ignoreHideNPCPrefab)
			{
				return;
			}
			if (Time.time - lastCheck > 0.016f * (float)checkIndex * distanceFactor)
			{
				lastCheck = Time.time;
				Vector3 vector = mainCamera.WorldToViewportPoint(transform.position);
				distanceFactor = 1f;
				if (vector.x < renderRange.x * 6f || vector.x > renderRange.y * 6f)
				{
					distanceFactor = 4f;
				}
				if (vector.y < renderRange.x * 6f || vector.y > renderRange.y * 6f)
				{
					distanceFactor = 4f;
				}
				if (vector.x >= renderRange.x && vector.x <= renderRange.y && vector.y >= renderRange.x && vector.y <= renderRange.y)
				{
					if (disabled)
					{
						disabled = false;
						if (npcPrefab != null)
						{
							npcPrefab.SetActive(value: true);
						}
					}
				}
				else if (!disabled)
				{
					disabled = true;
					if (npcPrefab != null)
					{
						npcPrefab.SetActive(value: false);
					}
				}
				if (npcPrefab != null)
				{
					if (disabled && npcPrefab.activeSelf)
					{
						disabled = false;
					}
					if (!disabled && !npcPrefab.activeSelf)
					{
						disabled = true;
					}
				}
			}
			if (!(cityIdleSFXSource != null) || !(npcPrefab != null) || !npcPrefab.activeInHierarchy || (CityController.instance.pinchZoomer.isZoomedOut && character != CharacterName.Robin))
			{
				return;
			}
			idleSFXTimer -= Time.deltaTime;
			if (!(idleSFXTimer <= 0f) || npcDef.cityIdleSFX.Count <= 0)
			{
				return;
			}
			idleSFXTimer = 10f;
			float value = Random.value;
			if (Vector3.Distance(CityController.instance.cameraHolder.transform.position, transform.position) <= cityIdleSFXSource.maxDistance && !TitleScreenController.instance.titleScreenActive && !FlashSaleController.instance.FlashSaleActive && !FlashSaleController.instance.IntroSequenceActive && !CityController.instance.communicator.PopUpActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !DayNightCard.instance.isActive && !CityController.instance.IsCityVOPlaying() && !GFAudioManager.instance.IsGroupMuted(GFAudioManager.VOICEOVER_GROUP))
			{
				string text = npcDef.cityIdleSFX[Random.Range(0, npcDef.cityIdleSFX.Count)];
				if (!text.Contains(cityIdleSFXSource.clip.name))
				{
					cityIdleSFXSource.clip = Resources.Load<AudioClip>(text);
				}
				cityIdleSFXSource.Play();
				CityController.instance.RegisterCityVO(cityIdleSFXSource);
			}
		}
	}

	public void SetupComponentValues()
	{
		GetComponent<BoxCollider>().size = npcDef.colliderSize;
		GetComponent<BoxCollider>().center = npcDef.colliderCenter;
		GetComponent<TransformLink>().linkRotation = true;
	}

	private void OnNPCClicked(tk2dUIItem uiItem)
	{
		if (CityController.instance.cityCharacter.AllowInput && !CityReward.instance.showingRewards && (character != CharacterName.BwanaBeast || SaveUtility.GetInt("starfish7BwanaEnable") > 0))
		{
			if (npcAnimator != null)
			{
				npcAnimator.Play("NPC_City_tap");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_city_touchnpc", GFAudioManager.SFX_GROUP, string.Empty);
			}
			if (closestNode == null)
			{
				closestNode = CityController.instance.GetNearestPathNodeToPosition(transform.position);
			}
			if (Vector3.Distance(transform.position, CityController.instance.cityCharacter.transform.position) <= minTalkingDist || (CityController.instance.cityCharacter.CurrentNode == closestNode && Vector3.Distance(CityController.instance.cityCharacter.transform.position, closestNode.transform.position) < 0.1f))
			{
				InteractWithNPC();
				return;
			}
			CityController.instance.pinchZoomer.ZoomIn();
			CityController.instance.cityCharacter.MoveToInteractiveNode(closestNode, InteractWithNPC);
		}
	}

	private void InteractWithNPC()
	{
		CityController.instance.cityCharacter.StopMove();
		if ((TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive) || DayNightCard.instance.isActive || CityReward.instance.showingRewards)
		{
			return;
		}
		CheckDeliveryMission();
		GameObject gameObject = null;
		if (animationController != null)
		{
			gameObject = animationController.gameObject;
		}
		if (gameObject == null)
		{
			gameObject = placeholderSprite.gameObject;
		}
		if (gameObject != null)
		{
			Vector3 vector = CityController.instance.pinchZoomer.thisCamera.WorldToViewportPoint(CityController.instance.cityCharacter.transform.position);
			Vector3 vector2 = CityController.instance.pinchZoomer.thisCamera.WorldToViewportPoint(gameObject.transform.position);
			if (vector.x < vector2.x)
			{
				if (!npcDef.prefabPreventFlip)
				{
					if (animationController != null)
					{
						animationController.isFlipped = !npcDef.prefabFacingLeft;
					}
					if (placeholderSprite != null)
					{
						placeholderSprite.transform.localEulerAngles = new Vector3(0f, (!npcDef.prefabFacingLeft) ? 180f : 0f, 0f);
					}
				}
				CityController.instance.cityCharacter.animator.isFlipped = true;
			}
			else
			{
				if (!npcDef.prefabPreventFlip)
				{
					if (animationController != null)
					{
						animationController.isFlipped = (npcDef.prefabFacingLeft ? true : false);
					}
					if (placeholderSprite != null)
					{
						placeholderSprite.transform.localEulerAngles = new Vector3(0f, (!npcDef.prefabFacingLeft) ? 0f : 180f, 0f);
					}
				}
				CityController.instance.cityCharacter.animator.isFlipped = false;
			}
		}
		CityController.instance.HandleOnNPCDown(uiItem);
	}

	private void CheckShouldEnable()
	{
		if (character == CharacterName.HoodedHood)
		{
			return;
		}
		shouldEnable = false;
		bool flag = false;
		if (FlashSaleController.instance.FlashSaleActive)
		{
			if (activeNPCData == null || activeNPCData.disablesGameObject == null)
			{
				shouldEnable = false;
				DisableNPC();
			}
			return;
		}
		isDayTime = !(CityController.instance != null) || CityController.instance.communicator.isDayTime;
		if (timeOfDay != TimeOfDay.Both)
		{
			if (dayOnly && !isDayTime)
			{
				shouldEnable = false;
				DisableNPC();
				return;
			}
			if (nightOnly && isDayTime)
			{
				shouldEnable = false;
				DisableNPC();
				return;
			}
		}
		if (Player.instance.HasDefeatedNPC(Guid) && !NeededInActiveDailyMission())
		{
			defeatedNPC = true;
			shouldEnable = false;
		}
		else
		{
			defeatedNPC = false;
		}
		for (int i = 0; i < npcData.Count; i++)
		{
			CityNPCData cityNPCData = npcData[i];
			if (cityNPCData.activeOnActiveMissionUID > -1)
			{
				MissionData missionData = Player.instance.GetMissionData(cityNPCData.activeOnActiveMissionUID);
				if (missionData != null && !missionData.completed)
				{
					shouldEnable = true;
				}
			}
			if (cityNPCData.activeOnCompletedMissionUID > -1)
			{
				MissionData missionData2 = Player.instance.GetMissionData(cityNPCData.activeOnCompletedMissionUID);
				MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(cityNPCData.activeOnCompletedMissionUID);
				if (missionCompletedData != null || (missionData2 != null && missionData2.completed))
				{
					shouldEnable = true;
				}
			}
			else if (cityNPCData.activeOnCompletedMissionUID == -1)
			{
				shouldEnable = true;
			}
			if (cityNPCData.hidesOnCompletedMissionUID > -1 && Player.instance.HasMissionCompleted(cityNPCData.hidesOnCompletedMissionUID))
			{
				shouldEnable = false;
			}
			if (cityNPCData.activeOnCompletedMissionUID == -1 && cityNPCData.hidesOnCompletedMissionUID == -1)
			{
				shouldEnable = true;
			}
			if (shouldEnable)
			{
				activeNPCData = cityNPCData;
			}
			if (!shouldEnable || activeNPCData == null || !(cityNPCData.initialConversation != null))
			{
				continue;
			}
			if (cityNPCData.initialConversation.missionUID < 0)
			{
				conversation = cityNPCData.initialConversation;
				if (cityNPCData.requiresPreviousConvoPlayed && cityNPCData.requiredConversation != null && SaveUtility.GetInt(cityNPCData.requiredConversation.name) != 1)
				{
					if (cityNPCData.alternateConversation != null)
					{
						conversation = cityNPCData.alternateConversation;
					}
					else if (cityNPCData.firstMessage.Count > 0)
					{
						conversation = cityNPCData.firstMessage[Random.Range(0, cityNPCData.firstMessage.Count)];
					}
				}
				if (cityNPCData.requiresMissionItem && cityNPCData.alternateConversation != null && SaveUtility.GetInt(cityNPCData.requiredMissionItemName) == 0)
				{
					if (cityNPCData.alternateConversation != null)
					{
						conversation = cityNPCData.alternateConversation;
					}
					else if (cityNPCData.firstMessage.Count > 0)
					{
						conversation = cityNPCData.firstMessage[Random.Range(0, cityNPCData.firstMessage.Count)];
					}
				}
				if (cityNPCData.preBattleConversations.Count > 0)
				{
					for (int j = 0; j < cityNPCData.preBattleConversations.Count; j++)
					{
						if (cityNPCData.preBattleConversations[j].missionUID > -1)
						{
							Mission mission = GameManager.instance.communicatorDatabase.GetMission(cityNPCData.preBattleConversations[j].missionUID);
							MissionData missionData3 = Player.instance.GetMissionData(cityNPCData.preBattleConversations[j].missionUID);
							if (mission != null && missionData3 != null && missionData3.active && missionData3.completed && !missionData3.collected && mission.completionConvo != null)
							{
								conversation = (Conversation)mission.completionConvo;
							}
						}
					}
				}
				if (cityNPCData.alternateConvoAfterDays)
				{
					int currentDay = Player.instance.currentDay;
					MissionCompletedData missionCompletedData2 = Player.instance.GetMissionCompletedData(cityNPCData.activeOnCompletedMissionUID);
					if (missionCompletedData2 != null && currentDay >= missionCompletedData2.dayCompleted + cityNPCData.daysAfterMissionPlayAlternateConvo && cityNPCData.alternateConversation != null)
					{
						conversation = cityNPCData.alternateConversation;
					}
				}
				if (cityNPCData.initialConversation.completesMissionUID > 0)
				{
					Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(cityNPCData.initialConversation.completesMissionUID);
					MissionData missionData4 = Player.instance.GetMissionData(cityNPCData.initialConversation.completesMissionUID);
					if (mission2.type == MissionType.Side && missionData4 != null && missionData4.active && missionData4.completed && !missionData4.collected && cityNPCData.missionCompleteConversation != null)
					{
						conversation = cityNPCData.missionCompleteConversation;
					}
				}
				if (cityNPCData.alternateConversation != null && cityNPCData.alternateConversation.missionUID > 0)
				{
					Mission mission3 = GameManager.instance.communicatorDatabase.GetMission(cityNPCData.alternateConversation.missionUID);
					MissionData missionData5 = Player.instance.GetMissionData(cityNPCData.alternateConversation.missionUID);
					MissionCompletedData missionCompletedData3 = Player.instance.GetMissionCompletedData(cityNPCData.alternateConversation.missionUID);
					if (mission3.type == MissionType.Side && missionData5 == null && missionCompletedData3 == null)
					{
						if (cityNPCData.alternateConversation != null)
						{
							conversation = cityNPCData.alternateConversation;
						}
					}
					else if (mission3.type == MissionType.Side && missionData5 != null && !missionData5.completed)
					{
						if (cityNPCData.activeConversation != null)
						{
							conversation = cityNPCData.activeConversation;
						}
					}
					else if (mission3.type == MissionType.Side && missionCompletedData3 != null && cityNPCData.missionCompleteConversation != null)
					{
						conversation = cityNPCData.missionCompleteConversation;
					}
					if (cityNPCData.requiresMissionItem && missionData5 != null && !missionData5.completed)
					{
						if (SaveUtility.GetInt(cityNPCData.requiredMissionItemName) == 0)
						{
							if (cityNPCData.alternateConversation != null)
							{
								if (cityNPCData.alternateConversation != null)
								{
									conversation = cityNPCData.alternateConversation;
								}
								else if (cityNPCData.firstMessage.Count > 0)
								{
									conversation = cityNPCData.firstMessage[Random.Range(0, cityNPCData.firstMessage.Count)];
								}
							}
						}
						else if (cityNPCData.initialConversation != null)
						{
							conversation = cityNPCData.initialConversation;
						}
					}
				}
			}
			else
			{
				Mission mission4 = GameManager.instance.communicatorDatabase.GetMission(cityNPCData.initialConversation.missionUID);
				MissionData missionData6 = Player.instance.GetMissionData(cityNPCData.initialConversation.missionUID);
				MissionCompletedData missionCompletedData4 = Player.instance.GetMissionCompletedData(cityNPCData.initialConversation.missionUID);
				if (missionData6 == null && missionCompletedData4 == null)
				{
					conversation = cityNPCData.initialConversation;
					if (cityNPCData.requiresPreviousConvoPlayed && cityNPCData.requiredConversation != null && SaveUtility.GetInt(cityNPCData.requiredConversation.name) != 1)
					{
						if (cityNPCData.alternateConversation != null)
						{
							conversation = cityNPCData.alternateConversation;
						}
						else if (cityNPCData.firstMessage.Count > 0)
						{
							conversation = cityNPCData.firstMessage[Random.Range(0, cityNPCData.firstMessage.Count)];
						}
					}
					if (cityNPCData.requiresMissionItem && cityNPCData.alternateConversation != null && SaveUtility.GetInt(cityNPCData.requiredMissionItemName) == 0)
					{
						if (cityNPCData.alternateConversation != null)
						{
							conversation = cityNPCData.alternateConversation;
						}
						else if (cityNPCData.firstMessage.Count > 0)
						{
							conversation = cityNPCData.firstMessage[Random.Range(0, cityNPCData.firstMessage.Count)];
						}
					}
				}
				else if (missionData6 != null && missionData6.active && !missionData6.completed)
				{
					if (cityNPCData.activeConversation != null)
					{
						conversation = cityNPCData.activeConversation;
					}
				}
				else if (mission4.type == MissionType.Story && ((missionData6 != null && missionData6.active && missionData6.completed) || missionCompletedData4 != null))
				{
					if (cityNPCData.missionCompleteConversation != null)
					{
						conversation = cityNPCData.missionCompleteConversation;
					}
				}
				else if (mission4.type == MissionType.Side && missionData6 != null && missionData6.active && missionData6.completed && !missionData6.collected)
				{
					if (cityNPCData.missionCompleteConversation != null)
					{
						conversation = cityNPCData.missionCompleteConversation;
					}
				}
				else if (mission4.type == MissionType.Side && missionCompletedData4 != null)
				{
					if (cityNPCData.missionCompleteConversation != null)
					{
						conversation = cityNPCData.missionCompleteConversation;
					}
				}
				else
				{
					conversation = cityNPCData.initialConversation;
				}
			}
			if (cityNPCData.triggersEnteringCityScene && SaveUtility.GetInt("GAME_USED_NPC_CHAT_TRIGGER_" + cityNPCData.guid) == 0 && cityNPCData.triggersEnteringCityScene)
			{
				flag = true;
			}
		}
		if (shouldEnable)
		{
			EnableNPC();
			if (activeNPCData != null)
			{
				if (activeNPCData.disablesGameObject != null)
				{
					activeNPCData.disablesGameObject.SetActive(value: false);
				}
				if (activeNPCData.battleComplete.Count > 0)
				{
					conversationDefeated = activeNPCData.battleComplete[Random.Range(0, activeNPCData.battleComplete.Count)];
				}
				else
				{
					conversationDefeated = null;
				}
				if (activeNPCData.movesAfterConversation && activeNPCData.initialConversation != null)
				{
					CheckForMoveAfterConvo(activeNPCData.initialConversation.name);
				}
			}
			StartCoroutine("DelayCheckForMission");
		}
		else
		{
			DisableNPC();
			if (activeNPCData != null && activeNPCData.disablesGameObject != null)
			{
				activeNPCData.disablesGameObject.SetActive(value: true);
			}
		}
	}

	private IEnumerator DelayCheckForMission()
	{
		yield return new WaitForEndOfFrame();
		CheckForBattle();
		CheckForMission();
	}

	public bool NeededInActiveDailyMission()
	{
		return false;
	}

	private IEnumerator PoofInNpc()
	{
		isPoofing = true;
		if (npcPrefab != null)
		{
			npcPrefab.SetActive(value: false);
		}
		if (character != CharacterName.HoodedHood)
		{
			while (CityController.instance.communicator.MissionScrollerActive())
			{
				yield return new WaitForEndOfFrame();
			}
		}
		if (hasPoofedIn)
		{
			isPoofing = false;
			yield break;
		}
		isPoofing = true;
		hasPoofedIn = true;
		GameObject poofObj = Object.Instantiate(Resources.Load("Prefabs/FX/CityNPCSmokePoof") as GameObject);
		poofObj.transform.parent = gameObject.transform;
		poofObj.transform.localPosition = new Vector3(0.027f, 0.239f, -0.145f);
		poofObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		poofObj.transform.localEulerAngles = Vector3.zero;
		poofObj.SetActive(value: true);
		yield return new WaitForSeconds(0.1f);
		isPoofing = false;
		if (npcPrefab != null)
		{
			npcPrefab.SetActive(value: true);
		}
	}

	private IEnumerator PoofOutNpc()
	{
		isPoofing = true;
		GameObject poofObj = Object.Instantiate(Resources.Load("Prefabs/FX/CityNPCSmokePoof") as GameObject);
		poofObj.transform.parent = gameObject.transform;
		poofObj.transform.localPosition = new Vector3(0.027f, 0.239f, -0.145f);
		poofObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		poofObj.transform.localEulerAngles = Vector3.zero;
		poofObj.transform.parent = null;
		yield return new WaitForEndOfFrame();
		poofObj.SetActive(value: true);
		hasPoofedIn = false;
		yield return new WaitForSeconds(0.0325f);
		if (npcPrefab != null)
		{
			npcPrefab.SetActive(value: false);
		}
		yield return new WaitForEndOfFrame();
		isPoofing = false;
	}

	public void EnableNPC()
	{
		if (npcPrefab == null && !string.IsNullOrEmpty(npcPrefabPath))
		{
			GameObject gameObject = Resources.Load(npcPrefabPath) as GameObject;
			if (gameObject != null)
			{
				npcPrefab = Object.Instantiate(gameObject);
			}
			if (npcPrefab != null)
			{
				npcPrefab.transform.parent = base.transform;
				npcPrefab.transform.localPosition = Vector3.zero;
				npcPrefab.transform.localEulerAngles = Vector3.zero;
				animationController = npcPrefab.GetComponentInChildren<AEControllerTk2d>(includeInactive: true);
				landmarkIcon = npcPrefab.GetComponent<Landmark>();
				foreach (Transform item in npcPrefab.transform)
				{
					if (item.gameObject.name.Equals("icon"))
					{
						missionIndicator = item.gameObject;
					}
					if (item.gameObject.name.Equals("PlaceholderSprite"))
					{
						placeholderSprite = item.gameObject.GetComponent<tk2dSprite>();
					}
				}
			}
		}
		if (npcPrefab != null)
		{
			if (flipCharacter)
			{
				if (animationController != null)
				{
					animationController.isFlipped = flipCharacter;
				}
				if (placeholderSprite != null)
				{
					placeholderSprite.transform.localEulerAngles = new Vector3(0f, (!npcDef.prefabFacingLeft) ? 180f : 0f, 0f);
				}
			}
			if (animationController != null)
			{
				DayNightCard.instance.AddAnimationControllerToTint(animationController);
			}
		}
		uiItem.enabled = true;
		foreach (Transform item2 in transform)
		{
			if (item2.gameObject != missionIndicator)
			{
				item2.gameObject.SetActive(value: true);
			}
		}
		if (!defeatedNPC)
		{
			bool flag = false;
			if (activeNPCData != null && activeNPCData.figures.Count > 0)
			{
				flag = true;
			}
			if (flag)
			{
				if (conversation != null && conversation.triggersImmediately)
				{
					ShowPedestrianLandmarkIcon();
				}
				else
				{
					ShowVSLandmarkIcon();
				}
			}
			else
			{
				ShowPedestrianLandmarkIcon();
			}
		}
		else
		{
			ShowPedestrianLandmarkIcon();
		}
		if (npcPrefab != null)
		{
			if (activeNPCData != null && activeNPCData.poofsIn)
			{
				npcPrefab.SetActive(value: false);
				StartCoroutine("PoofInNpc");
			}
			else if (activeNPCData != null)
			{
				StopCoroutine("DelayTurningOffNPCPrefab");
				StartCoroutine("DelayTurningOffNPCPrefab");
			}
		}
		if (!CityController.instance.activeNpcs.Contains(this))
		{
			CityController.instance.activeNpcs.Add(this);
		}
	}

	private IEnumerator DelayTurningOffNPCPrefab()
	{
		npcPrefab.SetActive(value: true);
		ignoreHideNPCPrefab = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		ignoreHideNPCPrefab = false;
	}

	private void ShowVSLandmarkIcon()
	{
		if (landmarkIcon != null)
		{
			landmarkIcon.spriteName = "gui_citynotifiers_npc";
			if (landmarkIcon.icon != null)
			{
				landmarkIcon.icon.sprite.gameObject.SetActive(value: true);
				landmarkIcon.icon.tailSprite.gameObject.SetActive(value: true);
				landmarkIcon.UpdateSpriteIcon(landmarkIcon.spriteName);
			}
		}
	}

	private void ShowPedestrianLandmarkIcon()
	{
		if (landmarkIcon != null)
		{
			if (character == CharacterName.StickyJoe)
			{
				landmarkIcon.spriteName = "gui_citynotifiers_shop";
			}
			else
			{
				landmarkIcon.spriteName = "gui_citynotifiers_pedestrian";
			}
			if (landmarkIcon.icon != null)
			{
				landmarkIcon.icon.sprite.gameObject.SetActive(value: true);
				landmarkIcon.icon.tailSprite.gameObject.SetActive(value: true);
				landmarkIcon.UpdateSpriteIcon(landmarkIcon.spriteName);
			}
		}
	}

	public void RemoveAnimationControllerFromDayNight()
	{
		if (animationController != null)
		{
			DayNightCard.instance.RemoveAnimationControllerToTint(animationController);
		}
	}

	public void DisableNPC()
	{
		if (npcPrefab != null)
		{
			if (activeNPCData != null && activeNPCData.disablesGameObject != null)
			{
				activeNPCData.disablesGameObject.SetActive(value: true);
			}
			if (activeNPCData != null && activeNPCData.poofsOut)
			{
				StartCoroutine("PoofOutNpc");
			}
			else
			{
				npcPrefab.SetActive(value: false);
			}
			RemoveAnimationControllerFromDayNight();
		}
		uiItem.enabled = false;
		foreach (Transform item in base.transform)
		{
			if (item.gameObject != npcPrefab)
			{
				item.gameObject.SetActive(value: false);
			}
		}
		if (landmarkIcon != null && landmarkIcon.icon != null)
		{
			landmarkIcon.icon.sprite.gameObject.SetActive(value: false);
			landmarkIcon.icon.tailSprite.gameObject.SetActive(value: false);
		}
		if (CityController.instance != null && CityController.instance.activeNpcs.Contains(this))
		{
			CityController.instance.activeNpcs.Remove(this);
		}
	}

	public void CheckForMoveAfterConvo(string convoName, bool initialCheck = true)
	{
		if (activeNPCData == null || string.IsNullOrEmpty(convoName) || !activeNPCData.movesAfterConversation || !(activeNPCData.moveToPosition != null) || (initialCheck && (!initialCheck || (SaveUtility.GetInt(convoName + "_queued") != 1 && SaveUtility.GetInt(convoName) != 1))))
		{
			return;
		}
		if (activeNPCData.disablesGameObject != null)
		{
			activeNPCData.disablesGameObject.SetActive(value: true);
		}
		if ((!initialCheck && SaveUtility.GetInt(convoName) == 0) || (initialCheck && SaveUtility.GetInt(convoName + "_queued") == 1))
		{
			SaveUtility.SetInt(convoName + "_queued", 1, save: false);
			SaveUtility.SetInt(convoName, 1, save: false);
			if (moveAfterConvoRoutine != null)
			{
				StopCoroutine(moveAfterConvoRoutine);
			}
			moveAfterConvoRoutine = StartCoroutine(StartMoveAfterConvo(convoName));
		}
		else
		{
			gameObject.transform.position = activeNPCData.moveToPosition.transform.position;
		}
		if (activeNPCData.alternateConversation != null)
		{
			conversation = activeNPCData.alternateConversation;
		}
	}

	private IEnumerator StartMoveAfterConvo(string convoName)
	{
		yield return new WaitForSeconds(1f);
		yield return new WaitForEndOfFrame();
		Communicator communicator = CityController.instance.communicator;
		while (communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.3f);
		yield return StartCoroutine("PoofOutNpc");
		isPoofing = true;
		gameObject.transform.position = activeNPCData.moveToPosition.transform.position;
		yield return new WaitForSeconds(0.6f);
		yield return StartCoroutine("PoofInNpc");
		SaveUtility.SetInt(convoName + "_queued", 0, save: false);
		if (DialogueTree.instance != null)
		{
			Object.Destroy(DialogueTree.instance.gameObject);
		}
	}

	private void CheckForBattle()
	{
		if (activeNPCData != null)
		{
			if (conversation != null && conversation.triggersImmediately)
			{
				ShowPedestrianLandmarkIcon();
			}
			else if (activeNPCData.figures.Count == 0 || defeatedNPC)
			{
				ShowPedestrianLandmarkIcon();
			}
			else
			{
				ShowVSLandmarkIcon();
			}
		}
	}

	private Conversation CheckForActiveMissionConvo()
	{
		if (npcDef != null)
		{
			for (int i = 0; i < npcDef.spawnSetups.Count; i++)
			{
				NPCSpawnPointSetup nPCSpawnPointSetup = npcDef.spawnSetups[i];
				if (nPCSpawnPointSetup.npcData.activeOnActiveMissionUID > -1)
				{
					MissionData missionData = Player.instance.GetMissionData(nPCSpawnPointSetup.npcData.activeOnActiveMissionUID);
					if (missionData != null && missionData.active && !missionData.completed && nPCSpawnPointSetup.npcData.initialConversation != null)
					{
						return nPCSpawnPointSetup.npcData.initialConversation;
					}
				}
			}
			if (character == CharacterName.Raven)
			{
				MissionData missionData2 = Player.instance.GetMissionData(61);
				if (missionData2 != null && missionData2.active && !missionData2.completed)
				{
					return (Conversation)Resources.Load("Data/Conversations/CONVO_RAVEN_STARFIRE");
				}
			}
			if (character == CharacterName.Cyborg)
			{
				MissionData missionData3 = Player.instance.GetMissionData(63);
				if (missionData3 != null && missionData3.active && !missionData3.completed)
				{
					return (Conversation)Resources.Load("Data/Conversations/CONVO_CYBORG_STARFIRE");
				}
			}
			if (character == CharacterName.BeastBoy)
			{
				MissionData missionData4 = Player.instance.GetMissionData(65);
				if (missionData4 != null && missionData4.active && !missionData4.completed)
				{
					return (Conversation)Resources.Load("Data/Conversations/CONVO_BEASTBOY_STARFIRE");
				}
			}
		}
		return null;
	}

	private void CheckDeliveryMission()
	{
	}

	private void CheckForMission()
	{
		if (CoreGameTutorial.instance != null && !CoreGameTutorial.instance.CheckShouldShowMissionIndicators())
		{
			HideMissionIndicators();
			return;
		}
		bool flag = false;
		if (activeNPCData != null && conversation != null)
		{
			int missionUID = activeNPCData.initialConversation.missionUID;
			if (missionUID == -1)
			{
				missionUID = conversation.missionUID;
			}
			if (missionUID == -1 && activeNPCData.preBattleConversations != null && activeNPCData.preBattleConversations.Count > 0)
			{
				for (int i = 0; i < activeNPCData.preBattleConversations.Count; i++)
				{
					missionUID = activeNPCData.preBattleConversations[i].missionUID;
					if (missionUID > -1)
					{
						break;
					}
				}
			}
			if (missionUID > -1)
			{
				bool flag2 = GameManager.instance.communicatorDatabase.GetMission(missionUID).type == MissionType.Side;
				MissionData missionData = Player.instance.GetMissionData(missionUID);
				MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(missionUID);
				if (missionData == null && flag2 && missionCompletedData == null)
				{
					flag = true;
					if (landmarkIcon != null && landmarkIcon.icon != null)
					{
						landmarkIcon.icon.sprite.gameObject.SetActive(value: true);
						landmarkIcon.icon.tailSprite.gameObject.SetActive(value: true);
						landmarkIcon.UpdateSpriteIcon("gui_citynotifiers_missionbubble");
					}
					if (missionIndicator != null)
					{
						missionIndicator.SetActive(value: true);
					}
				}
			}
		}
		if (!flag)
		{
			HideMissionIndicators();
		}
	}

	private void HideMissionIndicators()
	{
		if (missionIndicator != null)
		{
			missionIndicator.SetActive(value: false);
		}
		if (landmarkIcon != null)
		{
			landmarkIcon.ResetSpriteIcon();
		}
	}

	private void Awake()
	{
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
	}

	private void Reset()
	{
		uiItem = GetComponent<tk2dUIItem>();
	}

	public Landmark GetLandmark()
	{
		return landmarkIcon;
	}
}
