using UnityEngine;
using System.Collections.Generic;
using Skill;
using System.Linq;
using System;

public partial class SkillConfigTable
{
    public static List<SkillConfig> RowsToSkillConfigList(List<Row> Rows)
    {
        List<SkillConfig> skillConfigs = new List<SkillConfig>();
        foreach(Row row in Rows)
        {
            SkillConfig newConfig = RowToSkillConfig(row);
            if (newConfig != null)
                skillConfigs.Add(newConfig);
                
            if (!LegalStateType(row.ATTACK_TYPE))
            {
                Debug.Log("崩溃级错误，技能Type有错：RECORDID"+ row.RECORD_ID);
            }
        }
        return skillConfigs;
    }

    public static Row SkillConfigToRow(SkillConfig skillConfig)
    {
        if (skillConfig == null)
            return null;
        Row row = new Row
        {
            RECORD_ID = skillConfig.RECORD_ID,
            USEABLE_MONSTER_TYPE = skillConfig.TYPE,
            REAL_NAME = skillConfig.REAL_NAME,
            ATTACK_WEIGHT = skillConfig.ATTACK_WEIGHT.ToString(),
            HP_WEIGHT = skillConfig.HP_WEIGHT.ToString()
        };
        
        switch (skillConfig.STATE_TYPE)
        {
            case BehaviorType.GR:
                row.ATTACK_TYPE = "GR";
                break;
            case BehaviorType.GI:
                row.ATTACK_TYPE = "GI";
                break;
            case BehaviorType.GM:
                row.ATTACK_TYPE = "GM";
                break;
            case BehaviorType.CT:
                row.ATTACK_TYPE = "CT";
                break;
            case BehaviorType.NONE:
                row.ATTACK_TYPE = "NONE";
                break;
        }
        
        if (!LegalStateType(row.ATTACK_TYPE))
        {
            Debug.Log("崩溃级错误，技能Type有错：RECORDID"+ skillConfig.RECORD_ID);
        }
        
        row.TRIGGER_DIS_MIN = skillConfig.AI_MIN_DIS.ToString();
        row.TRIGGER_DIS_MAX = skillConfig.AI_MAX_DIS.ToString();
                
        switch (skillConfig.SP_LEVEL)
        {
            case 0:
                row.SP_LEVEL = "0";
                break;
            case 1:
                row.SP_LEVEL = "1";
                break;
            case 2:
                row.SP_LEVEL = "2";
                break;
            case 3:
                row.SP_LEVEL = "3";
                break;
            default:
                row.SP_LEVEL = "-1";
                break;
        }
        row.EVENT_CODE = skillConfig.EVENT_CODE;
        row.RARITY_LEVEL = skillConfig.RARITY_LEVEL.ToString();
        return row;
    }
    
    public static SkillConfig RowToSkillConfig(Row row)
    {
        try
        {
            SkillConfig _SkillConfig = new SkillConfig
            {
                TYPE = row.USEABLE_MONSTER_TYPE,
                RECORD_ID = row.RECORD_ID,
                REAL_NAME = row.REAL_NAME,
                ATTACK_WEIGHT = float.Parse(row.ATTACK_WEIGHT),
                HP_WEIGHT = float.Parse(row.HP_WEIGHT)
            };
            
            switch (row.ATTACK_TYPE)
            {
                case "GR":
                    _SkillConfig.STATE_TYPE = BehaviorType.GR;
                    break;
                case "GI":
                    _SkillConfig.STATE_TYPE = BehaviorType.GI;
                    break;
                case "GM":
                    _SkillConfig.STATE_TYPE = BehaviorType.GM;
                    break;
                case "CT":
                    _SkillConfig.STATE_TYPE = BehaviorType.CT;
                    break;
                case "NONE":
                    _SkillConfig.STATE_TYPE = BehaviorType.NONE;
                    break;
            }
            
            if (!LegalStateType(row.ATTACK_TYPE))
            {
                Debug.Log("崩溃级错误，技能Type有错：RECORDID"+ _SkillConfig.RECORD_ID);
            }
            
            _SkillConfig.AI_MIN_DIS = float.Parse(row.TRIGGER_DIS_MIN);
            _SkillConfig.AI_MAX_DIS = float.Parse(row.TRIGGER_DIS_MAX);
            
            switch(row.SP_LEVEL)
            {
                case "0":
                    _SkillConfig.SP_LEVEL = 0;
                    break;
                case "1":
                    _SkillConfig.SP_LEVEL = 1;
                    break;
                case "2":
                    _SkillConfig.SP_LEVEL = 2;
                    break;
                case "3":
                    _SkillConfig.SP_LEVEL = 3;
                    break;
                default:
                    _SkillConfig.SP_LEVEL = -1;
                    break;
            }
            _SkillConfig.SHOW_NAME = SkillNameTable.GetSkillName(row.RECORD_ID);
            _SkillConfig.EVENT_CODE = row.EVENT_CODE;
            _SkillConfig.RARITY_LEVEL = int.Parse(row.RARITY_LEVEL);
            return _SkillConfig;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            Debug.Log(row.REAL_NAME);
            return null;
        }
    }
    
    public static List<string> GetTargetSkillRecordIds(string type, bool[] ranges, bool[] EXType, BehaviorType behaviorType, int rarelevel, int Count)
    {
        IDictionary<string, string> _Skills = GetSkillIDAndNameDic(type, ranges, EXType, behaviorType, rarelevel);
        List<int> indexes = RandomSelect.Get(0, _Skills.Count -1 , Count);
        List<string> normalSKillRecordIds = _Skills.Keys.ToList();

        List<string> targetRecordIds = new List<string>();
        for (int i = 0; i < indexes.Count; i++)
        {
            targetRecordIds.Add(normalSKillRecordIds[indexes[i]]);
        }
        return targetRecordIds;
    }
    
    public static IDictionary<string,string> GetSkillIDAndNameDic(string type, bool[] ranges, bool[] EXType, BehaviorType behaviorType, int rarelevel)// close, near, far , out , rarelevel = -1代表全部，0代表无星级技能
    {
        Dictionary<string, string> SkillIDAndNameDic = new Dictionary<string, string>
        {
            { "-1", "空" }
        };
        List<SkillConfig> list = GetSkillConfigsOfType(type);
        foreach (SkillConfig one in list)
        {
            if (behaviorType != BehaviorType.NONE && one.STATE_TYPE != behaviorType)
            {
                continue;
            }
            
            if (SkillConfig.RangeLimit(one.AI_MIN_DIS, one.AI_MAX_DIS, ranges[0], ranges[1], ranges[2]) && (one.RARITY_LEVEL == rarelevel || rarelevel == -1))
            {
                if (!SkillIDAndNameDic.ContainsKey(one.RECORD_ID))
                {
                    switch (one.SP_LEVEL)
                    {
                        case 0:
                        if (!EXType[0])
                            continue;
                        break;
                        case 1:
                        if (!EXType[1])
                            continue;
                        break;
                        case 2:
                         if (!EXType[2])
                            continue;   
                        break;
                        case 3:
                        if (!EXType[3])
                            continue;
                        break;
                    }
                    SkillIDAndNameDic.Add(one.RECORD_ID, one.REAL_NAME);
                }
                else
                {
                    Debug.Log("重复的技能ID？？："+one.RECORD_ID);
                }
            }
        }
        return SkillIDAndNameDic;
    }
}
