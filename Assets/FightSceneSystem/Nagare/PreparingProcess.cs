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
        RTFightManager.target._CameraManager.Assign_Camera(C_Mode.NULL, null,null);
        CameraManager._camera.transform.position = CameraManager._StartPosRef.transform.position;
        CameraManager._camera.transform.rotation = CameraManager._StartPosRef.transform.rotation;
        Sensor.ClearFightingMember();
        UILayerLoader.Load<ProgressLayer>();
        ProgressLayer.LoadingPercent("loading Essentials", 0.5f);
        
        var tasks = new List<UniTask>
        {
            AddressablesLogic.Essentials(),
            BoundaryControlByGod.target.ChangeBackGround(NetFightScene.Fight.battleGroundID),
            RTFightManager.target.LoadUnits(NetFightScene.Fight),
            EffectsManager.IniEffectsPool("hit_ground", null, 3),
            EffectsManager.IniEffectsPool("wallCrack", null, 3),
            EffectsManager.IniEffectsPool("break_free", null, 3),
            EffectsManager.IniEffectsPool("memberShift", null, 3)
        };
        await UniTask.WhenAll(tasks);
        
        var teamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        RTFightManager.target.heroTeamConfig.playID = NetFightScene.Fight.Team1ID;
        RTFightManager.target.EnemyTeamConfig.playID = NetFightScene.Fight.Team2ID;
        
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.target.heroTeamConfig, RTFightManager.target.team1.TeamMembers.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.target.EnemyTeamConfig, RTFightManager.target.team2.TeamMembers.GetValues());
        FightLogger.value.ReadyToLog(teamMembers);
        
        RTFightManager.target.team1.Auto = NetFightScene.Fight.Team1Auto;
        RTFightManager.target.team2.Auto = NetFightScene.Fight.RunTutorial ? false : NetFightScene.Fight.Team2Auto;
        RTFightManager.target.team1.TeamMode = NetFightScene.Fight.team1Mode;
        RTFightManager.target.team2.TeamMode = NetFightScene.Fight.team2Mode;
        RTFightManager.target.team1.teamConfig = RTFightManager.target.heroTeamConfig;
        RTFightManager.target.team2.teamConfig = RTFightManager.target.EnemyTeamConfig;
        RTFightManager.target.team1.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        RTFightManager.target.team2.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        
        if (NetFightScene.Fight.EventType == FightEventType.Screensaver)
        {
            RTFightManager.target.team1.TurnAllUnitsInvincible(true);
            RTFightManager.target.team2.TurnAllUnitsInvincible(true);
        }else{
            RTFightManager.target.team1.TurnAllUnitsInvincible(FightGlobalSetting._Team1Invincible);
            RTFightManager.target.team2.TurnAllUnitsInvincible(false);
        }
        
        switch (RTFightManager.target.team1.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.target.team1.Initialize_Multi(NetFightScene.Fight.team1HpRate, NetFightScene.Fight.team1CGMode);
                break;
            case TeamMode.Rotation:
                RTFightManager.target.team1.TeamsIni_Rotate(NetFightScene.Fight.team1HpRate, NetFightScene.Fight.team1CGMode);
                break;
        }
        
        switch (RTFightManager.target.team2.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.target.team2.Initialize_Multi(NetFightScene.Fight.team2HpRate, NetFightScene.Fight.team2CGMode);
                break;
            case TeamMode.Rotation:
                Debug.Log("team2 mode:" + NetFightScene.Fight.team2CGMode);
                RTFightManager.target.team2.TeamsIni_Rotate(NetFightScene.Fight.team2HpRate, NetFightScene.Fight.team2CGMode);
                break;
        }
        
        RTFightManager.target.SetGame(NetFightScene.Fight);
        ProgressLayer.LoadingPercent("loading UI", 1f);
        fightingStepLayer = UILayerLoader.Load<FightingStepLayer>();
        await fightingStepLayer.Setup(false);
        
        switch (RTFightManager.target.team1.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.target.team1.ToStartPos_Multi();
                break;
            case TeamMode.Rotation:
                RTFightManager.target.team1.ToStartPos_Rotate();
                break;
        }
        
        switch (RTFightManager.target.team2.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.target.team2.ToStartPos_Multi();
                break;
            case TeamMode.Rotation:
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
               && RTFightManager.target.team1.IfAllUnitsPreparedForBattle()
               && RTFightManager.target.team2.IfAllUnitsPreparedForBattle()
               && (fightingStepLayer != null && fightingStepLayer.Initialized);
    }
}
