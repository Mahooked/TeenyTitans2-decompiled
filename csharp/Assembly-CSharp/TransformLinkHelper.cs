using UnityEngine;

public class TransformLinkHelper : MonoBehaviour
{
	[SerializeField]
	private bool trackPosition;

	[SerializeField]
	private bool trackRotation;

	[SerializeField]
	private bool trackScale;

	private Vector3 prevPosition;

	private Quaternion prevRotation;

	private Vector3 prevScale;

	public bool HasPositionChanged { get; private set; }

	public bool HasRotationChanged { get; private set; }

	public bool HasScaleChanged { get; private set; }

	private void Awake()
	{
		prevPosition = transform.position;
		prevRotation = transform.rotation;
		prevScale = transform.lossyScale;
	}

	private void LateUpdate()
	{
		if (trackPosition)
		{
			if (V3Equal(transform.position, prevPosition))
			{
				HasPositionChanged = false;
			}
			else
			{
				HasPositionChanged = true;
			}
		}
		if (trackRotation)
		{
			if (Quaternion.Angle(transform.rotation, prevRotation) > 0.1f)
			{
				HasRotationChanged = true;
			}
			else
			{
				HasRotationChanged = false;
			}
		}
		if (trackScale)
		{
			if (V3Equal(transform.lossyScale, prevScale))
			{
				HasScaleChanged = false;
			}
			else
			{
				HasScaleChanged = true;
			}
		}
		prevPosition = transform.position;
		prevRotation = transform.rotation;
		prevScale = transform.lossyScale;
	}

	private bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < 0.0001f;
	}
}
