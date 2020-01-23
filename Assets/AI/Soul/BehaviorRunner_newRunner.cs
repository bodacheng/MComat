using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        // 这个纯粹为了按钮效果
        public List<Behavior_Transition_Set> SRTListForCasualTransitionbuttonRefresh = new List<Behavior_Transition_Set>();        
        List<Behavior_Transition_Set> avaliable_casual_Transitions = new List<Behavior_Transition_Set>();
        List<string> avaliable_forced_Transitions = new List<string>();
        
        public void BehaviourTransitionEngine()
        {
            avaliable_casual_Transitions.Clear();
            avaliable_forced_Transitions.Clear();
            SRTListForCasualTransitionbuttonRefresh.Clear();
            if (now_Behavior != null)
            {
                Behaviour_Transition_Dictionary.TryGetValue(now_Behavior.StateKey, out CurrentBehaviorTransitionSet);
            }
            
            #region Forced state transition 
            if (CurrentBehaviorTransitionSet.forced_to_state_nums != null && CurrentBehaviorTransitionSet.forced_to_state_nums.Length > 0)
            {
                foreach (string num in CurrentBehaviorTransitionSet.forced_to_state_nums)
                {
                    Behaviour_Dictionary.TryGetValue(num, out try_Behavior);
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

            foreach (Behavior_Transition_Set Behavior_set in CurrentBehaviorTransitionSet.Casual_To_Behaviours)
            {
                Behaviour_Dictionary.TryGetValue(Behavior_set.StateKey, out try_Behavior);
                if (!try_Behavior.Capacity_enter_condition())
                {
                    continue;
                }
                //能走到这里说明 Capacity_enter_condition 已经通过
                SRTListForCasualTransitionbuttonRefresh.Add(Behavior_set);//avaliable_casual_Transitions是真正可以启动的技能的列表，SRTListForCasualTransitionbuttonRefresh根据作用得有个预告作用
                if ((Behavior_set.can_be_cancelled_to && _SkillCancelFlag.Cancel_Flag) || now_Behavior.Capacity_Exit_Condition())
                {
                    avaliable_casual_Transitions.Add(Behavior_set);
                }
            }

            #region 做决定
            controller.Control(this, avaliable_casual_Transitions);
            #endregion

            if (!now_Behavior.Capacity_Exit_Condition())
                return;
                
            Behaviour_Dictionary.TryGetValue(commandWaitingState.StateKey, out try_Behavior);
            if (try_Behavior != now_Behavior)//避免战斗待机状态重复进入
            {
                ChangeState(commandWaitingState.StateKey);
            }
        }
    }
}
