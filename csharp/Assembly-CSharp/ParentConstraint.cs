using UnityEngine;

public class ParentConstraint : MonoBehaviour
{
	public Transform parent;

	public bool constrainPosition;

	public Vector3 positionOffset;

	public bool constrainRotation;

	public Vector3 rotationOffset;

	public bool constrainScale;

	public Vector3 scaleOffset;

	private void LateUpdate()
	{
		if (!(parent == null))
		{
			if (constrainPosition)
			{
				transform.position = parent.position + positionOffset;
			}
			if (constrainRotation)
			{
				transform.rotation = parent.rotation;
				Vector3 eulerAngles = transform.localRotation.eulerAngles;
				eulerAngles += rotationOffset;
				transform.localRotation = Quaternion.Euler(eulerAngles);
			}
			if (constrainScale)
			{
				transform.localScale = parent.localScale + scaleOffset;
			}
		}
	}
}
