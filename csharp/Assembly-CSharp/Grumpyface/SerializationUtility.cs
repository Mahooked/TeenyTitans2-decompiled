using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Grumpyface;

public static class SerializationUtility
{
	public static byte[] SerializeToBytes<T>(T item)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, item);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		return memoryStream.ToArray();
	}

	public static object DeserializeFromBytes(byte[] bytes)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using MemoryStream serializationStream = new MemoryStream(bytes);
		return binaryFormatter.Deserialize(serializationStream);
	}
}
