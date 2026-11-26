using System.Collections.Generic;
using UnityEngine;

public static class TournamentTracker
{
	private static int _lastRoundCompleted = -1;

	private static bool _isTournamentStarted;

	private static List<int> _usedAccessories;

	private static TournamentDefinition _activeTournament;

	public static int lastRoundCompleted => _lastRoundCompleted;

	public static int currentRoundIndex => _activeTournament.rounds.Count - (_lastRoundCompleted + 1) - 1;

	public static TournamentRound currentRound => _activeTournament.rounds[currentRoundIndex];

	public static bool isTournamentActive => _activeTournament != null && _isTournamentStarted;

	public static bool isTournamentComplete => _lastRoundCompleted >= _activeTournament.numRounds - 1;

	public static bool isTournamentStarted => _isTournamentStarted;

	public static List<int> usedAccessories
	{
		get
		{
			if (_usedAccessories == null)
			{
				_usedAccessories = new List<int>();
			}
			return _usedAccessories;
		}
	}

	public static TournamentDefinition activeTournament => _activeTournament;

	public static void TrackTournament(TournamentDefinition definition)
	{
		if (definition == _activeTournament)
		{
			Debug.LogError("This tournament is already being tracked");
			return;
		}
		_activeTournament = definition;
		_isTournamentStarted = false;
	}

	public static void StartTournament()
	{
		if (_activeTournament == null)
		{
			Debug.LogError("There is no active tournament being tracked. Start tracking a tournament before starting it.");
		}
		else if (_isTournamentStarted)
		{
			Debug.LogError("Tournament is already started");
		}
		else
		{
			_isTournamentStarted = true;
		}
	}

	public static void CompleteCurrentRound()
	{
		_lastRoundCompleted++;
	}

	public static void FinishTournament()
	{
		_activeTournament = null;
		_isTournamentStarted = false;
		_lastRoundCompleted = -1;
		usedAccessories.Clear();
		Player.instance.HealActiveParty();
		Debug.Log("Active Tournament Ended");
	}
}
