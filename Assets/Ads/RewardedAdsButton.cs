using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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
        _showAdButton.interactable = false;
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
        Advertisement.Load(_adUnitId, this);
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
        // // Disable the button:
        _showAdButton.interactable = false;
        // // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            watchedAdExtraProcess.Invoke();
            // Load another ad: 需要检查在实机上这里跑的是否有问题。在editor上产生一个造成广告再次观看时连续跑了两次的错误
            if (reloadAfterWatched)
                Advertisement.Load(_adUnitId, this);
            else
            {
                _showAdButton.gameObject.SetActive(false);
            }
                
        }
        AppSetting.Value.UnMute();
    }
    
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        Advertisement.Load(_adUnitId, this);
    }
    
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        AppSetting.Value.UnMute();
        // Use the error details to determine whether to try to load another ad.
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        AppSetting.Value.Mute();
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        watchedAdExtraProcess.Invoke();
        // Load another ad: 需要检查在实机上这里跑的是否有问题。在editor上产生一个造成广告再次观看时连续跑了两次的错误
        if (reloadAfterWatched)
            Advertisement.Load(_adUnitId, this);
    }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}