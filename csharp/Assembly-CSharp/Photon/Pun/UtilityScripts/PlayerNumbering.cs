using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.UtilityScripts;

public class PlayerNumbering : MonoBehaviourPunCallbacks
{
	public delegate void PlayerNumberingChanged();

	public static PlayerNumbering instance;

	public static Photon.Realtime.Player[] SortedPlayers;

	public const string RoomPlayerIndexedProp = "pNr";

	public bool dontDestroyOnLoad;

	public static event PlayerNumberingChanged OnPlayerNumberingChanged;

	public void Awake()
	{
		if (instance != null && instance != this && instance.gameObject != null)
		{
			Object.DestroyImmediate(instance.gameObject);
		}
		instance = this;
		if (dontDestroyOnLoad)
		{
			Object.DontDestroyOnLoad(gameObject);
		}
		RefreshData();
	}

	public override void OnJoinedRoom()
	{
		RefreshData();
	}

	public override void OnLeftRoom()
	{
		PhotonNetwork.LocalPlayer.CustomProperties.Remove("pNr");
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		RefreshData();
	}

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
		RefreshData();
	}

	public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
	{
		if (changedProps != null && changedProps.ContainsKey("pNr"))
		{
			RefreshData();
		}
	}

	public void RefreshData()
	{
		if (PhotonNetwork.CurrentRoom == null)
		{
			return;
		}
		if (PhotonNetwork.LocalPlayer.GetPlayerNumber() >= 0)
		{
			SortedPlayers = PhotonNetwork.CurrentRoom.Players.Values.OrderBy(PlayerNumberingExtensions.GetPlayerNumber).ToArray();
			if (OnPlayerNumberingChanged != null)
			{
				OnPlayerNumberingChanged();
			}
			return;
		}
		HashSet<int> hashSet = new HashSet<int>();
		Photon.Realtime.Player[] array = PhotonNetwork.PlayerList.OrderBy((Photon.Realtime.Player p) => p.ActorNumber).ToArray();
		string text = "all players: ";
		Photon.Realtime.Player[] array2 = array;
		foreach (Photon.Realtime.Player player in array2)
		{
			string text2 = text;
			text = text2 + player.ActorNumber + "=pNr:" + player.GetPlayerNumber() + ", ";
			int playerNumber = player.GetPlayerNumber();
			if (player.IsLocal)
			{
				Debug.Log("PhotonNetwork.CurrentRoom.PlayerCount = " + PhotonNetwork.CurrentRoom.PlayerCount);
				for (int num2 = 0; num2 < PhotonNetwork.CurrentRoom.PlayerCount; num2++)
				{
					if (!hashSet.Contains(num2))
					{
						player.SetPlayerNumber(num2);
						break;
					}
				}
				break;
			}
			if (playerNumber < 0)
			{
				break;
			}
			hashSet.Add(playerNumber);
		}
		SortedPlayers = PhotonNetwork.CurrentRoom.Players.Values.OrderBy(PlayerNumberingExtensions.GetPlayerNumber).ToArray();
		if (OnPlayerNumberingChanged != null)
		{
			OnPlayerNumberingChanged();
		}
	}
}
