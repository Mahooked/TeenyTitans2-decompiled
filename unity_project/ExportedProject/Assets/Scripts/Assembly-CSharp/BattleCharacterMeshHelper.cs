using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterMeshHelper : MonoBehaviour
{
	public List<MeshRenderer> meshes = new List<MeshRenderer>();

	public void FindAllMeshes()
	{
		meshes.Clear();
		meshes.AddRange(base.gameObject.GetComponentsInChildren<MeshRenderer>(true));
	}

	private void LateUpdate()
	{
		for (int i = 0; i < meshes.Count; i++)
		{
			if (meshes[i].transform.parent.localScale.x <= 0.01f || (meshes[i].transform.localScale.x <= 0.01f && meshes[i].transform.localScale.x >= 0f))
			{
				meshes[i].enabled = false;
			}
			else
			{
				meshes[i].enabled = true;
			}
		}
	}
}
