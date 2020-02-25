using UnityEngine;
using Soul;

[System.Serializable]
public class Behavior_Transition_Set
{
    public string StateKey;
    public BehaviorType stateType;
    public float AT;
    public BehaviorEnterRange[] AI_trigger_ranges;
    public Behavior_Transition_Set[] Casual_To_Behaviours = {};
    public bool can_be_cancelled_to = true;
    public Inputs_defined enterInput = Inputs_defined.Null;
    public Inputs_defined exitInput = Inputs_defined.Null;
    public int SPLevel;
    public int rarelevel;
    [HideInInspector]
    public string[] forced_to_state_nums = {};

    public Behavior_Transition_Set()
    {
    }

    public Behavior_Transition_Set( string _StateKey,
                                    BehaviorType _attackType,
                                    float _AT,
                                    BehaviorEnterRange[] _ai_trigger_ranges,
                                    Behavior_Transition_Set[] _casual_to_state_nums, 
                                    string[] _forced_to_state_nums, 
                                    Inputs_defined _enterInput,Inputs_defined _exitInput,
                                    int _SPMove,
                                    int _rarelevel)
    {
        this.StateKey = _StateKey;
        this.stateType = _attackType;
        this.AT = _AT;
        this.AI_trigger_ranges = _ai_trigger_ranges;
        this.Casual_To_Behaviours = _casual_to_state_nums;
        this.forced_to_state_nums = _forced_to_state_nums;
        this.enterInput = _enterInput;
        this.exitInput = _exitInput;
        this.SPLevel = _SPMove;
        this.rarelevel = _rarelevel;
    }
    
    public Behavior_Transition_Set( string _StateKey,
                                    BehaviorType _attackType,
                                    float AT,
                                    BehaviorEnterRange[] ai_trigger_ranges,
                                    bool can_be_cancelled_to,
                                    Inputs_defined enterInput,Inputs_defined exitInput,
                                    int SPMove)
    {
        StateKey = _StateKey;
        stateType = _attackType;
        this.AT = AT;
        this.can_be_cancelled_to = can_be_cancelled_to;
        AI_trigger_ranges = ai_trigger_ranges;
        this.enterInput = enterInput;
        this.exitInput = exitInput;
        SPLevel = SPMove;
    }
}
