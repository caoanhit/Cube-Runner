using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialDisplay : MonoBehaviour
{
    public static InterstitialDisplay instance;
    public float spacing;
    private float showTime;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        showTime = spacing;
    }
    public void ShowAd()
    {
        if (Time.time > showTime)
        {
            showTime += spacing;
            Ads.instance.DisplayInerstitial();
        }
    }


}
