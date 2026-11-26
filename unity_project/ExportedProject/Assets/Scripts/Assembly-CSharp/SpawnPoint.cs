using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	public List<SpawnPointNPC> npcs = new List<SpawnPointNPC>();

	public PathNode nearestPathNode;

	public bool ignoreFindingNearestPathNode;

	public bool flipCharacter;

	private float yOffset = 0.1f;

	private void Start()
	{
		if (nearestPathNode == null && !ignoreFindingNearestPathNode && CityController.instance != null && CityController.instance.allNodes != null)
		{
			nearestPathNode = CityController.instance.GetNearestPathNodeToPosition(base.transform.position, true);
		}
	}
}
