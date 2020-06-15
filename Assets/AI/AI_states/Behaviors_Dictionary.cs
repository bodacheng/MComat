using System.Collections.Generic;
using UnityEngine;
using Soul;
using Skill;

public class Behaviors_Incubator
{
    public List<BehaviorIndex_With_Behavior> Num_State_List;
    public List<string> StateIndexList;
    public List<string> SkillTypeKeys;//之所以要设置出这样一个列表，是为了方便对一个个加载的skill类ab包进行读取，回避掉一些其他读取流程的基础状态动画

    public Behaviors_Incubator(Empty_State empty_State, IDictionary<string, SkillEntity> ToFormAttackStateList)
    {
        Num_State_List = new List<BehaviorIndex_With_Behavior>();
        StateIndexList = new List<string>();

        Num_State_List.Add(new BehaviorIndex_With_Behavior("Empty", empty_State));
        StateIndexList.Add("Empty");
        Idle_State victory = new Idle_State("victory");
        Idle_State zhuangbi = new Idle_State("zhuangbi");
        Death_State death = new Death_State(1f, "death");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Victory", victory));
        StateIndexList.Add("Victory");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("zhuangbi", zhuangbi));
        StateIndexList.Add("zhuangbi");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Death", death));
        StateIndexList.Add("Death");

        Move_State move1 = new Move_State(AIMoveMode.normal,10f, 1f);
        Move_State move2 = new Move_State(AIMoveMode.normal,2f, 2f);
        Move_State move3 = new Move_State(AIMoveMode.normal,3f, 2f);
        Move_State testmove = new Move_State(AIMoveMode.test, 1f, 2f);
        move1.StateType = BehaviorType.MV;
        move2.StateType = BehaviorType.MV;
        move3.StateType = BehaviorType.MV;
        testmove.StateType = BehaviorType.MV;
        move1.nextAttackStateCanRushFirst = false;
        move2.nextAttackStateCanRushFirst = false;
        move3.nextAttackStateCanRushFirst = false;
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Move_normal", move1));
        StateIndexList.Add("Move_normal");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Move_slow", move2));
        StateIndexList.Add("Move_slow");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Move_fast", move3));
        StateIndexList.Add("Move_fast");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Test_Move", testmove));
        StateIndexList.Add("Test_Move");

        if (FightGlobalSetting._hasDefend)
        {
            Defend_State defend = new Defend_State("block", "block_break")
            {
                StateType = BehaviorType.Def,
                nextAttackStateCanRushFirst = false
            };
            Num_State_List.Add(new BehaviorIndex_With_Behavior("Defend", defend));
            StateIndexList.Add("Defend");
        }

        Dash_Back_State RushBack = new Dash_Back_State
        {
            nextAttackStateCanRushFirst = false,
            StateType = BehaviorType.AC
        };
        Num_State_List.Add(new BehaviorIndex_With_Behavior("RushBack", RushBack));
        StateIndexList.Add("RushBack");
        G_Ani_MoveEscape_State Rush = new G_Ani_MoveEscape_State("rush")
        {
            nextAttackStateCanRushFirst = true,
            StateType = BehaviorType.AC
        };
        Num_State_List.Add(new BehaviorIndex_With_Behavior("Rush", Rush));
        StateIndexList.Add("Rush");

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

        Num_State_List.Add(new BehaviorIndex_With_Behavior("Hit", hit));
        StateIndexList.Add("Hit");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("KnockOff", knock_off));
        StateIndexList.Add("KnockOff");
        Num_State_List.Add(new BehaviorIndex_With_Behavior("getUp",getUp));
        StateIndexList.Add("getUp");

        SkillTypeKeys = new List<string>();
        foreach (KeyValuePair<string, SkillEntity> valuePair in ToFormAttackStateList)
        {
            SkillEntity _set = valuePair.Value;
            if (_set == null)
                continue;

            if (!StateIndexList.Contains(_set.REAL_NAME))
            {
                switch (_set.StateType)
                {
                    case BehaviorType.GI:
                        G_Attack_State _GI_Attack = new G_Attack_State(null, 0f, 0f, 0f, _set.REAL_NAME)
                        {
                            StateType = BehaviorType.GI,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new BehaviorIndex_With_Behavior(_set.REAL_NAME, _GI_Attack));
                        StateIndexList.Add(_set.REAL_NAME);
                        if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                        break;
                    case BehaviorType.GM:
                        G_M_Attack_State _GM_Attack = new G_M_Attack_State(_set.REAL_NAME)
                        {
                            StateType = BehaviorType.GM,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new BehaviorIndex_With_Behavior(_set.REAL_NAME, _GM_Attack));
                        StateIndexList.Add(_set.REAL_NAME);
                        if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                        break;
                    case BehaviorType.GR:
                        G_Attack_State _GR_Attack = new G_Attack_State("dash", 40f, 1.4f, 20f, _set.REAL_NAME)
                        {
                            StateType = BehaviorType.GR,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new BehaviorIndex_With_Behavior(_set.REAL_NAME, _GR_Attack));
                        StateIndexList.Add(_set.REAL_NAME);
                        if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                        break;
                    case BehaviorType.CT:
                        Counter_State _Counter = new Counter_State(_set.REAL_NAME)
                        {
                            StateType = BehaviorType.CT,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new BehaviorIndex_With_Behavior(_set.REAL_NAME, _Counter));
                        StateIndexList.Add(_set.REAL_NAME);
                        if (!SkillTypeKeys.Contains(_set.REAL_NAME)) SkillTypeKeys.Add(_set.REAL_NAME);
                        break;
                    case BehaviorType.NONE:
                        // 除了我们特别例举出来的那些基础状态外按说都是攻击性状
                        // 另外脚本保存函数中，被带入toFormAttackStateList参数的是一个全部state的列表。
                        // 所以可能存在none状态
                        break;
                }
            }else{
                //Debug.Log("正在回避状态重复定义："+ _set.REAL_NAME);
            }
        }             
	}

    public bool IfContainsKey(string key)
    {
        foreach(string _key in StateIndexList)
        {
            if (_key.GetHashCode() == key.GetHashCode())
            {
                return true;
            }
        }
        return false;
    }
}

public class Behaviors_Incubator_ForLocalResourceCheck // 用于本地脚本做成。我们姑且认为在开发环境下所有动画片段都是放在resource下
{
    public List<string> BehaviorIndexList;
    readonly List<SkillConfig> SkillConfigs;
    
    public Behaviors_Incubator_ForLocalResourceCheck(string anim_path)
    {
        if (anim_path == null)
        {
            return;
        }
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        
        SkillConfigs = SkillConfigTable.GetSkillConfigsOfType(anim_path);
        BehaviorIndexList = new List<string>
        {
            "Empty",
            "Move_normal",
            "Move_slow",
            "Move_fast",
            "Test_Move"
        };

        BehaviorIndexList.Add("Victory");
        BehaviorIndexList.Add("zhuangbi");
        BehaviorIndexList.Add("Death");
        BehaviorIndexList.Add("RushBack");
        BehaviorIndexList.Add("Rush");
        BehaviorIndexList.Add("Hit");
        BehaviorIndexList.Add("KnockOff");
        BehaviorIndexList.Add("getUp");

        if (FightGlobalSetting._hasDefend)
        {
            BehaviorIndexList.Add("Defend");
        }

        foreach (SkillConfig skillConfig in SkillConfigs)
        {
            if (!BehaviorIndexList.Contains(skillConfig.REAL_NAME))
                BehaviorIndexList.Add(skillConfig.REAL_NAME);
            else
                Debug.Log("重复的片段名，请检查资源");
        }
    }

    public Behaviors_Incubator_ForLocalResourceCheck(string anim_path, List<SkillEntity> toFormAttackStateList)
    {
        if (anim_path == null)
        {
            return;
        }
        
        BehaviorIndexList = new List<string>
        {
            "Empty",
            "Move_normal",
            "Move_slow",
            "Move_fast",
            "Test_Move"
        };

        BehaviorIndexList.Add("Victory");
        BehaviorIndexList.Add("zhuangbi");
        BehaviorIndexList.Add("Death");
        BehaviorIndexList.Add("RushBack");
        BehaviorIndexList.Add("Rush");
        BehaviorIndexList.Add("Hit");
        BehaviorIndexList.Add("KnockOff");
        BehaviorIndexList.Add("getUp");
        if (FightGlobalSetting._hasDefend)
        {
            BehaviorIndexList.Add("Defend");
        }
        
        foreach (SkillEntity _set in toFormAttackStateList)
        {
            if (!BehaviorIndexList.Contains(_set.REAL_NAME))
            {
                BehaviorType _attackType = _set.StateType;
                switch (_attackType)
                {
                    case BehaviorType.GI:
                        BehaviorIndexList.Add(_set.REAL_NAME);
                        break;
                    case BehaviorType.GM:
                        BehaviorIndexList.Add(_set.REAL_NAME);
                        break;
                    case BehaviorType.GR:
                        BehaviorIndexList.Add(_set.REAL_NAME);
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
                Debug.Log("正在回避状态重复定义："+ _set.REAL_NAME);
            }
        }             
    }
}

namespace Skill
{
    public class BehaviorIndex_With_Behavior
    {
        public BehaviorIndex_With_Behavior(string num, Behavior state)
        {
            this.num = num;
            this.state = state;
        }
        public string num;
        public Behavior state;
    }
}
