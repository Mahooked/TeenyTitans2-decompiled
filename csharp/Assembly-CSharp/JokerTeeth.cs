using UnityEngine;

public class JokerTeeth : Drone, IPoolable
{
	public Transform rotationGroup;

	public float rotationSpeed = 10f;

	private void Update()
	{
		if (!animator.IsName("intro"))
		{
			Vector3 eulerAngles = rotationGroup.localRotation.eulerAngles;
			eulerAngles.y += rotationSpeed * Time.deltaTime;
			rotationGroup.localRotation = Quaternion.Euler(eulerAngles);
		}
	}

	public override void PlayAttack()
	{
		base.PlayAttack();
		animator.Play("attack");
	}

	public void ResetObject()
	{
		rotationGroup.localRotation = Quaternion.identity;
	}
}
