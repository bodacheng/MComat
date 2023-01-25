using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;

public class SettingLayer : UILayer
{
    [SerializeField] RectTransform selectedFrame;
    
    #region Btns
    [SerializeField] Button accountBtn;
    [SerializeField] Button volumeBtn;
    [SerializeField] Button deviceBtn;
    [SerializeField] Button supportBtn;
    [SerializeField] Button nickNameBtn;
    #endregion
    
    #region Panels
    [SerializeField] RectTransform volumePanel;
    [SerializeField] RectTransform accountPanel;
    [SerializeField] RectTransform devicePanel;
    [SerializeField] RectTransform supportPanel;
    [SerializeField] RectTransform nickNamePanel;
    #endregion
    
    #region Sound
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider cvSlider;
    [SerializeField] Slider effectsSoundsSlider;
    #endregion

    #region Email
    [SerializeField] RectTransform emailSettingT;
    [SerializeField] RectTransform emailT;
    [SerializeField] InputField CurrentEmail;
    [SerializeField] InputField EmailInput;
    [SerializeField] Button EmailConfirmBtn;
    [SerializeField] Button SendPwResetBtn;
    #endregion

    #region linkDevice
    [SerializeField] Button linkDeviceBtn;
    [SerializeField] Button unLinkDeviceBtn;
    [SerializeField] Text linkInstruction;
    #endregion
    
    #region Support
    [SerializeField] Button privacyBtn;
    [SerializeField] Button contactBtn;
    #endregion
    
    #region nickName
    [SerializeField] Text nickName;
    [SerializeField] Button resetNickNameBtn;
    #endregion

    public void AccountPhase_EmailToBeSet()
    {
        emailSettingT.gameObject.SetActive(true);
        emailT.gameObject.SetActive(false);
        
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
    
    public void AccountPhase_EmailSet()
    {
        emailSettingT.gameObject.SetActive(false);
        emailT.gameObject.SetActive(true);
        
        CurrentEmail.gameObject.SetActive(true);
        CurrentEmail.text = PlayerAccountInfo.Me.Email;
        
        EmailInput.gameObject.SetActive(false);
        EmailConfirmBtn.gameObject.SetActive(false);
        SendPwResetBtn.gameObject.SetActive(true);
        
        SendPwResetBtn.onClick.AddListener(
        () =>
            {
                PlayFabReadClient.SendPwResetEmail(
                    PlayerAccountInfo.Me.Email,
                    () =>
                    {
                        PopupLayer.ArrangeWarnWindow(" Email Sent ");
                    },
                    (x)=>
                    {
                        PopupLayer.ArrangeWarnWindow(x.ErrorMessage);
                    }
                );
            }
        );
    }

    void SetSelectedFrame(RectTransform target)
    {
        selectedFrame.position = target.position;
        selectedFrame.gameObject.SetActive(true);
    }
    
    public void Initialise()
    {
        nickName.text = PlayerAccountInfo.Me.TitleDisplayName;
        CurrentEmail.text = PlayerAccountInfo.Me.PlayFabUserName;
        
        void CloseAllPanels()
        {
            volumePanel.gameObject.SetActive(false);
            accountPanel.gameObject.SetActive(false);
            devicePanel.gameObject.SetActive(false);
            supportPanel.gameObject.SetActive(false);
            nickNamePanel.gameObject.SetActive(false);
        }
        
        volumeBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            volumePanel.gameObject.SetActive(true);
            SetSelectedFrame(volumeBtn.GetComponent<RectTransform>());
        });
        
        accountBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            accountPanel.gameObject.SetActive(true);
            SetSelectedFrame(accountBtn.GetComponent<RectTransform>());
        });
        
        deviceBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            devicePanel.gameObject.SetActive(true);
            SetSelectedFrame(deviceBtn.GetComponent<RectTransform>());
        });
        
        supportBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            supportPanel.gameObject.SetActive(true);
            SetSelectedFrame(supportBtn.GetComponent<RectTransform>());
        });
        
        nickNameBtn.onClick.AddListener(() =>
        {
            CloseAllPanels();
            nickNamePanel.gameObject.SetActive(true);
            SetSelectedFrame(nickNameBtn.GetComponent<RectTransform>());
            resetNickNameBtn.onClick.AddListener(() =>
            {
                SettingPage.SetNickName((x) =>
                {
                    PopupLayer.ArrangeWarnWindow("Nickname Set");
                    nickName.text = x;
                }, true);
            });
        });
        
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
        ResetSliders();
        
        linkDeviceBtn.onClick.AddListener(() =>
            {
                PlayFabReadClient.LinkAccountPopup(RefreshLinkDeviceBtn);
            }
        );
        unLinkDeviceBtn.onClick.AddListener(() =>
            {
                //PlayFabReadClient.UnLinkAccountPopup(RefreshLinkDeviceBtn);
            }
        );
        
        privacyBtn.onClick.AddListener(() =>
        {
            Application.OpenURL("https://hotaru-4.jimdosite.com/");
        });
        
        contactBtn.onClick.AddListener(() =>
        {
            Application.OpenURL("https://hotaru-4.jimdosite.com/%E3%81%8A%E5%95%8F%E3%81%84%E5%90%88%E3%82%8F%E3%81%9B/");
        });
        accountBtn.onClick.Invoke();
    }

    public void RefreshLinkDeviceBtn()
    {
        unLinkDeviceBtn.gameObject.SetActive(PlayerAccountInfo.Me.currentLinkedDeviceId == PlayFabReadClient.CustomId);
        linkDeviceBtn.gameObject.SetActive(PlayerAccountInfo.Me.currentLinkedDeviceId != PlayFabReadClient.CustomId);
        linkInstruction.text = PlayerAccountInfo.Me.currentLinkedDeviceId == PlayFabReadClient.CustomId ? 
            "当前账户已经和设备进行了链接，你可以在程序打开后直接登陆本账户。如果你希望将本设备和其他账户进行绑定，请点击下方按钮。解除当前账户与设备的绑定会使您无法直接登陆目前账户，如果希望保留当前账户，请设置好本账户的邮箱与密码，否则可能造成账户丢失。" 
            : 
            "当前账户没有与当前设备进行绑定。点击下方绑定按钮可以绑定。";
    }
    
    public static void Close()
    {
        AppSetting.Save();
        UILayerLoader.Remove<SettingLayer>();
    }
    
    void ResetSliders()
    {
        effectsSoundsSlider.value = AppSetting.value.EffectsVolume;
        bgmSlider.value = AppSetting.value.BgmVolume;
        cvSlider.value = AppSetting.value.CvVolume;
    }
    
    public void onBgmChange()
    {
        AppSetting.value.BgmVolume = bgmSlider.value;
    }
    public void onCVsChange()
    {
        AppSetting.value.CvVolume = cvSlider.value;        
    }
    public void onEffectsSoundChange()
    {
        AppSetting.value.EffectsVolume = effectsSoundsSlider.value;
    }
}