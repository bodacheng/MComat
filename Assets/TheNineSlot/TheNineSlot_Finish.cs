using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using Api.Dto.Model;
using mainMenu;
using Skill;

public partial class NineAndTwo
{
    // 根据账户内拥有的技能石来补完当前九宫格
    public static NineAndTwo FixSkillSet(string type, NineAndTwo originSkillSet, int skilllevel, bool baseOnAcc)
    {
        NineAndTwo nineAndTwo = SkillSetRandomFix(type, originSkillSet, 1, baseOnAcc);
        if (nineAndTwo == null)
        {
            Debug.Log("无法根据现在的技能石安排合法补全九宫格");
            return null;
        }
        nineAndTwo.SetSkillLevel(skilllevel);
        nineAndTwo.SortNineAndTwo();
        return nineAndTwo;
    }

    static NineAndTwo SkillSetRandomFix(string focusingtype, NineAndTwo nineAndTwo, int targetSlot, bool baseOnAcc)
    {
        if (targetSlot == 10)
        {
            return nineAndTwo;
        }

        string skillid = null;
        switch (targetSlot)
        {
            case 1:
                skillid = nineAndTwo.a1;
                break;
            case 2:
                skillid = nineAndTwo.a2;
                break;
            case 3:
                skillid = nineAndTwo.a3;
                break;
            case 4:
                skillid = nineAndTwo.b1;
                break;
            case 5:
                skillid = nineAndTwo.b2;
                break;
            case 6:
                skillid = nineAndTwo.b3;
                break;
            case 7:
                skillid = nineAndTwo.c1;
                break;
            case 8:
                skillid = nineAndTwo.c2;
                break;
            case 9:
                skillid = nineAndTwo.c3;
                break;
        }

        // 已经有技能石的格子不做修改
        if (skillid != null)
            return SkillSetRandomFix(focusingtype, nineAndTwo, targetSlot + 1, baseOnAcc);

        skillid = null;

        SkillStonesBox.StoneFilterForm filterForm;

        if (targetSlot == 7)
        {
            // 第一列技能必须有普通技能
            SkillConfig A1skillConfig = SkillConfigTable.GetSkillConfigByID(nineAndTwo.a1);
            SkillConfig B1skillConfig = SkillConfigTable.GetSkillConfigByID(nineAndTwo.b1);
            if (A1skillConfig.SP_LEVEL != 0 && B1skillConfig.SP_LEVEL != 0)
            {
                filterForm = new SkillStonesBox.StoneFilterForm
                {
                    type = focusingtype,
                    exType = new int[1] { 0 },
                    close = false,
                    near = false,
                    far = false
                };
                goto A;
            }
        }

        filterForm = new SkillStonesBox.StoneFilterForm
        {
            type = focusingtype,
            exType = RemainSlotSPLevelCal(nineAndTwo).ToArray(),
            close = false,
            near = false,
            far = false
        };

        A:

        List<string> exceptSKIds = nineAndTwo.SkillIDList();

        if (baseOnAcc)
        {
            StoneOfPlayerInfo stoneInfoModel = Stones.SearchStoneForRandomSet(filterForm, exceptSKIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return null;
            }
            skillid = stoneInfoModel.skillId;
        }
        else
        {
            string skid = RandomSkillIDOfStone(filterForm, exceptSKIds);
            if (skid == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return null;
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
            NineAndTwo.SkillEditError valR = NineAndTwo.CheckEdit(
                nineAndTwo.a1, nineAndTwo.a2, nineAndTwo.a3,
                nineAndTwo.b1, nineAndTwo.b2, nineAndTwo.b3,
                nineAndTwo.c1, nineAndTwo.c2, nineAndTwo.c3);
            if (valR == SkillEditError.Perfect)
                return nineAndTwo;
            else
                return null;
        }
        else
        {
            return SkillSetRandomFix(focusingtype, nineAndTwo, targetSlot + 1, baseOnAcc);
        }
    }
}
