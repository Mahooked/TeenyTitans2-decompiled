using UnityEngine;

namespace TouchScript.Hit;

internal sealed class TouchHit2D : TouchHit, ITouchHit, ITouchHit2D
{
	private RaycastHit2D hit;

	public Collider2D Collider2D => hit.collider;

	public Vector3 Point => hit.point;

	public Rigidbody2D Rigidbody2D => hit.rigidbody;

	internal void InitWith(RaycastHit2D value)
	{
		InitWith(value.collider.transform);
		hit = value;
	}
}
