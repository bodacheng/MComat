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
    [SerializeField] Button languageBtn;
    [SerializeField] Button nickNameBtn;
    #endregion
    
    #region Panels
    [SerializeField] RectTransform volumePanel;
    [SerializeField] RectTransform accountPanel;
    [SerializeField] RectTransform devicePanel;
    [SerializeField] RectTransform supportPanel;
    [SerializeField] RectTransform languagePanel;
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
    
    #region Support
    [SerializeField] Button chBtn;
    [SerializeField] Button jpBtn;
    [SerializeField] Button enBtn;
    [SerializeField] GameObject selectedIndicator;
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
            languagePanel.gameObject.SetActive(false);
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

        void LanguageIndicator()
        {
            switch (AppSetting.Value.Language)
            {
                case SystemLanguage.English:
                    selectedIndicator.transform.SetParent(enBtn.transform);
                    break;
                case SystemLanguage.Japanese:
                    selectedIndicator.transform.SetParent(jpBtn.transform);
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    selectedIndicator.transform.SetParent(chBtn.transform);
                    break;
            }
            selectedIndicator.transform.localPosition= Vector3.zero;
        }

        async void SetLanguage(SystemLanguage code)
        {
            AppSetting.Value.Language = code;
            LanguageConverterManger.ChangeLanguage();
            await SkillNameTable.LoadSkillNamesFromConfig();
            LanguageIndicator();
        }
        
        languageBtn.onClick.AddListener(
            () =>
            {
                CloseAllPanels();
                languagePanel.gameObject.SetActive(true);
                enBtn.onClick.AddListener(() => { SetLanguage(SystemLanguage.English); });
                jpBtn.onClick.AddListener(() => { SetLanguage(SystemLanguage.Japanese); });
                chBtn.onClick.AddListener(() => { SetLanguage(SystemLanguage.Chinese); });
                SetSelectedFrame(languageBtn.GetComponent<RectTransform>());
            }
        );
        
        LanguageIndicator();
        
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
            Translate.Get("DeviceBindInstruction") : 
            Translate.Get("DeviceNotBindInstruction");
    }
    
    public static void Close()
    {
        AppSetting.Save();
        UILayerLoader.Remove<SettingLayer>();
    }
    
    void ResetSliders()
    {
        effectsSoundsSlider.value = AppSetting.Value.EffectsVolume;
        bgmSlider.value = AppSetting.Value.BgmVolume;
        cvSlider.value = AppSetting.Value.CvVolume;
    }
    
    public void onBgmChange()
    {
        AppSetting.Value.BgmVolume = bgmSlider.value;
    }
    public void onCVsChange()
    {
        AppSetting.Value.CvVolume = cvSlider.value;        
    }
    public void onEffectsSoundChange()
    {
        AppSetting.Value.EffectsVolume = effectsSoundsSlider.value;
    }
}