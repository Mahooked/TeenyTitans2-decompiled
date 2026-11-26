using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Utilities;

public class AotHelper
{
	private static bool s_alwaysFalse = DateTime.UtcNow.Year < 0;

	public static void Ensure(Action action)
	{
		if (IsFalse())
		{
			try
			{
				action();
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("", innerException);
			}
		}
	}

	public static void EnsureType<T>() where T : new()
	{
		Ensure(() =>
		{
			new T();
		});
	}

	public static void EnsureList<T>()
	{
		Ensure(() =>
		{
			List<T> list = new List<T>();
			HashSet<T> hashSet = new HashSet<T>();
			CollectionWrapper<T> collectionWrapper = new CollectionWrapper<T>((IList)list);
			CollectionWrapper<T> collectionWrapper2 = new CollectionWrapper<T>((ICollection<T>)list);
		});
	}

	public static void EnsureDictionary<TKey, TValue>()
	{
		Ensure(() =>
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
			DictionaryWrapper<TKey, TValue> dictionaryWrapper = new DictionaryWrapper<TKey, TValue>((IDictionary)null);
			DictionaryWrapper<TKey, TValue> dictionaryWrapper2 = new DictionaryWrapper<TKey, TValue>((IDictionary<TKey, TValue>)null);
		});
	}

	public static bool IsFalse()
	{
		return s_alwaysFalse;
	}
}
