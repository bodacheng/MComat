using UnityEngine;
using System.Collections.Generic;
using Skill;
using System.Linq;
using System;
using mainMenu;

public partial class SkillConfigTable
{
    public static List<SkillConfig> RowsToSkillConfigList(List<Row> Rows)
    {
        List<SkillConfig> skillConfigs = new List<SkillConfig>();
        for (int i = 0; i < Rows.Count; i++)
        {
            SkillAIAttrs.Row aiRow = SkillAIAttrs.Find_RECORD_ID(Rows[i].RECORD_ID);
            SkillConfig newConfig = RowToSkillConfig(Rows[i], aiRow);
            if (newConfig != null)
                skillConfigs.Add(newConfig);
                
            if (!LegalStateType(Rows[i].ATTACK_TYPE))
            {
                Debug.Log("崩溃级错误，技能Type有错：RECORDID"+ Rows[i].RECORD_ID);
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
            TYPE = skillConfig.TYPE,
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
    
    public static SkillConfig RowToSkillConfig(Row row, SkillAIAttrs.Row aiRow)
    {
        try
        {
            SkillConfig _SkillConfig = new SkillConfig
            {
                TYPE = row.TYPE,
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
            
            _SkillConfig.AIAttrs.AI_MIN_DIS = float.Parse(aiRow.TRIGGER_DIS_MIN);
            _SkillConfig.AIAttrs.AI_MAX_DIS = float.Parse(aiRow.TRIGGER_DIS_MAX);
            _SkillConfig.AIAttrs.height = int.Parse(aiRow.TRIGGER_HEIGHT);
            
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
        
    public static IDictionary<string,string> GetSkillIDAndNameDic(SkillStonesBox.StoneFilterForm filterForm)
    {
        Dictionary<string, string> SkillIDAndNameDic = new Dictionary<string, string>();
        List<SkillConfig> list = GetSkillConfigsOfType(filterForm.type);
        foreach (SkillConfig one in list)
        {
            if (filterForm.BType != BehaviorType.NONE && filterForm.BType != one.STATE_TYPE)
            {
                continue;
            }
            
            if (SkillConfig.RangeLimit(one.AIAttrs.AI_MIN_DIS, one.AIAttrs.AI_MAX_DIS, filterForm.close, filterForm.near, filterForm.far) 
                && (filterForm.rare.ToList().Contains(one.RARITY_LEVEL))
                && filterForm.exType.ToList().Contains(one.SP_LEVEL))
            {
                if (one.RECORD_ID == null)
                {
                    Debug.Log(one.REAL_NAME);
                }
                if (!SkillIDAndNameDic.ContainsKey(one.RECORD_ID))
                {
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
