using System;
using TouchScript;
using TouchScript.Gestures;
using TouchScript.Gestures.Simple;
using UnityEngine;

public class ModelViewer : MonoBehaviour
{
	public float xSpeed = 200f;

	public float ySpeed = 200f;

	public float rotationDampening = 5f;

	public bool autoRotate;

	public float autoRotateSpeedX = 10f;

	public float autoRotateSpeedY;

	public bool useHitArea;

	public tk2dUIItem hitArea;

	public Vector3[] snappingRotations;

	public ModelViewerCamera viewerCamera;

	public TapGesture tapGesture;

	public SimpleRotateGesture rotateGesture;

	public bool rotateGestureEnabled = true;

	public bool doubleTapToReset = true;

	private Quaternion initialRotation;

	private bool isDown;

	private Vector2 prevScreenPosition;

	private Vector2 curScreenPosition;

	private Quaternion desiredRotation;

	private Quaternion rotateBy = Quaternion.identity;

	private bool resetRotation;

	private Quaternion curRotate;

	private Quaternion prevRotate;

	private float rotateDelta;

	private void Awake()
	{
		if (snappingRotations == null)
		{
			snappingRotations = new Vector3[0];
		}
		if (viewerCamera == null)
		{
			viewerCamera = UnityEngine.Object.FindObjectOfType<ModelViewerCamera>();
		}
		initialRotation = base.transform.rotation;
		if (hitArea != null)
		{
			hitArea.OnDown += HandleOnDown;
		}
		tapGesture.Tapped += HandleTapped;
		rotateGesture.RotateStarted += HandleRotateStarted;
		rotateGesture.Rotated += HandleRotated;
		rotateGesture.RotateCompleted += HandleRotateCompleted;
	}

	private void HandleOnDown()
	{
		if (useHitArea)
		{
			isDown = true;
			resetRotation = false;
			prevScreenPosition = (curScreenPosition = Input.mousePosition);
		}
	}

	private void HandleTapped(object sender, EventArgs e)
	{
		if (doubleTapToReset)
		{
			desiredRotation = initialRotation;
			rotateBy = Quaternion.identity;
			resetRotation = true;
			viewerCamera.Reset();
		}
	}

	private void HandleRotateStarted(object sender, EventArgs e)
	{
		if (rotateGestureEnabled)
		{
			rotateDelta = 0f;
		}
	}

	private void HandleRotated(object sender, EventArgs e)
	{
		if (rotateGestureEnabled)
		{
			ITouch touch = rotateGesture.ActiveTouches[0];
			ITouch touch2 = rotateGesture.ActiveTouches[1];
			float num = (touch.PreviousPosition.y - touch2.PreviousPosition.y) / (touch.PreviousPosition.x - touch2.PreviousPosition.x);
			float num2 = (touch.Position.y - touch2.Position.y) / (touch.Position.x - touch2.Position.x);
			float num3 = Mathf.Atan((num - num2) / (1f + num * num2));
			if (float.IsNaN(num3))
			{
				num3 = 0f;
			}
			rotateDelta = num3 * 57.29578f;
		}
	}

	private void HandleRotateCompleted(object sender, EventArgs e)
	{
		if (rotateGestureEnabled)
		{
			rotateDelta = 0f;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			desiredRotation = initialRotation;
			rotateBy = Quaternion.identity;
			resetRotation = true;
			viewerCamera.Reset();
		}
		if (LODRequestFlag.isInviteScreenActive)
		{
			return;
		}
		if ((Application.isEditor && Input.GetKey(KeyCode.LeftControl)) || (Input.touchSupported && Input.touchCount > 1))
		{
			Vector3 direction = viewerCamera.transform.TransformDirection(Vector3.forward);
			Vector3 axis = base.transform.InverseTransformDirection(direction);
			rotateBy = Quaternion.AngleAxis(0f - rotateDelta, axis);
			base.transform.rotation = base.transform.rotation * rotateBy;
			rotateDelta = 0f;
			rotateBy = Quaternion.identity;
			isDown = false;
			return;
		}
		if ((!Input.touchSupported || Input.touchCount == 1) && !useHitArea && Input.GetMouseButtonDown(0))
		{
			isDown = true;
			resetRotation = false;
			prevScreenPosition = (curScreenPosition = Input.mousePosition);
		}
		else if (isDown && Input.GetMouseButton(0) && (!Input.touchSupported || Input.touchCount == 1))
		{
			curScreenPosition = Input.mousePosition;
			Vector2 vector = prevScreenPosition - curScreenPosition;
			vector.x /= Screen.dpi;
			vector.y /= Screen.dpi;
			Vector3 direction2 = viewerCamera.transform.TransformDirection(Vector3.up);
			Vector3 direction3 = viewerCamera.transform.TransformDirection(Vector3.right);
			Vector3 axis2 = base.transform.InverseTransformDirection(direction2);
			Vector3 axis3 = base.transform.InverseTransformDirection(direction3);
			rotateBy = Quaternion.AngleAxis(vector.x * xSpeed, axis2) * Quaternion.AngleAxis((0f - vector.y) * ySpeed, axis3);
			prevScreenPosition = curScreenPosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			isDown = false;
			if (snappingRotations.Length > 0)
			{
				resetRotation = true;
				rotateBy = Quaternion.identity;
				desiredRotation = Quaternion.Euler(snappingRotations[0]);
				float num = Quaternion.Angle(base.transform.rotation, Quaternion.Euler(snappingRotations[0]));
				int num2 = snappingRotations.Length;
				for (int i = 1; i < num2; i++)
				{
					float num3 = Quaternion.Angle(base.transform.rotation, Quaternion.Euler(snappingRotations[i]));
					if (num3 < num)
					{
						num = num3;
						desiredRotation = Quaternion.Euler(snappingRotations[i]);
					}
				}
			}
		}
		if (resetRotation)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, desiredRotation, Time.deltaTime * rotationDampening);
			if (base.transform.rotation == desiredRotation)
			{
				base.transform.rotation = desiredRotation;
				resetRotation = false;
			}
		}
		else if (rotateBy != Quaternion.identity)
		{
			base.transform.rotation = base.transform.rotation * rotateBy;
			rotateBy = Quaternion.Lerp(rotateBy, Quaternion.identity, Time.deltaTime * rotationDampening);
		}
		else if (autoRotate && !isDown)
		{
			Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
			eulerAngles.x += autoRotateSpeedX * Time.deltaTime;
			eulerAngles.y += autoRotateSpeedY * Time.deltaTime;
			base.transform.localRotation = Quaternion.Euler(eulerAngles);
		}
	}

	public void ResetRotation()
	{
		base.transform.rotation = initialRotation;
		rotateBy = Quaternion.identity;
	}
}
