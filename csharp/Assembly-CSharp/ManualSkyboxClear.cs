using UnityEngine;

public class ManualSkyboxClear : MonoBehaviour
{
	[SerializeField]
	private Camera camera;

	private void Awake()
	{
		if (camera == null)
		{
			camera = GetComponent<Camera>();
		}
	}

	private void Reset()
	{
		camera = GetComponent<Camera>();
	}

	private void OnPreRender()
	{
		GL.ClearWithSkybox(clearDepth: true, camera);
	}
}
