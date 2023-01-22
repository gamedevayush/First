using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManagerScript : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    public GameObject FreeAdsButton; 

    public string BannerID="ca-app-pub-3940256099942544/6300978111";
    public string InterstitialID="ca-app-pub-3940256099942544/1033173712";
    public string RewardedId1 = "ca-app-pub-3940256099942544/5224354917";



    private static AdManagerScript _instance;
    public static AdManagerScript Instance { get { return _instance; } }


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

    public void Start()
    {
        FreeAdsButton.SetActive(false);
        MobileAds.Initialize(initStatus => { });
        RequestandShowBanner();
        CreateAndLoadRewardedAd();
        RequestInterstitial();
        InvokeRepeating("ReCheckRewarded", 5f,10f);
    }

    private void RequestandShowBanner()
    {
        this.bannerView = new BannerView(BannerID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }


    /// ///////////////////////////////////////////////////////////////
    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(InterstitialID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            Invoke("RequestInterstitial",20f);
        }
    }


    ///////////////////////////////////////////////////////////////////////
    

    public void CreateAndLoadRewardedAd()
    {
        this.rewardedAd = new RewardedAd(RewardedId1);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request);

    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        FreeAdsButton.SetActive(true);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.IncreaseCoin(400);
        FreeAdsButton.SetActive(false);
    }



    public void ShowRewardedAd()
    {
            this.rewardedAd.Show();

    }

    void ReCheckRewarded()
    {
        if (this.rewardedAd.IsLoaded())
        {
            FreeAdsButton.SetActive(true);
        }
    }
    
}

