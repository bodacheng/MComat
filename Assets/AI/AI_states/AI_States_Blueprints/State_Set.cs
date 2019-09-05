using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State_Transition_Set
{
    public string StateKey;
    public stateType stateType;
    public float AT;
    public behaviorEnterRange[] ai_trigger_ranges;
    public skillEmergentLevel skillEmergentLevel;
    public State_Rate_Set[] casual_to_state_Sets;
    [HideInInspector]
	public string[] forced_to_state_nums;
    public inputs_defined enterInput = inputs_defined.Null;
    public inputs_defined exitInput = inputs_defined.Null;
    public int SPLevel;
    public int rarelevel;

    public State_Transition_Set()
    {
    }

    public State_Transition_Set(string num,
                                stateType _attackType,
                                float AT,
                                behaviorEnterRange[] ai_trigger_ranges,
                                State_Rate_Set[] casual_to_state_nums, 
                                string[] forced_to_state_nums, 
                                inputs_defined enterInput,inputs_defined exitInput,
                                int SPMove,
                                skillEmergentLevel skillEmergentLevel,
                                int rarelevel)
    {
        this.StateKey = num;
        this.stateType = _attackType;
        this.AT = AT;
        this.ai_trigger_ranges = ai_trigger_ranges;
        this.casual_to_state_Sets = casual_to_state_nums;
        this.forced_to_state_nums = forced_to_state_nums;
        this.enterInput = enterInput;
        this.exitInput = exitInput;
        this.SPLevel = SPMove;
        this.skillEmergentLevel = skillEmergentLevel;
        this.rarelevel = rarelevel;
    }

    public State_Rate_Set GetStateRateSet()
    {
        State_Rate_Set state_Rate_Set = 
            new State_Rate_Set(this.StateKey,
                               this.stateType,
                               this.AT,
                               this.ai_trigger_ranges,
                               true, 
                               this.enterInput, this.exitInput, 
                               this.SPLevel,
                               this.skillEmergentLevel);
        return state_Rate_Set;
    }
}

//我们权衡了相当时间State_Rate_Set这个类到底需不需要存在
//毕竟它的内容和State_Transition_Set基本是一样的，但最后我们还是让他留下来了。
//原因有以下
// 1.毕竟它是一个专门来形容状态与状态迁移关系的类，其中casualToNextInputDepend就是反应它性质的特殊存在。
// 打个比方说，你两个不同状态都可以向另外一同样状态迁移，但一个是取消式迁移，并且要耗气，另一个是非取消式迁移，不耗气
// 如果你没有一个专门这样一个类来描写它们的这些迁移属性，那你就做不到以上的分别处理对不对？
// 2. 打个比方说你这个系统要用来做另一个游戏，角色技能是固定的，如最早版本那样靠xml文件保存技能组，
// 这样情况下我们每个角色的技能可能就不局限于现在的9宫格机制，可能有一些特殊的设定对吧？
// 这样情况下比如说某个技能是某两个状态的一个连接？那我们就有了一个专门的地方去设置它们的连接方式
// 注意看，其实State_Rate_Set类中除了AI_State_Number与State_Transition_Set.num真正的相应之外，
// 其他所有变量都不是一码事，State_Transition_Set形容的是状态本身的属性，偏向于对首发技能触发方式的形容
// State_Rate_Set形容的是状态向另一个状态的迁移方式。
[System.Serializable]
public class State_Rate_Set //This class defines how a state should transitate to the next
{
	public string AI_State_Number;
    public stateType attackType;
    public float AT;
    public behaviorEnterRange[] ai_trigger_ranges;
    public bool can_be_cancelled_to;
    public inputs_defined enterInput = inputs_defined.Null;
    public inputs_defined exitInput = inputs_defined.Null;
    public int SPLevel = 0;
    public skillEmergentLevel skillEmergentLevel;

    public State_Rate_Set()
    {        
    }
	public State_Rate_Set(string AI_State_Number,
                          stateType _attackType,
                          float AT,
                          behaviorEnterRange[] ai_trigger_ranges,
                          bool can_be_cancelled_to,inputs_defined enterInput,inputs_defined exitInput,int SPlevel, skillEmergentLevel skillEmergentLevel)
	{
		this.AI_State_Number = AI_State_Number;
        this.attackType = _attackType;
        this.AT = AT;
        this.ai_trigger_ranges = ai_trigger_ranges;
		this.can_be_cancelled_to = can_be_cancelled_to;
        this.enterInput = enterInput;
        this.exitInput = exitInput;
        this.SPLevel = SPlevel;
        this.skillEmergentLevel = skillEmergentLevel;
    }
}

