using TouchScript.Debugging;
using UnityEngine;

public class ModelViewerController : MonoBehaviour
{
	public TouchDebugger touchDebugger;

	public tk2dUIItem exitButton;

	private void Awake()
	{
		if (touchDebugger == null)
		{
			touchDebugger = Object.FindObjectOfType<TouchDebugger>();
		}
	}

	private void Start()
	{
		touchDebugger.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		exitButton.OnClick += HandleOnExitClick;
	}

	private void OnDisable()
	{
		exitButton.OnClick -= HandleOnExitClick;
	}

	private void HandleOnExitClick()
	{
		LoadingScreenCover.LoadScene(CityController.previousCityScene);
	}
}
