using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Soul
{
    public class Controller : MonoBehaviour
    {
        public BehaviorRunner observaing_runner;
        
        readonly List<Behavior_Transition_Set> finalDecisions = new List<Behavior_Transition_Set>();
        readonly IDictionary<KeyValuePair<string, string>, int> Triggerd = new Dictionary<KeyValuePair<string, string>, int>();// 满足了触发条件的情况对策因果组 果，因，优先级
               
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
        
        public void ButtonFeatureRefresh(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions,List<Behavior_Transition_Set> preview)
        {
            MobileInputsManager.target.ButtonsFeatureLoad(runner,avaliable_casual_Transitions,preview);
        }

        Behavior try_Behavior;
        public void AI_RUNs(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions)
        {
            finalDecisions.Clear();
            Triggerd.Clear();
            if (runner.GetNowState().Strategic_exit_condition())
            {
                for (int i = 0; i < avaliable_casual_Transitions.Count; i++)
                {
                    runner.Behaviour_Dictionary.TryGetValue(avaliable_casual_Transitions[i].StateKey, out try_Behavior);
                    List<string> Conditions = runner.RespondAndCondition[avaliable_casual_Transitions[i].StateKey];
                    for (int y = 0; y < Conditions.Count; y++)
                    {
                        if (try_Behavior.CheckTriggerCondition(Conditions[y]))//条件满足
                        {
                            // 查看情况与行为反应的优先度
                            foreach (KeyValuePair<KeyValuePair<string, string>, int> keyValuePair in runner.RespondAndConditionPriority)
                            {
                                if (keyValuePair.Key.Key == try_Behavior.StateKey && keyValuePair.Key.Value == Conditions[y])//这个检索机制应该找点什么给取代掉
                                {
                                    //Debug.Log("状态"+try_Behavior.StateKey + "遇到了情况："+Conditions[y] +"从而可能要触发");
                                    Triggerd.Add(keyValuePair);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (Triggerd.Count > 0)
            {
                List<KeyValuePair<string,string>> minkeys = Triggerd.Keys.Select(x => new { x, y = Triggerd[x] }).GroupBy(x => x.y).OrderBy(x => x.Key).First().Select(x => x.x).ToList();
                for (int x = 0; x < minkeys.Count;x++)
                {
                    //Debug.Log("AI甄选的最佳决定："+minkeys[x].Key);
                    finalDecisions.Add(runner.Behaviour_Transition_Dictionary[minkeys[x].Key]);
                }
                if (finalDecisions.Count > 0)//本质上说控制模式下finalDecisions的长度基本只能是1或0，而AI模式下则在这里进一步牵扯到多个选项做选择的问题
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

