using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TT2NetMatchData
{
	private TT2NetMatchDataProperties _properties;

	public int id { get; set; }

	public string state { get; set; }

	public TT2NetMatchDataProperties properties
	{
		get
		{
			if (_properties == null)
			{
				_properties = new TT2NetMatchDataProperties();
			}
			return _properties;
		}
		set
		{
			_properties = value;
		}
	}

	public byte[] Serialize()
	{
		using MemoryStream memoryStream = new MemoryStream();
		using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
		{
			binaryWriter.Write(id);
			binaryWriter.Write(state);
			byte[] array = SerializeToBytes(properties);
			binaryWriter.Write(array.Length);
			binaryWriter.Write(array);
		}
		return memoryStream.ToArray();
	}

	public static TT2NetMatchData Desserialize(byte[] data)
	{
		TT2NetMatchData tT2NetMatchData = new TT2NetMatchData();
		using (MemoryStream input = new MemoryStream(data))
		{
			using BinaryReader binaryReader = new BinaryReader(input);
			tT2NetMatchData.id = binaryReader.ReadInt32();
			tT2NetMatchData.state = binaryReader.ReadString();
			int num = binaryReader.ReadInt32();
			if (num > 0)
			{
				object obj = DeserializeFromBytes(binaryReader.ReadBytes(num));
				tT2NetMatchData.properties = (TT2NetMatchDataProperties)obj;
			}
		}
		return tT2NetMatchData;
	}

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
