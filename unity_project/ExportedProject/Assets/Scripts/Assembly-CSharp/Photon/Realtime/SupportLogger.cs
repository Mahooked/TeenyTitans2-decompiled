using System.Collections.Generic;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Realtime
{
	[AddComponentMenu("")]
	public class SupportLogger : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks
	{
		public bool LogTrafficStats;

		private bool loggedStillOfflineMessage;

		private LoadBalancingClient client;

		public LoadBalancingClient Client
		{
			get
			{
				return client;
			}
			set
			{
				if (client != value)
				{
					if (client != null)
					{
						client.RemoveCallbackTarget(this);
					}
					client = value;
					client.AddCallbackTarget(this);
				}
			}
		}

		protected void Start()
		{
			if (LogTrafficStats)
			{
				InvokeRepeating("LogStats", 10f, 10f);
			}
		}

		protected void OnApplicationPause(bool pause)
		{
			Debug.Log("SupportLogger OnApplicationPause: " + pause + " connected: " + client.IsConnected);
		}

		protected void OnApplicationQuit()
		{
			CancelInvoke();
		}

		public void LogStats()
		{
			if (client.State == ClientState.PeerCreated)
			{
				if (!loggedStillOfflineMessage)
				{
					loggedStillOfflineMessage = true;
					Debug.Log("SupportLogger Photon Client is not connected yet: this logger won't be able to capture the state.");
				}
			}
			else if (LogTrafficStats)
			{
				Debug.Log("SupportLogger " + client.LoadBalancingPeer.VitalStatsToString(false));
			}
		}

		private void LogBasics()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SupportLogger Info: ");
			stringBuilder.AppendFormat("AppID: {0}*** AppVersion: {1} PeerID: {2} ", client.AppId.Substring(0, 8), client.AppVersion, client.LoadBalancingPeer.PeerID);
			stringBuilder.AppendFormat("Server: {0} IP: {1} Region: {2}", client.NameServerHost, client.CurrentServerAddress, client.CloudRegion);
			Debug.Log(stringBuilder.ToString());
		}

		public void OnConnected()
		{
			Debug.Log("SupportLogger OnConnected().");
			LogBasics();
			if (LogTrafficStats)
			{
				client.EnableLobbyStatistics = true;
			}
		}

		public void OnConnectedToMaster()
		{
			Debug.Log("SupportLogger OnConnectedToMaster().");
		}

		public void OnFriendListUpdate(List<FriendInfo> friendList)
		{
			Debug.Log("SupportLogger OnFriendListUpdate(friendList).");
		}

		public void OnJoinedLobby()
		{
			Debug.Log(string.Concat("SupportLogger OnJoinedLobby(", client.CurrentLobby, ")."));
		}

		public void OnLeftLobby()
		{
			Debug.Log("SupportLogger OnLeftLobby().");
		}

		public void OnCreateRoomFailed(short returnCode, string message)
		{
			Debug.Log("SupportLogger OnCreateRoomFailed(" + returnCode + "," + message + ").");
		}

		public void OnJoinedRoom()
		{
			Debug.Log(string.Concat("SupportLogger OnJoinedRoom(", client.CurrentRoom, "). ", client.CurrentLobby, " GameServer:", client.GameServerAddress));
		}

		public void OnJoinRoomFailed(short returnCode, string message)
		{
		}

		public void OnJoinRandomFailed(short returnCode, string message)
		{
			Debug.Log("SupportLogger OnJoinRandomFailed(" + returnCode + "," + message + ").");
		}

		public void OnCreatedRoom()
		{
			Debug.Log(string.Concat("SupportLogger OnCreatedRoom(", client.CurrentRoom, "). ", client.CurrentLobby, " GameServer:", client.GameServerAddress));
		}

		public void OnLeftRoom()
		{
			Debug.Log("SupportLogger OnLeftRoom().");
		}

		public void OnDisconnected(DisconnectCause cause)
		{
			Debug.Log(string.Concat("SupportLogger OnDisconnected(", cause, ")."));
			LogBasics();
		}

		public void OnRegionListReceived(RegionHandler regionHandler)
		{
			Debug.Log("SupportLogger OnRegionListReceived(regionHandler).");
		}

		public void OnRoomListUpdate(List<RoomInfo> roomList)
		{
			Debug.Log("SupportLogger OnRoomListUpdate(roomList). roomList.Count: " + roomList.Count);
		}

		public void OnPlayerEnteredRoom(Player newPlayer)
		{
			Debug.Log(string.Concat("SupportLogger OnPlayerEnteredRoom(", newPlayer, ")."));
		}

		public void OnPlayerLeftRoom(Player otherPlayer)
		{
			Debug.Log(string.Concat("SupportLogger OnPlayerLeftRoom(", otherPlayer, ")."));
		}

		public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
			Debug.Log("SupportLogger OnRoomPropertiesUpdate(propertiesThatChanged).");
		}

		public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
			Debug.Log("SupportLogger OnPlayerPropertiesUpdate(targetPlayer,changedProps).");
		}

		public void OnMasterClientSwitched(Player newMasterClient)
		{
			Debug.Log(string.Concat("SupportLogger OnMasterClientSwitched(", newMasterClient, ")."));
		}

		public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
			Debug.Log("SupportLogger OnCustomAuthenticationResponse(" + data.ToStringFull() + ").");
		}

		public void OnCustomAuthenticationFailed(string debugMessage)
		{
			Debug.Log("SupportLogger OnCustomAuthenticationFailed(" + debugMessage + ").");
		}

		public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
		{
			Debug.Log("SupportLogger OnLobbyStatisticsUpdate(lobbyStatistics).");
		}
	}
}
