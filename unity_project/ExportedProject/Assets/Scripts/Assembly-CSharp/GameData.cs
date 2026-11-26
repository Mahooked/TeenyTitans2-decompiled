using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class GameData : ISerializable
{
	public int saveVersion = -1;

	public int sortIndex = -1;

	public string fileName = string.Empty;

	public List<SerializedStringData> keyValueStringData = new List<SerializedStringData>();

	public List<SerializedIntData> keyValueIntData = new List<SerializedIntData>();

	private int CURRENT_VERSION = 2;

	public GameData()
	{
	}

	public GameData(SerializationInfo info, StreamingContext ctxt)
	{
		saveVersion = (int)info.GetValue("saveVersion", typeof(int));
		try
		{
			sortIndex = (int)info.GetValue("sortIndex", typeof(int));
		}
		catch (Exception message)
		{
			Debug.Log(message);
			sortIndex = -1;
		}
		try
		{
			SerializedStringData[] collection = (SerializedStringData[])info.GetValue("keyValueStringData", typeof(SerializedStringData[]));
			keyValueStringData = new List<SerializedStringData>(collection);
		}
		catch (Exception message2)
		{
			Debug.Log(message2);
			SerializedStringData[] value = keyValueStringData.ToArray();
			info.AddValue("keyValueStringData", value);
		}
		try
		{
			SerializedIntData[] collection2 = (SerializedIntData[])info.GetValue("keyValueIntData", typeof(SerializedIntData[]));
			keyValueIntData = new List<SerializedIntData>(collection2);
		}
		catch (Exception message3)
		{
			Debug.Log(message3);
			SerializedIntData[] value2 = keyValueIntData.ToArray();
			info.AddValue("keyValueIntData", value2);
		}
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("saveVersion", CURRENT_VERSION);
		info.AddValue("sortIndex", sortIndex);
		SerializedStringData[] value = keyValueStringData.ToArray();
		info.AddValue("keyValueStringData", value);
		SerializedIntData[] value2 = keyValueIntData.ToArray();
		info.AddValue("keyValueIntData", value2);
	}

	public void SetString(string key, string value)
	{
		foreach (SerializedStringData keyValueStringDatum in keyValueStringData)
		{
			if (keyValueStringDatum.key == key)
			{
				keyValueStringDatum.value = value;
				return;
			}
		}
		SerializedStringData serializedStringData = new SerializedStringData();
		serializedStringData.key = key;
		serializedStringData.value = value;
		keyValueStringData.Add(serializedStringData);
	}

	public void SetInt(string key, int value)
	{
		foreach (SerializedIntData keyValueIntDatum in keyValueIntData)
		{
			if (keyValueIntDatum.key == key)
			{
				keyValueIntDatum.value = value;
				return;
			}
		}
		SerializedIntData serializedIntData = new SerializedIntData();
		serializedIntData.key = key;
		serializedIntData.value = value;
		keyValueIntData.Add(serializedIntData);
	}

	public string GetString(string key)
	{
		foreach (SerializedStringData keyValueStringDatum in keyValueStringData)
		{
			if (keyValueStringDatum.key == key)
			{
				return keyValueStringDatum.value;
			}
		}
		return null;
	}

	public string GetString(string key, string defaultValue)
	{
		foreach (SerializedStringData keyValueStringDatum in keyValueStringData)
		{
			if (keyValueStringDatum.key == key)
			{
				return keyValueStringDatum.value;
			}
		}
		return defaultValue;
	}

	public int GetInt(string key, int defaultValue = 0)
	{
		foreach (SerializedIntData keyValueIntDatum in keyValueIntData)
		{
			if (keyValueIntDatum.key == key)
			{
				return keyValueIntDatum.value;
			}
		}
		return defaultValue;
	}

	public bool HasKey(string key)
	{
		for (int i = 0; i < keyValueStringData.Count; i++)
		{
			SerializedStringData serializedStringData = keyValueStringData[i];
			if (serializedStringData.key == key)
			{
				return true;
			}
		}
		for (int j = 0; j < keyValueIntData.Count; j++)
		{
			SerializedIntData serializedIntData = keyValueIntData[j];
			if (serializedIntData.key == key)
			{
				return true;
			}
		}
		return false;
	}

	public void DeleteKey(string key)
	{
		for (int num = keyValueStringData.Count - 1; num >= 0; num--)
		{
			SerializedStringData serializedStringData = keyValueStringData[num];
			if (serializedStringData.key == key)
			{
				keyValueStringData.Remove(serializedStringData);
			}
		}
		keyValueStringData.TrimExcess();
		for (int num2 = keyValueIntData.Count - 1; num2 >= 0; num2--)
		{
			SerializedIntData serializedIntData = keyValueIntData[num2];
			if (serializedIntData.key == key)
			{
				keyValueIntData.Remove(serializedIntData);
			}
		}
		keyValueIntData.TrimExcess();
	}

	public void DeleteStringKeyContaining(string key)
	{
		for (int num = keyValueStringData.Count - 1; num >= 0; num--)
		{
			SerializedStringData serializedStringData = keyValueStringData[num];
			if (serializedStringData.key.Contains(key))
			{
				keyValueStringData.Remove(serializedStringData);
			}
		}
		keyValueStringData.TrimExcess();
	}

	public void DeleteIntKeyContaining(string key)
	{
		for (int num = keyValueIntData.Count - 1; num >= 0; num--)
		{
			SerializedIntData serializedIntData = keyValueIntData[num];
			if (serializedIntData.key.Contains(key))
			{
				keyValueIntData.Remove(serializedIntData);
			}
		}
		keyValueIntData.TrimExcess();
	}

	public GameData Copy()
	{
		GameData gameData = new GameData();
		gameData.saveVersion = saveVersion;
		gameData.sortIndex = sortIndex;
		gameData.fileName = fileName;
		gameData.keyValueStringData = new List<SerializedStringData>();
		for (int i = 0; i < keyValueStringData.Count; i++)
		{
			gameData.keyValueStringData.Add(keyValueStringData[i].Copy());
		}
		gameData.keyValueIntData = new List<SerializedIntData>();
		for (int j = 0; j < keyValueIntData.Count; j++)
		{
			gameData.keyValueIntData.Add(keyValueIntData[j].Copy());
		}
		return gameData;
	}
}
