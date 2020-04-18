using System.Collections.Generic;
using UnityEngine;
using Skill;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        // 这个纯粹为了按钮效果
        public List<SkillEntity> SRTListForCasualTransitionbuttonRefresh = new List<SkillEntity>();        
        List<SkillEntity> avaliable_casual_Transitions = new List<SkillEntity>();
        List<string> avaliable_forced_Transitions = new List<string>();
                      
        public void BehaviourTransitionEngine()
        {
            avaliable_casual_Transitions.Clear();
            avaliable_forced_Transitions.Clear();
            SRTListForCasualTransitionbuttonRefresh.Clear();
            if (now_Behavior != null)
            {
                SkillEntityDic.TryGetValue(now_Behavior.StateKey, out CurrentSKillEntity);
            }
            
            if (CurrentSKillEntity == null)
            {
                Debug.Log("???" + now_Behavior.StateKey);
                return;
            }
            
            #region Forced state transition 
            if (CurrentSKillEntity.ForcedTransitions != null && CurrentSKillEntity.ForcedTransitions.Length > 0)
            {
                foreach (string num in CurrentSKillEntity.ForcedTransitions)
                {
                    BehaviourDic.TryGetValue(num, out try_Behavior);
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
            
            #region 查找已经可以触发的后续技能
            foreach (SkillEntity Behavior_set in CurrentSKillEntity.CasualTo)
            {
                BehaviourDic.TryGetValue(Behavior_set.REAL_NAME, out try_Behavior);
                if (!try_Behavior.Capacity_enter_condition())
                {
                    continue;
                }
                //能走到这里说明 Capacity_enter_condition 已经通过
                SRTListForCasualTransitionbuttonRefresh.Add(Behavior_set);//avaliable_casual_Transitions是真正可以启动的技能的列表，SRTListForCasualTransitionbuttonRefresh根据作用得有个预告作用
                if ((Behavior_set.CANBECANCELLEDTO && _SkillCancelFlag.Cancel_Flag) || now_Behavior.Capacity_Exit_Condition())
                {
                    avaliable_casual_Transitions.Add(Behavior_set);
                }
            }
            #endregion
            
            #region 按钮技能刷新
            if (MobileInputsManager.target.Observing_Runner == this)
                MobileInputsManager.target.ButtonsFeatureLoad(SRTListForCasualTransitionbuttonRefresh);
            #endregion

            #region 决策制定
            controller.PlayerControll(this,avaliable_casual_Transitions,!((MobileInputsManager.playerMode || MobileInputsManager.inputting) && MobileInputsManager.target.Observing_Runner == this));
            #endregion

            CalAdviceDistanceFromEnemy();
        }

        float min,max;
        void CalAdviceDistanceFromEnemy()
        {
            min = 9999f;
            max = 0f;
            for (int index = 0; index < avaliable_casual_Transitions.Count; index++)
            {
                if (min > avaliable_casual_Transitions[index].AI_MIN_DIS)
                    min = avaliable_casual_Transitions[index].AI_MIN_DIS;
                if (max < avaliable_casual_Transitions[index].AI_MAX_DIS)
                    max = avaliable_casual_Transitions[index].AI_MAX_DIS;
            }
        }

        public float Test()
        {
            return (min + max) / 2;
        }
    }
}
