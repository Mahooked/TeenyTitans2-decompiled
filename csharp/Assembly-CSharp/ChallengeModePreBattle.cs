using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChallengeModePreBattle : MonoBehaviour
{
	public PreBattleController prebattleController;

	public GameObject challengeModeBanner;

	public GameObject challengeModeButton;

	public tk2dSprite challengeModeIcon;

	public ChallengeRoulette challengeRoulette;

	private Vector3 modeButtonShownPosition;

	public GameObject surpriseSquadGroup;

	public Animator rouletteAnimator;

	public BoxCollider2D rouleteTouchBlocker;

	private List<string> initialEnemyPortraits = new List<string>();

	private List<string> initialEnemyLvls = new List<string>();

	private bool hideBannerDirty;

	private void OnEnable()
	{
		rouletteAnimator.enabled = false;
	}

	private IEnumerator Start()
	{
		if (!ChallengeModeManager.challengeModeDirty)
		{
			gameObject.SetActive(value: false);
			yield break;
		}
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		modeButtonShownPosition = challengeModeButton.transform.position;
		challengeModeButton.transform.position = new Vector3(modeButtonShownPosition.x + 300f, modeButtonShownPosition.y, modeButtonShownPosition.z);
		challengeRoulette.challengeModePreBattle = this;
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < prebattleController.teamSelectScreen.enemyPortraits.Count; i++)
		{
			initialEnemyPortraits.Add(prebattleController.teamSelectScreen.enemyPortraits[i].portraitSprite.CurrentSprite.name);
			initialEnemyLvls.Add(prebattleController.teamSelectScreen.enemyPortraits[i].levelText.text);
			prebattleController.teamSelectScreen.enemyPortraits[i].portraitSprite.SetSprite("icon_shadowcharacter");
			prebattleController.teamSelectScreen.enemyPortraits[i].levelText.text = string.Empty;
		}
		while (!prebattleController.handheldConsole.model.animator.gameObject.activeSelf)
		{
			yield return null;
		}
		if (ChallengeModeManager.challengeModeDirty)
		{
			ChallengeModeManager.challengeModeDirty = false;
			IntroRoulette();
		}
	}

	private void OnChallangeModeClicked()
	{
		Debug.Log("On Challenge Mode Clicked...");
		IntroRoulette();
	}

	private void IntroRoulette()
	{
		challengeModeBanner.SetActive(value: true);
		rouletteAnimator.enabled = true;
		rouletteAnimator.Play("challengeRoulette_intro");
		rouleteTouchBlocker.enabled = true;
	}

	private void ShowChallengeModeButton()
	{
		challengeModeButton.transform.DOMove(modeButtonShownPosition, 0.3f).SetEase(Ease.OutSine);
		challengeModeIcon.SetSprite(ChallengeModeManager.modeDefinition.challengeModeSprite);
		challengeModeButton.SetActive(value: true);
	}

	public void AnimateOutChallengeModeButton()
	{
		challengeModeButton.transform.DOMove(modeButtonShownPosition + new Vector3(200f, 0f, 0f), 0.4f).SetEase(Ease.InBack);
	}

	public void DisplayEnemyFigurePortraits()
	{
		for (int i = 0; i < prebattleController.teamSelectScreen.enemyPortraits.Count; i++)
		{
			prebattleController.teamSelectScreen.enemyPortraits[i].portraitSprite.SetSprite(initialEnemyPortraits[i]);
			prebattleController.teamSelectScreen.enemyPortraits[i].levelText.text = initialEnemyLvls[i];
		}
	}

	public void SetupChallengeModeSurpiseParty()
	{
		for (int i = 0; i < prebattleController.preBattleParty.slots.Count; i++)
		{
			MySquadSlot mySquadSlot = prebattleController.preBattleParty.slots[i];
			mySquadSlot.boxCollider2D.enabled = false;
		}
		prebattleController.handheldConsole.model.classCircle.SetActive(value: false);
		surpriseSquadGroup.SetActive(value: true);
		int count = Player.instance.activeParty.Count;
		int num = 0;
		List<Character> list = new List<Character>();
		while (num < 3 && num != count)
		{
			Character item = Player.instance.activeParty[Random.Range(0, count)];
			if (!list.Contains(item))
			{
				list.Add(item);
				num++;
			}
		}
		for (int j = 0; j < prebattleController.teamSelectScreen.enemyPortraits.Count; j++)
		{
			prebattleController.teamSelectScreen.enemyPortraits[j].portraitSprite.SetSprite("icon_shadowcharacter");
			prebattleController.teamSelectScreen.enemyPortraits[j].levelText.text = string.Empty;
		}
		prebattleController.handheldConsole.leftArrow.uiItem.enabled = false;
		prebattleController.handheldConsole.centerArrow.uiItem.enabled = false;
		prebattleController.handheldConsole.rightArrow.uiItem.enabled = false;
		Player.instance.activeTeam.Clear();
		for (int k = 0; k < list.Count; k++)
		{
			if (k == 0)
			{
				prebattleController.handheldConsole.centerArrow.character = list[k];
			}
			if (k == 1)
			{
				prebattleController.handheldConsole.leftArrow.character = list[k];
			}
			if (k == 2)
			{
				prebattleController.handheldConsole.rightArrow.character = list[k];
			}
			Player.instance.AddToTeam(list[k]);
		}
		for (int l = 0; l < prebattleController.preBattleParty.slots.Count; l++)
		{
			if (prebattleController.preBattleParty.slots[l].portraitSprite != null)
			{
				prebattleController.preBattleParty.slots[l].portraitSprite.color = new Color(0.5f, 0.5f, 0.5f);
			}
		}
	}

	public void SetupChallengeModeBossFight()
	{
		for (int num = PreBattleController.battleDefinition.enemyTeam.Count - 1; num >= 0; num--)
		{
			if (num != 0)
			{
				PreBattleController.battleDefinition.enemyTeam.RemoveAt(num);
			}
			else
			{
				PreBattleController.battleDefinition.enemyTeam[num].hitPointsTempIncrease = ChallengeModeManager.currentModeValues.intValue1;
				PreBattleController.battleDefinition.enemyTeam[num].shinyRank = 3;
			}
		}
		for (int i = 0; i < prebattleController.teamSelectScreen.enemyPortraits.Count; i++)
		{
			if (i == 0)
			{
				prebattleController.teamSelectScreen.enemyPortraits[i].portraitSprite.SetSprite(initialEnemyPortraits[i]);
				prebattleController.teamSelectScreen.enemyPortraits[i].levelText.text = initialEnemyLvls[i];
			}
			else
			{
				prebattleController.teamSelectScreen.enemyPortraits[i].gameObject.SetActive(value: false);
			}
		}
	}

	public void SetupChallengeModeKeepItClassy()
	{
	}

	private IEnumerator CloseChallengeRoulette()
	{
		rouletteAnimator.Play("challengeRoulette_outro");
		rouletteAnimator.Update(0f);
		yield return new WaitWhile(() => rouletteAnimator.IsPlaying());
		rouleteTouchBlocker.enabled = false;
		ShowChallengeModeButton();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && rouletteAnimator.enabled && rouletteAnimator.IsNamed("challengeRoulette_intro", "challengeRoulette_select") && !challengeRoulette.isSpinning)
		{
			hideBannerDirty = true;
		}
	}

	private void LateUpdate()
	{
		if (hideBannerDirty)
		{
			hideBannerDirty = false;
			StartCoroutine(CloseChallengeRoulette());
		}
	}
}
