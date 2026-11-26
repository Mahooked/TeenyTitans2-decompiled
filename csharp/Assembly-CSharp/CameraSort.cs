using UnityEngine;

public class CameraSort : MonoBehaviour
{
	public Camera camera;

	public TransparencySortMode sortOrder;

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

	private void Start()
	{
		camera.transparencySortMode = sortOrder;
	}
}
