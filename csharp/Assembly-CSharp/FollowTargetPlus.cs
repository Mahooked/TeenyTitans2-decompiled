using UnityEngine;

public class FollowTargetPlus : MonoBehaviour
{
	public Transform target;

	public Vector3 offset;

	public AxisConstraints positionConstraints;

	public float smoothTime;

	private Vector3 velocity;

	private void LateUpdate()
	{
		UpdatePosition();
	}

	public void UpdatePosition()
	{
		if (target != null)
		{
			Vector3 position = new Vector3
			{
				x = ((!positionConstraints.freezeX) ? (target.position.x + offset.x) : transform.position.x),
				y = ((!positionConstraints.freezeY) ? (target.position.y + offset.y) : transform.position.y),
				z = ((!positionConstraints.freezeZ) ? (target.position.z + offset.z) : transform.position.z)
			};
			if (smoothTime <= 0f)
			{
				transform.position = position;
			}
			else
			{
				transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
			}
		}
	}
}
