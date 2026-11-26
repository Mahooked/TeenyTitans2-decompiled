using UnityEngine;

public class SplashImages : MonoBehaviour
{
	public tk2dSprite dcLogo;

	public tk2dSprite wbLogo;

	private void Awake()
	{
		dcLogo.gameObject.SetActive(false);
		wbLogo.gameObject.SetActive(false);
	}
}
