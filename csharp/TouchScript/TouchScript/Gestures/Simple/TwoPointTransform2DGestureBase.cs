using System.Collections.Generic;
using UnityEngine;

namespace TouchScript.Gestures.Simple;

public abstract class TwoPointTransform2DGestureBase : Transform2DGestureBase
{
	[SerializeField]
	private float minPointsDistance = 0.5f;

	protected float minPixelDistance;

	protected float minPixelDistanceSquared;

	protected Vector2 screenPosition;

	protected Vector2 previousScreenPosition;

	public virtual float MinPointsDistance
	{
		get
		{
			return minPointsDistance;
		}
		set
		{
			minPointsDistance = value;
			minPixelDistance = minPointsDistance * touchManager.DotsPerCentimeter;
			minPixelDistanceSquared = Mathf.Pow(minPixelDistance, 2f);
		}
	}

	public override Vector2 ScreenPosition
	{
		get
		{
			if (TouchManager.IsInvalidPosition(screenPosition))
			{
				return base.ScreenPosition;
			}
			return screenPosition;
		}
	}

	public override Vector2 PreviousScreenPosition
	{
		get
		{
			if (TouchManager.IsInvalidPosition(previousScreenPosition))
			{
				return base.PreviousScreenPosition;
			}
			return previousScreenPosition;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		minPixelDistance = minPointsDistance * touchManager.DotsPerCentimeter;
		minPixelDistanceSquared = Mathf.Pow(minPixelDistance, 2f);
	}

	protected virtual bool gotEnoughTouches()
	{
		return activeTouches.Count >= 2;
	}

	protected virtual bool relevantTouches(IList<ITouch> touches)
	{
		bool result = false;
		foreach (ITouch touch in touches)
		{
			if (touch == activeTouches[0] || touch == activeTouches[1])
			{
				result = true;
				break;
			}
		}
		return result;
	}

	protected virtual Vector2 getPointScreenPosition(int index)
	{
		if (index < 0)
		{
			index = 0;
		}
		else if (index > 1)
		{
			index = 1;
		}
		return activeTouches[index].Position;
	}

	protected virtual Vector2 getPointPreviousScreenPosition(int index)
	{
		if (index < 0)
		{
			index = 0;
		}
		else if (index > 1)
		{
			index = 1;
		}
		return activeTouches[index].PreviousPosition;
	}

	protected virtual void restart()
	{
		screenPosition = TouchManager.INVALID_POSITION;
		previousScreenPosition = TouchManager.INVALID_POSITION;
	}

	protected override void touchesEnded(IList<ITouch> touches)
	{
		base.touchesEnded(touches);
		if (activeTouches.Count == 1 && (State == GestureState.Began || State == GestureState.Changed))
		{
			restart();
		}
	}

	protected override void reset()
	{
		base.reset();
		restart();
	}
}
