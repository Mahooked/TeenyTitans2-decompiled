using UnityEngine;

public class AnimatorDisabler : MonoBehaviour
{
	private static int totalIndex;

	private Vector2 renderRange = new Vector2(-0.5f, 1.5f);

	private int checkIndex;

	private bool disabled;

	private float lastCheck;

	private float distanceFactor = 1f;

	private Animator _animator;

	private Camera _mainCamera;

	private Animator animator
	{
		get
		{
			if (_animator == null)
			{
				_animator = GetComponent<Animator>();
			}
			return _animator;
		}
	}

	private Camera mainCamera
	{
		get
		{
			if (_mainCamera == null)
			{
				_mainCamera = ((!(CityController.instance != null)) ? Camera.main : CityController.instance.pinchZoomer.thisCamera);
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
			distanceFactor = 5f;
		}
		if (vector.y < renderRange.x * 6f || vector.y > renderRange.y * 6f)
		{
			distanceFactor = 5f;
		}
		if (vector.x >= renderRange.x && vector.x <= renderRange.y && vector.y >= renderRange.x && vector.y <= renderRange.y)
		{
			if (disabled)
			{
				disabled = false;
				if (animator != null)
				{
					animator.enabled = true;
				}
			}
		}
		else if (!disabled)
		{
			disabled = true;
			if (animator != null)
			{
				animator.enabled = false;
			}
		}
		if (disabled && animator.enabled)
		{
			disabled = false;
		}
		if (!disabled && !animator.enabled)
		{
			disabled = true;
		}
	}
}
