using DummyLayerSystem;
using mainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingLayer : UILayer {
    
    #region Btns
    [SerializeField] Button volumeBtn;
    [SerializeField] Button accountBtn;
    [SerializeField] Button supportBtn;
    #endregion
    
    #region Panels
    [SerializeField] RectTransform volumePanel;
    [SerializeField] RectTransform accountPanel;
    [SerializeField] RectTransform supportPanel;
    #endregion
    
    #region Sound
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider cvSlider;
    [SerializeField] Slider effectsSoundsSlider;
    #endregion

    #region Email
    [SerializeField] TextMeshProUGUI CurrentEmail;
    [SerializeField] InputField EmailInput;
    [SerializeField] Button EmailConfirmBtn;
    [SerializeField] Button SendPwResetBtn;
    #endregion

    #region linkDevice
    [SerializeField] private Button linkDeviceBtn;
    #endregion

    void AccountPhase_EmailToBeSet()
    {
        CurrentEmail.gameObject.SetActive(false);
        EmailInput.gameObject.SetActive(true);
        EmailConfirmBtn.gameObject.SetActive(true);
        SendPwResetBtn.gameObject.SetActive(false);
        
        EmailConfirmBtn.onClick.RemoveAllListeners();
        EmailConfirmBtn.onClick.AddListener(() =>
        {
            if (PlayerAccountInfo.Me.PlayFabUserName == null)
            {
                PlayFabReadClient.AddUserNameAndEmail(
                    PlayerAccountInfo.Me.PlayFabId, 
                    EmailInput.text.Trim(),
                    AccountPhase_EmailSet
                ); // 这个方法没有server版，只能客户端主动执行
            }
        });
    }
    
    void AccountPhase_EmailSet()
    {
        CurrentEmail.gameObject.SetActive(true);
        CurrentEmail.text = PlayerAccountInfo.Me.Email;
        
        EmailInput.gameObject.SetActive(false);
        EmailConfirmBtn.gameObject.SetActive(false);
        SendPwResetBtn.gameObject.SetActive(true);
        
        SendPwResetBtn.onClick.AddListener(
        () =>
            {
                PlayFabReadClient.SendPwResetEmail(PlayerAccountInfo.Me.Email);
            }
        );
    }
    
    void Initialise()
    {
        CurrentEmail.text = PlayerAccountInfo.Me.PlayFabUserName;
        
        void CloseAllPanels()
        {
            volumePanel.gameObject.SetActive(false);
            accountPanel.gameObject.SetActive(false);
            supportPanel.gameObject.SetActive(false);
        }
        
        volumeBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            volumePanel.gameObject.SetActive(true);
        });
        
        accountBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            accountPanel.gameObject.SetActive(true);
            
            PlayFabReadClient.GetAccountInfo(
                PlayerAccountInfo.Me.PlayFabId,
                () =>
                {
                    if (PlayerAccountInfo.Me.Email != null)
                    {
                        AccountPhase_EmailSet();
                    }
                    else
                    {
                        AccountPhase_EmailToBeSet();
                    }
                }
            );
        });
        
        supportBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            supportPanel.gameObject.SetActive(true);
        });
        
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
        ResetSliders();
        
        linkDeviceBtn.onClick.AddListener(() =>
            {
                PlayFabReadClient.LinkAccountPopup(gameObject);
            }
        );
    }
    
    static SettingLayer Get()
    {
        var l = UILayerLoader.Get("SettingLayer");
        SettingLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as SettingLayer;
        }
        return returnValue;
    }
    
    public static SettingLayer Open()
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(PreScene.target.T,"SettingLayer") as SettingLayer;
        returnValue.Initialise();
        return returnValue;
    }
    
    public static void Close()
    {
        AppSetting.Save();
        UILayerLoader.Remove("SettingLayer");
    }
    
    void ResetSliders()
    {
        effectsSoundsSlider.value = AppSetting.value.EffectsVolumn;
        bgmSlider.value = AppSetting.value.BgmVolumn;
        cvSlider.value = AppSetting.value.CvVolumn;
    }
    
    public void onBgmChange()
    {
        AppSetting.value.BgmVolumn = bgmSlider.value;
    }
    public void onCVsChange()
    {
        AppSetting.value.CvVolumn = cvSlider.value;        
    }
    public void onEffectsSoundChange()
    {
        AppSetting.value.EffectsVolumn = effectsSoundsSlider.value;
    }
}