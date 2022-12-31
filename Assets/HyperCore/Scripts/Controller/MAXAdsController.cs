using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MAXAdsController : Singleton<MAXAdsController>
    {
//    [SerializeField] private string sdkKey;
//    [SerializeField] string adUnitIdBanner = "";
//    [SerializeField] string adUnitIdInter = "";
//    [SerializeField] string adUnitIdReward = "";
//    public float TimeShowAds = 30f;


//    int retryAttemptInter;
//    int retryAttemptReward;
//    private UnityEvent onRewarded;
//    private float lastTimeShowAd;

//    private void Start ()
//    {
//        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
//        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
//        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
//        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
//        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
//        {
//            // AppLovin SDK is initialized, start loading ads
//            if (!GlobalController.Instance.ForTesting)
//            {
//                LoadAds();
//            }
//        };

//        MaxSdk.SetSdkKey(sdkKey);
//        MaxSdk.InitializeSdk();
//    }

//    private void OnAdRevenuePaidEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        if (adInfo != null)
//        {
//            double revenue = adInfo.Revenue;
//            string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
//            string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
//            string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
//            string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
//            Firebase.Analytics.Parameter[] AdParameters = {
//              new Firebase.Analytics.Parameter("ad_platform", "max"),
//              new Firebase.Analytics.Parameter("ad_source", networkName),
//              new Firebase.Analytics.Parameter("ad_unit_name", adUnitIdentifier),
//              new Firebase.Analytics.Parameter("ad_format", placement),
//              new Firebase.Analytics.Parameter("currency","USD"),
//              new Firebase.Analytics.Parameter("country", countryCode),
//              new Firebase.Analytics.Parameter("value", revenue)
//            };
//            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", AdParameters);
//        }
//    }

//    public void ShowInter ()
//    {
//        if (Time.time - lastTimeShowAd >= TimeShowAds)
//        {
//            AppsflyerController.Instance.LogEvent("af_inters_ad_eligible");
//            if (MaxSdk.IsInterstitialReady(adUnitIdInter))
//            {
//                lastTimeShowAd = Time.time;
//                MaxSdk.ShowInterstitial(adUnitIdInter);
//            }
//            else
//            {
//                LoadInterstitial();
//            }
//        }
//    }

//    public void ShowRewarded (UnityEvent callback)
//    {
//        AppsflyerController.Instance.LogEvent("af_rewarded_ad_eligible");
//        if (MaxSdk.IsRewardedAdReady(adUnitIdReward))
//        {
//            onRewarded = callback;
//            MaxSdk.ShowRewardedAd(adUnitIdReward);
//        }
//    }

//    public void ShowBanner ()
//    {
//        MaxSdk.ShowBanner(adUnitIdBanner);
//    }

//    public void HideBanner ()
//    {
//        MaxSdk.HideBanner(adUnitIdBanner);
//    }

//    private void LoadAds ()
//    {
//        InitializeInterstitialAds();
//        InitializeRewardedAds();
//        InitializeBannerAds();
//    }

//    private bool isInterShowing;
//    public void InitializeInterstitialAds ()
//    {
//        // Attach callback
//        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
//        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
//        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
//        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
//        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
//        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

//        // Load the first interstitial
//        LoadInterstitial();
//    }

//    private void LoadInterstitial ()
//    {
//        MaxSdk.LoadInterstitial(adUnitIdInter);
//    }

//    public bool IsShowingFullscreenAds ()
//    {
//        return isRewardShowing || isInterShowing;
//    }

//    private void OnInterstitialLoadedEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
//        AppsflyerController.Instance.LogEvent("af_inters_api_called");
//        AnalyticsController.Instance.LogCustomEvent("ad_inter_load", "placement", adInfo.Placement);

//        // Reset retry attempt
//        retryAttemptInter = 0;
//    }

//    private void OnInterstitialLoadFailedEvent (string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
//    {
//        // Interstitial ad failed to load 
//        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

//        retryAttemptInter++;
//        double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttemptInter));

//        AnalyticsController.Instance.LogCustomEvent("ad_inter_fail", "placement", errorInfo.Message);
//        Invoke("LoadInterstitial", (float)retryDelay);
//    }

//    private void OnInterstitialDisplayedEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        AppsflyerController.Instance.LogEvent("af_inters_displayed");
//        AnalyticsController.Instance.LogCustomEvent("ad_inter_show", "placement", adInfo.Placement);
//        isInterShowing = true;
//    }

//    private void OnInterstitialAdFailedToDisplayEvent (string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
//    {
//        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
//        LoadInterstitial();

//    }

//    private void OnInterstitialClickedEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        AnalyticsController.Instance.LogCustomEvent("ad_inter_click", "placement", adInfo.Placement);

//    }

//    private void OnInterstitialHiddenEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        // Interstitial ad is hidden. Pre-load the next ad.
//        LoadInterstitial();
//        isInterShowing = false;
//    }

//    /// ------------------- REWARDED VIDEOS --------------------
//    private bool isRewardShowing;
//    public void InitializeRewardedAds ()
//    {
//        // Attach callback
//        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
//        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
//        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
//        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
//        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
//        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
//        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

//        // Load the first rewarded ad
//        LoadRewardedAd();
//    }

//    private void LoadRewardedAd ()
//    {
//        MaxSdk.LoadRewardedAd(adUnitIdReward);
//    }

//    private void OnRewardedAdLoadedEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.
//        AppsflyerController.Instance.LogEvent("af_rewarded_api_called");

//        // Reset retry attempt
//        retryAttemptReward = 0;
//    }

//    private void OnRewardedAdLoadFailedEvent (string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
//    {
//        // Rewarded ad failed to load 
//        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

//        retryAttemptReward++;
//        double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttemptReward));
//        AnalyticsController.Instance.LogCustomEvent("ads_reward_failed", "errormsg", errorInfo.Message);

//        Invoke("LoadRewardedAd", (float)retryDelay);
//    }

//    private void OnRewardedAdDisplayedEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        AppsflyerController.Instance.LogEvent("af_rewarded_ad_displayed");
//        AnalyticsController.Instance.LogCustomEvent("ads_reward_show", "placement", adInfo.Placement);
//        isRewardShowing = true;
//    }

//    private void OnRewardedAdFailedToDisplayEvent (string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
//    {
//        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
//        LoadRewardedAd();
//    }

//    private void OnRewardedAdClickedEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        AnalyticsController.Instance.LogCustomEvent("ads_reward_click", "placement", adInfo.Placement);
//    }

//    private void OnRewardedAdHiddenEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        // Rewarded ad is hidden. Pre-load the next ad
//        LoadRewardedAd();
//        isRewardShowing = false;
//    }

//    private void OnRewardedAdReceivedRewardEvent (string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
//    {
//        // The rewarded ad displayed and the user should receive the reward.
//        onRewarded?.Invoke();
//        AnalyticsController.Instance.LogCustomEvent("ads_reward_complete", "placement", adInfo.Placement);

//    }

//    private void OnRewardedAdRevenuePaidEvent (string adUnitId, MaxSdkBase.AdInfo adInfo)
//    {
//        // Ad revenue paid. Use this callback to track user revenue.
//    }

//    /// ---------------------- BANNER -------------------------


//    public void InitializeBannerAds ()
//    {
//        MaxSdk.CreateBanner(adUnitIdBanner, MaxSdkBase.BannerPosition.BottomCenter);

//        // Set background or background color for banners to be fully functional
//        MaxSdk.SetBannerBackgroundColor(adUnitIdBanner, Color.white);

//        MaxSdk.SetBannerWidth(adUnitIdBanner, 320);

//    }
}
