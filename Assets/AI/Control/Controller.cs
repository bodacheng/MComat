using System.Collections.Generic;
using UnityEngine;

// 20200226
// 整个状态迁移系统进行了大翻修，而最初的动机就是想到如果AI系统要具备一定自我改善能力的话，最起码AI系统自身应该更独立。
// 目前AI的特点是根据事前登录好的若干条件组与各个条件组下登录的行为列表进行反应。
// 如果说将来这套AI系统要变的更高级，那么可能条件组并不是固定的，而是能在战斗过程中动态调整，动态自我追加和细化。
// 但目前来说我们还不需要。或许可以制作一个简单的通过logger分析条件组下各个行为有没有效益，并调整优先级的脚本，但这个点到为止。
namespace Soul
{
    public class Controller : MonoBehaviour
    {
        readonly List<Behavior_Transition_Set> finalDecisions = new List<Behavior_Transition_Set>();
        readonly SSIMultiDictionary Triggerd = new SSIMultiDictionary();
                
        public void PlayerControll(BehaviorRunner behaviorRunner, List<Behavior_Transition_Set> Options, bool AI_Active)
        {
            if (MobileInputsManager.target.Observing_Runner == behaviorRunner)
            {
                #region 主动退出当前状态的控制类条件是否激活
                if (!BehaviourExitInputTrigger(behaviorRunner.CurrentBehaviorTransitionSet))
                {
                    return;
                }
                #endregion

                #region 按键触发
                for (int i = 0; i < Options.Count; i++)
                {
                    switch (Options[i].enterInput)
                    {
                        case Inputs_defined.Attack:
                            if (MobileInputsManager.attack)
                            {
                                MobileInputsManager.SkillButtonExplosion(Options[i].enterInput, Options[i].SPLevel);
                                behaviorRunner.ChangeState(Options[i].StateKey);
                                return;
                            }
                            break;
                        case Inputs_defined.Fire1:
                            if (MobileInputsManager.fire1)
                            {
                                MobileInputsManager.SkillButtonExplosion(Options[i].enterInput, Options[i].SPLevel);
                                behaviorRunner.ChangeState(Options[i].StateKey);
                                return;
                            }
                            break;
                        case Inputs_defined.Fire2:
                            if (MobileInputsManager.fire2)
                            {
                                MobileInputsManager.SkillButtonExplosion(Options[i].enterInput, Options[i].SPLevel);
                                behaviorRunner.ChangeState(Options[i].StateKey);
                                return;
                            }
                            break;
                        case Inputs_defined.Acc:
                            if (MobileInputsManager.acc)
                            {
                                behaviorRunner.ChangeState(Options[i].StateKey);
                                return;
                            }
                            break;
                        case Inputs_defined.Defend:
                            if (MobileInputsManager.defendButtonHover)
                            {
                                behaviorRunner.ChangeState(Options[i].StateKey);
                                return;
                            }
                            break;
                    }
                }
                #endregion
            }
            
            if (AI_Active)
            {
                #region AI决策
                if (AI_RUNs(behaviorRunner,Options))
                {
                    return;//20200205的bug怀疑是卡在这个部位。原因在于bug明显受AI模式影响，并且角色后跳后“定住”的时候只要输入方向，就会开始走动。
                    //基本说明输入行为让AI_Active为false从而放行至接下来的归idle判断，从而让角色直接进入move state
                }
                #endregion
            }
            
            #region 当前状态可自然退出了却没有任何后续行为被触发的话，回复起始状态
            if (behaviorRunner.GetNowState().Capacity_Exit_Condition())
            {
                behaviorRunner.ChangeToWaitingState();
            }
            #endregion
        }
        
        // 状态的退出可以由特定的控制条件来决定时进行的判断。目前全项目只有防御这一种情况
        bool BehaviourExitInputTrigger(Behavior_Transition_Set current_Behavior_Set)
        {
            switch(current_Behavior_Set.exitInput)
            {
                case Inputs_defined.Defend_Cancel:
                    return MobileInputsManager.target.DefendExitTrigger();
                default:
                    return true;
            }
        }

        readonly List<string> allAvaliableKeyCodes = new List<string>();
        string Condition, BehaviourCode;
        Behavior temp;
        bool AI_RUNs(BehaviorRunner behaviorRunner,List<Behavior_Transition_Set> avaliable_casual_Transitions) // AI根据目前可作出的行为作出选择
        {
            finalDecisions.Clear();
            Triggerd.main.Clear();
            allAvaliableKeyCodes.Clear();
            for (int i = 0; i < avaliable_casual_Transitions.Count; i++)
            {
                allAvaliableKeyCodes.Add(avaliable_casual_Transitions[i].StateKey);
            }
            
            if (behaviorRunner.GetNowState().Strategic_exit_condition())
            {
                for (int y = 0; y < behaviorRunner.AllConditionCodes.Count; y++)
                {
                    if (behaviorRunner.GetNowState().CheckTriggerCondition(behaviorRunner.AllConditionCodes[y]))
                    {
                        for (int x = 0; x < allAvaliableKeyCodes.Count; x++)
                        {
                            Condition = behaviorRunner.AllConditionCodes[y];
                            BehaviourCode = allAvaliableKeyCodes[x];
                            if (behaviorRunner.ConditionAndRespond[Condition].Contains(BehaviourCode))
                            {
                                Triggerd.main.Set(Condition, BehaviourCode, behaviorRunner.ConditionAndRespondPriority.Get(Condition,BehaviourCode));
                            }
                        }
                    }
                }            
            }
            
            if (Triggerd.main.values.Count > 0)
            {
                List<KeyValuePair<string, string>> minkeys = Triggerd.GiveOutMin();
                for (int x = 0; x < minkeys.Count; x++)
                {
                    finalDecisions.Add(behaviorRunner.Behaviour_Transition_Dictionary[minkeys[x].Value]); // Debug.Log("AI甄选的最佳决定："+minkeys[x].Value);
                }
                if (finalDecisions.Count > 0)
                {
                    //Debug.Log("---------------");
                    //for (int i = 0; i < finalDecisions.Count; i++)
                    //{
                    //    Debug.Log("we here"+ finalDecisions[i].StateKey);
                    //}
                    //Debug.Log("---------------");
                    int random = Random.Range(0,finalDecisions.Count);//这里虽然是随机但是毕竟随机的这几个选项在优先级上是相同的。
                    if (MobileInputsManager.target.Observing_Runner == behaviorRunner)
                        MobileInputsManager.SkillButtonExplosion(finalDecisions[random].enterInput, finalDecisions[random].SPLevel);
                    behaviorRunner.ChangeState(finalDecisions[random].StateKey);
                    return true;
                }
            }
            return false;
        }
    }
}

