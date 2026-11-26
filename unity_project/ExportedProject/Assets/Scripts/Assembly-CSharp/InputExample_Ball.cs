using UnityEngine;

public class InputExample_Ball : MonoBehaviour
{
	public float Speed = 1f;

	private void Update()
	{
		base.transform.localPosition += Vector3.forward * Speed * Time.deltaTime;
		if (base.transform.localPosition.z > 100f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
