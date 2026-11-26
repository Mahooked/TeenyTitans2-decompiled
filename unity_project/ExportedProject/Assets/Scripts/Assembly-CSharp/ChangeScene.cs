using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeScene : MonoBehaviour
{
	public string TargetScene;

	public void Start()
	{
		Text componentInChildren = GetComponentInChildren<Text>();
		if (componentInChildren != null)
		{
			componentInChildren.text = TargetScene;
		}
	}

	public void SwitchScene()
	{
		SceneManager.LoadScene(TargetScene);
	}
}
