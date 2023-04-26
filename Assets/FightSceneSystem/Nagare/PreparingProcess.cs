using FightScene;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UniRx;

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
        RTFightManager.Target.ClearUnits();
        Sensor.ClearFightingMember();
        UILayerLoader.Remove<ArenaFightOver>();
        RTFightManager.Target._CameraManager.Assign_Camera(C_Mode.NULL, null,null);
        CameraManager._camera.transform.position = CameraManager._StartPosRef.transform.position;
        CameraManager._camera.transform.rotation = CameraManager._StartPosRef.transform.rotation;
        UILayerLoader.Load<ProgressLayer>();
        ProgressLayer.LoadingPercent("loading Essentials", 0.5f);
        
        var tasks = new List<UniTask>
        {
            AppSetting.PlayBGM(FightScene.FightScene.Fight.GetBGMKey()),
            AddressablesLogic.Essentials(),
            BoundaryControlByGod.target.ChangeBackGround(FightScene.FightScene.Fight.battleGroundID),
            RTFightManager.Target.LoadUnits(FightScene.FightScene.Fight),
            EffectsManager.IniEffectsPool(CommonSetting.HitGroundEffectCode, null, 3),
            EffectsManager.IniEffectsPool(CommonSetting.WallCrackEffectCode, null, 3),
            EffectsManager.IniEffectsPool(CommonSetting.BreakFreeEffectCode, null, 3),
            EffectsManager.IniEffectsPool(CommonSetting.MemberShiftEffectCode, null, 3)
        };
        
        await UniTask.WhenAll(tasks);
        
        var teamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        RTFightManager.Target.heroTeamConfig.playID = FightScene.FightScene.Fight.Team1ID;
        RTFightManager.Target.EnemyTeamConfig.playID = FightScene.FightScene.Fight.Team2ID;
        
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.Target.heroTeamConfig, RTFightManager.Target.team1.teamMembers.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.Target.EnemyTeamConfig, RTFightManager.Target.team2.teamMembers.GetValues());
        FightLogger.value.ReadyToLog(teamMembers);
        
        RTFightManager.Target.team1.TeamMode = FightScene.FightScene.Fight.team1Mode;
        RTFightManager.Target.team2.TeamMode = FightScene.FightScene.Fight.team2Mode;
        RTFightManager.Target.team1.teamConfig = RTFightManager.Target.heroTeamConfig;
        RTFightManager.Target.team2.teamConfig = RTFightManager.Target.EnemyTeamConfig;
        RTFightManager.Target.team1.Auto = FightScene.FightScene.Fight.Team1Auto;
        RTFightManager.Target.team2.Auto = FightScene.FightScene.Fight.RunTutorial ? false : FightScene.FightScene.Fight.Team2Auto;
        
        if (FightScene.FightScene.Fight.EventType == FightEventType.Screensaver)
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
                RTFightManager.Target.team1.InitializeMulti(FightScene.FightScene.Fight.team1HpRate, FightScene.FightScene.Fight.team1CGMode, 
                    FightScene.FightScene.Fight.team1AIMode, FightScene.FightScene.Fight.dumbAIDecisionDelay);
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team1.TeamsIniRotate(FightScene.FightScene.Fight.team1HpRate, FightScene.FightScene.Fight.team1CGMode, 
                    FightScene.FightScene.Fight.team1AIMode, FightScene.FightScene.Fight.dumbAIDecisionDelay);
                break;
        }
        
        switch (RTFightManager.Target.team2.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team2.InitializeMulti(FightScene.FightScene.Fight.team2HpRate, FightScene.FightScene.Fight.team2CGMode, 
                    FightScene.FightScene.Fight.team2AIMode, FightScene.FightScene.Fight.dumbAIDecisionDelay);
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team2.TeamsIniRotate(FightScene.FightScene.Fight.team2HpRate, FightScene.FightScene.Fight.team2CGMode, 
                    FightScene.FightScene.Fight.team2AIMode, FightScene.FightScene.Fight.dumbAIDecisionDelay);
                break;
        }
        
        RTFightManager.Target.SetGame(FightScene.FightScene.Fight);
        ProgressLayer.LoadingPercent("loading UI", 1f);
        fightingStepLayer = UILayerLoader.Load<FightingStepLayer>();
        await fightingStepLayer.Setup(false);
        
        switch (RTFightManager.Target.team1.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team1.ToStartPosMulti();
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team1.ToStartPos_Rotate();
                break;
        }
        
        switch (RTFightManager.Target.team2.TeamMode)
        {
            case TeamMode.MultiRaid:
                RTFightManager.Target.team2.ToStartPosMulti();
                break;
            case TeamMode.Rotation:
                RTFightManager.Target.team2.ToStartPos_Rotate();
                break;
        }
        
        RTFightManager.Target.team1.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.Target.CameraAdjustment(RTFightManager.playerTeam, RTFightManager.Target.team1.TeamMode);
            }
        ).AddTo(RTFightManager.Target.Disposables);
        
        RTFightManager.Target.team2.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.Target.CameraAdjustment(RTFightManager.playerTeam, RTFightManager.Target.team1.TeamMode);
            }
        ).AddTo(RTFightManager.Target.Disposables);
        
        ProgressLayer.Close();
    }
    
    public override void ProcessEnter()
    {
        //HighLightLayer.DarkOff(Color.white, 0, true);
        var unitInstructionLayer = UILayerLoader.Load<UnitInstructionLayer>();
        unitInstructionLayer.LoadUnitImage();
        EnterProcess().Forget();
    }
    
    public override void ProcessEnd()
    {
        FightScene.FightScene.target.LoadStageFinished.Value = false;
        //HighLightLayer.LightUp(1f);
        UILayerLoader.Remove<UnitInstructionLayer>();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return FightScene.FightScene.target.LoadStageFinished.Value
               && RTFightManager.Target.team1.IfAllUnitsPreparedForBattle()
               && RTFightManager.Target.team2.IfAllUnitsPreparedForBattle()
               && (fightingStepLayer != null && fightingStepLayer.Initialized);
    }
}
