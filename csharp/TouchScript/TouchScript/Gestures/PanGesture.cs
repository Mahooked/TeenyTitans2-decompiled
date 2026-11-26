using System.Collections.Generic;
using TouchScript.Clusters;
using TouchScript.Gestures.Simple;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Pan Gesture")]
public class PanGesture : SimplePanGesture
{
	private TouchScript.Clusters.Clusters clusters = new TouchScript.Clusters.Clusters();

	public override Vector2 ScreenPosition
	{
		get
		{
			if (activeTouches.Count == 0)
			{
				return TouchManager.INVALID_POSITION;
			}
			if (activeTouches.Count == 1)
			{
				return activeTouches[0].Position;
			}
			return (clusters.GetCenterPosition(0) + clusters.GetCenterPosition(1)) * 0.5f;
		}
	}

	public override Vector2 PreviousScreenPosition
	{
		get
		{
			if (activeTouches.Count == 0)
			{
				return TouchManager.INVALID_POSITION;
			}
			if (activeTouches.Count == 1)
			{
				return activeTouches[0].PreviousPosition;
			}
			return (clusters.GetPreviousCenterPosition(0) + clusters.GetPreviousCenterPosition(1)) * 0.5f;
		}
	}

	protected override void touchesBegan(IList<ITouch> touches)
	{
		clusters.AddPoints(touches);
		base.touchesBegan(touches);
	}

	protected override void touchesMoved(IList<ITouch> touches)
	{
		clusters.Invalidate();
		base.touchesMoved(touches);
	}

	protected override void touchesEnded(IList<ITouch> touches)
	{
		clusters.RemovePoints(touches);
		base.touchesEnded(touches);
	}

	protected override void reset()
	{
		base.reset();
		clusters.RemoveAllPoints();
	}
}
