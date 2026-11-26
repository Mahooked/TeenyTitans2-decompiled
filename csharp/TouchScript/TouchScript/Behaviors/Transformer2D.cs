using System;
using TouchScript.Gestures.Simple;
using UnityEngine;

namespace TouchScript.Behaviors;

[AddComponentMenu("TouchScript/Behaviors/Transformer2D")]
public class Transformer2D : MonoBehaviour
{
	public float Speed = 10f;

	public float PanMultiplier = 1f;

	public bool AllowChangingFromOutside = true;

	private Vector3 localPositionToGo;

	private Vector3 localScaleToGo;

	private Quaternion localRotationToGo;

	private Vector3 lastLocalPosition;

	private Vector3 lastLocalScale;

	private Quaternion lastLocalRotation;

	private void Awake()
	{
		setDefaults();
	}

	private void OnEnable()
	{
		if (GetComponent<SimplePanGesture>() != null)
		{
			GetComponent<SimplePanGesture>().Panned += panHandler;
		}
		if (GetComponent<SimpleScaleGesture>() != null)
		{
			GetComponent<SimpleScaleGesture>().Scaled += scaleHandler;
		}
		if (GetComponent<SimpleRotateGesture>() != null)
		{
			GetComponent<SimpleRotateGesture>().Rotated += rotateHandler;
		}
	}

	private void OnDisable()
	{
		if (GetComponent<SimplePanGesture>() != null)
		{
			GetComponent<SimplePanGesture>().Panned -= panHandler;
		}
		if (GetComponent<SimpleScaleGesture>() != null)
		{
			GetComponent<SimpleScaleGesture>().Scaled -= scaleHandler;
		}
		if (GetComponent<SimpleRotateGesture>() != null)
		{
			GetComponent<SimpleRotateGesture>().Rotated -= rotateHandler;
		}
	}

	private void Update()
	{
		float t = Speed * Time.deltaTime;
		if (AllowChangingFromOutside)
		{
			if (!Mathf.Approximately(transform.localPosition.x, lastLocalPosition.x))
			{
				localPositionToGo.x = transform.localPosition.x;
			}
			if (!Mathf.Approximately(transform.localPosition.y, lastLocalPosition.y))
			{
				localPositionToGo.y = transform.localPosition.y;
			}
			if (!Mathf.Approximately(transform.localPosition.z, lastLocalPosition.z))
			{
				localPositionToGo.z = transform.localPosition.z;
			}
		}
		transform.localPosition = (lastLocalPosition = Vector3.Lerp(transform.localPosition, localPositionToGo, t));
		if (AllowChangingFromOutside)
		{
			if (!Mathf.Approximately(transform.localScale.x, lastLocalScale.x))
			{
				localScaleToGo.x = transform.localScale.x;
			}
			if (!Mathf.Approximately(transform.localScale.y, lastLocalScale.y))
			{
				localScaleToGo.y = transform.localScale.y;
			}
			if (!Mathf.Approximately(transform.localScale.z, lastLocalScale.z))
			{
				localScaleToGo.z = transform.localScale.z;
			}
		}
		Vector3 vector = Vector3.Lerp(transform.localScale, localScaleToGo, t);
		if (vector != transform.localScale)
		{
			transform.localScale = (lastLocalScale = vector);
		}
		if (AllowChangingFromOutside && transform.localRotation != lastLocalRotation)
		{
			localRotationToGo = transform.localRotation;
		}
		transform.localRotation = (lastLocalRotation = Quaternion.Lerp(transform.localRotation, localRotationToGo, t));
	}

	private void setDefaults()
	{
		localPositionToGo = (lastLocalPosition = transform.localPosition);
		localRotationToGo = (lastLocalRotation = transform.localRotation);
		localScaleToGo = (lastLocalScale = transform.localScale);
	}

	private void panHandler(object sender, EventArgs e)
	{
		SimplePanGesture simplePanGesture = (SimplePanGesture)sender;
		localPositionToGo += simplePanGesture.LocalDeltaPosition * PanMultiplier;
	}

	private void rotateHandler(object sender, EventArgs e)
	{
		SimpleRotateGesture simpleRotateGesture = (SimpleRotateGesture)sender;
		if ((double)Math.Abs(simpleRotateGesture.DeltaRotation) > 0.01)
		{
			if (transform.parent == null)
			{
				localRotationToGo = Quaternion.AngleAxis(simpleRotateGesture.DeltaRotation, simpleRotateGesture.RotationAxis) * localRotationToGo;
			}
			else
			{
				localRotationToGo = Quaternion.AngleAxis(simpleRotateGesture.DeltaRotation, transform.parent.InverseTransformDirection(simpleRotateGesture.RotationAxis)) * localRotationToGo;
			}
		}
	}

	private void scaleHandler(object sender, EventArgs e)
	{
		SimpleScaleGesture simpleScaleGesture = (SimpleScaleGesture)sender;
		if ((double)Math.Abs(simpleScaleGesture.LocalDeltaScale - 1f) > 1E-05)
		{
			localScaleToGo *= simpleScaleGesture.LocalDeltaScale;
		}
	}
}
