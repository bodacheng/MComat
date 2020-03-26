using System.Collections.Generic;
using Skill;
using UnityEngine;

namespace Soul
{
    public class SingleFightLog
    {        
        readonly List<FightRecord> MyBehaviourHistory = new List<FightRecord>();
        public IDictionary<string, int> StateTriggerTimes = new Dictionary<string, int>();
        public IDictionary<string, int> StateInterruptedTimes = new Dictionary<string, int>();
        
        public void WriteLog(FightRecord behaviourHistory)
        {
            MyBehaviourHistory.Add(behaviourHistory);
        }

        public class FightRecord
        {
            public string stateKey;
            public BehaviorType behaviorType;
        }

        public class BehaviourFightRecord : FightRecord
        {
            public bool AI_Decided;
            public string whyIDidThis;
        }

        public class PositiveRecord : FightRecord
        {
        }
        public class NegativeRecord : FightRecord
        {
        }

        // 这个函数待写。我们设想可以每次战斗结束后进行个总的报告，来分析各个技能有没有取得正面效应。
        public void Summary()
        {
            for (int i = 0; i < MyBehaviourHistory.Count; i++)
            {
                if (MyBehaviourHistory[i] is BehaviourFightRecord)
                {
                    if (StateTriggerTimes.ContainsKey(MyBehaviourHistory[i].stateKey))
                    {
                        StateTriggerTimes[MyBehaviourHistory[i].stateKey] += 1;
                    }
                    else
                    {
                        StateTriggerTimes.Add(MyBehaviourHistory[i].stateKey, 1);
                    }
                    
                    if (i != MyBehaviourHistory.Count - 1)
                    {
                        if (MyBehaviourHistory[i+1] is NegativeRecord)
                        {
                            if (StateInterruptedTimes.ContainsKey(MyBehaviourHistory[i].stateKey))
                            {
                                StateInterruptedTimes[MyBehaviourHistory[i].stateKey] += 1;
                            }
                            else
                            {
                                StateInterruptedTimes.Add(MyBehaviourHistory[i].stateKey, 1);
                            }
                        }
                    }
                }
            }
        }
        
        public void Clear()
        {
            MyBehaviourHistory.Clear();
            StateTriggerTimes.Clear();
            StateInterruptedTimes.Clear();
        }

        readonly IDictionary<string, int> skillnobenefitlog = new Dictionary<string, int>();
        public void AnalysisLog(IDictionary<string, Behavior> Behaviour_Dictionary)
        {
            if (MyBehaviourHistory.Count < 5)
            {
                return;
            }
            skillnobenefitlog.Clear();
            int Count = MyBehaviourHistory.Count;
            for (int i = 2; i < MyBehaviourHistory.Count; i++)
            {
                FightRecord fightRecord = MyBehaviourHistory[i];
                if (fightRecord is BehaviourFightRecord)
                {
                    if ((MyBehaviourHistory[i - 2] is BehaviourFightRecord) && (MyBehaviourHistory[i - 1] is BehaviourFightRecord))// 发招两次没有获得正面效益
                    {
                        if (skillnobenefitlog.ContainsKey(MyBehaviourHistory[i - 2].stateKey))
                        {
                            skillnobenefitlog[MyBehaviourHistory[i - 2].stateKey] += 1;
                        }
                        else
                        {
                            skillnobenefitlog.Add(MyBehaviourHistory[i - 2].stateKey, 1);
                        }
                    }
                }
            }
            
            foreach (KeyValuePair<string,int> keyValuePair in skillnobenefitlog)
            {
                if (keyValuePair.Value > 2)
                {
                    Behavior behavior = Behaviour_Dictionary[keyValuePair.Key];
                    behavior.triggerAtttackRangeMin = Mathf.Clamp(behavior.triggerAtttackRangeMin -1 ,0, 5);
                    behavior.triggerAtttackRangeMax = Mathf.Clamp(behavior.triggerAtttackRangeMax - 1, 4, 10);
                    Debug.Log("触发距离调整:"+ keyValuePair.Key);
                }
            }
            MyBehaviourHistory.Clear();
        }
    }
}