using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
	[Tooltip("Time in seconds between each change in the camera's position offset")]
	[Range(0.016f, float.MaxValue)]
	public float shakeDelay = 0.02f;

	[Tooltip("The rate at which the shake intensity reduces over time")]
	[Range(0f, 0.999f)]
	public float shakeDecay = 0.823f;

	[HideInInspector]
	[SerializeField]
	private Camera thisCamera;

	private float shakeCounter;

	private Vector3 positionIntensity = Vector3.zero;

	private float rotationIntensity;

	private Vector3 shakeOffset = Vector3.zero;

	private static CameraShake _instance;

	public bool isShaking { get; set; }

	public bool UseUnscaledTime { get; set; }

	public static CameraShake instance => _instance;

	private void Awake()
	{
		_instance = this;
		if (thisCamera == null)
		{
			thisCamera = GetComponent<Camera>();
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	private void Reset()
	{
		thisCamera = GetComponent<Camera>();
	}

	private void LateUpdate()
	{
		if (positionIntensity.Equals(Vector3.zero) && rotationIntensity.Equals(0f))
		{
			return;
		}
		if (UseUnscaledTime)
		{
			shakeCounter -= Time.unscaledDeltaTime;
		}
		else
		{
			shakeCounter -= Time.deltaTime;
		}
		if (shakeCounter <= 0f)
		{
			isShaking = true;
			thisCamera.transform.localPosition = new Vector3(Random.Range(positionIntensity.x * 0.5f, positionIntensity.x) * (float)((!(Random.value <= 0.5f)) ? 1 : (-1)) + shakeOffset.x, Random.Range(positionIntensity.y * 0.5f, positionIntensity.y) * (float)((!(Random.value <= 0.5f)) ? 1 : (-1)) + shakeOffset.y, Random.Range(positionIntensity.z * 0.5f, positionIntensity.z) * (float)((!(Random.value <= 0.5f)) ? 1 : (-1)) + shakeOffset.z);
			if (rotationIntensity > 0f)
			{
			}
			positionIntensity *= shakeDecay;
			rotationIntensity *= shakeDecay;
			shakeOffset *= shakeDecay;
			if (positionIntensity.magnitude < 0.001f && rotationIntensity <= 0f)
			{
				shakeCounter = shakeDelay;
				positionIntensity = Vector3.zero;
				isShaking = false;
				thisCamera.transform.localPosition = Vector3.zero;
			}
			else
			{
				shakeCounter += shakeDelay;
			}
		}
	}

	public void SetPositionIntensity(Vector3 newIntensity)
	{
		isShaking = true;
		if (newIntensity.x < 0f)
		{
			newIntensity.x = 0f - newIntensity.x;
		}
		if (newIntensity.y < 0f)
		{
			newIntensity.y = 0f - newIntensity.y;
		}
		if (newIntensity.z < 0f)
		{
			newIntensity.z = 0f - newIntensity.z;
		}
		if (newIntensity.x > positionIntensity.x)
		{
			positionIntensity.x = newIntensity.x;
		}
		if (newIntensity.y > positionIntensity.y)
		{
			positionIntensity.y = newIntensity.y;
		}
		if (newIntensity.z > positionIntensity.z)
		{
			positionIntensity.z = newIntensity.z;
		}
		shakeDecay = 0.823f;
		shakeOffset = Vector3.zero;
		Debug.Log("Position Intensity Set");
	}

	public void SetPositionIntensity(float x, float y, float z)
	{
		SetPositionIntensity(new Vector3(x, y, z));
	}

	public void SetPositionIntensity(Vector3 newIntensity, float decay, Vector3 offset)
	{
		isShaking = true;
		if (newIntensity.x < 0f)
		{
			newIntensity.x = 0f - newIntensity.x;
		}
		if (newIntensity.y < 0f)
		{
			newIntensity.y = 0f - newIntensity.y;
		}
		if (newIntensity.z < 0f)
		{
			newIntensity.z = 0f - newIntensity.z;
		}
		if (newIntensity.x > positionIntensity.x)
		{
			positionIntensity.x = newIntensity.x;
		}
		if (newIntensity.y > positionIntensity.y)
		{
			positionIntensity.y = newIntensity.y;
		}
		if (newIntensity.z > positionIntensity.z)
		{
			positionIntensity.z = newIntensity.z;
		}
		shakeDecay = decay;
		shakeOffset = offset;
	}

	public void SetPositionIntensity(float x, float y, float z, float decay, Vector3 offset)
	{
		SetPositionIntensity(new Vector3(x, y, z), decay, offset);
	}

	public void SetRotationIntensity(float newIntensity)
	{
		isShaking = true;
		if (newIntensity > rotationIntensity)
		{
			rotationIntensity = newIntensity;
		}
	}
}
