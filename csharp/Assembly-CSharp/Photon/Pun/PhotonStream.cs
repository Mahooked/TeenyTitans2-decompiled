using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun;

public class PhotonStream
{
	private readonly Queue<object> writeData;

	private object[] readData;

	private byte currentItem;

	public bool IsWriting { get; private set; }

	public bool IsReading => !IsWriting;

	public int Count => (!IsWriting) ? readData.Length : writeData.Count;

	public PhotonStream(bool write, object[] incomingData)
	{
		IsWriting = write;
		if (incomingData == null)
		{
			writeData = new Queue<object>(10);
		}
		else
		{
			readData = incomingData;
		}
	}

	public void SetReadStream(object[] incomingData, byte pos = 0)
	{
		readData = incomingData;
		currentItem = pos;
		IsWriting = false;
	}

	internal void ResetWriteStream()
	{
		writeData.Clear();
	}

	public object ReceiveNext()
	{
		if (IsWriting)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		object result = readData[currentItem];
		currentItem++;
		return result;
	}

	public object PeekNext()
	{
		if (IsWriting)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		return readData[currentItem];
	}

	public void SendNext(object obj)
	{
		if (!IsWriting)
		{
			Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
		}
		else
		{
			writeData.Enqueue(obj);
		}
	}

	public object[] ToArray()
	{
		return (!IsWriting) ? readData : writeData.ToArray();
	}

	public void Serialize(ref bool myBool)
	{
		if (IsWriting)
		{
			writeData.Enqueue(myBool);
		}
		else if (readData.Length > currentItem)
		{
			myBool = (bool)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref int myInt)
	{
		if (IsWriting)
		{
			writeData.Enqueue(myInt);
		}
		else if (readData.Length > currentItem)
		{
			myInt = (int)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref string value)
	{
		if (IsWriting)
		{
			writeData.Enqueue(value);
		}
		else if (readData.Length > currentItem)
		{
			value = (string)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref char value)
	{
		if (IsWriting)
		{
			writeData.Enqueue(value);
		}
		else if (readData.Length > currentItem)
		{
			value = (char)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref short value)
	{
		if (IsWriting)
		{
			writeData.Enqueue(value);
		}
		else if (readData.Length > currentItem)
		{
			value = (short)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref float obj)
	{
		if (IsWriting)
		{
			writeData.Enqueue(obj);
		}
		else if (readData.Length > currentItem)
		{
			obj = (float)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref Photon.Realtime.Player obj)
	{
		if (IsWriting)
		{
			writeData.Enqueue(obj);
		}
		else if (readData.Length > currentItem)
		{
			obj = (Photon.Realtime.Player)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref Vector3 obj)
	{
		if (IsWriting)
		{
			writeData.Enqueue(obj);
		}
		else if (readData.Length > currentItem)
		{
			obj = (Vector3)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref Vector2 obj)
	{
		if (IsWriting)
		{
			writeData.Enqueue(obj);
		}
		else if (readData.Length > currentItem)
		{
			obj = (Vector2)readData[currentItem];
			currentItem++;
		}
	}

	public void Serialize(ref Quaternion obj)
	{
		if (IsWriting)
		{
			writeData.Enqueue(obj);
		}
		else if (readData.Length > currentItem)
		{
			obj = (Quaternion)readData[currentItem];
			currentItem++;
		}
	}
}
