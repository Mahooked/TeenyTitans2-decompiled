using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using ExitGames.Client.Photon;
using Grumpyface;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PreBattleController : MonoBehaviour
{
	public static PreBattleController Instance;

	public static BattleDefinition battleDefinition;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FinishedIntro__BackingField;

	public tk2dCamera mainCamera;

	public tk2dCamera guiCamera2;

	public tk2dTintAdapter backgroundTintAdapter;

	public tk2dSprite whiteCover;

	public AudioSource preBattleMusic;

	public VersusScreen versusScreen;

	public TeamSelectScreen teamSelectScreen;

	public Animator handheld;

	public HandheldConsole handheldConsole;

	public PreBattleParty preBattleParty;

	public tk2dUIItem exitButton;

	public tk2dSprite exitButtonSprite;

	public CharacterInfoBox characterInfoBox;

	public AccessorySelectorBar accessorySelectorBar;

	public ChallengeModePreBattle challengeModePreBattle;

	public AudioClip sfxHandheldOpen;

	public float fadeStartDelay;

	public float fadeDuration = 1.45f;

	public Ease fadeEase = Ease.InSine;

	[Header("PvP Related Objects")]
	public tk2dSprite enemyNameIcon;

	public TextMeshPro enemyNameTextMesh;

	public tk2dSprite enemySkillRatingIcon;

	public TextMeshPro enemySkillRatingTextMesh;

	[Header("Debug")]
	public BattleType debugBattleType;

	private int prevCountdown = -1;

	public bool exiting { get; set; }

	public event Action FinishedIntro
	{
		add
		{
			Action action = FinishedIntro__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FinishedIntro__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FinishedIntro__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FinishedIntro__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		Instance = this;
		exitButton.gameObject.SetActive(false);
		if (teamSelectScreen.otherPlayerReadyText != null)
		{
			teamSelectScreen.otherPlayerReadyText.gameObject.SetActive(false);
		}
		if (teamSelectScreen.countdownTimerText != null)
		{
			teamSelectScreen.countdownTimerText.text = string.Empty;
		}
		if (!versusScreen.gameObject.activeSelf)
		{
			versusScreen.gameObject.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		Instance = null;
		TT2NetCountdownTimer.CountdownTimerExpired -= TT2NetCountdownTimer_CountdownTimerExpired;
		if (TT2NetCountdownTimer.Instance != null && PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.Destroy(TT2NetCountdownTimer.Instance.gameObject);
		}
	}

	private void OnEnable()
	{
		exitButton.OnClick += HandleExitClick;
		Input.multiTouchEnabled = false;
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
	}

	private void OnDisable()
	{
		exitButton.OnClick -= HandleExitClick;
		Input.multiTouchEnabled = true;
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
	}

	private IEnumerator Start()
	{
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		Player.instance.activeTeam.Clear();
		teamSelectScreen.Init(Player.instance, battleDefinition);
		teamSelectScreen.preBattleParty.EnableButtons(false);
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend)
		{
			versusScreen.playerPortraitSprite.SetSprite(battleDefinition.pvpPortraitSpriteCollection, Player.instance.pvpData.PortraitSpriteName);
			versusScreen.playerNameText.text = Player.instance.pvpData.PlayerName;
			versusScreen.playerLevelTextMesh.text = Player.instance.pvpData.level.ToString();
			versusScreen.playerSkillRatingTextMesh.text = Player.instance.pvpData.skillRating.ToString();
			versusScreen.playerPortraitBacking.SetSprite(GameManager.instance.legionOfDoomSettings.GetBorderSpriteForRank(Player.instance.pvpData.level));
			versusScreen.playerRankPlateBacking.SetSprite(GameManager.instance.legionOfDoomSettings.GetNumberPlateSpriteForRank(Player.instance.pvpData.level));
			versusScreen.enemyPortraitSprite.SetSprite(battleDefinition.pvpPortraitSpriteCollection, TT2NetManager.Instance.otherNetworkPlayer.PortraitSpriteName);
			versusScreen.enemySkillRatingTextMesh.text = TT2NetManager.Instance.otherNetworkPlayer.SkillRating.ToString();
			versusScreen.enemyLevelTextMesh.text = TT2NetManager.Instance.otherNetworkPlayer.Rank.ToString();
			versusScreen.enemyPortraitBacking.SetSprite(GameManager.instance.legionOfDoomSettings.GetBorderSpriteForRank(TT2NetManager.Instance.otherNetworkPlayer.Rank));
			versusScreen.enemyRankPlateBacking.SetSprite(GameManager.instance.legionOfDoomSettings.GetNumberPlateSpriteForRank(TT2NetManager.Instance.otherNetworkPlayer.Rank));
			enemyNameTextMesh.text = battleDefinition.enemyName;
			enemySkillRatingTextMesh.text = TT2NetManager.Instance.otherNetworkPlayer.SkillRating.ToString();
			teamSelectScreen.readyForBattleMessage.SetActive(false);
			teamSelectScreen.waitingForPlayerText.text = string.Format(Language.Get("WAITINGFOROPPONENT2", "MENUS,GUI"), TT2NetManager.Instance.otherNetworkPlayer.PlayerName);
			int count = teamSelectScreen.enemyPortraits.Count;
			for (int i = 0; i < count; i++)
			{
				if (i < battleDefinition.enemyActiveParty.Count)
				{
					teamSelectScreen.enemyPortraits[i].Init(battleDefinition.enemyActiveParty[i]);
				}
				else
				{
					teamSelectScreen.enemyPortraits[i].gameObject.SetActive(false);
				}
			}
			if (PhotonNetwork.IsMasterClient)
			{
				PhotonNetwork.Instantiate("Prefabs/TT2Net/TT2NetPreBattleCountdownTimer", Vector3.zero, Quaternion.identity, 0);
			}
			TT2NetCountdownTimer.CountdownTimerExpired += TT2NetCountdownTimer_CountdownTimerExpired;
			TT2NetManager.Instance.otherNetworkPlayer.Ready += OtherNetworkPlayer_Ready;
		}
		else if (battleDefinition.type == BattleType.PvpCpu)
		{
			versusScreen.playerPortraitSprite.SetSprite(battleDefinition.pvpPortraitSpriteCollection, Player.instance.pvpData.PortraitSpriteName);
			versusScreen.playerNameText.text = Player.instance.pvpData.PlayerName;
			versusScreen.playerLevelTextMesh.text = Player.instance.pvpData.level.ToString();
			versusScreen.playerSkillRatingTextMesh.text = Player.instance.pvpData.skillRating.ToString();
			versusScreen.playerPortraitBacking.SetSprite(GameManager.instance.legionOfDoomSettings.GetBorderSpriteForRank(Player.instance.pvpData.level));
			versusScreen.playerRankPlateBacking.SetSprite(GameManager.instance.legionOfDoomSettings.GetNumberPlateSpriteForRank(Player.instance.pvpData.level));
			versusScreen.enemySkillRatingSprite.gameObject.SetActive(false);
			versusScreen.enemySkillRatingTextMesh.renderer.enabled = false;
			versusScreen.enemyLevelTextMesh.renderer.enabled = false;
			versusScreen.enemyPortraitBacking.gameObject.SetActive(false);
			versusScreen.enemyRankPlateBacking.gameObject.SetActive(false);
			enemyNameIcon.gameObject.SetActive(false);
			enemyNameTextMesh.renderer.enabled = false;
			enemySkillRatingIcon.gameObject.SetActive(false);
			enemySkillRatingTextMesh.renderer.enabled = false;
			teamSelectScreen.readyForBattleMessage.SetActive(false);
			NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(battleDefinition.enemyCharacterName);
			if (nPCDefByType != null)
			{
				versusScreen.enemyPortraitSprite.SetSprite(nPCDefByType.battleIconSpriteCollection, (!string.IsNullOrEmpty(nPCDefByType.preBattleSpriteRight)) ? nPCDefByType.preBattleSpriteRight : nPCDefByType.preBattleSprite);
				battleDefinition.useDefaultPreBattleSound = nPCDefByType.useDefaultPreBattleSounds;
				battleDefinition.soundData = nPCDefByType.preBattleSoundData;
				battleDefinition.countdownCharacterUID = nPCDefByType.countdownCharacterUID;
			}
			for (int j = 0; j < teamSelectScreen.enemyPortraits.Count; j++)
			{
				if (j < battleDefinition.enemyTeam.Count)
				{
					teamSelectScreen.enemyPortraits[j].Init(battleDefinition.enemyTeam[j]);
				}
				else
				{
					teamSelectScreen.enemyPortraits[j].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			NPCDefinition nPCDefByType2 = GameManager.instance.npcDatabase.GetNPCDefByType(CityCharacter.RobinType);
			versusScreen.playerPortraitSprite.SetSprite(nPCDefByType2.battleIconSpriteCollection, nPCDefByType2.preBattleSprite);
			versusScreen.playerNameText.text = ((!string.IsNullOrEmpty(nPCDefByType2.nameKey)) ? Language.Get(nPCDefByType2.nameKey, "NPCS,FIGS") : nPCDefByType2.name);
			NPCDefinition nPCDefByType3 = GameManager.instance.npcDatabase.GetNPCDefByType(battleDefinition.enemyCharacterName);
			if (nPCDefByType3 != null)
			{
				versusScreen.enemyPortraitSprite.SetSprite(nPCDefByType3.battleIconSpriteCollection, (!string.IsNullOrEmpty(nPCDefByType3.preBattleSpriteRight)) ? nPCDefByType3.preBattleSpriteRight : nPCDefByType3.preBattleSprite);
				battleDefinition.useDefaultPreBattleSound = nPCDefByType3.useDefaultPreBattleSounds;
				battleDefinition.soundData = nPCDefByType3.preBattleSoundData;
				battleDefinition.countdownCharacterUID = nPCDefByType3.countdownCharacterUID;
			}
			int count2 = teamSelectScreen.enemyPortraits.Count;
			for (int k = 0; k < count2; k++)
			{
				if (k < battleDefinition.enemyTeam.Count)
				{
					teamSelectScreen.enemyPortraits[k].Init(battleDefinition.enemyTeam[k]);
				}
				else
				{
					teamSelectScreen.enemyPortraits[k].gameObject.SetActive(false);
				}
			}
		}
		versusScreen.enemyNameText.text = battleDefinition.enemyName;
		List<string> checkList = new List<string>();
		int len = Player.instance.activeParty.Count;
		for (int l = 0; l < len; l++)
		{
			if (!checkList.Contains(Player.instance.activeParty[l].characterDefinition.shopFigurePath))
			{
				ResourceRequest request = Resources.LoadAsync(Player.instance.activeParty[l].characterDefinition.shopFigurePath);
				StartCoroutine(WaitForRequest(request));
				checkList.Add(Player.instance.activeParty[l].characterDefinition.shopFigurePath);
			}
		}
		yield return new WaitForSeconds(3f);
		versusScreen.animator.enabled = false;
		DOTween.ToAlpha(() => versusScreen.backingTopRenderer.material.color, (Color x) =>
		{
			versusScreen.backingTopRenderer.material.color = x;
		}, 0f, 0.35f);
		DOTween.ToAlpha(() => versusScreen.backingBottomRenderer.material.color, (Color x) =>
		{
			versusScreen.backingBottomRenderer.material.color = x;
		}, 0f, 0.35f);
		DOTween.ToAlpha(() => versusScreen.stripSprite.color, (Color x) =>
		{
			versusScreen.stripSprite.color = x;
		}, 0f, 0.35f);
		DOTween.ToAlpha(() => versusScreen.stripSprite2.color, (Color x) =>
		{
			versusScreen.stripSprite2.color = x;
		}, 0f, 0.35f);
		DOTween.To(() => versusScreen.vsSprite.scale, (Vector3 x) =>
		{
			versusScreen.vsSprite.scale = x;
		}, new Vector3(0f, 0f, 1f), 0.35f).SetEase(Ease.InBack);
		versusScreen.playerNameText.transform.parent = versusScreen.animator.transform;
		versusScreen.enemyNameText.transform.parent = versusScreen.animator.transform;
		versusScreen.playerNameText.transform.DOLocalMoveX(-400f, 0.25f).SetRelative().SetEase(Ease.InBack);
		versusScreen.enemyNameText.transform.DOLocalMoveX(400f, 0.25f).SetRelative().SetEase(Ease.InBack);
		versusScreen.playerPortraitSprite.transform.parent = teamSelectScreen.playerBar;
		versusScreen.enemyPortraitSprite.transform.parent = teamSelectScreen.enemyBar;
		versusScreen.playerPortraitSprite.transform.DOLocalMove(new Vector3(-135f, 0f, versusScreen.playerPortraitSprite.transform.localPosition.z), 0.35f).SetEase(Ease.OutSine);
		versusScreen.enemyPortraitSprite.transform.DOLocalMove(new Vector3((battleDefinition.type != BattleType.Pvp && battleDefinition.type != BattleType.PvpFriend) ? 135f : 146.2f, 0f, versusScreen.enemyPortraitSprite.transform.localPosition.z), 0.35f).SetEase(Ease.OutSine);
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend)
		{
			versusScreen.enemySkillRatingTextMesh.DOFade(0f, 0.15f);
			DOTween.ToAlpha(() => versusScreen.enemySkillRatingSprite.color, (Color x) =>
			{
				versusScreen.enemySkillRatingSprite.color = x;
			}, 0f, 0.15f);
		}
		if (battleDefinition.useDefaultPreBattleSound)
		{
			if (UnityEngine.Random.value < GameManager.instance.gameSettings.defaultPrebattleVOProbability)
			{
				CityCharacterVOSet characterVOSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(CityCharacter.characterType);
				GFAudioManager.instance.PlayClipFromResources(characterVOSet.enterPrebattleClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
		}
		else if (battleDefinition.soundData != null && battleDefinition.soundData.soundPaths.Count > 0 && UnityEngine.Random.value < battleDefinition.soundData.probability)
		{
			GFAudioManager.instance.PlayClipFromResources(battleDefinition.soundData.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
		yield return new WaitForSeconds(0.35f);
		versusScreen.playerPortraitSprite.transform.DOLocalMoveX(-400f, 0.25f).SetEase(Ease.OutSine);
		versusScreen.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(0.25f);
		handheldConsole.model.animator.gameObject.SetActive(true);
		GFAudioManager.instance.PlayClip(sfxHandheldOpen, 1f, false, 0.25f, 128, GFAudioManager.SFX_GROUP);
		preBattleMusic.Play();
		if (SaveUtility.GetInt(string.Concat(CoreGameTutorial.Step.CityBattleNPC, "_Complete"), 0) == 1)
		{
			exitButton.gameObject.SetActive(true);
		}
		if ((battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend) && PhotonNetwork.IsMasterClient)
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable.Add("PreBattleStartTime", (float)PhotonNetwork.Time);
			PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
		}
		teamSelectScreen.goButton.OnClick += HandleOnGoClick;
		yield return Resources.UnloadUnusedAssets();
		yield return new WaitForSeconds(1.5f);
		teamSelectScreen.preBattleParty.EnableButtons();
		if (FinishedIntro__BackingField != null)
		{
			FinishedIntro__BackingField();
		}
	}

	private void Update()
	{
		if (TT2NetCountdownTimer.Instance != null && TT2NetCountdownTimer.Instance.isTimerRunning)
		{
			int num = Mathf.RoundToInt(TT2NetCountdownTimer.Instance.countdown);
			if (prevCountdown == -1)
			{
				prevCountdown = num;
			}
			teamSelectScreen.countdownTimerText.text = num.ToString();
			if (num <= 10 && num != prevCountdown)
			{
				teamSelectScreen.countdownTimerText.transform.DOComplete();
				teamSelectScreen.countdownTimerText.transform.DOScale(1.75f, 0.25f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutCubic);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_prebattle_countdown", GFAudioManager.SFX_GROUP, string.Empty);
			}
			prevCountdown = num;
		}
		if (Input.GetKeyDown(KeyCode.Keypad6))
		{
			UnityEngine.Debug.Log("Create Test Battle Request...");
			if (Player.instance.pvpData.friends.Count > 0)
			{
				BattleRequest battleRequest = new BattleRequest();
				battleRequest.playerId = Player.instance.pvpData.friends[0].playerID;
				battleRequest.region = "US";
				battleRequest.roomName = "TestRoom";
				BattleRequest battleRequest2 = battleRequest;
				Player.instance.pvpData.AddBattleRequest(battleRequest2);
			}
			else
			{
				UnityEngine.Debug.LogError("You need at least 1 friend to test a battle request...");
			}
		}
		if (Input.GetKeyDown(KeyCode.Keypad7))
		{
			UnityEngine.Debug.Log("Create Test Friend Request...");
			FriendRequest friendRequest = new FriendRequest();
			friendRequest.playerId = FriendCodeGenerator.Generate();
			friendRequest.playerName = "TestFriend" + UnityEngine.Random.Range(0, 101);
			friendRequest.level = UnityEngine.Random.Range(1, GameManager.instance.legionOfDoomLevelTable.MaxLevel + 1);
			friendRequest.portraitSpriteName = GameManager.instance.legionOfDoomSettings.GetRandomPortraitSprite();
			friendRequest.skillRating = UnityEngine.Random.Range(1000, 4001);
			FriendRequest friendRequest2 = friendRequest;
			Player.instance.pvpData.AddFriendRequest(friendRequest2);
		}
	}

	private void TT2NetCountdownTimer_CountdownTimerExpired()
	{
		if (TT2NetManager.Instance.myNetworkPlayer.battleReady)
		{
			return;
		}
		UnityEngine.Debug.Log("COUNTDOWN EXPIRED: We should now randomly fill in all character slots and force this player to be ready.");
		teamSelectScreen.countdownTimerText.text = string.Empty;
		List<Character> list = new List<Character>();
		for (int i = 0; i < Player.instance.activeParty.Count; i++)
		{
			if (!Player.instance.activeTeam.Contains(Player.instance.activeParty[i]) && preBattleParty.CanUseCharacterInBattle(Player.instance.activeParty[i]))
			{
				list.Add(Player.instance.activeParty[i]);
			}
		}
		preBattleParty.ForceStopDragging();
		preBattleParty.ForceStopEquipping();
		if (handheldConsole.centerArrow.character == null && list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			preBattleParty.SetCharacterForArrow(list[index], handheldConsole.centerArrow);
			list.RemoveAt(index);
		}
		if (handheldConsole.leftArrow.character == null && list.Count > 0)
		{
			int index2 = UnityEngine.Random.Range(0, list.Count);
			preBattleParty.SetCharacterForArrow(list[index2], handheldConsole.leftArrow);
			list.RemoveAt(index2);
		}
		if (handheldConsole.rightArrow.character == null && list.Count > 0)
		{
			int index3 = UnityEngine.Random.Range(0, list.Count);
			preBattleParty.SetCharacterForArrow(list[index3], handheldConsole.rightArrow);
			list.RemoveAt(index3);
		}
		HandleOnGoClick();
	}

	private void OtherNetworkPlayer_Ready()
	{
		teamSelectScreen.otherPlayerReadyText.gameObject.SetActive(true);
		teamSelectScreen.otherPlayerReadyText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Sequence sequence = DOTween.Sequence().OnComplete(() =>
		{
			teamSelectScreen.otherPlayerReadyText.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		});
		sequence.Append(teamSelectScreen.otherPlayerReadyText.transform.DOScale(2f, 0.15f).SetEase(Ease.OutCubic));
		sequence.Append(teamSelectScreen.otherPlayerReadyText.transform.DOScale(1f, 0.15f).SetEase(Ease.InCubic));
		sequence.Play();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_prebattle_ready", GFAudioManager.SFX_GROUP, string.Empty);
	}

	public void UpdateEnemyPortraits()
	{
		UnityEngine.Debug.Log("UpdateEnemyPortraits");
		int count = teamSelectScreen.enemyPortraits.Count;
		for (int i = 0; i < count; i++)
		{
			if (i < battleDefinition.enemyTeam.Count)
			{
				teamSelectScreen.enemyPortraits[i].gameObject.SetActive(true);
				teamSelectScreen.enemyPortraits[i].Init(battleDefinition.enemyTeam[i]);
			}
			else
			{
				teamSelectScreen.enemyPortraits[i].gameObject.SetActive(false);
			}
		}
	}

	public void HandleExitClick()
	{
		if (!exiting)
		{
			exiting = true;
			UnityEngine.Debug.Log("Exit Clicked");
			if (battleDefinition != null && battleDefinition.type == BattleType.Tournament)
			{
				StartCoroutine(ShowWarningDialog());
			}
			else if (battleDefinition != null && battleDefinition.type == BattleType.Challenge && !ChallengeModeManager.isNonJLChallenge)
			{
				battleDefinition = null;
				LoadingScreenCover.LoadScene("Challenge");
				ChallengeModeManager.isChallengeModeActive = false;
			}
			else if (battleDefinition != null && (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpCpu || battleDefinition.type == BattleType.PvpFriend))
			{
				battleDefinition = null;
				TT2NetManager.Instance.LeaveRoom();
				TT2NetMatch.SendEndMatch();
				TT2NetChat.Instance.UpdateUserStatus(2);
				LoadingScreenCover.LoadScene("LegionOfDoomMode");
				ChallengeModeManager.isChallengeModeActive = false;
			}
			else
			{
				battleDefinition = null;
				Communicator.afterBattleConvoName = string.Empty;
				TT2NetManager.Instance.LeaveRoom();
				TT2NetMatch.SendEndMatch();
				LoadingScreenCover.LoadScene(CityController.previousCityScene);
				ChallengeModeManager.isChallengeModeActive = false;
			}
		}
	}

	private IEnumerator ShowWarningDialog()
	{
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default(Vector3), false);
		dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		dialog.transform.parent = guiCamera2.transform;
		dialog.transform.localPosition = new Vector3(0f, 0f, 200f);
		dialog.descriptionText.text = Language.Get("TOURNYEXITWARNING", "MENUS,GUI");
		dialog.gameObject.SetActive(true);
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			TournamentTracker.FinishTournament();
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
			ChallengeModeManager.isChallengeModeActive = false;
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
	}

	private void HandleOnGoClick()
	{
		Player instance = Player.instance;
		if (battleDefinition.type == BattleType.Normal)
		{
			instance.IncreaseCurrentTime(20);
		}
		instance.activeTeam.Clear();
		instance.AddToTeam(handheldConsole.centerArrow.character);
		instance.AddToTeam(handheldConsole.leftArrow.character);
		instance.AddToTeam(handheldConsole.rightArrow.character);
		if (battleDefinition.type == BattleType.Challenge && ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.SurpriseSquad)
		{
			for (int i = 0; i < preBattleParty.slots.Count; i++)
			{
				MySquadSlot mySquadSlot = preBattleParty.slots[i];
				if (mySquadSlot.portraitSprite != null)
				{
					mySquadSlot.portraitSprite.color = Color.grey;
				}
				if (mySquadSlot.repaintIcon != null)
				{
					mySquadSlot.repaintIcon.color = Color.grey;
				}
			}
		}
		if (battleDefinition.type == BattleType.Tournament && TournamentTracker.isTournamentActive && battleDefinition.accessoryUID >= 0)
		{
			TournamentTracker.usedAccessories.Add(battleDefinition.accessoryUID);
		}
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
			teamSelectScreen.handheldConsole.centerArrow.uiItem.enabled = false;
			teamSelectScreen.handheldConsole.leftArrow.uiItem.enabled = false;
			teamSelectScreen.handheldConsole.rightArrow.uiItem.enabled = false;
			preBattleParty.EnableButtons(false);
			accessorySelectorBar.Disable();
			List<int> list = new List<int>();
			if (handheldConsole.centerArrow.character != null)
			{
				list.Add(instance.activeParty.IndexOf(handheldConsole.centerArrow.character));
			}
			if (handheldConsole.leftArrow.character != null)
			{
				list.Add(instance.activeParty.IndexOf(handheldConsole.leftArrow.character));
			}
			if (handheldConsole.rightArrow.character != null)
			{
				list.Add(instance.activeParty.IndexOf(handheldConsole.rightArrow.character));
			}
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable["Team"] = SerializationUtility.SerializeToBytes(list);
			hashtable["AccessoryUid"] = battleDefinition.accessoryUID;
			PhotonNetwork.SetPlayerCustomProperties(hashtable);
			TT2NetManager.SendUpdateBattleReady(true);
			teamSelectScreen.goButtonScaleGroup.DOKill();
			teamSelectScreen.goButtonScaleGroup.transform.localScale = Vector3.one;
			DOTween.Kill(teamSelectScreen.goButtonScaleGroup);
			teamSelectScreen.goButtonScaleGroup.localScale = Vector3.one;
			teamSelectScreen.DisableGoSpriteButton();
			StartCoroutine(TransitionToNetBattle());
		}
		else
		{
			StartCoroutine(TransitionToBattle());
		}
	}

	private IEnumerator TransitionToNetBattle()
	{
		if (characterInfoBox != null)
		{
			characterInfoBox.gameObject.SetActive(false);
		}
		if (TT2NetManager.Instance.otherNetworkPlayer.Team.Count == 0)
		{
			teamSelectScreen.goButtonScaleGroup.gameObject.SetActive(false);
			teamSelectScreen.readyForBattleMessage.SetActive(true);
			teamSelectScreen.readyForBattleMessage.transform.DOLocalMoveX(400f, 0.25f).From(true).SetEase(Ease.OutSine);
		}
		yield return null;
		UnityEngine.Debug.Log("[PreBattleController::TransitionToNetBattle] - Waiting for MatchState to equal \"Ready\"");
		yield return new WaitUntil(() => TT2NetMatch.Instance.MatchState.Equals("Ready") && TT2NetManager.Instance.otherNetworkPlayer.Team.Count > 0);
		UnityEngine.Debug.Log("[PreBattleController::TransitionToNetBattle] - MatchState equals \"Ready\"");
		TT2NetCountdownTimer.Instance.ForceStop();
		teamSelectScreen.otherPlayerReadyText.gameObject.SetActive(false);
		battleDefinition.enemyTeam = new List<Character>(TT2NetManager.Instance.otherNetworkPlayer.Team);
		battleDefinition.enemyAccessoryUID = TT2NetManager.Instance.otherNetworkPlayer.AccessoryUid;
		TeamStatBonusDatabase.BonusResult result = GameManager.instance.teamStatBonusDatabase.GetBonusResult(TT2NetManager.Instance.otherNetworkPlayer.Team);
		if (result.IsBonus)
		{
			battleDefinition.enemyStatTeamBoosted = result.statType;
			battleDefinition.enemyStatTeamBoostAmount = result.bonusAmount;
			battleDefinition.enemyCharactersTeamBoosted = new List<Character>(result.characters);
		}
		StartCoroutine(TransitionToBattle());
	}

	private IEnumerator TransitionToBattle()
	{
		exitButton.gameObject.SetActive(false);
		if (characterInfoBox != null)
		{
			characterInfoBox.gameObject.SetActive(false);
		}
		if (battleDefinition.type == BattleType.Challenge)
		{
			if (ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.KeepItClassy)
			{
				battleDefinition.enemyTeam.Clear();
				battleDefinition.enemyTeam.AddRange(ChallengeModeBattleRandomizor.GetClassyEnemyCharacters(Player.instance.activeTeam));
			}
			if (challengeModePreBattle != null)
			{
				challengeModePreBattle.AnimateOutChallengeModeButton();
			}
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_prebattle_gobutton", GFAudioManager.SFX_GROUP, string.Empty);
		backgroundTintAdapter.tintColor = new Color(1f, 1f, 1f, 0f);
		whiteCover.color = new Color(1f, 1f, 1f, 0f);
		whiteCover.gameObject.SetActive(true);
		DOTween.Kill(teamSelectScreen.goButtonScaleGroup);
		teamSelectScreen.goButtonScaleGroup.localScale = Vector3.one;
		Sequence seq = DOTween.Sequence();
		seq.Append(DOTween.ToAlpha(() => backgroundTintAdapter.tintColor, (Color x) =>
		{
			backgroundTintAdapter.tintColor = x;
		}, 1f, 1f));
		seq.Insert(0f, teamSelectScreen.playerBar.DOLocalMoveY(-200f, 0.45f).SetEase(Ease.InBack));
		seq.Insert(0f, teamSelectScreen.enemyBar.DOLocalMoveY(200f, 0.45f).SetEase(Ease.InBack));
		seq.Insert(0f, teamSelectScreen.goButtonScaleGroup.DOScale(0f, 0.45f).SetEase(Ease.InBack));
		seq.Insert(0f, teamSelectScreen.choosePartyText.transform.DOLocalMoveY(-200f, 0.45f).SetEase(Ease.InBack));
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend)
		{
			seq.Insert(0f, teamSelectScreen.readyForBattleMessageAnimGroup.transform.DOScale(0f, 0.45f).SetEase(Ease.InBack));
		}
		float startTime = fadeStartDelay;
		ShopFigureModel figureModel = null;
		int len = handheldConsole.arrows.Length;
		for (int num = 0; num < len; num++)
		{
			figureModel = handheldConsole.arrows[num].figureModel;
			if (figureModel != null)
			{
				if (figureModel.renderer != null)
				{
					seq.Insert(startTime, figureModel.renderer.material.DOFade(1f, "_TintColor", fadeDuration).SetEase(fadeEase));
				}
				if (figureModel.faceRenderer != null)
				{
					seq.Insert(startTime, figureModel.faceRenderer.material.DOFade(1f, "_TintColor", fadeDuration).SetEase(fadeEase));
				}
				if (figureModel.baseRenderer != null)
				{
					seq.Insert(startTime, figureModel.baseRenderer.material.DOFade(1f, "_TintColor", fadeDuration).SetEase(fadeEase));
				}
				ObjectPool.GetObjectFromResource("Prefabs/FX/SpawnHandHeld_ParticleSystem", handheldConsole.arrows[num].figurePosition.position);
			}
		}
		seq.Insert(1.45f, mainCamera.transform.DOLocalRotate(new Vector3(14f, 0f, 0f), 1f).SetEase(Ease.OutSine));
		seq.Insert(1.45f, DOTween.To(() => mainCamera.ZoomFactor, (float x) =>
		{
			mainCamera.ZoomFactor = x;
		}, 10f, 1f).SetEase(Ease.InSine));
		seq.Insert(1.45f, DOTween.ToAlpha(() => whiteCover.color, (Color x) =>
		{
			whiteCover.color = x;
		}, 1f, 1f).SetEase(Ease.OutSine));
		seq.Play();
		if (accessorySelectorBar != null && accessorySelectorBar.gameObject.activeInHierarchy)
		{
			accessorySelectorBar.MoveOut();
		}
		yield return new WaitForSeconds(1.45f);
		preBattleMusic.DOFade(0f, 0.9f);
		yield return seq.WaitForCompletion();
		preBattleMusic.DOKill();
		BattleController.battleDefinition = battleDefinition;
		if (battleDefinition.type == BattleType.Pvp || battleDefinition.type == BattleType.PvpFriend)
		{
			TT2NetMatch.SendUpdateBattleMatchData();
			LoadingScreenCover.LoadScene("Battle", true, null, false, WaitForBattleLoaded());
		}
		else
		{
			LoadingScreenCover.LoadScene("Battle");
		}
		battleDefinition = null;
	}

	private static IEnumerator WaitForBattleLoaded()
	{
		TT2NetPlayer player1 = TT2NetManager.Instance.myNetworkPlayer;
		TT2NetPlayer player2 = TT2NetManager.Instance.otherNetworkPlayer;
		yield return new WaitUntil(() => player1.battleLoaded && player2.battleLoaded);
		if (PhotonNetwork.IsMasterClient)
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable.Add("StartTime", PhotonNetwork.ServerTimestamp);
			PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
		}
	}

	private void RemoveFigureCallback(PreBattleArrow arrow)
	{
		ObjectPool.PoolObject(arrow.figureModel.gameObject);
		arrow.figureModel.transform.localRotation = Quaternion.identity;
		arrow.figureModel.transform.localScale = Vector3.one;
		arrow.character = null;
		arrow.figureModel = null;
	}

	private IEnumerator WaitForRequest(ResourceRequest request)
	{
		while (!request.isDone)
		{
			yield return null;
		}
		ObjectPool.instance.AddType((GameObject)request.asset, 1);
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		if (battleDefinition.type == BattleType.Tournament)
		{
			TournamentTracker.FinishTournament();
		}
		battleDefinition = null;
		Communicator.afterBattleConvoName = string.Empty;
		TT2NetManager.Instance.LeaveRoom();
		TT2NetMatch.SendEndMatch();
		ChallengeModeManager.isChallengeModeActive = false;
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}
}
