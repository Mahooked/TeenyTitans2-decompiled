using System.Collections.Generic;

namespace UnityEngine.Purchasing;

public static class IAPConfigurationHelper
{
	public static void PopulateConfigurationBuilder(ref ConfigurationBuilder builder, ProductCatalog catalog)
	{
		foreach (ProductCatalogItem allProduct in catalog.allProducts)
		{
			IDs ds = null;
			if (allProduct.allStoreIDs.Count > 0)
			{
				ds = new IDs();
				foreach (StoreID allStoreID in allProduct.allStoreIDs)
				{
					ds.Add(allStoreID.id, allStoreID.store);
				}
			}
			List<PayoutDefinition> list = new List<PayoutDefinition>();
			foreach (ProductCatalogPayout payout in allProduct.Payouts)
			{
				list.Add(new PayoutDefinition(payout.typeString, payout.subtype, payout.quantity, payout.data));
			}
			builder.AddProduct(allProduct.id, allProduct.type, ds, list.ToArray());
		}
	}
}
