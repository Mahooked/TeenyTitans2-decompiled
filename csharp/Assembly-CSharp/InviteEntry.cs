using System;
using TMPro;
using UnityEngine;

public class InviteEntry : MonoBehaviour
{
	public enum InviteType
	{
		Battle,
		Friend
	}

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

	public event Action<BattleRequest, InviteEntry> BattleRequestAccepted;

	public event Action<BattleRequest, InviteEntry> BattleRequestDeclined;

	public event Action<FriendRequest, InviteEntry> FriendRequestAccepted;

	public event Action<FriendRequest, InviteEntry> FriendRequestDeclined;

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
			if (BattleRequestAccepted != null)
			{
				BattleRequestAccepted(battleRequest, this);
			}
		}
		else if (FriendRequestAccepted != null)
		{
			FriendRequestAccepted(friendRequest, this);
		}
	}

	private void HandleDeclineClicked()
	{
		if (inviteType == InviteType.Battle)
		{
			if (BattleRequestDeclined != null)
			{
				BattleRequestDeclined(battleRequest, this);
			}
		}
		else if (FriendRequestDeclined != null)
		{
			FriendRequestDeclined(friendRequest, this);
		}
	}
}
