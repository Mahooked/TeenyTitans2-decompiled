using UnityEngine;

public struct SwipeResult
{
	public Vector3 start;

	public Vector3 end;

	public Vector3 direction;

	public Vector3 worldStart;

	public Vector3 worldEnd;

	public Vector3 worldDirection;

	public float magnitude;

	public float length;

	public Vector2 start2D => new Vector2(start.x, start.y);

	public Vector2 end2D => new Vector2(end.x, end.y);

	public Vector2 direction2D => new Vector2(direction.x, direction.y);

	public Vector2 worldStart2D => new Vector2(worldStart.x, worldStart.y);

	public Vector2 worldEnd2D => new Vector2(worldEnd.x, worldEnd.y);

	public Vector2 worldDirection2D => new Vector2(worldDirection.x, worldDirection.y);
}
