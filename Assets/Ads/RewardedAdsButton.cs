using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
 
public class RewardedAdsButton : MonoBehaviour
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    [SerializeField] bool reloadAfterWatched;
    [SerializeField] private Text text;
    string _adUnitId = null; // This will remain null for unsupported platforms

    private Func<bool> extraEnableCondition;
    private Action watchedAdExtraProcess;

    public String Text
    {
        set => text.text = value;
    }
    
    public void SetExtraEnableCondition(Func<bool> extraEnableCondition)
    {
        this.extraEnableCondition = extraEnableCondition;
    }

    public void SetWatchedAdExtraProcess(Action watchedAdProcess)
    {
        this.watchedAdExtraProcess = watchedAdProcess;
    }

    public void Enable(bool on)
    {
        _showAdButton.interactable = on;
        _showAdButton.gameObject.SetActive(on);
    }

    void Awake()
    {
        IniUnitId();
        //Disable the button until the ad is ready to show:
        //_showAdButton.interactable = false;
        _showAdButton.onClick.AddListener(ShowAd);
    }

    void IniUnitId()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }
    
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        LoadInterstitialAd();
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = extraEnableCondition != null ? extraEnableCondition() : true;
        }
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        const string rewardMsg =
            "Rewarded interstitial ad rewarded the user. Type: {0}, amount: {1}.";

        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        } 
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }
    
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Rewarded interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content opened.");
            AppSetting.Value.Mute();
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");
            watchedAdExtraProcess.Invoke();
            // Load another ad: 需要检查在实机上这里跑的是否有问题。在editor上产生一个造成广告再次观看时连续跑了两次的错误
            if (reloadAfterWatched)
                LoadAd();
            else
            {
                _showAdButton.gameObject.SetActive(false);
            }
            AppSetting.Value.UnMute();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open " +
                           "full screen content with error : " + error);
        };
    }
    
    public void OnUnityAdsShowStart(string adUnitId)
    {
        AppSetting.Value.Mute();
    }
    
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
    
    // These ad units are configured to always serve test ads.
    
    private InterstitialAd _interstitialAd;

    /// <summary>
    /// Loads the rewarded interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });

        RegisterEventHandlers(_interstitialAd);
    }
}