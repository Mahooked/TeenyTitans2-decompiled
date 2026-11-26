using System;

[Serializable]
public class FriendData
{
	public string playerID;

	public string name;

	public string portraitSpriteName;

	public int level;

	public int skillRating;

	public bool requestAccepted;

	public bool IsOnline { get; private set; }

	public bool IsInBattle { get; private set; }

	public event Action<FriendData> Updated;

	public event Action StatusUpdated;

	public event Action<FriendData> StatusUpdatedFriendData;

	public event Action OnlineStatusUpdated;

	public event Action<FriendData> OnlineStatusUpdatedFriendData;

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
			if (StatusUpdated != null)
			{
				StatusUpdated();
			}
			if (StatusUpdatedFriendData != null)
			{
				StatusUpdatedFriendData(this);
			}
			if (OnlineStatusUpdated != null)
			{
				OnlineStatusUpdated();
			}
			if (OnlineStatusUpdatedFriendData != null)
			{
				OnlineStatusUpdatedFriendData(this);
			}
		}
	}

	public void SetInBattleStatus(bool inBattle)
	{
		if (inBattle != IsInBattle)
		{
			IsInBattle = inBattle;
			if (StatusUpdated != null)
			{
				StatusUpdated();
			}
			if (StatusUpdatedFriendData != null)
			{
				StatusUpdatedFriendData(this);
			}
		}
	}

	public void ForceUpdatedEvent()
	{
		if (Updated != null)
		{
			Updated(this);
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
