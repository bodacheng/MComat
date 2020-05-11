using System;

namespace Skill
{
    [Serializable]
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

    [Serializable]
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
}

