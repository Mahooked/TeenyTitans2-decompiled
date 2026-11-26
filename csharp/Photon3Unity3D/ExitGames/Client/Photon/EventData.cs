using System.Collections.Generic;

namespace ExitGames.Client.Photon;

public class EventData
{
	public byte Code;

	public Dictionary<byte, object> Parameters;

	private static byte SenderKey = 254;

	private int sender = -1;

	private static byte CustomDataKey = 245;

	public int Sender
	{
		get
		{
			if (sender >= 0)
			{
				return sender;
			}
			sender = 0;
			object obj = this[SenderKey];
			if (obj is int)
			{
				sender = (int)obj;
			}
			return sender;
		}
	}

	public object CustomData => this[CustomDataKey];

	public object this[byte key]
	{
		get
		{
			Parameters.TryGetValue(key, out var value);
			return value;
		}
		set
		{
			Parameters[key] = value;
		}
	}

	public override string ToString()
	{
		return $"Event {Code.ToString()}.";
	}

	public string ToStringFull()
	{
		return $"Event {Code}: {SupportClass.DictionaryToString(Parameters)}";
	}
}
