using UnityEngine;
using UnityEngine.SceneManagement;

public class IAPTestInit : MonoBehaviour
{
	private void Start()
	{
		Application.targetFrameRate = 60;
		tk2dSystem.CurrentPlatform = InitController.GetScale() + "x";
		SceneManager.LoadScene("RiddlerShop");
	}
}
