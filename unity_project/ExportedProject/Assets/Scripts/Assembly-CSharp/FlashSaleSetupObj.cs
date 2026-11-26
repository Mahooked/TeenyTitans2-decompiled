using UnityEngine;

[ExecuteInEditMode]
public class FlashSaleSetupObj : MonoBehaviour
{
	public PathNode spawnNode;

	private PathNode prevSpawnNode;

	private void LateUpdate()
	{
		if (prevSpawnNode != spawnNode)
		{
			prevSpawnNode = spawnNode;
			base.transform.position = spawnNode.transform.position;
		}
	}
}
