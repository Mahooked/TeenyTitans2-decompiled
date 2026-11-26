using System.Collections.Generic;
using TouchScript.Hit;
using UnityEngine;
using UnityEngine.Serialization;

namespace TouchScript.Layers;

[AddComponentMenu("TouchScript/Layers/Camera Layer 2D")]
public sealed class CameraLayer2D : CameraLayerBase
{
	[SerializeField]
	[FormerlySerializedAs("sortedLayerIds")]
	[HideInInspector]
	private int[] layerIds = new int[0];

	private Dictionary<int, int> layerById = new Dictionary<int, int>();

	private List<RaycastHit2D> sortedHits;

	private void OnEnable()
	{
		sortedHits = new List<RaycastHit2D>();
		layerById.Clear();
		for (int i = 0; i < layerIds.Length; i++)
		{
			int key = layerIds[i];
			if (!layerById.ContainsKey(key))
			{
				layerById.Add(key, i);
			}
		}
	}

	protected override LayerHitResult castRay(Ray ray, out ITouchHit hit)
	{
		hit = null;
		RaycastHit2D[] rayIntersectionAll = Physics2D.GetRayIntersectionAll(ray, float.PositiveInfinity, LayerMask);
		if (rayIntersectionAll.Length == 0)
		{
			return LayerHitResult.Miss;
		}
		if (rayIntersectionAll.Length > 1)
		{
			sortHits(rayIntersectionAll);
			RaycastHit2D raycastHit2D = default;
			for (int i = 0; i < sortedHits.Count; i++)
			{
				raycastHit2D = sortedHits[i];
				switch (doHit(raycastHit2D, out hit))
				{
				case HitTest.ObjectHitResult.Hit:
					return LayerHitResult.Hit;
				case HitTest.ObjectHitResult.Discard:
					return LayerHitResult.Miss;
				}
			}
			return LayerHitResult.Miss;
		}
		return doHit(rayIntersectionAll[0], out hit) switch
		{
			HitTest.ObjectHitResult.Hit => LayerHitResult.Hit, 
			HitTest.ObjectHitResult.Error => LayerHitResult.Error, 
			_ => LayerHitResult.Miss, 
		};
	}

	private HitTest.ObjectHitResult doHit(RaycastHit2D raycastHit, out ITouchHit hit)
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

	private void sortHits(RaycastHit2D[] hits)
	{
		sortedHits.Clear();
		sortedHits.AddRange(hits);
		sortedHits.Sort((RaycastHit2D a, RaycastHit2D b) =>
		{
			if (a.collider.transform == b.collider.transform)
			{
				return 0;
			}
			SpriteRenderer component = a.transform.GetComponent<SpriteRenderer>();
			SpriteRenderer component2 = b.transform.GetComponent<SpriteRenderer>();
			if (component != null && component2 != null)
			{
				if (!layerById.TryGetValue(component.sortingLayerID, out var value))
				{
					value = 0;
				}
				if (!layerById.TryGetValue(component2.sortingLayerID, out var value2))
				{
					value2 = 0;
				}
				if (value < value2)
				{
					return 1;
				}
				if (value > value2)
				{
					return -1;
				}
				if (component.sortingOrder < component2.sortingOrder)
				{
					return 1;
				}
				if (component.sortingOrder > component2.sortingOrder)
				{
					return -1;
				}
			}
			Vector3 position = GetComponent<Camera>().transform.position;
			float sqrMagnitude = (a.transform.position - position).sqrMagnitude;
			float sqrMagnitude2 = (b.transform.position - position).sqrMagnitude;
			return (!(sqrMagnitude < sqrMagnitude2)) ? 1 : (-1);
		});
	}
}
