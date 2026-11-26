using UnityEngine;

public class InputHelper : MonoBehaviour
{
	private Vector3 _worldPoint;

	private bool _worldPointCalculated;

	private Plane _plane;

	private Camera _mainCamera;

	private static InputHelper _instance;

	public Vector3 worldPoint
	{
		get
		{
			if (!_worldPointCalculated)
			{
				Update();
			}
			return _worldPoint;
		}
	}

	public Camera mainCamera => _mainCamera;

	public static InputHelper instance => _instance;

	private void Awake()
	{
		_instance = this;
		_plane = new Plane(Vector3.up, Vector3.zero);
	}

	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}

	private void Start()
	{
		_mainCamera = Camera.main;
		if (_mainCamera == null)
		{
			_mainCamera = Camera.allCameras[0];
		}
	}

	private void FixedUpdate()
	{
		_worldPointCalculated = false;
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) && !_worldPointCalculated)
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			_plane.Raycast(ray, out var enter);
			_worldPoint = ray.GetPoint(enter);
			_worldPointCalculated = true;
		}
	}

	public static bool GetButtonDown(int button)
	{
		if (Input.touchCount >= button + 1)
		{
			return Input.touches[button].phase == TouchPhase.Began;
		}
		return false;
	}

	public static bool GetButton(int button)
	{
		if (Input.touchCount >= button + 1)
		{
			return Input.touches[button].phase == TouchPhase.Moved || Input.touches[button].phase == TouchPhase.Stationary;
		}
		return false;
	}

	public static bool GetButtonUp(int button)
	{
		if (Input.touchCount >= button + 1)
		{
			return Input.touches[button].phase == TouchPhase.Canceled || Input.touches[button].phase == TouchPhase.Ended;
		}
		return false;
	}

	public static Vector3 GetPosition(int button)
	{
		if (Input.touchCount >= button + 1)
		{
			return new Vector3(Input.touches[button].position.x, Input.touches[button].position.y, 0f);
		}
		return Vector3.zero;
	}

	public Vector3 ScreenToWorldPoint(Vector3 point)
	{
		Ray ray = _mainCamera.ScreenPointToRay(point);
		_plane.Raycast(ray, out var enter);
		return ray.GetPoint(enter);
	}

	public Vector3 ScreenToWorldPoint(Vector2 point)
	{
		return ScreenToWorldPoint(new Vector3(point.x, point.y));
	}
}
