using UnityEngine;

public class RotateIt : MonoBehaviour
{
	public Vector3 RotationAxis;

	private void Start()
	{
	}

	private void Update()
	{
		gameObject.transform.Rotate(RotationAxis);
	}
}
