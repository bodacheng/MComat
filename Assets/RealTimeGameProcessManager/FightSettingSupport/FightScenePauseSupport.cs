using System;
using UnityEngine;
using UnityEngine.UI;
using DummyLayerSystem;

// 战斗暂停相关。从暂停界面可以跳转至Setting界面，因此两个模块靠OptionsButton连接在一起
public class FightScenePauseSupport : UILayer
{
    [Header("暂停菜单里的Resume")]
    public Button resumeButton;
    
    [Header("暂停菜单里的Return")]
    public Button returnButton;
    
    public void Setup(Action runNow, Action returnToFront, Action onClose)
    {
        runNow.Invoke();
        resumeButton.onClick.AddListener(() =>
        {
            onClose.Invoke();
            UILayerLoader.Remove<FightScenePauseSupport>();
        });
        returnButton.onClick.AddListener(returnToFront.Invoke);
    }
}
