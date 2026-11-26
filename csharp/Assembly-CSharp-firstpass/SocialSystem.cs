using System;
using UnityEngine;

public class SocialSystem : MonoBehaviour
{
	public static event Action<bool> OnDisplayAchievementsUnavailable;

	public static event Action<bool> OnDisplayLeaderboardsUnavailable;

	public static event Action<bool> OnDisplayCNMoreGamesUnavailable;

	public static event Action<bool> UserAuthenticated;

	public static void OpenIAPWarning()
	{
	}

	public static void DisplayItunesProduct(string productID)
	{
	}

	public static void DisplayTOUPP(string header, string msg, string button1, string button2, string accept)
	{
	}

	public static void GetLocale()
	{
	}

	public static void DisplayTOUPP()
	{
	}

	public static void OpenPrivacyPolicy()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/WebViewObject")) as GameObject;
		gameObject.GetComponent<WebView>().url = "http://www.cartoonnetwork.com/legal/priv_tou.html#textBox_priv";
	}

	public static void OpenTermsOfUse()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/WebViewObject")) as GameObject;
		gameObject.GetComponent<WebView>().url = "http://www.cartoonnetwork.com/legal/priv_tou.html#textBox_tou_a";
	}

	public static void DisplayRateThisGame()
	{
		Application.OpenURL("amzn://android?p=com.turner.ttgfigures");
	}

	public static bool DisplayMoreCNGames()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			Application.OpenURL("http://www.amazon.com/s/ref=bl_sr_mobile-apps?_encoding=UTF8&field-brandtextbin=Cartoon%20Network&node=2350149011");
		}
		else if (OnDisplayCNMoreGamesUnavailable != null)
		{
			OnDisplayCNMoreGamesUnavailable(obj: false);
		}
		return false;
	}

	public bool isGameCenterAvailable()
	{
		return false;
	}

	public static bool IsUserAuthenticated()
	{
		return Social.localUser.authenticated;
	}

	public static void AuthenticateUser(bool isAtBootUp)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer && !IsUserAuthenticated())
		{
			Social.localUser.Authenticate(ProcessAuthentication);
		}
	}

	private static void ProcessAuthentication(bool success)
	{
		if (success)
		{
		}
		if (UserAuthenticated != null)
		{
			UserAuthenticated(success);
		}
	}

	public static void ShowAchievementsUI()
	{
	}

	public static void ReportScore(string id, int score)
	{
	}
}
