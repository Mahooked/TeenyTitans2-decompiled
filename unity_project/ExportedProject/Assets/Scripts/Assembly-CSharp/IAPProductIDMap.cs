using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[Serializable]
public class IAPProductIDMap
{
	[Serializable]
	public class IDSet
	{
		public string productID;

		public IAPProduct.StoreID[] storeIDs;

		public IDs ids
		{
			get
			{
				IDs ds = new IDs();
				for (int i = 0; i < storeIDs.Length; i++)
				{
					ds.Add(storeIDs[i].id, appStoreNamesByType[storeIDs[i].storeType]);
				}
				return ds;
			}
		}

		public string ProductID
		{
			get
			{
				return productID.Replace("*", Application.identifier);
			}
		}
	}

	private static Dictionary<IAPAppStoreType, string> appStoreNamesByType = new Dictionary<IAPAppStoreType, string>
	{
		{
			IAPAppStoreType.Apple,
			"AppleAppStore"
		},
		{
			IAPAppStoreType.GooglePlay,
			"GooglePlay"
		},
		{
			IAPAppStoreType.Amazon,
			"AmazonApps"
		}
	};

	[SerializeField]
	private IDSet[] _idSets;

	public IDSet[] idSets
	{
		get
		{
			return _idSets;
		}
	}
}
