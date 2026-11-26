using UnityEngine;

public class CharacterSetupController : MonoBehaviour
{
	public Camera mainCamera;

	private void Awake()
	{
		mainCamera.transform.position += new Vector3(0f, 0f, -4.8f);
	}
}
