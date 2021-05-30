using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using mainMenu;

public class Setting : MonoBehaviour {

    public Canvas SettingCanvas;
    public RectTransform SettingMenuT;
    public AudioSource bgmSource;
    public Slider bgmSLider, CVSlider, effectsSoundsSlider;
    
    public static ApiLanguage Language = ApiLanguage.JaJp;
    public static Setting target;
    
    void Awake()
    {
        target = this;
        LoadAppSetting();
    }
    
    // 打开Setting面板。战斗界面置于暂停按钮的option按钮上。主界面直接置于上方悬挂按钮的option按钮上
    public void Open()
    {
        SettingCanvas.gameObject.SetActive(true);
        SettingCanvas.sortingOrder = 1;
        LoadingCanvas.target.HigtLightRect(SettingMenuT);
    }
    
    // 按钮函数，置于Seting面板返回键上
    public void Close()
    {
        SaveAppSetting();
        SettingCanvas.sortingOrder = 0;
        LoadingCanvas.target.ClearHigtLight();
        SettingCanvas.gameObject.SetActive(false);
    }
    
    void Start()
    {
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
    }
    
    public void LoadAppSetting()
    {
        bgmSLider.value = AppSetting.value.BgmVolumn;
        effectsSoundsSlider.value = AppSetting.value.EffectsVolumn;
        bgmSource.volume = bgmSLider.value;
        AudioManager.effectsVolumn = effectsSoundsSlider.value;
    }
    
    void SaveAppSetting()
    {
        AppSetting.value.BgmVolumn = bgmSLider.value;
        AppSetting.value.EffectsVolumn = effectsSoundsSlider.value;
        AppSetting.Save();
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