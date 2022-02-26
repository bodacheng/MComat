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
        
        team1UI.Refresh(RTFightManager.target.Team1Members);
        team2UI.Refresh(RTFightManager.target.Team2Members);

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
        
        team1UI.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        team2UI.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        team1UI.TeamMode = NetFightScene.Fight.Team1Mode;
        team2UI.TeamMode = NetFightScene.Fight.Team2Mode;
        
        team1UI.teamConfig = RTFightManager.target.heroTeamConfig;
        team2UI.teamConfig = RTFightManager.target.EnemyTeamConfig;
        team1UI.teamConfig.playID = NetFightScene.Fight.team1ID;
        team2UI.teamConfig.playID = NetFightScene.Fight.team2ID;
            
        // 角色第二次初始化在这之前已经结束
            
        team1UI.InsTeamUI(RTFightManager.target.Team1Members);
        team2UI.InsTeamUI(RTFightManager.target.Team2Members);
            
        team1UI.TeamsInit(RTFightManager.target.Team1Members, NetFightScene.Fight.Team1HpRate ,NetFightScene.Fight.team1CGMode);
        team2UI.TeamsInit(RTFightManager.target.Team2Members, NetFightScene.Fight.Team2HpRate ,NetFightScene.Fight.team2CGMode);
            
        if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
        {
            team1UI.TurnAllMembersInvincible(true, RTFightManager.target.Team1Members);
            team2UI.TurnAllMembersInvincible(true, RTFightManager.target.Team2Members);
        }else{
            team1UI.TurnAllMembersInvincible(false, RTFightManager.target.Team1Members);
            team2UI.TurnAllMembersInvincible(false, RTFightManager.target.Team2Members);
        }
            
        switch (team1UI.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team1StartUnit = team1UI.ToStartPos_Multi(RTFightManager.target.Team1Members);
                break;
            case TeamMode.rotation:
                RTFightManager.target.team1StartUnit = team1UI.ToStartPos_Rotate(RTFightManager.target.Team1Members);
                break;
        }
            
        switch (team2UI.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team2StartUnit = team2UI.ToStartPos_Multi(RTFightManager.target.Team2Members);
                break;
            case TeamMode.rotation:
                RTFightManager.target.team2StartUnit = team2UI.ToStartPos_Rotate(RTFightManager.target.Team2Members);
                break;
        }
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
