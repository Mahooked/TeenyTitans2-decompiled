using UnityEngine;

namespace Crosstales.Common.Util;

[RequireComponent(typeof(Renderer))]
public class RandomColor : MonoBehaviour
{
	public Vector2 ChangeInterval = new Vector2(5f, 15f);

	[Range(0f, 1f)]
	public float Saturation = 1f;

	[Range(0f, 1f)]
	public float Value = 1f;

	[Range(0f, 1f)]
	public float Opacity = 1f;

	public bool ChangeMaterial;

	public Material Material;

	public Vector2 ColorRange = new Vector2(0f, 360f);

	public bool GrayScale;

	private float elapsedTime;

	private float changeTime;

	private Renderer currentRenderer;

	private Color32 startColor;

	private Color32 endColor;

	private float lerpProgress;

	public void Start()
	{
		elapsedTime = (changeTime = Random.Range(ChangeInterval.x, ChangeInterval.y));
		if (ChangeMaterial)
		{
			startColor = Material.GetColor("_Color");
			return;
		}
		currentRenderer = GetComponent<Renderer>();
		startColor = currentRenderer.material.color;
	}

	public void Update()
	{
		elapsedTime += Time.deltaTime;
		if (elapsedTime > changeTime)
		{
			if (GrayScale)
			{
				int num = (int)Random.Range(ColorRange.x, ColorRange.y);
				endColor = new Color32((byte)num, (byte)num, (byte)num, 1);
			}
			else
			{
				endColor = BaseHelper.HSVToRGB(Random.Range(ColorRange.x, ColorRange.y), Saturation, Value, Opacity);
			}
			changeTime = Random.Range(ChangeInterval.x, ChangeInterval.y);
			lerpProgress = (elapsedTime = 0f);
		}
		if (ChangeMaterial)
		{
			Material.SetColor("_Color", Color.Lerp(startColor, endColor, lerpProgress));
		}
		else
		{
			currentRenderer.material.color = Color.Lerp(startColor, endColor, lerpProgress);
		}
		if (lerpProgress < 1f)
		{
			lerpProgress += Time.deltaTime / (changeTime - 0.1f);
		}
		else if (ChangeMaterial)
		{
			startColor = Material.GetColor("_Color");
		}
		else
		{
			startColor = currentRenderer.material.color;
		}
	}
}
