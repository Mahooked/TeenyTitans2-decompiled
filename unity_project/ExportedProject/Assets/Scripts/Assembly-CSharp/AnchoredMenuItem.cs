using UnityEngine;

public class AnchoredMenuItem : MonoBehaviour
{
	public tk2dCameraAnchor cameraAnchor;

	public Vector3 localPositionToAnchor;

	private void Start()
	{
		base.transform.parent = cameraAnchor.transform;
		base.transform.localPosition = localPositionToAnchor;
	}
}
