using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Skill;

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

        void AddAITriggerConditionToBehavior(SkillEntity behavior_define)
        {
            switch(behavior_define.StateType)
            {
                case BehaviorType.MV:
                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME,"TimeToStopRunning");
                    break;
                case BehaviorType.AC:
                    KeyValuePair<string, string> keyValuePair1 = new KeyValuePair<string, string>("LosingDefendStrength", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair1);
                    ConditionAndRespondPriority.Set(keyValuePair1.Key, keyValuePair1.Value, 1);

                    KeyValuePair<string, string> keyValuePair2 = new KeyValuePair<string, string>("DangerousNearby", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair2);
                    ConditionAndRespondPriority.Set(keyValuePair2.Key,keyValuePair2.Value,3);

                    KeyValuePair<string, string> keyValuePair3 = new KeyValuePair<string, string>("DangerousClose", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair3);
                    ConditionAndRespondPriority.Set(keyValuePair3.Key, keyValuePair3.Value, 3);

                    KeyValuePair<string, string> keyValuePair4 = new KeyValuePair<string, string>("DangerousVeryClose", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair4);
                    ConditionAndRespondPriority.Set(keyValuePair4.Key, keyValuePair4.Value, 3);
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME, null);
                    break;
                case BehaviorType.CT:                    
                    KeyValuePair<string, string> keyValuePair_ct = new KeyValuePair<string, string>("DangerousClose", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair_ct);
                    ConditionAndRespondPriority.Set(keyValuePair_ct.Key,keyValuePair_ct.Value,1);
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME, null);
                    break;
                case BehaviorType.Def:
                    KeyValuePair<string, string> keyValuePair_def1 = new KeyValuePair<string, string>("DangerousVeryClose", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair_def1);
                    ConditionAndRespondPriority.Set(keyValuePair_def1.Key, keyValuePair_def1.Value, 2);

                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME, "TimeToRespond");
                    break;
                case BehaviorType.GR:
                    KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>("TimeToAttack", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePair);
                    ConditionAndRespondPriority.Set(keyValuePair.Key, keyValuePair.Value, 2);

                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME, null);
                    break;
                case BehaviorType.GI:
                    KeyValuePair<string, string> keyValuePairuu = new KeyValuePair<string, string>("TimeToAttack", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePairuu);
                    ConditionAndRespondPriority.Set(keyValuePairuu.Key, keyValuePairuu.Value, 2);
                    
                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME, null);
                    break;
                case BehaviorType.GM:
                    KeyValuePair<string, string> keyValuePairqq = new KeyValuePair<string, string>("TimeToAttack", behavior_define.REAL_NAME);
                    RegisterConditionToRespond(keyValuePairqq);
                    ConditionAndRespondPriority.Set(keyValuePairqq.Key, keyValuePairqq.Value, 2);

                    BehaviourAndStrategicExitCondition.Add(behavior_define.REAL_NAME, null);
                    break;
            }
        }

        public void FormFightingSetsByNineAndTwo(NineAndTwo nineAndTwo)
        {
            nineAndTwo.SortNineAndTwo();
            //这上下两个函数之间存在一个chuanEndCasualT0的问题，从而必须一前一后紧密连接，下次review时候可以看看代码能不能整更利索一些。
            SkillEntityDic = nineAndTwo.GenerateBeheviourSets();
            SkillEntity_List = nineAndTwo.ReturnSTSlist();//这一行于本游戏本身已经无用，但该列表牵扯到开发环境下角色技能详细的显示，以及框架本身保存xml战斗脚本的功能。
            _States_Incubator = new Behaviors_Incubator(empty_State, SkillEntityDic);
            List<BehaviorIndex_With_Behavior> Num_State_List = _States_Incubator.Num_State_List; // 理解整个系统的关键
            BehaviourDic.Clear();
            ConditionAndRespondPriority.Clear();
            BehaviourAndStrategicExitCondition.Clear();
            foreach (BehaviorIndex_With_Behavior s in Num_State_List)
            {
                if (SkillEntityDic.ContainsKey(s.num))
                {
                    s.state.StateKey = SkillEntityDic[s.num].REAL_NAME;
                    s.state.splevel = SkillEntityDic[s.num].SP_LEVEL;
                    s.state.enterInput = SkillEntityDic[s.num].EnterInput;
                    s.state.exitInput = SkillEntityDic[s.num].ExitInput;
                    s.state.triggerAtttackRangeMin = SkillEntityDic[s.num].AI_MIN_DIS;
                    s.state.triggerAtttackRangeMax = SkillEntityDic[s.num].AI_MAX_DIS;
                    AddAITriggerConditionToBehavior(SkillEntityDic[s.num]);
                    BehaviourDic.Add(new KeyValuePair<string, Behavior>(s.num, s.state));
                }
                else
                {
                    //Debug.Log("没用上的key？：" + s.num);
                }
            }
            AllConditionCodes = ConditionAndRespond.Keys.ToList();
            commandWaitingState = BehaviourDic[nineAndTwo.GetM_STS().REAL_NAME];
        }
    }
}
