using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TouchScript.Utils;

public static class ClusterUtils
{
	private static StringBuilder hashString = new StringBuilder();

	public static Vector2 Get2DCenterPosition(IList<ITouch> touches)
	{
		int count = touches.Count;
		switch (count)
		{
		case 0:
			return TouchManager.INVALID_POSITION;
		case 1:
			return touches[0].Position;
		default:
		{
			Vector2 vector = default;
			foreach (ITouch touch in touches)
			{
				vector += touch.Position;
			}
			return vector / count;
		}
		}
	}

	public static Vector2 GetPrevious2DCenterPosition(IList<ITouch> touches)
	{
		int count = touches.Count;
		switch (count)
		{
		case 0:
			return TouchManager.INVALID_POSITION;
		case 1:
			return touches[0].PreviousPosition;
		default:
		{
			Vector2 vector = default;
			foreach (ITouch touch in touches)
			{
				vector += touch.PreviousPosition;
			}
			return vector / count;
		}
		}
	}

	public static string GetPointsHash(IList<ITouch> touches)
	{
		hashString.Remove(0, hashString.Length);
		for (int i = 0; i < touches.Count; i++)
		{
			hashString.Append("#");
			hashString.Append(touches[i].Id);
		}
		return hashString.ToString();
	}
}
