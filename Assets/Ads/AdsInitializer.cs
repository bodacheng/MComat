using UnityEngine;
//using UnityEngine.Advertisements;
//using GoogleMobileAds.Api;

public class AdsInitializer : MonoBehaviour//, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    
    void Awake()
    {
        // Unity ads
        InitializeAds();
        
        // Google admob
        // MobileAds.Initialize(initStatus =>
        // {
        //     Debug.Log("谷歌广告插件初始化状态："+initStatus);
        // });
    }
 
    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#endif
        
#if UNITY_ANDROID
        _gameId = _androidGameId;
#endif
        //Advertisement.Initialize(_gameId, _testMode, this);
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    // public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    // {
    //     Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    // }
}