using System;
using UnityEngine;

[ExecuteInEditMode]
public class ResizeToScreen : MonoBehaviour
{
	public Camera targetCamera;

	public float widthFactor = 0.0013f;

	public float heightFactor = 0.0013f;

	public float widthFactor16_9;

	public float heightFactor16_9;

	[SerializeField]
	private bool flipXScale;

	private void OnEnable()
	{
		if (targetCamera == null)
		{
			targetCamera = CameraUtility.GetCameraForLayer(LayerMask.GetMask(LayerMask.LayerToName(base.gameObject.layer)));
		}
	}

	private void Update()
	{
		if (!(targetCamera == null))
		{
			float num = 2f * Mathf.Tan(0.5f * targetCamera.fieldOfView * ((float)Math.PI / 180f)) * Vector3.Distance(base.transform.position, targetCamera.transform.position);
			float num2 = num * (float)Screen.width / (float)Screen.height;
			int num3 = ((!flipXScale) ? 1 : (-1));
			float num4 = widthFactor;
			float num5 = heightFactor;
			if (targetCamera.aspect < 0.75f || targetCamera.aspect > 1.33f)
			{
				num4 = ((!(widthFactor16_9 > 0.0001f)) ? widthFactor : widthFactor16_9);
				num5 = ((!(heightFactor16_9 > 0.0001f)) ? heightFactor : heightFactor16_9);
			}
			base.transform.localScale = new Vector3(num2 * num4 * (float)num3, num * num5, 1f);
		}
	}

	public void SetXDirection(int direction)
	{
		flipXScale = Mathf.Sign(direction) == -1f;
	}
}
