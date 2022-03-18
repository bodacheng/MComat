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

    [SerializeField] MobileInputsManager InputsManager;
    
    [Header("TeamUIManager")]
    public TeamUIManager team1UI;
    public TeamUIManager team2UI;
    
    [Header("Auto Button")]
    public Toggle Team1Auto;
    public Toggle Team2Auto;
    
    public static FightingStepLayer target;
    
    public void Refresh()
    {
        team1UI.Refresh(RTFightManager.target.team1.RMode_Unit.Value);
        team2UI.Refresh(RTFightManager.target.team2.RMode_Unit.Value);
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
                RTFightManager.target.team1.Auto = x;
            },
            (x) =>
            {
                RTFightManager.target.team2.Auto = x;
            },
            ()=> FightScenePauseSupport.Open(NetFightScene.target.T.gameObject), 
            RTFightManager.target.SwitchToWatchMode);

        if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
        {
            returnValue.gameObject.SetActive(false);
        }
        
        returnValue.Team1Auto.isOn = RTFightManager.target.team1.Auto;
        returnValue.Team2Auto.isOn = RTFightManager.target.team2.Auto;
        
        return returnValue;
    }
    
    void StartUp(Action<bool> switchTeam1Auto, Action<bool> switchTeam2Auto, Action pauseAction, Action cameraSwitchAction)
    {
        target = this;
        
        InputsManager.ZokuseiButtonRegister(Zokusei.blueMagic);
        InputsManager.ZokuseiButtonRegister(Zokusei.redMagic);
        InputsManager.ZokuseiButtonRegister(Zokusei.greenMagic);
        InputsManager.ZokuseiButtonRegister(Zokusei.darkMagic);
        InputsManager.ZokuseiButtonRegister(Zokusei.lightMagic);
        
        RTFightManager.target.team1.InputsManager = InputsManager;
        RTFightManager.target.team2.InputsManager = InputsManager;
        
        pauseButton.onClick.AddListener(pauseAction.Invoke);
        cameraSwitchBtn.onClick.AddListener(cameraSwitchAction.Invoke);
        
        Team1Auto.onValueChanged.AddListener(switchTeam1Auto.Invoke);
        Team2Auto.onValueChanged.AddListener(switchTeam2Auto.Invoke);
        
        team1UI.TeamMode = NetFightScene.Fight.Team1Mode;
        team2UI.TeamMode = NetFightScene.Fight.Team2Mode;
        
        team1UI.teamConfig = RTFightManager.target.heroTeamConfig;
        team2UI.teamConfig = RTFightManager.target.EnemyTeamConfig;
        team1UI.teamConfig.playID = NetFightScene.Fight.team1ID;
        team2UI.teamConfig.playID = NetFightScene.Fight.team2ID;

        team1UI.TeamMembers = RTFightManager.target.team1.TeamMembers;
        team2UI.TeamMembers = RTFightManager.target.team2.TeamMembers;
        
        // 角色第二次初始化在这之前已经结束
        team1UI.InsTeamUI(RTFightManager.target.team1.ReadyForNextMember, RTFightManager.target.team1.RMode_Unit);
        team2UI.InsTeamUI(RTFightManager.target.team2.ReadyForNextMember, RTFightManager.target.team2.RMode_Unit);
    }
}
