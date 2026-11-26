using System;
using UnityEngine;

public class WebViewObject : MonoBehaviour
{
	private static WebViewObject _instance;

	private Action<bool> callback;

	private AndroidJavaObject webView;

	public bool visibility;

	public static WebViewObject Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("WebViewObject");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				_instance = gameObject.AddComponent<WebViewObject>();
			}
			return _instance;
		}
	}

	public void Init(Action<bool> cb = null)
	{
		callback = cb;
		webView = new AndroidJavaObject("com.kayac.unitywebview.WebViewPlugin");
		webView.Call("Init", name);
	}

	public WebViewObjectMessage PollMessage()
	{
		if (webView == null)
		{
			return null;
		}
		string text = webView.Call<string>("pollWebViewMessage", new object[0]);
		return (text == null) ? null : new WebViewObjectMessage(text);
	}

	private void OnDestroy()
	{
		if (webView != null)
		{
			webView.Call("Destroy");
			_instance = null;
		}
	}

	public void SetVisibleWithMargins(int left, int top, int right, int bottom)
	{
		SetMargins(left, top, right, bottom);
		SetVisibility(v: true);
	}

	public void SetInvisible()
	{
		SetVisibility(v: false);
	}

	private void SetMargins(int left, int top, int right, int bottom)
	{
		if (webView != null)
		{
			webView.Call("SetMargins", left, top, right, bottom);
		}
	}

	private void SetVisibility(bool v)
	{
		if (webView != null)
		{
			visibility = v;
			webView.Call("SetVisibility", v);
		}
	}

	public void makeBlackWebViewBackground()
	{
	}

	public void LoadURL(string url)
	{
		if (webView != null)
		{
			webView.Call("LoadURL", url);
		}
	}

	public void EvaluateJS(string js)
	{
		if (webView != null)
		{
			webView.Call("LoadURL", "javascript:" + js);
		}
	}

	public void CallFromJS(string message)
	{
	}

	private void LoadComplete(string message)
	{
		if (callback != null)
		{
			callback(string.Equals(message, "1"));
		}
	}
}
