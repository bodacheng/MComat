using System.Linq;
using dataAccess;
using UnityEngine;
using Skill;
using mainMenu;

public partial class SkillSet
{
    // 随机技能组
    public static SkillSet RandomSkillSet(string type, string originSkill, bool baseOnAcc, SkillStonesBox.StoneFilterForm filterForm = null)
    {
        var skillSet = new SkillSet();
        var originSkillConfig = SkillConfigTable.GetSkillConfig(originSkill);
        
        skillSet = RandomSkillSetRec(type, skillSet, 1, originSkillConfig, baseOnAcc, filterForm);
        skillSet.SortNineAndTwo();
        return skillSet;
    }
    
    static SkillStonesBox.StoneFilterForm DecideRemainForm(SkillSet skillSet, string type, SkillStonesBox.StoneFilterForm form)
    {
        // 必然从0开始，可能依次包括1，2，3
        bool useRemainSlotSPLevelCal = false;
        var remainSlotSpLevel = RemainSlotSPLevelCal(skillSet);
        if (form != null)
        {
            var formSps = form.ExType.ToList();
            foreach (var spLevel in formSps)
            {
                if (!remainSlotSpLevel.Contains(spLevel))
                    useRemainSlotSPLevelCal = true;
            }

            if (useRemainSlotSPLevelCal)
            {
                form = new SkillStonesBox.StoneFilterForm
                {
                    Type = type,
                    ExType = RemainSlotSPLevelCal(skillSet).ToArray()
                };
            }
            return form;
        }
        
        form = new SkillStonesBox.StoneFilterForm
        {
            Type = type,
            ExType = RemainSlotSPLevelCal(skillSet).ToArray()
        };

        return form;
    }
    
    /// <summary>
    /// 递归适配技能组
    /// </summary>
    /// <param name="type"></param>
    /// <param name="skillSet"></param>
    /// <param name="filterForm"></param>
    /// <param name="targetSlot"></param>
    /// <param name="origin"></param>
    /// <param name="baseOnAcc"></param>
    /// <returns></returns>
    static SkillSet RandomSkillSetRec(string type, SkillSet skillSet, int targetSlot, SkillConfig origin, bool baseOnAcc, 
        SkillStonesBox.StoneFilterForm filterForm)
    {
        if (targetSlot == 1)
        {
            if (origin != null && origin.SP_LEVEL == 0)
            {
                skillSet.a1 = origin.RECORD_ID;
                return RandomSkillSetRec(type, skillSet, targetSlot + 1, origin, baseOnAcc, filterForm);
            }
        }
        else if (targetSlot == 2)
        {
            if (origin != null && origin.SP_LEVEL != 0)
            {
                skillSet.a2 = origin.RECORD_ID;
                return RandomSkillSetRec(type, skillSet, targetSlot + 1, origin, baseOnAcc, filterForm);
            }
        }
        
        filterForm = DecideRemainForm(skillSet, type, filterForm);
        
        var exceptSkIds = skillSet.SkillIDList();
        string skillId = null;
        if (baseOnAcc)
        {
            var stoneInfoModel = Stones.SearchStoneForRandomSet(filterForm, exceptSkIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return skillSet;
            }
            skillId = stoneInfoModel.SkillId;
        }
        else
        {
            var skid = RandomSkillIDOfStone(filterForm, exceptSkIds);
            if (skid == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return skillSet;
            }
            skillId = skid;
        }
        
        switch (targetSlot)
        {
            case 1:
                skillSet.a1 = skillId;
                break;
            case 2:
                skillSet.a2 = skillId;
                break;
            case 3:
                skillSet.a3 = skillId;
                break;
            case 4:
                skillSet.b1 = skillId;
                break;
            case 5:
                skillSet.b2 = skillId;
                break;
            case 6:
                skillSet.b3 = skillId;
                break;
            case 7:
                skillSet.c1 = skillId;
                break;
            case 8:
                skillSet.c2 = skillId;
                break;
            case 9:
                skillSet.c3 = skillId;
                break;
        }
        if (targetSlot == 9)
        {
            return skillSet;
        }
        return RandomSkillSetRec(type, skillSet, targetSlot + 1, origin, baseOnAcc, filterForm);
    }
}