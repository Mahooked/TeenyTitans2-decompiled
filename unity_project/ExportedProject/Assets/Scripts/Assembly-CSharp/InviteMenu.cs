using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InviteMenu : MonoBehaviour
{
	public GameObject noInvitesObj;

	public InviteEntry inviteEntryPrototype;

	public tk2dUIScrollableArea scrollableArea;

	public float inviteSpacing;

	public float repositionTime;

	private List<InviteEntry> inviteEntries;

	private Coroutine repositionRoutine;

	private bool _deleteSequenceActive;

	public bool deleteSequenceActive
	{
		get
		{
			return _deleteSequenceActive;
		}
	}

	private void OnEnable()
	{
		Player.instance.pvpData.BattleRequestsUpdated += HandleRequestsUpdated;
		Player.instance.pvpData.FriendRequestsUpdated += HandleRequestsUpdated;
	}

	private void OnDisable()
	{
		if (repositionRoutine != null)
		{
			StopCoroutine(repositionRoutine);
		}
		if (inviteEntries != null)
		{
			int count = inviteEntries.Count;
			for (int i = 0; i < count; i++)
			{
				InviteEntry inviteEntry = inviteEntries[i];
				inviteEntry.BattleRequestAccepted -= HandleBattleRequestAccepted;
				inviteEntry.BattleRequestDeclined -= HandleBattleRequestDeclined;
				inviteEntry.FriendRequestAccepted -= HandleFriendRequestAccepted;
				inviteEntry.FriendRequestDeclined -= HandleFriendRequestDeclined;
				ObjectPool.PoolObject(inviteEntry.gameObject);
			}
			inviteEntries.Clear();
		}
		if (Player.instance != null)
		{
			Player.instance.pvpData.BattleRequestsUpdated -= HandleRequestsUpdated;
			Player.instance.pvpData.FriendRequestsUpdated -= HandleRequestsUpdated;
		}
	}

	public void Init(bool animate = false)
	{
		List<BattleRequest> battleRequests = Player.instance.pvpData.BattleRequests;
		int num = 0;
		if (inviteEntries == null)
		{
			inviteEntries = new List<InviteEntry>();
		}
		int count = battleRequests.Count;
		for (int i = 0; i < count; i++)
		{
			BattleRequest newBattleRequest = battleRequests[i];
			if (num >= inviteEntries.Count)
			{
				InviteEntry component = ObjectPool.GetObject(inviteEntryPrototype.gameObject).GetComponent<InviteEntry>();
				component.gameObject.name = "BattleRequest" + i;
				component.transform.SetParent(inviteEntryPrototype.transform.parent);
				component.transform.localPosition = inviteEntryPrototype.transform.localPosition + new Vector3(0f, inviteSpacing * (float)num);
				component.transform.localScale = Vector3.one;
				component.Init(newBattleRequest);
				component.BattleRequestAccepted += HandleBattleRequestAccepted;
				component.BattleRequestDeclined += HandleBattleRequestDeclined;
				inviteEntries.Add(component);
			}
			else
			{
				InviteEntry inviteEntry = inviteEntries[num];
				inviteEntry.transform.SetParent(inviteEntryPrototype.transform.parent);
				inviteEntry.transform.localPosition = inviteEntryPrototype.transform.localPosition + new Vector3(0f, inviteSpacing * (float)num);
				inviteEntry.transform.localScale = Vector3.one;
				if (inviteEntry.inviteType == InviteEntry.InviteType.Friend)
				{
					inviteEntry.FriendRequestAccepted -= HandleFriendRequestAccepted;
					inviteEntry.FriendRequestDeclined -= HandleFriendRequestDeclined;
					inviteEntry.BattleRequestAccepted += HandleBattleRequestAccepted;
					inviteEntry.BattleRequestDeclined += HandleBattleRequestDeclined;
				}
				inviteEntry.Init(newBattleRequest);
			}
			num++;
		}
		List<FriendRequest> list = Player.instance.pvpData.friendRequests.FindAll((FriendRequest x) => x.status == FriendRequest.Status.Pending);
		count = list.Count;
		for (int num2 = 0; num2 < count; num2++)
		{
			FriendRequest newFriendRequest = list[num2];
			if (num >= inviteEntries.Count)
			{
				InviteEntry component2 = ObjectPool.GetObject(inviteEntryPrototype.gameObject).GetComponent<InviteEntry>();
				component2.gameObject.name = "FriendRequest" + num2;
				component2.transform.SetParent(inviteEntryPrototype.transform.parent);
				component2.transform.localPosition = inviteEntryPrototype.transform.localPosition + new Vector3(0f, inviteSpacing * (float)num);
				component2.transform.localScale = Vector3.one;
				component2.Init(newFriendRequest);
				component2.FriendRequestAccepted += HandleFriendRequestAccepted;
				component2.FriendRequestDeclined += HandleFriendRequestDeclined;
				inviteEntries.Add(component2);
			}
			else
			{
				InviteEntry inviteEntry2 = inviteEntries[num];
				inviteEntry2.transform.SetParent(inviteEntryPrototype.transform.parent);
				inviteEntry2.transform.localPosition = inviteEntryPrototype.transform.localPosition + new Vector3(0f, inviteSpacing * (float)num);
				inviteEntry2.transform.localScale = Vector3.one;
				if (inviteEntry2.inviteType == InviteEntry.InviteType.Battle)
				{
					inviteEntry2.FriendRequestAccepted += HandleFriendRequestAccepted;
					inviteEntry2.FriendRequestDeclined += HandleFriendRequestDeclined;
					inviteEntry2.BattleRequestAccepted -= HandleBattleRequestAccepted;
					inviteEntry2.BattleRequestDeclined -= HandleBattleRequestDeclined;
				}
				inviteEntry2.Init(newFriendRequest);
			}
			num++;
		}
		scrollableArea.ContentLength = (float)inviteEntries.Count * Mathf.Abs(inviteSpacing);
		noInvitesObj.gameObject.SetActive(inviteEntries.Count == 0);
		inviteEntryPrototype.gameObject.SetActive(false);
	}

	private void HandleRequestsUpdated()
	{
		if (!_deleteSequenceActive)
		{
			Init();
		}
	}

	private void HandleBattleRequestAccepted(BattleRequest battleRequest, InviteEntry inviteEntry)
	{
		if (!_deleteSequenceActive)
		{
			inviteEntries.Remove(inviteEntry);
			inviteEntry.BattleRequestAccepted -= HandleBattleRequestAccepted;
			inviteEntry.BattleRequestDeclined -= HandleBattleRequestDeclined;
			ObjectPool.PoolObject(inviteEntry.gameObject);
			repositionRoutine = StartCoroutine(RepositionEntriesSequence());
			Player.instance.pvpData.AcceptBattleRequest(battleRequest.playerId);
		}
	}

	private void HandleBattleRequestDeclined(BattleRequest battleRequest, InviteEntry inviteEntry)
	{
		if (!_deleteSequenceActive)
		{
			inviteEntries.Remove(inviteEntry);
			inviteEntry.BattleRequestAccepted -= HandleBattleRequestAccepted;
			inviteEntry.BattleRequestDeclined -= HandleBattleRequestDeclined;
			ObjectPool.PoolObject(inviteEntry.gameObject);
			repositionRoutine = StartCoroutine(RepositionEntriesSequence());
			Player.instance.pvpData.DeclineBattleRequest(battleRequest.playerId);
		}
	}

	private void HandleFriendRequestAccepted(FriendRequest request, InviteEntry inviteEntry)
	{
		if (!_deleteSequenceActive)
		{
			inviteEntries.Remove(inviteEntry);
			inviteEntry.FriendRequestAccepted -= HandleFriendRequestAccepted;
			inviteEntry.FriendRequestDeclined -= HandleFriendRequestDeclined;
			ObjectPool.PoolObject(inviteEntry.gameObject);
			repositionRoutine = StartCoroutine(RepositionEntriesSequence());
			Player.instance.pvpData.AcceptFriendRequest(request.playerId);
		}
	}

	private void HandleFriendRequestDeclined(FriendRequest request, InviteEntry inviteEntry)
	{
		if (!_deleteSequenceActive)
		{
			inviteEntries.Remove(inviteEntry);
			inviteEntry.FriendRequestAccepted -= HandleFriendRequestAccepted;
			inviteEntry.FriendRequestDeclined -= HandleFriendRequestDeclined;
			ObjectPool.PoolObject(inviteEntry.gameObject);
			repositionRoutine = StartCoroutine(RepositionEntriesSequence());
			Player.instance.pvpData.DeclineFriendRequest(request.playerId);
		}
	}

	private IEnumerator RepositionEntriesSequence()
	{
		_deleteSequenceActive = true;
		yield return new WaitForSeconds(0.2f);
		int len = inviteEntries.Count;
		for (int i = 0; i < len; i++)
		{
			InviteEntry inviteEntry = inviteEntries[i];
			inviteEntry.transform.DOLocalMove(inviteEntryPrototype.transform.localPosition + new Vector3(0f, inviteSpacing * (float)i), repositionTime).SetEase(Ease.OutBack);
		}
		yield return new WaitForSeconds(repositionTime);
		scrollableArea.enabled = true;
		_deleteSequenceActive = false;
		Init();
	}
}
