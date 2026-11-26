using UnityEngine;

public class Rotate : MonoBehaviour
{
	private Vector3 angle;

	private void Start()
	{
		angle = transform.eulerAngles;
	}

	private void Update()
	{
		angle.y += Time.deltaTime * 100f;
		transform.eulerAngles = angle;
	}
}
