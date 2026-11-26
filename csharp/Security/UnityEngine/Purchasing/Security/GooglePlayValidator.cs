using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Purchasing.Security;

internal class GooglePlayValidator
{
	private RSAKey key;

	public GooglePlayValidator(byte[] rsaKey)
	{
		key = new RSAKey(rsaKey);
	}

	public GooglePlayReceipt Validate(string receipt, string signature)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(receipt);
		byte[] signature2 = Convert.FromBase64String(signature);
		if (!key.Verify(bytes, signature2))
		{
			throw new InvalidSignatureException();
		}
		Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
		dictionary.TryGetValue("orderId", out var value);
		dictionary.TryGetValue("packageName", out var value2);
		dictionary.TryGetValue("productId", out var value3);
		dictionary.TryGetValue("purchaseToken", out var value4);
		dictionary.TryGetValue("purchaseTime", out var value5);
		dictionary.TryGetValue("purchaseState", out var value6);
		DateTime purchaseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long)value5);
		GooglePurchaseState purchaseState = (GooglePurchaseState)(long)value6;
		return new GooglePlayReceipt((string)value3, (string)value, (string)value2, (string)value4, purchaseTime, purchaseState);
	}
}
