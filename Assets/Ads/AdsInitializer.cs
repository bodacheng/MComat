using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInitializer : MonoBehaviour
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }
    
    void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#endif
        
#if UNITY_ANDROID
        _gameId = _androidGameId;
#endif
        //Advertisement.Initialize(_gameId, _testMode, this);
        // Google admob
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("谷歌广告插件初始化状态："+initStatus);
        });
    }
}