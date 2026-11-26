using UnityEngine;

public class Dumb : MonoBehaviour
{
	public Animator animator;

	private void LateUpdate()
	{
		if (animator.runtimeAnimatorController != null)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.z = 0f - localPosition.z;
			base.transform.localPosition = localPosition;
			Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
			eulerAngles.x = 0f - eulerAngles.x;
			eulerAngles.y = 0f - eulerAngles.y;
			base.transform.localRotation = Quaternion.Euler(eulerAngles);
		}
	}
}
