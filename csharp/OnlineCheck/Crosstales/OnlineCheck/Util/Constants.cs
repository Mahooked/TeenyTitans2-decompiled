using System;
using Crosstales.Common.Util;

namespace Crosstales.OnlineCheck.Util;

public abstract class Constants : BaseConstants
{
	public static readonly bool isPro = false;

	public const string ASSET_NAME = "Online Check";

	public const string ASSET_NAME_SHORT = "OC";

	public const string ASSET_VERSION = "1.6.1";

	public const int ASSET_BUILD = 20180606;

	public static readonly DateTime ASSET_CREATED = new DateTime(2017, 5, 4);

	public static readonly DateTime ASSET_CHANGED = new DateTime(2018, 6, 6);

	public const string ASSET_PRO_URL = "https://www.assetstore.unity3d.com/#!/content/74688?aid=1011lNGT&pubref=Online Check";

	public const string ASSET_UPDATE_CHECK_URL = "https://www.crosstales.com/media/assets/onlinecheck_versions.txt";

	public const string ASSET_CONTACT = "onlinecheck@crosstales.com";

	public const string ASSET_MANUAL_URL = "https://www.crosstales.com/media/data/assets/OnlineCheck/OnlineCheck-doc.pdf";

	public const string ASSET_API_URL = "https://crosstales.com/media/data/assets/OnlineCheck/api";

	public const string ASSET_FORUM_URL = "https://goo.gl/9XH5Ke";

	public const string ASSET_WEB_URL = "https://www.crosstales.com/en/portfolio/OnlineCheck/";

	public const string ASSET_VIDEO_PROMO = "https://youtu.be/pPvKE-eyxV4?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

	public const string ASSET_VIDEO_TUTORIAL = "https://youtu.be/bNdafUNcs68?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

	public const string KEY_PREFIX = "ONLINECHECK_CFG_";

	public const string KEY_DEBUG = "ONLINECHECK_CFG_DEBUG";

	public const string KEY_DONT_DESTROY_ON_LOAD = "ONLINECHECK_CFG_DONT_DESTROY_ON_LOAD";

	public const string ONLINECHECK_SCENE_OBJECT_NAME = "OnlineCheck";

	public const string PROXY_SCENE_OBJECT_NAME = "Proxy";
}
