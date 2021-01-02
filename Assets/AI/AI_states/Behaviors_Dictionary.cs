using System.Collections.Generic;
using UnityEngine;
using Skill;

namespace Soul
{
    public class BehaviorsIncubator
    {
        public IDictionary<string, Behavior> BehaviorDic;
        // 技能动画列表（不包括基础动画）
        public List<string> SkillTypeKeys;

        public BehaviorsIncubator(Empty_State empty_State, IDictionary<string, SkillEntity> ToFormAttackStateList)
        {
            BehaviorDic = new Dictionary<string, Behavior>
            {
                { "Empty", empty_State }
            };
            Idle_State victory = new Idle_State("victory");
            Idle_State zhuangbi = new Idle_State("zhuangbi");
            Death_State death = new Death_State();
            BehaviorDic.Add("Victory", victory);
            BehaviorDic.Add("zhuangbi", zhuangbi);
            BehaviorDic.Add("Death", death);
            Move_State move = new Move_State(AIMoveMode.normal, 10f, 1f)
            {
                StateType = BehaviorType.MV,
                nextAttackStateCanRushFirst = false
            };
            BehaviorDic.Add("Move", move);

            if (FightGlobalSetting._hasDefend)
            {
                Defend_State defend = new Defend_State("block", "block_break")
                {
                    StateType = BehaviorType.Def,
                    nextAttackStateCanRushFirst = false
                };
                BehaviorDic.Add("Defend", defend);
            }

            Hurt_State hit = new Hurt_State()
            {
                nextAttackStateCanRushFirst = false,
                StateType = BehaviorType.Hit
            };

            Knock_Off_State knock_off = new Knock_Off_State()
            {
                StateType = BehaviorType.KnockOff,
                nextAttackStateCanRushFirst = true
            };
            GetUp getUp = new GetUp("getup")
            {
                StateType = BehaviorType.GetUp
            };
            BehaviorDic.Add("Hit", hit);
            BehaviorDic.Add("KnockOff", knock_off);
            BehaviorDic.Add("getUp", getUp);

            SkillTypeKeys = new List<string>();
            foreach (KeyValuePair<string, SkillEntity> valuePair in ToFormAttackStateList)
            {
                SkillEntity _set = valuePair.Value;
                if (_set == null)
                    continue;

                if (!BehaviorDic.Keys.Contains(_set.REAL_NAME))
                {
                    switch (_set.StateType)
                    {
                        case BehaviorType.AC:
                            switch (_set.REAL_NAME)
                            {
                                case "RushBack":
                                    Dash_Back_State RushBack = new Dash_Back_State
                                    {
                                        nextAttackStateCanRushFirst = false,
                                        StateType = BehaviorType.AC
                                    };
                                    BehaviorDic.Add("RushBack", RushBack);
                                    break;
                                case "Rush":
                                    G_Ani_MoveEscape_State Rush = new G_Ani_MoveEscape_State("rush")
                                    {
                                        nextAttackStateCanRushFirst = true,
                                        StateType = BehaviorType.AC
                                    };
                                    BehaviorDic.Add("Rush", Rush);
                                    break;
                            }
                            break;
                        case BehaviorType.GI:
                            G_Attack_State _GI_Attack = new G_Attack_State(null, 0f, 0f, 10f, _set.REAL_NAME)
                            {
                                StateType = BehaviorType.GI,
                                AT = _set.AT,
                                nextAttackStateCanRushFirst = false
                            };
                            BehaviorDic.Add(_set.REAL_NAME, _GI_Attack);
                            if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                            break;
                        case BehaviorType.GM:
                            G_M_Attack_State _GM_Attack = new G_M_Attack_State(_set.REAL_NAME)
                            {
                                StateType = BehaviorType.GM,
                                AT = _set.AT,
                                nextAttackStateCanRushFirst = false
                            };
                            BehaviorDic.Add(_set.REAL_NAME, _GM_Attack);
                            if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                            break;
                        case BehaviorType.GR:
                            G_Attack_State _GR_Attack = new G_Attack_State("dash", 40f, 1.4f, 10f, _set.REAL_NAME)
                            {
                                StateType = BehaviorType.GR,
                                AT = _set.AT,
                                nextAttackStateCanRushFirst = false
                            };
                            BehaviorDic.Add(_set.REAL_NAME, _GR_Attack);
                            if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                            break;
                        case BehaviorType.CT:
                            Counter_State _Counter = new Counter_State(_set.REAL_NAME)
                            {
                                StateType = BehaviorType.CT,
                                AT = _set.AT,
                                nextAttackStateCanRushFirst = false
                            };
                            BehaviorDic.Add(_set.REAL_NAME, _Counter);
                            if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                            break;
                        case BehaviorType.NONE:
                            // 除了我们特别例举出来的那些基础状态外按说都是攻击性状
                            // 另外脚本保存函数中，被带入toFormAttackStateList参数的是一个全部state的列表。
                            // 所以可能存在none状态
                            break;
                    }
                }
                else
                {
                    Debug.Log("正在回避状态重复定义：" + _set.REAL_NAME);
                }
            }
        }
    }
}