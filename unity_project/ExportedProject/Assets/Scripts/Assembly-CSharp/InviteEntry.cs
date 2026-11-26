using System;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class InviteEntry : MonoBehaviour
{
	public enum InviteType
	{
		Battle = 0,
		Friend = 1
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BattleRequest, InviteEntry> BattleRequestAccepted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BattleRequest, InviteEntry> BattleRequestDeclined__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendRequest, InviteEntry> FriendRequestAccepted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendRequest, InviteEntry> FriendRequestDeclined__BackingField;

	public TextMeshPro inviteTypeTextMesh;

	public TextMeshPro playerNameTextMesh;

	public tk2dUIItem acceptBtn;

	public tk2dUIItem declineBtn;

	public tk2dSprite backingSprite;

	public Material battleFontMaterial;

	public Material friendFontMaterial;

	private BattleRequest battleRequest;

	private FriendRequest friendRequest;

	public InviteType inviteType { get; private set; }

	public event Action<BattleRequest, InviteEntry> BattleRequestAccepted
	{
		add
		{
			Action<BattleRequest, InviteEntry> action = BattleRequestAccepted__BackingField;
			Action<BattleRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestAccepted__BackingField, (Action<BattleRequest, InviteEntry>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BattleRequest, InviteEntry> action = BattleRequestAccepted__BackingField;
			Action<BattleRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestAccepted__BackingField, (Action<BattleRequest, InviteEntry>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<BattleRequest, InviteEntry> BattleRequestDeclined
	{
		add
		{
			Action<BattleRequest, InviteEntry> action = BattleRequestDeclined__BackingField;
			Action<BattleRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestDeclined__BackingField, (Action<BattleRequest, InviteEntry>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BattleRequest, InviteEntry> action = BattleRequestDeclined__BackingField;
			Action<BattleRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleRequestDeclined__BackingField, (Action<BattleRequest, InviteEntry>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<FriendRequest, InviteEntry> FriendRequestAccepted
	{
		add
		{
			Action<FriendRequest, InviteEntry> action = FriendRequestAccepted__BackingField;
			Action<FriendRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestAccepted__BackingField, (Action<FriendRequest, InviteEntry>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendRequest, InviteEntry> action = FriendRequestAccepted__BackingField;
			Action<FriendRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestAccepted__BackingField, (Action<FriendRequest, InviteEntry>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<FriendRequest, InviteEntry> FriendRequestDeclined
	{
		add
		{
			Action<FriendRequest, InviteEntry> action = FriendRequestDeclined__BackingField;
			Action<FriendRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestDeclined__BackingField, (Action<FriendRequest, InviteEntry>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendRequest, InviteEntry> action = FriendRequestDeclined__BackingField;
			Action<FriendRequest, InviteEntry> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendRequestDeclined__BackingField, (Action<FriendRequest, InviteEntry>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void OnEnable()
	{
		acceptBtn.OnClick += HandleAcceptClicked;
		declineBtn.OnClick += HandleDeclineClicked;
	}

	private void OnDisable()
	{
		acceptBtn.OnClick -= HandleAcceptClicked;
		declineBtn.OnClick -= HandleDeclineClicked;
	}

	public void Init(BattleRequest newBattleRequest)
	{
		inviteType = InviteType.Battle;
		battleRequest = newBattleRequest;
		inviteTypeTextMesh.text = Language.Get("BATTLEVS", "MENUS,GUI");
		inviteTypeTextMesh.fontMaterial = battleFontMaterial;
		if (newBattleRequest.isLocal)
		{
			FriendData friendData = LegionOfDoomController.instance.networkDiscoveryClient.GetFriendData(battleRequest.playerId);
			if (friendData == null)
			{
				friendData = TT2NetBluetooth.Instance.GetFriendData(battleRequest.playerId);
			}
			playerNameTextMesh.text = ((friendData == null) ? "N/A" : friendData.name);
			backingSprite.SetSprite("multiplayermenu_friendslist_backing_red");
		}
		else
		{
			playerNameTextMesh.text = Player.instance.pvpData.GetFriend(battleRequest.playerId).name;
			backingSprite.SetSprite("multiplayermenu_friendslist_backing_red");
		}
	}

	public void Init(FriendRequest newFriendRequest)
	{
		inviteType = InviteType.Friend;
		friendRequest = newFriendRequest;
		inviteTypeTextMesh.text = Language.Get("FRIENDREQUEST", "MENUS,GUI");
		inviteTypeTextMesh.fontMaterial = friendFontMaterial;
		playerNameTextMesh.text = friendRequest.playerName;
		backingSprite.SetSprite("multiplayermenu_friendslist_backing_purple");
	}

	private void HandleAcceptClicked()
	{
		if (inviteType == InviteType.Battle)
		{
			if (BattleRequestAccepted__BackingField != null)
			{
				BattleRequestAccepted__BackingField(battleRequest, this);
			}
		}
		else if (FriendRequestAccepted__BackingField != null)
		{
			FriendRequestAccepted__BackingField(friendRequest, this);
		}
	}

	private void HandleDeclineClicked()
	{
		if (inviteType == InviteType.Battle)
		{
			if (BattleRequestDeclined__BackingField != null)
			{
				BattleRequestDeclined__BackingField(battleRequest, this);
			}
		}
		else if (FriendRequestDeclined__BackingField != null)
		{
			FriendRequestDeclined__BackingField(friendRequest, this);
		}
	}
}
