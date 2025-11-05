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

    static readonly Dictionary<Element, int> elementCountBuffer = new Dictionary<Element, int>(8);
    static readonly List<UniTask> taskBuffer = new List<UniTask>(16);
    static readonly string[] elementEffectKeys = { "light_hit", "heavy_hit", "super_hit", "electric_s_e" };
    
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
        
        var heroUnits = FightLoad.Fight.FightMembers.HeroSets.GetValues();
        var enemyUnits = FightLoad.Fight.FightMembers.EnemySets.GetValues();

        var effectPreloadCount = FightLoad.Fight.FightMode is (FightMode.Rotate or FightMode.Evolve)
            ? 1
            : Mathf.Max(heroUnits.Count, enemyUnits.Count);
        
        var tasks = taskBuffer;
        tasks.Clear();
        tasks.Add(AppSetting.PlayBGM(FightLoad.Fight.GetBGMKey()));
        tasks.Add(HurtObjectManager.ConstructDPool());
        tasks.Add(AddressablesLogic.Essentials());
        tasks.Add(BoundaryControlByGod.target.ChangeBackGround(FightLoad.Fight.battleGroundID));
        tasks.Add(RTFightManager.Target.LoadUnits(FightLoad.Fight));
        tasks.Add(EffectsManager.IniEffectsPool(CommonSetting.HitGroundEffectCode, null, effectPreloadCount));
        tasks.Add(EffectsManager.IniEffectsPool(CommonSetting.WallCrackEffectCode, null, effectPreloadCount));
        
        var elementCounts = elementCountBuffer;
        elementCounts.Clear();
        var totalUnitCount = 0;
        void AccumulateElementCounts(List<UnitInfo> units)
        {
            if (units == null || units.Count == 0)
                return;

            foreach (var unit in units)
            {
                if (unit == null || string.IsNullOrEmpty(unit.r_id))
                    continue;

                var config = Units.GetUnitConfig(unit.r_id);
                if (config == null)
                    continue;

                totalUnitCount++;
                
                if (elementCounts.TryGetValue(config.element, out var current))
                {
                    elementCounts[config.element] = current + 1;
                }
                else
                {
                    elementCounts[config.element] = 1;
                }
            }
        }

        AccumulateElementCounts(heroUnits);
        AccumulateElementCounts(enemyUnits);

        foreach (var kv in elementCounts)
        {
            var effectPath = FightGlobalSetting.EffectPathDefine(kv.Key);
            for (int i = 0; i < elementEffectKeys.Length; i++)
            {
                tasks.Add(EffectsManager.IniEffectsPool(elementEffectKeys[i], effectPath, kv.Value));
            }
        }
        elementCounts.Clear();

        var sharedEffectCount = Mathf.Max(effectPreloadCount, totalUnitCount);
        tasks.Add(EffectsManager.IniEffectsPool("super_combo_explosion", null, sharedEffectCount));
        tasks.Add(EffectsManager.IniEffectsPool("dream_buff", null, sharedEffectCount));
        
        if (FightLoad.Fight.FightMode is (FightMode.Rotate or FightMode.Evolve))
        {
            if (!string.IsNullOrEmpty(CommonSetting.MemberShiftEffectCode))
            {
                tasks.Add(EffectsManager.IniEffectsPool(CommonSetting.MemberShiftEffectCode, null, 1));
            }
            if (!string.IsNullOrEmpty(CommonSetting.SubMemberShiftEffectCode) &&
                CommonSetting.SubMemberShiftEffectCode != CommonSetting.MemberShiftEffectCode)
            {
                tasks.Add(EffectsManager.IniEffectsPool(CommonSetting.SubMemberShiftEffectCode, null, 1));
            }
        }
        ProgressLayer.LoadingPercent(Translate.Get("LoadingBattle"), 0.7f);
        await UniTask.WhenAll(tasks);
        tasks.Clear();
        
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
