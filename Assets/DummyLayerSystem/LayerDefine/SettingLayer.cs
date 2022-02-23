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

    [SerializeField] TextMeshProUGUI ID;
    [SerializeField] InputField EmailInput;
    [SerializeField] Button SendEmail;
    #endregion
    
    void Initialise()
    {
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

        ID.text = PlayerAccountInfo.Me.PlayFabUsername;
        
        SendEmail.onClick.AddListener(() =>
        {
            PlayFabReadClient.SendPwResetEmail(EmailInput.text);
        });
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