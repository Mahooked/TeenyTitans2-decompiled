using System;

[Serializable]
public class FriendRequest
{
	public enum Status
	{
		Pending,
		Accepted,
		Declined
	}

	public string playerId;

	public string playerName;

	public string portraitSpriteName;

	public int level;

	public int skillRating;

	public Status status;

	public event Action<FriendRequest> Updated;

	public void ForceUpdatedEvent()
	{
		if (Updated != null)
		{
			Updated(this);
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
