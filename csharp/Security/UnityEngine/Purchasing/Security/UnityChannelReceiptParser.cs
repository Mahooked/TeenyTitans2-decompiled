using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Security;

public class UnityChannelReceiptParser
{
	public UnityChannelReceipt ParseUnityChannelReceipt(string receipt)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
		dictionary.TryGetValue("cpOrderId", out var value);
		object obj = "";
		dictionary.TryGetValue("productId", out var value2);
		dictionary.TryGetValue("paidTime", out var value3);
		dictionary.TryGetValue("quantity", out var value4);
		dictionary.TryGetValue("status", out var value5);
		dictionary.TryGetValue("clientId", out var value6);
		dictionary.TryGetValue("payFee", out var value7);
		dictionary.TryGetValue("orderAttemptId", out var value8);
		dictionary.TryGetValue("country", out var value9);
		dictionary.TryGetValue("currency", out var value10);
		DateTime result = default;
		if (!DateTime.TryParse((string)value3, out result))
		{
			result = DateTime.MinValue;
		}
		return new UnityChannelReceipt((string)value, (string)obj, (string)value2, result, (string)value5, (string)value6, (string)value7, (string)value8, (string)value9, (string)value10, (string)value4);
	}
}
