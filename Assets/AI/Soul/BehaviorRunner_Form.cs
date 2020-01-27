using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Soul
{
    public partial class BehaviorRunner : MonoBehaviour
    {
        public IDictionary<string, List<string>> ConditionAndRespond = new Dictionary<string, List<string>>();
        public List<string> AllConditionCodes;
        public MultiDictionary<string, string, int> ConditionAndRespondPriority = new MultiDictionary<string, string, int>();
        
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

        void AddAITriggerConditionToBehavior(Behavior behavior)
        {
            switch(behavior.StateType)
            {
                case BehaviorType.MV:
                    behavior.strategic_exit_condition_code = "TimeToStopRunning";
                    break;
                case BehaviorType.AC:
                    KeyValuePair<string, string> keyValuePair1 = new KeyValuePair<string, string>("LosingDefendStrength", behavior.StateKey);
                    KeyValuePair<string, string> keyValuePair2 = new KeyValuePair<string, string>("DangerousNearby", behavior.StateKey);
                    RegisterConditionToRespond(keyValuePair1);
                    RegisterConditionToRespond(keyValuePair2);
                    ConditionAndRespondPriority.Set(keyValuePair1.Key,keyValuePair1.Value,1);
                    ConditionAndRespondPriority.Set(keyValuePair2.Key,keyValuePair2.Value,2);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.CT:                    
                    KeyValuePair<string, string> keyValuePair_ct = new KeyValuePair<string, string>("DangerousClose", behavior.StateKey);
                    RegisterConditionToRespond(keyValuePair_ct);
                    ConditionAndRespondPriority.Set(keyValuePair_ct.Key,keyValuePair_ct.Value,1);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.Def:
                    KeyValuePair<string, string> keyValuePair_def1 = new KeyValuePair<string, string>("DangerousVeryClose", behavior.StateKey);
                    KeyValuePair<string, string> keyValuePair_def2 = new KeyValuePair<string, string>("MayBeDefend", behavior.StateKey);
                    RegisterConditionToRespond(keyValuePair_def1);
                    RegisterConditionToRespond(keyValuePair_def2);
                    ConditionAndRespondPriority.Set(keyValuePair_def1.Key,keyValuePair_def1.Value,2);
                    ConditionAndRespondPriority.Set(keyValuePair_def2.Key,keyValuePair_def2.Value,3);
                    
                    behavior.strategic_exit_condition_code = "TimeToRespond";
                    break;
                case BehaviorType.GR:
                    KeyValuePair<string, string> keyValuePair_gr = new KeyValuePair<string, string>("TimeToAttack", behavior.StateKey);
                    RegisterConditionToRespond(keyValuePair_gr);
                    ConditionAndRespondPriority.Set(keyValuePair_gr.Key,keyValuePair_gr.Value,2);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.GI:
                    KeyValuePair<string, string> keyValuePair_gi = new KeyValuePair<string, string>("TimeToAttack", behavior.StateKey);
                    RegisterConditionToRespond(keyValuePair_gi);
                    ConditionAndRespondPriority.Set(keyValuePair_gi.Key,keyValuePair_gi.Value,2);
                    
                    behavior.strategic_exit_condition_code = null;
                    break;
                case BehaviorType.GM:
                    KeyValuePair<string, string> keyValuePair_gm = new KeyValuePair<string, string>("TimeToDashAttack", behavior.StateKey);
                    RegisterConditionToRespond(keyValuePair_gm);
                    ConditionAndRespondPriority.Set(keyValuePair_gm.Key,keyValuePair_gm.Value,2);
                    
                    behavior.strategic_exit_condition_code = null;                  
                    break;
                default:
                    break;
            }
        }
    
        public void FormFightingSetsByNineAndTwo(string type, NineAndTwo nineAndTwo)
        {
            nineAndTwo.SortNineAndTwo();
            //这上下两个函数之间存在一个chuanEndCasualT0的问题，从而必须一前一后紧密连接，下次review时候可以看看代码能不能整更利索一些。
            Behaviour_Transition_Dictionary = nineAndTwo.GenerateBeheviourSets();
            State_Transition_Set_List = nineAndTwo.ReturnSTSlist();//这一行于本游戏本身已经无用，但该列表牵扯到开发环境下角色技能详细的显示，以及框架本身保存xml战斗脚本的功能。
            
            bool hasD, hasR;
            hasD = nineAndTwo.GetDConfig() != null;
            hasR = nineAndTwo.GetRConfig() != null;

            _States_Incubator = new Behaviors_Incubator(type, empty_State,this.Behaviour_Transition_Dictionary);
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
                    s.state.behaviorEnterRanges = Behaviour_Transition_Dictionary[s.num].AI_trigger_ranges;
                    AddAITriggerConditionToBehavior(s.state);
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
