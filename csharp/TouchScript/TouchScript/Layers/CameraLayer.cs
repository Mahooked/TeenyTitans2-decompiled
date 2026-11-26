using System.Collections.Generic;
using TouchScript.Hit;
using UnityEngine;

namespace TouchScript.Layers;

[AddComponentMenu("TouchScript/Layers/Camera Layer")]
public sealed class CameraLayer : CameraLayerBase
{
	private List<RaycastHit> sortedHits;

	private Transform cachedTransform;

	protected override void Awake()
	{
		base.Awake();
		cachedTransform = GetComponent<Transform>();
	}

	private void OnEnable()
	{
		sortedHits = new List<RaycastHit>(20);
	}

	protected override LayerHitResult castRay(Ray ray, out ITouchHit hit)
	{
		hit = null;
		RaycastHit[] array = Physics.RaycastAll(ray, float.PositiveInfinity, LayerMask);
		if (array.Length == 0)
		{
			return LayerHitResult.Miss;
		}
		if (array.Length > 1)
		{
			sortHits(array);
			RaycastHit raycastHit = default;
			for (int i = 0; i < sortedHits.Count; i++)
			{
				raycastHit = sortedHits[i];
				switch (doHit(raycastHit, out hit))
				{
				case HitTest.ObjectHitResult.Hit:
					return LayerHitResult.Hit;
				case HitTest.ObjectHitResult.Discard:
					return LayerHitResult.Miss;
				}
			}
			return LayerHitResult.Miss;
		}
		return doHit(array[0], out hit) switch
		{
			HitTest.ObjectHitResult.Hit => LayerHitResult.Hit, 
			HitTest.ObjectHitResult.Error => LayerHitResult.Error, 
			_ => LayerHitResult.Miss, 
		};
	}

	private HitTest.ObjectHitResult doHit(RaycastHit raycastHit, out ITouchHit hit)
	{
		hit = TouchHitFactory.Instance.GetTouchHit(raycastHit);
		HitTest[] components = raycastHit.transform.GetComponents<HitTest>();
		if (components.Length == 0)
		{
			return HitTest.ObjectHitResult.Hit;
		}
		HitTest.ObjectHitResult objectHitResult = HitTest.ObjectHitResult.Hit;
		HitTest[] array = components;
		foreach (HitTest hitTest in array)
		{
			if (hitTest.enabled)
			{
				objectHitResult = hitTest.IsHit(hit);
				if (objectHitResult == HitTest.ObjectHitResult.Miss || objectHitResult == HitTest.ObjectHitResult.Discard)
				{
					break;
				}
			}
		}
		return objectHitResult;
	}

	private void sortHits(RaycastHit[] hits)
	{
		Vector3 cameraPos = cachedTransform.position;
		sortedHits.Clear();
		sortedHits.AddRange(hits);
		sortedHits.Sort((RaycastHit a, RaycastHit b) =>
		{
			if (a.collider.transform == b.collider.transform)
			{
				return 0;
			}
			float sqrMagnitude = (a.point - cameraPos).sqrMagnitude;
			float sqrMagnitude2 = (b.point - cameraPos).sqrMagnitude;
			return (!(sqrMagnitude < sqrMagnitude2)) ? 1 : (-1);
		});
	}
}
