using UnityEngine;

public class Examples : MonoBehaviour
{
	private static Examples instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		if (Application.loadedLevelName == "_Examples" && Application.levelCount > 1)
		{
			loadNextLevel();
			Object.Destroy(GetComponent<Example>());
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width - 120, 20f, 100f, 30f), "Next >>"))
		{
			loadNextLevel();
		}
	}

	private void loadNextLevel()
	{
		Application.LoadLevel((Application.loadedLevel + 1) % Application.levelCount);
	}
}
