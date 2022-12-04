using FightScene;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UniRx;
using UnityEngine;

public class PreparingProcess : FSceneProcess
{
    private FightingStepLayer fightingStepLayer;
    
    public PreparingProcess()
    {
        Step = SceneStep.Preparing;
        nextProcessStep = SceneStep.CountDown;
    }
    
    async UniTask EnterProcess()
    {
        RTFightManager.Target._CameraManager.Assign_Camera(C_Mode.NULL, null,null);
        CameraManager._camera.transform.position = CameraManager._StartPosRef.transform.position;
        CameraManager._camera.transform.rotation = CameraManager._StartPosRef.transform.rotation;
        Sensor.ClearFightingMember();
        UILayerLoader.Load<ProgressLayer>();
        ProgressLayer.LoadingPercent("loading Essentials", 0.5f);
        
        var tasks = new List<UniTask>
        {
            AddressablesLogic.Essentials(),
            BoundaryControlByGod.target.ChangeBackGround(NetFightScene.Fight.battleGroundID),
            RTFightManager.Target.LoadUnits(NetFightScene.Fight),
            EffectsManager.IniEffectsPool("hit_ground", null, 3),
            EffectsManager.IniEffectsPool("wallCrack", null, 3),
            EffectsManager.IniEffectsPool("break_free", null, 3),
            EffectsManager.IniEffectsPool("memberShift", null, 3)
        };
        await UniTask.WhenAll(tasks);
        
        var teamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        RTFightManager.Target.heroTeamConfig.playID = NetFightScene.Fight.Team1ID;
        RTFightManager.Target.EnemyTeamConfig.playID = NetFightScene.Fight.Team2ID;
        
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.Target.heroTeamConfig, RTFightManager.Target.team1.teamMembers.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.Target.EnemyTeamConfig, RTFightManager.Target.team2.teamMembers.GetValues());
        FightLogger.value.ReadyToLog(teamMembers);
        
        RTFightManager.Target.team1.Auto = NetFightScene.Fight.Team1Auto;
        RTFightManager.Target.team2.Auto = NetFightScene.Fight.RunTutorial ? false : NetFightScene.Fight.Team2Auto;
        RTFightManager.Target.team1.TeamMode = NetFightScene.Fight.team1Mode;
        RTFightManager.Target.team2.TeamMode = NetFightScene.Fight.team2Mode;
        RTFightManager.Target.team1.teamConfig = RTFightManager.Target.heroTeamConfig;
        RTFightManager.Target.team2.teamConfig = RTFightManager.Target.EnemyTeamConfig;
        RTFightManager.Target.team1.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        RTFightManager.Target.team2.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        
        if (NetFightScene.Fight.EventType == FightEventType.Screensaver)
        {
            RTFightManager.Target.team1.TurnAllUnitsInvincible(true);
            RTFightManager.Target.team2.TurnAllUnitsInvincible(true);
        }else{
            RTFightManager.Target.team1.TurnAllUnitsInvincible(FightGlobalSetting._Team1Invincible);
            RTFightManager.Target.team2.TurnAllUnitsInvincible(false);
        }
        
        switch (RTFightManager.Target.team1.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team1.Initialize_Multi(NetFightScene.Fight.team1HpRate, NetFightScene.Fight.team1CGMode, 
                    NetFightScene.Fight.team1AIMode, NetFightScene.Fight.dumbAIDecisionDelay);
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team1.TeamsIni_Rotate(NetFightScene.Fight.team1HpRate, NetFightScene.Fight.team1CGMode, 
                    NetFightScene.Fight.team1AIMode, NetFightScene.Fight.dumbAIDecisionDelay);
                break;
        }
        
        switch (RTFightManager.Target.team2.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team2.Initialize_Multi(NetFightScene.Fight.team2HpRate, NetFightScene.Fight.team2CGMode, 
                    NetFightScene.Fight.team2AIMode, NetFightScene.Fight.dumbAIDecisionDelay);
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team2.TeamsIni_Rotate(NetFightScene.Fight.team2HpRate, NetFightScene.Fight.team2CGMode, 
                    NetFightScene.Fight.team2AIMode, NetFightScene.Fight.dumbAIDecisionDelay);
                break;
        }
        
        RTFightManager.Target.SetGame(NetFightScene.Fight);
        ProgressLayer.LoadingPercent("loading UI", 1f);
        fightingStepLayer = UILayerLoader.Load<FightingStepLayer>();
        await fightingStepLayer.Setup(false);
        
        switch (RTFightManager.Target.team1.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team1.ToStartPos_Multi();
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team1.ToStartPos_Rotate();
                break;
        }
        
        switch (RTFightManager.Target.team2.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team2.ToStartPos_Multi();
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team2.ToStartPos_Rotate();
                break;
        }
        
        RTFightManager.Target.team1.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.Target.CameraAdjustment(RTFightManager.playerTeam);
            }
        ).AddTo(RTFightManager.Target.Disposables);
        
        RTFightManager.Target.team2.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.Target.CameraAdjustment(RTFightManager.playerTeam);
            }
        ).AddTo(RTFightManager.Target.Disposables);
        
        ProgressLayer.Close();
    }
    
    public override void ProcessEnter()
    {
        HighLightLayer.DarkOff(Color.white, 0, true);
        EnterProcess().Forget();
    }
    
    public override void ProcessEnd()
    {
        NetFightScene.target.LoadStageFinished.Value = false;
        HighLightLayer.LightUp(1f);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return NetFightScene.target.LoadStageFinished.Value
               && RTFightManager.Target.team1.IfAllUnitsPreparedForBattle()
               && RTFightManager.Target.team2.IfAllUnitsPreparedForBattle()
               && (fightingStepLayer != null && fightingStepLayer.Initialized);
    }
}
