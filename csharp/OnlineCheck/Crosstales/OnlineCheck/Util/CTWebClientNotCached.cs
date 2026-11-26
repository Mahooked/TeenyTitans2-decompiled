using System;
using System.Net;
using System.Net.Cache;
using Crosstales.Common.Util;

namespace Crosstales.OnlineCheck.Util;

public class CTWebClientNotCached : CTWebClient
{
	public CTWebClientNotCached()
		: base(5000)
	{
	}

	public CTWebClientNotCached(int timeout, int connectionLimit = 20)
		: base(timeout, connectionLimit)
	{
	}

	protected override WebRequest GetWebRequest(Uri uri)
	{
		WebRequest webRequest = base.GetWebRequest(uri);
		if (webRequest != null)
		{
			webRequest.Timeout = Timeout;
			HttpRequestCachePolicy cachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			webRequest.CachePolicy = cachePolicy;
		}
		return webRequest;
	}
}
