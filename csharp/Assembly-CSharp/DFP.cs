using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class DFP
{
	public string debugString;

	public bool isPhone;

	private List<string> timesPlayedKeyNames;

	private List<string> timeSpentKeyNames;

	private List<string> moneySpentKeyNames;

	private BannerView bannerView;

	private InterstitialAd interstitial;

	public bool bannerLoaded;

	private Texture2D mainImageTexture;

	private string headline;

	private GameObject adObject;

	private bool initialized;

	private static DFP instance;

	private DFPGameConfiguration dfpGameConfig;

	private BannerAdRequest queuedBannerAd;

	private InterstitialAdRequest queuedInterstitialAd;

	public DFPGameConfiguration DFPGameConfig => dfpGameConfig;

	public bool Initialized => initialized;

	public static DFP Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new DFP();
			}
			return instance;
		}
	}

	public void Initialize(DFPGameConfiguration gameConfig)
	{
		if (gameConfig == null)
		{
			Debug.Log("game config is null");
		}
		dfpGameConfig = gameConfig;
		if (dfpGameConfig != null)
		{
			initialized = true;
			SendQueuedAds();
		}
	}

	public void RequestBanner()
	{
		if (!initialized)
		{
			Debug.Log("DFPGameConfiguration not initialized!");
			if (queuedBannerAd == null)
			{
				queuedBannerAd = new BannerAdRequest(AdPosition.Center);
			}
			return;
		}
		Debug.Log("RequestBanner call. " + dfpGameConfig.AdUnitId + " " + dfpGameConfig.Promo);
		AdSize adSize = new AdSize(300, 250);
		string empty = string.Empty;
		if (isPhone)
		{
			empty = "phone";
			adSize = new AdSize(300, 250);
		}
		else
		{
			empty = "tab";
			adSize = new AdSize(600, 400);
		}
		bannerView = new BannerView(dfpGameConfig.AdUnitId, adSize, AdPosition.Center);
		bannerView.OnAdLoaded += HandleOnBannerLoaded;
		bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		List<string> list = new List<string>();
		list = GetInstalledAndroidApps();
		string text = ((list.Count <= 0) ? "none" : string.Join(",", list.ToArray()));
		Debug.Log("Installed apps: " + text);
		AdRequest request = new AdRequest.Builder().TagForChildDirectedTreatment(tagForChildDirectedTreatment: true).AddExtra("app", "true").AddExtra("plat", empty)
			.AddExtra("appmode", debugString)
			.AddExtra("game", dfpGameConfig.Promo)
			.AddExtra("installed", text)
			.Build();
		bannerView.LoadAd(request);
	}

	public void DestroyBanner()
	{
		if (bannerView != null)
		{
			bannerView.Destroy();
		}
	}

	public void RequestInterstitial(DFPKeyValues keyValues)
	{
		if (!initialized)
		{
			Debug.Log("DFPGameConfiguration not initialized!");
			if (queuedInterstitialAd == null)
			{
				queuedInterstitialAd = new InterstitialAdRequest(keyValues);
			}
			return;
		}
		if (keyValues == null)
		{
			Debug.LogError("DFPKeyValues are NULL! Not sending ad request.");
			return;
		}
		interstitial = new InterstitialAd(dfpGameConfig.AdUnitId);
		interstitial.OnAdLoaded += HandleOnInterstitialLoaded;
		string text = "timespent_" + keyValues.getTimeSpent();
		string text2 = "timesplayed_" + keyValues.getTimesPlayed();
		string text3 = "moneyspent_" + keyValues.getMoneySpent();
		string empty = string.Empty;
		empty = ((!isPhone) ? "tab" : "phone");
		List<string> list = new List<string>();
		list = GetInstalledAndroidApps();
		string text4 = string.Join(",", list.ToArray());
		Debug.Log("Installed apps: " + text4);
		Debug.Log("RequestInterstitial call. " + dfpGameConfig.AdUnitId + " " + dfpGameConfig.Promo + " " + text + " " + text2 + " " + text3);
		AdRequest request = new AdRequest.Builder().TagForChildDirectedTreatment(tagForChildDirectedTreatment: true).AddExtra("app", "true").AddExtra("plat", empty)
			.AddExtra("appmode", debugString)
			.AddExtra("game", dfpGameConfig.Promo)
			.AddExtra("installed", text4)
			.AddExtra(text, "true")
			.AddExtra(text2, "true")
			.AddExtra(text3, "true")
			.Build();
		interstitial.LoadAd(request);
	}

	public void DestroyInterstitial()
	{
		if (interstitial != null)
		{
			interstitial.Destroy();
		}
	}

	public void LoadCustomNativeTemplateAd(GameObject go, string adUnitId, List<string> templateIds)
	{
		if (!initialized)
		{
			Debug.LogError("DFPGameConfiguration not initialized!");
			return;
		}
		Debug.Log("Native ad call. " + adUnitId + " " + go.name);
		adObject = go;
		if (templateIds == null || templateIds.Count == 0)
		{
			Debug.LogError("LoadCustomNativeTemplateAd no template ids provided!");
			return;
		}
		AdLoader.Builder builder = new AdLoader.Builder(adUnitId);
		for (int i = 0; i < templateIds.Count; i++)
		{
			Debug.Log("Adding custom template: " + templateIds[i]);
			builder.ForCustomNativeAd(templateIds[i]);
		}
		AdLoader adLoader = builder.Build();
		AdRequest request = new AdRequest.Builder().Build();
		adLoader.OnCustomNativeTemplateAdLoaded += HandleCustomNativeAdLoaded;
		adLoader.OnAdFailedToLoad += HandleCustomNativeAdFailedToLoad;
		adLoader.LoadAd(request);
	}

	private void HandleOnBannerLoaded(object sender, EventArgs args)
	{
		bannerView.Hide();
		bannerLoaded = true;
		Debug.Log("HandleOnBannerLoaded event received.");
	}

	public bool ShowBanner()
	{
		if (bannerLoaded)
		{
			bannerView.Show();
		}
		return bannerLoaded;
	}

	public void HideBanner()
	{
		if (bannerView != null)
		{
			bannerView.Hide();
		}
	}

	public void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
	}

	private void HandleOnInterstitialLoaded(object sender, EventArgs args)
	{
		ShowInterstitial();
		Debug.Log("HandleInterstitialOnLoaded even received");
	}

	private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log(string.Concat("HandleOnAdFailedToLoad: ", sender.GetType(), " ", args.Message));
	}

	private void HandleCustomNativeAdLoaded(object sender, CustomNativeEventArgs args)
	{
		Debug.Log("HandleCustomNativeAdLoaded.");
		ApplyAdTexture(args.nativeAd);
	}

	private void HandleCustomNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log("HandleCustomNativeAdFailedToLoad: " + args.Message);
	}

	private void HandleCustomNativeAdClicked(CustomNativeTemplateAd customNativeTemplateAd, string assetName)
	{
		Debug.Log("Native ad asset with name " + assetName + " was clicked.");
	}

	private void ApplyAdTexture(CustomNativeTemplateAd nativeAd)
	{
		if (nativeAd == null)
		{
			Debug.Log("nativeAd is NULL!");
			return;
		}
		Debug.Log("Native Ad Loaded...  Getting texture");
		mainImageTexture = nativeAd.GetTexture2D("MainImage");
		Debug.Log("Got native ad texture");
		headline = nativeAd.GetText("Headline");
		if (adObject != null)
		{
			Debug.Log("Applying texture");
			adObject.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_MainTex", mainImageTexture);
		}
		string customTemplateId = nativeAd.GetCustomTemplateId();
		Debug.Log("Playing ad " + customTemplateId);
	}

	private void SendQueuedAds()
	{
		if (queuedBannerAd != null)
		{
			RequestBanner();
			queuedBannerAd = null;
		}
		if (queuedInterstitialAd != null)
		{
			RequestInterstitial(queuedInterstitialAd.KeyValues);
			queuedInterstitialAd = null;
		}
	}

	private List<string> TrimForMaxAppInstalls(List<string> installedApps)
	{
		int maxInstalledApps = DFPManager.Instance.Config.MaxInstalledApps;
		if (maxInstalledApps > DFPConfiguration.UNLIMITED_APPS)
		{
			if (maxInstalledApps == 0)
			{
				Debug.Log("Max installed apps 0.  Clear installed apps.");
				installedApps.Clear();
				return installedApps;
			}
			if (installedApps.Count > maxInstalledApps)
			{
				Debug.Log("Trimming installed app list...  count: " + installedApps.Count + " max: " + maxInstalledApps);
				installedApps.RemoveRange(maxInstalledApps, installedApps.Count - maxInstalledApps);
				Debug.Log("Trimmed apps to " + installedApps.Count);
			}
		}
		return installedApps;
	}

	private List<string> GetInstalledAndroidApps()
	{
		List<string> list = new List<string>();
		List<string> packages = DFPManager.Instance.Config.GetPackages();
		for (int i = 0; i < packages.Count; i++)
		{
			string text = packages[i];
			if (CheckURLExists.AndroidAppInstalled(text))
			{
				list.Add(text);
			}
		}
		list = DFPManager.Instance.Config.GetSchemeNamesFromAndroidPackages(list);
		return TrimForMaxAppInstalls(list);
	}
}
