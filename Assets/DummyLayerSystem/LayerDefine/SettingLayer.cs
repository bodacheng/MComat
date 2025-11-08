using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingLayer : UILayer
{
    [SerializeField] RectTransform selectedFrame;
    
    #region Btns
    [SerializeField] BOButton accountBtn;
    [SerializeField] BOButton volumeBtn;
    [SerializeField] BOButton graphicBtn;
    [SerializeField] BOButton deviceBtn;
    [SerializeField] BOButton supportBtn;
    [SerializeField] BOButton languageBtn;
    [SerializeField] BOButton nickNameBtn;
    [SerializeField] BOButton controlBtn;
    #endregion
    
    #region Panels
    [SerializeField] RectTransform volumePanel;
    [SerializeField] RectTransform graphicPanel;
    [SerializeField] RectTransform accountPanel;
    [SerializeField] RectTransform devicePanel;
    [SerializeField] RectTransform supportPanel;
    [SerializeField] RectTransform languagePanel;
    [SerializeField] RectTransform nickNamePanel;
    [SerializeField] RectTransform controlPanel;
    #endregion
    
    #region Sound
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider effectsSoundsSlider;
    #endregion

    #region PlayFab Id
    [SerializeField] Text playFabId;
    #endregion

    #region Email
    [SerializeField] RectTransform emailSettingT;
    [SerializeField] RectTransform emailT;
    [SerializeField] InputField CurrentEmail;
    [SerializeField] InputField EmailInput;
    [SerializeField] BOButton EmailConfirmBtn;
    [SerializeField] BOButton SendPwResetBtn;
    [SerializeField] BOButton deleteAccountBtn;
    #endregion

    #region linkDevice
    [SerializeField] BOButton linkDeviceBtn;
    [SerializeField] BOButton unLinkDeviceBtn;
    [SerializeField] Text linkInstruction;
    #endregion
    
    #region Support
    [SerializeField] BOButton privacyBtn;
    [SerializeField] BOButton contactBtn;
    #endregion
    
    #region Support
    [SerializeField] BOButton chBtn;
    [SerializeField] BOButton jpBtn;
    [SerializeField] BOButton enBtn;
    [SerializeField] GameObject selectedIndicator;
    #endregion
    
    #region nickName
    [SerializeField] Text nickName;
    [SerializeField] BOButton resetNickNameBtn;
    #endregion
    
    #region Graphics
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] List<ResolutionOption> resolutionOptions;
    
    [System.Serializable]
    public struct ResolutionOption
    {
        public int width;
        public int height;
        public Toggle toggle;

        public void Toggle(bool isOn)
        {
            
            if (isOn)
            {
#if !UNITY_IOS && !UNITY_ANDROID
                Screen.SetResolution(width, height, Screen.fullScreen);
#endif
            }
        }
    }
    #endregion
    
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    
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
        playFabId.text = PlayerAccountInfo.Me.PlayFabId;
        
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
        RefreshNickNameDisplay();
        CurrentEmail.text = PlayerAccountInfo.Me.PlayFabUserName;
        
        void CloseAllPanels()
        {
            volumePanel.gameObject.SetActive(false);
            accountPanel.gameObject.SetActive(false);
            devicePanel.gameObject.SetActive(false);
            supportPanel.gameObject.SetActive(false);
            nickNamePanel.gameObject.SetActive(false);
            languagePanel.gameObject.SetActive(false);
            controlPanel.gameObject.SetActive(false);
            if (CommonSetting.PcMode)
                graphicPanel.gameObject.SetActive(false);
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

        if (CommonSetting.PcMode)
        {
            graphicBtn.onClick.AddListener(() =>
            {
                CloseAllPanels();
                graphicPanel.gameObject.SetActive(true);
                SetSelectedFrame(graphicBtn.GetComponent<RectTransform>());
            });
            
            // 获取当前屏幕设置
            int currentWidth = Screen.width;
            int currentHeight = Screen.height;
            bool isFullscreen = Screen.fullScreen;

            // 设置全屏 Toggle 的初始状态
            fullScreenToggle.SetIsOnWithoutNotify(isFullscreen);

            // 根据当前分辨率设置对应的 Toggle
            foreach (var option in resolutionOptions)
            {
#if !UNITY_IOS && !UNITY_ANDROID
                option.toggle.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        Screen.SetResolution(option.width, option.height, Screen.fullScreen);
                    }
                });
#endif
                if (option.width == currentWidth && option.height == currentHeight)
                {
                    option.toggle.SetIsOnWithoutNotify(true);
                }
                else
                {
                    option.toggle.SetIsOnWithoutNotify(false);
                }
            }
        }
        
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
            SkillConfigTable.RefreshSkillConfigDicForReference();
            LanguageIndicator();
            RefreshNickNameDisplay();
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
        
        nickNameBtn.SetListener(
            () =>
            {
                CloseAllPanels();
                nickNamePanel.gameObject.SetActive(true);
                SetSelectedFrame(nickNameBtn.GetComponent<RectTransform>());
            }
        );

        resetNickNameBtn.SetListener(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.Rename);
        });
        
        controlBtn.onClick.AddListener(
            () =>
            {
                CloseAllPanels();
                controlPanel.gameObject.SetActive(true);
                SetSelectedFrame(controlBtn.GetComponent<RectTransform>());
            }
        );
        
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
            Application.OpenURL("https://mugencombat.webnode.jp/purofiru/");
        });
        
        contactBtn.onClick.AddListener(() =>
        {
            Application.OpenURL("https://mugencombat.webnode.jp/o-weni-hewase/");
        });
        accountBtn.onClick.Invoke();
        
        deleteAccountBtn.SetListener(() =>
        {
            PlayFabReadClient.DeleteAccountPopup(() =>
            {
                ReturnLayer.ReturnMissionList.Clear();
                PopupLayer.ArrangeWarnWindow(
                    () =>
                    {
                        SceneManager.LoadScene(0);
                    },
                    Translate.Get("AccountDeleted")
                );
            });
        });
    }

    public void RefreshLinkDeviceBtn()
    {
        unLinkDeviceBtn.gameObject.SetActive(PlayerAccountInfo.Me.currentLinkedDeviceId == PlayFabReadClient.CustomId);
        linkDeviceBtn.gameObject.SetActive(PlayerAccountInfo.Me.currentLinkedDeviceId != PlayFabReadClient.CustomId);
        linkInstruction.text = PlayerAccountInfo.Me.currentLinkedDeviceId == PlayFabReadClient.CustomId ? 
            Translate.Get("DeviceBindInstruction") : 
            Translate.Get("DeviceNotBindInstruction");
    }

    void RefreshNickNameDisplay()
    {
        if (nickName == null || PlayerAccountInfo.Me == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(PlayerAccountInfo.Me.TitleDisplayName))
        {
            nickName.text = GetNickNamePlaceholder();
        }
        else
        {
            nickName.text = PlayerAccountInfo.Me.TitleDisplayName;
        }
    }

    string GetNickNamePlaceholder()
    {
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.Japanese:
                return "右側のボタンでニックネームを設定してください";
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                return "点击右侧按钮设置昵称";
            default:
                return "Tap the button on the right to set your nickname";
        }
    }
    
    public static void Close()
    {
        AppSetting.Save();
        if (CommonSetting.PcMode)
            AppSetting.Value.SaveSettings();
        UILayerLoader.Remove<SettingLayer>();
    }
    
    void ResetSliders()
    {
        effectsSoundsSlider.value = AppSetting.Value.EffectsVolume;
        bgmSlider.value = AppSetting.Value.BgmVolume;
    }
    
    public void OnBgmChange()
    {
        AppSetting.Value.BgmVolume = bgmSlider.value;
    }
    
    public void OnEffectChange()
    {
        AppSetting.Value.EffectsVolume = effectsSoundsSlider.value;
    }
}
