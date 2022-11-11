using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class BannerAds : MonoBehaviour
{
    private BannerView bannerWiew;
    string AdsID;
    AdRequest request;

    void Start()
    {
        RequestBanner();
    }


    void RequestBanner()
    {
        AdsID = "ca-app-pub-3940256099942544/6300978111";

        bannerWiew = new BannerView(AdsID, AdSize.Banner, AdPosition.Bottom);

        request = new AdRequest.Builder().Build();

#if UNITY_EDITOR
        bannerWiew.OnAdLoaded += HandleOnAdLoaded;
#endif

        bannerWiew.LoadAd(request);

        bannerWiew.OnAdLoaded += HandleOnAdLoaded;
    }


    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        ManagerDownload.ListDownload.IsAdsLoaded = true;
    }
}