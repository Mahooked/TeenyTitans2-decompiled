using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ChallengeController : MonoBehaviour
{
	public bool useTestMode;

	public ChallengeModeType testMode;

	public Camera guiCamera;

	public Transform dialogueSystemParent;

	public DialogueTree dialogueSystem;

	public CityReward cityReward;

	public tk2dSlicedSpriteHelper rankFillSprite;

	public tk2dUIItem easyButton;

	public tk2dUIItem hardButton;

	public GameObject exitButton;

	public GameObject towerButton;

	public GameObject looseChangeGroup;

	public GameObject rankBar;

	public TextMeshPro playerCoins;

	public TextMeshPro rankNumber;

	public TextMeshPro rankName;

	public TextMeshPro coinReward;

	public TextMeshPro justicePointsReward;

	public TextMeshPro hardBonusText;

	public TextMeshPro consecutiveBonusReward;

	public TextMeshPro consecutiveBonusText;

	public GameObject rewardBox;

	public GameObject rewardBoxNextBtn;

	public GameObject rewardBoxHardNoBonus;

	public GameObject rewardBoxHardBonus;

	public GameObject rewardBoxConsecutiveNoBonus;

	public GameObject rewardBoxConsecutiveBonus;

	public tk2dSprite rankBadgeSprite;

	public tk2dSprite rankRewardSprite;

	public tk2dSprite blackFadeSprite;

	public tk2dSprite blackFadeRewardsSprite;

	public GameObject lockedGroup;

	public GameObject backgroundObjects;

	public Animator animatorEasyDoor;

	public Animator animatorHardDoor;

	public Animator animatorRankBar;

	public Animator animatorChallengeRewardBox;

	public ParticleSystem coinParticle;

	public ParticleSystem starParticle1;

	public ParticleSystem starParticle2;

	public ParticleSystem starParticle3;

	public ParticleSystem fillBarStarParticle;

	public ParticleAttractor particleAttractorCoins;

	public ParticleAttractor particleAttractorStars;

	private float fullBarWidth = 244f;

	private float lastJusticePointsAdded;

	private int currentMeter;

	private int currentCoins;

	private bool usedExitButton;

	private bool fillingBar;

	private ParticleSystem ps;

	private const string PLAYER_JUSTICE_POINTS = "PLAYER_JUSTICE_POINTS";

	private const string PLAYER_JUSTICE_LEVEL = "PLAYER_JUSTICE_LEVEL";

	public AudioClip fillLoop;

	private JusticePointsLevelData highestJusticeLevelData;

	private bool startedCoinUpdated;

	private Conversation queueConvo;

	private float _maxScaleFactor = 1f;

	public float maxScaleFactor
	{
		get
		{
			return _maxScaleFactor;
		}
		set
		{
			if (value != _maxScaleFactor)
			{
				_maxScaleFactor = value;
				if (rankFillSprite.width > fullBarWidth * _maxScaleFactor)
				{
					rankFillSprite.width = fullBarWidth * _maxScaleFactor;
				}
			}
		}
	}

	private int PlayerRankLevel()
	{
		return SaveUtility.GetInt("PLAYER_JUSTICE_LEVEL", 1);
	}

	private IEnumerator Start()
	{
		Application.targetFrameRate = 60;
		ChallengeModeManager.isChallengeModeActive = false;
		SaveUtility.SetInt("GAME_CHALLENGE_IN_PROGRESS", 0);
		if (SaveUtility.GetInt("PLAYER_JUSTICE_LEVEL") <= 0)
		{
			SaveUtility.SetInt("PLAYER_JUSTICE_LEVEL", 1);
		}
		CheckHighestJusticeLevel();
		IsHardModeUnlock();
		currentMeter = SaveUtility.GetInt("PLAYER_JUSTICE_POINTS");
		float targetWidth = Mathf.Min(1f, (float)currentMeter / (float)GetRankFullMeter()) * fullBarWidth;
		rankFillSprite.width = targetWidth;
		rankNumber.text = PlayerRankLevel().ToString();
		rankName.text = PlayerRankName();
		rankBadgeSprite.SetSprite(PlayerRankBadge());
		rankRewardSprite.SetSprite(NextRewardSprite());
		currentCoins = Player.instance.currency;
		playerCoins.text = currentCoins.ToString();
		RefreshRankBar();
		rankBar.transform.localPosition = new Vector3(0f, -300f, -85f);
		rankBar.transform.DOLocalMove(new Vector3(0f, 0f, -85f), 0.3f).SetEase(Ease.OutSine);
		animatorRankBar.Play("JLA_rankUp_idle", 0, 1f);
		yield return new WaitForEndOfFrame();
		looseChangeGroup.SetActive(value: false);
		Player.instance.HealActiveParty();
		if (SaveUtility.GetInt("GAME_PLAYED_CHALLENGE_INTRO") == 0)
		{
			while (!LoadingScreenCover.allowInit)
			{
				yield return null;
			}
			SaveUtility.SetInt("GAME_PLAYED_CHALLENGE_INTRO", 1);
			yield return StartCoroutine(PlayDialogue("CONVO_JLA_INTRO"));
		}
		if (ChallengeModeManager.challengeWon && ChallengeModeManager.modeDefinition != null)
		{
			ChallengeModeManager.challengeWon = false;
			StartCoroutine(BringInCharacters());
			yield return new WaitForSeconds(0.5f);
			yield return StartCoroutine("RewardChallengeWon");
		}
	}

	private void CheckHighestJusticeLevel()
	{
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		int num = -1;
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if (justicePointsLevelData.high >= num)
			{
				num = justicePointsLevelData.high;
				highestJusticeLevelData = justicePointsLevelData;
			}
		}
	}

	private bool IsHardModeUnlock()
	{
		if (SaveUtility.GetInt("PLAYER_JUSTICE_LEVEL") >= 5)
		{
			lockedGroup.SetActive(value: false);
			return true;
		}
		lockedGroup.SetActive(value: true);
		hardButton.enabled = false;
		return false;
	}

	private void Update()
	{
		if (rankFillSprite.slicedSprite.dimensions.x < 70f)
		{
			rankFillSprite.slicedSprite.borderLeft = 19f / 136f;
			rankFillSprite.slicedSprite.borderRight = 35f / 136f;
		}
		else
		{
			rankFillSprite.slicedSprite.borderLeft = 90f / 255f;
			rankFillSprite.slicedSprite.borderRight = 135f / 255f;
		}
		if (rewardBox != null)
		{
			rewardBox.transform.position = new Vector3(rewardBox.transform.position.x, -30f, rewardBox.transform.position.z);
		}
	}

	private IEnumerator RewardChallengeWon()
	{
		int currentWins = SaveUtility.GetInt("GAME_CHALLENGE_CONSECUTIVE_WINS") + 1;
		SaveUtility.SetInt("GAME_CHALLENGE_CONSECUTIVE_WINS", currentWins);
		ChallengeModeDefinition modeDefinition = ChallengeModeManager.modeDefinition;
		ChallengeModeValues modeValues = ChallengeModeManager.currentModeValues;
		exitButton.SetActive(value: false);
		towerButton.SetActive(value: false);
		looseChangeGroup.SetActive(value: true);
		rewardBoxNextBtn.SetActive(value: false);
		easyButton.enabled = false;
		hardButton.enabled = false;
		coinReward.gameObject.SetActive(value: false);
		justicePointsReward.gameObject.SetActive(value: false);
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		int currentJusticePoints = SaveUtility.GetInt("PLAYER_JUSTICE_POINTS");
		currentJusticePoints += modeValues.justicePoints;
		SaveUtility.SetInt("PLAYER_JUSTICE_POINTS", currentJusticePoints);
		Player.instance.AddCurrency(modeValues.coinReward);
		coinReward.text = modeValues.coinReward.ToString();
		justicePointsReward.text = modeValues.justicePoints.ToString();
		rewardBox.SetActive(value: true);
		yield return new WaitForSeconds(0.7f);
		coinParticle.transform.parent.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
		coinReward.gameObject.SetActive(value: true);
		coinReward.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
		startedCoinUpdated = false;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_resultsscreen_coinreward", GFAudioManager.SFX_GROUP, string.Empty);
		ps = coinParticle.GetComponent<ParticleSystem>();
		ps.maxParticles = 30;
		coinParticle.gameObject.SetActive(value: true);
		particleAttractorCoins.AttractParticleSystem(ps);
		particleAttractorCoins.ParticleDestroyedParticleSystem += HandleParticleDestroyed;
		yield return new WaitForSeconds(1f);
		starParticle1.maxParticles = 30;
		starParticle1.transform.parent.gameObject.SetActive(value: true);
		particleAttractorStars.AttractParticleSystem(starParticle1);
		starParticle1.transform.parent.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
		justicePointsReward.gameObject.SetActive(value: true);
		justicePointsReward.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_resultsscreen_bluestarreward", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(0.85f);
		while (fillingBar)
		{
			yield return null;
		}
		RefreshRankBar();
		yield return null;
		yield return new WaitForSeconds(0.3f);
		if (!ChallengeModeManager.challengeModeHard)
		{
			rewardBoxHardNoBonus.SetActive(value: true);
			rewardBoxHardNoBonus.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			rewardBoxHardBonus.SetActive(value: false);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_resultsscreen_bonusmissed", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_resultsscreen_bluestarreward", GFAudioManager.SFX_GROUP, string.Empty);
			rewardBoxHardBonus.SetActive(value: true);
			rewardBoxHardBonus.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			rewardBoxHardNoBonus.SetActive(value: false);
			hardBonusText.text = GameManager.instance.challengeModeDatabase.HardModeBonus.ToString();
			currentJusticePoints += GameManager.instance.challengeModeDatabase.HardModeBonus;
			SaveUtility.SetInt("PLAYER_JUSTICE_POINTS", currentJusticePoints);
			yield return new WaitForSeconds(0.1f);
			starParticle2.maxParticles = 30;
			starParticle2.transform.parent.gameObject.SetActive(value: true);
			particleAttractorStars.AttractParticleSystem(starParticle2);
			starParticle2.transform.parent.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			yield return new WaitForSeconds(0.85f);
			while (fillingBar)
			{
				yield return null;
			}
			RefreshRankBar();
		}
		yield return new WaitForSeconds(0.3f);
		if (currentWins <= 1)
		{
			rewardBoxConsecutiveNoBonus.SetActive(value: true);
			rewardBoxConsecutiveNoBonus.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_resultsscreen_bonusmissed", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_resultsscreen_bluestarreward", GFAudioManager.SFX_GROUP, string.Empty);
			rewardBoxConsecutiveBonus.SetActive(value: true);
			rewardBoxConsecutiveBonus.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			int adjustedConsecutiveWins = Mathf.Min(GameManager.instance.challengeModeDatabase.MaxConsecutiveWins, currentWins);
			currentJusticePoints += GameManager.instance.challengeModeDatabase.ConsecutiveBonusMultiplier * adjustedConsecutiveWins;
			SaveUtility.SetInt("PLAYER_JUSTICE_POINTS", currentJusticePoints);
			string adjustedWinsString = adjustedConsecutiveWins.ToString();
			if (currentWins > GameManager.instance.challengeModeDatabase.MaxConsecutiveWins)
			{
				adjustedWinsString = adjustedConsecutiveWins + "+";
			}
			consecutiveBonusText.text = "<size=6>x</size><size=10>" + adjustedWinsString + "</size>\n" + Language.Get("WINS", "MENUS,GUI");
			consecutiveBonusReward.text = string.Empty + GameManager.instance.challengeModeDatabase.ConsecutiveBonusMultiplier * adjustedConsecutiveWins;
			starParticle3.maxParticles = 30;
			starParticle3.transform.parent.gameObject.SetActive(value: true);
			particleAttractorStars.AttractParticleSystem(starParticle3);
			starParticle3.transform.parent.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			yield return new WaitForSeconds(0.85f);
			while (fillingBar)
			{
				yield return null;
			}
			RefreshRankBar();
		}
		yield return new WaitForSeconds(1f);
		while (fillingBar || cityReward.showingRewards)
		{
			yield return null;
		}
		RefreshRankBar();
		rewardBoxNextBtn.SetActive(value: true);
	}

	private void HandleParticleDestroyed(ParticleSystem psParticleDestroyed)
	{
		if (psParticleDestroyed != null && ps != null && psParticleDestroyed == ps && !startedCoinUpdated)
		{
			startedCoinUpdated = true;
			StartCoroutine("UpdatePlayerCoins");
		}
	}

	private bool isNewRankForLevel(int level)
	{
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if (level == justicePointsLevelData.low && justicePointsLevelData.newRank)
			{
				return true;
			}
		}
		return false;
	}

	private string PlayerRankName()
	{
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		int num = PlayerRankLevel();
		string result = "RANK1";
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if (num >= justicePointsLevelData.low && justicePointsLevelData.newRank)
			{
				result = Language.Get(justicePointsLevelData.rankNameKey, "MENUS,GUI");
			}
		}
		return result;
	}

	private string PlayerRankBadge()
	{
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		int num = PlayerRankLevel();
		string result = "jla_menu_rank-lilleaguer";
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if (num >= justicePointsLevelData.low && justicePointsLevelData.newRank)
			{
				result = justicePointsLevelData.rankBadgeSprite;
			}
		}
		return result;
	}

	private string NextRewardSprite()
	{
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		int num = PlayerRankLevel();
		string result = "jla_menu_bar_goldriddlerbox";
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if ((num < justicePointsLevelData.low || num > justicePointsLevelData.high) && (num < highestJusticeLevelData.high || justicePointsLevelData != highestJusticeLevelData))
			{
				continue;
			}
			for (int j = 0; j < justicePointsLevelData.rewards.Count; j++)
			{
				JusticePointsLevelReward justicePointsLevelReward = justicePointsLevelData.rewards[j];
				if (justicePointsLevelReward.rewardType == JusticePointsLevelUpRewardType.Figure)
				{
					if (justicePointsLevelReward.characterReward.blindBox)
					{
						result = "jla_menu_bar_riddlerbox";
					}
					if (justicePointsLevelReward.characterReward.goldBox)
					{
						result = "jla_menu_bar_goldriddlerbox";
					}
				}
			}
		}
		return result;
	}

	private void HandleOnRewardBoxButtonClick()
	{
		StartCoroutine("RewardBoxNextPressed");
	}

	private void HandleOnTowerButtonClick()
	{
		if (!usedExitButton)
		{
			usedExitButton = true;
			ChallengeModeManager.modeDefinition = null;
			ChallengeModeManager.currentModeValues = null;
			ChallengeModeManager.isChallengeModeActive = false;
			TitanTowerController.fromChallengeScene = true;
			LoadingScreenCover.LoadScene("TitanTower");
		}
	}

	private void HandleOnExitButtonClick()
	{
		if (!usedExitButton)
		{
			usedExitButton = true;
			ChallengeModeManager.modeDefinition = null;
			ChallengeModeManager.currentModeValues = null;
			ChallengeModeManager.isChallengeModeActive = false;
			CityController.fromChallengeScene = true;
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
	}

	private void HandleOnEasyModeButtonClick()
	{
		if (!usedExitButton)
		{
			usedExitButton = true;
			StartCoroutine("StartEasyMode");
		}
	}

	private void AnimateOutRankBar()
	{
		rankBar.gameObject.transform.DOMove(new Vector3(rankBar.gameObject.transform.position.x, rankBar.gameObject.transform.position.y - 500f, rankBar.gameObject.transform.position.z), 1f);
	}

	private void AnimateOutTopButtons()
	{
		exitButton.transform.DOMove(new Vector3(exitButton.transform.position.x, exitButton.transform.position.y + 500f, exitButton.transform.position.z), 1f);
		towerButton.transform.DOMove(new Vector3(exitButton.transform.position.x, exitButton.transform.position.y + 500f, exitButton.transform.position.z), 1f);
	}

	private IEnumerator StartEasyMode()
	{
		animatorEasyDoor.Play("JLA_normal_door_open");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_challengemode_DoorOpenNormal", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(0.3f);
		AnimateOutRankBar();
		AnimateOutTopButtons();
		yield return new WaitForSeconds(0.3f);
		float scale = 6f;
		blackFadeSprite.gameObject.SetActive(value: true);
		DOTween.ToAlpha(() => blackFadeSprite.color, (Color x) =>
		{
			blackFadeSprite.color = x;
		}, 1f, 0.4f).SetDelay(0.5f);
		guiCamera.transform.DOMove(new Vector3(animatorEasyDoor.transform.position.x * scale + 50f, guiCamera.transform.position.y, guiCamera.transform.position.z), 1f);
		backgroundObjects.transform.DOScale(new Vector3(scale, scale, scale), 0.8f).SetEase(Ease.Linear);
		yield return new WaitForEndOfFrame();
		while (animatorEasyDoor.NormalizedTime() < 0.9f)
		{
			yield return new WaitForEndOfFrame();
		}
		SaveUtility.SetInt("GAME_CHALLENGE_IN_PROGRESS", 1);
		BattleDefinition battleDefinition = ChallengeModeBattleRandomizor.GetBattleDefinitionForEasyMode();
		PreBattleController.battleDefinition = battleDefinition;
		ChallengeModeManager.isNonJLChallenge = false;
		ChallengeModeManager.challengeModeDirty = true;
		ChallengeModeManager.challengeModeHard = false;
		ChallengeModeManager.isChallengeModeActive = true;
		ChallengeModeManager.useTestMode = useTestMode;
		ChallengeModeManager.testMode = testMode;
		LoadingScreenCover.LoadScene("PreBattle", showLoadingScreen: false);
	}

	private void HandleOnHardModeButtonClick()
	{
		if (!usedExitButton)
		{
			usedExitButton = true;
			StartCoroutine("StartHardMode");
		}
	}

	private IEnumerator StartHardMode()
	{
		animatorHardDoor.Play("JLA_hard_door_open");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_challengemode_DoorOpenIntense", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(0.3f);
		AnimateOutRankBar();
		AnimateOutTopButtons();
		yield return new WaitForSeconds(0.3f);
		float scale = 6f;
		blackFadeSprite.gameObject.SetActive(value: true);
		DOTween.ToAlpha(() => blackFadeSprite.color, (Color x) =>
		{
			blackFadeSprite.color = x;
		}, 1f, 0.4f).SetDelay(0.5f);
		guiCamera.transform.DOMove(new Vector3(animatorHardDoor.transform.position.x * scale - 50f, guiCamera.transform.position.y, guiCamera.transform.position.z), 1f);
		backgroundObjects.transform.DOScale(new Vector3(scale, scale, scale), 0.8f).SetEase(Ease.Linear);
		while (animatorHardDoor.NormalizedTime() < 0.9f)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.3f);
		SaveUtility.SetInt("GAME_CHALLENGE_IN_PROGRESS", 1);
		BattleDefinition battleDefinition = ChallengeModeBattleRandomizor.GetBattleDefinitionForHardMode();
		PreBattleController.battleDefinition = battleDefinition;
		ChallengeModeManager.isNonJLChallenge = false;
		ChallengeModeManager.challengeModeDirty = true;
		ChallengeModeManager.challengeModeHard = true;
		ChallengeModeManager.isChallengeModeActive = true;
		LoadingScreenCover.LoadScene("PreBattle", showLoadingScreen: false);
	}

	private int GetRankFullMeter()
	{
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		int num = PlayerRankLevel();
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if ((num >= justicePointsLevelData.low && num <= justicePointsLevelData.high) || (num >= highestJusticeLevelData.high && justicePointsLevelData == highestJusticeLevelData))
			{
				return justicePointsLevelData.amount;
			}
		}
		return 500;
	}

	private void RefreshRankBar()
	{
		if (!fillingBar)
		{
			StartCoroutine("FillBar");
		}
	}

	private IEnumerator FillBar()
	{
		while (cityReward.showingRewards)
		{
			yield return null;
		}
		fillingBar = true;
		string initialBadge = PlayerRankBadge();
		currentMeter = SaveUtility.GetInt("PLAYER_JUSTICE_POINTS");
		int fullMeter = GetRankFullMeter();
		float targetWidth = Mathf.Min(1f, (float)currentMeter / (float)fullMeter) * fullBarWidth;
		GFAudioManager.instance.StopById("fillLoop");
		GFAudioManager.instance.PlayClip(fillLoop, "fillLoop", 1f, loop: true, 0f, 128, GFAudioManager.SFX_GROUP);
		if (targetWidth < rankFillSprite.width)
		{
			DOTween.To(() => rankFillSprite.width, (float x) =>
			{
				rankFillSprite.width = x;
			}, 0f, 0.4f).OnComplete(() =>
			{
				DOTween.To(() => rankFillSprite.width, (float x) =>
				{
					rankFillSprite.width = x;
				}, targetWidth, 0.6f).SetDelay(1.4f);
			});
		}
		else
		{
			DOTween.To(() => rankFillSprite.width, (float x) =>
			{
				rankFillSprite.width = x;
			}, targetWidth, 0.6f);
		}
		if (rankFillSprite.width < targetWidth)
		{
			float startTime = Time.time;
			while (rankFillSprite.width < targetWidth)
			{
				if (Time.time - startTime > 2.5f)
				{
					rankFillSprite.width = targetWidth;
				}
				yield return null;
			}
		}
		AudioSource printAudioSourceStop = GFAudioManager.instance.GetAudioSourceById("fillLoop");
		if (printAudioSourceStop != null)
		{
			printAudioSourceStop.volume = 0f;
		}
		if (currentMeter >= fullMeter)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_questitem_appear", GFAudioManager.SFX_GROUP, string.Empty);
			rankRewardSprite.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 3f, 3, 0.3f);
			fillBarStarParticle.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(1.8f);
			currentMeter = Mathf.Max(0, currentMeter - fullMeter);
			SaveUtility.SetInt("PLAYER_JUSTICE_POINTS", currentMeter, save: false);
			int currentJusticeLevel = PlayerRankLevel();
			SaveUtility.SetInt("PLAYER_JUSTICE_LEVEL", currentJusticeLevel + 1, save: false);
			StopCoroutine("RevealChallengeRewards");
			yield return StartCoroutine("RevealChallengeRewards");
			StartCoroutine("FillBar");
			StartCoroutine("UpdateRank");
		}
		fillingBar = false;
		yield return null;
	}

	private IEnumerator UpdatePlayerCoins()
	{
		int targetCoins = Player.instance.currency;
		playerCoins.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
		while (currentCoins < targetCoins)
		{
			currentCoins += 3;
			if (currentCoins > targetCoins)
			{
				currentCoins = targetCoins;
			}
			playerCoins.text = currentCoins.ToString();
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator UpdateRank()
	{
		rankNumber.text = PlayerRankLevel().ToString();
		rankName.text = PlayerRankName();
		rankRewardSprite.SetSprite(NextRewardSprite());
		yield return new WaitForSeconds(0.25f);
		rankBadgeSprite.SetSprite(PlayerRankBadge());
		if (IsHardModeUnlock())
		{
			hardButton.enabled = true;
		}
	}

	private IEnumerator RewardBoxNextPressed()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_nextbubble1", GFAudioManager.SFX_GROUP, string.Empty);
		animatorChallengeRewardBox.Play("ChallengeRewardBox_Outro");
		yield return new WaitForEndOfFrame();
		while (animatorChallengeRewardBox.NormalizedTime() < 0.9f)
		{
			yield return null;
		}
		if (queueConvo != null)
		{
			yield return StartCoroutine(ShowDialogue(queueConvo));
		}
		else
		{
			StartCoroutine(ExitCharacters());
		}
		rewardBox.SetActive(value: false);
		exitButton.SetActive(value: true);
		towerButton.SetActive(value: true);
		looseChangeGroup.SetActive(value: false);
		easyButton.enabled = true;
		hardButton.enabled = IsHardModeUnlock();
	}

	private IEnumerator RevealChallengeRewards()
	{
		List<CityRewardData> rewardsQueue = new List<CityRewardData>();
		JusticePointsLevel justiceLevels = GameManager.instance.justiceLevels;
		int justiceLevel = PlayerRankLevel() - 1;
		string currentJusticeNameRankKey = "RANK1";
		Conversation convo = null;
		bool found = false;
		bool containedCoinReward = false;
		for (int i = 0; i < justiceLevels.levels.Count; i++)
		{
			JusticePointsLevelData justicePointsLevelData = justiceLevels.levels[i];
			if (((justiceLevel < justicePointsLevelData.low || justiceLevel > justicePointsLevelData.high) && (justiceLevel < highestJusticeLevelData.high || justicePointsLevelData != highestJusticeLevelData)) || found)
			{
				continue;
			}
			found = true;
			if (justicePointsLevelData.conversation != null)
			{
				convo = justicePointsLevelData.conversation;
			}
			for (int j = 0; j < justicePointsLevelData.rewards.Count; j++)
			{
				JusticePointsLevelReward justicePointsLevelReward = justicePointsLevelData.rewards[j];
				if (justicePointsLevelReward.rewardType == JusticePointsLevelUpRewardType.Coins && justicePointsLevelReward.coins > 0)
				{
					rewardsQueue.Add(new CityRewardData(justicePointsLevelReward.coins, -1, -1, -1));
					containedCoinReward = true;
				}
				if (justicePointsLevelReward.rewardType == JusticePointsLevelUpRewardType.Figure && justicePointsLevelReward.characterReward.characterUID > -1)
				{
					int characterUID = justicePointsLevelReward.characterReward.characterUID;
					int b = justicePointsLevelReward.characterReward.characterLvl;
					bool blindBox = justicePointsLevelReward.characterReward.blindBox;
					if (blindBox && justicePointsLevelReward.characterReward.blindBoxShopFigures.Count > 0)
					{
						int index = Random.Range(0, justicePointsLevelReward.characterReward.blindBoxShopFigures.Count);
						characterUID = justicePointsLevelReward.characterReward.blindBoxShopFigures[index].characterUID;
						b = justicePointsLevelReward.characterReward.blindBoxShopFigures[index].level;
					}
					rewardsQueue.Add(new CityRewardData(0, characterUID, Mathf.Max(1, b), -1, blindBox, justicePointsLevelReward.characterReward.goldBox));
				}
				if (justicePointsLevelReward.rewardType == JusticePointsLevelUpRewardType.Modchip && justicePointsLevelReward.modchipUID > -1)
				{
					rewardsQueue.Add(new CityRewardData(0, -1, 0, justicePointsLevelReward.modchipUID));
				}
			}
		}
		easyButton.enabled = false;
		hardButton.enabled = false;
		if (rewardsQueue.Count > 0)
		{
			blackFadeRewardsSprite.gameObject.SetActive(value: true);
			DOTween.ToAlpha(() => blackFadeRewardsSprite.color, (Color x) =>
			{
				blackFadeRewardsSprite.color = x;
			}, 0.5f, 0.4f).SetDelay(0.01f);
		}
		yield return new WaitForSeconds(0.18f);
		if (rewardsQueue.Count > 0)
		{
			cityReward.ShowRewardWithRewards(rewardsQueue);
		}
		yield return new WaitForEndOfFrame();
		while (cityReward.showingRewards)
		{
			yield return null;
		}
		if (containedCoinReward)
		{
			StartCoroutine("UpdatePlayerCoins");
		}
		DOTween.ToAlpha(() => blackFadeRewardsSprite.color, (Color x) =>
		{
			blackFadeRewardsSprite.color = x;
		}, 0f, 0.2f).OnComplete(() =>
		{
			blackFadeRewardsSprite.gameObject.SetActive(value: false);
		});
		queueConvo = convo;
		yield return null;
		if (isNewRankForLevel(justiceLevel + 1))
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_newtitle", GFAudioManager.SFX_GROUP, string.Empty);
			animatorRankBar.Play("JLA_rankUp_titleChange", 0, 0f);
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_challengemode_newrank", GFAudioManager.SFX_GROUP, string.Empty);
			animatorRankBar.Play("JLA_rankUp", 0, 0f);
		}
		easyButton.enabled = true;
		hardButton.enabled = IsHardModeUnlock();
		yield return null;
	}

	private IEnumerator ExitCharacters()
	{
		while (rewardBox.activeSelf)
		{
			yield return null;
		}
		yield return StartCoroutine(dialogueSystem.ExitSequenceDoNotDestroy());
		while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
		{
			yield return null;
		}
		exitButton.SetActive(value: true);
		towerButton.SetActive(value: true);
		easyButton.enabled = true;
		hardButton.enabled = IsHardModeUnlock();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private IEnumerator BringInCharacters()
	{
		exitButton.SetActive(value: false);
		towerButton.SetActive(value: false);
		looseChangeGroup.SetActive(value: false);
		easyButton.enabled = false;
		hardButton.enabled = false;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		if (dialogueSystem == null)
		{
			dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
		}
		dialogueSystem.transform.parent = dialogueSystemParent;
		dialogueSystem.transform.localPosition = new Vector3(0f, 328f, 33f);
		dialogueSystem.gameObject.SetActive(value: true);
		StartCoroutine(dialogueSystem.BringInCharacters(Resources.Load("Data/Conversations/CONVO_JLA_EMPTY") as Conversation, GUICamera.instance.camera));
		yield return null;
	}

	public IEnumerator PlayDialogue(string convoName)
	{
		yield return StartCoroutine(ShowDialogue(Resources.Load<Conversation>("Data/Conversations/" + convoName)));
	}

	private IEnumerator ShowDialogue(Conversation conversation, bool showExitButton = false)
	{
		exitButton.SetActive(value: false);
		towerButton.SetActive(value: false);
		looseChangeGroup.SetActive(value: false);
		easyButton.enabled = false;
		hardButton.enabled = false;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		if (dialogueSystem == null)
		{
			dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
		}
		dialogueSystem.transform.parent = dialogueSystemParent;
		dialogueSystem.transform.localPosition = new Vector3(0f, 328f, 33f);
		dialogueSystem.gameObject.SetActive(value: true);
		dialogueSystem.Init(GUICamera.instance.camera, conversation, null, null, showExitButton, skipEntrance: true);
		while (dialogueSystem != null && dialogueSystem.gameObject.activeSelf)
		{
			yield return null;
		}
		exitButton.SetActive(value: true);
		towerButton.SetActive(value: true);
		easyButton.enabled = true;
		hardButton.enabled = IsHardModeUnlock();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
	}
}
