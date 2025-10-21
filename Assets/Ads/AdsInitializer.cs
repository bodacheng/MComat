using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInitializer : MonoBehaviour
{
    private string _gameId;

#if UNITY_IOS || UNITY_ANDROID
    void Awake()
    {
        InitializeAds();
    }
#endif
    
    void InitializeAds()
    {
        // Google admob
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("谷歌广告插件初始化状态："+initStatus);
        });
    }

    public static bool ShouldEnableAds()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                return false;
            default:
                return true;
        }
    }
}
