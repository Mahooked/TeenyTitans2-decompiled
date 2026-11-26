using System;
using UnityEngine;

[Serializable]
public class IAPProduct
{
	[Serializable]
	public class StoreID
	{
		public IAPAppStoreType storeType;

		public string id;
	}

	[SerializeField]
	private string _id;

	[SerializeField]
	private LocalizedString _name;

	[SerializeField]
	private LocalizedString _description;

	[SerializeField]
	private StoreID[] _storeIDs;

	[SerializeField]
	private PlayerReward[] _rewards;

	public string id
	{
		get
		{
			return _id;
		}
	}

	public LocalizedString name
	{
		get
		{
			return _name;
		}
	}

	public LocalizedString description
	{
		get
		{
			return _description;
		}
	}

	public StoreID[] storeIDs
	{
		get
		{
			return _storeIDs;
		}
	}

	public PlayerReward[] rewards
	{
		get
		{
			return _rewards;
		}
	}

	public string ProductID
	{
		get
		{
			return _id.Replace("*", Application.identifier);
		}
	}

	public IAPProduct(string id, LocalizedString name, PlayerReward[] rewards, StoreID[] storeIDs = null)
	{
		_id = id;
		_name = name;
		_rewards = rewards;
		_storeIDs = ((storeIDs != null) ? storeIDs : new StoreID[0]);
	}
}
