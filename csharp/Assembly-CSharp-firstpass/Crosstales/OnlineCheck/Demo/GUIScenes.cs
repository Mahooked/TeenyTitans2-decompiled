using UnityEngine;
using UnityEngine.SceneManagement;

namespace Crosstales.OnlineCheck.Demo;

[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_demo_1_1_g_u_i_scenes.html")]
public class GUIScenes : MonoBehaviour
{
	[Tooltip("Name of the previous scene.")]
	public string PreviousScene;

	[Tooltip("Name of the next scene.")]
	public string NextScene;

	public void LoadPreviousScene()
	{
		SceneManager.LoadScene(PreviousScene);
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(NextScene);
	}

	public void OpenAssetURL()
	{
		Application.OpenURL("https://goo.gl/qwtXyb");
	}

	public void OpenCTURL()
	{
		Application.OpenURL("https://www.crosstales.com");
	}

	public void Quit()
	{
		if (!Application.isEditor)
		{
			Application.Quit();
		}
	}
}
