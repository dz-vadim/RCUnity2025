using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _andriodGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _isTestMode;
    [SerializeField] private string _gameId;
    public static UnityEvent OnAdsInitialized = new UnityEvent();
    public void OnInitializationComplete()
    {
        OnAdsInitialized.Invoke();
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization error: {error}. Message: {message}");
    }
    public void InitializeAds()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {_gameId =  _iOSGameId; }
        else {_gameId = _andriodGameId; }

        if (!Advertisement.isInitialized && !Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _isTestMode, this);
        }
    }
    private void Awake()
    {
        InitializeAds();
    }
}
