using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun;

public struct InstantiateParameters(string prefabName, Vector3 position, Quaternion rotation, byte group, object[] data, byte objLevelPrefix, int[] viewIDs, Photon.Realtime.Player creator, int timestamp)
{
	public int[] viewIDs = viewIDs;

	public byte objLevelPrefix = objLevelPrefix;

	public object[] data = data;

	public byte group = group;

	public Quaternion rotation = rotation;

	public Vector3 position = position;

	public string prefabName = prefabName;

	public Photon.Realtime.Player creator = creator;

	public int timestamp = timestamp;
}
