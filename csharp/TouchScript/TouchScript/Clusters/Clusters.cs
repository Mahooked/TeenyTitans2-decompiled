using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript.Clusters;

public sealed class Clusters
{
	public const int CLUSTER1 = 0;

	public const int CLUSTER2 = 1;

	private List<ITouch> points = new List<ITouch>();

	private bool dirty;

	private List<ITouch> cluster1 = new List<ITouch>();

	private List<ITouch> cluster2 = new List<ITouch>();

	private float minPointDistance;

	private float minPointDistanceSqr;

	private bool hasClusters;

	public int PointsCount => points.Count;

	public float MinPointsDistance
	{
		get
		{
			return minPointDistance;
		}
		set
		{
			minPointDistance = value;
			minPointDistanceSqr = value * value;
		}
	}

	public bool HasClusters
	{
		get
		{
			if (dirty)
			{
				distributePoints();
			}
			return hasClusters;
		}
	}

	public Clusters()
	{
		MinPointsDistance = 0f;
		markDirty();
	}

	public Vector2 GetCenterPosition(int id)
	{
		if (!HasClusters)
		{
			return TouchManager.INVALID_POSITION;
		}
		return id switch
		{
			0 => ClusterUtils.Get2DCenterPosition(cluster1), 
			1 => ClusterUtils.Get2DCenterPosition(cluster2), 
			_ => TouchManager.INVALID_POSITION, 
		};
	}

	public Vector2 GetPreviousCenterPosition(int id)
	{
		if (!HasClusters)
		{
			return TouchManager.INVALID_POSITION;
		}
		return id switch
		{
			0 => ClusterUtils.GetPrevious2DCenterPosition(cluster1), 
			1 => ClusterUtils.GetPrevious2DCenterPosition(cluster2), 
			_ => TouchManager.INVALID_POSITION, 
		};
	}

	public void AddPoint(ITouch point)
	{
		if (!points.Contains(point))
		{
			points.Add(point);
			markDirty();
		}
	}

	public void AddPoints(IList<ITouch> points)
	{
		foreach (ITouch point in points)
		{
			AddPoint(point);
		}
	}

	public void RemovePoint(ITouch point)
	{
		if (points.Contains(point))
		{
			points.Remove(point);
			markDirty();
		}
	}

	public void RemovePoints(IList<ITouch> points)
	{
		foreach (ITouch point in points)
		{
			RemovePoint(point);
		}
	}

	public void RemoveAllPoints()
	{
		points.Clear();
		markDirty();
	}

	public void Invalidate()
	{
		markDirty();
	}

	private void distributePoints()
	{
		cluster1.Clear();
		cluster2.Clear();
		hasClusters = checkClusters();
		if (!hasClusters)
		{
			return;
		}
		cluster1.Add(points[0]);
		cluster2.Add(points[1]);
		int count = points.Count;
		if (count == 2)
		{
			return;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = "#";
		string text4 = "#";
		while (text != text3 || text2 != text4)
		{
			Vector2 vector = ClusterUtils.Get2DCenterPosition(cluster1);
			Vector2 vector2 = ClusterUtils.Get2DCenterPosition(cluster2);
			ITouch touch = null;
			ITouch touch2 = null;
			float num = float.MinValue;
			float num2 = float.MinValue;
			for (int i = 0; i < count; i++)
			{
				ITouch touch3 = points[i];
				float sqrMagnitude = (vector - touch3.Position).sqrMagnitude;
				if (sqrMagnitude > num2)
				{
					num2 = sqrMagnitude;
					touch2 = touch3;
				}
				sqrMagnitude = (vector2 - touch3.Position).sqrMagnitude;
				if (sqrMagnitude > num)
				{
					num = sqrMagnitude;
					touch = touch3;
				}
			}
			if (touch == touch2)
			{
				vector = (vector + vector2) * 0.5f;
				vector2 = touch2.Position;
			}
			else
			{
				vector = touch.Position;
				vector2 = touch2.Position;
			}
			cluster1.Clear();
			cluster2.Clear();
			for (int j = 0; j < count; j++)
			{
				ITouch touch4 = points[j];
				if ((vector - touch4.Position).sqrMagnitude < (vector2 - touch4.Position).sqrMagnitude)
				{
					cluster1.Add(touch4);
				}
				else
				{
					cluster2.Add(touch4);
				}
			}
			text = text3;
			text2 = text4;
			text3 = ClusterUtils.GetPointsHash(cluster1);
			text4 = ClusterUtils.GetPointsHash(cluster2);
		}
		markClean();
	}

	private bool checkClusters()
	{
		int num = points.Count - 1;
		if (num < 1)
		{
			return false;
		}
		if (num == 1)
		{
			Vector2 position = points[0].Position;
			Vector2 position2 = points[1].Position;
			float num2 = position.x - position2.x;
			float num3 = position.y - position2.y;
			if (num2 * num2 + num3 * num3 >= minPointDistanceSqr)
			{
				return true;
			}
			return false;
		}
		for (int i = 0; i < num; i++)
		{
			for (int j = i + 1; j <= num; j++)
			{
				Vector2 position3 = points[i].Position;
				Vector2 position4 = points[j].Position;
				float num4 = position3.x - position4.x;
				float num5 = position3.y - position4.y;
				if (num4 * num4 + num5 * num5 >= minPointDistanceSqr)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void markDirty()
	{
		dirty = true;
	}

	private void markClean()
	{
		dirty = false;
	}
}
