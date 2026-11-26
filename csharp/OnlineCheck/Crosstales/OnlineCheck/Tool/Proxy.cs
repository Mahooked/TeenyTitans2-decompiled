using System;
using Crosstales.Common.Util;
using UnityEngine;

namespace Crosstales.OnlineCheck.Tool;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_tool_1_1_proxy.html")]
public class Proxy : MonoBehaviour
{
	[Header("HTTP Proxy Settings")]
	[Tooltip("URL (without protocol) or IP of the proxy server.")]
	public string HTTPProxyURL;

	[Tooltip("Port of the proxy server.")]
	[Range(0f, 65535f)]
	public int HTTPProxyPort;

	[Tooltip("Username for the proxy server (optional).")]
	public string HTTPProxyUsername = string.Empty;

	[Tooltip("Password for the proxy server (optional).")]
	public string HTTPProxyPassword = string.Empty;

	[Tooltip("Protocol (e.g. 'http://') for the proxy server (optional).")]
	public string HTTPProxyURLProtocol = string.Empty;

	[Header("HTTPS Proxy Settings")]
	[Tooltip("URL (without protocol) or IP of the proxy server.")]
	public string HTTPSProxyURL;

	[Tooltip("Port of the proxy server.")]
	[Range(0f, 65535f)]
	public int HTTPSProxyPort;

	[Tooltip("Username for the proxy server (optional).")]
	public string HTTPSProxyUsername = string.Empty;

	[Tooltip("Password for the proxy server (optional).")]
	public string HTTPSProxyPassword = string.Empty;

	[Tooltip("Protocol (e.g. 'http://') for the proxy server (optional).")]
	public string HTTPSProxyURLProtocol = string.Empty;

	[Header("Startup behaviour")]
	[Tooltip("Enable the proxy on awake (default: false).")]
	public bool EnableOnAwake;

	private const string HTTPProxyEnvVar = "HTTP_PROXY";

	private const string HTTPSProxyEnvVar = "HTTPS_PROXY";

	private static bool httpProxy;

	private static bool httpsProxy;

	public static bool hasHTTPProxy => httpProxy;

	public static bool hasHTTPSProxy => httpsProxy;

	public void Awake()
	{
		if (EnableOnAwake)
		{
			EnableHTTPProxy();
			EnableHTTPSProxy();
		}
	}

	public void Update()
	{
		if (BaseHelper.isEditorMode)
		{
			name = "Proxy";
		}
	}

	public void EnableHTTPProxy(bool enabled = true)
	{
		if (enabled)
		{
			EnableHTTPProxy(HTTPProxyURL, HTTPProxyPort, HTTPProxyUsername, HTTPProxyPassword, HTTPProxyURLProtocol);
		}
		else
		{
			DisableHTTPProxy();
		}
	}

	public void EnableHTTPSProxy(bool enabled = true)
	{
		if (enabled)
		{
			EnableHTTPSProxy(HTTPSProxyURL, HTTPSProxyPort, HTTPSProxyUsername, HTTPSProxyPassword, HTTPSProxyURLProtocol);
		}
		else
		{
			DisableHTTPSProxy();
		}
	}

	public void EnableHTTPProxy(string url, int port, string username = "", string password = "", string urlProtocol = "")
	{
		if (string.IsNullOrEmpty(url))
		{
			Debug.LogError("HTTP proxy url cannot be null or empty!");
			return;
		}
		if (!validPort(port))
		{
			Debug.LogError("HTTP proxy port must be valid (between 0 - 65535): " + port);
			return;
		}
		if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
		{
			Environment.SetEnvironmentVariable("HTTP_PROXY", urlProtocol + username + ":" + password + "@" + url + ":" + port);
		}
		else
		{
			Environment.SetEnvironmentVariable("HTTP_PROXY", urlProtocol + url + ":" + port);
		}
		httpProxy = true;
	}

	public void EnableHTTPSProxy(string url, int port, string username = "", string password = "", string urlProtocol = "")
	{
		if (string.IsNullOrEmpty(url))
		{
			Debug.LogError("HTTPS proxy url cannot be null or empty!");
			return;
		}
		if (!validPort(port))
		{
			Debug.LogError("HTTPS proxy port must be valid (between 0 - 65535): " + port);
			return;
		}
		if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
		{
			Environment.SetEnvironmentVariable("HTTPS_PROXY", urlProtocol + username + ":" + password + "@" + url + ":" + port);
		}
		else
		{
			Environment.SetEnvironmentVariable("HTTPS_PROXY", urlProtocol + url + ":" + port);
		}
		httpsProxy = true;
	}

	public void DisableHTTPProxy()
	{
		Environment.SetEnvironmentVariable("HTTP_PROXY", string.Empty);
		httpProxy = false;
	}

	public void DisableHTTPSProxy()
	{
		Environment.SetEnvironmentVariable("HTTPS_PROXY", string.Empty);
		httpsProxy = false;
	}

	private static bool validPort(int port)
	{
		if (port >= 0)
		{
			return port < 65536;
		}
		return false;
	}
}
