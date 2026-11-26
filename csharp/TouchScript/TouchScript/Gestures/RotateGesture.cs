using System.Collections.Generic;
using TouchScript.Clusters;
using TouchScript.Gestures.Simple;
using UnityEngine;

namespace TouchScript.Gestures;

[AddComponentMenu("TouchScript/Gestures/Rotate Gesture")]
public class RotateGesture : SimpleRotateGesture
{
	protected TouchScript.Clusters.Clusters clusters = new TouchScript.Clusters.Clusters();

	public override float MinPointsDistance
	{
		get
		{
			return base.MinPointsDistance;
		}
		set
		{
			base.MinPointsDistance = value;
			if (Application.isPlaying)
			{
				clusters.MinPointsDistance = minPixelDistance;
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		clusters.MinPointsDistance = minPixelDistance;
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

	protected override bool gotEnoughTouches()
	{
		return clusters.HasClusters;
	}

	protected override bool relevantTouches(IList<ITouch> touches)
	{
		return true;
	}

	protected override Vector2 getPointScreenPosition(int index)
	{
		if (index < 0)
		{
			index = 0;
		}
		else if (index > 1)
		{
			index = 1;
		}
		return clusters.GetCenterPosition(index);
	}

	protected override Vector2 getPointPreviousScreenPosition(int index)
	{
		if (index < 0)
		{
			index = 0;
		}
		else if (index > 1)
		{
			index = 1;
		}
		return clusters.GetPreviousCenterPosition(index);
	}
}
