using System;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class FightingStepLayer : UILayer
{
    [Header("Auto Button")]
    public Button autoBUtton;
    public Image _C_flag;
    public Image _A_flag;
    
    [Header("Pause Button")]
    public Button pauseButton;
    
    [Header("Camera Switch Button")]
    public Button cameraSwitchBtn;
    
    [Header("TeamUIManager")]
    public TeamUIManager TeamUI1Manager;
    public TeamUIManager TeamUI2Manager;
    
    public static FightingStepLayer target;
    
    public static FightingStepLayer Get()
    {
        var l = UILayerLoader.Get("FightingStepLayer");
        FightingStepLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as FightingStepLayer;
        }
        return returnValue;
    }
    
    public static FightingStepLayer Open()
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(NetFightScene.target.T.gameObject,"FightingStepLayer") as FightingStepLayer;
        returnValue.StartUp(RTFightManager.target.SwitchAutoMode, 
            ()=> FightScenePauseSupport.Open(NetFightScene.target.T.gameObject), 
            RTFightManager.target.SwitchToWatchMode);

        if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
        {
            returnValue.gameObject.SetActive(false);
        }
        return returnValue;
    }
    
    void StartUp(Action autoAction, Action pauseAction, Action cameraSwitchAction)
    {
        target = this;
        autoBUtton.onClick.AddListener(autoAction.Invoke);
        pauseButton.onClick.AddListener(pauseAction.Invoke);
        cameraSwitchBtn.onClick.AddListener(cameraSwitchAction.Invoke);
    }
    
    public void RefreshAIFlag(bool ai)
    {
        if (!ai)
        {
            _C_flag.gameObject.SetActive(true);
            _A_flag.gameObject.SetActive(false);
        }
        else
        {
            _C_flag.gameObject.SetActive(false);
            _A_flag.gameObject.SetActive(true);
        }
    }
}
