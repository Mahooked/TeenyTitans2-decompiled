using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class TransformLink : MonoBehaviour
{
	public Transform linkTo;

	public bool linkPosition;

	public bool linkRotation;

	public bool linkRotationX = true;

	public bool linkRotationY = true;

	public bool linkRotationZ = true;

	public Space rotationSpace = Space.Self;

	public bool linkScale;

	private bool _ignoreDisable;

	private Transform _thisTransform;

	private TransformLinkHelper linkHelper;

	private static int totalIndex;

	private Vector2 renderRange = new Vector2(-0.2f, 1.2f);

	private int checkIndex;

	private bool disabled;

	private bool forceUpdate;

	private float lastCheck;

	private float distanceFactor = 1f;

	private CityController _cityController;

	private Camera _mainCamera;

	public Transform LinkTo
	{
		get
		{
			return linkTo;
		}
		set
		{
			if (!(value != linkTo))
			{
				return;
			}
			linkTo = value;
			if (linkTo != null)
			{
				linkHelper = linkTo.GetComponent<TransformLinkHelper>();
				if (linkRotation)
				{
					LinkRotation();
				}
			}
			else
			{
				linkHelper = null;
			}
		}
	}

	public bool ignoreDisable
	{
		get
		{
			return _ignoreDisable;
		}
		set
		{
			_ignoreDisable = value;
		}
	}

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
		_thisTransform = base.transform;
		if (linkTo != null)
		{
			linkHelper = linkTo.GetComponent<TransformLinkHelper>();
			if (linkRotation)
			{
				LinkRotation();
			}
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		checkIndex = totalIndex++;
		if (totalIndex > 18)
		{
			totalIndex = 2;
		}
		if (cityController != null)
		{
			cityController.pinchZoomer.ZoomedIn += HandleZoomedIn;
		}
		if (linkRotation && linkTo != null)
		{
			LinkRotation();
		}
	}

	private void OnDestroy()
	{
		if (cityController != null)
		{
			cityController.pinchZoomer.ZoomedIn -= HandleZoomedIn;
		}
	}

	private void HandleZoomedIn()
	{
		StopCoroutine("ForceLinkUpdate");
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine("ForceLinkUpdate");
		}
	}

	private IEnumerator ForceLinkUpdate()
	{
		forceUpdate = true;
		yield return new WaitForSeconds(3f);
		forceUpdate = false;
	}

	private void LateUpdate()
	{
		if (linkTo == null)
		{
			return;
		}
		if (linkHelper != null)
		{
			if (linkRotation && linkHelper.HasRotationChanged)
			{
				LinkRotation();
			}
		}
		else if (cityController != null)
		{
			if (Time.time - lastCheck > 0.016f * (float)checkIndex * distanceFactor)
			{
				lastCheck = Time.time;
				Vector3 vector = mainCamera.WorldToViewportPoint(base.transform.position);
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
					disabled = false;
				}
				else
				{
					disabled = true;
				}
			}
			if (linkRotation && (!disabled || forceUpdate || ignoreDisable))
			{
				LinkRotation();
			}
		}
		else if (linkRotation)
		{
			LinkRotation();
		}
	}

	public void LinkRotation()
	{
		if (_thisTransform == null)
		{
			_thisTransform = base.transform;
		}
		if (rotationSpace == Space.Self)
		{
			Vector3 eulerAngles = _thisTransform.localRotation.eulerAngles;
			if (linkRotationX)
			{
				eulerAngles.x = linkTo.localRotation.eulerAngles.x;
			}
			if (linkRotationY)
			{
				eulerAngles.y = linkTo.localRotation.eulerAngles.y;
			}
			if (linkRotationZ)
			{
				eulerAngles.z = linkTo.localRotation.eulerAngles.z;
			}
			_thisTransform.localRotation = Quaternion.Euler(eulerAngles);
		}
		else
		{
			Vector3 eulerAngles2 = _thisTransform.eulerAngles;
			if (linkRotationX)
			{
				eulerAngles2.x = linkTo.eulerAngles.x;
			}
			if (linkRotationY)
			{
				eulerAngles2.y = linkTo.eulerAngles.y;
			}
			if (linkRotationZ)
			{
				eulerAngles2.z = linkTo.eulerAngles.z;
			}
			_thisTransform.rotation = Quaternion.Euler(eulerAngles2);
		}
	}
}
