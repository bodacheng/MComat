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
        public BehaviorEnterRange[] ai_trigger_ranges;
        public int SP_LEVEL;
        public string CAN_LEVELUP;
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
            ai_trigger_ranges = new BehaviorEnterRange[] { };
            SP_LEVEL = 1;
            CAN_LEVELUP = "0";
        }
        public SkillConfig(string id, string type, string keyName, string ShowName, int AT, BehaviorType stateType, BehaviorEnterRange[] ai_trigger_ranges, int SPLevel, int CAN_LEVELUP)
        {
            this.RECORD_ID = id;//和Skills表id对应
            this.TYPE = type;
            this.REAL_NAME = keyName;
            this.SHOW_NAME = ShowName;
            this.ATTACK_WEIGHT = AT;
            this.STATE_TYPE = stateType;
            this.ai_trigger_ranges = ai_trigger_ranges;
            this.SP_LEVEL = SPLevel;
            this.CAN_LEVELUP = CAN_LEVELUP.ToString();
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
                case MoveType.Mode1:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_normal", "normal move", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                case MoveType.Mode2:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_slow", "normal move", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                case MoveType.Mode3:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_fast", "normal move", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                case MoveType.Test:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Test_Move", "测试用移动状态(角色站着不动)", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                default:
                    this.MConfig = new SkillConfig
                        (
                            null, null, "Move_normal", "normal move", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
            }

            this.DConfig = hasDefend
                ? new SkillConfig
                        (
                            null, null, "Defend", "防衛", 0, BehaviorType.NONE, null, 0, 0
                        )
                : null;

            switch (RStyle)
            {
                case RushType.Jump:
                    this.RConfig = new SkillConfig
                        (
                            null, null, "Jump", "Jump", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                case RushType.Rush:
                    this.RConfig = new SkillConfig
                        (
                            null, null, "Rush", "Rush", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                case RushType.RushBack:
                    this.RConfig = new SkillConfig
                        (
                            null, null, "RushBack", "RushBack", 0, BehaviorType.NONE, null, 0, 0
                        );
                    break;
                case RushType.None:
                    this.RConfig = null;
                    break;
            }
        }
    }
}

