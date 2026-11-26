using UnityEngine;

namespace TouchScript.Utils;

public static class ProjectionUtils
{
	public static Vector3 CameraToPlaneProjection(Vector2 position, Camera camera, Plane projectionPlane)
	{
		float enter = 0f;
		Ray ray = camera.ScreenPointToRay(position);
		if (!projectionPlane.Raycast(ray, out enter) && enter == 0f)
		{
			return -projectionPlane.normal * projectionPlane.GetDistanceToPoint(Vector3.zero);
		}
		return ray.origin + ray.direction * enter;
	}

	public static Vector3 ScreenToPlaneProjection(Vector2 position, Plane projectionPlane)
	{
		float enter = 0f;
		Ray ray = new Ray(position, Vector3.forward);
		if (!projectionPlane.Raycast(ray, out enter) && enter == 0f)
		{
			return -projectionPlane.normal * projectionPlane.GetDistanceToPoint(Vector3.zero);
		}
		return ray.origin + new Vector3(0f, 0f, enter);
	}
}
