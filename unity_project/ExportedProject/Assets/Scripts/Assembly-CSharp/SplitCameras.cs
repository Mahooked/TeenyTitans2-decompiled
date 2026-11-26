using DG.Tweening;
using UnityEngine;

public class SplitCameras : MonoBehaviour
{
	public Camera mainCamera;

	public Camera secondaryCamera;

	public BattleCamera player1Camera;

	public BattleCamera player2Camera;

	public Transform mainCameraMaskHolder;

	public Transform secondaryCameraMaskHolder;

	public Transform dividerHolder;

	public GameObject mainCameraMask;

	public GameObject secondaryCameraMask;

	public MeshRenderer dividerMesh;

	public float dividerFadeTime = 0.15f;

	[Tooltip("The minimum distance required between the two cameras for a screen split to occur")]
	public float splitDistanceThreshold = 1f;

	[Tooltip("The minimum angle difference required between the two cameras for a screen split to occur")]
	public float splitAngleThreshold = 1f;

	private bool mainIsMain = true;

	[SerializeField]
	[HideInInspector]
	private float _angle;

	[SerializeField]
	[HideInInspector]
	private Vector3 _offset;

	[SerializeField]
	[HideInInspector]
	private float _splitScale = 1f;

	public bool isSplit { get; private set; }

	public bool isLocked { get; set; }

	public Camera activeMainCamera
	{
		get
		{
			return (!mainIsMain) ? secondaryCamera : mainCamera;
		}
	}

	public float angle
	{
		get
		{
			return _angle;
		}
		set
		{
			if (value != _angle)
			{
				_angle = value;
				UpdateRotation();
			}
		}
	}

	public Vector3 offset
	{
		get
		{
			return _offset;
		}
		set
		{
			if (!(value == _offset))
			{
				_offset = value;
				UpdatePosition();
			}
		}
	}

	public float splitScale
	{
		get
		{
			return _splitScale;
		}
		set
		{
			if (value != _splitScale)
			{
				_splitScale = value;
				UpdateScale();
			}
		}
	}

	private void Awake()
	{
		if (splitDistanceThreshold < 0f)
		{
			splitDistanceThreshold = 0f;
		}
		UpdateRotation();
	}

	private void Update()
	{
		if (!isLocked)
		{
			if (isSplit && mainCamera.transform.position == secondaryCamera.transform.position && mainCamera.transform.localRotation == secondaryCamera.transform.localRotation)
			{
				player2Camera.EnableCameras(false);
				mainCameraMask.SetActive(false);
				dividerMesh.material.DOFade(0f, dividerFadeTime);
				isSplit = false;
			}
			else if (!isSplit && (mainCamera.transform.position != secondaryCamera.transform.position || mainCamera.transform.localRotation != secondaryCamera.transform.localRotation))
			{
				mainCameraMask.SetActive(true);
				player2Camera.EnableCameras();
				dividerMesh.material.DOFade(1f, dividerFadeTime);
				isSplit = true;
			}
		}
	}

	public void SetMainCamera(Camera camera)
	{
		if ((!(camera == mainCamera) || !mainIsMain) && (!(camera == secondaryCamera) || mainIsMain) && (!(camera != mainCamera) || !(camera != secondaryCamera)))
		{
			mainIsMain = !mainIsMain;
		}
	}

	public void InstantMerge(bool doLock)
	{
		if (isSplit)
		{
			if (mainIsMain)
			{
				player2Camera.EnableCameras(false);
				mainCameraMask.SetActive(false);
			}
			else
			{
				player1Camera.EnableCameras(false);
				secondaryCameraMask.SetActive(false);
			}
			dividerMesh.material.DOFade(0f, dividerFadeTime);
			isSplit = false;
			if (doLock)
			{
				isLocked = true;
			}
		}
	}

	public void InstantSplit()
	{
		if (!isSplit)
		{
			if (mainIsMain)
			{
				mainCameraMask.SetActive(true);
				player2Camera.EnableCameras();
			}
			else
			{
				secondaryCameraMask.SetActive(true);
				player1Camera.EnableCameras();
			}
			dividerMesh.material.DOFade(1f, dividerFadeTime);
			isSplit = true;
		}
	}

	private void UpdateRotation()
	{
		Vector3 eulerAngles = mainCameraMaskHolder.localRotation.eulerAngles;
		eulerAngles.z = _angle;
		mainCameraMaskHolder.localRotation = Quaternion.Euler(eulerAngles);
		eulerAngles = secondaryCameraMaskHolder.localRotation.eulerAngles;
		eulerAngles.z = _angle - 180f;
		secondaryCameraMaskHolder.localRotation = Quaternion.Euler(eulerAngles);
		eulerAngles = dividerHolder.localRotation.eulerAngles;
		eulerAngles.z = _angle;
		dividerHolder.localRotation = Quaternion.Euler(eulerAngles);
	}

	private void UpdatePosition()
	{
		Vector3 localPosition = mainCameraMaskHolder.localPosition;
		localPosition.x = _offset.x;
		localPosition.y = _offset.y;
		mainCameraMaskHolder.localPosition = localPosition;
		localPosition = secondaryCameraMaskHolder.localPosition;
		localPosition.x = _offset.x;
		localPosition.y = _offset.y;
		secondaryCameraMaskHolder.localPosition = localPosition;
		localPosition = dividerHolder.localPosition;
		localPosition.x = _offset.x;
		localPosition.y = _offset.y;
		dividerHolder.localPosition = localPosition;
	}

	private void UpdateScale()
	{
		Vector3 localScale = mainCameraMaskHolder.localScale;
		localScale.x = _splitScale;
		localScale.y = _splitScale;
		mainCameraMaskHolder.localScale = localScale;
		secondaryCameraMaskHolder.localScale = localScale;
		dividerHolder.localScale = localScale;
	}
}
