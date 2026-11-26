using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;

public class TT2NetChat : MonoBehaviour, IChatClientListener
{
	private static TT2NetChat instance;

	private static bool applicationDidQuit;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> PlayerAcceptedBattleRequest__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> PlayerDeclinedBattleRequest__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> BattleRequestCanceled__BackingField;

	private ChatClient chatClient;

	public static TT2NetChat Instance
	{
		get
		{
			if (instance == null && !applicationDidQuit)
			{
				instance = new GameObject("TT2NetChat").AddComponent<TT2NetChat>();
			}
			return instance;
		}
	}

	public bool IsConnected
	{
		get
		{
			return chatClient != null;
		}
	}

	public string ChatRegion
	{
		get
		{
			return (chatClient == null) ? string.Empty : chatClient.ChatRegion;
		}
	}

	public event Action<string> PlayerAcceptedBattleRequest
	{
		add
		{
			Action<string> action = PlayerAcceptedBattleRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerAcceptedBattleRequest__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = PlayerAcceptedBattleRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerAcceptedBattleRequest__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> PlayerDeclinedBattleRequest
	{
		add
		{
			Action<string> action = PlayerDeclinedBattleRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerDeclinedBattleRequest__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = PlayerDeclinedBattleRequest__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerDeclinedBattleRequest__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> BattleRequestCanceled
	{
		add
		{
			Action<string> action = BattleRequestCanceled__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestCanceled__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = BattleRequestCanceled__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestCanceled__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		Player.instance.pvpData.FriendAdded += PvpData_FriendAdded;
		Player.instance.pvpData.FriendRemoved += PvpData_FriendRemoved;
		Player.instance.pvpData.FriendRequestAdded += PvpData_FriendRequestAdded;
		Player.instance.pvpData.FriendRequestAccepted += PvpData_FriendRequestAccepted;
		Player.instance.pvpData.FriendRequestDeclined += PvpData_FriendRequestDeclined;
		Player.instance.pvpData.BattleRequestAdded += PvpData_BattleRequestAdded;
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
		Player.instance.pvpData.BattleRequestDeclined += PvpData_BattleRequestDeclined;
	}

	private void Update()
	{
		if (chatClient != null)
		{
			chatClient.Service();
		}
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.pvpData.FriendAdded -= PvpData_FriendAdded;
			Player.instance.pvpData.FriendRemoved -= PvpData_FriendRemoved;
			Player.instance.pvpData.FriendRequestAdded -= PvpData_FriendRequestAdded;
			Player.instance.pvpData.FriendRequestAccepted -= PvpData_FriendRequestAccepted;
			Player.instance.pvpData.FriendRequestDeclined -= PvpData_FriendRequestDeclined;
			Player.instance.pvpData.BattleRequestAdded -= PvpData_BattleRequestAdded;
			Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
			Player.instance.pvpData.BattleRequestDeclined -= PvpData_BattleRequestDeclined;
		}
		if (chatClient != null)
		{
			chatClient.Disconnect();
		}
	}

	private void OnApplicationQuit()
	{
		applicationDidQuit = true;
	}

	public void Connect(string userId)
	{
		if (chatClient == null)
		{
			chatClient = new ChatClient(this);
			chatClient.ChatRegion = "US";
			chatClient.UseBackgroundWorkerForSending = true;
			chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion, new AuthenticationValues(userId));
		}
	}

	public void Disconnect()
	{
		if (chatClient != null)
		{
			chatClient.Disconnect();
		}
	}

	public void SendPrivateMessage(string userId, string message)
	{
		if (chatClient != null && chatClient.CanChat)
		{
			chatClient.SendPrivateMessage(userId, message);
			Log(string.Format("Sent PRIVATE message to {0}. Message is: {1}", userId, message));
		}
	}

	public void SendPrivateMessage(string userId, object message)
	{
		if (chatClient != null && chatClient.CanChat)
		{
			chatClient.SendPrivateMessage(userId, SerializedData(message));
			Log(string.Format("Sent PRIVATE message to {0}. Message is: {1}", userId, message));
		}
	}

	public void SendBattleRequest(string userId, string roomName, bool isLocal = false)
	{
		BattleRequest battleRequest = new BattleRequest();
		battleRequest.playerId = Player.instance.pvpData.playerID;
		battleRequest.region = PhotonNetwork.CloudRegion;
		battleRequest.roomName = roomName;
		battleRequest.isLocal = isLocal;
		SendPrivateMessage(userId, battleRequest);
	}

	public void CancelBattleRequest(string userId)
	{
		SendPrivateMessage(userId, "BattleRequestCanceled");
		if (BattleRequestCanceled__BackingField != null)
		{
			BattleRequestCanceled__BackingField(userId);
		}
	}

	public void UpdateUserStatus(int userStatus)
	{
		if (chatClient != null)
		{
			chatClient.SetOnlineStatus(userStatus);
		}
	}

	private void PvpData_FriendAdded(string playerId)
	{
		Log("Friend Added with Player ID: " + playerId);
		if (chatClient != null)
		{
			Log("Add Friends to Chat Server");
			chatClient.RemoveFriends(new string[1] { playerId });
			chatClient.AddFriends(new string[1] { playerId });
			if (!Player.instance.pvpData.GetFriend(playerId).requestAccepted)
			{
				Log("Attempt to send a Friend Request to the new friend");
				SendPrivateMessage(playerId, Player.instance.pvpData.MyFriendRequest());
			}
		}
	}

	private void PvpData_FriendRemoved(string playerId)
	{
		if (chatClient != null)
		{
			chatClient.RemoveFriends(new string[1] { playerId });
			if (Player.instance.pvpData.pendingRemovedFriends.Contains(playerId))
			{
				SendPrivateMessage(playerId, "FriendRemoved");
			}
		}
	}

	private void PvpData_FriendRequestAdded(FriendRequest friendRequest)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_multiplayerinvite1", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void PvpData_FriendRequestAccepted(string playerId)
	{
		Log("Accepted Friend Request from player with Player ID: " + playerId);
		SendPrivateMessage(playerId, "FriendRequestAccepted");
	}

	private void PvpData_FriendRequestDeclined(string playerId)
	{
		Log("Declined Friend Request from player with Player ID: " + playerId);
		SendPrivateMessage(playerId, "FriendRequestDeclined");
	}

	private void PvpData_BattleRequestAdded(BattleRequest battleRequest)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_multiplayerinvite1", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		Log("Accepted Battle Request from player with Player ID: " + battleRequest.playerId);
		SendPrivateMessage(battleRequest.playerId, "BattleRequestAccepted");
	}

	private void PvpData_BattleRequestDeclined(string playerId)
	{
		Log("Declined Battle Request from player with Player ID: " + playerId);
		SendPrivateMessage(playerId, "BattleRequestDeclined");
	}

	private byte[] SerializedData(object data)
	{
		if (data == null)
		{
			return null;
		}
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream.ToArray();
	}

	private object DeserializeData(byte[] bytes)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		memoryStream.Write(bytes, 0, bytes.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		return binaryFormatter.Deserialize(memoryStream);
	}

	private void ProcessMessageString(string sender, string message, string channelName)
	{
		if (string.IsNullOrEmpty(message))
		{
			return;
		}
		switch (message)
		{
		case "FriendRequestAccepted":
		{
			FriendData friend = Player.instance.pvpData.GetFriend(sender);
			if (friend != null)
			{
				friend.requestAccepted = true;
				friend.ForceUpdatedEvent();
				Player.instance.Save();
				SendPrivateMessage(sender, "FriendRequestResponseAcknowledged");
			}
			break;
		}
		case "FriendRequestDeclined":
			if (Player.instance.pvpData.HasFriend(sender))
			{
				Player.instance.pvpData.RemoveFriend(sender);
				Player.instance.Save();
				SendPrivateMessage(sender, "FriendRequestResponseAcknowledged");
			}
			break;
		case "FriendRequestResponseAcknowledged":
		{
			FriendRequest friendRequest = Player.instance.pvpData.GetFriendRequest(sender);
			if (friendRequest != null && friendRequest.status != FriendRequest.Status.Pending)
			{
				Player.instance.pvpData.RemoveFriendRequest(sender);
				Player.instance.Save();
			}
			break;
		}
		case "FriendRemoved":
			if (Player.instance.pvpData.HasFriend(sender))
			{
				Player.instance.pvpData.RemoveFriend(sender, false);
				Player.instance.Save();
				SendPrivateMessage(sender, "FriendRemovedAcknowledged");
			}
			else if (Player.instance.pvpData.HasFriendRequest(sender))
			{
				Player.instance.pvpData.RemoveFriendRequest(sender);
				Player.instance.Save();
			}
			break;
		case "FriendRemovedAcknowledged":
			if (Player.instance.pvpData.pendingRemovedFriends.Contains(sender))
			{
				Player.instance.pvpData.pendingRemovedFriends.Remove(sender);
				Player.instance.Save();
				if (chatClient != null)
				{
					chatClient.RemoveFriends(new string[1] { sender });
				}
			}
			break;
		case "BattleRequestAccepted":
			if (PlayerAcceptedBattleRequest__BackingField != null)
			{
				PlayerAcceptedBattleRequest__BackingField(sender);
			}
			break;
		case "BattleRequestDeclined":
			if (PlayerDeclinedBattleRequest__BackingField != null)
			{
				PlayerDeclinedBattleRequest__BackingField(sender);
			}
			break;
		case "BattleRequestCanceled":
			Player.instance.pvpData.RemoveBattleRequest(sender);
			break;
		}
	}

	private void ProcessMessageData(string sender, object data, string channelName)
	{
		if (data == null)
		{
			return;
		}
		if (data is FriendData)
		{
			FriendData friendData = (FriendData)data;
			if (friendData != null)
			{
				Log(string.Format("Received PRIVATE message from {0} in channel {1}. Message is a FriendData instance | portraitSpriteName: {2}", sender, channelName, friendData.portraitSpriteName));
				if (Player.instance.pvpData.HasFriend(friendData.playerID))
				{
					Player.instance.pvpData.UpdateFriendData(friendData);
				}
				else
				{
					Player.instance.pvpData.AddFriend(friendData);
				}
				Player.instance.Save();
			}
		}
		else if (data is FriendRequest)
		{
			FriendRequest friendRequest = (FriendRequest)data;
			if (friendRequest != null)
			{
				Log(string.Format("Received PRIVATE message from {0} in channel {1}. Message is a FriendRequest instance | playerName: {2}", sender, channelName, friendRequest.playerName));
				if (Player.instance.pvpData.HasFriendRequest(friendRequest.playerId))
				{
					Player.instance.pvpData.UpdateFriendRequest(friendRequest);
				}
				else
				{
					Player.instance.pvpData.AddFriendRequest(friendRequest);
				}
				Player.instance.Save();
				SendPrivateMessage(friendRequest.playerId, Player.instance.pvpData.MyFriendData());
			}
		}
		else
		{
			if (!(data is BattleRequest))
			{
				return;
			}
			BattleRequest battleRequest = (BattleRequest)data;
			if (battleRequest != null)
			{
				Log(string.Format("Received PRIVATE message from {0} in channel {1}. Message is a BattleRequest instance | playerId: {2}    region: {3}    roomName: {4}", sender, channelName, battleRequest.playerId, battleRequest.region, battleRequest.roomName));
				if (!Player.instance.pvpData.HasBattleRequest(battleRequest.playerId) && (battleRequest.isLocal || Player.instance.pvpData.HasFriend(battleRequest.playerId)))
				{
					Player.instance.pvpData.AddBattleRequest(battleRequest);
				}
			}
		}
	}

	private void Log(string message, DebugLevel level = DebugLevel.INFO)
	{
		switch (level)
		{
		case DebugLevel.ERROR:
			UnityEngine.Debug.LogErrorFormat("[TT2NetChat] - {0}", message);
			break;
		case DebugLevel.WARNING:
			UnityEngine.Debug.LogWarningFormat("[TT2NetChat] - {0}", message);
			break;
		default:
			UnityEngine.Debug.LogFormat("[TT2NetChat] - {0}", message);
			break;
		}
	}

	public void DebugReturn(DebugLevel level, string message)
	{
		Log(message, level);
	}

	public void OnChatStateChange(ChatState state)
	{
		Log("Chat State changed to: " + state);
	}

	public void OnConnected()
	{
		Log("Chat connected");
		Player.instance.pvpData.hasConnected = true;
		chatClient.SetOnlineStatus(2);
		chatClient.AddFriends(Player.instance.pvpData.GetFriendPlayerIds());
		chatClient.AddFriends(Player.instance.pvpData.pendingRemovedFriends.ToArray());
		for (int i = 0; i < Player.instance.pvpData.friendRequests.Count; i++)
		{
			if (Player.instance.pvpData.friendRequests[i].status == FriendRequest.Status.Pending)
			{
				SendPrivateMessage(Player.instance.pvpData.friendRequests[i].playerId, Player.instance.pvpData.MyFriendData());
			}
			else if (Player.instance.pvpData.friendRequests[i].status == FriendRequest.Status.Accepted)
			{
				SendPrivateMessage(Player.instance.pvpData.friendRequests[i].playerId, "FriendRequestAccepted");
			}
			else
			{
				SendPrivateMessage(Player.instance.pvpData.friendRequests[i].playerId, "FriendRequestDeclined");
			}
		}
		for (int j = 0; j < Player.instance.pvpData.pendingRemovedFriends.Count; j++)
		{
			SendPrivateMessage(Player.instance.pvpData.pendingRemovedFriends[j], "FriendRemoved");
		}
	}

	public void OnDisconnected()
	{
		Log("Chat disconnected");
		chatClient = null;
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		Log(string.Format("Received public messages from channel: {0}", channelName));
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		if (!sender.Equals(chatClient.UserId))
		{
			if (message is string)
			{
				Log(string.Format("Received PRIVATE message from {0} in channel {1}. Message is: {2}", sender, channelName, message));
				ProcessMessageString(sender, (string)message, channelName);
			}
			else
			{
				ProcessMessageData(sender, DeserializeData((byte[])message), channelName);
			}
		}
	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Log(string.Format("User Status updated:    User: {0}    Status: {1}    Got Message?: {2}    Message: {3}", user, status, gotMessage, message));
		FriendData friend = Player.instance.pvpData.GetFriend(user);
		if (friend != null)
		{
			friend.SetOnlineStatus(status >= 2);
			friend.SetInBattleStatus(status == 6);
			if (friend.IsOnline)
			{
				if (friend.requestAccepted)
				{
					SendPrivateMessage(user, Player.instance.pvpData.MyFriendData());
				}
				else
				{
					SendPrivateMessage(user, Player.instance.pvpData.MyFriendRequest());
				}
			}
			else
			{
				Player.instance.pvpData.RemoveBattleRequest(user);
			}
		}
		else if (status >= 2)
		{
			SendPrivateMessage(user, "FriendRemoved");
		}
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		Log("Subscribed to channel(s)");
	}

	public void OnUnsubscribed(string[] channels)
	{
		Log("Unsubscribed to channel(s)");
	}
}
