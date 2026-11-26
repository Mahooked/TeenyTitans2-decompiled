using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeRoulette : MonoBehaviour
{
	private static List<ChallengeModeType> ignoreTypes = new List<ChallengeModeType> { ChallengeModeType.KeepItClassy };

	public TextMeshPro challengeNameHeader;

	public TextMeshPro challengeDescription;

	public tk2dSpriteRoulette rouletteSprite;

	public ChallengeModePreBattle challengeModePreBattle;

	public bool isSpinning;

	private void Start()
	{
		challengeNameHeader.text = string.Empty;
		challengeDescription.text = string.Empty;
		BuildRouletteSprites();
		StartCoroutine("ActivateChallengeRoulette");
	}

	private void BuildRouletteSprites()
	{
		ChallengeModeDatabase challengeModeDatabase = GameManager.instance.challengeModeDatabase;
		rouletteSprite.spriteNames.Clear();
		int num = SaveUtility.GetInt("CHALLENGE_ROULETTE_COUNT") + 1;
		if (num > 2147483637)
		{
			num = 0;
		}
		SaveUtility.SetInt("CHALLENGE_ROULETTE_COUNT", num, false);
		int num2 = 0;
		for (int i = 0; i < challengeModeDatabase.items.Count; i++)
		{
			if (challengeModeDatabase.items[i].isAvailable)
			{
				num2++;
			}
		}
		for (int j = 0; j < challengeModeDatabase.items.Count; j++)
		{
			ChallengeModeDefinition challengeModeDefinition = challengeModeDatabase.items[j];
			if (!ignoreTypes.Contains(challengeModeDefinition.challengeModeType))
			{
				bool flag = !SaveUtility.HasKey("CHALLENGE_ROULETTE_ID_" + challengeModeDefinition.uid);
				int num3 = SaveUtility.GetInt("CHALLENGE_ROULETTE_ID_" + challengeModeDefinition.uid);
				if (challengeModeDefinition.isAvailable && (flag || num - num3 > challengeModeDefinition.minChallengesBetween || (num == 0 && challengeModeDefinition.minChallengesBetween < 2) || num2 == 1))
				{
					rouletteSprite.spriteNames.Add(challengeModeDefinition.challengeModeSprite);
				}
			}
		}
	}

	private ChallengeModeDefinition GetChallengeModeDefinition(string spriteName)
	{
		ChallengeModeDatabase challengeModeDatabase = GameManager.instance.challengeModeDatabase;
		for (int i = 0; i < challengeModeDatabase.items.Count; i++)
		{
			ChallengeModeDefinition challengeModeDefinition = challengeModeDatabase.items[i];
			if (spriteName.Equals(challengeModeDefinition.challengeModeSprite))
			{
				return challengeModeDefinition;
			}
		}
		return null;
	}

	private IEnumerator ActivateChallengeRoulette()
	{
		if (PreBattleController.battleDefinition == null)
		{
			yield break;
		}
		bool startRoutlette = true;
		bool forceMode = false;
		if (ChallengeModeManager.useTestMode && ChallengeModeManager.isChallengeModeActive && ChallengeModeManager.isNonJLChallenge)
		{
			TournamentRound currentRound = TournamentTracker.currentRound;
			if (currentRound.challengesIncluded.Count > 1)
			{
				forceMode = true;
				List<ChallengeModeDefinition> items = GameManager.instance.challengeModeDatabase.GetItems(currentRound.challengesIncluded);
				if (items.Count > 0)
				{
					ChallengeModeDefinition challengeModeDefinition = items[Random.Range(0, items.Count)];
					ChallengeModeManager.testMode = challengeModeDefinition.challengeModeType;
					ChallengeModeManager.modeDefinition = challengeModeDefinition;
				}
			}
		}
		if (startRoutlette)
		{
			isSpinning = true;
			rouletteSprite.StartRoulette();
			yield return new WaitForSeconds(1f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_rouletteloop", GFAudioManager.SFX_GROUP, "roulette", 1f, 0f, true);
			while (!Input.GetMouseButtonDown(0))
			{
				yield return null;
			}
			yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
			if (PreBattleController.battleDefinition == null)
			{
				yield break;
			}
			rouletteSprite.StopRoulette();
		}
		if (ChallengeModeManager.useTestMode)
		{
			ChallengeModeDefinition item = GameManager.instance.challengeModeDatabase.GetItem(ChallengeModeManager.testMode);
			if (item != null)
			{
				rouletteSprite.sprite.SetSprite(item.challengeModeSprite);
			}
		}
		GetComponent<Animator>().Play("challengeRoulette_select");
		GFAudioManager.instance.StopById("roulette");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_roulettestop", GFAudioManager.SFX_GROUP, string.Empty);
		ChallengeModeDefinition modeDefinition = (ChallengeModeManager.modeDefinition = GetChallengeModeDefinition(rouletteSprite.sprite.CurrentSprite.name));
		ChallengeModeManager.currentModeValues = ((!ChallengeModeManager.challengeModeHard) ? modeDefinition.easyModeValues : modeDefinition.hardModeValues);
		PreBattleController.battleDefinition.coinReward = ChallengeModeManager.currentModeValues.battleCoinReward;
		PreBattleController.battleDefinition.xpReward = ChallengeModeManager.currentModeValues.xpReward;
		SaveUtility.SetInt("CHALLENGE_ROULETTE_ID_" + modeDefinition.uid, SaveUtility.GetInt("CHALLENGE_ROULETTE_COUNT"), false);
		challengeNameHeader.text = Language.Get(modeDefinition.challengeModeNameKey, "MENUS,GUI");
		challengeDescription.text = Language.Get(modeDefinition.challengeModeDescriptionKey, "MENUS,GUI");
		if (ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.SurpriseSquad)
		{
			challengeModePreBattle.SetupChallengeModeSurpiseParty();
		}
		else if (ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.BossFight)
		{
			PreBattleController.battleDefinition.battleThemePath = "Audio/Music/music_battle-tournament";
			challengeModePreBattle.SetupChallengeModeBossFight();
		}
		else if (ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.KeepItClassy)
		{
			challengeModePreBattle.SetupChallengeModeKeepItClassy();
		}
		else
		{
			challengeModePreBattle.DisplayEnemyFigurePortraits();
		}
		yield return new WaitForSeconds(0.1f);
		isSpinning = false;
	}
}
