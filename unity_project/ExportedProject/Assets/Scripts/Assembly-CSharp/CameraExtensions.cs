using UnityEngine;

public static class CameraExtensions
{
	public static void LayerCullingShow(this Camera cam, int layerMask)
	{
		cam.cullingMask |= layerMask;
	}

	public static void LayerCullingShow(this Camera cam, string layer)
	{
		cam.LayerCullingShow(1 << LayerMask.NameToLayer(layer));
	}

	public static void LayerCullingHide(this Camera cam, int layerMask)
	{
		cam.cullingMask &= ~layerMask;
	}

	public static void LayerCullingHide(this Camera cam, string layer)
	{
		cam.LayerCullingHide(1 << LayerMask.NameToLayer(layer));
	}

	public static void LayerCullingToggle(this Camera cam, int layerMask)
	{
		cam.cullingMask ^= layerMask;
	}

	public static void LayerCullingToggle(this Camera cam, string layer)
	{
		cam.LayerCullingToggle(1 << LayerMask.NameToLayer(layer));
	}

	public static bool LayerCullingIncludes(this Camera cam, int layerMask)
	{
		return (cam.cullingMask & layerMask) > 0;
	}

	public static bool LayerCullingIncludes(this Camera cam, string layer)
	{
		return cam.LayerCullingIncludes(1 << LayerMask.NameToLayer(layer));
	}

	public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn)
	{
		bool flag = cam.LayerCullingIncludes(layerMask);
		if (isOn && !flag)
		{
			cam.LayerCullingShow(layerMask);
		}
		else if (!isOn && flag)
		{
			cam.LayerCullingHide(layerMask);
		}
	}

	public static void LayerCullingToggle(this Camera cam, string layer, bool isOn)
	{
		cam.LayerCullingToggle(1 << LayerMask.NameToLayer(layer), isOn);
	}
}
