using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public partial class AIStateRunner : MonoBehaviour
    {
        // 这个纯粹为了按钮效果
        List<Behavior_Transition_Set> SRTListForCasualTransitionbuttonRefresh = new List<Behavior_Transition_Set>();        
        // 满足了触发条件的情况对策因果组
        IDictionary<KeyValuePair<string, string>, int> Triggerd = new Dictionary<KeyValuePair<string, string>, int>();// 果，因，优先级
        // 上面当中优先度最高的情况对策因果组
        IDictionary<KeyValuePair<string, string>, int> finalTriggerd = new Dictionary<KeyValuePair<string, string>, int>();// 果，因，优先级
        
        public void StateTransitionEngine_new(IDictionary<string, Behavior_Transition_Set> state_Transition_Dictionary)
        {
            avaliable_casual_Transitions.Clear();
            avaliable_forced_Transitions.Clear();
            SRTListForCasualTransitionbuttonRefresh.Clear();

            if (current_state_num != null)
                state_Transition_Dictionary.TryGetValue(current_state_num, out CurrentStateTransitionSet);

            #region Forced state transition 
            if (CurrentStateTransitionSet.forced_to_state_nums != null && CurrentStateTransitionSet.forced_to_state_nums.Length > 0)
            {
                foreach (string num in CurrentStateTransitionSet.forced_to_state_nums)
                {
                    state_Dictionary.TryGetValue(num, out try_Behavior);
                    if (try_Behavior.Force_enter_condition())
                    {
                        avaliable_forced_Transitions.Add(num);
                    }
                }
            }
            if (avaliable_forced_Transitions.Count > 0)
            {
                ChangeState(avaliable_forced_Transitions[0]);
                return; // Once a state is forced to trigger, there is no need for the rest of codes to run at this frame
            }
            #endregion

            ///////// 18.10.17追加修改:
            /// 下面这些部分是17日所追加的全新部分，目的在于更正确的适配技能输入图标。
            /// 重点1:一旦发动了某一技能后，技能图标应该立刻更新，而不是等待可以super cancel之后，因此一旦avaliable_casual_Transitions.Count > 0
            /// 我们就立刻开始了对输入按键的图标刷新工作
            /// 重点2:如果在一个技能的连击选择中，attack，fire1，fire2中任何一个技能因为没蓝等原因无法发动，应该给准确的予以表示，而不是不进行任何键位刷新处理
            /// 另外详见refreshSPLevelButtonsInfo在NewCommandWaiting中的运行机制，那里也做了非常重要的修改
            ////////////////////////////////////////////////////////////

            bool exitCommandFufilled = true;
            if (playerMode || _inputManager.PlayerInputting)
            {
                exitCommandFufilled = CurrentStateTransitionSet.exitInput == Inputs_defined.Null || CheckInput(CurrentStateTransitionSet.exitInput);
            }
            
            // 满足了触发条件的情况对策因果组
            Triggerd.Clear();
            // 上面当中有限度最高的情况对策因果组
            finalTriggerd.Clear();
            
            foreach (Behavior_Transition_Set Behavior_set in CurrentStateTransitionSet.casual_to_state_Sets)
            {
                state_Dictionary.TryGetValue(Behavior_set.StateKey, out try_Behavior);
                if (!try_Behavior.Capacity_enter_condition())
                {
                    continue;
                }
                SRTListForCasualTransitionbuttonRefresh.Add(Behavior_set);//avaliable_casual_Transitions是真正可以启动的技能的列表，SRTListForCasualTransitionbuttonRefresh根据作用得有个预告作用
                if ((Behavior_set.can_be_cancelled_to && _SkillCancelFlag.Cancel_Flag) || (now_Behavior.Capacity_Exit_Condition() && exitCommandFufilled))
                {
                    if (playerMode || _inputManager.PlayerInputting)
                    {
                        if ((Behavior_set.enterInput != Inputs_defined.Null && CheckInput(Behavior_set.enterInput)) || Behavior_set.enterInput == Inputs_defined.Null)
                        {
                            avaliable_casual_Transitions.Add(Behavior_set);
                        }
                    }
                    else
                    {
                        List<string> Conditions = RespondAndCondition[try_Behavior.StateKey];
                        for (int i = 0; i < Conditions.Count; i++)
                        {
                            if (try_Behavior.CheckTriggerCondition(Conditions[i]))//条件满足
                            {
                                // 查看情况与行为反应的优先度
                                foreach (KeyValuePair<KeyValuePair<string, string>, int> keyValuePair in RespondAndConditionPriority)
                                {
                                    if (keyValuePair.Key.Key == try_Behavior.StateKey && keyValuePair.Key.Value == Conditions[i])
                                    {
                                        Debug.Log("状态"+try_Behavior.StateKey + "遇到了情况："+Conditions[i] +"从而可能要触发");
                                        Triggerd.Add(keyValuePair);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        avaliable_casual_Transitions.Add(Behavior_set);
                    }
                }
            }
            
            if (Triggerd.Count > 0)
            {
                List<KeyValuePair<string,string>> minkeys = Triggerd.Keys.Select(x => new { x, y = Triggerd[x] }).GroupBy(x => x.y).OrderBy(x => x.Key).First().Select(x => x.x).ToList();
                for (int i = 0; i < minkeys.Count;i++)
                {
                    finalTriggerd.Add(minkeys[i],Triggerd[minkeys[i]]);
                }
            }
            if (MobileInputsManager.target.watchingInputManger == this._inputManager)
            {
                _inputManager.ButtonRefreshForCasualTransition(SRTListForCasualTransitionbuttonRefresh, _BO_Health);
            }

            #region 状态迁移判断
            if (avaliable_casual_Transitions.Count > 0)
            {
                if (playerMode || _inputManager.PlayerInputting)
                {
                    if (MobileInputsManager.target.watchingInputManger == this._inputManager)
                        MobileInputsManager.Skillbuttonexplosion(avaliable_casual_Transitions[0].enterInput, avaliable_casual_Transitions[0].SPLevel);
                    _SkillCancelFlag.turn_off_flag();
                    ChangeState(avaliable_casual_Transitions[0].StateKey);
                    return;
                }
                if (finalTriggerd.Count >0) 
                {
                    ChangeState(finalTriggerd.First().Key.Key);
                    return;
                }
            }

            if (!now_Behavior.Capacity_Exit_Condition())
            {
                return;
            }

            if (playerMode || _inputManager.PlayerInputting)
            {
                if (!exitCommandFufilled)
                {
                    return;
                }
            }
            else
            {
                if (!now_Behavior.Strategic_exit_condition())
                {
                    return;
                } 
            }

            state_Dictionary.TryGetValue(commandWaitingState.StateKey, out try_Behavior);
            if (try_Behavior != now_Behavior)//避免战斗待机状态重复进入
            {
                ChangeState(commandWaitingState.StateKey);
            }
            #endregion
        }
    }
}