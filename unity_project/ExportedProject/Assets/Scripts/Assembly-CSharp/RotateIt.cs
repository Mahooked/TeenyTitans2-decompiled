using UnityEngine;

public class RotateIt : MonoBehaviour
{
	public Vector3 RotationAxis;

	private void Start()
	{
	}

	private void Update()
	{
		base.gameObject.transform.Rotate(RotationAxis);
	}
}
