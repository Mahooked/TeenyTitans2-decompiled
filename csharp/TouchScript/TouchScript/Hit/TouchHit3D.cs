using UnityEngine;

namespace TouchScript.Hit;

internal sealed class TouchHit3D : TouchHit, ITouchHit, ITouchHit3D
{
	private RaycastHit hit;

	public Collider Collider => hit.collider;

	public Vector3 Normal => hit.normal;

	public Vector3 Point => hit.point;

	public Rigidbody Rigidbody => hit.rigidbody;

	internal void InitWith(RaycastHit value)
	{
		InitWith(value.collider.transform);
		hit = value;
	}
}
