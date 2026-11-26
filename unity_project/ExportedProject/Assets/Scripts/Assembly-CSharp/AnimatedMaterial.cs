using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatedMaterial : MonoBehaviour
{
	public float offsetX;

	public float offsetY;

	private MeshRenderer renderer;

	private static int totalIndex;

	private Vector2 renderRange = new Vector2(-0.5f, 1.5f);

	private int checkIndex;

	private bool disabled;

	private bool forceUpdate;

	private float lastCheck;

	private float distanceFactor = 1f;

	public bool ignoreRenderCheck;

	private CityController _cityController;

	private Camera _mainCamera;

	private CityController cityController
	{
		get
		{
			if (_cityController == null && CityController.instance != null)
			{
				_cityController = CityController.instance;
			}
			return _cityController;
		}
	}

	private Camera mainCamera
	{
		get
		{
			if (_mainCamera == null && cityController != null)
			{
				_mainCamera = cityController.pinchZoomer.thisCamera;
			}
			return _mainCamera;
		}
	}

	private void Awake()
	{
		renderer = GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		if (cityController != null)
		{
			cityController.pinchZoomer.ZoomedIn += HandleZoomedIn;
		}
	}

	private void OnDisable()
	{
		if (cityController != null)
		{
			cityController.pinchZoomer.ZoomedIn -= HandleZoomedIn;
		}
	}

	private void Start()
	{
		checkIndex = totalIndex++;
		if (totalIndex > 18)
		{
			totalIndex = 2;
		}
	}

	private void HandleZoomedIn()
	{
		StopCoroutine("ForceUpdate");
		StartCoroutine("ForceUpdate");
	}

	private IEnumerator ForceUpdate()
	{
		forceUpdate = true;
		yield return new WaitForSeconds(1f);
		forceUpdate = false;
	}

	private void LateUpdate()
	{
		if (cityController != null && !ignoreRenderCheck)
		{
			if (Time.time - lastCheck > 0.016f * (float)checkIndex * distanceFactor)
			{
				lastCheck = Time.time;
				Vector3 vector = mainCamera.WorldToViewportPoint(base.transform.position);
				distanceFactor = 1f;
				if (vector.x < renderRange.x * 6f || vector.x > renderRange.y * 6f)
				{
					distanceFactor = 3f;
				}
				if (vector.y < renderRange.x * 6f || vector.y > renderRange.y * 6f)
				{
					distanceFactor = 3f;
				}
				if (vector.x >= renderRange.x && vector.x <= renderRange.y && vector.y >= renderRange.x && vector.y <= renderRange.y)
				{
					disabled = false;
				}
				else
				{
					disabled = true;
				}
			}
			if (!disabled || forceUpdate)
			{
				UpdateMaterial();
			}
		}
		else
		{
			UpdateMaterial();
		}
	}

	private void UpdateMaterial()
	{
		if (renderer != null)
		{
			renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
		}
	}
}
