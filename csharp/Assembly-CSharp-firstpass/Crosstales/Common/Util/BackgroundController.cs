using UnityEngine;

namespace Crosstales.Common.Util;

public class BackgroundController : MonoBehaviour
{
	[Tooltip("Selected objects for the controller.")]
	public GameObject[] Objects;

	private bool isFocused;

	public void Start()
	{
		isFocused = Application.isFocused;
	}

	public void FixedUpdate()
	{
		if (Application.isFocused == isFocused)
		{
			return;
		}
		isFocused = Application.isFocused;
		if ((!BaseHelper.isAndroidPlatform && !BaseHelper.isIOSPlatform) || TouchScreenKeyboard.visible)
		{
			return;
		}
		GameObject[] objects = Objects;
		foreach (GameObject gameObject in objects)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(isFocused);
			}
		}
		Debug.Log("Application.isFocused: " + isFocused);
	}
}
