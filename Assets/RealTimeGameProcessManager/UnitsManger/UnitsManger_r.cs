using System;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using HittingDetection;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public ReactiveProperty<Data_Center> RMode_Unit = new ReactiveProperty<Data_Center>();
        Data_Center waitingMember;
        
        public void ToStartPosRotate()
        {
            Data_Center unit = null;
            foreach (var dataCenter in teamMembers.mDict
                         .OrderBy(kv => kv.Key.Item1)
                         .ThenBy(kv => kv.Key.Item2)
                         .Select(kv => kv.Value))
            {
                if (!RTFightManager.Target.SubUnits.CanSelectAsRotationMember(dataCenter))
                {
                    continue;
                }
                unit = dataCenter;
                dataCenter.WholeT.parent = null;
                dataCenter.WholeT.gameObject.SetActive(true);
                break;
            }
            if (unit == null)
            {
                ChangeFightingUnit(null, true, TeamStandPoints[0]);
                return;
            }
            if (FightLoad.Fight.RunTutorial)
                unit.StartAutoModeWhenGetHurt();
            ChangeFightingUnit(unit, true, TeamStandPoints[0]);
        }
        
        void ToNewUnit(int delayInSeconds)
        {
            var disposable = new SerialDisposable();
            disposable.Disposable = Observable.Timer(TimeSpan.FromSeconds(delayInSeconds)).Subscribe((_) =>
                {
                    RandomToAliveUnit();
                    disposable.Dispose();
                }).AddTo(RTFightManager.Target.Disposables);
        }
        
        public void TeamsIniRotate(float teamHpRate, CriticalGaugeMode teamCgMode, AIMode aiMode, int aiDelayFrame, 
            Func<bool> aiTriggerDreamComboRateCondition, bool evolutionMode = false)
        {
            var list = teamMembers.GetValues();
            for (var index = 0; index < list.Count; index++)
            {
                var center = list[index];
                //  时间刷新整备
                RTFightManager.Target.RefreshTimeDic.Add(center, new ReactiveProperty<float>(0));
                if (!evolutionMode)
                    center.Step3Initialize(teamConfig, teamCgMode, aiMode, aiDelayFrame, aiTriggerDreamComboRateCondition, teamHpRate, RTFightManager.Target.UnitInfoRef[center]);
                else
                {
                    var hpRate = teamHpRate * (index * FightGlobalSetting.EvolutionModeEnemyHpIncreaseRate + 1);
                    center.Step3Initialize(teamConfig, teamCgMode, aiMode, aiDelayFrame, aiTriggerDreamComboRateCondition, hpRate, RTFightManager.Target.UnitInfoRef[center]);
                }
                
                center.FightDataRef.IsDead.Subscribe(x =>
                {
                    if (x)
                    {
                        Sensor.AddOrRemoveSharedDeadUnitInfo(center, teamConfig.myTeam, true);
                        Sensor.AddOrRemoveSharedUnitInfo(center, teamConfig.myTeam, false);
                        if (FightLogger.value.GetWinnerTeam() == Team.none)
                        {
                            if (teamConfig.myTeam == Team.player2 && FightLoad.Fight.FightMode == FightMode.Evolve && center.CanUseMainUnitDeathFlow)
                            {
                                HitBoxesProcesser.Instance.AllProcessingFade();
                                RTFightManager.Target.team1.RMode_Unit.Value._MyBehaviorRunner.ChangeToWaitingState();
                                var inBattleEvolution = UILayerLoader.Load<InBattleEvolution>();
                                var fightingLayer = FightingStepLayer.Open();
                                fightingLayer.gameObject.SetActive(false);
                                RTFightManager.Target.team1.InputsManager.FocusUnit(null);
                                RTFightManager.Target.EvolutionManager.EvolutionCount++;
                                string bottomText = "";
                                switch (RTFightManager.Target.EvolutionManager.EvolutionCount)
                                {
                                    case 1:
                                        bottomText = Translate.Get("InBattleEvolutionInfo1");
                                        break;
                                    case 2:
                                        bottomText = Translate.Get("InBattleEvolutionInfo2");
                                        break;
                                    case 3:
                                        bottomText = Translate.Get("InBattleEvolutionInfo3");
                                        break;
                                }
                                
                                inBattleEvolution.Setup(RTFightManager.Target.team1.RMode_Unit.Value, () =>
                                    {
                                        UILayerLoader.Remove<InBattleEvolution>();
                                        ToNewUnit(0);
                                        switch (RTFightManager.Target.EvolutionManager.EvolutionCount)
                                        {
                                            case 1:
                                                RTFightManager.Target.team2.RMode_Unit.Value.FightDataRef
                                                    .CriticalGaugeMode = CriticalGaugeMode.Normal;
                                                break;
                                            case 2:
                                                RTFightManager.Target.team2.RMode_Unit.Value.FightDataRef
                                                    .CriticalGaugeMode = CriticalGaugeMode.DoubleGain;
                                                break;
                                            case 3:
                                                RTFightManager.Target.team2.RMode_Unit.Value.FightDataRef
                                                    .CriticalGaugeMode = CriticalGaugeMode.Unlimited;
                                                break;
                                        }
                                        
                                        fightingLayer.gameObject.SetActive(true);
                                        RTFightManager.Target.team1.InputsManager.FocusUnit(RTFightManager.Target.team1.RMode_Unit.Value);
                                    },
                                    Translate.Get("ChooseYourEvolution"), bottomText);
                            }
                            else
                            {
                                if (!center.HasChangedToSubUnit)
                                    ToNewUnit(2);
                            }
                        }
                        
                        var disposableUnit = new SerialDisposable();
                        var boolObservable = Observable.EveryUpdate()
                            .Where(_ => center._BasicPhysicSupport.AtRing)
                            .Take(1)
                            .Select(_ => Unit.Default);

                        var timerObservable = Observable.Timer(TimeSpan.FromSeconds(1))
                            .Select(_ => Unit.Default);  // Timer 也转换为 Unit 类型
                        
                        disposableUnit.Disposable = Observable.Amb<Unit>(boolObservable, timerObservable)
                            .Subscribe(async (_) =>
                            {
                                if (center != null && !center.HasChangedToSubUnit)
                                {
                                    await EffectsManager.GenerateEffect(CommonSetting.MemberShiftEffectCode, null,
                                        center.geometryCenter.position, Quaternion.identity, null);
                                    center.WholeT.gameObject.SetActive(false);
                                }
                                
                                disposableUnit.Dispose();
                            }).AddTo(center);
                    }
                }).AddTo(center);

                if (RTFightManager.Target.SubUnits.HasSubUnit(center))
                {
                    center.AssignSubUnitSwitcher((x, y) => ChangeFightingUnitToHerSub(center, x, y));
                }
            }
        }
        
        private bool ChangeFightingUnitToHerSub(Data_Center main, string stateKey, V_Damage damage)
        {
            var changeTo = RTFightManager.Target.SubUnits.FindSubUnit(main, RTFightManager.Target.UnitInfoRef);
            if (changeTo == null)
            {
                return false;
            }
            
            var fightingStepLayer = FightingStepLayer.Open();
            TeamUIManager teamUI;
            if (main._TeamConfig.myTeam == Team.player1)
            {
                teamUI = fightingStepLayer.Team1UI;
            }
            else
            {
                teamUI = fightingStepLayer.Team2UI;
            }
            
            var sideIcon = teamUI.UnitIconDic[changeTo];
            var formalSideIcon = teamUI.UnitIconDic[main];
            sideIcon.gameObject.SetActive(true);
            sideIcon.Icon.gameObject.SetActive(true);
            formalSideIcon.gameObject.SetActive(false);

            var animationSnapshot = main.AnimationManger.CaptureAnimatorState();
            
            var targetPos = main.WholeT.transform.position;
            var targetRot = main.WholeT.transform.rotation;
            
            if (RMode_Unit.Value != null) // 继承hit数
            {
                Sensor.AddOrRemoveSharedUnitInfo(RMode_Unit.Value, teamConfig.myTeam, false);
                changeTo.FightDataRef._comboHitCount.HitCount.Value = RMode_Unit.Value.FightDataRef._comboHitCount.HitCount.Value;
            }
            Sensor.AddOrRemoveSharedUnitInfo(changeTo, teamConfig.myTeam, true);
            RMode_Unit.Value = changeTo;
            RMode_Unit.Value._MyBehaviorRunner.ChangeState(stateKey, damage);
            
            RMode_Unit.Value.WholeT.transform.position = targetPos;
            RMode_Unit.Value.WholeT.transform.rotation = targetRot;
            
            RMode_Unit.Value.FightDataRef.CurrentHp.Value = main.FightDataRef.CurrentHp.Value;
            RMode_Unit.Value.FightDataRef.Resistance.Value = main.FightDataRef.Resistance.Value;
            RMode_Unit.Value.FightDataRef.CriticalGaugeMode = main.FightDataRef.CriticalGaugeMode;
            main.MarkChangedToSubUnit();
            main.FightDataRef.IsDead.Value = true;
            
            main.WholeT.gameObject.SetActive(false);
            RMode_Unit.Value.WholeT.gameObject.SetActive(true);

            if (animationSnapshot.IsValid)
            {
                RMode_Unit.Value.AnimationManger.RestoreAnimatorState(animationSnapshot);
            }
            
            EffectsManager.GenerateEffect(CommonSetting.SubMemberShiftEffectCode, null, RMode_Unit.Value.WholeT.transform.position, Quaternion.identity, RMode_Unit.Value.geometryCenter).Forget();
            
            if (teamConfig.myTeam == RTFightManager.playerTeam && InputsManager != null)
            {
                InputsManager.FocusUnit(RMode_Unit.Value, true);
            }

            global::FightScene.FightScene.target?.SensorUnity.ForceImmediateDetection();
            
            return true;
        }
        
        // 切换队员
        bool ChangeFightingUnit(Data_Center changeTo, bool emptyState = false, Transform iniStandPoint = null)
        {
            if (changeTo == null)
            {
                var returnValue = RMode_Unit.Value != changeTo;
                RMode_Unit.Value = changeTo;
                return returnValue;
            }
            
            if (changeTo.FightDataRef.IsDead.Value)
            {
                return false;
            }
            var unitChanged = false;
            var targetPos = Vector3.zero;
            var targetRot = Quaternion.identity;
            if (iniStandPoint != null)
            {
                targetPos = iniStandPoint.position;
                targetRot = iniStandPoint.rotation;
            }
            else
            {
                if (RMode_Unit.Value != null)
                {
                    targetPos = RMode_Unit.Value.transform.position;
                    targetRot = Quaternion.Euler(new Vector3(1,0,1));
                }
            }
            
            foreach (var dataCenter in teamMembers.GetValues())
            {
                if (changeTo == dataCenter)
                {
                    if (RMode_Unit.Value != null && changeTo != null) //继承hit数
                    {
                        Sensor.AddOrRemoveSharedUnitInfo(RMode_Unit.Value, teamConfig.myTeam, false);
                        changeTo.FightDataRef._comboHitCount.HitCount.Value = RMode_Unit.Value.FightDataRef._comboHitCount.HitCount.Value;
                    }
                    Sensor.AddOrRemoveSharedUnitInfo(changeTo, teamConfig.myTeam, true);
                    RMode_Unit.Value = changeTo;
                    RMode_Unit.Value.WholeT.gameObject.SetActive(true);
                    
                    if (emptyState)
                    {
                        RMode_Unit.Value._MyBehaviorRunner.ChangeState("Empty");
                    }
                    else
                    {
                        RMode_Unit.Value._MyBehaviorRunner.ChangeToWaitingState();
                    }
                    RMode_Unit.Value.WholeT.transform.position = targetPos;
                    RMode_Unit.Value.WholeT.transform.rotation = targetRot;
                    EffectsManager.GenerateEffect(CommonSetting.MemberShiftEffectCode, null, RMode_Unit.Value.WholeT.transform.position, Quaternion.identity, RMode_Unit.Value.geometryCenter).Forget();
                    unitChanged = true;
                }
                else
                {
                    if (dataCenter._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                    {
                        dataCenter._MyBehaviorRunner.ChangeState("Empty");
                    }
                    dataCenter.WholeT.gameObject.SetActive(false);
                }
            }
            
            if (teamConfig.myTeam == RTFightManager.playerTeam && InputsManager != null)
            {
                InputsManager.FocusUnit(RMode_Unit.Value, true);
            }

            global::FightScene.FightScene.target?.SensorUnity.ForceImmediateDetection();
            
            //Refresh(TeamMembers);
            return unitChanged;
        }

        public void UnitStartOff()
        {
            RMode_Unit.Value._MyBehaviorRunner.ChangeToWaitingState();
        }
        
        // 计算时间统计可上场角色，更新上场冷却图标UI
        void WaitUnitChange()
        {
            if (FSceneProcessesRunner.Main.CurrentStep() == SceneStep.Fighting)
            {
                var refreshTimes = RTFightManager.Target.RefreshTimeDic;
                foreach (var member in teamMembers.mDict.Values)
                {
                    if (member == null || !refreshTimes.TryGetValue(member, out var refreshTime))
                    {
                        continue;
                    }

                    if (refreshTime.Value > 0)
                    {
                        refreshTime.Value -= Time.deltaTime; // 角色切换倒计时;
                    }
                }
            
                if (waitingMember != null &&  RMode_Unit.Value != waitingMember && CanChangeToThisMember(waitingMember))
                {
                    RTFightManager.Target.RefreshTimeDic[RMode_Unit.Value].Value = 10f;
                    ChangeFightingUnit(waitingMember);
                    waitingMember = null;
                }
            }
            if (FSceneProcessesRunner.Main.CurrentStep() == SceneStep.CountDown)
            {
                if (waitingMember != null && RMode_Unit.Value != waitingMember)
                {
                    ChangeFightingUnit(waitingMember, true, TeamStandPoints[0]);
                }
            }
        }
        
        bool CanChangeToThisMember(Data_Center target)
        {
            if (target == RMode_Unit.Value)
            {
                return false;
            }
            if (target.FightDataRef.IsDead.Value)
            {
                return false;
            }
            if (RTFightManager.Target.RefreshTimeDic[target].Value > 0)
            {
                return false;
            }
            if (target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit || target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.KnockOff)
            {
                return false;
            }
            if (target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GI || 
                target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GM || 
                target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GMB || 
                target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GR)
            {
                if (!target._SkillCancelFlag.Cancel_Flag)
                    return true;
            }
            return true;
        }
        
        public void ReadyForNextMember(Data_Center next)
        {
            if (RTFightManager.Target.SubUnits.CanSelectAsRotationMember(next))
                waitingMember = next;
        }
        
        bool RandomToAliveUnit()
        {
            if (waitingMember != null && waitingMember.FightDataRef.CurrentHp.Value > 0)
            {
                if (!waitingMember.FightDataRef.IsDead.Value)
                {
                    if (ChangeFightingUnit(waitingMember))
                    {
                        return true;
                    }
                }
            }
            
            foreach (var dataCenter in teamMembers.mDict
                         .OrderBy(kv => kv.Key.Item1)
                         .ThenBy(kv => kv.Key.Item2)
                         .Select(kv => kv.Value))
            {
                if (dataCenter != null && !dataCenter.FightDataRef.IsDead.Value && RTFightManager.Target.SubUnits.CanSelectAsRotationMember(dataCenter))
                {
                    if (ChangeFightingUnit(dataCenter))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        
    }
}
