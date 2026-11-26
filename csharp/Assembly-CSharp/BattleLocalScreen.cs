using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleLocalScreen : MonoBehaviour
{
	[Header("Searching")]
	public GameObject searchingObj;

	public TextMeshPro searchingTextMesh;

	[Header("Results")]
	public GameObject resultsObj;

	public GameObject directionsObj;

	public tk2dUIScrollableArea scrollableArea;

	public float displaySpacing = 53f;

	public float directionsHeight = 40f;

	private List<PlayerInfoDisplay> allPlayerInfo;

	public event Action<FriendData> BattleClicked;

	public void Init()
	{
		scrollableArea.gameObject.SetActive(value: true);
		if (allPlayerInfo == null)
		{
			allPlayerInfo = new List<PlayerInfoDisplay>();
		}
		RefreshPlayerList();
	}

	private void OnEnable()
	{
		LegionOfDoomController.instance.networkDiscoveryClient.DiscoveredPlayersUpdated += NetworkDiscoveryClient_DiscoveredPlayersUpdated;
		TT2NetBluetooth.Instance.DiscoveredPlayersUpdated += TT2NetBluetooth_DiscoveredPlayersUpdated;
		StartCoroutine(StartLocalSearch());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		if (LegionOfDoomController.instance != null && LegionOfDoomController.instance.networkDiscoveryClient != null)
		{
			LegionOfDoomController.instance.networkDiscoveryClient.DiscoveredPlayersUpdated -= NetworkDiscoveryClient_DiscoveredPlayersUpdated;
		}
		if (TT2NetBluetooth.Instance != null)
		{
			TT2NetBluetooth.Instance.DiscoveredPlayersUpdated -= TT2NetBluetooth_DiscoveredPlayersUpdated;
		}
		int count = allPlayerInfo.Count;
		for (int i = 0; i < count; i++)
		{
			PlayerInfoDisplay playerInfoDisplay = allPlayerInfo[i];
			playerInfoDisplay.BattleClicked -= HandleBattleBtnClicked;
			ObjectPool.PoolObject(playerInfoDisplay.gameObject);
		}
		allPlayerInfo.Clear();
	}

	private void NetworkDiscoveryClient_DiscoveredPlayersUpdated()
	{
		RefreshPlayerList();
	}

	private void TT2NetBluetooth_DiscoveredPlayersUpdated()
	{
		RefreshPlayerList();
	}

	private IEnumerator StartLocalSearch()
	{
		float periodTimer = 0.5f;
		searchingTextMesh.text = Language.Get("SEARCHINGNEARBY", "MENUS,GUI");
		while (true)
		{
			if (searchingObj.activeSelf)
			{
				periodTimer -= Time.deltaTime;
				if (periodTimer <= 0f)
				{
					periodTimer = 0.5f;
					searchingTextMesh.text += ".";
					if (searchingTextMesh.text.Contains("...."))
					{
						searchingTextMesh.text = Language.Get("SEARCHINGNEARBY", "MENUS,GUI");
					}
				}
			}
			yield return null;
		}
	}

	private void RefreshPlayerList()
	{
		while (allPlayerInfo.Count > 0)
		{
			PlayerInfoDisplay playerInfoDisplay = allPlayerInfo.Pop();
			playerInfoDisplay.BattleClicked -= HandleBattleBtnClicked;
			ObjectPool.PoolObject(playerInfoDisplay.gameObject);
		}
		List<FriendData> list = new List<FriendData>(LegionOfDoomController.instance.networkDiscoveryClient.PlayerList);
		for (int i = 0; i < TT2NetBluetooth.Instance.PlayerList.Count; i++)
		{
			if (!list.Contains(TT2NetBluetooth.Instance.PlayerList[i]))
			{
				list.Add(TT2NetBluetooth.Instance.PlayerList[i]);
			}
		}
		if (list.Count > 0)
		{
			resultsObj.SetActive(value: false);
			for (int j = 0; j < list.Count; j++)
			{
				FriendData friendData = list[j];
				PlayerInfoDisplay objectFromResource = ObjectPool.GetObjectFromResource<PlayerInfoDisplay>("Prefabs/LegionOfDoom/PlayerInfoDisplay");
				objectFromResource.transform.SetParent(scrollableArea.contentContainer.transform, worldPositionStays: true);
				objectFromResource.transform.localPosition = new Vector3(0f, (0f - displaySpacing) * (float)j, 0f);
				objectFromResource.Init(friendData, isBattleLocalResult: true);
				objectFromResource.BattleClicked += HandleBattleBtnClicked;
				allPlayerInfo.Add(objectFromResource);
			}
			directionsObj.transform.localPosition = new Vector3(0f, allPlayerInfo[allPlayerInfo.Count - 1].transform.localPosition.y - displaySpacing * 0.5f - directionsHeight * 0.5f - 5f, 0f);
			scrollableArea.ContentLength = (float)allPlayerInfo.Count * displaySpacing + directionsHeight + 5f;
		}
		else
		{
			resultsObj.SetActive(value: true);
			directionsObj.transform.localPosition = new Vector3(0f, -92f, 0f);
			scrollableArea.ContentLength = scrollableArea.VisibleAreaLength;
		}
	}

	private void HandleBattleBtnClicked(FriendData data)
	{
		if (BattleClicked != null)
		{
			BattleClicked(data);
		}
	}
}
