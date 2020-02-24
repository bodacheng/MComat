using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        public IDictionary<string, List<string>> ConditionAndRespond = new Dictionary<string, List<string>>();
        public MultiDictionary<string, string, int> ConditionAndRespondPriority = new MultiDictionary<string, string, int>();
        public IDictionary<string, string> BehaviourAndStrategicExitCondition = new Dictionary<string, string>();
        public List<string> AllConditionCodes;

        void RegisterConditionToRespond(KeyValuePair<string, string> ConditionAndBeheviourcode)//string target_beheviour,string condition_code
        {
            if (ConditionAndRespond.ContainsKey(ConditionAndBeheviourcode.Key))
            {
                ConditionAndRespond[ConditionAndBeheviourcode.Key].Add(ConditionAndBeheviourcode.Value);
            }
            else{
                ConditionAndRespond.Add(ConditionAndBeheviourcode.Key,new List<string>() { ConditionAndBeheviourcode.Value});
            }
        }

        void AddAITriggerConditionToBehavior(Behavior_Transition_Set behavior_define)
        {
            switch(behavior_define.stateType)
            {
                case BehaviorType.MV:
                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey,"TimeToStopRunning");
                    break;
                case BehaviorType.AC:
                    KeyValuePair<string, string> keyValuePair1 = new KeyValuePair<string, string>("LosingDefendStrength", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair1);
                    ConditionAndRespondPriority.Set(keyValuePair1.Key, keyValuePair1.Value, 1);

                    KeyValuePair<string, string> keyValuePair2 = new KeyValuePair<string, string>("DangerousNearby", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair2);
                    ConditionAndRespondPriority.Set(keyValuePair2.Key,keyValuePair2.Value,3);

                    KeyValuePair<string, string> keyValuePair3 = new KeyValuePair<string, string>("DangerousClose", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair3);
                    ConditionAndRespondPriority.Set(keyValuePair3.Key, keyValuePair3.Value, 3);

                    KeyValuePair<string, string> keyValuePair4 = new KeyValuePair<string, string>("DangerousVeryClose", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair4);
                    ConditionAndRespondPriority.Set(keyValuePair4.Key, keyValuePair4.Value, 2);

                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey, null);
                    break;
                case BehaviorType.CT:                    
                    KeyValuePair<string, string> keyValuePair_ct = new KeyValuePair<string, string>("DangerousClose", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair_ct);
                    ConditionAndRespondPriority.Set(keyValuePair_ct.Key,keyValuePair_ct.Value,1);
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey, null);
                    break;
                case BehaviorType.Def:
                    KeyValuePair<string, string> keyValuePair_def1 = new KeyValuePair<string, string>("DangerousVeryClose", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair_def1);
                    ConditionAndRespondPriority.Set(keyValuePair_def1.Key, keyValuePair_def1.Value, 2);

                    KeyValuePair<string, string> keyValuePair_def2 = new KeyValuePair<string, string>("MayBeDefend", behavior_define.StateKey);
                    RegisterConditionToRespond(keyValuePair_def2);
                    ConditionAndRespondPriority.Set(keyValuePair_def2.Key,keyValuePair_def2.Value,3);
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey, "TimeToRespond");
                    break;
                case BehaviorType.GR:
                    List<BehaviorEnterRange> behaviorEnterRanges = behavior_define.AI_trigger_ranges.ToList();
                    if (behaviorEnterRanges.Contains(BehaviorEnterRange.inner_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Close", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges.Contains(BehaviorEnterRange.mid_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Near", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges.Contains(BehaviorEnterRange.far_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Far", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges.Contains(BehaviorEnterRange.out_of_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_OutterRange", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                                        
                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey, null);
                    break;
                case BehaviorType.GI:
                    List<BehaviorEnterRange> behaviorEnterRanges2 = behavior_define.AI_trigger_ranges.ToList();
                    if (behaviorEnterRanges2.Contains(BehaviorEnterRange.inner_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Close", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges2.Contains(BehaviorEnterRange.mid_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Near", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);

                        KeyValuePair<string, string> keyValuePair_gi = new KeyValuePair<string, string>("DangerousNearButEnemyFar", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gi);
                        ConditionAndRespondPriority.Set(keyValuePair_gi.Key, keyValuePair_gi.Value, 2);
                    }
                    if (behaviorEnterRanges2.Contains(BehaviorEnterRange.far_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Far", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);

                        KeyValuePair<string, string> keyValuePair_gi = new KeyValuePair<string, string>("DangerousNearButEnemyFar", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gi);
                        ConditionAndRespondPriority.Set(keyValuePair_gi.Key, keyValuePair_gi.Value, 2);
                    }
                    if (behaviorEnterRanges2.Contains(BehaviorEnterRange.out_of_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_OutterRange", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);

                        KeyValuePair<string, string> keyValuePair_gi = new KeyValuePair<string, string>("DangerousNearButEnemyFar", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gi);
                        ConditionAndRespondPriority.Set(keyValuePair_gi.Key, keyValuePair_gi.Value, 2);
                    }
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey, null);
                    break;
                case BehaviorType.GM:
                    List<BehaviorEnterRange> behaviorEnterRanges3 = behavior_define.AI_trigger_ranges.ToList();
                    if (behaviorEnterRanges3.Contains(BehaviorEnterRange.inner_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Close", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges3.Contains(BehaviorEnterRange.mid_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Near", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges3.Contains(BehaviorEnterRange.far_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_Far", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    if (behaviorEnterRanges3.Contains(BehaviorEnterRange.out_of_range))
                    {
                        KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack_OutterRange", behavior_define.StateKey);
                        RegisterConditionToRespond(keyValuePair_gr);
                        ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    }
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.StateKey, null);
                    break;
            }
        }
    
        public void FormFightingSetsByNineAndTwo(NineAndTwo nineAndTwo)
        {
            nineAndTwo.SortNineAndTwo();
            //这上下两个函数之间存在一个chuanEndCasualT0的问题，从而必须一前一后紧密连接，下次review时候可以看看代码能不能整更利索一些。
            Behaviour_Transition_Dictionary = nineAndTwo.GenerateBeheviourSets();
            State_Transition_Set_List = nineAndTwo.ReturnSTSlist();//这一行于本游戏本身已经无用，但该列表牵扯到开发环境下角色技能详细的显示，以及框架本身保存xml战斗脚本的功能。
            
            bool hasD, hasR;
            hasD = nineAndTwo.GetDConfig() != null;
            hasR = nineAndTwo.GetRConfig() != null;

            _States_Incubator = new Behaviors_Incubator(empty_State,this.Behaviour_Transition_Dictionary);
            List<BehaviorIndex_With_Behavior> Num_State_List = _States_Incubator.Num_State_List; // 理解整个系统的关键
            Behaviour_Dictionary = new Dictionary<string, Behavior>();
            ConditionAndRespondPriority = new MultiDictionary<string, string, int>();
            
            foreach (BehaviorIndex_With_Behavior s in Num_State_List)
            {
                if (Behaviour_Transition_Dictionary.ContainsKey(s.num))
                {
                    s.state.StateKey = Behaviour_Transition_Dictionary[s.num].StateKey;
                    s.state.splevel = Behaviour_Transition_Dictionary[s.num].SPLevel;
                    s.state.enterInput = Behaviour_Transition_Dictionary[s.num].enterInput;
                    s.state.exitInput = Behaviour_Transition_Dictionary[s.num].exitInput;
                    AddAITriggerConditionToBehavior(Behaviour_Transition_Dictionary[s.num]);
                    Behaviour_Dictionary.Add(new KeyValuePair<string, Behavior>(s.num, s.state));
                }
                else
                {
                    Debug.Log("没用上的key？：" + s.num);
                }
            }
            AllConditionCodes = ConditionAndRespond.Keys.ToList();
            if (nineAndTwo.GetM_STS() != null)
            {
                commandWaitingState = Behaviour_Dictionary[nineAndTwo.GetM_STS().StateKey];
            }
        }
    }
}
