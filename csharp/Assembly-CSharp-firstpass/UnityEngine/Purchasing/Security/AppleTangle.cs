using System;

namespace UnityEngine.Purchasing.Security;

public class AppleTangle
{
	private static byte[] data = Convert.FromBase64String("hFQ/uBBDmDIS1r9oTvcYwgAQQLxSyM7IVtRwCnq/5NYNw2GZ/N1flebuPN8KHhiM4mIM/rW2rj2Aq+4BfVxLThhJR15HDD09IShtBCMuY3w9IShtHyIiOW0ODH1TWkB9e315f2MN67oKADJFE31SS04YUG5JVX1bbSwjKW0uKD85JCskLiw5JCIjbT0ElTvSflko7DrZhGBPTkxNTO7PTM9MTUtEZ8sFy7ouKUhMfcy/fWdLjS5+Orp3SmEbppdCbEOX9z5UAvhITU7PTEJNfc9MR0/PTExNqdzkREtOGFBDSVtJWWadJArZO0SzuSbAIShtBCMuY3xrfWlLThhJRl5QDD3Y0zdB6QrGFplben6GiUIAg1kknCMpbS4iIykkOSQiIz5tIittOD4oCDNSASYd2wzEiTkvRl3ODMp+x8zGVMSTtAYhuErmb31PpVVztR1Enmt9aUtOGElGXlAMPT0hKG0OKD85PywuOSQuKG0+OSw5KCAoIzk+Y31S3JZTCh2mSKATNMlgpnvvGgEYoWmvppz6PZJCCKxqh7wgNaCq+FpaOSQrJC4sOShtLzRtLCM0bT0sPzn8fRWhF0l/wSX+wlCTKD6yKhMo8UlLXk8YHnxefVxLThhJR15HDD09bSIrbTklKG05JSgjbSw9PSEkLixAS0RnywXLukBMTEhITU7PTExNEUqhMHTOxh5tnnWJ/PLXAkcmsmax5ZEzb3iHaJiUQpsmme9pbly67OFhbS4oPzkkKyQuLDkobT0iISQuNELQcL5mBGVXhbOD+PRDlBNRm4ZwPSEobQ4oPzkkKyQuLDkkIiNtDDgqwkX5bbqG4WFtIj37ckx9wfoOgpR7MozKGJTq1PR/D7aVmDzTM+wfNG0sPj44ICg+bSwuLig9OSwjLih9z0n2fc9O7u1OT0xPT0xPfUBLRHh/fHl9fnsXWkB+eH1/fXR/fHl9W31ZS04YSU5eQAw9PSEobR8iIjlFE33PTFxLThhQbUnPTEV9z0xJfX57F30vfEZ9REtOGElLXk8YHnxeZ8sFy7pATExISE19L3xGfURLThgyDOXVtJyHK9FpJlyd7vapVmeOUjklIj8kOTR8W31ZS04YSU5eQAw9wj7MLYtWFkRi3/+1CQW9LXXTWLhwayptx34nukDPgpOm7mK0HicWKSQrJC4sOSQiI20MODklIj8kOTR8bQ4Mfc9Mb31AS0RnywXLukBMTEzNWWadJArZO0SzuSbAYw3rugoAMjo6Yyw9PSEoYy4iIGIsPT0hKC4sRWZLTEhISk9MW1MlOTk9PndiYjopeG5YBlgUUP7ZurvR04Id94wVHWJ9zI5LRWZLTEhISk9Pfcz7V8z+e9QBYDX6oMHWkb461r87nzp9AoxLfUJLThhQXkxMsklIfU5MTLJ9UPO5PtajnylChjQCeZXvc7Q1siaFN33PTDt9Q0tOGFBCTEyySUlOT0wvIShtPjksIyksPyltOSg/ID5tLPpW8N4PaV9nikJQ+wDREy6FBs1aHyghJCwjLihtIiNtOSUkPm0uKD/4d+C5QkNN30b8bFtjOZhxQJYvWxTqSEQxWg0bXFM5nvrGbnYK7pgiHefHmJepsZ1ESnr9ODhs");

	private static int[] order = new int[61]
	{
		19, 49, 24, 9, 6, 31, 45, 27, 34, 57,
		14, 30, 57, 18, 43, 25, 16, 43, 46, 25,
		53, 39, 26, 24, 41, 47, 51, 54, 44, 51,
		45, 39, 58, 40, 47, 54, 57, 40, 47, 49,
		43, 49, 55, 56, 45, 46, 58, 57, 54, 53,
		56, 54, 54, 56, 58, 55, 59, 57, 59, 59,
		60
	};

	private static int key = 77;

	public static readonly bool IsPopulated = true;

	public static byte[] Data()
	{
		if (!IsPopulated)
		{
			return null;
		}
		return Obfuscator.DeObfuscate(data, order, key);
	}
}
