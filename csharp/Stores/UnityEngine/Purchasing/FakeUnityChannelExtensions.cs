using System;

namespace UnityEngine.Purchasing;

public class FakeUnityChannelExtensions : IUnityChannelExtensions, IStoreExtension
{
	public void ConfirmPurchase(string transactionId, Action<bool, string, string> callback)
	{
		callback(arg1: true, "fakeTransactionId", "fakeMessage");
	}

	public void ValidateReceipt(string transactionId, Action<bool, string, string> callback)
	{
		callback(arg1: true, "fakeSignData", "fakeSignature");
	}

	public string GetLastPurchaseError()
	{
		return "{ \"error\": \"DuplicateTransaction\" }";
	}
}
