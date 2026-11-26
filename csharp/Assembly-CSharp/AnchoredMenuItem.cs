using UnityEngine;

public class AnchoredMenuItem : MonoBehaviour
{
	public tk2dCameraAnchor cameraAnchor;

	public Vector3 localPositionToAnchor;

	private void Start()
	{
		transform.parent = cameraAnchor.transform;
		transform.localPosition = localPositionToAnchor;
	}
}
