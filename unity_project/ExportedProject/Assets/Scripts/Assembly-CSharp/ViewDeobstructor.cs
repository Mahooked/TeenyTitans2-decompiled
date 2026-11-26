using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ViewDeobstructor : MonoBehaviour
{
	public Camera thisCamera;

	public List<Transform> trackObjects;

	public Vector3 trackOffset;

	public bool paused;

	[Range(0f, 1f)]
	public float fadeTo = 0.1f;

	public float fadeOutDuration = 0.4f;

	public Ease fadeOutEase = Ease.OutSine;

	public float fadeInDuration = 0.4f;

	public Ease fadeInEase = Ease.InSine;

	public Shader transparentShader;

	private Vector3 screenPosition;

	private Vector3 centerPosition;

	private List<Collider> curCollidersList = new List<Collider>();

	private List<Collider> previousCollidersList = new List<Collider>();

	private RaycastHit[] hits = new RaycastHit[3];

	private int layerMask;

	private float lastCheck;

	private float checkDelay = 0.06f;

	private void Awake()
	{
		if (thisCamera == null)
		{
			thisCamera = GetComponent<Camera>();
		}
		centerPosition = new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
		layerMask = 1 << LayerMask.NameToLayer("Buildings");
	}

	private void LateUpdate()
	{
		if (paused || Time.time - lastCheck < checkDelay)
		{
			return;
		}
		lastCheck = Time.time;
		for (int i = 0; i < trackObjects.Count; i++)
		{
			Transform transform = trackObjects[i];
			if (transform != null)
			{
				screenPosition = thisCamera.WorldToScreenPoint(transform.position + trackOffset);
			}
			else
			{
				screenPosition = centerPosition;
			}
			Ray ray = thisCamera.ScreenPointToRay(screenPosition);
			int num = Physics.RaycastNonAlloc(ray, hits, 100f, layerMask);
			curCollidersList.Clear();
			for (int j = 0; j < num; j++)
			{
				if (hits[j].collider != null)
				{
					Collider collider = hits[j].collider;
					curCollidersList.Add(collider);
				}
			}
			for (int k = 0; k < curCollidersList.Count; k++)
			{
				Collider collider2 = curCollidersList[k];
				if (!previousCollidersList.Contains(collider2))
				{
					previousCollidersList.Add(collider2);
					Renderer component = collider2.GetComponent<Renderer>();
					DOTween.Kill(component.material);
					component.material.DOFade(fadeTo, fadeOutDuration).SetEase(fadeOutEase).SetRecyclable(true);
					component.material.shader = transparentShader;
				}
			}
			for (int num2 = previousCollidersList.Count - 1; num2 >= 0; num2--)
			{
				Collider collider3 = previousCollidersList[num2];
				if (!curCollidersList.Contains(collider3))
				{
					Renderer renderer = collider3.GetComponent<Renderer>();
					DOTween.Kill(renderer.material);
					renderer.material.DOFade(1f, fadeInDuration).SetEase(fadeInEase).SetRecyclable(true)
						.OnComplete(() =>
						{
							renderer.GetComponent<RendererHelper>().RevertToSharedMaterial();
						});
					renderer.material.shader = transparentShader;
					previousCollidersList.Remove(collider3);
				}
			}
		}
	}

	public void ResetAllObstructionsNow()
	{
		for (int num = previousCollidersList.Count - 1; num >= 0; num--)
		{
			Collider item = previousCollidersList[num];
			Renderer component = previousCollidersList[num].GetComponent<Renderer>();
			DOTween.Kill(component.material);
			component.GetComponent<RendererHelper>().RevertToSharedMaterial();
			previousCollidersList.Remove(item);
		}
	}

	public void AddTarget(Transform newTarget)
	{
		if (!trackObjects.Contains(newTarget))
		{
			trackObjects.Add(newTarget);
		}
	}

	public void RemoveTarget(Transform newTarget)
	{
		if (trackObjects.Contains(newTarget))
		{
			trackObjects.Remove(newTarget);
		}
	}
}
