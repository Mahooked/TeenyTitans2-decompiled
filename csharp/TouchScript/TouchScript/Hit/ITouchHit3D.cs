using UnityEngine;

namespace TouchScript.Hit;

public interface ITouchHit3D : ITouchHit
{
	Collider Collider { get; }

	Vector3 Normal { get; }

	Vector3 Point { get; }

	Rigidbody Rigidbody { get; }
}
