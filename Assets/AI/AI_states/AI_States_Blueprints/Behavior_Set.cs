using UnityEngine;

namespace Skill
{
    [System.Serializable]
    public class SkillEntity
    {
        public string REAL_NAME;
        public int LEVEL;
        public BehaviorType StateType;
        public float AT;
        public float AI_MIN_DIS;
        public float AI_MAX_DIS;
        public string[] CasualTo = { };
        public bool CANBECANCELLEDTO = true;
        public InputKey EnterInput = InputKey.Null;
        public InputKey ExitInput = InputKey.Null;
        public int SP_LEVEL;
        public int RARITY_LEVEL;
        [HideInInspector]
        public string[] ForcedTransitions = { };

        public SkillEntity()
        {
        }
        
        public SkillEntity(string _StateKey,
                            int LEVEL,
                            BehaviorType _attackType,
                            float _AT,
                            float AITriggerDistanceMin,float AITriggerDistanceMax,
                            string[] _casual_to_state_nums,
                            string[] _forced_to_state_nums,
                            InputKey _enterInput, InputKey _exitInput,
                            int _SPMove,
                            int _rarelevel)
        {
            this.REAL_NAME = _StateKey;
            this.LEVEL = LEVEL;
            this.StateType = _attackType;
            this.AT = _AT;
            this.AI_MIN_DIS = AITriggerDistanceMin;
            this.AI_MAX_DIS = AITriggerDistanceMax;
            this.CasualTo = _casual_to_state_nums;
            this.ForcedTransitions = _forced_to_state_nums;
            this.EnterInput = _enterInput;
            this.ExitInput = _exitInput;
            this.SP_LEVEL = _SPMove;
            this.RARITY_LEVEL = _rarelevel;

            if (this.CasualTo == null)
            {
                this.CasualTo = new string[] { };
            }
            if (this.ForcedTransitions == null)
            {
                this.ForcedTransitions = new string[] { };
            }
        }

        public SkillEntity(string _StateKey,
                            int level,
                            BehaviorType _BType,
                            float _AT,
                            float AITriggerDistanceMin,float AITriggerDistanceMax,
                            bool can_be_cancelled_to,
                            InputKey enterInput, InputKey exitInput,
                            int SPMove)
        {
            REAL_NAME = _StateKey;
            LEVEL = level;
            StateType = _BType;
            AT = _AT;
            CANBECANCELLEDTO = can_be_cancelled_to;
            AI_MIN_DIS = AITriggerDistanceMin;
            AI_MAX_DIS = AITriggerDistanceMax;
            EnterInput = enterInput;
            ExitInput = exitInput;
            SP_LEVEL = SPMove;
        }
    }
    
    public enum MoveType
    {
        Move_normal = 1,
        Move_slow = 2,
        Move_fast = 3,
        Test_Move = 0
    }
    
    public enum RushType
    {
        None = -1,
        Jump = 1,
        RushBack = 2,
        Rush = 3
    }
}
