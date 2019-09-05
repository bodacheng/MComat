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
    public string id;//和Skills表id对应
    public string type;
    public string keyName;
    public string ShowName;
    public float AT;
    public stateType stateType;
    public behaviorEnterRange[] ai_trigger_ranges;
    public int SPLevel = 0;
    public skillEmergentLevel skillEmergentLevel;
    public int rarelevel = 0;

    public SkillConfig Clone()
    {
        return (SkillConfig)MemberwiseClone();
    }

    public bool rangeLimit(bool close,bool near, bool far,bool outrange)
    {
        List<behaviorEnterRange> behaviorEnterRangesList = this.ai_trigger_ranges.ToList();
        for (int i = 0; i < behaviorEnterRangesList.Count;i++)
        {
            switch(behaviorEnterRangesList[i])
            {
                case behaviorEnterRange.inner_range:
                    if (close)
                        return true;
                    break;
                case behaviorEnterRange.mid_range:
                    if (near)
                        return true;
                    break;
                case behaviorEnterRange.far_range:
                    if (far)
                        return true;
                    break;
                case behaviorEnterRange.out_of_range:
                    if (outrange)
                        return true;
                    break;
            }
        }
        return false;
    }

    public SkillConfig()
    {
        id = null;
        type = null;
        keyName = null;
        ShowName = null;
        AT = 1;
        stateType = stateType.NONE;
        ai_trigger_ranges = new behaviorEnterRange[] { };
        SPLevel = 0;
        skillEmergentLevel = skillEmergentLevel.none;
    }
    public SkillConfig(string id, string type, string keyName, string ShowName, int AT,
                       stateType stateType, behaviorEnterRange[] ai_trigger_ranges, int SPLevel, skillEmergentLevel _skillEmergentLevel)
    {
        this.id = id;//和Skills表id对应
        this.type = type;
        this.keyName = keyName;
        this.ShowName = ShowName;
        this.AT = AT;
        this.stateType = stateType;
        this.ai_trigger_ranges = ai_trigger_ranges;
        this.SPLevel = SPLevel;
        this.skillEmergentLevel = _skillEmergentLevel;
        this.rarelevel = 0;
    }
}
