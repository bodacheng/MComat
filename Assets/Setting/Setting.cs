using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using mainMenu;

public class Setting : MonoBehaviour {

    public Button OpenSetting, CloseSetting;
    public Canvas SettingCanvas;
    public RectTransform SettingMenuT;
    public AudioSource bgmSource;
    public Slider bgmSLider, CVSlider, effectsSoundsSlider;
    
    public static ApiLanguage Language = ApiLanguage.EnUs;
    public static Setting target;
    
    void Awake()
    {
        target = this;
        
        void Open()
        {
            LoadProgrameSettingFromAccount();
            
            SettingCanvas.gameObject.SetActive(true);
            SettingCanvas.sortingOrder = 1;
            LoadingCanvas.target.HigtLightRect(SettingMenuT);
        }
        OpenSetting.onClick.AddListener(Open);
        
        void Close()
        {
            SaveProgrameSettingToAccount();
            
            SettingCanvas.sortingOrder = 0;
            LoadingCanvas.target.ClearHigtLight();
            SettingCanvas.gameObject.SetActive(false);
        }
        CloseSetting.onClick.AddListener(Close);
    }
    
    void Start()
    {
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
    }
    
    public void LoadProgrameSettingFromAccount()
    {
        bgmSLider.value = AccountSet._AccInfo.BgmVolumn;
        effectsSoundsSlider.value = AccountSet._AccInfo.EffectsVolumn;
        
        bgmSource.volume = bgmSLider.value;
        AudioManager.effectsVolumn = effectsSoundsSlider.value;
    }
    
    public void SaveProgrameSettingToAccount()
    {
        AccountSet._AccInfo.BgmVolumn = bgmSLider.value;
        AccountSet._AccInfo.EffectsVolumn = effectsSoundsSlider.value;
        PreScene.target.mainProcessRunner.Run(AccountSet.SaveCustomerInfo());
    }

    public void onBgmChange()
    {
        bgmSource.volume = bgmSLider.value;
    }
    public void onCVsChange()
    {
        AudioManager.voiceVolumn = CVSlider.value;        
    }
    public void onEffectsSoundChange()
    {
        AudioManager.effectsVolumn = effectsSoundsSlider.value;
    }
}