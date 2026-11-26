using UnityEngine;

public class Clamp : MonoBehaviour
{
	public bool ScaleDownHide;

	public bool ForceBehind;

	public bool ForceScale;

	public float maxScale = 1f;

	private void LateUpdate()
	{
		if (ForceScale)
		{
			if (transform.localScale.x == 0.2f)
			{
				Vector3 localScale = new Vector3(0.35f, 0.2f, transform.localScale.z);
				transform.localScale = localScale;
			}
			return;
		}
		if ((double)transform.localScale.x <= 0.5 && (double)transform.localScale.y <= 0.5)
		{
			Vector3 localScale2 = new Vector3(0f, 0f, transform.localScale.z);
			transform.localScale = localScale2;
		}
		if (transform.localScale.x >= maxScale && transform.localScale.y >= maxScale)
		{
			Vector3 localScale3 = new Vector3(maxScale, maxScale, transform.localScale.z);
			transform.localScale = localScale3;
		}
		if (ScaleDownHide)
		{
			Vector3 localScale4 = new Vector3(0f, 0f, transform.localScale.z);
			transform.localScale = localScale4;
		}
		if (ForceBehind)
		{
			Vector3 position = new Vector3(transform.position.x, transform.position.y, -1f);
			transform.position = position;
		}
	}
}
