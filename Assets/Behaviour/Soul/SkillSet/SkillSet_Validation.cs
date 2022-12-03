using System.Collections.Generic;
using Skill;

public partial class SkillSet
{
    public enum SkillEditError
    {
        UnBalanced,
        RepeatedSkill,
        NoNormalStart,
        NotFull,
        Perfect
    }
    
    // 判断技能组是否合法。包括了首技能有无普攻，有无重复，总点数是否平衡 这三方面
    public static SkillEditError CheckEdit(string A1, string A2, string A3, string B1, string B2, string B3, string C1, string C2, string C3)
    {
        if (!(hasStone(A1) && hasStone(A2) && hasStone(A3) &&
              hasStone(B1) && hasStone(B2) && hasStone(B3) &&
              hasStone(C1) && hasStone(C2) && hasStone(C3)))
        {
            return SkillEditError.NotFull;
        }
        
        // 第一列技能必须有普通技能
        if (CheckStartSKills(A1, B1, C1) == SkillEditError.NoNormalStart)
        {
            return SkillEditError.NoNormalStart;
        }
        
        if (!CheckRepeat(A1, A2, A3, B1, B2, B3, C1, C2, C3))
        {
            return SkillEditError.RepeatedSkill;
        }
        
        bool hasStone(string skillID)
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(skillID);
            return skillConfig != null;
        }
        
        var wholePoint = SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
        return wholePoint < 0 ? SkillEditError.UnBalanced : SkillEditError.Perfect;
    }
    
    // 当前总分。不问技能组是否合法
    public static int SkillBalancePoint(string a1SkillId, string a2SkillId, string a3SkillId, string b1SkillId, string b2SkillId, string b3SkillId, string c1SkillId, string c2SkillId, string c3SkillId)
    {
        var skillConfigA1 = SkillConfigTable.GetSkillConfig(a1SkillId);
        var skillConfigA2 = SkillConfigTable.GetSkillConfig(a2SkillId);
        var skillConfigA3 = SkillConfigTable.GetSkillConfig(a3SkillId);
        var skillConfigB1 = SkillConfigTable.GetSkillConfig(b1SkillId);
        var skillConfigB2 = SkillConfigTable.GetSkillConfig(b2SkillId);
        var skillConfigB3 = SkillConfigTable.GetSkillConfig(b3SkillId);
        var skillConfigC1 = SkillConfigTable.GetSkillConfig(c1SkillId);
        var skillConfigC2 = SkillConfigTable.GetSkillConfig(c2SkillId);
        var skillConfigC3 = SkillConfigTable.GetSkillConfig(c3SkillId);
        
        var skillConfigs = new List<SkillConfig>();
        
        if (skillConfigA1 != null)
            skillConfigs.Add(skillConfigA1);
        if (skillConfigA2 != null)
            skillConfigs.Add(skillConfigA2);
        if (skillConfigA3 != null)
            skillConfigs.Add(skillConfigA3);
        if (skillConfigB1 != null)
            skillConfigs.Add(skillConfigB1);
        if (skillConfigB2 != null)
            skillConfigs.Add(skillConfigB2);
        if (skillConfigB3 != null)
            skillConfigs.Add(skillConfigB3);
        if (skillConfigC1 != null)
            skillConfigs.Add(skillConfigC1);
        if (skillConfigC2 != null)
            skillConfigs.Add(skillConfigC2);
        if (skillConfigC3 != null)
            skillConfigs.Add(skillConfigC3);
            
        var balancePoint = 0;
        foreach (var t in skillConfigs)
        {
            switch (t.SP_LEVEL)
            {
                case 0:
                    balancePoint += 10;
                    break;
                case 1:
                    balancePoint -= 10;
                    break;
                case 2:
                    balancePoint -= 20;
                    break;
                case 3:
                    balancePoint -= 30;
                    break;
                case -1:
                    break;
            }
        }
        return balancePoint;
    }
    
    // 查看技能组内是否有重复 false :不合法，有重复  true：合法，无重复
    static bool CheckRepeat(string A1, string A2, string A3, string B1, string B2, string B3, string C1, string C2, string C3)
    {
        // 检查技能重复
        var checkSame = new List<string>
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
        
        for (var i = 0; i < checkSame.Count; i++)
        {
            if (i != checkSame.Count - 1 && SkillConfigTable.GetSkillConfig(checkSame[i]) != null)
            {
                for (var y = i + 1; y < checkSame.Count; y++)
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
        var NormalSkillsOfAList = new List<string>();            
        var _SkillConfigA1 = SkillConfigTable.GetSkillConfig(a1skill);
        var _SkillConfigB1 = SkillConfigTable.GetSkillConfig(a2skill);
        var _SkillConfigC1 = SkillConfigTable.GetSkillConfig(a3skill);
        
        if (_SkillConfigA1 != null && _SkillConfigA1.SP_LEVEL == 0)
            NormalSkillsOfAList.Add(_SkillConfigA1.REAL_NAME);
        if (_SkillConfigB1 != null && _SkillConfigB1.SP_LEVEL == 0)
            NormalSkillsOfAList.Add(_SkillConfigB1.REAL_NAME);
        if (_SkillConfigC1 != null && _SkillConfigC1.SP_LEVEL == 0)
            NormalSkillsOfAList.Add(_SkillConfigC1.REAL_NAME);
        
        return NormalSkillsOfAList.Count == 0 ? SkillEditError.NoNormalStart : SkillEditError.Perfect;
    }
}