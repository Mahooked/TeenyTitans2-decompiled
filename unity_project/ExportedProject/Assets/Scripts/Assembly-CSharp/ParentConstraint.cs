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
				base.transform.position = parent.position + positionOffset;
			}
			if (constrainRotation)
			{
				base.transform.rotation = parent.rotation;
				Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
				eulerAngles += rotationOffset;
				base.transform.localRotation = Quaternion.Euler(eulerAngles);
			}
			if (constrainScale)
			{
				base.transform.localScale = parent.localScale + scaleOffset;
			}
		}
	}
}
