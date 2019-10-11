using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobControl : MonoBehaviour
{
    private RewardedAd rewardedAd;
    const string rewardedAdId = "ca-app-pub-3940256099942544/5224354917";
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.rewardedAd = new RewardedAd(rewardedAdId);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
}
