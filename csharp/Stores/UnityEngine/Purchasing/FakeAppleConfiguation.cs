using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing;

internal class FakeAppleConfiguation : IAppleConfiguration, IStoreConfiguration
{
	public string appReceipt => "This is a fake receipt. When running on an Apple store, a base64 encoded App Receipt would be returned";

	public bool canMakePayments => true;

	public void SetApplePromotionalPurchaseInterceptorCallback(Action<Product> callback)
	{
	}
}
