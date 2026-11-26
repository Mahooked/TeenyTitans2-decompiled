using System;
using UnityEngine;

public class RiddleEggRevealQMark : MonoBehaviour
{
	public Animator animator;

	public tk2dUIItem uiItem;

	public BoxCollider boxCollider;

	public Transform rewardContainer;

	private RiddleEggRevealReward rewardObject;

	private PlayerReward _reward;

	public PlayerReward reward
	{
		get
		{
			return _reward;
		}
		set
		{
			Init(value);
		}
	}

	public bool isRevealed { get; private set; }

	public event Action Revealed;

	public void Init(PlayerReward reward)
	{
		_reward = reward;
	}

	public void RevealReward()
	{
		if (_reward != null && !isRevealed)
		{
			CreateRewardObject();
			animator.Play("open");
			animator.Update(0f);
			isRevealed = true;
			if (Revealed != null)
			{
				Revealed();
			}
		}
	}

	public void ResetReward()
	{
		_reward = null;
		if (rewardObject != null)
		{
			ObjectPool.PoolObject(rewardObject.gameObject);
			rewardObject = null;
		}
		isRevealed = false;
		gameObject.SetActive(value: false);
	}

	private void CreateRewardObject()
	{
		if (_reward == null)
		{
			return;
		}
		string text = string.Empty;
		switch (_reward.type)
		{
		case PlayerRewardType.Currency:
			if (_reward.currencyType != CurrencyType.Coins)
			{
				text = ((_reward.amount >= 5) ? "egg_reward_rainbowcoin_Large" : "egg_reward_rainbowcoin_small");
			}
			else if (_reward.amount >= 1000)
			{
				if (reward.amount >= 2000)
				{
					text = ((reward.amount >= 3000) ? "egg_reward_coin_XL04" : "egg_reward_coin_Large03");
				}
				else
				{
					text = "egg_reward_coin_medium02";
				}
			}
			else
			{
				text = "egg_reward_coin_small01";
			}
			break;
		case PlayerRewardType.Figure:
			text = "egg_reward_figure";
			break;
		case PlayerRewardType.Repaint:
			text = "egg_reward_Repaint";
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			rewardObject = ObjectPool.GetObjectFromResource<RiddleEggRevealReward>("Prefabs/GUI/" + text);
			rewardObject.Init(reward);
			rewardObject.transform.SetParent(rewardContainer, worldPositionStays: true);
			rewardObject.transform.localPosition = Vector3.zero;
			rewardObject.transform.localRotation = Quaternion.identity;
			rewardObject.transform.localScale = Vector3.one;
			if (rewardObject.revealClip != null)
			{
				GFAudioManager.instance.PlayClip(rewardObject.revealClip, rewardObject.revealVolume, loop: false, 0f, 128, GFAudioManager.SFX_GROUP, ignoreLastPlayTime: true);
			}
		}
	}
}
