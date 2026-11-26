using System;
using System.Collections.Generic;

[Serializable]
public class TournamentRound
{
	public TournamentRoundType type;

	public int opponentDifficulty = 5;

	public int opponentNPCIndex;

	public List<TournamentRoundFigureData> figures;

	public int xpReward;

	public int coinReward;

	public bool challengesOn;

	public bool challengeHidden;

	public List<int> challengesIncluded;

	public Conversation preBattleConversation;

	public string battleThemePath = string.Empty;

	public PlayerAIType playerAIType;

	public int accessoryUID = -1;

	public List<TournamentTofuData> tofuData;

	public TournamentRound()
	{
		figures = new List<TournamentRoundFigureData>();
		tofuData = new List<TournamentTofuData>();
	}
}
