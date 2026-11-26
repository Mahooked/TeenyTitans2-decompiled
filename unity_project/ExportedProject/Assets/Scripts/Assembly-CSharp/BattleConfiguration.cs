using System;

[Serializable]
public class BattleConfiguration
{
	public TournamentRoundFigureData[] figures = new TournamentRoundFigureData[0];

	public int teamSizeWeight1 = 50;

	public int teamSizeWeight2 = 50;

	public int teamSizeWeight3 = 50;

	public int equippedAccessoryUID = -1;

	public int xpReward;

	public int coinReward;
}
