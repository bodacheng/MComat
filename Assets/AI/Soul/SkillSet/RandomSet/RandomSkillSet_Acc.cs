using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using Skill;
using Api.Dto.Model;
using mainMenu;

public partial class NineAndTwo
{
    // 随机技能组
    public static NineAndTwo RandomSkillSet(string type, string originSkill, int skilllevel, bool baseOnAcc)
    {
        NineAndTwo nineAndTwo = new NineAndTwo();
        SkillConfig originSkillConfig = SkillConfigTable.GetSkillConfigByID(originSkill);
        nineAndTwo = RandomSkillSetRec(type, nineAndTwo, 1, originSkillConfig, baseOnAcc);
        nineAndTwo.SetSkillLevel(skilllevel);
        nineAndTwo.SortNineAndTwo();
        return nineAndTwo;
    }

    static NineAndTwo RandomSkillSetRec(string focusingtype, NineAndTwo nineAndTwo, int targetSlot, SkillConfig originSkillConfig, bool baseOnAcc)
    {
        SkillStonesBox.StoneFilterForm filterForm;
        if (targetSlot == 1)
        {
            if (originSkillConfig != null && originSkillConfig.SP_LEVEL == 0)
            {
                nineAndTwo.A1skillid = originSkillConfig.RECORD_ID;
                return RandomSkillSetRec(focusingtype, nineAndTwo, targetSlot + 1, originSkillConfig, baseOnAcc);
            }

            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = focusingtype,
                exType = new int[1] { 0 },
                close = false,
                near = false,
                far = false
            };
        }
        else if (targetSlot == 2)
        {
            if (originSkillConfig != null && originSkillConfig.SP_LEVEL != 0)
            {
                nineAndTwo.A2skillid = originSkillConfig.RECORD_ID;
                return RandomSkillSetRec(focusingtype, nineAndTwo, targetSlot + 1, originSkillConfig, baseOnAcc);
            }
            filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = focusingtype,
                exType = NineAndTwo.RemainSlotSPLevelCal(nineAndTwo).ToArray(),
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
                exType = NineAndTwo.RemainSlotSPLevelCal(nineAndTwo).ToArray(),
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
                nineAndTwo.A1skillid = skillid;
                break;
            case 2:
                nineAndTwo.A2skillid = skillid;
                break;
            case 3:
                nineAndTwo.A3skillid = skillid;
                break;
            case 4:
                nineAndTwo.B1skillid = skillid;
                break;
            case 5:
                nineAndTwo.B2skillid = skillid;
                break;
            case 6:
                nineAndTwo.B3skillid = skillid;
                break;
            case 7:
                nineAndTwo.C1skillid = skillid;
                break;
            case 8:
                nineAndTwo.C2skillid = skillid;
                break;
            case 9:
                nineAndTwo.C3skillid = skillid;
                break;
        }
        if (targetSlot == 9)
        {
            return nineAndTwo;
        }
        else
        {
            return RandomSkillSetRec(focusingtype, nineAndTwo, targetSlot + 1, originSkillConfig, baseOnAcc);
        }
    }
}