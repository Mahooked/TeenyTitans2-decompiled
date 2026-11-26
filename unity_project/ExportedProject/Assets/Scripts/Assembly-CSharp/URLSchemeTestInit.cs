using UnityEngine;
using UnityEngine.SceneManagement;

public class URLSchemeTestInit : MonoBehaviour
{
	private void Start()
	{
		tk2dSystem.CurrentPlatform = InitController.GetScale() + "x";
		SceneManager.LoadScene("URLSchemeTest");
	}
}
