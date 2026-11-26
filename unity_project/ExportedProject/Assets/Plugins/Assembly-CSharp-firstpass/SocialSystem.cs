using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class SocialSystem : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnDisplayAchievementsUnavailable__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnDisplayLeaderboardsUnavailable__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> OnDisplayCNMoreGamesUnavailable__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> UserAuthenticated__BackingField;

	public static event Action<bool> OnDisplayAchievementsUnavailable
	{
		add
		{
			Action<bool> action = OnDisplayAchievementsUnavailable__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDisplayAchievementsUnavailable__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnDisplayAchievementsUnavailable__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDisplayAchievementsUnavailable__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> OnDisplayLeaderboardsUnavailable
	{
		add
		{
			Action<bool> action = OnDisplayLeaderboardsUnavailable__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDisplayLeaderboardsUnavailable__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnDisplayLeaderboardsUnavailable__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDisplayLeaderboardsUnavailable__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> OnDisplayCNMoreGamesUnavailable
	{
		add
		{
			Action<bool> action = OnDisplayCNMoreGamesUnavailable__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDisplayCNMoreGamesUnavailable__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OnDisplayCNMoreGamesUnavailable__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDisplayCNMoreGamesUnavailable__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> UserAuthenticated
	{
		add
		{
			Action<bool> action = UserAuthenticated__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref UserAuthenticated__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = UserAuthenticated__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref UserAuthenticated__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
		else if (OnDisplayCNMoreGamesUnavailable__BackingField != null)
		{
			OnDisplayCNMoreGamesUnavailable__BackingField(false);
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
		if (UserAuthenticated__BackingField != null)
		{
			UserAuthenticated__BackingField(success);
		}
	}

	public static void ShowAchievementsUI()
	{
	}

	public static void ReportScore(string id, int score)
	{
	}
}
