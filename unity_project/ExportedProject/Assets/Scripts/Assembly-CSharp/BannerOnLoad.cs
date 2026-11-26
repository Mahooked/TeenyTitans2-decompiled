using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BannerOnLoad : MonoBehaviour
{
	public string NextScene;

	private void Start()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			SceneManager.LoadScene(NextScene);
		}
		else if (DFP.Instance.ShowBanner())
		{
			StartCoroutine(WaitFiveSecsForBanner());
			Debug.Log("Showing banner for 5 secs");
		}
		else
		{
			Debug.Log("Skipping banner");
			SceneManager.LoadScene(NextScene);
		}
	}

	public void StartWaiting()
	{
		StartCoroutine(WaitFiveSecsForBanner());
		Debug.Log("Waiting for 5 secs banner");
	}

	private void OnDisable()
	{
		DFP.Instance.HideBanner();
	}

	private IEnumerator WaitFiveSecsForBanner()
	{
		yield return new WaitForSeconds(5f);
		SceneManager.LoadScene(NextScene);
		Debug.Log("Banner done");
	}
}
