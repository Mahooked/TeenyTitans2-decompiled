using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.adobe.mobile;

public class ADBMobile
{
	public enum ADBPrivacyStatus
	{
		MOBILE_PRIVACY_STATUS_OPT_IN = 1,
		MOBILE_PRIVACY_STATUS_OPT_OUT,
		MOBILE_PRIVACY_STATUS_UNKNOWN
	}

	public enum ADBBeaconProximity
	{
		PROXIMITY_UNKNOWN,
		PROXIMITY_IMMEDIATE,
		PROXIMITY_NEAR,
		PROXIMITY_FAR
	}

	private static AndroidJavaClass analytics = new AndroidJavaClass("com.adobe.mobile.Analytics");

	private static AndroidJavaClass config = new AndroidJavaClass("com.adobe.mobile.Config");

	private static AndroidJavaClass visitor = new AndroidJavaClass("com.adobe.mobile.Visitor");

	public static void CollectLifecycleData()
	{
		if (!IsEditor())
		{
			AndroidJavaObject androidJavaObject = null;
			using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			config.CallStatic("collectLifecycleData", androidJavaObject);
		}
	}

	public static void CollectLifecycleData(Dictionary<string, object> cdata)
	{
		if (IsEditor())
		{
			return;
		}
		AndroidJavaObject androidJavaObject = null;
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		using AndroidJavaObject androidJavaObject2 = GetHashMapFromDictionary(cdata);
		config.CallStatic("collectLifecycleData", androidJavaObject, androidJavaObject2);
	}

	public static bool GetDebugLogging()
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = config.CallStatic<AndroidJavaObject>("getDebugLogging", new object[0]))
			{
				return androidJavaObject.Call<bool>("booleanValue", new object[0]);
			}
		}
		return false;
	}

	public static double GetLifetimeValue()
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = config.CallStatic<AndroidJavaObject>("getLifetimeValue", new object[0]))
			{
				return androidJavaObject.Call<double>("doubleValue", new object[0]);
			}
		}
		return 0.0;
	}

	public static ADBPrivacyStatus GetPrivacyStatus()
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = config.CallStatic<AndroidJavaObject>("getPrivacyStatus", new object[0]))
			{
				int num = androidJavaObject.Call<int>("getValue", new object[0]);
				return ADBPrivacyStatusFromInt(num + 1);
			}
		}
		return ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN;
	}

	public static string GetUserIdentifier()
	{
		if (!IsEditor())
		{
			try
			{
				using AndroidJavaObject androidJavaObject = config.CallStatic<AndroidJavaObject>("getUserIdentifier", new object[0]);
				return androidJavaObject?.Call<string>("toString", new object[0]);
			}
			catch (Exception)
			{
				return null;
			}
		}
		return string.Empty;
	}

	public static string GetVersion()
	{
		if (!IsEditor())
		{
			return config.CallStatic<string>("getVersion", new object[0]);
		}
		return string.Empty;
	}

	public static void KeepLifecycleSessionAlive()
	{
	}

	public static void OverrideConfigPath(string fileName)
	{
		if (IsEditor())
		{
			return;
		}
		AndroidJavaObject androidJavaObject = null;
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		using AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getResources", new object[0]);
		using AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getAssets", new object[0]);
		using AndroidJavaObject androidJavaObject4 = androidJavaObject3.Call<AndroidJavaObject>("open", new object[1] { fileName });
		config.CallStatic("overrideConfigStream", androidJavaObject4);
	}

	public static void PauseCollectingLifecycleData()
	{
		if (!IsEditor())
		{
			config.CallStatic("pauseCollectingLifecycleData");
		}
	}

	public static void SetContext()
	{
		if (!IsEditor())
		{
			AndroidJavaObject androidJavaObject = null;
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			config.CallStatic("setContext", androidJavaObject);
		}
	}

	public static void SetDebugLogging(bool enabled)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.lang.Boolean", enabled))
			{
				config.CallStatic("setDebugLogging", androidJavaObject);
			}
		}
	}

	public static void SetPrivacyStatus(ADBPrivacyStatus status)
	{
		if (!IsEditor())
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.adobe.mobile.MobilePrivacyStatus"))
			{
				AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>(status.ToString());
				config.CallStatic("setPrivacyStatus", androidJavaObject);
			}
		}
	}

	public static void SetUserIdentifier(string userId)
	{
		if (!IsEditor())
		{
			config.CallStatic("setUserIdentifier", userId);
		}
	}

	public static void EnableLocalNotifications()
	{
	}

	public static void TrackState(string state, Dictionary<string, object> cdata)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata))
			{
				analytics.CallStatic("trackState", state, androidJavaObject);
			}
		}
	}

	public static void TrackAction(string action, Dictionary<string, object> cdata)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata))
			{
				analytics.CallStatic("trackAction", action, androidJavaObject);
			}
		}
	}

	public static void TrackActionFromBackground(string action, Dictionary<string, object> cdata)
	{
	}

	public static void TrackLocation(float latValue, float lonValue, Dictionary<string, object> cdata)
	{
		if (IsEditor())
		{
			return;
		}
		using AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata);
		using AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("android.location.Location", "dummyProvider");
		androidJavaObject2.Call("setLatitude", (double)latValue);
		androidJavaObject2.Call("setLongitude", (double)lonValue);
		analytics.CallStatic("trackLocation", androidJavaObject2, androidJavaObject);
	}

	public static void TrackBeacon(int major, int minor, string uuid, ADBBeaconProximity proximity, Dictionary<string, object> cdata)
	{
		if (IsEditor())
		{
			return;
		}
		using AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata);
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.adobe.mobile.Analytics$BEACON_PROXIMITY");
		AndroidJavaObject androidJavaObject2 = androidJavaClass.GetStatic<AndroidJavaObject>(proximity.ToString());
		AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("java.lang.String", major.ToString());
		AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("java.lang.String", minor.ToString());
		analytics.CallStatic("trackBeacon", uuid, androidJavaObject3, androidJavaObject4, androidJavaObject2, androidJavaObject);
	}

	public static void TrackingClearCurrentBeacon()
	{
		if (!IsEditor())
		{
			analytics.CallStatic("clearBeacon");
		}
	}

	public static void TrackLifetimeValueIncrease(double amount, Dictionary<string, object> cdata)
	{
		if (IsEditor())
		{
			return;
		}
		using AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata);
		using AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.math.BigDecimal", amount);
		analytics.CallStatic("trackLifetimeValueIncrease", androidJavaObject2, androidJavaObject);
	}

	public static void TrackTimedActionStart(string action, Dictionary<string, object> cdata)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata))
			{
				analytics.CallStatic("trackTimedActionStart", action, androidJavaObject);
			}
		}
	}

	public static void TrackTimedActionUpdate(string action, Dictionary<string, object> cdata)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(cdata))
			{
				analytics.CallStatic("trackTimedActionUpdate", action, androidJavaObject);
			}
		}
	}

	public static void TrackTimedActionEnd(string action)
	{
		if (!IsEditor())
		{
			analytics.CallStatic("trackTimedActionEnd", action, null);
		}
	}

	public static bool TrackingTimedActionExists(string action)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = analytics.CallStatic<AndroidJavaObject>("trackingTimedActionExists", new object[1] { action }))
			{
				return androidJavaObject.Call<bool>("booleanValue", new object[0]);
			}
		}
		return false;
	}

	public static string GetTrackingIdentifier()
	{
		if (!IsEditor())
		{
			try
			{
				using AndroidJavaObject androidJavaObject = analytics.CallStatic<AndroidJavaObject>("getTrackingIdentifier", new object[0]);
				return androidJavaObject?.Call<string>("toString", new object[0]);
			}
			catch (Exception)
			{
				return null;
			}
		}
		return string.Empty;
	}

	public static void TrackingSendQueuedHits()
	{
		if (!IsEditor())
		{
			analytics.CallStatic("sendQueuedHits");
		}
	}

	public static void TrackingClearQueue()
	{
		if (!IsEditor())
		{
			analytics.CallStatic("clearQueue");
		}
	}

	public static int TrackingGetQueueSize()
	{
		if (!IsEditor())
		{
			return (int)analytics.CallStatic<long>("getQueueSize", new object[0]);
		}
		return 0;
	}

	public static string GetMarketingCloudID()
	{
		if (!IsEditor())
		{
			try
			{
				using AndroidJavaObject androidJavaObject = visitor.CallStatic<AndroidJavaObject>("getMarketingCloudId", new object[0]);
				return androidJavaObject?.Call<string>("toString", new object[0]);
			}
			catch (Exception)
			{
				return null;
			}
		}
		return string.Empty;
	}

	public static void VisitorSyncIdentifiers(Dictionary<string, object> identifiers)
	{
		if (!IsEditor())
		{
			using (AndroidJavaObject androidJavaObject = GetHashMapFromDictionary(identifiers))
			{
				visitor.CallStatic("syncIdentifiers", androidJavaObject);
			}
		}
	}

	private static bool IsEditor()
	{
		return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
	}

	private static ADBPrivacyStatus ADBPrivacyStatusFromInt(int statusInt)
	{
		return statusInt switch
		{
			1 => ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_IN, 
			2 => ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_OUT, 
			3 => ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN, 
			_ => ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN, 
		};
	}

	private static ADBBeaconProximity ADBBeaconProximityFromInt(int proximity)
	{
		return proximity switch
		{
			1 => ADBBeaconProximity.PROXIMITY_IMMEDIATE, 
			2 => ADBBeaconProximity.PROXIMITY_NEAR, 
			3 => ADBBeaconProximity.PROXIMITY_FAR, 
			_ => ADBBeaconProximity.PROXIMITY_UNKNOWN, 
		};
	}

	private static AndroidJavaObject GetHashMapFromDictionary(Dictionary<string, object> dict)
	{
		if (dict == null || dict.Count <= 0)
		{
			return null;
		}
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.HashMap");
		IntPtr methodID = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
		object[] array = new object[2];
		foreach (KeyValuePair<string, object> item in dict)
		{
			using AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.lang.String", item.Key);
			using AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("java.lang.String", item.Value);
			array[0] = androidJavaObject2;
			array[1] = androidJavaObject3;
			AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
		}
		return androidJavaObject;
	}
}
