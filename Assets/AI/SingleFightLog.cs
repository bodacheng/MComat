using System.Collections.Generic;

namespace Soul
{
    public class SingleFightLog
    {
        List<FightRecord> MyBehaviourHistory = new List<FightRecord>();

        public void WriteLog(FightRecord behaviourHistory)
        {
            MyBehaviourHistory.Add(behaviourHistory);
        }

        public class FightRecord
        {
            public string stateKey;
        }

        public class BehaviourFightRecord : FightRecord
        {
            public bool AI_Decided;
            public string whyIDidThis;
        }

        public class BenefitRecord : FightRecord
        {
            public bool plus;
        }
    }
}