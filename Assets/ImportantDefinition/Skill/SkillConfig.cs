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
        public BehaviorType STATE_TYPE;
        public float AI_MIN_DIS;
        public float AI_MAX_DIS;
        public int SP_LEVEL;
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
            STATE_TYPE = BehaviorType.NONE;
            SP_LEVEL = 1;
        }
        public SkillConfig(string id, string type, string keyName, string ShowName, int AT, BehaviorType stateType, float AITriggerDistanceMin,float AITriggerDistanceMax, int SPLevel)
        {
            this.RECORD_ID = id;//和Skills表id对应
            this.TYPE = type;
            this.REAL_NAME = keyName;
            this.SHOW_NAME = ShowName;
            this.ATTACK_WEIGHT = AT;
            this.STATE_TYPE = stateType;
            this.AI_MIN_DIS = AITriggerDistanceMin;
            this.AI_MAX_DIS = AITriggerDistanceMax;
            this.SP_LEVEL = SPLevel;
            this.RARITY_LEVEL = 0;
        }
    }

    public class PassiveSkillConfigs
    {
        public MoveType moveType;
        public bool hasDefend;
        public RushType rushType;

        public SkillConfig MConfig;
        public SkillConfig DConfig;
        public SkillConfig RConfig;

        public PassiveSkillConfigs(MoveType moveType, bool hasDefend, RushType RStyle)
        {
            this.moveType = moveType;
            this.hasDefend = hasDefend;
            this.rushType = RStyle;

            switch (moveType)
            {
                case MoveType.Move_normal:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_normal", "normal move", 0, 0,0, 0, 0
                        );
                    break;
                case MoveType.Move_slow:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_slow", "normal move", 0, 0, 0, 0, 0
                        );
                    break;
                case MoveType.Move_fast:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_fast", "normal move",  0, 0, 0, 0, 0
                        );
                    break;
                case MoveType.Test_Move:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Test_Move", "测试用移动状态(角色站着不动)",  0, 0, 0, 0, 0
                        );
                    break;
                default:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_normal", "normal move",  0, 0, 0, 0, 0
                        );
                    break;
            }

            this.DConfig = hasDefend
                ? new SkillConfig
                        (
                            null, null, "Defend", "防衛",  0, 0, 0, 0, 0
                        )
                : null;

            switch (RStyle)
            {
                case RushType.Jump:
                    this.RConfig = new SkillConfig
                        (
                            null, null, "Jump", "Jump", 0, 0, 0, 0, 0
                        );
                    break;
                case RushType.Rush:
                    this.RConfig = new SkillConfig
                        (
                            null, null, "Rush", "Rush", 0, 0, 0, 0, 0
                        );
                    break;
                case RushType.RushBack:
                    this.RConfig = new SkillConfig
                        (
                            null, null, "RushBack", "RushBack",0, 0, 0, 0, 0
                        );
                    break;
                case RushType.None:
                    this.RConfig = null;
                    break;
            }
        }
    }
}

