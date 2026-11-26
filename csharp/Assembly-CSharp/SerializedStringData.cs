using System;

[Serializable]
public class SerializedStringData
{
	public string key;

	public string value;

	public SerializedStringData Copy()
	{
		SerializedStringData serializedStringData = new SerializedStringData();
		serializedStringData.key = key;
		serializedStringData.value = value;
		return serializedStringData;
	}
}
