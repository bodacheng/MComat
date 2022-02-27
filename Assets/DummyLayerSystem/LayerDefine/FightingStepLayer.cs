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
    public TeamUIManager team1UI;
    public TeamUIManager team2UI;
    
    public static FightingStepLayer target;
    
    public void Refresh()
    {
        if (!RTFightManager.Auto && RTFightManager.focusingUnit != null)
        {
            _C_flag.gameObject.SetActive(true);
            _A_flag.gameObject.SetActive(false);
        }
        else
        {
            _C_flag.gameObject.SetActive(false);
            _A_flag.gameObject.SetActive(true);
        }
        
        team1UI.Refresh(RTFightManager.target.Team1Members, RTFightManager.target.team1.RMode_Unit.Value);
        team2UI.Refresh(RTFightManager.target.Team2Members, RTFightManager.target.team2.RMode_Unit.Value);
        
        if (RTFightManager.focusingUnit == null)
        {
            MobileInputsManager.target.TurnOffButtons();
        }
        else
        {
            MobileInputsManager.target.FocusCharInputs(RTFightManager.focusingUnit._MyBehaviorRunner, RTFightManager.focusingUnit.zokusei);
        }
    }
    
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
    
    void StartUp(Action switchAutoAction, Action pauseAction, Action cameraSwitchAction)
    {
        target = this;
        
        autoBUtton.onClick.AddListener(switchAutoAction.Invoke);
        pauseButton.onClick.AddListener(pauseAction.Invoke);
        cameraSwitchBtn.onClick.AddListener(cameraSwitchAction.Invoke);
        
        team1UI.TeamMode = NetFightScene.Fight.Team1Mode;
        team2UI.TeamMode = NetFightScene.Fight.Team2Mode;
        
        team1UI.teamConfig = RTFightManager.target.heroTeamConfig;
        team2UI.teamConfig = RTFightManager.target.EnemyTeamConfig;
        team1UI.teamConfig.playID = NetFightScene.Fight.team1ID;
        team2UI.teamConfig.playID = NetFightScene.Fight.team2ID;
        
        // 角色第二次初始化在这之前已经结束
        team1UI.InsTeamUI(RTFightManager.target.Team1Members, RTFightManager.target.team1.ReadyForNextMember, RTFightManager.target.team1.RMode_Unit);
        team2UI.InsTeamUI(RTFightManager.target.Team2Members, RTFightManager.target.team2.ReadyForNextMember, RTFightManager.target.team2.RMode_Unit);
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
