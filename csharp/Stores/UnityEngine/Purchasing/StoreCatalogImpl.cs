using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing;

internal class StoreCatalogImpl
{
	private IAsyncWebUtil m_AsyncUtil;

	private string m_CatalogURL;

	private string m_StoreName;

	private FileReference m_cachedStoreCatalogReference;

	private const string kFileName = "store.json";

	private static ILogger m_Logger;

	private static ProfileData profile;

	private const string kCatalogURL = "https://ecommerce.iap.unity3d.com";

	public static StoreCatalogImpl CreateInstance(string storeName, string baseUrl, ILogger logger)
	{
		if (string.IsNullOrEmpty(storeName) || string.IsNullOrEmpty(baseUrl))
		{
			return null;
		}
		try
		{
			GameObject gameObject = new GameObject();
			Object.DontDestroyOnLoad(gameObject);
			gameObject.name = "Unity IAP E-Commerce";
			gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			AsyncWebUtil util = gameObject.AddComponent<AsyncWebUtil>();
			if (logger == null)
			{
				logger = Debug.unityLogger;
			}
			profile = ProfileData.Instance();
			Dictionary<string, string> profileIds = profile.GetProfileIds();
			string catalogURL = baseUrl + "/catalog" + profileIds.ToQueryString();
			FileReference fileReference = FileReference.CreateInstance("store.json", logger);
			return new StoreCatalogImpl(util, logger, catalogURL, storeName, fileReference);
		}
		catch
		{
			return null;
		}
	}

	internal StoreCatalogImpl(IAsyncWebUtil util, ILogger logger, string catalogURL, string storeName, FileReference fileReference)
	{
		m_AsyncUtil = util;
		m_Logger = logger;
		m_CatalogURL = catalogURL;
		m_StoreName = storeName;
		m_cachedStoreCatalogReference = fileReference;
	}

	internal void FetchProducts(Action<List<ProductDefinition>> callback)
	{
		m_Logger.Log("Fetching optimized store details from " + m_CatalogURL);
		m_AsyncUtil.Get(m_CatalogURL, (string response) =>
		{
			List<ProductDefinition> list = ParseProductsFromJSON(response, m_StoreName);
			if (list == null)
			{
				m_Logger.Log("Failed to fetch IAP catalog due to malformed response, attempting to use cache");
				List<ProductDefinition> obj = ParseProductsFromJSON(m_cachedStoreCatalogReference.Load(), m_StoreName);
				callback(obj);
			}
			else
			{
				m_Logger.Log("Fetched catalog succesfully");
				m_cachedStoreCatalogReference.Save(response);
				callback(list);
			}
		}, (string error) =>
		{
			List<ProductDefinition> obj = ParseProductsFromJSON(m_cachedStoreCatalogReference.Load(), m_StoreName);
			if (string.IsNullOrEmpty(error))
			{
				m_Logger.Log("Failed to fetch IAP catalog due to timeout, attempting to use cache");
				callback(obj);
			}
			else
			{
				m_Logger.Log("Failed to fetch IAP catalog due to unexpected http status code, attempting to use cache");
				callback(obj);
			}
		});
	}

	internal static List<ProductDefinition> ParseProductsFromJSON(string json, string storeName)
	{
		if (string.IsNullOrEmpty(json))
		{
			return null;
		}
		HashSet<ProductDefinition> hashSet = new HashSet<ProductDefinition>();
		try
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(json);
			dictionary.TryGetValue("catalog", out var value);
			if (dictionary.TryGetValue("abGroup", out var value2) && profile != null)
			{
				profile.SetStoreABGroup(Convert.ToInt32(value2));
			}
			Dictionary<string, object> dictionary2 = value as Dictionary<string, object>;
			if (dictionary2.TryGetValue("id", out var value3) && profile != null)
			{
				profile.SetCatalogId(value3 as string);
			}
			dictionary2.TryGetValue("products", out var value4);
			List<object> productsList = (List<object>)value4;
			return productsList.DecodeJSON(storeName);
		}
		catch (Exception ex)
		{
			if (m_Logger != null)
			{
				m_Logger.LogWarning("UnityIAP", "Error parsing catalog, exception " + ex);
			}
			return null;
		}
	}
}
