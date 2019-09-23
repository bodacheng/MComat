using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

[System.Serializable]
public enum stateType : int
{
    NONE = 0,
    AC = 4,
    GR = 1,
    GM = 2,
    GI = 3,
    CT = 9,
    Def = 8,
    Hit = 6,
    KnockOff = 5,
    getUp = 10,
}

[System.Serializable]
public class SkillConfig
{
    public string RECORD_ID;//和Skills表id对应
    public string type;
    public string REAL_NAME;
    public string ShowName;
    public float ATTACK_WEIGHT;
    public stateType stateType;
    public behaviorEnterRange[] ai_trigger_ranges;
    public int SP_LEVEL = 0;
    public string AI_PRIORITY;
    public int RARITY_LEVEL = 0;

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
        stateType = stateType.NONE;
        ai_trigger_ranges = new behaviorEnterRange[] { };
        SP_LEVEL = 0;
        AI_PRIORITY = "0";
    }
    public SkillConfig(string id, string type, string keyName, string ShowName, int AT, stateType stateType, behaviorEnterRange[] ai_trigger_ranges, int SPLevel, int _skillEmergentLevel)
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
