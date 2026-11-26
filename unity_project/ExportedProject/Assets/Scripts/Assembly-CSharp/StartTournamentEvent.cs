public class StartTournamentEvent : GameEvent
{
	public TournamentDefinition tournament { get; private set; }

	public StartTournamentEvent(TournamentDefinition tournament)
	{
		this.tournament = tournament;
	}
}
