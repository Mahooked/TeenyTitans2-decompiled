using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class FriendsListMenu : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<FriendData> BattleClicked__BackingField;

	public GameObject noFriendsTextObj;

	public GameObject swipeLeftTextMesh;

	public tk2dUIScrollableArea scrollableArea;

	public float displaySpacing = 60f;

	public float repositionTime = 1f;

	[Header("No Internet")]
	public GameObject noInternetObj;

	[Header("Debug")]
	public tk2dUIItem debugAddFriend;

	private List<PlayerInfoDisplay> allPlayerInfo;

	private Coroutine repositionRoutine;

	private bool _deleteSequenceActive;

	public bool deleteSequenceActive
	{
		get
		{
			return _deleteSequenceActive;
		}
	}

	public event Action<FriendData> BattleClicked
	{
		add
		{
			Action<FriendData> action = BattleClicked__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleClicked__BackingField, (Action<FriendData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<FriendData> action = BattleClicked__BackingField;
			Action<FriendData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BattleClicked__BackingField, (Action<FriendData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
		_deleteSequenceActive = false;
		Player.instance.pvpData.FriendsListUpdated += PvpData_FriendsListUpdated;
		PvpData_FriendsListUpdated();
		debugAddFriend.OnClick += HandleDebugAddFriend;
	}

	private void OnDisable()
	{
		if (Player.instance != null)
		{
			Player.instance.pvpData.FriendsListUpdated -= PvpData_FriendsListUpdated;
		}
		if (repositionRoutine != null)
		{
			StopCoroutine(repositionRoutine);
		}
		int count = allPlayerInfo.Count;
		for (int i = 0; i < count; i++)
		{
			PlayerInfoDisplay playerInfoDisplay = allPlayerInfo[i];
			playerInfoDisplay.BattleClicked -= HandleFriendBattleBtnClicked;
			playerInfoDisplay.DeleteClicked -= HandleFriendDeleteBtnClicked;
			playerInfoDisplay.DeleteExitFinished -= HandleFriendDeleteExitFinished;
			playerInfoDisplay.DraggingStarted -= HandleDraggingStarted;
			playerInfoDisplay.DraggingEnded -= HandleDraggingEnded;
			playerInfoDisplay.OnlineStatusChanged -= HandleOnlineStatusChanged;
			playerInfoDisplay.DOKill();
			UnityEngine.Debug.Log("Pool Player Info Display for " + playerInfoDisplay.nameTextMesh.text);
			ObjectPool.PoolObject(playerInfoDisplay.gameObject);
		}
		allPlayerInfo.Clear();
		scrollableArea.gameObject.SetActive(true);
		debugAddFriend.gameObject.SetActive(true);
		noFriendsTextObj.SetActive(true);
		noInternetObj.SetActive(false);
		if (debugAddFriend != null)
		{
			debugAddFriend.OnClick -= HandleDebugAddFriend;
		}
	}

	public void Init(List<FriendData> friendData)
	{
		UnityEngine.Debug.Log("Init Friends List...");
		if (allPlayerInfo == null)
		{
			allPlayerInfo = new List<PlayerInfoDisplay>();
		}
		int count = friendData.Count;
		if (count <= 0)
		{
			for (int i = 0; i < allPlayerInfo.Count; i++)
			{
				PlayerInfoDisplay playerInfoDisplay = allPlayerInfo[i];
				playerInfoDisplay.BattleClicked -= HandleFriendBattleBtnClicked;
				playerInfoDisplay.DeleteClicked -= HandleFriendDeleteBtnClicked;
				playerInfoDisplay.DeleteExitFinished -= HandleFriendDeleteExitFinished;
				playerInfoDisplay.DraggingStarted -= HandleDraggingStarted;
				playerInfoDisplay.DraggingEnded -= HandleDraggingEnded;
				playerInfoDisplay.OnlineStatusChanged -= HandleOnlineStatusChanged;
				playerInfoDisplay.DOKill();
				ObjectPool.PoolObject(playerInfoDisplay.gameObject);
			}
			allPlayerInfo.Clear();
			noFriendsTextObj.SetActive(true);
			swipeLeftTextMesh.SetActive(false);
			return;
		}
		List<FriendData> list = new List<FriendData>(friendData);
		list.Sort(FriendsListSort);
		noFriendsTextObj.SetActive(false);
		UnityEngine.Debug.Log("\t Print Friends List: ");
		for (int j = 0; j < count; j++)
		{
			FriendData friendData2 = list[j];
			UnityEngine.Debug.Log("\t\t " + j + ": " + friendData2.name);
			if (j >= allPlayerInfo.Count)
			{
				PlayerInfoDisplay objectFromResource = ObjectPool.GetObjectFromResource<PlayerInfoDisplay>("Prefabs/LegionOfDoom/PlayerInfoDisplay");
				objectFromResource.transform.parent = scrollableArea.contentContainer.transform;
				objectFromResource.transform.localPosition = new Vector3(0f, (0f - displaySpacing) * (float)j, -10f);
				objectFromResource.Init(friendData2);
				objectFromResource.BattleClicked += HandleFriendBattleBtnClicked;
				objectFromResource.DeleteClicked += HandleFriendDeleteBtnClicked;
				objectFromResource.DeleteExitFinished += HandleFriendDeleteExitFinished;
				objectFromResource.DraggingStarted += HandleDraggingStarted;
				objectFromResource.DraggingEnded += HandleDraggingEnded;
				objectFromResource.OnlineStatusChanged += HandleOnlineStatusChanged;
				allPlayerInfo.Add(objectFromResource);
			}
			else
			{
				UnityEngine.Debug.Log("\t\t\t Init this friend with an existing display");
				PlayerInfoDisplay playerInfoDisplay2 = allPlayerInfo[j];
				playerInfoDisplay2.transform.parent = scrollableArea.contentContainer.transform;
				playerInfoDisplay2.transform.localPosition = new Vector3(0f, (0f - displaySpacing) * (float)j, -10f);
				playerInfoDisplay2.Init(friendData2);
			}
		}
		scrollableArea.ContentLength = (float)allPlayerInfo.Count * displaySpacing;
		swipeLeftTextMesh.SetActive(true);
		swipeLeftTextMesh.transform.localPosition = allPlayerInfo[allPlayerInfo.Count - 1].transform.localPosition + new Vector3(0f, -36f);
	}

	public void InitForNoInternet()
	{
		scrollableArea.gameObject.SetActive(false);
		noFriendsTextObj.SetActive(false);
		debugAddFriend.gameObject.SetActive(false);
		noInternetObj.SetActive(true);
	}

	private void PvpData_FriendsListUpdated()
	{
		if (!_deleteSequenceActive)
		{
			Init(Player.instance.pvpData.friends);
		}
	}

	private void HandleFriendBattleBtnClicked(FriendData friendData)
	{
		if (BattleClicked__BackingField != null)
		{
			BattleClicked__BackingField(friendData);
		}
	}

	private void HandleFriendDeleteBtnClicked(FriendData friendData)
	{
		UnityEngine.Debug.Log("Delete Button clicked for " + friendData.name + ", Time: " + Time.time);
		scrollableArea.enabled = false;
		int count = allPlayerInfo.Count;
		for (int i = 0; i < count; i++)
		{
			PlayerInfoDisplay playerInfoDisplay = allPlayerInfo[i];
			playerInfoDisplay.battleBtn.enabled = false;
		}
		_deleteSequenceActive = true;
		Player.instance.pvpData.RemoveFriend(friendData.playerID);
	}

	private void HandleFriendDeleteExitFinished(FriendData friendData, PlayerInfoDisplay display)
	{
		UnityEngine.Debug.Log("Delete Exit Finished for " + display.nameTextMesh.text + ", Time: " + Time.time);
		allPlayerInfo.Remove(display);
		display.BattleClicked -= HandleFriendBattleBtnClicked;
		display.DeleteClicked -= HandleFriendDeleteBtnClicked;
		display.DeleteExitFinished -= HandleFriendDeleteExitFinished;
		ObjectPool.PoolObject(display.gameObject);
		repositionRoutine = StartCoroutine(RepositionEntriesSequence(friendData));
	}

	private IEnumerator RepositionEntriesSequence(FriendData friendData)
	{
		int len = allPlayerInfo.Count;
		for (int i = 0; i < len; i++)
		{
			PlayerInfoDisplay playerInfoDisplay = allPlayerInfo[i];
			playerInfoDisplay.transform.DOLocalMove(new Vector3(0f, (0f - displaySpacing) * (float)i, -10f), repositionTime).SetEase(Ease.OutBack);
		}
		if (allPlayerInfo.Count > 0)
		{
			swipeLeftTextMesh.transform.DOLocalMove(new Vector3(0f, (0f - displaySpacing) * (float)(allPlayerInfo.Count - 1), -10f) + new Vector3(0f, -36f), repositionTime).SetEase(Ease.OutBack);
		}
		else
		{
			swipeLeftTextMesh.SetActive(false);
		}
		yield return new WaitForSeconds(repositionTime);
		scrollableArea.enabled = true;
		for (int j = 0; j < len; j++)
		{
			PlayerInfoDisplay playerInfoDisplay2 = allPlayerInfo[j];
			playerInfoDisplay2.battleBtn.enabled = true;
		}
		_deleteSequenceActive = false;
		Init(Player.instance.pvpData.friends);
	}

	private void HandleDraggingStarted()
	{
		scrollableArea.enabled = false;
	}

	private void HandleDraggingEnded()
	{
		bool flag = true;
		int count = allPlayerInfo.Count;
		for (int i = 0; i < count; i++)
		{
			if (allPlayerInfo[i].activelyDragging)
			{
				flag = false;
				break;
			}
		}
		scrollableArea.enabled = flag;
	}

	private void HandleOnlineStatusChanged()
	{
		if (!_deleteSequenceActive)
		{
			Init(Player.instance.pvpData.friends);
		}
	}

	private void HandleDebugAddFriend()
	{
		FriendData friendData = new FriendData();
		friendData.playerID = FriendCodeGenerator.Generate();
		FriendData friendData2 = friendData;
		if (UnityEngine.Random.value <= 0.5f)
		{
			friendData2.name = GameManager.instance.legionOfDoomSettings.GenerateRandomPlayerName();
			friendData2.level = UnityEngine.Random.Range(1, GameManager.instance.legionOfDoomLevelTable.MaxLevel + 1);
			friendData2.portraitSpriteName = GameManager.instance.legionOfDoomSettings.GetRandomPortraitSprite();
			friendData2.skillRating = UnityEngine.Random.Range(1000, 4001);
			friendData2.requestAccepted = UnityEngine.Random.value <= 0.5f;
		}
		Player.instance.pvpData.AddFriend(friendData2);
	}

	private int FriendsListSort(FriendData friend1, FriendData friend2)
	{
		if (friend1.requestAccepted == friend2.requestAccepted)
		{
			if (friend1.requestAccepted)
			{
				if (friend1.IsOnline == friend2.IsOnline)
				{
					return friend1.name.CompareTo(friend2.name);
				}
				return (!friend1.IsOnline) ? 1 : (-1);
			}
			if (!string.IsNullOrEmpty(friend1.name) && !string.IsNullOrEmpty(friend2.name))
			{
				return friend1.name.CompareTo(friend2.name);
			}
			return string.IsNullOrEmpty(friend1.name) ? 1 : (-1);
		}
		return (!friend1.requestAccepted) ? 1 : (-1);
	}
}
