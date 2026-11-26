using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegionOfDoomRewardMenu : MonoBehaviour
{
	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public TextMeshPro headerTextMesh;

	public TextMeshPro coinTextMesh;

	public TextMeshPro mainXPTextMesh;

	public TextMeshPro humanXPTextMesh;

	public TextMeshPro winStreakXPTextMesh;

	public TextMeshPro winStreakCounterTextMesh;

	public tk2dUIItem closeBtn;

	public GameObject receivedHumanXPObj;

	public GameObject noHumanXPObj;

	public GameObject receivedStreakXPObj;

	public GameObject noStreakXPObj;

	public ParticleSystem coinParticle;

	public ParticleSystem mainXPParticle;

	public ParticleSystem humanXPParticle;

	public ParticleSystem winStreakXPParticle;

	[Header("Sound Effects")]
	public AudioClip rankUpSFX;

	public AudioClip bonusGetSFX;

	public AudioClip bonusMissSFX;

	public AudioClip coinSFX;

	private bool busy;

	private int currencyInterval;

	private PvpData pvpData;

	public bool outroing
	{
		get
		{
			return animator.IsName("LOD_RewardBubble_Outro");
		}
	}

	public bool outroFinished
	{
		get
		{
			return outroing && !animator.IsPlaying();
		}
	}

	public void Init()
	{
		pvpData = Player.instance.pvpData;
		LegionOfDoomSettings legionOfDoomSettings = GameManager.instance.legionOfDoomSettings;
		headerTextMesh.text = ((!pvpData.lastMatchIsLoss) ? Language.Get("JOBGREAT", "MENUS,GUI") : Language.Get("YOUTRIED", "MENUS,GUI"));
		coinTextMesh.text = pvpData.coinsReceived.ToString();
		mainXPTextMesh.text = pvpData.winXPReceived.ToString();
		humanXPTextMesh.text = pvpData.humanWinXPReceived.ToString();
		winStreakXPTextMesh.text = pvpData.winStreakXPReceived.ToString();
		string format = ((pvpData.currentWinStreak < legionOfDoomSettings.maxWinStreak) ? "x{0} Wins" : "x{0}+ Wins");
		winStreakCounterTextMesh.text = string.Format(format, Mathf.Clamp(pvpData.currentWinStreak, 0, legionOfDoomSettings.maxWinStreak));
		receivedHumanXPObj.SetActive(pvpData.humanWinXPReceived > 0);
		noHumanXPObj.SetActive(pvpData.humanWinXPReceived <= 0);
		receivedStreakXPObj.SetActive(pvpData.currentWinStreak > 1);
		noStreakXPObj.SetActive(pvpData.currentWinStreak <= 1);
		currencyInterval = Mathf.RoundToInt((float)pvpData.coinsReceived / coinParticle.emission.GetBurst(0).count.constant);
		if (currencyInterval > 0)
		{
			LegionOfDoomController.instance.currencyGUI.particleAttractor.ParticleDestroyedParticleSystem += HandleCurrencyParticleDestroyed;
		}
		closeBtn.OnClick += HandleCloseClicked;
		animatorEventListener.EventTriggered += HandleAnimatorEvent;
	}

	private void OnDisable()
	{
		coinParticle.gameObject.SetActive(false);
		mainXPParticle.gameObject.SetActive(false);
		humanXPParticle.gameObject.SetActive(false);
		winStreakXPParticle.gameObject.SetActive(false);
		LegionOfDoomController.instance.currencyGUI.particleAttractor.ParticleDestroyedParticleSystem -= HandleCurrencyParticleDestroyed;
		closeBtn.OnClick -= HandleCloseClicked;
		animatorEventListener.EventTriggered -= HandleAnimatorEvent;
	}

	private void HandleAnimatorEvent(AnimationEvent animEvent)
	{
		switch (animEvent.stringParameter)
		{
		case "rewardCoins":
			if (pvpData.coinsReceived > 0)
			{
				Debug.Log("Reward Menu, Reward COINS: " + pvpData.coinsReceived);
				coinParticle.gameObject.SetActive(true);
				LegionOfDoomController.instance.currencyGUI.particleAttractor.AttractParticleSystem(coinParticle);
				pvpData.coinsReceived = 0;
				GFAudioManager.instance.PlayClip(coinSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
			break;
		case "rewardBaseXP":
			if (pvpData.winXPReceived > 0)
			{
				Debug.Log("Reward Menu, Reward BASE XP: " + pvpData.winXPReceived);
				mainXPParticle.gameObject.SetActive(true);
				LegionOfDoomController.instance.rankUpBar.AttractParticleSystem(mainXPParticle);
				AddXP(pvpData.winXPReceived, mainXPParticle);
				pvpData.winXPReceived = 0;
				GFAudioManager.instance.PlayClip(bonusGetSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
			break;
		case "rewardHumanXP":
			if (pvpData.humanWinXPReceived > 0)
			{
				Debug.Log("Reward Menu, Reward HUMAN XP: " + pvpData.humanWinXPReceived);
				humanXPParticle.gameObject.SetActive(true);
				LegionOfDoomController.instance.rankUpBar.AttractParticleSystem(humanXPParticle);
				AddXP(pvpData.humanWinXPReceived, humanXPParticle);
				pvpData.humanWinXPReceived = 0;
				GFAudioManager.instance.PlayClip(bonusGetSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
			else
			{
				GFAudioManager.instance.PlayClip(bonusMissSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
			break;
		case "rewardWinStreakXP":
			if (pvpData.winStreakXPReceived > 0)
			{
				Debug.Log("Reward Menu, Reward WIN STREAK XP: " + pvpData.winStreakXPReceived);
				winStreakXPParticle.gameObject.SetActive(true);
				LegionOfDoomController.instance.rankUpBar.AttractParticleSystem(winStreakXPParticle);
				AddXP(pvpData.winStreakXPReceived, winStreakXPParticle);
				pvpData.winStreakXPReceived = 0;
				GFAudioManager.instance.PlayClip(bonusGetSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
			else
			{
				GFAudioManager.instance.PlayClip(bonusMissSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			}
			break;
		case "levelUpCheck":
			StartCoroutine(LevelUpCheck());
			break;
		case "rewardPausePoint":
			if (busy)
			{
				animator.speed = 0f;
			}
			break;
		}
	}

	private void AddXP(int xp, ParticleSystem targetParticleSystem)
	{
		StartCoroutine(XPSequence(xp, targetParticleSystem));
	}

	private IEnumerator XPSequence(int xp, ParticleSystem targetParticleSystem)
	{
		busy = true;
		bool particleDestroyed = false;
		Action<ParticleSystem> HandleParticleDestroyed = (ParticleSystem partSys) =>
		{
			if (partSys == targetParticleSystem)
			{
				particleDestroyed = true;
			}
		};
		LegionOfDoomController.instance.rankUpBar.particleAttractor.ParticleDestroyedParticleSystem += HandleParticleDestroyed;
		yield return new WaitUntil(() => particleDestroyed);
		LegionOfDoomController.instance.rankUpBar.particleAttractor.ParticleDestroyedParticleSystem -= HandleParticleDestroyed;
		if (busy)
		{
			busy = false;
			animator.speed = 1f;
		}
		pvpData.PreviousXP += xp;
		yield return LegionOfDoomController.instance.rankUpBar.UpdateFill(true);
		if (pvpData.PreviousXP >= GameManager.instance.legionOfDoomLevelTable.GetLevelData(pvpData.PreviousLevel).xpToLevelUp)
		{
			LegionOfDoomController.instance.rankUpBar.BounceRewardSprite();
		}
	}

	private IEnumerator LevelUpCheck()
	{
		Debug.Log("Level Up Check...");
		LegionOfDoomLevelTable levelTable = GameManager.instance.legionOfDoomLevelTable;
		LegionOfDoomLevelData levelData = levelTable.GetLevelData(pvpData.PreviousLevel);
		int xpOver = pvpData.PreviousXP - levelData.xpToLevelUp;
		if (xpOver >= 0)
		{
			busy = true;
			yield return new WaitForSeconds(1.4f);
		}
		while (xpOver >= 0)
		{
			Debug.Log("You've leveled up, giving reward for that level...");
			LegionOfDoomLevelData nextLevelData = levelTable.GetLevelData(pvpData.PreviousLevel + 1);
			int len = nextLevelData.rewards.Length;
			List<PlayerReward> riddlerEggs = new List<PlayerReward>();
			List<PlayerReward> remainingRewards = new List<PlayerReward>();
			for (int i = 0; i < len; i++)
			{
				PlayerReward playerReward = nextLevelData.rewards[i];
				if (playerReward.type == PlayerRewardType.RiddleEgg)
				{
					riddlerEggs.Add(GameManager.instance.iapConfig.GenerateRiddleEggReward(playerReward.riddleEggType));
				}
				else if (playerReward.type == PlayerRewardType.Repaint && playerReward.isRepaintRandom)
				{
					remainingRewards.Add(PlayerRewardFactory.GetRandomRepaintReward(playerReward.isRepaintRare, 0.15f));
				}
				else
				{
					remainingRewards.Add(playerReward);
				}
			}
			riddlerEggs.Sort((PlayerReward x, PlayerReward y) => x.riddleEggType.CompareTo(y.riddleEggType));
			if (riddlerEggs.Count > 0)
			{
				RiddleEggReveal.Show(riddlerEggs);
				yield return new WaitWhile(() => RiddleEggReveal.Instance != null);
			}
			if (remainingRewards.Count > 0)
			{
				CityRewardReveal.Show(remainingRewards);
				yield return new WaitWhile(() => CityRewardReveal.Instance != null);
			}
			LegionOfDoomController.instance.rankUpBar.KillRewardSpriteBounce();
			pvpData.PreviousLevel++;
			pvpData.PreviousXP = 0;
			LegionOfDoomController.instance.rankUpBar.UpdateFill(false);
			AchievementsDatabase.UnlockAchievementId("TT2_JUSTGETTINGSTARTED");
			if (pvpData.PreviousLevel >= 15)
			{
				AchievementsDatabase.UnlockAchievementId("TT2_CHALLENGEROFTHESUPERFRIENDS");
			}
			if (pvpData.PreviousLevel >= 50)
			{
				AchievementsDatabase.UnlockAchievementId("TT2_MOVEOVERLEX");
			}
			pvpData.PreviousXP = xpOver;
			GFAudioManager.instance.PlayClip(rankUpSFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			LegionOfDoomController.instance.rankUpBar.BumpLevelPlate();
			yield return LegionOfDoomController.instance.rankUpBar.UpdateFill(true, false);
			levelData = levelTable.GetLevelData(pvpData.PreviousLevel);
			xpOver = pvpData.PreviousXP - levelData.xpToLevelUp;
		}
		if (busy)
		{
			busy = false;
			animator.speed = 1f;
		}
		Debug.Log("Level Up Check Finished");
	}

	private void HandleCloseClicked()
	{
		animator.Play("LOD_RewardBubble_Outro");
		animatorEventListener.EventTriggered -= HandleAnimatorEvent;
	}

	private void HandleCurrencyParticleDestroyed(ParticleSystem particleSystem)
	{
		Player.instance.AddCurrency(currencyInterval);
		LegionOfDoomController.instance.currencyGUI.BumpAmount(currencyInterval);
	}
}
