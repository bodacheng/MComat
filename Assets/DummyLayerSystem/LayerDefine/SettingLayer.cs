using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class SettingLayer : UILayer {
    
    [SerializeField] RectTransform SettingMenuT;

    #region Sound
    [SerializeField] Slider bgmSLider;
    [SerializeField] Slider cvSlider;
    [SerializeField] Slider effectsSoundsSlider;
    #endregion

    private PopupLayer _popupLayer;
    
    public void Initialise()
    {
        _popupLayer = PopupLayer.Open(PreScene.target.T);
        _popupLayer.HigtLightRect(SettingMenuT);
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
        ResetSliders();
    }
    
    // 按钮函数，置于Seting面板返回键上
    public void Close()
    {
        AppSetting.Save();
        _popupLayer.ClearHigtLight();
        PopupLayer.Close();
        UILayerLoader.Remove("SettingLayer");
    }
    
    void ResetSliders()
    {
        effectsSoundsSlider.value = AppSetting.value.EffectsVolumn;
        bgmSLider.value = AppSetting.value.BgmVolumn;
        cvSlider.value = AppSetting.value.CvVolumn;
    }
    
    public void onBgmChange()
    {
        AppSetting.value.BgmVolumn = bgmSLider.value;
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