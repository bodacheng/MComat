using System;
using FightScene;
using UnityEngine;
using UnityEngine.UI;

// 战斗暂停相关。从暂停界面可以跳转至Setting界面，因此两个模块靠OptionsButton连接在一起
public class FightScenePauseSupport : UILayer
{
    private Action resumeAction;
    private Action returnAction;

    [SerializeField] Toggle autoRotateCamera;
    
    #region Sound
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider effectsSoundsSlider;
    #endregion
    
    #region control
    [SerializeField] private EasyKeyBinding.KeyBindingUpdater keyBindingUpdater;
    #endregion
    
    public void Setup(Action runNow, Action returnToFront, Action resumeAction)
    {
        keyBindingUpdater.INI();
        
        this.resumeAction = resumeAction;
        this.returnAction = returnToFront;
        runNow.Invoke();
        ResetSliders();
        
        var c = RTFightManager.Target._CameraManager.GetMode(C_Mode.CertainYAntiVibration);
        var mode = ((ChatGptFix)c);
        autoRotateCamera.onValueChanged.AddListener(x =>
        {
            mode.AutoRotateCamera = x;
        });
        autoRotateCamera.SetIsOnWithoutNotify(mode.AutoRotateCamera);
        autoRotateCamera.gameObject.SetActive(FightLoad.Fight.FightMode is FightMode.Evolve or FightMode.Rotate);
    }

    public void Resume()
    {
        resumeAction.Invoke();
    }

    public void Return()
    {
        returnAction.Invoke();
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
        foreach (var dataCenter in RTFightManager.Target.team1.teamMembers.GetValues())
        {
            dataCenter._AudioSource.volume = AppSetting.Value.EffectsVolume;
        }
        foreach (var dataCenter in RTFightManager.Target.team2.teamMembers.GetValues())
        {
            dataCenter._AudioSource.volume = AppSetting.Value.EffectsVolume;
        }
    }
}