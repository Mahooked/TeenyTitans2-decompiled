using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public tk2dCamera cameraTk2d;

	public Camera camera;

	public Transform follow;

	public bool approach;

	public float approachFactor = 8f;

	private Vector3 offset;

	private float velocityFOV;

	[SerializeField]
	private float _targetFOV = 12f;

	[SerializeField]
	private float _targetFOVSmoothTime;

	public float targetFOV
	{
		get
		{
			return _targetFOV;
		}
		set
		{
			_targetFOV = value;
		}
	}

	public float targetFOVSmoothTime
	{
		get
		{
			return _targetFOVSmoothTime;
		}
		set
		{
			_targetFOVSmoothTime = value;
		}
	}

	private void Awake()
	{
		if (cameraTk2d == null)
		{
			cameraTk2d = GetComponentInChildren<tk2dCamera>();
		}
		if (camera == null)
		{
			camera = GetComponentInChildren<Camera>();
		}
		if (follow != null)
		{
			offset = transform.position - follow.position;
		}
	}

	private void Start()
	{
		if (PlayerPrefs.HasKey("CameraApproach"))
		{
			approach = PlayerPrefs.GetInt("CameraApproach") == 1;
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus && PlayerPrefs.HasKey("CameraApproach"))
		{
			approach = PlayerPrefs.GetInt("CameraApproach") == 1;
		}
	}

	private void LateUpdate()
	{
		if (follow != null)
		{
			if (approach)
			{
				transform.position = Vector3.Lerp(transform.position, follow.position + offset, Time.deltaTime * approachFactor);
			}
			else
			{
				transform.position = follow.position + offset;
			}
		}
		if (cameraTk2d != null && !Mathf.Approximately(cameraTk2d.CameraSettings.fieldOfView, _targetFOV))
		{
			if (float.IsNaN(velocityFOV))
			{
				velocityFOV = 0f;
			}
			cameraTk2d.CameraSettings.fieldOfView = Mathf.SmoothDamp(cameraTk2d.CameraSettings.fieldOfView, _targetFOV, ref velocityFOV, _targetFOVSmoothTime);
		}
	}
}
