using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class RiddleEggRevealQMark : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Revealed__BackingField;

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

	public event Action Revealed
	{
		add
		{
			Action action = Revealed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Revealed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Revealed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Revealed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
			if (Revealed__BackingField != null)
			{
				Revealed__BackingField();
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
		base.gameObject.SetActive(false);
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
			text = ((_reward.currencyType != CurrencyType.Coins) ? ((_reward.amount >= 5) ? "egg_reward_rainbowcoin_Large" : "egg_reward_rainbowcoin_small") : ((_reward.amount >= 1000) ? ((reward.amount >= 2000) ? ((reward.amount >= 3000) ? "egg_reward_coin_XL04" : "egg_reward_coin_Large03") : "egg_reward_coin_medium02") : "egg_reward_coin_small01"));
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
			rewardObject.transform.SetParent(rewardContainer, true);
			rewardObject.transform.localPosition = Vector3.zero;
			rewardObject.transform.localRotation = Quaternion.identity;
			rewardObject.transform.localScale = Vector3.one;
			if (rewardObject.revealClip != null)
			{
				GFAudioManager.instance.PlayClip(rewardObject.revealClip, rewardObject.revealVolume, false, 0f, 128, GFAudioManager.SFX_GROUP, true);
			}
		}
	}
}
