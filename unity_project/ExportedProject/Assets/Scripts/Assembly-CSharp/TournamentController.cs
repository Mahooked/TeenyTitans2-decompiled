using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TournamentController : MonoBehaviour
{
	public static TournamentDefinition initDefinition = null;

	public static bool isDebugTournament = false;

	public static TournamentController instance;

	private static Dictionary<TournamentTofuType, string> tofuSpriteNames = new Dictionary<TournamentTofuType, string>
	{
		{
			TournamentTofuType.Revive,
			"menu_tournament_tofu_revive"
		},
		{
			TournamentTofuType.SingleHeal,
			"menu_tournament_tofu_heal"
		},
		{
			TournamentTofuType.TeamHeal,
			"menu_tournament_tofu_teamheal"
		},
		{
			TournamentTofuType.Spicy,
			"menu_tournament_tofu_spicy"
		}
	};

	private static Dictionary<TournamentTofuType, string> tofuNames = new Dictionary<TournamentTofuType, string>
	{
		{
			TournamentTofuType.Revive,
			"REVIVE_TOFU"
		},
		{
			TournamentTofuType.SingleHeal,
			"HEAL_TOFU"
		},
		{
			TournamentTofuType.TeamHeal,
			"TEAM_HEAL"
		},
		{
			TournamentTofuType.Spicy,
			"SPICY_TOFU"
		}
	};

	private static Dictionary<TournamentTofuType, string> tofuDescriptions = new Dictionary<TournamentTofuType, string>
	{
		{
			TournamentTofuType.Revive,
			"REVIVE_TOFU_DESC"
		},
		{
			TournamentTofuType.SingleHeal,
			"HEAL_TOFU_DESC"
		},
		{
			TournamentTofuType.TeamHeal,
			"TEAM_HEAL_DESC"
		},
		{
			TournamentTofuType.Spicy,
			"SPICY_TOFU_DESC"
		}
	};

	public tk2dUIItem exitButton;

	public tk2dUIItem nextButton;

	public tk2dUIScrollableArea scrollableArea;

	public tk2dSprite backgroundSprite;

	public TournamentHeader header;

	public MySquad mySquad;

	public TournamentVictoryScreen victoryScreen;

	public ShopFigureReveal figureReveal;

	public Camera figureRevealCamera;

	public float contentStartYPos = -152f;

	public float contentSpacing = 100f;

	public float contentBottomPadding = 150f;

	public AudioSource backgroundMusic;

	public string testTournamentName = "AlfredTournament";

	public int startRound = 1;

	public tk2dSprite blackness;

	public tk2dUIItem autoWinButton;

	private float backgroundYLimit;

	private TournamentPlayerPortrait playerPortrait;

	private List<TournamentEnemyPortrait> enemyPortraits = new List<TournamentEnemyPortrait>();

	private BattleDefinition nextBattleDefinition;

	private Character tofuTarget;

	private bool clickedNext;

	private TournamentDefinition _definition;

	private void Awake()
	{
		instance = this;
		nextButton.gameObject.SetActive(false);
		exitButton.gameObject.SetActive(false);
		figureReveal.gameObject.SetActive(false);
		figureRevealCamera.enabled = false;
		header.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private IEnumerator Start()
	{
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		mySquad.gameObject.SetActive(true);
		mySquad.Init(Player.instance, false, true);
		mySquad.animator.Play("MySquad_outro_big", 0, 1f);
		if (initDefinition != null)
		{
			if (TournamentTracker.activeTournament == null)
			{
				TournamentTracker.TrackTournament(initDefinition);
			}
			Init(initDefinition);
		}
	}

	private void OnEnable()
	{
		scrollableArea.OnScroll += HandleOnScroll;
		nextButton.OnClick += HandleOnNextClick;
		exitButton.OnClick += HandleOnExitClick;
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
	}

	private void OnDisable()
	{
		scrollableArea.OnScroll -= HandleOnScroll;
		nextButton.OnClick -= HandleOnNextClick;
		exitButton.OnClick -= HandleOnExitClick;
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
	}

	public void Init(TournamentDefinition definition)
	{
		_definition = definition;
		backgroundSprite.SetSprite(definition.tournamentSpriteCollection, definition.backgroundSpriteId);
		backgroundSprite.scale = new Vector3(2.2f, 2.2f, 1f);
		Vector2 scaledResolution = GUICamera.instance.GetComponent<tk2dCamera>().ScaledResolution;
		float y = backgroundSprite.GetCurrentSpriteDef().GetUntrimmedBounds().size.y;
		backgroundYLimit = y * backgroundSprite.scale.y - scaledResolution.y;
		header.bannerSprite.SetSprite(definition.tournamentSpriteCollection, definition.bannerSpriteId);
		header.bannerTopperSprite.SetSprite(definition.tournamentSpriteCollection, definition.bannerTopperSpriteId);
		header.leftTrophySprite.SetSprite(definition.tournamentSpriteCollection, definition.trophySpriteId);
		header.rightTrophySprite.SetSprite(definition.tournamentSpriteCollection, definition.trophySpriteId);
		Material material = Resources.Load<Material>(definition.bannerTextMaterial);
		if (material != null)
		{
			header.titleText.fontSharedMaterial = material;
		}
		LocalizedText localizedText = header.titleText.gameObject.GetComponent<LocalizedText>();
		if (localizedText == null)
		{
			localizedText = header.titleText.gameObject.AddComponent<LocalizedText>();
		}
		localizedText.ignoreKey = true;
		localizedText.ForceInitFontProperties();
		header.titleText.text = ((!string.IsNullOrEmpty(definition.tournamentNameKey)) ? Language.Get(definition.tournamentNameKey, "MENUS,GUI") : definition.tournamentName);
		playerPortrait = ObjectPool.GetObject<TournamentPlayerPortrait>("Tournament_Player_Portrait");
		playerPortrait.transform.parent = scrollableArea.contentContainer.transform;
		playerPortrait.transform.localPosition = new Vector3(0f, contentStartYPos - contentSpacing * (float)(definition.rounds.Count - (TournamentTracker.lastRoundCompleted + 2)), 0f);
		enemyPortraits.Clear();
		float num = 0f - contentStartYPos;
		TournamentRound tournamentRound = null;
		TournamentEnemyPortrait tournamentEnemyPortrait = null;
		Vector3 localPosition = new Vector3(0f, contentStartYPos, 0f);
		int count = definition.rounds.Count;
		for (int i = 0; i < count; i++)
		{
			tournamentRound = definition.rounds[i];
			tournamentEnemyPortrait = ObjectPool.GetObject<TournamentEnemyPortrait>("Tournament_Enemy_Portrait");
			tournamentEnemyPortrait.transform.parent = scrollableArea.contentContainer.transform;
			tournamentEnemyPortrait.transform.localPosition = localPosition;
			tournamentEnemyPortrait.Init(definition, tournamentRound, i == 0);
			if (tournamentRound.type == TournamentRoundType.Battle)
			{
				if (i > TournamentTracker.currentRoundIndex)
				{
					tournamentEnemyPortrait.portraitAnimator.Play("Lose_Idle");
				}
				else if (i < TournamentTracker.currentRoundIndex)
				{
					tournamentEnemyPortrait.portraitAnimator.Play((i != 0) ? "Idle_Pre" : "Idle_Pre_Champ");
				}
			}
			else if (i > TournamentTracker.currentRoundIndex)
			{
				tournamentEnemyPortrait.tofuAnimator.Play("Outro", 0, 1f);
				tournamentEnemyPortrait.tofuBackingAnimator.Play("Outro", 0, 1f);
			}
			enemyPortraits.Add(tournamentEnemyPortrait);
			localPosition.y -= contentSpacing;
			num += contentSpacing;
		}
		scrollableArea.ContentLength = num + contentBottomPadding;
		scrollableArea.backgroundUIItem.enabled = false;
		nextButton.enabled = false;
		exitButton.enabled = false;
		nextBattleDefinition = new BattleDefinition(BattleType.Tournament);
		if (TournamentTracker.isTournamentStarted)
		{
			StartCoroutine(PlayReturn());
		}
		else
		{
			StartCoroutine(PlayTournamentIntro());
		}
	}

	private IEnumerator PlayTournamentIntro()
	{
		int len = Player.instance.activeParty.Count;
		for (int i = 0; i < len; i++)
		{
			Player.instance.activeParty[i].hitPoints = Player.instance.activeParty[i].maxHitPoints;
		}
		TournamentTracker.StartTournament();
		playerPortrait.gameObject.SetActive(false);
		len = enemyPortraits.Count;
		for (int j = 0; j < len; j++)
		{
			enemyPortraits[j].gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(WaitForVictorySound(GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_intro", GFAudioManager.SFX_GROUP, string.Empty)));
		header.gameObject.SetActive(true);
		while (header.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		if (_definition.preConversation != null)
		{
			DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default(Vector3), false);
			dialogueSystem.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -400f);
			dialogueSystem.gameObject.SetActive(true);
			dialogueSystem.Init(GUICamera.instance.camera, _definition.preConversation, null, null, false);
			while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
			{
				yield return null;
			}
		}
		for (int k = 0; k < len; k++)
		{
			enemyPortraits[k].gameObject.SetActive(true);
			if (enemyPortraits[k].round.type == TournamentRoundType.Battle)
			{
				enemyPortraits[k].portraitAnimator.Play((k != 0) ? "Idle_Pre" : "Idle_Pre_Champ");
			}
			enemyPortraits[k].transform.DOScale(new Vector3(0.2f, 0.2f, 1f), 0.35f).From().SetEase(Ease.OutBack);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.25f);
		Tweener tween = DOTween.To(() => scrollableArea.Value, (float x) =>
		{
			scrollableArea.Value = x;
		}, 1f, 0.6f).SetEase(Ease.InOutSine).SetSpeedBased();
		bool canceledByTap = false;
		while (tween.IsPlaying())
		{
			if (Input.GetMouseButtonDown(0))
			{
				tween.Complete();
				canceledByTap = true;
				break;
			}
			yield return null;
		}
		playerPortrait.gameObject.SetActive(true);
		tween = playerPortrait.transform.DOScale(new Vector3(0.2f, 0.2f, 1f), 0.35f).From().SetEase(Ease.OutBack);
		if (canceledByTap)
		{
			tween.Complete();
		}
		else
		{
			yield return tween.WaitForCompletion();
		}
		ActivateNextRound();
	}

	private IEnumerator PlayReturn()
	{
		header.gameObject.SetActive(true);
		header.animator.Play("Intro", 0, 1f);
		backgroundMusic.Play();
		int roundIndex = _definition.rounds.Count - (TournamentTracker.lastRoundCompleted + 1) - 1;
		scrollableArea.Value = (float)roundIndex / (float)_definition.rounds.Count;
		yield return new WaitForSeconds(1f);
		if (BattleController.battleResult.isVictory)
		{
			enemyPortraits[roundIndex].portraitAnimator.Play("Lose");
			yield return null;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_opponentportraitdefeated", GFAudioManager.SFX_GROUP, string.Empty);
			while (enemyPortraits[roundIndex].portraitAnimator.GetCurrentAnimatorStateInfo(0).IsName("Lose"))
			{
				yield return null;
			}
			TournamentTracker.CompleteCurrentRound();
			if (TournamentTracker.isTournamentComplete)
			{
				StartCoroutine(PlayTournamentVictory());
			}
			else
			{
				StartCoroutine(MoveToNextRound());
			}
		}
		else
		{
			playerPortrait.animator.Play("Lose");
			yield return null;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_opponentportraitdefeated", GFAudioManager.SFX_GROUP, string.Empty);
			while (playerPortrait.animator.GetCurrentAnimatorStateInfo(0).IsName("Lose"))
			{
				yield return null;
			}
			yield return new WaitForSeconds(1f);
			TournamentTracker.FinishTournament();
			isDebugTournament = false;
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
	}

	public void ForceWinTournament()
	{
		StartCoroutine(PlayTournamentVictory());
	}

	private IEnumerator PlayTournamentVictory()
	{
		victoryScreen.trophySprite.SetSprite(_definition.tournamentSpriteCollection, _definition.trophySpriteId);
		victoryScreen.gameObject.SetActive(true);
		backgroundMusic.Stop();
		StartCoroutine(WaitForVictorySound(GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_overallvictory", GFAudioManager.SFX_GROUP, string.Empty)));
		while (victoryScreen.animator.GetCurrentAnimatorStateInfo(0).IsName("Intro"))
		{
			yield return null;
		}
		if (Player.instance.activeParty.Find((Character x) => !x.characterDefinition.tags.Contains("80s") && !x.characterDefinition.tags.Contains("Radical")) == null)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_THROWBACKTITANS");
		}
		if (Player.instance.activeParty.Find((Character x) => x.characterDefinition.characterClass != CharacterClass.Cute) == null)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_LEAGUEOFCUTENESS");
		}
		if (Player.instance.activeParty.Find((Character x) => !x.characterDefinition.tags.Contains("Villain")) == null)
		{
			AchievementsDatabase.UnlockAchievementId("TT2_TEENYMEANIES");
		}
		yield return new WaitForSeconds(0.5f);
		if (!isDebugTournament)
		{
			CommunicatorManager.instance.UpdateMissionWithTargetMessage(_definition.tournamentName);
		}
		yield return new WaitForSeconds(1f);
		if (_definition.postConversation != null)
		{
			DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default(Vector3), false);
			dialogueSystem.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -550f);
			dialogueSystem.gameObject.SetActive(true);
			dialogueSystem.Init(GUICamera.instance.camera, _definition.postConversation, null, null, false);
			while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
			{
				yield return null;
			}
		}
		yield return StartCoroutine(ProcessRewards());
		if (!isDebugTournament)
		{
			Player.instance.AddCompletedTournament(_definition);
		}
		yield return new WaitForSeconds(1f);
		victoryScreen.animator.Play("Outro");
		yield return null;
		while (victoryScreen.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		TournamentTracker.FinishTournament();
		isDebugTournament = false;
		LoadingScreenCover.LoadScene(CityController.previousCityScene);
	}

	private IEnumerator WaitForVictorySound(AudioSource source)
	{
		while (source.isPlaying)
		{
			yield return null;
		}
		backgroundMusic.Play();
	}

	private IEnumerator MoveToNextRound()
	{
		int roundIndex = TournamentTracker.currentRoundIndex;
		TournamentEnemyPortrait enemyPortrait = enemyPortraits[roundIndex];
		Sequence seq = DOTween.Sequence();
		seq.Append(playerPortrait.transform.DOLocalMoveY(enemyPortrait.transform.localPosition.y, 0.85f).SetEase(Ease.InOutSine));
		seq.Insert(0f, DOTween.To(() => scrollableArea.Value, (float x) =>
		{
			scrollableArea.Value = x;
		}, (float)roundIndex / (float)_definition.rounds.Count, 1.15f).SetEase(Ease.InOutSine));
		seq.Play();
		yield return seq.WaitForCompletion();
		ActivateNextRound();
	}

	private void ActivateNextRound()
	{
		switch (TournamentTracker.currentRound.type)
		{
		case TournamentRoundType.Tofu:
			StartCoroutine(ActivateTofu());
			break;
		default:
			StartCoroutine(ActivateBattle());
			break;
		}
	}

	private IEnumerator ActivateBattle()
	{
		TournamentEnemyPortrait enemyPortrait = enemyPortraits[TournamentTracker.currentRoundIndex];
		string revealState = ((TournamentTracker.currentRoundIndex != 0) ? "Reveal" : "Reveal_Champ");
		enemyPortrait.portraitAnimator.Play(revealState);
		yield return null;
		while (enemyPortrait.portraitAnimator.GetCurrentAnimatorStateInfo(0).IsName(revealState))
		{
			yield return null;
		}
		if (enemyPortrait.round.preBattleConversation != null)
		{
			yield return new WaitForSeconds(0.5f);
			DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default(Vector3), false);
			dialogueSystem.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -400f);
			dialogueSystem.gameObject.SetActive(true);
			dialogueSystem.Init(GUICamera.instance.camera, enemyPortrait.round.preBattleConversation, null, null, false);
			while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
			{
				yield return null;
			}
		}
		playerPortrait.animator.Play("Next_VS");
		yield return null;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_versuslabel", GFAudioManager.SFX_GROUP, string.Empty);
		while (playerPortrait.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		nextButton.gameObject.SetActive(true);
		exitButton.gameObject.SetActive(true);
		Tweener tween = nextButton.transform.DOScale(new Vector3(0.2f, 0.2f, 1f), 0.35f).From().SetEase(Ease.OutBack);
		exitButton.transform.DOScale(new Vector3(0.2f, 0.2f, 1f), 0.35f).From().SetEase(Ease.OutBack);
		yield return tween.WaitForCompletion();
		scrollableArea.backgroundUIItem.enabled = true;
		nextButton.enabled = true;
		exitButton.enabled = true;
	}

	private IEnumerator ActivateTofu()
	{
		TournamentEnemyPortrait enemyPortrait = enemyPortraits[TournamentTracker.currentRoundIndex];
		enemyPortrait.transform.localPosition += new Vector3(0f, 0f, -100f);
		enemyPortrait.tofuBackingAnimator.transform.parent = GUICamera.instance.anchorMiddleCenter;
		Vector3 newPosition = enemyPortrait.tofuBackingAnimator.transform.localPosition;
		newPosition.y = 0f;
		enemyPortrait.tofuBackingAnimator.transform.localPosition = newPosition;
		enemyPortrait.tofuAnimator.Play("Activate");
		enemyPortrait.tofuBackingAnimator.gameObject.SetActive(true);
		yield return null;
		yield return new WaitForSeconds(1f / 3f);
		enemyPortrait.tofuAnimator.transform.parent = GUICamera.instance.anchorMiddleCenter;
		enemyPortrait.tofuAnimator.transform.DOLocalMoveY(-17f, 1f / 3f).SetEase(Ease.InOutSine);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_tofuintro", GFAudioManager.SFX_GROUP, string.Empty);
		while (enemyPortrait.tofuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		enemyPortrait.tofuSpriteRoulette.StartRoulette();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_rouletteloop", GFAudioManager.SFX_GROUP, "roulette", 1f, 0f, true);
		while (!Input.GetMouseButtonDown(0))
		{
			yield return null;
		}
		enemyPortrait.tofuSpriteRoulette.StopRoulette();
		GFAudioManager.instance.StopById("roulette");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_roulettestop", GFAudioManager.SFX_GROUP, string.Empty);
		List<TournamentTofuType> exclusions = new List<TournamentTofuType>();
		if (Player.instance.allPartyAlive)
		{
			exclusions.Add(TournamentTofuType.Revive);
		}
		int totalWeight = 0;
		int len = enemyPortrait.round.tofuData.Count;
		for (int i = 0; i < len; i++)
		{
			if (!exclusions.Contains((TournamentTofuType)i))
			{
				totalWeight += enemyPortrait.round.tofuData[i].weight;
			}
		}
		TournamentTofuType tofuType = TournamentTofuType.Revive;
		TournamentTofuData tofuData = null;
		int randomValue = Random.Range(0, totalWeight);
		int checkValue = 0;
		for (int j = 0; j < len; j++)
		{
			if (!exclusions.Contains((TournamentTofuType)j))
			{
				checkValue += enemyPortrait.round.tofuData[j].weight;
				if (randomValue < checkValue)
				{
					tofuType = (TournamentTofuType)j;
					tofuData = enemyPortrait.round.tofuData[j];
					break;
				}
			}
		}
		enemyPortrait.tofuSprite.SetSprite(tofuSpriteNames[tofuType]);
		enemyPortrait.tofuHeaderText.text = Language.Get(tofuNames[tofuType], "MENUS,GUI");
		enemyPortrait.tofuDescriptionText.text = Language.Get(tofuDescriptions[tofuType], "MENUS,GUI");
		int tofuNumber = Random.Range(tofuData.minValue, tofuData.maxValue + 1);
		enemyPortrait.tofuNumberText.text = tofuNumber.ToString();
		enemyPortrait.tofuNumberText.gameObject.SetActive(tofuType != TournamentTofuType.Revive);
		if (tofuType == TournamentTofuType.TeamHeal || tofuType == TournamentTofuType.Spicy)
		{
			enemyPortrait.chooseGrp.SetActive(false);
		}
		enemyPortrait.tofuAnimator.Play("Selected");
		yield return null;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_tofutextappear", GFAudioManager.SFX_GROUP, string.Empty, 1f, 1f);
		while (enemyPortrait.tofuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		tofuTarget = null;
		switch (tofuType)
		{
		case TournamentTofuType.Revive:
			yield return StartCoroutine(ApplyReviveTofu(tofuNumber));
			break;
		case TournamentTofuType.SingleHeal:
			yield return StartCoroutine(ApplySingleHealTofu(tofuNumber));
			break;
		case TournamentTofuType.TeamHeal:
			yield return StartCoroutine(ApplyTeamHealTofu(tofuNumber));
			break;
		case TournamentTofuType.Spicy:
			yield return StartCoroutine(ApplySpicyTofu(tofuNumber));
			break;
		}
		enemyPortrait.tofuAnimator.Play("Outro");
		enemyPortrait.tofuBackingAnimator.Play("Outro");
		yield return null;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_tofupartend", GFAudioManager.SFX_GROUP, string.Empty);
		while (enemyPortrait.tofuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		enemyPortrait.tofuBackingAnimator.gameObject.SetActive(false);
		enemyPortrait.transform.localPosition += new Vector3(0f, 0f, 100f);
		enemyPortrait.tofuAnimator.transform.parent = enemyPortrait.transform;
		enemyPortrait.tofuBackingAnimator.transform.parent = enemyPortrait.transform;
		TournamentTracker.CompleteCurrentRound();
		StartCoroutine(MoveToNextRound());
	}

	private IEnumerator ApplyReviveTofu(int value)
	{
		mySquad.ShowNow();
		mySquad.CharacterSelected += HandleCharacterSelectedForReviveTofu;
		int len = mySquad.slots.Count;
		float startTime = 0f;
		for (int i = 0; i < len; i++)
		{
			if (mySquad.slots[i].character != null && mySquad.slots[i].character.isDead)
			{
				mySquad.slots[i].arrowGrp.SetActive(true);
				mySquad.slots[i].arrowAnimator.Play("Idle", 0, startTime);
				startTime += 0.075f;
			}
		}
		while (tofuTarget == null)
		{
			yield return null;
		}
		len = mySquad.slots.Count;
		for (int j = 0; j < len; j++)
		{
			mySquad.slots[j].arrowGrp.SetActive(false);
		}
		tofuTarget.hitPoints = tofuTarget.maxHitPoints;
		yield return new WaitForSeconds(0.5f);
		mySquad.HideNow();
	}

	private IEnumerator ApplySingleHealTofu(int value)
	{
		mySquad.ShowNow();
		mySquad.CharacterSelected += HandleCharacterSelectedForHealTofu;
		int len = mySquad.slots.Count;
		float startTime = 0f;
		for (int i = 0; i < len; i++)
		{
			if (mySquad.slots[i].character != null)
			{
				mySquad.slots[i].arrowGrp.SetActive(true);
				mySquad.slots[i].arrowAnimator.Play("Idle", 0, startTime);
				startTime += 0.075f;
			}
		}
		while (tofuTarget == null)
		{
			yield return null;
		}
		len = mySquad.slots.Count;
		for (int j = 0; j < len; j++)
		{
			mySquad.slots[j].arrowGrp.SetActive(false);
		}
		int hitPointsToApply = value;
		while (hitPointsToApply > 0)
		{
			if (tofuTarget == null)
			{
				Debug.LogError("Tofu Target is NULL! Cancelling single heal tofu.");
				yield break;
			}
			tofuTarget.hitPoints++;
			hitPointsToApply--;
			if (GFAudioManager.instance != null)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_healthcountup_loop", GFAudioManager.SFX_GROUP, string.Empty);
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		mySquad.HideNow();
	}

	private IEnumerator ApplyTeamHealTofu(int value)
	{
		mySquad.ShowNow();
		yield return new WaitForSeconds(1f);
		int len = mySquad.slots.Count;
		int hitPointsToApply = value;
		while (hitPointsToApply > 0)
		{
			for (int i = 0; i < len; i++)
			{
				if (mySquad.slots[i].character != null && !mySquad.slots[i].character.isDead)
				{
					mySquad.slots[i].character.hitPoints++;
				}
			}
			if (GFAudioManager.instance != null)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_tournament_healthcountup_loop", GFAudioManager.SFX_GROUP, string.Empty);
			}
			hitPointsToApply--;
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		mySquad.HideNow();
	}

	private IEnumerator ApplySpicyTofu(int value)
	{
		nextBattleDefinition.startingAttackBonus = value;
		while (!Input.GetMouseButtonDown(0))
		{
			yield return null;
		}
	}

	private IEnumerator ProcessRewards()
	{
		if (!string.IsNullOrEmpty(_definition.achievementReward))
		{
			AchievementsDatabase.UnlockAchievementId(_definition.achievementReward);
		}
		if (_definition.figureRewards.Count > 0)
		{
			figureRevealCamera.enabled = true;
			CharacterRewardData rewardData = null;
			CharacterDefinition character = null;
			int len = _definition.figureRewards.Count;
			for (int i = 0; i < len; i++)
			{
				rewardData = _definition.figureRewards[i];
				character = GameManager.instance.characterDatabase.GetCharacter(rewardData.characterUID);
				Player.instance.AddCharacter(CharacterRoller.RollCharacterData(character, -1, character.uid, rewardData.characterLvl));
				Player.instance.AddToParty(Player.instance.characters.GetLastItem());
				yield return StartCoroutine(RevealFigure(new ShopFigure
				{
					level = rewardData.characterLvl,
					characterUID = rewardData.characterUID
				}));
			}
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
			figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Outro");
			yield return null;
			while (figureReveal.figureRevealAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || figureReveal.figureRevealScreenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			figureRevealCamera.enabled = false;
		}
	}

	private IEnumerator RevealFigure(ShopFigure figureData)
	{
		bool firstReveal = !figureReveal.gameObject.activeSelf;
		if (!firstReveal)
		{
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
			yield return new WaitForEndOfFrame();
			while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		figureReveal.figureRevealBoxGroupAnimator.Play("empty");
		figureReveal.newFigureBacking.SetActive(true);
		figureReveal.RemoveBlindBox();
		if (figureData == null)
		{
			yield break;
		}
		CharacterDefinition currentRevealCharacterDef = GameManager.instance.characterDatabase.GetCharacter(figureData.characterUID);
		bool isNewFigure = !Player.instance.HasAcquiredCharacter(currentRevealCharacterDef);
		figureReveal.figureRevealToyBox.figureData = figureData;
		figureReveal.figureRevealToyBox.UpdateBoxForCharacter(currentRevealCharacterDef, true);
		figureReveal.newFigureBacking.SetActive(isNewFigure);
		Player.instance.AcquireCharacter(figureData.characterUID);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.SetActive(false);
		}
		yield return new WaitForSeconds(0.2f);
		if (firstReveal)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_firstfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_subsequentfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		}
		figureReveal.gameObject.SetActive(true);
		figureReveal.figuresDiscoveredAmt.transform.parent.gameObject.SetActive(isNewFigure);
		figureReveal.figuresDiscoveredAmt.text = string.Format("<color=\"orange\">{0}<color=\"white\">/{1}", Mathf.Max(0, Player.instance.charactersAcquired.Count - 1), GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance));
		figureReveal.figuredRevealName.text = ((!string.IsNullOrEmpty(currentRevealCharacterDef.characterNameKey)) ? Language.Get(currentRevealCharacterDef.characterNameKey, "NPCS,FIGS") : currentRevealCharacterDef.characterName);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.transform.DOKill();
			figureReveal.figureRevealToyBox.figure.transform.localEulerAngles = new Vector3(currentRevealCharacterDef.shopFigureRotationOffset.x, 180f + currentRevealCharacterDef.shopFigureRotationOffset.y, currentRevealCharacterDef.shopFigureRotationOffset.z);
		}
		if (!firstReveal)
		{
			while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_Outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Intro");
		}
		else
		{
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Intro");
			figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Intro");
		}
		yield return new WaitForSeconds(0.1f);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.SetActive(true);
		}
		figureReveal.waitToOpenBox = true;
		float lastTimeWigglePlayed = Time.time;
		while (figureReveal.waitToOpenBox)
		{
			yield return new WaitForEndOfFrame();
		}
		figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Tap");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_open", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(1.6f);
		if (!string.IsNullOrEmpty(currentRevealCharacterDef.shopRevealNameSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(currentRevealCharacterDef.shopRevealNameSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		}
		yield return new WaitForSeconds(0.6f);
		figureReveal.figuresDiscoveredAmt.transform.parent.gameObject.SetActive(isNewFigure);
		figureReveal.figuresDiscoveredAmt.text = string.Format("<color=\"orange\">{0}<color=\"white\">/{1}", Player.instance.charactersAcquired.Count, GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance));
		figureReveal.figureRevealToyBox.figure.transform.DOLocalRotate(new Vector3(0f, 900f, 0f), 25f, RotateMode.FastBeyond360);
		while (!Input.GetMouseButtonDown(0))
		{
			yield return null;
		}
	}

	private void HandleCharacterSelectedForReviveTofu(Character character)
	{
		if (character.isDead)
		{
			mySquad.CharacterSelected -= HandleCharacterSelectedForReviveTofu;
			tofuTarget = character;
		}
	}

	private void HandleCharacterSelectedForHealTofu(Character character)
	{
		if (!character.isDead)
		{
			mySquad.CharacterSelected -= HandleCharacterSelectedForHealTofu;
			tofuTarget = character;
		}
	}

	private void HandleOnScroll(tk2dUIScrollableArea obj)
	{
		Vector3 localPosition = backgroundSprite.transform.localPosition;
		localPosition.y = backgroundYLimit * obj.Value;
		backgroundSprite.transform.localPosition = localPosition;
	}

	private void HandleOnNextClick()
	{
		if (clickedNext)
		{
			return;
		}
		clickedNext = true;
		TournamentRound currentRound = TournamentTracker.currentRound;
		NPCDefinition nPCDef = GameManager.instance.npcDatabase.GetNPCDef(currentRound.opponentNPCIndex);
		nextBattleDefinition.enemyName = ((!string.IsNullOrEmpty(nPCDef.nameKey)) ? Language.Get(nPCDef.nameKey, "NPCS,FIGS") : nPCDef.name);
		nextBattleDefinition.enemyDefeatedMsg = nPCDef.name;
		nextBattleDefinition.enemyPortraitSprite = ((!string.IsNullOrEmpty(nPCDef.preBattleSpriteRight)) ? nPCDef.preBattleSpriteRight : nPCDef.preBattleSprite);
		nextBattleDefinition.enemyCharacterName = nPCDef.characterType;
		nextBattleDefinition.enemyGUID = nPCDef.guid;
		nextBattleDefinition.coinReward = currentRound.coinReward;
		nextBattleDefinition.xpReward = currentRound.xpReward;
		bool flag = false;
		ChallengeModeManager.isNonJLChallenge = false;
		if (currentRound.challengesOn)
		{
			List<ChallengeModeDefinition> items = GameManager.instance.challengeModeDatabase.GetItems(currentRound.challengesIncluded);
			if (items != null && items.Count > 0)
			{
				ChallengeModeDefinition challengeModeDefinition = items[Random.Range(0, items.Count)];
				if (challengeModeDefinition != null)
				{
					flag = true;
					ChallengeModeManager.useTestMode = true;
					ChallengeModeManager.testMode = challengeModeDefinition.challengeModeType;
					ChallengeModeManager.modeDefinition = challengeModeDefinition;
					ChallengeModeManager.currentModeValues = challengeModeDefinition.easyModeValues;
					ChallengeModeManager.challengeModeDirty = true;
					ChallengeModeManager.challengeModeHard = false;
					ChallengeModeManager.isChallengeModeActive = true;
					ChallengeModeManager.isNonJLChallenge = true;
				}
			}
		}
		if (!flag)
		{
			ChallengeModeManager.isChallengeModeActive = false;
			ChallengeModeManager.modeDefinition = null;
		}
		if (!string.IsNullOrEmpty(currentRound.battleThemePath))
		{
			nextBattleDefinition.battleThemePath = currentRound.battleThemePath;
		}
		else
		{
			nextBattleDefinition.battleThemePath = "Audio/Music/music_battle-tournament";
		}
		nextBattleDefinition.playerAIType = currentRound.playerAIType;
		if (initDefinition != null)
		{
			nextBattleDefinition.cityName = initDefinition.cityName;
			nextBattleDefinition.districtLocation = initDefinition.district;
		}
		int count = currentRound.figures.Count;
		for (int i = 0; i < count; i++)
		{
			nextBattleDefinition.enemyTeam.Add(new Character(i, currentRound.figures[i]));
		}
		nextBattleDefinition.enemyAccessoryUID = currentRound.accessoryUID;
		PreBattleController.battleDefinition = nextBattleDefinition;
		blackness.gameObject.GetComponent<MeshRenderer>().enabled = true;
		DOTween.ToAlpha(() => blackness.color, (Color x) =>
		{
			blackness.color = x;
		}, 1f, 0.3f).SetEase(Ease.InCubic).OnComplete(() =>
		{
			LoadingScreenCover.LoadScene("PreBattle", false);
		});
	}

	private void HandleOnExitClick()
	{
		StartCoroutine(ShowWarningDialog());
	}

	private IEnumerator ShowWarningDialog()
	{
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default(Vector3), false);
		dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
		dialog.transform.localPosition = new Vector3(0f, 0f, -500f);
		dialog.descriptionText.text = Language.Get("TOURNYEXITWARNING", "MENUS,GUI");
		dialog.gameObject.SetActive(true);
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			TournamentTracker.FinishTournament();
			isDebugTournament = false;
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		TournamentTracker.FinishTournament();
		isDebugTournament = false;
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}
}
