using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TTGDFPBanner : MonoBehaviour
{
	[SerializeField]
	private Image blackCover;

	[SerializeField]
	private float fadeTime = 0.25f;

	[SerializeField]
	private float bannerWaitTime = 3f;

	public IEnumerator Show()
	{
		yield return blackCover.DOFade(0f, fadeTime).WaitForCompletion();
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			if (DFP.Instance.ShowBanner())
			{
				Debug.LogFormat("Showing banner for {0} secs", bannerWaitTime);
				yield return new WaitForSeconds(bannerWaitTime);
				Debug.Log("Banner Done");
			}
			else
			{
				Debug.Log("Skipping banner");
			}
		}
		else
		{
			Debug.Log("Skipping banner due to no internet");
		}
		yield return blackCover.DOFade(1f, fadeTime).WaitForCompletion();
		DFP.Instance.HideBanner();
	}
}
