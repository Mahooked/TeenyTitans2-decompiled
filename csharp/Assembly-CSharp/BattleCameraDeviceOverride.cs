using UnityEngine;

[ExecuteInEditMode]
public class BattleCameraDeviceOverride : MonoBehaviour
{
	public BattleCamera battleCamera;

	public Vector3 defaultFinalOffset;

	public Vector3 iPhoneXFinalOffset;

	private void Awake()
	{
		if (battleCamera == null)
		{
			battleCamera = GetComponent<BattleCamera>();
		}
	}

	private void Start()
	{
		UpdateOffset();
		battleCamera.UpdateCameraOffsetPosition(0f);
	}

	private void UpdateOffset()
	{
		if (!(battleCamera == null))
		{
			if (Screen.height == 2436 || Screen.height == 609)
			{
				battleCamera.finalOffsetPosition = iPhoneXFinalOffset;
			}
			else
			{
				battleCamera.finalOffsetPosition = defaultFinalOffset;
			}
		}
	}
}
