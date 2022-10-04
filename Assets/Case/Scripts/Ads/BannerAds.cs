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
    void Start()
    {
        RequestBanner();
    }

    void RequestBanner()
    {
        AdsID = "ca-app-pub-3940256099942544/6300978111";

        bannerWiew = new BannerView(AdsID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerWiew.LoadAd(request);

        bannerWiew.OnAdLoaded += this.HandleOnAdLoaded;
    }



    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        ManagerDownload.ListDownload.names += "Ads loaded" + " \n";
        Debug.Log("Reklam yüklendi");
    }



    void RemoveBanner()
    {
        bannerWiew.Destroy();
    }


}