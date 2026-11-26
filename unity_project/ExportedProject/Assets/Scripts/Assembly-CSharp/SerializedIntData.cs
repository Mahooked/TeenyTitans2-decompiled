using System;

[Serializable]
public class SerializedIntData
{
	public string key;

	public int value;

	public SerializedIntData Copy()
	{
		SerializedIntData serializedIntData = new SerializedIntData();
		serializedIntData.key = key;
		serializedIntData.value = value;
		return serializedIntData;
	}
}
