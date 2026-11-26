using System.Collections;
using System.IO;
using UnityEngine;

public class WebView : MonoBehaviour
{
	public string url = "http://www.cartoonnetwork.com/legal/priv_tou.html#textBox_priv";

	private void Awake()
	{
		WebView[] array = Object.FindObjectsOfType<WebView>();
		if (array.Length > 1)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		WebViewObject.Instance.Init((bool loadSuc) =>
		{
			if (loadSuc)
			{
				Debug.Log("Web View Loaded");
			}
			else
			{
				Debug.Log("Web View Not Loaded");
			}
		});
	}

	private IEnumerator Start()
	{
		WWW www = new WWW(url);
		yield return www;
		if (string.IsNullOrEmpty(www.error))
		{
			WebViewObject.Instance.LoadURL(url);
		}
		else
		{
			url = url.Replace(url.Substring(0, url.LastIndexOf("/")), Application.streamingAssetsPath);
			string vars = url.Substring(url.LastIndexOf("#"));
			url = url.Replace(vars, string.Empty);
			www = new WWW(url);
			yield return www;
			if (string.IsNullOrEmpty(www.error))
			{
				url = Application.persistentDataPath + "/priv_tou.html";
				if (!File.Exists(url))
				{
					try
					{
						FileStream fileStream = File.Open(url, FileMode.Create);
						fileStream.Write(www.bytes, 0, www.bytes.Length);
						fileStream.Close();
					}
					catch
					{
					}
				}
				WebViewObject.Instance.LoadURL("file://" + url + vars);
			}
		}
		WebViewObject.Instance.SetVisibleWithMargins(0, (int)(80f * ((float)Screen.height / 700f)), 0, 0);
	}

	public void CloseWebView()
	{
		WebViewObject.Instance.SetInvisible();
		WebViewObject.Instance.LoadURL("about:blank");
		if (WebViewObject.Instance != null && WebViewObject.Instance.gameObject != null)
		{
			Object.Destroy(WebViewObject.Instance.gameObject);
		}
		Object.Destroy(base.gameObject);
	}
}
