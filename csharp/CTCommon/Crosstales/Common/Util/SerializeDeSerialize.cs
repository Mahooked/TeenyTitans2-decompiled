using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Crosstales.Common.Util;

public class SerializeDeSerialize<T>
{
	private BinaryFormatter binaryFormatter => new BinaryFormatter
	{
		AssemblyFormat = FormatterAssemblyStyle.Simple
	};

	public void ToFile(T o, string path)
	{
		try
		{
			using FileStream serializationStream = new FileStream(path, FileMode.Create, FileAccess.Write);
			binaryFormatter.Serialize(serializationStream, o);
		}
		catch (Exception ex)
		{
			Debug.LogError("Could not save serialized file: " + ex);
		}
	}

	public MemoryStream ToMemory(T o)
	{
		MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, o);
		return memoryStream;
	}

	public byte[] ToByteArray(T o)
	{
		using MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, o);
		return memoryStream.ToArray();
	}

	public T FromFile(string path)
	{
		T result = default;
		try
		{
			using FileStream serializationStream = new FileStream(path, FileMode.Open);
			result = (T)binaryFormatter.Deserialize(serializationStream);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError("Could not load serialized file: " + ex);
		}
		return result;
	}

	public T FromMemory(byte[] data)
	{
		using MemoryStream serializationStream = new MemoryStream(data);
		return (T)binaryFormatter.Deserialize(serializationStream);
	}
}
