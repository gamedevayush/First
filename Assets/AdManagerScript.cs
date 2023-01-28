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


    public UnityEngine.UI.Text newCoinsGetText;
    public GameObject newCoinsGetBox;
    public GameObject FreeAdsButton; 

	//public UnityEngine.UI.Text ErrorText;
    public string BannerID= "ca-app-pub-2294561581875039/3106879603";
    public string InterstitialID= "ca-app-pub-2294561581875039/1925111136";
    public string RewardedId1 = "ca-app-pub-2294561581875039/8478895280";



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
		//ErrorText.text=ErrorText.text+"Awake ";
    }

    public void Start()
    {
		BannerID="ca-app-pub-3940256099942544/6300978111";
		InterstitialID="ca-app-pub-3940256099942544/1033173712";
		RewardedId1 = "ca-app-pub-3940256099942544/5224354917";
        FreeAdsButton.SetActive(false);
        MobileAds.Initialize(initStatus => { });
		
        RequestandShowBanner();
        CreateAndLoadRewardedAd();
        RequestInterstitial();
        InvokeRepeating("ReCheckRewarded", 5f,80f);
    }

    private void RequestandShowBanner()
    {
        this.bannerView = new BannerView(BannerID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
		//ErrorText.text=ErrorText.text+"Banner Requested and Shown ";
    }


    /// ///////////////////////////////////////////////////////////////
    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(InterstitialID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
		//ErrorText.text=ErrorText.text+request+"Interstitial Requested ";
    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
			//ErrorText.text=ErrorText.text+"Inter Shown ";
            Invoke("RequestInterstitial",5f);
        }
		else
		{
			//ErrorText.text=ErrorText.text+"Inter Not Loaded ";
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
		//ErrorText.text=ErrorText.text+"Rewarded Requested ";

    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
       // FreeAdsButton.SetActive(true);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.IncreaseCoin(100);
        newCoinsGetBox.SetActive(true);
        newCoinsGetText.text = "+100";
        FreeAdsButton.SetActive(false);
		//ErrorText.text=ErrorText.text+"Reward Ace4pted ";
    }



    public void ShowRewardedAd()
    {
            this.rewardedAd.Show();
			//ErrorText.text=ErrorText.text+"Rewarded Shown ";

    }

    void ReCheckRewarded()
    {
        if (this.rewardedAd.IsLoaded())
        {
            FreeAdsButton.SetActive(true);
        }
		else
		{
			Invoke("ReCheckRewarded",2f);
		}
    }
    
}

