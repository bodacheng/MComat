using FightScene;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

public class PreparingProcess : FSceneProcess
{
    private FightingStepLayer fightingStepLayer;
    
    public PreparingProcess()
    {
        Step = SceneStep.Preparing;
        nextProcessStep = SceneStep.StoryBeforeFight;
    }
    
    async UniTask EnterProcess()
    {
        RTFightManager.target._CameraManager.Assign_Camera(C_Mode.NULL, null,null);
        CameraManager._camera.transform.position = CameraManager._StartPosRef.transform.position;
        CameraManager._camera.transform.rotation = CameraManager._StartPosRef.transform.rotation;
        Sensor.ClearFightingMember();
        PopupLayer.LoadingPercent("loading Essentials", NetFightScene.target.T.gameObject, 0.1f);
        await AddressablesLogic.Essentials();
        PopupLayer.LoadingPercent("loading BattleGround", NetFightScene.target.T.gameObject, 0.3f);
        await BoundaryControlByGod.target.ChangeBackGround(NetFightScene.Fight.BattleGroundID);
        PopupLayer.LoadingPercent("loading Units", NetFightScene.target.T.gameObject, 0.5f);
        await RTFightManager.target.LoadUnits(NetFightScene.Fight);
        
        var teamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.target.heroTeamConfig, RTFightManager.target.team1.TeamMembers.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.target.EnemyTeamConfig, RTFightManager.target.team2.TeamMembers.GetValues());
        FightLogger.value.ReadyToLog(teamMembers);
        
        PopupLayer.LoadingPercent("loading Effects", NetFightScene.target.T.gameObject, 0.5f);
        await EffectsManager.INIEffectsPool("hit_ground", null, 3);
        await EffectsManager.INIEffectsPool("wallCrack", null, 3);
        
        RTFightManager.target.team1.Auto = NetFightScene.Fight.team1Auto;
        RTFightManager.target.team2.Auto = NetFightScene.Fight.team2Auto;
        RTFightManager.target.team1.TeamMode = NetFightScene.Fight.Team1Mode;
        RTFightManager.target.team2.TeamMode = NetFightScene.Fight.Team2Mode;
        RTFightManager.target.team1.teamConfig = RTFightManager.target.heroTeamConfig;
        RTFightManager.target.team2.teamConfig = RTFightManager.target.EnemyTeamConfig;
        RTFightManager.target.team1.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        RTFightManager.target.team2.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        
        if (NetFightScene.Fight.EventType == FightEventType.Screensaver)
        {
            RTFightManager.target.team1.TurnAllUnitsInvincible(true);
            RTFightManager.target.team2.TurnAllUnitsInvincible(true);
        }else{
            RTFightManager.target.team1.TurnAllUnitsInvincible(false);
            RTFightManager.target.team2.TurnAllUnitsInvincible(false);
        }
        
        switch (RTFightManager.target.team1.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team1.Initialize_Multi(NetFightScene.Fight.Team1HpRate, NetFightScene.Fight.team1CGMode);
                break;
            case TeamMode.rotation:
                RTFightManager.target.team1.TeamsIni_Rotate(NetFightScene.Fight.Team1HpRate, NetFightScene.Fight.team1CGMode);
                break;
        }
        
        switch (RTFightManager.target.team2.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team2.Initialize_Multi(NetFightScene.Fight.Team2HpRate, NetFightScene.Fight.team2CGMode);
                break;
            case TeamMode.rotation:
                RTFightManager.target.team2.TeamsIni_Rotate(NetFightScene.Fight.Team2HpRate, NetFightScene.Fight.team2CGMode);
                break;
        }
        
        RTFightManager.target.SetGame(NetFightScene.Fight);
        PopupLayer.LoadingPercent("loading UI", NetFightScene.target.T.gameObject, 1f);
        fightingStepLayer = await FightingStepLayer.Open(false);
        
        switch (RTFightManager.target.team1.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team1.ToStartPos_Multi();
                break;
            case TeamMode.rotation:
                RTFightManager.target.team1.ToStartPos_Rotate();
                break;
        }
        
        switch (RTFightManager.target.team2.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team2.ToStartPos_Multi();
                break;
            case TeamMode.rotation:
                RTFightManager.target.team2.ToStartPos_Rotate();
                break;
        }
        
        RTFightManager.target.team1.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
            }
        ).AddTo(RTFightManager.target.disposables);
        
        RTFightManager.target.team2.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
            }
        ).AddTo(RTFightManager.target.disposables);
        
        PopupLayer.Close();
    }
    
    public override void ProcessEnter()
    {
        PopupLayer.DarkOff(NetFightScene.target.T.gameObject, 1, 0);
        EnterProcess().Forget();
    }
    
    public override void ProcessEnd()
    {
        NetFightScene.target.LoadStageFinished.Value = false;
        PopupLayer.LightUp(1f);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return NetFightScene.target.LoadStageFinished.Value
               && RTFightManager.target.team1.IfAllUnitsPreparedForBattle()
               && RTFightManager.target.team2.IfAllUnitsPreparedForBattle()
               && (fightingStepLayer != null && fightingStepLayer.Initialized);
    }
}
