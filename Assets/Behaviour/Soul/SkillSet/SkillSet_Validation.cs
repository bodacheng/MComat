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
    public static SkillEditError CheckEdit(string a1, string a2, string a3, string b1, string b2, string b3, string c1, string c2, string c3)
    {
        if (!(HasStone(a1) && HasStone(a2) && HasStone(a3) &&
              HasStone(b1) && HasStone(b2) && HasStone(b3) &&
              HasStone(c1) && HasStone(c2) && HasStone(c3)))
        {
            return SkillEditError.NotFull;
        }
        
        // 第一列技能必须有普通技能
        if (CheckStartSKills(a1, b1, c1) == SkillEditError.NoNormalStart)
        {
            return SkillEditError.NoNormalStart;
        }
        
        if (!CheckRepeat(a1, a2, a3, b1, b2, b3, c1, c2, c3))
        {
            return SkillEditError.RepeatedSkill;
        }
        
        bool HasStone(string skillID)
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(skillID);
            return skillConfig != null;
        }
        
        var wholePoint = SkillBalancePoint(a1, a2, a3, b1, b2, b3, c1, c2, c3);
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
    static bool CheckRepeat(string a1, string a2, string a3, string b1, string b2, string b3, string c1, string c2, string c3)
    {
        // 检查技能重复
        var checkSame = new List<string>
        {
            a1,
            a2,
            a3,
            b1,
            b2,
            b3,
            c1,
            c2,
            c3
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
    static SkillEditError CheckStartSKills(string a1Skill, string a2Skill, string a3Skill)
    {
        // 第一列技能必须有普通技能
        var normalSkillsOfAList = new List<string>();            
        var skillConfigA1 = SkillConfigTable.GetSkillConfig(a1Skill);
        var skillConfigB1 = SkillConfigTable.GetSkillConfig(a2Skill);
        var skillConfigC1 = SkillConfigTable.GetSkillConfig(a3Skill);
        
        if (skillConfigA1 != null && skillConfigA1.SP_LEVEL == 0)
            normalSkillsOfAList.Add(skillConfigA1.REAL_NAME);
        if (skillConfigB1 != null && skillConfigB1.SP_LEVEL == 0)
            normalSkillsOfAList.Add(skillConfigB1.REAL_NAME);
        if (skillConfigC1 != null && skillConfigC1.SP_LEVEL == 0)
            normalSkillsOfAList.Add(skillConfigC1.REAL_NAME);
        
        return normalSkillsOfAList.Count == 0 ? SkillEditError.NoNormalStart : SkillEditError.Perfect;
    }
}