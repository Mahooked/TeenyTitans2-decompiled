using System;
using System.Collections.Generic;
using UnityEngine;

public class URLSchemeHandler : MonoBehaviour
{
	private Dictionary<string, string> queuedParameters;

	private string openURL;

	public static URLSchemeHandler Instance { get; private set; }

	public event Action<Dictionary<string, string>> OpenedWithURL;

	private void Awake()
	{
		Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		CheckAndroidLaunch();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			CheckAndroidLaunch();
		}
	}

	private void OnApplicationPause(bool isPaused)
	{
		if (isPaused)
		{
			openURL = string.Empty;
		}
	}

	public void OnOpenWithUrl(string url)
	{
		if (!string.IsNullOrEmpty(openURL))
		{
			Debug.LogError("[URLSchemeHandler::OnOpenWithURL] - App was already opened by a URL");
			return;
		}
		Debug.LogFormat("[URLSchemeHandler::OnOpenWithURL] - Opened by URL: {0}", url);
		openURL = url;
		queuedParameters = null;
		string text = ((!url.Contains("?")) ? string.Empty : url.Split('?')[1]);
		if (!string.IsNullOrEmpty(text))
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array = text.Split('&');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split('=');
				string key = WWW.UnEscapeURL(array2[0]);
				string value = WWW.UnEscapeURL(array2[1]);
				dictionary.Add(key, value);
			}
			int num = 1;
			foreach (string key2 in dictionary.Keys)
			{
				Debug.LogFormat("[URLSchemeHandler::OnOpenWithURL] - Parameter {0}:    {1} = {2}", num, key2, dictionary[key2]);
				num++;
			}
			if (OpenedWithURL != null)
			{
				OpenedWithURL(dictionary);
			}
			else
			{
				queuedParameters = dictionary;
			}
		}
		else
		{
			Debug.Log("[URLSchemeHandler::OnOpenWithURL] - There are no parameters in the URL");
			if (OpenedWithURL != null)
			{
				OpenedWithURL(new Dictionary<string, string>());
			}
			else
			{
				queuedParameters = new Dictionary<string, string>();
			}
		}
	}

	public void FlushQueuedParameters()
	{
		if (queuedParameters != null)
		{
			if (OpenedWithURL != null)
			{
				OpenedWithURL(queuedParameters);
			}
			queuedParameters = null;
		}
	}

	private void CheckAndroidLaunch()
	{
		Debug.Log("[URLSchemeHandler::CheckAndroidLaunch] - Checking Android Launch");
		string text = "cn43://?";
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getIntent", new object[0]);
		AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getExtras", new object[0]);
		if (androidJavaObject3 != null && androidJavaObject3.GetRawObject().ToInt32() != 0)
		{
			Debug.Log("[URLSchemeHandler::CheckAndroidLaunch] - Launch Intent HAS extras");
			AndroidJavaObject androidJavaObject4 = androidJavaObject3.Call<AndroidJavaObject>("keySet", new object[0]);
			AndroidJavaObject androidJavaObject5 = androidJavaObject4.Call<AndroidJavaObject>("iterator", new object[0]);
			while (androidJavaObject5.Call<bool>("hasNext", new object[0]))
			{
				string text2 = androidJavaObject5.Call<string>("next", new object[0]);
				string text3 = androidJavaObject3.Call<string>("getString", new object[1] { text2 });
				Debug.LogFormat("[URLSchemeHandler::CheckAndroidLaunch] - Key: {0}    Value: {1}", text2, text3);
				text += $"{WWW.EscapeURL(text2)}={WWW.EscapeURL(text3)}&";
				Debug.LogFormat("[URLSchemeHandler::CheckAndroidLaunch] - Current fakeURL: {0}", text);
				androidJavaObject2.Call("removeExtra", text2);
				Debug.LogFormat("[URLSchemeHandler::CheckAndroidLaunch] - Removed Extra \"{0}\" from intent", text2);
			}
			text = text.Substring(0, text.Length - 1);
			Debug.LogFormat("[URLSchemeHandler::CheckAndroidLaunch] - Final fakeURL: {0}", text);
		}
		OnOpenWithUrl(text);
	}
}
