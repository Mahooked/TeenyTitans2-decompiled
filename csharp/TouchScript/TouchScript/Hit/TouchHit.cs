using UnityEngine;

namespace TouchScript.Hit;

internal class TouchHit : ITouchHit
{
	public Transform Transform { get; private set; }

	internal TouchHit()
	{
	}

	internal void InitWith(Transform value)
	{
		Transform = value;
	}
}
