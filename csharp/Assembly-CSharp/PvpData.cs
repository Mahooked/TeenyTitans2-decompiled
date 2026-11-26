using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class PvpData
{
	[Serializable]
	public class ViewedPortrait
	{
		public string portrait;

		public bool isViewed;

		public ViewedPortrait(string portraitName, bool isViewed = false)
		{
			portrait = portraitName;
			this.isViewed = isViewed;
		}
	}

	public string playerID;

	[SerializeField]
	private string playerName;

	public int level;

	public int currentXP;

	public int skillRating;

	public List<FriendData> friends;

	public List<FriendRequest> friendRequests;

	public List<string> pendingRemovedFriends = new List<string>();

	[SerializeField]
	private string portraitSpriteName;

	public int currentWinStreak;

	public bool lastMatchIsLoss;

	[OptionalField]
	public bool isInPvpBattle;

	[OptionalField]
	public BattleType isInPvpBattleType;

	[SerializeField]
	private int previousLevel;

	[SerializeField]
	private int previousXP;

	public int previousSkillRating;

	public int winXPReceived;

	public int humanWinXPReceived;

	public int winStreakXPReceived;

	public int coinsReceived;

	public bool hasConnected;

	public List<string> allPortraitsUnlocked = new List<string>();

	[OptionalField]
	public List<ViewedPortrait> viewedPortraits;

	public List<int> triggeredConvosPlayed = new List<int>();

	public List<int> portraitsUnlocked = new List<int>();

	private List<BattleRequest> battleRequests;

	public bool AllPortraitsViewed => viewedPortraits.Find((ViewedPortrait vp) => !vp.isViewed) == null;

	public string PlayerName
	{
		get
		{
			return playerName;
		}
		set
		{
			if (value != playerName)
			{
				playerName = value;
				if (PlayerNameUpdated != null)
				{
					PlayerNameUpdated();
				}
			}
		}
	}

	public string PortraitSpriteName
	{
		get
		{
			return portraitSpriteName;
		}
		set
		{
			if (value != portraitSpriteName)
			{
				portraitSpriteName = value;
				if (PortraitSpriteUpdated != null)
				{
					PortraitSpriteUpdated();
				}
			}
		}
	}

	public int PreviousLevel
	{
		get
		{
			return previousLevel;
		}
		set
		{
			if (value != previousLevel)
			{
				previousLevel = value;
				if (PreviousLevelUpdated != null)
				{
					PreviousLevelUpdated();
				}
			}
		}
	}

	public int PreviousXP
	{
		get
		{
			return previousXP;
		}
		set
		{
			Debug.Log("Set Previous XP, value: " + value);
			if (value != previousXP)
			{
				previousXP = value;
				if (PreviousXPUpdated != null)
				{
					PreviousXPUpdated();
				}
			}
		}
	}

	public List<BattleRequest> BattleRequests
	{
		get
		{
			if (battleRequests == null)
			{
				battleRequests = new List<BattleRequest>();
			}
			return battleRequests;
		}
	}

	public int PendingRequestCount => BattleRequests.Count + friendRequests.FindAll((FriendRequest request) => request.status == FriendRequest.Status.Pending).Count;

	public event Action PlayerNameUpdated;

	public event Action PortraitSpriteUpdated;

	public event Action PortraitUnlocked;

	public event Action PortraitsViewedUpdated;

	public event Action PreviousLevelUpdated;

	public event Action PreviousXPUpdated;

	public event Action FriendsListUpdated;

	public event Action<string> FriendAdded;

	public event Action<string> FriendRemoved;

	public event Action FriendRequestsUpdated;

	public event Action<FriendRequest> FriendRequestAdded;

	public event Action<string> FriendRequestRemoved;

	public event Action BattleRequestsUpdated;

	public event Action<BattleRequest> BattleRequestAdded;

	public event Action<string> BattleRequestRemoved;

	public event Action<string> FriendRequestAccepted;

	public event Action<string> FriendRequestDeclined;

	public event Action<BattleRequest> BattleRequestAccepted;

	public event Action<string> BattleRequestDeclined;

	public PvpData()
	{
		playerID = string.Empty;
		playerName = string.Empty;
		level = 1;
		currentXP = 0;
		skillRating = 2500;
		portraitSpriteName = string.Empty;
		friends = new List<FriendData>();
		friendRequests = new List<FriendRequest>();
		pendingRemovedFriends = new List<string>();
		allPortraitsUnlocked = new List<string>();
		viewedPortraits = new List<ViewedPortrait>();
		triggeredConvosPlayed = new List<int>();
		previousLevel = level;
		previousXP = currentXP;
		previousSkillRating = skillRating;
		currentWinStreak = 0;
		winXPReceived = 0;
		humanWinXPReceived = 0;
		winStreakXPReceived = 0;
		coinsReceived = 0;
		hasConnected = false;
		portraitsUnlocked = new List<int>();
	}

	public PvpData(string newID, string name)
	{
		playerID = newID;
		playerName = name;
		level = 1;
		currentXP = 0;
		skillRating = 2500;
		portraitSpriteName = string.Empty;
		friends = new List<FriendData>();
		friendRequests = new List<FriendRequest>();
		pendingRemovedFriends = new List<string>();
		allPortraitsUnlocked = new List<string>();
		viewedPortraits = new List<ViewedPortrait>();
		triggeredConvosPlayed = new List<int>();
		previousLevel = level;
		previousXP = currentXP;
		previousSkillRating = skillRating;
		currentWinStreak = 0;
		winXPReceived = 0;
		humanWinXPReceived = 0;
		winStreakXPReceived = 0;
		coinsReceived = 0;
		hasConnected = false;
		portraitsUnlocked = new List<int>();
	}

	public PvpData Copy()
	{
		PvpData pvpData = new PvpData();
		pvpData.playerID = playerID;
		pvpData.playerName = playerName;
		pvpData.level = level;
		pvpData.currentXP = currentXP;
		pvpData.skillRating = skillRating;
		pvpData.portraitSpriteName = portraitSpriteName;
		pvpData.friends = new List<FriendData>();
		if (friends != null)
		{
			for (int i = 0; i < friends.Count; i++)
			{
				pvpData.friends.Add(friends[i].Copy());
			}
		}
		pvpData.friendRequests = new List<FriendRequest>();
		if (friendRequests != null)
		{
			for (int j = 0; j < friendRequests.Count; j++)
			{
				pvpData.friendRequests.Add(friendRequests[j].Copy());
			}
		}
		pvpData.pendingRemovedFriends = ((pendingRemovedFriends == null) ? new List<string>() : new List<string>(pendingRemovedFriends));
		pvpData.allPortraitsUnlocked = ((allPortraitsUnlocked == null) ? new List<string>() : new List<string>(allPortraitsUnlocked));
		pvpData.viewedPortraits = ((viewedPortraits == null) ? new List<ViewedPortrait>() : new List<ViewedPortrait>(viewedPortraits));
		pvpData.previousLevel = previousLevel;
		pvpData.previousXP = previousXP;
		pvpData.previousSkillRating = previousSkillRating;
		pvpData.currentWinStreak = currentWinStreak;
		pvpData.lastMatchIsLoss = lastMatchIsLoss;
		pvpData.isInPvpBattle = isInPvpBattle;
		pvpData.isInPvpBattleType = isInPvpBattleType;
		pvpData.winXPReceived = winXPReceived;
		pvpData.humanWinXPReceived = humanWinXPReceived;
		pvpData.winStreakXPReceived = winStreakXPReceived;
		pvpData.coinsReceived = coinsReceived;
		pvpData.hasConnected = hasConnected;
		pvpData.portraitsUnlocked = new List<int>();
		pvpData.triggeredConvosPlayed = ((triggeredConvosPlayed == null) ? new List<int>() : new List<int>(triggeredConvosPlayed));
		return pvpData;
	}

	public bool IsPortraitUnlocked(string portraitName)
	{
		return allPortraitsUnlocked.Contains(portraitName);
	}

	public void UnlockPortrait(string portraitName)
	{
		if (!IsPortraitUnlocked(portraitName))
		{
			allPortraitsUnlocked.Add(portraitName);
			if (PortraitUnlocked != null)
			{
				PortraitUnlocked();
			}
		}
	}

	public void AddViewablePortrait(string portraitName)
	{
		if (viewedPortraits.Find((ViewedPortrait vp) => vp.portrait == portraitName) == null)
		{
			viewedPortraits.Add(new ViewedPortrait(portraitName));
		}
		if (PortraitsViewedUpdated != null)
		{
			PortraitsViewedUpdated();
		}
	}

	public void SetPortraitViewed(string portraitName, bool isViewed)
	{
		ViewedPortrait viewedPortrait = viewedPortraits.Find((ViewedPortrait vp) => vp.portrait == portraitName);
		if (viewedPortrait != null)
		{
			viewedPortrait.isViewed = isViewed;
			if (PortraitsViewedUpdated != null)
			{
				PortraitsViewedUpdated();
			}
		}
	}

	public bool IsPortraitViewed(string portraitName)
	{
		return viewedPortraits.Find((ViewedPortrait vp) => vp.portrait == portraitName)?.isViewed ?? false;
	}

	public void AddFriend(string playerId)
	{
		if (!string.IsNullOrEmpty(playerId) && !HasFriend(playerId) && !(playerId == playerID))
		{
			if (pendingRemovedFriends.Contains(playerId))
			{
				pendingRemovedFriends.Remove(playerId);
			}
			FriendData friendData = new FriendData();
			friendData.playerID = playerId;
			friendData.name = string.Empty;
			friendData.portraitSpriteName = string.Empty;
			friendData.requestAccepted = false;
			FriendData item = friendData;
			friends.Add(item);
			Player.instance.Save();
			if (FriendAdded != null)
			{
				FriendAdded(playerId);
			}
			if (FriendsListUpdated != null)
			{
				FriendsListUpdated();
			}
		}
	}

	public void AddFriend(FriendData data)
	{
		if (!string.IsNullOrEmpty(data.playerID) && !HasFriend(data.playerID) && !(data.playerID == playerID))
		{
			if (pendingRemovedFriends.Contains(data.playerID))
			{
				pendingRemovedFriends.Remove(data.playerID);
			}
			friends.Add(data.Copy());
			Player.instance.Save();
			if (FriendAdded != null)
			{
				FriendAdded(data.playerID);
			}
			if (FriendsListUpdated != null)
			{
				FriendsListUpdated();
			}
		}
	}

	public void RemoveFriend(string playerId, bool addToPendingRemovedList = true)
	{
		bool flag = false;
		for (int num = friends.Count - 1; num >= 0; num--)
		{
			if (friends[num].playerID == playerId)
			{
				DeclineBattleRequest(playerId);
				if (addToPendingRemovedList)
				{
					pendingRemovedFriends.Add(playerId);
				}
				friends.RemoveAt(num);
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Player.instance.Save();
			if (FriendRemoved != null)
			{
				FriendRemoved(playerId);
			}
			if (FriendsListUpdated != null)
			{
				FriendsListUpdated();
			}
		}
	}

	public void RemoveAllFriends()
	{
		if (friends.Count <= 0)
		{
			return;
		}
		for (int num = friends.Count - 1; num >= 0; num--)
		{
			string text = friends[num].playerID;
			DeclineBattleRequest(text);
			friends.RemoveAt(num);
			if (FriendRemoved != null)
			{
				FriendRemoved(text);
			}
		}
		if (FriendsListUpdated != null)
		{
			FriendsListUpdated();
		}
	}

	public void UpdateFriendData(FriendData friendData)
	{
		FriendData friend = GetFriend(friendData.playerID);
		if (friend != null)
		{
			friend.level = friendData.level;
			friend.name = friendData.name;
			friend.portraitSpriteName = friendData.portraitSpriteName;
			friend.skillRating = friendData.skillRating;
			friend.ForceUpdatedEvent();
		}
	}

	public FriendData GetFriend(string playerId)
	{
		for (int i = 0; i < friends.Count; i++)
		{
			if (friends[i].playerID == playerId)
			{
				return friends[i];
			}
		}
		return null;
	}

	public bool HasFriend(string playerId)
	{
		for (int i = 0; i < friends.Count; i++)
		{
			if (friends[i].playerID == playerId)
			{
				return true;
			}
		}
		return false;
	}

	public string[] GetFriendPlayerIds()
	{
		string[] array = new string[friends.Count];
		for (int i = 0; i < friends.Count; i++)
		{
			array[i] = friends[i].playerID;
		}
		return array;
	}

	public void AddFriendRequest(FriendRequest friendRequest)
	{
		if (!HasFriendRequest(friendRequest.playerId))
		{
			friendRequests.Add(friendRequest.Copy());
			LODRequestFlag.ShowInvite(friendRequest);
			if (FriendRequestAdded != null)
			{
				FriendRequestAdded(friendRequest);
			}
			if (FriendRequestsUpdated != null)
			{
				FriendRequestsUpdated();
			}
		}
	}

	public void RemoveFriendRequest(string playerId)
	{
		bool flag = false;
		for (int num = friendRequests.Count - 1; num >= 0; num--)
		{
			if (friendRequests[num].playerId == playerId)
			{
				friendRequests.RemoveAt(num);
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Player.instance.Save();
			if (FriendRequestRemoved != null)
			{
				FriendRequestRemoved(playerId);
			}
			if (FriendRequestsUpdated != null)
			{
				FriendRequestsUpdated();
			}
		}
	}

	public void RemoveAllFriendRequests()
	{
		if (friendRequests.Count <= 0)
		{
			return;
		}
		for (int num = friendRequests.Count - 1; num >= 0; num--)
		{
			string playerId = friendRequests[num].playerId;
			friendRequests.RemoveAt(num);
			if (FriendRequestRemoved != null)
			{
				FriendRequestRemoved(playerId);
			}
		}
		if (FriendRequestsUpdated != null)
		{
			FriendRequestsUpdated();
		}
	}

	public void UpdateFriendRequest(FriendRequest friendRequest)
	{
		FriendRequest friendRequest2 = GetFriendRequest(friendRequest.playerId);
		if (friendRequest2 != null)
		{
			friendRequest2.playerName = friendRequest.playerName;
			friendRequest2.ForceUpdatedEvent();
		}
	}

	public FriendRequest GetFriendRequest(string playerId)
	{
		for (int i = 0; i < friendRequests.Count; i++)
		{
			if (friendRequests[i].playerId == playerId)
			{
				return friendRequests[i];
			}
		}
		return null;
	}

	public bool HasFriendRequest(string playerId)
	{
		for (int i = 0; i < friendRequests.Count; i++)
		{
			if (friendRequests[i].playerId == playerId)
			{
				return true;
			}
		}
		return false;
	}

	public void AcceptFriendRequest(string playerId)
	{
		FriendRequest friendRequest = GetFriendRequest(playerId);
		if (friendRequest != null && friendRequest.status == FriendRequest.Status.Pending)
		{
			FriendData friendData = new FriendData();
			friendData.playerID = friendRequest.playerId;
			friendData.name = friendRequest.playerName;
			friendData.portraitSpriteName = friendRequest.portraitSpriteName;
			friendData.level = friendRequest.level;
			friendData.skillRating = friendRequest.skillRating;
			friendData.requestAccepted = true;
			AddFriend(friendData);
			friendRequest.status = FriendRequest.Status.Accepted;
			if (FriendRequestAccepted != null)
			{
				FriendRequestAccepted(playerId);
			}
		}
	}

	public void DeclineFriendRequest(string playerId)
	{
		FriendRequest friendRequest = GetFriendRequest(playerId);
		if (friendRequest != null)
		{
			friendRequest.status = FriendRequest.Status.Declined;
			if (FriendRequestDeclined != null)
			{
				FriendRequestDeclined(playerId);
			}
		}
	}

	public void AddBattleRequest(BattleRequest battleRequest)
	{
		if (!HasBattleRequest(battleRequest.playerId))
		{
			BattleRequests.Add(battleRequest.Copy());
			LODRequestFlag.ShowInvite(battleRequest);
			if (BattleRequestAdded != null)
			{
				BattleRequestAdded(battleRequest);
			}
			if (BattleRequestsUpdated != null)
			{
				BattleRequestsUpdated();
			}
		}
	}

	public void RemoveBattleRequest(string playerId)
	{
		bool flag = false;
		for (int num = BattleRequests.Count - 1; num >= 0; num--)
		{
			if (BattleRequests[num].playerId == playerId)
			{
				BattleRequests.RemoveAt(num);
				flag = true;
				break;
			}
		}
		if (flag)
		{
			if (BattleRequestRemoved != null)
			{
				BattleRequestRemoved(playerId);
			}
			if (BattleRequestsUpdated != null)
			{
				BattleRequestsUpdated();
			}
		}
	}

	public void RemoveAllBattleRequest()
	{
		if (BattleRequests.Count <= 0)
		{
			return;
		}
		for (int num = BattleRequests.Count - 1; num >= 0; num--)
		{
			string playerId = BattleRequests[num].playerId;
			BattleRequests.RemoveAt(num);
			if (BattleRequestRemoved != null)
			{
				BattleRequestRemoved(playerId);
			}
		}
		if (BattleRequestsUpdated != null)
		{
			BattleRequestsUpdated();
		}
	}

	public BattleRequest GetBattleRequest(string playerId)
	{
		for (int i = 0; i < BattleRequests.Count; i++)
		{
			if (BattleRequests[i].playerId == playerId)
			{
				return BattleRequests[i];
			}
		}
		return null;
	}

	public bool HasBattleRequest(string playerId)
	{
		for (int i = 0; i < BattleRequests.Count; i++)
		{
			if (BattleRequests[i].playerId == playerId)
			{
				return true;
			}
		}
		return false;
	}

	public void AcceptBattleRequest(string playerId)
	{
		BattleRequest battleRequest = GetBattleRequest(playerId);
		if (battleRequest != null)
		{
			RemoveBattleRequest(playerId);
			if (BattleRequestAccepted != null)
			{
				BattleRequestAccepted(battleRequest);
			}
		}
	}

	public void DeclineBattleRequest(string playerId)
	{
		BattleRequest battleRequest = GetBattleRequest(playerId);
		if (battleRequest != null)
		{
			RemoveBattleRequest(playerId);
			if (BattleRequestDeclined != null)
			{
				BattleRequestDeclined(playerId);
			}
		}
	}

	public FriendData MyFriendData()
	{
		FriendData friendData = new FriendData();
		friendData.playerID = playerID;
		friendData.name = playerName;
		friendData.portraitSpriteName = portraitSpriteName;
		friendData.level = level;
		friendData.skillRating = skillRating;
		return friendData;
	}

	public FriendRequest MyFriendRequest()
	{
		FriendRequest friendRequest = new FriendRequest();
		friendRequest.playerId = playerID;
		friendRequest.playerName = playerName;
		friendRequest.portraitSpriteName = portraitSpriteName;
		friendRequest.level = level;
		friendRequest.skillRating = skillRating;
		return friendRequest;
	}
}
