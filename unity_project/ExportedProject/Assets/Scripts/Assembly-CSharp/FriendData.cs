using System;
using System.Diagnostics;
using System.Threading;

[Serializable]
public class FriendData
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendData> Updated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action StatusUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendData> StatusUpdatedFriendData__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnlineStatusUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendData> OnlineStatusUpdatedFriendData__BackingField;

	public string playerID;

	public string name;

	public string portraitSpriteName;

	public int level;

	public int skillRating;

	public bool requestAccepted;

	public bool IsOnline { get; private set; }

	public bool IsInBattle { get; private set; }

	public event Action<FriendData> Updated
	{
		add
		{
			Action<FriendData> action = Updated__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Updated__BackingField, (Action<FriendData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendData> action = Updated__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Updated__BackingField, (Action<FriendData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action StatusUpdated
	{
		add
		{
			Action action = StatusUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = StatusUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<FriendData> StatusUpdatedFriendData
	{
		add
		{
			Action<FriendData> action = StatusUpdatedFriendData__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusUpdatedFriendData__BackingField, (Action<FriendData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendData> action = StatusUpdatedFriendData__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusUpdatedFriendData__BackingField, (Action<FriendData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnlineStatusUpdated
	{
		add
		{
			Action action = OnlineStatusUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnlineStatusUpdated__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnlineStatusUpdated__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnlineStatusUpdated__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<FriendData> OnlineStatusUpdatedFriendData
	{
		add
		{
			Action<FriendData> action = OnlineStatusUpdatedFriendData__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnlineStatusUpdatedFriendData__BackingField, (Action<FriendData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendData> action = OnlineStatusUpdatedFriendData__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnlineStatusUpdatedFriendData__BackingField, (Action<FriendData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public FriendData()
	{
		level = 1;
		skillRating = 2500;
	}

	public FriendData Copy()
	{
		FriendData friendData = new FriendData();
		friendData.playerID = playerID;
		friendData.name = name;
		friendData.portraitSpriteName = portraitSpriteName;
		friendData.level = level;
		friendData.skillRating = skillRating;
		friendData.requestAccepted = requestAccepted;
		return friendData;
	}

	public void SetOnlineStatus(bool isOnline)
	{
		if (isOnline != IsOnline)
		{
			IsOnline = isOnline;
			if (StatusUpdated__BackingField != null)
			{
				StatusUpdated__BackingField();
			}
			if (StatusUpdatedFriendData__BackingField != null)
			{
				StatusUpdatedFriendData__BackingField(this);
			}
			if (OnlineStatusUpdated__BackingField != null)
			{
				OnlineStatusUpdated__BackingField();
			}
			if (OnlineStatusUpdatedFriendData__BackingField != null)
			{
				OnlineStatusUpdatedFriendData__BackingField(this);
			}
		}
	}

	public void SetInBattleStatus(bool inBattle)
	{
		if (inBattle != IsInBattle)
		{
			IsInBattle = inBattle;
			if (StatusUpdated__BackingField != null)
			{
				StatusUpdated__BackingField();
			}
			if (StatusUpdatedFriendData__BackingField != null)
			{
				StatusUpdatedFriendData__BackingField(this);
			}
		}
	}

	public void ForceUpdatedEvent()
	{
		if (Updated__BackingField != null)
		{
			Updated__BackingField(this);
		}
	}

	public override bool Equals(object obj)
	{
		if (obj == null || !(obj is FriendData))
		{
			return false;
		}
		if (object.ReferenceEquals(this, obj))
		{
			return true;
		}
		FriendData friendData = (FriendData)obj;
		return playerID == friendData.playerID;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode() ^ playerID.GetHashCode();
	}
}
