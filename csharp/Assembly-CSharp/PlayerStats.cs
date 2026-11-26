using System;

[Serializable]
public class PlayerStats
{
	public int numFiguresPurchased;

	public int numWins;

	public int numLosses;

	public int numDailyMissionsCompleted;

	public PlayerStats Copy()
	{
		PlayerStats playerStats = new PlayerStats();
		playerStats.numFiguresPurchased = numFiguresPurchased;
		playerStats.numWins = numWins;
		playerStats.numLosses = numLosses;
		playerStats.numDailyMissionsCompleted = numDailyMissionsCompleted;
		return playerStats;
	}
}
