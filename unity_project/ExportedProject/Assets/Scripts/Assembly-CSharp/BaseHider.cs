using UnityEngine;

public class BaseHider : MonoBehaviour
{
	public Camera checkCamera;

	public Transform checkTransform;

	public float fadeTime = 0.5f;

	private Renderer _renderer;

	private Vector3 screenPosition;

	private Ray ray;

	private RaycastHit[] hits;

	private float fromAlpha;

	private float targetAlpha = 1f;

	private float fadeTimeline;

	private void Awake()
	{
		_renderer = GetComponent<Renderer>();
		fadeTimeline = fadeTime;
	}

	private void Start()
	{
		if (checkCamera == null)
		{
			checkCamera = Camera.main;
		}
	}

	private void LateUpdate()
	{
		if (checkTransform == null)
		{
			ray = checkCamera.ScreenPointToRay(new Vector3((float)checkCamera.pixelWidth * 0.5f, (float)checkCamera.pixelHeight * 0.5f));
		}
		else
		{
			ray = checkCamera.ScreenPointToRay(checkCamera.WorldToScreenPoint(checkTransform.position));
		}
		hits = Physics.RaycastAll(ray);
		if (hits.Length > 0 && hits[0].normal == -base.transform.up)
		{
			if (targetAlpha != 0f)
			{
				fromAlpha = 1f;
				targetAlpha = 0f;
				fadeTimeline = fadeTime - fadeTimeline;
			}
		}
		else if (targetAlpha != 1f)
		{
			fromAlpha = 0f;
			targetAlpha = 1f;
			fadeTimeline = fadeTime - fadeTimeline;
		}
		if (fadeTimeline < fadeTime)
		{
			fadeTimeline += Time.deltaTime;
			if (fadeTimeline >= fadeTime)
			{
				fadeTimeline = fadeTime;
			}
			Color color = _renderer.material.color;
			color.a = Mathf.Lerp(fromAlpha, targetAlpha, fadeTimeline / fadeTime);
			_renderer.material.color = color;
		}
	}
}
