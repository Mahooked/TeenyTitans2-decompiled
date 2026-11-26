using UnityEngine;

public static class CameraUtility
{
	public static Camera GetCameraForLayer(int layerMask)
	{
		int num = Camera.allCameras.Length;
		for (int i = 0; i < num; i++)
		{
			if (Camera.allCameras[i].LayerCullingIncludes(layerMask))
			{
				return Camera.allCameras[i];
			}
		}
		return null;
	}

	public static Camera GetCameraRenderingObject(GameObject go)
	{
		Camera camera = null;
		Camera[] allCameras = Camera.allCameras;
		Renderer componentInChildren = go.GetComponentInChildren<Renderer>();
		if (componentInChildren != null)
		{
			Bounds bounds = componentInChildren.bounds;
			int num = allCameras.Length;
			for (int i = 0; i < num; i++)
			{
				if (allCameras[i].LayerCullingIncludes(1 << go.layer) && componentInChildren.IsVisibleFrom(allCameras[i]))
				{
					camera = allCameras[i];
					break;
				}
			}
		}
		if (camera == null)
		{
			Bounds bounds2 = new Bounds(go.transform.position, Vector3.one);
			Plane[] array = null;
			int num2 = allCameras.Length;
			for (int j = 0; j < num2; j++)
			{
				if (allCameras[j].LayerCullingIncludes(1 << go.layer))
				{
					array = GeometryUtility.CalculateFrustumPlanes(allCameras[j]);
					if (GeometryUtility.TestPlanesAABB(array, bounds2))
					{
						camera = allCameras[j];
						break;
					}
				}
			}
			if (camera == null)
			{
				float num3 = 0f;
				for (int k = 0; k < num2; k++)
				{
					if (!allCameras[k].LayerCullingIncludes(1 << go.layer))
					{
						continue;
					}
					if (camera == null)
					{
						camera = allCameras[k];
						num3 = Vector3.Distance(allCameras[k].transform.position, go.transform.position);
						continue;
					}
					float num4 = Vector3.Distance(allCameras[k].transform.position, go.transform.position);
					if (num4 < num3)
					{
						camera = allCameras[k];
						num3 = num4;
					}
				}
			}
		}
		return camera;
	}

	public static Vector3 GetWorldPositionInCamera(Vector3 position, Camera sourceCamera, Camera targetCamera)
	{
		if (sourceCamera == null || targetCamera == null)
		{
			Debug.LogError(string.Concat("CameraUtility.cs, NULL Parameters passed in, Source Camera: ", sourceCamera, ", Target Camera: ", targetCamera, ". Returning position argument."));
			return position;
		}
		Vector3 position2 = sourceCamera.WorldToScreenPoint(position);
		return targetCamera.ScreenToWorldPoint(position2);
	}
}
