using UnityEngine;

public static class GFGameObjectExtensions
{
	public static void SetLayerRecursively(this GameObject go, int layer)
	{
		go.layer = layer;
		int childCount = go.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			go.transform.GetChild(i).gameObject.SetLayerRecursively(layer);
		}
	}
}
