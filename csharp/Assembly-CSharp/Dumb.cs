using UnityEngine;

public class Dumb : MonoBehaviour
{
	public Animator animator;

	private void LateUpdate()
	{
		if (animator.runtimeAnimatorController != null)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.z = 0f - localPosition.z;
			transform.localPosition = localPosition;
			Vector3 eulerAngles = transform.localRotation.eulerAngles;
			eulerAngles.x = 0f - eulerAngles.x;
			eulerAngles.y = 0f - eulerAngles.y;
			transform.localRotation = Quaternion.Euler(eulerAngles);
		}
	}
}
