using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing;

public class FakeAmazonExtensions : IAmazonExtensions, IStoreExtension, IAmazonConfiguration, IStoreConfiguration
{
	public string amazonUserId => "fakeid";

	public void WriteSandboxJSON(HashSet<ProductDefinition> products)
	{
	}

	public void NotifyUnableToFulfillUnavailableProduct(string transactionID)
	{
	}
}
