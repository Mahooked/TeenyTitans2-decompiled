using System;
using System.Diagnostics;
using System.Threading;

[Serializable]
public class FriendRequest
{
	public enum Status
	{
		Pending = 0,
		Accepted = 1,
		Declined = 2
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendRequest> Updated__BackingField;

	public string playerId;

	public string playerName;

	public string portraitSpriteName;

	public int level;

	public int skillRating;

	public Status status;

	public event Action<FriendRequest> Updated
	{
		add
		{
			Action<FriendRequest> action = Updated__BackingField;
			Action<FriendRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Updated__BackingField, (Action<FriendRequest>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendRequest> action = Updated__BackingField;
			Action<FriendRequest> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Updated__BackingField, (Action<FriendRequest>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void ForceUpdatedEvent()
	{
		if (Updated__BackingField != null)
		{
			Updated__BackingField(this);
		}
	}

	public FriendRequest Copy()
	{
		FriendRequest friendRequest = new FriendRequest();
		friendRequest.playerId = playerId;
		friendRequest.playerName = playerName;
		friendRequest.portraitSpriteName = portraitSpriteName;
		friendRequest.level = level;
		friendRequest.skillRating = skillRating;
		friendRequest.status = status;
		return friendRequest;
	}
}
