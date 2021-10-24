using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using Skill;
using mainMenu;

public partial class SkillSet
{
    // 随机技能组
    public static SkillSet RandomSkillSet(string type, string originSkill, int skilllevel, bool baseOnAcc, SkillStonesBox.StoneFilterForm filterForm = null)
    {
        SkillSet nineAndTwo = new SkillSet();
        SkillConfig originSkillConfig = SkillConfigTable.GetSkillConfigByID(originSkill);

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
        
        nineAndTwo = RandomSkillSetRec(type, nineAndTwo, filterForm, 1, originSkillConfig, baseOnAcc);
        nineAndTwo.SetSkillLevel(skilllevel);
        nineAndTwo.SortNineAndTwo();
        return nineAndTwo;
    }

    static SkillSet RandomSkillSetRec(string focusingtype, SkillSet nineAndTwo, SkillStonesBox.StoneFilterForm filterForm, int targetSlot, SkillConfig origin, bool baseOnAcc)
    {
        if (targetSlot == 1)
        {
            if (origin != null && origin.SP_LEVEL == 0)
            {
                nineAndTwo.a1 = origin.RECORD_ID;
                return RandomSkillSetRec(focusingtype, nineAndTwo, filterForm,targetSlot + 1, origin, baseOnAcc);
            }
        }
        else if (targetSlot == 2)
        {
            if (origin != null && origin.SP_LEVEL != 0)
            {
                nineAndTwo.a2 = origin.RECORD_ID;
                return RandomSkillSetRec(focusingtype, nineAndTwo, filterForm, targetSlot + 1, origin, baseOnAcc);
            }
            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = focusingtype,
                exType = RemainSlotSPLevelCal(nineAndTwo).ToArray(),
                close = false,
                near = false,
                far = false
            };
        }
        else
        {
            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = focusingtype,
                exType = RemainSlotSPLevelCal(nineAndTwo).ToArray(),
                close = false,
                near = false,
                far = false
            };
        }

        List<string> exceptSKIds = nineAndTwo.SkillIDList();

        string skillid = null;
        if (baseOnAcc)
        {
            StoneOfPlayerInfo stoneInfoModel = Stones.SearchStoneForRandomSet(filterForm, exceptSKIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return nineAndTwo;
            }
            skillid = stoneInfoModel.skillId;
        }
        else
        {
            string skid = RandomSkillIDOfStone(filterForm, exceptSKIds);
            if (skid == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return nineAndTwo;
            }
            skillid = skid;
        }

        switch (targetSlot)
        {
            case 1:
                nineAndTwo.a1 = skillid;
                break;
            case 2:
                nineAndTwo.a2 = skillid;
                break;
            case 3:
                nineAndTwo.a3 = skillid;
                break;
            case 4:
                nineAndTwo.b1 = skillid;
                break;
            case 5:
                nineAndTwo.b2 = skillid;
                break;
            case 6:
                nineAndTwo.b3 = skillid;
                break;
            case 7:
                nineAndTwo.c1 = skillid;
                break;
            case 8:
                nineAndTwo.c2 = skillid;
                break;
            case 9:
                nineAndTwo.c3 = skillid;
                break;
        }
        if (targetSlot == 9)
        {
            return nineAndTwo;
        }
        else
        {
            return RandomSkillSetRec(focusingtype, nineAndTwo, filterForm, targetSlot + 1, origin, baseOnAcc);
        }
    }
}