using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
	public static BattleDefinition battleDefinition;

	public static BattleResult battleResult;

	public Camera guiCamera;

	public SplitCameras splitCameras;

	public BattleCamera playerBattleCamera;

	public BattleCamera enemyBattleCamera;

	public ActionMeter playerActionMeter;

	public ActionMeter enemyActionMeter;

	public PortraitSwitcher playerPortraits;

	public PortraitSwitcher enemyPortraits;

	public AccessoryBattleButton playerAccessoryButton;

	public AccessoryBattleButton enemyAccessoryButton;

	public EmoteUI playerEmoteUI;

	public EmoteUIEnemy enemyEmoteUI;

	public TextMeshPro pvpEnemyPlayerName;

	public TextMeshPro playerPortraitSwitcherTimer;

	public TextMeshPro enemyPortraitSwitcherTimer;

	public SimpleFaderTk2d blackFade;

	public PauseButton pauseButton;

	public DefeatScreen defeatScreen;

	public VictoryScreen victoryScreen;

	public PauseScreen pauseScreen;

	public LevelUpScreen levelUpScreen;

	public WalletGUI walletGUI;

	public BatteryGUI batteryGUI;

	public BattleCountdown battleCountdown;

	[HideInInspector]
	public float goTapBonusMultiplier = 1f;

	public RuntimeAnimatorController battleSpawnAnimController;

	public RuntimeAnimatorController battleVictoryAnimController;

	public RuntimeAnimatorController accessoryIntroAnimController;

	public Vector3 secondCharacterOffset = new Vector3(1.5f, 0f, 0f);

	public Vector3 thirdCharacterOffset = new Vector3(-1.5f, 0f, 0f);

	public float explosionDelay = 3.5f;

	public float spawnStaggerDelay = 0.5f;

	public float fadeStartDelay;

	public float fadeDuration = 1f;

	public Ease fadeEaseFunction = Ease.InSine;

	[HideInInspector]
	public List<CharacterData> playerCharacters;

	[HideInInspector]
	public List<bool> playerCharactersEnabled;

	[HideInInspector]
	public List<CharacterData> enemyCharacters;

	[HideInInspector]
	public List<bool> enemyCharactersEnabled;

	[HideInInspector]
	public int testBattleArena = -1;

	[HideInInspector]
	public PlayerAIType playerAIType;

	[HideInInspector]
	public ChallengeModeType testChallengeMode = ChallengeModeType.None;

	[HideInInspector]
	public int accessoryUID = -1;

	[HideInInspector]
	public int enemyAccessoryUID = -1;

	[HideInInspector]
	public bool playerAIEnabled;

	[HideInInspector]
	public bool enemyAIEnabled = true;

	private BattleCharacter _playerCharacter;

	private BattleCharacter _enemyCharacter;

	private List<Character> _playerParty;

	private List<Character> _enemyParty;

	private Dictionary<Character, int> swapInCounts;

	private Dictionary<Character, Dictionary<AbilityData, int>> abilityUseCounts;

	private BattleArenaData _arenaData;

	private DiminishingReturns diminishingReturnsUtil;

	private bool _battleStarted;

	private bool _battleEnded;

	private float timeScaleWhenPaused = 1f;

	private float timeScaleWhenAbilityPaused = 1f;

	private bool _isViewingAbilityDescriptions;

	private bool _tofuUsed;

	private bool startingFromEditor;

	private AccessoryInfoBox accessoryInfoBox;

	private Vector3 originalAccessoryPosition;

	private AbilityInfoBox infoBox;

	private AbilityButton selectedButton;

	private bool introCameraComplete;

	private List<BattleCharacter> benchedPlayerCharacters;

	public List<BattleCharacter> benchedEnemyCharacters = new List<BattleCharacter>();

	private int previousCameraStateHash;

	private bool _waiting;

	private AudioSource _battleTheme;

	private float _battleThemeVolume = 1f;

	public BattleArenaData arenaData => _arenaData;

	public bool isViewingAbilityDescriptions => _isViewingAbilityDescriptions;

	public bool waiting => _waiting;

	public bool ignoreLongPress { get; set; }

	public bool tofuUsed
	{
		get
		{
			return _tofuUsed;
		}
		set
		{
			_tofuUsed = value;
		}
	}

	public bool battleStarted => _battleStarted;

	public bool battleEnded => _battleEnded;

	public bool isBattleRunning => _battleStarted && !_battleEnded;

	public BattleCharacter playerCharacter => _playerCharacter;

	public BattleCharacter enemyCharacter => _enemyCharacter;

	public List<Character> playerParty => _playerParty;

	public List<Character> enemyParty => _enemyParty;

	public int numAliveInPlayerParty
	{
		get
		{
			int num = 0;
			int count = _playerParty.Count;
			for (int i = 0; i < count; i++)
			{
				if (!_playerParty[i].isDead)
				{
					num++;
				}
			}
			return num;
		}
	}

	public bool isPlayerPartyDead
	{
		get
		{
			int count = _playerParty.Count;
			for (int i = 0; i < count; i++)
			{
				if (!_playerParty[i].isDead)
				{
					return false;
				}
			}
			return true;
		}
	}

	public bool isEnemyPartyDead
	{
		get
		{
			int count = _enemyParty.Count;
			for (int i = 0; i < count; i++)
			{
				if (!_enemyParty[i].isDead)
				{
					return false;
				}
			}
			return true;
		}
	}

	public int averageCharacterLevelInBattle
	{
		get
		{
			int num = 0;
			for (int i = 0; i < _playerParty.Count; i++)
			{
				num += _playerParty[i].level;
			}
			for (int j = 0; j < _enemyParty.Count; j++)
			{
				num += _enemyParty[j].level;
			}
			return Mathf.FloorToInt((float)num / (float)(_playerParty.Count + _enemyParty.Count));
		}
	}

	public AudioSource battleTheme => _battleTheme;

	public bool useFlashbackBattleEnding { get; set; }

	public static BattleController instance { get; private set; }

	public event Action BattleIntroStarted;

	public event Action BattleStarted;

	public event Action BattleEnded;

	public event Action<Character> CharacterSwapInStart;

	public event Action<Character> CharacterSwappedIn;

	public event Action<Character> CharacterSwapOutStart;

	public event Action<Character> CharacterSwappedOut;

	public event Action WaitingStarted;

	public event Action WaitingEnded;

	public event Action AbilityTooltipOpened;

	public event Action AbilityTooltipClosed;

	private void Awake()
	{
		instance = this;
		if (pauseButton == null)
		{
			pauseButton = UnityEngine.Object.FindObjectOfType<PauseButton>();
		}
		if (defeatScreen.gameObject.activeSelf)
		{
			defeatScreen.gameObject.SetActive(value: false);
		}
		if (victoryScreen.gameObject.activeSelf)
		{
			victoryScreen.gameObject.SetActive(value: false);
		}
		if (pauseScreen.gameObject.activeSelf)
		{
			pauseScreen.gameObject.SetActive(value: false);
		}
		if (playerPortraitSwitcherTimer.gameObject.activeSelf)
		{
			playerPortraitSwitcherTimer.gameObject.SetActive(value: false);
		}
		if (enemyPortraitSwitcherTimer.gameObject.activeSelf)
		{
			enemyPortraitSwitcherTimer.gameObject.SetActive(value: false);
		}
		if (batteryGUI.gameObject.activeSelf)
		{
			batteryGUI.gameObject.SetActive(value: false);
		}
		PauseManager.instance.isEnabled = true;
		PauseManager.instance.Resume();
		Player.instance.pvpData.BattleRequestAccepted += HandleBattleRequestAccepted;
	}

	private void Update()
	{
	}

	private IEnumerator Start()
	{
		if (Time.frameCount == 1)
		{
			startingFromEditor = true;
			battleDefinition = new BattleDefinition();
			battleDefinition.playerAIType = playerAIType;
			battleDefinition.accessoryUID = accessoryUID;
			battleDefinition.enemyAccessoryUID = enemyAccessoryUID;
			if (testChallengeMode != ChallengeModeType.None)
			{
				battleDefinition.type = BattleType.Challenge;
				ChallengeModeManager.isNonJLChallenge = true;
				ChallengeModeManager.challengeModeDirty = true;
				ChallengeModeManager.challengeModeHard = false;
				ChallengeModeManager.isChallengeModeActive = true;
				ChallengeModeManager.useTestMode = true;
				ChallengeModeManager.testMode = testChallengeMode;
				ChallengeModeManager.modeDefinition = GameManager.instance.challengeModeDatabase.GetItem(testChallengeMode);
				ChallengeModeManager.currentModeValues = ChallengeModeManager.modeDefinition.easyModeValues;
			}
		}
		tofuUsed = false;
		battleResult = null;
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		_playerParty = new List<Character>();
		_enemyParty = new List<Character>();
		swapInCounts = new Dictionary<Character, int>();
		abilityUseCounts = new Dictionary<Character, Dictionary<AbilityData, int>>();
		if (GameManager.instance.battleArenaDatabase.arenas.Count > 0)
		{
			if (testBattleArena > -1)
			{
				_arenaData = GameManager.instance.battleArenaDatabase.GetArenaByUID(testBattleArena);
			}
			else if (battleDefinition.arenaUID > -1)
			{
				_arenaData = GameManager.instance.battleArenaDatabase.GetArenaByUID(battleDefinition.arenaUID);
			}
			if (_arenaData == null)
			{
				if (!Player.instance.HasMissionCompleted(4))
				{
					_arenaData = GameManager.instance.battleArenaDatabase.GetArenaByUID(4);
				}
				else
				{
					_arenaData = GameManager.instance.battleArenaDatabase.GetRandomArena();
				}
			}
			GameObject original = Resources.Load<GameObject>(_arenaData.prefabPath);
			GameObject go = UnityEngine.Object.Instantiate(original, Vector3.zero, Quaternion.identity);
			SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName("Battle"));
		}
		if (battleDefinition != null)
		{
			switch (battleDefinition.type)
			{
			case BattleType.Normal:
			case BattleType.Pvp:
			case BattleType.PvpCpu:
			case BattleType.PvpFriend:
				pauseScreen.HomeClicked += HandleOnHomeClicked;
				break;
			case BattleType.Tournament:
				pauseScreen.HomeClicked += HandleOnHomeClicked;
				break;
			case BattleType.Challenge:
				pauseScreen.HomeClicked += HandleOnHomeClicked;
				break;
			}
		}
		Dictionary<string, int> characterCounts = new Dictionary<string, int>();
		string bcPath = string.Empty;
		List<Character> playerTeam = null;
		List<Character> enemyTeam = null;
		if (battleDefinition != null && battleDefinition.playerTeamOverride != null)
		{
			playerTeam = battleDefinition.playerTeamOverride;
		}
		else
		{
			playerTeam = ((Player.instance.activeTeam.Count <= 0) ? playerCharacters.ConvertAll((CharacterData input) => new Character(input)) : Player.instance.activeTeam);
		}
		enemyTeam = ((battleDefinition == null || battleDefinition.enemyTeam == null) ? enemyCharacters.ConvertAll((CharacterData input) => new Character(input)) : battleDefinition.enemyTeam);
		int len = Mathf.Max(playerTeam.Count, enemyTeam.Count);
		for (int num = 0; num < len; num++)
		{
			if (num < playerTeam.Count)
			{
				_playerParty.Add(playerTeam[num]);
				if (battleDefinition == null || battleDefinition.type == BattleType.Normal || battleDefinition.type == BattleType.Challenge || battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend)
				{
					_playerParty[num].hitPoints = _playerParty[num].maxHitPoints;
				}
				if (battleDefinition != null && battleDefinition.type == BattleType.Tournament)
				{
					_playerParty[num].hitPoints += _playerParty[num].hitPointsTeamBoost;
				}
				_playerParty[num].tempAttackBonus = 0;
				_playerParty[num].attackModifier = 1f;
				_playerParty[num].finalDamageMultiplier = 1f;
				if (_playerParty[num].modChip != null)
				{
					_playerParty[num].modChipSlot.PrepareChipForBattle(this, playerActionMeter);
				}
				if (characterCounts.ContainsKey(_playerParty[num].characterDefinition.battleCharacterPath))
				{
					characterCounts[_playerParty[num].characterDefinition.battleCharacterPath]++;
				}
				else
				{
					characterCounts.Add(_playerParty[num].characterDefinition.battleCharacterPath, 1);
				}
				_playerParty[num].Updated += HandlePlayerCharacterUpdated;
				if (_playerParty[num].characterDefinition.transformations.Count > 0)
				{
					for (int num2 = 0; num2 < _playerParty[num].characterDefinition.transformations.Count; num2++)
					{
						if (!characterCounts.ContainsKey(_playerParty[num].characterDefinition.transformations[num2].battleCharacterPath))
						{
							characterCounts.Add(_playerParty[num].characterDefinition.transformations[num2].battleCharacterPath, 1);
						}
					}
				}
			}
			if (num >= enemyTeam.Count)
			{
				continue;
			}
			_enemyParty.Add(enemyTeam[num]);
			if (GameManager.instance.gameSettings.easyBattles)
			{
				_enemyParty[num].hitPoints = 1;
			}
			else
			{
				_enemyParty[num].hitPoints = _enemyParty[num].maxHitPoints;
			}
			if (_enemyParty[num].modChip != null)
			{
				_enemyParty[num].modChipSlot.PrepareChipForBattle(this, enemyActionMeter);
			}
			bcPath = ((!string.IsNullOrEmpty(_enemyParty[num].characterDefinition.battleCharacterPathP2)) ? _enemyParty[num].characterDefinition.battleCharacterPathP2 : _enemyParty[num].characterDefinition.battleCharacterPath);
			if (characterCounts.ContainsKey(bcPath))
			{
				characterCounts[bcPath]++;
			}
			else
			{
				characterCounts.Add(bcPath, 1);
			}
			if (_enemyParty[num].characterDefinition.transformations.Count <= 0)
			{
				continue;
			}
			for (int num3 = 0; num3 < _enemyParty[num].characterDefinition.transformations.Count; num3++)
			{
				bcPath = ((!string.IsNullOrEmpty(_enemyParty[num].characterDefinition.transformations[num3].battleCharacterPathP2)) ? _enemyParty[num].characterDefinition.transformations[num3].battleCharacterPathP2 : _enemyParty[num].characterDefinition.transformations[num3].battleCharacterPath);
				if (!characterCounts.ContainsKey(bcPath))
				{
					characterCounts.Add(bcPath, 1);
				}
			}
		}
		if (battleDefinition != null && battleDefinition.startingAttackBonus > 0)
		{
			StatusEffectManager.instance.AddActionMeterStatusEffect(playerActionMeter, new TeamStatBuff(playerActionMeter, CharacterStatType.Attack, battleDefinition.startingAttackBonus));
		}
		foreach (string key in characterCounts.Keys)
		{
			ObjectPool.instance.AddType(key, characterCounts[key]);
		}
		_playerCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(_playerParty[0].characterDefinition.battleCharacterPath));
		_playerCharacter.character = _playerParty[0];
		_playerCharacter.character.DeathCharacter += HandleOnDeathCharacter;
		_playerCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
		RepaintBattleCharacter(_playerCharacter);
		playerBattleCamera.offset.localPosition = _playerCharacter.characterDefinition.playerCameraOffsetPosition;
		playerBattleCamera.offset.localRotation = Quaternion.Euler(_playerCharacter.characterDefinition.playerCameraOffsetRotation);
		playerBattleCamera.character = _playerCharacter.character;
		bcPath = ((!string.IsNullOrEmpty(_enemyParty[0].characterDefinition.battleCharacterPathP2)) ? _enemyParty[0].characterDefinition.battleCharacterPathP2 : _enemyParty[0].characterDefinition.battleCharacterPath);
		_enemyCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(bcPath));
		_enemyCharacter.character = _enemyParty[0];
		_enemyCharacter.character.DeathCharacter += HandleOnDeathCharacter;
		_enemyCharacter.transform.localScale = new Vector3(1f, 1f, -1f);
		RepaintBattleCharacter(_enemyCharacter);
		enemyBattleCamera.animator.runtimeAnimatorController = _enemyCharacter.cameraController;
		enemyBattleCamera.animator.avatar = _enemyCharacter.cameraAvatar;
		enemyBattleCamera.offset.localPosition = _enemyCharacter.characterDefinition.enemyCameraOffsetPosition;
		enemyBattleCamera.offset.localRotation = Quaternion.Euler(_enemyCharacter.characterDefinition.enemyCameraOffsetRotation);
		enemyBattleCamera.character = _enemyCharacter.character;
		len = playerActionMeter.abilityButtons.Count;
		for (int num4 = 0; num4 < len; num4++)
		{
			playerActionMeter.abilityButtons[num4].LongPressed += HandleLongPressed;
		}
		PlayerAI ai = null;
		if (battleDefinition.playerAIType == PlayerAIType.Default)
		{
			ai = enemyActionMeter.gameObject.AddComponent<PlayerAI>();
		}
		else if (battleDefinition.playerAIType == PlayerAIType.HoodedHood)
		{
			ai = enemyActionMeter.gameObject.AddComponent<PlayerAI_HoodedHood>();
		}
		else if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			ai = enemyActionMeter.gameObject.AddComponent<PlayerAI_Network>();
		}
		else
		{
			ai = enemyActionMeter.gameObject.AddComponent<PlayerAI>();
		}
		playerActionMeter.gameObject.SetActive(value: false);
		enemyActionMeter.gameObject.SetActive(value: false);
		playerPortraits.gameObject.SetActive(value: false);
		enemyPortraits.gameObject.SetActive(value: false);
		playerAccessoryButton.Init(battleDefinition.accessoryUID, isPlayerOwned: true);
		playerAccessoryButton.LongPressed += HandleAccessoryLongPressed;
		if (playerAccessoryButton.accessory != null)
		{
			playerAccessoryButton.accessory.Activated += HandleAccessoryActivated;
			playerAccessoryButton.accessory.IntroFinished += HandleAccessoryIntroFinished;
		}
		enemyAccessoryButton.Init(battleDefinition.enemyAccessoryUID, isPlayerOwned: false);
		if (enemyAccessoryButton.accessory != null)
		{
			enemyAccessoryButton.accessory.Activated += HandleAccessoryActivated;
			enemyAccessoryButton.accessory.IntroFinished += HandleAccessoryIntroFinished;
		}
		diminishingReturnsUtil = new DiminishingReturns(this);
		pvpEnemyPlayerName.gameObject.SetActive(battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend);
		pvpEnemyPlayerName.color = Color.white;
		playerEmoteUI.SetActive(battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend);
		enemyEmoteUI.SetActive(battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend);
		if (battleDefinition.IsLODBattle)
		{
			Player.instance.pvpData.isInPvpBattle = true;
			Player.instance.pvpData.isInPvpBattleType = battleDefinition.type;
			Player.instance.Save();
		}
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendUpdateBattleLoaded(isLoaded: true);
			TT2NetPlayer player1 = TT2NetManager.Instance.myNetworkPlayer;
			TT2NetPlayer player2 = TT2NetManager.Instance.otherNetworkPlayer;
			BattleObjectManager.instance.SpawnedTofu += HandleSpawnedTofu;
			pvpEnemyPlayerName.text = player2.PlayerName;
			TT2NetManager.Instance.OnPlayerDisconnectedEvent += HandlePlayerDisconnected;
			TT2NetManager.Instance.DisconnectedFromNetworkEvent += HandleDisconnectedFromNetworkEvent;
			yield return new WaitUntil(() => player1.battleLoaded && player2.battleLoaded);
		}
		else if (battleDefinition.type == BattleType.PvpCpu)
		{
			pvpEnemyPlayerName.text = battleDefinition.enemyName;
		}
		else
		{
			pvpEnemyPlayerName.text = string.Empty;
		}
		StartCoroutine(BattleIntro());
	}

	private void HandlePlayerDisconnected(Photon.Realtime.Player player)
	{
		pvpEnemyPlayerName.color = Color.gray;
	}

	private void HandleDisconnectedFromNetworkEvent()
	{
		pvpEnemyPlayerName.color = Color.gray;
	}

	private void HandleSpawnedTofu()
	{
		TT2NetManager.SendTofuSpawned();
	}

	private void RepaintBattleCharacter(BattleCharacter battleCharacter)
	{
		if (battleCharacter.character.repaintColorID > 0)
		{
			battleCharacter.RepaintCharacter(battleCharacter.character.repaintColorID);
		}
	}

	private void OnDestroy()
	{
		if (pauseScreen != null)
		{
			pauseScreen.HomeClicked -= HandleOnHomeClicked;
		}
		if (playerAccessoryButton != null)
		{
			playerAccessoryButton.LongPressed -= HandleAccessoryLongPressed;
		}
		if (BattleObjectManager.instance != null)
		{
			BattleObjectManager.instance.SpawnedTofu -= HandleSpawnedTofu;
		}
		BattleIntroStarted = null;
		BattleStarted = null;
		BattleEnded = null;
		CharacterSwappedIn = null;
		CharacterSwappedOut = null;
		WaitingStarted = null;
		WaitingEnded = null;
		ChallengeModeManager.isChallengeModeActive = false;
		if (TT2NetManager.Instance != null)
		{
			TT2NetManager.Instance.OnPlayerDisconnectedEvent -= HandlePlayerDisconnected;
			TT2NetManager.Instance.DisconnectedFromNetworkEvent -= HandleDisconnectedFromNetworkEvent;
		}
		if (Player.instance != null)
		{
			Player.instance.pvpData.BattleRequestAccepted -= HandleBattleRequestAccepted;
		}
		instance = null;
	}

	private void HandleAccessoryLongPressed()
	{
		if (!_isViewingAbilityDescriptions && !ignoreLongPress)
		{
			_isViewingAbilityDescriptions = true;
			if (battleDefinition.playerAIType != PlayerAIType.Network)
			{
				timeScaleWhenAbilityPaused = Time.timeScale;
				Time.timeScale = 0f;
				DOTween.Pause("deathTime");
				GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
			}
			HandleCharacterSelectModeEntered();
			blackFade.FadeTo(0.6f, 0.2f);
			accessoryInfoBox = ObjectPool.GetObjectFromResource<AccessoryInfoBox>("Prefabs/GUI/Battle/AccessoryInfoBox");
			accessoryInfoBox.Init(playerAccessoryButton.itemDef);
			accessoryInfoBox.transform.parent = playerAccessoryButton.transform;
			accessoryInfoBox.transform.localPosition = new Vector3(0f, 0f, 3f);
			playerAccessoryButton.touchCollider.enabled = false;
			Vector3 position = (originalAccessoryPosition = playerAccessoryButton.transform.position);
			position.z = blackFade.transform.position.z - 5f;
			playerAccessoryButton.transform.position = position;
			blackFade.uiItem.OnDown += HandleOnBlackFadeDownAccessory;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void HandleOnBlackFadeDownAccessory()
	{
		Debug.Log("Handle Accessory Black Fade Out");
		blackFade.uiItem.OnDown -= HandleOnBlackFadeDownAccessory;
		blackFade.FadeTo(0f, 0.2f);
		if (battleDefinition.playerAIType != PlayerAIType.Network)
		{
			if (DOTween.Play("deathTime") > 0)
			{
				Time.timeScale = timeScaleWhenAbilityPaused;
			}
			else
			{
				Time.timeScale = 1f;
			}
			GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
		}
		playerAccessoryButton.touchCollider.enabled = true;
		playerAccessoryButton.transform.position = originalAccessoryPosition;
		ObjectPool.PoolObject(accessoryInfoBox.gameObject);
		HandleCharacterSelectModeExited();
		_isViewingAbilityDescriptions = false;
	}

	private void HandleLongPressed(AbilityButton button)
	{
		if (_isViewingAbilityDescriptions || ignoreLongPress)
		{
			return;
		}
		_isViewingAbilityDescriptions = true;
		if (battleDefinition.playerAIType != PlayerAIType.Network)
		{
			timeScaleWhenAbilityPaused = Time.timeScale;
			Time.timeScale = 0f;
			DOTween.Pause("deathTime");
			GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		}
		HandleCharacterSelectModeEntered();
		blackFade.FadeTo(0.6f, 0.2f);
		Vector3 position = playerActionMeter.buttonContainer.position;
		position.z = blackFade.transform.position.z - 5f;
		playerActionMeter.buttonContainer.position = position;
		playerActionMeter.isEnabled = false;
		int count = playerActionMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			AbilityButton tempButton = playerActionMeter.abilityButtons[i];
			tempButton.Pressed += HandlePressed;
			tempButton.buttonContainer.DOScale(tempButton.activatedScale, 0.25f).SetEase(Ease.OutBack).SetUpdate(isIndependentUpdate: true);
			DOTween.To(() => tempButton.backingSprite.color, (Color x) =>
			{
				tempButton.backingSprite.color = x;
			}, tempButton.activatedColor, 0.25f).SetEase(Ease.OutBack).SetId("activateBack" + i)
				.SetUpdate(isIndependentUpdate: true);
			DOTween.To(() => tempButton.iconSprite.color, (Color x) =>
			{
				tempButton.iconSprite.color = x;
			}, tempButton.activatedColor, 0.25f).SetEase(Ease.OutBack).SetId("activateIcon" + i)
				.SetUpdate(isIndependentUpdate: true);
		}
		infoBox = ObjectPool.GetObjectFromResource<AbilityInfoBox>("Prefabs/GUI/Battle/AbilityInfoBoxBattle");
		blackFade.uiItem.OnDown += HandleOnBlackFadeDown;
		HandlePressed(button);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		if (AbilityTooltipOpened != null)
		{
			AbilityTooltipOpened();
		}
	}

	private void HandlePressed(AbilityButton button)
	{
		if (selectedButton != null)
		{
			selectedButton.boxCollider2D.enabled = true;
		}
		selectedButton = button;
		selectedButton.boxCollider2D.enabled = false;
		infoBox.Init(button.abilityData, _playerCharacter.character);
		infoBox.transform.parent = button.buttonContainer;
		infoBox.transform.localPosition = new Vector3(0f, 0f, 2f);
		infoBox.transform.localScale = Vector3.one;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void HandleOnBlackFadeDown()
	{
		blackFade.uiItem.OnDown -= HandleOnBlackFadeDown;
		blackFade.FadeTo(0f, 0.2f);
		if (battleDefinition.playerAIType != PlayerAIType.Network)
		{
			if (DOTween.Play("deathTime") > 0)
			{
				Time.timeScale = timeScaleWhenAbilityPaused;
			}
			else
			{
				Time.timeScale = 1f;
			}
			GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
		}
		Vector3 position = playerActionMeter.buttonContainer.position;
		position.z = -100f;
		playerActionMeter.buttonContainer.position = position;
		playerActionMeter.isEnabled = true;
		int count = playerActionMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			AbilityButton tempButton = playerActionMeter.abilityButtons[i];
			tempButton.Pressed -= HandlePressed;
			if (!tempButton.isActivated)
			{
				tempButton.buttonContainer.DOScale(tempButton.deactivatedScale, 0.25f).SetEase(Ease.OutBack);
			}
			if (playerActionMeter.isPaused)
			{
				DOTween.To(() => tempButton.backingSprite.color, (Color x) =>
				{
					tempButton.backingSprite.color = x;
				}, playerActionMeter.disabledColor, 0.25f).SetId("deactivateBack" + i);
				DOTween.To(() => tempButton.iconSprite.color, (Color x) =>
				{
					tempButton.iconSprite.color = x;
				}, playerActionMeter.disabledColor, 0.25f).SetId("deactivateIcon" + i);
			}
			else if (!tempButton.isActivated)
			{
				DOTween.To(() => tempButton.backingSprite.color, (Color x) =>
				{
					tempButton.backingSprite.color = x;
				}, tempButton.deactivatedColor, 0.25f).SetId("deactivateBack" + i);
				DOTween.To(() => tempButton.iconSprite.color, (Color x) =>
				{
					tempButton.iconSprite.color = x;
				}, tempButton.deactivatedColor, 0.25f).SetId("deactivateIcon" + i);
			}
		}
		selectedButton.boxCollider2D.enabled = true;
		selectedButton = null;
		ObjectPool.PoolObject(infoBox.gameObject);
		HandleCharacterSelectModeExited();
		_isViewingAbilityDescriptions = false;
		if (AbilityTooltipClosed != null)
		{
			AbilityTooltipClosed();
		}
	}

	private void OnEnable()
	{
		playerActionMeter.CastStarted += HandleCastStarted;
		playerActionMeter.AbilityUsed += HandleAbilityUsed;
		playerActionMeter.AbilityFinished += HandleAbilityFinished;
		enemyActionMeter.CastStarted += HandleCastStarted;
		enemyActionMeter.AbilityUsed += HandleAbilityUsed;
		enemyActionMeter.AbilityFinished += HandleAbilityFinished;
		playerPortraits.CharacterChanged += HandlePlayerCharacterChanged;
		playerPortraits.SelectModeEntered += HandleCharacterSelectModeEntered;
		playerPortraits.SelectModeExited += HandleCharacterSelectModeExited;
		playerPortraits.DragModeEntered += HandleCharacterSelectModeEntered;
		playerPortraits.DragModeExited += HandleCharacterSelectModeExited;
		enemyPortraits.CharacterChanged += HandleEnemyCharacterChanged;
		batteryGUI.BatteryModeEntered += HandleBatteryModeEntered;
		batteryGUI.BatteryModeExited += HandleBatteryModeExited;
		PauseManager.instance.Paused += HandlePaused;
	}

	private void OnDisable()
	{
		playerActionMeter.CastStarted -= HandleCastStarted;
		playerActionMeter.AbilityUsed -= HandleAbilityUsed;
		playerActionMeter.AbilityFinished -= HandleAbilityFinished;
		enemyActionMeter.CastStarted -= HandleCastStarted;
		enemyActionMeter.AbilityUsed -= HandleAbilityUsed;
		enemyActionMeter.AbilityFinished -= HandleAbilityFinished;
		playerPortraits.CharacterChanged -= HandlePlayerCharacterChanged;
		playerPortraits.SelectModeEntered -= HandleCharacterSelectModeEntered;
		playerPortraits.SelectModeExited -= HandleCharacterSelectModeExited;
		playerPortraits.DragModeEntered -= HandleCharacterSelectModeEntered;
		playerPortraits.DragModeExited -= HandleCharacterSelectModeExited;
		enemyPortraits.CharacterChanged -= HandleEnemyCharacterChanged;
		batteryGUI.BatteryModeEntered -= HandleBatteryModeEntered;
		batteryGUI.BatteryModeExited -= HandleBatteryModeExited;
		PauseManager.instance.Paused -= HandlePaused;
		pauseScreen.HomeClicked -= HandleOnHomeClicked;
		if (_battleTheme != null)
		{
			GFAudioManager.instance.StopByAudioSource(_battleTheme);
		}
	}

	private IEnumerator BattleIntro()
	{
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(isPlaying: true, RpcTarget.All);
		}
		OnBattleIntroStarted();
		if (_arenaData != null && !string.IsNullOrEmpty(_arenaData.battleThemePath))
		{
			string text = _arenaData.battleThemePath;
			if (text.Contains("nbts"))
			{
				string[] array = new string[1] { string.Empty };
				text += array[UnityEngine.Random.Range(0, array.Length)];
			}
			_battleThemeVolume = _arenaData.battleThemeVolume;
			_battleTheme = GFAudioManager.instance.PlayClipFromResources(text, GFAudioManager.MUSIC_GROUP, "battleTheme", _battleThemeVolume, 0f, loop: true);
		}
		else
		{
			_battleThemeVolume = 1f;
			_battleTheme = GFAudioManager.instance.PlayClipFromResources(battleDefinition.battleThemePath, GFAudioManager.MUSIC_GROUP, "battleTheme", _battleThemeVolume, 0f, loop: true);
		}
		battleCountdown.allowBonusTapping = false;
		if (battleDefinition.countdownCharacterUID > -1)
		{
			battleCountdown.Init(GameManager.instance.gameSettings.GetSoundSetForCharacter(battleDefinition.countdownCharacterUID));
		}
		else
		{
			battleCountdown.Init(GameManager.instance.gameSettings.GetRandomCountdownSoundSet());
		}
		playerBattleCamera.animator.runtimeAnimatorController = battleSpawnAnimController;
		AnimationEvent evt = new AnimationEvent
		{
			stringParameter = "fullscreen",
			intParameter = 0
		};
		playerBattleCamera.OffsetIn(evt);
		playerBattleCamera.animatorEventListener.EventTriggered += HandleIntroCameraEventTriggered;
		benchedPlayerCharacters = new List<BattleCharacter>();
		BattleCharacter tempCharacter = null;
		for (int i = 1; i < _playerParty.Count; i++)
		{
			tempCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(_playerParty[i].characterDefinition.battleCharacterPath));
			tempCharacter.transform.localScale = _playerCharacter.transform.localScale;
			tempCharacter.transform.localPosition = _playerCharacter.transform.localPosition + ((i != 1) ? thirdCharacterOffset : secondCharacterOffset);
			tempCharacter.sharedMat.SetColor("_TintColor", Color.white);
			if (tempCharacter.sharedFaceMat != null)
			{
				tempCharacter.sharedFaceMat.SetColor("_TintColor", Color.white);
			}
			tempCharacter.gameObject.SetActive(value: false);
			tempCharacter.character = _playerParty[i];
			RepaintBattleCharacter(tempCharacter);
			benchedPlayerCharacters.Add(tempCharacter);
		}
		benchedEnemyCharacters = new List<BattleCharacter>();
		for (int j = 1; j < _enemyParty.Count; j++)
		{
			if (!(_enemyParty[j].characterDefinition.characterName == "Shadow Fig"))
			{
				string path = ((!string.IsNullOrEmpty(_enemyParty[j].characterDefinition.battleCharacterPathP2)) ? _enemyParty[j].characterDefinition.battleCharacterPathP2 : _enemyParty[j].characterDefinition.battleCharacterPath);
				tempCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(path));
				tempCharacter.transform.localScale = _enemyCharacter.transform.localScale;
				tempCharacter.transform.localPosition = _playerCharacter.transform.localPosition + ((j != 1) ? (thirdCharacterOffset * -1f) : (secondCharacterOffset * -1f));
				tempCharacter.Play("idle", UnityEngine.Random.value);
				tempCharacter.straightLookWeight = 1f;
				tempCharacter.character = _enemyParty[j];
				RepaintBattleCharacter(tempCharacter);
				benchedEnemyCharacters.Add(tempCharacter);
			}
		}
		_enemyCharacter.straightLookWeight = 1f;
		_playerCharacter.sharedMat.SetColor("_TintColor", Color.white);
		if (_playerCharacter.sharedFaceMat != null)
		{
			_playerCharacter.sharedFaceMat.SetColor("_TintColor", Color.white);
		}
		_playerCharacter.gameObject.SetActive(value: false);
		StartCoroutine(CharacterSpawnSequence(_playerCharacter, benchedPlayerCharacters.Count == 0));
		yield return new WaitForSeconds(spawnStaggerDelay);
		for (int k = 0; k < benchedPlayerCharacters.Count; k++)
		{
			StartCoroutine(CharacterSpawnSequence(benchedPlayerCharacters[k], k == benchedPlayerCharacters.Count - 1));
			yield return new WaitForSeconds(spawnStaggerDelay);
		}
		while (!introCameraComplete)
		{
			yield return null;
		}
		playerBattleCamera.animatorEventListener.EventTriggered -= HandleIntroCameraEventTriggered;
		if (battleCountdown.isBonusTap)
		{
			float endValue = Mathf.Clamp01(GameManager.instance.gameSettings.goTapBonus * goTapBonusMultiplier);
			DOTween.To(() => playerActionMeter.progress, (float x) =>
			{
				playerActionMeter.progress = x;
			}, endValue, 0.25f).SetEase(Ease.Linear);
		}
		yield return new WaitForSeconds(0.25f);
		if (_playerParty.Count >= 2 && !useFlashbackBattleEnding)
		{
			Player.instance.SetTrigger("Has2PlusCharacters");
		}
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(isPlaying: false);
			yield return new WaitWhile(() => TT2NetManager.Instance.IsPlayerPlayingAction);
		}
		playerActionMeter.Resume();
		enemyActionMeter.Resume();
		_battleStarted = true;
		OnBattleStarted();
		TrackSwapIn(_playerCharacter.character);
		OnCharacterSwappedIn(_playerCharacter.character);
		TrackSwapIn(_enemyCharacter.character);
		OnCharacterSwappedIn(_enemyCharacter.character);
	}

	private IEnumerator CharacterSpawnSequence(BattleCharacter character, bool isLast = false)
	{
		Transform particles = ObjectPool.GetObject("Spawn_ParticleSystem").transform;
		Vector3 newPosition = character.top.position;
		newPosition.y = 0f;
		particles.position = newPosition;
		yield return new WaitForSeconds(explosionDelay);
		GFAudioManager.instance.PlayClipFromResources((!isLast) ? "Audio/SFX/sfx_battle_intro_player" : "Audio/SFX/sfx_battle_intro_player_last", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.25f);
		character.gameObject.SetActive(value: true);
		character.straightLookWeight = 1f;
		playerBattleCamera.camera.transform.DOShakePosition(0.35f, new Vector3(0.25f, 0.25f, 0.25f), 35, 70f);
		Sequence seq = DOTween.Sequence();
		seq.Insert(0f, character.sharedMat.DOFade(0f, "_TintColor", fadeDuration).SetDelay(fadeStartDelay).SetEase(fadeEaseFunction));
		if (character.sharedFaceMat != null)
		{
			seq.Insert(0f, character.sharedFaceMat.DOFade(0f, "_TintColor", fadeDuration).SetDelay(fadeStartDelay).SetEase(fadeEaseFunction));
		}
		seq.Play();
		yield return seq.WaitForCompletion();
	}

	private IEnumerator CharacterSpawnExitSequence(BattleCharacter character)
	{
		character.Play("swap out");
		yield return null;
		yield return null;
		while (character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		ObjectPool.PoolObject(character.gameObject);
	}

	private void HandleIntroCameraEventTriggered(AnimationEvent evt)
	{
		switch (evt.stringParameter)
		{
		case "startSpawnSound":
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_intro_main", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.25f);
			break;
		case "split":
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.intParameter = evt.intParameter;
			playerBattleCamera.OffsetOut(animationEvent);
			for (int i = 0; i < 2; i++)
			{
				if (i < benchedPlayerCharacters.Count)
				{
					StartCoroutine(CharacterSpawnExitSequence(benchedPlayerCharacters[i]));
				}
				if (i < benchedEnemyCharacters.Count)
				{
					StartCoroutine(CharacterSpawnExitSequence(benchedEnemyCharacters[i]));
				}
			}
			float num = (float)evt.intParameter / 60f;
			DOTween.To(() => _playerCharacter.straightLookWeight, (float x) =>
			{
				_playerCharacter.straightLookWeight = x;
			}, 0f, num);
			DOTween.To(() => _enemyCharacter.straightLookWeight, (float x) =>
			{
				_enemyCharacter.straightLookWeight = x;
			}, 0f, num);
			playerPortraits.gameObject.SetActive(value: true);
			playerPortraits.Init(_playerParty);
			playerPortraits.RefreshClassBonusIcons(enemyCharacter.characterDefinition.characterClass);
			enemyPortraits.gameObject.SetActive(value: true);
			enemyPortraits.Init(_enemyParty);
			enemyPortraits.RefreshClassBonusIcons(playerCharacter.characterDefinition.characterClass);
			playerActionMeter.gameObject.SetActive(value: true);
			playerActionMeter.character = _playerCharacter;
			playerActionMeter.Pause();
			enemyActionMeter.gameObject.SetActive(value: true);
			enemyActionMeter.character = enemyCharacter;
			enemyActionMeter.Pause();
			batteryGUI.gameObject.SetActive(value: true);
			playerActionMeter.transform.DOLocalMoveX(-300f, num * 1.25f).From().SetRelative()
				.SetEase(Ease.OutBack);
			playerPortraits.transform.DOLocalMoveX(-300f, num * 1.25f).From().SetRelative()
				.SetEase(Ease.OutBack);
			enemyActionMeter.transform.DOLocalMoveX(300f, num * 1.25f).From().SetRelative()
				.SetEase(Ease.OutBack);
			enemyPortraits.transform.DOLocalMoveX(300f, num * 1.25f).From().SetRelative()
				.SetEase(Ease.OutBack);
			batteryGUI.transform.DOLocalMoveX(-45f, num * 1.25f).From().SetRelative()
				.SetEase(Ease.OutBack);
			break;
		}
		case "countdown":
			battleCountdown.ShowNumber(evt.intParameter);
			break;
		case "go":
			battleCountdown.ShowGo();
			if (battleDefinition != null && (battleDefinition.type == BattleType.Challenge || ChallengeModeManager.isNonJLChallenge))
			{
				ChallengeModeManager.instance.Init();
			}
			break;
		case "complete":
			playerBattleCamera.animator.runtimeAnimatorController = _playerCharacter.cameraController;
			playerBattleCamera.animator.avatar = _playerCharacter.cameraAvatar;
			playerBattleCamera.animator.Update(0f);
			introCameraComplete = true;
			break;
		}
	}

	public void UnPause()
	{
		if (pauseScreen != null)
		{
			pauseScreen.HandleOnPlayClick();
		}
	}

	private void HandlePaused(bool pauseStatus)
	{
		if (pauseStatus)
		{
			if (battleDefinition == null || battleDefinition.playerAIType != PlayerAIType.Network)
			{
				timeScaleWhenPaused = Time.timeScale;
				Time.timeScale = 0f;
				DOTween.Pause("deathTime");
			}
			pauseScreen.gameObject.SetActive(value: true);
			GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 0.25f);
			GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
			GFAudioManager.instance.PauseGroup(GFAudioManager.VOICEOVER_GROUP);
			if (_battleTheme != null)
			{
				_battleTheme.volume = 0.25f;
			}
			return;
		}
		if (_battleEnded || (!playerPortraits.IsSelectMode && !enemyPortraits.IsSelectMode && !batteryGUI.isUsingBattery))
		{
			GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 1f);
			if (_battleTheme != null)
			{
				_battleTheme.volume = _battleThemeVolume;
			}
		}
		if (!_battleEnded && (_isViewingAbilityDescriptions || playerPortraits.IsSelectMode || enemyPortraits.IsSelectMode || batteryGUI.isUsingBattery))
		{
			return;
		}
		GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
		GFAudioManager.instance.UnPauseGroup(GFAudioManager.VOICEOVER_GROUP);
		if (battleDefinition == null || battleDefinition.playerAIType != PlayerAIType.Network)
		{
			if (DOTween.Play("deathTime") > 0)
			{
				Time.timeScale = timeScaleWhenPaused;
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
	}

	private void HandleOnHomeClicked()
	{
		pauseScreen.HomeClicked -= HandleOnHomeClicked;
		if (battleDefinition != null && battleDefinition.IsLODBattle && Player.instance.pvpData.isInPvpBattle)
		{
			if (Player.instance.pvpData.isInPvpBattleType == BattleType.Pvp)
			{
				Player.instance.AddSkillRating(-GameManager.instance.legionOfDoomSettings.lossSkillRating, updatePrevRating: true);
				Player.instance.pvpData.currentWinStreak = 0;
				Player.instance.pvpData.lastMatchIsLoss = true;
			}
			Player.instance.pvpData.isInPvpBattle = false;
		}
		if (battleDefinition != null && (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu) && (battleResult == null || !battleResult.isVictory))
		{
			Player.instance.pvpData.currentWinStreak = 0;
			Player.instance.pvpData.lastMatchIsLoss = true;
		}
		_battleEnded = true;
		OnBattleEnded();
		ResetBattleStuff();
		if (TT2NetManager.Instance != null)
		{
			TT2NetManager.Instance.OnPlayerDisconnectedEvent -= HandlePlayerDisconnected;
			TT2NetManager.Instance.DisconnectedFromNetworkEvent -= HandleDisconnectedFromNetworkEvent;
		}
		battleDefinition = null;
	}

	private void HandleBatteryModeEntered()
	{
		GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 0.25f);
		GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		GFAudioManager.instance.PauseGroup(GFAudioManager.VOICEOVER_GROUP);
		if (_battleTheme != null)
		{
			_battleTheme.volume = 0.25f;
		}
		ignoreLongPress = true;
	}

	private void HandleBatteryModeExited()
	{
		if (!PauseManager.instance.isPaused)
		{
			GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 1f);
			GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
			GFAudioManager.instance.UnPauseGroup(GFAudioManager.VOICEOVER_GROUP);
			if (_battleTheme != null)
			{
				_battleTheme.volume = _battleThemeVolume;
			}
			ignoreLongPress = false;
		}
	}

	private void HandlePlayerCharacterUpdated()
	{
		playerActionMeter.ForceUpdateButtons();
	}

	private void HandlePlayerCharacterChanged(Character character)
	{
		StartCoroutine(PlayCharacterSwap(character, isPlayer: true));
	}

	private void HandleCharacterSelectModeEntered()
	{
		GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 0.25f);
		GFAudioManager.instance.PauseGroup(GFAudioManager.SFX_GROUP);
		GFAudioManager.instance.PauseGroup(GFAudioManager.VOICEOVER_GROUP);
		if (_battleTheme != null)
		{
			_battleTheme.volume = 0.25f;
		}
	}

	private void HandleCharacterSelectModeExited()
	{
		if (!PauseManager.instance.isPaused)
		{
			GFAudioManager.instance.SetGroupVolume(GFAudioManager.MUSIC_GROUP, 1f);
			GFAudioManager.instance.UnPauseGroup(GFAudioManager.SFX_GROUP);
			GFAudioManager.instance.UnPauseGroup(GFAudioManager.VOICEOVER_GROUP);
			if (_battleTheme != null)
			{
				_battleTheme.volume = _battleThemeVolume;
			}
		}
	}

	private void HandleEnemyCharacterChanged(Character character)
	{
		StartCoroutine(PlayCharacterSwap(character, isPlayer: false));
	}

	private IEnumerator PlayCharacterSwap(Character character, bool isPlayer)
	{
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(isPlaying: true, RpcTarget.All);
		}
		StartWaiting();
		if (isPlayer)
		{
			if (_playerCharacter.character.isDead)
			{
				yield return null;
				if (_playerCharacter.animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
				{
					bool deathComplete = false;
					Action<AnimationEvent> deathCompleteDelegate = null;
					deathCompleteDelegate = (AnimationEvent obj) =>
					{
						if (obj.stringParameter == "deathComplete")
						{
							deathComplete = true;
							_playerCharacter.animatorEventListener.EventTriggered -= deathCompleteDelegate;
						}
					};
					_playerCharacter.animatorEventListener.EventTriggered += deathCompleteDelegate;
					yield return new WaitWhile(() => !deathComplete);
					_playerCharacter.character.OnDeathComplete();
				}
			}
			else if (!_playerCharacter.character.isKnockedOut)
			{
				Flight flightEffect = _playerCharacter.character.GetStatusEffect<Flight>();
				if (flightEffect != null)
				{
					flightEffect.ForceEndFlightSquence();
					yield return new WaitWhile(() => _playerCharacter.character.HasStatusEffect<Flight>());
				}
				if (_playerCharacter.Play("swap out"))
				{
					if (_playerCharacter.characterDefinition.sounds.swapOutSounds.soundPaths.Count > 0 && UnityEngine.Random.value < _playerCharacter.characterDefinition.sounds.swapOutSounds.probability)
					{
						GFAudioManager.instance.PlayClipFromResources(_playerCharacter.characterDefinition.sounds.swapOutSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
					}
					OnCharacterSwapOutStart(_playerCharacter.character);
					bool swapOutComplete = false;
					Action<AnimationEvent> swapOutCompleteDelegate = null;
					swapOutCompleteDelegate = (AnimationEvent obj) =>
					{
						if (obj.stringParameter == "complete")
						{
							swapOutComplete = true;
							_playerCharacter.animatorEventListener.EventTriggered -= swapOutCompleteDelegate;
						}
					};
					_playerCharacter.animatorEventListener.EventTriggered += swapOutCompleteDelegate;
					yield return new WaitWhile(() => !swapOutComplete);
				}
			}
			OnCharacterSwappedOut(_playerCharacter.character);
			bool prevCharacterWasKilled = _playerCharacter.character.isDead;
			ReturnBattleCharacterToPool(_playerCharacter);
			_playerCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(character.battleCharacterPath));
			_playerCharacter.character = character;
			_playerCharacter.character.DeathCharacter += HandleOnDeathCharacter;
			_playerCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
			_playerCharacter.gameObject.SetActive(value: true);
			_playerCharacter.RepaintCharacter(character.repaintColorID);
			playerBattleCamera.animator.runtimeAnimatorController = _playerCharacter.cameraController;
			playerBattleCamera.animator.avatar = _playerCharacter.cameraAvatar;
			playerBattleCamera.character = _playerCharacter.character;
			playerBattleCamera.UpdateCameraOffsetPosition(0.5f);
			playerActionMeter.character = _playerCharacter;
			if (prevCharacterWasKilled || GameManager.instance.gameSettings.resetActionMeterOnSwitch)
			{
				playerActionMeter.progress = 0f;
			}
			enemyPortraits.RefreshClassBonusIcons(character.characterClass);
			if (character.characterDefinition.sounds.swapInSounds.soundPaths.Count > 0 && UnityEngine.Random.value < character.characterDefinition.sounds.swapInSounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(character.characterDefinition.sounds.swapInSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty, 1f, 0.75f);
			}
			if (_playerCharacter.Play("swap in"))
			{
				OnCharacterSwapInStart(character);
				bool swapOutComplete2 = false;
				_playerCharacter.animatorEventListener.EventTriggered += (AnimationEvent obj) =>
				{
					if (obj.stringParameter == "complete")
					{
						swapOutComplete2 = true;
					}
				};
				yield return new WaitWhile(() => !swapOutComplete2);
			}
			else
			{
				yield return new WaitForSeconds(0.5f);
			}
		}
		else
		{
			if (_enemyCharacter.character.isDead)
			{
				yield return null;
				if (_enemyCharacter.animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
				{
					bool deathComplete2 = false;
					Action<AnimationEvent> deathCompleteDelegate2 = null;
					deathCompleteDelegate2 = (AnimationEvent obj) =>
					{
						if (obj.stringParameter == "deathComplete")
						{
							deathComplete2 = true;
							_enemyCharacter.animatorEventListener.EventTriggered -= deathCompleteDelegate2;
						}
					};
					_enemyCharacter.animatorEventListener.EventTriggered += deathCompleteDelegate2;
					yield return new WaitWhile(() => !deathComplete2);
					_enemyCharacter.character.OnDeathComplete();
				}
			}
			else if (!_enemyCharacter.character.isKnockedOut)
			{
				if (_enemyCharacter.character.HasStatusEffect<Flight>())
				{
					_enemyCharacter.character.GetStatusEffect<Flight>().ForceEndFlightSquence();
					yield return new WaitWhile(() => _enemyCharacter.character.HasStatusEffect<Flight>());
				}
				if (_enemyCharacter.Play("swap out"))
				{
					if (_enemyCharacter.characterDefinition.sounds.swapOutSounds.soundPaths.Count > 0 && UnityEngine.Random.value < _enemyCharacter.characterDefinition.sounds.swapOutSounds.probability)
					{
						GFAudioManager.instance.PlayClipFromResources(_enemyCharacter.characterDefinition.sounds.swapOutSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
					}
					OnCharacterSwapOutStart(_enemyCharacter.character);
					bool swapOutComplete3 = false;
					Action<AnimationEvent> swapOutCompleteDelegate2 = null;
					swapOutCompleteDelegate2 = (AnimationEvent obj) =>
					{
						if (obj.stringParameter == "complete")
						{
							swapOutComplete3 = true;
							_enemyCharacter.animatorEventListener.EventTriggered -= swapOutCompleteDelegate2;
						}
					};
					_enemyCharacter.animatorEventListener.EventTriggered += swapOutCompleteDelegate2;
					yield return new WaitWhile(() => !swapOutComplete3);
				}
			}
			OnCharacterSwappedOut(_enemyCharacter.character);
			bool prevCharacterWasKilled2 = _enemyCharacter.character.isDead;
			Character prevCharacter = _enemyCharacter.character;
			ReturnBattleCharacterToPool(_enemyCharacter);
			string[] forms = new string[4] { "default", "starfire", "cyborg", "raven" };
			if (character.characterDefinition.characterName == "Shadow Fig")
			{
				string id = forms[UnityEngine.Random.Range(0, forms.Length)];
				character.Transform(id);
			}
			string bcPath = string.Empty;
			if (character.activeTransformation != null)
			{
				bcPath = ((!string.IsNullOrEmpty(character.activeTransformation.battleCharacterPathP2)) ? character.activeTransformation.battleCharacterPathP2 : character.activeTransformation.battleCharacterPath);
			}
			else
			{
				bcPath = ((!string.IsNullOrEmpty(character.characterDefinition.battleCharacterPathP2)) ? character.characterDefinition.battleCharacterPathP2 : character.characterDefinition.battleCharacterPath);
			}
			_enemyCharacter = ObjectPool.GetObject<BattleCharacter>(Path.GetFileNameWithoutExtension(bcPath));
			_enemyCharacter.character = character;
			_enemyCharacter.character.DeathCharacter += HandleOnDeathCharacter;
			_enemyCharacter.transform.localScale = new Vector3(1f, 1f, -1f);
			_enemyCharacter.gameObject.SetActive(value: true);
			_enemyCharacter.RepaintCharacter(character.repaintColorID);
			enemyBattleCamera.animator.runtimeAnimatorController = _enemyCharacter.cameraController;
			enemyBattleCamera.animator.avatar = _enemyCharacter.cameraAvatar;
			enemyBattleCamera.character = _enemyCharacter.character;
			enemyBattleCamera.UpdateCameraOffsetPosition(0.5f);
			enemyActionMeter.character = _enemyCharacter;
			if (prevCharacterWasKilled2 || GameManager.instance.gameSettings.resetActionMeterOnSwitch)
			{
				enemyActionMeter.progress = 0f;
			}
			playerPortraits.RefreshClassBonusIcons(character.characterClass);
			playerActionMeter.ForceUpdateButtons();
			if (character.characterDefinition.sounds.swapInSounds.soundPaths.Count > 0 && UnityEngine.Random.value < character.characterDefinition.sounds.swapInSounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(character.characterDefinition.sounds.swapInSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty, 1f, 0.75f);
			}
			if (_enemyCharacter.Play("swap in"))
			{
				OnCharacterSwapInStart(character);
				bool swapOutComplete4 = false;
				_enemyCharacter.animatorEventListener.EventTriggered += (AnimationEvent obj) =>
				{
					if (obj.stringParameter == "complete")
					{
						swapOutComplete4 = true;
					}
				};
				yield return new WaitWhile(() => !swapOutComplete4);
			}
			else
			{
				yield return new WaitForSeconds(0.5f);
			}
			if (prevCharacter != null && prevCharacter.characterDefinition.characterName == "Shadow Fig")
			{
				prevCharacter.hitPoints = prevCharacter.maxHitPoints;
				prevCharacter.RemoveAllStatusEffects();
			}
		}
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(isPlaying: false);
			yield return new WaitWhile(() => TT2NetManager.Instance.IsPlayerPlayingAction);
		}
		EndWaiting();
		TrackSwapIn(character);
		OnCharacterSwappedIn(character);
	}

	private void HandleAccessoryActivated(bool isPlayer)
	{
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(isPlaying: true, RpcTarget.All);
		}
		StartWaiting();
		BattleCamera battleCamera = ((!isPlayer) ? enemyBattleCamera : playerBattleCamera);
		previousCameraStateHash = battleCamera.animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
		battleCamera.animator.runtimeAnimatorController = accessoryIntroAnimController;
		battleCamera.Play("camera in");
	}

	private void HandleAccessoryIntroFinished(bool isPlayer)
	{
		StartCoroutine(FinishAccessoryIntroSequence(isPlayer));
	}

	private IEnumerator FinishAccessoryIntroSequence(bool isPlayer)
	{
		BattleCamera battleCamera = ((!isPlayer) ? enemyBattleCamera : playerBattleCamera);
		battleCamera.animator.Play("camera out");
		yield return null;
		yield return null;
		while (battleCamera.animator.IsPlaying())
		{
			yield return null;
		}
		battleCamera.animator.runtimeAnimatorController = ((!isPlayer) ? enemyCharacter.cameraController : playerCharacter.cameraController);
		battleCamera.animator.CrossFade(previousCameraStateHash, 0.125f);
		if (battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(isPlaying: false);
			yield return new WaitWhile(() => TT2NetManager.Instance.IsPlayerPlayingAction);
		}
		EndWaiting();
	}

	public void ForceStartWaiting(bool useDisabledVisuals = true)
	{
		StartWaiting(useDisabledVisuals);
	}

	public void ForceEndWaiting()
	{
		EndWaiting();
	}

	private void StartWaiting(bool useDisabledVisuals = true)
	{
		if (!_waiting)
		{
			Debug.Log("<color=green>Start Waiting</color>");
			playerActionMeter.Pause(useDisabledVisuals);
			enemyActionMeter.Pause(useDisabledVisuals);
			playerPortraits.UseDisabledVisuals = useDisabledVisuals;
			playerPortraits.isEnabled = false;
			enemyPortraits.UseDisabledVisuals = useDisabledVisuals;
			enemyPortraits.isEnabled = false;
			_waiting = true;
			if (WaitingStarted != null)
			{
				WaitingStarted();
			}
		}
	}

	private void EndWaiting()
	{
		if (_waiting)
		{
			Debug.Log("<color=red>End Waiting</color>");
			playerActionMeter.Resume();
			enemyActionMeter.Resume();
			playerPortraits.UseDisabledVisuals = true;
			playerPortraits.isEnabled = true;
			enemyPortraits.UseDisabledVisuals = true;
			enemyPortraits.isEnabled = true;
			_waiting = false;
			if (WaitingEnded != null)
			{
				WaitingEnded();
			}
		}
	}

	private void ReturnBattleCharacterToPool(BattleCharacter battleCharacter)
	{
		battleCharacter.character.DeathCharacter -= HandleOnDeathCharacter;
		battleCharacter.character = null;
		ObjectPool.PoolObject(battleCharacter.gameObject);
	}

	private void HandleOnDeathCharacter(Character character)
	{
		HandleDeathCharacter(character, fromEvent: true);
	}

	private void HandleDeathCharacter(Character character, bool fromEvent)
	{
		if (fromEvent && (playerActionMeter.isPaused || enemyActionMeter.isPaused) && waiting)
		{
			return;
		}
		if (character == _playerCharacter.character)
		{
			int num = _playerParty.IndexOf(_playerCharacter.character) + 1;
			if (num >= _playerParty.Count)
			{
				num = 0;
			}
			Character character2 = null;
			for (int i = 0; i < _playerParty.Count; i++)
			{
				if (!_playerParty[num].isDead)
				{
					character2 = _playerParty[num];
					break;
				}
				num++;
				if (num >= _playerParty.Count)
				{
					num = 0;
				}
			}
			if (character2 != null)
			{
				Vector3 position = playerBattleCamera.camera.WorldToScreenPoint(_playerCharacter.top.position);
				Vector3 vector = guiCamera.ScreenToWorldPoint(position);
				vector.z = -600f;
				int batteryDropOnPlayerDeath = GameManager.instance.gameSettings.batteryDropOnPlayerDeath;
				if (batteryDropOnPlayerDeath > 0)
				{
					float num2 = 60f;
					Vector3 vector2 = vector;
					vector2.x -= (float)(batteryDropOnPlayerDeath - 1) / 2f * num2;
					for (int j = 0; j < batteryDropOnPlayerDeath; j++)
					{
						BattleObjectManager.instance.SpawnBatteryAt(vector2 + new Vector3(num2 * (float)j, UnityEngine.Random.Range(-50, 50)), isPlayerBattery: true);
					}
				}
				playerPortraits.SelectCharacter(character2);
			}
			else
			{
				StartCoroutine(WaitForFinalDeath(_playerCharacter));
			}
		}
		else
		{
			if (character != _enemyCharacter.character)
			{
				return;
			}
			int num3 = _enemyParty.IndexOf(_enemyCharacter.character) + 1;
			if (num3 >= _enemyParty.Count)
			{
				num3 = 0;
			}
			Character character3 = null;
			for (int k = 0; k < _enemyParty.Count; k++)
			{
				if (!_enemyParty[num3].isDead)
				{
					character3 = _enemyParty[num3];
					break;
				}
				num3++;
				if (num3 >= _enemyParty.Count)
				{
					num3 = 0;
				}
			}
			if (character3 != null)
			{
				Vector3 position2 = enemyBattleCamera.camera.WorldToScreenPoint(_enemyCharacter.top.position);
				Vector3 vector3 = guiCamera.ScreenToWorldPoint(position2);
				vector3.z = -600f;
				int batteryDropOnPlayerDeath2 = GameManager.instance.gameSettings.batteryDropOnPlayerDeath;
				if (batteryDropOnPlayerDeath2 > 0)
				{
					float num4 = 60f;
					Vector3 vector4 = vector3;
					vector4.x -= (float)(batteryDropOnPlayerDeath2 - 1) / 2f * num4;
					for (int l = 0; l < batteryDropOnPlayerDeath2; l++)
					{
						BattleObjectManager.instance.SpawnBatteryAt(vector4 + new Vector3(num4 * (float)l, UnityEngine.Random.Range(-50, 50)), isPlayerBattery: false);
					}
				}
				enemyPortraits.SelectCharacter(character3);
			}
			else
			{
				StartCoroutine(WaitForFinalDeath(_enemyCharacter));
			}
		}
	}

	private IEnumerator WaitForFinalDeath(BattleCharacter battleCharacter)
	{
		StartWaiting();
		while (battleCharacter.animator.gameObject.activeSelf || Time.timeScale < 1f)
		{
			yield return null;
		}
		if (battleCharacter == _enemyCharacter)
		{
			ReturnBattleCharacterToPool(battleCharacter);
			battleResult = new BattleResult(victory: true);
			victoryScreen.Init(Player.instance, battleDefinition, useFlashbackBattleEnding ? VictoryScreen.Mode.Tutorial : VictoryScreen.Mode.Normal);
			victoryScreen.gameObject.SetActive(value: true);
			if (string.IsNullOrEmpty(_arenaData.battleThemePath) || !_arenaData.overrideResultsMusic)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_victory", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.15f);
				GFAudioManager.instance.PlayClipFromResources("Audio/Music/music_resultsupgrades", GFAudioManager.MUSIC_GROUP, "music_results", 1f, 3f, loop: true);
			}
			playerBattleCamera.animator.runtimeAnimatorController = battleVictoryAnimController;
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.stringParameter = "fullscreen";
			animationEvent.intParameter = 60;
			playerBattleCamera.OffsetIn(animationEvent);
			StartCoroutine(EndGame());
			yield break;
		}
		ReturnBattleCharacterToPool(battleCharacter);
		battleResult = new BattleResult(victory: false);
		if (TournamentTracker.isTournamentActive)
		{
			StartCoroutine(PlayTournamentLoss());
		}
		else if (ChallengeModeManager.isChallengeModeActive && !ChallengeModeManager.isNonJLChallenge)
		{
			StartCoroutine(PlayChallengeLoss());
		}
		else if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend)
		{
			StartCoroutine(PlayPvpLoss());
		}
		else
		{
			defeatScreen.gameObject.SetActive(value: true);
		}
		enemyBattleCamera.animator.runtimeAnimatorController = battleVictoryAnimController;
		AnimationEvent animationEvent2 = new AnimationEvent();
		animationEvent2.stringParameter = "fullscreen";
		animationEvent2.intParameter = 60;
		enemyBattleCamera.OffsetIn(animationEvent2);
		enemyBattleCamera.transform.DOLocalMoveY(0.48f, 0.5f).SetEase(Ease.InOutSine);
		StartCoroutine(EndGame());
	}

	private void HandleCastStarted(ActionMeter meter)
	{
		if (meter == playerActionMeter)
		{
			playerPortraits.isEnabled = false;
		}
		else
		{
			enemyPortraits.isEnabled = false;
		}
	}

	private void HandleAbilityUsed(ActionMeter meter, Character character, AbilityData abilityData)
	{
		StartWaiting();
	}

	public void CheckCharacterDeath()
	{
		if (_playerCharacter.character != null && _playerCharacter.character.isDead)
		{
			HandleDeathCharacter(_playerCharacter.character, fromEvent: false);
		}
		else if (_enemyCharacter.character != null && _enemyCharacter.character.isDead)
		{
			HandleDeathCharacter(_enemyCharacter.character, fromEvent: false);
		}
	}

	private void HandleAbilityFinished(ActionMeter meter, Character character, AbilityData abilityData)
	{
		TrackAbilityUse(character, abilityData);
		if (_playerCharacter.character.isDead)
		{
			HandleDeathCharacter(_playerCharacter.character, fromEvent: false);
		}
		else if (_enemyCharacter.character.isDead)
		{
			HandleDeathCharacter(_enemyCharacter.character, fromEvent: false);
		}
		else if (_playerCharacter.character.isKnockedOut)
		{
			int num = _playerParty.IndexOf(_playerCharacter.character) + 1;
			if (num >= _playerParty.Count)
			{
				num = 0;
			}
			Character character2 = null;
			for (int i = 0; i < _playerParty.Count; i++)
			{
				if (_playerParty[num] != _playerCharacter.character && !_playerParty[num].isDead && !_playerParty[num].isKnockedOut)
				{
					character2 = _playerParty[num];
					break;
				}
				num++;
				if (num >= _playerParty.Count)
				{
					num = 0;
				}
			}
			if (character2 == null)
			{
				List<Character> list = _playerParty.FindAll((Character obj) => obj != _playerCharacter.character && obj.isKnockedOut);
				if (list.Count > 0)
				{
					character2 = list[0];
					for (int num2 = 1; num2 < list.Count; num2++)
					{
						if (list[num2].GetStatusEffect<KnockedOut>().Duration < character2.GetStatusEffect<KnockedOut>().Duration)
						{
							character2 = list[num2];
						}
					}
					character2.RemoveStatusEffect<KnockedOut>();
				}
				else
				{
					character2 = _playerCharacter.character;
					character2.RemoveStatusEffect<KnockedOut>();
				}
			}
			playerPortraits.SelectCharacter(character2, forceEventCall: true);
		}
		else if (_enemyCharacter.character.isKnockedOut)
		{
			int num3 = _enemyParty.IndexOf(_enemyCharacter.character) + 1;
			if (num3 >= _enemyParty.Count)
			{
				num3 = 0;
			}
			Character character3 = null;
			for (int num4 = 0; num4 < _enemyParty.Count; num4++)
			{
				if (enemyParty[num3] != _enemyCharacter.character && !_enemyParty[num3].isDead && !_enemyParty[num3].isKnockedOut)
				{
					character3 = _enemyParty[num3];
					break;
				}
				num3++;
				if (num3 >= _enemyParty.Count)
				{
					num3 = 0;
				}
			}
			if (character3 == null)
			{
				List<Character> list2 = _enemyParty.FindAll((Character obj) => obj != _enemyCharacter.character && obj.isKnockedOut);
				if (list2.Count > 0)
				{
					character3 = list2[0];
					for (int num5 = 1; num5 < list2.Count; num5++)
					{
						if (list2[num5].GetStatusEffect<KnockedOut>().Duration < character3.GetStatusEffect<KnockedOut>().Duration)
						{
							character3 = list2[num5];
						}
					}
				}
				else
				{
					character3 = _enemyCharacter.character;
				}
				character3.RemoveStatusEffect<KnockedOut>();
			}
			enemyPortraits.SelectCharacter(character3, forceEventCall: true);
		}
		else
		{
			EndWaiting();
		}
	}

	public void SwapBattleCharacter(BattleCharacter battleCharacter, bool isPlayer)
	{
		if (isPlayer)
		{
			ReturnBattleCharacterToPool(_playerCharacter);
			_playerCharacter = battleCharacter;
			_playerCharacter.character.DeathCharacter += HandleOnDeathCharacter;
			playerBattleCamera.animator.runtimeAnimatorController = _playerCharacter.cameraController;
			playerBattleCamera.animator.avatar = _playerCharacter.cameraAvatar;
			playerActionMeter.character = _playerCharacter;
		}
		else
		{
			ReturnBattleCharacterToPool(_enemyCharacter);
			_enemyCharacter = battleCharacter;
			_enemyCharacter.character.DeathCharacter += HandleOnDeathCharacter;
			enemyBattleCamera.animator.runtimeAnimatorController = _enemyCharacter.cameraController;
			enemyBattleCamera.animator.avatar = _enemyCharacter.cameraAvatar;
			enemyActionMeter.character = _enemyCharacter;
		}
	}

	private IEnumerator EndGame()
	{
		PauseManager.instance.Resume();
		PauseManager.instance.isEnabled = false;
		if (battleDefinition.IsLODBattle)
		{
			Player.instance.pvpData.isInPvpBattle = false;
		}
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend)
		{
			if (battleResult.isVictory && (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend))
			{
				AchievementsDatabase.UnlockAchievementId("TT2_TEENYPVP", 1);
			}
			TT2NetManager.Instance.LeaveRoom();
			TT2NetMatch.Instance.ResetMatch();
		}
		if (battleResult.isVictory && (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend))
		{
			Character character = _playerParty.Find((Character character4) => character4.characterClass != CharacterClass.Super);
			if (character == null)
			{
				AchievementsDatabase.UnlockAchievementId("TT2_JUSTSUPER", 1);
			}
			Character character2 = _playerParty.Find((Character character4) => character4.characterClass != CharacterClass.Beast);
			if (character2 == null)
			{
				AchievementsDatabase.UnlockAchievementId("TT2_BEASTMODE", 1);
			}
			Character character3 = _playerParty.Find((Character character4) => character4.characterClass != CharacterClass.DarkArts);
			if (character3 == null)
			{
				AchievementsDatabase.UnlockAchievementId("TT2_LIGHTENUP", 1);
			}
		}
		_battleEnded = true;
		OnBattleEnded();
		ResetBattleStuff();
		pauseButton.gameObject.SetActive(value: false);
		Sequence sequence = DOTween.Sequence().OnComplete(() =>
		{
			playerActionMeter.gameObject.SetActive(value: false);
			playerPortraits.gameObject.SetActive(value: false);
			enemyActionMeter.gameObject.SetActive(value: false);
			enemyPortraits.gameObject.SetActive(value: false);
			batteryGUI.gameObject.SetActive(value: false);
		});
		sequence.Insert(0f, playerActionMeter.transform.DOLocalMoveX(-350f, 0.33f).SetRelative().SetEase(Ease.InBack));
		sequence.Insert(0f, playerPortraits.transform.DOLocalMoveX(-350f, 0.33f).SetRelative().SetEase(Ease.InBack));
		sequence.Insert(0f, enemyActionMeter.transform.DOLocalMoveX(350f, 0.33f).SetRelative().SetEase(Ease.InBack));
		sequence.Insert(0f, enemyPortraits.transform.DOLocalMoveX(350f, 0.33f).SetRelative().SetEase(Ease.InBack));
		sequence.Insert(0f, batteryGUI.transform.DOLocalMoveX(350f, 0.33f).SetRelative().SetEase(Ease.InBack));
		sequence.Play();
		if (battleResult.isVictory)
		{
			if (string.IsNullOrEmpty(_arenaData.battleThemePath) || !_arenaData.overrideResultsMusic)
			{
				GFAudioManager.instance.StopByAudioSource(_battleTheme);
				_battleTheme = null;
			}
			_playerCharacter.dancingWeight = 1f;
			_playerCharacter.Play("idle");
			if (_playerCharacter.characterDefinition.sounds.victorySounds.soundPaths.Count > 0 && UnityEngine.Random.value < _playerCharacter.characterDefinition.sounds.victorySounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(_playerCharacter.characterDefinition.sounds.victorySounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			if (_playerParty.Count >= 3)
			{
				bool flag = true;
				for (int num = 0; num < _playerParty.Count; num++)
				{
					if (_playerParty[num].repaintColorID <= 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					AchievementsDatabase.UnlockAchievementId("TT2_VICTORIOUSVARIANTS");
				}
			}
		}
		else
		{
			GFAudioManager.instance.FadeOut("battleTheme", 2f);
			_battleTheme = null;
			_enemyCharacter.Play("idle");
		}
		if (TT2NetManager.Instance != null)
		{
			TT2NetManager.Instance.OnPlayerDisconnectedEvent -= HandlePlayerDisconnected;
			TT2NetManager.Instance.DisconnectedFromNetworkEvent -= HandleDisconnectedFromNetworkEvent;
		}
		if (battleResult.isVictory)
		{
			battleDefinition = null;
		}
		yield break;
	}

	private void ResetBattleStuff()
	{
		playerActionMeter.RemoveAllStatusEffects();
		enemyActionMeter.RemoveAllStatusEffects();
		battleDefinition.playerCharactersTeamBoosted.Clear();
		battleDefinition.playerStatTeamBoostAmount = 0;
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend)
		{
			TT2NetChat.Instance.UpdateUserStatus(2);
		}
		int count = _playerParty.Count;
		for (int i = 0; i < count; i++)
		{
			_playerParty[i].modChipSlot.UnprepareChipForBattle();
			_playerParty[i].RemoveAllStatusEffects();
			_playerParty[i].ResetTransformation();
			_playerParty[i].Updated -= HandlePlayerCharacterUpdated;
			_playerParty[i].DeathCharacter -= HandleOnDeathCharacter;
			if (battleDefinition.type == BattleType.Normal || battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend)
			{
				_playerParty[i].hitPoints = _playerParty[i].maxHitPoints;
			}
			else if (battleDefinition.type == BattleType.Tournament && _playerParty[i].hitPoints > _playerParty[i].maxHitPoints)
			{
				_playerParty[i].hitPoints = _playerParty[i].maxHitPoints;
			}
		}
		count = _enemyParty.Count;
		for (int j = 0; j < count; j++)
		{
			_enemyParty[j].modChipSlot.UnprepareChipForBattle();
			_enemyParty[j].RemoveAllStatusEffects();
			_enemyParty[j].ResetTransformation();
			_enemyParty[j].DeathCharacter -= HandleOnDeathCharacter;
		}
		diminishingReturnsUtil.Dispose();
		diminishingReturnsUtil = null;
	}

	private IEnumerator PlayTournamentLoss()
	{
		yield return new WaitForSeconds(2f);
		LoadingScreenCover.LoadScene("Tournament");
	}

	private IEnumerator PlayChallengeLoss()
	{
		yield return new WaitForSeconds(2f);
		SaveUtility.SetInt("GAME_CHALLENGE_CONSECUTIVE_WINS", 0);
		LoadingScreenCover.LoadScene("Challenge");
	}

	private IEnumerator PlayPvpLoss()
	{
		yield return new WaitForSeconds(2f);
		Player.instance.pvpData.currentWinStreak = 0;
		Player.instance.pvpData.coinsReceived += GameManager.instance.legionOfDoomSettings.coinRewardLoss;
		Player.instance.pvpData.lastMatchIsLoss = true;
		Player.instance.AddPvpXP(GameManager.instance.legionOfDoomSettings.lossXP, 0);
		if (battleDefinition.type == BattleType.Pvp)
		{
			Player.instance.AddSkillRating(-GameManager.instance.legionOfDoomSettings.lossSkillRating);
		}
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}

	protected void OnBattleIntroStarted()
	{
		if (BattleIntroStarted != null)
		{
			BattleIntroStarted();
		}
	}

	protected void OnBattleStarted()
	{
		if (BattleStarted != null)
		{
			BattleStarted();
		}
	}

	protected void OnBattleEnded()
	{
		if (BattleEnded != null)
		{
			BattleEnded();
		}
		EventMessenger.Instance.Raise(new BattleEndedEvent(battleDefinition, battleResult));
	}

	protected void OnCharacterSwapInStart(Character character)
	{
		if (CharacterSwapInStart != null)
		{
			CharacterSwapInStart(character);
		}
	}

	protected void OnCharacterSwappedIn(Character character)
	{
		if (CharacterSwappedIn != null)
		{
			CharacterSwappedIn(character);
		}
	}

	protected void OnCharacterSwapOutStart(Character character)
	{
		if (CharacterSwapOutStart != null)
		{
			CharacterSwapOutStart(character);
		}
	}

	protected void OnCharacterSwappedOut(Character character)
	{
		if (CharacterSwappedOut != null)
		{
			CharacterSwappedOut(character);
		}
	}

	private void TrackSwapIn(Character character)
	{
		if (!swapInCounts.ContainsKey(character))
		{
			swapInCounts.Add(character, 1);
		}
		else
		{
			swapInCounts[character]++;
		}
	}

	public int GetSwapInCount(Character character)
	{
		if (!swapInCounts.ContainsKey(character))
		{
			return 0;
		}
		return swapInCounts[character];
	}

	private void TrackAbilityUse(Character character, AbilityData abilityData)
	{
		if (!abilityUseCounts.ContainsKey(character))
		{
			abilityUseCounts.Add(character, new Dictionary<AbilityData, int>());
		}
		if (!abilityUseCounts[character].ContainsKey(abilityData))
		{
			abilityUseCounts[character].Add(abilityData, 1);
		}
		else
		{
			abilityUseCounts[character][abilityData]++;
		}
	}

	public int GetAbilityUseCount(Character character, AbilityData abilityData)
	{
		if (!abilityUseCounts.ContainsKey(character))
		{
			return 0;
		}
		if (!abilityUseCounts[character].ContainsKey(abilityData))
		{
			return 0;
		}
		return abilityUseCounts[character][abilityData];
	}

	public bool HasCharacterParticipated(Character character)
	{
		return swapInCounts.ContainsKey(character);
	}

	private void HandleBattleRequestAccepted(BattleRequest battleRequest)
	{
		Debug.Log("Battle Controller, Handle Battle Request Accepted...");
		if (battleDefinition.type == BattleType.Tournament)
		{
			TournamentTracker.FinishTournament();
		}
		HandleOnHomeClicked();
		Communicator.afterBattleConvoName = string.Empty;
		PauseManager.instance.Resume();
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}
}
