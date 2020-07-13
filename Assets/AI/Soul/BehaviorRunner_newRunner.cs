using System.Collections.Generic;
using UnityEngine;
using Skill;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        // 这个纯粹为了按钮效果
        public List<SkillEntity> OptionsForButtonRefresh = new List<SkillEntity>();// 按钮刷新。预告作用
        List<SkillEntity> CanTranTo = new List<SkillEntity>(); //可以启动的技能的列表
        List<string> ForcedTransitions = new List<string>();
        
        public void BehaviourTransitionEngine()
        {
            CanTranTo.Clear();
            ForcedTransitions.Clear();
            OptionsForButtonRefresh.Clear();
            
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
                        ForcedTransitions.Add(num);
                    }
                }
            }
            if (ForcedTransitions.Count > 0)
            {
                ChangeState(ForcedTransitions[0]);
                return; // Once a state is forced to trigger, there is no need for the rest of codes to run at this frame
            }
            #endregion

            #region 查找已经可以触发的后续技能
            foreach (string _Key in CurrentSKillEntity.CasualTo)
            {
                BehaviourDic.TryGetValue(_Key, out try_Behavior);
                if (try_Behavior == null)
                {
                    Debug.Log("没找到"+_Key);
                    return;
                }
                if (!try_Behavior.Capacity_enter_condition())
                {
                    continue;
                }
                SkillEntityDic.TryGetValue(_Key, out tempSKillEntity);
                OptionsForButtonRefresh.Add(tempSKillEntity);
                if ((tempSKillEntity.CANBECANCELLEDTO && _SkillCancelFlag.Cancel_Flag) || now_Behavior.Capacity_Exit_Condition())
                {
                    CanTranTo.Add(tempSKillEntity);
                }
            }
            #endregion
            
            #region 按钮技能刷新
            if (MobileInputsManager.target.Observing_Runner == this)
                MobileInputsManager.target.ButtonsFeatureLoad(OptionsForButtonRefresh);
            #endregion
            
            CalAdviceDistanceFromEnemy();
        }
               
        // 获取接下来等待释放的技能，并非是真正可触发技能，但反应了是否够气
        public List<SkillEntity> GetNextSkills()
        {
            List<SkillEntity> List = new List<SkillEntity>();
            SkillEntity _CurrentSKillEntity = new SkillEntity();
            if (now_Behavior != null)
            {
                SkillEntityDic.TryGetValue(now_Behavior.StateKey, out _CurrentSKillEntity);
            }
            if (_CurrentSKillEntity == null)
                return List;
            foreach (string _Key in _CurrentSKillEntity.CasualTo)
            {
                BehaviourDic.TryGetValue(_Key, out try_Behavior);
                if (try_Behavior == null)
                {
                    Debug.Log("没找到"+_Key);
                    continue;
                }
                if (!try_Behavior.Capacity_enter_condition())
                {
                    continue;
                }
                SkillEntityDic.TryGetValue(_Key, out tempSKillEntity);
                List.Add(tempSKillEntity);
            }
            return List;
        }

        float min,max;
        void CalAdviceDistanceFromEnemy()
        {
            min = 9999f;
            max = 0f;
            for (int index = 0; index < CanTranTo.Count; index++)
            {
                if (min > CanTranTo[index].AI_MIN_DIS)
                    min = CanTranTo[index].AI_MIN_DIS;
                if (max < CanTranTo[index].AI_MAX_DIS)
                    max = CanTranTo[index].AI_MAX_DIS;
            }
        }

        public float FixedSkillTriggerDis()
        {
            return (min + max) / 2;
        }
    }
}
