using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing;

[Serializable]
public class ProductCatalog
{
	private static IProductCatalogImpl instance;

	public string appleSKU;

	public string appleTeamID;

	[SerializeField]
	private List<ProductCatalogItem> products = new List<ProductCatalogItem>();

	public const string kCatalogPath = "Assets/Plugins/UnityPurchasing/Resources/IAPProductCatalog.json";

	public ICollection<ProductCatalogItem> allProducts => products;

	internal static void Initialize()
	{
		if (instance == null)
		{
			Initialize(new ProductCatalogImpl());
		}
	}

	public static void Initialize(IProductCatalogImpl productCatalogImpl)
	{
		instance = productCatalogImpl;
	}

	public void Add(ProductCatalogItem item)
	{
		products.Add(item);
	}

	public void Remove(ProductCatalogItem item)
	{
		products.Remove(item);
	}

	public static string Serialize(ProductCatalog catalog)
	{
		return JsonUtility.ToJson(catalog);
	}

	public static ProductCatalog Deserialize(string catalogJSON)
	{
		return JsonUtility.FromJson<ProductCatalog>(catalogJSON);
	}

	public static ProductCatalog FromTextAsset(TextAsset asset)
	{
		return Deserialize(asset.text);
	}

	public static ProductCatalog LoadDefaultCatalog()
	{
		Initialize();
		return instance.LoadDefaultCatalog();
	}
}
