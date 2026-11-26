namespace Crosstales.OnlineCheck.Util;

public static class Context
{
	public static int NumberOfChecks;

	public static float Runtime;

	public static float Uptime;

	public static float ChecksPerMinute => (float)NumberOfChecks / (Runtime / 60f);

	public static float Downtime => Runtime - Uptime;
}
