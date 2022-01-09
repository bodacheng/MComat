using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using Skill;
using mainMenu;

public partial class SkillSet
{
    // 随机技能组
    public static SkillSet RandomSkillSet(string type, string originSkill, int skillLevel, bool baseOnAcc, SkillStonesBox.StoneFilterForm filterForm = null)
    {
        SkillSet skillSet = new SkillSet();
        SkillConfig originSkillConfig = SkillConfigTable.GetSkillConfig(originSkill);
        
        if (filterForm == null)
        {
            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = type,
                exType = new int[1] { 0 },
                close = false,
                near = false,
                far = false
            };
        }
        
        skillSet = RandomSkillSetRec(type, skillSet, filterForm, 1, originSkillConfig, baseOnAcc);
        skillSet.SetSkillLevel(skillLevel);
        skillSet.SortNineAndTwo();
        return skillSet;
    }

    static SkillSet RandomSkillSetRec(string type, SkillSet skillSet, SkillStonesBox.StoneFilterForm filterForm, int targetSlot, SkillConfig origin, bool baseOnAcc)
    {
        if (targetSlot == 1)
        {
            if (origin != null && origin.SP_LEVEL == 0)
            {
                skillSet.a1 = origin.RECORD_ID;
                return RandomSkillSetRec(type, skillSet, filterForm,targetSlot + 1, origin, baseOnAcc);
            }
        }
        else if (targetSlot == 2)
        {
            if (origin != null && origin.SP_LEVEL != 0)
            {
                skillSet.a2 = origin.RECORD_ID;
                return RandomSkillSetRec(type, skillSet, filterForm, targetSlot + 1, origin, baseOnAcc);
            }
            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = type,
                exType = RemainSlotSPLevelCal(skillSet).ToArray(),
                close = false,
                near = false,
                far = false
            };
        }
        else
        {
            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = type,
                exType = RemainSlotSPLevelCal(skillSet).ToArray(),
                close = false,
                near = false,
                far = false
            };
        }

        List<string> exceptSKIds = skillSet.SkillIDList();
        string skillId = null;
        if (baseOnAcc)
        {
            StoneOfPlayerInfo stoneInfoModel = Stones.SearchStoneForRandomSet(filterForm, exceptSKIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return skillSet;
            }
            skillId = stoneInfoModel.skillId;
        }
        else
        {
            string skid = RandomSkillIDOfStone(filterForm, exceptSKIds);
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
        else
        {
            return RandomSkillSetRec(type, skillSet, filterForm, targetSlot + 1, origin, baseOnAcc);
        }
    }
}