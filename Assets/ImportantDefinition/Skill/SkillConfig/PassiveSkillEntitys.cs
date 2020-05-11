
namespace Skill
{
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
            
            D_SE = hasDefend ? 
            new SkillEntity
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