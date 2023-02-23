using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;

public class AdmobController : MonoBehaviour
{
    private readonly TimeSpan APPOPEN_TIMEOUT = TimeSpan.FromHours(4);
    private DateTime appOpenExpireTime;
    private AppOpenAd appOpenAd;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    private float deltaTime;
    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;
                        int adsCounter = 0;
                    

    public GameObject FreeAdsBtn;
    public UnityEngine.UI.Text newCoinsGetText;
    public UnityEngine.UI.Text ErrorText;
    public GameObject newCoinsGetBox;

    private static AdmobController _instance;
    public static AdmobController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
    UnityMainThread.wkr.AddJob(() =>
    {
        adsCounter = 0;
        PlayerPrefs.SetInt("AdsCounter", 0);
        FreeAdsBtn.SetActive(false);
    });
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
            .SetMaxAdContentRating(MaxAdContentRating.G)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => { });
        RequestBannerAd();
        RequestAndLoadAppOpenAd();
        RequestAndLoadInterstitialAd();
        RequestAndLoadRewardedAd();
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        
    }


    /////////////////////////////HELPER 

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }


    public void Debugger(string msg)
    {
        Debug.Log(msg);
    }

    ///////////////////////////////APPOPEN
    
    public bool IsAppOpenAdAvailable
    {
        get
        {
            return (appOpenAd != null
                    && appOpenAd.CanShowAd()
                    && DateTime.Now < appOpenExpireTime);
        }
    }

    public void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);

        // OnAppStateChanged is not guaranteed to execute on the Unity UI thread.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (state == AppState.Foreground)
            {
                ShowAppOpenAd();
            }
        });
    }

    public void RequestAndLoadAppOpenAd()
    {
        Debug.Log("Requesting App Open ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-2294561581875039/4793323768"; ////UPDATED
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/5662855259";
#else
        string adUnitId = "unexpected_platform";
#endif

        // destroy old instance.
        if (appOpenAd != null)
        {
            DestroyAppOpenAd();
        }

        // Create a new app open ad instance.
        AppOpenAd.Load(adUnitId, ScreenOrientation.Portrait, CreateAdRequest(),
            (AppOpenAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    Debug.Log("App open ad failed to load with error: " +
                        loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    Debug.Log("App open ad failed to load.");
                    return;
                }

                Debug.Log("App Open ad loaded. Please background the app and return.");
                this.appOpenAd = ad;
                this.appOpenExpireTime = DateTime.Now + APPOPEN_TIMEOUT;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    Debug.Log("App open ad opened.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("App open ad closed.");
                    OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    Debug.Log("App open ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    Debug.Log("App open ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.Log("App open ad failed to show with error: " +
                        error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "App open ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    Debug.Log(msg);
                };
            });
    } /// APP OPEN ID

    public void DestroyAppOpenAd()
    {
        if (this.appOpenAd != null)
        {
            this.appOpenAd.Destroy();
            this.appOpenAd = null;
        }
    }

    public void ShowAppOpenAd()
    {
        if (!IsAppOpenAdAvailable)
        {
            return;
        }
        appOpenAd.Show();
    }


    ///////////////BANNER

    public void RequestBannerAd()
    {
        Debugger("Requesting Banner ad.");

        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-2294561581875039/4569962486"; /////UPDATED
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Add Event Handlers
        bannerView.OnBannerAdLoaded += () =>
        {
            Debugger("Banner ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debugger("Banner ad failed to load with error: " + error.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
        };
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debugger("Banner ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debugger("Banner ad closed.");
            OnAdClosedEvent.Invoke();
        };
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Banner ad received a paid event.",
                                        adValue.CurrencyCode,
                                        adValue.Value);
            Debugger(msg);
        };

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
    }  //BANNER ID

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    //////////////////////REWARDED

    public void RequestAndLoadRewardedAd()
    {
        Debugger("Requesting Rewarded ad.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-2294561581875039/3995247418"; //updated
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        // create new rewarded ad instance
        RewardedAd.Load(adUnitId, CreateAdRequest(),
            (RewardedAd ad, LoadAdError loadError) =>
            {
            if (loadError != null)
            {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + "Rewarded ad failed to load with error: " +
                            loadError.GetMessage();
                    });
                    Debugger("Rewarded ad failed to load with error: " +
                                loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    Debugger("Rewarded ad failed to load.");
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + "Rewarded ad failed to load.";
                    });
                    return;
                }

                Debugger("Rewarded ad loaded.");
            UnityMainThread.wkr.AddJob(() =>
            {
                ErrorText.text = ErrorText.text + "Rewarded ad loaded.";
                FreeAdsBtn.SetActive(true);
            });
                rewardedAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    Debugger("Rewarded ad opening.");
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + ("Rewarded ad opening.");                        FreeAdsBtn.SetActive(false);
                    });
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debugger("Rewarded ad closed.");
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + "Rewarded ad closed.";
                    
                    if (adsCounter < 3)
                    {
                        Invoke("RequestAndLoadRewardedAd", 5f);

                    }
                    });
                    OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + "Rewarded ad recorded an impression.";
                    });
                    Debugger("Rewarded ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + "Rewarded ad recorded a click.";
                    });
                    Debugger("Rewarded ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        ErrorText.text = ErrorText.text + "Rewarded ad failed to show with error: " +
                               error.GetMessage();
                    });
                    Debugger("Rewarded ad failed to show with error: " +
                               error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Rewarded ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    Debugger(msg);
                };
            });
    } //REWARDED ID

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show((Reward reward) =>
            {
                UnityMainThread.wkr.AddJob(() =>
                {
                    GameManager.Instance.IncreaseCoin(100);
                    ErrorText.text = ErrorText.text + "Worked";
                    newCoinsGetText.text = "+100";
                    newCoinsGetBox.SetActive(true);
                    adsCounter++;
                    PlayerPrefs.SetInt("AdsCounter", adsCounter);
                });
            });
        }
        else
        {
            Debugger("Rewarded ad is not ready yet.");
            UnityMainThread.wkr.AddJob(() =>
            {
                ErrorText.text = ErrorText.text + "Rewarded ad is not ready yet.";
            });
        }
    }

  



    ////////////////////////INTERSTITIAL

    public void RequestAndLoadInterstitialAd()
    {
        Debugger("Requesting Interstitial ad.");

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-2294561581875039/8349425392"; 
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        // Load an interstitial ad
        InterstitialAd.Load(adUnitId, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    Debugger("Interstitial ad failed to load with error: " +
                        loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    Debugger("Interstitial ad failed to load.");
                    return;
                }

                Debugger("Interstitial ad loaded.");
                interstitialAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    Debugger("Interstitial ad opening.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debugger("Interstitial ad closed.");
                    OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    Debugger("Interstitial ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    Debugger("Interstitial ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debugger("Interstitial ad failed to show with error: " +
                                error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Interstitial ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    Debugger(msg);
                };
            });
    } //INTERAD ID

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            Invoke("RequestAndLoadInterstitialAd", 5f);
        }
        else
        {
            Debugger("Interstitial ad is not ready yet.");
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

}
