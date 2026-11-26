using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
	private static int totalIndex;

	private Vector2 renderRange = new Vector2(-0.2f, 1.2f);

	private int checkIndex;

	private bool disabled;

	private float lastCheck;

	private float distanceFactor = 1f;

	public GameObject objectToDisable;

	private Camera _mainCamera;

	private Camera mainCamera
	{
		get
		{
			if (_mainCamera == null)
			{
				_mainCamera = CityController.instance.pinchZoomer.thisCamera;
			}
			return _mainCamera;
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

	private void Update()
	{
		if (!(Time.time - lastCheck > 0.016f * (float)checkIndex * distanceFactor))
		{
			return;
		}
		lastCheck = Time.time;
		Vector3 vector = mainCamera.WorldToViewportPoint(transform.position);
		distanceFactor = 1f;
		if (vector.x < renderRange.x * 6f || vector.x > renderRange.y * 6f)
		{
			distanceFactor = 4f;
		}
		if (vector.y < renderRange.x * 6f || vector.y > renderRange.y * 6f)
		{
			distanceFactor = 4f;
		}
		if (vector.x >= renderRange.x && vector.x <= renderRange.y && vector.y >= renderRange.x && vector.y <= renderRange.y)
		{
			if (disabled)
			{
				disabled = false;
				if (objectToDisable != null)
				{
					objectToDisable.SetActive(value: true);
				}
			}
		}
		else if (!disabled)
		{
			disabled = true;
			if (objectToDisable != null)
			{
				objectToDisable.SetActive(value: false);
			}
		}
		if (disabled && objectToDisable.activeSelf)
		{
			disabled = false;
		}
		if (!disabled && !objectToDisable.activeSelf)
		{
			disabled = true;
		}
	}
}
