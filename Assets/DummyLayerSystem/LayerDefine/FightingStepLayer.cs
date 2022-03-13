using System;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class FightingStepLayer : UILayer
{
    [Header("Pause Button")]
    public Button pauseButton;
    
    [Header("Camera Switch Button")]
    public Button cameraSwitchBtn;
    
    [Header("TeamUIManager")]
    public TeamUIManager team1UI;
    public TeamUIManager team2UI;
    
    [Header("Auto Button")]
    public Toggle Team1Auto;
    public Toggle Team2Auto;
    
    public static FightingStepLayer target;
    
    public void Refresh()
    {
        Team1Auto.isOn = RTFightManager.target.team1.Auto;
        Team2Auto.isOn = RTFightManager.target.team2.Auto;
        
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
        returnValue.StartUp((x) =>
            {
                RTFightManager.target.SwitchAuto(RTFightManager.playerTeam == Team.player1 ? Team.player1 : Team.player2, x);
                returnValue.Refresh();
            },
            (x) =>
            {
                RTFightManager.target.SwitchAuto(RTFightManager.playerTeam == Team.player1 ? Team.player2 : Team.player1, x);
                returnValue.Refresh();
            },
            ()=> FightScenePauseSupport.Open(NetFightScene.target.T.gameObject), 
            RTFightManager.target.SwitchToWatchMode);

        if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
        {
            returnValue.gameObject.SetActive(false);
        }
        return returnValue;
    }
    
    void StartUp(Action<bool> switchTeam1Auto, Action<bool> switchTeam2Auto, Action pauseAction, Action cameraSwitchAction)
    {
        target = this;
        
        pauseButton.onClick.AddListener(pauseAction.Invoke);
        cameraSwitchBtn.onClick.AddListener(cameraSwitchAction.Invoke);
        
        Team1Auto.onValueChanged.AddListener((x)=> switchTeam1Auto.Invoke(x));
        Team2Auto.onValueChanged.AddListener((x)=> switchTeam2Auto.Invoke(x));
        
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
}
