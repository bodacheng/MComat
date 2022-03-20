using System.Collections.Generic;
using UnityEngine;
using Skill;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        // 这个纯粹为了按钮效果 按钮刷新。预告作用
        public List<SkillEntity> optionsForButtonRefresh = new List<SkillEntity>();
        readonly List<SkillEntity> _canTranTo = new List<SkillEntity>(); //可以启动的技能的列表
        readonly List<string> _forcedTransitions = new List<string>();
        
        void BehaviourTransitionEngine()
        {
            _canTranTo.Clear();
            _forcedTransitions.Clear();
            optionsForButtonRefresh.Clear();
            
            if (_nowBehavior != null)
            {
                SkillEntityDic.TryGetValue(_nowBehavior.StateKey, out CurrentSKillEntity);
            }
                        
            #region Forced state transition 
            if (CurrentSKillEntity.ForcedTransitions != null)
            {
                for (var i = 0; i < CurrentSKillEntity.ForcedTransitions.Length; i++)
                {
                    BehaviourDic.TryGetValue(CurrentSKillEntity.ForcedTransitions[i], out _tryBehavior);
                    if (_tryBehavior.Force_enter_condition())
                    {
                        _forcedTransitions.Add(CurrentSKillEntity.ForcedTransitions[i]);
                    }
                }
            }
            if (_forcedTransitions.Count > 0)
            {
                ChangeState(_forcedTransitions[0]);
                return; // Once a state is forced to trigger, there is no need for the rest of codes to run at this frame
            }
            #endregion

            #region 查找已经可以触发的后续技能
            foreach (string _Key in CurrentSKillEntity.CasualTo)
            {
                BehaviourDic.TryGetValue(_Key, out _tryBehavior);
                if (_tryBehavior == null)
                {
                    Debug.Log("没找到"+_Key);
                    return;
                }
                if (!_tryBehavior.Capacity_enter_condition())
                {
                    continue;
                }
                SkillEntityDic.TryGetValue(_Key, out tempSKillEntity);
                optionsForButtonRefresh.Add(tempSKillEntity);
                if ((tempSKillEntity.CANBECANCELLEDTO && _SkillCancelFlag.Cancel_Flag) || _nowBehavior.Capacity_Exit_Condition())
                {
                    _canTranTo.Add(tempSKillEntity);
                }
            }
            #endregion
            
            #region 按钮技能刷新
            InputsManager?.ButtonsFeatureLoad(optionsForButtonRefresh);
            #endregion
            
            CalAdviceDistanceFromEnemy();
        }
        
        // 获取接下来等待释放的技能，并非是真正可触发技能，但反应了是否够气
        public List<SkillEntity> GetNextSkills()
        {
            List<SkillEntity> List = new List<SkillEntity>();
            SkillEntity _CurrentSKillEntity = new SkillEntity();
            if (_nowBehavior != null)
            {
                SkillEntityDic.TryGetValue(_nowBehavior.StateKey, out _CurrentSKillEntity);
            }
            if (_CurrentSKillEntity == null)
                return List;
            foreach (string _Key in _CurrentSKillEntity.CasualTo)
            {
                BehaviourDic.TryGetValue(_Key, out _tryBehavior);
                if (_tryBehavior == null)
                {
                    Debug.Log("没找到"+_Key);
                    continue;
                }
                if (!_tryBehavior.Capacity_enter_condition())
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
            for (var index = 0; index < _canTranTo.Count; index++)
            {
                if (min > _canTranTo[index].AIAttrs.AI_MIN_DIS)
                    min = _canTranTo[index].AIAttrs.AI_MIN_DIS;
                if (max < _canTranTo[index].AIAttrs.AI_MAX_DIS)
                    max = _canTranTo[index].AIAttrs.AI_MAX_DIS;
            }
        }
        
        public float FixedSkillTriggerDis()
        {
            return (min + max) / 2;
        }
    }
}
