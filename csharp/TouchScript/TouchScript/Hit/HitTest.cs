using UnityEngine;

namespace TouchScript.Hit;

public abstract class HitTest : MonoBehaviour
{
	public enum ObjectHitResult
	{
		Error,
		Hit,
		Miss,
		Discard
	}

	public virtual ObjectHitResult IsHit(ITouchHit hit)
	{
		return ObjectHitResult.Hit;
	}

	private void OnEnable()
	{
	}
}
