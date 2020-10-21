using System.Collections.Generic;
using Skill;
using UnityEngine;

public partial class NineAndTwo
{
    public enum SkillEditError
    {
        UnBalanced,
        RepeatedSkill,
        NoNormalStart,
        Perfect
    }
    
    // 判断技能组是否合法。包括了首技能有无普攻，有无重复，总点数是否平衡 这三方面
    public static SkillEditError CheckEdit(string A1, string A2, string A3, string B1, string B2, string B3, string C1, string C2, string C3)
    {
        // 第一列技能必须有普通技能
        if (CheckStartSKills(A1, B1, C1) == SkillEditError.NoNormalStart)
        {
            return SkillEditError.NoNormalStart;
        }
        
        if (!CheckRepeat(A1, A2, A3, B1, B2, B3, C1, C2, C3))
        {
            return SkillEditError.RepeatedSkill;
        }
        
        int wholePoint = SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
        return wholePoint < 0 ? SkillEditError.UnBalanced : SkillEditError.Perfect;
    }
    
    // 当前总分。不问技能组是否合法
    public static int SkillBalancePoint(string A1skillid, string A2skillid, string A3skillid, string B1skillid, string B2skillid, string B3skillid, string C1skillid, string C2skillid, string C3skillid)
    {
        SkillConfig _SkillConfigA1 = SkillConfigTable.GetSkillConfigByID(A1skillid);
        SkillConfig _SkillConfigA2 = SkillConfigTable.GetSkillConfigByID(A2skillid);
        SkillConfig _SkillConfigA3 = SkillConfigTable.GetSkillConfigByID(A3skillid);
        SkillConfig _SkillConfigB1 = SkillConfigTable.GetSkillConfigByID(B1skillid);
        SkillConfig _SkillConfigB2 = SkillConfigTable.GetSkillConfigByID(B2skillid);
        SkillConfig _SkillConfigB3 = SkillConfigTable.GetSkillConfigByID(B3skillid);
        SkillConfig _SkillConfigC1 = SkillConfigTable.GetSkillConfigByID(C1skillid);
        SkillConfig _SkillConfigC2 = SkillConfigTable.GetSkillConfigByID(C2skillid);
        SkillConfig _SkillConfigC3 = SkillConfigTable.GetSkillConfigByID(C3skillid);
        List<SkillConfig> allnineskill = new List<SkillConfig>();
        
        if (_SkillConfigA1 != null)
            allnineskill.Add(_SkillConfigA1);
        if (_SkillConfigA2 != null)
            allnineskill.Add(_SkillConfigA2);
        if (_SkillConfigA3 != null)
            allnineskill.Add(_SkillConfigA3);
        if (_SkillConfigB1 != null)
            allnineskill.Add(_SkillConfigB1);
        if (_SkillConfigB2 != null)
            allnineskill.Add(_SkillConfigB2);
        if (_SkillConfigB3 != null)
            allnineskill.Add(_SkillConfigB3);
        if (_SkillConfigC1 != null)
            allnineskill.Add(_SkillConfigC1);
        if (_SkillConfigC2 != null)
            allnineskill.Add(_SkillConfigC2);
        if (_SkillConfigC3 != null)
            allnineskill.Add(_SkillConfigC3);
            
        int wholeskillpoint = 0;
        for (int i = 0; i < allnineskill.Count; i++)
        {
            switch (allnineskill[i].SP_LEVEL)
            {
                case 0:
                    wholeskillpoint += 10;
                    break;
                case 1:
                    wholeskillpoint -= 10;
                    break;
                case 2:
                    wholeskillpoint -= 20;
                    break;
                case 3:
                    wholeskillpoint -= 30;
                    break;
                case -1:
                    break;
            }
        }
        return wholeskillpoint;
    }
    
    // 查看技能组内是否有重复 false :不合法，有重复  true：合法，无重复
    static bool CheckRepeat(string A1, string A2, string A3, string B1, string B2, string B3, string C1, string C2, string C3)
    {
        // 检查技能重复
        List<string> checkSame = new List<string>
        {
            A1,
            A2,
            A3,
            B1,
            B2,
            B3,
            C1,
            C2,
            C3
        };
        
        for (int i = 0; i < checkSame.Count; i++)
        {
            if (i != checkSame.Count - 1 && SkillConfigTable.GetSkillConfigByID(checkSame[i]) != null)
            {
                for (int y = i + 1; y < checkSame.Count; y++)
                {
                    if (checkSame[i] == checkSame[y])
                        return false;
                }
            }
        }
        
        return true;
    }
    
    // 检查起始技能有没有普通技能
    static SkillEditError CheckStartSKills(string a1skill, string a2skill, string a3skill)
    {
        // 第一列技能必须有普通技能
        List<string> NormalSkillsOfAList = new List<string>();            
        SkillConfig _SkillConfigA1 = SkillConfigTable.GetSkillConfigByID(a1skill);
        SkillConfig _SkillConfigB1 = SkillConfigTable.GetSkillConfigByID(a2skill);
        SkillConfig _SkillConfigC1 = SkillConfigTable.GetSkillConfigByID(a3skill);
        
        if (_SkillConfigA1 != null && _SkillConfigA1.SP_LEVEL == 0)
            NormalSkillsOfAList.Add(_SkillConfigA1.REAL_NAME);
        if (_SkillConfigB1 != null && _SkillConfigB1.SP_LEVEL == 0)
            NormalSkillsOfAList.Add(_SkillConfigB1.REAL_NAME);
        if (_SkillConfigC1 != null && _SkillConfigC1.SP_LEVEL == 0)
            NormalSkillsOfAList.Add(_SkillConfigC1.REAL_NAME);
            
        return NormalSkillsOfAList.Count == 0 ? SkillEditError.NoNormalStart : SkillEditError.Perfect;
    }
}