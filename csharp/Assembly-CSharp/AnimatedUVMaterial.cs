using UnityEngine;

[ExecuteInEditMode]
public class AnimatedUVMaterial : MonoBehaviour
{
	[Range(0f, 1f)]
	public float offsetX;

	[Range(1f, 0f)]
	public float offsetY;

	private MeshRenderer renderer;

	private void Awake()
	{
		renderer = GetComponent<MeshRenderer>();
	}

	private void LateUpdate()
	{
		if (renderer != null)
		{
			if (Application.isPlaying)
			{
				renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
			}
			else
			{
				renderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
			}
		}
	}
}
