using TMPro;
using UnityEngine;

public class TournamentEnemyPortrait : MonoBehaviour, IPoolable
{
	public Animator portraitAnimator;

	public Animator tofuAnimator;

	public Animator tofuBackingAnimator;

	public tk2dSprite portraitSprite;

	public tk2dSprite defeatedSprite;

	public tk2dSprite leftTrophySprite;

	public tk2dSprite rightTrophySprite;

	public tk2dSprite tofuBackingSprite;

	public tk2dSprite tofuSprite;

	public tk2dSpriteRoulette tofuSpriteRoulette;

	public TextMeshPro tofuNumberText;

	public TextMeshPro tofuHeaderText;

	public TextMeshPro tofuDescriptionText;

	public GameObject chooseGrp;

	public GameObject challengeGrp;

	public tk2dSprite challengeQuestionMark;

	public tk2dSprite challengeSprite;

	private TournamentDefinition _tournament;

	private TournamentRound _round;

	public TournamentDefinition tournament => _tournament;

	public TournamentRound round => _round;

	private void Awake()
	{
		leftTrophySprite.gameObject.SetActive(value: false);
		rightTrophySprite.gameObject.SetActive(value: false);
		tofuBackingSprite.gameObject.SetActive(value: false);
	}

	private void OnDestroy()
	{
		_tournament = null;
		_round = null;
	}

	public void Init(TournamentDefinition tournament, TournamentRound round, bool isChampion = false)
	{
		if (round == _round)
		{
			return;
		}
		_tournament = tournament;
		_round = round;
		leftTrophySprite.SetSprite(_tournament.tournamentSpriteCollection, _tournament.trophySpriteId);
		rightTrophySprite.SetSprite(_tournament.tournamentSpriteCollection, _tournament.trophySpriteId);
		switch (round.type)
		{
		case TournamentRoundType.Battle:
			InitBattle(isChampion);
			if (round.challengesOn)
			{
				if (challengeGrp != null)
				{
					challengeGrp.SetActive(value: true);
				}
				if (round.challengesIncluded.Count > 1)
				{
					challengeSprite.gameObject.SetActive(value: false);
					challengeQuestionMark.gameObject.SetActive(value: true);
				}
				else if (round.challengesIncluded.Count == 1)
				{
					challengeSprite.gameObject.SetActive(value: true);
					challengeSprite.SetSprite(GameManager.instance.challengeModeDatabase.GetItem(round.challengesIncluded[0]).challengeModeSprite);
					challengeQuestionMark.gameObject.SetActive(value: false);
				}
			}
			else if (challengeGrp != null)
			{
				challengeGrp.SetActive(value: false);
			}
			break;
		case TournamentRoundType.Tofu:
			InitTofu();
			break;
		}
	}

	private void InitBattle(bool isChampion)
	{
		NPCDefinition nPCDef = GameManager.instance.npcDatabase.GetNPCDef(_round.opponentNPCIndex);
		if (nPCDef != null)
		{
			portraitSprite.SetSprite(nPCDef.battleIconSpriteCollection, (!string.IsNullOrEmpty(nPCDef.preBattleSpriteRight)) ? nPCDef.preBattleSpriteRight : nPCDef.preBattleSprite);
		}
		else
		{
			Debug.LogErrorFormat("Tournament Enemy Portrait.cs, InitBattle(), NPC Definition is NULL and portrait sprite cannot be set, NPC Index: {0}", _round.opponentNPCIndex.ToString());
		}
		if (isChampion)
		{
			leftTrophySprite.gameObject.SetActive(value: true);
			rightTrophySprite.gameObject.SetActive(value: true);
		}
		else
		{
			leftTrophySprite.gameObject.SetActive(value: false);
			rightTrophySprite.gameObject.SetActive(value: false);
		}
	}

	private void InitTofu()
	{
		portraitAnimator.gameObject.SetActive(value: false);
		tofuBackingSprite.gameObject.SetActive(value: true);
		tofuAnimator.gameObject.SetActive(value: true);
	}

	public void ResetObject()
	{
		leftTrophySprite.gameObject.SetActive(value: false);
		rightTrophySprite.gameObject.SetActive(value: false);
		tofuBackingSprite.gameObject.SetActive(value: false);
	}
}
