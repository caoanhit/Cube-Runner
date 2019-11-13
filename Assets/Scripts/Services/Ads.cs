using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour
{
    public static Ads instance;
    private InterstitialAd interstitial;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        MobileAds.Initialize(initstatus => { });
        RequestInerstitial();
    }

    private void RequestInerstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/8691691433";

        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void DisplayInerstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
}
