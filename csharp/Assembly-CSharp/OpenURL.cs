using UnityEngine;

public class OpenURL : MonoBehaviour
{
	public IsStaging staging;

	public void openURL(string url)
	{
		if (!staging.staging)
		{
			Application.OpenURL(url);
		}
		else
		{
			url = url.Replace("www", "staging");
			Application.OpenURL(url);
		}
		Debug.Log(url);
	}
}
