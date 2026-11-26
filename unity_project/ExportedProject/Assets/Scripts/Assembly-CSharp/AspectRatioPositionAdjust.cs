using UnityEngine;

public class AspectRatioPositionAdjust : MonoBehaviour
{
	[Tooltip("The object to adjust. If this is empty, this gameobject will be chosen to adjust")]
	public Transform transformToAdjust;

	public Vector3 localPosition4x3;

	public Vector3 localPosition16x9;

	private Vector3 previouslocalPosition4x3;

	private Vector3 previouslocalPosition16x9;

	private void Start()
	{
		if (transformToAdjust == null)
		{
			transformToAdjust = base.transform;
		}
		UpdatePosition(true);
	}

	public void UpdatePosition(bool force = false)
	{
		if (force || previouslocalPosition4x3 != localPosition4x3 || previouslocalPosition16x9 != localPosition16x9)
		{
			transformToAdjust.localPosition = ((!((float)Screen.height / (float)Screen.width > 1.34f)) ? localPosition4x3 : localPosition16x9);
			previouslocalPosition4x3 = localPosition4x3;
			previouslocalPosition16x9 = localPosition16x9;
		}
	}
}
