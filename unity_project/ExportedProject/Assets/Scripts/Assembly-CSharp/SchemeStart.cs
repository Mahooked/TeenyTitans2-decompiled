using System;
using System.Collections.Generic;
using UnityEngine;

public class SchemeStart : MonoBehaviour
{
	public List<string> installed = new List<string>();

	public List<string> notInstalled = new List<string>();

	private void Start()
	{
	}

	public void BeginTest()
	{
		string text = Resources.Load<TextAsset>("schemeList").text;
		string[] array = text.Split("\n"[0]);
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			if (text2 != string.Empty && text2 != " ")
			{
				string text3 = text2 + "://";
			}
		}
		List<string> packages = DFPManager.Instance.Config.GetPackages();
		foreach (string item in packages)
		{
			if (item != string.Empty && item != " ")
			{
				IsAppInstalled(item);
				Debug.Log(item + " bundle-tested");
			}
		}
		foreach (string item2 in installed)
		{
			Debug.Log(item2 + " installed");
		}
		foreach (string item3 in notInstalled)
		{
			Debug.Log(item3 + " not installed");
		}
	}

	public void openApp(string url)
	{
		Application.OpenURL(url);
	}

	public void testURL(string url)
	{
	}

	public bool IsAppInstalled(string bundleID)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getPackageManager", new object[0]);
		Debug.Log(" ********LaunchOtherApp ");
		AndroidJavaObject androidJavaObject3 = null;
		try
		{
			androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getLaunchIntentForPackage", new object[1] { bundleID });
		}
		catch (Exception ex)
		{
			Debug.Log("exception" + ex.Message);
		}
		if (androidJavaObject3 == null)
		{
			Debug.Log(bundleID + " dont got it");
			return false;
		}
		Debug.Log(bundleID + " got it");
		return true;
	}

	public void getPackage(string tf)
	{
		Debug.Log("Has package " + tf);
	}
}
