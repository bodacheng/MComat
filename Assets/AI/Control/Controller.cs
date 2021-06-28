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
        readonly SSIMultiDictionary Triggerd = new SSIMultiDictionary();
        public bool TestMode { get; set; } = false;

        public void PlayerControll(BehaviorRunner behaviorRunner, List<SkillEntity> Options, bool AI_Active)
        {
            if (MobileInputsManager.target.Observing_Runner == behaviorRunner)
            {
                #region 主动退出当前状态的控制类条件是否激活
                if (!BehaviourExitInputTrigger(behaviorRunner.CurrentSKillEntity))
                {
                    return;
                }
                #endregion

                #region 按键触发
                for (int i = 0; i < Options.Count; i++)
                {
                    switch (Options[i].EnterInput)
                    {
                        case InputKey.Attack1:
                            if (MobileInputsManager.attack)
                            {
                                MobileInputsManager.SkillButtonExplosion(Options[i].EnterInput, Options[i].SP_LEVEL);
                                behaviorRunner.SingleFightLog.WriteLog(
                                    new SingleFightLog.BehaviourFightRecord
                                    {
                                        AI_Decided = false,
                                        stateKey = Options[i].REAL_NAME,
                                        whyIDidThis = null
                                    }
                                );
                                behaviorRunner.SingleFightLog.AnalysisLog(behaviorRunner.ConditionAndRespondPriority);
                                behaviorRunner.ChangeState(Options[i].REAL_NAME);
                                return;
                            }
                            break;
                        case InputKey.Attack2:
                            if (MobileInputsManager.fire1)
                            {
                                MobileInputsManager.SkillButtonExplosion(Options[i].EnterInput, Options[i].SP_LEVEL);
                                behaviorRunner.SingleFightLog.WriteLog(
                                    new SingleFightLog.BehaviourFightRecord
                                    {
                                        AI_Decided = false,
                                        stateKey = Options[i].REAL_NAME,
                                        whyIDidThis = null
                                    }
                                );
                                behaviorRunner.SingleFightLog.AnalysisLog(behaviorRunner.ConditionAndRespondPriority);
                                behaviorRunner.ChangeState(Options[i].REAL_NAME);
                                return;
                            }
                            break;
                        case InputKey.Attack3:
                            if (MobileInputsManager.fire2)
                            {
                                MobileInputsManager.SkillButtonExplosion(Options[i].EnterInput, Options[i].SP_LEVEL);
                                behaviorRunner.SingleFightLog.WriteLog(
                                    new SingleFightLog.BehaviourFightRecord
                                    {
                                        AI_Decided = false,
                                        stateKey = Options[i].REAL_NAME,
                                        whyIDidThis = null
                                    }
                                );
                                behaviorRunner.SingleFightLog.AnalysisLog(behaviorRunner.ConditionAndRespondPriority);
                                behaviorRunner.ChangeState(Options[i].REAL_NAME);
                                return;
                            }
                            break;
                        case InputKey.Acc:
                            if (MobileInputsManager.acc)
                            {
                                behaviorRunner.SingleFightLog.WriteLog(
                                    new SingleFightLog.BehaviourFightRecord
                                    {
                                        AI_Decided = false,
                                        stateKey = Options[i].REAL_NAME,
                                        whyIDidThis = null
                                    }
                                );
                                behaviorRunner.SingleFightLog.AnalysisLog(behaviorRunner.ConditionAndRespondPriority);
                                behaviorRunner.ChangeState(Options[i].REAL_NAME);
                                return;
                            }
                            break;
                        case InputKey.Defend:
                            if (MobileInputsManager.defendButtonHover)
                            {
                                behaviorRunner.SingleFightLog.WriteLog(
                                    new SingleFightLog.BehaviourFightRecord
                                    {
                                        AI_Decided = false,
                                        stateKey = Options[i].REAL_NAME,
                                        whyIDidThis = null
                                    }
                                );
                                behaviorRunner.SingleFightLog.AnalysisLog(behaviorRunner.ConditionAndRespondPriority);
                                behaviorRunner.ChangeState(Options[i].REAL_NAME);
                                return;
                            }
                            break;
                        case InputKey.Null:
                            if (!MobileInputsManager.defendButtonHover && !MobileInputsManager.acc && !MobileInputsManager.fire2 && !MobileInputsManager.fire1 && !MobileInputsManager.attack)
                            {
                                behaviorRunner.ChangeState(Options[i].REAL_NAME);
                                return;
                            }
                            break;
                    }
                }
                #endregion
            }
            
            #region AI决策
            if (AI_Active)
            {
                AI_RUNs(behaviorRunner, Options);
            }
            #endregion
        }

        // 状态的退出可以由特定的控制条件来决定时进行的判断。目前全项目只有防御这一种情况
        bool BehaviourExitInputTrigger(SkillEntity current_Behavior_Set)
        {
            switch(current_Behavior_Set.ExitInput)
            {
                case InputKey.Defend_Cancel:
                    return MobileInputsManager.target.DefendExitTrigger();
                default:
                    return true;
            }
        }

        string Condition;
        List<(string, string)> finalConditionStateKeySet = new List<(string, string)>();
        bool AI_RUNs(BehaviorRunner behaviorRunner, List<SkillEntity> options) // AI根据目前可作出的行为作出选择
        {
            Triggerd.main.Clear();
            if (behaviorRunner.GetNowState().Strategic_exit_condition())
            {
                for (int y = 0; y < behaviorRunner.AllConditionCodes.Count; y++)
                {
                    Condition = behaviorRunner.AllConditionCodes[y];
                    for (int x = 0; x < options.Count; x++)
                    {
                        if (behaviorRunner.ConditionAndRespond[Condition].Contains(options[x].REAL_NAME))
                        {
                            behaviorRunner.BehaviourDic.TryGetValue(options[x].REAL_NAME, out Behavior try_behavior);
                            if (try_behavior.CheckTriggerCondition(Condition))
                            {
                                if (TestMode)
                                {
                                    if (options[x].StateType == BehaviorType.MV)
                                        Triggerd.main.Set(Condition, options[x].REAL_NAME, behaviorRunner.ConditionAndRespondPriority.Get(Condition, options[x].REAL_NAME));
                                }else{
                                    Triggerd.main.Set(Condition, options[x].REAL_NAME, behaviorRunner.ConditionAndRespondPriority.Get(Condition, options[x].REAL_NAME));
                                }
                            }
                        }
                    }
                }
            }
            
            if (Triggerd.main.GetValues().Count > 0)
            {
                finalConditionStateKeySet = Triggerd.GiveOutMin();
                
                if (finalConditionStateKeySet.Count > 0)
                {
                    int random = Random.Range(0, finalConditionStateKeySet.Count);//这里虽然是随机但是毕竟随机的这几个选项在优先级上是相同的。
                    SkillEntity _SE = behaviorRunner.SkillEntityDic[finalConditionStateKeySet[random].Item2];
                    if (MobileInputsManager.target.Observing_Runner == behaviorRunner)
                    {
                        MobileInputsManager.SkillButtonExplosion(_SE.EnterInput, _SE.SP_LEVEL);
                    }
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
                    return true;
                }
            }
            return false;
        }
    }
}

