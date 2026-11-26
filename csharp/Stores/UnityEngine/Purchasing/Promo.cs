using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing;

public class Promo
{
	private static JSONStore s_PromoPurchaser = null;

	private static IStoreCallback s_Unity = null;

	private static RuntimePlatform s_RuntimePlatform;

	private static ILogger s_Logger;

	private static string s_Version;

	private static bool s_IsReady = false;

	private static string s_ProductJSON;

	[Preserve]
	public static bool IsReady()
	{
		return s_IsReady;
	}

	[Preserve]
	public static string Version()
	{
		return s_Version;
	}

	public static void InitPromo(RuntimePlatform platform, ILogger logger)
	{
		InitPromo(platform, logger, "Unknown");
	}

	public static void InitPromo(RuntimePlatform platform, ILogger logger, string version)
	{
		s_RuntimePlatform = platform;
		if (logger != null)
		{
			s_Logger = logger;
			s_Version = version;
			return;
		}
		throw new ArgumentException("UnityIAP: Promo initialized with null logger!");
	}

	private static HashSet<Product> UpdatePromoProductList()
	{
		if (s_Unity == null || s_Unity.products == null)
		{
			s_Logger.LogError("UnityIAP Promo", "Trying to update list without manager or products ready");
			return null;
		}
		HashSet<Product> hashSet = new HashSet<Product>();
		Product[] all = s_Unity.products.all;
		Product[] array = all;
		foreach (Product product in array)
		{
			if (product.availableToPurchase && (product.definition.type == ProductType.Consumable || string.IsNullOrEmpty(product.transactionID)))
			{
				hashSet.Add(product);
			}
		}
		return (hashSet.Count > 0) ? hashSet : null;
	}

	internal static void ProvideProductsToAds(JSONStore purchaser, IStoreCallback manager)
	{
		if (purchaser == null || manager == null)
		{
			s_Logger.LogError("UnityIAP Promo", "Attempt to set promo products without a valid purchaser!");
			return;
		}
		s_PromoPurchaser = purchaser;
		s_Unity = manager;
		ProvideProductsToAds(UpdatePromoProductList());
	}

	private static void ProvideProductsToAds(HashSet<Product> productsForAds)
	{
		List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
		if (productsForAds != null)
		{
			foreach (Product productsForAd in productsForAds)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("productId", productsForAd.definition.id);
				dictionary.Add("iapProductId", productsForAd.definition.id);
				dictionary.Add("localizedPriceString", productsForAd.metadata.localizedPriceString);
				dictionary.Add("localizedTitle", productsForAd.metadata.localizedTitle);
				dictionary.Add("imageUrl", null);
				list.Add(dictionary);
			}
		}
		else
		{
			s_Logger.Log("UnityIAP Promo", "Clearing promo product metadata");
		}
		s_ProductJSON = Json.Serialize(list);
		if (list.Count > 0)
		{
			s_Logger.Log("UnityIAP: Promo is ready");
			s_IsReady = true;
		}
	}

	[Preserve]
	public static string QueryPromoProducts()
	{
		return s_ProductJSON;
	}

	[Preserve]
	public static bool InitiatePromoPurchase(string itemRequest)
	{
		return InitiatePurchasingCommand(itemRequest);
	}

	[Preserve]
	public static bool InitiatePurchasingCommand(string command)
	{
		if (string.IsNullOrEmpty(command))
		{
			return false;
		}
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = (Dictionary<string, object>)Json.Deserialize(command);
			if (dictionary == null)
			{
				return false;
			}
			if (dictionary.TryGetValue("purchaseTrackingUrls", out var value))
			{
				if (value != null)
				{
					List<object> list = value as List<object>;
					EventQueue eventQueue = EventQueue.Instance();
					if (list.Count > 0)
					{
						eventQueue.SetIapUrl(list[0] as string);
					}
					if (list.Count > 1)
					{
						eventQueue.SetAdsUrl(list[1] as string);
					}
				}
				dictionary.Remove("purchaseTrackingUrls");
			}
			command = Json.Serialize(dictionary);
			if (!dictionary.TryGetValue("request", out var value2))
			{
				return OldPromoPurchase(command);
			}
			switch (((string)value2).ToLower())
			{
			case "purchase":
				return OldPromoPurchase(command);
			case "setgamerid":
			case "setabgroup":
			case "setids":
			{
				ProfileData profileData = ProfileData.Instance();
				if (dictionary.TryGetValue("gamerId", out var value3))
				{
					profileData.SetGamerId(value3 as string);
				}
				if (dictionary.TryGetValue("gameId", out value3))
				{
					profileData.SetGameId(value3 as string);
				}
				if (dictionary.TryGetValue("abGroup", out value3))
				{
					profileData.SetABGroup(value3 as int?);
				}
				return true;
			}
			case "close":
				if (s_Logger != null)
				{
					s_Logger.Log("UnityIAP Promo: AdUnit closed without purchase");
				}
				return true;
			default:
				if (s_Logger != null)
				{
					s_Logger.LogWarning("UnityIAP Promo", "Unknown IPC Received");
				}
				return false;
			}
		}
		catch (Exception ex)
		{
			if (s_Logger != null)
			{
				s_Logger.LogError("UnityIAP Promo", "Purchasing command argument exception " + ex);
			}
			return false;
		}
	}

	internal static bool OldPromoPurchase(string itemRequest)
	{
		if (!s_IsReady || s_PromoPurchaser == null)
		{
			if (s_Logger != null)
			{
				s_Logger.LogError("UnityIAP Promo", "Promo purchase attempted without proper configuration");
			}
			return false;
		}
		object value = null;
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = (Dictionary<string, object>)Json.Deserialize(itemRequest);
			if (!dictionary.TryGetValue("productId", out value))
			{
				s_Logger.LogError("UnityIAP", "Promo purchase unable to determine Product ID");
				return false;
			}
		}
		catch
		{
			s_Logger.LogError("UnityIAP", "Promo purchase argument exception");
			return false;
		}
		if (string.IsNullOrEmpty(value as string))
		{
			s_Logger.LogError("UnityIAP", "Promo product is null or empty!");
			return false;
		}
		Product product = s_Unity.products.WithID((string)value);
		if (product == null)
		{
			s_Logger.LogError("UnityIAP", "Promo product lookup failed");
			return false;
		}
		dictionary.Add("storeSpecificId", product.definition.storeSpecificId);
		string developerPayload = Json.Serialize(dictionary);
		s_PromoPurchaser.Purchase(product.definition, developerPayload);
		return true;
	}
}
