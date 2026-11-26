using System;
using UnityEngine;

[Serializable]
public class PathConnection
{
	public PathNode end;

	public Vector3[] curveWaypoints;

	public PathConnection(PathNode end, Vector3[] curveWaypoints = null)
	{
		this.end = end;
		this.curveWaypoints = curveWaypoints;
		if (this.curveWaypoints == null)
		{
			this.curveWaypoints = new Vector3[0];
		}
	}
}
