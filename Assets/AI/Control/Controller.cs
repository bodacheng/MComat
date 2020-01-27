using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public class Controller : MonoBehaviour
    {
        readonly List<Behavior_Transition_Set> finalDecisions = new List<Behavior_Transition_Set>();
        readonly SSIMultiDictionary Triggerd = new SSIMultiDictionary();
        
        // 动态刷新按键机能
        public void ButtonFeatureRefresh(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions,List<Behavior_Transition_Set> preview)
        {
            MobileInputsManager.target.ButtonsFeatureLoad(runner, avaliable_casual_Transitions, preview);
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
        List<string> allAvaliableKeyCodes = new List<string>();
        string Condition, BehaviourCode;
        public void AI_RUNs(BehaviorRunner runner,List<Behavior_Transition_Set> avaliable_casual_Transitions) // AI根据目前可作出的行为作出选择
        {
            finalDecisions.Clear();
            Triggerd.main.Clear();
            allAvaliableKeyCodes.Clear();
            for (int i = 0; i < avaliable_casual_Transitions.Count; i++)
            {
                allAvaliableKeyCodes.Add(avaliable_casual_Transitions[i].StateKey);
            }
            
            if (runner.GetNowState().Strategic_exit_condition())
            {
                for (int y = 0; y < runner.AllConditionCodes.Count; y++)
                {
                    if (runner.GetNowState().CheckTriggerCondition(runner.AllConditionCodes[y]))
                    {
                        for (int x = 0; x < allAvaliableKeyCodes.Count; x++)
                        {
                            Condition = runner.AllConditionCodes[y];
                            BehaviourCode = allAvaliableKeyCodes[x];
                            if (runner.ConditionAndRespond[Condition].Contains(BehaviourCode))
                            {
                                Triggerd.main.Set(Condition, BehaviourCode, runner.ConditionAndRespondPriority.Get(Condition,BehaviourCode));
                            }
                        }
                    }
                }            
            }
            
            if (Triggerd.main.values.Count > 0)
            {
                List<KeyValuePair<string, string>> minkeys = Triggerd.GiveOutMin();
                for (int x = 0; x < minkeys.Count; x++)
                {
                    finalDecisions.Add(runner.Behaviour_Transition_Dictionary[minkeys[x].Value]); // Debug.Log("AI甄选的最佳决定："+minkeys[x].Value);
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

