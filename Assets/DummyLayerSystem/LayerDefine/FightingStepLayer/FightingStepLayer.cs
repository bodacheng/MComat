using System;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class FightingStepLayer : UILayer
{
    [Header("Pause Button")]
    [SerializeField] Button pauseButton;
    
    [Header("MobileInputsManager")]
    [SerializeField] MobileInputsManager InputsManager;
    
    [Header("TeamUIManager")]
    [SerializeField] TeamUIManager team1UI;
    [SerializeField] TeamUIManager team2UI;
    
    [Header("Auto Button")]
    [SerializeField] AutoSwitch Team1Auto;
    [SerializeField] AutoSwitch Team2Auto;

    [Header("Tutorial")]
    [SerializeField] FightingStepTutorial fightingStepTutorial;
    
    
    public static async UniTask<FightingStepLayer> Open(bool active = true)
    {
        var returnV = UILayerLoader.Get<FightingStepLayer>();
        if (returnV != null)
        {
            return returnV;
        }
        
        var returnValue = UILayerLoader.Load(NetFightScene.target.T.gameObject,"FightingStepLayer") as FightingStepLayer;
        returnValue.gameObject.SetActive(active && NetFightScene.Fight.EventType != FightEventType.Screensaver);
        await returnValue.StartUp((x) =>
            {
                RTFightManager.target.team1.Auto = x;
            },
            (x) =>
            {
                RTFightManager.target.team2.Auto = x;
            },
            ()=> FightScenePauseSupport.Open(NetFightScene.target.T.gameObject)
        );
        
        return returnValue;
    }
    
    public static void Close()
    {
        var layer = UILayerLoader.Get<FightingStepLayer>();
        if (layer == null)
            return;
        layer.InputsManager.FocusUnit(null);
        layer.InputsManager.Clear();
        layer.team1UI.Clear();
        layer.team2UI.Clear();
        UILayerLoader.Remove("FightingStepLayer");
    }

    public void OpenTutorial()
    {
        pauseButton.gameObject.SetActive(false);
        fightingStepTutorial.Open();
    }

    public bool Initialized { get; set; } = false;

    async UniTask StartUp(Action<bool> switchTeam1Auto, Action<bool> switchTeam2Auto, Action pauseAction)
    {
        Initialized = false;
        
        RTFightManager.target.team1.InputsManager = InputsManager;
        RTFightManager.target.team2.InputsManager = InputsManager;
        
        pauseButton.onClick.AddListener(pauseAction.Invoke);
        
        Team1Auto.Initialize((() => RTFightManager.target.team1.Auto), switchTeam1Auto);
        Team2Auto.Initialize((() => RTFightManager.target.team2.Auto), switchTeam2Auto);
        
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

        foreach (var d in RTFightManager.target.team1.TeamMembers.GetValues())
        {
            await InputsManager.ElementRegister(d.element, RTFightManager.target.UnitInfoRef[d]);
        }
        foreach (var d in RTFightManager.target.team2.TeamMembers.GetValues())
        {
            await InputsManager.ElementRegister(d.element, RTFightManager.target.UnitInfoRef[d]);
        }
        Initialized = true;
    }
}
