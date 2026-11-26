using UnityEngine;

public class ComScoreController : MonoBehaviour
{
	public string appName;

	private static AndroidJavaClass comScore;

	private static AndroidJavaObject unityActivity;

	private bool init;

	private const string TURNER_C2 = "6035748";

	private const string TURNER_PUBLISH_SECRET = "6bba25a9ff38cd173c1c93842c768e28";

	private void Start()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		unityActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		comScore = new AndroidJavaClass("com.comscore.analytics.comScore");
		comScore.CallStatic("setAppContext", unityActivity);
		comScore.CallStatic("setAppName", appName);
		comScore.CallStatic("setCustomerC2", "6035748");
		comScore.CallStatic("setPublisherSecret", "6bba25a9ff38cd173c1c93842c768e28");
		comScore.CallStatic("onEnterForeground");
		init = true;
	}

	private void OnApplicationPause(bool pause)
	{
		if (init)
		{
			if (pause)
			{
				comScore.CallStatic("onExitForeground");
			}
			else
			{
				comScore.CallStatic("onEnterForeground");
			}
		}
	}
}
