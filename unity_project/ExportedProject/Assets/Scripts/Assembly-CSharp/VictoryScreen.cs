using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
	public enum Mode
	{
		Normal = 0,
		Tutorial = 1
	}

	public Transform coin;

	public Transform coinShineOverlays;

	public TextMeshPro currencyText;

	public ParticleSystem coinParticleSystem;

	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public Transform anchorUpperCenter;

	public Transform anchorLowerCenter;

	public float portraitSpacingActive = 100f;

	public float portraitSpacingInactive = 100f;

	public float portraitRevealStagger = 0.15f;

	public float portraitRemoveStagger = 0.05f;

	private List<Character> inactiveTeam;

	private List<ResultsScreenPortrait> activePortraits = new List<ResultsScreenPortrait>();

	private List<ResultsScreenPortrait> inactivePortraits = new List<ResultsScreenPortrait>();

	private bool waitForTap;

	private Mode currentMode;

	private int currencyIncrement;

	private int leftover;

	private Player _player;

	private BattleDefinition _battleDefinition;

	private void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	private void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
	}

	private void OnDestroy()
	{
		_player = null;
	}

	private void Update()
	{
		if (waitForTap && Input.GetMouseButtonDown(0))
		{
			waitForTap = false;
			StartCoroutine(CollectCoins());
		}
	}

	private void HandleEventTriggered(AnimationEvent obj)
	{
		if (currentMode != Mode.Tutorial)
		{
			if (obj.stringParameter == "addCurrency")
			{
				waitForTap = true;
			}
			else if (obj.stringParameter == "introComplete")
			{
				StartCoroutine(ShowPortraits());
			}
		}
	}

	public void Init(Player player, BattleDefinition battleDefinition, Mode mode = Mode.Normal)
	{
		_player = player;
		_battleDefinition = battleDefinition;
		currentMode = mode;
		activePortraits.Clear();
		inactivePortraits.Clear();
		inactiveTeam = _player.activeParty.FindAll((Character character) => !_player.activeTeam.Contains(character));
		if (battleDefinition == null || _battleDefinition.coinReward <= 0)
		{
			currencyText.text = GameManager.instance.gameSettings.currencyPerBattle.ToString();
		}
		else
		{
			currencyText.text = _battleDefinition.coinReward.ToString();
		}
		if (_battleDefinition != null && currentMode == Mode.Normal)
		{
			if (!string.IsNullOrEmpty(_battleDefinition.enemyGUID))
			{
				Player.instance.AddToDefeatedNPCs(_battleDefinition.enemyGUID);
			}
			if (!string.IsNullOrEmpty(_battleDefinition.enemyDefeatedMsg))
			{
				CommunicatorManager.instance.UpdateMissionWithTargetMessage(_battleDefinition.enemyDefeatedMsg);
			}
		}
		else if (currentMode == Mode.Tutorial)
		{
			coin.gameObject.SetActive(false);
			coinShineOverlays.gameObject.SetActive(false);
		}
	}

	private IEnumerator ShowPortraits()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_xprise", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.5f);
		if (_battleDefinition.type == BattleType.Pvp || _battleDefinition.type == BattleType.PvpCpu || _battleDefinition.type == BattleType.PvpFriend)
		{
			LegionOfDoomSettings legionOfDoomSettings = GameManager.instance.legionOfDoomSettings;
			Player.instance.pvpData.currentWinStreak++;
			Player.instance.pvpData.coinsReceived += legionOfDoomSettings.coinRewardWin;
			Player.instance.pvpData.lastMatchIsLoss = false;
			Player.instance.AddPvpXP(legionOfDoomSettings.winXP, (_battleDefinition.type == BattleType.Pvp) ? legionOfDoomSettings.humanWinXP : 0);
			if (_battleDefinition.type == BattleType.Pvp)
			{
				Player.instance.AddSkillRating(GameManager.instance.legionOfDoomSettings.winSkillRating);
			}
		}
		int len = _player.activeTeam.Count;
		Character character = null;
		Vector3 newPosition = new Vector3(portraitSpacingActive * (float)(len - 1) * -0.5f, 140f);
		for (int i = 0; i < len; i++)
		{
			character = _player.activeTeam[i];
			int xpAmount = 0;
			if (_battleDefinition != null)
			{
				xpAmount = (character.isDead ? Mathf.CeilToInt((float)_battleDefinition.xpReward * 0.5f) : ((!BattleController.instance.HasCharacterParticipated(character)) ? Mathf.CeilToInt((float)_battleDefinition.xpReward * 0.75f) : _battleDefinition.xpReward));
			}
			ResultsScreenPortrait portrait = ObjectPool.GetObjectFromResource<ResultsScreenPortrait>("Prefabs/GUI/Battle/ResultsScreenPortrait", default(Vector3), false);
			portrait.Init(character, xpAmount);
			portrait.transform.parent = anchorLowerCenter;
			portrait.transform.localPosition = newPosition;
			portrait.gameObject.SetActive(true);
			activePortraits.Add(portrait);
			newPosition.x += portraitSpacingActive;
			yield return new WaitForSeconds(portraitRevealStagger);
		}
		len = inactiveTeam.Count;
		newPosition = new Vector3(portraitSpacingInactive * (float)(len - 1) * -0.5f, 60f, -30f);
		for (int j = 0; j < len; j++)
		{
			character = inactiveTeam[j];
			int xpAmount2 = 0;
			if (_battleDefinition != null)
			{
				xpAmount2 = ((!character.isDead) ? Mathf.CeilToInt((float)_battleDefinition.xpReward * 0.2f) : 0);
			}
			ResultsScreenPortrait portrait = ObjectPool.GetObjectFromResource<ResultsScreenPortrait>("Prefabs/GUI/Battle/ResultsScreenPortraitInactive", default(Vector3), false);
			portrait.Init(character, xpAmount2);
			portrait.transform.parent = anchorLowerCenter;
			portrait.transform.localPosition = newPosition;
			portrait.gameObject.SetActive(true);
			inactivePortraits.Add(portrait);
			newPosition.x += portraitSpacingInactive;
			yield return new WaitForSeconds(portraitRevealStagger);
		}
	}

	private IEnumerator CollectCoins()
	{
		WalletGUI walletGUI = BattleController.instance.walletGUI;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_coinsadd", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.1f);
		coin.gameObject.SetActive(false);
		coinShineOverlays.gameObject.SetActive(false);
		walletGUI.transform.localPosition += new Vector3(0f, 100f);
		walletGUI.Show();
		int count = ((_battleDefinition != null && _battleDefinition.coinReward > 0) ? _battleDefinition.coinReward : GameManager.instance.gameSettings.currencyPerBattle);
		currencyIncrement = Mathf.Max(1, Mathf.FloorToInt(count / coinParticleSystem.maxParticles));
		leftover = ((count > coinParticleSystem.maxParticles) ? (count % coinParticleSystem.maxParticles) : 0);
		coinParticleSystem.emission.SetBursts(new ParticleSystem.Burst[1]
		{
			new ParticleSystem.Burst(0f, (short)count)
		});
		coinParticleSystem.gameObject.SetActive(true);
		walletGUI.particleAttractor.ParticleDestroyed += HandleOnParticleDestroyed;
		walletGUI.particleAttractor.AttractParticleSystem(coinParticleSystem);
		while (walletGUI.particleAttractor.numParticleSystems > 0)
		{
			yield return null;
		}
		walletGUI.particleAttractor.ParticleDestroyed -= HandleOnParticleDestroyed;
		coinParticleSystem.gameObject.SetActive(false);
		yield return new WaitForSeconds(1.75f);
		BattleController.instance.levelUpScreen.CheckQueue();
		while (BattleController.instance.levelUpScreen.gameObject.activeSelf)
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(PlayExit());
	}

	private void HandleOnParticleDestroyed()
	{
		int num = currencyIncrement;
		if (leftover > 0)
		{
			num++;
			leftover--;
		}
		BattleController.instance.walletGUI.AddToWallet(num);
	}

	private IEnumerator PlayExit()
	{
		animator.Play("Outro");
		int len = activePortraits.Count;
		for (int i = 0; i < len; i++)
		{
			activePortraits[i].animator.Play("Outro");
			yield return new WaitForSeconds(portraitRemoveStagger);
		}
		len = inactivePortraits.Count;
		for (int j = 0; j < len; j++)
		{
			inactivePortraits[j].animator.Play("Outro");
			yield return new WaitForSeconds(portraitRemoveStagger);
		}
		GFAudioManager.instance.FadeOut("music_results", 1f);
		yield return new WaitForSeconds(1f);
		Player.instance.Save();
		if (TournamentTracker.isTournamentActive)
		{
			LoadingScreenCover.LoadScene("Tournament");
		}
		else if (ChallengeModeManager.isChallengeModeActive && !ChallengeModeManager.isNonJLChallenge)
		{
			ChallengeModeManager.challengeWon = true;
			LoadingScreenCover.LoadScene("Challenge");
		}
		else if (_battleDefinition.type == BattleType.Pvp || _battleDefinition.type == BattleType.PvpCpu || _battleDefinition.type == BattleType.PvpFriend)
		{
			LoadingScreenCover.LoadScene("LegionOfDoomMode");
		}
		else
		{
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
	}
}
