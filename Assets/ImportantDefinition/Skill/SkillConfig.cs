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
    Hit_early = 6,
    Hit_late = 7,
    KnockOff = 5,
}

[System.Serializable]
public class SkillConfig
{
    public int id;//和Skills表id对应
    public string type;
    public string keyName;
    public string ShowName;
    public int SkillPoint;
    public stateType stateType;
    public behaviorEnterRange[] ai_trigger_ranges;
    public EX SPLevel = EX.normal;
    public bool canAirTrigger;
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
        id = -1;//和Skills表id对应
        type = null;
        keyName = null;
        ShowName = null;
        SkillPoint = 0;
        stateType = stateType.NONE;
        ai_trigger_ranges = new behaviorEnterRange[] { };
        SPLevel = EX.normal;
        canAirTrigger = false;
        skillEmergentLevel = skillEmergentLevel.none;
    }
    public SkillConfig(int id, string type, string keyName, string ShowName, int SkillPoint,
                       stateType stateType, behaviorEnterRange[] ai_trigger_ranges, EX SPLevel, bool canAirTrigger, skillEmergentLevel _skillEmergentLevel)
    {
        this.id = id;//和Skills表id对应
        this.type = type;
        this.keyName = keyName;
        this.ShowName = ShowName;
        this.SkillPoint = SkillPoint;
        this.stateType = stateType;
        this.ai_trigger_ranges = ai_trigger_ranges;
        this.SPLevel = SPLevel;
        this.canAirTrigger = canAirTrigger;
        this.skillEmergentLevel = _skillEmergentLevel;
        rarelevel = 0;
    }
}

[System.Serializable]
public enum EX : int
{
    NULL = -1,
    normal = 0,
    EX1 = 1,
    EX2 = 2,
    EX3 = 3
}
