using System;
using UnityEngine;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;

public class InterstitialAd : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    bool isAdLoaded = false;
    private int builtTowers = 0;
    public void 
        OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void 
        OnUnityAdsShowStart(string placementId) { }
    public void 
        OnUnityAdsShowClick(string placementId) { }
    public void 
        OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
    public void 
        OnUnityAdsAdLoaded(string placementId) { isAdLoaded = true; }
    public void 
        OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    private void LoadAd()
    {
        Debug.Log("Advertisement has started loading");
        Advertisement.Load(_adUnitId, this);
    }

    private void ShowAd()
    {
        Time.timeScale = 0;
        if (isAdLoaded)
        {
            Debug.Log("The ad is already loaded");
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Debug.Log("The ad is not loaded");
        }
    }
    public void TowerWasBuilt()
    {
        builtTowers++;
        if (builtTowers >= 3)
        {
            builtTowers = 0;
            if (Random.Range(0,100) < 90) ShowAd();
        }
    }

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) 
        {
            _adUnitId = _iOsAdUnitId;
        }
        else
        {
            _adUnitId = _androidAdUnitId;
        }
        AdsInitializer.OnAdsInitialized.AddListener(LoadAd);
        InvokeRepeating(nameof(ShowAd), 5f, 5f);
    }
}
