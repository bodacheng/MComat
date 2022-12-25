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
    [SerializeField] ClickNextTutorial clickNextTutorial;
    
    public async UniTask Setup(bool active = true)
    {
        gameObject.SetActive(active && NetFightScene.Fight.EventType != FightEventType.Screensaver);
        await StartUp((x) =>
            {
                PlayerPrefs.SetInt("auto", x ? 1:0);
                RTFightManager.Target.team1.Auto = x;
            },
            (x) =>
            {
                RTFightManager.Target.team2.Auto = x;
            },
            ()=> UILayerLoader.Load<FightScenePauseSupport>()
            );
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
        UILayerLoader.Remove<FightingStepLayer>();
    }

    public void OpenTutorial()
    {
        pauseButton.gameObject.SetActive(false);
        clickNextTutorial.Open();
    }

    public bool Initialized { get; set; } = false;

    async UniTask StartUp(Action<bool> switchTeam1Auto, Action<bool> switchTeam2Auto, Action pauseAction)
    {
        Initialized = false;
        
        RTFightManager.Target.team1.InputsManager = InputsManager;
        RTFightManager.Target.team2.InputsManager = InputsManager;
        
        pauseButton.onClick.AddListener(pauseAction.Invoke);
        
        Team1Auto.Initialize((() => RTFightManager.Target.team1.Auto), switchTeam1Auto);
        Team2Auto.Initialize((() => RTFightManager.Target.team2.Auto), switchTeam2Auto);
        
        team1UI.TeamMode = NetFightScene.Fight.team1Mode;
        team2UI.TeamMode = NetFightScene.Fight.team2Mode;
        
        team1UI.teamConfig = RTFightManager.Target.heroTeamConfig;
        team2UI.teamConfig = RTFightManager.Target.EnemyTeamConfig;
        team1UI.teamConfig.playID = NetFightScene.Fight.Team1ID;
        team2UI.teamConfig.playID = NetFightScene.Fight.Team2ID;
        team1UI.TeamMembers = RTFightManager.Target.team1.teamMembers;
        team2UI.TeamMembers = RTFightManager.Target.team2.teamMembers;
        
        // 角色第二次初始化在这之前已经结束
        team1UI.InsTeamUI(RTFightManager.Target.team1.ReadyForNextMember, RTFightManager.Target.team1.RMode_Unit);
        team2UI.InsTeamUI(RTFightManager.Target.team2.ReadyForNextMember, RTFightManager.Target.team2.RMode_Unit);

        foreach (var d in RTFightManager.Target.team1.teamMembers.GetValues())
        {
            await InputsManager.ElementRegister(d.element, RTFightManager.Target.UnitInfoRef[d]);
        }
        foreach (var d in RTFightManager.Target.team2.teamMembers.GetValues())
        {
            await InputsManager.ElementRegister(d.element, RTFightManager.Target.UnitInfoRef[d]);
        }
        Initialized = true;
    }
}
