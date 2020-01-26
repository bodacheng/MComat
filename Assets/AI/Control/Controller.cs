using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Soul
{
    public class Controller : MonoBehaviour
    {
        readonly List<Behavior_Transition_Set> finalDecisions = new List<Behavior_Transition_Set>();
        readonly SSIMultiDictionary Triggerd = new SSIMultiDictionary();
        
        //动态刷新按键机能
        public void ButtonFeatureRefresh(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions,List<Behavior_Transition_Set> preview)
        {
            MobileInputsManager.target.ButtonsFeatureLoad(runner,avaliable_casual_Transitions,preview);
        }
        
        // 状态的退出可以由特定的控制条件来决定时进行的判断。目前全项目只有防御这一种情况
        public bool BehaviourExitInputTrigger(Behavior_Transition_Set current_Behavior_Set)
        {
            switch(current_Behavior_Set.exitInput)
            {
                case Inputs_defined.Defend_Cancel:
                    return MobileInputsManager.target.DefendExitTrigger();
                default:
                    return true;
            }
        }

        Behavior try_Behavior;
        public void AI_RUNs(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions) // AI根据目前可作出的行为作出选择
        {
            finalDecisions.Clear();
            Triggerd.main.Clear();
            if (runner.GetNowState().Strategic_exit_condition())
            {
                for (int i = 0; i < avaliable_casual_Transitions.Count; i++)
                {
                    runner.Behaviour_Dictionary.TryGetValue(avaliable_casual_Transitions[i].StateKey, out try_Behavior);
                    List<string> Conditions = runner.RespondAndCondition[avaliable_casual_Transitions[i].StateKey];
                    for (int y = 0; y < Conditions.Count; y++)
                    {
                        if (try_Behavior.CheckTriggerCondition(Conditions[y]))//触发条件满足
                        {
                            int value = runner.RespondAndConditionPriority.Get(try_Behavior.StateKey,Conditions[y],-1);//-1是无对应值情况下的默认值
                            if (value > 0)
                            {
                                Triggerd.main.Set(try_Behavior.StateKey,Conditions[y], value);
                            }
                        }
                    }
                }
            }
            
            if (Triggerd.main.values.Count > 0)
            {
                //List<KeyValuePair<string,string>> minkeys = Triggerd.Keys.Select(x => new { x, y = Triggerd[x] }).GroupBy(x => x.y).OrderBy(x => x.Key).First().Select(x => x.x).ToList();
                List<KeyValuePair<string, string>> minkeys = Triggerd.GiveOutMin();
                for (int x = 0; x < minkeys.Count;x++)
                {
                    finalDecisions.Add(runner.Behaviour_Transition_Dictionary[minkeys[x].Key]);//Debug.Log("AI甄选的最佳决定："+minkeys[x].Key);
                }
                if (finalDecisions.Count > 0)
                {
                    int random = Random.Range(0,finalDecisions.Count);//这里虽然是随机但是毕竟随机的这几个选项在优先级上是相同的。
                    if (MobileInputsManager.target.Observing_Runner == runner)
                        MobileInputsManager.SkillButtonExplosion(finalDecisions[random].enterInput, finalDecisions[random].SPLevel);
                    runner.ChangeState(finalDecisions[random].StateKey);
                    return;
                }
            }
        }
    }
}

