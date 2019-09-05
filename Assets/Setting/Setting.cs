using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//主界面那里应该通过这些指标来决定defaultPools里的设置信息。
public class Setting : MonoBehaviour {

    // 上面这些除了初始界面外应该都是没用的。
    public AudioSource bgmSource;
    public Slider bgmSLider,CVSlider,effectsSoundsSlider;
    
    void Awake()
    {
        //牵扯到一个初始值问题。
        if (bgmSLider)
            onBgmChange();
        if (CVSlider)
            onCVsChange();
        if (effectsSoundsSlider)
            onEffectsSoundChange();
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