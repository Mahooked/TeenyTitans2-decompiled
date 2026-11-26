using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	private void LateUpdate()
	{
		Vector3 position = Camera.main.transform.position;
		position.x = 0f - position.x;
		position.y = transform.position.y;
		position.z = 0f - position.z;
		transform.LookAt(position);
	}
}
