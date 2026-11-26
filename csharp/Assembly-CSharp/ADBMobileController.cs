using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.adobe.mobile;

public class ADBMobileController : MonoBehaviour
{
	public string appName;

	private bool setContextComplete;

	private const string ADOBE_SDK_VERSION = "4.10.0";

	private void Awake()
	{
		ADBMobile.SetContext();
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("appname", appName);
		dictionary.Add("sdkversion", "4.10.0:" + Application.version);
		ADBMobile.CollectLifecycleData(dictionary);
		StartCoroutine(DelayPauseCalls());
	}

	private void Start()
	{
	}

	private IEnumerator DelayPauseCalls()
	{
		yield return new WaitForSeconds(2f);
		setContextComplete = true;
	}

	private void OnApplicationPause(bool isPaused)
	{
		if (setContextComplete)
		{
			if (isPaused)
			{
				ADBMobile.PauseCollectingLifecycleData();
				return;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("appname", appName);
			dictionary.Add("sdkversion", "4.10.0:" + Application.version);
			ADBMobile.CollectLifecycleData(dictionary);
		}
	}
}
