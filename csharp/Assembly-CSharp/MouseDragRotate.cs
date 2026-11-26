using System;
using UnityEngine;

public class MouseDragRotate : MonoBehaviour
{
	public float rotationSpeed = 1f;

	public Action<Quaternion> OnMouseEvent;

	private void OnMouseDrag()
	{
		float angle = Input.GetAxis("Mouse X") * rotationSpeed;
		float angle2 = Input.GetAxis("Mouse Y") * rotationSpeed;
		transform.Rotate(Vector3.down, angle);
		transform.Rotate(Vector3.right, angle2);
		if (OnMouseEvent != null)
		{
			OnMouseEvent(transform.rotation);
		}
	}
}
