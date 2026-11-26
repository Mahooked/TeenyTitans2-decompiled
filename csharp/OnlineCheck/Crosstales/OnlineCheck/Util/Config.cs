using Crosstales.Common.Util;

namespace Crosstales.OnlineCheck.Util;

public static class Config
{
	public static bool DEBUG;

	public static bool isLoaded;

	public static void Reset()
	{
		DEBUG = false;
	}

	public static void Load()
	{
		if (CTPlayerPrefs.HasKey("ONLINECHECK_CFG_DEBUG"))
		{
			DEBUG = CTPlayerPrefs.GetBool("ONLINECHECK_CFG_DEBUG");
		}
		isLoaded = true;
	}

	public static void Save()
	{
		CTPlayerPrefs.SetBool("ONLINECHECK_CFG_DEBUG", DEBUG);
		CTPlayerPrefs.Save();
	}
}
