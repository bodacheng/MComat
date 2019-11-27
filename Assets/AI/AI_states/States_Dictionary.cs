using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

//记住我们AI系统的机制是这样：在任何时候，如果之前的状态已经可以退出，那么进入的是一个所有状态之间抽签的环节。
//比方说一个角色现在和一个敌人处于近距离，他技能组中所有的近距离攻击的absolutely rate总和与move状态的比例可能是体现出他攻击性的关键。
//而他一旦开始了一个攻击动作，那么接下来如果说他有着很长的技能串，这些技能串如果都是近距离攻击的话那他就会一直攻击，除非你在设定技能串的时候，
//每一个casual to state列表里有设置了move状态并且有一定rate值，否则这个角色就会进入一个不停攻击直到技能串到头或者被中断环节。
//上面这一套你不要问为什么我们现在就是这么设计的。

public class States_Incubator
{
    public List<AI_Num_With_State> Num_State_List;
    public List<string> StateIndexList;
    public List<string> moveTypeKeys;
    public List<string> SkillTypeKeys;//之所以要设置出这样一个列表，是为了方便对一个个加载的skill类ab包进行读取，回避掉一些其他读取流程的基础状态动画

    public States_Incubator(string anim_path, Empty_State empty_State,IDictionary<string, State_Transition_Set> toFormAttackStateList)
    {
        if (anim_path == null)
            return;

        Num_State_List = new List<AI_Num_With_State>();
        StateIndexList = new List<string>();

        Num_State_List.Add(new AI_Num_With_State("Empty", empty_State));
        StateIndexList.Add("Empty");

        Move_State move1 = new Move_State(AIMoveStyle.normal,12f, 2f);
        Move_State move2 = new Move_State(AIMoveStyle.normal,5f, 2f);
        Move_State move3 = new Move_State(AIMoveStyle.normal,15f, 2f);
        Move_State testmove = new Move_State(AIMoveStyle.test, 15f, 2f);
        move1.nextAttackStateCanRushFirst = false;
        move2.nextAttackStateCanRushFirst = false;
        move3.nextAttackStateCanRushFirst = false;
        Num_State_List.Add(new AI_Num_With_State("Move_normal", move1));
        StateIndexList.Add("Move_normal");
        Num_State_List.Add(new AI_Num_With_State("Move_slow", move2));
        StateIndexList.Add("Move_slow");
        Num_State_List.Add(new AI_Num_With_State("Move_fast", move3));
        StateIndexList.Add("Move_fast");
        Num_State_List.Add(new AI_Num_With_State("Test_Move", testmove));
        StateIndexList.Add("Test_Move");

        moveTypeKeys = new List<string>
        {
            "Move_normal",
            "Move_slow",
            "Move_fast"
        };

        Defend_State defend = new Defend_State("block", "block_break")
        {
            StateType = stateType.Def,
            nextAttackStateCanRushFirst = false
        };
        Num_State_List.Add(new AI_Num_With_State("Defend", defend));
        StateIndexList.Add("Defend");

        Idle_State victory = new Idle_State("victory");
        Idle_State zhuangbi = new Idle_State("zhuangbi");
        Death_State death = new Death_State(1f, "death");

        Num_State_List.Add(new AI_Num_With_State("Victory", victory));
        StateIndexList.Add("Victory");
        Num_State_List.Add(new AI_Num_With_State("zhuangbi", zhuangbi));
        StateIndexList.Add("zhuangbi");
        Num_State_List.Add(new AI_Num_With_State("Death", death));
        StateIndexList.Add("Death");

        Jump_State jump = new Jump_State("jump", 30f, 30f, 0.3f, true)
        {
            nextAttackStateCanRushFirst = false
        };
        Num_State_List.Add(new AI_Num_With_State("Jump", jump));
        StateIndexList.Add("Jump");

        Dash_Back_State RushBack = new Dash_Back_State
        {
            nextAttackStateCanRushFirst = false,
            StateType = stateType.AC
        };
        Num_State_List.Add(new AI_Num_With_State("RushBack", RushBack));
        StateIndexList.Add("RushBack");

        G_Ani_MoveEscape_State Rush = new G_Ani_MoveEscape_State("rush")
        {
            nextAttackStateCanRushFirst = true,
            StateType = stateType.AC
        };
        Num_State_List.Add(new AI_Num_With_State("Rush", Rush));
        StateIndexList.Add("Rush");

        Hurt_State hit = new Hurt_State()
        {
            nextAttackStateCanRushFirst = false,
            StateType = stateType.Hit
        };

        Controlled_State controlled = new Controlled_State("controlled")
        {
            nextAttackStateCanRushFirst = false
        };

        GetUp getUp = new GetUp("getup")
        {
            StateType = stateType.getUp
        };

        Knock_Off_State knock_off = new Knock_Off_State()
        {
            StateType = stateType.KnockOff,
            nextAttackStateCanRushFirst = true
        };

        Num_State_List.Add(new AI_Num_With_State("Hit", hit));
        StateIndexList.Add("Hit");
        Num_State_List.Add(new AI_Num_With_State("Controlled", controlled));
        StateIndexList.Add("Controlled");
        Num_State_List.Add(new AI_Num_With_State("KnockOff", knock_off));
        StateIndexList.Add("KnockOff");
        Num_State_List.Add(new AI_Num_With_State("getUp",getUp));
        StateIndexList.Add("getUp");

        SkillTypeKeys = new List<string>();
        foreach (KeyValuePair<string, State_Transition_Set> valuePair in toFormAttackStateList)
        {
            State_Transition_Set _set = valuePair.Value;
            if (_set == null)
                continue;
                
            if (!StateIndexList.Contains(_set.StateKey))
            {
                stateType _attackType = _set.stateType;
                switch (_attackType)
                {
                    case stateType.GI:
                        G_Attack_State _GI_Attack = new G_Attack_State(null, 30f, 1.4f, 0f, _set.StateKey, _set.skillEmergentLevel)
                        {
                            StateType = stateType.GI,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new AI_Num_With_State(_set.StateKey, _GI_Attack));
                        StateIndexList.Add(_set.StateKey);
                        if (!SkillTypeKeys.Contains(_set.StateKey)) SkillTypeKeys.Add(_set.StateKey);
                        break;
                    case stateType.GM:
                        G_M_Attack_State _GM_Attack = new G_M_Attack_State(_set.StateKey, 5f, 2f, _set.skillEmergentLevel)
                        {
                            StateType = stateType.GM,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new AI_Num_With_State(_set.StateKey, _GM_Attack));
                        StateIndexList.Add(_set.StateKey);
                        if (!SkillTypeKeys.Contains(_set.StateKey)) SkillTypeKeys.Add(_set.StateKey);
                        break;
                    case stateType.GR:
                        G_Attack_State _GR_Attack = new G_Attack_State("dash", 40f, 1.4f, 20f, _set.StateKey, _set.skillEmergentLevel)
                        {
                            StateType = stateType.GR,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new AI_Num_With_State(_set.StateKey, _GR_Attack));
                        StateIndexList.Add(_set.StateKey);
                        if (!SkillTypeKeys.Contains(_set.StateKey)) SkillTypeKeys.Add(_set.StateKey);
                        break;
                    case stateType.CT:
                        Counter_State _Counter = new Counter_State(_set.StateKey, 1.4f, 2f, 1, _set.skillEmergentLevel)
                        {
                            StateType = stateType.CT,
                            AT = _set.AT,
                            nextAttackStateCanRushFirst = false
                        };
                        Num_State_List.Add(new AI_Num_With_State(_set.StateKey, _Counter));
                        StateIndexList.Add(_set.StateKey);
                        if (!SkillTypeKeys.Contains(_set.StateKey)) SkillTypeKeys.Add(_set.StateKey);
                        break;
                    case stateType.NONE:
                        // 除了我们特别例举出来的那些基础状态外按说都是攻击性状
                        // 另外脚本保存函数中，被带入toFormAttackStateList参数的是一个全部state的列表。
                        // 所以可能存在none状态
                        break;
                }
            }else{
                //Debug.Log("正在回避状态重复定义："+ _set.StateKey);
            }
        }

        //        G_Attack_State e1_launcher = new G_Attack_State("G_Shoot_3", true, false, new behaviorEnterRange[2] { (behaviorEnterRange)3, (behaviorEnterRange)4 },true);
        //        G_Attack_State e1_ender = new G_Attack_State("SpecialAttack1", true, false, null, false);
        //        G_Attack_State G_super1 = new G_Attack_State("G_super1", false);                
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

public class States_Incubator_ForLocalResourceCheck // 用于本地脚本做成。我们姑且认为在开发环境下所有动画片段都是放在resource下
{
    public List<string> StateIndexList;
    public List<string> moveTypeKeys;

    public States_Incubator_ForLocalResourceCheck(string anim_path)
    {
        if (anim_path == null)
        {
            return;
        }

        StateIndexList = new List<string>
        {
            "Empty",
            "Move_normal",
            "Move_slow",
            "Move_fast",
            "Test_Move"
        };
        moveTypeKeys = new List<string>
        {
            "Move_normal",
            "Move_slow",
            "Move_fast",
            "Test_Move"
        };

        StateIndexList.Add("Defend");

        StateIndexList.Add("Victory");
        StateIndexList.Add("zhuangbi");
        StateIndexList.Add("Death");

        StateIndexList.Add("Jump");
        StateIndexList.Add("RushBack");
        StateIndexList.Add("Rush");

        StateIndexList.Add("Hit");
        StateIndexList.Add("Controlled");
        StateIndexList.Add("KnockOff");
        StateIndexList.Add("getUp");

        UnityEngine.Object[] GAttackStateResources = Resources.LoadAll("Animations/" + anim_path + "/G_Attack_State", typeof(AnimationClip));//这个系列的状态角色必须有个叫做“dash”的冲刺动作
        foreach (UnityEngine.Object _anim in GAttackStateResources)
        {
            if (!StateIndexList.Contains(_anim.name))
                StateIndexList.Add(_anim.name);
            else
                Debug.Log("重复的片段名，请检查资源");
        }

        UnityEngine.Object[] GAttackStateStayResources = Resources.LoadAll("Animations/" + anim_path + "/G_Attack_State_Stay", typeof(AnimationClip));//这个系列的状态角色必须有个叫做“Rush”的冲刺动作
        foreach (UnityEngine.Object _anim in GAttackStateStayResources)
        {
            if (!StateIndexList.Contains(_anim.name))
                StateIndexList.Add(_anim.name);
            else
                Debug.Log("重复的片段名，请检查资源");
        }

        UnityEngine.Object[] GShootResources = Resources.LoadAll("Animations/" + anim_path + "/Gshoot", typeof(AnimationClip));
        foreach (UnityEngine.Object _anim in GShootResources)
        {
            if (!StateIndexList.Contains(_anim.name))
                StateIndexList.Add(_anim.name);
            else
                Debug.Log("重复的片段名，请检查资源");
        }

        //        G_Attack_State e1_launcher = new G_Attack_State("G_Shoot_3", true, false, new behaviorEnterRange[2] { (behaviorEnterRange)3, (behaviorEnterRange)4 },true);
        //        G_Attack_State e1_ender = new G_Attack_State("SpecialAttack1", true, false, null, false);
        //        G_Attack_State G_super1 = new G_Attack_State("G_super1", false);

        UnityEngine.Object[] GMResources = Resources.LoadAll("Animations/" + anim_path + "/GMStates", typeof(AnimationClip));
        foreach (UnityEngine.Object _anim in GMResources)
        {
            if (!StateIndexList.Contains(_anim.name))
                StateIndexList.Add(_anim.name);
            else
                Debug.Log("重复的片段名，请检查资源");
        }
    }

    public States_Incubator_ForLocalResourceCheck(string anim_path, List<State_Transition_Set> toFormAttackStateList)
    {
        if (anim_path == null)
        {
            return;
        }

        StateIndexList = new List<string>
        {
            "Empty",
            "Move_normal",
            "Move_slow",
            "Move_fast",
            "Test_Move"
        };
        moveTypeKeys = new List<string>
        {
            "Move_normal",
            "Move_slow",
            "Move_fast",
            "Test_Move"
        };

        StateIndexList.Add("Defend");

        StateIndexList.Add("Victory");
        StateIndexList.Add("zhuangbi");
        StateIndexList.Add("Death");
        StateIndexList.Add("Jump");
        StateIndexList.Add("RushBack");
        StateIndexList.Add("Rush");

        StateIndexList.Add("Hit");
        StateIndexList.Add("Controlled");
        StateIndexList.Add("KnockOff");
        StateIndexList.Add("getUp");

        foreach (State_Transition_Set _set in toFormAttackStateList)
        {
            if (!StateIndexList.Contains(_set.StateKey))
            {
                stateType _attackType = _set.stateType;
                switch (_attackType)
                {
                    case stateType.GI:
                        StateIndexList.Add(_set.StateKey);
                        break;
                    case stateType.GM:
                        StateIndexList.Add(_set.StateKey);
                        break;
                    case stateType.GR:
                        StateIndexList.Add(_set.StateKey);
                        break;
                    case stateType.NONE:
                        // 除了我们特别例举出来的那些基础状态外按说都是攻击性状
                        // 另外脚本保存函数中，被带入toFormAttackStateList参数的是一个全部state的列表。
                        // 所以可能存在none状态
                        break;
                }
            }
            else
            {
                //Debug.Log("正在回避状态重复定义："+ _set.StateKey);
            }
        }             
    }

    public bool IfContainsKey(string key)
    {
        foreach (string _key in StateIndexList)
        {
            if (_key.GetHashCode() == key.GetHashCode())
            {
                return true;
            }
        }
        return false;
    }
}

public enum MoveType
{
    Mode1 = 1,
    Mode2 = 2,
    Mode3 = 3,
    Test = 0
}

public enum RushType
{
    None = -1,
    Jump = 1,
    RushBack = 2,
    Rush = 3
}

[System.Serializable]
public enum BehaviorEnterRange
{
    out_of_range = 3,
    far_range = 2,
    mid_range = 1,
    inner_range = 0
}

public class AI_Num_With_State
{
    public AI_Num_With_State(string num, AI_State state)
    {
        this.num = num;
        this.state = state;
    }
	public string num;
    public AI_State state;
}
