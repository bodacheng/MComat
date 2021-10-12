using UnityEngine;
using UnityEngine.UI;

public class SettingLayer : UILayer {
    
    public RectTransform SettingMenuT;
    public Slider bgmSLider, CVSlider, effectsSoundsSlider;
    
    public static ApiLanguage Language = ApiLanguage.JaJp;
    
    public void Initialise()
    {
        LoadingCanvas.target.HigtLightRect(SettingMenuT);
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
        ResetSliders();
    }
    
    // 按钮函数，置于Seting面板返回键上
    public void Close()
    {
        AppSetting.Save();
        LoadingCanvas.target.ClearHigtLight();
        UILayerLoader.Remove("SettingLayer");
    }
    
    void ResetSliders()
    {
        effectsSoundsSlider.value = AppSetting.value.EffectsVolumn;
        bgmSLider.value = AppSetting.value.BgmVolumn;
        CVSlider.value = AppSetting.value.CvVolumn;
    }
    
    public void onBgmChange()
    {
        AppSetting.value.BgmVolumn = bgmSLider.value;
    }
    public void onCVsChange()
    {
        AppSetting.value.CvVolumn = CVSlider.value;        
    }
    public void onEffectsSoundChange()
    {
        AppSetting.value.EffectsVolumn = effectsSoundsSlider.value;
    }
}