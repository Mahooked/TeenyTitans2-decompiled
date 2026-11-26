using UnityEngine;

public class DestroyOnTitleScreenActive : MonoBehaviour
{
	private void Update()
	{
		if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			Object.Destroy(gameObject);
		}
	}
}
