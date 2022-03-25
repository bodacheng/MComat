using System.Collections.Generic;
using UnityEngine;
using Skill;

// 20200226
// 整个状态迁移系统进行了大翻修，而最初的动机就是想到如果AI系统要具备一定自我改善能力的话，最起码AI系统自身应该更独立。
// 目前AI的特点是根据事前登录好的若干条件组与各个条件组下登录的行为列表进行反应。
// 如果说将来这套AI系统要变的更高级，那么可能条件组并不是固定的，而是能在战斗过程中动态调整，动态自我追加和细化。
// 但目前来说我们还不需要。或许可以制作一个简单的通过logger分析条件组下各个行为有没有效益，并调整优先级的脚本，但这个点到为止。
namespace Soul
{
    public class Controller
    {
        readonly SSIMultiDictionary _triggerd = new SSIMultiDictionary();
        
        public void Decision(BehaviorRunner runner, List<SkillEntity> Options, bool Auto)
        {
            bool changed = false;
            
            #region 按键触发
            if (runner.InputsManager != null)
            {
                changed = BtnTrigger(runner, Options, runner.InputsManager);
                if (changed)
                {
                    runner.InputsManager.BtnRefreshEffects();
                }
            }
            #endregion
            
            if (changed)
            {
                goto A;
            }
            
            #region AI决策
            if (Auto)
            {
                changed = AI_RUNs(runner, Options);
            }
            #endregion
            
            A:
            if (!changed)
                AutoReset(runner);
        }

        bool BtnTrigger(BehaviorRunner runner, List<SkillEntity> Options, MobileInputsManager InputsManager)
        {
            // 主动退出当前状态的控制类条件是否激活
            if (!BehaviourExitInputTrigger(runner.CurrentSKillEntity, InputsManager))
            {
                return false;
            }
            
            for (var i = 0; i < Options.Count; i++)
            {
                switch (Options[i].EnterInput)
                {
                    case InputKey.Attack1:
                    if (MobileInputsManager.attack)
                    {
                        InputsManager.SkillExplosion(Options[i].EnterInput, Options[i].SP_LEVEL);
                        runner.SingleFightLog.WriteLog(
                        new SingleFightLog.BehaviourFightRecord
                        {
                            AI_Decided = false,
                            stateKey = Options[i].REAL_NAME,
                            whyIDidThis = null
                        }
                        );
                        runner.SingleFightLog.AnalysisLog(runner.ConditionAndRespondPriority);
                        runner.ChangeState(Options[i].REAL_NAME);
                        return true;
                    }
                    break;
                    case InputKey.Attack2:
                    if (MobileInputsManager.fire1)
                    {
                        InputsManager.SkillExplosion(Options[i].EnterInput, Options[i].SP_LEVEL);
                        runner.SingleFightLog.WriteLog(
                            new SingleFightLog.BehaviourFightRecord
                            {
                                AI_Decided = false,
                                stateKey = Options[i].REAL_NAME,
                                whyIDidThis = null
                            }
                        );
                        runner.SingleFightLog.AnalysisLog(runner.ConditionAndRespondPriority);
                        runner.ChangeState(Options[i].REAL_NAME);
                        return true;
                    }
                    break;
                    case InputKey.Attack3:
                    if (MobileInputsManager.fire2)
                    {
                        InputsManager.SkillExplosion(Options[i].EnterInput, Options[i].SP_LEVEL);
                        runner.SingleFightLog.WriteLog(
                        new SingleFightLog.BehaviourFightRecord
                            {
                                AI_Decided = false,
                                stateKey = Options[i].REAL_NAME,
                                whyIDidThis = null
                            }
                        );
                        runner.SingleFightLog.AnalysisLog(runner.ConditionAndRespondPriority);
                        runner.ChangeState(Options[i].REAL_NAME);
                        return true;
                    }
                    break;
                    case InputKey.Acc:
                    if (MobileInputsManager.acc)
                    {
                        runner.SingleFightLog.WriteLog(
                        new SingleFightLog.BehaviourFightRecord
                            {
                                AI_Decided = false,
                                stateKey = Options[i].REAL_NAME,
                                whyIDidThis = null
                            }
                        );
                        runner.SingleFightLog.AnalysisLog(runner.ConditionAndRespondPriority);
                        runner.ChangeState(Options[i].REAL_NAME);
                        return true;
                    }
                    break;
                    case InputKey.Defend:
                    if (MobileInputsManager.defendButtonHover)
                    {
                        runner.SingleFightLog.WriteLog(
                        new SingleFightLog.BehaviourFightRecord
                            {
                                AI_Decided = false,
                                stateKey = Options[i].REAL_NAME,
                                whyIDidThis = null
                            }
                        );
                        runner.SingleFightLog.AnalysisLog(runner.ConditionAndRespondPriority);
                        runner.ChangeState(Options[i].REAL_NAME);
                        return true;
                    }
                    break;
                    case InputKey.Null:
                        if (!MobileInputsManager.defendButtonHover && !MobileInputsManager.acc && !MobileInputsManager.fire2 && !MobileInputsManager.fire1 && !MobileInputsManager.attack)
                        {
                            runner.ChangeState(Options[i].REAL_NAME);
                            return true;
                        }
                    break;
                }
            }
            return false;
        }

        // 状态的退出可以由特定的控制条件来决定时进行的判断。目前全项目只有防御这一种情况
        bool BehaviourExitInputTrigger(SkillEntity current, MobileInputsManager _inputsManager)
        {
            switch(current.ExitInput)
            {
                case InputKey.Defend_Cancel:
                    return _inputsManager.DefendExitTrigger();
                default:
                    return true;
            }
        }

        string Condition;
        List<(string, string)> finalConditionStateKeySet = new List<(string, string)>();
        bool AI_RUNs(BehaviorRunner behaviorRunner, List<SkillEntity> options) // AI根据目前可作出的行为作出选择
        {
            _triggerd.main.Clear();
            if (behaviorRunner.GetNowState().Strategic_exit_condition())
            {
                for (var y = 0; y < behaviorRunner.AllConditionCodes.Count; y++)
                {
                    Condition = behaviorRunner.AllConditionCodes[y];
                    for (var x = 0; x < options.Count; x++)
                    {
                        if (behaviorRunner.ConditionAndRespond[Condition].Contains(options[x].REAL_NAME))
                        {
                            behaviorRunner.BehaviourDic.TryGetValue(options[x].REAL_NAME, out var try_behavior);
                            if (try_behavior.CheckTriggerCondition(Condition))
                            {
                                _triggerd.main.Set(Condition, options[x].REAL_NAME, behaviorRunner.ConditionAndRespondPriority.Get(Condition, options[x].REAL_NAME));
                            }
                        }
                    }
                }
            }
            
            if (_triggerd.main.GetValues().Count > 0)
            {
                finalConditionStateKeySet = _triggerd.GiveOutMin();
                
                if (finalConditionStateKeySet.Count > 0)
                {
                    int random = Random.Range(0, finalConditionStateKeySet.Count);//这里虽然是随机但是毕竟随机的这几个选项在优先级上是相同的。
                    var _SE = behaviorRunner.SkillEntityDic[finalConditionStateKeySet[random].Item2];
                    if (_SE.StateType == BehaviorType.AC || _SE.StateType == BehaviorType.CT || _SE.StateType == BehaviorType.Def
                        || _SE.StateType == BehaviorType.GI || _SE.StateType == BehaviorType.GM || _SE.StateType == BehaviorType.GR)
                    {
                        behaviorRunner.SingleFightLog.WriteLog(
                            new SingleFightLog.BehaviourFightRecord
                            {
                                AI_Decided = true,
                                stateKey = _SE.REAL_NAME,
                                whyIDidThis = finalConditionStateKeySet[random].Item1
                            }
                        );
                        behaviorRunner.SingleFightLog.AnalysisLog(behaviorRunner.ConditionAndRespondPriority);
                    }
                    behaviorRunner.ChangeState(_SE.REAL_NAME);
                    behaviorRunner.InputsManager?.SkillExplosion(_SE.EnterInput, _SE.SP_LEVEL);
                    return true;
                }
            }
            return false;
        }

        void AutoReset(BehaviorRunner behaviorRunner)
        {
            if (behaviorRunner.GetNowState().StateKey != behaviorRunner._commandWaitingState.StateKey && behaviorRunner.GetNowState().Capacity_Exit_Condition())
            {
                behaviorRunner.ChangeToWaitingState();
            }
        }
    }
}

