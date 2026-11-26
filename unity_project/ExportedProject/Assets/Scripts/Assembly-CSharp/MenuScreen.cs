using UnityEngine;

public class MenuScreen : MonoBehaviour
{
	public GameObject[] menuItems;

	public void SetScreenActive(bool isActive)
	{
		for (int i = 0; i < menuItems.Length; i++)
		{
			menuItems[i].SetActive(isActive);
		}
	}
}
