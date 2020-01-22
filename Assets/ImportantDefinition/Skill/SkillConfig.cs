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
    public string type;
    public string REAL_NAME;
    public string ShowName;
    public float ATTACK_WEIGHT;
    public BehaviorType stateType;
    public BehaviorEnterRange[] ai_trigger_ranges;
    public int SP_LEVEL;
    public string AI_PRIORITY;
    public int RARITY_LEVEL;

    public SkillConfig Clone()
    {
        return (SkillConfig)MemberwiseClone();
    }

    public SkillConfig()
    {
        RECORD_ID = null;
        type = null;
        REAL_NAME = null;
        ShowName = null;
        ATTACK_WEIGHT = 1;
        stateType = BehaviorType.NONE;
        ai_trigger_ranges = new BehaviorEnterRange[] { };
        SP_LEVEL = 0;
        AI_PRIORITY = "0";
    }
    public SkillConfig(string id, string type, string keyName, string ShowName, int AT, BehaviorType stateType, BehaviorEnterRange[] ai_trigger_ranges, int SPLevel, int _skillEmergentLevel)
    {
        this.RECORD_ID = id;//和Skills表id对应
        this.type = type;
        this.REAL_NAME = keyName;
        this.ShowName = ShowName;
        this.ATTACK_WEIGHT = AT;
        this.stateType = stateType;
        this.ai_trigger_ranges = ai_trigger_ranges;
        this.SP_LEVEL = SPLevel;
        this.AI_PRIORITY = _skillEmergentLevel.ToString();
        this.RARITY_LEVEL = 0;
    }
}
