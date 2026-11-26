using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UnityEngine.Purchasing;

internal static class ProductDefinitionExtensions
{
	public static string[] Identifiers(this ReadOnlyCollection<ProductDefinition> identifiers)
	{
		return identifiers.Select((ProductDefinition x) => x.storeSpecificId).ToArray();
	}

	internal static List<ProductDefinition> DecodeJSON(this List<object> productsList, string storeName)
	{
		List<ProductDefinition> list = new List<ProductDefinition>();
		try
		{
			foreach (object products in productsList)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)products;
				dictionary.TryGetValue("id", out var value);
				dictionary.TryGetValue("store_ids", out var value2);
				dictionary.TryGetValue("type", out var value3);
				Dictionary<string, object> dictionary2 = value2 as Dictionary<string, object>;
				string storeSpecificId = (string)value;
				if (dictionary2 != null)
				{
					foreach (KeyValuePair<string, object> item2 in dictionary2)
					{
						string text = item2.Key.ToLower();
						string text2 = (string)item2.Value;
						if (!string.IsNullOrEmpty(text2) && storeName.ToLower() == text)
						{
							storeSpecificId = text2;
						}
					}
				}
				ProductType type = (ProductType)Enum.Parse(typeof(ProductType), (string)value3);
				ProductDefinition item = new ProductDefinition((string)value, storeSpecificId, type);
				list.Add(item);
			}
			return list;
		}
		catch
		{
			return null;
		}
	}
}
