using System;
using FightScene;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UniRx;
using UnityEngine;
using Random = System.Random;

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
        var unitInstructionLayer = UILayerLoader.Load<UnitInstructionLayer>(true, null, true);
        unitInstructionLayer.LoadUnitImage();
        
        RTFightManager.Target.team1.Clear();
        RTFightManager.Target.team2.Clear();
        
        if ((FightLoad.Fight.EventType == FightEventType.Quest || FightLoad.Fight.FightMode == FightMode.Group || FightLoad.Fight.EventType == FightEventType.Event))
        {
#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
            FightScene.FightScene.target.LoadAds();
#endif
        }
        
        RTFightManager.Target.Disposables?.Dispose();
        RTFightManager.Target.Disposables = new CompositeDisposable();
        
        Sensor.ClearFightingMember();
        UILayerLoader.Remove<ArenaFightOver>();
        RTFightManager.Target._CameraManager.Assign_Camera(C_Mode.NULL, null,null);
        RTFightManager.Target._CameraManager.SetPosToStart();
        UILayerLoader.Load<ProgressLayer>(true, null, true);
        ProgressLayer.LoadingPercent(Translate.Get("LoadingBattle"), 0.5f);
        
        var effectPreloadCount = FightLoad.Fight.FightMode is (FightMode.Rotate or FightMode.Evolve) ? 
            1 
            :
            Mathf.Max(RTFightManager.Target.team1.teamMembers.GetValues().Count, RTFightManager.Target.team2.teamMembers.GetValues().Count);
        
        var tasks = new List<UniTask>
        {
            AppSetting.PlayBGM(FightLoad.Fight.GetBGMKey()),
            HurtObjectManager.ConstructDPool(),
            AddressablesLogic.Essentials(),
            BoundaryControlByGod.target.ChangeBackGround(FightLoad.Fight.battleGroundID),
            RTFightManager.Target.LoadUnits(FightLoad.Fight),
            EffectsManager.IniEffectsPool(CommonSetting.HitGroundEffectCode, null, effectPreloadCount),
            EffectsManager.IniEffectsPool(CommonSetting.WallCrackEffectCode, null, effectPreloadCount)
        };
        
        IDictionary<Element, int> effectCount = new Dictionary<Element, int>();
        void AddBasicEffectLoadingTask(List<UnitInfo> list)
        {
            foreach (var unit in list)
            {
                var unitConfig = Units.RowToUnitConfigInfo(Units.Find_RECORD_ID(unit.r_id));
                if (!effectCount.TryAdd(unitConfig.element, 1))
                {
                    effectCount[unitConfig.element]++;
                }
            }
            
            foreach (var kv in effectCount)
            {
                tasks.AddRange(
                    new UniTask[]
                    {
                        EffectsManager.IniEffectsPool("light_hit", FightGlobalSetting.EffectPathDefine(kv.Key), kv.Value),
                        EffectsManager.IniEffectsPool("heavy_hit", FightGlobalSetting.EffectPathDefine(kv.Key), kv.Value),
                        EffectsManager.IniEffectsPool("super_hit", FightGlobalSetting.EffectPathDefine(kv.Key), kv.Value),
                        EffectsManager.IniEffectsPool("electric_s_e", FightGlobalSetting.EffectPathDefine(kv.Key), kv.Value),
                        EffectsManager.IniEffectsPool("super_combo_explosion", null, kv.Value),
                        EffectsManager.IniEffectsPool("dream_buff", null, kv.Value)
                    }
                );
            }
        }
        
        AddBasicEffectLoadingTask(FightLoad.Fight.FightMembers.HeroSets.GetValues());
        AddBasicEffectLoadingTask(FightLoad.Fight.FightMembers.EnemySets.GetValues());
        
        tasks.AddRange(
            new UniTask[]
            {
                EffectsManager.IniEffectsPool("super_combo_explosion", null, effectPreloadCount),
                EffectsManager.IniEffectsPool("dream_buff", null, effectPreloadCount)
            }
        );
        
        if ( FightLoad.Fight.FightMode is (FightMode.Rotate or FightMode.Evolve))
        {
            tasks.Add(EffectsManager.IniEffectsPool(CommonSetting.MemberShiftEffectCode, null, 1));
        }
        ProgressLayer.LoadingPercent(Translate.Get("LoadingBattle"), 0.7f);
        await UniTask.WhenAll(tasks);
        
        var teamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        RTFightManager.Target.heroTeamConfig.playID = FightLoad.Fight.Team1ID;
        RTFightManager.Target.EnemyTeamConfig.playID = FightLoad.Fight.Team2ID;
        
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.Target.heroTeamConfig, RTFightManager.Target.team1.teamMembers.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.Target.EnemyTeamConfig, RTFightManager.Target.team2.teamMembers.GetValues());
        FightLogger.value.ReadyToLog(teamMembers);
        
        RTFightManager.Target.team1.FightMode = FightLoad.Fight.FightMode;
        RTFightManager.Target.team2.FightMode = FightLoad.Fight.FightMode;
        RTFightManager.Target.team1.teamConfig = RTFightManager.Target.heroTeamConfig;
        RTFightManager.Target.team2.teamConfig = RTFightManager.Target.EnemyTeamConfig;
        RTFightManager.Target.team1.Auto = FightLoad.Fight.Team1Auto;
        RTFightManager.Target.team2.Auto = FightLoad.Fight.RunTutorial ? false : FightLoad.Fight.Team2Auto;
        
        if (FightLoad.Fight.EventType == FightEventType.Screensaver)
        {
            RTFightManager.Target.team1.TurnAllUnitsInvincible(true);
            RTFightManager.Target.team2.TurnAllUnitsInvincible(true);
        }else{
            RTFightManager.Target.team1.TurnAllUnitsInvincible(FightGlobalSetting._Team1Invincible);
            RTFightManager.Target.team2.TurnAllUnitsInvincible(false);
        }
        
        switch (FightLoad.Fight.FightMode)
        {
            case FightMode.Multi:
                RTFightManager.Target.team1.InitializeMulti(
                    FightLoad.Fight.team1HpRate, FightLoad.Fight.team1CGMode,
                    FightLoad.Fight.team1AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(FightGlobalSetting._player1DreamComboAIRateNumM));
                RTFightManager.Target.team2.InitializeMulti(
                    FightLoad.Fight.team2HpRate, FightLoad.Fight.team2CGMode,
                    FightLoad.Fight.team2AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(
                        FightLoad.Fight.EventType == FightEventType.Arena ? 
                            FightGlobalSetting.ArenaEnemyDreamComboAIRate : FightLoad.Fight.dreamComboAIRateNum));
                break;
            case FightMode.Group:
                RTFightManager.Target.team1.InitializeMulti(
                    FightLoad.Fight.team1HpRate, FightLoad.Fight.team1CGMode,
                    FightLoad.Fight.team1AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(100));
                RTFightManager.Target.team2.InitializeMulti(
                    FightLoad.Fight.team2HpRate, FightLoad.Fight.team2CGMode, 
                    FightLoad.Fight.team2AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(100));
                break;
            case FightMode.Rotate:
                RTFightManager.Target.team1.TeamsIniRotate(
                    FightLoad.Fight.team1HpRate, FightLoad.Fight.team1CGMode, 
                    FightLoad.Fight.team1AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(FightGlobalSetting._player1DreamComboAIRateNumM));
                RTFightManager.Target.team2.TeamsIniRotate(
                    FightLoad.Fight.team2HpRate, FightLoad.Fight.team2CGMode, 
                    FightLoad.Fight.team2AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(FightLoad.Fight.EventType == FightEventType.Arena ? 
                        FightGlobalSetting.ArenaEnemyDreamComboAIRate: FightLoad.Fight.dreamComboAIRateNum));
                break;
            case FightMode.Evolve:
                RTFightManager.Target.team1.TeamsIniRotate(
                    FightLoad.Fight.team1HpRate, FightLoad.Fight.team1CGMode, 
                    FightLoad.Fight.team1AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(FightGlobalSetting._player1DreamComboAIRateNumM));
                RTFightManager.Target.team2.TeamsIniRotate(
                    FightLoad.Fight.team2HpRate, FightLoad.Fight.team2CGMode, 
                    FightLoad.Fight.team2AIMode, FightLoad.Fight.dumbAIDecisionDelay,
                    CreateRandomBoolFunc(FightLoad.Fight.EventType == FightEventType.Arena ? 
                        FightGlobalSetting.ArenaEnemyDreamComboAIRate: FightLoad.Fight.dreamComboAIRateNum), 
                    true);
                break;
        }
        
        RTFightManager.Target.SetGame(FightLoad.Fight);
        ProgressLayer.LoadingPercent(Translate.Get("LoadingBattleAboutToEnd"), 0.8f);
        fightingStepLayer = FightingStepLayer.Open();
        await fightingStepLayer.Setup(false);
        ProgressLayer.LoadingPercent(Translate.Get("LoadingBattleAboutToEnd"), 1f);
        
        switch (FightLoad.Fight.FightMode)
        {
            case FightMode.Multi:
            case FightMode.Group:
                RTFightManager.Target.team1.ToStartPosMulti();
                RTFightManager.Target.team2.ToStartPosMulti();
                break;
            case FightMode.Rotate:
            case FightMode.Evolve:
                RTFightManager.Target.team1.ToStartPosRotate();
                RTFightManager.Target.team2.ToStartPosRotate();
                break;
        }
        
        RTFightManager.Target.team1.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.Target.CameraAdjustment(RTFightManager.playerTeam, RTFightManager.Target.team1.FightMode);
            }
        ).AddTo(RTFightManager.Target.Disposables);
        
        RTFightManager.Target.team2.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.Target.CameraAdjustment(RTFightManager.playerTeam, RTFightManager.Target.team1.FightMode);
            }
        ).AddTo(RTFightManager.Target.Disposables);
        
        ProgressLayer.Close();
    }
    
    public override void ProcessEnter()
    {
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
    
    Func<bool> CreateRandomBoolFunc(int probabilityPercentage)
    {
        // 使用Random类生成随机数
        Random random = new Random();

        // 生成一个介于0到99之间的随机数
        int randomNumber = random.Next(100);

        // 如果随机数小于等于给定的概率百分比，返回true；否则返回false
        return () => randomNumber <= probabilityPercentage;
    }
}
