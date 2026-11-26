using UnityEngine;

namespace TouchScript.Hit;

[AddComponentMenu("TouchScript/Behaviors/Untouchable")]
public class Untouchable : HitTest
{
	public bool DiscardTouch;

	public override ObjectHitResult IsHit(ITouchHit hit)
	{
		return (!DiscardTouch) ? ObjectHitResult.Miss : ObjectHitResult.Discard;
	}
}
