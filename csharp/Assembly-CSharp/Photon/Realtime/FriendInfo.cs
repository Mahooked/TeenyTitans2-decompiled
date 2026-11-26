using System;

namespace Photon.Realtime;

public class FriendInfo
{
	[Obsolete("Use UserId.")]
	public string Name => UserId;

	public string UserId { get; protected internal set; }

	public bool IsOnline { get; protected internal set; }

	public string Room { get; protected internal set; }

	public bool IsInRoom => IsOnline && !string.IsNullOrEmpty(Room);

	public override string ToString()
	{
		string userId = UserId;
		string arg;
		if (IsOnline)
		{
			arg = ((!IsInRoom) ? "on master" : "playing");
		}
		else
		{
			arg = "offline";
		}
		return $"{userId}\t is: {arg}";
	}
}
