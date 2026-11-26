using System;
using System.Collections;
using System.Net;
using System.Threading;
using Crosstales.Common.Util;
using Crosstales.OnlineCheck.Util;
using UnityEngine;

namespace Crosstales.OnlineCheck;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_online_check.html")]
public class OnlineCheck : MonoBehaviour
{
	public delegate void OnlineStatusChange(bool isConnected);

	public delegate void NetworkReachabilityChange(NetworkReachability networkReachability);

	public delegate void OnlineCheckComplete(bool isConnected, NetworkReachability networkReachability);

	[Header("General Settings")]
	[Tooltip("Continuously check for Internet availability within given intervals (default: true).")]
	public bool EndlessMode = true;

	[Tooltip("Minimum delay between checks in seconds (default: 4, range: 3 - 120).")]
	[Range(3f, 120f)]
	public int IntervalMin = 4;

	[Tooltip("Maximum delay between checks in seconds (default: 10, range: 4 - 120).")]
	[Range(4f, 120f)]
	public int IntervalMax = 10;

	[Header("Behaviour Settings")]
	[Tooltip("Start at runtime (default: true).")]
	public bool RunOnStart = true;

	[Tooltip("Optimized check routines (default: true).")]
	public bool Optimized = true;

	[Tooltip("Don't destroy gameobject during scene switches (default: true).")]
	public bool DontDestroy = true;

	private bool isFirsttime = true;

	private bool lastInternetAvailable;

	private NetworkReachability lastNetworkReachability;

	private bool isRunning;

	private float checkTime = 9999f;

	private float checkTimeCounter;

	private float burstTime = 9999f;

	private const float burstIntervalMin = 2f;

	private const float burstIntervalMax = 5f;

	private float burstTimeCounter;

	private static bool internetAvailable;

	private static NetworkReachability networkReachability;

	private static GameObject go;

	private static OnlineCheck instance;

	private static bool initalized;

	private static bool loggedOnlyOneInstance;

	private const string windowsUrl = "http://www.msftncsi.com/ncsi.txt";

	private const string appleUrl = "https://www.apple.com/library/test/success.html";

	private const string defaultUrl = "http://start.ubuntu.com/connectivity-check";

	private const string fallbackUrl = "https://crosstales.com/media/downloads/up.txt";

	private const string windowsText = "Microsoft NCSI";

	private const string appleText = "<TITLE>Success</TITLE>";

	private const string defaultText = "<TITLE>Lorem Ipsum</TITLE>";

	private const string fallbackText = "crosstales rulez!";

	private const int timeout = 1500;

	private static float lastCheckTime;

	private Thread worker;

	private static OnlineStatusChange _onOnlineStatusChange;

	private static NetworkReachabilityChange _onNetworkReachabilityChange;

	private static OnlineCheckComplete _onOnlineCheckComplete;

	public static int CheckIntervalMin
	{
		get
		{
			if (instance != null)
			{
				if (instance.IntervalMin > instance.IntervalMax)
				{
					return instance.IntervalMax - 1;
				}
				return instance.IntervalMin;
			}
			return 4;
		}
		set
		{
			if (instance != null)
			{
				int num = Mathf.Clamp(value, 3, 120);
				instance.IntervalMin = ((num < instance.IntervalMax) ? num : (instance.IntervalMax - 1));
			}
		}
	}

	public static int CheckIntervalMax
	{
		get
		{
			if (instance != null)
			{
				if (instance.IntervalMax < instance.IntervalMin)
				{
					return instance.IntervalMin + 1;
				}
				return instance.IntervalMax;
			}
			return 10;
		}
		set
		{
			if (instance != null)
			{
				int num = Mathf.Clamp(value, 4, 120);
				instance.IntervalMax = ((num > instance.IntervalMin) ? num : (instance.IntervalMin + 1));
			}
		}
	}

	public static bool isInternetAvailable
	{
		get
		{
			if (instance == null)
			{
				return Application.internetReachability != NetworkReachability.NotReachable;
			}
			return internetAvailable;
		}
	}

	public static NetworkReachability NetworkReachability
	{
		get
		{
			if (instance == null)
			{
				return Application.internetReachability;
			}
			return networkReachability;
		}
	}

	public static bool isOptimized
	{
		get
		{
			if (instance != null)
			{
				return instance.Optimized;
			}
			return true;
		}
		set
		{
			if (instance != null)
			{
				instance.Optimized = value;
			}
		}
	}

	public static bool isEndlessMode
	{
		get
		{
			if (instance != null)
			{
				return instance.EndlessMode;
			}
			return true;
		}
		set
		{
			if (instance != null)
			{
				instance.EndlessMode = value;
			}
		}
	}

	public static DateTime LastCheck { get; private set; }

	public static int DataDownloaded { get; private set; }

	public static event OnlineStatusChange OnOnlineStatusChange
	{
		add
		{
			_onOnlineStatusChange = (OnlineStatusChange)Delegate.Combine(_onOnlineStatusChange, value);
		}
		remove
		{
			_onOnlineStatusChange = (OnlineStatusChange)Delegate.Remove(_onOnlineStatusChange, value);
		}
	}

	public static event NetworkReachabilityChange OnNetworkReachabilityChange
	{
		add
		{
			_onNetworkReachabilityChange = (NetworkReachabilityChange)Delegate.Combine(_onNetworkReachabilityChange, value);
		}
		remove
		{
			_onNetworkReachabilityChange = (NetworkReachabilityChange)Delegate.Remove(_onNetworkReachabilityChange, value);
		}
	}

	public static event OnlineCheckComplete OnOnlineCheckComplete
	{
		add
		{
			_onOnlineCheckComplete = (OnlineCheckComplete)Delegate.Combine(_onOnlineCheckComplete, value);
		}
		remove
		{
			_onOnlineCheckComplete = (OnlineCheckComplete)Delegate.Remove(_onOnlineCheckComplete, value);
		}
	}

	public void OnEnable()
	{
		if (BaseHelper.isEditorMode || !initalized)
		{
			go = gameObject;
			go.name = "OnlineCheck";
			instance = this;
			internetAvailable = Application.internetReachability != NetworkReachability.NotReachable;
			if (!BaseHelper.isEditorMode && DontDestroy)
			{
				UnityEngine.Object.DontDestroyOnLoad(transform.root.gameObject);
				initalized = true;
			}
			networkReachability = (lastNetworkReachability = Application.internetReachability);
			if (Config.DEBUG)
			{
				Debug.LogWarning("Using new instance!");
			}
			return;
		}
		if (!BaseHelper.isEditorMode && DontDestroy && instance != this)
		{
			if (!loggedOnlyOneInstance)
			{
				Debug.LogWarning("Only one active instance of 'OnlineCheck' allowed in all scenes!" + Environment.NewLine + "This object will now be destroyed.");
				loggedOnlyOneInstance = true;
			}
			UnityEngine.Object.Destroy(gameObject, 0.2f);
		}
		if (Config.DEBUG)
		{
			Debug.LogWarning("Using old instance!");
		}
	}

	public void Start()
	{
		if ((RunOnStart || BaseHelper.isEditorMode) && !isRunning)
		{
			StartCoroutine(internetCheck());
		}
	}

	public void Update()
	{
		if (BaseHelper.isEditorMode)
		{
			if (go != null)
			{
				go.name = "OnlineCheck";
			}
		}
		else
		{
			Context.Runtime += Time.deltaTime;
			if (internetAvailable)
			{
				Context.Uptime += Time.deltaTime;
			}
		}
		if (!EndlessMode || isRunning)
		{
			return;
		}
		checkTimeCounter += Time.deltaTime;
		burstTimeCounter += Time.deltaTime;
		if (isInternetAvailable)
		{
			if (checkTimeCounter > checkTime)
			{
				if (BaseConstants.DEV_DEBUG)
				{
					Debug.Log("Normal-Mode!");
				}
				checkTimeCounter = 0f;
				burstTimeCounter = 0f;
				StartCoroutine(internetCheck());
			}
		}
		else if (burstTimeCounter > burstTime)
		{
			if (BaseConstants.DEV_DEBUG)
			{
				Debug.Log("Burst-Mode!");
			}
			checkTimeCounter = 0f;
			burstTimeCounter = 0f;
			StartCoroutine(internetCheck());
		}
	}

	public void OnApplicationQuit()
	{
		if (worker != null && worker.IsAlive)
		{
			if (BaseConstants.DEV_DEBUG)
			{
				Debug.Log("Kill worker!");
			}
			worker.Abort();
		}
		if (instance != null)
		{
			instance.StopAllCoroutines();
		}
	}

	public void OnValidate()
	{
		if (IntervalMin < 3)
		{
			IntervalMin = 3;
		}
		if (IntervalMin > 120)
		{
			IntervalMin = 120;
		}
		if (IntervalMax < 4)
		{
			IntervalMax = 4;
		}
		if (IntervalMax > 120)
		{
			IntervalMax = 120;
		}
		if (IntervalMin >= IntervalMax)
		{
			IntervalMin = IntervalMax - 1;
		}
	}

	public static void Refresh()
	{
		if (instance != null && !instance.isRunning && lastCheckTime + 1f < Time.realtimeSinceStartup)
		{
			instance.StartCoroutine(instance.internetCheck());
		}
	}

	private IEnumerator internetCheck()
	{
		if (!isRunning)
		{
			Context.NumberOfChecks++;
			isRunning = true;
			if (Config.DEBUG)
			{
				Debug.Log(string.Concat("[", DateTime.Now, "] OnlineCheck running..."));
			}
			bool available;
			if (BaseHelper.isWebPlatform || BaseHelper.isEditorMode)
			{
				available = Application.internetReachability != NetworkReachability.NotReachable;
			}
			else
			{
				ServicePointManager.ServerCertificateValidationCallback = BaseHelper.RemoteCertificateValidationCallback;
				available = false;
				if ((Optimized || BaseHelper.isWindowsBasedPlatform) && !BaseHelper.isAppleBasedPlatform)
				{
					if (BaseConstants.DEV_DEBUG)
					{
						Debug.LogWarning(string.Concat("[", DateTime.Now, "] Testing the Internet availability for Windows-based systems..."));
					}
					if (BaseHelper.isWSAPlatform)
					{
						WWW wWW;
						WWW www = (wWW = new WWW("http://www.msftncsi.com/ncsi.txt"));
						using (wWW)
						{
							do
							{
								yield return www;
							}
							while (!www.isDone);
							if (string.IsNullOrEmpty(www.error))
							{
								if (BaseConstants.DEV_DEBUG)
								{
									Debug.LogWarning("Content for Windows-based systems: " + www.text);
								}
								available = !string.IsNullOrEmpty(www.text) && www.text.CTEquals("Microsoft NCSI");
								DataDownloaded += www.bytesDownloaded;
							}
							else if (BaseConstants.DEV_DEBUG)
							{
								Debug.LogError("Error getting content for Windows-based systems: " + www.error);
							}
						}
					}
					else if (worker == null || !worker.IsAlive)
					{
						worker = new Thread(() =>
						{
							checkWindows(out available);
						});
						worker.Start();
						do
						{
							yield return null;
						}
						while (worker.IsAlive);
					}
				}
				if (BaseHelper.isAppleBasedPlatform)
				{
					if (BaseConstants.DEV_DEBUG)
					{
						Debug.LogWarning(string.Concat("[", DateTime.Now, "] Testing the Internet availability for Apple-based systems..."));
					}
					if (worker == null || !worker.IsAlive)
					{
						worker = new Thread(() =>
						{
							checkApple(out available);
						});
						worker.Start();
						do
						{
							yield return null;
						}
						while (worker.IsAlive);
					}
				}
				if (!available)
				{
					if (BaseConstants.DEV_DEBUG)
					{
						Debug.Log(string.Concat("[", DateTime.Now, "] Testing with default Internet availability"));
					}
					if (BaseHelper.isWSAPlatform)
					{
						WWW wWW;
						WWW www = (wWW = new WWW("http://start.ubuntu.com/connectivity-check"));
						using (wWW)
						{
							do
							{
								yield return www;
							}
							while (!www.isDone);
							if (string.IsNullOrEmpty(www.error))
							{
								if (Config.DEBUG)
								{
									Debug.Log("Content for default: " + www.text);
								}
								available = !string.IsNullOrEmpty(www.text) && www.text.CTContains("<TITLE>Lorem Ipsum</TITLE>");
								DataDownloaded += www.bytesDownloaded;
							}
							else if (BaseConstants.DEV_DEBUG)
							{
								Debug.LogError("Error getting content for default: " + www.error);
							}
						}
					}
					else if (worker == null || !worker.IsAlive)
					{
						worker = new Thread(() =>
						{
							checkDefault(out available);
						});
						worker.Start();
						do
						{
							yield return null;
						}
						while (worker.IsAlive);
					}
				}
				if (!available)
				{
					if (Config.DEBUG)
					{
						Debug.Log("Testing with fallback Internet availability: " + DateTime.Now);
					}
					if (BaseHelper.isWSAPlatform)
					{
						WWW wWW;
						WWW www = (wWW = new WWW("https://crosstales.com/media/downloads/up.txt"));
						using (wWW)
						{
							do
							{
								yield return www;
							}
							while (!www.isDone);
							if (string.IsNullOrEmpty(www.error))
							{
								if (BaseConstants.DEV_DEBUG)
								{
									Debug.Log("Content for fallback: " + www.text);
								}
								available = !string.IsNullOrEmpty(www.text) && www.text.CTEquals("crosstales rulez!");
								DataDownloaded += www.bytesDownloaded;
							}
							else if (BaseConstants.DEV_DEBUG)
							{
								Debug.LogError("Error getting content for fallback: " + www.error);
							}
						}
					}
					else if (worker == null || !worker.IsAlive)
					{
						worker = new Thread(() =>
						{
							checkFallback(out available);
						});
						worker.Start();
						do
						{
							yield return null;
						}
						while (worker.IsAlive);
					}
				}
			}
			internetAvailable = available;
			if (!internetAvailable || Application.internetReachability == NetworkReachability.NotReachable)
			{
				networkReachability = NetworkReachability.NotReachable;
			}
			else
			{
				networkReachability = Application.internetReachability;
			}
			onInternetCheckComplete(internetAvailable, networkReachability);
			if (isFirsttime || internetAvailable != lastInternetAvailable)
			{
				lastInternetAvailable = internetAvailable;
				onInternetStatusChange(internetAvailable);
			}
			if (isFirsttime || networkReachability != lastNetworkReachability)
			{
				lastNetworkReachability = networkReachability;
				onNetworkReachabilityChange(networkReachability);
			}
			if (Config.DEBUG)
			{
				if (internetAvailable)
				{
					Debug.Log(string.Concat("[", DateTime.Now, "] Internet access AVAILABLE!"));
				}
				else
				{
					Debug.LogWarning(string.Concat("[", DateTime.Now, "] Internet access UNAVAILABLE!"));
				}
			}
			checkTime = UnityEngine.Random.Range(CheckIntervalMin, CheckIntervalMax);
			burstTime = UnityEngine.Random.Range(2f, 5f);
			lastCheckTime = Time.realtimeSinceStartup;
			isFirsttime = false;
			isRunning = false;
		}
		else
		{
			Debug.LogError("OnlineCheck already running!");
		}
	}

	private void checkWindows(out bool available)
	{
		available = false;
		try
		{
			using WebClient webClient = new CTWebClientNotCached(1500);
			string text = webClient.DownloadString("http://www.msftncsi.com/ncsi.txt");
			available = !string.IsNullOrEmpty(text) && text.CTEquals("Microsoft NCSI");
			DataDownloaded += text.Length;
		}
		catch (Exception ex)
		{
			if (BaseConstants.DEV_DEBUG)
			{
				Debug.LogError("Error getting content for Windows-based systems: " + ex);
			}
		}
	}

	private void checkApple(out bool available)
	{
		available = false;
		try
		{
			using WebClient webClient = new CTWebClientNotCached(1500);
			string text = webClient.DownloadString("https://www.apple.com/library/test/success.html");
			available = !string.IsNullOrEmpty(text) && text.CTContains("<TITLE>Success</TITLE>");
			DataDownloaded += text.Length;
		}
		catch (Exception ex)
		{
			if (BaseConstants.DEV_DEBUG)
			{
				Debug.LogError("Error getting content for Apple-based systems: " + ex);
			}
		}
	}

	private void checkDefault(out bool available)
	{
		available = false;
		try
		{
			using WebClient webClient = new CTWebClientNotCached(1500);
			string text = webClient.DownloadString("http://start.ubuntu.com/connectivity-check");
			available = !string.IsNullOrEmpty(text) && text.CTContains("<TITLE>Lorem Ipsum</TITLE>");
			DataDownloaded += text.Length;
		}
		catch (Exception ex)
		{
			if (BaseConstants.DEV_DEBUG)
			{
				Debug.LogError("Error getting content for default: " + ex);
			}
		}
	}

	private void checkFallback(out bool available)
	{
		available = false;
		try
		{
			using WebClient webClient = new CTWebClientNotCached(1500);
			string text = webClient.DownloadString("https://crosstales.com/media/downloads/up.txt");
			available = !string.IsNullOrEmpty(text) && text.CTEquals("crosstales rulez!");
			DataDownloaded += text.Length;
		}
		catch (Exception ex)
		{
			if (BaseConstants.DEV_DEBUG)
			{
				Debug.LogError("Error getting content for fallback: " + ex);
			}
		}
	}

	private void onInternetStatusChange(bool isConnected)
	{
		if (Config.DEBUG)
		{
			Debug.Log("onInternetStatusChange: " + isConnected);
		}
		if (_onOnlineStatusChange != null)
		{
			_onOnlineStatusChange(isConnected);
		}
	}

	private void onNetworkReachabilityChange(NetworkReachability networkReachability)
	{
		if (Config.DEBUG)
		{
			Debug.Log("onNetworkReachabilityChange: " + networkReachability);
		}
		if (_onNetworkReachabilityChange != null)
		{
			_onNetworkReachabilityChange(networkReachability);
		}
	}

	private void onInternetCheckComplete(bool isConnected, NetworkReachability networkReachability)
	{
		LastCheck = DateTime.Now;
		if (Config.DEBUG)
		{
			Debug.Log("onInternetCheckComplete: " + isConnected.ToString() + " - " + networkReachability);
		}
		if (_onOnlineCheckComplete != null)
		{
			_onOnlineCheckComplete(isConnected, networkReachability);
		}
	}
}
