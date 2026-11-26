using System.Collections.Generic;

namespace UnityEngine.Purchasing;

internal class ProfileData
{
	private static ProfileData ProfileInstance;

	private const string kConnectSessionInfoApplicationId = "appid";

	private const string kConnectSessionInfoUserId = "userid";

	private const string kConnectSessionInfoSessionId = "sessionid";

	private const string kConnectSessionInfoPlatformName = "platform";

	private const string kConnectSessionInfoPlatformId = "platformid";

	private const string kConnectSessionInfoSdkVersion = "sdk_ver";

	private const string kConnectSessionInfoDeviceId = "deviceid";

	private const string kConnectSessionInfoBuildGuid = "build_guid";

	private const string kConnectSessionInfoIapVersion = "iap_ver";

	private const string kConnectSessionInfoAdsGamerId = "gamerId";

	private const string kConnectSessionInfoAdsGameId = "gameId";

	private const string kConnectSessionInfoAdsABGroup = "abGroup";

	private const string kConnectSessionInfoStoreABGroup = "store_abgroup";

	private const string kConnectSessionInfoCatalogId = "catalogid";

	private const string kConnectSessionInfoMonetizationId = "umpid";

	private const string kConnectSessionInfoStoreTest = "iap_test";

	public string AppId { get; internal set; }

	public string UserId { get; internal set; }

	public ulong SessionId { get; internal set; }

	public string Platform { get; internal set; }

	public int PlatformId { get; internal set; }

	public string SdkVer { get; internal set; }

	public string DeviceId { get; internal set; }

	public string BuildGUID { get; internal set; }

	public string IapVer { get; internal set; }

	public string AdsGamerId { get; internal set; }

	public int? AdsABGroup { get; internal set; }

	public string AdsGameId { get; internal set; }

	public int? StoreABGroup { get; internal set; }

	public string CatalogId { get; internal set; }

	public string MonetizationId { get; internal set; }

	public bool? StoreTestEnabled { get; internal set; }

	private ProfileData()
	{
		try
		{
			AppId = Application.cloudProjectId;
			Platform = Application.platform.ToString();
			PlatformId = (int)Application.platform;
			SdkVer = Application.unityVersion;
			DeviceId = SystemInfo.deviceUniqueIdentifier;
			IapVer = Promo.Version();
			UserId = PlayerPrefs.GetString("unity.cloud_userid", string.Empty);
			SessionId = ulong.Parse(PlayerPrefs.GetString("unity.player_sessionid", "0"));
		}
		catch
		{
		}
	}

	internal Dictionary<string, object> GetProfileDict()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("appid", AppId);
		dictionary.Add("platform", Platform);
		dictionary.Add("platformid", PlatformId);
		if (!string.IsNullOrEmpty(AdsGameId))
		{
			dictionary.Add("gameId", AdsGameId);
		}
		dictionary.Add("sdk_ver", SdkVer);
		if (DeviceId != "n/a")
		{
			dictionary.Add("deviceid", DeviceId);
		}
		if (!string.IsNullOrEmpty(UserId))
		{
			dictionary.Add("userid", UserId);
		}
		if (SessionId != 0)
		{
			dictionary.Add("sessionid", SessionId);
		}
		if (!string.IsNullOrEmpty(BuildGUID))
		{
			dictionary.Add("build_guid", BuildGUID);
		}
		if (!string.IsNullOrEmpty(IapVer))
		{
			dictionary.Add("iap_ver", IapVer);
		}
		if (!string.IsNullOrEmpty(AdsGamerId))
		{
			dictionary.Add("gamerId", AdsGamerId);
		}
		if (AdsABGroup.HasValue)
		{
			dictionary.Add("abGroup", AdsABGroup);
		}
		if (StoreABGroup.HasValue)
		{
			dictionary.Add("store_abgroup", StoreABGroup);
		}
		if (!string.IsNullOrEmpty(CatalogId))
		{
			dictionary.Add("catalogid", CatalogId);
		}
		if (StoreTestEnabled.HasValue)
		{
			dictionary.Add("iap_test", StoreTestEnabled);
		}
		return dictionary;
	}

	internal Dictionary<string, string> GetProfileIds()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("appid", AppId);
		if (DeviceId != "n/a")
		{
			dictionary.Add("deviceid", DeviceId);
		}
		if (!string.IsNullOrEmpty(UserId))
		{
			dictionary.Add("userid", UserId);
		}
		if (!string.IsNullOrEmpty(AdsGamerId))
		{
			dictionary.Add("gamerId", AdsGamerId);
		}
		if (!string.IsNullOrEmpty(MonetizationId))
		{
			dictionary.Add("umpid", MonetizationId);
		}
		return dictionary;
	}

	internal static ProfileData Instance()
	{
		if (ProfileInstance == null)
		{
			ProfileInstance = new ProfileData();
		}
		return ProfileInstance;
	}

	internal void SetGamerId(string gamerid)
	{
		if (!string.IsNullOrEmpty(gamerid))
		{
			AdsGamerId = gamerid;
		}
	}

	internal void SetGameId(string gameid)
	{
		if (!string.IsNullOrEmpty(gameid))
		{
			AdsGameId = gameid;
		}
	}

	internal void SetABGroup(int? abgroup)
	{
		if (abgroup.HasValue && abgroup > 0)
		{
			AdsABGroup = abgroup;
		}
	}

	internal void SetStoreABGroup(int? abgroup)
	{
		if (abgroup.HasValue)
		{
			StoreABGroup = abgroup;
		}
	}

	internal void SetCatalogId(string storeid)
	{
		if (!string.IsNullOrEmpty(storeid))
		{
			CatalogId = storeid;
		}
	}

	internal void SetMonetizationId(string umpid)
	{
		if (!string.IsNullOrEmpty(umpid))
		{
			MonetizationId = umpid;
		}
	}

	internal void SetStoreTestEnabled(bool enable)
	{
		StoreTestEnabled = enable;
	}
}
