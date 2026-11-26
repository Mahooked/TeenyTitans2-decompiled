using DG.Tweening;
using UnityEngine;

public class CitySandboxController : MonoBehaviour
{
	public CameraPinchZoomer pinchZoomer;

	private void Start()
	{
		pinchZoomer.HomeRotation = new Vector3(39f, 331f, 359f);
		DOTween.Complete(pinchZoomer.transform);
	}
}
