using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {

    public Button OpenSetting,CloseSetting;
    public Canvas SettingCanvas;
    public RectTransform SettingMenuT;
    public AudioSource bgmSource;
    public Slider bgmSLider,CVSlider,effectsSoundsSlider;
    
    public static Setting target;
    
    void Awake()
    {
        target = this;
        //牵扯到一个初始值问题。
        if (bgmSLider)
            onBgmChange();
        if (CVSlider)
            onCVsChange();
        if (effectsSoundsSlider)
            onEffectsSoundChange();

        void Open()
        {
            SettingCanvas.gameObject.SetActive(true);
            SettingCanvas.sortingOrder = 1;
            LoadingCanvas.target.HigtLightRect(SettingMenuT);
        }
        OpenSetting.onClick.AddListener(Open);
        
        void Close()
        {
            SettingCanvas.sortingOrder = 0;
            LoadingCanvas.target.ClearHigtLight();
            SettingCanvas.gameObject.SetActive(false);
        }
        CloseSetting.onClick.AddListener(Close);
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