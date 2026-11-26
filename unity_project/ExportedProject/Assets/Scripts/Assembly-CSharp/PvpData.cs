using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
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

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PlayerNameUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PortraitSpriteUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PortraitUnlocked__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PortraitsViewedUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PreviousLevelUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PreviousXPUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FriendsListUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> FriendAdded__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> FriendRemoved__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FriendRequestsUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendRequest> FriendRequestAdded__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> FriendRequestRemoved__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action BattleRequestsUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BattleRequest> BattleRequestAdded__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> BattleRequestRemoved__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> FriendRequestAccepted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> FriendRequestDeclined__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BattleRequest> BattleRequestAccepted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> BattleRequestDeclined__BackingField;

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

	public bool AllPortraitsViewed
	{
		get
		{
			return viewedPortraits.Find((ViewedPortrait vp) => !vp.isViewed) == null;
		}
	}

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
				if (PlayerNameUpdated__BackingField != null)
				{
					PlayerNameUpdated__BackingField();
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
				if (PortraitSpriteUpdated__BackingField != null)
				{
					PortraitSpriteUpdated__BackingField();
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
				if (PreviousLevelUpdated__BackingField != null)
				{
					PreviousLevelUpdated__BackingField();
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
			UnityEngine.Debug.Log("Set Previous XP, value: " + value);
			if (value != previousXP)
			{
				previousXP = value;
				if (PreviousXPUpdated__BackingField != null)
				{
					PreviousXPUpdated__BackingField();
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

	public int PendingRequestCount
	{
		get
		{
			return BattleRequests.Count + friendRequests.FindAll((FriendRequest request) => request.status == FriendRequest.Status.Pending).Count;
		}
	}

	public event Action PlayerNameUpdated
	{
		add
		{
			Action action = PlayerNameUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerNameUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PlayerNameUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PlayerNameUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action PortraitSpriteUpdated
	{
		add
		{
			Action action = PortraitSpriteUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitSpriteUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PortraitSpriteUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitSpriteUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action PortraitUnlocked
	{
		add
		{
			Action action = PortraitUnlocked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitUnlocked__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PortraitUnlocked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitUnlocked__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action PortraitsViewedUpdated
	{
		add
		{
			Action action = PortraitsViewedUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitsViewedUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PortraitsViewedUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitsViewedUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action PreviousLevelUpdated
	{
		add
		{
			Action action = PreviousLevelUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PreviousLevelUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PreviousLevelUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PreviousLevelUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action PreviousXPUpdated
	{
		add
		{
			Action action = PreviousXPUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PreviousXPUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PreviousXPUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PreviousXPUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action FriendsListUpdated
	{
		add
		{
			Action action = FriendsListUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendsListUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FriendsListUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendsListUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> FriendAdded
	{
		add
		{
			Action<string> action = FriendAdded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendAdded__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = FriendAdded__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendAdded__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> FriendRemoved
	{
		add
		{
			Action<string> action = FriendRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRemoved__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = FriendRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRemoved__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action FriendRequestsUpdated
	{
		add
		{
			Action action = FriendRequestsUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestsUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FriendRequestsUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestsUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<FriendRequest> FriendRequestAdded
	{
		add
		{
			Action<FriendRequest> action = FriendRequestAdded__BackingField;
			Action<FriendRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestAdded__BackingField, (Action<FriendRequest>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendRequest> action = FriendRequestAdded__BackingField;
			Action<FriendRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestAdded__BackingField, (Action<FriendRequest>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> FriendRequestRemoved
	{
		add
		{
			Action<string> action = FriendRequestRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestRemoved__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = FriendRequestRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestRemoved__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action BattleRequestsUpdated
	{
		add
		{
			Action action = BattleRequestsUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestsUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = BattleRequestsUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestsUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<BattleRequest> BattleRequestAdded
	{
		add
		{
			Action<BattleRequest> action = BattleRequestAdded__BackingField;
			Action<BattleRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestAdded__BackingField, (Action<BattleRequest>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BattleRequest> action = BattleRequestAdded__BackingField;
			Action<BattleRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestAdded__BackingField, (Action<BattleRequest>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> BattleRequestRemoved
	{
		add
		{
			Action<string> action = BattleRequestRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestRemoved__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = BattleRequestRemoved__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestRemoved__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> FriendRequestAccepted
	{
		add
		{
			Action<string> action = FriendRequestAccepted__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestAccepted__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = FriendRequestAccepted__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestAccepted__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> FriendRequestDeclined
	{
		add
		{
			Action<string> action = FriendRequestDeclined__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestDeclined__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = FriendRequestDeclined__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestDeclined__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<BattleRequest> BattleRequestAccepted
	{
		add
		{
			Action<BattleRequest> action = BattleRequestAccepted__BackingField;
			Action<BattleRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestAccepted__BackingField, (Action<BattleRequest>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BattleRequest> action = BattleRequestAccepted__BackingField;
			Action<BattleRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestAccepted__BackingField, (Action<BattleRequest>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> BattleRequestDeclined
	{
		add
		{
			Action<string> action = BattleRequestDeclined__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestDeclined__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = BattleRequestDeclined__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestDeclined__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
			if (PortraitUnlocked__BackingField != null)
			{
				PortraitUnlocked__BackingField();
			}
		}
	}

	public void AddViewablePortrait(string portraitName)
	{
		if (viewedPortraits.Find((ViewedPortrait vp) => vp.portrait == portraitName) == null)
		{
			viewedPortraits.Add(new ViewedPortrait(portraitName));
		}
		if (PortraitsViewedUpdated__BackingField != null)
		{
			PortraitsViewedUpdated__BackingField();
		}
	}

	public void SetPortraitViewed(string portraitName, bool isViewed)
	{
		ViewedPortrait viewedPortrait = viewedPortraits.Find((ViewedPortrait vp) => vp.portrait == portraitName);
		if (viewedPortrait != null)
		{
			viewedPortrait.isViewed = isViewed;
			if (PortraitsViewedUpdated__BackingField != null)
			{
				PortraitsViewedUpdated__BackingField();
			}
		}
	}

	public bool IsPortraitViewed(string portraitName)
	{
		ViewedPortrait viewedPortrait = viewedPortraits.Find((ViewedPortrait vp) => vp.portrait == portraitName);
		return viewedPortrait != null && viewedPortrait.isViewed;
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
			if (FriendAdded__BackingField != null)
			{
				FriendAdded__BackingField(playerId);
			}
			if (FriendsListUpdated__BackingField != null)
			{
				FriendsListUpdated__BackingField();
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
			if (FriendAdded__BackingField != null)
			{
				FriendAdded__BackingField(data.playerID);
			}
			if (FriendsListUpdated__BackingField != null)
			{
				FriendsListUpdated__BackingField();
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
			if (FriendRemoved__BackingField != null)
			{
				FriendRemoved__BackingField(playerId);
			}
			if (FriendsListUpdated__BackingField != null)
			{
				FriendsListUpdated__BackingField();
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
			if (FriendRemoved__BackingField != null)
			{
				FriendRemoved__BackingField(text);
			}
		}
		if (FriendsListUpdated__BackingField != null)
		{
			FriendsListUpdated__BackingField();
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
			if (FriendRequestAdded__BackingField != null)
			{
				FriendRequestAdded__BackingField(friendRequest);
			}
			if (FriendRequestsUpdated__BackingField != null)
			{
				FriendRequestsUpdated__BackingField();
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
			if (FriendRequestRemoved__BackingField != null)
			{
				FriendRequestRemoved__BackingField(playerId);
			}
			if (FriendRequestsUpdated__BackingField != null)
			{
				FriendRequestsUpdated__BackingField();
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
			if (FriendRequestRemoved__BackingField != null)
			{
				FriendRequestRemoved__BackingField(playerId);
			}
		}
		if (FriendRequestsUpdated__BackingField != null)
		{
			FriendRequestsUpdated__BackingField();
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
			if (FriendRequestAccepted__BackingField != null)
			{
				FriendRequestAccepted__BackingField(playerId);
			}
		}
	}

	public void DeclineFriendRequest(string playerId)
	{
		FriendRequest friendRequest = GetFriendRequest(playerId);
		if (friendRequest != null)
		{
			friendRequest.status = FriendRequest.Status.Declined;
			if (FriendRequestDeclined__BackingField != null)
			{
				FriendRequestDeclined__BackingField(playerId);
			}
		}
	}

	public void AddBattleRequest(BattleRequest battleRequest)
	{
		if (!HasBattleRequest(battleRequest.playerId))
		{
			BattleRequests.Add(battleRequest.Copy());
			LODRequestFlag.ShowInvite(battleRequest);
			if (BattleRequestAdded__BackingField != null)
			{
				BattleRequestAdded__BackingField(battleRequest);
			}
			if (BattleRequestsUpdated__BackingField != null)
			{
				BattleRequestsUpdated__BackingField();
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
			if (BattleRequestRemoved__BackingField != null)
			{
				BattleRequestRemoved__BackingField(playerId);
			}
			if (BattleRequestsUpdated__BackingField != null)
			{
				BattleRequestsUpdated__BackingField();
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
			if (BattleRequestRemoved__BackingField != null)
			{
				BattleRequestRemoved__BackingField(playerId);
			}
		}
		if (BattleRequestsUpdated__BackingField != null)
		{
			BattleRequestsUpdated__BackingField();
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
			if (BattleRequestAccepted__BackingField != null)
			{
				BattleRequestAccepted__BackingField(battleRequest);
			}
		}
	}

	public void DeclineBattleRequest(string playerId)
	{
		BattleRequest battleRequest = GetBattleRequest(playerId);
		if (battleRequest != null)
		{
			RemoveBattleRequest(playerId);
			if (BattleRequestDeclined__BackingField != null)
			{
				BattleRequestDeclined__BackingField(playerId);
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
