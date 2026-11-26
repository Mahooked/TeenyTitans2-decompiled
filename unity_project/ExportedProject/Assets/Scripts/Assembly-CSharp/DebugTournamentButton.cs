using UnityEngine;

[RequireComponent(typeof(tk2dUIItem))]
public class DebugTournamentButton : MonoBehaviour
{
	public TournamentDefinition tournament;

	public tk2dUIItem uiItem;

	private void Start()
	{
		uiItem.OnClick += HandleOnClick;
	}

	private void HandleOnClick()
	{
		if (!(CityController.instance == null))
		{
			PauseManager.instance.Resume();
			if (tournament.name.Equals("FinalTournament"))
			{
				CoreGameTutorial.instance.SkipAllTutorials();
				Player.instance.AddMission(37);
				TournamentController.initDefinition = tournament;
				TournamentController.isDebugTournament = false;
				LoadingScreenCover.LoadScene("Tournament");
			}
			else
			{
				CityController.instance.DebugPlayTournament(tournament);
			}
		}
	}
}
