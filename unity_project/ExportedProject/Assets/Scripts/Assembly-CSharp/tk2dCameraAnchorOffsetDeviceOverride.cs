using System;
using UnityEngine;

public class tk2dCameraAnchorOffsetDeviceOverride : MonoBehaviour
{
	public enum MatchBy
	{
		Resolution = 0,
		AspectRatio = 1
	}

	[Serializable]
	public class OffsetOverride
	{
		public MatchBy matchBy;

		public Vector2 matchValue;

		public Vector2 offset;
	}

	public tk2dCameraAnchor cameraAnchor;

	public Vector2 offset;

	private string deviceModel;

	public Vector2 Offset
	{
		get
		{
			return (Screen.height != 2436 && Screen.height != 609 && Screen.height != 1792 && !(deviceModel == "iPhone11,8")) ? Vector2.zero : offset;
		}
	}

	private void Awake()
	{
		if (cameraAnchor == null)
		{
			cameraAnchor = GetComponent<tk2dCameraAnchor>();
		}
	}

	private void Start()
	{
		ForceOffsetUpdate();
		deviceModel = SystemInfo.deviceModel;
	}

	public void ForceOffsetUpdate()
	{
		cameraAnchor.AnchorOffsetPixels = Offset;
		cameraAnchor.ForceUpdateTransform();
	}
}
