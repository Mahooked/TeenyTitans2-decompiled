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
			if (base.transform.localScale.x == 0.2f)
			{
				Vector3 localScale = new Vector3(0.35f, 0.2f, base.transform.localScale.z);
				base.transform.localScale = localScale;
			}
			return;
		}
		if ((double)base.transform.localScale.x <= 0.5 && (double)base.transform.localScale.y <= 0.5)
		{
			Vector3 localScale2 = new Vector3(0f, 0f, base.transform.localScale.z);
			base.transform.localScale = localScale2;
		}
		if (base.transform.localScale.x >= maxScale && base.transform.localScale.y >= maxScale)
		{
			Vector3 localScale3 = new Vector3(maxScale, maxScale, base.transform.localScale.z);
			base.transform.localScale = localScale3;
		}
		if (ScaleDownHide)
		{
			Vector3 localScale4 = new Vector3(0f, 0f, base.transform.localScale.z);
			base.transform.localScale = localScale4;
		}
		if (ForceBehind)
		{
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, -1f);
			base.transform.position = position;
		}
	}
}
