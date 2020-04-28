namespace Skill
{
    [System.Serializable]
    public enum BehaviorType
    {
        NONE = 0,
        MV = 7,
        AC = 4,
        GR = 1,
        GM = 2,
        GI = 3,
        CT = 9,
        Def = 8,
        Hit = 6,
        KnockOff = 5,
        GetUp = 10,
    }

    [System.Serializable]
    public class SkillConfig
    {
        public string RECORD_ID;//和Skills表id对应
        public string TYPE;
        public string REAL_NAME;
        public string SHOW_NAME;
        public float ATTACK_WEIGHT;
        public float HP_WEIGHT;
        public BehaviorType STATE_TYPE;
        public float AI_MIN_DIS;
        public float AI_MAX_DIS;
        public int SP_LEVEL;
        public string EVENT_CODE;
        public int RARITY_LEVEL;

        public SkillConfig Clone()
        {
            return (SkillConfig)MemberwiseClone();
        }
                
        public SkillConfig()
        {
            RECORD_ID = null;
            TYPE = null;
            REAL_NAME = null;
            SHOW_NAME = null;
            ATTACK_WEIGHT = 1;
            HP_WEIGHT = 1;
            STATE_TYPE = BehaviorType.NONE;
            SP_LEVEL = 1;
            EVENT_CODE = null;
        }
                
        public static bool RangeLimit(float dis_min ,float dis_max, bool close, bool near, bool far, bool outrange) // 待修 
        {
            if (dis_max > dis_min)
            {
                if ((dis_min <= 5 && dis_max >= 0f) && close)
                {
                    return true;
                }
                if ((dis_min <= 10f && dis_max >= 5f) && near)
                {
                    return true;
                }
                if ((dis_min <= 15f && dis_max >= 10f) && far)
                {
                    return true;
                }
                if ((dis_min <= 50f && dis_max >= 15f) && outrange)
                {
                    return true;
                }
            }
            return false;
        }
    }
    
    public class PassiveSkillEntitys
    {
        public MoveType moveType;
        public bool hasDefend;
        public RushType rushType;
        
        public SkillEntity M_SE;
        public SkillEntity D_SE;
        public SkillEntity R_SE;
        
        public PassiveSkillEntitys(MoveType moveType, bool hasDefend, RushType RStyle)
        {
            this.moveType = moveType;
            this.hasDefend = hasDefend;
            rushType = RStyle;

            switch (moveType)
            {
                case MoveType.Move_normal:
                    M_SE = new SkillEntity
                    {
                        REAL_NAME = "Move_normal",
                        LEVEL = 0,
                        StateType = BehaviorType.MV,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Null,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                case MoveType.Move_slow:
                    M_SE = new SkillEntity
                    {
                        REAL_NAME = "Move_slow",
                        LEVEL = 0,
                        StateType = BehaviorType.MV,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Null,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                case MoveType.Move_fast:
                    M_SE = new SkillEntity()
                    {
                        REAL_NAME = "Move_fast",
                        LEVEL = 0,
                        StateType = BehaviorType.MV,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Null,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                case MoveType.Test_Move:
                    M_SE = new SkillEntity()
                    {
                        REAL_NAME = "Test_Move",
                        LEVEL = 0,
                        StateType = BehaviorType.MV,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Null,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                default:
                    M_SE = new SkillEntity
                    {
                        REAL_NAME = "Move_normal",
                        LEVEL = 0,
                        StateType = BehaviorType.MV,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Null,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
            }

            D_SE = hasDefend
                ? new SkillEntity
                    {
                        REAL_NAME = "Defend",
                        LEVEL = 0,
                        StateType = BehaviorType.Def,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Defend,
                        ExitInput = InputKey.Defend_Cancel,
                        SP_LEVEL = 0
                    } : null;

            switch (RStyle)
            {
                case RushType.Jump:
                    R_SE = new SkillEntity
                    {
                        REAL_NAME = "Jump",
                        LEVEL = 0,
                        StateType = BehaviorType.AC,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Acc,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                case RushType.Rush:
                    R_SE = new SkillEntity
                    {
                        REAL_NAME = "Rush",
                        LEVEL = 0,
                        StateType = BehaviorType.AC,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Acc,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                case RushType.RushBack:
                    R_SE = new SkillEntity
                    {
                        REAL_NAME = "RushBack",
                        LEVEL = 0,
                        StateType = BehaviorType.AC,
                        AT = 0,
                        HP = 0,
                        AI_MIN_DIS = -1,
                        AI_MAX_DIS = -1,
                        CasualTo = null,
                        ForcedTransitions = null,
                        EnterInput = InputKey.Acc,
                        ExitInput = InputKey.Null,
                        SP_LEVEL = 0
                    };
                    break;
                case RushType.None:
                    R_SE = null;
                    break;
            }
        }
    }
}

