using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityController : MonoBehaviour
{
	public static bool usePrevPosition;

	public static Vector3 prevPosition;

	public static int prevCameraAngleIndex;

	public static bool fromInitScene;

	public static bool fromChallengeScene = false;

	public static string startFromNodeNamed = string.Empty;

	public static bool startFacingRight;

	public CityName cityName;

	public bool isInterior;

	public PathNode cityStartPathNode;

	public bool cityStartFacingRight;

	[Space]
	public Transform cameraHolder;

	public List<Vector3> cameraAngles;

	public int initialAngleIndex;

	public AudioSource cityTheme;

	public AudioSource cityTheme2;

	public CityCharacter cityCharacter;

	public SwipeManager swipeManager;

	public List<tk2dUIItem> shopUIItems;

	public PauseScreen pauseScreen;

	public PauseButton pauseButton;

	public CameraPinchZoomer pinchZoomer;

	public MySquad mySquad;

	public Camera guiCamera2;

	public Transform dialogueSystemParent;

	public Communicator communicator;

	public ViewDeobstructor viewDeobstructer;

	public TextMeshPro newFiguresAmount;

	public WalletGUI walletGUI;

	public CurrencyGUI rainbowWalletGUI;

	public WalletGUI dialogueWalletGUI;

	public GameObject magnifyingGlassObj;

	public GameObject titanTowerBtnObj;

	public GameObject titanTowerTeleporterObj;

	public GameObject titanTowerTeleporterReturnObj;

	public CityDialogBox hotelDialogBox;

	public PathNode titanTowerPathNode;

	public GameObject wayPoint;

	public LandmarkIcon wayPointIcon;

	public HoodedHoodEncounters hoodedHoodEncounters;

	public GameObject looseChangeWarning;

	public GameObject titanTowerObj;

	public CloudsHelper cloudsHelper;

	public SpawnGroupMaster spawnGroupMaster;

	public CityLocationBounds[] allCityLocationBounds;

	public List<CityShop> cityShops = new List<CityShop>();

	public List<CityNPC> activeNpcs = new List<CityNPC>();

	public List<GameObject> tournamentBuildings = new List<GameObject>();

	public List<LandmarkIcon> landmarkIcons = new List<LandmarkIcon>();

	public List<Conversation> conversationsToCheck = new List<Conversation>();

	public List<QuestItem> hotDogs = new List<QuestItem>();

	[HideInInspector]
	public int curCameraAngleIndex;

	private List<CityEntityNPC> _activeNPCEntities = new List<CityEntityNPC>();

	public Transform jailOutsideStartPosition;

	public Transform jailInsideStartPosition;

	public Transform cityBeachDockPosition;

	private float targetCityThemeVolume = 1f;

	[HideInInspector]
	public bool exitingCityScene;

	private bool cameFromOtherCity;

	private bool landmarkPressed;

	private float lastDistrictCheck;

	private AudioClip previousPlayed;

	private bool isTraveling;

	[HideInInspector]
	public GameObject hoodedHoodCity;

	private AudioClip hoodedHoodAudioClip;

	[HideInInspector]
	public bool preventShowingGUIAfterDialogue;

	[HideInInspector]
	public bool preventShowingGUICameraAfterDialogue;

	private bool startingBattle;

	private string afterBattleConvoName = string.Empty;

	private bool touchStartedOnButton;

	private bool preventManualWayPoint;

	private bool allowWayPointMovement = true;

	private Coroutine tapRoutine;

	public bool showDebugButtonsAtStart;

	public List<GameObject> debugButtons;

	private Coroutine noLooseChangeCoroutine;

	private Dictionary<AudioSource, AudioClip> cityVOSources;

	private ZoomType _zoomType;

	private CityLocation _currentLocation;

	private List<PathNode> _allNodes;

	private static CityController _instance;

	private static string _previousCityScene = string.Empty;

	public bool initialized { get; set; }

	public bool preventShowingCommunicatorAfterDialogue { get; set; }

	public ZoomType zoomType
	{
		get
		{
			return _zoomType;
		}
		set
		{
			_zoomType = value;
		}
	}

	public CityLocation currentLocation => _currentLocation;

	public CityLocationBounds CurrentCityLocationBounds
	{
		get
		{
			for (int i = 0; i < allCityLocationBounds.Length; i++)
			{
				CityLocationBounds cityLocationBounds = allCityLocationBounds[i];
				if (cityLocationBounds.cityLocaton == _currentLocation)
				{
					return cityLocationBounds;
				}
			}
			return allCityLocationBounds[0];
		}
	}

	public List<PathNode> allNodes
	{
		get
		{
			if (_allNodes == null)
			{
				_allNodes = new List<PathNode>(UnityEngine.Object.FindObjectsOfType<PathNode>());
			}
			return _allNodes;
		}
	}

	public static CityController instance => _instance;

	public static string previousCityScene
	{
		get
		{
			if (string.IsNullOrEmpty(_previousCityScene))
			{
				_previousCityScene = GameManager.instance.gameSettings.citySceneName;
			}
			return _previousCityScene;
		}
		set
		{
			_previousCityScene = value;
		}
	}

	public event Action<CityLocation> OnCityLocationChanged;

	public event Action<LandmarkIcon, bool, bool> OnLandmarkWayPointClicked;

	public event Action OnExitCityScene;

	public event Action FinishedDialogue;

	private void Awake()
	{
		_instance = this;
		bool flag = previousCityScene == "City_JumpCityNew" || previousCityScene == "City_Gotham" || previousCityScene == "City_Metropolis";
		previousCityScene = SceneManagerHelper.ActiveSceneName;
		Application.targetFrameRate = 60;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Default"), ignore: false);
		PauseManager.instance.isEnabled = true;
		PauseManager.instance.Resume();
		cityTheme.Stop();
		cityTheme2.Stop();
		cityTheme2.gameObject.SetActive(value: false);
		cameFromOtherCity = false;
		if (!string.IsNullOrEmpty(startFromNodeNamed))
		{
			GameObject gameObject = GameObject.Find(startFromNodeNamed);
			if (gameObject != null)
			{
				cityCharacter.transform.position = gameObject.transform.position;
				cameraHolder.position = cityCharacter.transform.position;
				cameFromOtherCity = flag;
			}
			else
			{
				cityCharacter.transform.position = cityStartPathNode.transform.position;
				cityCharacter.UpdateCurrentNode(cityStartPathNode);
				cameraHolder.position = cityCharacter.transform.position;
				startFacingRight = cityStartFacingRight;
				cameFromOtherCity = flag;
			}
			startFromNodeNamed = string.Empty;
		}
		else if (!string.IsNullOrEmpty(SaveUtility.GetString("PlayerPosition")))
		{
			cityCharacter.transform.position = Vector3FromString(SaveUtility.GetString("PlayerPosition"));
			cameraHolder.position = cityCharacter.transform.position;
		}
		else
		{
			cityCharacter.transform.position = cityStartPathNode.transform.position;
			cityCharacter.UpdateCurrentNode(cityStartPathNode);
			startFacingRight = cityStartFacingRight;
		}
		curCameraAngleIndex = Mathf.Clamp(initialAngleIndex, 0, cameraAngles.Count - 1);
		UpdateCameraAngle(instant: true);
		if (mySquad != null)
		{
			mySquad.Init(Player.instance, isCity: true);
			mySquad.HideNow();
		}
		if (rainbowWalletGUI != null)
		{
			rainbowWalletGUI.Init(Player.instance);
		}
		if (pauseScreen != null)
		{
			pauseScreen.ShowPauseDebugButtons(showDebugButtonsAtStart);
		}
		TournamentController.initDefinition = null;
	}

	private void UnlockTT2Chars()
	{
		if (GameManager.instance == null || Player.instance == null)
		{
			return;
		}
		Player.instance.AddCurrency(3000);
		List<CharacterDefinition> list = null;
		if (GameManager.instance.characterDatabase != null)
		{
			list = GameManager.instance.characterDatabase.characters;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					CharacterDefinition characterDefinition = list[i];
					if (characterDefinition.isActive && (characterDefinition.uid == 0 || characterDefinition.uid == 85 || characterDefinition.uid == 78 || characterDefinition.uid == 8 || characterDefinition.uid == 90 || characterDefinition.uid == 26))
					{
						Player.instance.AddCharacter(CharacterRoller.RollCharacterData(characterDefinition, -1, characterDefinition.uid, 5));
						Player.instance.AcquireCharacter(characterDefinition.uid);
						Character characterAdded = Player.instance.characters.GetLastItem();
						if (Player.instance.activeParty.Find((Character x) => x.characterDefinition.uid == characterAdded.characterDefinition.uid) == null)
						{
							Player.instance.AddToParty(characterAdded);
						}
					}
				}
			}
			else
			{
				Debug.LogError("charactersReference is NULL! Which means characterDatabase.characters is NULL! Report this error!");
			}
		}
		else
		{
			Debug.LogError("Game Manager Character Database is NULL! Report this error!");
		}
	}

	private void UnlockAllItems()
	{
		AcquireAllCharacters();
		UnlockAllModChips();
		Player.instance.Save();
	}

	public static Vector3 Vector3FromString(string s)
	{
		s = s.Substring(1, s.Length - 2);
		string[] array = s.Split(new string[1] { "," }, StringSplitOptions.None);
		return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
	}

	private void OnEnable()
	{
		PauseManager.instance.Paused += HandlePaused;
		pinchZoomer.ZoomedIn += HandleZoomedIn;
		pinchZoomer.SingleTap += HandleSingleTap;
		EventMessenger.Instance.AddListener<StartConversationEvent>(HandleStartConversationEvent);
		EventMessenger.Instance.AddListener<NPCEntityInteractEvent>(HandleNPCEntityInteractEvent);
		EventMessenger.Instance.AddListener<BuildingEntityInteractEvent>(HandleBuildingEntityInteractEvent);
		EventMessenger.Instance.AddListener<EventEntityInteractEvent>(HandleEventEntityInteractEvent);
		EventMessenger.Instance.AddListener<EnterShopEvent>(HandleEnterShopEvent);
		EventMessenger.Instance.AddListener<ExitCityEvent>(HandleExitCityEvent);
		EventMessenger.Instance.AddListener<StartTournamentEvent>(HandleStartTournamentEvent);
		EventMessenger.Instance.AddListener<TeleportEvent>(HandleTeleportEvent);
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
	}

	private void OnDisable()
	{
		PauseManager.instance.Paused -= HandlePaused;
		pinchZoomer.ZoomedIn -= HandleZoomedIn;
		pinchZoomer.SingleTap -= HandleSingleTap;
		_zoomType = ZoomType.Normal;
		EventMessenger.Instance.RemoveListener<StartConversationEvent>(HandleStartConversationEvent);
		EventMessenger.Instance.RemoveListener<NPCEntityInteractEvent>(HandleNPCEntityInteractEvent);
		EventMessenger.Instance.RemoveListener<BuildingEntityInteractEvent>(HandleBuildingEntityInteractEvent);
		EventMessenger.Instance.RemoveListener<EventEntityInteractEvent>(HandleEventEntityInteractEvent);
		EventMessenger.Instance.RemoveListener<EnterShopEvent>(HandleEnterShopEvent);
		EventMessenger.Instance.RemoveListener<ExitCityEvent>(HandleExitCityEvent);
		EventMessenger.Instance.RemoveListener<StartTournamentEvent>(HandleStartTournamentEvent);
		EventMessenger.Instance.RemoveListener<TeleportEvent>(HandleTeleportEvent);
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
	}

	private void OnDestroy()
	{
		if (CityButtonEventHelper.instance != null)
		{
			CityButtonEventHelper.instance.tk2dUIItemOnDown -= Handletk2dUIItemOnDown;
			CityButtonEventHelper.instance.tk2dUIItemOnRelease -= Handletk2dUIItemOnRelease;
		}
		if (communicator != null)
		{
			communicator.StartDayTime -= HandleTimeChange;
			communicator.StartNightTime -= HandleTimeChange;
		}
		OnCityLocationChanged = null;
		OnLandmarkWayPointClicked = null;
		OnExitCityScene = null;
		if (CityModel.instance != null)
		{
			CityModel.instance.gameObject.SetActive(value: false);
		}
		_instance = null;
	}

	private void HandleStartConversationEvent(StartConversationEvent evt)
	{
		StartCoroutine(PlayConvo(evt.conversation));
	}

	private void HandleNPCEntityInteractEvent(NPCEntityInteractEvent evt)
	{
		if (!CanPlayerInteract())
		{
			return;
		}
		PathNode pathNode = evt.npcEntity.nearestNode;
		if (pathNode == null)
		{
			pathNode = GetNearestPathNodeToPosition(evt.npcEntity.transform.position);
		}
		float num = Vector3.Distance(evt.npcEntity.transform.position, cityCharacter.transform.position);
		float num2 = Vector3.Distance(cityCharacter.transform.position, pathNode.transform.position);
		if (num <= 0.5f || num2 < 0.1f)
		{
			cityCharacter.StopMove();
			evt.npcEntity.FaceTarget(cityCharacter.transform);
			cityCharacter.FaceTarget(evt.npcEntity.transform);
			MissionData missionData = null;
			List<MissionData> missionDatasByType = Player.instance.GetMissionDatasByType(MissionSideType.Buy);
			for (int i = 0; i < missionDatasByType.Count; i++)
			{
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionDatasByType[i].uid);
				if (mission != null && mission.deliveryNPC == evt.npcEntity.npcGuid && mission.HasMetBuyConfigs(missionDatasByType[i]))
				{
					missionData = missionDatasByType[i];
					break;
				}
			}
			if (missionData != null)
			{
				StartCoroutine(DeliverGoodsForMission(missionData, evt.npcEntity));
			}
			else
			{
				StartCoroutine(ShowNPCDialogueSystem(evt.npcEntity));
			}
		}
		else
		{
			pinchZoomer.ZoomIn();
			cityCharacter.MoveToInteractiveNode(pathNode, () =>
			{
				HandleNPCEntityInteractEvent(evt);
			});
		}
	}

	private void HandleBuildingEntityInteractEvent(BuildingEntityInteractEvent evt)
	{
		if (!CanPlayerInteract())
		{
			return;
		}
		PathNode pathNode = evt.buildingEntity.entranceNode;
		if (pathNode == null)
		{
			pathNode = GetNearestPathNodeToPosition(evt.buildingEntity.transform.position);
		}
		float num = Vector3.Distance(evt.buildingEntity.transform.position, cityCharacter.transform.position);
		float num2 = Vector3.Distance(cityCharacter.transform.position, pathNode.transform.position);
		if (num <= 0.5f || num2 < 0.1f)
		{
			cityCharacter.StopMove();
			evt.buildingEntity.Interact();
			return;
		}
		pinchZoomer.ZoomIn();
		cityCharacter.MoveToInteractiveNode(pathNode, () =>
		{
			HandleBuildingEntityInteractEvent(evt);
		});
	}

	private void HandleEventEntityInteractEvent(EventEntityInteractEvent evt)
	{
		if (!CanPlayerInteract())
		{
			return;
		}
		PathNode interactNode = evt.eventEntity.interactNode;
		if (interactNode == null)
		{
			evt.eventEntity.Interact();
			return;
		}
		float num = Vector3.Distance(evt.eventEntity.transform.position, cityCharacter.transform.position);
		float num2 = Vector3.Distance(cityCharacter.transform.position, interactNode.transform.position);
		if (num <= 0.5f || num2 < 0.1f)
		{
			cityCharacter.StopMove();
			evt.eventEntity.Interact();
			return;
		}
		pinchZoomer.ZoomIn();
		cityCharacter.MoveToInteractiveNode(interactNode, () =>
		{
			HandleEventEntityInteractEvent(evt);
		});
	}

	private void HandleEnterShopEvent(EnterShopEvent evt)
	{
		if (exitingCityScene)
		{
			return;
		}
		prevPosition = cityCharacter.transform.position;
		prevCameraAngleIndex = curCameraAngleIndex;
		usePrevPosition = true;
		communicator.SaveDayAndTime();
		SaveUtility.SetString("PlayerPosition", prevPosition.ToString(), save: false);
		SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
		ShopController.inventory = ((!(evt.shopInventory != null)) ? evt.shopDef.defaultInventory : evt.shopInventory);
		int num = -1;
		if (evt.shopDef.additionalInventories.Count > 0)
		{
			for (int i = 0; i < evt.shopDef.additionalInventories.Count; i++)
			{
				ShopInventorySetup shopInventorySetup = evt.shopDef.additionalInventories[i];
				if (shopInventorySetup.newsFlashInitiated && shopInventorySetup.newsFlashUID > -1 && Player.instance.GetNewsFlashData(shopInventorySetup.newsFlashUID) != null && shopInventorySetup.inventoryFile != null && shopInventorySetup.priority > num)
				{
					num = shopInventorySetup.priority;
					ShopController.inventory = shopInventorySetup.inventoryFile;
				}
			}
		}
		bool flag = communicator.activeNewsFlash != null && communicator.activeNewsFlash.targetShopUID == evt.shopDef.uid;
		ShopController.shopDef = evt.shopDef;
		ShopController.isFlashSaleShop = false;
		ShopController.newsFlashUID = ((!flag) ? (-1) : communicator.activeNewsFlash.uid);
		if (!flag)
		{
			communicator.ResolveActiveNewsFlash();
		}
		ExitCityScene();
		LoadingScreenCover.LoadScene("ShopTest");
	}

	private void HandleExitCityEvent(ExitCityEvent evt)
	{
		if (!exitingCityScene)
		{
			cityCharacter.StopMove();
			SaveUtility.SetString("PlayerPosition", cityCharacter.transform.position.ToString(), save: false);
			SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
			communicator.SaveDayAndTime();
			communicator.ResolveActiveNewsFlash();
			Player.instance.IncreaseCurrentTime(20);
			ExitCityScene();
			if (!string.IsNullOrEmpty(evt.startAtNodeNamed))
			{
				startFromNodeNamed = evt.startAtNodeNamed;
			}
			startFacingRight = evt.startFacingRight;
			LoadingScreenCover.LoadScene(evt.destSceneName);
		}
	}

	private void HandleStartTournamentEvent(StartTournamentEvent evt)
	{
		if (!exitingCityScene && !(evt.tournament == null))
		{
			SaveUtility.SetString("PlayerPosition", cityCharacter.transform.position.ToString(), save: false);
			SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
			communicator.SaveDayAndTime();
			communicator.ResolveActiveNewsFlash();
			Player.instance.IncreaseCurrentTime(20);
			ExitCityScene();
			TournamentController.initDefinition = evt.tournament;
			LoadingScreenCover.LoadScene("Tournament");
		}
	}

	private void HandleTeleportEvent(TeleportEvent evt)
	{
		if (!exitingCityScene && !(evt.pathNode == null))
		{
			StartCoroutine(TeleportSequence(evt.pathNode, evt.stopMovement));
		}
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}

	private bool CanPlayerInteract()
	{
		if (CoreGameTutorial.instance != null && CoreGameTutorial.instance.isActive)
		{
			return false;
		}
		if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			return false;
		}
		if (CityReward.instance != null && CityReward.instance.showingRewards)
		{
			return false;
		}
		return cityCharacter.AllowInput;
	}

	public bool IsPlayerAvailable()
	{
		if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			return false;
		}
		if (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (CityReward.instance != null && CityReward.instance.showingRewards)
		{
			return false;
		}
		if (PauseManager.instance.isPaused)
		{
			return false;
		}
		if (CoreGameTutorial.instance != null && CoreGameTutorial.instance.isActive)
		{
			return false;
		}
		return true;
	}

	private IEnumerator Start()
	{
		if (Time.frameCount == 1 && Player.instance.pvpData.hasConnected)
		{
			TT2NetChat.Instance.Connect(Player.instance.pvpData.playerID);
		}
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		yield return new WaitUntil(() => cityCharacter.animator != null);
		if (SceneManager.GetActiveScene().name == "City_JumpCityNew")
		{
			if (!cityCharacter.CanReachNode(cityStartPathNode, allowInactiveNodes: false) && cityCharacter.CanReachNode(cityStartPathNode, allowInactiveNodes: true))
			{
				Debug.Log("Character is stuck, resetting position...");
				cityCharacter.transform.position = cityStartPathNode.transform.position;
				cityCharacter.UpdateCurrentNode(cityStartPathNode);
				startFacingRight = cityStartFacingRight;
			}
			else
			{
				Debug.Log("Character is NOT stuck");
			}
		}
		cityCharacter.animator.isFlipped = startFacingRight;
		startFacingRight = false;
		Input.multiTouchEnabled = true;
		if (CityModel.instance != null)
		{
			CityModel.instance.gameObject.SetActive(value: true);
		}
		UpdateCityLocation();
		if (cameFromOtherCity)
		{
			cityCharacter.PlayCityLocationVO(_currentLocation);
			cameFromOtherCity = false;
		}
		if (isInterior)
		{
			if (GameUI.instance.magnifyingGlassButton != null)
			{
				GameUI.instance.magnifyingGlassButton.isEnabled = false;
			}
			if (pinchZoomer != null)
			{
				pinchZoomer.allowInteraction = false;
			}
		}
		yield return new WaitForEndOfFrame();
		if (communicator != null)
		{
			communicator.StartDayTime += HandleTimeChange;
			communicator.StartNightTime += HandleTimeChange;
		}
		CityButtonEventHelper.instance.tk2dUIItemOnDown += Handletk2dUIItemOnDown;
		CityButtonEventHelper.instance.tk2dUIItemOnRelease += Handletk2dUIItemOnRelease;
		if (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive)
		{
			PlayCurrentLocationMusic();
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		wayPointIcon = wayPoint.GetComponent<Landmark>().icon;
		if (wayPointIcon != null)
		{
			wayPointIcon.IsManualWayPoint = true;
			wayPointIcon.gameObject.SetActive(value: false);
		}
		if (fromChallengeScene)
		{
			fromChallengeScene = false;
			CommunicatorManager.instance.UpdateMissionWithTargetMessage("VISITED_JLRO");
		}
		yield return new WaitWhile(() => TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive);
		if (SaveUtility.GetInt("PLAYED_TT2_INTRO") == 0)
		{
			Debug.Log("\t Start Coroutine for TT2 INTRO");
			StartCoroutine(PlayTT2Dialogue("CONVO_TUTORIAL_GAMESTART"));
			SaveUtility.SetInt("PLAYED_TT2_INTRO", 1);
		}
		Player.instance.CheckIfAllSideMissionsComplete();
		Player.instance.CheckRadicalFigureAchievement();
		initialized = true;
	}

	public void HandleUnloadSpriteCollection(tk2dSpriteCollectionData spriteCollectionData)
	{
		StartCoroutine(UnloadSpriteCollectionRoutine(spriteCollectionData));
	}

	private IEnumerator UnloadSpriteCollectionRoutine(tk2dSpriteCollectionData spriteCollectionData)
	{
		if (spriteCollectionData != null)
		{
			spriteCollectionData.UnloadTextures();
			spriteCollectionData.ResetPlatformData();
			yield return new WaitForEndOfFrame();
			yield return Resources.UnloadUnusedAssets();
		}
	}

	private void HandleTimeChange()
	{
		if (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive)
		{
			PlayCurrentLocationMusic(crossFade: true);
		}
	}

	public void LandmarkWayPointDown(LandmarkIcon icon)
	{
		if (icon == null || (icon != null && !icon.IsManualWayPoint))
		{
			landmarkPressed = true;
		}
	}

	public void LandmarkWayPointReleased(LandmarkIcon icon)
	{
		landmarkPressed = false;
	}

	public void LandmarkWayPointClicked(LandmarkIcon icon, bool unselectSelected = true, bool manualWayPointClick = false)
	{
		if (OnLandmarkWayPointClicked != null && icon != null)
		{
			OnLandmarkWayPointClicked(icon, unselectSelected, manualWayPointClick);
		}
		if (icon != null && icon.landmark != null)
		{
			cameraHolder.transform.DOMove(icon.landmark.position, 0.6f);
		}
	}

	private void Update()
	{
		if (initialized && Time.time - lastDistrictCheck > 0.03f)
		{
			lastDistrictCheck = Time.time;
			UpdateCityLocation();
			if (dialogueWalletGUI != null && (DialogueTree.instance == null || (DialogueTree.instance != null && !DialogueTree.instance.gameObject.activeInHierarchy)))
			{
				dialogueWalletGUI.gameObject.SetActive(value: false);
			}
		}
	}

	public void UpdateCityLocation()
	{
		if (allCityLocationBounds == null)
		{
			return;
		}
		for (int i = 0; i < allCityLocationBounds.Length; i++)
		{
			CityLocationBounds cityLocationBounds = allCityLocationBounds[i];
			if (cityLocationBounds.cityLocaton != CityLocation.None && cityLocationBounds.cityLocaton != _currentLocation && cityLocationBounds.bounds.Contains(cityCharacter.transform.position))
			{
				_currentLocation = cityLocationBounds.cityLocaton;
				if (OnCityLocationChanged != null)
				{
					OnCityLocationChanged(_currentLocation);
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (allCityLocationBounds != null)
		{
			for (int i = 0; i < allCityLocationBounds.Length; i++)
			{
				CityLocationBounds cityLocationBounds = allCityLocationBounds[i];
				Gizmos.color = cityLocationBounds.boundsColor;
				Gizmos.DrawWireCube(cityLocationBounds.bounds.center, cityLocationBounds.bounds.size);
			}
		}
	}

	public void AddActiveNPCEntity(CityEntityNPC npcEntity)
	{
		if (!_activeNPCEntities.Contains(npcEntity))
		{
			_activeNPCEntities.Add(npcEntity);
		}
	}

	public void RemoveActiveNPCEntity(CityEntityNPC npcEntity)
	{
		int num = _activeNPCEntities.IndexOf(npcEntity);
		if (num > -1)
		{
			_activeNPCEntities.RemoveAt(num);
		}
	}

	public CityEntityNPC GetActiveNPCEntity(string guid)
	{
		for (int i = 0; i < _activeNPCEntities.Count; i++)
		{
			if (_activeNPCEntities[i].npcGuid == guid)
			{
				return _activeNPCEntities[i];
			}
		}
		return null;
	}

	public void PlayCurrentLocationMusic(bool crossFade = false)
	{
		PlayMusic(GameManager.instance.cityLocationMusicDatabase.GetCityLocationMusic(currentLocation, communicator.isDayTime), crossFade);
	}

	public void PlayMusic(AudioClip clip, bool crossFade = false)
	{
		if (previousPlayed != clip && clip != null)
		{
			if (!crossFade)
			{
				StopCoroutine("FadeToNewAudioClip");
				AudioSource audioSource = ((!cityTheme.gameObject.activeInHierarchy) ? cityTheme2 : cityTheme);
				AudioSource audioSource2 = ((!cityTheme.gameObject.activeInHierarchy) ? cityTheme : cityTheme2);
				audioSource.Stop();
				audioSource.gameObject.SetActive(value: false);
				audioSource2.gameObject.SetActive(value: true);
				audioSource2.clip = clip;
				audioSource2.volume = targetCityThemeVolume;
				audioSource2.Play();
				previousPlayed = clip;
			}
			else
			{
				StopCoroutine("FadeToNewAudioClip");
				StartCoroutine("FadeToNewAudioClip", clip);
				previousPlayed = clip;
			}
		}
	}

	private IEnumerator FadeToNewAudioClip(AudioClip newClip)
	{
		float curLerp = 0f;
		float lerpTime = 1.5f;
		float t = 0f;
		AudioSource curSource = ((!cityTheme.gameObject.activeInHierarchy) ? cityTheme2 : cityTheme);
		AudioSource targetSource = ((!cityTheme.gameObject.activeInHierarchy) ? cityTheme : cityTheme2);
		float curSrcStartVolume = curSource.volume;
		if (!curSource.isPlaying)
		{
			curSource.Play();
		}
		targetSource.gameObject.SetActive(value: true);
		targetSource.clip = newClip;
		targetSource.volume = 0f;
		targetSource.Play();
		while (t < 1f)
		{
			curLerp += Time.unscaledDeltaTime;
			curLerp = ((!(curLerp > lerpTime)) ? curLerp : lerpTime);
			t = curLerp / lerpTime;
			curSource.volume = Mathf.Lerp(curSrcStartVolume, 0f, t);
			targetSource.volume = Mathf.Lerp(0f, targetCityThemeVolume, t);
			yield return null;
		}
		curSource.gameObject.SetActive(value: false);
	}

	public void StopMusic()
	{
		cityTheme.Stop();
		cityTheme2.Stop();
	}

	public void FadeOutMusic(float fadeDuration = 1f)
	{
		cityTheme.DOFade(0f, fadeDuration).OnComplete(() =>
		{
			cityTheme.Stop();
		});
		cityTheme2.DOFade(0f, fadeDuration).OnComplete(() =>
		{
			cityTheme2.Stop();
		});
		previousPlayed = null;
	}

	private void HandlePaused(bool pauseStatus)
	{
		if (pauseStatus)
		{
			communicator.SaveDayAndTime();
			Time.timeScale = 0f;
			pauseScreen.gameObject.SetActive(value: true);
			targetCityThemeVolume = 0.25f;
			cityTheme.volume = targetCityThemeVolume;
			cityTheme2.volume = targetCityThemeVolume;
			if (!isInterior)
			{
				pinchZoomer.allowInteraction = false;
			}
		}
		else
		{
			Time.timeScale = 1f;
			targetCityThemeVolume = 1f;
			cityTheme.volume = targetCityThemeVolume;
			cityTheme2.volume = targetCityThemeVolume;
			if (!isInterior)
			{
				pinchZoomer.allowInteraction = true;
			}
		}
	}

	public void TravelToJail(bool insideJail)
	{
		if (!isTraveling)
		{
			StartCoroutine(TravelToJailRoutine(insideJail));
		}
	}

	public IEnumerator TravelToJailRoutine(bool insideJail = false)
	{
		isTraveling = true;
		cityCharacter.StopMove();
		cityCharacter.AllowInput = false;
		yield return new WaitForEndOfFrame();
		string loadingScreen = ((!insideJail) ? "Prefabs/GUI/LoadingScreenWhale" : "Prefabs/GUI/LoadingScreenDummy");
		GameObject loadingScreenWhale = ObjectPool.GetObjectFromResource(loadingScreen);
		Animator animator = loadingScreenWhale.GetComponent<Animator>();
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("intro"))
		{
			yield return null;
		}
		cityCharacter.SetPosition((!insideJail) ? jailOutsideStartPosition.GetComponent<PathNode>() : jailInsideStartPosition.GetComponent<PathNode>());
		SaveUtility.SetString("PlayerPosition", cityCharacter.gameObject.transform.position.ToString(), save: false);
		SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
		yield return new WaitForSeconds(2f);
		PlayCurrentLocationMusic(crossFade: true);
		animator.Play("outro");
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("outro") && animator.NormalizedTime() < 0.9f)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(loadingScreenWhale);
		isTraveling = false;
		cityCharacter.AllowInput = true;
	}

	public void TravelToCity()
	{
		if (!isTraveling && !pinchZoomer.isZoomedOut)
		{
			StartCoroutine("TravelToCityRoutine");
		}
	}

	private IEnumerator TravelToCityRoutine()
	{
		isTraveling = true;
		cityCharacter.StopMove();
		cityCharacter.AllowInput = false;
		yield return new WaitForEndOfFrame();
		GameObject loadingScreenWhale = ObjectPool.GetObjectFromResource("Prefabs/GUI/LoadingScreenWhale");
		Animator animator = loadingScreenWhale.GetComponent<Animator>();
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("intro"))
		{
			yield return null;
		}
		cityCharacter.StopMove();
		cityCharacter.SetPosition(cityBeachDockPosition.GetComponent<PathNode>());
		yield return new WaitForSeconds(2f);
		PlayCurrentLocationMusic(crossFade: true);
		animator.Play("outro");
		yield return new WaitForEndOfFrame();
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("outro") && animator.NormalizedTime() < 0.9f)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(loadingScreenWhale);
		isTraveling = false;
		cityCharacter.AllowInput = true;
	}

	public IEnumerator StartCreateHoodedHoodRoutine(string convoName)
	{
		yield return new WaitForEndOfFrame();
		while (LoadingScreenCover.isLoading || !LoadingScreenCover.allowInit)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
		GameUI.instance.ShowCinemaBars(0.8f);
		yield return new WaitForSeconds(0.9f);
		hoodedHoodAudioClip = Resources.Load("Audio/Music/music_hoodedhood_loop") as AudioClip;
		PlayMusic(hoodedHoodAudioClip, crossFade: true);
		hoodedHoodCity = new GameObject("Hooded Hood");
		hoodedHoodCity.transform.position = hoodedHoodEncounters.GetHoodPositionForName(convoName);
		CityNPC cityNPC = hoodedHoodCity.AddComponent<CityNPC>();
		cityNPC.activeNPCData = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.HoodedHood).spawnSetups[0].npcData;
		cityNPC.npcData.Add(cityNPC.activeNPCData);
		cityNPC.character = CharacterName.HoodedHood;
		cityNPC.EnableNPC();
	}

	public IEnumerator RemoveHoodedHoodCity(bool hideCinemaBars = true)
	{
		yield return new WaitForEndOfFrame();
		EndHoodedHoodAudio();
		if (hideCinemaBars)
		{
			GameUI.instance.HideCinemaBars(0.8f);
		}
		if (hoodedHoodCity != null)
		{
			CityNPC component = hoodedHoodCity.GetComponent<CityNPC>();
			component.DisableNPC();
			UnityEngine.Object.Destroy(hoodedHoodCity, 0.03f);
		}
	}

	public void EndHoodedHoodAudio()
	{
		StartCoroutine("StartEndHoodedHoodAudioRoutine");
	}

	private IEnumerator StartEndHoodedHoodAudioRoutine()
	{
		PlayCurrentLocationMusic(crossFade: true);
		yield return new WaitForSeconds(2f);
		if (hoodedHoodAudioClip != null)
		{
			if (cityTheme.clip == hoodedHoodAudioClip)
			{
				cityTheme.clip = null;
			}
			if (cityTheme2.clip == hoodedHoodAudioClip)
			{
				cityTheme2.clip = null;
			}
			Resources.UnloadAsset(hoodedHoodAudioClip);
		}
	}

	private void HandleOnCameraAngleClick()
	{
		CycleCameraAngle();
	}

	public void HandleOnNPCDown(tk2dUIItem uiItem)
	{
	}

	private void HandleOnTitanTowerDown()
	{
		if (!exitingCityScene)
		{
			exitingCityScene = true;
			prevPosition = cityCharacter.transform.position;
			prevCameraAngleIndex = curCameraAngleIndex;
			usePrevPosition = true;
			SaveUtility.SetString("PlayerPosition", cityCharacter.gameObject.transform.position.ToString(), save: false);
			SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
			if (communicator != null)
			{
				communicator.SaveDayAndTime();
				communicator.ResolveActiveNewsFlash();
			}
			ExitCityScene();
			LoadingScreenCover.LoadScene("TitanTower");
		}
	}

	private void HandleOnTitanTowerDownPathToTower()
	{
		if (!pinchZoomer.isZoomedOut)
		{
			titanTowerObj.GetComponent<Animator>().Play("ShopBuilding_AnimTap");
			cityCharacter.MoveToInteractiveNode(GetNearestPathNodeToPosition(titanTowerObj.transform.position), HandleOnTitanTowerDown);
		}
	}

	private void HandleOnTitanTowerTeleport()
	{
		titanTowerTeleporterObj.GetComponent<Animator>().Play("City_waypoint_tap");
		StartTeleporter(titanTowerTeleporterObj.transform, titanTowerPathNode);
	}

	private void HandleOnTitanTowerTeleportToCity()
	{
		titanTowerTeleporterReturnObj.GetComponent<Animator>().Play("City_waypoint_tap");
		StartTeleporter(titanTowerTeleporterReturnObj.transform, cityStartPathNode);
	}

	private void StartTeleporter(Transform teleporter, PathNode targetNode)
	{
		if (pinchZoomer.isZoomedOut)
		{
			return;
		}
		PathNode nearestPathNodeToPosition = GetNearestPathNodeToPosition(teleporter.position);
		float volume = 1f;
		if (cityCharacter.CurrentNode == nearestPathNodeToPosition && Vector3.Distance(nearestPathNodeToPosition.transform.position, cityCharacter.transform.position) < 0.3f)
		{
			volume = 0.5f;
			InteractWithTowerTeleport(targetNode);
		}
		else
		{
			cityCharacter.MoveToInteractiveNode(nearestPathNodeToPosition, () =>
			{
				InteractWithTowerTeleport(targetNode);
			});
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_wes_boing", GFAudioManager.SFX_GROUP, string.Empty, volume);
	}

	private void InteractWithTowerTeleport(PathNode targetNode)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_city_teleporter", GFAudioManager.SFX_GROUP, string.Empty);
		StartCoroutine(TeleportSequence(targetNode));
	}

	private IEnumerator TeleportSequence(PathNode targetNode, bool stopMovement = true)
	{
		prevPosition = cityCharacter.transform.position;
		prevCameraAngleIndex = curCameraAngleIndex;
		usePrevPosition = true;
		communicator.SaveDayAndTime();
		cityCharacter.AllowInput = false;
		if (stopMovement)
		{
			cityCharacter.StopMove();
			cityCharacter.PlayIdleAnimation();
		}
		GameUI.instance.FadeInBlackness();
		yield return new WaitForSeconds(0.4f);
		pinchZoomer.cameraFollow.approach = false;
		cityCharacter.SetPosition(targetNode);
		cityCharacter.StopMove();
		cityCharacter.PlayIdleAnimation();
		GameUI.instance.FadeOutBlackness();
		yield return new WaitForEndOfFrame();
		pinchZoomer.cameraFollow.approach = true;
		cityCharacter.AllowInput = true;
	}

	private void CycleCameraAngle()
	{
		curCameraAngleIndex++;
		if (curCameraAngleIndex >= cameraAngles.Count)
		{
			curCameraAngleIndex = 0;
		}
		UpdateCameraAngle();
	}

	private void UpdateCameraAngle(bool instant = false)
	{
		pinchZoomer.HomeRotation = cameraAngles[curCameraAngleIndex];
		if (instant)
		{
			DOTween.Complete(pinchZoomer.transform);
		}
	}

	public void DialogueBattleTriggered(CityEntityNPC npcEntity, CityEntityStateNPC npcState)
	{
		HandleBeginBattle(npcEntity, npcState, null, null);
	}

	public IEnumerator ShowNPCDialogueSystem(CityEntityNPC npcEntity, bool showExitButton = true, Conversation overrideConvo = null)
	{
		CityEntityStateNPC npcState = (CityEntityStateNPC)npcEntity.activeState;
		if (mySquad != null)
		{
			mySquad.HideNow();
		}
		swipeManager.enabled = false;
		if (pauseButton != null)
		{
			pauseButton.gameObject.SetActive(value: false);
		}
		GUICamera.instance.gameObject.SetActive(value: false);
		if (Communicator.instance != null)
		{
			Communicator.instance.SetCommunicatorShown(show: false);
		}
		pinchZoomer.allowPinchZoom = false;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
		dialogueSystem.transform.parent = dialogueSystemParent;
		dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -400f);
		dialogueSystem.BeginBattle += HandleBeginBattle;
		dialogueSystem.gameObject.SetActive(value: true);
		if (overrideConvo != null && overrideConvo.triggersImmediately)
		{
			dialogueSystem.Init(guiCamera2, overrideConvo, npcEntity, npcState, showExitButton);
		}
		else if (npcState.mainConversation != null && npcState.mainConversation.triggersImmediately)
		{
			dialogueSystem.Init(guiCamera2, npcState.mainConversation, npcEntity, npcState, showExitButton);
		}
		else
		{
			dialogueSystem.Init(guiCamera2, npcEntity, npcState);
		}
		while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialogueSystem != null)
		{
			dialogueSystem.BeginBattle -= HandleBeginBattle;
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForEndOfFrame();
		if (!preventShowingCommunicatorAfterDialogue && Communicator.instance != null)
		{
			Communicator.instance.SetCommunicatorShown(show: true);
		}
		if (!preventShowingGUIAfterDialogue && (CityReward.instance == null || !CityReward.instance.showingRewards) && !startingBattle)
		{
			GUICamera.instance.gameObject.SetActive(value: true);
		}
		pinchZoomer.allowPinchZoom = true;
		preventShowingCommunicatorAfterDialogue = false;
		preventShowingGUIAfterDialogue = false;
		swipeManager.enabled = true;
		if (pauseButton != null)
		{
			pauseButton.gameObject.SetActive(value: true);
		}
	}

	private IEnumerator DeliverGoodsForMission(MissionData missionData, CityEntityNPC npcEntity)
	{
		Conversation convo = (Conversation)Resources.Load("Data/TT2/CONVO_ANY_DAILYFIGGIVEN_0" + UnityEngine.Random.Range(1, 5));
		CityEntityStateNPC npcState = (CityEntityStateNPC)npcEntity.activeState;
		if (mySquad != null)
		{
			mySquad.HideNow();
		}
		swipeManager.enabled = false;
		pinchZoomer.allowPinchZoom = false;
		if (pauseButton != null)
		{
			pauseButton.gameObject.SetActive(value: false);
		}
		GUICamera.instance.gameObject.SetActive(value: false);
		if (Communicator.instance != null)
		{
			Communicator.instance.SetCommunicatorShown(show: false);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
		dialogueSystem.transform.parent = dialogueSystemParent;
		dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -400f);
		dialogueSystem.gameObject.SetActive(value: true);
		dialogueSystem.Init(guiCamera2, convo, npcEntity, npcState, showExitButton: false);
		while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
		{
			yield return null;
		}
		Player.instance.CompleteMission(missionData.uid, GameManager.instance.communicatorDatabase.GetMission(missionData.uid).autoCollect);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForEndOfFrame();
		if (!preventShowingCommunicatorAfterDialogue && Communicator.instance != null)
		{
			Communicator.instance.SetCommunicatorShown(show: true);
		}
		if (!preventShowingGUIAfterDialogue && (CityReward.instance == null || !CityReward.instance.showingRewards) && !startingBattle)
		{
			GUICamera.instance.gameObject.SetActive(value: true);
		}
		pinchZoomer.allowPinchZoom = true;
		preventShowingCommunicatorAfterDialogue = false;
		preventShowingGUIAfterDialogue = false;
		swipeManager.enabled = true;
		if (pauseButton != null)
		{
			pauseButton.gameObject.SetActive(value: true);
		}
	}

	private void HandleBeginBattle(CityEntityNPC npcEntity, CityEntityStateNPC npcState, Mission battleMission, Mission dailyBattleMission)
	{
		if (startingBattle)
		{
			return;
		}
		startingBattle = true;
		SaveUtility.SetString("PlayerPosition", cityCharacter.gameObject.transform.position.ToString(), save: false);
		prevPosition = cityCharacter.transform.position;
		prevCameraAngleIndex = curCameraAngleIndex;
		usePrevPosition = true;
		ChallengeModeManager.isNonJLChallenge = false;
		BattleDefinition battleDefinition = new BattleDefinition();
		battleDefinition.enemyName = npcEntity.characterName;
		battleDefinition.enemyDefeatedMsg = npcEntity.npcDefinition.name;
		battleDefinition.enemyCharacterName = npcEntity.npcDefinition.characterType;
		battleDefinition.enemyGUID = npcEntity.npcGuid;
		battleDefinition.cityName = cityName;
		battleDefinition.districtLocation = CityLocation.None;
		if (allCityLocationBounds != null)
		{
			for (int i = 0; i < allCityLocationBounds.Length; i++)
			{
				CityLocationBounds cityLocationBounds = allCityLocationBounds[i];
				if (cityLocationBounds.bounds.Contains(npcEntity.transform.position))
				{
					battleDefinition.districtLocation = cityLocationBounds.cityLocaton;
					break;
				}
			}
		}
		afterBattleConvoName = string.Empty;
		if (npcState != null)
		{
			battleDefinition.xpReward = npcState.battleConfig.xpReward;
			battleDefinition.coinReward = npcState.battleConfig.coinReward;
			battleDefinition.useDefaultPreBattleSound = npcEntity.npcDefinition.useDefaultPreBattleSounds;
			battleDefinition.soundData = npcEntity.npcDefinition.preBattleSoundData;
			battleDefinition.countdownCharacterUID = npcEntity.npcDefinition.countdownCharacterUID;
			if (battleMission != null && battleMission.battleConditions.Length > 0)
			{
				battleDefinition.newBattleConditions = new List<BattleCondition>(battleMission.battleConditions);
			}
			else if (npcState.battleRequirements.useRequirements)
			{
				if (npcState.battleRequirements.extendIntoBattle)
				{
					battleDefinition.battleConditions = new List<NPCBattleCondition>(npcState.battleRequirements.conditions);
				}
			}
			else
			{
				battleDefinition.battleConditions = null;
			}
			if (npcState.defeatedConversation != null)
			{
				afterBattleConvoName = npcState.defeatedConversation.name;
			}
		}
		List<TournamentRoundFigureData> list = new List<TournamentRoundFigureData>();
		if (battleMission != null && battleMission.useFigureOverrides)
		{
			Debug.Log("Player has Battle mission with figure overrides");
			list.AddRange(battleMission.figureOverrides);
		}
		else if (dailyBattleMission != null && dailyBattleMission.useFigureOverrides)
		{
			Debug.Log("Player has Daily Battle mission with figure overrides");
			list.AddRange(dailyBattleMission.figureOverrides);
		}
		else if (npcState != null)
		{
			int[] array = new int[3]
			{
				npcState.battleConfig.teamSizeWeight1,
				npcState.battleConfig.teamSizeWeight2,
				npcState.battleConfig.teamSizeWeight3
			};
			int max = npcState.battleConfig.teamSizeWeight1 + npcState.battleConfig.teamSizeWeight2 + npcState.battleConfig.teamSizeWeight3;
			int num = UnityEngine.Random.Range(0, max);
			int num2 = 0;
			int a = 3;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] > 0)
				{
					num2 += array[j];
					if (num < num2)
					{
						a = j + 1;
						break;
					}
				}
			}
			a = Mathf.Min(a, npcState.battleConfig.figures.Length);
			while (list.Count < a)
			{
				TournamentRoundFigureData item = npcState.battleConfig.figures[UnityEngine.Random.Range(0, npcState.battleConfig.figures.Length)];
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
		}
		int count = list.Count;
		for (int k = 0; k < count; k++)
		{
			battleDefinition.enemyTeam.Add(new Character(k, list[k]));
		}
		if (npcState != null)
		{
			battleDefinition.enemyAccessoryUID = npcState.battleConfig.equippedAccessoryUID;
		}
		PreBattleController.battleDefinition = battleDefinition;
		communicator.SaveDayAndTime();
		StartCoroutine("BeginPreBattle");
	}

	private IEnumerator BeginPreBattle()
	{
		Debug.Log("BeginPreBattle");
		yield return new WaitForSeconds(0.16f);
		if (communicator != null)
		{
			while (communicator.MissionScrollerActive())
			{
				yield return new WaitForEndOfFrame();
			}
		}
		if (GameUI.instance != null)
		{
			GameUI.instance.FadeInBlackness();
		}
		yield return new WaitForSeconds(0.3f);
		if (DialogueTree.instance != null)
		{
			UnityEngine.Object.Destroy(DialogueTree.instance.gameObject);
		}
		yield return new WaitForEndOfFrame();
		Communicator.afterBattleConvoName = afterBattleConvoName;
		if (communicator != null)
		{
			communicator.ResolveActiveNewsFlash();
		}
		ExitCityScene();
		LoadingScreenCover.LoadScene("PreBattle", showLoadingScreen: false);
	}

	public void HandleShowDialogue(Conversation conversation, bool showExitButton = true, CityEntityNPC npcEntity = null, CityEntityStateNPC npcState = null, bool trackCompletion = false, float delay = 0f)
	{
		StartCoroutine(ShowDialogue(conversation, showExitButton, npcEntity, npcState, trackCompletion, delay));
	}

	public IEnumerator PlayConvo(Conversation convo)
	{
		yield return StartCoroutine(ShowDialogue(convo, showExitButton: false));
	}

	public IEnumerator PlayDialogue(string convoName)
	{
		Conversation convo = Resources.Load("Data/Conversations/" + convoName) as Conversation;
		if (convo != null)
		{
			yield return StartCoroutine(ShowDialogue(convo, showExitButton: false));
		}
	}

	public IEnumerator PlayTT2Dialogue(string convoName)
	{
		Conversation convo = Resources.Load("Data/TT2/" + convoName) as Conversation;
		if (convo != null)
		{
			yield return StartCoroutine(ShowDialogue(convo, showExitButton: false));
		}
	}

	public IEnumerator ShowDialogue(Conversation conversation, bool showExitButton = true, CityEntityNPC npcEntity = null, CityEntityStateNPC npcState = null, bool trackCompletion = false, float delay = 0f, bool showBacking = true)
	{
		if (conversation == null)
		{
			Debug.LogError("Convo is null");
			yield break;
		}
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			while (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		if (mySquad != null)
		{
			mySquad.HideNow();
		}
		swipeManager.enabled = false;
		if (pauseButton != null)
		{
			pauseButton.gameObject.SetActive(value: false);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
		dialogueSystem.transform.parent = dialogueSystemParent;
		dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -400f);
		dialogueSystem.gameObject.SetActive(value: true);
		dialogueSystem.Init(guiCamera2, conversation, npcEntity, npcState, showExitButton);
		dialogueSystem.BeginBattle += HandleBeginBattle;
		dialogueSystem.backing.gameObject.SetActive(showBacking);
		GUICamera.instance.gameObject.SetActive(value: false);
		if (Communicator.instance != null)
		{
			Communicator.instance.SetCommunicatorShown(show: false);
		}
		pinchZoomer.allowPinchZoom = false;
		while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
		{
			yield return null;
		}
		dialogueSystem.BeginBattle -= HandleBeginBattle;
		if (trackCompletion && conversation != null)
		{
			SaveUtility.SetInt("GAME_PLAYED_" + conversation.name, 1, save: false);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
		if (FinishedDialogue != null)
		{
			FinishedDialogue();
		}
		if (!preventShowingCommunicatorAfterDialogue && Communicator.instance != null)
		{
			Communicator.instance.SetCommunicatorShown(show: true);
		}
		if (!preventShowingGUIAfterDialogue && (CityReward.instance == null || (!CityReward.instance.showingRewards && !startingBattle)))
		{
			GUICamera.instance.gameObject.SetActive(value: true);
		}
		pinchZoomer.allowPinchZoom = true;
		preventShowingCommunicatorAfterDialogue = false;
		preventShowingGUIAfterDialogue = false;
		swipeManager.enabled = true;
		if (pauseButton != null)
		{
			pauseButton.gameObject.SetActive(value: true);
		}
	}

	public void SetMagnifyingGlassActive(bool isActive)
	{
		magnifyingGlassObj.SetActive(isActive);
	}

	public void SetTitanTowerBtnActive(bool isActive)
	{
		titanTowerBtnObj.SetActive(isActive);
	}

	public void AcquireAllCharacters()
	{
		if (GameManager.instance == null || Player.instance == null)
		{
			return;
		}
		List<CharacterDefinition> list = null;
		if (GameManager.instance.characterDatabase != null)
		{
			list = GameManager.instance.characterDatabase.characters;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					CharacterDefinition characterDefinition = list[i];
					if (characterDefinition.isActive)
					{
						Player.instance.AddCharacter(CharacterRoller.RollCharacterData(characterDefinition, -1, characterDefinition.uid, 10));
						Player.instance.AcquireCharacter(characterDefinition.uid);
						Character characterAdded = Player.instance.characters.GetLastItem();
						if ((characterAdded.characterDefinition.uid == 0 || characterAdded.characterDefinition.uid == 85 || characterAdded.characterDefinition.uid == 78 || characterAdded.characterDefinition.uid == 8 || characterAdded.characterDefinition.uid == 90 || characterAdded.characterDefinition.uid == 26) && Player.instance.activeParty.Find((Character x) => x.characterDefinition.uid == characterAdded.characterDefinition.uid) == null)
						{
							Player.instance.AddToParty(characterAdded);
						}
					}
				}
			}
			else
			{
				Debug.LogError("charactersReference is NULL! Which means characterDatabase.characters is NULL! Report this error!");
			}
		}
		else
		{
			Debug.LogError("Game Manager Character Database is NULL! Report this error!");
		}
	}

	public PathNode GetNearestPathNodeToPosition(Vector3 position, bool ignoreDisabledNodes = false)
	{
		PathNode result = null;
		float num = float.PositiveInfinity;
		for (int i = 0; i < allNodes.Count; i++)
		{
			PathNode pathNode = allNodes[i];
			if (!pathNode.isTemporary && (pathNode.gameObject.activeInHierarchy || !ignoreDisabledNodes))
			{
				float num2 = Vector3.Distance(pathNode.transform.position, position);
				if (num2 < num)
				{
					num = num2;
					result = pathNode;
				}
			}
		}
		return result;
	}

	private void HandleZoomedIn()
	{
		if (wayPoint != null && wayPointIcon != null && wayPointIcon.gameObject.activeSelf && wayPointIcon.animator.IsNamed("Landmarkicon_intro_waypoint", "Landmarkicon_select_loop"))
		{
			wayPointIcon.animator.Play("Landmarkicon_select_outro");
		}
	}

	public void PreventManualWayPoint(bool prevent)
	{
		if (!prevent)
		{
			if (!(CoreGameTutorial.instance != null) || CoreGameTutorial.instance.currentStep != CoreGameTutorial.Step.ShopTutorial2Start)
			{
				StopCoroutine("ResetPreventManualWayPoint");
				StartCoroutine("ResetPreventManualWayPoint");
			}
		}
		else
		{
			preventManualWayPoint = prevent;
		}
	}

	private void Handletk2dUIItemOnDown(tk2dUIItem uiItem)
	{
		touchStartedOnButton = true;
	}

	private void Handletk2dUIItemOnRelease(tk2dUIItem uiItem)
	{
		StopCoroutine("ResetTouchStartedOnButton");
		StartCoroutine("ResetTouchStartedOnButton");
	}

	private IEnumerator ResetPreventManualWayPoint()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		preventManualWayPoint = false;
	}

	private IEnumerator ResetTouchStartedOnButton()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		touchStartedOnButton = false;
	}

	private void HandleSingleTap(Vector2 screenPosition)
	{
		if (!exitingCityScene)
		{
			if (tapRoutine != null)
			{
				StopCoroutine(tapRoutine);
			}
			tapRoutine = StartCoroutine(HandleSingleTapRoutine(screenPosition));
		}
	}

	private IEnumerator HandleSingleTapRoutine(Vector2 screenPosition)
	{
		yield return new WaitForEndOfFrame();
		if ((TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive) || PauseManager.instance.isPaused || !(wayPoint != null) || preventManualWayPoint || touchStartedOnButton || landmarkPressed || communicator.PopUpActive || !pinchZoomer.isZoomedOut || !allowWayPointMovement || _zoomType != ZoomType.Normal)
		{
			yield break;
		}
		Vector3 position = new Vector3(screenPosition.x, screenPosition.y, pinchZoomer.thisCamera.nearClipPlane - pinchZoomer.transform.position.y - pinchZoomer.cameraTransform.localPosition.z);
		Vector3 vector = pinchZoomer.thisCamera.ScreenToWorldPoint(position);
		vector.y = 0f;
		if (IsPointInsideAnyDistrictBounds(vector))
		{
			MoveWayPointToPosition(vector);
			yield break;
		}
		PathNode nearestPathNodeToPosition = GetNearestPathNodeToPosition(vector);
		if (nearestPathNodeToPosition != null)
		{
			MoveWayPointToPosition(nearestPathNodeToPosition.transform.position);
		}
	}

	public void MoveWayPointToPosition(Vector3 worldPos)
	{
		if (allowWayPointMovement && !exitingCityScene)
		{
			allowWayPointMovement = false;
			StartCoroutine("DelayWayPointMovement");
			wayPoint.transform.position = worldPos;
			wayPointIcon.transform.parent.gameObject.SetActive(value: true);
			wayPointIcon.gameObject.SetActive(value: true);
			wayPointIcon.waypointArrow.gameObject.SetActive(value: true);
			wayPointIcon.animator.Play("Landmarkicon_intro_waypoint");
			LandmarkWayPointClicked(wayPointIcon, unselectSelected: true, manualWayPointClick: true);
		}
	}

	private bool IsPointInsideAnyDistrictBounds(Vector3 pointToCheck)
	{
		if (allCityLocationBounds != null)
		{
			for (int i = 0; i < allCityLocationBounds.Length; i++)
			{
				CityLocationBounds cityLocationBounds = allCityLocationBounds[i];
				if (cityLocationBounds.bounds.Contains(pointToCheck))
				{
					return true;
				}
			}
		}
		return false;
	}

	private IEnumerator DelayWayPointMovement()
	{
		yield return new WaitForSeconds(0.4f);
		allowWayPointMovement = true;
	}

	public void DebugPlayTournament(TournamentDefinition tournament)
	{
		TournamentController.initDefinition = tournament;
		TournamentController.isDebugTournament = true;
		LoadingScreenCover.LoadScene("Tournament");
	}

	public void UnlockAllTunnels()
	{
		Tunnel[] array = UnityEngine.Object.FindObjectsOfType<Tunnel>();
		foreach (Tunnel tunnel in array)
		{
			tunnel.ForceUnlock();
		}
	}

	public void UnlockAllModChips()
	{
		List<ModChipDefinition> modChips = GameManager.instance.itemDatabase.modChips;
		int count = modChips.Count;
		for (int i = 0; i < count; i++)
		{
			Player.instance.AddItem(modChips[i]);
		}
	}

	public void UnlockAllAccessories()
	{
		List<AccessoryItemDefinition> list = new List<AccessoryItemDefinition>(GameManager.instance.itemDatabase.accessories);
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			int uid = list[i].uid;
			Player.instance.AddAccessory(uid);
			if (list[i].showInTower)
			{
				Player.instance.AddToEquippedAccessories(uid);
			}
		}
	}

	public void UnlockAllRepaints()
	{
		int count = Player.instance.characters.Count;
		for (int i = 0; i < count; i++)
		{
			Character character = Player.instance.characters[i];
			for (int j = 0; j < character.characterDefinition.repaintSlots.Count; j++)
			{
				Player.instance.AddToRepaintColorsPurchased(character, character.characterDefinition.repaintSlots[j].id);
			}
		}
	}

	public void ToggleDebugButtons()
	{
	}

	public void LoadPaxSave()
	{
		SaveUtility.CopySave("TTG_PAX.sav", "TT2.sav");
		SaveUtility.LoadSaveFile();
		Player.instance.Load();
		PauseManager.instance.Resume();
		TitleScreenController.shouldShowTitleScreen = true;
		LoadingScreenCover.ReloadActiveScene();
	}

	public void ResetTowerTutorials()
	{
		Player.instance.UnsetTrigger("tutorialEnhancement");
		Player.instance.UnsetTrigger("runTutorialEnhancement");
		Player.instance.UnsetTrigger("tutorialEnhancementUnlocked");
		Player.instance.UnsetTrigger("runTutorialEnhancementUnlocked");
		Player.instance.UnsetTrigger("tutorialFirstModChip");
		Player.instance.UnsetTrigger("runTutorialFirstModChip");
		Player.instance.UnsetTrigger("tutorialFirstModChipFound");
		Player.instance.UnsetTrigger("runTutorialFirstModChipFound");
		Player.instance.UnsetTrigger("tutorialSquadFull");
		Player.instance.UnsetTrigger("runTutorialSquadFull");
		if (Player.instance.HasCharacterAtLevel(5))
		{
			Player.instance.SetTrigger("runTutorialEnhancementUnlocked");
		}
		if (Player.instance.HasItemOfType<ModChipDefinition>())
		{
			Player.instance.SetTrigger("tutorialFirstModChipFound");
			Player.instance.SetTrigger("runTutorialFirstModChip");
		}
		if (Player.instance.isActivePartyFull)
		{
			Player.instance.SetTrigger("runTutorialSquadFull");
		}
		Player.instance.Save();
	}

	public void CopySave()
	{
		SaveUtility.CopySave();
	}

	public void UpdateNodeForceStopRemovingNPCs(SpawnGroupManager spawnGroup, List<CreatedNPC> npcsRemoved)
	{
		List<CreatedNPC> list = new List<CreatedNPC>(npcsRemoved);
		List<CreatedNPC> npcsCreated = spawnGroupMaster.npcsCreated;
		for (int num = npcsCreated.Count - 1; num >= 0; num--)
		{
			CreatedNPC createdNPC = npcsCreated[num];
			for (int num2 = list.Count - 1; num2 >= 0; num2--)
			{
				CreatedNPC createdNPC2 = list[num2];
				if (createdNPC.nearestNode == createdNPC2.nearestNode)
				{
					list.RemoveAt(num2);
				}
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].nearestNode != null)
			{
				list[i].nearestNode.SetForceStop(shouldForceStop: false);
			}
		}
	}

	public void DisplayNotEnoughLooseChange(float displayDelay = 0f, float duration = 1.5f)
	{
		if (noLooseChangeCoroutine != null)
		{
			StopCoroutine(noLooseChangeCoroutine);
		}
		noLooseChangeCoroutine = StartCoroutine(DisplayNoLooseChangeSequence(displayDelay, duration));
	}

	private IEnumerator DisplayNoLooseChangeSequence(float displayDelay, float duration)
	{
		yield return new WaitForSeconds(displayDelay);
		looseChangeWarning.SetActive(value: true);
		yield return new WaitForSeconds(duration);
		looseChangeWarning.SetActive(value: false);
	}

	public void ExitCityScene()
	{
		if (!exitingCityScene)
		{
			exitingCityScene = true;
			if (OnExitCityScene != null)
			{
				OnExitCityScene();
			}
		}
	}

	public void RegisterCityVO(AudioSource voSource)
	{
		if (!(voSource == null))
		{
			if (cityVOSources == null)
			{
				cityVOSources = new Dictionary<AudioSource, AudioClip>();
			}
			CleanCityVOSources();
			if (!cityVOSources.ContainsKey(voSource))
			{
				cityVOSources.Add(voSource, voSource.clip);
			}
		}
	}

	public void StopAllCityVO()
	{
		if (cityVOSources == null)
		{
			return;
		}
		foreach (AudioSource key in cityVOSources.Keys)
		{
			if (key != null)
			{
				key.Stop();
			}
		}
		CleanCityVOSources();
	}

	private void CleanCityVOSources()
	{
		if (cityVOSources == null || cityVOSources.Count <= 0)
		{
			return;
		}
		List<AudioSource> list = new List<AudioSource>();
		foreach (KeyValuePair<AudioSource, AudioClip> cityVOSource in cityVOSources)
		{
			if (cityVOSource.Key == null || !cityVOSource.Key.isPlaying || cityVOSource.Key.clip != cityVOSource.Value)
			{
				list.Add(cityVOSource.Key);
			}
		}
		foreach (AudioSource item in list)
		{
			cityVOSources.Remove(item);
		}
	}

	public bool IsCityVOPlaying()
	{
		CleanCityVOSources();
		if (cityVOSources != null)
		{
			foreach (KeyValuePair<AudioSource, AudioClip> cityVOSource in cityVOSources)
			{
				if (cityVOSource.Key != null && cityVOSource.Key.isPlaying)
				{
					return true;
				}
			}
		}
		return false;
	}
}
