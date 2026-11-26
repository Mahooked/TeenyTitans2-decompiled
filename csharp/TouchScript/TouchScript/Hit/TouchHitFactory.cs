using UnityEngine;

namespace TouchScript.Hit;

public sealed class TouchHitFactory : ITouchHitFactory
{
	private static TouchHitFactory instance;

	public static ITouchHitFactory Instance => instance ?? (instance = new TouchHitFactory());

	private TouchHitFactory()
	{
	}

	public ITouchHit GetTouchHit(RaycastHit value)
	{
		TouchHit3D touchHit3D = new TouchHit3D();
		touchHit3D.InitWith(value);
		return touchHit3D;
	}

	public ITouchHit GetTouchHit(RaycastHit2D value)
	{
		TouchHit2D touchHit2D = new TouchHit2D();
		touchHit2D.InitWith(value);
		return touchHit2D;
	}

	public ITouchHit GetTouchHit(Transform value)
	{
		TouchHit touchHit = new TouchHit();
		touchHit.InitWith(value);
		return touchHit;
	}
}
