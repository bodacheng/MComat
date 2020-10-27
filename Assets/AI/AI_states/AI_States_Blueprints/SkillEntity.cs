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
        public float HP;
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
                            float _HP,
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
            this.HP = _HP;
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
                            float _HP,
                            float AITriggerDistanceMin,float AITriggerDistanceMax,
                            bool can_be_cancelled_to,
                            InputKey enterInput, InputKey exitInput,
                            int SPMove)
        {
            REAL_NAME = _StateKey;
            LEVEL = level;
            StateType = _BType;
            AT = _AT;
            HP = _HP;
            CANBECANCELLEDTO = can_be_cancelled_to;
            AI_MIN_DIS = AITriggerDistanceMin;
            AI_MAX_DIS = AITriggerDistanceMax;
            EnterInput = enterInput;
            ExitInput = exitInput;
            SP_LEVEL = SPMove;
        }
        
        // 900血，10攻击力，1打1的话接近40秒左右游戏结束。但如果存在大量远距离对火立回那么就不太好说这个时间。。
        // 那么level 是1的情况下，攻击力是1
        // 从而在技能定义表里，技能标准攻击值应该是1，存在超迅速多连击的情况多半应该少于1，而一些比较赌的重攻击则是大于1
        // HP和攻击力等比缩放。攻击是从1涨到10，HP是从10涨到100        
        // 1级的基准伤害
        // n：1   
        // ex1 ： 2
        // Ex2  ： 4
        // Ex3 ：  6
        // 100级的基准伤害
        // n ：10
        // ex1 ：20
        // Ex2 ：40
        // Ex3 ：60
        public static float ATCal(float originAT, float level)
        {
            return FightGlobalSetting._AT_coefficient * originAT * (10 + level) / 11;
        }
        public static float StoneHpCal(float originHP, float level)
        {
            return FightGlobalSetting._HP_coefficient * originHP * (10 + level) / 11;
        }
    }
    
    public enum MoveType
    {
        Move_normal = 1,
        Move_slow = 2,
        Move_fast = 3
    }
    
    public enum RushType
    {
        None = -1,
        RushBack = 2,
        Rush = 3
    }
}