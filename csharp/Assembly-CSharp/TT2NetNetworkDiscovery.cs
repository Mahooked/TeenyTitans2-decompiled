using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;

public class TT2NetNetworkDiscovery : NetworkDiscovery
{
	public class DiscoveredPlayer
	{
		public string ipAddress;

		public FriendData friendData;

		public float timestamp;
	}

	[Tooltip("Number of seconds that must pass without a player broadcasting before they are removed from the list.")]
	public float playerTimeout = 2f;

	private Dictionary<string, DiscoveredPlayer> discoveredPlayers = new Dictionary<string, DiscoveredPlayer>();

	public List<FriendData> PlayerList { get; private set; }

	public event Action DiscoveredPlayersUpdated;

	private void Awake()
	{
		PlayerList = new List<FriendData>();
	}

	private void OnDisable()
	{
		discoveredPlayers.Clear();
		PlayerList.Clear();
	}

	private void LateUpdate()
	{
		bool flag = false;
		List<string> list = new List<string>(discoveredPlayers.Keys);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (Time.unscaledTime - discoveredPlayers[list[num]].timestamp >= playerTimeout)
			{
				PlayerList.Remove(discoveredPlayers[list[num]].friendData);
				discoveredPlayers.Remove(list[num]);
				flag = true;
			}
		}
		if (flag)
		{
			OnDiscoveredPlayersUpdated();
		}
	}

	public override void OnReceivedBroadcast(string fromAddress, string data)
	{
		data = data.Substring(0, data.IndexOf('}', data.IndexOf('}') + 1) + 1);
		TT2NetLocalBroadcastData tT2NetLocalBroadcastData = null;
		try
		{
			tT2NetLocalBroadcastData = JsonUtility.FromJson<TT2NetLocalBroadcastData>(data);
		}
		catch (Exception)
		{
			Debug.LogErrorFormat("[TT2NetNetworkDiscovery] - Cannot parse the broadcast data from address {0}    |    Data: {1}", fromAddress, data);
			return;
		}
		if (!(tT2NetLocalBroadcastData.friendData.playerID == Player.instance.pvpData.playerID) && !(PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion != tT2NetLocalBroadcastData.appVersion))
		{
			DiscoveredPlayer value = null;
			if (!discoveredPlayers.TryGetValue(tT2NetLocalBroadcastData.friendData.playerID, out value))
			{
				value = new DiscoveredPlayer();
				value.ipAddress = fromAddress;
				value.friendData = tT2NetLocalBroadcastData.friendData;
				discoveredPlayers.Add(tT2NetLocalBroadcastData.friendData.playerID, value);
				PlayerList.Add(tT2NetLocalBroadcastData.friendData);
				OnDiscoveredPlayersUpdated();
			}
			value.timestamp = Time.unscaledTime;
		}
	}

	public FriendData GetFriendData(string playerId)
	{
		return PlayerList.Find((FriendData x) => x.playerID == playerId);
	}

	protected void OnDiscoveredPlayersUpdated()
	{
		if (DiscoveredPlayersUpdated != null)
		{
			DiscoveredPlayersUpdated();
		}
	}
}
