using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRewardManager : MonoBehaviour
{
	private static PlayerRewardManager _instance;

	public bool isShowingRewards { get; private set; }

	public static PlayerRewardManager instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Object.Destroy(this);
			return;
		}
		_instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		EventMessenger.Instance.AddListener<RevealPurchasedRewardsEvent>(HandleRevealPurchasedRewardsEvent);
	}

	private void OnDestroy()
	{
		EventMessenger.Instance.RemoveListener<RevealPurchasedRewardsEvent>(HandleRevealPurchasedRewardsEvent);
	}

	private void HandleRevealPurchasedRewardsEvent(RevealPurchasedRewardsEvent evt)
	{
		if (Player.instance.purchasedRewardsQueue.Count > 0)
		{
			StartCoroutine(ProcessPurchasedRewardsQueue());
		}
	}

	private IEnumerator ProcessPurchasedRewardsQueue()
	{
		Debug.Log("[PlayerRewardManager:ProcessPurchasedRewardsQueue] - Start showing purchased rewards from the player's queue");
		isShowingRewards = true;
		Player.instance.isSavingEnabled = false;
		List<PlayerReward> rewards = new List<PlayerReward>(Player.instance.purchasedRewardsQueue);
		List<PlayerReward> greenRiddleEggs = new List<PlayerReward>();
		List<PlayerReward> goldRiddleEggs = new List<PlayerReward>();
		List<PlayerReward> otherRewards = new List<PlayerReward>();
		for (int i = 0; i < rewards.Count; i++)
		{
			if (rewards[i].type == PlayerRewardType.RiddleEgg)
			{
				if (rewards[i].riddleEggType == RiddleEggType.Green)
				{
					greenRiddleEggs.Add(rewards[i]);
				}
				else
				{
					goldRiddleEggs.Add(rewards[i]);
				}
			}
			else
			{
				otherRewards.Add(rewards[i]);
			}
		}
		if (greenRiddleEggs.Count > 0)
		{
			RiddleEggReveal.Show(greenRiddleEggs);
			yield return new WaitWhile(() => RiddleEggReveal.Instance != null);
		}
		if (goldRiddleEggs.Count > 0)
		{
			RiddleEggReveal.Show(goldRiddleEggs);
			yield return new WaitWhile(() => RiddleEggReveal.Instance != null);
		}
		if (otherRewards.Count > 0)
		{
			CityRewardReveal.Show(otherRewards);
			yield return new WaitWhile(() => CityRewardReveal.Instance != null);
		}
		Player.instance.purchasedRewardsQueue.Clear();
		Player.instance.isSavingEnabled = true;
		Player.instance.Save();
		isShowingRewards = false;
	}
}
