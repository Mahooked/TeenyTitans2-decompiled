using UnityEngine;

namespace TouchScript.Hit;

public interface ITouchHitFactory
{
	ITouchHit GetTouchHit(RaycastHit value);

	ITouchHit GetTouchHit(RaycastHit2D value);

	ITouchHit GetTouchHit(Transform value);
}
