using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Photon.Pun.UtilityScripts;

public class PunTeams : MonoBehaviourPunCallbacks
{
	public enum Team : byte
	{
		none,
		red,
		blue
	}

	public static Dictionary<Team, List<Photon.Realtime.Player>> PlayersPerTeam;

	public const string TeamPlayerProp = "team";

	public void Start()
	{
		PlayersPerTeam = new Dictionary<Team, List<Photon.Realtime.Player>>();
		Array values = Enum.GetValues(typeof(Team));
		foreach (object item in values)
		{
			PlayersPerTeam[(Team)item] = new List<Photon.Realtime.Player>();
		}
	}

	public override void OnDisable()
	{
		PlayersPerTeam = new Dictionary<Team, List<Photon.Realtime.Player>>();
	}

	public override void OnJoinedRoom()
	{
		UpdateTeams();
	}

	public override void OnLeftRoom()
	{
		Start();
	}

	public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
	{
		UpdateTeams();
	}

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
		UpdateTeams();
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		UpdateTeams();
	}

	public void UpdateTeams()
	{
		Array values = Enum.GetValues(typeof(Team));
		foreach (object item in values)
		{
			PlayersPerTeam[(Team)item].Clear();
		}
		for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
		{
			Photon.Realtime.Player player = PhotonNetwork.PlayerList[i];
			Team team = player.GetTeam();
			PlayersPerTeam[team].Add(player);
		}
	}
}
