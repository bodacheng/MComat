using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class SettingLayer : UILayer {
    
    #region Sound
    [SerializeField] Slider bgmSLider;
    [SerializeField] Slider cvSlider;
    [SerializeField] Slider effectsSoundsSlider;
    #endregion
    
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
    
    void Initialise()
    {
        onBgmChange();
        onCVsChange();
        onEffectsSoundChange();
        ResetSliders();
    }
    
    public static void Close()
    {
        AppSetting.Save();
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