using System;

[Serializable]
public class TrophyGroup
{
	public TournamentDefinition definition;

	public tk2dSprite trophySprite;

	public TrophyBanner banner;

	public TrophyGroup(TournamentDefinition newDefinition, tk2dSprite newTrophySprite, TrophyBanner newBanner)
	{
		definition = newDefinition;
		trophySprite = newTrophySprite;
		banner = newBanner;
	}
}
