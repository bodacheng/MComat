using System;
using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class FightingStepLayer : UILayer
{
    [Header("Auto Button")]
    [Space(6)]
    public Button autoBUtton;
    public Image _C_flag;
    public Image _A_flag;
    
    [Header("Pause Button")]
    [Space(6)]
    public Button pauseButton;
    
    [Header("Camera Switch Button")]
    [Space(6)]
    public Button cameraSwitchBtn;
    
    [Header("TeamUIManager")]
    [Space(6)]
    public TeamUIManager TeamUI1Manager;
    public TeamUIManager TeamUI2Manager;
    
    [Header("MobileInputsManager")]
    [Space(6)]
    public MobileInputsManager MobileInputsManager;
    
    public static FightingStepLayer target;
    
    public void StartUp(Action autoAction, Action pauseAction, Action cameraSwitchAction)
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
